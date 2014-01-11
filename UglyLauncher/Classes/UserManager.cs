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

        public void LoadUserList()
        {
            XMLStotage XML = new XMLStotage();

            if (File.Exists(AppPathes.sDataDir + xmlfile))
            {
                // load that shit
            }
            else this.createEmptyFile();
        }

        public void SaveUserList()
        {
            XMLStotage XML = new XMLStotage();
        }

        private void createEmptyFile()
        {
            XMLStotage XML = new XMLStotage();
            users UsersFile = new users();
            UsersFile.activeAccount = "none";

            XML.SerializeObject(UsersFile, AppPathes.sDataDir + xmlfile);
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
