using System;
using System.IO;
using System.Net;
using System.Text;
using UglyLauncher.Minecraft.Authentication.Json.AuthenticateRequest;
using UglyLauncher.Minecraft.Authentication.Json.AuthenticateResponse;
using UglyLauncher.Minecraft.Authentication.Json.AuthenticatieError;
using UglyLauncher.Minecraft.Authentication.Json.RefreshRequest;
using UglyLauncher.Minecraft.Authentication.Json.RefreshResponse;

namespace UglyLauncher.Minecraft.Authentication
{
    class AuthHandler
    {
        private readonly string sAuthServer = "https://authserver.mojang.com";

        public AuthenticateResponse Authenticate(string sUser, string sPassword)
        {
            // declare needed objects
            string sJsonResponse = null;
            WebRequest request = null;
            WebResponse response = null;
            StreamReader stringResponse = null;
            Stream dataStream = null;

            // create and fill JSON object
            AuthenticateRequest jsonObject = new AuthenticateRequest
            {
                Username = sUser,
                Password = sPassword
            };
            jsonObject.Agent.Name = "Minecraft";
            jsonObject.Agent.Version = 1;

            // Serialize JSON
            string sJsonRequest = Json.AuthenticateRequest.Serialize.ToJson(jsonObject);

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

                            AuthenticatieError ErrorMessage = AuthenticatieError.FromJson(sJsonResponse);
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
            AuthenticateResponse MCResponse = AuthenticateResponse.FromJson(sJsonResponse);

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
            RefreshRequest jsonObject = new RefreshRequest
            {
                AccessToken = sAccessToken,
                ClientToken = sClientToken
            };


            // Serialize JSON
            string sJsonRequest = Json.RefreshRequest.Serialize.ToJson(jsonObject);


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
                            AuthenticatieError ErrorMessage = AuthenticatieError.FromJson(sJsonResponse);
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
            RefreshResponse MCResponse = RefreshResponse.FromJson(sJsonResponse);

            //return
            return MCResponse.AccessToken;
        }
    }
}
