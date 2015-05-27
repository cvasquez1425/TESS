#region Includes
using System;
using System.Web.UI.WebControls;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Services;
using System.Data;
using System.Text;
using System.Linq;
#endregion

namespace Greenspoon.Tess.Pages
{
    public partial class TitleSearch : PageBase
    {
        private readonly DateTime _cacheDuration = DateTime.Now.AddMinutes(5);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                BindAllDropDowns();
                BindResult();
                BindUI();
            }
        }

        #region DindDropDowns
        private void BindAllDropDowns()
        {
            BindProjectDropDown();
            //BindWeekDropDown();
            BindYearDorpDown();
        }
     
        void BindProjectDropDown()
        {
            var list = project.GetProjectList();
            drpProject.DataSource = list;
            drpProject.DataBind();
        }
        
        void BindYearDorpDown()
        {
            var list =  cont_year_master.GetContractYearList();
            drpYears.DataSource = list;
            drpYears.DataBind();
        }
        
        // TESS Enhancement 2015 Feb 20, 2015
        void BindWeekDropDown(int weekId)
        //void BindWeekDropDown()
        {
            //var list = cont_week_master.GetContractWeekList();
            //drpWeeks.DataSource = list;
            //drpWeeks.DataBind();
            var list = cont_week_master_single.GetSingleWeekList(weekId);
            txtWeeks.Text = list.Item2;
               
        }

        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var empty = string.Empty;
            if (txtWeeks.Text != empty) { BindWeekDropDown(int.Parse(txtWeeks.Text)); }
            DoTitleSearch();
        }
        
        void ClearForm()
        {
            new CacheService<DataTable>(GetResultKey()).Clear();
            new CacheService<TitleSearchDTO>(GetUIKey()).Clear();
            Server.Transfer("~/Pages/TitleSearch.aspx");
        }
        
        protected void TitleSearchListView_OnItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (!string.Equals(e.CommandName, "GoToBatchEscrow")) return;
            var dataItem = (ListViewDataItem)e.Item;
            var contractId = lvData.DataKeys[dataItem.DisplayIndex].Value.ToString();
            var batchId = e.CommandArgument.ToString();
            RegisterThickBoxCloseScript(batchId, contractId);
        }
        
        void RegisterThickBoxCloseScript(string batchId, string contractId)
        {
            Page.ClientScript
                .RegisterStartupScript(GetType(), "closeThickBox", string.Format("self.parent.GotoBatchEscrow({0},{1});", batchId, contractId), true);
        }
        
        protected void btnRedirectToBatchEscrow_click(object sender, EventArgs e) { }
        
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        void DoTitleSearch()
        {
            var ui     = new TitleSearchDTO {
                ProjectId         = drpProject.SelectedValue,
                MasterId          = txtMasterId.Text,
                DevK              = txtDevKNo.Text.Trim(),
                AltBldg           = txtAltBldg.Text.Trim(),
                Active            = chkActive.Checked,
                FullName          = txtName.Text.Trim(),
                Zip               = txtZip.Text.Trim(),
                UnitNumber        = txtUnitNumber.Text.Trim(),
                BuildingCode      = txtBuildingCode.Text.Trim(),
                //ContWeekId        = drpWeeks.SelectedValue,
                ContWeekId        = txtWeeks.Text,
                ContYearId        = drpYears.SelectedValue,
                DeedBook          = txtDeedBook.Text.Trim(),
                DeedPage          = txtDeedPage.Text.Trim(),
                BatchCancelId     = string.Empty,
                Color             = txtColor.Text.Trim(),
                ProjectIdDb       = txtProjectIdDb.Text.Trim(), // March 2014 Adding ProjectId_Database
                Phase             = txtPhaseId.Text.Trim()
            };

            var data = TitleSearchServie.DoTitleSearch(ui);
            InsertFormDataToCache(ui, data);
            lvData.DataSource = data;
            lvData.DataBind();
        }

        void InsertFormDataToCache(TitleSearchDTO ui, DataTable data)
        {
            if (txtWeeks.Text != string.Empty)
            {
                txtWeeks.Text = cont_week_master_single.GetWeekNumber(int.Parse(txtWeeks.Text));
                ui.ContWeekId = txtWeeks.Text;
            }

            var cData = new CacheService<DataTable>(GetResultKey());
            var cUI   = new CacheService<TitleSearchDTO>(GetUIKey());
            cData.Insert(data, _cacheDuration);
            cUI.Insert(ui, _cacheDuration);
        }

        void BindResult()
        {
            var data = new CacheService<DataTable>(GetResultKey()).Grab();
            if (data == null) return;
            lvData.DataSource = data;
            lvData.DataBind();
        }

        void BindUI()
        {
            var ui = new CacheService<TitleSearchDTO>(GetUIKey()).Grab();
            if (ui == null) return;
            drpProject.SelectedValue = ui.ProjectId;
            txtMasterId.Text         = ui.MasterId;
            txtDevKNo.Text           = ui.DevK;
            txtAltBldg.Text          = ui.AltBldg;
            chkActive.Checked        = ui.Active;
            txtName.Text             = ui.FullName;
            txtZip.Text              = ui.Zip;
            txtUnitNumber.Text       = ui.UnitNumber;
            txtBuildingCode.Text     = ui.BuildingCode;
            //drpWeeks.SelectedValue   = ui.ContWeekId;           //Feb 20, 2015
            txtWeeks.Text            = ui.ContWeekId;
            drpYears.SelectedValue   = ui.ContYearId;
            txtDeedPage.Text         = ui.DeedPage;
            txtDeedBook.Text         = ui.DeedPage;
        }

        string GetUIKey()
        {
            return string.Format("{0}{1}", "U", Session.SessionID);
        }

        string GetResultKey()
        {
            return string.Format("{0}{1}", "R", Session.SessionID);
        }

        protected string GetStatusReportUrl(string url, int masterid)
        {
            return string.Format(url, masterid);
        }

        protected void lvData_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewItem item = e.Item;
            if (item.ItemType == ListViewItemType.DataItem)
            {
                Label gvgLabel = (Label)item.FindControl("gvgLabel");
                string gvgTest = gvgLabel.Text;
                if (gvgTest == "DEVELOPER OWNED = YES")
                {
                    gvgLabel.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    gvgLabel.BackColor = System.Drawing.Color.Red;
                }
            }
        }
    } 
}