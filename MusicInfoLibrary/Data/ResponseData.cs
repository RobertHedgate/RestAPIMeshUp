using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicInfoLibrary.Data
{
    public class ResponseData
    {
        [JsonProperty("mbid")]
        public string Mbid { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("album")]
        public List<AlbumData> Albums { get; set; }
    }
}
