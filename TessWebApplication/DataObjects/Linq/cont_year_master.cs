using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Greenspoon.Tess.Classes;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class cont_year_master
    {
        internal static Expression<Func<cont_year_master, bool>> EqualsToYearId(int yearId)
        {
            return y => y.cont_year_master_id == yearId;
        }
        internal static IList<cont_year_master> GetAllRecords()
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.cont_year_master.ToList();
            }
        }
        internal static cont_year_master GetYear(int yearId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.cont_year_master.SingleOrDefault(EqualsToYearId(yearId));
            }
        }
        internal static List<DropDownItem> GetContractYearList(bool AllowCache = true)
        {
            const string _strCacheKey = "ContractYearDropDownList";
            if (AllowCache && ( HttpContext.Current.Cache[_strCacheKey] != null )) {
                return (List<DropDownItem>)HttpContext.Current.Cache[_strCacheKey];
            }
            var yearList = new List<DropDownItem>();
            using (var ctx = DataContextFactory.CreateContext()) {
                var years = ( from y in ctx.cont_year_master
                              orderby y.year_name ascending
                              select new {
                                  Name = y.year_name,
                                  Value = y.cont_year_master_id
                              } ).ToList();
                yearList.AddRange(years.Select(item => new DropDownItem
                                                           {
                                                               Name = item.Name, Value = item.Value.ToString()
                                                           }));
                if (AllowCache && ( HttpContext.Current.Cache[_strCacheKey] == null )) {
                    HttpContext.Current
                        .Cache.Insert(_strCacheKey, yearList, null, DateTime.Now.AddHours(9), TimeSpan.Zero);
                }
                yearList.Insert(0, new DropDownItem());
            }
            return yearList;
        }
        internal static bool Save(cont_year_master param)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var y =  param.cont_year_master_id > 0
                   ? ctx.cont_year_master.SingleOrDefault(EqualsToYearId(param.cont_year_master_id))
                   : new cont_year_master();
                if (y != null) {
                    y.year_name = param.year_name;
                    y.year_active = param.year_active;
                    y.a_b_t = param.a_b_t;
                    y.year_name_abbrev = param.year_name_abbrev;
                    y.year_active = param.year_active;
                }
                if (param.cont_year_master_id == 0) {
                    y.createdby = param.createdby;
                    y.createddate = DateTime.Now;
                    ctx.cont_year_master.AddObject(y);
                }
                return ctx.SaveChanges() > 0;
            }
        }

        public static DateTime? DataTime { get; set; }
    } // end of class
}