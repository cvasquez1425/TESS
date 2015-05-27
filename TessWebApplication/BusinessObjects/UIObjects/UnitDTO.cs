namespace Greenspoon.Tess.BusinessObjects.UIObjects
{
    public class UnitDTO
    {
        public string InventoryUnitId     { get; set; }
        public string InventoryBuildingId { get; set; }
        public string Description         { get; set; }
        public string Type                { get; set; }
        public string UnitNumber          { get; set; }
        public string NumberOfBedroom     { get; set; }
        public bool   Active              { get; set; }
        public string CreatedBy           { get; set; }
        public string CreatedOn           { get; set; }
    }
}