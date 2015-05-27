#region Includes
using System;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Controls
{
    public partial class County : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e) {
            var data = county.GetCounties();
            gvData.DataSource = data;
            gvData.DataBind();
        }
    } // end of class.
}