using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFParsing
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
                        CALCULATE_REPORT();
                        CALCULATE_EODBALANCE();
                        CALCULATE_FUNDSRECEIVED();
                        CALCULATE_FUNDSREMITTANCE();
                    }
                }
                else
                {
                    Response.Redirect("/Default.aspx");
                }
            }
        }

        private void CALCULATE_FUNDSREMITTANCE()
        {
            dt = new DataTable();
            dt.Columns.Add("Month");
            dt.Columns.Add("Description1");
            dt.Columns.Add("Amount1");
            dt.Columns.Add("Description2");
            dt.Columns.Add("Amount2");
            dt.Columns.Add("Description3");
            dt.Columns.Add("Amount3");
            dt.Columns.Add("Description4");
            dt.Columns.Add("Amount4");
            dt.Columns.Add("Description5");
            dt.Columns.Add("Amount5");
            int PARSER_ID = Convert.ToInt32(Session["PARSER_ID"].ToString());
            var DATA = dbcontext.GetReport(PARSER_ID);
            foreach (var d in DATA)
            {
                //dt.Columns.Add(d.RMONTH.ToString() + " " + d.RYEAR.ToString());
                string Month = d.RMONTH.ToString() + " " + d.RYEAR.ToString();
                int MonthId = GetMonthId(d.RMONTH);
                IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(d.RYEAR.ToString()) &&
                    x.DATE.Value.Month == MonthId && x.WITHDRAWAL_AMOUNT != null).OrderByDescending(x => x.WITHDRAWAL_AMOUNT).Take(5);
                int d2 = 1;
                string D1 = "";
                string D2 = "";
                string D3 = "";
                string D4 = "";
                string D5 = "";
                string A1 = "";
                string A2 = "";
                string A3 = "";
                string A4 = "";
                string A5 = "";
                foreach (PARSER_SHEET P in PS)
                {
                    if (P.WITHDRAWAL_AMOUNT != 0)
                    {
                        if (d2 == 1)
                        {
                            D1 = P.NARRATION;
                            A1 = P.WITHDRAWAL_AMOUNT.ToString();
                        }
                        if (d2 == 2)
                        {
                            D2 = P.NARRATION;
                            A2 = P.WITHDRAWAL_AMOUNT.ToString();
                        }
                        if (d2 == 3)
                        {
                            D3 = P.NARRATION;
                            A3 = P.WITHDRAWAL_AMOUNT.ToString();
                        }
                        if (d2 == 4)
                        {
                            D4 = P.NARRATION;
                            A4 = P.WITHDRAWAL_AMOUNT.ToString();
                        }
                        if (d2 == 5)
                        {
                            D5 = P.NARRATION;
                            A5 = P.WITHDRAWAL_AMOUNT.ToString();
                        }
                    }
                    else
                    {
                        if (d2 == 1)
                        {
                            D1 = "---";
                            A1 = "-";
                        }
                        if (d2 == 2)
                        {
                            D2 = "---";
                            A2 = "-";
                        }
                        if (d2 == 3)
                        {
                            D3 = "---";
                            A3 = "-";
                        }
                        if (d2 == 4)
                        {
                            D4 = "---";
                            A4 = "-";
                        }
                        if (d2 == 5)
                        {
                            D5 = "---";
                            A5 = "-";
                        }
                    }
                    d2 = d2 + 1;
                }
                DataRow row = dt.NewRow();
                row[0] = Month;
                row[1] = D1;
                row[2] = A1;
                row[3] = D2;
                row[4] = A2;
                row[5] = D3;
                row[6] = A3;
                row[7] = D4;
                row[8] = A4;
                row[9] = D5;
                row[10] = A5;
                dt.Rows.Add(row);
            }

            rptFundsRemittance.DataSource = dt;
            rptFundsRemittance.DataBind();
        }

        private void CALCULATE_FUNDSRECEIVED()
        {
            dt = new DataTable();
            dt.Columns.Add("Month");
            dt.Columns.Add("Description1");
            dt.Columns.Add("Amount1");
            dt.Columns.Add("Description2");
            dt.Columns.Add("Amount2");
            dt.Columns.Add("Description3");
            dt.Columns.Add("Amount3");
            dt.Columns.Add("Description4");
            dt.Columns.Add("Amount4");
            dt.Columns.Add("Description5");
            dt.Columns.Add("Amount5");
            int PARSER_ID = Convert.ToInt32(Session["PARSER_ID"].ToString());
            var DATA = dbcontext.GetReport(PARSER_ID);
            foreach (var d in DATA)
            {
                //dt.Columns.Add(d.RMONTH.ToString() + " " + d.RYEAR.ToString());
                string Month = d.RMONTH.ToString() + " " + d.RYEAR.ToString();
                int MonthId = GetMonthId(d.RMONTH);
                IEnumerable<PARSER_SHEET> PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(d.RYEAR.ToString()) &&
                    x.DATE.Value.Month == MonthId && x.DEPOSIT_AMOUNT != null).OrderByDescending(x => x.DEPOSIT_AMOUNT).Take(5);
                int d2 = 1;
                string D1 = "";
                string D2 = "";
                string D3 = "";
                string D4 = "";
                string D5 = "";
                string A1 = "";
                string A2 = "";
                string A3 = "";
                string A4 = "";
                string A5 = "";
                foreach (PARSER_SHEET P in PS)
                {
                    if (P.DEPOSIT_AMOUNT != 0)
                    {
                        if (d2 == 1)
                        {
                            D1 = P.NARRATION;
                            A1 = P.DEPOSIT_AMOUNT.ToString();
                        }
                        if (d2 == 2)
                        {
                            D2 = P.NARRATION;
                            A2 = P.DEPOSIT_AMOUNT.ToString();
                        }
                        if (d2 == 3)
                        {
                            D3 = P.NARRATION;
                            A3 = P.DEPOSIT_AMOUNT.ToString();
                        }
                        if (d2 == 4)
                        {
                            D4 = P.NARRATION;
                            A4 = P.DEPOSIT_AMOUNT.ToString();
                        }
                        if (d2 == 5)
                        {
                            D5 = P.NARRATION;
                            A5 = P.DEPOSIT_AMOUNT.ToString();
                        }
                    }
                    else
                    {
                        if (d2 == 1)
                        {
                            D1 = "---";
                            A1 = "-";
                        }
                        if (d2 == 2)
                        {
                            D2 = "---";
                            A2 = "-";
                        }
                        if (d2 == 3)
                        {
                            D3 = "---";
                            A3 = "-";
                        }
                        if (d2 == 4)
                        {
                            D4 = "---";
                            A4 = "-";
                        }
                        if (d2 == 5)
                        {
                            D5 = "---";
                            A5 = "-";
                        }
                    }
                    d2 = d2 + 1;
                }
                DataRow row = dt.NewRow();
                row[0] = Month;
                row[1] = D1;
                row[2] = A1;
                row[3] = D2;
                row[4] = A2;
                row[5] = D3;
                row[6] = A3;
                row[7] = D4;
                row[8] = A4;
                row[9] = D5;
                row[10] = A5;
                dt.Rows.Add(row);
            }

            rptFundsReceived.DataSource = dt;
            rptFundsReceived.DataBind();
        }

        private void CALCULATE_EODBALANCE()
        {
            dt = new DataTable();
            dt.Columns.Add("Day/Month");
            int PARSER_ID = Convert.ToInt32(Session["PARSER_ID"].ToString());
            var DATA = dbcontext.GetReport(PARSER_ID);
            foreach (var d in DATA)
            {
                dt.Columns.Add(d.RMONTH.ToString() + " " + d.RYEAR.ToString());
            }

            Calculate_DailyBalance(PARSER_ID, dt);
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
            grdEODBalance.DataSource = dt;
            grdEODBalance.DataBind();
        }

        private string GetDailyCalculation(int PARSER_ID, string MONTHYEAR, int DAY)
        {
            string res = "";
            if (MONTHYEAR != "Total")
            {
                string[] mon = MONTHYEAR.Split(' ');
                int MonthId = GetMonthId(mon[0].ToString());
                //Total No of Credit Transactions
                var PS = dbcontext.PARSER_SHEETs.Where(x => x.PARSER_ID == PARSER_ID && x.DATE.Value.Year == Convert.ToInt32(mon[1].ToString()) &&
                    x.DATE.Value.Month == MonthId && x.DATE.Value.Day == DAY).OrderByDescending(x=>x.SHEET_ID).Take(1);
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

        private void CALCULATE_REPORT()
        {
            dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Particulars");
            int PARSER_ID = Convert.ToInt32(Session["PARSER_ID"].ToString());
            var DATA = dbcontext.GetReport(PARSER_ID);
            foreach (var d in DATA)
            {
                dt.Columns.Add(d.RMONTH.ToString() + " " + d.RYEAR.ToString());
            }
            dt.Columns.Add("Total");

            Calculate_Analysis(PARSER_ID, dt);
            //rptReport1.DataSource = DATA;
            //rptReport1.DataBind();
        }

        public void Calculate_Analysis(int PARSER_ID, DataTable dt)
        {
            DataRow row = dt.NewRow();
            row[0] = "1";
            row[1] = "Total No. of Credit Transactions";
            int k=0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 1);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[0] = "2";
            row[1] = "Total Amount of Credit Transactions";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 2);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "3";
            row[1] = "Total No. of Debit Transactions";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 3);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "4";
            row[1] = "Total Amount of Debit Transactions";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 4);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "5";
            row[1] = "Total No. of Cash Deposits";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 5);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "6";
            row[1] = "Total Amount of Cash Deposits";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 6);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "7";
            row[1] = "Total No. of Cash Withdrawals";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 7);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "8";
            row[1] = "Total Amount of Cash Withdrawals";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 8);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "9";
            row[1] = "Total No. of Cheque Deposits";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 9);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "10";
            row[1] = "Total Amount of Cheque Deposits";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 10);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "11";
            row[1] = "Total No. of Cheque Issues";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 11);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "12";
            row[1] = "Total Amount of Cheque Issues";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 12);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "13";
            row[1] = "Total No. of Inward Cheque Bounces";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 13);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "14";
            row[1] = "Total No. of Outward Cheque Bounces";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 14);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "15";
            row[1] = "Min EOD Balance";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 15);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "16";
            row[1] = "Max EOD Balance";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 16);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "17";
            row[1] = "Average EOD Balance";
            k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k >= 2)
                {
                    row[k] = GetAnalysisCalculation(PARSER_ID, dc.ColumnName, 17);
                }
                k = k + 1;
            }
            dt.Rows.Add(row);
            grdAnalysis.DataSource = dt;
            grdAnalysis.DataBind();
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
                        if (P.DEPOSIT_AMOUNT!=null)
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
                        res = Math.Round((d / count),2).ToString();
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

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                Repeater headerRepeater = e.Item.FindControl("Header1") as Repeater;
                headerRepeater.DataSource = dt.Columns;
                headerRepeater.DataBind();
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater columnRepeater = e.Item.FindControl("rptEoDChart") as Repeater;
                var row = e.Item.DataItem as System.Data.DataRowView;
                columnRepeater.DataSource = row.Row.ItemArray;
                columnRepeater.DataBind();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UpdateCategory.aspx");
        }
    }
}