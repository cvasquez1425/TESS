using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Services;

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class CrystalSetup : PageBase
    {
        IList<CrystalReportSetting> _settings;
        CrystalReportConfig _config;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthService.IsUserTessAdmin()) {
                Response.Redirect("~/NoAuth.aspx");
            }
            SetSettingandConfiq();
            if (Page.IsPostBack == false) {
                ShowCurrentReportList();
            }
        }

        void ShowCurrentReportList(bool refreshFromServer = false)
        {
            grdP.DataSource = GetDistinctFolderNames(refreshFromServer);
            grdP.DataBind();
        }

        void SetSettingandConfiq()
        {
            _settings = CrystalReportSetting.GetReportSettings().ToList();
            _config = CrystalReportConfig.Instance;
        }

        IEnumerable<CrystalReportSetting> GetDistinctFolderNames(bool refreshFromServer = false)
        {
            if (refreshFromServer) {
                SetSettingandConfiq();
            }
            return _settings.Distinct(new CrystalSettingComparer()).AsEnumerable();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid && !updReportFile.HasFile) {
                ShowMsg("ERROR: Invalid action.");
                return;
            }
            if (ReportExists()) {
                ShowMsg("ERROR: Report already exists. Please delete report from system then try again.");
                return;
            }
            if (UploadFile()) {
                SaveSettings();
            }
            ShowCurrentReportList(true);
            ClearForm();
        }

        void ClearForm()
        {
            txtReportDisplayName.Text = string.Empty;
            drpFormName.ClearSelection();
        }

        bool ReportExists()
        {
            return CrystalReportSetting.Exists(updReportFile.FileName) || File.Exists(GetReportFile());
        }

        bool UploadFile()
        {
            var ext = Path.GetExtension(updReportFile.FileName);

            if (InvalidFileType(ext)) {
                ShowMsg("ERROR: Only .rpt file allowed.");
                return false;
            }
            try {
                var report = GetReportFile();
                CheckOrSetReportFilePath(report);
                updReportFile.SaveAs(report);
                return true;
            } catch {
                ShowMsg("ERROR: Upload Failed. Contact Admin.");
                return false;
            }
        }

        private void CheckOrSetReportFilePath(string report)
        {
            var path = Path.GetDirectoryName(report);
            if (path != null && !Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
        }

        private string GetReportFile()
        {
            var instance = CrystalReportConfig.Instance;
            var reportFile = string.Format(@"{0}{1}\{2}", Server.MapPath(instance.CrystalReportBasePath), drpFormName.SelectedValue, updReportFile.FileName);

            return reportFile;
        }

        void SaveSettings()
        {
            var setting = new CrystalReportSetting {
                FolderName        = drpFormName.SelectedValue,
                ReportDisplayName = txtReportDisplayName.Text.Trim(),
                ReportFileName    = Path.GetFileName(updReportFile.FileName),
                CreatedOn         = DateTime.Today,
                CreatedBy         = UserName
            };
            CrystalReportSetting.Save(setting);
            ShowMsg("Report added successfully.", false);
        }

        bool InvalidFileType(string fileExtension)
        {
            return !fileExtension.ToLower().Equals(".rpt");
        }

        void ShowMsg(string msg, bool isError = true)
        {
            lblMsg.CssClass = isError ? "error" : "clear";
            lblMsg.Text = msg;
        }

        protected void grdP_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                var row = (CrystalReportSetting)e.Row.DataItem;
                var ds = _settings.Where(r => r.FolderName == row.FolderName).AsEnumerable();

                var childView = (GridView)e.Row.FindControl("grdC");
                childView.DataSource = ds;
                childView.DataBind();
            }
        }

        protected void DeleteRecord(object sender, GridViewDeleteEventArgs e)
        {
            var id = Convert.ToInt32(e.Keys[0].ToString());
            var setting = CrystalReportSetting.GetReportSettingById(id);
            var report = string.Format("{0}/{1}/{2}", Server.MapPath(_config.CrystalReportBasePath), setting.FolderName, setting.ReportFileName);

            if (File.Exists(report)) {
                File.Delete(report);
            }

            var result = CrystalReportSetting.Delete(id);
            if (result) {
                ShowMsg("Record Deleted", false);
                ShowCurrentReportList(true);
            }
        }
    }
}