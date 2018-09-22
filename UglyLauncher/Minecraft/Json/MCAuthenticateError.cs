using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UglyLauncher.Minecraft.Json.MCAuthenticateError
{
    public partial class MCAuthenticatieError
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("cause")]
        public string Cause { get; set; }
    }

    public partial class MCAuthenticatieError
    {
        public static MCAuthenticatieError FromJson(string json) => JsonConvert.DeserializeObject<MCAuthenticatieError>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this MCAuthenticatieError self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
