using System;
using Greenspoon.Tess.DataObjects.Linq;

namespace Greenspoon.Tess.Admin.Controls
{
    public partial class Developer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var data = developer.GetDevelopers();
            gvData.DataSource = data;
            gvData.DataBind();
        }
    }
}