using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MusicInfoLibrary.Data
{
    public partial class MusicBrainz
    {
        [JsonProperty("area", NullValueHandling = NullValueHandling.Ignore)]
        public Area Area { get; set; }

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("type-id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? TypeId { get; set; }

        [JsonProperty("disambiguation", NullValueHandling = NullValueHandling.Ignore)]
        public string Disambiguation { get; set; }

        [JsonProperty("life-span", NullValueHandling = NullValueHandling.Ignore)]
        public LifeSpan LifeSpan { get; set; }

        [JsonProperty("isnis", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Isnis { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("gender-id")]
        public Guid? GenderId { get; set; }

        [JsonProperty("ipis", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Ipis { get; set; }

        [JsonProperty("release-groups", NullValueHandling = NullValueHandling.Ignore)]
        public List<ReleaseGroup> ReleaseGroups { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Id { get; set; }

        [JsonProperty("relations", NullValueHandling = NullValueHandling.Ignore)]
        public List<Relation> Relations { get; set; }

        [JsonProperty("begin_area", NullValueHandling = NullValueHandling.Ignore)]
        public Area BeginArea { get; set; }

        [JsonProperty("sort-name", NullValueHandling = NullValueHandling.Ignore)]
        public string SortName { get; set; }

        [JsonProperty("end_area")]
        public object EndArea { get; set; }
    }

    public partial class Area
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Id { get; set; }

        [JsonProperty("sort-name", NullValueHandling = NullValueHandling.Ignore)]
        public string SortName { get; set; }

        [JsonProperty("iso-3166-1-codes", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Iso31661Codes { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("disambiguation", NullValueHandling = NullValueHandling.Ignore)]
        public string Disambiguation { get; set; }
    }

    public partial class LifeSpan
    {
        [JsonProperty("end", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? End { get; set; }

        [JsonProperty("ended", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Ended { get; set; }

        [JsonProperty("begin", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Begin { get; set; }
    }

    public partial class Relation
    {
        [JsonProperty("attribute-ids", NullValueHandling = NullValueHandling.Ignore)]
        public Attribute AttributeIds { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public Url Url { get; set; }

        [JsonProperty("type-id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? TypeId { get; set; }

        [JsonProperty("source-credit", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceCredit { get; set; }

        [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
        public string Direction { get; set; }

        [JsonProperty("attribute-values", NullValueHandling = NullValueHandling.Ignore)]
        public Attribute AttributeValues { get; set; }

        [JsonProperty("end")]
        public DateTimeOffset? End { get; set; }

        [JsonProperty("target-credit", NullValueHandling = NullValueHandling.Ignore)]
        public string TargetCredit { get; set; }

        [JsonProperty("begin")]
        public DateTimeOffset? Begin { get; set; }

        [JsonProperty("attributes", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Attributes { get; set; }

        [JsonProperty("ended", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Ended { get; set; }

        [JsonProperty("target-type", NullValueHandling = NullValueHandling.Ignore)]
        public string TargetType { get; set; }
    }

    public partial class Attribute
    {
    }

    public partial class Url
    {
        [JsonProperty("resource", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Resource { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Id { get; set; }
    }

    public partial class ReleaseGroup
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Id { get; set; }

        [JsonProperty("primary-type", NullValueHandling = NullValueHandling.Ignore)]
        public string PrimaryType { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("secondary-type-ids", NullValueHandling = NullValueHandling.Ignore)]
        public List<Guid> SecondaryTypeIds { get; set; }

        [JsonProperty("secondary-types", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> SecondaryTypes { get; set; }

        [JsonProperty("primary-type-id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? PrimaryTypeId { get; set; }

        [JsonProperty("first-release-date", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstReleaseDate { get; set; }

        [JsonProperty("disambiguation", NullValueHandling = NullValueHandling.Ignore)]
        public string Disambiguation { get; set; }
    }

    public partial class MusicBrainz
    {
        public static MusicBrainz FromJson(string json) => JsonConvert.DeserializeObject<MusicBrainz>(json, MusicInfoLibrary.Data.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this MusicBrainz self) => JsonConvert.SerializeObject(self, MusicInfoLibrary.Data.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
 }
