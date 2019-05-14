using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicInfoLibrary;
using MusicInfoLibrary.Data;
using Newtonsoft.Json;

namespace UnitTestProject
{
    [TestClass]
    public class MusicInfoLibraryTest
    {
        [TestMethod]
        public void CleanUpArtistDataRemovesUnWantedSyntax()
        {
            var musicInfo = new MusicInfo();

            var newArtistData = musicInfo.CleanUpArtistData("<p class=\"mw-empty-elt\"> \n</p>\n\n<p class=\"mw-empty-elt\">\n\n</p>\n<p><b>Queen</b>");
            Assert.AreEqual("<p><b>Queen</b>", newArtistData);
        }

        [TestMethod]
        public void CleanUpArtistDataStaysSame()
        {
            var musicInfo = new MusicInfo();

            var newArtistData = musicInfo.CleanUpArtistData("<p><b>Queen</b>");
            Assert.AreEqual("<p><b>Queen</b>", newArtistData);
        }

        [TestMethod]
        public void GetDataWithRealMusicBrainzId()
        {
            var musicInfo = new MusicInfo();

            var data = musicInfo.GetArtistInfoAsync("0383dadf-2a4e-4d10-a46a-e9e041da8eb3").Result;
            var responseData = JsonConvert.DeserializeObject<ResponseData>(data);
            Assert.AreEqual("0383dadf-2a4e-4d10-a46a-e9e041da8eb3", responseData.Mbid);
        }

        [TestMethod]
        public void GetErrorWithIncorrectMusicBrainzId()
        {
            var musicInfo = new MusicInfo();

            var data = musicInfo.GetArtistInfoAsync("BadMusicBrainzId").Result;
            Assert.AreEqual("{\"Error\": \"Something went wrong\"}", data);
        }

        [TestMethod]
        public void GetErrorWithEmptyMusicBrainzId()
        {
            var musicInfo = new MusicInfo();

            var data = musicInfo.GetArtistInfoAsync("").Result;
            Assert.AreEqual("{\"Error\": \"id is null or empty\"}", data);
        }

    }
}
