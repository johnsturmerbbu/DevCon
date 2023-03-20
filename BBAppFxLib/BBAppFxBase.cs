using Blackbaud.AppFx.WebAPI.ServiceProxy;
using Blackbaud.AppFx.XmlTypes.DataForms;
using System;
using System.Linq;
using System.Net;
using System.Reflection;

namespace BBAppFxLib
{

    public class DoNotDefaultField : Attribute
    {
    }

    public class FormFieldProperty : Attribute
    {
    }
    public class BBAppFxBase
    {
        public AppFxWebService Service { get; set; }
        public ICredentials UserCredential { get; set; }
        public string URL { get; set; }
        public string DBToUse { get; set; }
        public Guid ObjectId { get; set; }
        public string AppName { get; set; }

        public BBAppFxBase() { }
        public BBAppFxBase(Guid objectId)
        {
            ObjectId = objectId;
        }
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

        public void InitAppFx()
        {
            if (Service == null)
                Service = new AppFxWebService();
            if ((Service.Credentials == null || UserCredential != Service.Credentials) && UserCredential != null)
                Service.Credentials = UserCredential;
            if ((string.IsNullOrEmpty(Service.Url) || Service.Url != URL) && !string.IsNullOrEmpty(URL))
                Service.Url = URL;
        }
        public ClientAppInfoHeader GetRequestHeader()
        {
            ClientAppInfoHeader header = new ClientAppInfoHeader();
            header.ClientAppName = AppName;
            header.REDatabaseToUse = DBToUse;
            return header;

        }


    }

    public class FormFilterBase : BBAppFxBase
    {
        public DataFormFieldValueSet FieldValueSet { get; set; }
        public DataFormItem FormItem { get; protected set; }
        public FormFilterBase(Guid ObjId) : base(ObjId)
        {
            FieldValueSet = new DataFormFieldValueSet();
            FormItem = new DataFormItem();
        }
        public void ExecuteLoad()
        {
            PopulateFieldValues();
            FormItem.Values = FieldValueSet;
            InitAppFx();
        }
        protected void PopulateFieldValues()
        {
            var props = this.GetType().GetProperties().Where<PropertyInfo>(prop => prop.GetCustomAttribute<FormFieldProperty>(true) != null);
            PropertyInfo[] PropList = props.ToArray<PropertyInfo>();
            foreach (PropertyInfo p in PropList)
            {
                if (p.GetCustomAttribute<DoNotDefaultField>() == null)
                {
                    if (p.PropertyType == typeof(String) && p.GetValue(this) == null)
                        FieldValueSet.SetValue(p.Name, string.Empty);
                    else
                        FieldValueSet.SetValue(p.Name, p.GetValue(this));
                }
            }
        }


    }


    public class ContextFormFilterBase : FormFilterBase
    {
        public string ContextRecordId { get; set; }
        public ContextFormFilterBase(Guid ObjId) : base(ObjId) { }
        public ContextFormFilterBase(Guid ObjId, string ContextId)
            : base(ObjId)
        {
            ContextRecordId = ContextId;
        }

    }

    public class SearchListBase : FormFilterBase
    {
        public SearchListLoadRequest ReqObj { get; set; }
        public SearchListLoadReply ReplyObj { get; set; }
        public SearchListBase(Guid SearchListId)
            : base(SearchListId)
        {
            ReqObj = new SearchListLoadRequest();
            ReqObj.SearchListID = ObjectId;
        }
        public virtual void ExecuteSearch()
        {
            ExecuteLoad();
            ReqObj.Filter = FormItem;
            ReqObj.SearchListID = ObjectId;
            ReqObj.ClientAppInfo = GetRequestHeader();
            ReplyObj = Service.SearchListLoad(ReqObj);
        }
    }
    public class DataListBase : ContextFormFilterBase
    {
        public DataListLoadRequest ReqObj { get; set; }
        public DataListLoadReply ReplyObj { get; set; }
        public DataListBase(Guid DataListId, string ContextId)
            : base(DataListId, ContextId)
        {
            ReqObj = new DataListLoadRequest();
            ReqObj.DataListID = ObjectId;
        }

        public DataListBase(Guid DataListId)
            : base(DataListId)
        {
            ReqObj = new DataListLoadRequest();
            ReqObj.DataListID = ObjectId;
        }
        public void ExecuteList()
        {
            ExecuteLoad();
            ReqObj.Parameters = FormItem;
            ReqObj.DataListID = ObjectId;
            ReqObj.ClientAppInfo = GetRequestHeader();
            ReqObj.ContextRecordID = ContextRecordId;
            ReplyObj = Service.DataListLoad(ReqObj);
        }

    }

    public class DataFormBase : ContextFormFilterBase
    {
        public DataFormMode FormMode { get; set; }
        public DataFormLoadRequest ReqObj { get; set; }
        public DataFormLoadReply ReplyObj { get; set; }
        public DataFormBase(Guid DataFormId, string ContextId) : base(DataFormId, ContextId)
        {
            ReqObj = new DataFormLoadRequest();
            ReqObj.FormID = ObjectId;
            ReqObj.RecordID = ContextRecordId;

        }
        public DataFormBase(Guid DataFormId)
            : base(DataFormId)
        {
            ReqObj = new DataFormLoadRequest();
            ReqObj.FormID = ObjectId;

        }
        protected void PopulateObjectProperties()
        {
            foreach (DataFormFieldValue v in ReplyObj.DataFormItem.Values)
            {
                if (this.GetType().GetProperty(v.ID) != null)
                    this.GetType().GetProperty(v.ID).SetValue(this, v.Value);
            }
        }

        public virtual void LoadDataForm()
        {

            ReqObj.ContextRecordID = ContextRecordId;
            ReqObj.FormID = ObjectId;
            InitAppFx();
            ReqObj.ClientAppInfo = GetRequestHeader();
            ReplyObj = Service.DataFormLoad(ReqObj);
            FormItem = ReplyObj.DataFormItem;
            FieldValueSet = FormItem.Values;
            PopulateObjectProperties();

        }
    }
    public class WriteableDataFormBase : DataFormBase
    {
        public DataFormSaveRequest sReqObj { get; set; }
        public DataFormSaveReply sReplyObj { get; set; }

        public WriteableDataFormBase(Guid DataFormId, string ContextId)
            : base(DataFormId, ContextId)
        {
            //LoadDataForm();
        }
        public WriteableDataFormBase(Guid DataFormId)
            : base(DataFormId)
        {
            //LoadDataForm();
        }
        public virtual void SaveDataForm()
        {

            PopulateFieldValues();
            FormItem.Values = FieldValueSet;
            sReqObj.DataFormItem = FormItem;
            sReqObj.ContextRecordID = ContextRecordId;
            sReqObj.ID = ContextRecordId;
            sReqObj.FormID = ReqObj.FormID;
            sReqObj.ClientAppInfo = GetRequestHeader();
            sReplyObj = Service.DataFormSave(sReqObj);
        }
        public override void LoadDataForm()
        {
            sReqObj = new DataFormSaveRequest();
            base.LoadDataForm();
            sReqObj.DataFormItem = FormItem;
            PopulateFieldValues();

        }
    }

}
