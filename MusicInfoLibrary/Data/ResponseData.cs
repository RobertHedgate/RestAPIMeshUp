using System;
using System.Collections.Generic;
using System.Text;

namespace MusicInfoLibrary.Data
{
    public class ResponseData
    {
        public string Mbid { get; set; }
        public string Description { get; set; }
        public List<AlbumData> Albums { get; set; }
    }
}
