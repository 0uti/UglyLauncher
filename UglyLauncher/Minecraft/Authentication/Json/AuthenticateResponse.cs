using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace UglyLauncher.Minecraft.Authentication.Json.AuthenticateResponse
{
    public partial class AuthenticateResponse
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("clientToken")]
        public string ClientToken { get; set; }

        [JsonProperty("availableProfiles")]
        public Profile[] AvailableProfiles { get; set; }

        [JsonProperty("selectedProfile")]
        public Profile SelectedProfile { get; set; }

        [JsonProperty("user")]
        public User[] User { get; set; }
    }

    public partial class Profile
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("legacy")]
        public bool Legacy { get; set; }
    }

    public partial class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("properties")]
        public Property[] Properties { get; set; }
    }

    public partial class Property
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class AuthenticateResponse
    {
        public static AuthenticateResponse FromJson(string json) => JsonConvert.DeserializeObject<AuthenticateResponse>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this AuthenticateResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
