using System.Web;

namespace Greenspoon.Tess.Services
{
    public class AuthService
    {
        internal static bool IsUserTessAdmin()
        {
           // return HttpContext.Current.IsDebuggingEnabled || HttpContext.Current.User.IsInRole(TessRoles.TessAdmin);
           // return HttpContext.Current.User.IsInRole(@"tessadmin");
            return true;
        }
    }

    public class TessRoles
    {
        internal readonly static string TessAdmin = @"greenspoonmarde\tessadmingroup";
    }
}