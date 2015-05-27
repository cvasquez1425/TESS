#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions; 
#endregion
namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class title_company
    {
        internal static Expression<Func<title_company, bool>> EqualsToTitleCompanyId(int titleCompanyId) {
            return t => t.title_company_id == titleCompanyId;
        }

        internal static List<title_company> GetTitleCompany() {
            using(var ctx = DataContextFactory.CreateContext()) {
                var list = (from t in ctx.title_company
                            orderby t.title_company_id descending
                            select t).ToList<title_company>();
                return list;
            }
        }

        internal static title_company GetTitleCompany(int titleCompanyId) {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.title_company
                          .SingleOrDefault(EqualsToTitleCompanyId(titleCompanyId));
            }
        }
        internal static bool Save(title_company param){
            using(var ctx = DataContextFactory.CreateContext()) {
                var instance = param.title_company_id > 0 
                              ? ctx.title_company
                                   .SingleOrDefault(EqualsToTitleCompanyId(param.title_company_id)) 
                              : new title_company();
                if(instance != null) {
                    instance.pol_prefix                = param.pol_prefix;
                    instance.title_company_name        = param.title_company_name;
                    instance.title_company_active      = param.title_company_active;
                    instance.createdby                 = param.createdby;
                    instance.createddate               = param.createddate;
                }
                // If insert mode then add to the table.
                if(param.title_company_id == 0) {
                    ctx.AddTotitle_company(instance);
                }

                var result = false;
                if(ctx.SaveChanges() > 0) {
                    result = true;
                }
                // return if Save is successful or not.
                return result;
            }// end of context
        }
    }
}