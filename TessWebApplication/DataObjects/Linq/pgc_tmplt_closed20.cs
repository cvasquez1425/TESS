#region Include
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class pgc_tmplt_closed20
    {
        internal static Expression<Func<pgc_tmplt_closed20, bool>> EqualsToColsed20Id(int closed20Id)
        {
            return c => c.pgc_tmplt_closed20_id == closed20Id;
        }
        internal static List<pgc_tmplt_closed20> GetAllRecords()
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_closed20.ToList();
            }
        }
        internal static pgc_tmplt_closed20 GetClosed20(int closed20Id)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_closed20
                    .SingleOrDefault(EqualsToColsed20Id(closed20Id));
            }
        }
        internal static bool Save(pgc_tmplt_closed20 param)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var c = param.pgc_tmplt_closed20_id > 0
                ? ctx.pgc_tmplt_closed20
                     .SingleOrDefault(EqualsToColsed20Id(param.pgc_tmplt_closed20_id))
                : new pgc_tmplt_closed20();
                c.project_group_id      = param.project_group_id;
                c.criteria_basis_table  = param.criteria_basis_table;
                c.criteria_basis_field  = param.criteria_basis_field;
                c.criteria_start_amount = param.criteria_start_amount;
                c.criteria_end_amount   = param.criteria_end_amount;
                c.include               = param.include;
                c.calc_basis_table      = param.calc_basis_table;
                c.calc_basis_field      = param.calc_basis_field;
                c.project_field_type    = param.project_field_type;
                c.flat_amt              = param.flat_amt;

                if (param.pgc_tmplt_closed20_id == 0) {
                    ctx.AddTopgc_tmplt_closed20(c);
                }
                return ctx.SaveChanges() > 0;
            };
        }
    } // end of class
}