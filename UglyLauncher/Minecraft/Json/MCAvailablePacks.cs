using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UglyLauncher.Minecraft.Json.AvailablePacks
{

    public partial class MCAvailablePacks
    {
        [JsonProperty("packs")]
        public IList<MCAvailablePack> Packs { get; set; }
    }

    public partial class MCAvailablePack
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("recommended_version")]
        public string RecommendedVersion { get; set; }

        [JsonProperty("versions")]
        public IList<string> Versions { get; set; }
    }

    public partial class MCAvailablePacks
    {
        public static MCAvailablePacks FromJson(string json) => JsonConvert.DeserializeObject<MCAvailablePacks>(json, Converter.Settings);
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


