#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class firm
    {
        internal static Expression<Func<firm, bool>> EqualsToFirmId(int firmId) {
            return f => f.firm_id == firmId;
        }

        internal static List<firm> GetFirms() {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.firms.ToList();
            }
        }

        internal static firm GetFirm(int firmId) {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.firms.SingleOrDefault(EqualsToFirmId(firmId));
            }
        }

        internal static bool Save(firm param) {
            using(var ctx = DataContextFactory.CreateContext()) {
                if(param.firm_id == 0) {
                    param.createddate = DateTime.Now;
                    ctx.AddTofirms(param);
                }
                else {
                    // Do Update.
                    var f = ctx.firms
                               .SingleOrDefault(EqualsToFirmId(param.firm_id));
                    if(f != null) {
                        f.firm_code        =	 param.firm_code;
                        f.firm_designation = 	 param.firm_designation;
                        f.firm_name        = 	 param.firm_name;
                        f.firm_address1    = 	 param.firm_address1;
                        f.firm_address2    = 	 param.firm_address2;
                        f.firm_replyto     = 	 param.firm_replyto;
                        f.firm_agent       = 	 param.firm_agent;
                        f.firm_agent_title = 	 param.firm_agent_title;
                        f.firm_bar_num     = 	 param.firm_bar_num;
                        f.firm_gender      = 	 param.firm_gender;
                        f.createdby        = 	 param.createdby;
                    }
                    else { return false; }
                }
                return ctx.SaveChanges() > 0;
            }
        }
    }// end of class
}