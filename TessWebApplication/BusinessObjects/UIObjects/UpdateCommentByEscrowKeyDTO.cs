
namespace Greenspoon.Tess.BusinessObjects.UIObjects
{
    public class UpdateCommentByEscrowKeyDTO
    {
        public string BatchEscrowId  { get; set; }
        public string MasterId       { get; set; }
        public string StatusMasterId { get; set; }
        public string Comments       { get; set; }
        public string EffectiveDate  { get; set; }
        public string CreatedBy      { get; set; }
        public string County         { get; set; }
    }
}