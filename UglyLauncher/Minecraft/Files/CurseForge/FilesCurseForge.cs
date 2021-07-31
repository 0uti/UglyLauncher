using System;
using System.Collections.Generic;
using System.IO;
using UglyLauncher.Internet;
using UglyLauncher.Minecraft.Json.Pack;

namespace UglyLauncher.Minecraft.Files.CurseForge
{
    internal class FilesCurseForge
    {
        private readonly DownloadHelper dhelper;
        private readonly string modfile_infourl = "https://addons-ecs.forgesvc.net/api/v2/addon/{0}/file/{1}";
        private readonly StartupSide side;

        public string ModsDir { get; set; }

        public FilesCurseForge(StartupSide side, DownloadHelper dhelper)
        {
            this.dhelper = dhelper;
            this.side = side;
        }

        public void DownloadModFiles(List<MCPackCurseFile> modlist)
        {
            try
            {
                if (modlist == null || modlist.Count == 0)
                {
                    return;
                }

                if (!Directory.Exists(ModsDir))
                {
                    Directory.CreateDirectory(ModsDir);
                }

                foreach (MCPackCurseFile mcPackCurseFile in modlist)
                {
                    if ((side != StartupSide.Client || !mcPackCurseFile.Side.Equals("server")) && (side != StartupSide.Server || !mcPackCurseFile.Side.Equals("client")))
                    {
                        DownloadModFile(mcPackCurseFile.ProjectID, mcPackCurseFile.FileID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DownloadModFile(int projectID, int fileID)
        {
            try
            {
                CurseModInfo.CurseModInfo curseModInfo = CurseModInfo.CurseModInfo.FromJson(Http.GET(string.Format(modfile_infourl, projectID, fileID)));
                if (File.Exists(ModsDir + Path.DirectorySeparatorChar.ToString() + curseModInfo.FileName))
                {
                    return;
                }
                dhelper.DownloadFileTo(curseModInfo.DownloadUrl, ModsDir + Path.DirectorySeparatorChar.ToString() + curseModInfo.FileName, sBarDisplayText: curseModInfo.FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
