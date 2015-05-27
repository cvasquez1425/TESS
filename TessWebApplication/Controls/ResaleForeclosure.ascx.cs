using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;

namespace Greenspoon.Tess.Controls
{
    public partial class ResaleForeclosure : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if there is a valid contract id. load data.
            if (ContractID <= 0) return;
            // Bind the status grid.
            var trsList =
                trs.GetResaleForeclosureUIList(FormName, ContractID);
            gvResale.DataSource = trsList;
            gvResale.DataBind();

            btnExpandedView.HRef =
                string.Format("~/Pages/TrsExpandedView.aspx?a=v&cid={0}&form={1}&id={2}", ContractID, FormName, RecID);
            btnExpandedView.Visible = true;

            //btnExpandedViewB.HRef =
            //    string.Format("~/Pages/StatusExpandedView.aspx?a=v&cid={0}&form={1}&id={2}", ContractID, FormName, RecID);
            //btnExpandedViewB.Visible = true;

        }

        #region Properties
        public int ContractID
        {
            get
            {
                return ViewState["ContractID"] != null
                    ? int.Parse(ViewState["ContractID"].ToString())
                    : 0;
            }
            set
            {
                ViewState["ContractID"] = value;
            }
        }
        public FormNameEnum FormName
        {
            get
            {
                return ViewState["FormName"] != null
                    ? (FormNameEnum)ViewState["FormName"]
                    : FormNameEnum.Unknown;
            }
            set
            {
                ViewState["FormName"] = value;
            }
        }
        /// <summary>
        /// This ID is used to identity the parent form ID.
        /// For example, If parent form is Batch Escrow.
        /// then it will be batch escrow id.
        /// </summary>
        public int RecID
        {
            get
            {
                return ViewState["RecID"] != null
                    ? int.Parse(ViewState["RecID"].ToString())
                    : 0;
            }
            set
            {
                ViewState["RecID"] = value;
            }
        }
        #endregion
    }
}