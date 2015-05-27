using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Text;
using System.Web.Script.Services;

namespace Greenspoon.Tess
{
    /// <summary>
    /// Summary description for SlideService
    /// </summary>
    [WebService(Namespace = "http://microsoft.com/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 

    [ScriptService]
    public class SlideService : System.Web.Services.WebService
    {

        public SlideService() { }

        [WebMethod]
        public AjaxControlToolkit.Slide[] GetSlides()
        {
            AjaxControlToolkit.Slide[] slide;

            DirectoryInfo dir;
            StringBuilder sb = new StringBuilder();
            FileInfo[] files;

            dir = new DirectoryInfo(Server.MapPath(".") + "//Images//Slides");
            files = dir.GetFiles();
            int imgCount = files.Count();
            slide = new AjaxControlToolkit.Slide[imgCount];
            string[] imgNames = new string[imgCount];
            string[] imgDesc = new string[imgCount];
            string[] result = new string[imgCount];
            int x = 0;
            foreach (FileInfo f in files)
            {
                imgNames[x] = f.Name.ToString();
                imgDesc[x] = Path.GetExtension(f.Name);
                result[x] = f.Name.Substring(0, f.Name.Length - imgDesc[x].Length);
                x++;
            }
            for (int i = 0; i < imgCount; i++)
            {
                slide[i] = new AjaxControlToolkit.Slide("Images/Slides/" + imgNames[i], result[i], result[i]);
            }
            return slide;
        }
    }
}
