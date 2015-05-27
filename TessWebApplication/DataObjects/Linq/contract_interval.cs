#region Includes
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using SearchKit;
#endregion
namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class contract_interval
    {
        #region Predicates
        internal static Expression<Func<contract_interval, bool>> EqualsToContractIntervalId(int contractIntervalId)
        {
            return ci => ci.contract_interval_id == contractIntervalId;
        }

        internal static Expression<Func<contract_interval, bool>> EqualsToContractId(int contractId)
        {
            return ci => ci.contract_id == contractId;
        }

        internal static Expression<Func<contract_interval, bool>> EqualsToContractIdAndActive(int contractId)
        {
            return ci => ci.contract_id == contractId && ci.is_active == true;
        }

        internal static Expression<Func<contract_interval, bool>> EqualsToWeekMasterId(int weekId)
        {
            return ci => ci.cont_week_master_id == weekId;
        }

        internal static Expression<Func<contract_interval, bool>> EqualsToYearMasterId(int yearId)
        {
            return ci => ci.cont_year_master_id == yearId;
        }

        internal static Expression<Func<contract_interval, bool>> EqualsToBuildingCode(string buildingCode)
        {
            return ci => ci.inventory_building.building_code == buildingCode;
        }

        internal static Expression<Func<contract_interval, bool>> EqualsToUnitNumber(string unitNumber)
        {
            return ci => ci.inventory_unit.unit_number == unitNumber;
        }

        internal static Expression<Func<contract_interval, bool>> ContainsInContractInterval(TitleSearchDTO ui)
        {
            var predicate = PredicateBuilder.True<contract_interval>();
            if (ui.ContWeekId.Length > 0)
            {
                int weekMasterId = Convert.ToInt32(ui.ContWeekId);
                predicate = predicate.And(EqualsToWeekMasterId(weekMasterId));
            }
            if (ui.ContYearId.Length > 0)
            {
                int yearMasterId = Convert.ToInt32(ui.ContYearId);
                predicate = predicate.And(EqualsToYearMasterId(yearMasterId));
            }
            if (ui.BuildingCode.Length > 0)
            {
                predicate = predicate.And(EqualsToBuildingCode(ui.BuildingCode));
            }
            if (ui.UnitNumber.Length > 0)
            {
                predicate = predicate.And(EqualsToUnitNumber(ui.UnitNumber));
            }
            return predicate;
        }

        internal static List<TitleSearchParam> TitleSearchInContractInterval(TitleSearchDTO ui)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var query = ctx.contract_interval
                               .AsExpandable()
                               .Where(ContainsInContractInterval(ui))
                               .Select(c => new TitleSearchParam
                               {
                                   ContractId = c.contract_id,
                                   BatchEscrowId = c.contract.batch_escrow_id,
                                   ProjectId = c.contract.batch_escrow.project_id,
                                   Contract_IntervalId = c.contract_interval_id
                               });
                return query.ToList();
            }
        }
        #endregion

        internal static List<InventoryDTO> GetInventoryUIList(int contractId)
        {
            var invList = new List<InventoryDTO>();
            using (var ctx = DataContextFactory.CreateContext())
            {
                var inv = ctx.contract_interval
                              .Where(EqualsToContractIdAndActive(contractId)).ToList();

                foreach (var i in inv)
                {
                    var ui = new InventoryDTO
                    {
                        ContractIntervalId = i.contract_interval_id.ToString(),
                        ContractId = i.contract_id.ToString(),
                        ContWeekMasterId = i.cont_week_master_id.ToString(),
                        Week = i.cont_week_master_id.HasValue ? i.cont_week_master.week_number
                                                                                       : string.Empty,
                        ContYearMasterId = i.cont_year_master_id.ToString(),
                        Year = i.cont_year_master.year_name,
                        InventoryBuildingId = i.inventory_building_id.ToString(),
                        Building = i.inventory_building.building_code,
                        InventoryUnitId = i.inventory_unit_id.ToString(),
                        Unit = i.inventory_unit.unit_number,
                        Bedroom = i.inventory_unit.number_of_bedrooms,
                        Floor = i.inventory_unit.floor_number.ToString(),
                        ABT = i.cont_year_master.a_b_t,
                        Active = i.is_active,
                    };
                    invList.Add(ui);
                }
                return invList;
            }
        }

        internal static InventoryDTO GetInventoryDTO(int contractIntervelId)
        {
            InventoryDTO ui = null;
            using (var ctx = DataContextFactory.CreateContext())
            {
                var i = ctx.contract_interval
                             .SingleOrDefault(EqualsToContractIntervalId(contractIntervelId));
                if (i != null)
                {
                    ui = new InventoryDTO
                    {
                        ContractIntervalId = i.contract_interval_id.ToString(),
                        ContractId = i.contract_id.ToString(),
                        ContWeekMasterId = i.cont_week_master_id.ToString(),
                        Week = i.cont_week_master_id.HasValue == true
                                                     ? i.cont_week_master.week_number : string.Empty,
                        ContYearMasterId = i.cont_year_master_id.ToString(),
                        Year = i.cont_year_master.year_name,
                        InventoryBuildingId = i.inventory_building_id.ToString(),
                        Building = i.inventory_building.building_code,
                        InventoryUnitId = i.inventory_unit_id.ToString(),
                        Unit = i.inventory_unit.unit_number,
                        Bedroom = i.inventory_unit.number_of_bedrooms,
                        Floor = i.inventory_unit.floor_number.ToString(),
                        ABT = i.cont_year_master.a_b_t,
                        Active = i.is_active
                    };
                }
            }
            return ui;
        }

        internal static bool Save(InventoryDTO ui)
        {
            var result = false;
            using (var ctx = DataContextFactory.CreateContext())
            {
                contract_interval i;
                int id = Convert.ToInt32(ui.ContractIntervalId);
                if (id == 0)
                {
                    i = new contract_interval { contract_id = Convert.ToInt32(ui.ContractId) };
                    ctx.AddTocontract_interval(i);
                }
                else
                {
                    i = ctx.contract_interval
                        .SingleOrDefault(EqualsToContractIntervalId(id));
                }
                if (i != null)
                {
                    // Resale Forclosure Orlando
                    bool neworUpdRecord;
                    var inventoryFound = from iv in ctx.contract_interval.Where(iv => iv.contract_interval_id == id)
                                         select iv;
                    if (inventoryFound.Any()) { neworUpdRecord = true; } else { neworUpdRecord = false; }
                    i.cont_week_master_id = ui.ContWeekMasterId.NullIfEmpty<int?>();
                    i.cont_year_master_id = Convert.ToInt32(ui.ContYearMasterId);
                    i.inventory_building_id = Convert.ToInt32(ui.InventoryBuildingId);
                    i.inventory_unit_id = Convert.ToInt32(ui.InventoryUnitId);
                    i.is_active = ui.Active;
                    i.createdby = ui.CreatedBy.NullIfEmpty<string>();
                    // Resale Forclosure Orlando
                    i.modifieddate = neworUpdRecord ? DateTime.Now : i.modifieddate = null;
                    i.modifiedby = neworUpdRecord ? ui.ModifyBy : i.modifiedby = null;
                    if (id == 0)
                    {
                        i.createddate = DateTime.Now;
                    }
                    if (ctx.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}