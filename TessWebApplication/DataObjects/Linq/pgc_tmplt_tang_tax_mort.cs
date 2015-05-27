#region Include
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
#endregion
namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class pgc_tmplt_tang_tax_mort
    {
        internal static Expression<Func<pgc_tmplt_tang_tax_mort, bool>> EqualsToMortId(int mortId)
        {
            return m => m.pgc_tmplt_tang_tax_mort_id == mortId;
        }
        internal static IList<pgc_tmplt_tang_tax_mort> GetAllRecords()
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_tang_tax_mort.ToList();
            }
        }
        internal static pgc_tmplt_tang_tax_mort GetMort(int mortId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_tang_tax_mort.SingleOrDefault(EqualsToMortId(mortId));
            }
        }
        internal static bool Save(pgc_tmplt_tang_tax_mort param)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var m = param.pgc_tmplt_tang_tax_mort_id > 0
                    ? ctx.pgc_tmplt_tang_tax_mort.SingleOrDefault(EqualsToMortId(param.pgc_tmplt_tang_tax_mort_id))
                    : new pgc_tmplt_tang_tax_mort();
                m.project_group_id = param.project_group_id;
                m.calc_basis_table = param.calc_basis_table;
                m.calc_basis_field = param.calc_basis_field;
                m.project_field_type = param.project_field_type;
                m.calc_multiplier_table = param.calc_multiplier_table;
                m.calc_multiplier_field = param.calc_multiplier_field;
                m.rounding = param.rounding;
                m.flat_rate = param.flat_rate;
                m.include_rounding_calc = param.include_rounding_calc;
                if (param.pgc_tmplt_tang_tax_mort_id  == 0) {
                    ctx.pgc_tmplt_tang_tax_mort.AddObject(m);
                }
                return ctx.SaveChanges() > 0;
            }
        }
    }
}