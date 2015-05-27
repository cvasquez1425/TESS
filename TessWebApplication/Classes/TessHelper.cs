using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Greenspoon.Tess.DataObjects.Linq;
namespace Greenspoon.Tess.Classes
{
    public static class TessHelper
    {
        internal static readonly string ReportingServerUserName   = ConfigurationManager.AppSettings.Get("ReportingServerUserName");
        internal static readonly string ReportingServerPassword   = ConfigurationManager.AppSettings.Get("ReportingServerPassword");
        internal static readonly string ReportingServerUserDomain = ConfigurationManager.AppSettings.Get("ReportingServerUserDomain");
        // Get the base url of the application
        public static string baseUrl = 
            string.Format("{0}{1}", HttpContext.Current.Request
                                        .Url
                                        .GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped), "/");
        public static List<DropDownItem> GetCountryList()
        {
            var countryList = new List<DropDownItem>();
            using (var db = DataContextFactory.CreateContext()) {
                var countries = 
                    ( from c in db.countries
                      where c.country_name.Length > 0
                      select new {
                          Name = c.country_name,
                          Value = c.country_id
                      } ).OrderBy(c => c.Name).ToList();
                countryList.AddRange(countries.Select(item => new DropDownItem { Name = item.Name, Value = item.Value.ToString() }));
                countryList.Insert(0, new DropDownItem());
            }
            return countryList;
        }
        public static List<DropDownItem> GetAmountFieldList()
        {
            var amountFieldList = new List<DropDownItem>();
            using (var db = DataContextFactory.CreateContext()) {
                var af = ( from f in db.contract_amount_field
                           select new {
                               Name = f.contract_amt_field_name,
                               Value = f.contract_amount_field_id
                           } ).ToList();
                amountFieldList.AddRange(af.Select(item => new DropDownItem { Name = item.Name, Value = item.Value.ToString() }));
                amountFieldList.Insert(0, new DropDownItem());
            }
            return amountFieldList;
        }
        public static List<DropDownItem> GetForeclosureTypeList()
        {
            var fcTypeList = new List<DropDownItem>();
            // Populate the list.
            using (var db = DataContextFactory.CreateContext()) {
                var fcTypes = ( from t in db.foreclosure_type
                                select new {
                                    Name  = t.foreclosure_type_name,
                                    Value = t.foreclosure_type_id
                                } ).ToList();
                fcTypeList.AddRange(from type in fcTypes where type.Name != null && type.Name.Trim().Length > 0 select new DropDownItem { Name = type.Name, Value = type.Value.ToString() });
                fcTypeList.Insert(0, new DropDownItem());
                return fcTypeList;
            }
        }
        public static List<DropDownItem> GetJudgeListByProjectId(int projId)
        {
            var judgeList = new List<DropDownItem>();

            // Populate the list.
            using (var db = DataContextFactory.CreateContext()) {
                try {
                    var countyId = db.projects.SingleOrDefault(p => p.project_id == projId).county_id;
                    var judges = ( from j in db.judges
                                   where j.county_id == countyId
                                   select new {
                                       Name    = j.judge_name,
                                       Divsion = j.division,
                                       Value   = j.judge_id
                                   } ).ToList();
                    // Combine Project ID and Name for Display
                    judgeList.AddRange(from judge in judges
                                       where judge.Name != null && judge.Name.Trim().Length > 0
                                       select new DropDownItem {
                                           Name = string.Format("{0} [{1}]", judge.Name, judge.Divsion),
                                           Value = judge.Value.ToString()
                                       });
                } catch (Exception) { }
                if (judgeList.Any()) {
                    judgeList.Insert(0, new DropDownItem());
                }
                return judgeList;
            }
        }
        public static contract GetContractById(int contractId)
        {
            if (contractId > 0) {
                using (var db = DataContextFactory.CreateContext()) {
                    return db.contracts.SingleOrDefault(c => c.contract_id == contractId);
                }
            }
            return new contract();
        }

        public static string GetCancelForeclosureDate(int contractId)
        {
            string result;
            using (var db = DataContextFactory.CreateContext()) {
                var cancel = ( from c in db.cancels
                               where c.contract_id == contractId
                               select new {
                                   CancelDate = c.cancel_date
                               } ).FirstOrDefault();
                var fc = ( from f in db.foreclosures
                           where f.contract_id.Equals(contractId)
                           select new {
                               ForeclosureDate = f.default_date
                           } ).FirstOrDefault();

                string cd = string.Empty;
                string fd = string.Empty;

                if (cancel != null) {
                    cd = string.Format("Cancel Date: {0}", cancel.CancelDate.ToDateOnly());
                }
                if (fc != null) {
                    fd = string.Format(" Foreclosure Date: {0}", fc.ForeclosureDate.ToDateOnly());
                }
                result = string.Format("{0}  {1}", cd, fd);
            }
            return result;
        }
    } // End of Class


    internal class CrystalSettingComparer : IEqualityComparer<CrystalReportSetting>
    {
        public bool Equals(CrystalReportSetting x, CrystalReportSetting y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;
            return x.FolderName == y.FolderName;
        }

        public int GetHashCode(CrystalReportSetting setting)
        {
            if (ReferenceEquals(setting, null)) return 0;
            return setting.FolderName == null ? 0 : setting.FolderName.GetHashCode();
        }
    }
}