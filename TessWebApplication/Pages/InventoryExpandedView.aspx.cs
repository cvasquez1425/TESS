#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion
namespace Greenspoon.Tess.Pages {
    public partial class InventoryExpandedView : PageBase {
        protected void Page_Load(object sender, EventArgs e) {
            SetPageBase();
            SetupPage();
        }
        void Page_PreRender(object sender, EventArgs e) {
            // Get the contract id.
            // Get the contract id from the drop down.    
            var conId = ContractId;
            if (conId == 0) {
                return;
            }
            // Load Legal Name user control from control folder.
            // ------------------------------------------------
            // Get the legal names user control.
//            if (!FormName.isBatchEscrow()) return;             RIQ-309
            var usrConLegalNames = (Controls.LegalNames)LoadControl("~/Controls/LegalNames.ascx");
            // If control is found
            if (usrConLegalNames == null) return;
            // Set up contract ID property.
            usrConLegalNames.ContractID = conId;
            // Set up form name.
            usrConLegalNames.FormName = FormName;
            usrConLegalNames.SetTallStyle();
            // Load user control on the page.
            plcLegalNames.Controls.Add(usrConLegalNames);
        }
        void SetupPage() {
            if (ContractId <= 0) return;
            var invList = 
                contract_interval.GetInventoryUIList(ContractId);
            gvInventory.DataSource = invList;
            gvInventory.DataBind();
            // Display return path.
            if (Page.IsPostBack) return;
            btnReturn.HRef = GetReturnPath();
            // DisplayAddNewLegalNameLink();
            // Display add new link.
            DisplayAddNewInventoryLink();
        }

        string GetReturnPath() {
            string returnPath;
            string previousPath = Request.UrlReferrer.ToString();
            if (previousPath.IndexOf("StatusExpandedView.aspx") != -1)
            {
                RecID = Request.QueryString.GetValue<int>("id");  // StatusExpandedView.aspx?a=v&cid=406608&form=BatchEscrow&id=46086"
            }
            const string defaultPath = "~/Default.aspx";
            switch(FormName) {
                case FormNameEnum.BatchEscrow:
                    returnPath = RecID > 0 
                        // if button action is save then pass the contract id for return path.
                        // else contract id should be empty. 
                        ? string.Format("~/Pages/BatchEscrow.aspx?a=e&beid={0}&cid={1}", RecID, (ButtonAction == "s" 
                                                                                                          ? ContractId.ToString() 
                                                                                                          : string.Empty)) 
                        : previousPath;
                    break;
                case FormNameEnum.Cancel:
                    returnPath = RecID > 0 
                        ? string.Format("~/Pages/BatchCancel.aspx?a=e&bcid={0}&cid={1}", RecID, ContractId) 
                        : previousPath;
                    break;
                case FormNameEnum.Foreclosure:
                    returnPath = RecID > 0 
                        ? string.Format("~/Pages/foreclosure.aspx?a=e&bfid={0}&cid={1}", RecID, ContractId) 
                        : previousPath;
                    break;
                default:
                    returnPath = defaultPath;
                    break;
            }
            // Return the path. If empty: return default page.
            return string.IsNullOrEmpty(returnPath) == false ? returnPath : defaultPath;
        }

        void DisplayAddNewInventoryLink() {
            btnShowPopupInv.HRef = string.Format("~/Pages/Inventory.aspx?a=n&cid={0}&TB_iframe=true&height=340&width=500", ContractId);
            btnShowPopupInv.Visible = true;
        }
        void SetPageBase() {
            PageMode         = Request.QueryString.GetValue<PageModeEnum>("a");
            FormName         = Request.QueryString.GetValue<FormNameEnum>("form");
            EscrowId         = Request.QueryString.GetValue<int>("id"); // Code 99 go back to inventory legal name address
            if (PageMode != PageModeEnum.View) return;
            ContractId   = Request.QueryString.GetValue<int>("cid");
            RecID        = Request.QueryString.GetValue<int>("id");
            // this indicates the button action for user.
            // Button action can be new or save.
            ButtonAction = Request.QueryString.GetValue<string>("ba");
            Session["URL"] = "";           // RIQ-303
        }
    }
}