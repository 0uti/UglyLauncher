using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace UglyBootstrap
{
    class configuration
    {
        private string regpath = "Software\\Minestar\\UglyLauncher";

        public void SaveAppInfo()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(this.regpath);

            key.SetValue("Bootstrap_Version", Application.ProductVersion);
            key.SetValue("Bootstrap_Path", Application.ExecutablePath);
        }
    }
}
