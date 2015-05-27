using System.Collections.Generic;
using System.Linq;
using Greenspoon.Tess.Classes;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class status_group
    {
        public static List<DropDownItem> GetStatusGroupList() {
            // Declare the return list.
            var groupList = new List<DropDownItem>();
                     // Populate the list.
            using(var ctx = DataContextFactory.CreateContext()) {
                var groups = (from s in ctx.status_group
                              select new
                              {
                                  Name  = s.status_group_name,
                                  Value = s.status_group_id
                              }).ToList();
                // Build the list with in a groupList.
                foreach(var group in groups) {
                    if(group.Name != null && group.Name.Trim().Length > 0) {
                        groupList.Add(new DropDownItem
                        {
                            Name  = group.Name,
                            Value = group.Value.ToString()
                        });
                    }
                }
                // if list has values.
                // insert an empty row.
                if(groupList.Any() == true) {
                    groupList.Insert(0, new DropDownItem());
                }
            }
            return groupList;
        }
    } // end of class.
}