using System;
using Greenspoon.Tess.DataObjects.Linq;

namespace Greenspoon.Tess.Admin.Controls
{
    public partial class Year : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var data = cont_year_master.GetAllRecords();
            gvData.DataSource = data;
            gvData.DataBind();
        }
    }
}