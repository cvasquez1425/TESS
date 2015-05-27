#region Include
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
#endregion

namespace Greenspoon.Tess.DataObjects.Linq {
    public partial class pgc_tmplt_stamp_sn {
        internal static Expression<Func<pgc_tmplt_stamp_sn, bool>> EqualsToStampSNId(int stampSNId) {
            return s => s.pgc_tmplt_stamp_sn_id == stampSNId;
        }
        internal static List<pgc_tmplt_stamp_sn> GetAllRecords() {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_stamp_sn.ToList();
            }
        }
        internal static pgc_tmplt_stamp_sn GetStampSN(int stampSNId) {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_stamp_sn
                          .SingleOrDefault(EqualsToStampSNId(stampSNId));
            }
        }
        internal static bool Save(pgc_tmplt_stamp_sn param) {
            using(var ctx = DataContextFactory.CreateContext()) {
                var s = param.pgc_tmplt_stamp_sn_id > 0
                    ? ctx.pgc_tmplt_stamp_sn
                         .SingleOrDefault(EqualsToStampSNId(param.pgc_tmplt_stamp_sn_id))
                : new pgc_tmplt_stamp_sn();
                s.project_group_id	    = param.project_group_id;
                s.flat_rate       	    = param.flat_rate;
                s.calc_multiplier_table	= param.calc_multiplier_table;
                s.calc_multiplier_field	= param.calc_multiplier_field;
                s.field_type1	        = param.field_type1;
                s.calc_exec_table1	    = param.calc_exec_table1;
                s.calc_exec_field1	    = param.calc_exec_field1;
                s.calc_exec_divisor1	= param.calc_exec_divisor1;
                s.calc_exec_rounding1	= param.calc_exec_rounding1;

                if(param.pgc_tmplt_stamp_sn_id == 0) {
                    ctx.AddTopgc_tmplt_stamp_sn(s);
                }
                var result = false;
                if(ctx.SaveChanges() > 0) {
                    result = true;
                }
                return result;

            }
        }

    } // end of class
}