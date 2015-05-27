#region Includes
using System;
using System.Linq;
using System.Linq.Expressions; 
#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class judge
    {
        internal static Expression<Func<judge, bool>> EqualsToJudgeId(int judgeId) {
            return j => j.judge_id == judgeId;
        }
        internal static judge GetJudge(int judgeId) {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.judges.SingleOrDefault(EqualsToJudgeId(judgeId));
            }
        } // end of GetJudge by Id
        internal static bool Save(judge param) {
            using(var ctx = DataContextFactory.CreateContext()) {
                var instance = param.judge_id > 0 
                              ? ctx.judges.SingleOrDefault(EqualsToJudgeId(param.judge_id)) 
                              : new judge();
                if(instance != null) {
                    instance.county_id                 = param.county_id;
                    instance.room                      = param.room;
                    instance.division                  = param.division;
                    instance.judge_name                = param.judge_name;
                    instance.judge_last_name           = param.judge_last_name;
                    instance.phone                     = param.phone;
                    instance.judge_active              = param.judge_active;
                    instance.document_group_id         = param.document_group_id;
                    instance.createdby                 = param.createdby;
                    instance.createddate               = param.createddate;
                }
                // If insert mode then add to the table.
                if(param.judge_id == 0) {
                    ctx.AddTojudges(instance);
                }

                var result = false;
                if(ctx.SaveChanges() > 0) {
                    result = true;
                }
                // return if Save is successful or not.
                return result;        
            }
        } // end of save

    } // end of class.
}