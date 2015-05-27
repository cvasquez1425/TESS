#region Includes
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Greenspoon.Tess.Classes;
#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class partner
    {
        internal static Expression<Func<partner, bool>> EqualsToPartnerId(int partnerId)
        {
            return p => p.partner_id == partnerId;
        }

        internal static Expression<Func<partner, bool>> EqualsToProjectId(int projectId)
        {
            return p => p.project_id.Equals(projectId);
        }

        internal static partner GetPartner(int partnerid)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.partners.SingleOrDefault(EqualsToPartnerId(partnerid));
            }
        }

        internal static List<DropDownItem> GetPartnerByProject(int projectId)
        {
            var partnerList = new List<DropDownItem>();
            using (var ctx = DataContextFactory.CreateContext()) {
                var tempList = from pa in ctx.partners
                               where pa.project_id == projectId
                               select new {
                                   Name = pa.partner_name,
                                   Value = pa.partner_id
                               };
                foreach (var t in tempList) {
                    if(string.IsNullOrEmpty(t.Name.Trim()) || string.IsNullOrWhiteSpace(t.Name))
                        continue;
                    partnerList.Add(new DropDownItem { Name = t.Name, Value = t.Value.ToString() });
                }

                partnerList.Insert(0, new DropDownItem());
                return partnerList.ToList();
            }
        }

        internal static bool Save(partner pa)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                partner partner = pa.partner_id > 0 
                              ? ctx.partners.SingleOrDefault(EqualsToPartnerId(pa.partner_id)) 
                              : new partner();
                if (partner != null) {
                    partner.project_id   = pa.project_id;
                    partner.partner_name = pa.partner_name;
                    partner.createdby    = pa.createdby;
                    partner.createddate  = pa.createddate;
                }

                if (pa.partner_id == 0) {
                    ctx.AddTopartners(partner);
                }
                return ctx.SaveChanges() > 0;
            }
        }
    }
}