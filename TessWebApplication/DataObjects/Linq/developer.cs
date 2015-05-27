#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
#endregion
namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class developer
    {
        internal static Expression<Func<developer, bool>> EqualsToDeveloperId(int developerId)
        {
            return d => d.developer_id == developerId;
        }
        internal static developer GetDeveloper(int developerId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.developers.SingleOrDefault(EqualsToDeveloperId(developerId));
            }
        } // end of get developer
        internal static List<developer> GetDevelopers()
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.developers.ToList();
            }
        }
        internal static bool Save(developer arg)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var d = arg.developer_id > 0 
                              ? ctx.developers.SingleOrDefault(EqualsToDeveloperId(arg.developer_id)) 
                              : new developer();
                if (d != null) {
                    d.developer_group_id = arg.developer_group_id;
                    d.developer_master_id = arg.developer_master_id;
                    d.active          = arg.active;
                    d.address1        = arg.address1;
                    d.address2        = arg.address2;
                    d.address3        = arg.address3;
                    d.alt_address1    = arg.alt_address1;
                    d.alt_address2    = arg.alt_address2;
                    d.alt_address3    = arg.alt_address3;
                    d.billing_atty    = arg.billing_atty;
                    d.developer_group = arg.developer_group;
                    d.developer_name  = arg.developer_name;
                    d.developer_pg2   = arg.developer_pg2;
                    d.developer_txt   = arg.developer_txt;
                    d.intro_atty      = arg.intro_atty;
                    d.reassign        = arg.reassign;
                }
                // If insert mode then add to the table.
                if (arg.developer_id  == 0) {
                    d.createdby       = arg.createdby;
                    d.createddate     = DateTime.Now;
                    // Add instance to context.
                    ctx.AddTodevelopers(d);
                }
                return ctx.SaveChanges() > 0;
            } // end of ctx
        } // end of save
    } // end of class
}