#region Includes
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Greenspoon.Tess.Classes;
using System.Web;
#endregion


namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class vesting
    {
        internal static Expression<Func<vesting, bool>> EqualsToVestingId(int vestingId){
            return v => v.vesting_id == vestingId;
        }
        internal static List<vesting> GetVestings() {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.vestings.ToList();
            }
        }
        internal static vesting GetVesting(int vestingId) {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.vestings.SingleOrDefault(EqualsToVestingId(vestingId));
            }
        }
        internal static bool Save(vesting param) {
            using(var ctx = DataContextFactory.CreateContext()) {
                var v = param.vesting_id > 0
                    ? ctx.vestings.SingleOrDefault(EqualsToVestingId(param.vesting_id))
                    : new vesting();
                if(v != null) {
                    v.vesting_active = param.vesting_active;
                    v.vesting_type   = param.vesting_type;
                }
                // If insert mode then add to the table.
                if(param.vesting_id == 0) {
                    v.createdby     = param.createdby;
                    v.createddate   = DateTime.Now;
                    ctx.AddTovestings(v);
                }
                var result = false;
                if(ctx.SaveChanges() > 0) {
                    result = true;
                }
                // return true or false to caller.
                return result;
            }
        } // end of save
        internal static List<DropDownItem> GetVestingList(bool AlloCache = true) {
            string strCacheKey = "VestingDropDownList";
            if((AlloCache == true) && (HttpContext.Current.Cache[strCacheKey] != null)) {
                return (List<DropDownItem>)HttpContext.Current.Cache[strCacheKey];
            }
            var vestingList = new List<DropDownItem>();
            // Populate the list.
            using(var ctx = DataContextFactory.CreateContext()) {
                // Get all projects in db.
                // Sorry order is by project id.
                var  vestings = (from v in ctx.vestings
                                 select new
                                 {
                                     Name  = v.vesting_type,
                                     Value = v.vesting_id
                                 }).ToList();
                // Combine Project ID and Name for Display
                foreach(var vesting in vestings) {
                    if(vesting.Name != null && vesting.Name.Trim().Length > 0) {
                        vestingList.Add(new DropDownItem { 
                            Name = vesting.Name
                            ,Value = vesting.Value.ToString() });
                    }
                }
                vestingList.Insert(0, new DropDownItem());
                if((AlloCache == true) && (HttpContext.Current.Cache[strCacheKey] == null)) {
                    HttpContext.Current
                        .Cache.Insert(strCacheKey, vestingList, null, DateTime.Now.AddHours(9), TimeSpan.Zero);
                }
                return vestingList;
            }
        } // end of vesting list

    } // end of class
}