#region Include
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
#endregion
namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class pgc_tmplt_stamp_sd
    {
        internal static Expression<Func<pgc_tmplt_stamp_sd, bool>> EqualsToStampId(int stampId)
        {
            return s => s.pgc_tmplt_stamp_sd_id == stampId;
        }
        internal static IList<pgc_tmplt_stamp_sd> GetAllRecords()
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_stamp_sd.ToList();
            }
        }
        internal static pgc_tmplt_stamp_sd GetStampSD(int stampId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_stamp_sd.SingleOrDefault(EqualsToStampId(stampId));
            }
        }
        internal static bool Save(pgc_tmplt_stamp_sd param)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var s = param.pgc_tmplt_stamp_sd_id > 0
                    ? ctx.pgc_tmplt_stamp_sd.SingleOrDefault(EqualsToStampId(param.pgc_tmplt_stamp_sd_id))
                    : new pgc_tmplt_stamp_sd();
                s.project_group_id      = param.project_group_id;
                s.flat_rate             = param.flat_rate;
                s.calc_multiplier_table = param.calc_multiplier_table;
                s.calc_multiplier_field = param.calc_multiplier_field;
                s.field_type1           = param.field_type1;
                s.calc_exec_table1      = param.calc_exec_table1;
                s.calc_exec_field1      = param.calc_exec_field1;
                s.calc_exec_divisor1    = param.calc_exec_divisor1;
                s.calc_exec_rounding1   = param.calc_exec_rounding1;
                s.calc_exec_table2      = param.calc_exec_table2;
                s.calc_exec_field2      = param.calc_exec_field2;
                s.calc_exec_divisor2    = param.calc_exec_divisor2;
                s.calc_exec_rounding2   = param.calc_exec_rounding2;
                s.calc_exec_table3      = param.calc_exec_table3;
                s.calc_exec_field3      = param.calc_exec_field3;
                s.calc_exec_divisor3    = param.calc_exec_divisor3;
                s.calc_exec_rounding3   = param.calc_exec_rounding3;
                if (param.pgc_tmplt_stamp_sd_id == 0) {
                    ctx.pgc_tmplt_stamp_sd.AddObject(s);
                }
                return ctx.SaveChanges() > 0;
            }
        }
    }
}