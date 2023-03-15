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
            lbNickName.Text = LookupNickname(new Guid(lbID.Text));
            tbNewNickName.Text = lbNickName.Text;
            ContextId = new Guid(ExactMatch[0].Values[0].ToString());
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
        private bool UpdateNickname()
        {
            DataFormLoadRequest lReq = new DataFormLoadRequest();
            DataFormSaveRequest sReq = new DataFormSaveRequest();
            DataFormLoadReply lReply;
            DataFormSaveReply sReply;
            lReq.ClientAppInfo = GetRequestHeader();
            lReq.ContextRecordID = ContextId.ToString();
            lReq.RecordID = ContextId.ToString();
            lReq.FormID = new Guid("788ab947-26ed-40c4-865e-8fe29577e593");
            lReply = Service.DataFormLoad(lReq);
            sReq = new DataFormSaveRequest();
            sReq.ClientAppInfo = GetRequestHeader();
            sReq.ID = lReq.RecordID;
            sReq.ContextRecordID = lReq.ContextRecordID;
            sReq.DataFormItem = lReply.DataFormItem;
            sReq.DataFormItem.SetValue("NICKNAME", tbNewNickName.Text);
            sReq.FormID = lReq.FormID;
            sReply = Service.DataFormSave(sReq);
            return false;


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
