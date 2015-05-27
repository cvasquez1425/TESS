#region Includes
using System;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Controls
{
    public partial class Vesting1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e) {
            var data = vesting.GetVestings();
            gvData.DataSource = data;
            gvData.DataBind();
        }
    }
}