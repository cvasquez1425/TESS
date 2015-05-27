#region Includes
using System;
using System.Linq;
using System.Linq.Expressions;
using SearchKit;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using System.Collections.Generic;
using System.Text;
using System.Data.Objects.DataClasses;
#endregion
namespace Greenspoon.Tess.DataObjects.Linq {
    public partial class cancel_extra {
        internal static Expression<Func<cancel_extra, bool>> EqualsToCancelId(int cancelId) {
            return c => c.cancel_id == cancelId;
        }
        internal static Expression<Func<cancel_extra, bool>> EqualsToCancelExtraId(int cancelExtraId) {
            return c => c.cancel_extra_id == cancelExtraId;
        }
        internal static CancelExtraDTO GetCancelExtraUI(int cancelExtraId) {
            using(var ctx = DataContextFactory.CreateContext()) {
                var ce = ctx.cancel_extra
                            .SingleOrDefault(EqualsToCancelExtraId(cancelExtraId));
                if(ce != null) {
                    var ui = new CancelExtraDTO {
                        CancelExtraId        = ce.cancel_extra_id.ToString(),
                        CancelExtraTypeId    = ce.cancel_extra_type_id.ToString(),
                        CancelId             = ce.cancel_id.ToString(),
                        CancelExtraTypeValue = ce.cancel_extra_type.cancel_extra_type_value,
                        Names                = ce.Names.ToString(),
                        Pages                = ce.Pages.ToString()

                    };
                    return ui;
                }
                else { return null; }
            }
        }
        internal static List<CancelExtraDTO> GetCancelExtraUIList(int cancelId) {
            var list = new List<CancelExtraDTO>();
            using(var ctx = DataContextFactory.CreateContext()) {
                var exList = ctx.cancel_extra
                                 .Where(EqualsToCancelId(cancelId)); // TODO: verify if TOList calls throws the context off.
                if(exList != null && exList.Any() == true) {
                    CancelExtraDTO ui;
                    foreach(var ex in exList) {
                        ui = new CancelExtraDTO() {
                            CancelExtraId        = ex.cancel_extra_id.ToString(),
                            CancelExtraTypeId    = ex.cancel_extra_type_id.ToString(),
                            CancelExtraTypeValue = ex.cancel_extra_type.cancel_extra_type_value,
                            CancelId             = ex.cancel_id.ToString(),
                            Names                = ex.Names.ToString(),
                            Pages                = ex.Pages.ToString()
                        };
                        list.Add(ui);
                    } // end foreach loop.
                    return list;
                } // end of empty data check
                else { return null; }
            } // end of context.
        } // end of UI List
        internal static bool Save(CancelExtraDTO ui) {
            using(var ctx = DataContextFactory.CreateContext()) {
                // Decide if this is Edit or new.
                var ceId = 0;
                cancel_extra ce;
                // Try to get cancel extra id.
                // If an id is available. page is Edit mode.
                int.TryParse(ui.CancelExtraId, out ceId);

                if(ceId > 0) {
                    ce = ctx.cancel_extra
                            .SingleOrDefault(EqualsToCancelExtraId(ceId));
                }
                else {
                    ce = new cancel_extra();
                }
                if(ce != null) {
                    ce.cancel_extra_type_id = int.Parse(ui.CancelExtraTypeId);
                    ce.Pages                = ui.Pages.NullIfEmpty<int?>();
                    ce.Names                = ui.Names.NullIfEmpty<int?>();
                    ce.cancel_id            = int.Parse(ui.CancelId);
                    ce.createdby            = ui.CreatedBy;
                    ce.createddate          = DateTime.Now;
                }
                if(ceId == 0) {
                    ctx.AddTocancel_extra(ce);
                }
                // variable to hold return value.
                bool result = false;
                if(ctx.SaveChanges() > 0) {
                    result = true;
                }
                // Return fail or success.
                return result;
            } // end of context.
        } // end of method Save.

    } // end of class
}