#region Include
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System; 
#endregion
namespace Greenspoon.Tess.DataObjects.Linq {
    public partial class pgc_tmplt_premium {
        internal static Expression<Func<pgc_tmplt_premium, bool>> EqualsToPremiumId(int premiumId) {
            return p => p.pgc_tmplt_premium_id == premiumId;
        }
        internal static List<pgc_tmplt_premium> GetAllRecords() {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_premium.ToList();
            }           
        }
        internal static pgc_tmplt_premium GetPremium(int premiumId) {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_premium
                          .SingleOrDefault(EqualsToPremiumId(premiumId));
            }
        }
        internal static bool Save(pgc_tmplt_premium param) {
            using(var ctx = DataContextFactory.CreateContext()) {
                var p = param.pgc_tmplt_premium_id > 0
                    ? ctx.pgc_tmplt_premium.SingleOrDefault(EqualsToPremiumId(param.pgc_tmplt_premium_id))
                    : new pgc_tmplt_premium();

                p.project_group_id       = param.project_group_id;
                p.calc_basis_field       = param.calc_basis_field;
                p.calc_basis_table       = param.calc_basis_table;
                p.basis_field_multiplier = param.basis_field_multiplier;
                p.criteria_start_amount  = param.criteria_start_amount;
                p.criteria_end_amount    = param.criteria_end_amount;
                p.calc_type              = param.calc_type;
                p.calc_exec_table        = param.calc_exec_table;
                p.calc_exec_field        = param.calc_exec_field;
                p.flat_amt               = param.flat_amt;

                if(param.pgc_tmplt_premium_id == 0) {
                    ctx.AddTopgc_tmplt_premium(p);
                }
               return ctx.SaveChanges() > 0;
            }
        }
    } // end of class
}