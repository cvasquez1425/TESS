#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEditFirm : PageBase
    {
        protected void Page_Load(object sender, EventArgs e) {
            SetPageBase();
            if(IsPostBack == false) {
                if(PageMode == PageModeEnum.Edit && RecID > 0) {
                    SetupEditForm();
                }
                else {
                    lblCreateBy.Text   = UserName;
                    lblCreateDate.Text = DateTime.Now.ToDateOnly();
                }
            }
        }

        void SetupEditForm() {
            var f = firm.GetFirm(RecID);
            if (f == null) return;
            txtDeveloperGroupId.Text = f.developer_group_id.ToString();
            txtFirmCode.Text         = f.firm_code.ToString();
            txtFirmDesignation.Text  = f.firm_designation;
            txtFirmName.Text         = f.firm_name;
            txtFirmAddress1.Text     = f.firm_address1;
            txtFirmAddress2.Text     = f.firm_address2;
            txtFirmReplyTo.Text      = f.firm_replyto;
            txtFirmAgent.Text        = f.firm_agent;
            txtFirmAgentTitle.Text   = f.firm_agent_title;
            txtFirmBarNumber.Text    = f.firm_bar_num;
            txtFirmGender.Text       = f.firm_gender;
            lblCreateBy.Text         = f.createdby;
            lblCreateDate.Text       = f.createddate.ToDateOnly();
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            if(Save() == true) {
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Failed"; }
        }

        bool Save() {
            var f = new firm
            {
                firm_id            = RecID,
                developer_group_id = Convert.ToInt32(txtDeveloperGroupId.Text),
                firm_code          = txtFirmCode.Text.NullIfEmpty<Int16?>(),
                firm_designation   = txtFirmDesignation.Text.NullIfEmpty<string>(),
                firm_name          = txtFirmName.Text.NullIfEmpty<string>(),
                firm_address1      = txtFirmAddress1.Text.NullIfEmpty<string>(),
                firm_address2      = txtFirmAddress2.Text.NullIfEmpty<string>(),
                firm_replyto       = txtFirmReplyTo.Text.NullIfEmpty<string>(),
                firm_agent         = txtFirmAgent.Text.NullIfEmpty<string>(),
                firm_agent_title   = txtFirmAgentTitle.Text.NullIfEmpty<string>(),
                firm_bar_num       = txtFirmBarNumber.Text.NullIfEmpty<string>(),
                firm_gender        = txtFirmGender.Text.NullIfEmpty<string>(),
                createdby          = lblCreateBy.Text.NullIfEmpty<string>()
            };

            var result = firm.Save(f);
            return result;
        }
        #region Util
     
        void RegisterThickBoxCloseScript() {
            Page.ClientScript.RegisterStartupScript(GetType(), "closeThickBox", "self.parent.FormSetup_Updated();", true);
        }
       
        void SetPageBase() {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            if(PageMode == PageModeEnum.Edit) {
                RecID = Request.QueryString.GetValue<int>("id");
            }
        }
        #endregion

    } 
}