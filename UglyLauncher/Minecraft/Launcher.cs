using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Minecraft
{
    class Launcher
    {
        public string sPackServer = "http://outi-networks.de/UglyLauncher";
        public string sVersionServer = "http://s3.amazonaws.com/Minecraft.Download/versions";

        // Get Package liste from Server
        public MCPacksAvailable GetClientPackList(string MCPlayerName)
        {
            MCPacksAvailable Packs = new MCPacksAvailable();
            string jsonString = Internet.Http.GET(sPackServer + @"/packs.php?player=" + MCPlayerName);

            Packs = UglyLauncher.JsonHelper.JsonDeserializer<MCPacksAvailable>(jsonString);
            return Packs;
        }

        // Get installes packages
        public MCPacksInstalled GetInstalledPacks()
        {
            MCPacksInstalled Packs = new MCPacksInstalled();
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(UglyLauncher.AppPathes.sPacksDir));

            foreach (var dir in dirs)
            {
                if (File.Exists(dir + @"\version") && File.Exists(dir + @"\pack.json"))
                {
                    MCPacksInstalled.pack pack = new MCPacksInstalled.pack();
                    pack.name = dir.Substring(dir.LastIndexOf("\\") + 1);
                    // Get versions files
                    pack.current_version = File.ReadAllText(dir + @"\version").Trim();
                    if (File.Exists(dir + @"\selected")) pack.selected_version = File.ReadAllText(dir + @"\selected").Trim();
                    Packs.packs.Add(pack);
                }
            }
            return Packs;
        }

        // Check if Pack is Installed (and the right version)
        public bool IsPackInstalled(string sPackName, string sPackVersion)
        {
            // find Pack in List
            int iPackId = -1;
            for (int i = 0; i < Statics.PacksInstalled.packs.Count; i++)
                if (Statics.PacksInstalled.packs[i].name == sPackName) iPackId = i;

            // return false if pack not found
            if (iPackId == -1) return false;

            MCPacksInstalled.pack oInstalledPack = new MCPacksInstalled.pack();
            oInstalledPack = Statics.PacksInstalled.packs[iPackId];

            // if recommended version, getting the version from available packs
            if (sPackVersion == "recommended")
            {
                iPackId = -1;
                for (int i = 0; i < Statics.PacksAvailable.packs.Count; i++)
                    if (Statics.PacksAvailable.packs[i].name == sPackName) iPackId = i;

                MCPacksAvailable.pack oAvailablePack = new MCPacksAvailable.pack();
                oAvailablePack = Statics.PacksAvailable.packs[iPackId];
                sPackVersion = oAvailablePack.recommended_version;
            }

            // check if version is installed
            if (oInstalledPack.current_version != sPackVersion) return false;

            // Pack is fine :)
            return true;
        }

        // prepare Pack
        public void PreparePack(string sPackName)
        {
            // Getting pack.json
            string sPackJson = File.ReadAllText(UglyLauncher.AppPathes.sPacksDir + @"\" + sPackName + @"\pack.json").Trim();
            MCGameStructure MCLocal = UglyLauncher.JsonHelper.JsonDeserializer<MCGameStructure>(sPackJson);

            // Getting Mojang Version json
            string sVersionJson = Internet.Http.GET(sVersionServer + "/" + MCLocal.id + "/" + MCLocal.id + ".json");
            MCGameStructure MCMojang = UglyLauncher.JsonHelper.JsonDeserializer<MCGameStructure>(sVersionJson);

            // Merging Objects
            MCMojang = this.MergeObjects(MCLocal, MCMojang);


        }

        // install Pack
        public void InstallPack(string sPackname, string sPackVersion)
        {

        }




        private MCGameStructure MergeObjects(MCGameStructure oPack, MCGameStructure oMojang)
        {

            return oMojang;
        }

    }
}
