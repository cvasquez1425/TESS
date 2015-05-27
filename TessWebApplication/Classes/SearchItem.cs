using System;

namespace Greenspoon.Tess.Classes
{
    public class SearchItem {
        public int? BatchEscrowId              { get; set; }
        public int? BatchEscrowNumber          { get; set; }
        public int? ContractId                 { get; set; }
        public int? ProjectId                  { get; set; }
        public string BatchEscrowCreatedBy     { get; set; }
        public string DevK                     { get; set; }
        public DateTime? BatchEscrowCreateDate { get; set; }
        public string LegalLastName            { get; set; }
        public int? MasterId                   { get; set; }  // RIQ-304
    }
}
