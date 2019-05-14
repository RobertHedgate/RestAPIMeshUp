using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicInfoLibrary.Data
{
    public class AlbumData
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
