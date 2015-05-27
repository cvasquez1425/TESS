using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Greenspoon.Tess.Classes;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class cont_week_master_single
    {
        //readonly static string _strCacheKey = "ContractWeekDropDownList";
        internal static Tuple<string, string> GetSingleWeekList(int weekId)
        {
            //if (CacheAllowedAndCurrentCacheIsNotEmpty(AllowCache))
            //{
            //    return CachedWeekList();
            //}

            string weekID = weekId.ToString();
            string weekList = string.Empty;
            Tuple<string, string> weekT = new Tuple<string, string>("", "0");
            using (var ctx = DataContextFactory.CreateContext())
            {
                var weeks = (from w in ctx.cont_week_master
                             where w.week_number == weekID
                             orderby w.cont_week_master_id
                             select new
                             {
                                 Name = w.week_number,
                                 Value = w.cont_week_master_id
                             }).FirstOrDefault();    
                //if (weeks.ToString().Any()) { weekList = weeks.Value.ToString(); }
                //if (CacheAllowedAndCurrentCacheIsEmpty(AllowCache))
                //{
                //    Tuple<string, string> weekCache = Tuple.Create(weeks.Name, weeks.Value.ToString());                    
                //    InsertWeekListToCurrentCache(weekCache);
                //}

                if (weeks.ToString().Any()) { return Tuple.Create(weeks.Name, weeks.Value.ToString()); }
            }
            //return weekList;
            return weekT;
        }

        internal static string GetWeekNumber(int weekId)
        {
            string weekList = string.Empty;
            using (var ctx = DataContextFactory.CreateContext())
            {
                var weeks = (from w in ctx.cont_week_master
                             where w.cont_week_master_id == weekId
                             orderby w.cont_week_master_id
                             select new
                             {
                                 Name = w.week_number,
                                 Value = w.cont_week_master_id
                             }).FirstOrDefault();
                if (weeks.ToString().Any()) { weekList = weeks.Name; }
                return weekList;
            }
        }

        //private static void InsertWeekListToCurrentCache(Tuple<string, string> weekList)
        //{
        //    HttpContext.Current
        //        .Cache.Insert(_strCacheKey, weekList, null, DateTime.Now.AddHours(9), TimeSpan.Zero);
        //}
        //private static bool CacheAllowedAndCurrentCacheIsEmpty(bool AllowCache)
        //{
        //    return (AllowCache == true) && (HttpContext.Current.Cache[_strCacheKey] == null);
        //}
        //static Tuple<string, string> CachedWeekList()
        //{
        //    return (Tuple<string, string>)HttpContext.Current.Cache[_strCacheKey];
        //}
        //static bool CacheAllowedAndCurrentCacheIsNotEmpty(bool AllowCache)
        //{
        //    return (AllowCache == true) && (HttpContext.Current.Cache[_strCacheKey] != null);
        //}
    }  // end of class
}