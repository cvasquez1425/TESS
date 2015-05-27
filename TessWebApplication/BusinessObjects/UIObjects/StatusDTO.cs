using System.Collections.Generic;
namespace Greenspoon.Tess.BusinessObjects.UIObjects
{
    public class StatusDTO
    {
        public string StatusId           { get; set; }
        public string ContractId         { get; set; }
        public string StatusMasterId     { get; set; }
        public string StatusMasterName   { get; set; }
        public string StatusGroupId      { get; set; }
        public string StatusGroupName    { get; set; }
        public string CountyId           { get; set; }
        public string CountyName         { get; set; }
        public string OriginalCountyId   { get; set; }
        public string OriginalCountyName { get; set; }
        public string LegalNameId        { get; set; }
        public string LegalName          { get; set; }
        public string Invoice            { get; set; }
        public string RecDate            { get; set; }
        public string Book               { get; set; }
        public string Page               { get; set; }
        public string AssignmentNumber   { get; set; }
        public string EffectiveDate      { get; set; }
        public string Comment            { get; set; }
        public string Batch              { get; set; }
        public string UploadeBatchId     { get; set; }
        public bool   Active             { get; set; }
        public string CreatedBy          { get; set; }
        public string CreatedDate        { get; set; }
        //Resale Forclosure Orlando
        public string ModifyDate { get; set; }
        public string ModifyBy { get; set; }        
    }
}