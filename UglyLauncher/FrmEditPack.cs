using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;


namespace UglyLauncher
{
    public partial class FrmEditPack : Form
    {
        private string sPackName;
        private Minecraft.Launcher L = new Minecraft.Launcher(false);

        public FrmEditPack(string sPackName)
        {
            // Store Parameter
            this.sPackName = sPackName;
            // init
            InitializeComponent();
        }

        private void GetActiveMods(string sPackname)
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
                lst_enabled.Items.Add(mItem);

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
                lst_availble.Items.Add(mItem);
            }
            
        }

        private string GetModName(string sJson)
        {
            string[] sLines = sJson.Replace("\r", "").Split('\n');
            string sModName = null;
            foreach (string sLine in sLines)
            {
                if (sLine.Contains("\"name\""))
                {
                    string[] Line = sLine.Split(':');
                    sModName = Line[1].Trim().Replace("\"", "").Trim();
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
            string sModDescription = null;
            foreach (string sLine in sLines)
            {
                if (sLine.Contains("\"description\""))
                {
                    string[] Line = sLine.Split(':');
                    sModDescription = Line[1].Trim().Replace("\"", "").Trim();
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
            string sModVersion = null;
            foreach (string sLine in sLines)
            {
                if (sLine.Contains("\"version\""))
                {
                    string[] Line = sLine.Split(':');
                    sModVersion = Line[1].Trim().Replace("\"", "").Trim();
                    sModVersion = sModVersion.Remove(sModVersion.Length - 1).Trim();
                    return sModVersion;
                }
            }
            return null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void frm_EditPack_Shown(object sender, EventArgs e)
        {
            Init();
            
        }

        private void Init()
        {
            lst_availble.Items.Clear();
            lst_enabled.Items.Clear();
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
            //btn_disable_selected.Enabled = false;
            //btn_enable_selected.Enabled = false;
        }


        private void btn_enable_selected_Click(object sender, EventArgs e)
        {
            if (lst_availble.SelectedIndex == -1) return;
            ListBoxItem selected = (lst_availble.SelectedItem as ListBoxItem);

            if (File.Exists(selected.FileName))
            {
                File.Move(selected.FileName, selected.FileName.Replace(".disabled",""));
            }

            Init();

        }

        private void btn_enable_all_Click(object sender, EventArgs e)
        {

        }

        private void btn_disable_all_Click(object sender, EventArgs e)
        {

        }

        private void btn_disable_selected_Click(object sender, EventArgs e)
        {
            if (lst_enabled.SelectedIndex == -1) return;
            ListBoxItem selected = (lst_enabled.SelectedItem as ListBoxItem);

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
