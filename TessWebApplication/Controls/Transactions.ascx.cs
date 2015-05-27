#region Includes
using System;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Classes;
#endregion
namespace Greenspoon.Tess.Controls {
    public partial class Transactions : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e)
        {
           // return;
            // if there is a valid contract id. load data.
            //if(ContractID > 0) {
            //    var transList = contract_amount.GetTransactionUIList(ContractID);
            //    gvTransactions.DataSource = transList;
            //    gvTransactions.DataBind();

            //    // If user is allowed to add.
            //    // new Transaction, Activate Add Link.
            //    if(this.CanCreate == true) {
            //        DisplayAddNewInventoryLink();
            //    }
            //}

        } // end page load.

        /// <summary>
        /// Activate the Add new button.
        /// </summary>
        void DisplayAddNewInventoryLink() {
            btnShowPopupTran.HRef = 
                        string.Format("~/Pages/Transactions.aspx?a=n&id={0}&TB_iframe=true&height=200&width=400", this.ContractID);
            btnShowPopupTran.Visible = true;
        }
        #region Properties
        public int ContractID {
            get {
                return ViewState["ContractID"] == null ? 0 
                    : int.Parse(ViewState["ContractID"].ToString());
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
        public bool CanEdit {
            get {
                return this.FormName.isBatchEscrow();
            }
        }
        public bool CanCreate {
            get {
                return this.FormName.isBatchEscrow();
            }
        }
        #endregion
    }
}