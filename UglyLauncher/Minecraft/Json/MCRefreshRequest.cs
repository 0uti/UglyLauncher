using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace UglyLauncher.Minecraft.Json.MCRefreshRequest
{


    public partial class MCRefreshRequest
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("clientToken")]
        public string ClientToken { get; set; }

        [JsonProperty("requestUser")]
        public bool RequestUser { get; set; }
    }

    public partial class SelectedProfile
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class MCRefreshRequest
    {
        public static MCRefreshRequest FromJson(string json) => JsonConvert.DeserializeObject<MCRefreshRequest>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this MCRefreshRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
