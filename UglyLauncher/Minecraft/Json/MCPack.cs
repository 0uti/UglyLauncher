using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UglyLauncher.Minecraft.Json.Pack
{
    public partial class MCPack
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("mc_version")]
        public string MCVersion { get; set; }

        [JsonProperty("forge_version")]
        public string ForgeVersion { get; set; }
    }

    public partial class MCPack
    {
        public static MCPack FromJson(string json) => JsonConvert.DeserializeObject<MCPack>(json, Converter.Settings);
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
