#region Includes
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Greenspoon.Tess.Classes;
#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class phase_detail
    {
        internal static Expression<Func<phase_detail, bool>> EqualsToPhaseDetailId(int phaseDetailId) {
            return p => p.phase_detail_id == phaseDetailId;
        }
        internal static List<phase_detail> GetPhaseDetails() {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.phase_detail.Include("project").Include("phase_name").ToList();
            }
        }
        internal static phase_detail GetPhaseDetail(int phaseDetailId) {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.phase_detail.SingleOrDefault(EqualsToPhaseDetailId(phaseDetailId));
            }
        }
        public static List<DropDownItem> GetPhaseListByProjectId(int projId) {
            var phaseList = new List<DropDownItem>();
            using(var ctx = DataContextFactory.CreateContext()) {
                var phases = (from pd in ctx.phase_detail
                              where pd.project_id == projId
                              select new
                              {
                                  Name  = pd.phase_name.phase_name1,
                                  Text  = pd.phase_name.text,
                                  Value = pd.phase_name_id
                              }).ToList();

                foreach(var item in phases) {
                    if(item.Name != null && item.Name.Trim().Length > 0) {
                        phaseList.Add(new DropDownItem
                        {
                            Name  = string.Format("{0} [ {1} ]", item.Text, item.Name),
                            Value = item.Value.ToString()
                        });
                    }
                }
                if(phaseList.Any() == true) {
                    phaseList.Insert(0, new DropDownItem());
                }
            }
            return phaseList;
        }
        internal static bool Save(phase_detail param) {
            var result = false;
            using(var ctx = DataContextFactory.CreateContext()) {
                if(param.phase_detail_id == 0) {
                    param.createddate = DateTime.Now;
                    ctx.AddTophase_detail(param);
                }
                else {
                    // Do Update.
                    var p = ctx.phase_detail
                               .SingleOrDefault(EqualsToPhaseDetailId(param.phase_detail_id));
                    if(p != null) {
                        p.project_id      = param.project_id;
                        p.phase_name_id   = param.phase_name_id;
                        p.phs_or_book     = param.phs_or_book;
                        p.phs_or_page     = param.phs_or_page;
                        p.createdby       = param.createdby;
                    }
                    else { return false; }
                }
                if(ctx.SaveChanges() > 0) {
                    result = true;
                }
                return result;
            }
        } // end of save.

    }// end of class
}