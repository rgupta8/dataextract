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
    public class ICICI
    {
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

        public DataSet ProcessParsingPDF(string file, bool IsPasswordProtected, string Password)
        {
            HttpContext.Current.Session["sessionValue"] = null;
            DataSet ds = new DataSet();
            String TableValues = ReadPdfFile(file, IsPasswordProtected, Password);

            var builder = new StringBuilder();
            int startloc = 0, endloc = 0;
            Dictionary<int, List<string>> dicValues = new Dictionary<int, List<string>>();
            int RowCount = 1;
            int chk1 = 0;
            int chk2 = 0;
            int chk3 = 0;
            int chk4 = 0;
            chk1 = TableValues.IndexOf("Statement of Transactions in Current Account Number");
            chk2 = TableValues.IndexOf("S No");
            chk3 = TableValues.IndexOf("Statement of transactions in account number");
            chk4 = TableValues.IndexOf("Balance(INR)");
            if (chk1 >= 0)
            {
                #region Current Account Statement
                string ToUse = TableValues.Substring(chk1);
                DataTable ds_Basic = GetICICIBasic_CURRENT_SAVING(TableValues);
                ToUse = ToUse.Substring(0, ToUse.IndexOf("For ICICI Bank Limited"));

                ToUse = ToUse.Substring(ToUse.IndexOf("Location") + 8);

                Dictionary<int, string> lstPartParticulars = new Dictionary<int, string>();

                List<string> lst = ToUse.Split('\n').ToList();
                List<string> lstFinalVal = new List<string>();

                List<string> lstValToReplace = new List<string>();

                string PrevBalance = string.Empty;

                foreach (string strText1 in lst.ToList())
                {
                    try
                    {
                        string strText = strText1;
                        strText = strText1.Replace("Cr ", "");
                        string RemainingParticularText = string.Empty;
                        string transDate = string.Empty;
                        if (strText.Split(' ').Count() > 0)
                            transDate = strText.Split(' ')[0];
                        if (transDate.Split('-').Length - 1 == 2 && transDate.Length == 10)
                        {

                        }
                        else
                        {
                            RemainingParticularText = strText1.Trim();
                        }
                        string valueDate = string.Empty;
                        if (strText.Split(' ').Count() > 1)
                            valueDate = strText.Split(' ')[1];
                        string Balance = string.Empty;
                        string location = string.Empty;
                        string withdrawl = string.Empty;
                        string deposit = string.Empty;
                        string chequeNo = string.Empty;
                        string particulars = string.Empty;
                        if (valueDate.Split('-').Length - 1 == 2 && valueDate.Length == 10)
                        {

                        }
                        else
                        {
                            valueDate = string.Empty;
                        }


                        if (RemainingParticularText == string.Empty)
                        {
                            foreach (string str in strText.Split(' ').ToList())
                            {
                                string number = str;
                                long n1;
                                bool isNumeric1 = long.TryParse(number, out n1);
                                if (isNumeric1)
                                {
                                    if (!number.Contains("."))
                                    {
                                        int index1 = strText.Split(' ').ToList().FindIndex(a => a == number);
                                        //string D2 = strText.Split(' ')[index1 + 1];
                                        if (strText.Split(' ').Count() != index1 + 1 && number.Length == 6)
                                            chequeNo = number;
                                    }
                                }
                                if (str.Contains("."))
                                {
                                    number = str.Replace(",", "");
                                    number = number.Replace(".", "");
                                    long n;
                                    bool isNumeric = long.TryParse(number, out n);
                                    if (isNumeric)
                                    {
                                        int index = strText.Split(' ').ToList().FindIndex(a => a == str);
                                        string D1 = str;
                                        string D2 = strText.Split(' ')[index - 1];
                                        string D3 = strText.Split(' ')[index + 1];

                                        if (D3 == "D")
                                            D3 = strText.Split(' ')[index + 2];

                                        if (!D2.Contains(".") && !D3.Contains(".") && !D3.Contains("D"))
                                        {
                                            for (int i = index + 1; i < strText.Split(' ').ToList().Count; i++)
                                            {
                                                location += strText.Split(' ')[i] + " ";
                                            }

                                            location = location.Trim().Replace(transDate, "");
                                        }
                                        if (D3.Contains(".") && !D3.Contains("D"))
                                        {
                                            index = strText.Split(' ').ToList().FindIndex(a => a == D3);
                                            valueDate = strText.Split(' ')[index + 1];
                                            if (valueDate.Split('-').Length - 1 == 2 && valueDate.Length == 10)
                                            {
                                                for (int i = index + 2; i < strText.Split(' ').ToList().Count; i++)
                                                {
                                                    location += strText.Split(' ')[i] + " ";
                                                }
                                            }
                                            else
                                            {
                                                valueDate = string.Empty;
                                            }

                                            location = location.Replace(transDate, "");

                                            if (particulars == string.Empty)
                                                particulars = strText.Substring(10, strText.IndexOf(D1) - D1.Length).Replace(D1, "").Trim();
                                            Balance = D3;
                                            if (double.Parse(Balance.Replace(",", "")) > double.Parse(PrevBalance.Replace(",", "")))
                                            {
                                                //Deposit
                                                deposit = D1;
                                            }
                                            else
                                            {
                                                withdrawl = D1;
                                                //Withdrawl
                                            }
                                        }
                                        else
                                        {
                                            Balance = D1;
                                            if (particulars == string.Empty)
                                                particulars = strText.Substring(10, strText.IndexOf(D1) - 1).Trim().Replace(D1, "").Trim();
                                        }
                                    }
                                }
                            }
                            PrevBalance = Balance;
                            List<string> lstValues = new List<string>();
                            lstValues.Add(transDate);
                            lstValues.Add(valueDate);
                            lstValues.Add(particulars);
                            lstValues.Add(chequeNo);
                            lstValues.Add(location);
                            lstValues.Add(withdrawl);
                            lstValues.Add(deposit);
                            lstValues.Add(Balance);
                            dicValues.Add(RowCount, lstValues);
                            RowCount++;
                        }
                        else
                        {
                            if (!RemainingParticularText.Contains("This is an authenticated intimation/statement. Customers are requested to immediately"))
                            {
                                if (!RemainingParticularText.Contains("Statement of Transactions in Current Account Numbe"))
                                {
                                    if (!RemainingParticularText.Contains("Chq.No Particulars Tran Date Withdrawals Deposits Balance (INR) Value Date Location"))
                                    {
                                        if (!RemainingParticularText.Contains("ROG Dec"))
                                        {

                                            if (RemainingParticularText.Split(':')[0] != "Total")
                                            {
                                                if (RemainingParticularText.Split(':').Count() > 1)
                                                {
                                                    if (!RemainingParticularText.Split(':')[1].Contains("Cr"))
                                                    {
                                                        List<string> keyVal = dicValues[RowCount - 1];
                                                        string tempParticular = keyVal[2];
                                                        tempParticular += RemainingParticularText;
                                                        keyVal[2] = tempParticular;
                                                        dicValues[RowCount - 1] = keyVal;
                                                    }
                                                }
                                                else
                                                {
                                                    List<string> keyVal = dicValues[RowCount - 1];
                                                    string tempParticular = keyVal[2];
                                                    tempParticular += RemainingParticularText;
                                                    keyVal[2] = tempParticular;
                                                    dicValues[RowCount - 1] = keyVal;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        //do nothing
                    }
                }

                foreach (KeyValuePair<int, List<string>> keyVal in dicValues.ToList())
                {
                    List<string> lstVal = keyVal.Value;
                    lstVal[0] = lstVal[0].Replace(",", "");
                    lstVal[1] = lstVal[1].Replace(",", "");
                    lstVal[2] = lstVal[2].Replace(",", "");
                    lstVal[3] = lstVal[3].Replace(",", "");
                    lstVal[4] = lstVal[4].Replace(",", "");
                    lstVal[5] = lstVal[5].Replace(",", "");
                    lstVal[6] = lstVal[6].Replace(",", "");
                    lstVal[7] = lstVal[7].Replace(",", "");
                    if (lstVal[3] != string.Empty)
                    {
                        lstVal[2] = lstVal[2].Replace(lstVal[3], "");
                    }
                    dicValues[keyVal.Key] = lstVal;
                }

                DataTable dt = new DataTable();
                DataColumn dcTransDate = new DataColumn("TransDate");
                DataColumn dcValueDate = new DataColumn("ValueDate");
                DataColumn dcTransRemarks = new DataColumn("Particulars");
                DataColumn dcLocation = new DataColumn("Location");
                DataColumn dcChequeNumber = new DataColumn("ChequeNumber");
                DataColumn dcWithdrawl = new DataColumn("Withdrawl");
                DataColumn dcDepositAmount = new DataColumn("Deposit");
                DataColumn dcBalance = new DataColumn("Balance (INR)");

                dt.Columns.Add(dcTransDate);
                dt.Columns.Add(dcValueDate);
                dt.Columns.Add(dcTransRemarks);
                dt.Columns.Add(dcLocation);
                dt.Columns.Add(dcChequeNumber);
                dt.Columns.Add(dcWithdrawl);
                dt.Columns.Add(dcDepositAmount);
                dt.Columns.Add(dcBalance);


                //Value Date,Trans Date,Cheque Number,Balance,Deposit Amount,Withdrawl Amount,Trans Remarks

                foreach (KeyValuePair<int, List<string>> keyVal in dicValues)
                {
                    DataRow dr = dt.NewRow();
                    List<string> lstVal = keyVal.Value;
                    dr["TransDate"] = lstVal[0];
                    dr["ValueDate"] = lstVal[1];
                    dr["Particulars"] = lstVal[2];
                    dr["Location"] = lstVal[4];
                    dr["ChequeNumber"] = lstVal[3];
                    dr["Withdrawl"] = lstVal[5];
                    dr["Deposit"] = lstVal[6];
                    dr["Balance (INR)"] = lstVal[7];
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(ds_Basic);
                ds.Tables.Add(dt);
                #endregion
            }
            else if (chk2 >= 0)
            {
                #region Saving Account Statement
                string ToUse = TableValues.Substring(chk2);
                int chk33 = 0;
                chk33 = TableValues.IndexOf("DETAILED STATEMENT");
                DataTable ds_Basic = new DataTable();
                if (chk33 >= 0)
                {
                    ds_Basic = GetICICIBasic_CURRENT_SAVING1(TableValues);
                }
                else
                {
                    ds_Basic = GetICICIBasic_CURRENT_SAVING(TableValues);
                }
                int StartIndex = ToUse.IndexOfAny("0123456789".ToCharArray());

                string Text = ToUse.Substring(StartIndex + 2);

                int Sno = 0;
                while (Text.Length != 0)
                {
                    try
                    {
                        if (Text[Sno] == '\n')
                        {
                            Sno = Sno + 3;
                            string Date1 = Text.Substring(Sno).Trim().Split(' ')[0];
                            string Date2 = Text.Substring(Sno).Trim().Split(' ')[1];
                            //DateTime d1, d2;
                            if (Date1 == "30/03/2017")
                            {

                            }
                            if (Date1.Split('/').Length - 1 == 2 && Date1.Length == 10)
                            {

                            }
                            //if (DateTime.TryParseExact(Date1, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d1) && DateTime.TryParse(Date2, out d2))
                            //{

                            //}
                            else
                            {
                                Sno = Sno - 3;
                                string Date3 = Text.Substring(Sno).Trim().Split(' ')[0];
                                string Date4 = Text.Substring(Sno).Trim().Split(' ')[1];
                                if (Date3.Split('/').Length - 1 == 2 && Date3.Length == 10)
                                {

                                }
                                else
                                {
                                    var aStringBuilder = new StringBuilder(Text);
                                    aStringBuilder.Remove(Sno, 1);
                                    aStringBuilder.Insert(Sno, " ");
                                    Text = aStringBuilder.ToString();
                                }
                            }

                        }
                        Sno++;
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }

                List<string> lst = Text.Split('\n').ToList();
                List<string> lstFinalVal = new List<string>();

                foreach (string lstItem in lst.ToList())
                {
                    if (lstItem.Length < 7)
                        lst.Remove(lstItem);

                }

                for (int k = 0; k <= lst.Count() - 1; k++)
                {
                    string val = lst[k];
                    if (!(val.Split(' ')[0].Trim().Split('/').Length - 1 == 2 && val.Split(' ')[0].Trim().Length == 10))
                    {
                        lst.RemoveAt(k);
                        val = val.Substring(val.Split(' ')[0].Trim().Length);
                        lst.Insert(k, val.Trim());
                    }
                }

                for (int i = 0; i <= lst.Count() - 1; i++)
                {
                    string str = lst[i].Split(' ')[lst[i].Split(' ').Length - 1];
                    if (!str.Contains("."))
                    {
                        lst[i] = lst[i].Replace(str, "").Trim();
                    }
                }

                List<string> lstValToReplace = new List<string>();

                foreach (string lstItem in lst.ToList())
                {
                    string[] arrItems = lstItem.Split(' ');
                    bool FirstFound = false;
                    int TotalOccurences = 0;
                    for (int i = 0; i <= arrItems.Count() - 1; i++)
                    {
                        if (arrItems[i].Trim().Split('/').Length - 1 == 2 && arrItems[i].Trim().Length == 10)
                        {
                            FirstFound = true;
                        }
                        if (FirstFound)
                        {
                            if (arrItems[i + 1].Trim().Split('/').Length - 1 == 2 && arrItems[i + 1].Trim().Length == 10)
                            {
                                TotalOccurences++;
                                if (TotalOccurences == 2)
                                {
                                    string ItemToShift = string.Empty;
                                    for (int k = i; k <= arrItems.Length - 1; k++)
                                    {
                                        ItemToShift += " " + arrItems[k];
                                    }
                                    lst.Insert(RowCount, ItemToShift.Trim());
                                    lst = lst.Select(s => s.Replace(ItemToShift, "")).ToList();
                                }
                            }
                            FirstFound = false;
                        }
                    }
                    RowCount++;
                }

                RowCount = 1;
                foreach (string lstItem in lst.ToList())
                {
                    List<string> lstColumnValues = new List<string>();
                    for (int i = 0; i <= lstItem.Length - 1; i++)
                    {
                        if (lstItem[i].ToString() == " " || lstItem[i].ToString() == "-")
                        {
                            lstColumnValues.Add(builder.ToString());
                            builder = builder.Clear();
                            if (lstColumnValues.Count == 3)
                            {
                                startloc = i;
                                break;
                            }
                        }
                        else
                        {
                            builder.Append(lstItem[i]);
                        }
                    }
                    for (int i = lstItem.Length - 1; i >= 0; i--)
                    {
                        if (lstItem[i].ToString() == " " || lstItem[i].ToString() == "-")
                        {
                            lstColumnValues.Add(builder.ToString());
                            builder = builder.Clear();
                            if (lstColumnValues.Count == 6)
                            {
                                endloc = i;
                                break;
                            }
                        }
                        else
                        {
                            builder.Insert(0, lstItem[i]);
                        }
                    }
                    int length = endloc - startloc;
                    string summary = lstItem.Substring(startloc + 1, length);
                    lstColumnValues.Add(summary);
                    dicValues.Add(RowCount, lstColumnValues);
                    RowCount++;
                }


                DataTable dt = new DataTable();
                DataColumn dcValueDate = new DataColumn("ValueDate");
                DataColumn dcTransDate = new DataColumn("TransDate");
                DataColumn dcChequeNumber = new DataColumn("ChequeNumber");
                DataColumn dcBalance = new DataColumn("Balance");
                DataColumn dcDepositAmount = new DataColumn("DepositAmount");
                DataColumn dcWithdrawlAmount = new DataColumn("WithdrawlAmount");
                DataColumn dcTransRemarks = new DataColumn("TransRemarks");

                dt.Columns.Add(dcValueDate);
                dt.Columns.Add(dcTransDate);
                dt.Columns.Add(dcChequeNumber);
                dt.Columns.Add(dcBalance);
                dt.Columns.Add(dcDepositAmount);
                dt.Columns.Add(dcWithdrawlAmount);
                dt.Columns.Add(dcTransRemarks);

                //Value Date,Trans Date,Cheque Number,Balance,Deposit Amount,Withdrawl Amount,Trans Remarks

                foreach (KeyValuePair<int, List<string>> keyVal in dicValues)
                {
                    DataRow dr = dt.NewRow();
                    List<string> lstVal = keyVal.Value;
                    dr["ValueDate"] = lstVal[0];
                    dr["TransDate"] = lstVal[1];
                    dr["ChequeNumber"] = lstVal[2];
                    dr["Balance"] = lstVal[3];
                    dr["DepositAmount"] = lstVal[4];
                    dr["WithdrawlAmount"] = lstVal[5];
                    dr["TransRemarks"] = lstVal[6];
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(ds_Basic);
                ds.Tables.Add(dt);
                #endregion
            }
            else if (chk3 >= 0)
            {
                #region OD Account Statement
                string ToUse = TableValues.Substring(chk3);
                DataTable ds_Basic = GetICICIBasic(TableValues);
                ToUse = ToUse.Substring(0, ToUse.IndexOf("For ICICI Bank Limited"));

                ToUse = ToUse.Substring(ToUse.IndexOf("Balance(INR)") + 12);

                Dictionary<int, string> lstPartParticulars = new Dictionary<int, string>();

                List<string> lst = ToUse.Split('\n').ToList();
                List<string> lstFinalVal = new List<string>();

                List<string> lstValToReplace = new List<string>();

                string PrevBalance = string.Empty;

                foreach (string strText1 in lst.ToList())
                {
                    try
                    {
                        if (strText1 != "")
                        {
                            string text = "This is an authenticated intimation/statement. Customers are requested to immediately notify the Bank of any discrepancy in the statement";
                            string strText = strText1.Replace(text, "");
                            strText = strText1.Replace("Cr ", "");
                            string RemainingParticularText = string.Empty;
                            string transDate = string.Empty;
                            if (strText.Split(' ').Count() > 0)
                                transDate = strText.Split(' ')[0];
                            if (transDate.Split('-').Length - 1 == 2 && transDate.Length == 10)
                            {

                            }
                            else
                            {
                                RemainingParticularText = strText1.Replace(text, "").Trim();
                            }
                            string valueDate = string.Empty;
                            if (strText.Split(' ').Count() > 1)
                                valueDate = strText.Split(' ')[1];
                            string Balance = string.Empty;
                            string location = string.Empty;
                            string withdrawl = string.Empty;
                            string deposit = string.Empty;
                            string chequeNo = string.Empty;
                            string particulars = string.Empty;
                            if (valueDate.Split('-').Length - 1 == 2 && valueDate.Length == 10)
                            {

                            }
                            else
                            {
                                valueDate = string.Empty;
                            }


                            if (RemainingParticularText == string.Empty)
                            {
                                foreach (string str in strText.Split(' ').ToList())
                                {
                                    string number = str;
                                    long n1;
                                    bool isNumeric1 = long.TryParse(number, out n1);
                                    if (isNumeric1)
                                    {
                                        if (!number.Contains("."))
                                        {
                                            int index1 = strText.Split(' ').ToList().FindIndex(a => a == number);
                                            //string D2 = strText.Split(' ')[index1 + 1];
                                            if (strText.Split(' ').Count() != index1 + 1 && number.Length == 6)
                                                chequeNo = number;
                                        }
                                    }
                                    if (str.Contains("."))
                                    {
                                        number = str.Replace(",", "");
                                        number = number.Replace(".", "");
                                        long n;
                                        bool isNumeric = long.TryParse(number, out n);
                                        if (isNumeric)
                                        {
                                            int index = strText.Split(' ').ToList().FindIndex(a => a == str);
                                            string D1 = str;
                                            string D2 = strText.Split(' ')[index - 1];
                                            string D3 = strText.Split(' ')[index + 1];

                                            if (D3 == "D")
                                                D3 = strText.Split(' ')[index + 2];

                                            if (!D2.Contains(".") && !D3.Contains(".") && !D3.Contains("D"))
                                            {
                                                for (int i = index + 1; i < strText.Split(' ').ToList().Count; i++)
                                                {
                                                    location += strText.Split(' ')[i] + " ";
                                                }

                                                location = location.Trim().Replace(transDate, "");
                                            }
                                            if (D3.Contains(".") && !D3.Contains("D"))
                                            {
                                                index = strText.Split(' ').ToList().FindIndex(a => a == D3);
                                                valueDate = strText.Split(' ')[index + 1];
                                                if (valueDate.Split('-').Length - 1 == 2 && valueDate.Length == 10)
                                                {
                                                    for (int i = index + 2; i < strText.Split(' ').ToList().Count; i++)
                                                    {
                                                        location += strText.Split(' ')[i] + " ";
                                                    }
                                                }
                                                else
                                                {
                                                    valueDate = string.Empty;
                                                }

                                                location = location.Replace(transDate, "");

                                                if (particulars == string.Empty)
                                                    particulars = strText.Substring(10, strText.IndexOf(D1) - D1.Length).Replace(D1, "").Trim();
                                                Balance = D3;
                                                if (double.Parse(Balance.Replace(",", "")) > double.Parse(PrevBalance.Replace(",", "")))
                                                {
                                                    //Deposit
                                                    deposit = D1;
                                                }
                                                else
                                                {
                                                    withdrawl = D1;
                                                    //Withdrawl
                                                }
                                            }
                                            else
                                            {
                                                Balance = D1;
                                                if (particulars == string.Empty)
                                                    particulars = strText.Substring(10, strText.IndexOf(D1) - 1).Trim().Replace(D1, "").Trim();
                                            }
                                        }
                                    }
                                }
                                PrevBalance = Balance;
                                List<string> lstValues = new List<string>();
                                lstValues.Add(transDate);
                                lstValues.Add(valueDate);
                                lstValues.Add(particulars);
                                lstValues.Add(chequeNo);
                                lstValues.Add(location);
                                lstValues.Add(withdrawl);
                                lstValues.Add(deposit);
                                lstValues.Add(Balance);
                                dicValues.Add(RowCount, lstValues);
                                RowCount++;
                            }
                            else
                            {
                                if (!RemainingParticularText.Contains("This is an authenticated intimation/statement. Customers are requested to immediately"))
                                {
                                    if (!RemainingParticularText.Contains("Statement of transactions in account number"))
                                    {
                                        if (!RemainingParticularText.Contains("Category of service"))
                                        {
                                            if (!RemainingParticularText.Contains("REGD ADDRESS"))
                                            {
                                                if (!RemainingParticularText.Contains("Page"))
                                                {
                                                    if (RemainingParticularText.Split(':')[0] != "Total")
                                                    {
                                                        if (RemainingParticularText.Split(':').Count() > 1)
                                                        {
                                                            if (!RemainingParticularText.Split(':')[1].Contains("Cr"))
                                                            {
                                                                List<string> keyVal = dicValues[RowCount - 1];
                                                                string tempParticular = keyVal[2];
                                                                tempParticular = tempParticular + RemainingParticularText;
                                                                keyVal[2] = tempParticular;
                                                                dicValues[RowCount - 1] = keyVal;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            List<string> keyVal = dicValues[RowCount - 1];
                                                            string tempParticular = keyVal[2];
                                                            tempParticular = tempParticular + " " + RemainingParticularText;
                                                            keyVal[2] = tempParticular;
                                                            dicValues[RowCount - 1] = keyVal;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //do nothing
                    }
                }

                foreach (KeyValuePair<int, List<string>> keyVal in dicValues.ToList())
                {
                    List<string> lstVal = keyVal.Value;
                    lstVal[0] = lstVal[0].Replace(",", "");
                    lstVal[1] = lstVal[1].Replace(",", "");
                    lstVal[2] = lstVal[2].Replace(",", "");
                    lstVal[3] = lstVal[3].Replace(",", "");
                    lstVal[4] = lstVal[4].Replace(",", "");
                    lstVal[5] = lstVal[5].Replace(",", "");
                    lstVal[6] = lstVal[6].Replace(",", "");
                    lstVal[7] = lstVal[7].Replace(",", "");
                    if (lstVal[3] != string.Empty)
                    {
                        lstVal[2] = lstVal[2].Replace(lstVal[3], "");
                    }
                    dicValues[keyVal.Key] = lstVal;
                }

                DataTable dt = new DataTable();
                DataColumn dcTransDate = new DataColumn("TransDate");
                DataColumn dcValueDate = new DataColumn("ValueDate");
                DataColumn dcTransRemarks = new DataColumn("Particulars");
                DataColumn dcLocation = new DataColumn("Location");
                DataColumn dcChequeNumber = new DataColumn("ChequeNumber");
                DataColumn dcWithdrawl = new DataColumn("Withdrawl");
                DataColumn dcDepositAmount = new DataColumn("Deposit");
                DataColumn dcBalance = new DataColumn("Balance (INR)");

                dt.Columns.Add(dcTransDate);
                dt.Columns.Add(dcValueDate);
                dt.Columns.Add(dcTransRemarks);
                dt.Columns.Add(dcLocation);
                dt.Columns.Add(dcChequeNumber);
                dt.Columns.Add(dcWithdrawl);
                dt.Columns.Add(dcDepositAmount);
                dt.Columns.Add(dcBalance);


                //Value Date,Trans Date,Cheque Number,Balance,Deposit Amount,Withdrawl Amount,Trans Remarks

                foreach (KeyValuePair<int, List<string>> keyVal in dicValues)
                {
                    DataRow dr = dt.NewRow();
                    List<string> lstVal = keyVal.Value;
                    dr["TransDate"] = lstVal[0];
                    dr["ValueDate"] = lstVal[1];
                    dr["Particulars"] = lstVal[2];
                    dr["Location"] = lstVal[4];
                    dr["ChequeNumber"] = lstVal[3];
                    dr["Withdrawl"] = lstVal[5];
                    dr["Deposit"] = lstVal[6];
                    dr["Balance (INR)"] = lstVal[7];
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(ds_Basic);
                ds.Tables.Add(dt);
                #endregion
            }
            else if (chk4 >= 0)
            {
                #region OD Account Statement
                string ToUse = TableValues.Substring(chk4);
                DataTable ds_Basic = GetICICIBasic(TableValues);
                //ToUse = ToUse.Substring(0, ToUse.IndexOf("For ICICI Bank Limited"));

                ToUse = ToUse.Substring(ToUse.IndexOf("Balance(INR)") + 12);

                Dictionary<int, string> lstPartParticulars = new Dictionary<int, string>();

                List<string> lst = ToUse.Split('\n').ToList();
                List<string> lstFinalVal = new List<string>();

                List<string> lstValToReplace = new List<string>();

                string PrevBalance = string.Empty;
                bool multiline = false;
                int multiC = 1;
                foreach (string strText1 in lst.ToList())
                {
                    try
                    {
                        if (strText1 != "")
                        {
                            if (HttpContext.Current.Session["sessionValue"] == null)
                            {
                                multiC = 1;
                                string text = "This is an authenticated intimation/statement. Customers are requested to immediately notify the Bank of any discrepancy in the statement";
                                string strText = strText1.Replace(text, "");
                                strText = strText.Replace("Cr ", "");
                                if (strText.Contains(" CR ") && strText.Contains(" DR "))
                                {
                                    int c01 = strText.IndexOf(" CR ");
                                    int c02 = strText.IndexOf(" DR ");
                                    string secondText = "";
                                    if (c01 > c02) {
                                        strText = strText.Substring(0, c02);
                                        int firstBlankPosition = strText.Substring(c02, strText1.Length - 1).Trim().IndexOf(' ');
                                        string name = strText.Substring(c02, strText1.Length - 1).Trim().Substring(0, firstBlankPosition);
                                        strText = strText + " " + name;
                                            }
                                    else
                                    {
                                        strText = strText.Substring(0, c01);
                                        int firstBlankPosition = strText1.Trim().Substring(c01).Replace("CR","").Replace("DR","").Trim().IndexOf(' ');
                                        string name = strText1.Trim().Substring(c01).Replace("CR", "").Replace("DR", "").Trim().Substring(0, firstBlankPosition);
                                        strText = strText + " " + name;
                                        secondText = strText1.Trim().Substring(c01).Replace("CR", "").Replace("DR", "").Trim().Substring(firstBlankPosition);
                                    }
                                    {
                                        string RemainingParticularText = string.Empty;
                                        string transDate = string.Empty;
                                        if (strText.Split(' ').Count() > 0)
                                            transDate = strText.Split(' ')[0];
                                        if (transDate.Split('-').Length - 1 == 2 && transDate.Length == 10)
                                        {

                                        }
                                        else
                                        {
                                            RemainingParticularText = strText.Replace(text, "").Trim();
                                        }
                                        strText = strText.Trim();
                                        string valueDate = string.Empty;
                                        if (strText.Split(' ').Count() > 1)
                                            valueDate = strText.Split(' ')[3];
                                        string Balance = string.Empty;
                                        string location = string.Empty;
                                        string withdrawl = string.Empty;
                                        string deposit = string.Empty;
                                        string chequeNo = string.Empty;
                                        string particulars = string.Empty;
                                        if (valueDate.Split('-').Length - 1 == 2 && valueDate.Length == 10)
                                        {

                                        }
                                        else
                                        {
                                            valueDate = string.Empty;
                                        }

                                        string[] str = strText.Split(' ');

                                        transDate = str[3].ToString();
                                        valueDate = str[3].ToString();
                                        if (str[str.Length - 2].ToString() == "CR")
                                        {
                                            deposit = str.Last().ToString();
                                        }
                                        else
                                        {
                                            withdrawl = str.Last().ToString();
                                        }
                                        Balance = str[0].ToString();
                                        chequeNo = str[7].ToString();
                                        bool chkgate = false;
                                        int count = 0;
                                        foreach (string s in str.ToList())
                                        {
                                            count = count + 1;
                                            if (count == 8)
                                            {
                                                chkgate = true;
                                                continue;
                                            }
                                            if (s == "CR" || s == "DR")
                                            {
                                                chkgate = false;
                                                continue;
                                            }
                                            if (chkgate)
                                            {
                                                particulars = particulars + " " + s;
                                            }

                                        }
                                        location = "---";
                                        if (str.Length == 8)
                                        {
                                            HttpContext.Current.Session["transDate"] = transDate;
                                            HttpContext.Current.Session["Balance"] = Balance;
                                            HttpContext.Current.Session["valueDate"] = valueDate;
                                            HttpContext.Current.Session["chequeNo"] = chequeNo;
                                            HttpContext.Current.Session["sessionValue"] = "1";
                                        }
                                        else
                                        {
                                            if (HttpContext.Current.Session["sessionValue"] != null)
                                            {
                                                transDate = HttpContext.Current.Session["transDate"].ToString();
                                                Balance = HttpContext.Current.Session["Balance"].ToString();
                                                valueDate = HttpContext.Current.Session["valueDate"].ToString();
                                                chequeNo = HttpContext.Current.Session["chequeNo"].ToString();
                                                HttpContext.Current.Session["sessionValue"] = null;
                                            }
                                            List<string> lstValues = new List<string>();
                                            lstValues.Add(transDate);
                                            lstValues.Add(valueDate);
                                            lstValues.Add(particulars);
                                            lstValues.Add(chequeNo);
                                            lstValues.Add(location);
                                            lstValues.Add(withdrawl);
                                            lstValues.Add(deposit);
                                            lstValues.Add(Balance);
                                            dicValues.Add(RowCount, lstValues);
                                            RowCount++;
                                        }
                                    }
                                    {
                                        strText = secondText;
                                        string RemainingParticularText = string.Empty;
                                        string transDate = string.Empty;
                                        if (strText.Split(' ').Count() > 0)
                                            transDate = strText.Split(' ')[0];
                                        if (transDate.Split('-').Length - 1 == 2 && transDate.Length == 10)
                                        {

                                        }
                                        else
                                        {
                                            RemainingParticularText = strText.Replace(text, "").Trim();
                                        }
                                        strText = strText.Trim();
                                        string valueDate = string.Empty;
                                        if (strText.Split(' ').Count() > 1)
                                            valueDate = strText.Split(' ')[3];
                                        string Balance = string.Empty;
                                        string location = string.Empty;
                                        string withdrawl = string.Empty;
                                        string deposit = string.Empty;
                                        string chequeNo = string.Empty;
                                        string particulars = string.Empty;
                                        if (valueDate.Split('-').Length - 1 == 2 && valueDate.Length == 10)
                                        {

                                        }
                                        else
                                        {
                                            valueDate = string.Empty;
                                        }

                                        string[] str = strText.Split(' ');

                                        transDate = str[3].ToString();
                                        valueDate = str[3].ToString();
                                        if (str[str.Length - 2].ToString() == "CR")
                                        {
                                            deposit = str.Last().ToString();
                                        }
                                        else
                                        {
                                            withdrawl = str.Last().ToString();
                                        }
                                        Balance = str[0].ToString();
                                        chequeNo = str[7].ToString();
                                        bool chkgate = false;
                                        int count = 0;
                                        foreach (string s in str.ToList())
                                        {
                                            count = count + 1;
                                            if (count == 8)
                                            {
                                                chkgate = true;
                                                continue;
                                            }
                                            if (s == "CR" || s == "DR")
                                            {
                                                chkgate = false;
                                                continue;
                                            }
                                            if (chkgate)
                                            {
                                                particulars = particulars + " " + s;
                                            }

                                        }
                                        location = "---";
                                        if (str.Length == 8)
                                        {
                                            HttpContext.Current.Session["transDate"] = transDate;
                                            HttpContext.Current.Session["Balance"] = Balance;
                                            HttpContext.Current.Session["valueDate"] = valueDate;
                                            HttpContext.Current.Session["chequeNo"] = chequeNo;
                                            HttpContext.Current.Session["sessionValue"] = "1";
                                        }
                                        else
                                        {
                                            if (HttpContext.Current.Session["sessionValue"] != null)
                                            {
                                                transDate = HttpContext.Current.Session["transDate"].ToString();
                                                Balance = HttpContext.Current.Session["Balance"].ToString();
                                                valueDate = HttpContext.Current.Session["valueDate"].ToString();
                                                chequeNo = HttpContext.Current.Session["chequeNo"].ToString();
                                                HttpContext.Current.Session["sessionValue"] = null;
                                            }
                                            List<string> lstValues = new List<string>();
                                            lstValues.Add(transDate);
                                            lstValues.Add(valueDate);
                                            lstValues.Add(particulars);
                                            lstValues.Add(chequeNo);
                                            lstValues.Add(location);
                                            lstValues.Add(withdrawl);
                                            lstValues.Add(deposit);
                                            lstValues.Add(Balance);
                                            dicValues.Add(RowCount, lstValues);
                                            RowCount++;
                                        }
                                    }

                                }
                                else {
                                    string RemainingParticularText = string.Empty;
                                    string transDate = string.Empty;
                                    if (strText.Split(' ').Count() > 0)
                                        transDate = strText.Split(' ')[0];
                                    if (transDate.Split('-').Length - 1 == 2 && transDate.Length == 10)
                                    {

                                    }
                                    else
                                    {
                                        RemainingParticularText = strText.Replace(text, "").Trim();
                                    }
                                    strText = strText.Trim();
                                    string valueDate = string.Empty;
                                    if (strText.Split(' ').Count() > 1)
                                        valueDate = strText.Split(' ')[3];
                                    string Balance = string.Empty;
                                    string location = string.Empty;
                                    string withdrawl = string.Empty;
                                    string deposit = string.Empty;
                                    string chequeNo = string.Empty;
                                    string particulars = string.Empty;
                                    if (valueDate.Split('-').Length - 1 == 2 && valueDate.Length == 10)
                                    {

                                    }
                                    else
                                    {
                                        valueDate = string.Empty;
                                    }

                                    string[] str = strText.Split(' ');

                                    transDate = str[3].ToString();
                                    valueDate = str[3].ToString();
                                    if (str[str.Length - 2].ToString() == "CR")
                                    {
                                        deposit = str.Last().ToString();
                                    }
                                    else
                                    {
                                        withdrawl = str.Last().ToString();
                                    }
                                    Balance = str[0].ToString();
                                    chequeNo = str[7].ToString();
                                    bool chkgate = false;
                                    int count = 0;
                                    foreach (string s in str.ToList())
                                    {
                                        count = count + 1;
                                        if (count == 8)
                                        {
                                            chkgate = true;
                                            continue;
                                        }
                                        if (s == "CR" || s == "DR")
                                        {
                                            chkgate = false;
                                            continue;
                                        }
                                        if (chkgate)
                                        {
                                            particulars = particulars + " " + s;
                                        }

                                    }
                                    location = "---";
                                    if (str.Length == 8)
                                    {
                                        HttpContext.Current.Session["transDate"] = transDate;
                                        HttpContext.Current.Session["Balance"] = Balance;
                                        HttpContext.Current.Session["valueDate"] = valueDate;
                                        HttpContext.Current.Session["chequeNo"] = chequeNo;
                                        HttpContext.Current.Session["sessionValue"] = "1";
                                    }
                                    else
                                    {
                                        if (HttpContext.Current.Session["sessionValue"] != null)
                                        {
                                            transDate = HttpContext.Current.Session["transDate"].ToString();
                                            Balance = HttpContext.Current.Session["Balance"].ToString();
                                            valueDate = HttpContext.Current.Session["valueDate"].ToString();
                                            chequeNo = HttpContext.Current.Session["chequeNo"].ToString();
                                            HttpContext.Current.Session["sessionValue"] = null;
                                        }
                                        List<string> lstValues = new List<string>();
                                        lstValues.Add(transDate);
                                        lstValues.Add(valueDate);
                                        lstValues.Add(particulars);
                                        lstValues.Add(chequeNo);
                                        lstValues.Add(location);
                                        lstValues.Add(withdrawl);
                                        lstValues.Add(deposit);
                                        lstValues.Add(Balance);
                                        dicValues.Add(RowCount, lstValues);
                                        RowCount++;
                                    }
                                }
                            }
                            else
                            {
                                string valueDate = string.Empty;
                                string Balance = string.Empty;
                                string location = string.Empty;
                                string withdrawl = string.Empty;
                                string deposit = string.Empty;
                                string chequeNo = string.Empty;
                                string particulars = string.Empty;
                                string transDate = string.Empty;
                                multiC = multiC + 1;
                                if (multiC == 2)
                                {
                                    if (HttpContext.Current.Session["particulars"] != null)
                                    {
                                        particulars = HttpContext.Current.Session["particulars"].ToString();
                                    }
                                    particulars = strText1.Trim();
                                    HttpContext.Current.Session["particulars"] = particulars;
                                }
                                if (multiC == 4)
                                {
                                    if (strText1.Trim().Split(' ')[0] == "CR")
                                    {
                                        deposit = strText1.Trim().Split(' ').Last();
                                    }
                                    else
                                    {
                                        withdrawl = strText1.Trim().Split(' ').Last();
                                    }
                                    transDate = HttpContext.Current.Session["transDate"].ToString();
                                    Balance = HttpContext.Current.Session["Balance"].ToString();
                                    valueDate = HttpContext.Current.Session["valueDate"].ToString();
                                    chequeNo = HttpContext.Current.Session["chequeNo"].ToString();
                                    particulars = HttpContext.Current.Session["particulars"].ToString();
                                    HttpContext.Current.Session["sessionValue"] = null;
                                    List<string> lstValues = new List<string>();
                                    lstValues.Add(transDate);
                                    lstValues.Add(valueDate);
                                    lstValues.Add(particulars);
                                    lstValues.Add(chequeNo);
                                    lstValues.Add(location);
                                    lstValues.Add(withdrawl);
                                    lstValues.Add(deposit);
                                    lstValues.Add(Balance);
                                    dicValues.Add(RowCount, lstValues);
                                    RowCount++;
                                }
                            }
                        }
                        if (RowCount == 169)
                            RowCount = RowCount;
                    }
                    catch (Exception ex)
                    {
                        //do nothing
                    }
                }

                foreach (KeyValuePair<int, List<string>> keyVal in dicValues.ToList())
                {
                    List<string> lstVal = keyVal.Value;
                    lstVal[0] = lstVal[0].Replace(",", "");
                    lstVal[1] = lstVal[1].Replace(",", "");
                    lstVal[2] = lstVal[2].Replace(",", "");
                    lstVal[3] = lstVal[3].Replace(",", "");
                    lstVal[4] = lstVal[4].Replace(",", "");
                    lstVal[5] = lstVal[5].Replace(",", "");
                    lstVal[6] = lstVal[6].Replace(",", "");
                    lstVal[7] = lstVal[7].Replace(",", "");
                    if (lstVal[3] != string.Empty)
                    {
                        lstVal[2] = lstVal[2].Replace(lstVal[3], "");
                    }
                    dicValues[keyVal.Key] = lstVal;
                }

                DataTable dt = new DataTable();
                DataColumn dcTransDate = new DataColumn("TransDate");
                DataColumn dcValueDate = new DataColumn("ValueDate");
                DataColumn dcTransRemarks = new DataColumn("Particulars");
                DataColumn dcLocation = new DataColumn("Location");
                DataColumn dcChequeNumber = new DataColumn("ChequeNumber");
                DataColumn dcWithdrawl = new DataColumn("Withdrawl");
                DataColumn dcDepositAmount = new DataColumn("Deposit");
                DataColumn dcBalance = new DataColumn("Balance (INR)");

                dt.Columns.Add(dcTransDate);
                dt.Columns.Add(dcValueDate);
                dt.Columns.Add(dcTransRemarks);
                dt.Columns.Add(dcLocation);
                dt.Columns.Add(dcChequeNumber);
                dt.Columns.Add(dcWithdrawl);
                dt.Columns.Add(dcDepositAmount);
                dt.Columns.Add(dcBalance);


                //Value Date,Trans Date,Cheque Number,Balance,Deposit Amount,Withdrawl Amount,Trans Remarks

                foreach (KeyValuePair<int, List<string>> keyVal in dicValues)
                {
                    DataRow dr = dt.NewRow();
                    List<string> lstVal = keyVal.Value;
                    dr["TransDate"] = lstVal[0];
                    dr["ValueDate"] = lstVal[1];
                    dr["Particulars"] = lstVal[2];
                    dr["Location"] = lstVal[4];
                    dr["ChequeNumber"] = lstVal[3];
                    dr["Withdrawl"] = lstVal[5];
                    dr["Deposit"] = lstVal[6];
                    dr["Balance (INR)"] = lstVal[7];
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(ds_Basic);
                ds.Tables.Add(dt);
                #endregion
            }
            return ds;
        }

        private DataTable GetICICIBasic_CURRENT_SAVING1(string TableValues)
        {
            DataTable dt_1 = new DataTable();
            dt_1.Columns.Add("NAME");
            dt_1.Columns.Add("ADDRESS1");
            dt_1.Columns.Add("ADDRESS2");
            dt_1.Columns.Add("ADDRESS3");
            dt_1.Columns.Add("CITY");
            dt_1.Columns.Add("STATE");
            dt_1.Columns.Add("ACCOUNT_BRANCH");
            dt_1.Columns.Add("BRANCH_ADDRESS");
            dt_1.Columns.Add("BRANCH_CITY");
            dt_1.Columns.Add("BRANCH_STATE");
            dt_1.Columns.Add("CUST_ID");
            dt_1.Columns.Add("ACCOUNT_NO");
            dt_1.Columns.Add("MICR_CODE");
            dt_1.Columns.Add("IFSC_CODE");
            dt_1.Columns.Add("ACCOUNT_TYPE");
            dt_1.Columns.Add("NOMINATION");
            try
            {
                int Row = 0;
                DataRow row = dt_1.NewRow();
                List<string> lst = TableValues.Split('\n').ToList();
                foreach (string strText1 in lst)
                {
                    if (Row == 2)
                    {
                        row["NAME"] = strText1.Split('-')[1]; ;
                    }
                    if (Row == 11)
                    {
                        row["ACCOUNT_NO"] = strText1.Split('-')[2];
                    }
                    Row = Row + 1;
                }
                dt_1.Rows.Add(row);
                return dt_1;
            }
            catch
            {
                dt_1 = GetICICIBasic_CURRENT_SAVING_nextFormat(TableValues);
            }
            return dt_1;
        }

        public DataTable GetICICIBasic(string TableValues)
        {
            DataTable dt_1 = new DataTable();
            dt_1.Columns.Add("NAME");
            dt_1.Columns.Add("ADDRESS1");
            dt_1.Columns.Add("ADDRESS2");
            dt_1.Columns.Add("ADDRESS3");
            dt_1.Columns.Add("CITY");
            dt_1.Columns.Add("STATE");
            dt_1.Columns.Add("ACCOUNT_BRANCH");
            dt_1.Columns.Add("BRANCH_ADDRESS");
            dt_1.Columns.Add("BRANCH_CITY");
            dt_1.Columns.Add("BRANCH_STATE");
            dt_1.Columns.Add("CUST_ID");
            dt_1.Columns.Add("ACCOUNT_NO");
            dt_1.Columns.Add("MICR_CODE");
            dt_1.Columns.Add("IFSC_CODE");
            dt_1.Columns.Add("ACCOUNT_TYPE");
            dt_1.Columns.Add("NOMINATION");
            int Row = 0;
            DataRow row = dt_1.NewRow();
            List<string> lst = TableValues.Split('\n').ToList();
            foreach (string strText1 in lst)
            {
                if (Row == 4)
                {
                    row["ADDRESS1"] = strText1;
                }
                if (Row == 5)
                {
                    row["ADDRESS2"] = strText1;
                }
                if (Row == 6)
                {
                    row["ADDRESS2"] = strText1;
                }
                //if (Row == 7)
                //{
                //    row["ADDRESS3"] = strText1;
                //}
                if (Row == 7)
                {
                    row["STATE"] = strText1;
                }
                if (Row == 3)
                {
                    row["NAME"] = strText1;
                }
                if (Row == 8)
                {
                    row["BRANCH_ADDRESS"] = strText1.Split(':')[1];
                }
                //if (Row == 15)
                //{
                //    row["CUST_ID"] = strText1.Split(':')[1];
                //}
                //if (Row == 20)
                //{
                //    row["ACCOUNT_NO"] = strText1.Split(':')[1];
                //}
                if (Row == 12)
                {
                    if (strText1.Contains("Not Registered"))
                    {
                        row["NOMINATION"] = "Not Registered";
                    }
                    string strText1_1 = strText1.Replace("Cr", "").Replace("Not Registered", "").Trim();
                    if (strText1_1.Contains("Advance"))
                    {

                        row["ACCOUNT_TYPE"] = "OD";
                        strText1_1 = strText1.Replace("Cr", "").Replace("Not Registered", "").Replace("Advance", "").Trim();
                        string[] ss = strText1_1.Split(' ');
                        row["ACCOUNT_NO"] = strText1_1.Split(' ')[0];
                        row["MICR_CODE"] = strText1_1.Split(' ')[3];
                        row["IFSC_CODE"] = strText1_1.Split(' ')[4];
                    }
                }
                if (Row == 14)
                {
                    row["CUST_ID"] = strText1.Split(':')[1];
                }
                //if (Row == 15)
                //{
                //    row["CUST_ID"] = strText1.Split(':')[1];
                //}
                Row = Row + 1;
            }
            dt_1.Rows.Add(row);
            return dt_1;
        }
        public DataTable GetICICIBasic_CURRENT_SAVING(string TableValues)
        {
            DataTable dt_1 = new DataTable();
            dt_1.Columns.Add("NAME");
            dt_1.Columns.Add("ADDRESS1");
            dt_1.Columns.Add("ADDRESS2");
            dt_1.Columns.Add("ADDRESS3");
            dt_1.Columns.Add("CITY");
            dt_1.Columns.Add("STATE");
            dt_1.Columns.Add("ACCOUNT_BRANCH");
            dt_1.Columns.Add("BRANCH_ADDRESS");
            dt_1.Columns.Add("BRANCH_CITY");
            dt_1.Columns.Add("BRANCH_STATE");
            dt_1.Columns.Add("CUST_ID");
            dt_1.Columns.Add("ACCOUNT_NO");
            dt_1.Columns.Add("MICR_CODE");
            dt_1.Columns.Add("IFSC_CODE");
            dt_1.Columns.Add("ACCOUNT_TYPE");
            dt_1.Columns.Add("NOMINATION");
            try
            {
                int Row = 0;
                DataRow row = dt_1.NewRow();
                List<string> lst = TableValues.Split('\n').ToList();
                foreach (string strText1 in lst)
                {
                    if (Row == 7)
                    {
                        row["ADDRESS1"] = strText1;
                    }
                    if (Row == 8 || Row == 9)
                    {
                        row["ADDRESS2"] = row["ADDRESS2"] + " " + strText1;
                    }
                    //if (Row == 9)
                    //{
                    //    row["ADDRESS2"] = strText1;
                    //}
                    if (Row == 10)
                    {
                        row["ADDRESS3"] = strText1;
                    }
                    if (Row == 8)
                    {
                        row["CITY"] = strText1;
                    }
                    if (Row == 9)
                    {
                        row["STATE"] = strText1;
                    }
                    if (Row == 12)
                    {
                        row["NAME"] = strText1;
                    }
                    if (Row == 13)
                    {

                        row["BRANCH_ADDRESS"] = strText1.Split(':')[1];


                    }
                    //if (Row == 15)
                    //{
                    //    row["CUST_ID"] = strText1.Split(':')[1];
                    //}
                    //if (Row == 20)
                    //{
                    //    row["ACCOUNT_NO"] = strText1.Split(':')[1];
                    //}
                    if (Row == 18)
                    {
                        if (strText1.Contains("Not Registered"))
                        {
                            row["NOMINATION"] = "Not Registered";
                        }
                        string strText1_1 = strText1.Replace("Cr", "").Replace("Not Registered", "").Trim();
                        if (strText1_1.Contains("Current"))
                        {

                            row["ACCOUNT_TYPE"] = "Current";
                            strText1_1 = strText1.Replace("Cr", "").Replace("Not Registered", "").Replace("Current", "").Trim();
                            string[] ss = strText1_1.Split(' ');
                            row["ACCOUNT_NO"] = strText1_1.Split(' ')[0];
                            row["MICR_CODE"] = strText1_1.Split(' ')[4];
                            row["IFSC_CODE"] = strText1_1.Split(' ')[5];
                        }
                    }
                    //if (Row == 14)
                    //{
                    //    row["CUST_ID"] = strText1;
                    //}
                    if (Row == 15)
                    {
                        string pp = strText1.Split(':')[1];
                        row["CUST_ID"] = pp.Trim().Split(' ')[0];
                    }
                    Row = Row + 1;
                }
                dt_1.Rows.Add(row);
                return dt_1;
            }
            catch
            {
                dt_1 = GetICICIBasic_CURRENT_SAVING_nextFormat(TableValues);
            }
            return dt_1;
        }
        public DataTable GetICICIBasic_CURRENT_SAVING_nextFormat(string TableValues)
        {
            DataTable dt_1 = new DataTable();
            dt_1.Columns.Add("NAME");
            dt_1.Columns.Add("ADDRESS1");
            dt_1.Columns.Add("ADDRESS2");
            dt_1.Columns.Add("ADDRESS3");
            dt_1.Columns.Add("CITY");
            dt_1.Columns.Add("STATE");
            dt_1.Columns.Add("ACCOUNT_BRANCH");
            dt_1.Columns.Add("BRANCH_ADDRESS");
            dt_1.Columns.Add("BRANCH_CITY");
            dt_1.Columns.Add("BRANCH_STATE");
            dt_1.Columns.Add("CUST_ID");
            dt_1.Columns.Add("ACCOUNT_NO");
            dt_1.Columns.Add("MICR_CODE");
            dt_1.Columns.Add("IFSC_CODE");
            dt_1.Columns.Add("ACCOUNT_TYPE");
            dt_1.Columns.Add("NOMINATION");
            int Row = 0;
            DataRow row = dt_1.NewRow();
            List<string> lst = TableValues.Split('\n').ToList();
            foreach (string strText1 in lst)
            {
                if (Row == 7)
                {
                    row["ADDRESS1"] = strText1;
                }
                if (Row == 8 || Row == 9)
                {
                    row["ADDRESS2"] = row["ADDRESS2"] + " " + strText1;
                }
                //if (Row == 9)
                //{
                //    row["ADDRESS2"] = strText1;
                //}
                if (Row == 10)
                {
                    row["ADDRESS3"] = strText1;
                }
                if (Row == 11)
                {
                    row["STATE"] = strText1;
                }
                if (Row == 12)
                {
                    row["NAME"] = strText1;
                }
                if (Row == 13)
                {
                    try
                    {
                        row["BRANCH_ADDRESS"] = strText1.Split(':')[1];
                    }
                    catch { }
                }
                //if (Row == 15)
                //{
                //    row["CUST_ID"] = strText1.Split(':')[1];
                //}
                //if (Row == 20)
                //{
                //    row["ACCOUNT_NO"] = strText1.Split(':')[1];
                //}
                if (Row == 18)
                {
                    try
                    {
                        if (strText1.Contains("Not Registered"))
                        {
                            row["NOMINATION"] = "Not Registered";
                        }
                        string strText1_1 = strText1.Replace("Cr", "").Replace("Not Registered", "").Trim();
                        if (strText1_1.Contains("Current"))
                        {

                            row["ACCOUNT_TYPE"] = "Current";
                            strText1_1 = strText1.Replace("Cr", "").Replace("Not Registered", "").Replace("Current", "").Trim();
                            string[] ss = strText1_1.Split(' ');
                            row["ACCOUNT_NO"] = strText1_1.Split(' ')[0];
                            row["MICR_CODE"] = strText1_1.Split(' ')[4];
                            row["IFSC_CODE"] = strText1_1.Split(' ')[5];
                        }
                    }
                    catch { }
                }
                if (Row == 14)
                {
                    row["CUST_ID"] = strText1;
                }
                //if (Row == 15)
                //{
                //    row["CUST_ID"] = strText1.Split(':')[1];
                //}
                Row = Row + 1;
            }
            dt_1.Rows.Add(row);
            return dt_1;
        }
    }
}