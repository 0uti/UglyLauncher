using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace UglyLauncher.Minecraft.Authentication.Json.AuthenticatieError
{
    public partial class AuthenticatieError
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("cause")]
        public string Cause { get; set; }
    }

    public partial class AuthenticatieError
    {
        public static AuthenticatieError FromJson(string json) => JsonConvert.DeserializeObject<AuthenticatieError>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this AuthenticatieError self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
