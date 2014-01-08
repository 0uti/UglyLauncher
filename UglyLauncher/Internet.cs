using System;
using System.IO;
using System.Net;
using System.Text;

namespace Internet
{
    public static class Http
    {
        public static string GET(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            StreamReader stringResponse = new StreamReader(response.GetResponseStream());
            string retstring = stringResponse.ReadToEnd().Trim();
            stringResponse.Close();
            response.Close();
            return retstring;
        }

        public static string POST(string url, string postdata, string contenttype)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(postdata);
            request.ContentType = contenttype;
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            // ToDo: WebExeptions
            WebResponse response = request.GetResponse();
            StreamReader stringResponse = new StreamReader(response.GetResponseStream());
            string retstring = stringResponse.ReadToEnd().Trim();
            stringResponse.Close();
            dataStream.Close();
            response.Close();
            return retstring;
        }
    }
}