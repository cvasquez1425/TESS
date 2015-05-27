namespace Greenspoon.Tess.BusinessObjects.UIObjects {
    public class BatchCancelDTO {
        public string BatchCancelId   { get; set; }
        public string ProjectId       { get; set; }
        public string ParentProjectId { get; set; }
        public string ExtraPages      { get; set; }
        public string ExtraRecording  { get; set; }        // RIQ-306
        public string ExtraNames      { get; set; }
        public string CancelTypeId    { get; set; }
        public string CancelNumber    { get; set; }
        public string CancelStatusId  { get; set; }
        public string CreatedBy       { get; set; }
        public string CreatedDate     { get; set; }
    }
}