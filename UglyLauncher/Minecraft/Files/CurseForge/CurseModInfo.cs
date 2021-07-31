using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace UglyLauncher.Minecraft.Files.CurseForge.CurseModInfo
{
    public class CurseModInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("fileDate")]
        public DateTime FileDate { get; set; }

        [JsonProperty("fileLength")]
        public int FileLength { get; set; }

        [JsonProperty("releaseType")]
        public int ReleaseType { get; set; }

        [JsonProperty("fileStatus")]
        public int FileStatus { get; set; }

        [JsonProperty("downloadUrl")]
        public string DownloadUrl { get; set; }

        [JsonProperty("isAlternate")]
        public bool IsAlternate { get; set; }

        [JsonProperty("alternateFileId")]
        public int AlternateFileId { get; set; }

        [JsonProperty("dependencies")]
        public List<object> Dependencies { get; set; }

        [JsonProperty("isAvailable")]
        public bool IsAvailable { get; set; }

        [JsonProperty("modules")]
        public List<CurseModInfoModule> Modules { get; set; }

        [JsonProperty("packageFingerprint")]
        public long PackageFingerprint { get; set; }

        [JsonProperty("gameVersion")]
        public List<string> GameVersion { get; set; }

        [JsonProperty("installMetadata")]
        public object InstallMetadata { get; set; }

        [JsonProperty("serverPackFileId")]
        public object ServerPackFileId { get; set; }

        [JsonProperty("hasInstallScript")]
        public bool HasInstallScript { get; set; }

        [JsonProperty("gameVersionDateReleased")]
        public DateTime GameVersionDateReleased { get; set; }

        [JsonProperty("gameVersionFlavor")]
        public object GameVersionFlavor { get; set; }

        public static CurseModInfo FromJson(
          string json)
        {
            return JsonConvert.DeserializeObject<CurseModInfo>(json, Converter.Settings);
        }
    }

    public class CurseModInfoModule
    {
        [JsonProperty("foldername")]
        public string Foldername { get; set; }

        [JsonProperty("fingerprint")]
        public object Fingerprint { get; set; }
    }

    public static class Serialize
    {
        public static string ToJson(this CurseModInfo self) => JsonConvert.SerializeObject((object)self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings;

        static Converter()
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
            serializerSettings.DateParseHandling = DateParseHandling.None;
            serializerSettings.Converters.Add((JsonConverter)new IsoDateTimeConverter()
            {
                DateTimeStyles = DateTimeStyles.AssumeUniversal
            });
            Converter.Settings = serializerSettings;
        }
    }
}
