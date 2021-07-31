using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace UglyLauncher.Minecraft.Files.Forge.ForgeInstaller
{
    public partial class ForgeInstaller
    {
        [JsonProperty("install")]
        public Install Install { get; set; }

        [JsonProperty("versionInfo")]
        public VersionInfo VersionInfo { get; set; }

        [JsonProperty("optionals")]
        public object[] Optionals { get; set; }
    }

    public partial class Install
    {
        [JsonProperty("profileName")]
        public string ProfileName { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("filePath")]
        public string FilePath { get; set; }

        [JsonProperty("welcome")]
        public string Welcome { get; set; }

        [JsonProperty("minecraft")]
        public string Minecraft { get; set; }

        [JsonProperty("mirrorList")]
        public string MirrorList { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("modList")]
        public string ModList { get; set; }
    }

    public partial class VersionInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("releaseTime")]
        public string ReleaseTime { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("minecraftArguments")]
        public string MinecraftArguments { get; set; }

        [JsonProperty("mainClass")]
        public string MainClass { get; set; }

        [JsonProperty("inheritsFrom")]
        public string InheritsFrom { get; set; }

        [JsonProperty("jar")]
        public string Jar { get; set; }

        [JsonProperty("logging")]
        public Logging Logging { get; set; }

        [JsonProperty("libraries")]
        public Library[] Libraries { get; set; }
    }

    public partial class Library
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Url { get; set; }

        [JsonProperty("serverreq", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Serverreq { get; set; }

        [JsonProperty("checksums", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Checksums { get; set; }

        [JsonProperty("clientreq", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Clientreq { get; set; }
    }

    public partial class Logging
    {
    }

    public partial class ForgeInstaller
    {
        public static ForgeInstaller FromJson(string json) => JsonConvert.DeserializeObject<ForgeInstaller>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ForgeInstaller self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
