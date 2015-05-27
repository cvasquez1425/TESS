#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using SearchKit;
#endregion

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class legal_name
    {
        internal static Expression<Func<legal_name, bool>> EqualsToContractId(int contractId)
        {
            return c => c.contract_id == contractId;
        }

        internal static Expression<Func<legal_name, bool>> EqualsToLegalNameId(int legalNameId)
        {
            return l => l.legal_name_id == legalNameId;
        }

        internal static Expression<Func<legal_name, bool>> IsPrimary()
        {
            return l => l.primary == true;
        }

        // RIQ-XYZ Shirley's Request to sort by legal_name_active on 6/14/2013 Friday, 
        internal static Expression<Func<legal_name, bool>> isLegalActive()
        {
            return l => l.legal_name_active == true;
        }

        internal static Expression<Func<legal_name, bool>> ContainsInName(string legalName)
        {
            // This is shortcut to build Func<T,bool>.
            // User False if doing OR.
            var predicate = PredicateBuilder.True<legal_name>();
            // Get all the name that was supplied by caller.
            // Trim was done incase user uses spaces before or after.
            List<string> names = legalName.Trim().SplitByWord();
            // Build the predicate dynamically for each name.
            return names.Aggregate(predicate, (current, tempName) => current.And(n => n.first_name.StartsWith(tempName) || n.last_name.StartsWith(tempName)));
        }

        internal static Expression<Func<legal_name, bool>> StartsWithZip(string zip)
        {
            return name => name.zip.StartsWith(zip);
        }

        internal static List<TitleSearchParam> SearchInName(TitleSearchDTO ui, List<TitleSearchParam> tsList)
        {
            var newResult = new List<TitleSearchParam>();
            var NameAndZipPredicate = ContainsInName(ui.FullName).And(StartsWithZip(ui.Zip));
            using (var ctx = DataContextFactory.CreateContext()) {
                if (tsList != null && tsList.Any()) {
                    foreach (var ts in tsList) {
                        int tempConId = ts.ContractId;
                        var tempPredicate = NameAndZipPredicate.And(EqualsToContractId(tempConId));
                        var temp = ctx.legal_name.AsExpandable()
                            .Where(tempPredicate);
                        foreach (var l in temp) {
                            var tsp = new TitleSearchParam {
                                ContractId = l.contract_id,
                                BatchEscrowId       = l.contract.batch_escrow_id,
                                ProjectId           = l.contract.batch_escrow.project_id,
                                Contract_IntervalId = ts.Contract_IntervalId,
                                LegalNameId         = l.legal_name_id
                            };
                            newResult.Add(tsp);
                        }
                    }
                }
                else {
                    newResult = ctx.legal_name.AsExpandable()
                                    .Where(NameAndZipPredicate)
                                    .Select(l => new TitleSearchParam {
                                        ContractId    = l.contract_id,
                                        BatchEscrowId = l.contract.batch_escrow_id,
                                        ProjectId     = l.contract.batch_escrow.project_id,
                                        LegalNameId   = l.legal_name_id
                                    }).ToList();
                }
                return newResult;
            }
        }

        internal static List<LegalNamesDTO> GetLegalNameUIList(int contractId)
        {
            var legalNameList = new List<LegalNamesDTO>();
            using (var ctx = DataContextFactory.CreateContext()) {
                // Get the list of legal names. 
                var ln = ctx.legal_name
                            .Where(EqualsToContractId(contractId))
                            .OrderByDescending(IsPrimary())
                            .OrderByDescending(isLegalActive());
                foreach (var l in ln) {
                    var ui = 
                        new LegalNamesDTO {
                            LegalNameId = l.legal_name_id.ToString(),
                            ContractId  = l.contract_id.ToString(),
                            Name        = string.Format("{0} {1}", l.first_name, l.last_name),
                            FirstName   = l.first_name,
                            LastName    = l.last_name,
                            Address1    = l.street_address1,
                            Address2    = l.street_address2,
                            Address3    = l.street_address3,
                            Address23   = string.Format("{0} {1}", l.street_address2, l.street_address3),
                            City        = l.city,
                            State       = l.state,
                            Zip         = l.zip,
                            Country     = l.country_id.HasValue 
                                                                ? l.country.country_name : string.Empty,
                            CountryId   = l.country_id.HasValue 
                                                                ? l.country_id.ToString() : string.Empty,
                            Phone       = l.phone,
                            Email       = l.email,
                            Dismiss     = l.fc_dismiss ?? false,
                            Order       = l.fc_order.ToString(),
                            Active      = l.legal_name_active ?? false,
                            Primary     = l.primary ?? false,
                            CreatedBy   = l.createdby,
                            CreatedDate = l.createddate.ToDateOnly()
                        };
                    legalNameList.Add(ui);
                }
                return legalNameList;
            }
        }

        // RIQ-323 zipcode lookup
        internal static string[] lookupZipCode(string zipcode)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var query = ctx.zipcodes.Where(z => z.zip_code_id == zipcode)
                            .Select(zs => new
                            {
                                postalCity  = zs.zip_city,
                                postalState = zs.zip_state                                
                            });
                foreach (var zip in query) 
                {
                    string[] zipStates = {zip.postalCity, zip.postalState};
                    return zipStates;
                }
                string[] zipstates= {" ", " "};
                return zipstates;
            }            
        }

        //      RIQ-316 Only allow 1 primary flag in legal name per contract
        internal static LegalNamesDTO GetPrimaryOnLegal_Name(int recID)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                LegalNamesDTO ui = null;
                if (ctx.legal_name.Exists(EqualsToLegalNameId(recID).And(IsPrimary())))
                {
                    int legalNameId = ctx.legal_name
                                         .FirstOrDefault(EqualsToLegalNameId(recID))
                                         .legal_name_id;
                    if (legalNameId > 0)
                    {
                        ui = GetLegalNameDTO(legalNameId);
                    }
                }
                return ui;
            }
        }
        //      RIQ-316 Only allow 1 primary flag in legal name per contract
        internal static int GetAnyPrimaryLegalName(int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                //                LegalNamesDTO ui = null;
                if (ctx.legal_name.Exists(EqualsToContractId(contractId).And(IsPrimary())))
                {
                    int legalNameId = ctx.legal_name
                                         .FirstOrDefault(EqualsToContractId(contractId))
                                         .legal_name_id;
                    if (legalNameId > 0)
                    {
                        return legalNameId;
                    }
                }
                return 0;
            }
        }
        //      RIQ-316 -----------------------------------------------------------------------------------

        internal static legal_name GetPrimaryLegalName(int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                return ctx.legal_name.AsExpandable()
                           .SingleOrDefault(EqualsToContractId(contractId).And(IsPrimary()));
            }
        }

        internal static LegalNamesDTO GetAnyLegalNameDTO(int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                LegalNamesDTO ui = null;
                if (ctx.legal_name.Exists(EqualsToContractId(contractId))) {
                    int legalNameId = ctx.legal_name
                                         .FirstOrDefault(EqualsToContractId(contractId))
                                         .legal_name_id;
                    if (legalNameId > 0) {
                        ui = GetLegalNameDTO(legalNameId);
                    }
                }
                return ui;
            }
        }

        internal static LegalNamesDTO GetLegalNameDTO(int legalNameId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var l = ctx.legal_name
                           .SingleOrDefault(EqualsToLegalNameId(legalNameId));
                if (l != null) {
                    var ui = new LegalNamesDTO {
                        LegalNameId = l.legal_name_id.ToString(),
                        ContractId  = l.contract_id.ToString(),
                        Name        = string.Format("{0} {1}", l.first_name, l.last_name),
                        FirstName   = l.first_name,
                        LastName    = l.last_name,
                        Address1    = l.street_address1,
                        Address2    = l.street_address2,
                        Address3    = l.street_address3,
                        Address23   = string.Format("{0} {1}", l.street_address2, l.street_address3),
                        City        = l.city,
                        State       = l.state,
                        Zip         = l.zip,
                        Country     = l.country_id.HasValue 
                                            ? l.country.country_name : string.Empty,
                        CountryId   = l.country_id.HasValue 
                                            ? l.country_id.ToString() : string.Empty,
                        Phone       = l.phone,
                        Email       = l.email,
                        Dismiss     = l.fc_dismiss ?? false,
                        Order       = l.fc_order.ToString(),
                        Active      = l.legal_name_active ?? false,
                        Primary     = l.primary ?? false,
                        CreatedBy   = l.createdby,
                        CreatedDate = l.createddate.ToDateOnly(),
                    };
                    return ui;
                }
                return null;
            }
        }

        internal static string GetFullLegalName(int legalNameId)
        {
            var fullName = string.Empty;
            using (var ctx = DataContextFactory.CreateContext()) {
                var l = ctx.legal_name.SingleOrDefault(EqualsToLegalNameId(legalNameId));
                if (l != null) {
                    fullName = string.Format("{0} {1}", l.first_name, l.last_name);
                }
                return fullName;
            }
        }

        internal static List<DropDownItem> GetLegalNameDropDownList(int contractId)
        {
            var legalNameList = new List<DropDownItem>();
            using (var db = DataContextFactory.CreateContext()) {
                var names = ( from n in db.legal_name
                              .Where(EqualsToContractId(contractId))
                              orderby n.last_name ascending
                              select new {
                                  FName = n.first_name,
                                  LName = n.last_name,
                                  Value = n.legal_name_id
                              } ).ToList();
                legalNameList.AddRange(names.Select(item => new DropDownItem {
                    Name = string.Format("{0} {1}", item.FName, item.LName),
                    Value = item.Value.ToString()
                }));
                legalNameList.Insert(0, new DropDownItem());
            }
            return legalNameList;
        }

        internal static bool Save(LegalNamesDTO ui)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                int id = Convert.ToInt32(ui.LegalNameId);
                var l = id > 0
                    ? ctx.legal_name.SingleOrDefault(EqualsToLegalNameId(id))
                    : new legal_name();
                // Resale Forclosure Orlando
                bool neworUpdRecord;
                var legalFound = from le in ctx.legal_name.Where(le => le.legal_name_id == id)
                                    select le;
                if (legalFound.Any()) { neworUpdRecord = true; } else { neworUpdRecord = false; }

                l.contract_id       = Convert.ToInt32(ui.ContractId);
                l.first_name        = ui.FirstName.NullIfEmpty<string>();
                l.last_name         = ui.LastName.NullIfEmpty<string>();
                l.street_address1   = ui.Address1.NullIfEmpty<string>();
                l.street_address2   = ui.Address2.NullIfEmpty<string>();
                l.street_address3   = ui.Address3.NullIfEmpty<string>();
                l.city              = ui.City.NullIfEmpty<string>();
                l.state             = ui.State.NullIfEmpty<string>();
                l.zip               = ui.Zip.NullIfEmpty<string>();
                l.country_id        = ui.CountryId.NullIfEmpty<int?>();
                l.phone             = ui.Phone.NullIfEmpty<string>();
                l.email             = ui.Email.NullIfEmpty<string>();
                l.fc_dismiss        = ui.Dismiss;
                l.fc_order          = ui.Order.NullIfEmpty<int?>();
                l.legal_name_active = ui.Active;
                l.primary           = ui.Primary;
                l.createdby         = ui.CreatedBy.NullIfEmpty<string>();
                // Resale Forclosure Orlando
                l.modifieddate      = neworUpdRecord ? DateTime.Now : l.modifieddate = null;
                l.modifiedby        = neworUpdRecord ? ui.ModifyBy  : l.modifiedby   = null;

                if (l.primary == true) {
                    RemovePrimaryFlags(l.contract_id);
                }
                if (id == 0) {
                    l.createddate   = DateTime.Now;
                    ctx.AddTolegal_name(l);
                }
                return ctx.SaveChanges() > 0;
            }
        }

        static void RemovePrimaryFlags(int contractId)
        {
            using (var ctx = DataContextFactory.CreateContext()) {
                var names = ctx.legal_name
                               .Where(EqualsToContractId(contractId));
                if (!names.Any()) return;
                foreach (var name in names) {
                    name.primary = false;
                }
                ctx.SaveChanges();
            }
        }
    }
}