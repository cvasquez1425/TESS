#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;

using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
#endregion
namespace Greenspoon.Tess.Pages
{
    public partial class Search : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false) {
                // Get the parameters and search database.
                GetSearchResult(GetSearchContext(), GetSearchKey());
            }
        }

        private void GetSearchResult(string context, string key)
        {
            // search context description
            var cDescription = string.Empty;
            // The search string will be converted to id
            // based on search context.
            // Data table for the search result.
            var result = new List<SearchItem>();
            // popular all local variables.
            using (var ctx = DataContextFactory.CreateContext()) {
                try {
                    int id;
                    switch (context) {
//                        case "mi":
                        case "dk":                      // Dev K# default Search Method 8-29-2014. Help desk 24137
                            goto default;
                        case "ebi":
                            cDescription = "Escrow Key";
                            id = Convert.ToInt32(key);
                            result =
                                ( from b in ctx.batch_escrow
                                  join c in ctx.contracts on b.batch_escrow_id equals c.batch_escrow_id into con
                                  from subCon in con.DefaultIfEmpty()
                                  join l in ctx.legal_name on subCon.contract_id equals l.contract_id into names
                                  from subNames in names.Where(n => n.primary == true).DefaultIfEmpty()
                                  where b.batch_escrow_id == id
                                  select new SearchItem {
                                      BatchEscrowId         = b.batch_escrow_id,
                                      BatchEscrowNumber     = b.batch_escrow_number,
                                      ProjectId             = b.project_id,
                                      ContractId            = ( subCon == null? (int?)null : subCon.contract_id ),
                                      DevK                  = ( subCon == null? string.Empty: subCon.dev_k_num ),
                                      LegalLastName         = subNames.last_name, 
                                      BatchEscrowCreateDate = b.createddate,
                                      BatchEscrowCreatedBy  = b.createdby
                                  } ).ToList<SearchItem>();
                            break;
                        case "ebiL":
                            cDescription = "Escrow Key";
                            id = Convert.ToInt32(key);
                            result =
                                ( from b in ctx.batch_escrow
                                  join c in ctx.contracts on b.batch_escrow_id equals c.batch_escrow_id into con
                                  from subCon in con.DefaultIfEmpty()
                                  join l in ctx.legal_name on subCon.contract_id equals l.contract_id into names
                                  from subNames in names.Where(n => n.primary == true).DefaultIfEmpty()
                                  where b.batch_escrow_id == id
                                  select new SearchItem {
                                      BatchEscrowId         = b.batch_escrow_id,
                                      BatchEscrowNumber     = b.batch_escrow_number,
                                      ProjectId             = b.project_id,
                                      ContractId            = ( subCon == null? (int?)null : subCon.contract_id ),
                                      DevK                  = ( subCon == null? string.Empty: subCon.dev_k_num ),
                                      LegalLastName         = subNames.last_name, 
                                      BatchEscrowCreateDate = b.createddate,
                                      BatchEscrowCreatedBy  = b.createdby,
                                      MasterId              = (subCon == null ? (int?)null : subCon.contract_id)  // RIQ-308
                                  } ).ToList<SearchItem>();
                            break;
                        case "pi":
                            cDescription = "Project ID";
                            id = Convert.ToInt32(key);
                            result =
                                ( from b in ctx.batch_escrow
                                  join c in ctx.contracts on b.batch_escrow_id equals c.batch_escrow_id into con
                                  from subCon in con.DefaultIfEmpty()
                                  join l in ctx.legal_name on subCon.contract_id equals l.contract_id into names
                                  from subNames in names.Where(n => n.primary == true).DefaultIfEmpty()
                                  where b.project_id == id
                                  select new SearchItem {
                                      BatchEscrowId         = b.batch_escrow_id,
                                      BatchEscrowNumber     = b.batch_escrow_number,
                                      ProjectId             = b.project_id,
                                      ContractId            = ( subCon == null ? (int?)null : subCon.contract_id ),
                                      DevK                  = ( subCon == null? string.Empty: subCon.dev_k_num ),
                                      LegalLastName         = subNames.last_name, 
                                      BatchEscrowCreateDate = b.createddate,
                                      BatchEscrowCreatedBy  = b.createdby
                                  } ).ToList<SearchItem>();
                            break;
                        case "bk":
                            cDescription = "Batch Key";
                            id = Convert.ToInt32(key);
                            result =
                                ( from b in ctx.batch_escrow
                                  join c in ctx.contracts on b.batch_escrow_id equals c.batch_escrow_id into con
                                  from subCon in con.DefaultIfEmpty()
                                  join l in ctx.legal_name on subCon.contract_id equals l.contract_id into names
                                  from subNames in names.Where(n => n.primary == true).DefaultIfEmpty()
                                  where b.batch_escrow_number == id
                                  select new SearchItem {
                                      BatchEscrowId         = b.batch_escrow_id,
                                      BatchEscrowNumber     = b.batch_escrow_number,
                                      ProjectId             = b.project_id,
                                      ContractId            = ( subCon == null ? (int?)null : subCon.contract_id ),
                                      DevK                  = ( subCon == null? string.Empty: subCon.dev_k_num ),
                                      LegalLastName         = subNames.last_name, 
                                      BatchEscrowCreateDate = b.createddate,
                                      BatchEscrowCreatedBy  = b.createdby
                                  } ).ToList<SearchItem>();
                            break;
                        case "pbk":
                            cDescription = "Project Id and Database";
                            var keys = key.SplitByInt();
                            if (keys.Count != 2) { DisplayProjDatabaseError(); return;}
                            var intKeys = keys.ConvertAll(Convert.ToInt32);
                            int projId = intKeys[0];
                            int ben = intKeys[1];
                            result =
                                ( from b in ctx.batch_escrow
                                  join c in ctx.contracts on b.batch_escrow_id equals c.batch_escrow_id into con
                                  from subCon in con.DefaultIfEmpty()
                                  join l in ctx.legal_name on subCon.contract_id equals l.contract_id into names
                                  from subNames in names.Where(n => n.primary == true).DefaultIfEmpty()
                                  where b.batch_escrow_number == ben && b.project_id == projId
                                  select new SearchItem {
                                      BatchEscrowId         = b.batch_escrow_id,
                                      BatchEscrowNumber     = b.batch_escrow_number,
                                      ProjectId             = b.project_id,
                                      ContractId            = ( subCon == null ? (int?)null : subCon.contract_id ),
                                      DevK                  = ( subCon == null? string.Empty: subCon.dev_k_num ),
                                      LegalLastName         = subNames.last_name, 
                                      BatchEscrowCreateDate = b.createddate,
                                      BatchEscrowCreatedBy  = b.createdby,
                                      MasterId              = ( subCon == null ? (int?)null : subCon.contract_id )
                                  } ).ToList<SearchItem>();
                            break;
                        case "mi":                                      // This was the Default Search Method up to August 29 2014. Help desk 24137
                            cDescription = "Master ID";
                            id = Convert.ToInt32(key);
                            result =
                                (
                                    from c in ctx.contracts
                                    join b in ctx.batch_escrow on c.batch_escrow_id equals b.batch_escrow_id into batch
                                    from subBatch in batch.DefaultIfEmpty()
                                    join l in ctx.legal_name on c.contract_id equals l.contract_id into names
                                    from subNames in names.Where(n => n.primary == true).DefaultIfEmpty()
                                    where c.contract_id == id
                                    select new SearchItem {
                                        BatchEscrowId         = ( subBatch == null ? (int?)null : subBatch.batch_escrow_id ),
                                        BatchEscrowNumber     = ( subBatch == null ? (int?)null : subBatch.batch_escrow_id ),
                                        ProjectId             = ( subBatch == null ? (int?)null : subBatch.project_id ),
                                        ContractId            = c.contract_id,
                                        DevK                  = c.dev_k_num,
                                        LegalLastName         = subNames.last_name,
                                        BatchEscrowCreateDate = ( subBatch == null ? (DateTime?)null : subBatch.createddate ),
                                        BatchEscrowCreatedBy  = ( subBatch == null ? string.Empty : subBatch.createdby )
                                    } ).ToList<SearchItem>();
                            break;
                        case "ck":
                            cDescription = "Client Batch";
                            id = Convert.ToInt32(key);
                            result =
                                (
                                    from c in ctx.contracts
                                    join b in ctx.batch_escrow on c.batch_escrow_id equals b.batch_escrow_id into batch
                                    from subBatch in batch.DefaultIfEmpty()
                                    join l in ctx.legal_name on c.contract_id equals l.contract_id into names
                                    from subNames in names.Where(n => n.primary == true).DefaultIfEmpty()
                                    where c.client_batch == id
                                    select new SearchItem {
                                        BatchEscrowId         = ( subBatch == null ? (int?)null : subBatch.batch_escrow_id ),
                                        BatchEscrowNumber     = ( subBatch == null ? (int?)null : subBatch.batch_escrow_id ),
                                        ProjectId             = ( subBatch == null ? (int?)null : subBatch.project_id ),
                                        ContractId            = c.contract_id,
                                        DevK                  = c.dev_k_num,
                                        LegalLastName         = subNames.last_name,
                                        BatchEscrowCreateDate = ( subBatch == null ? (DateTime?)null : subBatch.createddate ),
                                        BatchEscrowCreatedBy  = ( subBatch == null ? string.Empty : subBatch.createdby ),
                                        MasterId              = c.contract_id                                                       // RIQ-308
                                    } ).ToList<SearchItem>();
                            break;
                        default:
                            cDescription = "Dev K";
                            result =
                                (
                                    from c in ctx.contracts
                                    join b in ctx.batch_escrow on c.batch_escrow_id equals b.batch_escrow_id into batch
                                    from subBatch in batch.DefaultIfEmpty()
                                    join l in ctx.legal_name on c.contract_id equals l.contract_id into names
                                    from subNames in names.Where(n=> n.primary == true && n.legal_name_active==true).DefaultIfEmpty()
                                    where c.dev_k_num == key 
                                    select new SearchItem {
                                        BatchEscrowId         = ( subBatch == null ? (int?)null : subBatch.batch_escrow_id ),
                                        BatchEscrowNumber     = ( subBatch == null ? (int?)null : subBatch.batch_escrow_id ),
                                        ProjectId             = ( subBatch == null ? (int?)null : subBatch.project_id ),
                                        ContractId            = c.contract_id,
                                        DevK                  = c.dev_k_num,
                                        LegalLastName         = subNames.last_name,
                                        BatchEscrowCreateDate = ( subBatch == null ? (DateTime?)null : subBatch.createddate ),
                                        BatchEscrowCreatedBy  = ( subBatch == null ? string.Empty : subBatch.createdby ),
                                        MasterId              = c.contract_id                                                       // RIQ-308
                                    } ).ToList<SearchItem>();
                            break;
                    }
                } catch {
                    DisplayMessage(key, cDescription);
                }
            }

            var resCount = result.Count;

            if (context == "ebi" && resCount > 1) {
                var first = result.First();
                GotoEscrowPage(first.BatchEscrowId, first.ContractId);
            }
            if (resCount > 1) {
                DisplayMessage(key, cDescription, count: resCount, error: false);
                resultView.DataSource = result;
                resultView.DataBind();
            }
            else if (resCount == 1) {
                SearchItem si = ( from s in result select s ).SingleOrDefault();
                GotoEscrowPage(si.BatchEscrowId, si.ContractId);
            }
            else {
                DisplayMessage(key, cDescription);
            }
        }

        #region Utility
        /// <summary>
        /// Get the search context.
        /// ex:  
        /// mi = Master ID [ contract id] [default]
        /// ebi = Escrow Batch ID
        /// pi = Project ID
        /// bk = Batch Key
        /// dk = Dev K
        /// </summary>
        /// <returns>
        /// returns the search context. 
        /// Default context is master id in contract table.
        /// </returns>
        private string GetSearchContext()
        {
            var c = Request.QueryString.GetValue<string>("c");

            //Brian's approval of this change request August 29 2014, Help Desk 24137
//            return string.IsNullOrEmpty(c) == false ? c : "mi";
            return string.IsNullOrEmpty(c) == false ? c : "dk";
        }

        private string GetSearchKey()
        {
            return Request.QueryString.GetValue<string>("key");
        }

        private void GotoEscrowPage(int? eid, int? cid)
        {
            Response.Redirect(cid.HasValue
                                  ? string.Format("~/Pages/BatchEscrow.aspx?a=e&beid={0}&cid={1}", eid, cid)
                                  : string.Format("~/Pages/BatchEscrow.aspx?a=e&beid={0}", eid));
        }

        private void DisplayMessage(string key, string cDescription, bool error = true, int count = 0)
        {
            if (error == true) {
                lblMessage.Text = string.Format("Your search in \"{0}\" for \"{1}\" did not return any record.", cDescription, key);
            }
            else {
                lblMessage.Text = string.Format("[ {0} ] Records found for search in \"{1}\" for \"{2}\" ", count, cDescription, key);
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
        }

        void DisplayProjDatabaseError()
        {
            lblMessage.Text = "You must provide a project id and database id for this search. (ex. ##### #####)";
        }

        #endregion

        // RIQ-304 Project Dev K Search Results.
        protected void resultView_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            // Use the ItemDataBound event to customize the DataGrid control. 
            // The ItemDataBound event allows you to access the data before 
            // the item is displayed in the control. In this example, the 

            if ((e.Item.ItemType == ListItemType.Item) ||
             (e.Item.ItemType == ListItemType.AlternatingItem))
            {

                int masterid = Convert.ToInt32(e.Item.Cells[8].Text);         
                if (ContractActive(masterid))
                {
                    e.Item.Cells[0].BackColor = System.Drawing.Color.Red; // ContractID
                    e.Item.Cells[1].BackColor = System.Drawing.Color.Red; // Batch ID
                    e.Item.Cells[2].BackColor = System.Drawing.Color.Red; //Last Name
                    e.Item.Cells[3].BackColor = System.Drawing.Color.Red;
                    e.Item.Cells[4].BackColor = System.Drawing.Color.Red;
                    e.Item.Cells[5].BackColor = System.Drawing.Color.Red;
                    e.Item.Cells[6].BackColor = System.Drawing.Color.Red;
                    e.Item.Cells[7].BackColor = System.Drawing.Color.Red;
                }
                
            }
        }

        bool ContractActive(int ContractId)
        {

            var IsContractActive = contract.activeContract(ContractId);
            if (IsContractActive) { return true; } else { return false; }
        }

    }
}