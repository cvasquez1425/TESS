namespace Greenspoon.Tess.BusinessObjects.UIObjects {
    public class CancelDTO {
        public string CancelId       { get; set; }
        public string BatchCancelId  { get; set; }
        public string ContractId     { get; set; }
        public string DevK           { get; set; }
        public string NonTax         { get; set; }
        public string Death          { get; set; }
        public string CMA            { get; set; }
        public string PurchasePrice  { get; set; }
        public string MortgageBook   { get; set; }
        public string MortgagePage   { get; set; }
        public string MortgageDate   { get; set; }
        public string DeedBook       { get; set; }
        public string DeedPage       { get; set; }
        public string DeedDate       { get; set; }
        public string Vesting        { get; set; }
        public string Points         { get; set; }
        public string PointsGroup    { get; set; }
        public string CreatedBy      { get; set; }
        public string CreatedDate    { get; set; }
        public bool   Active         { get; set; }
        public string NonComply      { get; set; }
        public int    AffidavitOfRev { get; set; }
    }
}