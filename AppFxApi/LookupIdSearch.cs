using BBAppFxLib;
using Blackbaud.AppFx.WebAPI.ServiceProxy;
using Blackbaud.AppFx.XmlTypes.DataForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFxApi
{
    public class LookupIdSearch:SearchListBase
    {

        public bool ONLYPRIMARYADDRESS { get; set; }
        public string CONSTITUENTQUICKFIND { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string AddressBlock { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public Guid ConstituentId { get; set; }    
        public LookupIdSearch() : base(new Guid("fdf9d631-5277-4300-80b3-fdf5fb8850ec")) { }

        public override void ExecuteSearch()
        {
            FieldValueSet.Add(new DataFormFieldValue("ONLYPRIMARYADDRESS", ONLYPRIMARYADDRESS));
            FieldValueSet.Add(new DataFormFieldValue("CONSTITUENTQUICKFIND", CONSTITUENTQUICKFIND));
            base.ExecuteSearch();
            var ExactMatch = (from c in ReplyObj.Output.Rows where c.Values[1] == CONSTITUENTQUICKFIND select c).ToArray<ListOutputRow>();
            Id = ExactMatch[0].Values[0].ToString();
            Name = ExactMatch[0].Values[10].ToString();
            AddressBlock = ExactMatch[0].Values[4].ToString();
            City = ExactMatch[0].Values[5].ToString();
            State = ExactMatch[0].Values[6].ToString();
            Country = ExactMatch[0].Values[7].ToString();
            PostalCode = ExactMatch[0].Values[8].ToString();


        }

    }
}
