using Blackbaud.AppFx.WebAPI.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BBAppFxLib
{
    public class BBAppFxBase
    {
        public AppFxWebService Service { get; set; }
        public ICredentials UserCredential { get; set; }
        public string URL { get; set; }
        public string DBToUse { get; set; }

        public void Authenticate()
        {
            if (UserCredential == null)
                UserCredential = CredentialCache.DefaultCredentials;
            Service.Credentials = UserCredential;

        }
        public void Authenticate(ICredentials newCredential)
        {
            if (newCredential != UserCredential)
                UserCredential = newCredential;
            Authenticate();
        }

    }
}
