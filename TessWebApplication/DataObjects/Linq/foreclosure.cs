#region Includes
using System;
using System.Linq;
using System.Linq.Expressions;
using SearchKit;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
#endregion
namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class foreclosure
    {
        #region Predicate
        internal static Expression<Func<foreclosure, bool>> EqualsToForeclosureId(int fId)
        {
            return f => f.foreclosure_id == fId;
        }
        internal static Expression<Func<foreclosure, bool>> EqualsToBatchForeclosureId(int batchForeclosureId)
        {
            return f => f.batch_foreclosure_id == batchForeclosureId;
        }
        internal static Expression<Func<foreclosure, bool>> EqualsToContractId(int contractId)
        {
            return f => f.contract_id == contractId;
        }
        internal static Expression<Func<foreclosure, bool>> EqualsToActive()
        {
            return f => f.is_active == true;
        }
        internal static Expression<Func<foreclosure, bool>> NotEqualsToBatchForeclosureId(int batchForeclosureId)
        {
            return f => f.batch_foreclosure_id != batchForeclosureId;
        }
        internal static Expression<Func<foreclosure, bool>> IsActive(int contractId)
        {
            return EqualsToContractId(contractId).And(EqualsToActive());
        }
        internal static Expression<Func<foreclosure, bool>> EqualsToContractIdAndBatchForeclosureId(int batchForeclosureId, int contractId)
        {
            return EqualsToBatchForeclosureId(batchForeclosureId).And(EqualsToContractId(contractId));
        }
        #endregion
        internal static foreclosure GetForeclosure(int batchForeclosureId, int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.foreclosures.AsExpandable()
                           .FirstOrDefault(EqualsToContractIdAndBatchForeclosureId(batchForeclosureId, contractId));
            }
        }
        // If there is an active foreclosure with a contract id
        // return the foreclosure.
        internal static foreclosure GetActiveForeclosure(int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.foreclosures.AsExpandable().FirstOrDefault(IsActive(contractId));
            }
        }
        // Returns ActiveForeclosure which is Active but not active in a given batch.
        internal static foreclosure GetActiveForeclosure(int contractId, int batchForeclosureId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.foreclosures.AsExpandable()
                    .FirstOrDefault(IsActive(contractId)
                      .And(NotEqualsToBatchForeclosureId(batchForeclosureId)));
            }
        }
        // Get the list of foreclosed contract id by a Batch Foreclosure ID.
        internal static List<DropDownItem> GetForeclosureContractList(int batchForeclosureId)
        {
            var contractlist = new List<DropDownItem>();
            using (var ctx = DataContextFactory.CreateContext())
            {
                var contracts = ( from f in ctx.foreclosures.AsExpandable()
                                  .Where(EqualsToBatchForeclosureId(batchForeclosureId))
                                  orderby f.createddate descending
                                  select new {
                                      Name  = f.contract_id,
                                      Value = f.foreclosure_id
                                  } ).ToList();
                contractlist.AddRange(contracts.Select(contract => new DropDownItem {Name = contract.Name.ToString(), Value = contract.Value.ToString()}));
            }
            return contractlist;
        }
        // Returns the business object for Foreclosure UI
        internal static ForeclosureDTO GetForeclosureUI(int foreclosureId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var f = ctx.foreclosures.AsExpandable()
                           .SingleOrDefault(EqualsToForeclosureId(foreclosureId));
                if (f == null) { return null; }

                var fDto = new ForeclosureDTO {
                    ForeclosureId  = f.foreclosure_id.ToString(),
                    BatchForeclosureId 
                                       = f.batch_foreclosure_id.ToString(),
                    ContractId     = f.contract_id.ToString(),
                    DevK           = f.contract.dev_k_num,
                    IneterestPct   = f.interest_rate.ToString(),
                    DefaultBalance = f.default_balance.ToString(),
                    DefaultDate    = f.default_date.ToDateOnly(),
                    OnHold         = Convert.ToBoolean(f.hold),
                    Bankrupt       = Convert.ToBoolean(f.bankrupt),
                    MortgageBook   = f.contract.mortgage_book,
                    MortgagePage   = f.contract.mortgage_page,
                    MortgageDate   = f.contract.mortgage_date.ToDateOnly(),
                    DeedBook       = f.contract.deed_book,
                    DeedPage       = f.contract.deed_page,
                    DeedDate       = f.contract.deed_recording_date.ToDateOnly(),
                    Vesting        = f.contract.vesting_id.HasValue == true 
                                                        ? f.contract.vesting.vesting_type : string.Empty,
                    Points         = f.contract.points.ToString(),
                    PointsGroup    = f.contract.project_points_id.HasValue == true 
                                                        ? f.contract.project_points.display_grouping : string.Empty,
                    Active         = f.is_active
                };
                return fDto;
            }
        }

        /// <summary>
        /// Iterate over a set of foreclosures that are connected to the contract.
        /// Check to see if there is an active foreclosure that does have a default date.
        /// </summary>
        /// <remarks>This will return the first valid date it finds.</remarks>
        /// <param name="fList"></param>
        /// <returns>foreclosure default date.</returns>
        internal static string GetForeclosureDate(EntityCollection<foreclosure> fList)
        {
            if (fList == null || fList.Any() == false) {
                return null;
            }
            string result = null;

            var foreclosure = fList
                .FirstOrDefault(f => f.is_active == true && f.default_date.HasValue == true);
            if (foreclosure != null) {
                result = foreclosure.default_date.ToDateOnly();
            }
            return result;
        }
        /// <summary>
        /// Iterate over a contract list of foreclosures.
        /// check to see if there is an active bankrupt foreclosure exist.
        /// </summary>
        /// <param name="fList"></param>
        /// <returns></returns>
        internal static string IsBankrupt(EntityCollection<foreclosure> fList)
        {
            var result = "No";
            if (fList != null && fList.Any() == true) {
                if (fList.Any(f => f.is_active == true && f.bankrupt == true)) {
                    result = "Yes";
                }
            }
            return result;
        }

        internal static bool Save(ForeclosureDTO f)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var foreclosureId = int.Parse(f.ForeclosureId);
                var fc =  ctx.foreclosures.AsExpandable()
                             .Single(EqualsToForeclosureId(foreclosureId));
                if (fc == null) { return false; }

                fc.default_balance = string.IsNullOrEmpty(f.DefaultBalance) == false
                                             ? decimal.Parse(f.DefaultBalance)
                                             : (decimal?)null;
                fc.interest_rate = string.IsNullOrEmpty(f.IneterestPct) == false
                                           ? decimal.Parse(f.IneterestPct)
                                           : (decimal?)null;
                fc.default_date = string.IsNullOrEmpty(f.DefaultDate) == false
                                          ? DateTime.Parse(f.DefaultDate)
                                          : (DateTime?)null;
                fc.hold = f.OnHold;
                fc.bankrupt = f.Bankrupt;
                fc.is_active = f.Active;
                return ctx.SaveChanges() > 0;
            }
        }
    }// end of class
}// end of namespace