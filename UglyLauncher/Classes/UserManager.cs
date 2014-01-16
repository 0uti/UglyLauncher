using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace UglyLauncher
{
    public class UserManager
    {
        private string xmlfile = @"\users.xml";
        private MCUser Users = new MCUser();

        public UserManager()
        {
            if (!File.Exists(AppPathes.sDataDir + xmlfile)) this.createEmptyFile();
            else this.LoadXML();
        }

        private void LoadXML()
        {
            XMLStotage XML = new XMLStotage();
            this.Users = XML.DeSerializeObject<MCUser>(AppPathes.sDataDir + this.xmlfile);
        }

        public void SaveXML()
        {
            XMLStotage XML = new XMLStotage();
            XML.SerializeObject(this.Users, AppPathes.sDataDir + this.xmlfile);
        }

        private void CreateXML()
        {
            XMLStotage XML = new XMLStotage();
            this.Users.activeAccount = "none";
            this.SaveXML();
        }

        public MCUser GetAccounts()
        {
           return this.Users;
        }

        public MCUserAccount GetAccount(string sUsername)
        {
            foreach (MCUserAccount Account in this.Users.accounts)
            {
                if (Account.username == sUsername) return Account;
            }
            return null;
        }

        public string GetDefault()
        {
            return Users.activeAccount;
        }

        public void SetDefault(string accountname)
        {
            this.Users.activeAccount = accountname;
            this.SaveXML();
        }

        public MCUserAccountProfile GetActiveProfile(MCUserAccount Account)
        {
            foreach (MCUserAccountProfile Profile in Account.profiles)
            {
                if (Profile.id == Account.activeProfile) return Profile;
            }
            return null;
        }

        



        // Alter Scheiss !!
        public MCUser LoadUserListO()
        {
            XMLStotage XML = new XMLStotage();
            MCUser UserObj = new MCUser();

            if (!File.Exists(AppPathes.sDataDir + xmlfile)) this.createEmptyFile();
            UserObj = XML.DeSerializeObject<MCUser>(AppPathes.sDataDir + xmlfile);
            return UserObj;
        }

        public void SaveUserList(MCUser UserObj)
        {
            XMLStotage XML = new XMLStotage();
            XML.SerializeObject(UserObj, AppPathes.sDataDir + xmlfile);
        }

        private void createEmptyFile()
        {
            XMLStotage XML = new XMLStotage();
            MCUser UsersFile = new MCUser();
            UsersFile.activeAccount = "none";

            XML.SerializeObject(UsersFile, AppPathes.sDataDir + xmlfile);
        }

        public void SetDefault_O(string accountname)
        {
            MCUser Users = new MCUser();
            Users = LoadUserListO();
            Users.activeAccount = accountname;
            SaveUserList(Users);
        }

        public string GetDefault_O()
        {
            MCUser Users = new MCUser();
            Users = LoadUserListO();
            return Users.activeAccount;
        }

        public int GetProfileXmlId_O(string sAccount)
        {
            MCUser UserObj = new MCUser();
            UserObj = LoadUserListO();

            for (int i = 0; i < UserObj.accounts.Count; i++)
            {
                if (UserObj.accounts[i].username == sAccount)
                {
                    return i;
                }
            }
            return -1;
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
