using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFParsing
{
    public partial class q1 : System.Web.UI.Page
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
                    }
                }
                else
                {
                    Response.Redirect("/Default.aspx");
                }
            }
        }

        private void BindPARSER_DOCUMENT(int DOCUMENT_ID)
        {
            int PARSER_ID = Convert.ToInt32(Session["PARSER_ID"].ToString());
            int Srno = 1;
            var sheet = from a in dbcontext.PARSER_SHEETs.AsEnumerable().Select((r, i) => new { Row = r, Index = i }).Where(x => x.Row.PARSER_ID == PARSER_ID).ToList()
                        select new
                        {
                            INDEX = Srno++,
                            DATE = a.Row.DATE.Value.Day + "/" + a.Row.DATE.Value.Month + "/" + a.Row.DATE.Value.Year,
                            VALUE_DATE = a.Row.DATE.Value.Day + "/" + a.Row.DATE.Value.Month + "/" + a.Row.DATE.Value.Year,
                            NARRATION = a.Row.NARRATION,
                            CHQ_NO = a.Row.CHQ_NO,
                            WITHDRAWAL_AMOUNT = a.Row.WITHDRAWAL_AMOUNT == null ? "" : a.Row.WITHDRAWAL_AMOUNT.ToString(),
                            DEPOSIT_AMOUNT = a.Row.DEPOSIT_AMOUNT == null ? "" : a.Row.DEPOSIT_AMOUNT.ToString(),
                            CLOSING_BALANCE = a.Row.CLOSING_BALANCE,
                            CATEGORY_ID = a.Row.CATEGORY_ID,
                        };
            rptSHEETS.DataSource = sheet;
            rptSHEETS.DataBind();
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
                    string doctype = doc.DOCUMENT_TYPE;
                    IEnumerable<USER_CATEGORY> UCat = dbcontext.USER_CATEGORies.Where(x => x.USER_ID == Convert.ToInt32(User.Identity.Name) && x.DOCUMENT_TYPE.ToLower() == doctype.ToLower());
                    ddlList.DataSource = UCat;
                    ddlList.DataTextField = "NAME";
                    ddlList.DataValueField = "ID";
                    ddlList.DataBind();
                    ddlList.Items.Add(new ListItem("UnCategorized", "0"));
                    Label lblCat = (Label)e.Item.FindControl("lblCat");
                    ddlList.Items.FindByValue(lblCat.Text).Selected = true;


                    Button btnCat = (Button)e.Item.FindControl("btnCat");
                    btnCat.Attributes.Add("click", "alert('" + lblCat.Text + "');");
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
    }
}