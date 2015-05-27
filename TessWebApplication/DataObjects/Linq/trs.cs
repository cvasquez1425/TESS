#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using SearchKit;
#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class trs
    {

        internal static Expression<Func<trs, bool>> EqualsToResaleId(int contractId)
        {
            return r => r.contract_id == contractId;
        }

        public static List<TrsDTO> GetResaleForeclosureUIList(FormNameEnum f, int contractId)
        {
            var resaleList = new List<TrsDTO>();
            var filter = tr_type.GetStatusFilter(f);
            var resale = GetFilterResaleList(filter, contractId);
            if ((resale != null) && (resale.Any() == true))
            {
                resaleList.AddRange(resale.Select(r => GetResaleDTO(r.contract_id)).Where(ui => ui != null));
                return resaleList;
            }
            return resaleList;
        }

        internal static List<trs> GetFilterResaleList(Expression<Func<tr_type, bool>> filter, int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var queryResale =
                    (
                        from r in ctx.trs
                            .Include("tr_type")
                            .Include("tr_status")
                            .AsExpandable()
                        let g = ctx.tr_type
                               .Where(tt => tt.tr_type_id == r.tr_type_id)
                        where g.Any(filter)
                        where r.contract_id == contractId
                        select r);
                return queryResale.ToList();
            }
        }

        internal static TrsDTO GetResaleDTO(int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var r = ctx.trs
                        .SingleOrDefault(EqualsToResaleId(contractId));
                if ( r == null) return null;

                var rs = new TrsDTO
                {
                  TrId    =   r.tr_id,
                  TypeId  =   r.tr_type_id.ToString(),
                  StartDate = r.tr_start_date.ToDateOnly(),
                  CreatedDate = r.createddate.ToString(),
                  CreatedBy   = r.createdby,
                  StatusId    = r.tr_status_id.ToString(),
                  ContractId  = r.contract_id.ToString(),
                  ContPriceAmt = r.tr_cont_price_amt.HasValue == true
                                    ? Convert.ToString(r.tr_cont_price_amt )
                                    : null,
                  MortgageAmt  = r._tr_mortgage_amt.HasValue == true
                                    ? Convert.ToString(r._tr_mortgage_amt)
                                    : null,
                  DocPrepDate  = r.docs_prep_date.HasValue == true
                                 ? r.docs_prep_date.ToDateOnly()
                                 : null,
                  ExtraNames    = r.extra_names.HasValue == true
                                 ? Convert.ToString(r.extra_names)
                                 : null,
                  ExtraPages    = r.extra_pages.HasValue == true
                                 ? Convert.ToString(r.extra_pages)
                                 : null,
                 IsTrPendingBuyer   = r.Is_tr_pending_buyer.HasValue == true
                                 ?  r.Is_tr_pending_buyer.Value : r.Is_tr_pending_buyer.GetValueOrDefault(),

                 IsTrPendingSeller  = r.Is_tr_pending_seller.HasValue == true
                                 ?  r.Is_tr_pending_seller.Value : r.Is_tr_pending_seller.GetValueOrDefault(),

                 IsTrFinalBuyer     = r.Is_tr_final_buyer.HasValue == true
                                 ? r.Is_tr_final_buyer.Value : r.Is_tr_final_buyer.GetValueOrDefault(),

                 IsTrFinalSeller    = r.Is_tr_final_seller.HasValue == true
                                 ?  r.Is_tr_final_seller.Value : r.Is_tr_final_seller.GetValueOrDefault()
                };

                return rs;
            }
        }

    }
}