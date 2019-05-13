using MusicInfoLibrary.Data;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

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
            if (string.IsNullOrWhiteSpace(id))
                return "";
            var url = $"http://musicbrainz.org/ws/2/artist/{id}?&fmt=json&inc=url-rels+release-groups";
            var response = await _httpClient.GetStringAsync(url);
            try
            {
                var musicBrainz = JsonConvert.DeserializeObject<MusicBrainz>(response);
                var wikiData = await GetWikiDataAsync(musicBrainz);
            }
            catch (Exception ex)
            {
                response = "Error";
            }
            return response;
        }

        private async Task<string> GetWikiDataAsync(MusicBrainz musicBrainz)
        {
            foreach (var relation in musicBrainz.Relations)
            {
                if (relation.Type == "wikidata")
                {

                }
            }
        }
    }
}
