using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;

namespace Greenspoon.Tess.Controls
{
    public partial class Status : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e) {
            // if there is a valid contract id. load data.
            if (ContractID <= 0) return;
            // Bind the status grid.
            var statusList      = 
                status.GetStatusUIList(FormName, ContractID);
            gvStatus.DataSource = statusList;
            gvStatus.DataBind();
            // Create the add new link.
            btnNewStatus.HRef    = 
                string.Format("~/Pages/status.aspx?a=n&cid={0}&form={1}&id={2}&TB_iframe=true&height=600&width=550", ContractID, FormName, RecID);  // Due to RIQ-303 RecID was added
            btnNewStatus.Visible = true;
            btnNewStatus.Title = string.Format("Add Status to MasterID {0}", ContractID );  // RIQ-255 Need Master ID (Contract_id) on top of Contract Status Form

            btnExpandedView.HRef = 
                string.Format("~/Pages/StatusExpandedView.aspx?a=v&cid={0}&form={1}&id={2}", ContractID, FormName, RecID);
            btnExpandedView.Visible = true;

            btnNewStatusB.HRef    =
                string.Format("~/Pages/status.aspx?a=n&cid={0}&form={1}&id={2}&TB_iframe=true&height=600&width=550", ContractID, FormName, RecID);  // Due to RIQ-303 RecID was added
            btnNewStatusB.Visible = true;
            btnNewStatusB.Title = string.Format("Add Status to MasterID {0}", ContractID); // RIQ-255 Need Master ID (Contract_id) on top of Contract Status Form

            btnExpandedViewB.HRef = 
                string.Format("~/Pages/StatusExpandedView.aspx?a=v&cid={0}&form={1}&id={2}", ContractID, FormName, RecID);
            btnExpandedViewB.Visible = true;
        }

        #region Properties
        public int ContractID {
            get {
                return ViewState["ContractID"] != null 
                    ? int.Parse(ViewState["ContractID"].ToString())
                    : 0;
            }
            set {
                ViewState["ContractID"] = value;
            }
        }
        public FormNameEnum FormName {
            get {
                return ViewState["FormName"] != null 
                    ? (FormNameEnum)ViewState["FormName"]
                    : FormNameEnum.Unknown;
            }
            set {
                ViewState["FormName"] = value;
            }
        }
        /// <summary>
        /// This ID is used to identity the parent form ID.
        /// For example, If parent form is Batch Escrow.
        /// then it will be batch escrow id.
        /// </summary>
        public int RecID {
            get {
                return ViewState["RecID"] != null 
                    ? int.Parse(ViewState["RecID"].ToString())
                    : 0;
            }
            set {
                ViewState["RecID"] = value;
            }
        }
        #endregion

    } //  end of class.
}