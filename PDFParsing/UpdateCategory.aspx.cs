using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFParsing
{
    public partial class UpdateCategory : System.Web.UI.Page
    {
        PDFParseDataContext dbcontext = new PDFParseDataContext();
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (Session["DOCUMENT_ID"] == null)
                    {
                        Response.Redirect("/MyDocuments.aspx");
                    }
                    else
                    {
                        dt = new DataTable();
                        int DOCUMENT_ID = Convert.ToInt32(Session["DOCUMENT_ID"].ToString());
                        BindPARSER_DOCUMENT(DOCUMENT_ID);
                        BIND_USER_CATEGORY(DOCUMENT_ID);
                    }
                }
                else
                {
                    Response.Redirect("/Default.aspx");
                }
            }
        }

        private void BIND_USER_CATEGORY(int DOCUMENTID)
        {
            int DOCUMENT_ID = Convert.ToInt32(Session["DOCUMENT_ID"].ToString());
            DOCUMENT doc = dbcontext.DOCUMENTs.FirstOrDefault(x => x.DOCUMENT_ID == DOCUMENT_ID);
            if (doc != null)
            {
                string doctype = doc.DOCUMENT_TYPE;
                IEnumerable<USER_CATEGORY> UCat = dbcontext.USER_CATEGORies.Where(x => x.USER_ID == Convert.ToInt32(User.Identity.Name) && x.DOCUMENT_TYPE.ToLower() == doctype.ToLower());
                ddlCategory.DataSource = UCat;
                ddlCategory.DataTextField = "NAME";
                ddlCategory.DataValueField = "ID";
                ddlCategory.DataBind();
                ddlCategory.Items.Add(new ListItem("UnCategorized", "0"));
            }
        }

        private void BindPARSER_DOCUMENT(int DOCUMENT_ID)
        {
            int PARSER_ID = Convert.ToInt32(Session["PARSER_ID"].ToString());
            int Srno = 1;
            var sheet = from a in dbcontext.PARSER_SHEETs.AsEnumerable().Select((r, i) => new { Row = r, Index = i }).Where(x => x.Row.PARSER_ID == PARSER_ID).ToList()
                        select new
                        {
                            ID = a.Row.SHEET_ID,
                            INDEX = Srno++,
                            DATE = a.Row.DATE.Value.Day + "/" + a.Row.DATE.Value.Month + "/" + a.Row.DATE.Value.Year,
                            VALUE_DATE = a.Row.DATE.Value.Day + "/" + a.Row.DATE.Value.Month + "/" + a.Row.DATE.Value.Year,
                            NARRATION = a.Row.NARRATION,
                            CHQ_NO = a.Row.CHQ_NO,
                            WITHDRAWAL_AMOUNT = a.Row.WITHDRAWAL_AMOUNT == null ? "" : a.Row.WITHDRAWAL_AMOUNT.ToString(),
                            DEPOSIT_AMOUNT = a.Row.DEPOSIT_AMOUNT == null ? "" : a.Row.DEPOSIT_AMOUNT.ToString(),
                            CLOSING_BALANCE = a.Row.CLOSING_BALANCE,
                            CATEGORY_ID = a.Row.CATEGORY_ID,
                            CATEGORY_NAME = getCategoryName(Convert.ToInt32(a.Row.CATEGORY_ID)),
                        };
            rptSHEETS.DataSource = sheet;
            rptSHEETS.DataBind();
        }

        private string getCategoryName(int id)
        {
            string name = "";
            int DOCUMENT_ID = Convert.ToInt32(Session["DOCUMENT_ID"].ToString());
            DOCUMENT doc = dbcontext.DOCUMENTs.FirstOrDefault(x => x.DOCUMENT_ID == DOCUMENT_ID);
            if (doc != null)
            {
                string doctype = doc.DOCUMENT_TYPE;
                USER_CATEGORY UCat = dbcontext.USER_CATEGORies.FirstOrDefault(x => x.USER_ID == Convert.ToInt32(User.Identity.Name) && x.ID == id && x.DOCUMENT_TYPE.ToLower() == doctype.ToLower());
                if (UCat != null)
                {
                    if (UCat.IS_CREDIT == true)
                    {
                        name = "<span style='background-color:green;color:white;'>" + UCat.NAME + "</span>";
                    }
                    else
                    {
                        name = "<span style='background-color:red;color:white;'>" + UCat.NAME + "</span>";
                    }
                }
                else
                {
                    name = "<span style='background-color:black;color:white;'>Uncategorized</span>";
                }
            }
            return name;
        }

        protected void rptSHEETS_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlList = (DropDownList)e.Item.FindControl("ddlCategory");
                int DOCUMENT_ID = Convert.ToInt32(Session["DOCUMENT_ID"].ToString());
                DOCUMENT doc = dbcontext.DOCUMENTs.FirstOrDefault(x => x.DOCUMENT_ID == DOCUMENT_ID);
                if (doc != null)
                {
                    Label lblCat = (Label)e.Item.FindControl("lblCat");

                    LinkButton btnCat = (LinkButton)e.Item.FindControl("lnkedit");
                    btnCat.OnClientClick = "getCate('" + lblCat.Text + "');";
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ViewReport.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ViewReport.aspx");
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnUpdateCategory_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdateCategory_Click1(object sender, EventArgs e)
        {

        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(hdId.Value);
            PARSER_SHEET s = dbcontext.PARSER_SHEETs.FirstOrDefault(x => x.SHEET_ID == Id);
            if (s != null)
            {
                s.CATEGORY_ID = Convert.ToInt32(ddlCategory.SelectedItem.Value);
                dbcontext.SubmitChanges();

                USER_CATEGORY cat = dbcontext.USER_CATEGORies.FirstOrDefault(x => x.ID == Convert.ToInt32(ddlCategory.SelectedItem.Value));
                if(cat!=null)
                {
                    cat.IDENTIFIER_1 = s.NARRATION;
                    dbcontext.SubmitChanges();
                }
            }

            int DOCUMENT_ID = Convert.ToInt32(Session["DOCUMENT_ID"].ToString());
            BindPARSER_DOCUMENT(DOCUMENT_ID);
            BIND_USER_CATEGORY(DOCUMENT_ID);
        }
    }
}