﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Greenspoon.Tess.Pages
{
    public partial class WebFormPleaseWait : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Introducing delay for demonstration.
            System.Threading.Thread.Sleep(3000);
            //Label1.Text = "Page refreshed at " +
            //    DateTime.Now.ToString();  
        }
    }
}