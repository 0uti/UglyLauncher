using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Runtime.Serialization;

using Internet;



namespace Minecraft
{
    
    public class Launcher
    {
        
    }

    public class Authentication
    {
        public static string sAuthServer = "https://authserver.mojang.com";

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
            string sJsonResponse = Internet.Http.POST(sAuthServer + "/refresh", sJsonRequest, "application/json");

            // Deserialize JSON into object
            MCRefresh_Response MCResponse = UglyLauncher.JsonHelper.JsonDeserializer<MCRefresh_Response>(sJsonResponse);

            //return
            return MCResponse.accessToken;
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
}
