namespace Greenspoon.Tess.BusinessObjects.UIObjects
{
    public class BatchEscrowDTO
    {
        internal string BatchEscrowId  { get; set; }
        internal string ProjectId      { get; set; }
        internal string PhaseId        { get; set; }
        internal string EscrowKey      { get; set; }
        internal bool?  TitleInsurance { get; set; }
        internal string TotalDeedPages { get; set; }
        internal string TotalNotePages { get; set; }
        internal string BatchAmount    { get; set; }
        internal bool   NonEscrow      { get; set; }
        internal bool   Cashout        { get; set; } // Cashout deals
        internal string PartnerId      { get; set; }
        internal string CreatedBy      { get; set; }
        internal string CreatedDate    { get; set; }
        //Resale Forclosure Orlando
        public string ModifyDate       { get; set; }
        public string ModifyBy         { get; set; }
    }
}