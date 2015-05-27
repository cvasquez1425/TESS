#region Includes
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Classes;
#endregion

namespace Greenspoon.Tess.Pages {
    public partial class Transactions : PageBase {
        protected void Page_Load(object sender, EventArgs e) {
            //return;
            //// Set up page mode and ids.
            //SetPageBase();
            //if(Page.IsPostBack == false) {
            //    // Set page based on page mode.
            //    BindPage();
            //}
        }
        protected void BtnSave_Click(object sender, EventArgs args) {
            if(this.Page.IsValid) {
                if(PageMode == PageModeEnum.New) {
                    this.Insert();
                }
                else if(PageMode == PageModeEnum.Edit) {
                    this.Update();
                }
                // Once page is saved.
                // Close the dialog modal.
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.Transaction_Updated();", true);
            }
        }
        void BindFormEntity(ref contract_amount a) {
            a.contract_amount_field_id = int.Parse(drpAmoutType.SelectedValue);
            a.amount = double.Parse(txtAmount.Text);
            a.effective_date_from = DateTime.Parse(txtEffFrm.Text);
            a.effective_date_to = DateTime.Parse(txtEffTo.Text);
            if(PageMode == PageModeEnum.New) {
                a.createddate = DateTime.Now;
                a.createdby = UserName;
                a.contract_id = this.ContractId;
            }
        }
        private void BindPage() {
            BindAmountTypeDropDown();
            // Bind all fiend if page is edit mode.
            if((PageMode == PageModeEnum.Edit) && (RecID > 0)) {
                using(var ctx = DataContextFactory.CreateContext()) {
                    var amount                 = ctx.contract_amount.Where(a => a.contract_amt_id == RecID)
                                                    .SingleOrDefault<contract_amount>();
                    drpAmoutType.SelectedValue = amount.contract_amount_field_id.ToString();
                    txtAmount.Text             = amount.amount.ToString();
                    txtEffFrm.Text             = amount.effective_date_from.ToDateOnly();
                    txtEffTo.Text              = amount.effective_date_to.ToDateOnly();
                    lblDate.Text               = amount.createddate.ToDateOnly();
                    lblUser.Text               = amount.createdby;
                }
            }
            else if(PageMode == PageModeEnum.New) {
                lblDate.Text = DateTime.Now.ToDateOnly();
                lblUser.Text = UserName;
            }
        }

        void BindAmountTypeDropDown() {
            drpAmoutType.DataSource = TessHelper.GetAmountFieldList();
            drpAmoutType.DataBind();
        }
        void Insert() {
            contract_amount ca = new contract_amount();
            BindFormEntity(ref ca);
            using(var ctx = DataContextFactory.CreateContext()) {
                ctx.AddTocontract_amount(ca);
                ctx.SaveChanges();
            }
        }
        void Update() {
                using(var ctx = DataContextFactory.CreateContext()) {
                    var nl = ctx.contract_amount.SingleOrDefault(n => n.contract_amt_id == RecID);
                    BindFormEntity(ref nl);
                    ctx.SaveChanges();
                }
        }
        void SetPageBase() {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            if(PageMode == PageModeEnum.New) {
                ContractId = Request.QueryString.GetValue<int>("id");
            }
            else {
                RecID = Request.QueryString.GetValue<int>("id");
            }
        }
    }
}