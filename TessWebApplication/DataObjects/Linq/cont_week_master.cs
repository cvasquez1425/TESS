using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Greenspoon.Tess.Classes;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class cont_week_master
    {
        readonly static string _strCacheKey = "ContractWeekDropDownList";
        internal static List<DropDownItem> GetContractWeekList(bool AllowCache = true)
        {
            if (CacheAllowedAndCurrentCacheIsNotEmpty(AllowCache)) {
                return CachedWeekList();
            }
            var weekList = new List<DropDownItem>();
            using (var ctx = DataContextFactory.CreateContext()) {
                (from w in ctx.cont_week_master
                 orderby w.cont_week_master_id ascending
                 select new {
                     Name = w.week_number,
                     Value = w.cont_week_master_id
                 }).ToList()
                .ForEach(i => weekList.Add(new DropDownItem {
                    Name = i.Name, Value = i.Value.ToString() }));

                if (CacheAllowedAndCurrentCacheIsEmpty(AllowCache)) {
                    InsertWeekListToCurrentCache(weekList);
                }
                weekList.Insert(0, new DropDownItem());
            }
            return weekList;
        }

        private static void InsertWeekListToCurrentCache(List<DropDownItem> weekList)
        {
            HttpContext.Current
                .Cache.Insert(_strCacheKey, weekList, null, DateTime.Now.AddHours(9), TimeSpan.Zero);
        }
        private static bool CacheAllowedAndCurrentCacheIsEmpty(bool AllowCache)
        {
            return (AllowCache == true) && (HttpContext.Current.Cache[_strCacheKey] == null);
        }
        static List<DropDownItem> CachedWeekList()
        {
            return (List<DropDownItem>)HttpContext.Current.Cache[_strCacheKey];
        }
        static bool CacheAllowedAndCurrentCacheIsNotEmpty(bool AllowCache)
        {
            return (AllowCache == true) && (HttpContext.Current.Cache[_strCacheKey] != null);
        }
    } // end of class.
}