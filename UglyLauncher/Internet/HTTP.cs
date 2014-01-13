using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Internet
{
    public static class Http
    {
        public static string HttpErrorMessage = null;

        public static string GET(string url)
        {
            // declare needed objects
            WebRequest request = null;
            WebResponse response = null;
            StreamReader stringResponse = null;

            try
            {
                request = WebRequest.Create(url);
                response = request.GetResponse();
                stringResponse = new StreamReader(response.GetResponseStream());
                return stringResponse.ReadToEnd().Trim();
            }
            catch (WebException ex)
            {
                throw ex;
            }
            finally
            {
                stringResponse.Close();
                response.Close();
            }
        }

        public static string POST(string url, string postdata, string contenttype)
        {
            // declare needed objects
            WebRequest request = null;
            WebResponse response = null;
            StreamReader stringResponse = null;
            Stream dataStream = null;

            try
            {
                // Create request
                request = WebRequest.Create(url);
                // set Method
                request.Method = "POST";
                // append POST data
                byte[] byteArray = Encoding.UTF8.GetBytes(postdata);
                request.ContentType = contenttype;
                request.ContentLength = byteArray.Length;
                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Get response
                response = request.GetResponse();
                stringResponse = new StreamReader(response.GetResponseStream());
                // return answer
                return stringResponse.ReadToEnd().Trim();
                // close open objects
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpErrorMessage = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().Trim();
                }
                throw ex;
            }
            finally
            {
                // close everything
                if (stringResponse != null) stringResponse.Close();
                if (dataStream != null) dataStream.Close();
                if (response != null) response.Close();
            }
        }

        public static MemoryStream Download(string url)
        {
            // create needed objects
            WebClient wc;
            MemoryStream ms;

            try
            {
                wc = new WebClient();
                byte[] bytes = wc.DownloadData(url);
                ms = new MemoryStream(bytes);
                return ms;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }
    }
}
