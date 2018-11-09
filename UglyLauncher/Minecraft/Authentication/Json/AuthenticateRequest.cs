using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace UglyLauncher.Minecraft.Authentication.Json.AuthenticateRequest
{
    public partial class AuthenticateRequest
    {
        [JsonProperty("agent")]
        public Agent Agent = new Agent();

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("clientToken")]
        public string ClientToken { get; set; }

        [JsonProperty("requestUser")]
        public bool RequestUser { get; set; }
    }

    public partial class Agent
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }
    }

    public partial class AuthenticateRequest
    {
        public static AuthenticateRequest FromJson(string json) => JsonConvert.DeserializeObject<AuthenticateRequest>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this AuthenticateRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
