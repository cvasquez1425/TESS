using System;
namespace Greenspoon.Tess.Admin.Controls {
    public partial class pgc_tmplt_premium : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            var data = Greenspoon.Tess.DataObjects.Linq.
                         pgc_tmplt_premium.GetAllRecords();
            gvData.DataSource = data;
            gvData.DataBind();
        }
    }
}