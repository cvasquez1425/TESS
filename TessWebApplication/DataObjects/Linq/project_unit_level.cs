using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Greenspoon.Tess.Classes;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class project_unit_level
    {
        internal static List<DropDownItem> GetProjectUnitLevels(bool AllowCache = true)
        {
            string strCacheKey = "ProjectUnitLevelDropDownList";
            // if cache allowed and already exist in cache. return list.
            if ((AllowCache == true) && (HttpContext.Current.Cache[strCacheKey] != null))
            {
                return (List<DropDownItem>)HttpContext.Current.Cache[strCacheKey];
            }

            var unitlevelList = new List<DropDownItem>()
            {
                new DropDownItem { Name = "Upper",  Value = "Upper"},
                new DropDownItem { Name = "Lower",  Value = "Lower"},
                new DropDownItem { Name = "Both",   Value = "Both"}
            };

            if (unitlevelList.Any() == true)
            {
                unitlevelList.Insert(0, new DropDownItem());
            }
            
            // if cache allowed and not in the cache, add to cache.
            if ((AllowCache == true) && (HttpContext.Current.Cache[strCacheKey] == null))
            {
                HttpContext.Current
                    .Cache.Insert(strCacheKey, unitlevelList, null, DateTime.Now.AddHours(9), TimeSpan.Zero);
            }
            return unitlevelList;
        }
    }
}