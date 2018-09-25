using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyLauncher.Minecraft.Json.Version;

namespace UglyLauncher.Minecraft.Json.MCForgeVersion
{
    public partial class MCForgeVersion
    {
        [JsonProperty("arguments")]
        public MCForgeArguments Arguments { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("libraries")]
        public List<ForgeLibrary> Libraries { get; set; }

        /*
        [JsonProperty("logging")]
        public Logging Logging { get; set; }
        */
        [JsonProperty("mainClass")]
        public string MainClass { get; set; }

        [JsonProperty("releaseTime")]
        public DateTimeOffset ReleaseTime { get; set; }

        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

    }

    public partial class MCForgeArguments
    {
        [JsonProperty("game")]
        public GameElement[] Game { get; set; }

    }

    public partial class ForgeLibrary
    {
        [JsonProperty("downloads")]
        public McVersionJsonDownload Downloads { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }

    public partial class MCForgeVersion
    {
        public static MCForgeVersion FromJson(string json) => JsonConvert.DeserializeObject<MCForgeVersion>(json, Converter.Settings);
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



}
