#region Includes
using System;
using System.Web.UI.WebControls;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion
namespace Greenspoon.Tess.Pages
{
    public partial class LegalName : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageBase();
            if (Page.IsPostBack == false) {
                BindCountryList();
                if (PageMode == PageModeEnum.Edit && RecID > 0) {
                    SetupEditForm();
                }
                else {
                    SetupNewForm();
                }
            }
        } 

        void Page_PreRender(object sender, EventArgs e)
        {
            if (ContractId == 0) { return; }
            var usrConLegalNames = (Controls.LegalNames)LoadControl("~/Controls/LegalNames.ascx");
            if (usrConLegalNames == null) return;
            usrConLegalNames.ContractID = ContractId;
            usrConLegalNames.FormName = FormNameEnum.Unknown;
            usrConLegalNames.ShowActiveOnly = chkShowAllLN.Checked ? false : true;
            usrConLegalNames.ReadOnly = true;
            usrConLegalNames.SetTallStyle();
            plcLegalNames.Controls.Add(usrConLegalNames);

        } 

        void SetupEditForm()
        {
            var ui = legal_name.GetLegalNameDTO(RecID);
            if (ui == null) return;
            chkPrimary.Checked = ui.Primary;
            txtFirstName.Text  = ui.FirstName;
            txtLastName.Text   = ui.LastName;
            txtAddress1.Text   = ui.Address1;
            txtAddress2.Text   = ui.Address2;
            txtAddress3.Text   = ui.Address3;
            txtCity.Text       = ui.City;
            txtState.Text      = ui.State;
            txtZip.Text        = ui.Zip;
            txtEmail.Text      = ui.Email;
            txtOrder.Text      = ui.Order;
            chkDismiss.Checked = ui.Dismiss;
            chkActive.Checked  = ui.Active;
            lblCreateBy.Text   = ui.CreatedBy;
            lblCreateDate.Text = ui.CreatedDate;
            drpCountryList.SelectedValue = ui.CountryId;

            if (FormName.isForeclosure() != true) return;
            chkDismiss.Enabled = true;
            txtOrder.Enabled = true;
        } 

        void SetupNewForm()
        {
            var ui = legal_name.GetAnyLegalNameDTO(ContractId);
            if (ui != null) {
                txtAddress1.Text = ui.Address1;
                txtAddress2.Text = ui.Address2;
                txtAddress3.Text = ui.Address3;
                txtCity.Text = ui.City;
                txtState.Text = ui.State;
                txtZip.Text = ui.Zip;
                drpCountryList.SelectedValue = string.IsNullOrEmpty(ui.CountryId) ? "840" :ui.CountryId;
            } else { drpCountryList.SelectedValue = "840" ;}    // when adding new legal name address, defaulted to USA

            chkActive.Checked = true;
            lblCreateBy.Text = UserName;
            lblCreateDate.Text = DateTime.Now.ToDateOnly();
            //RIQ-316
            int legalNamePrimary = legal_name.GetAnyPrimaryLegalName(ContractId);
            if (legalNamePrimary > 0) { chkPrimary.Enabled = false; }
        }

        protected void Save_Click(object sender, EventArgs args)
        {
            // RIQ-316
            bool chkLegalNamePri = PrimaryLegalName_Validation();
            if (chkLegalNamePri) { return; }

            if (!Page.IsValid) return;
            if (Save()) {
                var lb = sender as LinkButton;
                if (lb != null) {
                    if (lb.ID == "btnSaveClose") {
                        RegisterThickBoxCloseScript();
                    }
                    else {
                        Response.Redirect(string.Format("~/Pages/LegalName.aspx?a=n&cid={0}&form={1}&LinkSource={2}", ContractId, FormName, LinkSource));
                    }
                }
                else { CreateMsg("Action Failed"); }
            }
            else { CreateMsg("Failed"); }
        }
        // RIQ-316
//        private void PrimaryLegalName_Validation()
        bool PrimaryLegalName_Validation()
        {
            var ui = legal_name.GetPrimaryOnLegal_Name(RecID);
            if (ui == null)
            {
                int legalNamePrimary = legal_name.GetAnyPrimaryLegalName(ContractId);
                if (legalNamePrimary > 0 && chkPrimary.Checked == true)
                {
                    CreateMsg("Uncheck previous primary before adding a new one.");
                    return true;
                }
            }
            return false;
        }

        bool Save()
        {
            var ui = new LegalNamesDTO {
                LegalNameId = RecID.ToString(),
                ContractId  = ContractId.ToString(),
                FirstName   = txtFirstName.Text,
                LastName    = txtLastName.Text,
                Address1    = txtAddress1.Text,
                Address2    = txtAddress2.Text,
                Address3    = txtAddress3.Text,
                City        = txtCity.Text,
                State       = txtState.Text,
                Zip         = txtZip.Text,
                CountryId   = drpCountryList.SelectedValue,
                Email       = txtEmail.Text,
                Dismiss     = chkDismiss.Checked,
                Order       = txtOrder.Text,
                Active      = chkActive.Checked,
                Primary     = chkPrimary.Checked,
                CreatedBy   = lblCreateBy.Text,
                //Resale Forclosure Orlando Perfect Practice
                ModifyDate      = DateTime.Today.ToString(),
                ModifyBy = UserName,
            };
            var result = legal_name.Save(ui);
            return result;
        }

        void BindCountryList()
        {
            drpCountryList.DataSource = TessHelper.GetCountryList();
            drpCountryList.DataBind();
        }
        #region Util

        void SetPageBase()
        {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            FormName = Request.QueryString.GetValue<FormNameEnum>("form");
            ContractId = Request.QueryString.GetValue<int>("cid");
            LinkSource = Request.QueryString.GetValue<string>("LinkSource");
            if (PageMode == PageModeEnum.Edit) {
                RecID = Request.QueryString.GetValue<int>("id");
            }
        }

        private void RegisterThickBoxCloseScript()
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "closeThickBox", "self.parent.Inventory_Updated();", true);
        }

        void CreateMsg(string msg)
        {
            lblMsg.Text = msg;
        }
        #endregion

        protected void txtZip_TextChanged(object sender, EventArgs e)
        {
            //if (drpCountryList.SelectedValue == "840" && RecID <= 0)  zipcode lookup features enabled for edit and addition of new ones.
            if (drpCountryList.SelectedValue == "840")
            {
                string[] zipState = legal_name.lookupZipCode(txtZip.Text);
                txtCity.Text    = zipState[0];
                txtState.Text   = zipState[1];
                txtEmail.Focus();
            }
        }

    } // end of class
}
