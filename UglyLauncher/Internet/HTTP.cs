using System;
using System.IO;
using System.Net;
using System.Text;

namespace UglyLauncher.Internet
{
    public static class Http
    {
        /// <summary>
        /// HTTP Get Request
        /// </summary>
        /// <param name="url">the URL as string</param>
        /// <returns>respronse from server</returns>
        public static string GET(string url)
        {
            return GET(new Uri(url));
        }

        /// <summary>
        /// HTTP Get Request
        /// </summary>
        /// <param name="url">the URL as Uri</param>
        /// <returns>respronse from server</returns>
        public static string GET(Uri url)
        {
            try
            {
                // declare needed objects
                WebRequest request = WebRequest.CreateHttp(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Timeout = 10000;
                
                WebResponse response = request.GetResponse();
                using (StreamReader stringResponse = new StreamReader(response.GetResponseStream()))
                {
                    return stringResponse.ReadToEnd().Trim();
                }
            }
            catch (WebException)
            {
                throw;
            }
        }

        /// <summary>
        /// HTTP POST Request
        /// </summary>
        /// <param name="url">the URL as string</param>
        /// <param name="postdata">data to post</param>
        /// <param name="contenttype">content type of data</param>
        /// <returns>response from server</returns>
        public static string POST(string url, string postdata, string contenttype)
        {
            return POST(new Uri(url), postdata, contenttype);
        }

        /// <summary>
        /// HTTP POST Request
        /// </summary>
        /// <param name="url">the URL as URI</param>
        /// <param name="postdata">data to post</param>
        /// <param name="contenttype">content type of data</param>
        /// <returns>response from server</returns>
        public static string POST(Uri url, string postdata, string contenttype)
        {
            WebResponse response = null;
            StreamReader stringResponse = null;
            Stream dataStream = null;

            try
            {
                // declare needed objects
                // Create request
                WebRequest request = WebRequest.Create(url);
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
            catch (WebException)
            {
                throw;
            }
            finally
            {
                // close everything
                if (stringResponse != null) stringResponse.Close();
                if (dataStream != null) dataStream.Close();
                if (response != null) response.Close();
            }
        }

        /// <summary>
        /// Download to MemoryStream
        /// </summary>
        /// <param name="url">the URL as string</param>
        /// <returns>the MemoryString of downloaded object</returns>
        public static MemoryStream DownloadToStream(string url)
        {
            return DownloadToStream(new Uri(url));
        }

        /// <summary>
        /// Download to MemoryStream
        /// </summary>
        /// <param name="url">the URL as Uri</param>
        /// <returns>the MemoryString of downloaded object</returns>
        public static MemoryStream DownloadToStream(Uri url)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    byte[] bytes = wc.DownloadData(url);
                    MemoryStream ms = new MemoryStream(bytes);
                    return ms;
                }
            }
            catch (WebException)
            {
                throw;
            }
        }
    }
}
