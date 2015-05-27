#region Includes
using System;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Classes;
using System.Collections.Generic;
using System.Linq;
#endregion
namespace Greenspoon.Tess.Pages
{
    public partial class AddContractToForeclosure : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageBase();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var contractIds = txtContractIds.Text.SplitByInt();
            if (contractIds.Count == 0) {
                divMsg.InnerHtml = "Sorry no master id was provided.";
                return;
            }
            using (var ctx = DataContextFactory.CreateContext()) {
                var errList = new List<string>();
                foreach (var c in contractIds) {
                    var conId = int.Parse(c);
                    if (ctx.contracts.Exists(contract.EqualsToContractId(conId))) {
                        var foreclosedContract = foreclosure.GetActiveForeclosure(conId);
                        if (foreclosedContract != null) {
                            errList.Add(string.Format("{0} is Active in Batch Foreclosure: {1}.", conId, foreclosedContract.batch_foreclosure_id));
                        }
                        else {
                            if (ctx.foreclosures.Exists(foreclosure.EqualsToContractIdAndBatchForeclosureId(BatchForeclosureId, conId)) == false) {
                                ctx.AddToforeclosures(new foreclosure {
                                    contract_id          = conId,
                                    batch_foreclosure_id = BatchForeclosureId,
                                    createdby            = UserName,
                                    createddate          = DateTime.Now
                                });
                            }
                            else {
                                errList.Add(string.Format("{0} already exists in this Batch foreclosure. You can edit as Active.", conId));
                            }
                        }
                    }
                    else {
                        errList.Add(string.Format("{0} is not a valid master id.", conId));
                    }
                }
                if (errList.Any()) {
                    divMsg.InnerHtml = "Your request could not be completed, for the following reason(s)";
                    foreach (string s in errList) {
                        divMsg.InnerHtml += string.Format("<p>{0}</p>", s);
                    }
                }
                else {
                    ctx.SaveChanges();
                    RegisterThickBoxCloseScript();
                }
            }
        }

        #region Util
        void RegisterThickBoxCloseScript()
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "closeThickBox", "self.parent.foreclosure_updated();", true);
        }
        void SetPageBase()
        {
            BatchForeclosureId = Request.QueryString.GetValue<int>("id");
        }
        #endregion
    }
}