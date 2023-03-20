using Blackbaud.AppFx.WebAPI.ServiceProxy;
using Blackbaud.AppFx.XmlTypes.DataForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFxApi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private AppFxWebService Service { get; set; }
        private Guid ContextId { get; set; }
        private void InitAppFx()
        {
            this.tbOutputLog.Text += System.DateTime.Now.ToString() + "\t" + "Initializing Service" + "\r\n";
            if (Service == null)
                Service = new AppFxWebService();
            if (Service.Credentials == null)
                Service.Credentials = CredentialCache.DefaultCredentials;
            Service.Url = @"https://localhost/bbappfxSDKDev/Appfxwebservice.asmx?DatabaseName=BBInfinity";
            this.tbOutputLog.Text += System.DateTime.Now.ToString() + "\t" + "Service Initialized" + "\r\n";

        }
        private ClientAppInfoHeader GetRequestHeader()
        {
            this.tbOutputLog.Text += System.DateTime.Now.ToString() + "\t" + "Building Request Header" + "\r\n";
            ClientAppInfoHeader result = new ClientAppInfoHeader();
            result.REDatabaseToUse = "BBInfinity";
            result.ClientAppName = "AppFxTester";
            this.tbOutputLog.Text += System.DateTime.Now.ToString() + "\t" + "Request Header Built" + "\r\n";
            return result;
        }

        private void SearchByLookupId()
        {

            LookupIdSearch lookup = new LookupIdSearch();
            lookup.URL = @"https://localhost/bbappfxSDKDev/Appfxwebservice.asmx?DatabaseName=BBInfinity";
            lookup.UserCredential = CredentialCache.DefaultCredentials;
            lookup.DBToUse = "BBInfinity";
            lookup.ONLYPRIMARYADDRESS = true;
            lookup.CONSTITUENTQUICKFIND = tbLookupId.Text;
            lookup.ExecuteSearch();

            lbID.Text = lookup.Id;
            lbName.Text = lookup.Name;
            lbAddressBlock.Text = lookup.AddressBlock;
            lbCity.Text = lookup.City;
            lbState.Text = lookup.State;
            lbCountry.Text = lookup.Country;
            lbPostCode.Text = lookup.PostalCode;
            lbNickName.Text = LookupNickname(new Guid(lbID.Text));
            tbNewNickName.Text = lbNickName.Text;
            ContextId = new Guid(lookup.Id);
        }
        private string LookupNickname(Guid ConstituentId)
        {
            string result = string.Empty;
            AdHocQueryProcessRequest qReq = new AdHocQueryProcessRequest();
            AdHocQueryProcessReply qReply;
            qReq.ClientAppInfo = GetRequestHeader();
            qReq.AdHocQueryID = new Guid("2bb763b5-7062-4988-95d9-aca7041ab735");
            qReply = Service.AdHocQueryProcess(qReq);
            var Nickname = from c in qReply.Output.Rows where c.Values[2].ToString() == ConstituentId.ToString() select c.Values[1];
            result = Nickname.ToArray<string>()[0];
            return result;
        }
        private void UpdateNickname()
        {
            BioEdit ConstUpdate = new BioEdit(ContextId);
            ConstUpdate.URL = @"https://localhost/bbappfxSDKDev/Appfxwebservice.asmx?DatabaseName=BBInfinity";
            ConstUpdate.UserCredential = CredentialCache.DefaultCredentials;
            ConstUpdate.DBToUse = "BBInfinity";
            ConstUpdate.LoadDataForm();
            ConstUpdate.NICKNAME = tbNewNickName.Text;
            ConstUpdate.SaveDataForm();
            SearchByLookupId();
            


        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.tbOutputLog.Text += System.DateTime.Now.ToString() + "\t" + "Executing Search" + "\r\n";
            InitAppFx();
            SearchByLookupId();
            this.tbOutputLog.Text += System.DateTime.Now.ToString() + "\t" + "Search Complete" + "\r\n";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateNickname();
        }
    }
}
