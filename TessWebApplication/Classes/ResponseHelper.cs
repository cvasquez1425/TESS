using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
//Response.Redirect into a new window Carlos B Vasquez

namespace Greenspoon.Tess.Classes
{
    public class ResponseHelper
    {
        public static void Redirect(string url, string target, string windowfeatures)
        {
            HttpContext context = HttpContext.Current;
            if ((String.IsNullOrEmpty(target) ||
                target.Equals("_self", StringComparison.OrdinalIgnoreCase)) && String.IsNullOrEmpty(windowfeatures))
            {
                context.Response.Redirect(url);
            }
            else
            {
                Page page = (Page)context.Handler;
                if (page == null)
                {
                    throw new InvalidOperationException("Cannot redirect to new window outside Page context.");
                }
                url = page.ResolveClientUrl(url);
                string script;
                if (!String.IsNullOrEmpty(windowfeatures))
                {
                    script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
                }
                else
                {
                    script = @"window.open(""{0}"", ""{1}"");";
                }
                script = String.Format(script, url, target, windowfeatures);
                ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
            }

        }
    }
}
