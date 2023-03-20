﻿using BBAppFxLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFxApi
{
    public class BioEdit : WriteableDataFormBase
    {
        [FormFieldProperty]
        public string NICKNAME { get; set; }
        public BioEdit(Guid ContextId) : base(new Guid("788ab947-26ed-40c4-865e-8fe29577e593"), ContextId.ToString()) { }
        public override void LoadDataForm()
        {
            base.LoadDataForm();
            //NICKNAME = ReplyObj.DataFormItem.Values[4].ToString();
        }
        public override void SaveDataForm()
        {
            //FieldValueSet.SetValue("NICKNAME", NICKNAME);
            base.SaveDataForm();
        }
    }
}
