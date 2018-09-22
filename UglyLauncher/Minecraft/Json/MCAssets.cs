using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UglyLauncher.Minecraft.Json.Assets
{
   
    public partial class MCAssets
    {
        [JsonProperty("objects")]
        public Dictionary<string, MCAssetObject> Objects { get; set; }
        [JsonProperty("virtual", NullValueHandling = NullValueHandling.Ignore)]
        public bool Virtual { get; set; }
    }

    public partial class MCAssetObject
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }
    }

    public partial class MCAssets
    {
        public static MCAssets FromJson(string json) => JsonConvert.DeserializeObject<MCAssets>(json, Converter.Settings);
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
