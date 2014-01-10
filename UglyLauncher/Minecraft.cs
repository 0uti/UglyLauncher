using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using Internet;



namespace Minecraft
{
    public static class UserInformation
    {
        public static bool bAuthenticated = false;
        public static string sAccessToken = null;
        public static string sClientToken = null;
        public static string sProfileId = null;
        public static string sProfileName = null;
        public static bool bProfileLegacy = false;
    }

    public class Launcher
    {
        public static string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string sDataDir = appData + @"\.UglyLauncher";
        public static string sLibraryDir = appData + @"\.UglyLauncher\libraries";
        public static string sAssetsDir = appData + @"\.UglyLauncher\assets";
        public static string sVersionDir = appData + @"\.UglyLauncher\versions";
        public static string sPacksDir = appData + @"\.UglyLauncher\packs";

        public void CheckDirectories()
        {
            if (!Directory.Exists(sDataDir)) Directory.CreateDirectory(sDataDir);
            if (!Directory.Exists(sLibraryDir)) Directory.CreateDirectory(sLibraryDir);
            if (!Directory.Exists(sAssetsDir)) Directory.CreateDirectory(sAssetsDir);
            if (!Directory.Exists(sVersionDir)) Directory.CreateDirectory(sVersionDir);
            if (!Directory.Exists(sPacksDir)) Directory.CreateDirectory(sPacksDir);
        }
    }

    public class Authentication
    {
        public static string sAuthServer = "https://authserver.mojang.com";

        public void Authenticate(string sUser, string sPassword)
        {
            // create and fill JSON object
            MCAuthenticate_Request jsonObject = new MCAuthenticate_Request();
            jsonObject.username = sUser;
            jsonObject.password = sPassword;
            jsonObject.agent.name = "Minecraft";
            jsonObject.agent.version = "1";
            
            // Serialize JSON
            string sJsonRequest = UglyLauncher.JsonHelper.JsonSerializer<MCAuthenticate_Request>(jsonObject);

            // send HTTP POST request
            string sJsonResponse = Internet.Http.POST(sAuthServer + "/authenticate", sJsonRequest, "application/json");

            // Deserialize JSON into object
            MCAuthenticate_Response MCResponse = UglyLauncher.JsonHelper.JsonDeserializer<MCAuthenticate_Response>(sJsonResponse);

            //Copy
            UserInformation.sAccessToken = MCResponse.accessToken;
            UserInformation.sClientToken = MCResponse.clientToken;
            UserInformation.sProfileId = MCResponse.selectedProfile.id;
            UserInformation.sProfileName = MCResponse.selectedProfile.name;
            UserInformation.bProfileLegacy = MCResponse.selectedProfile.legacy;
        }

        public void Refresh()
        {
            string url = sAuthServer + "/refresh";
            
            // create and fill JSON object
            MCRefresh_Request jsonObject = new MCRefresh_Request();
            jsonObject.accessToken = UserInformation.sAccessToken;
            jsonObject.clientToken = UserInformation.sClientToken;
            //jsonObject.selectedProfile.id = sProfileId;
            //jsonObject.selectedProfile.name = sProfileName;

            // Serialize JSON
            string sJsonRequest = UglyLauncher.JsonHelper.JsonSerializer<MCRefresh_Request>(jsonObject);

            // send HTTP POST request
            string sJsonResponse = Internet.Http.POST(sAuthServer + "/refresh", sJsonRequest, "application/json");

            // Deserialize JSON into object
            MCRefresh_Response MCResponse = UglyLauncher.JsonHelper.JsonDeserializer<MCRefresh_Response>(sJsonResponse);

            //Copy
            UserInformation.sAccessToken = MCResponse.accessToken;
        }

        public void Validate()
        {
            string url = sAuthServer + "/validate";


        }
    }



    /// <summary>
    /// The JSON authenticate request construct.
    /// </summary>
    [DataContract]
    internal class MCAuthenticate_Request
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
        public List<profilesavailable> availableProfiles;
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
}
