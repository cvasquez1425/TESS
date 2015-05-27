#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using SearchKit;
#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class status
    {
        internal static Expression<Func<status, bool>> EqualsToContractId(int contractId)
        {
            return s => s.contract_id == contractId;
        }

        internal static Expression<Func<status, bool>> EqualsToStatusId(int statusId)
        {
            return s => s.status_id == statusId;
        }
        //RIQ-289 CVJan2013
        internal static Expression<Func<status_master, bool>> EqualsToStatusMasterId(int statusMasterID)
        {
            return s => s.status_master_id == statusMasterID;
        }

        internal static Expression<Func<cancel, bool>> EqualsToBatchCanceld(int batchCancelId)
        {
            return c => c.batch_cancel_id == batchCancelId;
        }

        // RIQ-276 Need to stop the GVG pop up when code 631 is used
        internal static bool stopGVGpopup (int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {

                var qry =
                    (from s in ctx.status
                     where s.contract_id == contractId && s.status_master_id == 631
                     select new { s.status_master_id }).Distinct();
                
                if (qry.SingleOrDefault() == null)
                {
                    return false;
                }
                else
                {
                    // Update found Contract is_gvg  RIQ-276
                    //var cont = (from c in ctx.contracts
                    //            where c.contract_id == contractId
                    //            select c).First();
                    /*     Stored Proc runs in the Back-End of TESS db to update is_GVG field in the dbo.Contract table
                    cont.is_gvg = false;
                    try
                    {
                        ctx.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw(ex);  // (ex.Message); + "<br/>" +  ex.InnerException.Message);
                    }
                     */
                    return true;
                }
            }
        }

        internal static List<status> GetFilteredStatusList(Expression<Func<status_master, bool>> filter, int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var query =   
                    ( from s in ctx.status
                          .Include("status_master")
                          .Include("status_group")
                          .AsExpandable()
                      let g = ctx.status_master
                                 .Where(st => st.status_master_id == s.status_master_id)
                      where g.Any(filter)
                      where s.contract_id == contractId
                      orderby s.createddate, s.status_id ascending
                      select s );
                return query.ToList();
            }
        }

        public static List<StatusDTO> GetStatusUIList(FormNameEnum f, int contractId)
        {
            var statusList = new List<StatusDTO>();
            var filter     = status_master.GetStatusFilter(f);
            var status     = GetFilteredStatusList(filter, contractId);
            if (( status != null ) && ( status.Any() == true )) {
                statusList.AddRange(status.Select(s => GetStatusDTO(s.status_id)).Where(ui => ui != null));
                return statusList;
            }
            return null;
        }

        public static List<StatusDTO> GetStatusUIList(int contractId)
        {
            return GetStatusUIList(FormNameEnum.Unknown, contractId);
        }

        //RIQ-289 CVJan2013
        internal static bool GetStatusIsComment(int statusId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var s = ctx.status
                           .SingleOrDefault(EqualsToStatusId(statusId));
                if (s == null) return false;
                var qry = (
                            from sm in ctx.status_master
                            where sm.status_master_id == s.status_master_id 
                            select (sm.is_comment)).SingleOrDefault();
                
                if ( qry )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //RIQ-289 CVJan2013
        internal static bool GetStatusIsDeleted(int statusId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var s = ctx.status
                           .SingleOrDefault(EqualsToStatusId(statusId));
                if (s == null) return false;
                var qry = (
                            from sm in ctx.status_master
                            where sm.status_master_id == s.status_master_id
                            select (sm.is_deleted_allowed)).SingleOrDefault();

                if (qry)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // RIQ-303 Legal Name change field = 1 pull up the the legal name Web Form.
        internal static bool statusMasterLegalname(int statusMasterID)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var ln = ctx.status_master
                            .Where(l => l.is_legal_name_required == true)
                            .SingleOrDefault(EqualsToStatusMasterId(statusMasterID));
                if (ln == null) { return false; } else { return true; }
            }
        }

        // RIQ-303 is_Cancel_escrow
        internal static bool statusMasterCancelEscrow(int statusMasterID)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var ec = ctx.status_master
                            .Where(e => e.is_cancel_escrow == true)
                            .SingleOrDefault(EqualsToStatusMasterId(statusMasterID));
                if (ec == null) { return false; } else { return true; }
            }
        }

        //RIQ-289 CVJan2013 Status_Master Table look up status_master_id for is_comment
        internal static bool GetStatusMasterID(int statusMasterID)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var s = ctx.status_master
                           .Where(sm => sm.is_comment == true)
                           .SingleOrDefault(EqualsToStatusMasterId(statusMasterID));

                if (s == null) { return false; } else { return true; }
            }
        }

        internal static StatusDTO GetStatusDTO(int statusId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var s = ctx.status
                           .SingleOrDefault(EqualsToStatusId(statusId));
                if (s == null) return null;
                var st = new StatusDTO {
                    StatusId           = s.status_id.ToString(),
                    ContractId         = s.contract_id.ToString(),
                    StatusMasterId     = s.status_master_id.ToString(),
                    StatusMasterName   = s.status_master.status_master_name,
                    StatusGroupId      = s.status_master.status_group_id.ToString(),
                    StatusGroupName    = s.status_master.status_group.status_group_name,
                    CountyId           = s.county_id.ToString(),
                    CountyName         = s.county_id.HasValue == true 
                                            ? county.GetCountyName(Convert.ToInt32(s.county_id)) 
                                            : null,
                    OriginalCountyId   = s.original_county_id.ToString(),
                    OriginalCountyName = s.original_county_id.HasValue == true 
                                            ? county.GetCountyName(Convert.ToInt32(s.original_county_id)) 
                                            : null,
                    LegalNameId        = s.legal_name_id.ToString(),
                    LegalName          = s.legal_name_id.HasValue == true 
                                            ? legal_name.GetFullLegalName(Convert.ToInt32(s.legal_name_id))
                                            : null,
                    Invoice            = s.invoice,
                    RecDate            = s.rec_date.ToDateOnly(),
                    EffectiveDate      = s.effective_date.ToDateOnly(),
                    Book               = s.book,
                    Page               = s.page.ToString(),
                    AssignmentNumber   = s.assign_num,
                    Comment            = s.comments,
                    Batch              = s.batch.ToString(),
                    UploadeBatchId     = s.upload_batch_id.ToString(),
                    Active             = s.active,
                    CreatedBy          = s.createdby,
                    CreatedDate        = s.createddate.ToString(),
                };
                return st;
            }
        }

        internal static IList<CancelRecordingContractDTO> GetCancelStatusDTO(int batchCancelId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
//                var contractIds = ctx.cancels.OrderBy(cclid => cclid.cancel_id).Where(EqualsToBatchCanceld(batchCancelId)).Select(c => c.contract_id).Distinct();  //RIQ-275 CV102012
                var contractIdx = (
                                        from q in ctx.cancels
                                        orderby q.cancel_id
                                        where  q.batch_cancel_id == batchCancelId
                                        select q.contract_id
                                    ).ToList().Distinct();
                var contractIds = contractIdx.Select(q => q);                                                                                                       //RIQ-275 CV102012
                var contractInfo = new List<CancelRecordingContractDTO>();
                foreach (var id in contractIds) {
                    var contractId = id;
                    var temp= ( from c in ctx.contracts
                                join l in ctx.legal_name on c.contract_id equals l.contract_id into names
                                from subNames in names.Where(n => n.primary == true).DefaultIfEmpty()
                                where c.contract_id == contractId
                                select new { c, name = subNames.last_name, legalNameId = subNames.legal_name_id } )
                             .AsEnumerable()
                             .Select(ctemp => new { contract = ctemp.c, ctemp.name, lnId = ctemp.legalNameId, ci  = contract_interval.GetInventoryUIList(ctemp.c.contract_id) })
                             .Select(co =>
                                 new CancelRecordingContractDTO {
                                     MasterId = contractId.ToString(),
                                     LastName = co.name,
                                     LegalNameId = co.lnId.ToString(),
                                     Share = co.contract.share,
                                     Year = co.ci.Select(d => d.Year).ToArray(),
                                     Units = co.ci.Select(d => d.Unit).ToArray(),
                                     Weeks = co.ci.Select(d => d.Week).ToArray(),
                                 }).SingleOrDefault();
                    contractInfo.Add(temp);

                }
                return contractInfo;
            }
        }

        internal static bool Save(IList<StatusDTO> uiList)
        {
            foreach (var statusDto in uiList)
            {
                Save(statusDto);
            }
            return true;
        }

        internal static bool Save(StatusDTO ui)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                int id = Convert.ToInt32(ui.StatusId);
                var s = id > 0
                         ? ctx.status
                              .SingleOrDefault(EqualsToStatusId(id))
                         : new status();
                if (s != null) {
                    // Resale Forclosure Orlando
                    bool neworUpdRecord;
                    var inventoryFound = from i in ctx.status.Where(i => i.status_id == id)
                                         select i;
                    if (inventoryFound.Any()) { neworUpdRecord = true; } else { neworUpdRecord = false; }
                    s.contract_id          = Convert.ToInt32(ui.ContractId);
                    s.status_master_id     = Convert.ToInt32(ui.StatusMasterId);
                    s.county_id            = ui.CountyId.NullIfEmpty<int?>();
                    s.original_county_id   = ui.OriginalCountyId.NullIfEmpty<int?>();
                    s.legal_name_id        = ui.LegalNameId.NullIfEmpty<int?>();
                    s.invoice              = ui.Invoice.NullIfEmpty<string>();
                    s.rec_date             = ui.RecDate.NullIfEmpty<DateTime?>();
                    s.book                 = ui.Book.NullIfEmpty<string>();
                    s.page                 = ui.Page.NullIfEmpty<int?>();
                    s.assign_num           = ui.AssignmentNumber.NullIfEmpty<string>();
                    s.effective_date       = ui.EffectiveDate.NullIfEmpty<DateTime?>();
                    s.comments             = ui.Comment.Trim().NullIfEmpty<string>();
                    s.batch                = ui.Batch.NullIfEmpty<int?>();
                    s.createdby            = ui.CreatedBy.NullIfEmpty<string>();
                    s.active               = ui.Active;
                    s.upload_batch_id      = ui.UploadeBatchId.NullIfEmpty<int?>();
                    // Resale Forclosure Orlando
                    s.modifieddate = neworUpdRecord ? DateTime.Now : s.modifieddate = null;
                    s.modifiedby = neworUpdRecord ? ui.ModifyBy : s.modifiedby = null;

                }

                if (id == 0) {
                    s.createddate          = DateTime.Now;
                    ctx.AddTostatus(s);
                }
                return ctx.SaveChanges() > 0;
            }
        }
    }
}