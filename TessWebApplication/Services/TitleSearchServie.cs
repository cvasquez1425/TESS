#region Includes
using System.Data;
using System.Text;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.AdoNet;
using System;

#endregion

namespace Greenspoon.Tess.Services
{
    public class TitleSearchServie
    {
        public static DataTable DoTitleSearch(TitleSearchDTO ui)
        {
            // CB-03-2013 helpdesk ticket 177301 Change Request for title Searching issue with no legal names LEFT JOIN instead of INNER on legal_name
            var sb = new StringBuilder();
            sb.Append(
                 @" 
                    DECLARE @stat VARCHAR(MAX); 
                    SET @stat = (select system_values_info from system_values where system_values_id = 2) ;
                    select distinct top 100
	                    p.project_id,
	                    p.project_name,
	                    pn.phase_name,
                        case when Len(cy.web_site) > 7 then '<a href=""' + cy.web_site +'""  target=""'+'_blank"" > County </a>' else null end as [countywebsite] ,
	                    be.batch_escrow_id,
	                    c.contract_id,
	                    case when c.contract_active = 1 then 'Yes' else '<span style=""color: red;"">No</span>' end as [Active], 
	                    c.alternate_building,
	                    c.dev_k_num,
	                    c.deed_book,
	                    c.deed_page,
	                    c.mortgage_page,
	                    c.mortgage_book,
                        c.mortgage_date,
                        c.mortgage_amt,
	                    c.deed_recording_date,
                        c.partial_week,
                        c.color,
                        ISNULL(c.is_gvg, 0) as is_gvg,
                        case when ISNULL(c.is_gvg, 0) = 1 then '<span style=""color: white;font-weight:bold;"">DEVELOPER OWNED = NO</span>' else 'DEVELOPER OWNED = YES' end AS [isGVG],
	                    ln.last_name,
	                    ln.first_name,
	                    ln.zip,
	                    ci.contract_interval_id,
	                    ib.building_name,
	                    ib.building_code,
	                    ib.number_of_floors,
	                    iu.unit_number,
	                    cwm.week_number,
	                    cym.year_name,
	                    case 
	                      when f.bankrupt = 1  then 'Yes'
	                      else 'No'
	                      end AS [bankrupt],
	                    case when c.cancel = 1 then '<span style=""color: red;"">Yes</span>' else 'No' end AS [cancelled],
                       -- case when can.cancel_id is null then 1 else 0 end AS [CanAddToCancel],
                       case when f.hold = 1 then 'Yes' else 'No' end as FCHold,
                       '<a href=""' + @stat + CAST(c.contract_id AS VARCHAR) +'""  target=""'+'_blank"" >Status</a>' as  [statsummarylink]
                    from contract c 
                    inner join batch_escrow be
                        on c.batch_escrow_id = be.batch_escrow_id
                    inner join project p
                        on be.project_id  = p.project_id
                    left join contract_interval ci   
	                    on c.contract_id = ci.contract_id
                    left join county cy
                        on p.county_id = cy.county_id
                    left join phase_name as pn
                       on be.phase_name_id = pn.phase_name_id
                    left join cont_week_master cwm    
	                    on ci.cont_week_master_id = cwm.cont_week_master_id
                    left join cont_year_master cym 
	                    on ci.cont_year_master_id = cym.cont_year_master_id
                    Left join inventory_building ib 
	                    on ci.inventory_building_id = ib.inventory_building_id
                    Left join inventory_unit iu   
	                    on ci.inventory_unit_id = iu.inventory_unit_id	
                    join legal_name ln 
	                    on (ln.contract_id = c.contract_id and ln.[primary] = 1)
                    left join cancel can
                        on (c.contract_id = can.contract_id and can.cancel_active = 1)
                    left join foreclosure f
                        on (c.contract_id = f.contract_id and f.is_active = 1 )   where 1 = 1 ");
            if (!string.IsNullOrEmpty(ui.MasterId)) {
                sb.AppendLine(string.Format(" and c.contract_id = '{0}' ", int.Parse(ui.MasterId)));
            }
            if (!string.IsNullOrEmpty(ui.DevK.Trim())) {
                sb.AppendLine(string.Format(" and c.dev_k_num = '{0}' ", ui.DevK));
            }
            if (!string.IsNullOrEmpty(ui.AltBldg.Trim())) {
                sb.AppendLine(string.Format(" and c.alternate_building = '{0}' ", ui.AltBldg));
            }
            if (ui.Active) {
                sb.AppendLine(" and c.contract_active = 1 ");
            }
            if (!string.IsNullOrEmpty(ui.FullName))
            {
                var names = ui.FullName.SplitByWord();
                foreach (var name in names)
                {
                    sb.AppendLine(string.Format(" and ( ln.first_name like '%{0}%' or ln.last_name like '%{0}%' ) ", name));
                }
            }
            else { sb.Replace("join legal_name ln", "LEFT join legal_name ln"); }      // FullName IsNullOrEmpty add the LEFT OUTER JOIN
            if (!string.IsNullOrEmpty(ui.Zip)) {
                sb.AppendLine(string.Format(" and ln.zip like '{0}%' ", ui.Zip));
            }
            if (!string.IsNullOrEmpty(ui.ContWeekId)) {
                sb.AppendLine(string.Format(" and ci.cont_week_master_id = {0} ", ui.ContWeekId));
            }
            if (!string.IsNullOrEmpty(ui.ContYearId)) {
                sb.AppendLine(string.Format(" and ci.cont_year_master_id = {0} ", ui.ContYearId));
            }
            if (!string.IsNullOrEmpty(ui.UnitNumber)) {
                sb.AppendLine(string.Format(" and iu.unit_number = '{0}' ", ui.UnitNumber));
            }
            if (!string.IsNullOrEmpty(ui.BuildingCode)) {
                sb.AppendLine(string.Format(" and ib.building_code = '{0}' ", ui.BuildingCode));
            }
            if (!string.IsNullOrEmpty(ui.ProjectId)) {
                sb.AppendLine(string.Format(" and p.project_id = {0} ", ui.ProjectId));
            }
            if (!string.IsNullOrEmpty(ui.DeedBook)) {
                sb.AppendLine(string.Format(" and c.deed_book = '{0}' ", ui.DeedBook));
            }
            if (!string.IsNullOrEmpty(ui.DeedPage)) {
                sb.AppendLine(string.Format(" and c.deed_page = '{0}' ", ui.DeedPage));
            }
            if (!string.IsNullOrEmpty(ui.Color)) {
                sb.AppendLine(string.Format(" and c.color = '{0}' ", ui.Color));
            }
            if (!string.IsNullOrEmpty(ui.ProjectIdDb))
            {
                sb.AppendLine(string.Format(" and be.batch_escrow_number = '{0}' ", ui.ProjectIdDb));
            }
            if (!string.IsNullOrEmpty(ui.Phase))
            {
                sb.AppendLine(string.Format(" and be.phase_name_id = '{0}' ", ui.Phase));
            }
            sb.AppendLine(" ORDER BY c.contract_id DESC");
            return Db.GetDataTable(sb.ToString());
        }
    }
}