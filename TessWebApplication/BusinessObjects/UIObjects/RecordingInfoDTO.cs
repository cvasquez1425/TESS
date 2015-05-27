
namespace Greenspoon.Tess.BusinessObjects.UIObjects
{
    public class RecordingInfoDTO
    {
        public string RecordingDate { get; set; }
        public string DeedBook { get; set; }
        public string MortgageBook { get; set; }
        public string MasterId { get; set; }
        public string LastName { get; set; }
        public string Share { get; set; }
        public string[] Year { get; set; }
        public string[] Units { get; set; }
        public string[] Weeks { get; set; }
        public string DeedPage { get; set; }
        public string MtgPage { get; set; }
        public string mtgAmt { get; set; }
        // Myrtle Beach Bulk Upload Project
        public string OwnerPolicy   { get; set; }
        public string LenderPolicy  { get; set; }
        public string CreatedBy     { get; set; }
        public string MortRecordingDate { get; set; } // For Myrtle Beach Only March 2015
    }
}