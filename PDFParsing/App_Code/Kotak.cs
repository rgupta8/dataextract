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
    public class Kotak
    {
        public DataSet Export_HDFC_PDFToExcel(string fileName, bool IsPasswordProtected, string Password)
        {
            DataSet ds = new DataSet();
            DataTable dt_1 = new DataTable();
            dt_1.Columns.Add("COUNT_FILES");

            StringBuilder text = new StringBuilder();
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
            DataTable dt01 = new DataTable();
            DataTable dt_basic = new DataTable();
            dt01.Columns.Add("S.No.");
            dt01.Columns.Add("Date");
            dt01.Columns.Add("Descp");
            dt01.Columns.Add("Ref.No.");
            dt01.Columns.Add("Amount");
            dt01.Columns.Add("DR/CR");
            dt01.Columns.Add("Balance");
            dt01.Columns.Add("CR");
            for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            {

                // ClosingBalance2 = GetOpeningBalance(fileName, IsPasswordProtected, Password);
                SimpleTextExtractionStrategy its = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
                ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.UTF8.GetBytes(currentText)));
                text.Append(currentText);
                if (dt_basic.Rows.Count == 0)
                {
                    dt_basic = GetbasicInfo(currentText);
                }
                pdfReader.Close();
                int c1 = currentText.IndexOf("Sl. No.");

                int c3 = currentText.IndexOf("Statement of Banking Account");
                int c2 = currentText.IndexOf("Date");
                if (c1 >= 0)
                {
                    string ToUse = currentText.Substring(c1);

                    int StartIndex = ToUse.IndexOf("Balance Dr / Cr");

                    string Text = ToUse.Substring(StartIndex + 15);
                    string[] data = Text.Split('\n');

                    int p1 = 0;
                    DataRow row4 = dt01.NewRow();
                    string descp = "";
                    int Final = 0;
                    foreach (string s in data)
                    {
                        if (s != "")
                        {
                            if (s.Contains("Opening balance"))
                            {
                                break;
                            }
                            bool chk = false;
                            string[] txt = s.Split(' ');
                            if (p1 == 0)
                            {
                                row4 = dt01.NewRow();
                                p1 = 1;
                            }
                            int count = txt.Length;

                            for (int p = txt.Length - 1; p >= 0; p--)
                            {
                                if (chk == false)
                                {
                                    chk = true;
                                    if (txt[p] == "CR" || txt[p] == "DR")
                                    {
                                        if (descp != "")
                                        {
                                            row4["Descp"] = descp;
                                            dt01.Rows.Add(row4);
                                            p1 = 0;
                                            descp = "";
                                            row4 = dt01.NewRow();
                                            p1 = 1;
                                        }
                                        row4["CR"] = txt[txt.Length - 1];
                                        row4["Balance"] = txt[txt.Length - 2];
                                        row4["DR/CR"] = txt[txt.Length - 3];
                                        row4["Amount"] = txt[txt.Length - 4];
                                        row4["Ref.No."] = txt[txt.Length - 5];

                                        for (int k4 = 0; k4 <= txt.Length - 6; k4++)
                                        {
                                            descp = descp + " " + txt[k4].ToString();
                                        }

                                    }
                                    else
                                    {
                                        if (txt.Length == 2)
                                        {
                                            if (txt[1].ToString().Split('/').Length == 3 && txt[1].ToString().Length == 10)
                                            {
                                                row4["S.No."] = txt[0].ToString();
                                                row4["Date"] = txt[1].ToString();
                                            }
                                            else
                                            {

                                                foreach (string s1 in txt)
                                                {
                                                    descp = descp + " " + s1;
                                                    row4["Descp"] = descp;

                                                }
                                            }
                                        }
                                        else
                                        {
                                            Final = 0;
                                            foreach (string s1 in txt)
                                            {
                                                descp = descp + " " + s1;
                                                row4["Descp"] = descp;
                                                Final = 1;
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (descp != "")
                    {
                        row4["Descp"] = descp;
                        dt01.Rows.Add(row4);
                        p1 = 0;
                        descp = "";
                        p1 = 1;
                    }


                }
                else if (c3 >= 0)
                {
                    string ToUse = currentText.Substring(c3);

                    int StartIndex = ToUse.IndexOf("Balance");

                    string Text = ToUse.Substring(StartIndex + 7);
                    string[] data = Text.Split('\n');

                    int p1 = 0;
                    DataRow row4 = dt01.NewRow();
                    string descp = "";
                    int Final = 0;
                    foreach (string s in data)
                    {
                        if (s != "" && !s.Contains("B/F"))
                        {
                            if (s.Contains("Statement Summary"))
                            {
                                break;
                            }
                            bool chk = false;
                            string[] txt = s.Split(' ');
                            if (p1 == 0)
                            {
                                row4 = dt01.NewRow();
                                p1 = 1;
                            }
                            int count = txt.Length;

                            for (int p = txt.Length - 1; p >= 0; p--)
                            {
                                if (chk == false)
                                {
                                    chk = true;
                                    if (txt[p].ToString().Contains("(Cr)") && txt.Length >= 4)
                                    {
                                        if (descp != "")
                                        {
                                            row4["Descp"] = descp;
                                            dt01.Rows.Add(row4);
                                            p1 = 0;
                                            descp = "";
                                            row4 = dt01.NewRow();
                                            p1 = 1;
                                        }
                                        row4["S.No."] = txt[0].ToString();
                                        row4["Date"] = txt[0].ToString();
                                        row4["Balance"] = txt[txt.Length - 1];
                                        row4["DR/CR"] = txt[txt.Length - 2];
                                        row4["Amount"] = txt[txt.Length - 2];
                                        row4["Ref.No."] = txt[txt.Length - 3];
                                        for (int k4 = 1; k4 <= txt.Length - 4; k4++)
                                        {
                                            descp = descp + " " + txt[k4].ToString();
                                        }

                                    }
                                    else
                                    {
                                        if (txt.Length == 2)
                                        {
                                            foreach (string s1 in txt)
                                            {
                                                descp = descp + " " + s1;
                                                row4["Descp"] = descp;

                                            }
                                        }
                                        else
                                        {
                                            Final = 0;
                                            foreach (string s1 in txt)
                                            {
                                                descp = descp + " " + s1;
                                                row4["Descp"] = descp;
                                                Final = 1;
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (descp != "")
                    {
                        row4["Descp"] = descp;
                        dt01.Rows.Add(row4);
                        p1 = 0;
                        descp = "";
                        p1 = 1;
                    }


                }
                else if (c2 >= 0)
                {
                    string ToUse = currentText.Substring(c2);

                    int StartIndex = ToUse.IndexOf("Balance");

                    string Text = ToUse.Substring(StartIndex + 7);
                    string[] data = Text.Split('\n');

                    int p1 = 0;
                    DataRow row4 = dt01.NewRow();
                    string descp = "";
                    int Final = 0;
                    foreach (string s in data)
                    {
                        if (s != "" && !s.Contains("B/F") && !s.Contains("Deposit (Cr)"))
                        {
                            if (s.Contains("Statement Summary"))
                            {
                                break;
                            }
                            bool chk = false;
                            string[] txt = s.Split(' ');
                            if (p1 == 0)
                            {
                                row4 = dt01.NewRow();
                                p1 = 1;
                            }
                            int count = txt.Length;

                            for (int p = txt.Length - 1; p >= 0; p--)
                            {
                                if (chk == false)
                                {
                                    chk = true;
                                    if (txt[p].ToString().Contains("(Cr)") && txt.Length>=5)
                                    {
                                        if (descp != "")
                                        {
                                            row4["Descp"] = descp;
                                            dt01.Rows.Add(row4);
                                            p1 = 0;
                                            descp = "";
                                            row4 = dt01.NewRow();
                                            p1 = 1;
                                        }
                                        row4["S.No."] = txt[0].ToString();
                                        row4["Date"] = txt[0].ToString();
                                        row4["Balance"] = txt[txt.Length - 1];
                                        row4["DR/CR"] = txt[txt.Length - 2];
                                        row4["Amount"] = txt[txt.Length - 2];
                                        row4["Ref.No."] = txt[txt.Length - 3];
                                        for (int k4 = 1; k4 <= txt.Length - 4; k4++)
                                        {
                                            descp = descp + " " + txt[k4].ToString();
                                        }

                                    }
                                    else
                                    {
                                        if (txt.Length == 2)
                                        {
                                            foreach (string s1 in txt)
                                            {
                                                descp = descp + " " + s1;
                                                row4["Descp"] = descp;

                                            }
                                        }
                                        else
                                        {
                                            Final = 0;
                                            foreach (string s1 in txt)
                                            {
                                                descp = descp + " " + s1;
                                                row4["Descp"] = descp;
                                                Final = 1;
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (descp != "")
                    {
                        row4["Descp"] = descp;
                        dt01.Rows.Add(row4);
                        p1 = 0;
                        descp = "";
                        p1 = 1;
                    }


                }

            }
            ds.Tables.Add(dt_basic);
            ds.Tables.Add(dt01);
            return ds;
        }

        private DataTable GetbasicInfo(string currentText)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("CustRelnNo");
            dt.Columns.Add("AccountNo");
            dt.Columns.Add("Currency");
            dt.Columns.Add("Branch");
            dt.Columns.Add("NomineeRegistered");

            DataRow row = dt.NewRow();
            int index = currentText.IndexOf("Date");
            string FilteredText = currentText.Remove(index, currentText.Length - index);
            int k = -1;
            string Address = "";
            foreach (string s in FilteredText.Split('\n'))
            {
                if (s != "")
                {
                    if (s == "Account Statement")
                    {
                        k = 1;
                    }
                    else if (k >= 1)
                    {
                        if (k == 1)
                        {
                            row["Name"] = s;
                            k = k + 1;
                        }
                        else if (k == 2)
                        {
                            string[] spt = s.Replace("Cust. Reln. No.", "$").Split('$');
                            row["Address"] = spt[0];
                            row["CustRelnNo"] = spt[1];
                            k = k + 1;
                        }
                        else if (k == 3)
                        {
                            string[] spt = s.Replace("Account No.", "$").Split('$');
                            row["AccountNo"] = spt[1];
                            k = k + 1;
                        }
                        else if (k == 4)
                        {
                            k = k + 1;
                        }
                        else if (k == 5)
                        {
                            string[] spt = s.Replace("Currency", "$").Split('$');
                            row["Currency"] = spt[1];
                            k = k + 1;
                        }
                        else if (k == 6)
                        {
                            string[] spt = s.Replace("Branch", "$").Split('$');
                            row["Branch"] = spt[1];
                            k = k + 1;
                        }
                        else if (k == 7)
                        {
                            string[] spt = s.Replace("Nomination Regd", "$").Split('$');
                            row["NomineeRegistered"] = spt[1];
                            k = k + 1;
                        }
                    }
                    else
                    {
                        string[] txt = s.Split(':');

                        if (txt.Length == 2)
                        {
                            if (txt[0].Contains("Period"))
                            {
                                string res = txt[0].Replace("Period", "");
                                row["Name"] = res;
                            }
                            else if (txt[0].Trim() == "Cust.Reln.No")
                            {
                                row["CustRelnNo"] = txt[1];
                            }
                            else if (txt[0].Trim() == "Account No")
                            {
                                row["AccountNo"] = txt[1];
                            }
                            else if (txt[0].Trim() == "Currency")
                            {
                                row["Currency"] = txt[1];
                            }
                            else if (txt[0].Trim() == "Branch")
                            {
                                row["Branch"] = txt[1];
                            }
                            else if (txt[0].Trim() == "Nominee Registered")
                            {
                                row["NomineeRegistered"] = txt[1];
                            }
                        }
                        else
                        {
                            Address = Address + txt[0].ToString();
                            row["Address"] = Address;
                        }
                    }
                }
            }
            dt.Rows.Add(row);
            return dt;
        }

        public DataSet ProcessPDF(string file, bool passwordprotected, string password)
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
            if (TableValues.IndexOf("Sl. No.") == -1)
            {
                string ToUse = TableValues.Substring(TableValues.IndexOf("Sl. No."));

                int StartIndex = ToUse.IndexOf("Balance");

                string Text = ToUse.Substring(StartIndex + 7);

                int Sno = 0;
                string descp = "";
                string amt = "";
                int p = 0;
                bool submit = false;
                DataRow row = dtt.NewRow();
                bool isdate = false;
                foreach (string s in Text.Split('\n'))
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
                            }
                            else if ((isNumeric1 && txt.Length == 3))
                            {
                                string a11 = "";
                                string a21 = "";
                                string a31 = "";

                                a11 = txt[0].ToString();
                                a21 = txt[1].ToString();
                                a31 = txt[2].ToString();
                                row["branchcode"] = a31;
                                row["debit"] = a11;
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
                double balance = 0;
                foreach (string s in Text.Split('\n'))
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
                        if (s.Replace(@"/", "").Trim().Split(' ').Length == 3)
                        {
                            long n1;
                            bool isNumeric1 = long.TryParse(s.Replace(@"/", "").Trim().Split(' ')[0].ToString(), out n1);
                            bool isNumeric2 = s.Replace(@"/", "").Trim().Split(' ')[1].Contains(',');
                            bool isNumeric2_1 = s.Replace(@"/", "").Trim().Split(' ')[1].Contains('.');
                            bool isNumeric3 = s.Replace(@"/", "").Trim().Split(' ')[2].Contains(',');
                            bool isNumeric3_1 = s.Replace(@"/", "").Trim().Split(' ')[2].Contains('.');
                            if (isNumeric1 && (isNumeric2 || isNumeric2_1) && (isNumeric3 || isNumeric3_1))
                            {
                                row = dtt.NewRow();
                                col3 = true;
                                string[] trans = s.Replace(@"/", "").Trim().Split(' ');
                                Col4 = trans[0].ToString();
                                Col5 = trans[1].ToString();
                                Col6 = trans[2].ToString().Replace("Txn", "");
                                if (Convert.ToDouble(Col6) <= balance)
                                {
                                    row["debit"] = Col5;
                                }
                                else
                                {
                                    row["credit"] = Col5;
                                }
                                balance = Convert.ToDouble(Col6);
                                string desc = "";
                                string chequeno = "";
                                int index = Col3.IndexOf("TRANSFER FROM");
                                if (index >= 0)
                                {
                                    desc = Col3.Substring(0, index);
                                    chequeno = Col3.Substring(index, Col3.Length - index - 1);
                                }

                                row["txnDate"] = Col1;
                                row["valuedate"] = Col2;
                                row["description"] = desc;
                                row["chequeno"] = chequeno;
                                row["branchcode"] = Col4;


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
                            Col3 = Col3 + " " + s;
                        }
                    }
                }
                //foreach (string s1 in txt)
                //{
                //    if (s1 != "")
                //    {
                //        long n1;
                //        bool isNumeric1 = long.TryParse(s1, out n1);


                //        if (s1.Split('/').Length - 1 == 2 && s1.Length == 10)
                //        {
                //            isdate = true;
                //            row[0] = s1;
                //            row[1] = s1;
                //        }
                //        else if ((isNumeric1 && txt.Length == 3))
                //        {
                //            string a11 = "";
                //            string a21 = "";
                //            string a31 = "";

                //            a11 = txt[0].ToString();
                //            a21 = txt[1].ToString();
                //            a31 = txt[2].ToString();
                //            row["branchcode"] = a31;
                //            row["debit"] = a11;
                //            row["credit"] = a21;
                //            row["balance"] = a31;
                //            if (isdate == true)
                //            {
                //                dtt.Rows.Add(row);
                //            }
                //            submit = true;
                //            descp = "";
                //            p = 0;
                //        }
                //        else
                //        {
                //            if (submit == false)
                //            {
                //                descp = descp + s1 + " ";
                //                row["description"] = descp;
                //            }
                //        }
                //    }
                //}

            }
            DataSet ds = new DataSet();
            
            ds.Tables.Add(dtt);
            return ds;
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
                    if (txt[0].Trim() == "Account Name")
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
                    if (txt[0].Trim().Contains("Interest Rate"))
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