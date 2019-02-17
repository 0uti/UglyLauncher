using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace UglyLauncher.Internet
{
    class DownloadHelper
    {
        private WebClient _downloader = new WebClient();
        // bool
        private bool downloadfinished = false;

        public FrmProgressbar _bar = new FrmProgressbar();

        public DownloadHelper()
        {
            _downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
            _downloader.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(Downloader_DownloadFileCompleted);
        }

        private void Downloader_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            downloadfinished = true;
        }

        public bool IsBarVisible()
        {
            return _bar.Visible;
        }

        public void HideBar()
        {
            _bar.Hide();
        }

        // Progress event from downloader
        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            if (_bar.Visible) _bar.UpdateBar(e.ProgressPercentage);
        }

        public string ComputeHashSHA(string filename)
        {
            using (SHA1 sha = SHA1.Create())
            {
                using (FileStream stream = File.OpenRead(filename))
                {
                    return (BitConverter.ToString(sha.ComputeHash(stream)).Replace("-", string.Empty).ToLower());
                }
            }
        }

        // download file if needed
        public void DownloadFileTo(string sRemotePath, string sLocalPath, bool bShowBar = true, string sBarDisplayText = null, string sha1 = null)
        {
            DownloadFileTo(new Uri(sRemotePath), sLocalPath, bShowBar, sBarDisplayText, sha1);
        }

        // download file if needed
        public void DownloadFileTo(Uri Url, string sLocalPath, bool bShowBar = true, string sBarDisplayText = null, string sha1 = null)
        {
            bool _download = false;
            if (!File.Exists(sLocalPath)) _download = true;
            else
            {
                FileInfo f = new FileInfo(sLocalPath);
                if (f.Length == 0)
                {
                    _download = true;
                }

                // check SHA1
                if (sha1 != null)
                {
                    string file_sha = ComputeHashSHA(sLocalPath);
                    if (!file_sha.Equals(sha1))
                    {
                        _download = true;
                    }
                }
            }

            if (_download)
            {
                // Create Directory, if needed
                if (!Directory.Exists(sLocalPath.Substring(0, sLocalPath.LastIndexOf(@"\")))) Directory.CreateDirectory(sLocalPath.Substring(0, sLocalPath.LastIndexOf(@"\")));

                if (bShowBar == true)
                {
                    if (_bar.Visible == false) _bar.Show();
                    if (sBarDisplayText == null) _bar.SetLabel(Path.GetFileName(Url.LocalPath));
                    else _bar.SetLabel(sBarDisplayText);
                }
                downloadfinished = false;
                _downloader.DownloadFileAsync(Url, sLocalPath);
                Application.DoEvents();
                while (downloadfinished == false)
                    Application.DoEvents();
            }
        }

        public void ExtractZipFiles(string archiveFilenameIn, string outFolder)
        {
            ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(archiveFilenameIn);
                zf = new ZipFile(fs);
                foreach (ZipEntry zipEntry in zf)
                {
                    // ignore the META-INF folder
                    if (zipEntry.Name.Contains("META-INF")) continue;

                    if (!zipEntry.IsFile)
                    {
                        continue;           // Ignore directories
                    }
                    string entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    byte[] buffer = new byte[4096];     // 4K is optimum
                    Stream zipStream = zf.GetInputStream(zipEntry);

                    // Manipulate the output filename here as desired.
                    string fullZipToPath = Path.Combine(outFolder, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(directoryName);

                    if (!zipEntry.IsDirectory)
                    {
                        // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                        // of the file, but does not waste memory.
                        // The "using" will close the stream even if an exception occurs.
                        using (FileStream streamWriter = File.Create(fullZipToPath))
                        {
                            StreamUtils.Copy(zipStream, streamWriter, buffer);
                        }
                    }
                }
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources
                }
            }
        }

        public void ExtractZipFiles(string archiveFilenameIn, string outFolder, List<string> filesToExtract, bool keepPath = true)
        {
            ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(archiveFilenameIn);
                zf = new ZipFile(fs);
                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;           // Ignore directories
                    }

                    string entryFileName = zipEntry.Name;
                    if (filesToExtract.Contains(entryFileName))
                    {

                        byte[] buffer = new byte[4096];     // 4K is optimum
                        Stream zipStream = zf.GetInputStream(zipEntry);

                        // Manipulate the output filename here as desired.
                        string fullZipToPath = Path.Combine(outFolder, entryFileName);
                        if (keepPath == false)
                        {
                            fullZipToPath = Path.Combine(outFolder, Path.GetFileName(entryFileName));
                        }

                        string directoryName = Path.GetDirectoryName(fullZipToPath);
                        if (directoryName.Length > 0)
                            Directory.CreateDirectory(directoryName);

                        if (!zipEntry.IsDirectory)
                        {
                            // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                            // of the file, but does not waste memory.
                            // The "using" will close the stream even if an exception occurs.
                            using (FileStream streamWriter = File.Create(fullZipToPath))
                            {
                                StreamUtils.Copy(zipStream, streamWriter, buffer);
                            }
                        }
                    }
                }
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources
                }
            }
        }


    }
}
