#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEditPhaseDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e) {
            SetPageBase();
            if(IsPostBack == false) {
                BindProjectDropDown();
                BindPhaseNameDropDown();

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
            var p = phase_detail.GetPhaseDetail(RecID);
            if (p == null) return;
            drpProject.SelectedValue   = p.project_id.ToString();
            drpPhaseName.SelectedValue = p.phase_name_id.ToString();
            txtPhsOrBook.Text          = p.phs_or_book;
            txtPhsOrPage.Text          = p.phs_or_page;
            lblCreateBy.Text           = p.createdby;
            lblCreateDate.Text         = p.createddate.ToDateOnly();
        } 

        protected void btnSave_Click(object sender, EventArgs e) {
            if(Save() == true) {
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Failed"; }
        }

        bool Save() {
            var p = new phase_detail
            {
                phase_detail_id = RecID,
                project_id      = Convert.ToInt32(drpProject.SelectedValue),
                phase_name_id   = Convert.ToInt32(drpPhaseName.SelectedValue),
                phs_or_book     = txtPhsOrBook.Text,
                phs_or_page     = txtPhsOrPage.Text,
                createdby       = lblCreateBy.Text
            };

            var result = phase_detail.Save(p);
            return result;
        }
      
        void BindProjectDropDown() {
            var data = project.GetProjectList();
            drpProject.DataSource = data;
            drpProject.DataBind();
        }
        void BindPhaseNameDropDown() {
            var data = phase_name.GetPhaseNametList();
            drpPhaseName.DataSource = data;
            drpPhaseName.DataBind();
        }
        #region Util
      
        void RegisterThickBoxCloseScript() {
            Page.ClientScript.RegisterStartupScript(
                GetType(), "closeThickBox", "self.parent.FormSetup_Updated();", true);
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