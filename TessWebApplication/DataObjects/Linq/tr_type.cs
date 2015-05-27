#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Greenspoon.Tess.Classes;
#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class tr_type
    {
        internal static Expression<Func<status_master, bool>> EqualsToStatusMasterId(int statusMasterId)
        {
            return s => s.status_master_id == statusMasterId;
        }
        internal static Expression<Func<tr_type, bool>> NoFilter()
        {
            // Based on business rule.
            return t => t.tr_type_id > 0;
        }
        internal static Expression<Func<tr_type, bool>> BatchEscrowFilter()
        {
            return t => t.tr_type_id != 2  // Cancel
                        && t.tr_type_id != 3  // Other
                        && t.tr_type_id != 4  // Foreclosure-Mortgage
                        && t.tr_type_id != 6; // Funding
        }
        internal static Expression<Func<tr_type, bool>> BatchCancelFilter()
        {
            return t => t.tr_type_id != 1  // Escrow
                        && t.tr_type_id != 3  // Other
                        && t.tr_type_id != 4  // Foreclosure-Mortgage
                        && t.tr_type_id != 6; // Funding
        }
        internal static Expression<Func<tr_type, bool>> BatchForeclosureFilter()
        {
            return t => t.tr_type_id != 1  // Escrow
                        && t.tr_type_id != 2  // Cancel
                        && t.tr_type_id != 3  // Other
                        && t.tr_type_id != 6; // Funding
        }
        internal static Expression<Func<tr_type, bool>> GetStatusFilter(FormNameEnum f)
        {
            // The business logic has been deprecated.
            // return all value.
            //return f.isBatchEscrow() ? BatchEscrowFilter()
            //                         : f.isCancel()      ? BatchCancelFilter() 
            //                         : f.isForeclosure() ? BatchForeclosureFilter()
            //                         : NoFilter();
            return NoFilter();
        }

    }
}