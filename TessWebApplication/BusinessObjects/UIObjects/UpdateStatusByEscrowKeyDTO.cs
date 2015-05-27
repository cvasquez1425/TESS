using System;

namespace Greenspoon.Tess.BusinessObjects.UIObjects
{
    public class UpdateStatusByEscrowKeyDTO
    {
        public string StatusMasterId { get; set; }
        public string BeatchEscrowId { get; set; }
        public string Invoice        { get; set; }
        public string Book           { get; set; }
        public string Page           { get; set; }
        public string EffectiveDate  { get; set; }
        public string Assign         { get; set; }
        public string RecordDate     { get; set; }
        public string OrigCountyId   { get; set; }
        public string CountyId       { get; set; }
        public string Comments       { get; set; }
        public string UserName       { get; set; }
    }
}