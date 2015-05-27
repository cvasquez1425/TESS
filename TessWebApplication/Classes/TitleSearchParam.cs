using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Greenspoon.Tess.Classes {
    public class TitleSearchParam {
        public int? ProjectId            { get; set; }
        public int BatchEscrowId        { get; set; }
        public int ContractId           { get; set; }
        public int Contract_IntervalId  { get; set; }
        public int LegalNameId          { get; set; }

        public TitleSearchParam() { }
    }
}