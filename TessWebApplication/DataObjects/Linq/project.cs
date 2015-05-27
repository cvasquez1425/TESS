#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using Greenspoon.Tess.Classes;
#endregion
namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class project
    {
        internal static Expression<Func<project, bool>> EqualsToProjectId(int projectId)
        {
            return p => p.project_id == projectId;
        }

        internal static List<DropDownItem> GetProjectList(bool AllowCache = true)
        {
            const string strCacheKey = "ProjectDropDownList";
            if (AllowCache && ( HttpContext.Current.Cache[strCacheKey] != null )) {
                return (List<DropDownItem>)HttpContext.Current.Cache[strCacheKey];
            }
            var projList = new List<DropDownItem>();
            using (var ctx = DataContextFactory.CreateContext()) {
                var projects = ( from p in ctx.projects
                                 select new {
                                     Name  = p.project_name,
                                     Value = p.project_id
                                 } ).OrderBy(o => o.Value).ToList();
                projList.AddRange(from p in projects where p.Name != null && p.Name.Trim().Length > 0 
                                  select new DropDownItem { Name = string.Format("{1} {0}", p.Name, p.Value), Value = p.Value.ToString() });
                projList.Insert(0, new DropDownItem());
                if (AllowCache && ( HttpContext.Current.Cache[strCacheKey] == null )) {
                    HttpContext.Current
                        .Cache.Insert(strCacheKey, projList, null, DateTime.Now.AddHours(9), TimeSpan.Zero);
                }
                return projList;
            }
        }

        internal static List<TitleSearchParam> TitleSearchInProject(int projId, List<TitleSearchParam> tsList)
        {
            return tsList.Where(t => t.ProjectId == projId).ToList();
        }

        internal static project GetProjectByProjectId(int projectId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.projects.SingleOrDefault(EqualsToProjectId(projectId));
            }
        }

        // Project Unit Level June 7 2014
        internal static bool GetProjectUnitLevel(int projectId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var unitquery = from p in ctx.projects
                                where p.project_id == projectId && p.is_unit_level_used == true
                                select p;
                if (unitquery.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        internal static IEnumerable<string> GetInvalidIds(List<string> ids)
        {
            if (ids == null) return null;
            var tempIDs = ids.ConvertAll(Convert.ToInt32);
            using (var ctx = DataContextFactory.CreateContext()) {
                return
                    tempIDs.Where(i => ctx.projects.All(p => p.project_id != i)).ToList().
                        ConvertAll(Convert.ToString);
            }
        }

        // Owner Policy Escrow Key Las Vegas ONLY
        internal static bool IsLasVegasProjId (int projectId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                int[] lasvegasIds = { 66, 114, 116, 118 };
                List<int> projId  = lasvegasIds.Select(l => l).ToList();

                var lasvegasQuery =
                    from p in ctx.projects
                    where projId.Contains(projectId)
                    select p;

                if (lasvegasQuery.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    } 
}