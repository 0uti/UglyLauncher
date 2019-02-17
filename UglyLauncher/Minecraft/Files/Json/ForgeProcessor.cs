using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UglyLauncher.Minecraft.Files.Json.GameVersion;

namespace UglyLauncher.Minecraft.Files.Json.ForgeProcessor
{
    public partial class ForgeProcessor
    {
        [JsonProperty("_comment_")]
        public string[] Comment { get; set; }

        [JsonProperty("spec")]
        public long Spec { get; set; }

        [JsonProperty("profile")]
        public string Profile { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("json")]
        public string Json { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("minecraft")]
        public string Minecraft { get; set; }

        [JsonProperty("welcome")]
        public string Welcome { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, Side> Data { get; set; }

        [JsonProperty("processors")]
        public Processor[] Processors { get; set; }

        [JsonProperty("libraries")]
        public Library[] Libraries { get; set; }
    }

    public partial class Side
    {
        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("server")]
        public string Server { get; set; }
    }
    
    public partial class Processor
    {
        [JsonProperty("jar")]
        public string Jar { get; set; }

        [JsonProperty("classpath")]
        public string[] Classpath { get; set; }

        [JsonProperty("args")]
        public string[] Args { get; set; }

        [JsonProperty("outputs", NullValueHandling = NullValueHandling.Ignore)]
        public Outputs Outputs { get; set; }
    }

    public partial class Outputs
    {
        [JsonProperty("{MC_SLIM}", NullValueHandling = NullValueHandling.Ignore)]
        public string McSlim { get; set; }

        [JsonProperty("{MC_DATA}", NullValueHandling = NullValueHandling.Ignore)]
        public string McData { get; set; }

        [JsonProperty("{MC_EXTRA}", NullValueHandling = NullValueHandling.Ignore)]
        public string McExtra { get; set; }

        [JsonProperty("{PATCHED}", NullValueHandling = NullValueHandling.Ignore)]
        public string Patched { get; set; }
    }

    public partial class ForgeProcessor
    {
        public static ForgeProcessor FromJson(string json) => JsonConvert.DeserializeObject<ForgeProcessor>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ForgeProcessor self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
