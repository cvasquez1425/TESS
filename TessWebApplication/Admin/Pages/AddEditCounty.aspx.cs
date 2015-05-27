#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages {
    public partial class AddEditCounty : PageBase {
        protected void Page_Load(object sender, EventArgs e) {
            SetPageBase();
            if(IsPostBack == false) {
                BindStates();
                if(PageMode == PageModeEnum.Edit && RecID > 0) {
                    SetupEditForm();
                }
                else {
                    lblCreateBy.Text   = UserName;
                    lblCreateDate.Text = DateTime.Now.ToDateOnly();
                }
            }
        } 

        void BindStates() {
            var data = state.GetStateList();
            drpState.DataSource = data;
            drpState.DataBind();
        }
        void SetupEditForm() {
            var c = county.GetCounty(RecID);
            if (c == null) return;
            txtCountyName.Text          = c.county_name;
            txtCircuit.Text             = c.county_circuit;
            txtAddress1.Text            = c.address1;
            txtAddress2.Text            = c.address2;
            txtCity.Text                = c.city;
            txtZip.Text                 = c.zip;
            drpState.SelectedValue      = c.state_id.ToString();
            txtClerk.Text               = c.clerk;
            txtPhone1.Text              = c.phone1;
            txtPhone2.Text              = c.phone2;
            txtPhone3.Text              = c.phone3;
            txtNews.Text                = c.news;
            txtNewsAddress1.Text        = c.news_address1;
            txtNewsAddress2.Text        = c.news_address2;
            txtNewsCityStZip.Text       = c.news_citystzip;
            txtGSA.Text                 = c.gsa;
            txtDefenseText.Text         = c.defense_text;
            txtDisabilityText.Text      = c.disability_text;
            txtProof.Text               = c.proof;
            txtPublication.Text         = c.publication;
            txtForward.Text             = c.forward;
            txtNOALetterP6.Text         = c.NOA_Letter_P6;
            txtAliasLetter.Text         = c.alias_letter;
            txtClerkLetter.Text         = c.clerk_letter;
            txtSecondDefaultLetter.Text = c.second_default_letter;
            txtClerkSigText.Text        = c.clerk_sig_text;
            txtSaleType.Text            = c.sale_type;
            txtEffective.Text           = c.effective_date.ToDateOnly();
            lblCreateDate.Text          = c.createddate.ToDateOnly();
            lblCreateBy.Text            = c.createdby;
            txtWebSite.Text             = c.web_site;
            txtWebComments.Text         = c.web_comments;
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            if(Save() == true) {
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Failed"; }
        }

        bool Save() {
            var c                       = new county {
                county_id               = RecID,
                county_name	            = txtCountyName.Text.NullIfEmpty<string>(),
                county_circuit	        = txtCircuit.Text.NullIfEmpty<string>(),
                address1	            = txtAddress1.Text.NullIfEmpty<string>(),
                address2	            = txtAddress2.Text.NullIfEmpty<string>(),
                city	                = txtCity.Text.NullIfEmpty<string>(),
                zip	                    = txtZip.Text.NullIfEmpty<string>(),
                state_id	            = Convert.ToInt32(drpState.SelectedValue),
                clerk	                = txtClerk.Text.NullIfEmpty<string>(),
                phone1	                = txtPhone1.Text.NullIfEmpty<string>(),
                phone2	                = txtPhone2.Text.NullIfEmpty<string>(),
                phone3	                = txtPhone3.Text.NullIfEmpty<string>(),
                news	                = txtNews.Text.NullIfEmpty<string>(),
                news_address1	        = txtNewsAddress1.Text.NullIfEmpty<string>(),
                news_address2	        = txtNewsAddress2.Text.NullIfEmpty<string>(),
                news_citystzip	        = txtNewsCityStZip.Text.NullIfEmpty<string>(),
                gsa	                    = txtGSA.Text.NullIfEmpty<string>(),
                defense_text	        = txtDefenseText.Text.NullIfEmpty<string>(),
                disability_text	        = txtDisabilityText.Text.NullIfEmpty<string>(),
                proof	                = txtProof.Text.NullIfEmpty<string>(),
                publication	            = txtPublication.Text.NullIfEmpty<string>(),
                forward	                = txtForward.Text.NullIfEmpty<string>(),
                NOA_Letter_P6	        = txtNOALetterP6.Text.NullIfEmpty<string>(),
                alias_letter	        = txtAliasLetter.Text.NullIfEmpty<string>(),
                clerk_letter	        = txtClerkLetter.Text.NullIfEmpty<string>(),
                second_default_letter	= txtSecondDefaultLetter.Text.NullIfEmpty<string>(),
                clerk_sig_text	        = txtClerkSigText.Text.NullIfEmpty<string>(),
                sale_type	            = txtSaleType.Text.NullIfEmpty<string>(),
                effective_date  	    = txtEffective.Text.NullIfEmpty<DateTime?>(),
                createdby	            = lblCreateDate.Text.NullIfEmpty<string>(),
                web_site	            = txtWebSite.Text.NullIfEmpty<string>(),
                web_comments	        = txtWebComments.Text.NullIfEmpty<string>(),
            };

            var result = county.Save(c);
            return result;
        }

        #region Util
     
        void RegisterThickBoxCloseScript() {
            Page.ClientScript.RegisterStartupScript(GetType(), "closeThickBox", "self.parent.FormSetup_Updated();", true);
        }
     
        void SetPageBase() {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            if(PageMode == PageModeEnum.Edit) {
                RecID = Request.QueryString.GetValue<int>("id");
            }
        }
        #endregion
    } 
}