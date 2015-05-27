#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Pages
{
    public partial class AddContractToCancel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageBase();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var contractIdList = txtContractIds.Text.SplitByInt();
            if (contractIdList.Any()) {
                Session["cancelId"]         = BatchCancelId;
                Session["contractid"]       = ContractId;
                Session["selectedindex"]    = 0;
                Save(contractIdList);
            }
            else {
                divMsg.InnerHtml = "Sorry no master id was provided.";
            }
        }
        void Save(List<string> conIdList)
        {
            var result = cancel.AddContractToCancel(conIdList, BatchCancelId, UserName);
            if (result.Length == 0) {
                RegisterThickBoxCloseScript();
            }
            else {
                divMsg.InnerHtml = result;
            }
        }
        #region Util
        void RegisterThickBoxCloseScript()
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "closeThickBox", "self.parent.cancel_updated();", true);
        }
        void SetPageBase()
        {
            BatchCancelId = Request.QueryString.GetValue<int>("id");
        }
        #endregion
    }
}
