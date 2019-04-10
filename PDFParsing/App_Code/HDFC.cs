using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace PDFParsing
{
    public class HDFC
    {
        public DataSet Export_HDFC_PDFToExcel(string fileName, bool IsPasswordProtected, string Password)
        {
            DataSet ds = new DataSet();
            DataTable dt_1 = new DataTable();
            dt_1.Columns.Add("COUNT_FILES");
            dt_1.Columns.Add("DOCUMENT_ID");
            dt_1.Columns.Add("PARSER_ID");
            dt_1.Columns.Add("PARSER_NAME");
            dt_1.Columns.Add("ADDRESS1");
            dt_1.Columns.Add("ADDRESS2");
            dt_1.Columns.Add("ADDRESS3");
            dt_1.Columns.Add("CITY");
            dt_1.Columns.Add("STATE");
            dt_1.Columns.Add("ACCOUNT_BRANCH");
            dt_1.Columns.Add("BRANCH_ADDRESS");
            dt_1.Columns.Add("BRANCH_CITY");
            dt_1.Columns.Add("BRANCH_STATE");
            dt_1.Columns.Add("BRANCH_PHONENO");
            dt_1.Columns.Add("CURRENCY");
            dt_1.Columns.Add("EMAIL");
            dt_1.Columns.Add("CUSTID");
            dt_1.Columns.Add("ACCOUNTNO");
            dt_1.Columns.Add("ACCOUNT_OPEN_DATE");
            dt_1.Columns.Add("ACCOUNT_STATUS");
            dt_1.Columns.Add("BRANCH_CODE");
            dt_1.Columns.Add("PRODUCT_CODE");
            dt_1.Columns.Add("NOMINATION");

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
            decimal ClosingBalance1 = 0;
            decimal ClosingBalance2 = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add("Date");
            dt.Columns.Add("Narration");
            dt.Columns.Add("ChqNo");
            dt.Columns.Add("ValueDt");
            dt.Columns.Add("WithdrawalAmt");
            dt.Columns.Add("DepositAmt");
            dt.Columns.Add("ClosingBalance");
            for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            {

                if (page == 1)
                {
                    ClosingBalance2 = GetOpeningBalance(fileName, IsPasswordProtected, Password);
                    SimpleTextExtractionStrategy its = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
                    ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                    currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.UTF8.GetBytes(currentText)));
                    text.Append(currentText);
                    pdfReader.Close();
                    string[] data = currentText.Split('\n');
                    //Creating DataTable
                    DataTable dt_AccountInfo = new DataTable();



                    var result = string.Join("<br>", currentText);
                    data = result.Split('\n');
                    int i1 = 0;
                    int j1 = 0;
                    DataRow r1 = dt_1.NewRow();
                    foreach (string s in data)
                    {
                        if (j1 == 0)
                        {
                            if (s.Contains(":"))
                            {

                                string[] dataRow = s.Split(':');
                                if (dataRow[0].ToString().Trim() == "Account Branch")
                                {
                                    r1["ACCOUNT_BRANCH"] = dataRow[1].ToString();
                                    r1["PARSER_NAME"] = data[6].ToString();
                                    r1["ADDRESS1"] = data[8].ToString();
                                    r1["ADDRESS2"] = data[11].ToString();
                                    r1["ADDRESS3"] = data[13].ToString();
                                    r1["CITY"] = data[15].ToString();
                                    r1["STATE"] = data[18].ToString();
                                }



                                if (dataRow[0].ToString().Trim() == "Address")
                                {
                                    r1["BRANCH_ADDRESS"] = dataRow[1].ToString() + data[3].ToString() + " " + data[4].ToString();
                                }
                                if (dataRow[0].ToString().Trim() == "City")
                                {
                                    r1["BRANCH_CITY"] = dataRow[1].ToString();
                                }
                                if (dataRow[0].ToString().Trim() == "State")
                                {
                                    r1["BRANCH_STATE"] = dataRow[1].ToString();
                                }
                                if (dataRow[0].ToString().Trim() == "Phone no.")
                                {
                                    r1["BRANCH_PHONENO"] = dataRow[1].ToString();
                                }
                                if (dataRow[0].ToString().Trim() == "Currency")
                                {
                                    r1["CURRENCY"] = dataRow[1].ToString();
                                }
                                if (dataRow[0].ToString().Trim() == "Email")
                                {
                                    r1["EMAIL"] = dataRow[1].ToString();
                                }
                                if (dataRow[0].ToString().Trim() == "Cust ID")
                                {
                                    r1["CUSTID"] = dataRow[1].ToString();
                                }
                                if (dataRow[0].ToString().Trim() == "Account No")
                                {
                                    r1["ACCOUNTNO"] = dataRow[1].ToString();
                                }
                                if (dataRow[0].ToString().Trim() == "A/C Open Date")
                                {
                                    r1["ACCOUNT_OPEN_DATE"] = dataRow[1].ToString();
                                }
                                if (dataRow[0].ToString().Trim() == "Account Status")
                                {
                                    r1["ACCOUNT_STATUS"] = dataRow[1].ToString();
                                }
                                if (dataRow[0].ToString().Trim() == "Branch Code")
                                {
                                    string[] aa = dataRow[1].ToString().Split(' ');
                                    r1["BRANCH_CODE"] = aa[0].ToString();
                                    r1["PRODUCT_CODE"] = aa[4].ToString();
                                }
                                if (dataRow[0].ToString().Trim() == "Nomination")
                                {
                                    r1["NOMINATION"] = dataRow[1].ToString();
                                }

                            }
                        }
                        if (s.Contains("Narration"))
                        {
                            i1 = 1;
                        }
                        if (j1 == 1)
                        {
                            try
                            {
                                string[] splitt = s.Split(' ');
                                DataRow row = dt.NewRow();
                                row[0] = splitt[0].ToString();
                                int pp = 6;
                                if (splitt[0].ToString().Split('/').Length == 3)
                                {
                                    bool check = false;
                                    for (int kk = splitt.Length - 1; kk >= 0; kk--)
                                    {
                                        string value = splitt[kk].ToString();
                                        if (pp == 6)
                                        {
                                            ClosingBalance1 = Convert.ToDecimal(value);
                                            if (ClosingBalance1 >= ClosingBalance2)
                                            {
                                                ClosingBalance2 = ClosingBalance1;
                                                row[pp] = value;
                                                pp = pp - 1;
                                                check = true;
                                            }
                                            else
                                            {
                                                ClosingBalance2 = ClosingBalance1;
                                                row[pp] = value;
                                                pp = pp - 1;
                                                check = false;
                                            }
                                        }
                                        else if (pp == 5)
                                        {
                                            if (check == true)
                                            {
                                                row[5] = value;
                                                pp = 5 - 2;
                                            }
                                            else
                                            {
                                                row[4] = value;
                                                pp = 4 - 1;
                                            }
                                        }
                                        else
                                            if (pp == 3)
                                            {
                                                row[pp] = value;
                                                pp = pp - 1;
                                            }
                                            else
                                                if (pp == 2)
                                                {
                                                    row[pp] = value;
                                                    pp = pp - 1;
                                                }
                                                else
                                                    if (pp == 1)
                                                    {
                                                        value = "";
                                                        for (int pi = 1; pi <= kk; pi++)
                                                        {
                                                            string res = splitt[pi].ToString();
                                                            value = value + " " + res;
                                                        }
                                                        row[pp] = value;
                                                        pp = pp - 1;
                                                    }
                                    }
                                    dt.Rows.Add(row);
                                }
                            }
                            catch { }
                        }
                        if (i1 >= 1)
                        {
                            j1 = 1;
                        }
                    }
                    dt_1.Rows.Add(r1);
                }
                else
                {
                    ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                    currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.UTF8.GetBytes(currentText)));
                    text.Append(currentText);
                    pdfReader.Close();
                    var result = string.Join("<br>", currentText);
                    string[] data = result.Split('\n');
                    int i = 0;
                    int j = 0;
                    foreach (string s in data)
                    {
                        if (s.Contains("Statement of account") || s.Contains("Narration"))
                        {
                            i = 1;
                        }
                        if (j == 1)
                        {
                            try
                            {
                                string[] splitt = s.Split(' ');
                                DataRow row = dt.NewRow();
                                row[0] = splitt[0].ToString();
                                int pp = 6;
                                if (splitt[0].ToString().Split('/').Length == 3)
                                {
                                    bool check = false;
                                    for (int kk = splitt.Length - 1; kk >= 0; kk--)
                                    {
                                        string value = splitt[kk].ToString();
                                        if (pp == 6)
                                        {
                                            ClosingBalance1 = Convert.ToDecimal(value);
                                            if (ClosingBalance1 >= ClosingBalance2)
                                            {
                                                ClosingBalance2 = ClosingBalance1;
                                                row[pp] = value;
                                                pp = pp - 1;
                                                check = true;
                                            }
                                            else
                                            {
                                                ClosingBalance2 = ClosingBalance1;
                                                row[pp] = value;
                                                pp = pp - 1;
                                                check = false;
                                            }
                                        }
                                        else if (pp == 5)
                                        {
                                            if (check == true)
                                            {
                                                row[5] = value;
                                                pp = 5 - 2;
                                            }
                                            else
                                            {
                                                row[4] = value;
                                                pp = 4 - 1;
                                            }
                                        }
                                        else
                                            if (pp == 3)
                                            {
                                                row[pp] = value;
                                                pp = pp - 1;
                                            }
                                            else
                                                if (pp == 2)
                                                {
                                                    row[pp] = value;
                                                    pp = pp - 1;
                                                }
                                                else
                                                    if (pp == 1)
                                                    {
                                                        value = "";
                                                        for (int pi = 1; pi <= kk; pi++)
                                                        {
                                                            string res = splitt[pi].ToString();
                                                            value = value + " " + res;
                                                        }
                                                        row[pp] = value;
                                                        pp = pp - 1;
                                                    }
                                    }
                                    dt.Rows.Add(row);
                                }
                            }
                            catch { }
                        }
                        if (i >= 1)
                        {
                            j = 1;
                        }
                    }
                }
            }
            ds.Tables.Add(dt_1);
            ds.Tables.Add(dt);

            return ds;
        }
        private decimal GetOpeningBalance(string fileName, bool IsPasswordProtected, string Password)
        {
            decimal Result = 0;
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
            SimpleTextExtractionStrategy its = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
            ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
            string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, pdfReader.NumberOfPages, strategy);
            currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.UTF8.GetBytes(currentText)));
            text.Append(currentText);
            pdfReader.Close();

            string[] data = currentText.Split('\n');

            var result = string.Join("<br>", currentText);
            data = result.Split('\n');
            int i1 = 0;
            int j1 = 0;
            foreach (string s in data)
            {
                if (s.Contains("Opening Balance"))
                {
                    i1 = 1;
                }
                if (j1 == 1)
                {
                    string[] splitt = s.Split(' ');
                    Result = Convert.ToDecimal(splitt[0].ToString());
                    break;
                }
                if (i1 >= 1)
                {
                    j1 = 1;
                }
            }
            return Result;
        }
    }
}
