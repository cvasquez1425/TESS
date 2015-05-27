using System.Collections.Generic;
using System.Linq;
using Greenspoon.Tess.Classes;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class developer_group
    {
        public static IEnumerable<DropDownItem> GetDeveloperGroup()
        {
            var devGroupList = new List<DropDownItem>();
            using (var db = DataContextFactory.CreateContext()) {
                var devGroup = ( from d in db.developer_group
                                 select new {
                                     Name = d.developer_group_id,
                                     Value = d.developer_group_id
                                 } ).AsEnumerable();
                foreach (var item in devGroup) {
                    devGroupList.Add(new DropDownItem { Name = item.Name.ToString(), Value= item.Value.ToString() });
                }
                devGroupList.Insert(0, new DropDownItem());
                return devGroupList;
            }
        }
    }
}