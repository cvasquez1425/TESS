using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Greenspoon.Tess.Classes;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class Season
    {
        internal static List<DropDownItem> GetSeasonList()
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var seasonList = new List<DropDownItem>();
                var season = (from s in ctx.Seasons
                              select new
                              {
                                  Name = s.season_name,
                                  Value = s.season_id
                              }).ToList();
                foreach (var item in season)
                {
                    seasonList.Add(new DropDownItem { Name = item.Name, Value = item.Value.ToString() });
                }
                if (seasonList.Any() == true)
                {
                    seasonList.Insert(0, new DropDownItem());
                }
                return seasonList;
            }
        
        } // end of get list
    } // end of class
}