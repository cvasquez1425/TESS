using System;
using System.ComponentModel;
using System.Data;

namespace Greenspoon.Tess.Controls
{
    public partial class ScannedImages : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
         //   if(!IsPostBack)
            btnShowImage.Click += new EventHandler(btnShowImage_Click);

        }

        [Bindable(true)]
        public int ContractID
        {
            get
            {
                return ViewState["ContractID"] == null ? 0 
                    : int.Parse(ViewState["ContractID"].ToString());
            }
            set
            {
                ViewState["ContractID"] = value;
            }
        }

        protected void btnShowImage_Click(object sender, EventArgs e)
        {
            btnShowImage.Visible = false;
            gvScannedImages.Visible = true;
            gvScannedImages.DataSource = GetDummyData();
            gvScannedImages.DataBind();
        }

        DataTable GetDummyData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("docnum");
            dt.Columns.Add("docname");
            dt.Columns.Add("DocLocation");

            dt.Rows.Add("1", "Some doc 1", @"C:\Users\admin\Pictures\2011-01-22\001.JPG");
            dt.Rows.Add("2", "Some doc 1", @"C:\Users\admin\Pictures\2011-01-22\001.JPG");
            dt.Rows.Add("3", "Some doc 1", @"C:\Users\admin\Pictures\2011-01-22\001.JPG");

            return dt;
        }
    }
}