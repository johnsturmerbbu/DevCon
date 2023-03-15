using BBAppFxLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFxApi
{
    public class LookupIdSearch:SearchListBase
    {

        public string LookupId { get; set; }
        public LookupIdSearch() : base(new Guid("fdf9d631-5277-4300-80b3-fdf5fb8850ec")) { }


    }
}
