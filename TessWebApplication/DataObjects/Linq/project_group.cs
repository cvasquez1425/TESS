#region Includes
using System.Collections.Generic;
using System.Linq;
using Greenspoon.Tess.Classes;

#endregion
namespace Greenspoon.Tess.DataObjects.Linq {
    public partial class project_group {
        internal static List<DropDownItem> GetProjectGroupDropdownList() {
            var projectGroupList = new List<DropDownItem>();
            using(var ctx = DataContextFactory.CreateContext()) {
                var groups = (from g in ctx.project_group 
                              orderby g.project_group_id ascending
                                  select new {
                                      Name = g.project_group_id,
                                      Value = g.project_group_id}).ToList();
                foreach(var item in groups) {
                    projectGroupList.Add(new DropDownItem { Name = item.Name.ToString(), Value = item.Value.ToString() });
                }
                projectGroupList.Insert(0, new DropDownItem());
            }
            return projectGroupList;
        }
    }
}