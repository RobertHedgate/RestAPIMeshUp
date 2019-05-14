using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MusicInfoLibrary;

namespace RestAPIMashUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private MusicInfo _musicInfo = new MusicInfo();
        private IMemoryCache _cache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> Get(string id = "", bool usecache = true)
        {
#if DEBUG
            if (id == "")
                id = "0383dadf-2a4e-4d10-a46a-e9e041da8eb3";
#endif
            if (string.IsNullOrWhiteSpace(id))
                return "Error: Have to specify ?id=";

            var result = usecache ? (string)_cache.Get(id) : "";
            if (string.IsNullOrEmpty(result))
            {
                result = await GetValueFromMusicInfoAsync(id);
#if DEBUG
                // cache value for 5 min during development
                _cache.Set<string>(id, result, new TimeSpan(0, 5, 0));
#else
                // ToDo: is 24 hours a valid time to get new data? Possible to have wrong data during that time.
                _cache.Set<string>(id, result, new TimeSpan(24, 0, 0));
#endif
            }

            return result;
        }

        private async Task<string> GetValueFromMusicInfoAsync(string id)
        {
            return await _musicInfo.GetArtistInfoAsync(id);
        }
    }
}
