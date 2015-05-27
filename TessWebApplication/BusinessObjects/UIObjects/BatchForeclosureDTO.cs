namespace Greenspoon.Tess.BusinessObjects.UIObjects
{
    /// <summary>
    /// Batch Foreclosure user control UI.
    /// </summary>
    public class BatchForeclosureDTO
    {
        public string BatchForeclosureId    { get; set; }
        public string ProjectId             { get; set; }
        public string PhaseId               { get; set; }
        public string ForeclosureTypeId     { get; set; }
        public string BatchKey              { get; set; }
        public bool   FKA                   { get; set; }
        public string StatusId              { get; set; }
        public string JudgeId               { get; set; }
        public string FileDate              { get; set; }
        public string CaseNumber            { get; set; }
        public bool   LLC                   { get; set; }
        public string ProecessedDate        { get; set; }
        public string ReturnDate            { get; set; }
        public string HOAFileDate           { get; set; }
        public string CreatedBy             { get; set; }
        public string CreateDate            { get; set; }
    }
}