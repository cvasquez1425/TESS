using System;
using System.Collections.Generic;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.BusinessObjects.UIObjects;

namespace Greenspoon.Tess.Controls
{
    public partial class CancelExtra : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e) {
            if (CancelID > 0)
            {
                List<CancelExtraDTO> uiList = cancel_extra.GetCancelExtraUIList(CancelID);
                gvCancelExtra.DataSource = uiList;
                gvCancelExtra.DataBind();
                RegisterNewForm();
            }
            else {
                btnShowCancelExtraAdd.Visible = false;
                btnShowCurrentValueAdd.Visible = false;      // May-2015 Update Current Value - Add button on the Batch Cancel screen to Update Current Value fields for the batch.
            }
        }

        void RegisterNewForm() {
            btnShowCancelExtraAdd.Visible = true;
            btnShowCancelExtraAdd.HRef = string.Format("~/Pages/CancelExtra.aspx?a=n&form=cancel&id={0}&TB_iframe=true&height=200&width=400", CancelID);
            // May-2015 Update Current Value - Add button on the Batch Cancel screen to Update Current Value fields for the batch.
            btnShowCurrentValueAdd.Visible = true;
            btnShowCurrentValueAdd.HRef = string.Format("~/Pages/CurrentValue.aspx?a=n&form=cancel&id={0}&TB_iframe=true&height=200&width=400", BatchCancelID);
        }

        #region Properties
        public int CancelID {
            get {
                return ViewState["CancelID"] == null ? 0
                    : int.Parse(ViewState["CancelID"].ToString());
            }
            set {
                ViewState["CancelID"] = value;
            }
        }
        // Update Current Value May 2015
        public int BatchCancelID
        {
            get
            {
                return ViewState["BatchCancelID"] == null ? 0
                    : int.Parse(ViewState["BatchCancelID"].ToString());
            }
            set
            {
                ViewState["BatchCancelID"] = value;
            }
        }
        #endregion
    } // end of class
}