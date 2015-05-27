#region Includes
using System;
using System.Linq;
using Greenspoon.Tess.DataObjects.Linq;
#endregion
namespace Greenspoon.Tess.Admin.Controls
{
    public partial class Partner : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e) {
            using(var ctx = DataContextFactory.CreateContext()) {
                var data = (from p in ctx.partners
                            orderby p.partner_id descending
                            select new
                            {
                                PartnerId   = p.partner_id,
                                ProjectName = p.project.project_name,
                                PartnerName = p.partner_name,
                                CreatedBy   = p.createdby,
                                CreatedDate = p.createddate
                            }).ToList();

                gvData.DataSource = data;
                gvData.DataBind();
            }
        }
    }
}