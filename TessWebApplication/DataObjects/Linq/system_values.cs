using System.Linq;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class system_values
    {
        internal static string AnalyticalReportsLink(int id)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.system_values.SingleOrDefault(s => s.system_values_id == id).system_values_info;
            }
        }
    }
}