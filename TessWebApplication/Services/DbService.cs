using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.AdoNet;
using Greenspoon.Tess.DataObjects.Linq;

namespace Greenspoon.Tess.Services
{
    public static class DbService
    {
        public static bool UpdateStatusByEscrowKey(UpdateStatusByEscrowKeyDTO arg)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var status_master_id = int.Parse(arg.StatusMasterId);
                var invoice          = arg.Invoice.NullIfEmpty<string>();
                var effDate          = arg.EffectiveDate.NullIfEmpty<DateTime?>();
                var docRec           = arg.RecordDate.NullIfEmpty<DateTime?>();
                var batchKey         = int.Parse(arg.BeatchEscrowId);
                var book             = arg.Book.NullIfEmpty<string>();
                var page             = arg.Page.NullIfEmpty<int?>();
                var assign           = arg.Assign.NullIfEmpty<string>();
                var county           = arg.CountyId.NullIfEmpty<int?>();
                var orgCounty        = arg.OrigCountyId.NullIfEmpty<int?>();
                var comment          = arg.Comments.NullIfEmpty<string>();
                var userName         = arg.UserName;
                return ctx.UpdateStatusByEscrowKey(
                       assign: assign,
                       status_master_id: status_master_id,
                       book: book,
                       comments: comment,
                       county_id: county,
                       orig_county_id: orgCounty,
                       batch_escrow_id: batchKey,
                       eff_date: effDate,
                       invoice: invoice,
                       page: page,
                       rec_date: docRec,
                       user_id: userName) > 0;
            }
        }

        public static Tuple<bool, string> BatchStatusUpdate(UpdateBatchStatusDTO arg)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var recUpdated = new ObjectParameter("rec_upd", typeof(int));
                bool result = ctx.StatusBatchUpdate(key_type: int.Parse(arg.KeyType),
                                              project_id: arg.ProjectId.NullIfEmpty<int?>(),
                                              status_master_id: int.Parse(arg.StatusMasterId),
                                              key_data: arg.KeyData,
                                              eff_date: arg.EffectiveDate.NullIfEmpty<DateTime?>(),
                                              assign: arg.Assign.NullIfEmpty<string>(),
                                              orig_county_id: arg.OriginalCountyId.NullIfEmpty<int?>(),
                                              county_id: arg.CountyId.NullIfEmpty<int?>(),
                                              comments: arg.Comments.NullIfEmpty<string>(),
                                              user_id: arg.UserName,
                                              book: arg.Book.NullIfEmpty<string>(),
                                              page: arg.Page.NullIfEmpty<int?>(),
                                              rec_date: arg.RecordDate.NullIfEmpty<DateTime?>(), rec_upd: recUpdated) > 0;

                return new Tuple<bool, string>(result, recUpdated.Value.ToString());
            }
        }

        public static bool BatchEscrowBulkScan(int escrowId, string userName)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                try {
                    ctx.BatchEscrowBulkScan(escrowId, userName);
                    return true;
                } catch {
                    return false;
                }
            }
        }

        public static bool BatchCancelBulkScan(int cancelId, string userName)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                try {
                    ctx.BatchCancelBulkScan(cancelId, userName);
                    return true;
                } catch {
                    return false;
                }
            }
        }

        public static IList<GetDocLocation_Result> GetBulkScanImages(string projectId, string contractId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.GetDocLocation(projectId, contractId).AsEnumerable().ToList();
            }
        }
       
        public static IEnumerable<string> GetInvalidBatchUploadKeys(string delimatedKeys, KeyType type)
        {
            if (string.IsNullOrEmpty(delimatedKeys)) return null;
            var sb = new StringBuilder();
            sb.AppendLine(@"create Table #Items( item varchar(max) )");
            sb.AppendLine(string.Format("insert into #Items(item) ( select * from dbo.funcListToTableVar('{0}', ','))", delimatedKeys));
            sb.AppendLine(@"select item from #items");
            AddKeyLookUpCondition(sb, type);
            sb.AppendLine(@"drop table #items");

            var dt = Db.GetDataTable(sb.ToString());
            var result = dt.AsEnumerable().Select(columns => columns[0].ToString()).ToList();
            return result;
        }

        static void AddKeyLookUpCondition(StringBuilder sb, KeyType type)
        {
            switch (type) {
                case KeyType.MasterId:
                    sb.AppendLine(@"left outer join [contract] on");
                    sb.AppendLine(@"contract.contract_id = #items.item");
                    sb.AppendLine(@"where contract.contract_id is null");
                    return;
                case KeyType.CancelId:
                    sb.AppendLine(@"left outer join [batch_cancel] on");
                    sb.AppendLine(@"batch_cancel.batch_cancel_id = #items.item");
                    sb.AppendLine(@"where batch_cancel.batch_cancel_id is null");
                    return;
                case KeyType.EscrowId:
                    sb.AppendLine(@"left outer join [batch_escrow] on");
                    sb.AppendLine(@"batch_escrow.batch_escrow_id = #items.item");
                    sb.AppendLine(@"where batch_escrow.batch_escrow_id is null");
                    return;
                case KeyType.DevKId:
                    sb.AppendLine(@"left outer join [contract] on");
                    sb.AppendLine(@"contract.dev_k_num = #items.item");
                    sb.AppendLine(@"where contract.dev_k_num is null");
                    return;
                case KeyType.ClientBatchId:                                             // Adding Client Batch as a new Identifier August 2014
                    sb.AppendLine(@"left outer join [contract] on");
                    sb.AppendLine(@"contract.client_batch = #items.item");
                    sb.AppendLine(@"where contract.client_batch is null");
                    return;
                default:
                    throw new ApplicationException("Key Tupe undefined.");
            }
        }
    }

    public enum KeyType
    {
        MasterId,
        EscrowId,
        CancelId,
        DevKId,
        ClientBatchId
    }
}