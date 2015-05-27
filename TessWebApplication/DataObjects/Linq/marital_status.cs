#region Includes
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using Greenspoon.Tess.Classes;

#endregion
namespace Greenspoon.Tess.DataObjects.Linq {
    public partial class marital_status {
        internal static List<DropDownItem> GetMaritalStatus(bool AllowCache = true) {
            string strCacheKey = "MaritalStatusDropDownList";
            // if cache allowed and already exist in cache. return list.
            if((AllowCache == true) && (HttpContext.Current.Cache[strCacheKey] != null)) {
                return (List<DropDownItem>)HttpContext.Current.Cache[strCacheKey];
            }
            var maritalList = new List<DropDownItem>();
            using(var ctx = DataContextFactory.CreateContext()) {
                var marital = (from m in ctx.marital_status
                               select new {
                                   Name = m.marital_status_description,
                                   Value = m.marital_status_id
                               }).ToList();
                foreach(var item in marital) {
                    maritalList.Add(new DropDownItem { Name = item.Name, Value=item.Value.ToString() });
                }
                if(maritalList.Any() == true) {
                    maritalList.Insert(0, new DropDownItem());
                }
            }
            // if cache allowed and not in the cache, add to cache.
            if((AllowCache == true) && (HttpContext.Current.Cache[strCacheKey] == null)) {
                HttpContext.Current
                    .Cache.Insert(strCacheKey, maritalList, null, DateTime.Now.AddHours(9), TimeSpan.Zero);
            }
            return maritalList;
        }

    } // end of class
}