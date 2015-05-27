using System.Collections.Generic;
using System.Linq;
using Greenspoon.Tess.Classes;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class state
    {
        public static List<DropDownItem> GetStateList() {
            var stateList = new List<DropDownItem>();
            using(var db = DataContextFactory.CreateContext()) {
                var states = 
                     (from c in db.states
                      where c.state_name.Length > 0
                      select new
                      {
                          Name = c.state_name,
                          Value = c.state_id
                      }).ToList();
                foreach(var item in states) {
                    stateList.Add(new DropDownItem { Name = item.Name, Value = item.Value.ToString() });
                }
                stateList.Insert(0, new DropDownItem());
            }
            return stateList;
        }
    }
}