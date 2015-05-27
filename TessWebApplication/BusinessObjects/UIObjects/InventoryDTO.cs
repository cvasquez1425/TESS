namespace Greenspoon.Tess.BusinessObjects.UIObjects
{
    public class InventoryDTO
    {
        public string ContractIntervalId  { get; set; }
        public string ContractId          { get; set; }
        public string ContWeekMasterId    { get; set; }
        public string Week                { get; set; }
        public string ContYearMasterId    { get; set; }
        public string Year                { get; set; }
        public string InventoryBuildingId { get; set; }
        public string Building            { get; set; }
        public string InventoryUnitId     { get; set; }
        public string Unit                { get; set; }
        public string Bedroom             { get; set; }
        public string Floor               { get; set; }
        public string ABT                 { get; set; }
        public bool   Active              { get; set; }
        // added as of January 2015
        public string CreatedBy           { get; set; }
        public string CreatedDate         { get; set; }
        //Resale Forclosure Orlando
        public string ModifyDate { get; set; }
        public string ModifyBy { get; set; }        
    }
}