#region Includes
using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using SearchKit;
#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class cancel
    {
        #region Predicates
        internal static Expression<Func<cancel, bool>> EqualsToCanceBatchlId(int batchCancelId)
        {
            return c => c.batch_cancel_id == batchCancelId;
        }

        internal static Expression<Func<cancel, bool>> EqualsToCancelId(int cancelId)
        {
            return c => c.cancel_id == cancelId;
        }
        internal static Expression<Func<cancel, bool>> EqualsToContractId(int contractId)
        {
            return c => c.contract_id == contractId;
        }
        internal static Expression<Func<cancel, bool>> EqualsToActive()
        {
            return c => c.cancel_active == true;
        }
        internal static Expression<Func<cancel, bool>> EqualsToBatchCancelIdAndContractId(int batchCancelId, int contractId)
        {
            return EqualsToCanceBatchlId(batchCancelId).And(EqualsToContractId(contractId));
        }
        internal static Expression<Func<cancel, bool>> IsActive(int contractId)
        {
            return EqualsToContractId(contractId).And(EqualsToActive());
        }
        internal static Expression<Func<cancel, bool>> NotEqualsToBatchCancelId(int batchCancelId)
        {
            return c => c.batch_cancel_id != batchCancelId;
        }
        #endregion

        internal static IEnumerable<string> GetInvalidIds(List<string> ids)
        {
            if (ids == null) return null;
            var tempIDs = ids.ConvertAll(Convert.ToInt32);
            using (var ctx = DataContextFactory.CreateContext()) {
                return
                    tempIDs.Where(i => ctx.cancels.All(c => c.cancel_id != i)).ToList().
                        ConvertAll(Convert.ToString);
            }
        }

        internal static List<DropDownItem> GetCancelContractList(int batchCancelId)
        {
            var contractlist = new List<DropDownItem>();
            using (var ctx = DataContextFactory.CreateContext()) {
                var contracts = ( from f in ctx.cancels.AsExpandable()
                                  .Where(EqualsToCanceBatchlId(batchCancelId))
                                  orderby f.cancel_id            //  orderby f.cancel_date descending - I've coded to be order by f.cancel_id instead. Work Order Number 43022 with issue Creating Cancel Batch - QCD 
                                  select new {
                                      Name  = f.contract_id,
                                      Value = f.cancel_id
                                  } ).ToList();
                contractlist.AddRange(contracts.Select(contract => new DropDownItem { Name = contract.Name.ToString(), Value = contract.Value.ToString() }));
            }
            return contractlist;
        }

        internal static CancelDTO GetCancelUI(int cancelId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var c = ctx.cancels
                           .Single(EqualsToCancelId(cancelId));

                if (c == null) return null;

                var cDTO = new CancelDTO {
                    CancelId       = c.cancel_id.ToString(),
                    BatchCancelId  = c.batch_cancel_id.ToString(),
                    ContractId     = c.contract_id.ToString(),
                    NonTax         = c.non_tax.ToString(),
                    Death          = c.death.ToString(),
                    CMA            = c.marriage.ToString(),    // RIQ-268 Space out ONLY - no changes made
                    PurchasePrice  = c.purch_price.ToString(),
                    MortgageBook   = c.contract.mortgage_book,
                    MortgagePage   = c.contract.mortgage_page,
                    MortgageDate   = c.contract.mortgage_recording_date.ToDateOnly(),
                    DeedBook       = c.contract.deed_book,
                    DeedPage       = c.contract.deed_page,
                    DeedDate       = c.contract.deed_recording_date.ToDateOnly(),
                    Vesting        = c.contract.vesting_id.HasValue 
                                            ? c.contract.vesting.vesting_type : string.Empty,
                    Points         = c.contract.points.ToString(),
                    PointsGroup    = c.contract.project_points_id.HasValue 
                                            ? c.contract.project_points.display_grouping : string.Empty,
                    Active         = c.cancel_active,
                    DevK           = c.contract.dev_k_num,
                    NonComply      = c.non_comply_amt.ToString(),
                    AffidavitOfRev = c.aff_rev_num.HasValue
                                            ? c.aff_rev_num.Value : 1
                };
                return cDTO;

            }
        }

        #region Add Contract to Cancel
        internal static string AddContractToCancel(List<string> contractIdList, int batchCancelId, string userName)
        {
            try {
                var cList = contractIdList.ConvertAll(Convert.ToInt32);
                var validationSummary = ValidateContractIdList(cList, batchCancelId);
                if (validationSummary.Length == 0) {
                    return Insert(batchCancelId, cList, userName) ? string.Empty : "Failed to add Contract Id(s). <br /> Unknown Error.";
                }
                return validationSummary;
            } catch { return "Sorry. There are some invalid value in the list.<br /> Consider reformatting your input."; }
        }

        static bool Insert(int batchCancelId, List<int> cList, string userName)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                cList.ForEach(c => ctx.AddTocancels(CreateCancel(c, batchCancelId, userName)));
                foreach (var con in cList.Select(temCon => ctx.contracts.SingleOrDefault(contract.EqualsToContractId(temCon)))) {
                    MarkContractCancelIfAllowed(true, ctx, con.contract_id, batchCancelId);
                }
                return ctx.SaveChanges() > 0;
            }
        }

        internal static Tuple<bool, string> AddContractToCancel(int batchCancelId, int contractId, string userName)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                if (BatchProjectandContractProjectAreNotSame(batchCancelId, contractId, ctx)) {
                    return Tuple.Create(false, string.Format("Batch ID: {0} and Master Id {1} have diffrent project. ", batchCancelId, contractId));
                }

                if (ctx.cancels.Exists(EqualsToBatchCancelIdAndContractId(batchCancelId, contractId))) {
                    return Tuple.Create(false, string.Format("Batch ID: {0} with Master Id {1} already exists. ", batchCancelId, contractId));
                }
                var cancel = CreateCancel(contractId, batchCancelId, userName);
                ctx.AddTocancels(cancel);

                MarkContractCancelIfAllowed(true, ctx, contractId, batchCancelId);
                return ctx.SaveChanges() > 0 ? Tuple.Create(true, string.Format("Batch ID: {0} with Master Id {1} has been added.", batchCancelId, contractId)) 
                                             : Tuple.Create(false, string.Format("Batch ID: {0} with Master Id {1} could not be added.", batchCancelId, contractId));
            }
        }

        private static bool BatchProjectandContractProjectAreNotSame(int batchCancelId, int contractId, TessEntities ctx)
        {
            var batchProjectId = ctx.batch_cancel.SingleOrDefault(b => b.batch_cancel_id == batchCancelId).project_id;
            var contractProjectId = ctx.contracts.SingleOrDefault(c => c.contract_id == contractId).batch_escrow.project_id;
            return batchProjectId != contractProjectId;
        }

        static string ValidateContractIdList(IEnumerable<int> cList, int batchCancelId)
        {
            var sbErr = new StringBuilder();
            using (var ctx = DataContextFactory.CreateContext()) {
                foreach (var contractId in cList) {
                    if (ctx.contracts.Exists(contract.EqualsToContractId(contractId))) {
                        if (BatchProjectandContractProjectAreNotSame(batchCancelId, contractId, ctx)) {
                            sbErr.AppendLine(string.Format("Batch ID: {0} with Master Id {1} have diffrent project. ", batchCancelId, contractId));
                        }
                        if (ctx.cancels.Exists(EqualsToBatchCancelIdAndContractId(batchCancelId, contractId))) {
                            sbErr.AppendLine(string.Format("<br />{0} already exists in this Batch cancel. You can edit as Active/In Active.", contractId));
                        }
                    }
                    else {
                        sbErr.AppendLine(string.Format("<br />{0} is not a valid master id.", contractId));
                    }
                }
            }
            return sbErr.ToString();
        }

        static cancel CreateCancel(int contractId, int batchCancelId, string userName)
        {
            return new cancel {
                contract_id     = contractId,
                batch_cancel_id = batchCancelId,
                cancel_active   = true,
                createdby       = userName,
                createddate     = DateTime.Today
            };
        }
        #endregion

        internal static cancel GetActiveCancel(int contractId, int batchCancelId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.cancels.AsExpandable()
                          .FirstOrDefault(IsActive(contractId)
                          .And(NotEqualsToBatchCancelId(batchCancelId)));
            }
        }

        internal static cancel GetActiveCancelInSameBatch(int contractId, int batchCancelId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.cancels.AsExpandable()
                          .FirstOrDefault(IsActive(contractId)
                          .And(EqualsToCanceBatchlId(batchCancelId)));
            }
        }

        internal static string GetCancelDate(EntityCollection<cancel> cList)
        {
            string result = null;
            if (cList != null && cList.Any()) {
                var cancel = cList
                     .FirstOrDefault(c => c.cancel_active 
                                           && c.cancel_date.HasValue);
                if (cancel != null) {
                    result = cancel.cancel_date.ToDateOnly();
                }
            }
            return result;
        }

        internal static bool Save(CancelDTO ui)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var c = ctx.cancels
                           .SingleOrDefault(EqualsToCancelId(int.Parse(ui.CancelId)));
                if (c == null) { return false; }

                c.death          = ui.Death.NullIfEmpty<decimal?>();
                c.marriage       = ui.CMA.NullIfEmpty<decimal?>();    // RIQ-268 from short to decimal
                c.non_tax        = ui.NonTax.NullIfEmpty<decimal?>(); // RIQ-268 from short to decimal
                c.purch_price    = ui.PurchasePrice.NullIfEmpty<decimal?>();
                c.cancel_active  = ui.Active;
                c.non_comply_amt = ui.NonComply.NullIfEmpty<int?>();
                c.aff_rev_num    = ui.AffidavitOfRev;
// RIQ-309 Cancel Form Improvements.
                c.contract.mortgage_book                = ui.MortgageBook;
                c.contract.mortgage_page                = ui.MortgagePage;
                c.contract.mortgage_recording_date      = ui.MortgageDate.NullIfEmpty<DateTime?>();
                c.contract.deed_book                    = ui.DeedBook;
                c.contract.deed_page                    = ui.DeedPage;
                c.contract.deed_recording_date          = ui.DeedDate.NullIfEmpty<DateTime?>();


                MarkContractCancelIfAllowed(ui.Active, ctx, c.contract_id, c.batch_cancel_id);
                return ctx.SaveChanges() > 0;
            }
        }

        static void MarkContractCancelIfAllowed(bool active, TessEntities ctx, int contractId, int batchCancelId)
        {
            if (ContractDoesNotNeedToBeCancelledForThis(batchCancelId)) return;
            var con = ctx.contracts.SingleOrDefault(contract.EqualsToContractId(contractId));
            con.cancel = active;
        }

        static bool ContractDoesNotNeedToBeCancelledForThis(int batchCancelId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var bc = ctx.batch_cancel.SingleOrDefault(batch_cancel.EqualsToBatchCancelId(batchCancelId));
                if (!bc.cancel_type_id.HasValue) return false;
                return ( bc.cancel_type_id == 1 ) || ( bc.cancel_type_id == 5 ) || ( bc.cancel_type_id == 6 ) ||
                       ( bc.cancel_type_id == 13 );
            }
        }
    }
}