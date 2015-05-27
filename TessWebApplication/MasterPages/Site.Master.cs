using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;

namespace Greenspoon.Tess.MasterPages {
    public partial class SiteMaster : MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
        }
       
        protected void DoSearch(object sender, EventArgs e) {
            Response.Redirect(string.Format("~/Pages/Search.aspx?c={0}&key={1}", GetSearchContext(), searchField.Value));
        }
     
        private string GetSearchContext()
        {
            //  Change default search criteria from Master ID to Dev K Number. Help desk 24137
//            return searchContext.SelectedIndex > 0 ? searchContext.SelectedValue : "mi";
            return searchContext.SelectedIndex >= 0 ? searchContext.SelectedValue : "dk";
        }

        protected void StatusUpdateClick(object sender, EventArgs e)
        {
            var lb = sender as LinkButton;
            if (lb == null) return;
            var drpMaster =   Page.GetControl<DropDownList>("drpMasterId");
            var cid = drpMaster != null
                          ? ( drpMaster.SelectedIndex > 0 ? Convert.ToInt32(drpMaster.SelectedValue) : 0 )
                          : Request.QueryString.GetValue<int>("cid");

            var tempBid = Request.QueryString.GetValue<int>("beid");
            var bid = ( tempBid == 0 && ( cid > 0 ) ) 
                          ? batch_escrow.GetBatchEscrowIdByContractId(cid) 
                          : tempBid;
            var url =  string.Format("~/Admin/Pages/UpdateByEscrowKey.aspx?&bid={0}", bid.ToString(CultureInfo.InvariantCulture));
            Response.Redirect(url);
        }

        protected void ReportsClick(object sender, EventArgs e) 
        {
            var lb = sender as LinkButton;
            if (lb == null) return;
            var drpMaster =   Page.GetControl<DropDownList>("drpMasterId");
            int bid;
            var cid = drpMaster != null
                          ? (drpMaster.SelectedIndex > 0 ? Convert.ToInt32(drpMaster.SelectedValue) : 0)
                          : Request.QueryString.GetValue<int>("cid");

            var form = lb.ID.GetFormName<FormNameEnum>();
            switch(form) {
                case FormNameEnum.BatchEscrow:
                    var tempBid = Request.QueryString.GetValue<int>("beid");
                    bid = (tempBid == 0 && (cid > 0)) 
                        ? batch_escrow.GetBatchEscrowIdByContractId(cid) 
                        : tempBid;
                    break;
                case FormNameEnum.Cancel:
                    bid =  Request.QueryString.GetValue<int>("bcid");
                    break;
                case FormNameEnum.Foreclosure:
                    bid = Request.QueryString.GetValue<int>("bfid");
                    break;
                default:
                    bid = 0;
                    break;
            }
            var url =  string.Format("~/Pages/CrystalReports.aspx?form={0}&bid={1}&cid={2}", form, bid.ToString(CultureInfo.InvariantCulture), cid.ToString(CultureInfo.InvariantCulture));
            Response.Redirect(url);
        }
        protected void AnalyticClick(object sender, EventArgs e)
        {
            var link = system_values.AnalyticalReportsLink(1);
            ResponseHelper.Redirect(link.ToString(), "_blank", "menubar=0,width=1200,height=700");  // TESS 01-28-2014
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", string.Format("window.open('{0}')", link), true);
        }

        protected void BulkCancelStatusUpdateClick(object sender, EventArgs e)
        {
            var bcid =  Request.QueryString.GetValue<int>("bcid");
            var url = string.Format("~/Admin/Pages/CancelStatusUpdate.aspx?bcid={0}", bcid);
            Response.Redirect(url);
        }

        protected void BulkRecordingInfoUpdateClick(object sender, EventArgs e)
        {
            var beid =  Request.QueryString.GetValue<int>("beid");
            var url = string.Format("~/Admin/Pages/RecordInfoUpdate.aspx?beid={0}", beid);
            Response.Redirect(url);
        }

        // Myrtle Beach Bulk Update
        protected void BulkUpdateUploadMyrtle(object sender, EventArgs e)
        {
            var beid = Request.QueryString.GetValue<int>("beid");
            var url = string.Format("~/Admin/Pages/RecordPolicyUpload.aspx?beid={0}", beid);
            Response.Redirect(url);
        }

        // Status Code screen [4] for Escrow Keys with individual comments? - /Admin/Pages/BulkCommentsByEscrowKeyUpdate.aspx 
        protected void BulkUpdateCommentsByEscrowKey(object sender, EventArgs e)
        {
            var url = string.Format("~/Admin/Pages/BulkCommentsByEscrowKeyUpdate.aspx");
            Response.Redirect(url);
        }

    } // end of class
}
