#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion
namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEditPartner : PageBase
    {
        protected void Page_Load(object sender, EventArgs e) {
            SetPageBase();
            if(IsPostBack == false) {
                BindProjectDropDown();
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
            var par = partner.GetPartner(RecID);
            if (par == null) return;
            drpProject.SelectedValue = par.project_id.ToString();
            txtPartnerName.Text      = par.partner_name;
            lblCreateBy.Text         = par.createdby;
            lblCreateDate.Text       = par.createddate.ToDateOnly();
        } 

        void BindProjectDropDown() {
            var list = project.GetProjectList();
            drpProject.DataSource = list;
            drpProject.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            if(Save() == true) {
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Failed"; }
        }

        bool Save() {
            var pa = new partner
            {
                partner_id   = RecID,
                project_id   = int.Parse(drpProject.SelectedValue),
                partner_name = txtPartnerName.Text,
                createdby    = lblCreateBy.Text,
                createddate  = PageMode == PageModeEnum.Edit 
                                ? (lblCreateDate.Text.Length > 0) 
                                   ? DateTime.Parse(lblCreateDate.Text) 
                                   : (DateTime?)null
                                : DateTime.Now
            };
            var result = partner.Save(pa);
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