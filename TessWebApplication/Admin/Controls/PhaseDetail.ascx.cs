#region Includes
using System;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Controls
{
    public partial class PhaseDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e) {
            var data = phase_detail.GetPhaseDetails();
            gvData.DataSource = data;
            gvData.DataBind();
        }
    }
}