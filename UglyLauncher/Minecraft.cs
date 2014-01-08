using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Internet;


namespace Minecraft
{

    public static class Launcher
    {
        public static string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string sDataDir = appData + @"\.UglyLauncher";
        public static string sLibraryDir = appData + @"\.UglyLauncher\libraries";
        public static string sAssetsDir = appData + @"\.UglyLauncher\assets";
        public static string sVersionDir = appData + @"\.UglyLauncher\versions";
        public static string sPacksDir = appData + @"\.UglyLauncher\packs";

        public static void CheckDirectories()
        {
            if (!Directory.Exists(sDataDir)) Directory.CreateDirectory(sDataDir);
            if (!Directory.Exists(sLibraryDir)) Directory.CreateDirectory(sLibraryDir);
            if (!Directory.Exists(sAssetsDir)) Directory.CreateDirectory(sAssetsDir);
            if (!Directory.Exists(sVersionDir)) Directory.CreateDirectory(sVersionDir);
            if (!Directory.Exists(sPacksDir)) Directory.CreateDirectory(sPacksDir);
        }
    }


    public static class Authentication
    {
        public static string sAccessToken = null;
        public static string sClientToken = null;
        public static string sProfileId = null;
        public static string sProfileName = null;
        public static string sAuthServer = "https://authserver.mojang.com";

        public static void Authenticate(string sUser, string sPassword)
        {
            // create and fill JSON object
            MCAuthenticate_Request jsonObject = new MCAuthenticate_Request();
            jsonObject.username = sUser;
            jsonObject.password = sPassword;
            jsonObject.agent = new cls_agent();
            jsonObject.agent.name = "Minecraft";
            jsonObject.agent.version = "1";
            
            // Serialize JSON
            string sJsonRequest = UglyLauncher.JsonHelper.JsonSerializer<MCAuthenticate_Request>(jsonObject);

            // send HTTP POST request
            string sJsonResponse = Internet.Http.POST(sAuthServer + "/authenticate", sJsonRequest, "application/json");

            // Deserialize JSON into object
            MCAuthenticate_Response MCResponse = UglyLauncher.JsonHelper.JsonDeserializer<MCAuthenticate_Response>(sJsonResponse);

            //Copy
            sAccessToken = MCResponse.accessToken;
            sClientToken = MCResponse.clientToken;
            sProfileId = MCResponse.selectedProfile.id;
            sProfileName = MCResponse.selectedProfile.name;
        }

        public static void Refresh()
        {
            string url = sAuthServer + "/refresh";

            // create and fill JSON object
            MCRefresh_Request jsonObject = new MCRefresh_Request();
            jsonObject.accessToken = sAccessToken;
            jsonObject.clientToken = sClientToken;
            //jsonObject.selectedProfile = new cls_selectedprofile();
            //jsonObject.selectedProfile.id = sProfileId;
            //jsonObject.selectedProfile.name = sProfileName;

            // Serialize JSON
            string sJsonRequest = UglyLauncher.JsonHelper.JsonSerializer<MCRefresh_Request>(jsonObject);

            // send HTTP POST request
            string sJsonResponse = Internet.Http.POST(sAuthServer + "/refresh", sJsonRequest, "application/json");

            // Deserialize JSON into object
            MCRefresh_Response MCResponse = UglyLauncher.JsonHelper.JsonDeserializer<MCRefresh_Response>(sJsonResponse);

            //Copy
            sAccessToken = MCResponse.accessToken;
        }

        public static void Validate()
        {
            string url = sAuthServer + "/validate";


        }
    }



    /// <summary>
    /// The JSON authenticate request construct.
    /// </summary>
    public class MCAuthenticate_Request
    {
        public cls_agent agent { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string clientToken { get; set; }
    }

    /// <summary>
    /// The JSON authenticate response construct.
    /// </summary>
    public class MCAuthenticate_Response
    {
        public string accessToken { get; set; }
        public string clientToken { get; set; }
        public List<cls_profilesavailable> availableProfiles { get; set; }
        public cls_selectedprofile selectedProfile { get; set; }
    }
    
    /// <summary>
    /// The JSON refresh request construct.
    /// </summary>
    public class MCRefresh_Request
    {
        public string accessToken { get; set; }
        public string clientToken { get; set; }
        //public cls_selectedprofile selectedProfile { get; set; }
    }

    /// <summary>
    /// The JSON refresh response construct.
    /// </summary>
    public class MCRefresh_Response
    {
        public string accessToken { get; set; }
        public string clientToken { get; set; }
        public cls_selectedprofile selectedProfile { get; set; }
    }

    /// <summary>
    /// some struct parts.
    /// </summary>
    public class cls_profilesavailable
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class cls_agent
    {
        public string name { get; set; }
        public string version { get; set; }
    }

    public class cls_selectedprofile
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
