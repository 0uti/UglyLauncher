using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace UglyLauncher
{
    public partial class FrmEditPack : Form
    {
        private readonly string sPackName;
        private readonly Minecraft.Launcher L = new Minecraft.Launcher(false);

        public FrmEditPack(string sPackName)
        {
            // Store Parameter
            this.sPackName = sPackName;
            // init
            InitializeComponent();
        }

        private void GetActiveMods(string sPackName)
        {
            List<string> lMods = L.GetModFolderContents(sPackName, new[] { ".jar", ".zip" });
            foreach (string mod in lMods)
            {
                string sModName = "";
                string sModDescription = "";
                //get mcmod.info from File (only Mods has this file)
                string sJsonMcModInfo = L.GetMcModInfo(mod);
                if (sJsonMcModInfo != null)
                {
                    sModName = GetModName(sJsonMcModInfo);
                    sModDescription = GetModDescription(sJsonMcModInfo);
                    string sModVersion = GetModVersion(sJsonMcModInfo);
                    if (sModVersion != null) sModName = sModName + " (" + sModVersion + ")";
                }
                else
                {
                    sModName = mod.Substring(mod.LastIndexOf("\\") + 1 );
                    sModDescription = "";
                }
                ListBoxItem mItem = new ListBoxItem(sModName, sModDescription, mod);
                LstEnabledMods.Items.Add(mItem);
            }
        }

        private void GetDisabledMods(string sPackname)
        {
            List<string> lMods = L.GetModFolderContents(sPackname, new[] { ".disabled" });

            foreach (string mod in lMods)
            {
                string sModName = "";
                string sModDescription = "";
                //get mcmod.info from File (only Mods has this file)
                string sJsonMcModInfo = L.GetMcModInfo(mod);
                if (sJsonMcModInfo != null)
                {
                    sModName = GetModName(sJsonMcModInfo);
                    sModDescription = GetModDescription(sJsonMcModInfo);
                    string sModVersion = GetModVersion(sJsonMcModInfo);
                    if (sModVersion != null) sModName = sModName + " (" + sModVersion + ")";
                }
                else
                {
                    sModName = mod.Substring(mod.LastIndexOf("\\") + 1);
                    sModDescription = "";
                }
                ListBoxItem mItem = new ListBoxItem(sModName, sModDescription, mod);
                LstAvailbleMods.Items.Add(mItem);
            }
        }

        private string GetModName(string sJson)
        {
            string[] sLines = sJson.Replace("\r", "").Split('\n');
            foreach (string sLine in sLines)
            {
                if (sLine.Contains("\"name\""))
                {
                    string[] Line = sLine.Split(':');
                    string sModName = Line[1].Trim().Replace("\"", "").Trim();
                    sModName = sModName.Remove(sModName.Length - 1).Trim();
                    return sModName;
                }
            }
            return null;
        }

        // description
        private string GetModDescription(string sJson)
        {
            string[] sLines = sJson.Replace("\r", "").Split('\n');
            foreach (string sLine in sLines)
            {
                if (sLine.Contains("\"description\""))
                {
                    string[] Line = sLine.Split(':');
                    string sModDescription = Line[1].Trim().Replace("\"", "").Trim();
                    sModDescription = sModDescription.Remove(sModDescription.Length - 1).Trim();
                    return sModDescription;
                }
            }
            return null;
        }
      
        // Get mod Version
        private string GetModVersion(string sJson)
        {
            string[] sLines = sJson.Replace("\r", "").Split('\n');
            foreach (string sLine in sLines)
            {
                if (sLine.Contains("\"version\""))
                {
                    string[] Line = sLine.Split(':');
                    string sModVersion = Line[1].Trim().Replace("\"", "").Trim();
                    sModVersion = sModVersion.Remove(sModVersion.Length - 1).Trim();
                    return sModVersion;
                }
            }
            return null;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void FrmEditPack_Shown(object sender, EventArgs e)
        {
            Init();
            
        }

        private void Init()
        {
            LstAvailbleMods.Items.Clear();
            LstEnabledMods.Items.Clear();
            // Fill Gui
            try
            {
                GetActiveMods(sPackName);
                GetDisabledMods(sPackName);
            }
            catch (Exception)
            {
                Dispose();
            }
            // disable Buttons
            //BtnDisableSelected.Enabled = false;
            //BtnEnableSelected.Enabled = false;
        }


        private void BtnEnableSelected_Click(object sender, EventArgs e)
        {
            if (LstAvailbleMods.SelectedIndex == -1) return;
            ListBoxItem selected = (LstAvailbleMods.SelectedItem as ListBoxItem);

            if (File.Exists(selected.FileName))
            {
                File.Move(selected.FileName, selected.FileName.Replace(".disabled",""));
            }

            Init();
        }

        private void BtnEnableAll_Click(object sender, EventArgs e)
        {

        }

        private void BtnDisableAll_Click(object sender, EventArgs e)
        {

        }

        private void BtnDisableSelected_Click(object sender, EventArgs e)
        {
            if (LstEnabledMods.SelectedIndex == -1) return;
            ListBoxItem selected = (LstEnabledMods.SelectedItem as ListBoxItem);

            if (File.Exists(selected.FileName))
            {
                File.Move(selected.FileName, selected.FileName + ".disabled");
            }

            Init();
        }
    }

    class ListBoxItem : IToolTipDisplayer
    {
        public string DisplayText { get; private set; }
        public string ToolTipText { get; private set; }
        public string FileName { get; private set; }

        // Constructor
        public ListBoxItem(string displayText, string toolTipText, string sFileName)
        {
            DisplayText = displayText;
            ToolTipText = toolTipText;
            this.FileName = sFileName;
        }

        // Returns the display text of this item.
        public override string ToString()
        {
            return DisplayText;
        }
        // Returns the tooltip text of this item.
        public string GetToolTipText()
        {
            return ToolTipText;
        }
    }

    internal interface IToolTipDisplayer
    {
        string GetToolTipText();
    }
}
