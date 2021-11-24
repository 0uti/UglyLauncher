using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UglyLauncher.Internet
{
    class MyWebClient : WebClient
    {
        public int Timeout { get; set; }


        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest r = base.GetWebRequest(address);
            r.Timeout = Timeout;
            return r;
        }
    }
}
