#region Includes
using System;
using System.Linq;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Controls
{
    public partial class Judge : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e) {
            using(var ctx = DataContextFactory.CreateContext()) {
                var data = (from j in ctx.judges
                            orderby j.judge_id descending
                            select new
                            {
                                judge_id        = j.judge_id,
                                CountyName      = j.county.county_name,
                                room            = j.room,
                                division        = j.division,
                                judge_name      = j.judge_name,
                                judge_last_name = j.judge_last_name,
                                phone           = j.phone,
                                judge_active    = j.judge_active,
                                DocumentGroup   = j.document_group.document_group_name,
                                createdby       = j.createdby,
                                createddate     = j.createddate
                            }).ToList();

                gvData.DataSource = data;
                gvData.DataBind();
            } // end of context
        }

    } // end of class
}