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
        public static MCAuthenticate_Response MCTokens = new MCAuthenticate_Response();
        public static string sAuthServer = "https://authserver.mojang.com";

        public static void Authenticate(string user, string password)
        {
            MCAuthenticate_Request jsonObject = new MCAuthenticate_Request();
            jsonObject.username = user;
            jsonObject.password = password;
            
            string jsonString = Internet.Http.POST(sAuthServer + "/authenticate", UglyLauncher.JsonHelper.JsonSerializer<MCAuthenticate_Request>(jsonObject), "application/json");
            MCTokens = UglyLauncher.JsonHelper.JsonDeserializer<MCAuthenticate_Response>(jsonString);
        }

        public static void Refresh()
        {
            string url = sAuthServer + "/refresh";


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
        public struct agent
        {
            public string name { get; set; }
            public string version { get; set; }
        }
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
        public profilesavailable[] availableProfiles { get; set; }
        public struct selectedProfile
        {
            public string id { get; set; }
            public string name { get; set; }
        }
        public struct profilesavailable
        {
            public string id { get; set; }
            public string name { get; set; }
        }
    }


}
