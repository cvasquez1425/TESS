using System.Collections.Generic;

namespace Greenspoon.Tess.BusinessObjects.UIObjects
{
    public class TrsDTO
    {
        public string TrId              { get; set; }
        public string TypeId            { get; set; }
        public string StartDate         { get; set; }
        public string CreatedDate       { get; set; }
        public string CreatedBy         { get; set; }
        public string StatusId          { get; set; }
        public string ContractId        { get; set; }
        public string ContPriceAmt      { get; set; }
        public string MortgageAmt       { get; set; }
        public string DocPrepDate       { get; set; }
        public string ExtraPages        { get; set; }
        public string ExtraNames        { get; set; }
        public bool   IsTrPendingBuyer  { get; set; }
        public bool   IsTrPendingSeller { get; set; }
        public bool   IsTrFinalBuyer    { get; set; }
        public bool   IsTrFinalSeller   { get; set; }
    }
}