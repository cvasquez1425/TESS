#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion
namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEditDeveloper : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageBase();
            if (IsPostBack == false) {
                BindDropDowns();
                if (PageMode == PageModeEnum.Edit && RecID > 0) {
                    SetupEditForm();
                }
                else {
                    lblCreateBy.Text   = UserName;
                    lblCreateDate.Text = DateTime.Now.ToDateOnly();
                }
            }
        }
        private void BindDropDowns()
        {
            var data = developer_group.GetDeveloperGroup();
            drpDevGroup.DataSource = data;
            drpDevGroup.DataBind();

            data = developer_master.GetDevMaster();
            drpDevMaster.DataSource = data;
            drpDevMaster.DataBind();
        }
        void SetupEditForm()
        {
            var d = developer.GetDeveloper(RecID);
            if (d == null) return;
            drpDevGroup.SelectedValue = d.developer_group_id.ToString();
            drpDevMaster.SelectedValue = d.developer_master_id.ToString();
            txtDevName.Text     = d.developer_name;
            txtAddress1.Text    = d.address1;
            txtAddress2.Text    = d.address2;
            txtAddress3.Text    = d.address3;
            txtAltAddress1.Text = d.alt_address1;
            txtAltAddress2.Text = d.alt_address2;
            txtAltAddress3.Text = d.alt_address3;
            txtReassign.Text    = d.reassign.ToString();
            chkDevPG2.Checked   = d.developer_pg2 ?? false;
            txtIntroAtty.Text   = d.intro_atty.ToString();
            txtBillingAtty.Text = d.billing_atty.ToString();
            txtDevText.Text     = d.developer_txt;
            chkActive.Checked   = d.active;
            lblCreateBy.Text    = d.createdby;
            lblCreateDate.Text  = d.createddate.ToDateOnly();
        } 

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDevName.Text) || string.IsNullOrWhiteSpace(txtDevName.Text)) {
                lblMsg.Text = "Developer Name is blank.";
                return;
            }
            if (Save() == true) {
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Failed"; }
        }
        bool Save()
        {
            var dev = new developer {
                developer_id    =  RecID,
                developer_group_id = drpDevGroup.SelectedIndex > 0 
                                     ? Convert.ToInt16(drpDevGroup.SelectedValue) 
                                     : (int?)null,
                developer_master_id = drpDevMaster.SelectedIndex > 0
                                     ? Convert.ToInt16(drpDevMaster.SelectedValue) 
                                     : (int?)null,
                developer_name  =  txtDevName.Text,
                address1        =  txtAddress1.Text.NullIfEmpty<string>(),
                address2        =  txtAddress2.Text.NullIfEmpty<string>(),
                address3        =  txtAddress3.Text.NullIfEmpty<string>(),
                alt_address1    =  txtAltAddress1.Text.NullIfEmpty<string>(),
                alt_address2    =  txtAltAddress2.Text.NullIfEmpty<string>(),
                alt_address3    =  txtAltAddress3.Text.NullIfEmpty<string>(),
                reassign        =  txtReassign.Text.NullIfEmpty<decimal?>(),
                developer_pg2   =  chkDevPG2.Checked,
                intro_atty      =  txtIntroAtty.Text.NullIfEmpty<int?>(),
                billing_atty    =  txtBillingAtty.Text.NullIfEmpty<int?>(),
                developer_txt   =  txtDevText.Text.NullIfEmpty<string>(),
                active          =  chkActive.Checked,
                createdby       =  lblCreateBy.Text,
            };
            var result = developer.Save(dev);
            return result;
        } 
        
        #region Util
        
        void RegisterThickBoxCloseScript()
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "closeThickBox", "self.parent.FormSetup_Updated();", true);
        }
      
        void SetPageBase()
        {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            if (PageMode == PageModeEnum.Edit) {
                RecID = Request.QueryString.GetValue<int>("id");
            }
        }
        #endregion
    } 
}