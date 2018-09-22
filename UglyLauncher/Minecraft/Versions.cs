using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using UglyLauncher.Internet;
using UglyLauncher.Minecraft.Json.Version;
using UglyLauncher.Minecraft.Json.MCVersions;

namespace UglyLauncher.Minecraft
{
    class Versions
    {
        private readonly string _VersionManifest = "https://launchermeta.mojang.com/mc/game/version_manifest.json";
        private MCVersions _versions = null;

        public Versions()
        {
         
        }

        private void GetVersionManifest()
        {
            try
            {
                string sVersionManifest = Http.GET(_VersionManifest);
                _versions = MCVersions.FromJson(sVersionManifest);
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

                foreach (MCVersionsVersion version in _versions.Versions)
                {
                    switch(version.Type)
                    {
                        case TypeEnum.Snapshot:
                            if (bSnapshots == true) versions.Add(version.Id); break;
                        case TypeEnum.OldBeta:
                            if (bBeta == true) versions.Add(version.Id); break;
                        case TypeEnum.OldAlpha:
                            if (bAlpha== true) versions.Add(version.Id); break;
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

        public MCVersionsVersion GetVersion(string sVersion)
        {
            MCVersionsVersion oVersion = null;

            try
            {
                if (_versions == null) GetVersionManifest();

                foreach (MCVersionsVersion version in _versions.Versions)
                {
                    if (version.Id.Equals(sVersion))
                    {
                        oVersion = version;
                        break;
                    }
                }
                // throw execption when version not found
                if(oVersion == null) throw new Exception("Minecraft version not found.");

                //return verison object
                return oVersion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






    }
}
