using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
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
            try
            {
                using (ZipArchive zip = ZipFile.OpenRead(archiveFilenameIn))
                {
                    foreach (ZipArchiveEntry zipEntry in zip.Entries)
                    {
                        // ignore directories
                        if (zipEntry.Length == 0) continue;

                        // ignore the META-INF folder
                        if (zipEntry.FullName.Contains("META-INF")) continue;

                        // Gets the full path to ensure that relative segments are removed.
                        string destinationPath = Path.GetFullPath(Path.Combine(outFolder, zipEntry.FullName));

                        // create directory
                        if (!Directory.Exists(Path.GetDirectoryName(destinationPath))) Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                        // Ordinal match is safest, case-sensitive volumes can be mounted within volumes that
                        // are case-insensitive.
                        if (destinationPath.StartsWith(outFolder, StringComparison.Ordinal))
                        {
                            zipEntry.ExtractToFile(destinationPath,true);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void ExtractZipFiles(string archiveFilenameIn, string outFolder, List<string> filesToExtract, bool keepPath = true)
        {
            try
            {
                using (ZipArchive zip = ZipFile.OpenRead(archiveFilenameIn))
                {
                    foreach (ZipArchiveEntry zipEntry in zip.Entries)
                    {
                        // ignore directories
                        if (zipEntry.Length == 0) continue;

                        // ignore the META-INF folder
                        if (zipEntry.FullName.Contains("META-INF")) continue;

                        if (filesToExtract.Contains(zipEntry.FullName))
                        {
                        
                            // Gets the full path to ensure that relative segments are removed.
                            string destinationPath = Path.GetFullPath(Path.Combine(outFolder, zipEntry.FullName));
                            if (keepPath == false)
                            {
                                destinationPath = Path.GetFullPath(Path.Combine(outFolder, zipEntry.Name));
                            }

                            // create directory
                            if (!Directory.Exists(Path.GetDirectoryName(destinationPath))) Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                            // Ordinal match is safest, case-sensitive volumes can be mounted within volumes that
                            // are case-insensitive.
                            if (destinationPath.StartsWith(outFolder, StringComparison.Ordinal))
                            {
                                zipEntry.ExtractToFile(destinationPath,true);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
