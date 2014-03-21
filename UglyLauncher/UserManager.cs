using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

using UglyLauncher.Minecraft;

namespace UglyLauncher
{
    public class UserManager
    {
        private string xmlfile = @"\users.xml";
        private MCUser Users = new MCUser();

        // contructor
        public UserManager()
        {
            if (!File.Exists(Launcher.sDataDir + xmlfile)) this.CreateXML();
            else this.LoadXML();
        }

        // load XML from file
        private void LoadXML()
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(Launcher.sDataDir + this.xmlfile);
                using (StringReader read = new StringReader(xmlDocument.OuterXml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(MCUser));
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        this.Users = (MCUser)serializer.Deserialize(reader);
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

        // save XML to file
        private void SaveXML()
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(this.Users.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, this.Users);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(Launcher.sDataDir + this.xmlfile);
                    stream.Close();
                }
            }
            catch (Exception)
            {
                //Log exception here
            }
        }

        // create empty XML
        private void CreateXML()
        {
            this.Users.activeAccount = "none";
            this.SaveXML();
        }

        // get all accounts
        public MCUser GetAccounts()
        {
           return this.Users;
        }

        // get one account
        public MCUserAccount GetAccount(string sAccountname)
        {
            foreach (MCUserAccount Account in this.Users.accounts)
                if (Account.username == sAccountname) return Account;
            return null;
        }

        // get num accounts
        public int GetNumAccounts()
        {
            return this.Users.accounts.Count;
        }

        // get default account
        public string GetDefault()
        {
            return Users.activeAccount;
        }

        // set default account
        public void SetDefault(string sAccountname)
        {
            this.Users.activeAccount = sAccountname;
            this.SaveXML();
        }

        // get active profile from account
        public MCUserAccountProfile GetActiveProfile(MCUserAccount Account)
        {
            foreach (MCUserAccountProfile Profile in Account.profiles)
                if (Profile.id == Account.activeProfile) return Profile;
            return null;
        }

        // delete account
        public void DeleteAccount(string sAccountname)
        {
            MCUserAccount Account = this.GetAccount(sAccountname);
            this.Users.accounts.Remove(Account);
            if (this.Users.activeAccount == sAccountname) this.Users.activeAccount = "none";
            this.SaveXML();
        }

        // get ingame player name
        public string GetPlayerName(string sAccountName)
        {
            MCUserAccountProfile Profile = this.GetActiveProfile(this.GetAccount(sAccountName));
            return Profile.name;
        }

        public void AddAccount(MCUserAccount Account)
        {
            this.Users.accounts.Add(Account);
            this.SaveXML();
        }

        public void SaveAccount(MCUserAccount Account)
        {
            // this needs a better way
            bool bWasDefault = false;
            if (Account.username == this.Users.activeAccount) bWasDefault = true;

            this.DeleteAccount(Account.username);
            this.AddAccount(Account);

            if (bWasDefault) this.SetDefault(Account.username);
        }
    }

    [DataContract]
    public class MCUser
    {
        [DataMember]
        public List<MCUserAccount> accounts = new List<MCUserAccount>();
        [DataMember]
        public string activeAccount { get; set; }
    }

    [DataContract]
    public class MCUserAccount
    {
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string accessToken { get; set; }
        [DataMember]
        public string activeProfile { get; set; }
        [DataMember]
        public string clientToken { get; set; }
        [DataMember]
        public List<MCUserAccountProfile> profiles = new List<MCUserAccountProfile>();
    }

    [DataContract]
    public class MCUserAccountProfile
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public bool legacy { get; set; }
    }
}
