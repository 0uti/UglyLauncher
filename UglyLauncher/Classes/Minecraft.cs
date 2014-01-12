using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Windows.Forms;

using Internet;



namespace Minecraft
{
    // Static Class. More functions will use this data.
    public static class staticVars
    {
        public static MCPacks Packs = new MCPacks();
        public static MCPacksInstalled PacksInstalled = new MCPacksInstalled();
    }

    public class Launcher
    {
        public string sPackServer = "http://outi-networks.de/UglyLauncher";

        // Get Package liste from Server
        public MCPacks GetClientPackList(string MCPlayerName)
        {
            MCPacks Packs = new MCPacks();
            string jsonString = Internet.Http.GET(sPackServer + @"/packs.php?player=" + MCPlayerName);

            Packs = UglyLauncher.JsonHelper.JsonDeserializer<MCPacks>(jsonString);
            return Packs;
        }

        // Get installes packages
        public MCPacksInstalled GetInstalledPacks()
        {
            MCPacksInstalled Packs = new MCPacksInstalled();
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(UglyLauncher.AppPathes.sPacksDir));

            foreach (var dir in dirs)
            {
                if (File.Exists(dir + @"\version") && File.Exists(dir + @"\pack.json"))
                {
                    MCPacksInstalled.pack pack = new MCPacksInstalled.pack();
                    pack.name = dir.Substring(dir.LastIndexOf("\\") + 1);
                    // Get versions files
                    pack.current_version = File.ReadAllText(dir + @"\version").Trim();
                    if (File.Exists(dir + @"\selected")) pack.selected_version = File.ReadAllText(dir + @"\selected").Trim();
                    Packs.packs.Add(pack);
                }
            }
            return Packs;
        }

        public bool IsPackInstalled(string sPackName, string sPackVersion)
        {
            // find Pack in List
            int iPackId = -1;
            for (int i = 0; i < staticVars.PacksInstalled.packs.Count; i++)
                if (staticVars.PacksInstalled.packs[i].name == sPackName) iPackId = i;
            
            // return false if pack not found
            if (iPackId == -1) return false;

            MCPacksInstalled.pack oInstalledPack = new MCPacksInstalled.pack();
            oInstalledPack = staticVars.PacksInstalled.packs[iPackId];

            // check if version is installed
            if (oInstalledPack.current_version != sPackVersion) return false;

            // Pack is fine :)
            return true;
        }
    }

    public class Authentication
    {
        public string sAuthServer = "https://authserver.mojang.com";

        public MCAuthenticate_Response Authenticate(string sUser, string sPassword)
        {
            
            // create and fill JSON object
            MCAuthenticate_Request jsonObject = new MCAuthenticate_Request();
            jsonObject.username = sUser;
            jsonObject.password = sPassword;
            jsonObject.agent.name = "Minecraft";
            jsonObject.agent.version = "1";

            // Serialize JSON
            string sJsonRequest = UglyLauncher.JsonHelper.JsonSerializer<MCAuthenticate_Request>(jsonObject);
            string sJsonResponse = null;

            try
            {
                // send HTTP POST request
                sJsonResponse = Internet.Http.POST(sAuthServer + "/authenticate", sJsonRequest, "application/json");
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    // get JSON Error Message
                    sJsonResponse = Internet.Http.HttpErrorMessage;
                    MCError ErrorMessage = UglyLauncher.JsonHelper.JsonDeserializer<MCError>(sJsonResponse);
                    throw new Exception(ErrorMessage.errorMessage);
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
            // Deserialize JSON into object
            MCAuthenticate_Response MCResponse = UglyLauncher.JsonHelper.JsonDeserializer<MCAuthenticate_Response>(sJsonResponse);

            //return
            return MCResponse;
        }

        public string Refresh(string sAccessToken, string sClientToken)
        {
            string url = sAuthServer + "/refresh";
            
            // create and fill JSON object
            MCRefresh_Request jsonObject = new MCRefresh_Request();
            jsonObject.accessToken = sAccessToken;
            jsonObject.clientToken = sClientToken;
            //jsonObject.selectedProfile.id = sProfileId;
            //jsonObject.selectedProfile.name = sProfileName;

            // Serialize JSON
            string sJsonRequest = UglyLauncher.JsonHelper.JsonSerializer<MCRefresh_Request>(jsonObject);

            // send HTTP POST request
            string sJsonResponse = null;
            try
            {
                sJsonResponse = Internet.Http.POST(sAuthServer + "/refresh", sJsonRequest, "application/json");
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    // get JSON Error Message
                    sJsonResponse = Internet.Http.HttpErrorMessage;
                    MCError ErrorMessage = UglyLauncher.JsonHelper.JsonDeserializer<MCError>(sJsonResponse);
                    throw new Exception(ErrorMessage.errorMessage);
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
            // Deserialize JSON into object
            MCRefresh_Response MCResponse = UglyLauncher.JsonHelper.JsonDeserializer<MCRefresh_Response>(sJsonResponse);

            //return
            return MCResponse.accessToken;
        }
    }

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
    public class MCPacks
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
        public List<lib> libraries = new List<lib>();

        [DataContractAttribute]
        public struct lib
        {
            public string name { get; set; }
            [DataMember]
            public List<rule> rules;
            [DataMember]
            public native natives;
            [DataMember]
            public Extract extract;
        }

        [DataContractAttribute]
        public struct Extract
        {
            [DataMember]
            public List<string> exclude { get; set; }
        }

        [DataContractAttribute]
        public struct native
        {
            [DataMember]
            public string linux { get; set; }
            [DataMember]
            public string windows { get; set; }
            [DataMember]
            public string osx { get; set; }
        }

        [DataContractAttribute]
        public struct rule
        {
            [DataMember]
            public string action { get; set; }
            [DataMember]
            public OS os;
        }

        [DataContractAttribute]
        public struct OS
        {
            [DataMember]
            public string name { get; set; }
        }
    }

}
