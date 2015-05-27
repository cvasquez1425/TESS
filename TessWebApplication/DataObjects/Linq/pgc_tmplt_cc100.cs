#region Include
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class pgc_tmplt_cc100
    {
        internal static Expression<Func<pgc_tmplt_cc100, bool>> EqualsToCC100Id(int cc100Id)
        {
            return c => c.pgc_tmplt_cc100_id == cc100Id;
        }
        internal static List<pgc_tmplt_cc100> GetAllRecords()
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_cc100.ToList();
            }
        }
        internal static pgc_tmplt_cc100 GetCC100(int cc100Id)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.pgc_tmplt_cc100
                    .SingleOrDefault(EqualsToCC100Id(cc100Id));
            }
        }
        internal static bool Save(pgc_tmplt_cc100 param)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var c = param.pgc_tmplt_cc100_id > 0
                    ? ctx.pgc_tmplt_cc100
                    .SingleOrDefault(EqualsToCC100Id(param.pgc_tmplt_cc100_id))
                    : new pgc_tmplt_cc100();

                c.criteria_basis_table	=	param.criteria_basis_table;
                c.criteria_basis_field	=	param.criteria_basis_field;
                c.criteria_start_amount	=	param.criteria_start_amount;
                c.criteria_end_amount	=	param.criteria_end_amount;
                c.include	            =	param.include;
                c.percentage	        =	param.percentage;
                c.flat_rate	            =	param.flat_rate;
                c.calc_basis_table	    =	param.calc_basis_table;
                c.calc_basis_field	    =	param.calc_basis_field;
                c.rounding	            =	param.rounding;
                c.project_group_id	    =	param.project_group_id;
                c.include_rounding_calc	=	param.include_rounding_calc;
                c.effective_date_from   =   param.effective_date_from;
                c.effective_date_to     =   param.effective_date_to;

                if (param.pgc_tmplt_cc100_id == 0) {
                    ctx.AddTopgc_tmplt_cc100(c);
                }
                return ctx.SaveChanges() > 0;
            }
        }
    }  
}