#region Include
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using Greenspoon.Tess.Classes;

#endregion
namespace Greenspoon.Tess.DataObjects.Linq {
    
    public partial class cancel_type {
        internal static Expression<Func<cancel_type, bool>> EqualsToCancelTypeId(int cancelTypeId) {
            return c => c.cancel_type_id == cancelTypeId;
        }
        internal static List<DropDownItem> GetCancelyTypeList() {
            var cancelTypeList = new List<DropDownItem>();
            using(var ctx = DataContextFactory.CreateContext()) {
                var cancelType = (from t in ctx.cancel_type
                                  select new {
                                      Name  = t.cancel_type_name,
                                      Value = t.cancel_type_id
                                  }).ToList();

                foreach(var item in cancelType) {
                    if(string.IsNullOrEmpty(item.Name.Trim()) == false) {
                        cancelTypeList.Add(new DropDownItem { Name = item.Name, Value = item.Value.ToString() });
                    }
                }
                if(cancelTypeList.Any() == true) {
                    cancelTypeList.Insert(0, new DropDownItem());
                }
            }
            return cancelTypeList;
        } //  end of Getcanceltypelist.
        internal static status_master GetStatusMasterByCancelTypeId(int cancelTypeId) {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.cancel_type
                          .SingleOrDefault(EqualsToCancelTypeId(cancelTypeId))
                          .status_master;
            }
        } // end of GetStatusMasterByCancelTypeId
    }
}