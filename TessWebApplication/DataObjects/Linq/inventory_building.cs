using System;
using System.Collections.Generic;
using System.Linq;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using System.Linq.Expressions;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class inventory_building
    {
        internal static Expression<Func<inventory_building, bool>> EqualsToInventoryBuildingId(int id)
        {
            return ib => ib.inventory_building_id.Equals(id);
        }

        internal static inventory_building GetBuilding(int buildingId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.inventory_building.SingleOrDefault(EqualsToInventoryBuildingId(buildingId));
            }
        }

        internal static List<DropDownItem> GetInventoryBuildingList(batch_escrow be)
        {
            var invBuildList = new List<DropDownItem>();
            using (var ctx = DataContextFactory.CreateContext()) {
                var ib = ( from b in ctx.inventory_building
                           where b.project_id == be.project_id
                           orderby b.building_code ascending
                           select new {
                               Name  = b.building_name,
                               Code  = b.building_code,
                               Value = b.inventory_building_id
                           } ).ToList();
                invBuildList.AddRange(ib.Select(item => new DropDownItem { Name = string.Format("[{1}] {0}", item.Name, item.Code), Value = item.Value.ToString() }));
                invBuildList.Insert(0, new DropDownItem());
            }
            return invBuildList;
        }

        internal static int Save(BuildingDTO ui)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                int contractId = Convert.ToInt32(ui.ContractId);
                int id = Convert.ToInt32(ui.InventoryBuildingId);
                var projId = ctx.contracts.SingleOrDefault(c => c.contract_id.Equals(contractId)).batch_escrow.project_id;
                if (!projId.HasValue) { return 0; }

                var b = id > 0
                            ? ctx.inventory_building.SingleOrDefault(EqualsToInventoryBuildingId(id))
                            : ctx.inventory_building.CreateObject();

                b.project_id = Convert.ToInt32(projId);
                b.building_active = ui.Active;
                b.building_code = ui.BuildingCode.NullIfEmpty<string>();
                b.building_name = ui.BuildingName.NullIfEmpty<string>();

                if (id == 0) {
                    b.createdby = ui.CreatedBy.NullIfEmpty<string>();
                    b.createddate = DateTime.Now;
                    ctx.inventory_building.AddObject(b);
                }
                try {
                    ctx.SaveChanges();
                    return b.inventory_building_id;
                } catch {
                    return 0;
                }

            }
        }
    }
}