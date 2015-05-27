using System;

namespace Greenspoon.Tess.Admin.Controls {
    public partial class pgc_tmplt_legal_fees : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            var list = Greenspoon.Tess.DataObjects.Linq
                .pgc_tmplt_legal_fees.GetAllRecords();
            gvData.DataSource = list;
            gvData.DataBind();
        }
    }
}