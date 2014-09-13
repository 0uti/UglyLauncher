using System.IO;
using System.Net;
using System.Text;

namespace UglyLauncher.Internet
{
    public static class Http
    {
        public static string GET(string url)
        {
            // declare needed objects
            WebRequest request = null;
            WebResponse response = null;
            StreamReader stringResponse = null;

            try
            {
                request = WebRequest.Create(url);
                request.Timeout = 5000;
                response = request.GetResponse();
                stringResponse = new StreamReader(response.GetResponseStream());
                return stringResponse.ReadToEnd().Trim();
            }
            catch (WebException ex)
            {
                throw ex;
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

        public static MemoryStream DownloadToStream(string url)
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
