#region Includes
using System;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Classes;
using System.Linq;
using System.ComponentModel;
#endregion
namespace Greenspoon.Tess.Controls
{
    public partial class LegalNames : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e) {
            // if there is a valid contract id. load data.
            if (ContractID <= 0) return;
            var legalNameList       = ShowActiveOnly ?legal_name.GetLegalNameUIList(ContractID).Where(l => l.Active == true).ToList() 
                                               : legal_name.GetLegalNameUIList(ContractID).ToList();
            gvLegalNames.DataSource = legalNameList;
            gvLegalNames.DataBind();

            // Create the add new link.
            //if (FormName.isBatchEscrow() != true) return; RIQ-309 Cancel Form Improvements.
            btnNewLegalName.HRef    = 
                string.Format("~/Pages/LegalName.aspx?a=n&cid={0}&form={1}&TB_iframe=true&height=630&width=700", ContractID, FormName);
            btnNewLegalName.Visible = true;
            //RIQ-297  Expanded View
            btnExpandedView.HRef =
                    string.Format("~/Pages/InventoryExpandedView.aspx?a=v&cid={0}&form={1}&id={2}&ba=s", ContractID, FormName, RecID);
            btnExpandedView.Visible = true;
        }

        public void SetTallStyle()
        {
            divLN.Style.Clear();
            divLN.Attributes["class"] = "divLNTall";
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

        public bool ShowActiveOnly
        {
            get
            {
                return ViewState["ShowActiveOnly"] != null
                           ? (bool)ViewState["ShowActiveOnly"]
                           : false;
            }
            set
            {
                ViewState["ShowActiveOnly"] = value;
            }
        }

        public bool ReadOnly
        {
            get
            {
                return ViewState["ReadOnly"] != null
                           ? (bool)ViewState["ReadOnly"]
                           : false;
            }
            set
            {
                ViewState["ReadOnly"] = value;
            }
        }
        #endregion
    }
}