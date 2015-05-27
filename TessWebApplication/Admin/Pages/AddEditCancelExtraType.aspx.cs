#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion
namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEditCancelExtraType : PageBase
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
            var cet = cancel_extra_type
                      .GetCancelExtraType(RecID);
            if (cet == null) return;
            txtCancelExtraTypeValue.Text = cet.cancel_extra_type_value;
            lblCreateBy.Text             = cet.createdby;
            lblCreateDate.Text           = cet.createddate.ToDateOnly();
        } 


        protected void btnSave_Click(object sender, EventArgs e) {
            if(Save() == true) {
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Failed"; }
        }

        bool Save() {
            var cet = new cancel_extra_type {
                 cancel_extra_type_id    = RecID,
                 cancel_extra_type_value = txtCancelExtraTypeValue.Text.NullIfEmpty<string>(),
                 createdby               = lblCreateBy.Text.NullIfEmpty<string>(),
            };

            var result = cancel_extra_type.Save(cet);
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
    } // end of class
}