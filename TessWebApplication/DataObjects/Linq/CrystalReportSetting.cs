#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Greenspoon.Tess.Classes;

#endregion
namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class CrystalReportSetting
    {
        internal static Expression<Func<CrystalReportSetting, bool>> EqualsToFolderName(FormNameEnum form)
        {
            var formName = form.ToString().ToLower();
            return r => r.FolderName.ToLower().Equals(formName);
        }

        //BO Links PDF, Word, and Excel replacing Crystal Report Engine
        internal static Expression<Func<BusinessObjectSetting, bool>> EqualsToLocation(FormNameEnum form)
        {
            var formName = form.ToString().ToLower();
            return r => r.Location.ToLower().Equals(formName);
        }

        internal static Expression<Func<CrystalReportSetting, bool>> EqualsToID(int id)
        {
            return r => r.ReportID == id;
        }

        internal static IList<CrystalReportSetting> GetReportSettings()
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.CrystalReportSettings
                          .OrderBy(r => r.ReportDisplayName)
                          .ToList(); ;
            }
        }

        internal static IList<CrystalReportSetting> GetReportSettingByForm(FormNameEnum form)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.CrystalReportSettings.Where(EqualsToFolderName(form))
                           .OrderBy(r => r.ReportDisplayName).ToList();
            }
        }

        //BO Links PDF, Word, and Excel replacing Crystal Report Engine
        internal static IList<BusinessObjectSetting> GetReportBOSettingByForm(FormNameEnum form)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                return ctx.BusinessObjectSettings.Where(EqualsToLocation(form))
                           .OrderBy(r => r.ReportDisplayName).ToList();
            }
        }

        internal static CrystalReportSetting GetReportSettingById(int id)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.CrystalReportSettings.SingleOrDefault<CrystalReportSetting>(EqualsToID(id));
            }
        }

        internal static bool Save(CrystalReportSetting setting)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                ctx.AddToCrystalReportSettings(setting);
                return ctx.SaveChanges() > 0;
            }
        }

        internal static bool Delete(int Id)
        {
            try{
                using (var ctx = DataContextFactory.CreateContext()) {
                    var setting = ctx.CrystalReportSettings
                                     .SingleOrDefault<CrystalReportSetting>(s => s.ReportID == Id);
                    ctx.DeleteObject(setting);
                    ctx.SaveChanges();
                    return true;
                }
            } catch { return false; };
        }

        internal static bool Exists(string fileName)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.CrystalReportSettings.Any(s => s.ReportFileName.Equals(fileName));
            }
        }
    }
}