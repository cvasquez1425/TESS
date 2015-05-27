#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Greenspoon.Tess.Classes;
#endregion
namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class status_master
    {
        internal static Expression<Func<status_master, bool>> EqualsToStatusMasterId(int statusMasterId) {
            return s => s.status_master_id == statusMasterId;
        }
        internal static Expression<Func<status_master, bool>> NoFilter() {
            // Based on business rule.
            return s => s.status_master_id > 0;
        }
        internal static Expression<Func<status_master, bool>> BatchEscrowFilter() {
            return s => s.status_group_id    != 2  // Cancel
                        && s.status_group_id != 3  // Other
                        && s.status_group_id != 4  // Foreclosure-Mortgage
                        && s.status_group_id != 6; // Funding
        }
        internal static Expression<Func<status_master, bool>> BatchCancelFilter() {
            return s => s.status_group_id    != 1  // Escrow
                        && s.status_group_id != 3  // Other
                        && s.status_group_id != 4  // Foreclosure-Mortgage
                        && s.status_group_id != 6; // Funding
        }
        internal static Expression<Func<status_master, bool>> BatchForeclosureFilter() {
            return s => s.status_group_id    != 1  // Escrow
                        && s.status_group_id != 2  // Cancel
                        && s.status_group_id != 3  // Other
                        && s.status_group_id != 6; // Funding
        }
        internal static Expression<Func<status_master, bool>> GetStatusFilter(FormNameEnum f) {
             // The business logic has been deprecated.
            // return all value.
            //return f.isBatchEscrow() ? BatchEscrowFilter()
            //                         : f.isCancel()      ? BatchCancelFilter() 
            //                         : f.isForeclosure() ? BatchForeclosureFilter()
            //                         : NoFilter();
            return NoFilter();
        }
        internal static List<DropDownItem> GetStatusMasterList(FormNameEnum f = FormNameEnum.Unknown) {
            // Declare the return list.
            var groupList = new List<DropDownItem>();
            // Get the status filter condition 
            // based on the form type caller.
            var filter = GetStatusFilter(f);
            // Populate the list.
            using(var ctx = DataContextFactory.CreateContext()) {
                var groups = (from s in ctx.status_master.Where(filter)
                              select new
                              {
                                  Name  = s.status_master_name,
                                  Value = s.status_master_id
                              }).OrderBy(g => g.Value).ToList();
                // Build the list with in a groupList.
                groupList.AddRange(from @group in groups
                                   where @group.Name != null && @group.Name.Trim().Length > 0
                                   select new DropDownItem
                                              {
                                                  Name = string.Format("{1} {0}", @group.Name, @group.Value),
                                                  Value = @group.Value.ToString()
                                              });
                // if list has values.
                // insert an empty row.
                if(groupList.Any()) {
                    groupList.Insert(0, new DropDownItem());
                }
            }
            return groupList;
        } // end of GetStatus master list.

        internal static List<status_master> GetStatusMasters() {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.status_master.Include("status_group").ToList();
            }
        }
        
        internal static status_master GetStatusMaster(int statusMasterId) {
            using(var ctx = DataContextFactory.CreateContext()) {
                return ctx.status_master
                    .SingleOrDefault(EqualsToStatusMasterId(statusMasterId));
            }
        }
        internal static bool Save(status_master param) {
            var result = false;
            using(var ctx = DataContextFactory.CreateContext()) {
                if(param.status_master_id == 0) {
                    param.createddate = DateTime.Now;
                    ctx.AddTostatus_master(param);
                }
                else {
                    // Do Update.
                    var s = ctx.status_master
                               .SingleOrDefault(EqualsToStatusMasterId(param.status_master_id));
                    if(s != null) {
                        s.status_group_id           = param.status_group_id;
                        s.status_master_name	    = param.status_master_name;
                        s.status_master_active	    = param.status_master_active;
                        s.req_invoice	            = param.req_invoice;
                        s.req_datestamp	            = param.req_datestamp;
                        s.req_eff_date 	            = param.req_eff_date;
                        s.req_rec_date              = param.req_rec_date;
                        s.req_book	                = param.req_book;
                        s.req_page	                = param.req_page;
                        s.req_batch	                = param.req_batch;
                        s.req_county_name	        = param.req_county_name;
                        s.req_assign_num	        = param.req_assign_num;
                        s.req_original_county	    = param.req_original_county;
                        s.next	                    = param.next;
                        s.interval	                = param.interval;
                        s.is_comment                = param.is_comment;                // RIQ-289 CVJan2013
                        s.is_deleted_allowed        = param.is_deleted_allowed;        // RIQ-289 CVJan2013
                        s.is_cancel_escrow          = param.is_cancel_escrow;          // RIQ-308
                        s.is_legal_name_required    = param.is_legal_name_required;   // RIQ-308
                        s.comments                  = param.comments;
                    }
                }
                if(ctx.SaveChanges() > 0) {
                    result = true;
                }
                return result;
            }
        } // end of save

    }   // end of class
}

