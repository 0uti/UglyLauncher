using Newtonsoft.Json;

namespace UglyLauncher.Minecraft.Json.Pack
{
    public class MCPackCurseFile
    {
        [JsonProperty("projectID")]
        public int ProjectID { get; set; }

        [JsonProperty("fileID")]
        public int FileID { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }
    }
}
