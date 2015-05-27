using System.Collections.Generic;
using System.Linq;
using Greenspoon.Tess.Classes;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class developer_master
    {
        public static IEnumerable<DropDownItem> GetDevMaster()
        {
            var devMasterList = new List<DropDownItem>();
            using (var db = DataContextFactory.CreateContext()) {
                var devMasters = ( from m in db.developer_master
                                   select new {
                                       Name = m.developer_master_name,
                                       Value = m.developer_master_id
                                   } ).AsEnumerable();
                foreach (var item in devMasters) {
                    devMasterList.Add(new DropDownItem { Name = item.Name, Value= item.Value.ToString() });
                }
                devMasterList.Insert(0, new DropDownItem());
                return devMasterList;
            }
        }
    }
}