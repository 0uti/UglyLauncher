using System;
using System.IO;
using System.Net;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace UglyLauncher.Minecraft
{
    class Authentication
    {
        private string sAuthServer = "https://authserver.mojang.com";

        public MCAuthenticate_Response Authenticate(string sUser, string sPassword)
        {
            // declare needed objects
            string sJsonResponse = null;
            WebRequest request = null;
            WebResponse response = null;
            StreamReader stringResponse = null;
            Stream dataStream = null;

            // create and fill JSON object
            MCAuthenticate_Request jsonObject = new MCAuthenticate_Request();
            jsonObject.username = sUser;
            jsonObject.password = sPassword;
            jsonObject.agent.name = "Minecraft";
            jsonObject.agent.version = "1";

            // Serialize JSON
            string sJsonRequest = UglyLauncher.JsonHelper.JsonSerializer<MCAuthenticate_Request>(jsonObject);
            
            // Do POST request
            try
            {
                // Create request
                request = WebRequest.Create(this.sAuthServer + "/authenticate");
                // set Method
                request.Method = "POST";
                // set TimeOut
                request.Timeout = 3000;
                // append POST data
                byte[] byteArray = Encoding.UTF8.GetBytes(sJsonRequest);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Get response
                response = request.GetResponse();
                stringResponse = new StreamReader(response.GetResponseStream());
                // return answer
                sJsonResponse =  stringResponse.ReadToEnd().Trim();
                // close open objects
            }
            catch (WebException ex)
            {
                switch (ex.Status)
                {
                    case WebExceptionStatus.ProtocolError:
                        // Get Json Answer
                        sJsonResponse = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().Trim();
                        // deserialize JSON
                        MCError ErrorMessage = UglyLauncher.JsonHelper.JsonDeserializer<MCError>(sJsonResponse);
                        if (ErrorMessage.errorMessage == "Invalid credentials. Invalid username or password.") throw new MCInvalidCredentialsException(ErrorMessage.errorMessage);
                        if (ErrorMessage.errorMessage == "Invalid credentials. Account migrated, use e-mail as username.") throw new MCUserMigratedException(ErrorMessage.errorMessage);
                        throw new Exception(ErrorMessage.errorMessage);
                    default:
                        throw new Exception(ex.Message);
                }
            }
            finally
            {
                // close everything
                if (stringResponse != null) stringResponse.Close();
                if (dataStream != null) dataStream.Close();
                if (response != null) response.Close();
            }

            // Deserialize JSON into object
            MCAuthenticate_Response MCResponse = UglyLauncher.JsonHelper.JsonDeserializer<MCAuthenticate_Response>(sJsonResponse);

            //return
            return MCResponse;
        }

        public string Refresh(string sAccessToken, string sClientToken)
        {
            // declare needed objects
            string sJsonResponse = null;
            WebRequest request = null;
            WebResponse response = null;
            StreamReader stringResponse = null;
            Stream dataStream = null;

            // create and fill JSON object
            MCRefresh_Request jsonObject = new MCRefresh_Request();
            jsonObject.accessToken = sAccessToken;
            jsonObject.clientToken = sClientToken;
            //jsonObject.selectedProfile.id = sProfileId;
            //jsonObject.selectedProfile.name = sProfileName;

            // Serialize JSON
            string sJsonRequest = UglyLauncher.JsonHelper.JsonSerializer<MCRefresh_Request>(jsonObject);

            // send HTTP POST request
            try
            {
                // Create request
                request = WebRequest.Create(this.sAuthServer + "/refresh");
                // set Method
                request.Method = "POST";
                // set TimeOut
                request.Timeout = 3000;
                // append POST data
                byte[] byteArray = Encoding.UTF8.GetBytes(sJsonRequest);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Get response
                response = request.GetResponse();
                stringResponse = new StreamReader(response.GetResponseStream());
                // return answer
                sJsonResponse = stringResponse.ReadToEnd().Trim();
                // close open objects
            }
            catch (WebException ex)
            {
                switch (ex.Status)
                {
                    case WebExceptionStatus.ProtocolError:
                        if (ex.Message.Contains("500") || ex.Message.Contains("503"))
                        {
                            throw new Exception("Mojang Loginservice nicht verfügbar.");
                        }
                        if (ex.Message.Contains("403"))
                        {
                            // Get Json Answer
                            sJsonResponse = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().Trim();
                            // deserialize JSON
                            MCError ErrorMessage = UglyLauncher.JsonHelper.JsonDeserializer<MCError>(sJsonResponse);
                            if (ErrorMessage.errorMessage == "Invalid token.") throw new MCInvalidTokenException(ErrorMessage.errorMessage);
                            throw new Exception(ErrorMessage.errorMessage);
                        }
                        throw new Exception(ex.Message);
                    default:
                        throw new Exception(ex.Message);
                }
            }
            finally
            {
                // close everything
                if (stringResponse != null) stringResponse.Close();
                if (dataStream != null) dataStream.Close();
                if (response != null) response.Close();
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
        public MCAuthenticate_RequestAgent agent = new MCAuthenticate_RequestAgent();
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string clientToken { get; set; }
    }

    [DataContract]
    public class MCAuthenticate_RequestAgent
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string version { get; set; }
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
        public List<MCProfile> availableProfiles = new List<MCProfile>();
        [DataMember]
        public MCProfile selectedProfile = new MCProfile();
    }

    [DataContract]
    public class MCProfile
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public bool legacy { get; set; }
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
        public MCProfile selectedProfile = new MCProfile();
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
