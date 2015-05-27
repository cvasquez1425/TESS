using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Excel;
using Greenspoon.Tess.BusinessObjects.BusinessRules;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Services;

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class BulkStatusUpdate : PageBase
    {
        private BatchStatusUpdateConfiq _config;
        protected void Page_Load(object sender, EventArgs e)
        {
            //            if (Page.IsPostBack) return;
            if (!Page.IsPostBack)
            {

                BindCountyDropwDown();
                BindStatusMasterDropDown();
                BindProjectDropDown();
            }
            //RIQ-289 CVJan2013
            //if (Page.IsPostBack)
            //{
                string Selval = drpStatusMaster.SelectedValue;                              // Jackie Diaz error message "Input string was not in a correct format"
                if (Selval != String.Empty)
                {
                    int statusMasterID = int.Parse(drpStatusMaster.SelectedValue);
                    chkIsComment.Checked = status.GetStatusMasterID(statusMasterID);
                }
            //}
        }

        void BindStatusMasterDropDown()
        {
            var list = status_master.GetStatusMasterList();
            drpStatusMaster.DataSource = list;
            drpStatusMaster.DataBind();
        }

        void BindCountyDropwDown()
        {
            var list = county.GetCountyList();
            drpCounty.DataSource = list; drpCounty.DataBind();
            drpOriginalCounty.DataSource = list;
            drpOriginalCounty.DataBind();
        }

        void BindProjectDropDown()
        {
            var data = project.GetProjectList();
            drpProject.DataSource = data;
            drpProject.DataBind();
        }

        protected void btnSaveBatch_Click(object sender, EventArgs e)
        {
            if (StatusMasterIsInvalidShowError()) return; //RIQ-289 CVJan2013
            if (FormIsInvalidShowError()) return;
            if (BothFileUploadedAndKeyProvided()) return;
            List<string> keys;
            if (excelUpld.HasFile) {
                if (InvalidFileFormatShowError()) return;
                keys = GetKeysFromExcel();
                if (keys == null || keys.Any() == false) {
                    ShowMsg("Could not read data from excel.");
                    return;
                }
            }
            else {
                keys = GetKeysFromTextBox();
            }
            var delimatedKeys = string.Join(",", keys);

            if (NoIdenKeysProvidedShowError(keys)) return;
            if (AnyBatchKeyIsInvalidShowError(delimatedKeys)) return;

            var result = Save(delimatedKeys);

            if (result.Item1) {
                ShowMsg(string.Format("Status updated {0} records.", result.Item2), isError: false);
                var uploadKey = keys.First();         // RIQ-Bulk Update cv1212
                BulkUpdateDetBOReport(uploadKey);    // RIQ-Bulk Update cv1212
                return;
            }
            ShowMsg("Unknown Error: Could not update status.");
        }

        //RIQ-289 CVJan2013
        private bool StatusMasterIsInvalidShowError()
        {
            if (chkIsComment.Checked == true && txtComments.Text.Length <= 0)
            {
                ShowMsg("Comment is a required field for this STATUS MASTER CODE.");
                txtComments.Focus();
                return true;
            }
            return false;
        }

        bool InvalidFileFormatShowError()
        {
            var ext = Path.GetExtension(excelUpld.FileName);
            if (InvalidFileType(ext)) {
                ShowMsg("Error: Invalid file format. Excel Only.");
                excelUpld.Focus();
                return true;
            }
            return false;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/Admin/Pages/BulkStatusUpdate.aspx");
        }

        Tuple<bool, string> Save(string delimatedKeys)
        {
            var ui = new UpdateBatchStatusDTO {
                KeyType          = drpIden.SelectedValue,
                ProjectId        = drpProject.SelectedValue,
                StatusMasterId   = drpStatusMaster.SelectedValue,
                KeyData          = delimatedKeys,
                EffectiveDate    = txtEffDate.Text,
                Assign           = txtAssignNum.Text,
                OriginalCountyId = drpOriginalCounty.SelectedValue,
                CountyId         = drpCounty.SelectedValue,
                Comments         = txtComments.Text,
                UserName         = UserName,
                Book             = txtBook.Text,
                Page             = txtPage.Text,
                RecordDate       = txtDocRec.Text,
            };
            return DbService.BatchStatusUpdate(ui);
        }

        void ShowMsg(string msg, bool isError = true)
        {
            lblBatchMsg.CssClass = isError ? "error" : "clear";
            lblBatchMsg.Text = msg;
        }

        bool AnyBatchKeyIsInvalidShowError(string keys)
        {
            try
            {
                var invalidKeys =
                    new BulkStatusUpdateKeysValidator(keys, int.Parse(drpIden.SelectedValue)).GetInvalidKeys();

                if (invalidKeys == null || !invalidKeys.Any()) return false;

                //var errKeys = String.Join(",", invalidKeys);
                //ShowMsg(string.Format("Following {0} not valid. {1}", invalidKeys.Count() > 1 ? "keys are" : "key is", errKeys));
                ShowMsg("Invalid key(s) present. Check list.");
                return true;
            }
            catch
            {
                ShowMsg("There was an error reading keys.");
                return true;
            }
        }

        bool NoIdenKeysProvidedShowError(IEnumerable<string> readKeys)
        {
            if (!readKeys.Any()) {
                ShowMsg("Error: Identfier Keys contains no keys.");
                txtKeys.Focus();
                return true;
            }
            return false;
        }

        bool BothFileUploadedAndKeyProvided()
        {
            if (txtKeys.Text.Length > 0 && excelUpld.HasFile) {
                ShowMsg("Error: Cannot upload file and keys at the same time.");
                txtKeys.Focus();
                return true;
            }
            return false;
        }

        bool InvalidFileType(string fileExtension)
        {
            return !fileExtension.ToLower().Equals(".xls") && !fileExtension.ToLower().Equals(".xlsx");
        }

        List<string> GetKeysFromExcel()
        {
            try {
                var excelReader = Path.GetExtension(excelUpld.FileName).Equals(".xls")
                                      ? ExcelReaderFactory.CreateBinaryReader(excelUpld.FileContent)
                                      : ExcelReaderFactory.CreateOpenXmlReader(excelUpld.FileContent);

                excelReader.IsFirstRowAsColumnNames = true;
                var ds = excelReader.AsDataSet();

                var dt = ds.Tables[0];
                var columnName = GetExcelColumnName();
                if (Invalid(columnName)) return null;
                var keys = dt.AsEnumerable().Select(columns => columns[columnName].ToString()).ToList();
                excelReader.Close();
                var filteredKeys = keys.Where(k => !string.IsNullOrEmpty(k)).ToList();
                return filteredKeys;
            } catch {
                return null;
            }
        }

        List<string> GetKeysFromTextBox()
        {
            return GetIdenValue() != 4 ? txtKeys.Text.SplitByInt() 
                                       : txtKeys.Text.SplitBySeparator();
        }

        int GetIdenValue()
        {
            return int.Parse(drpIden.SelectedValue);
        }

        string GetExcelColumnName()
        {
            _config = BatchStatusUpdateConfiq.Instance;
            int iden = GetIdenValue();
            var columnName = iden == 1 ? _config.BatchEscrowId
                           : iden == 2 ? _config.MasterId
                           : iden == 3 ? _config.BatchCancelId 
                           : iden == 4 ? _config.DevK 
                           : string.Empty;
            return columnName;
        }

        bool Invalid(string columnName)
        {
            return string.IsNullOrEmpty(columnName);
        }

        bool FormIsInvalidShowError()
        {
            if (drpIden.SelectedIndex == 0) {
                ShowMsg("Error: Identfier dropdown is required.");
                drpIden.Focus();
                return true;
            }
            if (drpStatusMaster.SelectedIndex == 0) {
                ShowMsg("Error: Status master dropdown is required.");
                drpStatusMaster.Focus();
                return true;
            }
            return false;
        }

        // RIQ-Bulk Update cv1212
        void BulkUpdateDetBOReport(string uploadKey)
        {
            int UploadBatchID = 0 ;
            long firstKeyItem = Convert.ToInt64(uploadKey);
            int identKey = GetIdenValue();

            if (identKey == 2 )
            {
            using ( var ctx = DataContextFactory.CreateContext() ) {
                var data =
                    from s in ctx.status
                    where s.contract_id == firstKeyItem
                    select s ;
                    if( data.Any() )
                    {
                        UploadBatchID  = data.Max( a => a.upload_batch_id.Value  ) ;
                    }                    
                };
            }
            else
            {
                using ( var ctx = DataContextFactory.CreateContext() ) 
                {
                    var data = ctx.status.Max(a => a.upload_batch_id);
                    UploadBatchID = data.Value;
                };

            }
//            UploadBatchID = 85138;
            // Business Object Bulk Update Report changed as of August 13, 2014, NEW HYPERLINK.
            // Miami New Hyperlinks November 2014 - Basically, the only thing you need to update is the address from gm-atl-biprod to gm-mdv-biprod
            string urlString = "http://gm-mdv-biprod:8080/TokenLogin/BulkUpdateDetails.jsp?id=BulkUpdateID&docId=ARgCv5_03w5MkyAmuZ3JtAY";
            var sb = new System.Text.StringBuilder();
            if (urlString.Contains("BulkUpdateID")) sb.Append(urlString).Replace("BulkUpdateID", UploadBatchID.ToString());
            ResponseHelper.Redirect(sb.ToString(), "_blank", "menubar=0,width=1200,height=900");
        }

    }
}