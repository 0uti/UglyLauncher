using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Internet;
using System.Net;

namespace Minecraft
{
    class Authentication
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
            Http H = new Http();

            // Serialize JSON
            string sJsonRequest = UglyLauncher.JsonHelper.JsonSerializer<MCAuthenticate_Request>(jsonObject);
            string sJsonResponse = null;

            try
            {
                // send HTTP POST request
                sJsonResponse = H.POST(sAuthServer + "/authenticate", sJsonRequest, "application/json");
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    // get JSON Error Message
                    sJsonResponse = H.HttpErrorMessage;
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
            Http H = new Http();

            // Serialize JSON
            string sJsonRequest = UglyLauncher.JsonHelper.JsonSerializer<MCRefresh_Request>(jsonObject);

            // send HTTP POST request
            string sJsonResponse = null;
            try
            {
                sJsonResponse = H.POST(sAuthServer + "/refresh", sJsonRequest, "application/json");
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    // get JSON Error Message
                    sJsonResponse = H.HttpErrorMessage;
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
}
