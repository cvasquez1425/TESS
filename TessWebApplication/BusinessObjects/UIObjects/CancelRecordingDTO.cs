using System.Collections.Generic;

namespace Greenspoon.Tess.BusinessObjects.UIObjects
{
    public class CancelRecordingContractDTO
    {
        public string    MasterId    { get; set; }
        public string    LastName    { get; set; }
        public string    LegalNameId { get; set; }
        public string    Share       { get; set; }
        public string[]  Year        { get; set; }
        public string[]  Units       { get; set; }
        public string[]  Weeks       { get; set; }
        public string    Page        { get; set; }
        public StatusDTO StatusDTO   { get; set; }
    }
}