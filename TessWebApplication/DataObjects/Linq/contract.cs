#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using SearchKit;
using Greenspoon.Tess.DataObjects.AdoNet;
using Greenspoon.Tess.Services;
#endregion
namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class contract
    {

        #region Predicates
        internal static Expression<Func<contract, bool>> EqualsToContractId(int contractId)
        {
            return c => c.contract_id == contractId;
        }
        internal static Expression<Func<contract, bool>> EqualsToBatchEscrowId(int batchEscrowId)
        {
            return c => c.batch_escrow_id == batchEscrowId;
        }
        internal static Expression<Func<contract, bool>> IsActive()
        {
            return c => Equals(c.contract_active, true);
        }
        internal static Expression<Func<contract, bool>> EqualsToDevK(string sKey)
        {
            return c => c.dev_k_num == sKey;
        }
        internal static Expression<Func<contract, bool>> EqualsToAltBldg(string sKey)
        {
            return c => c.alternate_building == sKey;
        }
        #endregion

        internal static IEnumerable<string> GetInvalidIds(string ids)
        {
            return DbService.GetInvalidBatchUploadKeys(ids, KeyType.MasterId);
        }

        internal static IEnumerable<string> GetInvalidDevKIds(string ids)
        {
            return DbService.GetInvalidBatchUploadKeys(ids, KeyType.DevKId);
        }

        //  Adding Client Batch as a new Identifier August 2014
        internal static IEnumerable<string> GetInvalidClientBatchIds(string ids)
        {
            return DbService.GetInvalidBatchUploadKeys(ids, KeyType.ClientBatchId);
        }

        internal static contract GetContract(int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.contracts
                    .Include("batch_escrow")
                    .Include("batch_escrow.project")
                    .Include("foreclosures")
                    .Include("cancels")
                    .SingleOrDefault(EqualsToContractId(contractId));
            }
        }

        // RIQ-332  Myrtle Beach Bulk Update Web Form
        internal static bool GetMortAmt(int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var qryamt = from c in ctx.contracts.Where(c => c.mortgage_amt > 0)
                             select c;

                if (qryamt.Any()) { return true;} else {return false;}
            }
        }

        internal static IList<RecordingInfoDTO> GetRecordingInfo(int escrowKey)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                
                var data = ( from c in ctx.contracts
                             join l in ctx.legal_name on c.contract_id equals l.contract_id into names
                             from subNames in names.Where(n => n.primary == true).DefaultIfEmpty()
                             where c.batch_escrow_id == escrowKey
//                             orderby c.dev_k_num                                        // RIQ-271 Sorting Order is Incorrect cv102012
                             select new { c, name = subNames.last_name } )
                            .AsEnumerable()
                            .Select(ctemp => new { contract = ctemp.c, ctemp.name, ci  = Linq.contract_interval.GetInventoryUIList(ctemp.c.contract_id) })
                            .Select(co =>
                                new RecordingInfoDTO {
                                    RecordingDate = co.contract.deed_recording_date == null ? null : co.contract.deed_recording_date.ToDateOnly(),
                                    MortRecordingDate = co.contract.mortgage_recording_date == null ? null : co.contract.mortgage_recording_date.ToDateOnly(),  // Myrtle Beach 2015            
                                    DeedBook = co.contract.deed_book,
                                    MortgageBook =co.contract.mortgage_book,
                                    MasterId = co.contract.contract_id.ToString(),
                                    Share = co.contract.share,
                                    Year = co.ci.Select(d => d.Year).ToArray(),
                                    Units = co.ci.Select(d => d.Unit).ToArray(),
                                    Weeks = co.ci.Select(d => d.Week).ToArray(),
                                    LastName = co.name,
                                    DeedPage = co.contract.deed_page,
                                    MtgPage = co.contract.mortgage_page,
                                    mtgAmt = co.contract.mortgage_amt.ToString(),
                                    OwnerPolicy = co.contract.policy_number             // Added for Myrtle Beach Bulk Upload
                                }).ToList();
                return data;
            }
        }

        // RIQ-304
        internal static bool activeContract(int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var queryActiveContract = from c in ctx.contracts.Where(c => c.contract_id == contractId && c.contract_active == false)
                                          select c;
                if (queryActiveContract.Any()) { return true; } else { return false; }
            }
        }

        internal static bool SaveRecordingInfo(IList<RecordingInfoDTO> ui)
        {
            var sql = CreteRecordintInfoUpdateSql(ui);
            Db.Update(sql);

            var sqli = Insert_RecordInfoUpdateSql(ui);
            string sqlins = sqli.ToString();
            if (!string.IsNullOrEmpty(sqlins)) { Db.Insert(sqlins, false); }

            return true;
        }

        // ===========================Status Code screen [4] for Escrow Keys with individual comments? ===============================
        internal static bool SaveCommentsInfo(IList<UpdateCommentByEscrowKeyDTO> ui)
        {
            var sqli = Insert_CommentsInfoUpdateSql(ui);
            string sqlins = sqli.ToString();
            if (!string.IsNullOrEmpty(sqlins)) { Db.Insert(sqlins, false); }

            return true;
        }

        private static object Insert_CommentsInfoUpdateSql(IList<UpdateCommentByEscrowKeyDTO> ui)
        {
            var sb = new StringBuilder();

            foreach (var row in ui)
            {
                if (!string.IsNullOrEmpty(row.BatchEscrowId))
                {
                    sb.AppendLine(
                        string.Format("INSERT INTO [status] ([contract_id], [status_master_id], [comments], [createdby], [active], [createddate], [county_id], [effective_date]) VALUES ('{0}', {1}, '{2}', '{3}', '1', '{4}', '{5}', '{6}' )",
                            row.MasterId,
                            row.StatusMasterId,
                            row.Comments,
                            row.CreatedBy,
                            DateTime.Now,
                            row.County,
                            row.EffectiveDate
                            ));
                }
            }
            return sb.ToString();
        }

        //============================Added for MYRTLE Beach March 2015=======================
        internal static bool SaveRecordPolicyInfo(IList<RecordingInfoDTO> ui)
        {
            var sql = CreteRecordPolicyInfoUpdateSql(ui);
            Db.Update(sql);

            var sqli = Insert_RecordInfoUpdateSql(ui);
            string sqlins = sqli.ToString();
            if (!string.IsNullOrEmpty(sqlins)) { Db.Insert(sqlins, false); }

            return true;
        }

        // Added for MYRTLE Beach Feb 2014 - // RIQ-332  Myrtle Beach Bulk Update Web Form
        private static object Insert_RecordInfoUpdateSql(IList<RecordingInfoDTO> ui)
        {
            var sb = new StringBuilder();

            foreach (var row in ui)
            {
                bool mtgAmount = GetMortAmt(int.Parse(row.MasterId));
//                if (!string.IsNullOrEmpty( row.LenderPolicy) && !string.IsNullOrEmpty(row.mtgAmt))
                if (!string.IsNullOrEmpty(row.LenderPolicy) && mtgAmount == true)   // Mortgage Amount needs to be greater than 0, in other words, mortgage needed to add 648 status.
                {

                    sb.AppendLine(
                        string.Format("INSERT INTO [status] ([contract_id], [status_master_id], [comments], [createdby], [active], [createddate]) VALUES ('{0}', '648', '{1}', '{2}', '1', '{3}' )",
                            row.MasterId,
                            row.LenderPolicy,
                            row.CreatedBy,
                            DateTime.Now
                            ));
                }
            }
            return sb.ToString();
        }

        static string CreteRecordintInfoUpdateSql(IEnumerable<RecordingInfoDTO> ui)
        {
            
            var sb = new StringBuilder();
            
            foreach (var row in ui) {
                if (row.mtgAmt == "0") 
                {
                    sb.AppendLine(
                        string.Format(
                            "UPDATE [contract] SET [deed_book] = '{0}' , [deed_page] = {1}, [deed_recording_date] = '{2}',  [mortgage_recording_date] = {5}, [policy_insert_date] = '{6}' , [policy_number] = '{7}' WHERE contract_id = '{8}'",
                            row.DeedBook,
                            Db.Escape(row.DeedPage),
                            row.RecordingDate,
                            Db.Escape(row.MortgageBook),
                            Db.Escape(row.MtgPage),
                            string.IsNullOrEmpty(row.MortgageBook) ? "NULL" : Db.Escape(row.RecordingDate),
                            DateTime.Now,
                            row.OwnerPolicy,                                                                    // RIQ-332  Myrtle Beach Bulk Update Web Form
                            row.MasterId));
                }
                else
                {
                    sb.AppendLine(
                        string.Format(
                            "UPDATE [contract] SET [deed_book] = '{0}' , [deed_page] = {1}, [deed_recording_date] = '{2}',  [mortgage_book] = {3},  [mortgage_page] = {4}, [mortgage_recording_date] = {5}, [policy_insert_date] = '{6}', [policy_number] = '{7}' WHERE contract_id = '{8}'",
                            row.DeedBook,
                            Db.Escape(row.DeedPage),
                            row.RecordingDate = row.RecordingDate == "" ? Db.Escape("NULL") : row.RecordingDate,
                            Db.Escape(row.MortgageBook),
                            Db.Escape(row.MtgPage),
                            string.IsNullOrEmpty(row.MortgageBook) ? "NULL" : Db.Escape(row.RecordingDate),
                            DateTime.Now,
                            row.OwnerPolicy,
                            row.MasterId));
                }
            }
            return sb.ToString();
        }

        // Myrtle Beach Web Form March 2015
        static string CreteRecordPolicyInfoUpdateSql(IEnumerable<RecordingInfoDTO> ui)
        {

            var sb = new StringBuilder();

            foreach (var row in ui)
            {
                if (row.mtgAmt == "0")
                {
                    sb.AppendLine(
                        string.Format(
                            "UPDATE [contract] SET [deed_book] = '{0}' , [deed_page] = {1}, [deed_recording_date] = '{2}',  [mortgage_recording_date] = {5}, [policy_insert_date] = '{6}' , [policy_number] = '{7}' WHERE contract_id = '{8}'",
                            row.DeedBook,
                            Db.Escape(row.DeedPage),
                            row.RecordingDate,
                            Db.Escape(row.MortgageBook),
                            Db.Escape(row.MtgPage),
                            string.IsNullOrEmpty(row.MortgageBook) ? "NULL" : Db.Escape(row.RecordingDate),
                            DateTime.Now,
                            row.OwnerPolicy,                                                                    // RIQ-332  Myrtle Beach Bulk Update Web Form
                            row.MasterId));
                }
                else
                {
                    sb.AppendLine(
                        string.Format(
                            "UPDATE [contract] SET [deed_book] = '{0}' , [deed_page] = {1}, [deed_recording_date] = '{2}',  [mortgage_book] = {3},  [mortgage_page] = {4}, [mortgage_recording_date] = {5}, [policy_insert_date] = '{6}', [policy_number] = '{7}' WHERE contract_id = '{8}'",
                            row.DeedBook,
                            Db.Escape(row.DeedPage),
                            row.RecordingDate, 
                            Db.Escape(row.MortgageBook),
                            Db.Escape(row.MtgPage),
                            string.IsNullOrEmpty(row.MortgageBook) ? "NULL" : Db.Escape(row.MortRecordingDate), 
                            DateTime.Now,
                            row.OwnerPolicy,
                            row.MasterId));
                    sb.Replace("[deed_recording_date] = ''", "[deed_recording_date] = NULL");
                }
            }
            return sb.ToString();
        }

        internal static contract GetContract()
        {
            return new contract();
        }

        internal static IEnumerable<contract> GetLastSet()
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.contracts.Take(10).AsEnumerable();
            }
        }

        internal static project GetProjectByContractId(int contractId)
        {
            var con = GetContract(contractId);
            return con != null ? con.batch_escrow.project : null;
        }

        internal static ContractDTO GetContractUI(int contractId)
        {
            var c = GetContract(contractId);
            if (c == null) { return null; }
            var ui = new ContractDTO {
                ContractID      = c.contract_id.ToString(),
                FileOpen        = c.file_open_date.ToDateOnly(),
                DevK            = c.dev_k_num,
                PurchasePrice   = c.cont_price_amt.ToString(),
                AmountFinanced  = c.mortgage_amt.ToString(),
                ContractDate    = c.contract_date.ToDateOnly(),
                TitleIns        = c.title.ToString(),
                Active          = c.contract_active,
                NonComply       = c.non_comply_amt.ToString(),
                ClientBatch     = c.client_batch.ToString(),
                Share           = c.share,
                PolicyNumber    = c.policy_number,
                CancelDate      = Linq.cancel.GetCancelDate(c.cancels),
                SeasonId        = c.season_id.ToString(),
                VestingID       = c.vesting_id.ToString(),
                InterSpousal    = c.interspousal.ToString(),
                InitialFee      = c.initial_fee,
                AltBuild        = c.alternate_building,
                Bankrupt        = foreclosure.IsBankrupt(c.foreclosures),
                FixedFloat      = c.fixed_float,
                MaritalStatusId = c.marital_status_id.ToString(),
                GenderId        = c.gender_id.ToString(),
                PartialWkID     = c.partial_week.ToString(),
                AltUnit         = c.alternate_unit,
                ForeclosureDate = foreclosure.GetForeclosureDate(c.foreclosures),
                MortBook        = c.mortgage_book,
                MortRecDate     = c.mortgage_recording_date.ToDateOnly(),
                MortDate        = c.mortgage_date.ToDateOnly(),
                MortPage        = c.mortgage_page,
                Points          = c.points.ToString(),
                DeedBook        = c.deed_book,
                DeedDate        = c.deed_recording_date.ToDateOnly(),
                DeedPage        = c.deed_page,
                PointsGroupId   = c.project_points_id.ToString(),
                Cancel          = c.cancel ?? false,
                ExtraPages      = c.extra_pages.ToString(),
                ExtraMortPages  = c.extra_mort_pages.ToString(),
                IsGVG           = c.is_gvg,
                Color           = c.color,
                CreatedBy       = c.createdby,
                CreatedDate     = c.createddate.ToDateOnly(),
                EmailOptout     = c.email_optout ?? false,       // cv092012 RIQ-257
                OwnersPolResend = c.owners_pol_resend ?? false,  // cv092012 RIQ-257
                StartPeriod     = c.start_period.ToString(),     // cv092012 RIQ-263
                EndPeriod       = c.end_period.ToString(),       // cv092012 RIQ-263
                UnitLevel       = c.unit_level                  // Project Unit Level

            };
            return ui;
        } 

        internal static int Save(ContractDTO ui)
        {
            var conf = -1;
            using (var ctx = DataContextFactory.CreateContext()) {
                var contractlId = Convert.ToInt32(ui.ContractID);                          
                contract c;
                if (contractlId > 0) {
                    c = ctx.contracts
                        .SingleOrDefault(EqualsToContractId(contractlId));
                }
                else {
                    c = GetContract();
                    c.batch_escrow_id      = Convert.ToInt32(ui.BatchEscrowId);
                    c.createddate          = DateTime.Now;
                    ctx.AddTocontracts(c);
                    // Add a status to the contract as default.
                    var s = new status {
                        active           = true,
                        status_master_id = 1,
                        createdby        = ui.CreatedBy,
                        createddate      = DateTime.Now
                    };
                    c.status.Add(s);
                }
                // Populate contract
                if (c != null) {
                    bool neworUpdRecord;
                    var contractFound = from ct in ctx.contracts.Where(ct => ct.contract_id == contractlId)
                                        select ct;
                    if (contractFound.Any()) { neworUpdRecord = true; } else { neworUpdRecord =  false; }
                    c.contract_active         = ui.Active;
                    c.non_comply_amt          = ui.NonComply.NullIfEmpty<int>();
                    c.file_open_date          = ui.FileOpen.NullIfEmpty<DateTime?>();
                    c.dev_k_num               = ui.DevK.NullIfEmpty<string>();
                    c.cont_price_amt          = ui.PurchasePrice.NullIfEmpty<decimal?>();
                    c.mortgage_amt            = ui.AmountFinanced.NullIfEmpty<decimal?>();
                    c.contract_date           = ui.ContractDate.NullIfEmpty<DateTime?>();
                    c.title                   = ui.TitleIns.NullIfEmpty<bool?>();
                    c.client_batch            = ui.ClientBatch.NullIfEmpty<int?>();
                    c.share                   = ui.Share.NullIfEmpty<string>();
                    c.policy_number           = ui.PolicyNumber.NullIfEmpty<string>();
                    c.season_id               = ui.SeasonId.NullIfEmpty<int?>();
                    c.vesting_id              = ui.VestingID.NullIfEmpty<int?>();
                    c.interspousal            = ui.InterSpousal.NullIfEmpty<bool?>();
                    c.initial_fee             = ui.InitialFee.NullIfEmpty<string>();
                    c.alternate_building      = ui.AltBuild.NullIfEmpty<string>();
                    c.fixed_float             = ui.FixedFloat.NullIfEmpty<string>();
                    c.marital_status_id       = ui.MaritalStatusId.NullIfEmpty<int?>();
                    c.gender_id               = ui.GenderId.NullIfEmpty<int?>();
                    c.partial_week            = ui.PartialWkID.NullIfEmpty<int?>();
                    c.alternate_unit          = ui.AltUnit.NullIfEmpty<string>();
                    c.mortgage_date           = ui.MortDate.NullIfEmpty<DateTime?>();
                    c.mortgage_book           = ui.MortBook.NullIfEmpty<string>();
                    c.mortgage_recording_date = ui.MortRecDate.NullIfEmpty<DateTime?>();
                    c.mortgage_page           = ui.MortPage.NullIfEmpty<string>();
                    c.deed_book               = ui.DeedBook.NullIfEmpty<string>();
                    c.deed_recording_date     = ui.DeedDate.NullIfEmpty<DateTime?>();
                    c.deed_page               = ui.DeedPage.NullIfEmpty<string>();
                    c.points                  =	ui.Points.NullIfEmpty<int?>();
                    c.project_points_id       = ui.PointsGroupId.NullIfEmpty<int?>();
                    c.cancel                  = ui.Cancel;
                    c.color                   = ui.Color.NullIfEmpty<string>();
                    c.extra_pages             = ui.ExtraPages.NullIfEmpty<int?>();
                    c.extra_mort_pages        = ui.ExtraMortPages.NullIfEmpty<int?>();
                    c.createdby               = ui.CreatedBy.NullIfEmpty<string>();
                    c.email_optout            = ui.EmailOptout;                         // cv092012 RIQ-257
                    c.owners_pol_resend       = ui.OwnersPolResend;                     //  cv092012 RIQ-257
                    c.start_period            = ui.StartPeriod.NullIfEmpty<int?>();     // cv092012 RIQ-263
                    c.end_period              = ui.EndPeriod.NullIfEmpty<int?>();       // cv092012 RIQ-263
                    c.unit_level              = ui.UnitLevel.NullIfEmpty<string>();     // Project Unit Level
                    // Resale Forclosure Orlando
                    c.modifieddate            =  neworUpdRecord ? DateTime.Now : c.modifieddate = null;
                    c.modifiedby               = neworUpdRecord ? ui.ModifyBy  : c.modifiedby   = null;

                    // Save changes to database.
                    if (ctx.SaveChanges() > 0) {
                        conf = c.contract_id;
                    }
                }
            }
            return conf;
        } 

        internal static List<DropDownItem> GetContractListByBatchId(int batchId)
        {
            var contractList = new List<DropDownItem>();
            using (var ctx = DataContextFactory.CreateContext()) {
                var contracts = ( from c in ctx.contracts
                                  where c.batch_escrow_id == batchId
                                  orderby c.contract_id ascending
                                  select new {
                                      value = c.contract_id
                                  } ).ToList();
                contractList.AddRange(contracts.Select(item => new DropDownItem { Name = item.value.ToString(), Value = item.value.ToString() }));
                if (!contractList.Any()) return contractList;
                contractList.Insert(0, new DropDownItem());
            }
            return contractList;
        }

        internal static Expression<Func<contract, bool>> GetTitleSearchPredicate(TitleSearchDTO ui)
        {
            var predicate = PredicateBuilder.True<contract>();
            if (ui.MasterId.Length > 0) {
                var masterId = Convert.ToInt32(ui.MasterId);
                predicate = predicate.And(EqualsToContractId(masterId));
            }
            if (ui.DevK.Length > 0) {
                predicate = predicate.And(EqualsToDevK(ui.DevK));
            }
            if (ui.AltBldg.Length > 0) {
                predicate = predicate.And(EqualsToAltBldg(ui.AltBldg));
            }
            if (ui.Active) {
                predicate = predicate.And(IsActive());
            }
            return predicate;
        }

        internal static List<TitleSearchParam> TitleSearchInContract(TitleSearchDTO ui, List<TitleSearchParam> tsList)
        {
            var newResult = new List<TitleSearchParam>();
            var predicate = GetTitleSearchPredicate(ui);

            using (var ctx = DataContextFactory.CreateContext()) {
                if (tsList != null && tsList.Any()) {
                    foreach (var ts in tsList) {
                        var tempconId = ts.ContractId;
                        var tempPredicate = predicate.And(EqualsToContractId(tempconId));
                        var temp = ctx.contracts.AsExpandable()
                            .Where(tempPredicate);
                        foreach (var c in temp) {
                            var tsp = new TitleSearchParam {
                                ContractId = c.contract_id,
                                BatchEscrowId = c.batch_escrow_id,
                                ProjectId = c.batch_escrow.project_id,
                                Contract_IntervalId = ts.Contract_IntervalId,
                                LegalNameId = ts.LegalNameId
                            };
                            newResult.Add(tsp);
                        }
                    }
                }
                else {
                    newResult = ctx.contracts
                        .AsExpandable()
                        .Where(predicate)
                        .Select(c => new TitleSearchParam {
                            ContractId = c.contract_id,
                            BatchEscrowId = c.batch_escrow_id,
                            ProjectId = c.batch_escrow.project_id
                        }).ToList();
                }
                return newResult;
            }  // end ctx.
        }  
    } 
}