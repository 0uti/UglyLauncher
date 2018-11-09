using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UglyLauncher.Minecraft.Files.Json.GameVersionList
{
    public partial class GameVersionList
    {
        [JsonProperty("latest")]
        public VersionsLatest Latest { get; set; }

        [JsonProperty("versions")]
        public VersionsVersion[] Versions { get; set; }
    }

    public partial class VersionsLatest
    {
        [JsonProperty("release")]
        public string Release { get; set; }

        [JsonProperty("snapshot")]
        public string Snapshot { get; set; }
    }

    public partial class VersionsVersion
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("releaseTime")]
        public DateTimeOffset ReleaseTime { get; set; }
    }

    public enum TypeEnum { OldAlpha, OldBeta, Release, Snapshot };

    public partial class GameVersionList
    {
        public static GameVersionList FromJson(string json) => JsonConvert.DeserializeObject<GameVersionList>(json, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "old_alpha":
                    return TypeEnum.OldAlpha;
                case "old_beta":
                    return TypeEnum.OldBeta;
                case "release":
                    return TypeEnum.Release;
                case "snapshot":
                    return TypeEnum.Snapshot;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.OldAlpha:
                    serializer.Serialize(writer, "old_alpha");
                    return;
                case TypeEnum.OldBeta:
                    serializer.Serialize(writer, "old_beta");
                    return;
                case TypeEnum.Release:
                    serializer.Serialize(writer, "release");
                    return;
                case TypeEnum.Snapshot:
                    serializer.Serialize(writer, "snapshot");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}
