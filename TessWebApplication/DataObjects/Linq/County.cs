#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Greenspoon.Tess.Classes;
#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class county
    {
        internal static Expression<Func<county, bool>> IsEqualsToCountyId(int countyId) {
            return c => c.county_id == countyId;
        }
        internal static List<DropDownItem> GetCountyList() {
            var countyList = new List<DropDownItem>();
            using(var ctx = DataContextFactory.CreateContext()) {
                var counties = (from c in ctx.counties
                                orderby c.county_name ascending
                                select new
                                {
                                    Name = c.county_name,
                                    Value = c.county_id
                                }).ToList();
                foreach(var item in counties) {
                    countyList.Add(new DropDownItem { Name = item.Name, Value = item.Value.ToString() });
                }
                countyList.Insert(0, new DropDownItem());
            }
            return countyList;
        } // end of get country list.
        internal static List<county> GetCounties() {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.counties.Include("state").ToList();
            }
        }
        internal static county GetCounty(int countyId) {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.counties.SingleOrDefault(IsEqualsToCountyId(countyId));
            }
        }
        internal static bool Save(county param) {
            var result = false;
            using(var ctx = DataContextFactory.CreateContext()) {
                if(param.county_id == 0) {
                    param.createddate = DateTime.Now;
                    ctx.AddTocounties(param);
                }
                else {
                    var c = ctx.counties
                               .SingleOrDefault(IsEqualsToCountyId(param.county_id));
                    if(c != null) {
                        c.county_name	           = param.county_name.NullIfEmpty<string>();
                        c.county_circuit	       = param.county_circuit.NullIfEmpty<string>();
                        c.address1	               = param.address1.NullIfEmpty<string>();
                        c.address2	               = param.address2.NullIfEmpty<string>();
                        c.city	                   = param.city.NullIfEmpty<string>();
                        c.zip	                   = param.zip.NullIfEmpty<string>();
                        c.state_id	               = param.state_id;
                        c.clerk	                   = param.clerk.NullIfEmpty<string>();
                        c.phone1	               = param.phone1.NullIfEmpty<string>();
                        c.phone2	               = param.phone2.NullIfEmpty<string>();
                        c.phone3	               = param.phone3.NullIfEmpty<string>();
                        c.news	                   = param.news.NullIfEmpty<string>();
                        c.news_address1	           = param.news_address1.NullIfEmpty<string>();
                        c.news_address2	           = param.news_address2.NullIfEmpty<string>();
                        c.news_citystzip	       = param.news_citystzip.NullIfEmpty<string>();
                        c.gsa	                   = param.gsa.NullIfEmpty<string>();
                        c.defense_text	           = param.defense_text.NullIfEmpty<string>();
                        c.disability_text	       = param.disability_text.NullIfEmpty<string>();
                        c.proof	                   = param.proof.NullIfEmpty<string>();
                        c.publication	           = param.publication.NullIfEmpty<string>();
                        c.forward	               = param.forward.NullIfEmpty<string>();
                        c.NOA_Letter_P6	           = param.NOA_Letter_P6.NullIfEmpty<string>();
                        c.alias_letter	           = param.alias_letter.NullIfEmpty<string>();
                        c.clerk_letter	           = param.clerk_letter.NullIfEmpty<string>();
                        c.second_default_letter	   = param.second_default_letter.NullIfEmpty<string>();
                        c.clerk_sig_text	       = param.clerk_sig_text.NullIfEmpty<string>();
                        //c.base_file	               = param.base_file.ToString().NullIfEmpty<decimal?>();
                        //c.base_count	           = param.base_count.ToString().NullIfEmpty<int?>();
                        //c.additional_file	       = param.additional_file.ToString().NullIfEmpty<decimal?>();
                        //c.sale_stamp	           = param.sale_stamp.ToString().NullIfEmpty<decimal?>();
                        //c.county_percent	       = param.county_percent.ToString().NullIfEmpty<decimal?>();
                        //c.per_bid	               = param.per_bid.ToString().NullIfEmpty<decimal?>();
                        c.sale_type	               = param.sale_type.NullIfEmpty<string>();
                        c.effective_date	       = param.effective_date.ToString().NullIfEmpty<DateTime?>();
                        c.createdby	               = param.createdby.NullIfEmpty<string>();
                        c.web_site	               = param.web_site.NullIfEmpty<string>();
                        c.web_comments	           = param.web_comments.NullIfEmpty<string>();
                    }
                }
                if(ctx.SaveChanges() > 0){
                    result = true;
                }
                return result;
            }
        } // end of save.
        internal static string GetCountyName(int countyId) {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.counties.SingleOrDefault(IsEqualsToCountyId(countyId)).county_name;
            }
        }
    } // end of class
}