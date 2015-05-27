namespace Greenspoon.Tess.BusinessObjects.UIObjects
{
    public class ForeclosureDTO
    {
        public string ForeclosureId      { get; set; }
        public string BatchForeclosureId { get; set; }
        public string ContractId         { get; set; }
        public string DevK               { get; set; }
        public string IneterestPct       { get; set; }
        public string DefaultBalance     { get; set; }
        public string DefaultDate        { get; set; }
        public bool   OnHold             { get; set; }
        public bool   Bankrupt           { get; set; }
        public string MortgageBook       { get; set; }
        public string MortgagePage       { get; set; }
        public string MortgageDate       { get; set; }
        public string DeedBook           { get; set; }
        public string DeedPage           { get; set; }
        public string DeedDate           { get; set; }
        public string Vesting            { get; set; }
        public string Points             { get; set; }
        public string PointsGroup        { get; set; }
        public bool   Active             { get; set; }
        public string StatusId           { get; set; }
    }
}