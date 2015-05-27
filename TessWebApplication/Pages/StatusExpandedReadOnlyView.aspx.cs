#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Pages
{
    public partial class StatusExpandedReadOnlyView : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageBase();
            if (Page.IsPostBack == false) {
                SetupPage();
            }
        }
        /// <summary>
        /// Set up the Grid View and Return Link/Add new Link.
        /// </summary>
        void SetupPage()
        {
            BindStatusList();
        }
        /// <summary>
        /// Set up List View grid for a contract ID.
        /// Displays all status. 
        /// </summary>
        void BindStatusList()
        {
            // Form name represents which form the user is coming from.
            var ui = 
                status.GetStatusUIList(FormNameEnum.Cancel, ContractId);
            lvData.DataSource = ui;
            lvData.DataBind();
        }
        
        /// <summary>
        /// Read the query string and set up page values.
        /// GetValue is an extension method.
        /// </summary>
        void SetPageBase()
        {
            ContractId   = Request.QueryString.GetValue<int>("cid");
        }
    }
}