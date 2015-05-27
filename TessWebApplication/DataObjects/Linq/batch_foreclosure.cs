#region Includes
using System;
using System.Linq;
using System.Linq.Expressions;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
#endregion
namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class batch_foreclosure
    {
      
        internal static Expression<Func<batch_foreclosure, bool>> EqualsToBatchForeclosureId(int batchForeclosureId)
        {
            return bf => bf.batch_foreclosure_id == batchForeclosureId;
        }
      
        public static bool isValid(int batchForeclosureId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.batch_foreclosure.Exists(EqualsToBatchForeclosureId(batchForeclosureId));
            }
        }
      
        internal static BatchForeclosureDTO GetBatchForeclosureDTO()
        {
            return new BatchForeclosureDTO();
        }

        internal static BatchForeclosureDTO GetBatchForeclosureDTO(int batchForeclosureId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var bfc = 
                    ctx.batch_foreclosure
                       .SingleOrDefault(EqualsToBatchForeclosureId(batchForeclosureId));
                if (bfc != null) {
                    var b = new BatchForeclosureDTO {
                        BatchForeclosureId = batchForeclosureId.ToString(),
                        ProjectId          = bfc.project_id.ToString(),
                        PhaseId            = bfc.phase_name_id.HasValue == true 
                                              ? bfc.phase_name_id.ToString() : string.Empty,
                        ForeclosureTypeId  = bfc.foreclosure_type_id.ToString(),  
                        BatchKey           = bfc.foreclosure_number.ToString(),
                        FKA                = bfc.fka.HasValue == true 
                                              ? bool.Parse(bfc.fka.ToString()) : false,
                        StatusId           = bfc.status_master_id.HasValue == true 
                                              ? bfc.status_master_id.ToString() : string.Empty,
                        JudgeId            = bfc.judge_id.HasValue == true 
                                              ? bfc.judge_id.ToString() : string.Empty,
                        FileDate           = bfc.filedate.ToDateOnly(),
                        CaseNumber         = bfc.case_number,
                        LLC                = bfc.fc_llc.HasValue == true
                                              ? bool.Parse(bfc.fc_llc.ToString()) : false,
                        ProecessedDate     = bfc.courtdate.ToDateOnly(),
                        ReturnDate         = bfc.returndate.ToDateOnly(),
                        HOAFileDate        = bfc.hoa_respond.ToDateOnly(),
                        CreatedBy          = bfc.createdby,
                        CreateDate         = bfc.createddate.ToDateOnly()
                    };
                    return b;
                }
                return null;
            }
        }

        internal static int Save(BatchForeclosureDTO ui)
        {
            var bfId = -1;
            try {
                // a local variable to indicate new or edit.
                // 0 indicates new entry
                // Greater than 0 indicates update.
                int batchForeclosureId;
                int.TryParse(ui.BatchForeclosureId, out batchForeclosureId);
                using (var ctx = DataContextFactory.CreateContext()) {
                    batch_foreclosure bf;
                    if (batchForeclosureId > 0) {
                        bf = ctx.batch_foreclosure
                                .SingleOrDefault(EqualsToBatchForeclosureId(batchForeclosureId));
                    }
                    else {
                        bf = new batch_foreclosure { createdby = ui.CreatedBy, createddate = DateTime.Now };
                    }
                    if (bf != null) {
                        bf.project_id           = int.Parse(ui.ProjectId);
                        bf.phase_name_id        = ui.PhaseId.NullIfEmpty<int?>();
                        bf.foreclosure_type_id  = int.Parse(ui.ForeclosureTypeId);
                        bf.foreclosure_number   = String.IsNullOrEmpty(ui.BatchKey) == true
                                                ? 0 : int.Parse(ui.BatchKey);
//                        bf.foreclosure_number  = int.Parse(ui.BatchKey);
                        bf.fka                  = ui.FKA;
//                        bf.status_master_id    = ui.FileDate.NullIfEmpty<int?>();
                        bf.status_master_id     = ui.StatusId.NullIfEmpty<int?>(); 
                        bf.judge_id             = ui.JudgeId.NullIfEmpty<int?>();
                        bf.filedate             = ui.FileDate.NullIfEmpty<DateTime?>();
                        bf.case_number          = ui.CaseNumber.NullIfEmpty<string>();
                        bf.fc_llc               = ui.LLC;
                        bf.courtdate            = ui.ProecessedDate.NullIfEmpty<DateTime?>();
                        bf.returndate           = ui.ReturnDate.NullIfEmpty<DateTime?>();
                        bf.hoa_respond          = ui.HOAFileDate.NullIfEmpty<DateTime?>();
                    }
                    if (batchForeclosureId == 0) {
                        ctx.AddTobatch_foreclosure(bf);
                    }
                    if (ctx.SaveChanges() > 0) {
                        bfId = bf.batch_foreclosure_id;
                    }
                    return bfId;
                }
            } catch { return bfId; }
        }
    }
}