using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace UglyLauncher.Minecraft.Json.AvailablePacks
{
    public partial class MCAvailablePacks
    {
        [JsonProperty("packs")]
        public MCAvailablePack[] Packs { get; set; }
    }

    public partial class MCAvailablePack
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("recommended_version")]
        public string RecommendedVersion { get; set; }

        [JsonProperty("versions")]
        public MCAvailablePackVersion[] Versions { get; set; }
    }

    public partial class MCAvailablePackVersion
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("downloadZip")]
        public bool DownloadZip { get; set; }
    }

    public partial class MCAvailablePacks
    {
        public static MCAvailablePacks FromJson(string json) => JsonConvert.DeserializeObject<MCAvailablePacks>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this MCAvailablePacks self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
