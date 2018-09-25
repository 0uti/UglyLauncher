using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UglyLauncher.Minecraft.Json.Version
{
    public partial class MCVersion
    {
        [JsonProperty("arguments")]
        public MCVersionArguments Arguments { get; set; }

        [JsonProperty("assetIndex")]
        public MCVersionAssetIndex AssetIndex { get; set; }

        [JsonProperty("assets")]
        public string Assets { get; set; }

        [JsonProperty("downloads")]
        public McVersionJsonDownloads Downloads { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("libraries")]
        public List<Library> Libraries { get; set; }

        [JsonProperty("logging")]
        public Logging Logging { get; set; }

        [JsonProperty("mainClass")]
        public string MainClass { get; set; }

        [JsonProperty("minecraftArguments", NullValueHandling = NullValueHandling.Ignore)]
        public string MinecraftArguments { get; set; }

        [JsonProperty("minimumLauncherVersion")]
        public long MinimumLauncherVersion { get; set; }

        [JsonProperty("releaseTime")]
        public DateTimeOffset ReleaseTime { get; set; }

        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class MCVersionArguments
    {
        [JsonProperty("game")]
        public GameElement[] Game { get; set; }

        [JsonProperty("jvm")]
        public JvmElement[] Jvm { get; set; }
    }

    public partial class GameClass
    {
        [JsonProperty("rules")]
        public GameRule[] Rules { get; set; }

        [JsonProperty("value")]
        public MCVersionValue Value { get; set; }
    }

    public partial class GameRule
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("features")]
        public Features Features { get; set; }
    }

    public partial class Features
    {
        [JsonProperty("is_demo_user", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsDemoUser { get; set; }

        [JsonProperty("has_custom_resolution", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasCustomResolution { get; set; }
    }

    public partial class JvmClass
    {
        [JsonProperty("rules")]
        public JvmRule[] Rules { get; set; }

        [JsonProperty("value")]
        public MCVersionValue Value { get; set; }
    }

    public partial class JvmRule
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("os")]
        public PurpleOs Os { get; set; }
    }

    public partial class PurpleOs
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        [JsonProperty("arch", NullValueHandling = NullValueHandling.Ignore)]
        public string Arch { get; set; }
    }

    public partial class MCVersionAssetIndex
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("sha1")]
        public string Sha1 { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("totalSize", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalSize { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class McVersionJsonDownloads
    {
        [JsonProperty("client")]
        public McVersionJsonDownload Client { get; set; }

        [JsonProperty("server")]
        public McVersionJsonDownload Server { get; set; }
    }

    public partial class McVersionJsonDownload
    {
        [JsonProperty("sha1")]
        public string Sha1 { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }
    }

    public partial class Library
    {
        [JsonProperty("downloads")]
        public LibraryDownloads Downloads { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("natives", NullValueHandling = NullValueHandling.Ignore)]
        public Natives Natives { get; set; }

        [JsonProperty("extract", NullValueHandling = NullValueHandling.Ignore)]
        public Extract Extract { get; set; }

        [JsonProperty("rules", NullValueHandling = NullValueHandling.Ignore)]
        public LibraryRule[] Rules { get; set; }
    }

    public partial class LibraryDownloads
    {
        [JsonProperty("artifact")]
        public McVersionJsonDownload Artifact { get; set; }

        [JsonProperty("classifiers", NullValueHandling = NullValueHandling.Ignore)]
        public Classifiers Classifiers { get; set; }
    }

    public partial class Classifiers
    {
        [JsonProperty("javadoc", NullValueHandling = NullValueHandling.Ignore)]
        public McVersionJsonDownload Javadoc { get; set; }

        [JsonProperty("natives-linux", NullValueHandling = NullValueHandling.Ignore)]
        public McVersionJsonDownload nativeslinux { get; set; }

        [JsonProperty("natives-macos", NullValueHandling = NullValueHandling.Ignore)]
        public McVersionJsonDownload nativesmacos { get; set; }

        [JsonProperty("natives-windows", NullValueHandling = NullValueHandling.Ignore)]
        public McVersionJsonDownload nativeswindows { get; set; }

        [JsonProperty("natives-windows-32", NullValueHandling = NullValueHandling.Ignore)]
        public McVersionJsonDownload nativeswindows32 { get; set; }

        [JsonProperty("natives-windows-64", NullValueHandling = NullValueHandling.Ignore)]
        public McVersionJsonDownload nativeswindows64 { get; set; }

        [JsonProperty("sources")]
        public McVersionJsonDownload Sources { get; set; }

        [JsonProperty("natives-osx", NullValueHandling = NullValueHandling.Ignore)]
        public McVersionJsonDownload NativesOsx { get; set; }
    }

    public partial class Extract
    {
        [JsonProperty("exclude")]
        public string[] Exclude { get; set; }
    }

    public partial class Natives
    {
        [JsonProperty("linux", NullValueHandling = NullValueHandling.Ignore)]
        public string Linux { get; set; }

        [JsonProperty("osx", NullValueHandling = NullValueHandling.Ignore)]
        public string Osx { get; set; }

        [JsonProperty("windows", NullValueHandling = NullValueHandling.Ignore)]
        public string Windows { get; set; }
    }

    public partial class LibraryRule
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("os")]
        public FluffyOs Os { get; set; }
    }

    public partial class FluffyOs
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Logging
    {
        [JsonProperty("client")]
        public LoggingClient Client { get; set; }
    }

    public partial class LoggingClient
    {
        [JsonProperty("argument")]
        public string Argument { get; set; }

        [JsonProperty("file")]
        public MCVersionAssetIndex File { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial struct MCVersionValue
    {
        public string String;
        public string[] StringArray;

        public static implicit operator MCVersionValue(string String) => new MCVersionValue { String = String };
        public static implicit operator MCVersionValue(string[] StringArray) => new MCVersionValue { StringArray = StringArray };
    }


    public partial struct GameElement
    {
        public GameClass GameClass;
        public string String;

        public static implicit operator GameElement(GameClass GameClass) => new GameElement { GameClass = GameClass };
        public static implicit operator GameElement(string String) => new GameElement { String = String };
    }

    public partial struct JvmElement
    {
        public JvmClass JvmClass;
        public string String;

        public static implicit operator JvmElement(JvmClass JvmClass) => new JvmElement { JvmClass = JvmClass };
        public static implicit operator JvmElement(string String) => new JvmElement { String = String };
    }



    public partial class MCVersion
    {
        public static MCVersion FromJson(string json) => JsonConvert.DeserializeObject<MCVersion>(json, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                GameElementConverter.Singleton,
                ValueConverter.Singleton,
                JvmElementConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class GameElementConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(GameElement) || t == typeof(GameElement?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new GameElement { String = stringValue };
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<GameClass>(reader);
                    return new GameElement { GameClass = objectValue };
            }
            throw new Exception("Cannot unmarshal type GameElement");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (GameElement)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.GameClass != null)
            {
                serializer.Serialize(writer, value.GameClass);
                return;
            }
            throw new Exception("Cannot marshal type GameElement");
        }

        public static readonly GameElementConverter Singleton = new GameElementConverter();
    }

    internal class ValueConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MCVersionValue) || t == typeof(MCVersionValue?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new MCVersionValue { String = stringValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<string[]>(reader);
                    return new MCVersionValue { StringArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type Value");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (MCVersionValue)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.StringArray != null)
            {
                serializer.Serialize(writer, value.StringArray);
                return;
            }
            throw new Exception("Cannot marshal type Value");
        }

        public static readonly ValueConverter Singleton = new ValueConverter();
    }

    internal class JvmElementConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(JvmElement) || t == typeof(JvmElement?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new JvmElement { String = stringValue };
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<JvmClass>(reader);
                    return new JvmElement { JvmClass = objectValue };
            }
            throw new Exception("Cannot unmarshal type JvmElement");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (JvmElement)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.JvmClass != null)
            {
                serializer.Serialize(writer, value.JvmClass);
                return;
            }
            throw new Exception("Cannot marshal type JvmElement");
        }

        public static readonly JvmElementConverter Singleton = new JvmElementConverter();
    }
}
