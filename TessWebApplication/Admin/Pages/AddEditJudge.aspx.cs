#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEditJudge : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageBase();
            if (IsPostBack == false) {
                BindCountyDown();
                BindDocumentGroup();
                if (PageMode == PageModeEnum.Edit && RecID > 0) {
                    SetupEditForm();
                }
                else {
                    lblCreateBy.Text   = UserName;
                    lblCreateDate.Text = DateTime.Now.ToDateOnly();
                }
            } 
        }

        #region DropDowns
        void BindCountyDown()
        {
            var list = county.GetCountyList();
            drpCounty.DataSource = list;
            drpCounty.DataBind();
        }
        void BindDocumentGroup()
        {
            var list = document_group.GetDocumentGroupList();
            drpDocumentGroup.DataSource = list;
            drpDocumentGroup.DataBind();
        }
        #endregion

        void SetupEditForm()
        {
            var j = judge.GetJudge(RecID);
            if (j == null) return;
            drpCounty.SelectedValue        = j.county_id.ToString();
            txtRoom.Text                   = j.room;
            txtDivision.Text               = j.division;
            txtJudgeLastName.Text          = j.judge_last_name;
            txtJudgeName.Text              = j.judge_name;
            txtPhone.Text                  = j.phone;
            chkActive.Checked              = j.judge_active;
            drpDocumentGroup.SelectedValue = j.document_group_id.HasValue == true
                                                 ? j.document_group_id.ToString()
                                                 : string.Empty;
            lblCreateBy.Text    = j.createdby;
            lblCreateDate.Text  = j.createddate.ToDateOnly();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save() == true) {
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Failed"; }
        }

        bool Save()
        {
            var j = new judge {
                judge_id          = RecID,
                county_id         = int.Parse(drpCounty.SelectedValue),
                room              = txtRoom.Text.NullIfEmpty<string>(),
                division          = txtDivision.Text.NullIfEmpty<string>(),
                judge_name        = txtJudgeName.Text,
                judge_last_name   = txtJudgeLastName.Text,
                phone             = txtPhone.Text.NullIfEmpty<string>(),
                judge_active      = chkActive.Checked,
                document_group_id = drpDocumentGroup.SelectedIndex > 0 
                                     ? int.Parse(drpDocumentGroup.SelectedValue) 
                                     : (int?)null,
                createdby         = lblCreateBy.Text,
                createddate       = PageMode == PageModeEnum.Edit 
                                     ? ( lblCreateDate.Text.Length > 0 ) 
                                       ? DateTime.Parse(lblCreateDate.Text) 
                                       : (DateTime?)null
                                     : DateTime.Now

            };
            var result = judge.Save(j);
            return result;
        }
        #region Util
        // Call the method script on the parent form.
        // Parent form method will close the modal and
        // will refresh the parent page.
        void RegisterThickBoxCloseScript()
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.FormSetup_Updated();", true);
        }
        /// <summary>
        /// Read the query string and set up page values.
        /// GetValue is an extension method.
        /// </summary>
        void SetPageBase()
        {
            base.PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            if (base.PageMode == PageModeEnum.Edit) {
                base.RecID = Request.QueryString.GetValue<int>("id");
            }
        }
        #endregion
    } // end of class
}