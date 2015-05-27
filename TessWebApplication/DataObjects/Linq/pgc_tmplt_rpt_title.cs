#region Include
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
#endregion
namespace Greenspoon.Tess.DataObjects.Linq {
    public partial class pgc_tmplt_rpt_title {
        internal static Expression<Func<pgc_tmplt_rpt_title, bool>> EqualsToRptTitleId(int rptTitleId) {
            return r => r.pgc_tmplt_rpt_title_id == rptTitleId;
        }
        internal static List<pgc_tmplt_rpt_title> GetAllRecords() {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_rpt_title.ToList();
            }
        }
        internal static pgc_tmplt_rpt_title GetRptTitle(int rptTitleId) {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_rpt_title
                    .SingleOrDefault(EqualsToRptTitleId(rptTitleId));
            }
        }
        internal static bool Save(pgc_tmplt_rpt_title param) {
            using(var ctx = DataContextFactory.CreateContext()) {
                var r = param.pgc_tmplt_rpt_title_id > 0
                ? ctx.pgc_tmplt_rpt_title
                    .SingleOrDefault(EqualsToRptTitleId(param.pgc_tmplt_rpt_title_id))
                : new pgc_tmplt_rpt_title();

                r.project_group_id	    = param.project_group_id;
                r.calc_field	        = param.calc_field;
                r.criteria_start_amount	= param.criteria_start_amount;
                r.criteria_end_amount	= param.criteria_end_amount;
                r.field_type	        = param.field_type;
                r.calc_value	        = param.calc_value;
                r.flat_value	        = param.flat_value;
                r.use_base	            = param.use_base;
                r.calc_basis	        = param.calc_basis;

                if(param.pgc_tmplt_rpt_title_id == 0) {
                    ctx.AddTopgc_tmplt_rpt_title(r);
                }
                var result = false;
                if(ctx.SaveChanges() > 0) {
                    result = true;
                }
                return result;
            }

        }
    }  // end of class
}