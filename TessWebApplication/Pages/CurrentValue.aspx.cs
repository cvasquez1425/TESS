using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;

namespace Greenspoon.Tess.Pages
{
    public partial class CurrentValue : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageBase();
            if (Page.IsPostBack == false)
            {
                List<CurrentValueDTO> uiList = Current_Value.GetContractIdUIList(BatchCancelId);
                gvCurrentValue.DataSource = uiList;
                gvCurrentValue.DataBind();
            }
        }

        IList<CurrentValueDTO> BindData()
        {
            var ui = new List<CurrentValueDTO>();
            foreach (GridViewRow row in gvCurrentValue.Rows)
            {
                var contractId = (row.FindControl("ContractId") as Label).Text;
                var lastName = (row.FindControl("LastName") as Label).Text;
                var currentValue = (row.FindControl("txtCurrentValue") as TextBox).Text;
                var cancelId = (row.FindControl("CancelId") as Label).Text;

                ui.Add(new CurrentValueDTO
                {
                    CancelId = int.Parse(cancelId),
                    ContractId = int.Parse(contractId),
                    LastName = lastName,
                    CurrentValue = currentValue,
                    ModifiedBy = UserName,
                    ModifiedDate = DateTime.Now.ToString(),
                });
            }
            return ui;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        void Save()
        {
            var ui = BindData();
            Current_Value.Save(ui);
            //lblMsg.Text = "Current Value Updated.";
            CloseModalWindow();

            if (Current_Value.Save(ui))
            {
                CloseModalWindow();
            }
            else
            {
                lblMsg.Text = "ERROR: Could not Update Current Value.";
            }
        }

        private void CloseModalWindow()
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "closeThickBox", "self.parent.cancel_updated();", true);
        }

        void SetPageBase()
        {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            if (PageMode == PageModeEnum.New)
            {
                BatchCancelId = Request.QueryString.GetValue<int>("id");
            }
            if (PageMode == PageModeEnum.Edit)            
            {
                RecID = Request.QueryString.GetValue<int>("id");
                FormName = Request.QueryString.GetValue<FormNameEnum>("form");
            }
        }

    }
}