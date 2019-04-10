using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFParsing
{
    public class SBI
    {
        public DataSet ProcessPdfAndWriteCSVSBI(string file, bool passwordprotected, string password)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("txnDate");
            dtt.Columns.Add("valuedate");
            dtt.Columns.Add("description");
            dtt.Columns.Add("chequeno");
            dtt.Columns.Add("branchcode");
            dtt.Columns.Add("debit");
            dtt.Columns.Add("credit");
            dtt.Columns.Add("balance");
            //TableValues.Substring(TableValues.IndexOf("S No"))
            String TableValues = ReadPdfFile(file, passwordprotected, password);
            DataTable dt2 = ReadBasicInfo(TableValues);
            var builder = new StringBuilder();
            int startloc = 0, endloc = 0;
            Dictionary<int, List<string>> dicValues = new Dictionary<int, List<string>>();
            int RowCount = 1;
            if (TableValues.IndexOf("Txn\nDate") == -1)
            {
                string ToUse = TableValues.Substring(TableValues.IndexOf("Txn"));

                int StartIndex = ToUse.IndexOf("Balance");

                string Text = ToUse.Substring(StartIndex + 7);

                int Sno = 0;
                string descp = "";
                string amt = "";
                int p = 0;
                bool submit = false;
                DataRow row = dtt.NewRow();
                bool isdate = false;
                foreach (string s in Text.Replace("Txn Date Value Date Description Ref No./Cheque", "").Split('\n'))
                {





                    string[] txt = s.Split(' ');
                    if (p == 0)
                    {
                        row = dtt.NewRow();
                        submit = false;
                        p = 1;
                    }

                    foreach (string s1 in txt)
                    {
                        if (s1 != "")
                        {
                            long n1;
                            bool isNumeric1 = long.TryParse(s1, out n1);


                            if (s1.Split('/').Length - 1 == 2 && s1.Length == 10)
                            {
                                isdate = true;
                                row[0] = s1;
                                row[1] = s1;
                                descp = "";
                            }
                            else if ((isNumeric1 && txt.Length == 3))
                            {
                                bool isNumeric11 = long.TryParse(txt[0], out n1);
                                if (isNumeric11 && (txt[1].Contains(".") || txt[1].Contains(",")) && (txt[2].Contains(".") || txt[2].Contains(",")))
                                {
                                    string a11 = "";
                                    string a21 = "";
                                    string a31 = "";

                                    a11 = txt[0].ToString();
                                    a21 = txt[1].ToString();
                                    a31 = txt[2].ToString();
                                    row["branchcode"] = a11;
                                    row["debit"] = a21;
                                    row["credit"] = a21;
                                    row["balance"] = a31;
                                    if (isdate == true)
                                    {
                                        dtt.Rows.Add(row);
                                    }
                                    submit = true;
                                    descp = "";
                                    p = 0;
                                }
                                else
                                {
                                    if (submit == false)
                                    {
                                        descp = descp + s1 + " ";
                                        row["description"] = descp;
                                    }
                                }
                            }
                            else
                            {
                                if (submit == false)
                                {
                                    descp = descp + s1 + " ";
                                    row["description"] = descp;
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                string ToUse = TableValues.Substring(TableValues.IndexOf("Txn\nDate"));
                int StartIndex = ToUse.IndexOf("Balance");
                string Text = ToUse.Substring(StartIndex + 7);
                int Sno = 0;
                string descp = "";
                string amt = "";
                int p = 0;
                bool submit = false;
                DataRow row = dtt.NewRow();
                bool isdate = false;
                bool col1 = false;
                string Col1 = "";
                string Col2 = "";
                string Col3 = "";
                string Col4 = "";
                string Col5 = "";
                string Col6 = "";
                bool col2 = false;
                bool col3 = false;
                bool col4 = false;
                bool col5 = false;
                bool col6 = false;
                bool col7 = false;
                string brcode = "";
                string brdebit = "";
                string brcredit = "";
                string brbal = "";
                bool credit_yes = false;
                double balance = 0;
                int m_chk = 0;
                foreach (string s in Text.Split('\n'))
                {
                    if (credit_yes == false)
                    {
                        if (s == "Debit Credit Balance")
                        {
                            col1 = false;
                            Col1 = "";
                            Col2 = "";
                            Col3 = "";
                            Col4 = "";
                            Col5 = "";
                            Col6 = "";
                            col2 = false;
                            col3 = false;
                            col4 = false;
                            col5 = false;
                            col6 = false;
                            col7 = false;
                        }
                        string[] txt = s.Split(' ');
                        if (p == 0)
                        {

                            submit = false;
                            p = 1;
                        }
                        if (col1 == false)
                        {
                            if (s.Length == 5 || s.Length == 6 || s.Length == 4)
                            {
                                if (s.Length == 11 || s.Length == 10)
                                    col1 = true;
                                if (col1 == false)
                                {
                                    Col1 = Col1 + " " + s;
                                }
                                if (Col1.Trim().Length == 11 || Col1.Trim().Length == 10)
                                {
                                    col1 = true;
                                }
                            }
                        }
                        else if (col2 == false)
                        {
                            if (s.Length == 5 || s.Length == 6 || s.Length == 4)
                            {
                                if (s.Length == 11 || s.Length == 10)
                                    col2 = true;
                                if (col2 == false)
                                {
                                    Col2 = Col2 + " " + s;
                                }
                                if (Col2.Trim().Length == 11 || Col2.Trim().Length == 10)
                                {
                                    col2 = true;
                                }
                            }
                        }
                        else if (col3 == false)
                        {
                            
                            if (s.Replace(@"/", "").Trim().Split(' ').Length >= 2)
                            {
                                if (s.Trim().Split(' ').Length == 5)
                                {
                                    long n1;
                                    bool isNumeric1 = long.TryParse(s.Replace(@"/", "").Trim().Split(' ')[1].ToString(), out n1);
                                    bool isNumeric2 = s.Replace(@"/", "").Trim().Split(' ')[2].Contains(',');
                                    bool isNumeric2_1 = s.Replace(@"/", "").Trim().Split(' ')[2].Contains('.');
                                    bool isNumeric3 = false;
                                    try
                                    {
                                        isNumeric3 = s.Replace(@"/", "").Trim().Split(' ')[3].Contains(',');
                                    }
                                    catch { }
                                    bool isNumeric3_1 = false;
                                    try
                                    {
                                        isNumeric3_1 = s.Replace(@"/", "").Trim().Split(' ')[3].Contains('.');
                                    }
                                    catch { }
                                    if (isNumeric1 && (isNumeric2 || isNumeric2_1))
                                    {
                                        if (isNumeric3 || isNumeric3_1)
                                        {
                                            row = dtt.NewRow();
                                            col3 = true;
                                            string[] trans = s.Replace(@"/", "").Trim().Split(' ');
                                            Col4 = trans[1].ToString();
                                            Col5 = trans[2].ToString();
                                            Col6 = trans[3].ToString().Replace("Txn", "");
                                            balance = Convert.ToDouble(Col6);
                                            string desc = "";
                                            string chequeno = "";
                                            desc = Col3;

                                            row["txnDate"] = Col1;
                                            row["valuedate"] = Col2;
                                            row["description"] = desc;
                                            row["chequeno"] = desc;
                                            row["branchcode"] = Col4;
                                            row["debit"] = Col5;
                                            row["credit"] = Col5;
                                            row["balance"] = Col6;
                                            dtt.Rows.Add(row);
                                            col1 = false;
                                            Col1 = "";
                                            Col2 = "";
                                            Col3 = "";
                                            Col4 = "";
                                            Col5 = "";
                                            Col6 = "";
                                            col2 = false;
                                            col3 = false;
                                            col4 = false;
                                            col5 = false;
                                            col6 = false;
                                            col7 = false;
                                        }
                                        else
                                        {
                                            bool isNumeric42 = long.TryParse(s.Replace(@"/", "").Replace(".", "").Trim().Split(' ')[1].ToString(), out n1);
                                            if (isNumeric42)
                                            {
                                                string[] transs = s.Replace(@"/", "").Trim().Split(' ');
                                                brcode = transs[0];
                                                brdebit = transs[1];
                                                brcredit = transs[1];
                                                credit_yes = true;
                                            }
                                            else
                                            {
                                                Col3 = Col3 + " " + s;
                                            }
                                        }


                                    }
                                    else
                                    {
                                        if (isNumeric3 || isNumeric3_1)
                                        {
                                            brcode = s.Replace(@"/", "").Trim().Split(' ')[1];
                                            brdebit = s.Replace(@"/", "").Trim().Split(' ')[2];
                                            brcredit = s.Replace(@"/", "").Trim().Split(' ')[2];
                                            credit_yes = true;
                                        }
                                        Col3 = Col3 + " " + s;
                                    }
                                }
                                else
                                {
                                    long n1;
                                    bool isNumeric1 = long.TryParse(s.Replace(@"/", "").Trim().Split(' ')[0].ToString(), out n1);
                                    bool isNumeric2 = s.Replace(@"/", "").Trim().Split(' ')[1].Contains(',');
                                    bool isNumeric2_1 = s.Replace(@"/", "").Trim().Split(' ')[1].Contains('.');
                                    bool isNumeric3 = false;
                                    try
                                    {
                                        isNumeric3 = s.Replace(@"/", "").Trim().Split(' ')[2].Contains(',');
                                    }
                                    catch { }
                                    bool isNumeric3_1 = false;
                                    try
                                    {
                                        isNumeric3_1 = s.Replace(@"/", "").Trim().Split(' ')[2].Contains('.');
                                    }
                                    catch { }
                                    if (isNumeric1 && (isNumeric2 || isNumeric2_1))
                                    {
                                        if (isNumeric3 || isNumeric3_1)
                                        {
                                            row = dtt.NewRow();
                                            col3 = true;
                                            string[] trans = s.Replace(@"/", "").Trim().Split(' ');
                                            Col4 = trans[0].ToString();
                                            Col5 = trans[1].ToString();
                                            Col6 = trans[2].ToString().Replace("Txn", "");
                                            balance = Convert.ToDouble(Col6);
                                            string desc = "";
                                            string chequeno = "";
                                            desc = Col3;

                                            row["txnDate"] = Col1;
                                            row["valuedate"] = Col2;
                                            row["description"] = desc;
                                            row["chequeno"] = desc;
                                            row["branchcode"] = Col4;
                                            row["debit"] = Col5;
                                            row["credit"] = Col5;
                                            row["balance"] = Col6;
                                            dtt.Rows.Add(row);
                                            col1 = false;
                                            Col1 = "";
                                            Col2 = "";
                                            Col3 = "";
                                            Col4 = "";
                                            Col5 = "";
                                            Col6 = "";
                                            col2 = false;
                                            col3 = false;
                                            col4 = false;
                                            col5 = false;
                                            col6 = false;
                                            col7 = false;
                                        }
                                        else
                                        {
                                            bool isNumeric42 = long.TryParse(s.Replace(@"/", "").Replace(".", "").Trim().Split(' ')[1].ToString(), out n1);
                                            if (isNumeric42)
                                            {
                                                string[] transs = s.Replace(@"/", "").Trim().Split(' ');
                                                brcode = transs[0];
                                                brdebit = transs[1];
                                                brcredit = transs[1];
                                                credit_yes = true;
                                            }
                                            else
                                            {
                                                Col3 = Col3 + " " + s;
                                            }
                                        }


                                    }
                                    else
                                    {
                                        if (isNumeric3 || isNumeric3_1)
                                        {
                                            brcode = s.Replace(@"/", "").Trim().Split(' ')[1];
                                            brdebit = s.Replace(@"/", "").Trim().Split(' ')[2];
                                            brcredit = s.Replace(@"/", "").Trim().Split(' ')[2];
                                            credit_yes = true;
                                        }
                                        Col3 = Col3 + " " + s;
                                    }
                                }
                            }
                            else
                            {
                                Col3 = Col3 + " " + s;
                            }
                        }
                    }
                    else
                    {
                        if (s.Contains(",") || s.Contains("."))
                        {
                            brbal = s.Replace("Txn","");
                            row = dtt.NewRow();
                            col3 = true;
                            balance = Convert.ToDouble(brbal);
                            string desc = "";
                            string chequeno = "";
                            desc = Col3;

                            row["txnDate"] = Col1;
                            row["valuedate"] = Col2;
                            row["description"] = desc;
                            row["chequeno"] = desc;
                            row["branchcode"] = Col4;
                            row["debit"] = brdebit;
                            row["credit"] = brcredit;
                            row["balance"] = balance;
                            dtt.Rows.Add(row);
                            col1 = false;
                            Col1 = "";
                            Col2 = "";
                            Col3 = "";
                            Col4 = "";
                            Col5 = "";
                            Col6 = "";
                            col2 = false;
                            col3 = false;
                            col4 = false;
                            col5 = false;
                            col6 = false;
                            col7 = false;
                            credit_yes = false;
                            m_chk = 0;
                        }
                    }
                }
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt2);
            if (dtt.Rows.Count != 0)
            {
                ds.Tables.Add(dtt);
            }
            else
            {
                DataTable dttt = GetDataTable(TableValues);
                ds.Tables.Add(dttt);
            }
            return ds;
        }

        private DataTable GetDataTable(string TableValues)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("txnDate");
            dtt.Columns.Add("valuedate");
            dtt.Columns.Add("description");
            dtt.Columns.Add("chequeno");
            dtt.Columns.Add("branchcode");
            dtt.Columns.Add("debit");
            dtt.Columns.Add("credit");
            dtt.Columns.Add("balance");

            var builder = new StringBuilder();
            int startloc = 0, endloc = 0;
            Dictionary<int, List<string>> dicValues = new Dictionary<int, List<string>>();
            int RowCount = 1;
            if (TableValues.IndexOf("Txn\nDate") == -1)
            {
                string ToUse = TableValues.Substring(TableValues.IndexOf("Txn Date"));
                int StartIndex = ToUse.IndexOf("Balance");
                string Text = ToUse.Substring(StartIndex + 7);
                int Sno = 0;
                string descp = "";
                string amt = "";
                int p = 0;
                bool submit = false;
                DataRow row = dtt.NewRow();
                bool isdate = false;
                bool col1 = false;
                string Col1 = "";
                string Col2 = "";
                string Col3 = "";
                string Col4 = "";
                string Col5 = "";
                string Col6 = "";
                bool col2 = false;
                bool col3 = false;
                bool col4 = false;
                bool col5 = false;
                bool col6 = false;
                bool col7 = false;
                string brcode = "";
                string brdebit = "";
                string brcredit = "";
                string brbal = "";
                bool credit_yes = false;
                double balance = 0;
                int m_chk = 0;
                foreach (string s in Text.Split('\n'))
                {
                    if (credit_yes == false)
                    {
                        if (s == "Debit Credit Balance")
                        {
                            col1 = false;
                            Col1 = "";
                            Col2 = "";
                            Col3 = "";
                            Col4 = "";
                            Col5 = "";
                            Col6 = "";
                            col2 = false;
                            col3 = false;
                            col4 = false;
                            col5 = false;
                            col6 = false;
                            col7 = false;
                        }
                        string[] txt = s.Split(' ');
                        if (p == 0)
                        {

                            submit = false;
                            p = 1;
                        }
                        if (col1 == false)
                        {
                            if (txt.Length >= 6)
                            {
                                Col1 = txt[0] + " " + txt[1] + " " + txt[2];
                                Col2 = txt[3] + " " + txt[4] + " " + txt[5];
                                for (int k = 6; k < txt.Length; k++)
                                {
                                    Col3 = Col3 + " " + txt[k];
                                }
                                col1 = true;
                                col2 = true;
                            }
                            else
                            {
                                if (s.Length == 5 || s.Length == 6 || s.Length == 4)
                                {
                                    if (s.Length == 11 || s.Length == 10)
                                        col1 = true;
                                    if (col1 == false)
                                    {
                                        Col1 = Col1 + " " + s;
                                    }
                                    if (Col1.Trim().Length == 11 || Col1.Trim().Length == 10)
                                    {
                                        col1 = true;
                                    }
                                }
                            }
                        }
                        else if (col2 == false)
                        {
                            if (s.Length == 5 || s.Length == 6 || s.Length == 4)
                            {
                                if (s.Length == 11 || s.Length == 10)
                                    col2 = true;
                                if (col2 == false)
                                {
                                    Col2 = Col2 + " " + s;
                                }
                                if (Col2.Trim().Length == 11 || Col2.Trim().Length == 10)
                                {
                                    col2 = true;
                                }
                            }
                        }
                        else if (col3 == false)
                        {

                            if (s.Replace(@"/", "").Trim().Split(' ').Length >= 2)
                            {
                                if (s.Trim().Split(' ').Length == 5)
                                {
                                    long n1;
                                    bool isNumeric1 = long.TryParse(s.Replace(@"/", "").Trim().Split(' ')[1].ToString(), out n1);
                                    bool isNumeric2 = s.Replace(@"/", "").Trim().Split(' ')[2].Contains(',');
                                    bool isNumeric2_1 = s.Replace(@"/", "").Trim().Split(' ')[2].Contains('.');
                                    bool isNumeric3 = false;
                                    try
                                    {
                                        isNumeric3 = s.Replace(@"/", "").Trim().Split(' ')[3].Contains(',');
                                    }
                                    catch { }
                                    bool isNumeric3_1 = false;
                                    try
                                    {
                                        isNumeric3_1 = s.Replace(@"/", "").Trim().Split(' ')[3].Contains('.');
                                    }
                                    catch { }
                                    if (isNumeric1 && (isNumeric2 || isNumeric2_1))
                                    {
                                        if (isNumeric3 || isNumeric3_1)
                                        {
                                            row = dtt.NewRow();
                                            col3 = true;
                                            string[] trans = s.Replace(@"/", "").Trim().Split(' ');
                                            Col4 = trans[1].ToString();
                                            Col5 = trans[2].ToString();
                                            Col6 = trans[3].ToString().Replace("Txn", "");
                                            balance = Convert.ToDouble(Col6);
                                            string desc = "";
                                            string chequeno = "";
                                            desc = Col3;

                                            row["txnDate"] = Col1;
                                            row["valuedate"] = Col2;
                                            row["description"] = desc;
                                            row["chequeno"] = desc;
                                            row["branchcode"] = Col4;
                                            row["debit"] = Col5;
                                            row["credit"] = Col5;
                                            row["balance"] = Col6;
                                            dtt.Rows.Add(row);
                                            col1 = false;
                                            Col1 = "";
                                            Col2 = "";
                                            Col3 = "";
                                            Col4 = "";
                                            Col5 = "";
                                            Col6 = "";
                                            col2 = false;
                                            col3 = false;
                                            col4 = false;
                                            col5 = false;
                                            col6 = false;
                                            col7 = false;
                                        }
                                        else
                                        {
                                            bool isNumeric42 = long.TryParse(s.Replace(@"/", "").Replace(".", "").Trim().Split(' ')[1].ToString(), out n1);
                                            if (isNumeric42)
                                            {
                                                string[] transs = s.Replace(@"/", "").Trim().Split(' ');
                                                brcode = transs[0];
                                                brdebit = transs[1];
                                                brcredit = transs[1];
                                                credit_yes = true;
                                            }
                                            else
                                            {
                                                Col3 = Col3 + " " + s;
                                            }
                                        }


                                    }
                                    else
                                    {
                                        if (isNumeric3 || isNumeric3_1)
                                        {
                                            brcode = s.Replace(@"/", "").Trim().Split(' ')[1];
                                            brdebit = s.Replace(@"/", "").Trim().Split(' ')[2];
                                            brcredit = s.Replace(@"/", "").Trim().Split(' ')[2];
                                            credit_yes = true;
                                        }
                                        Col3 = Col3 + " " + s;
                                    }
                                }
                                else
                                {
                                    //////////////////////////////////////////////////
                                    string[] ppp = s.Replace("--", "-").Split('-');
                                    if (ppp.Length >= 2)
                                    {
                                        
                                        if (ppp[1].Trim().Split(' ').Length == 2)
                                        {
                                            long n1;
                                            bool isNumeric441 = long.TryParse(ppp[1].Trim().Split(' ')[0].ToString(), out n1);
                                            bool isNumeric442 = long.TryParse(ppp[1].Trim().Split(' ')[1].ToString(), out n1);
                                            if (isNumeric441 && isNumeric442)
                                            {
                                                row = dtt.NewRow();
                                                col3 = true;
                                                string[] trans = s.Replace(@"/", "").Trim().Split(' ');
                                                Col4 = "";
                                                Col5 = ppp[1].Trim().Split(' ')[0].ToString();
                                                Col6 = ppp[1].Trim().Split(' ')[1].ToString();
                                                balance = Convert.ToDouble(Col6);
                                                string desc = "";
                                                string chequeno = "";
                                                desc = Col3;

                                                row["txnDate"] = Col1;
                                                row["valuedate"] = Col2;
                                                row["description"] = ppp[0].Trim().ToString();
                                                row["chequeno"] = desc;
                                                row["branchcode"] = Col4;
                                                row["debit"] = Col5;
                                                row["credit"] = Col5;
                                                row["balance"] = Col6;
                                                dtt.Rows.Add(row);
                                                col1 = false;
                                                Col1 = "";
                                                Col2 = "";
                                                Col3 = "";
                                                Col4 = "";
                                                Col5 = "";
                                                Col6 = "";
                                                col2 = false;
                                                col3 = false;
                                                col4 = false;
                                                col5 = false;
                                                col6 = false;
                                                col7 = false;
                                            }
                                            else
                                            {
                                                Col3 = Col3 + " " + s;
                                            }
                                        }
                                        else
                                        {
                                            long n1;
                                            bool isNumeric1 = long.TryParse(s.Replace(@"/", "").Trim().Split(' ')[0].ToString(), out n1);
                                            bool isNumeric2 = s.Replace(@"/", "").Trim().Split(' ')[1].Contains(',');
                                            bool isNumeric2_1 = s.Replace(@"/", "").Trim().Split(' ')[1].Contains('.');
                                            bool isNumeric3 = false;
                                            try
                                            {
                                                isNumeric3 = s.Replace(@"/", "").Trim().Split(' ')[2].Contains(',');
                                            }
                                            catch { }
                                            bool isNumeric3_1 = false;
                                            try
                                            {
                                                isNumeric3_1 = s.Replace(@"/", "").Trim().Split(' ')[2].Contains('.');
                                            }
                                            catch { }
                                            if (isNumeric1 && (isNumeric2 || isNumeric2_1))
                                            {
                                                if (isNumeric3 || isNumeric3_1)
                                                {
                                                    row = dtt.NewRow();
                                                    col3 = true;
                                                    string[] trans = s.Replace(@"/", "").Trim().Split(' ');
                                                    Col4 = trans[0].ToString();
                                                    Col5 = trans[1].ToString();
                                                    Col6 = trans[2].ToString().Replace("Txn", "");
                                                    balance = Convert.ToDouble(Col6);
                                                    string desc = "";
                                                    string chequeno = "";
                                                    desc = Col3;

                                                    row["txnDate"] = Col1;
                                                    row["valuedate"] = Col2;
                                                    row["description"] = desc;
                                                    row["chequeno"] = desc;
                                                    row["branchcode"] = Col4;
                                                    row["debit"] = Col5;
                                                    row["credit"] = Col5;
                                                    row["balance"] = Col6;
                                                    dtt.Rows.Add(row);
                                                    col1 = false;
                                                    Col1 = "";
                                                    Col2 = "";
                                                    Col3 = "";
                                                    Col4 = "";
                                                    Col5 = "";
                                                    Col6 = "";
                                                    col2 = false;
                                                    col3 = false;
                                                    col4 = false;
                                                    col5 = false;
                                                    col6 = false;
                                                    col7 = false;
                                                }
                                                else
                                                {

                                                    bool isNumeric42 = long.TryParse(s.Replace(@"/", "").Replace(".", "").Trim().Split(' ')[1].ToString(), out n1);
                                                    if (isNumeric42)
                                                    {
                                                        string[] transs = s.Replace(@"/", "").Trim().Split(' ');
                                                        brcode = transs[0];
                                                        brdebit = transs[1];
                                                        brcredit = transs[1];
                                                        credit_yes = true;
                                                    }
                                                    else
                                                    {
                                                        Col3 = Col3 + " " + s;
                                                    }
                                                }


                                            }
                                            else
                                            {
                                                if (isNumeric3 || isNumeric3_1)
                                                {
                                                    brcode = s.Replace(@"/", "").Trim().Split(' ')[1];
                                                    brdebit = s.Replace(@"/", "").Trim().Split(' ')[2];
                                                    brcredit = s.Replace(@"/", "").Trim().Split(' ')[2];
                                                    credit_yes = true;
                                                }
                                                Col3 = Col3 + " " + s;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (ppp.Length == 1)
                                        {
                                            int L = ppp[0].ToString().Trim().Split(' ').Length;
                                            if (ppp[0].ToString().Trim().Split(' ').Length == 2)
                                            {
                                                long n1;
                                                bool isNumeric42 = long.TryParse(ppp[0].Trim().Split(' ')[1].Replace(",", "").Replace(".", ""), out n1);
                                                if (isNumeric42)
                                                {
                                                    row = dtt.NewRow();
                                                    col3 = true;
                                                    string[] trans = s.Replace(@"/", "").Trim().Split(' ');
                                                    Col4 = "";
                                                    Col5 = ppp[0].Trim().Split(' ')[0];
                                                    Col6 = ppp[0].Trim().Split(' ')[1];
                                                    balance = Convert.ToDouble(Col6);
                                                    string desc = "";
                                                    string chequeno = "";
                                                    desc = Col3;

                                                    row["txnDate"] = Col1;
                                                    row["valuedate"] = Col2;
                                                    row["description"] = desc;
                                                    row["chequeno"] = desc;
                                                    row["branchcode"] = Col4;
                                                    row["debit"] = Col5;
                                                    row["credit"] = Col5;
                                                    row["balance"] = Col6;
                                                    dtt.Rows.Add(row);
                                                    col1 = false;
                                                    Col1 = "";
                                                    Col2 = "";
                                                    Col3 = "";
                                                    Col4 = "";
                                                    Col5 = "";
                                                    Col6 = "";
                                                    col2 = false;
                                                    col3 = false;
                                                    col4 = false;
                                                    col5 = false;
                                                    col6 = false;
                                                    col7 = false;
                                                }
                                                else
                                                {
                                                    Col3 = Col3 + " " + s;
                                                }
                                            }
                                            else
                                            {
                                                if (ppp[0].ToString().Trim().Split(' ').Length == 3)
                                                {
                                                    long n1;
                                                    bool isNumeric42 = long.TryParse(ppp[0].Trim().Split(' ')[2].Replace(",","").Replace(".",""), out n1);
                                                if (isNumeric42)
                                                {
                                                    row = dtt.NewRow();
                                                    col3 = true;
                                                    string[] trans = s.Replace(@"/", "").Trim().Split(' ');
                                                    Col4 = ppp[0].Trim().Split(' ')[0];
                                                    Col5 = ppp[0].Trim().Split(' ')[1];
                                                    Col6 = ppp[0].Trim().Split(' ')[2];
                                                    balance = Convert.ToDouble(Col6);
                                                    string desc = "";
                                                    string chequeno = "";
                                                    desc = Col3;

                                                    row["txnDate"] = Col1;
                                                    row["valuedate"] = Col2;
                                                    row["description"] = desc;
                                                    row["chequeno"] = desc;
                                                    row["branchcode"] = Col4;
                                                    row["debit"] = Col5;
                                                    row["credit"] = Col5;
                                                    row["balance"] = Col6;
                                                    dtt.Rows.Add(row);
                                                    col1 = false;
                                                    Col1 = "";
                                                    Col2 = "";
                                                    Col3 = "";
                                                    Col4 = "";
                                                    Col5 = "";
                                                    Col6 = "";
                                                    col2 = false;
                                                    col3 = false;
                                                    col4 = false;
                                                    col5 = false;
                                                    col6 = false;
                                                    col7 = false;
                                                }
                                                else
                                                {
                                                    Col3 = Col3 + " " + s;
                                                }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Col3 = Col3 + " " + s;
                            }
                        }
                    }
                    else
                    {
                        if (s.Contains(",") || s.Contains("."))
                        {
                            brbal = s.Replace("Txn", "");
                            row = dtt.NewRow();
                            col3 = true;
                            balance = Convert.ToDouble(brbal);
                            string desc = "";
                            string chequeno = "";
                            desc = Col3;

                            row["txnDate"] = Col1;
                            row["valuedate"] = Col2;
                            row["description"] = desc;
                            row["chequeno"] = desc;
                            row["branchcode"] = Col4;
                            row["debit"] = brdebit;
                            row["credit"] = brcredit;
                            row["balance"] = balance;
                            dtt.Rows.Add(row);
                            col1 = false;
                            Col1 = "";
                            Col2 = "";
                            Col3 = "";
                            Col4 = "";
                            Col5 = "";
                            Col6 = "";
                            col2 = false;
                            col3 = false;
                            col4 = false;
                            col5 = false;
                            col6 = false;
                            col7 = false;
                            credit_yes = false;
                            m_chk = 0;
                        }
                    }
                }
            }
            return dtt;
        }

        public DataTable ReadBasicInfo(string TableValues)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AccountName");
            dt.Columns.Add("Date");
            dt.Columns.Add("AccountNo");
            dt.Columns.Add("AccDesc");
            dt.Columns.Add("Branch");
            dt.Columns.Add("DrawingPower");
            dt.Columns.Add("InterestRate");
            dt.Columns.Add("MODBalance");
            dt.Columns.Add("CIFNo");
            dt.Columns.Add("IFSCode");
            dt.Columns.Add("MICRCode");
            dt.Columns.Add("Balance");
            dt.Columns.Add("Address");

            DataRow drow = dt.NewRow();
            foreach (string s in TableValues.Split('\n'))
            {
                string[] txt = s.Split(':');
                if (txt.Length == 2)
                {
                    if (txt[0].Trim().Contains("Name"))
                    {
                        drow["AccountName"] = txt[1].ToString();
                    }
                    if (txt[0].Trim() == "Date")
                    {
                        drow["Date"] = txt[1].ToString();
                    }
                    if (txt[0].Trim() == "Account Number")
                    {
                        drow["AccountNo"] = txt[1].ToString();
                    }
                    if (txt[0].Trim() == "Account Description")
                    {
                        drow["AccDesc"] = txt[1].ToString();
                    }
                    if (txt[0].Trim() == "Branch")
                    {
                        drow["Branch"] = txt[1].ToString();
                    }
                    if (txt[0].Trim() == "Drawing Power")
                    {
                        drow["DrawingPower"] = txt[1].ToString();
                    }
                    if (txt[0].Trim().Contains("Interest"))
                    {
                        drow["InterestRate"] = txt[1].ToString();
                    }
                    if (txt[0].Trim() == "MOD Balance")
                    {
                        drow["MODBalance"] = txt[1].ToString();
                    }
                    if (txt[0].Trim() == "CIF No.")
                    {
                        drow["CIFNo"] = txt[1].ToString();
                    }
                    if (txt[0].Trim() == "IFS Code")
                    {
                        drow["IFSCode"] = txt[1].ToString();
                    }
                    if (txt[0].Trim() == "MICR Code")
                    {
                        drow["MICRCode"] = txt[1].ToString();
                    }
                    if (txt[0].Trim().Contains("Balance as on"))
                    {
                        drow["Balance"] = txt[1].ToString();
                    }
                    if (txt[0].Trim().Contains("Address"))
                    {
                        drow["Address"] = txt[0].ToString();
                    }
                }
            }
            dt.Rows.Add(drow);
            return dt;
        }
        public static string DataTableToCSV(DataTable datatable, char seperator)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < datatable.Columns.Count; i++)
            {
                sb.Append(datatable.Columns[i]);
                if (i < datatable.Columns.Count - 1)
                    sb.Append(seperator);
            }
            sb.AppendLine();
            foreach (DataRow dr in datatable.Rows)
            {
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    sb.Append(dr[i].ToString());

                    if (i < datatable.Columns.Count - 1)
                        sb.Append(seperator);


                }
                sb.AppendLine();
            }

            return sb.ToString();
        }


        public string ReadPdfFile(string fileName, bool IsPasswordProtected, string Password)
        {
            StringBuilder text = new StringBuilder();

            if (File.Exists(fileName))
            {
                byte[] toBytes = Encoding.ASCII.GetBytes(Password);
                PdfReader pdfReader;
                if (!IsPasswordProtected)
                {
                    pdfReader = new PdfReader(fileName);
                }
                else
                {
                    pdfReader = new PdfReader(fileName, toBytes);
                }

                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                    currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                    text.Append(currentText);
                }
                pdfReader.Close();
            }
            return text.ToString();
        }
    }
}