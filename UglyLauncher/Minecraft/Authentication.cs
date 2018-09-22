using System;
using System.IO;
using System.Net;
using System.Text;
using UglyLauncher.Minecraft.Json.MCAuthenticateRequest;
using UglyLauncher.Minecraft.Json.MCAuthenticateError;
using UglyLauncher.Minecraft.Json.MCAuthenticateResponse;
using UglyLauncher.Minecraft.Json.MCRefreshRequest;
using UglyLauncher.Minecraft.Json.MCRefreshResponse;

namespace UglyLauncher.Minecraft
{
    class Authentication
    {
        private readonly string sAuthServer = "https://authserver.mojang.com";

        public MCAuthenticateResponse Authenticate(string sUser, string sPassword)
        {
            // declare needed objects
            string sJsonResponse = null;
            WebRequest request = null;
            WebResponse response = null;
            StreamReader stringResponse = null;
            Stream dataStream = null;

            // create and fill JSON object
            MCAuthenticateRequest jsonObject = new MCAuthenticateRequest
            {
                Username = sUser,
                Password = sPassword
            };
            jsonObject.Agent.Name = "Minecraft";
            jsonObject.Agent.Version = 1;

            // Serialize JSON
            string sJsonRequest = Json.MCAuthenticateRequest.Serialize.ToJson(jsonObject);

            // Do POST request
            try
            {
                // Create request
                request = WebRequest.Create(sAuthServer + "/authenticate");
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
                        if (ex.Message.Contains("500") || ex.Message.Contains("503"))
                        {
                            throw new Exception("Mojang Loginservice nicht verfügbar.");
                        }
                        if (ex.Message.Contains("403"))
                        {
                            // Get Json Answer
                            sJsonResponse = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().Trim();
                            // deserialize JSON

                            MCAuthenticatieError ErrorMessage = MCAuthenticatieError.FromJson(sJsonResponse);
                            if (ErrorMessage.ErrorMessage == "Invalid credentials. Invalid username or password.") throw new MCInvalidCredentialsException(ErrorMessage.ErrorMessage);
                            if (ErrorMessage.ErrorMessage == "Invalid credentials. Account migrated, use e-mail as username.") throw new MCUserMigratedException(ErrorMessage.ErrorMessage);
                            throw new Exception(ErrorMessage.ErrorMessage);
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
            MCAuthenticateResponse MCResponse = MCAuthenticateResponse.FromJson(sJsonResponse);

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
            MCRefreshRequest jsonObject = new MCRefreshRequest
            {
                AccessToken = sAccessToken,
                ClientToken = sClientToken
            };


            // Serialize JSON
            string sJsonRequest = Json.MCRefreshRequest.Serialize.ToJson(jsonObject);

            // send HTTP POST request
            try
            {
                // Create request
                request = WebRequest.Create(sAuthServer + "/refresh");
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
                            MCAuthenticatieError ErrorMessage = MCAuthenticatieError.FromJson(sJsonResponse);
                            if (ErrorMessage.ErrorMessage == "Invalid token.") throw new MCInvalidTokenException(ErrorMessage.ErrorMessage);
                            throw new Exception(ErrorMessage.ErrorMessage);
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
            MCRefreshResponse MCResponse = MCRefreshResponse.FromJson(sJsonResponse);

            //return
            return MCResponse.AccessToken;
        }
    }
}
