using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UglyLauncher.Minecraft.Files.Json.Assets
{
   
    public partial class Assets
    {
        [JsonProperty("objects")]
        public Dictionary<string, AssetObject> Objects { get; set; }
        [JsonProperty("virtual", NullValueHandling = NullValueHandling.Ignore)]
        public bool Virtual { get; set; }
    }

    public partial class AssetObject
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }
    }

    public partial class Assets
    {
        public static Assets FromJson(string json) => JsonConvert.DeserializeObject<Assets>(json, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
