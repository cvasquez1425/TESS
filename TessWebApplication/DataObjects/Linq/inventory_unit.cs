#region Includes

using System;
using System.Linq;
using System.Collections.Generic;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using System.Linq.Expressions;

#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class inventory_unit
    {
        internal static Expression<Func<inventory_unit, bool>> EqualsToInventoryUnitId(int id)
        {
            return u => u.inventory_unit_id.Equals(id);
        }

        internal static UnitDTO GetInventoryUnit(int unitId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var u = ctx.inventory_unit.SingleOrDefault(EqualsToInventoryUnitId(unitId));

                return new UnitDTO {
                    UnitNumber = u.unit_number,
                    Description = u.unit_desc,
                    NumberOfBedroom = u.number_of_bedrooms
                };
            }
        }

        internal static List<DropDownItem> GetInventoryUnitListByBuildingId(int buildingId)
        {
            var invUnitList = new List<DropDownItem>();
            using (var ctx = DataContextFactory.CreateContext()) {
                var ib = ( from u in ctx.inventory_unit
                           where u.inventory_building_id == buildingId
                           orderby u.unit_number ascending
                           select new {
                               Name  = u.unit_number,
                               Value = u.inventory_unit_id
                           } ).ToList();
                invUnitList.AddRange(ib.Select(item => new DropDownItem { Name = item.Name, Value = item.Value.ToString() }));
                invUnitList.Insert(0, new DropDownItem());
            }
            return invUnitList;
        }

        internal static int Save(UnitDTO ui)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                int id = Convert.ToInt32(ui.InventoryUnitId);
                var u = id > 0
                            ? ctx.inventory_unit.SingleOrDefault(EqualsToInventoryUnitId(id))
                            : ctx.inventory_unit.CreateObject();
                u.inventory_building_id = Convert.ToInt32(ui.InventoryBuildingId);
                u.unit_number = ui.UnitNumber;
                u.unit_desc = ui.Description.NullIfEmpty<string>();
                u.number_of_bedrooms = ui.NumberOfBedroom.NullIfEmpty<string>();
                u.unit_active = ui.Active;
                u.createdby = ui.CreatedBy.NullIfEmpty<string>();

                if (id == 0) {
                    u.createddate = DateTime.Now;
                    ctx.inventory_unit.AddObject(u);
                }
                try {
                    ctx.SaveChanges();
                    return u.inventory_unit_id;
                } catch { return 0; }
            }
        }
    }
}