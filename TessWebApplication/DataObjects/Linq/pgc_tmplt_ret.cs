#region Include
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
#endregion
namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class pgc_tmplt_ret
    {
        internal static Expression<Func<pgc_tmplt_ret, bool>> EqualsToRetId(int retId)
        {
            return r => r.pgc_tmplt_ret_id == retId;
        }
        internal static IList<pgc_tmplt_ret> GetAllRecords()
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_ret.ToList();
            }
        }
        internal static pgc_tmplt_ret GetRet(int retId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_ret.SingleOrDefault(EqualsToRetId(retId));
            }
        }
        internal static bool Save(pgc_tmplt_ret param)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var r = param.pgc_tmplt_ret_id > 0
                    ? ctx.pgc_tmplt_ret.SingleOrDefault(EqualsToRetId(param.pgc_tmplt_ret_id))
                    : new pgc_tmplt_ret();
                r.project_group_id = param.project_group_id;
                r.calc_field       = param.calc_field;
                r.calc_type        = param.calc_type;
                r.basis_table      = param.basis_table;
                r.basis_field      = param.basis_field;
                r.calc_value       = param.calc_value;
                if (param.pgc_tmplt_ret_id == 0) {
                    ctx.pgc_tmplt_ret.AddObject(r);
                }
                try {
                    return ctx.SaveChanges() > 0;
                } catch { return false; }
            }
        }
    }
}