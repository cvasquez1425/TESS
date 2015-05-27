#region Includes
using System;
using System.Linq;
using System.Linq.Expressions;
using SearchKit;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using System.Collections.Generic;
using System.Text;
using System.Data.Objects.DataClasses;
#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class Current_Value
    {
        internal static Expression<Func<cancel, bool>> EqualsToCancelId(int cancelId)
        {
            return c => c.cancel_id == cancelId;
        }

        internal static List<CurrentValueDTO> GetContractIdUIList(int BatchCancelId)
        {
            var list = new List<CurrentValueDTO>();
            using (var ctx = DataContextFactory.CreateContext())
            {
                var queryListIdx = ctx.cancels.Include("batch_cancel").Where(c => c.batch_cancel_id == BatchCancelId).OrderBy(ca => ca.cancel_id).ToList();
                var queryListIds = queryListIdx.Select(c => c );
                if (queryListIds != null && queryListIds.Any() == true)
                {
                    foreach (var id in queryListIds)
                    {
                        var contractId    = id.contract_id;
                        var cancelId      = id.cancel_id;
                        string purchasePrice = id.purch_price.HasValue ? id.purch_price.Value.ToString() : "";
                        var temp = (from c in ctx.contracts
                                    join l in ctx.legal_name on c.contract_id equals l.contract_id into names
                                    from subNames in names.Where(n => n.primary == true).DefaultIfEmpty()
                                    where c.contract_id == contractId
                                    select new { c, name = subNames.last_name })
                                      .AsExpandable()
                                      .Select(co => new CurrentValueDTO
                                      {
                                          CancelId     = cancelId, 
                                          ContractId   = contractId,
                                          LastName     = co.name,
                                          CurrentValue = purchasePrice,
                                      }).SingleOrDefault();
                        list.Add(temp);                        
                    } 
                }
                return list;
            } // end of context.
        }     // end of UI List

        internal static bool Save(IList<CurrentValueDTO> uiList)
        {
            foreach (var currentDto in uiList)
            {
                Save(currentDto);
            }
            return true;
        }

        internal static bool Save(CurrentValueDTO ui)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                int id = Convert.ToInt32(ui.CancelId);
                //int purcPrice = int.Parse(ui.CurrentValue);

                var s = id > 0
                         ? ctx.cancels
                              .SingleOrDefault(EqualsToCancelId(id))
                         : new cancel();
                if (s != null && !string.IsNullOrEmpty(ui.CurrentValue) )
                {
                    s.purch_price  = Convert.ToDecimal(ui.CurrentValue);
                    s.modifieddate = DateTime.Now;
                    s.modifiedby   = ui.ModifiedBy;

                }

                if (id == 0)
                {
                    s.createddate = DateTime.Now;
                    ctx.AddTocancels(s);
                }
                return ctx.SaveChanges() > 0;
            }
        }

    }
}