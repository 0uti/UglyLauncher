using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace UglyBootstrap
{
    public partial class frm_main : Form
    {
        static string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string sAppServer = "http://outi-networks.de/UglyLauncher/Updates/";

        private WebClient Downloader = new WebClient();

        appinfo AppInfo = new appinfo();

        public frm_main()
        {
            InitializeComponent();
        }

        private void frm_main_Load(object sender, EventArgs e)
        {
            this.Downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Downloader_DownloadProgressChanged);
        }

        void Downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void frm_main_Shown(object sender, EventArgs e)
        {
            XmlDocument xmlDocument = new XmlDocument();

            // check if Launcher executable exists
            if (!Directory.Exists(appData + @"\.UglyLauncher")) Directory.CreateDirectory(appData + @"\.UglyLauncher");
            if (!File.Exists(appData + @"\.UglyLauncher\UglyLauncher.exe"))
            {
                this.WindowState = FormWindowState.Normal;
                this.DownloadFileTo(sAppServer + "UglyLauncher.exe", appData + @"\.UglyLauncher\UglyLauncher.exe");
                Process.Start(new ProcessStartInfo(appData + @"\.UglyLauncher\UglyLauncher.exe"));
                Application.Exit();
                return;
            }
            // check for version
            Version curVersion = AssemblyName.GetAssemblyName(appData + @"\.UglyLauncher\UglyLauncher.exe").Version;
            // get remote xml
            this.LoadXML(sAppServer + "version.xml");
            Version newVersion = new Version(this.AppInfo.version);

            if (curVersion.CompareTo(newVersion) < 0)
            {
                try
                {
                    // do update
                    if (File.Exists(appData + @"\.UglyLauncher\UglyLauncher.exe")) File.Delete(appData + @"\.UglyLauncher\UglyLauncher.exe");
                    this.WindowState = FormWindowState.Normal;
                    this.DownloadFileTo(this.AppInfo.installer, appData + @"\.UglyLauncher\UglyLauncher.exe");
                }
                catch (Exception)
                {
                    Application.Exit();
                    return;
                }
            }
            
            Process.Start(new ProcessStartInfo(appData + @"\.UglyLauncher\UglyLauncher.exe"));
            Application.Exit();
            return;
        }


        private void LoadXML(string xmlurl)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlurl);
                using (StringReader read = new StringReader(xmlDocument.OuterXml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(appinfo));
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        this.AppInfo = (appinfo)serializer.Deserialize(reader);
                        reader.Close();
                    }
                    read.Close();
                }
            }
            catch (Exception)
            {
                //Log exception here
            }
        }


        // download file if needed
        private void DownloadFileTo(string sRemotePath, string sLocalPath)
        {
            if (!File.Exists(sLocalPath))
            {
                this.Downloader.DownloadFileAsync(new Uri(sRemotePath), sLocalPath);
                Application.DoEvents();
                while (this.Downloader.IsBusy)
                    Application.DoEvents();
                Application.DoEvents();
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
        [DataMember]
        public string installer { get; set; }
        [DataMember]
        public string date { get; set; }
    }
    /*
    <appinfo>
        <version>0.0.1.0</version>
        <url>http://outi-networks.de/UglyLauncher/</url>
        <installer>http://outi-networks.de/UglyLauncher/UglyLauncher.exe</installer>
        <date>28/10/2010</date>
</appinfo>
    */
}
