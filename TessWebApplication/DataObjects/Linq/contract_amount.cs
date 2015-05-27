#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class contract_amount
    {
        public static Expression<Func<contract_amount, bool>> EqualsToContractId(int contractId)
        {
            return a => a.contract_id == contractId;
        }

        public static List<TransactionDTO> GetTransactionUIList(int contractId)
        {
            var transList = new List<TransactionDTO>();
            using (var ctx = DataContextFactory.CreateContext()) {
                var ca = ctx.contract_amount.Where(EqualsToContractId(contractId));
                if (ca.Any() == true) {
                    foreach (var c in ca) {
                        var ui = new TransactionDTO {
                            ContractAmountId = c.contract_amt_id.ToString(),
                            ContractId       = c.contract_id.ToString(),
                            Amount           = c.amount.ToCurrency(),
                            AmountTypeId     = c.contract_amount_field_id.ToString(),
                            AmountTypeDesc   = c.contract_amount_field.contract_amt_field_name,
                            CreatedBy        = c.createdby,
                            CreatedDate      = c.createddate.ToDateOnly()
                        };
                        transList.Add(ui);
                    }
                    return transList;
                }
                return null;
            }
        } 
    }
}