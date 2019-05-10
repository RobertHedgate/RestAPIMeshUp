using System;
using System.Threading.Tasks;

namespace MusicInfoLibrary
{
    public class MusicInfo
    {
        public async Task<string> GetArtistInfoAsync(string id)
        {
            return "{ \"value1\", \"value2\" }";
        }
    }
}
