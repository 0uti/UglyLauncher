using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Microsoft.Win32;
using System.Net;

namespace UglyLauncher
{
    class BootStrapUpdater
    {
        private appinfo AppInfo = new appinfo();
        private string sAppServer = "http://www.minestar.de/wiki/updates/";
        private string sRegPath = "Software\\Minestar\\UglyLauncher";
        private string sBootStrapVersion = null;
        private string sBootStrapPath = null;

        public bool HaveUpdate()
        {
            try
            {
                // Get XML
                this.LoadXML(sAppServer + "bootstrap.xml");
                // Get Installed
                this.GetBootStrapVersion();
                // Compare
                Version newVersion = new Version(this.AppInfo.version);
                Version curVersion = new Version(this.sBootStrapVersion);
                if (curVersion.CompareTo(newVersion) < 0) return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DoUpdate()
        {
            try
            {
                // Download new File
                WebClient Downloader = new WebClient();
                Downloader.DownloadFile(this.AppInfo.url, this.sBootStrapPath);
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }
        }



        private void LoadXML(string xmlurl)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(xmlurl);
                rq.Timeout = 5000;
                HttpWebResponse response = rq.GetResponse() as HttpWebResponse;

                using (Stream responseStream = response.GetResponseStream())
                {
                    XmlTextReader reader = new XmlTextReader(responseStream);
                    xmlDocument.Load(reader);
                }

                // xmlDocument.Load(xmlurl);
                using (StringReader read = new StringReader(xmlDocument.OuterXml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(appinfo));
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        this.AppInfo = (appinfo)serializer.Deserialize(reader);
                        //reader.Close();
                    }
                    //read.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetBootStrapVersion()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(this.sRegPath);
                if (key == null) throw new Exception("no subkey found");
                this.sBootStrapVersion = key.GetValue("Bootstrap_Version") as String;
                if (this.sBootStrapVersion == null) throw new Exception("no string value found");
                this.sBootStrapPath = key.GetValue("Bootstrap_Path") as String;
                if (this.sBootStrapPath == null) throw new Exception("no string value found");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [DataContract]
    public class appinfo
    {
        [DataMember]
        public string version { get; set; }
        [DataMember]
        public string url { get; set; }
    }
}
