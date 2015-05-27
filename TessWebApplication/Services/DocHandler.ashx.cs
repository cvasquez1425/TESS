using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Web;
using Greenspoon.Tess.Classes;

namespace Greenspoon.Tess.Services
{
    /// <summary>
    /// Summary description for DocHandler
    /// </summary>
    public class DocHandler : IHttpHandler
    {
        readonly DocumentConfig _config = DocumentConfig.Instance;
        const int LOGON32_PROVIDER_DEFAULT        = 0;
        const int LOGON32_LOGON_INTERACTIVE       = 2;
       
        /* const int LOGON32_LOGON_NETWORK           = 3;
           const int LOGON32_LOGON_BATCH             = 4;
           const int LOGON32_LOGON_SERVICE           = 5;
           const int LOGON32_LOGON_UNLOCK            = 7;
           const int LOGON32_LOGON_NETWORK_CLEARTEXT = 8;
           const int LOGON32_LOGON_NEW_CREDENTIALS   = 9; */
        
        [DllImport("advapi32.dll", SetLastError=true)]
        public static extern int LogonUser(
            string lpszUsername,
            string lpszDomain,
            string lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            out IntPtr phToken
            );

        [DllImport("advapi32.dll", SetLastError=true)]
        public static extern int ImpersonateLoggedOnUser(
            IntPtr hToken
        );

        [DllImport("advapi32.dll", SetLastError=true)]
        static extern int RevertToSelf();

        [DllImport("kernel32.dll", SetLastError=true)]
        static extern int CloseHandle(IntPtr hObject);

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;
            response.ContentType = "application/octet-stream";

            var doc =  HttpUtility.UrlDecode(request.QueryString.GetValue<string>("doc"));
            var docType = request.QueryString.GetValue<string>("doctype");

            if (string.IsNullOrEmpty(doc) || string.IsNullOrEmpty(docType))
                return;

            var token = default(IntPtr);
            try {
                var result =  LogonUser(_config.UserName, _config.Domain, _config.Password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, out token);
                if (result > 0) {
                    ImpersonateLoggedOnUser(token);
                    var fi = new FileInfo(doc);
                    response.AppendHeader("Content-Disposition", "attachment; filename=" + string.Format("{0}.{1}", fi.Name, GetFileExtention(docType)));
                    response.BufferOutput = false;
                    response.TransmitFile(fi.FullName);
                    response.End();
                }
                else {
                    response.AppendHeader("Content-Disposition", "attachment; filename=error.txt");
                    response.Write(Environment.NewLine);
                    response.Write(string.Format("Could not log in to retrieve file. Win 32 error code {0}. ", Marshal.GetLastWin32Error().ToString()));
                    response.End();
                }
            } catch (Exception ex) {
                response.AppendHeader("Content-Disposition", "attachment; filename=error.txt");
                response.Write(Environment.NewLine);
                response.Write(string.Format(@"Doc server threw an error. Error: {0} ", ex.Message));
                response.End();
            }
            RevertToSelf();
            CloseHandle(token);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        static string GetFileExtention(string docType)
        {
            var extList = FileExtentionList();
            return extList.ContainsKey(docType.ToUpper()) ? extList[docType.ToUpper()] : docType;
        }

        static Dictionary<string, string> FileExtentionList()
        {
            var extList = new Dictionary<string, string>(32)
                              {
                                  {"ACROBAT", "PDF"},
                                  {"ANSI", "TXT"},
                                  {"BMP", "BMP"},
                                  {"EXCEL", "XLS"},
                                  {"EXCELX", "XLSX"},
                                  {"FAX", "%V"},
                                  {"GIF", "GIF"},
                                  {"GW", "GW"},
                                  {"HTML", "HTML"},
                                  {"ILLUSTRATOR-AI", "AI"},
                                  {"JPEG", "JPEG"},
                                  {"LFD", "LFD"},
                                  {"LOTUS123", "%V"},
                                  {"MIME", "MSG"},
                                  {"NOTES", "DXL"},
                                  {"PCX", "PCX"},
                                  {"PNG", "PNG"},
                                  {"PPT", "PPT"},
                                  {"PPTX", "PPTX"},
                                  {"PRSHW", "SHW"},
                                  {"PTX", "PTX"},
                                  {"PUB", "PUB"},
                                  {"QUATTRO", "QPW"},
                                  {"TIFF", "TIF"},
                                  {"URL", "URL"},
                                  {"VISIO", "VSD"},
                                  {"WMF", "WMF"},
                                  {"WORD", "DOC"},
                                  {"WORDX", "DOCX"},
                                  {"WORDXT", "DOCT"},
                                  {"WPF", "WPD"},
                                  {"XML", "XML"}
                              };
            return extList;
        }
    }
}