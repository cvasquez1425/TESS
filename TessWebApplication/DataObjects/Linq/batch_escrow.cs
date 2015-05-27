#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.Services;
#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class batch_escrow
    {
        internal static Expression<Func<batch_escrow, bool>> EqualsToBatchEscrowId(int batchEscrowId)
        {
            return e => e.batch_escrow_id == batchEscrowId;
        }

        internal static bool IsValid(int batchEscrowId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.batch_escrow.Exists(EqualsToBatchEscrowId(batchEscrowId));
            }
        }

        internal static IEnumerable<string> GetInvalidIds(string ids)
        {
            return DbService.GetInvalidBatchUploadKeys(ids, KeyType.EscrowId);
        }

        internal static BatchEscrowDTO CreateBatchEscrowDTO()
        {
            return new BatchEscrowDTO();
        }

        internal static BatchEscrowDTO GetBatchEscrowDTO(int batchEscrowId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var be = ctx.batch_escrow
                            .SingleOrDefault(EqualsToBatchEscrowId(batchEscrowId));
                if (be == null) { return null; }
                return new BatchEscrowDTO {
                    BatchEscrowId    = be.batch_escrow_id.ToString(),
                    ProjectId        = be.project_id.ToString(),
                    PhaseId          = be.phase_name_id.ToString(),
                    EscrowKey        = be.batch_escrow_number.ToString(),
                    BatchAmount      = be.batch_total.ToString(),
                    TotalDeedPages   = be.rec_deed_pages.ToString(),
                    TotalNotePages   = be.rec_note_pages.ToString(),
                    TitleInsurance   = be.bat_titleco,
                    NonEscrow        = be.non_escrow ?? false,
                    Cashout          = be._cash_out  ?? false,       // cashout deals
                    PartnerId        = be.partner_id.ToString(),
                    CreatedBy        = be.createdby,
                    CreatedDate      = be.createddate.ToDateOnly(),
                //Resale Forclosure Orlando Perfect Practice
                    ModifyDate       = DateTime.Now.ToString(),
                    ModifyBy         = be.modifiedby
                };
            }
        }

        internal static int Save(BatchEscrowDTO ui)
        {
            var beId = Convert.ToInt32(ui.BatchEscrowId);
            using (var ctx = DataContextFactory.CreateContext()) {
                batch_escrow be;
                if (beId > 0) {
                    be = ctx.batch_escrow
                            .SingleOrDefault(EqualsToBatchEscrowId(beId));
                }
                else {
                    be = new batch_escrow { createddate = DateTime.Now };
                    ctx.batch_escrow.AddObject(be);
                }

                if (be != null) {
                    bool neworUpdRecord;
                    var batch_escrowFound = from ct in ctx.batch_escrow.Where(b => b.batch_escrow_id == beId)
                                            select ct;
                    if (batch_escrowFound.Any() && beId > 0) { neworUpdRecord = true; } else { neworUpdRecord = false; }
                    be.project_id          = ui.ProjectId.NullIfEmpty<int?>();
                    be.phase_name_id       = ui.PhaseId.NullIfEmpty<int?>();
                    be.batch_total         = ui.BatchAmount.NullIfEmpty<decimal?>();
                    be.rec_deed_pages      = ui.TotalDeedPages.NullIfEmpty<int?>();
                    be.rec_note_pages      = ui.TotalNotePages.NullIfEmpty<int?>();
                    be.bat_titleco         = ui.TitleInsurance;
                    be.non_escrow          = ui.NonEscrow;
                    be.cash_out            = ui.Cashout;                // cashout deals
                    be.partner_id          = ui.PartnerId.NullIfEmpty<int?>();
                    be.createdby           = ui.CreatedBy;
                    // Resale Forclosure Orlando
                    be.modifieddate        = neworUpdRecord ? DateTime.Now : be.modifieddate = null;
                    be.modifiedby          = neworUpdRecord ? ui.ModifyBy  : be.modifiedby   = null;
                    if (ctx.SaveChanges() > 0) {
                        beId = be.batch_escrow_id;
                    }
                }
                return beId;
            }
        }

        internal static batch_escrow GetBatchEscrowByContractIntervalId(int contractInterfalId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.contract_interval
                           .FirstOrDefault(contract_interval
                                            .EqualsToContractIntervalId(contractInterfalId))
                           .contract.batch_escrow;
            }
        }

        internal static batch_escrow GetBatchEscrowByContractId(int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.contracts
                           .SingleOrDefault(contract.EqualsToContractId(contractId))
                           .batch_escrow;
            }
        }

        internal static int GetBatchEscrowIdByContractId(int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var tempId = ctx.contracts
                                .SingleOrDefault(contract.EqualsToContractId(contractId))
                                .batch_escrow.batch_escrow_id;
                return tempId > 0 ? tempId : 0;
            }
        }

        // find out whether or not it is a las vegas project
        internal static int GetLasVegasProject(int escrowKey)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                return ctx.batch_escrow
                       .SingleOrDefault(EqualsToBatchEscrowId(escrowKey))
                       .project_id ?? 0;
                       
            }
        }

        // Verify the Escrow key entered in /Admin/Pages/BulkCommentsByEscrowKeyUpdate.aspx 
        //                                  Status Code screen [4] for Escrow Keys with individual comments? 
        internal static int EscrowKeyValidationByEscrowId(int batchEscrowId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var queryEscrow = ctx.contracts.Join(
                                    ctx.batch_escrow,
                                    c => c.batch_escrow_id,
                                    b => b.batch_escrow_id,
                                    (c, b) => new { c.contract_id, c.batch_escrow_id }).Where(c => c.batch_escrow_id == batchEscrowId).ToList();

                int fullCount = queryEscrow.Count();
                return fullCount;                                    
            }
        }

        // Verify the Escrow key entered in /Admin/Pages/BulkCommentsByEscrowKeyUpdate.aspx 
        //                                  Status Code screen [4] for Escrow Keys with individual comments? 
        internal static List<UpdateCommentDTO> ListContractIdByEscrowId(int batchEscrowId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var queryEscrow = ctx.contracts.Join(
                                    ctx.batch_escrow,
                                    c => c.batch_escrow_id,
                                    b => b.batch_escrow_id,
                                    (c, b) => new UpdateCommentDTO { MasterId = c.contract_id, BatchEscrowId = c.batch_escrow_id }).Where(c => c.BatchEscrowId == batchEscrowId).ToList();
                return queryEscrow;
            }
        }

        //Cashout deals
        internal static bool IsCashOut(int batchEscrowId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                return ctx.batch_escrow
                          .SingleOrDefault(EqualsToBatchEscrowId(batchEscrowId))
                          .cash_out ?? false;
                          
            }
        }

        internal static bool IsNonEscrow(int batchEscrowId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.batch_escrow
                          .SingleOrDefault(EqualsToBatchEscrowId(batchEscrowId))
                          .non_escrow ?? false;
            }
        }

        // Owner's Policy do not bulk scan when Master ID is Inactive CV082012
        internal static bool IsActiveMasterID(int contractId)
        {
            using (var ctx =DataContextFactory.CreateContext()) {
                var tempStatus = ctx.contracts
                                .SingleOrDefault(contract.EqualsToContractId(contractId))
                                .contract_active;
                return tempStatus ;
            }
        }
        // Single Owner Policy status
        internal static bool IsOwnerPolicyActive(int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var data = from s in ctx.contracts
                           where s.contract_id == contractId && s.policy_number != null && s.is_gvg == false   // RIQ-318
                           select s;
                if (data.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

    } 
}