using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFParsing.admin
{
    public partial class ViewReport : System.Web.UI.Page
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
                        CALCULATE_EODBALANCE();
                    }
                }
                else
                {
                    Response.Redirect("/Default.aspx");
                }
            }
        }




        private void CALCULATE_EODBALANCE()
        {
            dt = new DataTable();
            dt.Columns.Add("Day/Month");
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
                            WITHDRAWAL_AMOUNT = a.Row.WITHDRAWAL_AMOUNT,
                            DEPOSIT_AMOUNT = a.Row.DEPOSIT_AMOUNT,
                            CLOSING_BALANCE = a.Row.CLOSING_BALANCE,
                        };
            //var DATA = dbcontext.GetReport(PARSER_ID);
            rptSHEETS.DataSource = sheet;
            rptSHEETS.DataBind();
        }

        private void Calculate_DailyBalance(int PARSER_ID, DataTable dt)
        {
            string prevday = "0";
            for (int pp = 1; pp <= 31; pp++)
            {
                DataRow row = dt.NewRow();
                row[0] = pp.ToString();
                int k = 0;
                foreach (DataColumn dc in dt.Columns)
                {
                    if (k >= 1)
                    {
                        string res = GetDailyCalculation(PARSER_ID, dc.ColumnName, pp);
                        if (res == "0")
                        {
                            if (dt.Rows.Count >= 1)
                            {
                                res = dt.Rows[pp - 2][k].ToString();
                            }
                        }
                        row[k] = res;
                    }
                    k = k + 1;
                }
                dt.Rows.Add(row);
            }
            rptSHEETS.DataSource = dt;
            rptSHEETS.DataBind();
        }

        private string GetDailyCalculation(int PARSER_ID, string MONTHYEAR, int DAY)
        {
            string res = "";
            if (MONTHYEAR != "Total")
            {
                string[] mon = MONTHYEAR.Split(' ');
                int MonthId = GetMonthId(mon[0].ToString());
                //Total No of Credit Transactions
                IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                    x.DATE.Value.Month == MonthId && x.DATE.Value.Day == DAY);
                decimal d = 0;
                foreach (PARSER_SHEET P in PS)
                {
                    if (P.CLOSING_BALANCE != null)
                    {
                        d = Convert.ToDecimal(P.CLOSING_BALANCE);
                    }
                }
                res = d.ToString();
            }
            return res;
        }

        private string GetAnalysisCalculation(int PARSER_ID, string MONTHYEAR, int QUERY)
        {
            string res = "";
            int count = 0;
            if (MONTHYEAR != "Total")
            {
                string[] mon = MONTHYEAR.Split(' ');
                int MonthId = GetMonthId(mon[0].ToString());
                if (QUERY == 1)
                {
                    //Total No of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId);
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.DEPOSIT_AMOUNT != null)
                        {
                            count = count + 1;
                        }
                    }
                    res = count.ToString();
                }
                if (QUERY == 2)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId);
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.DEPOSIT_AMOUNT != null)
                        {
                            d = d + Convert.ToDecimal(P.DEPOSIT_AMOUNT);
                        }
                    }
                    res = d.ToString();
                }
                if (QUERY == 3)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId);
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.WITHDRAWAL_AMOUNT != null)
                        {
                            count = count + 1;
                        }
                    }
                    res = count.ToString();
                }
                if (QUERY == 4)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId);
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.WITHDRAWAL_AMOUNT != null)
                        {
                            d = d + Convert.ToDecimal(P.WITHDRAWAL_AMOUNT);
                        }
                    }
                    res = d.ToString();
                }
                if (QUERY == 5)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId && x.NARRATION.ToLower().Contains("cash"));
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.DEPOSIT_AMOUNT != null)
                        {
                            count = count + 1;
                        }
                    }
                    res = count.ToString();
                }
                if (QUERY == 6)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId && x.NARRATION.ToLower().Contains("cash"));
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.DEPOSIT_AMOUNT != null)
                        {
                            d = d + Convert.ToDecimal(P.DEPOSIT_AMOUNT);
                        }
                    }
                    res = d.ToString();
                }
                if (QUERY == 7)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId && x.NARRATION.ToLower().Contains("cash"));
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.WITHDRAWAL_AMOUNT != null)
                        {
                            count = count + 1;
                        }
                    }
                    res = count.ToString();
                }
                if (QUERY == 8)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId && x.NARRATION.ToLower().Contains("cash"));
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.WITHDRAWAL_AMOUNT != null)
                        {
                            d = d + Convert.ToDecimal(P.WITHDRAWAL_AMOUNT);
                        }
                    }
                    res = d.ToString();
                }
                if (QUERY == 9)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId && x.NARRATION.ToLower().Contains("chq dep"));
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.DEPOSIT_AMOUNT != null)
                        {
                            count = count + 1;
                        }
                    }
                    res = count.ToString();
                }
                if (QUERY == 10)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId && x.NARRATION.ToLower().Contains("chq dep"));
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.DEPOSIT_AMOUNT != null)
                        {
                            d = d + Convert.ToDecimal(P.DEPOSIT_AMOUNT);
                        }
                    }
                    res = d.ToString();
                }
                if (QUERY == 11)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId && x.NARRATION.ToLower().Contains("chq paid"));
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.WITHDRAWAL_AMOUNT != null)
                        {
                            count = count + 1;
                        }
                    }
                    res = count.ToString();
                }
                if (QUERY == 12)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId && x.NARRATION.ToLower().Contains("chq paid"));
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.WITHDRAWAL_AMOUNT != null)
                        {
                            d = d + Convert.ToDecimal(P.WITHDRAWAL_AMOUNT);
                        }
                    }
                    res = d.ToString();
                }
                if (QUERY == 13)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId && x.NARRATION.ToLower().Contains("reject"));
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.DEPOSIT_AMOUNT != null)
                        {
                            count = count + 1;
                        }
                    }
                    res = count.ToString();
                }
                if (QUERY == 14)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId && x.NARRATION.ToLower().Contains("reject"));
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.CLOSING_BALANCE != null)
                        {
                            count = count + 1;
                        }
                    }
                    res = d.ToString();
                }
                if (QUERY == 15)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId);
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.CLOSING_BALANCE != null)
                        {
                            if (d == 0)
                            {
                                d = Convert.ToDecimal(P.CLOSING_BALANCE);
                            }
                            if (d >= P.CLOSING_BALANCE)
                            {
                                d = Convert.ToDecimal(P.CLOSING_BALANCE);
                            }
                        }
                    }
                    res = d.ToString();
                }
                if (QUERY == 16)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId);
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.CLOSING_BALANCE != null)
                        {
                            if (d == 0)
                            {
                                d = Convert.ToDecimal(P.CLOSING_BALANCE);
                            }
                            if (d <= P.CLOSING_BALANCE)
                            {
                                d = Convert.ToDecimal(P.CLOSING_BALANCE);
                            }
                        }
                    }
                    res = d.ToString();
                }
                if (QUERY == 17)
                {
                    //Total Amount of Credit Transactions
                    IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                        x.DATE.Value.Month == MonthId);
                    decimal d = 0;
                    foreach (PARSER_SHEET P in PS)
                    {
                        if (P.CLOSING_BALANCE != null)
                        {
                            count = count + 1;
                            d = d + Convert.ToDecimal(P.CLOSING_BALANCE);
                        }
                    }
                    if (count > 0)
                    {
                        res = Math.Round((d / count), 2).ToString();
                    }
                    else
                        res = "0";
                }
            }
            return res;
        }

        private int GetMonthId(string Month)
        {
            int id = 0;
            if (Month.ToLower().Contains("jan"))
            {
                id = 1;
            }
            if (Month.ToLower().Contains("feb"))
            {
                id = 2;
            }
            if (Month.ToLower().Contains("mar"))
            {
                id = 3;
            }
            if (Month.ToLower().Contains("apr"))
            {
                id = 4;
            }
            if (Month.ToLower().Contains("may"))
            {
                id = 5;
            }
            if (Month.ToLower().Contains("june"))
            {
                id = 6;
            }
            if (Month.ToLower().Contains("july"))
            {
                id = 7;
            }
            if (Month.ToLower().Contains("aug"))
            {
                id = 8;
            }
            if (Month.ToLower().Contains("sept"))
            {
                id = 9;
            }
            if (Month.ToLower().Contains("oct"))
            {
                id = 10;
            }
            if (Month.ToLower().Contains("nov"))
            {
                id = 11;
            }
            if (Month.ToLower().Contains("dec"))
            {
                id = 12;
            }
            return id;
        }

        private void BindPARSER_DOCUMENT(int DOCUMENT_ID)
        {
            IEnumerable<PARSER_DOCUMENT> document = dbcontext.PARSER_DOCUMENTs.Where(x => x.DOCUMENT_ID == DOCUMENT_ID);
            rptPARSER_DOCUMENT.DataSource = document;
            rptPARSER_DOCUMENT.DataBind();
        }
    }
}