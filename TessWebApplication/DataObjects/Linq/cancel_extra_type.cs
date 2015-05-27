using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Greenspoon.Tess.Classes;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class cancel_extra_type
    {
        internal static Expression<Func<cancel_extra_type, bool>> EqualsToCancelExtraTypeId(int cancelExtraTypeId) {
            return c => c.cancel_extra_type_id == cancelExtraTypeId;
        }
        internal static List<DropDownItem> GetCancelExtraTypeList() {
            var extraTypeList = new List<DropDownItem>();
            using(var ctx = DataContextFactory.CreateContext()) {
                var types = (from t in ctx.cancel_extra_type
                             select new
                             {
                                 Name  = t.cancel_extra_type_value,
                                 Value = t.cancel_extra_type_id
                             }).ToList();
                foreach(var item in types) {
                    extraTypeList.Add(new DropDownItem { Name = item.Name, Value=item.Value.ToString() });
                }
                if(extraTypeList.Any() == true) {
                    extraTypeList.Insert(0, new DropDownItem());
                }
            }
            return extraTypeList;
        } // end of Extra Type List.
        internal static List<cancel_extra_type> GetCancelExtraType() {
            var extraTypeList = new List<cancel_extra_type>();
            using(var ctx = DataContextFactory.CreateContext()) {
                extraTypeList = (from c in ctx.cancel_extra_type
                                 orderby c.cancel_extra_type_id descending
                                 select c).ToList<cancel_extra_type>();
            }
            return extraTypeList;
        }
        internal static cancel_extra_type GetCancelExtraType(int cancelExtraTypeId) {
            using(var ctx = DataContextFactory.CreateContext()) {
                var ct = ctx.cancel_extra_type
                            .SingleOrDefault(EqualsToCancelExtraTypeId(cancelExtraTypeId));
                return ct;
            }
        } //  end of Get cancel extra type
        internal static bool Save(cancel_extra_type param) {
            using(var ctx = DataContextFactory.CreateContext()) {
                var instance = param.cancel_extra_type_id > 0 
                              ? ctx.cancel_extra_type
                                   .SingleOrDefault(EqualsToCancelExtraTypeId(param.cancel_extra_type_id)) 
                              : new cancel_extra_type();
                if(instance != null) {
                    instance.cancel_extra_type_value   = param.cancel_extra_type_value;
                }
                // If insert mode then add to the table.
                if(param.cancel_extra_type_id == 0) {
                    instance.createdby        = param.createdby;
                    instance.createddate      = DateTime.Now;
                    ctx.AddTocancel_extra_type(instance);
                }

                var result = false;
                if(ctx.SaveChanges() > 0) {
                    result = true;
                }
                // return if Save is successful or not.
                return result;
            }// end of context
        }
    } //  end of class.
}