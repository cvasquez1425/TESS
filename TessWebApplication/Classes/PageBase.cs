using System.Web.UI;

namespace Greenspoon.Tess.Classes
{
    /// <summary>
    /// <remarks>
    ///     I have used an ID and escrow id or contract id is cause,
    ///     those ids are used heavily in large pages. this increases
    ///     user readability. Other small sub forms I have used "ID"
    ///     since it will not be long code and will not be hard to read.
    /// </remarks>
    /// </summary>
    public class PageBase:Page
    {
        protected PageModeEnum PageMode  { get; set; }
        // Form type
        protected FormNameEnum FormName  { get; set; }
        // this is to be used when FormName is not available
        // the user control might open a page where a formname
        // is not possible to pass. ex: opening legal name add window
        // from Inventory extended page. Also when FormName is already
        // in use.
        protected string  LinkSource { get; set; }
        // This is generic id and can be used for 
        // sub forms.
        protected int RecID              { get; set; }
        // Batch Escrow table Id
        protected int EscrowId           { get; set; }
        // Contract Table Id.
        protected int ContractId         { get; set; }
        // Batch Foreclosure Id.
        protected int BatchForeclosureId { get; set; }
        // Batch Cancel Id
        protected int BatchCancelId      { get; set; }
        protected int CancelId           { get; set; }  
        // User button action.
        protected string ButtonAction    {get;set;}
        // Current logged in user.
        protected string UserName {
            get {
                var name = User.Identity.Name; 
                if(name.Contains("\\")) {
                    var start = name.IndexOf("\\", System.StringComparison.Ordinal);
                    if(name.Length > start) {
                        name = name.Substring(start + 1);
                    }
                }
                return name;
            }
        }

        protected bool PageIsEditMode
        {
            get { return PageMode == PageModeEnum.Edit; }
        }

        public bool PageIsNewMode 
        {
            get { return PageMode == PageModeEnum.New; }
        }
        #region Javascript support

        /// <summary>
        /// Adds an 'Open Window' Javascript function to page.
        /// </summary>
        protected void RegisterOpenWindowJavaScript()
        {
            const string script = "<script language='JavaScript' type='text/javascript'>" + "\r\n" +
                                  " <!--" + "\r\n" +
                                  " function openwindow(url,name,width,height) " + "\r\n" +
                                  " { " + "\r\n" +
                                  "   window.open(url,name,'toolbar=yes,location=yes,scrollbars=yes,status=yes,menubar=yes,resizable=yes,top=50,left=50,width='+width+',height=' + height) " + "\r\n" +
                                  " } " + "\r\n" +
                                  " //--> " + "\r\n" +
                                  "</script>" + "\r\n";

            ClientScript.RegisterClientScriptBlock(typeof(string), "OpenWindowScript", script);
        }

        #endregion
    }
}
