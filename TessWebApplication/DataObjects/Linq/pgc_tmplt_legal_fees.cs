#region Include
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
#endregion
namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class pgc_tmplt_legal_fees
    {
        internal static Expression<Func<pgc_tmplt_legal_fees, bool>> EqualstoLegalFeeId(int legalFeeId)
        {
            return l => l.pgc_tmplt_legal_fees_id == legalFeeId;
        }
        internal static IList<pgc_tmplt_legal_fees> GetAllRecords()
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_legal_fees.ToList();
            }
        }
        internal static pgc_tmplt_legal_fees GetLegalFee(int legalFeeId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_legal_fees.SingleOrDefault(EqualstoLegalFeeId(legalFeeId));
            }
        }
        internal static bool Save(pgc_tmplt_legal_fees param)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var l = param.pgc_tmplt_legal_fees_id > 0
                    ? ctx.pgc_tmplt_legal_fees.SingleOrDefault(EqualstoLegalFeeId(param.pgc_tmplt_legal_fees_id))
                    : new pgc_tmplt_legal_fees();

                l.project_group_id      = param.project_group_id;
                l.calc_field            = param.calc_field;
                l.calc_basis_table      = param.calc_basis_table;
                l.calc_basis_field      = param.calc_basis_field;
                l.project_field_type    = param.project_field_type;
                l.calc_multiplier_table = param.calc_multiplier_table;
                l.calc_multiplier_field = param.calc_multiplier_field;
                l.rounding              = param.rounding;
                l.include_rounding_calc = param.include_rounding_calc;

                if (param.pgc_tmplt_legal_fees_id == 0) {
                    ctx.pgc_tmplt_legal_fees.AddObject(l);
                }
                return ctx.SaveChanges() > 0;
            }
        }
    }
}