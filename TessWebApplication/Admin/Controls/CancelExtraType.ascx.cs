#region Includes
using System;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Controls
{
    public partial class CancelExtraType : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var data = cancel_extra_type.GetCancelExtraType();
            gvData.DataSource = data;
            gvData.DataBind();
        } // end of page load
    } // end of class
}