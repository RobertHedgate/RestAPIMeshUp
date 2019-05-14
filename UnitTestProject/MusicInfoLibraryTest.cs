using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicInfoLibrary;

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

    }
}
