using System.Collections.Generic;
using System.Linq;
using Greenspoon.Tess.Classes;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class project_points
    {
        public static List<DropDownItem> GetPointsGroup(int escrowId) {
            var pointsGroupList = new List<DropDownItem>();
            using(var ctx = DataContextFactory.CreateContext()) {
                var points = 
                                (from point in ctx.project_points
                                 from escrow in ctx.batch_escrow
                                 where escrow.batch_escrow_id == escrowId
                                && point.project_id == escrow.project_id
                                //&& point.phase_name_id == escrow.phase_name_id   Brian Request May 21, 2014
                                 select new
                                 {
                                     Name = point.display_grouping,
                                     Value = point.project_points_id
                                 }).ToList();
                foreach(var item in points) {
                    pointsGroupList.Add(new DropDownItem { Name = item.Name, Value = item.Value.ToString() });
                }
                if(pointsGroupList.Any() == true) {
                    pointsGroupList.Insert(0, new DropDownItem());
                }
            }
            return pointsGroupList;
        }
    }
}