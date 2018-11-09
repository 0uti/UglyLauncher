using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UglyLauncher.Internet;
using UglyLauncher.Minecraft.Files.Json.Assets;
using UglyLauncher.Minecraft.Files.Json.GameVersion;
using UglyLauncher.Minecraft.Files.Json.GameVersionList;
using UglyLauncher.Settings;

namespace UglyLauncher.Minecraft.Files
{
    class FileStorage
    {
        // remote path
        private readonly string _sAssetsFileServer = "https://resources.download.minecraft.net";
        private readonly string _VersionManifest = "https://launchermeta.mojang.com/mc/game/version_manifest.json";

        private GameVersionList _versions = null;

        private DownloadHelper dhelper;

        public FileStorage(DownloadHelper dhelper)
        {
            this.dhelper = dhelper;
        }

        private void GetVersionManifest()
        {
            try
            {
                string sVersionManifest = Http.GET(_VersionManifest);
                _versions = GameVersionList.FromJson(sVersionManifest);
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetVersions(bool bSnapshots, bool bBeta, bool bAlpha)
        {
            List<string> versions = new List<string>();

            try
            {
                if (_versions == null) GetVersionManifest();

                foreach (VersionsVersion version in _versions.Versions)
                {
                    switch (version.Type)
                    {
                        case TypeEnum.Snapshot:
                            if (bSnapshots == true) versions.Add(version.Id); break;
                        case TypeEnum.OldBeta:
                            if (bBeta == true) versions.Add(version.Id); break;
                        case TypeEnum.OldAlpha:
                            if (bAlpha == true) versions.Add(version.Id); break;
                        default:
                            versions.Add(version.Id); break;
                    }
                }
                return versions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VersionsVersion GetVersion(string sVersion)
        {
            VersionsVersion oVersion = null;

            try
            {
                if (_versions == null) GetVersionManifest();

                foreach (VersionsVersion version in _versions.Versions)
                {
                    if (version.Id.Equals(sVersion))
                    {
                        oVersion = version;
                        break;
                    }
                }
                // throw execption when version not found
                if (oVersion == null) throw new Exception("Minecraft version not found.");

                //return verison object
                return oVersion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DownloadVersionJson(string mcversion)
        {
            try
            {
                // create directory if not exists
                if (!Directory.Exists(Launcher._sVersionDir + @"\" + mcversion)) Directory.CreateDirectory(Launcher._sVersionDir + @"\" + mcversion);
                
                VersionsVersion version = GetVersion(mcversion);

                // delete and download json
                if (File.Exists(Launcher._sVersionDir + @"\" + mcversion + @"\" + mcversion + ".json")) File.Delete(Launcher._sVersionDir + @"\" + mcversion + @"\" + mcversion + ".json");
                dhelper.DownloadFileTo(version.Url, Launcher._sVersionDir + @"\" + mcversion + @"\" + mcversion + ".json", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, string> DownloadMCLibraries(GameVersion MC)
        {
            Configuration c = new Configuration();
            string sJavaArch = c.GetJavaArch();

            Dictionary<string, string> ClassPath = new Dictionary<string, string>(); // Library list for startup

            foreach (Library lib in MC.Libraries)
            {
                VersionJsonDownload download;

                // skip non windows libraries
                if (lib.Rules != null)
                {
                    bool bWindows = false;
                    foreach (LibraryRule Rule in lib.Rules)
                    {
                        if (Rule.Action == "allow")
                        {
                            if (Rule.Os == null) bWindows = true;
                            else if (Rule.Os.Name == null || Rule.Os.Name == "windows") bWindows = true;
                        }
                        if (Rule.Action == "disallow" && Rule.Os.Name == "windows") bWindows = false;
                    }
                    if (bWindows == false) continue;
                }

                // Natives ?
                if (lib.Natives != null)
                {
                    download = lib.Downloads.Classifiers.GetType().GetProperty(lib.Natives.Windows.Replace("${arch}", sJavaArch).Replace("-", "")).GetValue(lib.Downloads.Classifiers, null) as VersionJsonDownload;
                }
                else
                {
                    download = lib.Downloads.Artifact;
                }
                download.Path = Launcher._sLibraryDir + @"\" + download.Path.Replace("/", @"\");

                dhelper.DownloadFileTo(download.Url, download.Path);

                // extract pack if needed
                if (lib.Extract != null)
                {
                    if (!Directory.Exists(Launcher._sNativesDir + @"\" + MC.Id)) Directory.CreateDirectory(Launcher._sNativesDir + @"\" + MC.Id);
                    dhelper.ExtractZipFiles(download.Path, Launcher._sNativesDir + @"\" + MC.Id);
                }
                else
                {
                    //lLibraries.Add(download.Path); // files needed for startup
                    string[] libname = lib.Name.Split(':');

                    //natives could lead to already exists keys
                    if (lib.Natives != null)
                    {
                        libname[1] += "-native";
                    }
                    ClassPath.Add(libname[0] + ":" + libname[1], download.Path);
                }
            }
            return ClassPath;
        }

        public void DownloadAssets(GameVersion MC)
        {
            // get assetIndex Json
            dhelper.DownloadFileTo(MC.AssetIndex.Url, Launcher._sAssetsDir + @"\indexes\" + MC.AssetIndex.Id + ".json", true, null, MC.AssetIndex.Sha1);

            // load assetIndex Json File
            Assets assets = Assets.FromJson(File.ReadAllText(Launcher._sAssetsDir + @"\indexes\" + MC.AssetIndex.Id + ".json").Trim());

            foreach (KeyValuePair<string, AssetObject> Asset in assets.Objects)
            {
                string sRemotePath = _sAssetsFileServer + "/" + Asset.Value.Hash.Substring(0, 2) + "/" + Asset.Value.Hash;
                string sLocalPath = Launcher._sAssetsDir + @"\objects\" + Asset.Value.Hash.Substring(0, 2) + @"\" + Asset.Value.Hash;

                if (!Directory.Exists(sLocalPath.Substring(0, sLocalPath.LastIndexOf(@"\")))) Directory.CreateDirectory(sLocalPath.Substring(0, sLocalPath.LastIndexOf(@"\")));

                // Download the File
                dhelper.DownloadFileTo(sRemotePath, sLocalPath);

                if (assets.Virtual == true)
                {
                    string slegacyPath = Launcher._sAssetsDir + @"\virtual\legacy\" + Asset.Key.Replace("/", @"\");
                    if (!Directory.Exists(slegacyPath.Substring(0, slegacyPath.LastIndexOf(@"\")))) Directory.CreateDirectory(slegacyPath.Substring(0, slegacyPath.LastIndexOf(@"\")));
                    File.Copy(sLocalPath, slegacyPath, true);
                }
            }
        }

        public void DownloadGameJar(GameVersion MC)
        {
            bool download = false;
            long filesize;
            string fileSHA;

            try
            {
                if (File.Exists(Launcher._sVersionDir + @"\" + MC.Id + @"\" + MC.Id + ".jar"))
                {
                    // check filesize
                    filesize = new FileInfo(Launcher._sVersionDir + @"\" + MC.Id + @"\" + MC.Id + ".jar").Length;
                    if (MC.Downloads.Client.Size != filesize)
                    {
                        File.Delete(Launcher._sVersionDir + @"\" + MC.Id + @"\" + MC.Id + ".jar");
                        download = true;
                    }

                    // check SHA
                    fileSHA = dhelper.ComputeHashSHA(Launcher._sVersionDir + @"\" + MC.Id + @"\" + MC.Id + ".jar");
                    if (!MC.Downloads.Client.Sha1.Equals(fileSHA))
                    {
                        File.Delete(Launcher._sVersionDir + @"\" + MC.Id + @"\" + MC.Id + ".jar");
                        download = true;
                    }
                }
                else download = true;

                // download jar
                if (download == true)
                {
                    dhelper.DownloadFileTo(MC.Downloads.Client.Url, Launcher._sVersionDir + @"\" + MC.Id + @"\" + MC.Id + ".jar");
                }

                // post download check
                // check filesize
                filesize = new FileInfo(Launcher._sVersionDir + @"\" + MC.Id + @"\" + MC.Id + ".jar").Length;
                if (MC.Downloads.Client.Size != filesize)
                {
                    throw new Exception("Error downloading file: " + MC.Id + ".jar (filesize mismatch)");
                }

                // check SHA
                fileSHA = dhelper.ComputeHashSHA(Launcher._sVersionDir + @"\" + MC.Id + @"\" + MC.Id + ".jar");
                if (!MC.Downloads.Client.Sha1.Equals(fileSHA))
                {
                    throw new Exception("Error downloading file: " + MC.Id + ".jar (SHA1 mismatch)");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
