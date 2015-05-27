namespace Greenspoon.Tess.BusinessObjects.UIObjects
{
    public class ContractDTO
    {
        internal string ContractID       { get; set; }
        internal string BatchEscrowId    { get; set; }
        internal string FileOpen         { get; set; }
        internal string DevK             { get; set; }
        internal string PurchasePrice    { get; set; }
        internal string AmountFinanced   { get; set; }
        internal string ContractDate     { get; set; }
        internal string TitleIns         { get; set; }
        internal bool   Active           { get; set; }
        internal string NonComply        { get; set; } 
        internal string ClientBatch      { get; set; }
        internal string Share            { get; set; }
        internal string PolicyNumber     { get; set; }
        internal string CancelDate       { get; set; }
        internal string SeasonId         { get; set; }
        internal string VestingID        { get; set; }
        internal string InterSpousal     { get; set; }
        internal string InitialFee       { get; set; }
        internal string AltBuild         { get; set; }
        internal string Bankrupt         { get; set; }
        internal string FixedFloat       { get; set; }
        internal string MaritalStatusId  { get; set; }
        internal string GenderId         { get; set; }
        internal string PartialWkID      { get; set; }
        internal string AltUnit          { get; set; }
        internal string ForeclosureDate  { get; set; }
        internal string MortBook         { get; set; }
        internal string MortRecDate      { get; set; }
        internal string MortDate         { get; set; }
        internal string MortPage         { get; set; }
        internal string Points           { get; set; }
        internal string DeedBook         { get; set; }
        internal string DeedDate         { get; set; }
        internal string DeedPage         { get; set; }
        internal string PointsGroupId    { get; set; }
        internal bool   Cancel           { get; set; }
        internal string ExtraPages       { get; set; }
        internal string ExtraMortPages   { get; set; }
        internal bool   IsGVG            { get; set; }
        internal string Color            { get; set; }
        internal string CreatedBy        { get; set; }
        internal string CreatedDate      { get; set; }
        internal bool   EmailOptout      { get; set; }   // cv092012  RIQ-257
        internal bool   OwnersPolResend  { get; set; }   // cv092012 RIQ-257
        internal string StartPeriod      { get; set; }   // cv092012  RIQ-263
        internal string EndPeriod        { get; set; }   // cv092012  RIQ-263
        internal string UnitLevel        { get; set; }   // Project Unit Level June 7 2014
        //Resale Forclosure Orlando
        internal string ModifyDate       { get; set; }
        internal string ModifyBy       { get; set; }
    }
}