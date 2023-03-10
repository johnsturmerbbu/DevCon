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
            SearchListLoadRequest lReq = new SearchListLoadRequest();
            SearchListLoadReply lReply = new SearchListLoadReply();
            DataFormFieldValueSet FVSet = new DataFormFieldValueSet();
            DataFormItem DFI = new DataFormItem();
            lReq.ClientAppInfo = GetRequestHeader();
            lReq.SearchListID = new Guid("fdf9d631-5277-4300-80b3-fdf5fb8850ec");
            FVSet.Add(new DataFormFieldValue("ONLYPRIMARYADDRESS", true));
            FVSet.Add(new DataFormFieldValue("CONSTITUENTQUICKFIND", tbLookupId.Text));
            DFI.Values = FVSet;
            lReq.Filter = DFI;
            lReply = Service.SearchListLoad(lReq);
            var ExactMatch = (from c in lReply.Output.Rows where c.Values[1] == tbLookupId.Text select c).ToArray<ListOutputRow>();

            lbID.Text = ExactMatch[0].Values[0].ToString();
            lbName.Text = ExactMatch[0].Values[10].ToString();
            lbAddressBlock.Text = ExactMatch[0].Values[4].ToString();
            lbCity.Text = ExactMatch[0].Values[5].ToString();
            lbState.Text = ExactMatch[0].Values[6].ToString();
            lbCountry.Text = ExactMatch[0].Values[7].ToString();
            lbPostCode.Text = ExactMatch[0].Values[8].ToString();
            LookupNickname(new Guid(lbID.Text));
        }
        private string LookupNickname(Guid ConstituentId)
        {
            string result = string.Empty;
            AdHocQueryProcessRequest qReq = new AdHocQueryProcessRequest();
            AdHocQueryProcessReply qReply;
            qReq.ClientAppInfo = GetRequestHeader();
            qReq.QueryViewID = new Guid("2BB763B5-7062-4988-95D9-ACA7041AB735");
            qReq.SelectFields = new AdHocQuerySelectField[2]();
            qReq.SelectFields[0] = new AdHocQuerySelectField();
            qReq.SelectFields[0].ColumnName = "NICKNAME";
            
            return result;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.tbOutputLog.Text += System.DateTime.Now.ToString() + "\t" + "Executing Search" + "\r\n";
            InitAppFx();
            SearchByLookupId();
            this.tbOutputLog.Text += System.DateTime.Now.ToString() + "\t" + "Search Complete" + "\r\n";
        }
    }
}
