#region Includes
using System;
using System.Linq;
using System.Linq.Expressions;
using Greenspoon.Tess.BusinessObjects.UIObjects;
#endregion
namespace Greenspoon.Tess.DataObjects.Linq {
    public partial class project_link {
        internal static Expression<Func<project_link, bool>> EqualsToBatchCancelId(int batchCancelId) {
            return l => l.batch_cancel_id == batchCancelId;
        }
        internal static bool Save(BatchCancelDTO ui) {
            int batchCancelId   = int.Parse(ui.BatchCancelId);
            int projectId       = int.Parse(ui.ProjectId);
            int parentProjectId = int.Parse(ui.ParentProjectId);
            using(var ctx = DataContextFactory.CreateContext()) {
                project_link projectLink = null;
                projectLink = ctx.project_link
                                 .FirstOrDefault(EqualsToBatchCancelId(batchCancelId));
                if(projectLink == null) {
                    projectLink = new project_link();
                    ctx.AddToproject_link(projectLink);
                }
                projectLink.batch_cancel_id   = batchCancelId;
                projectLink.child_project_id  = projectId;
                projectLink.parent_project_id = parentProjectId;
                if(ctx.SaveChanges() > 0) {
                    return true;
                }
                else { return false; }
            }
        }
    } // end of class
}