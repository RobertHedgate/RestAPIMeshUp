using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicInfoLibrary;

namespace RestAPIMashUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private MusicInfo _musicInfo = new MusicInfo();

        // GET api/values
        //[HttpGet("{id}")]
        [HttpGet]
        public async Task<ActionResult<string>> Get(string id = "")
        {
#if DEBUG
            if (id == "")
                id = "0383dadf-2a4e-4d10-a46a-e9e041da8eb3";
#endif
            if (string.IsNullOrWhiteSpace(id))
                return "Error: Have to specify ?id=";
            return await GetValueFromMusicInfoAsync(id);
        }

        private async Task<string> GetValueFromMusicInfoAsync(string id)
        {
            return await _musicInfo.GetArtistInfoAsync(id);
        }
    }
}
