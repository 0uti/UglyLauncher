using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

using UglyLauncher.Minecraft;

namespace UglyLauncher.AccountManager
{
    public class Manager
    {
        private string xmlfile = @"\users.xml";
        private MCUser Users = new MCUser();

        // contructor
        public Manager()
        {
            if (!File.Exists(Launcher._sDataDir + xmlfile)) CreateXML();
            else LoadXML();
        }

        // load XML from file
        private void LoadXML()
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(Launcher._sDataDir + xmlfile);
                using (StringReader read = new StringReader(xmlDocument.OuterXml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(MCUser));
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        Users = (MCUser)serializer.Deserialize(reader);
                    }
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
                XmlSerializer serializer = new XmlSerializer(Users.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, Users);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(Launcher._sDataDir + xmlfile);
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
            Users.activeAccount = Guid.Empty;
            SaveXML();
        }

        // get all accounts
        public MCUser GetAccounts()
        {
           return Users;
        }

        // get one account
        public MCUserAccount GetAccount(Guid iAccountId)
        {
            foreach (MCUserAccount Account in Users.accounts)
                if (Account.guid == iAccountId) return Account;
            return null;
        }

        // get num accounts
        public int GetNumAccounts()
        {
            return Users.accounts.Count;
        }

        // get default account
        public Guid GetDefault()
        {
            return Users.activeAccount;
        }

        // set default account
        public void SetDefault(Guid iAccountId)
        {
            Users.activeAccount = iAccountId;
            SaveXML();
        }

        // get active profile from account
        public MCUserAccountProfile GetActiveProfile(MCUserAccount Account)
        {
            foreach (MCUserAccountProfile Profile in Account.profiles)
                if (Profile.id == Account.activeProfile) return Profile;
            return null;
        }

        // delete account
        public void DeleteAccount(Guid iAccountId)
        {
            MCUserAccount Account = GetAccount(iAccountId);
            Users.accounts.Remove(Account);
            if (Users.activeAccount == iAccountId) Users.activeAccount = Guid.Empty;
            SaveXML();
        }

        // get ingame player name
        public string GetPlayerName(Guid iAccountId)
        {
            MCUserAccountProfile Profile = GetActiveProfile(GetAccount(iAccountId));
            return Profile.name;
        }

        // get minecraft Profile ID
        public string GetMCProfileID(Guid iAccountId)
        {
            MCUserAccountProfile Profile = GetActiveProfile(GetAccount(iAccountId));
            return Profile.id;
        }

        public void AddAccount(MCUserAccount Account)
        {
            Users.accounts.Add(Account);
            SaveXML();
        }

        public void SaveAccount(MCUserAccount Account)
        {
            // this needs a better way
            bool bWasDefault = false;
            if (Account.guid == Users.activeAccount) bWasDefault = true;

            DeleteAccount(Account.guid);
            AddAccount(Account);

            if (bWasDefault) SetDefault(Account.guid);
        }
    }

    [DataContract]
    public class MCUser
    {
        [DataMember]
        public List<MCUserAccount> accounts = new List<MCUserAccount>();
        [DataMember]
        public Guid activeAccount { get; set; }
    }

    [DataContract]
    public class MCUserAccount
    {
        [DataMember]
        public Guid guid { get; set; }
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
