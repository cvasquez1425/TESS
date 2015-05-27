#region Includes
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Services;

#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class batch_cancel
    {
        internal static Expression<Func<batch_cancel, bool>> EqualsToBatchCancelId(int batchCancelId)
        {
            return c => c.batch_cancel_id == batchCancelId;
        }

        internal static bool IsValid(int batchCancelId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.batch_cancel.Exists(EqualsToBatchCancelId(batchCancelId));
            }
        }

        internal static BatchCancelDTO CreateBatchCancelDTO()
        {
            return new BatchCancelDTO();
        }

        internal static BatchCancelDTO GetBatchCancelDTO(int batchCancelId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var bc    = ctx.batch_cancel
                               .SingleOrDefault(EqualsToBatchCancelId(batchCancelId));
                if (bc == null) { return null; }
                var c = new BatchCancelDTO {
                    BatchCancelId   = bc.batch_cancel_id.ToString(),
                    ProjectId       = bc.project_id.ToString(),
                    ParentProjectId = bc.parent_project.ToString(),
                    ExtraNames      = bc.extra_names.ToString(),
                    ExtraPages      = bc.extra_pages.ToString(),
                    ExtraRecording  = bc.extra_recording.ToString(),                                   // Jira Brian's Request for Extra Recording
                    CancelTypeId    = bc.cancel_type_id.HasValue == true 
                                             ? bc.cancel_type_id.ToString() : string.Empty,
                    CancelStatusId  = bc.status_master_id.ToString(),
                    CancelNumber    = bc.batch_cancel_number.ToString(),
                    CreatedBy       = bc.createdby,
                    CreatedDate     = bc.createddate.ToDateOnly()
                };
                return c;
            }
        }

        internal static IEnumerable<string> GetInvalidIds(string ids)
        {
            //if (ids == null) return null;
            //var tempIDs = ids.ConvertAll(Convert.ToInt32);
            //using (var ctx = DataContextFactory.CreateContext()) {
            //    return
            //        tempIDs.Where(i => ctx.batch_cancel.All(c => c.batch_cancel_id != i)).ToList().
            //            ConvertAll(Convert.ToString);
            //}
            return DbService.GetInvalidBatchUploadKeys(ids, KeyType.CancelId);
        }

        internal static int Save(BatchCancelDTO ui)
        {
            var bcId = -1;
            try {
                // a local variable to indicate new or edit.
                // 0 indicates new entry
                // Greater than 0 indicates update.
                int batchCancelId;
                int.TryParse(ui.BatchCancelId, out batchCancelId);
                using (var ctx = DataContextFactory.CreateContext()) {
                    batch_cancel bc;
                    if (batchCancelId > 0)
                        bc = ctx.batch_cancel
                                .SingleOrDefault(EqualsToBatchCancelId(batchCancelId));
                    else { 
                        bc = new batch_cancel { createdby = ui.CreatedBy, createddate = DateTime.Now };
                    }
                    if (bc != null) {
                        bc.project_id           = int.Parse(ui.ProjectId);
                        bc.parent_project       = ui.ParentProjectId.NullIfEmpty<int?>();
                        bc.extra_names          = ui.ExtraNames.NullIfEmpty<int?>();
                        bc.extra_pages          = ui.ExtraPages.NullIfEmpty<int?>();
                        bc.extra_recording      = ui.ExtraRecording.NullIfEmpty<int?>() ;                             // Jira

                        bc.cancel_type_id       = ui.CancelTypeId.NullIfEmpty<int?>();
                        bc.batch_cancel_number  = ui.CancelNumber.NullIfEmpty<int?>();
                        bc.status_master_id     =  int.Parse(ui.CancelStatusId);
                    }

                    if (batchCancelId == 0) {
                        ctx.AddTobatch_cancel(bc);
                    }

                    if (ctx.SaveChanges() > 0) {
                        bcId = bc.batch_cancel_id;
                        ui.BatchCancelId = bcId.ToString();
                        Linq.project_link.Save(ui);
                    }
                    return bcId;
                }
            } catch { return bcId; }
        }

        // RIQ-306 Extra Recording Field - Batch Cancel Web Form
        internal static bool IsProjId108(int projectId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                int[] projectID108 = { 108 };
                List<int> projId = projectID108.Select(l => l).ToList();

                var proj108Query =
                    from p in ctx.projects
                    where projId.Contains(projectId)
                    select p;

                if (proj108Query.Any())
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