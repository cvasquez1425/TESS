#region Includes
using System;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Controls
{
    public partial class StatusMaster : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e) {
            var data = status_master.GetStatusMasters();
            gvData.DataSource = data;
            gvData.DataBind();
        }
    }
}