#region Includes
using System;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Controls
{
    public partial class TitleCompany : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e) {
            var data = title_company.GetTitleCompany();
            gvData.DataSource = data;
            gvData.DataBind();
        }
    }
}