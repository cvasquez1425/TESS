using System.Collections.Generic;
using System.Linq;
using Greenspoon.Tess.Classes;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class gender
    {
        internal static List<DropDownItem> GetGenderTypes() {
            var genderTypeList = new List<DropDownItem>();
            using(var db = DataContextFactory.CreateContext()) {
                var genderType = (from g in db.genders
                                  select new
                                  {
                                      Name  = g.gender_description,
                                      Value = g.gender_id
                                  }).ToList();
                foreach(var item in genderType) {
                    genderTypeList.Add(new DropDownItem { Name=item.Name, Value=item.Value.ToString() });
                }
                if(genderTypeList.Any() == true) {
                    genderTypeList.Insert(0, new DropDownItem());
                }
            }
            return genderTypeList;
        }
  
    }
}