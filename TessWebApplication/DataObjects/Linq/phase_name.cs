#region Includes
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Greenspoon.Tess.Classes;
#endregion
namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class phase_name
    {
        internal static List<DropDownItem> GetPhaseNametList() {
            var phaseNameList = new List<DropDownItem>();
            using(var ctx = DataContextFactory.CreateContext()) {
                var phaseNames = (from p in ctx.phase_name
                                  select new
                                  {
                                      Name = p.phase_name1,
                                      Value = p.phase_name_id
                                  }).ToList();

                foreach(var item in phaseNames) {
                    phaseNameList.Add(new DropDownItem { Name = item.Name, Value = item.Value.ToString() });
                }
                phaseNameList.Insert(0, new DropDownItem());
            }
            return phaseNameList;
        }

    } // end of class
}