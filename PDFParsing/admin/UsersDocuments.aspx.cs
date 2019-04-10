using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFParsing.admin
{
    public partial class UsersDocuments : System.Web.UI.Page
    {
        PDFParseDataContext dbcontext = new PDFParseDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("/Default.aspx");
                }
                int USER_ID = Convert.ToInt32(Request.QueryString["USER_ID"].ToString());
                BindDocuments(USER_ID);
            }
        }

        private void BindDocuments(int USER_ID)
        {
            int Srno = 1;
            var DATA = from a in dbcontext.DOCUMENTs.AsEnumerable().Select((r, i) => new { Row = r, Index = i }).Where(x => x.Row.USER_ID == Convert.ToInt32(USER_ID)).ToList()
                       select new
                       {
                           INDEX = Srno++,
                           UPLOAD_DATE = a.Row.UPLOAD_DATE.Value.Day + "/" + a.Row.UPLOAD_DATE.Value.Month + "/" + a.Row.UPLOAD_DATE.Value.Year,
                           DOCUMENT_NAME = a.Row.DOCUMENT_NAME,
                           DOCUMENT_TYPE = a.Row.DOCUMENT_TYPE,
                           ACCOUNT_TYPE = a.Row.ACCOUNT_TYPE,
                           DOCUMENT_ID = a.Row.DOCUMENT_ID,
                       };
            rptDOCUMENTS.DataSource = DATA.OrderByDescending(x => x.INDEX);
            rptDOCUMENTS.DataBind();
        }
        protected void rptDOCUMENTS_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "report")
            {
                int DOCUMENT_ID = Convert.ToInt32(e.CommandArgument);
                PARSER_DOCUMENT parse = dbcontext.PARSER_DOCUMENTs.FirstOrDefault(x => x.DOCUMENT_ID == DOCUMENT_ID);
                if (parse != null)
                {
                    Session["DOCUMENT_ID"] = DOCUMENT_ID;
                    Session["PARSER_ID"] = parse.PARSER_ID;
                    Response.Redirect("ViewReport.aspx");
                }
            }
            if (e.CommandName == "dnld")
            {
                int DOCUMENT_ID = Convert.ToInt32(e.CommandArgument);
                int PARSER_ID = 0;
                string DOCUMENT_NAME = "";
                DOCUMENT docu = dbcontext.DOCUMENTs.FirstOrDefault(x => x.DOCUMENT_ID == DOCUMENT_ID);
                DOCUMENT_NAME = docu.DOCUMENT_NAME;
                PARSER_DOCUMENT parse = dbcontext.PARSER_DOCUMENTs.FirstOrDefault(x => x.DOCUMENT_ID == DOCUMENT_ID);
                if (parse != null)
                {
                    PARSER_ID = parse.PARSER_ID;
                    var data = from a in dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID)
                               select new
                               {
                                   DATE = a.DATE.Value.Day.ToString() + "/" + a.DATE.Value.Month.ToString() + "/" + a.DATE.Value.Year.ToString(),
                                   NARRATION = a.NARRATION,
                                   CHQ_NO = a.CHQ_NO,
                                   VALUE_DATE = a.VALUE_DATE,
                                   WITHDRAWAL_AMOUNT = a.WITHDRAWAL_AMOUNT,
                                   DEPOSIT_AMOUNT = a.DEPOSIT_AMOUNT,
                                   CLOSING_BALANCE = a.CLOSING_BALANCE,
                               };
                    DataTable dt = LINQResultToDataTable(data);
                    EXPORT_TO_EXCEL(dt, DOCUMENT_NAME);
                }
            }
        }

        private void EXPORT_TO_EXCEL(DataTable dt, string DOCUMENT_NAME)
        {
            string attachment = "attachment; filename=" + DOCUMENT_NAME.Replace(".pdf", "").Replace(".PDF", "") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }

        public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();


            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }    
    }
}