using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace UglyLauncher
{
    class UserManager
    {
        public string xmlfile = @"\users.xml";

        public users LoadUserList()
        {
            XMLStotage XML = new XMLStotage();
            users UserObj = new users();

            if (!File.Exists(AppPathes.sDataDir + xmlfile)) this.createEmptyFile();
            UserObj = XML.DeSerializeObject<users>(AppPathes.sDataDir + xmlfile);
            return UserObj;
        }

        public void SaveUserList(users UserObj)
        {
            XMLStotage XML = new XMLStotage();
            XML.SerializeObject(UserObj, AppPathes.sDataDir + xmlfile);
        }

        private void createEmptyFile()
        {
            XMLStotage XML = new XMLStotage();
            users UsersFile = new users();
            UsersFile.activeAccount = "none";

            XML.SerializeObject(UsersFile, AppPathes.sDataDir + xmlfile);
        }

        public void SetDefault(string accountname)
        {
            users Users = new users();
            Users = LoadUserList();
            Users.activeAccount = accountname;
            SaveUserList(Users);
        }

        public string GetDefault()
        {
            users Users = new users();
            Users = LoadUserList();
            return Users.activeAccount;
        }

        public int GetProfileXmlId(string sAccount)
        {
            users UserObj = new users();
            UserObj = LoadUserList();

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
    public class users
    {
        [DataMember]
        public List<account> accounts = new List<account>();
        [DataMember]
        public string activeAccount { get; set; }

        [DataContractAttribute]
        public struct account
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
            public List<profile> profiles;
        }

        [DataContractAttribute]
        public struct profile
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public bool legacy { get; set; }
        }
    }
}
