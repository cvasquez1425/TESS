using System;
namespace Greenspoon.Tess.Admin.Controls {
    public partial class pgc_tmplt_rpt_title : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            var list = Greenspoon.Tess.DataObjects.Linq
                .pgc_tmplt_rpt_title.GetAllRecords();
            gvData.DataSource = list;
            gvData.DataBind();
        }
    }
}