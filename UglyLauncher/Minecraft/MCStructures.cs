using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Minecraft
{
    /// <summary>
    /// The JSON authenticate request construct.
    /// </summary>
    [DataContract]
    public class MCAuthenticate_Request
    {
        [DataMember]
        public Agent agent;
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string clientToken { get; set; }

        [DataContractAttribute]
        public struct Agent
        {
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public string version { get; set; }
        }
    }

    /// <summary>
    /// The JSON authenticate response construct.
    /// </summary>
    [DataContract]
    public class MCAuthenticate_Response
    {
        [DataMember]
        public string accessToken { get; set; }
        [DataMember]
        public string clientToken { get; set; }
        [DataMember]
        public List<profilesavailable> availableProfiles = new List<profilesavailable>();
        [DataMember]
        public profileselected selectedProfile;

        [DataContractAttribute]
        public struct profilesavailable
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public bool legacy { get; set; }
        }

        [DataContractAttribute]
        public struct profileselected
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public bool legacy { get; set; }
        }
    }

    /// <summary>
    /// The JSON refresh request construct.
    /// </summary>
    [DataContract]
    public class MCRefresh_Request
    {
        [DataMember]
        public string accessToken { get; set; }
        [DataMember]
        public string clientToken { get; set; }
        //public cls_selectedprofile selectedProfile { get; set; }
    }

    /// <summary>
    /// The JSON refresh response construct.
    /// </summary>
    [DataContract]
    public class MCRefresh_Response
    {
        [DataMember]
        public string accessToken { get; set; }
        [DataMember]
        public string clientToken { get; set; }
        [DataMember]
        public profileselected selectedProfile;

        [DataContractAttribute]
        public struct profileselected
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public bool legacy { get; set; }
        }
    }


    /// <summary>
    /// The JSON Error response construct.
    /// </summary>
    [DataContract]
    public class MCError
    {
        [DataMember]
        public string error { get; set; }
        [DataMember]
        public string errorMessage { get; set; }
        [DataMember]
        public string cause { get; set; }
    }


    


    /// <summary>
    /// The JSON Client Pack construct.
    /// </summary>
    [DataContract]
    public class MCPacksAvailable
    {
        [DataMember]
        public List<pack> packs = new List<pack>();

        [DataContractAttribute]
        public struct pack
        {
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public string recommended_version { get; set; }
            [DataMember]
            public bool autoupdate { get; set; }
            [DataMember]
            public List<String> versions { get; set; }
        }
    }

    /// <summary>
    /// The JSON Client installed Pack construct.
    /// </summary>
    [DataContract]
    public class MCPacksInstalled
    {
        [DataMember]
        public List<pack> packs = new List<pack>();

        [DataContractAttribute]
        public struct pack
        {
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public string current_version { get; set; } // Version of the package
            [DataMember]
            public string selected_version { get; set; } // selected version in Launcher window (recommended check)
        }
    }

    // Structures Neu!


    /// <summary>
    /// The JSON Minecraft Game Structure.
    /// </summary>
    [DataContract]
    public class MCGameStructure
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string time { get; set; }
        [DataMember]
        public string releaseTime { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string minecraftArguments { get; set; }
        [DataMember]
        public string minimumLauncherVersion { get; set; }
        [DataMember]
        public string assets { get; set; }
        [DataMember]
        public string mainClass { get; set; }
        [DataMember]
        public List<MCGameStructureLib> libraries = new List<MCGameStructureLib>();
    }

    [DataContract]
    public class MCGameStructureLibExtract
    {
        [DataMember]
        public List<string> exclude;
    }

    [DataContract]
    public class MCGameStructureLibRuleOS
    {
        [DataMember]
        public string name { get; set; }
    }

    [DataContract]
    public class MCGameStructureLibRule
    {
        [DataMember]
        public string action { get; set; }
        [DataMember]
        public MCGameStructureLibRuleOS os;
    }

    [DataContract]
    public class MCGameStructureLibNative
    {
        [DataMember]
        public string linux { get; set; }
        [DataMember]
        public string windows { get; set; }
        [DataMember]
        public string osx { get; set; }
    }

    [DataContract]
    public class MCGameStructureLib
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public List<MCGameStructureLibRule> rules;
        [DataMember]
        public MCGameStructureLibNative natives;
        [DataMember]
        public MCGameStructureLibExtract extract;
    }


    [DataContract]
    public class MCAssets
    {
        [DataMember]
        public string @virtual { get; set;}
        [DataMember]
        public Dictionary<string, MCAssetsObject> objects;

    }

    [DataContract]
    public class MCAssetsObject
    {
        [DataMember]
        public string hash { get; set; }
        [DataMember]
        public string size { get; set; }
    }


}
