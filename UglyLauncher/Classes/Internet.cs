using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Internet
{
    public static class Http
    {
        public static string HttpErrorMessage = null;

        public static string GET(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                StreamReader stringResponse = new StreamReader(response.GetResponseStream());
                string retstring = stringResponse.ReadToEnd().Trim();
                stringResponse.Close();
                response.Close();
                return retstring;
            }
            catch (WebException e)
            {
                throw e;
            }
        }

        public static string POST(string url, string postdata, string contenttype)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                byte[] byteArray = Encoding.UTF8.GetBytes(postdata);
                request.ContentType = contenttype;
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                StreamReader stringResponse = new StreamReader(response.GetResponseStream());
                string retstring = stringResponse.ReadToEnd().Trim();
                stringResponse.Close();
                dataStream.Close();
                response.Close();
                return retstring;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpErrorMessage = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().Trim();
                }
                throw ex;
            }
        }

        public static MemoryStream Download(string url)
        {
            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData(url);
            MemoryStream ms = new MemoryStream(bytes);
            return ms;

            /* Download Image in Imagelist
                
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
              
             */


        }
    }
}