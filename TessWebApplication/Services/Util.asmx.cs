using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Greenspoon.Tess.DataObjects.Linq;
using System.Text;

namespace Greenspoon.Tess.Services
{
    /// <summary>
    /// Summary description for Util
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Util : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetLastContractSet()
        {
            var contracts = contract.GetLastSet();

            var sb = new StringBuilder();

            foreach (var con in contracts) {
                sb.Append("<BR />");
                sb.Append(con.contract_id.ToString());
            }
            return sb.ToString();
        }
    }
}
