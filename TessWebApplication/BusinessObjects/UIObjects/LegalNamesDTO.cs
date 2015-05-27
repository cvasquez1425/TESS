namespace Greenspoon.Tess.BusinessObjects.UIObjects {
    public class LegalNamesDTO {
        public string LegalNameId { get; set; }
        public string ContractId  { get; set; }
        public string Name        { get; set; }
        public string FirstName   { get; set; }
        public string LastName    { get; set; }
        public string Address1    { get; set; }
        public string Address2    { get; set; }
        public string Address3    { get; set; }
        public string Address23   { get; set; }
        public string City        { get; set; }
        public string State       { get; set; }
        public string Zip         { get; set; }
        public string Country     { get; set; }
        public string CountryId   { get; set; }  
        public string Phone       { get; set; }
        public string Email       { get; set; }
        public string Order       { get; set; }
        public bool   Dismiss     { get; set; }
        public bool   Active      { get; set; }
        public bool   Primary     { get; set; }
        public string CreatedBy   { get; set; }
        public string CreatedDate { get; set; }
        //Resale Forclosure Orlando
        public string ModifyDate { get; set; }
        public string ModifyBy { get; set; }
   }
}