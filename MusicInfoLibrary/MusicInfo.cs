using MusicInfoLibrary.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MusicInfoLibrary
{
    public class MusicInfo
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public MusicInfo()
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "RestAPIMashUp/1 ( robert.hedgate@hotmail.com )");
        }

        public async Task<string> GetArtistInfoAsync(string id)
        {
            var response = "";
            if (string.IsNullOrWhiteSpace(id))
                return "{\"Error\": \"id is null or empty\"}";
            var url = $"http://musicbrainz.org/ws/2/artist/{id}?&fmt=json&inc=url-rels+release-groups";
            try
            {
                var musicBrainzJson = await _httpClient.GetStringAsync(url);
                var musicBrainz = JsonConvert.DeserializeObject<MusicBrainz>(musicBrainzJson);
                var wikiDataTask = GetWikiDataAsync(musicBrainz);
                var albumListTask = GetAlbumListWithCoverAsync(musicBrainz);

                await Task.WhenAll(wikiDataTask, albumListTask);

                var wikiData = await wikiDataTask;
                var albumList = await albumListTask;

                var responseData = new ResponseData
                {
                    Mbid = musicBrainz.Id.ToString(),
                    Description = wikiData,
                    Albums = albumList
                };
                return JsonConvert.SerializeObject(responseData);
            }
            catch (Exception ex)
            {
                response = "{\"Error\": \"Something went wrong\"}";
            }
            return response;
        }

        private async Task<List<AlbumData>> GetAlbumListWithCoverAsync(MusicBrainz musicBrainz)
        {
            var albumDataList = new List<AlbumData>();

            var listOfTasks = new List<Task<AlbumData>>();
            foreach (var album in musicBrainz.ReleaseGroups)
            {
                if (album.PrimaryType.ToLower() == "album")
                {
                    listOfTasks.Add(GetAlbumDataAsync(album));
                }
            }
            var list = await Task.WhenAll<AlbumData>(listOfTasks);
            albumDataList.AddRange(list);

            return albumDataList;
        }

        private async Task<AlbumData> GetAlbumDataAsync(ReleaseGroup album)
        {
            var albumData = new AlbumData
            {
                Title = album.Title,
                Id = album.Id.ToString()
            };
            var url = $"http://coverartarchive.org/release-group/{album.Id}";
            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var coverArt = JsonConvert.DeserializeObject<CoverArtData>(response);
                foreach (var image in coverArt.images)
                {
                    if (image.front)
                    {
                        albumData.Image = image.image;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Some albums doesn´t have a image. Image will be null.
            }
            return albumData;
        }

        private async Task<string> GetWikiDataAsync(MusicBrainz musicBrainz)
        {
            foreach (var relation in musicBrainz.Relations)
            {
                if (relation.Type.ToLower() == "wikidata")
                {
                    var url = relation.Url.Resource.ToString();
                    var id = url.Split('/').LastOrDefault();
                    var wikiUrl = $"https://www.wikidata.org/w/api.php?action=wbgetentities&ids={id}&format=json&props=sitelinks";
                    var response = await _httpClient.GetStringAsync(wikiUrl);
                    var wikiData = DecodeWikiData(response, id);
                    return await GetWikipediaAsync(wikiData.Title);
                }
                if (relation.Type.ToLower() == "wikipedia")
                {
                    // ToDo: Where do I get the artist title here?
                    // Have not seen any result which have a wikipedia link. Guessing that this is correct.
                    var url = relation.Url.Resource.ToString();
                    var title = url.Split('/').LastOrDefault();
                    return await GetWikipediaAsync(title);
                }
            }

            return "";
        }

        private WikiData DecodeWikiData(string response, string id)
        {
            var json = JObject.Parse(response);
            var entities = json.SelectToken("entities");
            var ids = entities.SelectToken(id);
            var siteLinks = ids.SelectToken("sitelinks");
            foreach (var site in siteLinks)
            {
                foreach (var data in site)
                {
                    var wikiSite = (string)data.SelectToken("site");
                    if (wikiSite.ToLower() == "enwiki")
                    {
                        var title = (string)data.SelectToken("title");
                        return new WikiData { Id = id, Title = title };
                    }
                }
            }

            return new WikiData { Id = "", Title = "" };
        }

        private async Task<string> GetWikipediaAsync(string title)
        {
            var urlTitle = HttpUtility.UrlEncode(title);
            var url = $"https://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&exintro=true&redirects=true&titles={urlTitle}";
            var response = await _httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);
            var query = json.SelectToken("query");
            var pages = query.SelectToken("pages");
            foreach (var page in pages)
            {
                foreach (var data in page)
                {
                    var artistData = (string)data.SelectToken("extract");
                    artistData = CleanUpArtistData(artistData);
                    return artistData;
                }
            }

            return "";
        }

        public string CleanUpArtistData(string artistData)
        {
            // ToDo: Is there a better way to remove unwanted syntax?
            artistData = artistData.Replace("<p class=\"mw-empty-elt\"> \n</p>\n\n<p class=\"mw-empty-elt\">\n\n</p>\n", "");
            artistData = artistData.Replace("<p class=\"mw-empty-elt\">\n</p>\n\n<p class=\"mw-empty-elt\">\n\n</p>\n", "");
            artistData = artistData.Replace("\n\n\n", "");
            return artistData;
        }
    }
}
