#region Includes
using System;
using System.Linq;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Classes;
using System.ComponentModel;
#endregion
namespace Greenspoon.Tess.Controls
{
    public partial class Inventory : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if there is a valid contract id. load data.
            if (ContractID <= 0) return;
            var invList = contract_interval.GetInventoryUIList(ContractID);
            gvInventory.DataSource = invList;
            gvInventory.DataBind();

            //            if (CanCreate != true) return;  RIQ-309
            var defaultBuildingId = invList.Any() ? invList.First().InventoryBuildingId : "0";
            DisplayAddNewInventoryLink(defaultBuildingId);
            DisplayExpandedView();
        }

        void DisplayExpandedView()
        {
            // User ba param in url indicates a button action
            // this param will be used for user to return
            // and start as new form. "s" indicates return to existing contract.
            btnExpandedView.HRef = 
                    string.Format("~/Pages/InventoryExpandedView.aspx?a=v&cid={0}&form={1}&id={2}&ba=s", ContractID, FormName, RecID);
            btnExpandedView.Visible = true;
        } 

        void DisplayAddNewInventoryLink(string defaultBuildingId)
        {
            btnShowPopupInv.HRef = 
                        string.Format("~/Pages/Inventory.aspx?a=n&cid={0}&dbid={1}&TB_iframe=true&height=380&width=500", ContractID, defaultBuildingId);
            btnShowPopupInv.Visible = true;
        }

        #region Properties
        [Bindable(true)]
        public int ContractID
        {
            get
            {
                return ViewState["ContractID"] == null ? 0 
                    : int.Parse(ViewState["ContractID"].ToString());
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

        public bool CanEdit
        {
            get
            {
                return FormName.isBatchEscrow();
            }
        }

        public bool CanCreate
        {
            get
            {
                return FormName.isBatchEscrow();
            }
        }
        #endregion
    } 
} 