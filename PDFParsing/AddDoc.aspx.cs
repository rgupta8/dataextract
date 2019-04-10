using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFParsing
{
    public partial class AddDoc : System.Web.UI.Page
    {
        private PDFParseDataContext dbcontext = new PDFParseDataContext();
        private string PARSER_NAME = "";
        private string ADDRESS1 = "";
        private string ADDRESS2 = "";
        private string ADDRESS3 = "";
        private string CITY = "";
        private string STATE = "";
        private string ACCOUNT_BRANCH = "";
        private string BRANCH_ADDRESS = "";
        private string BRANCH_CITY = "";
        private string BRANCH_STATE = "";
        private string BRANCH_PHONENO = "";
        private string CURRENCY = "";
        private string EMAIL = "";
        private string CUSTID = "";
        private string ACCOUNTNO = "";
        private string ACCOUNT_OPEN_DATE = "";
        private string ACCOUNT_STATUS = "";
        private string BRANCH_CODE = "";
        private string PRODUCT_CODE = "";
        private string NOMINATION = "";
        private int COUNT_FILES;
        private int DOCUMENT_ID;
        private int PARSER_ID;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.User.Identity.IsAuthenticated)
                this.Response.Redirect("/Default.aspx");
            if (this.IsPostBack)
                return;
            this.Session["dt_statement"] = (object)null;
            this.Session["Multi"] = (object)null;
            this.BindStatement();
            this.CheckStstement();
        }

        private void CheckStstement()
        {
            int count = rptFiles.Items.Count;
            if (count == 1)
            {
                ((Label)this.rptFiles.Items[0].FindControl("lblStatement")).Text = "Statement 1";
                this.rptFiles.Items[0].FindControl("lnkDelete").Visible = false;
            }
            else
            {
                int num = 0;
                foreach (RepeaterItem repeaterItem in this.rptFiles.Items)
                {
                    ((Label)repeaterItem.FindControl("lblStatement")).Text = "Statement " + (num + 1).ToString();
                    if (count != num + 1)
                        repeaterItem.FindControl("lnkAdd").Visible = false;
                    ++num;
                }
            }
        }

        private void BindStatement()
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2;
            if (this.Session["dt_statement"] == null)
            {
                dataTable2 = new DataTable();
                dataTable2.Columns.Add("Id");
                dataTable2.Columns.Add("Statement");
                DataRow row = dataTable2.NewRow();
                row[0] = (object)"1";
                row[1] = (object)"Statement 1";
                dataTable2.Rows.Add(row);
                this.Session["dt_statement"] = (object)dataTable2;
            }
            else
                dataTable2 = (DataTable)this.Session["dt_statement"];
            this.rptFiles.DataSource = (object)dataTable2;
            this.rptFiles.DataBind();
            this.CheckStstement();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string FileName = "";
            if (CheckIsAllPDF())
            {
                METADATA_INFO mataInfo;
                USER user = this.dbcontext.USERs.FirstOrDefault<USER>((Expression<Func<USER, bool>>)(x => x.USER_ID == Convert.ToInt32(this.User.Identity.Name)));
                try
                {
                    foreach (RepeaterItem repeaterItem in this.rptFiles.Items)
                    {
                        SaveUserBankCategory(User.Identity.Name, ddlBanks.SelectedItem.Value);
                        FileUpload control1 = (FileUpload)repeaterItem.FindControl("id_input_file_1");
                        TextBox control2 = (TextBox)repeaterItem.FindControl("txtPassword");
                        if (control1.HasFile)
                        {
                            string text = control2.Text;
                            string str = this.Server.MapPath("~") + "/DOCUMENTS/" + user.EMAIL_ADDRESS.Replace("@", "-").Replace(".", "-") + "/" + control1.PostedFile.FileName;
                            FileName = str;
                            control1.SaveAs(str);
                            DataSet dataSet = new DataSet();
                            bool IsPasswordProtected = false;
                            if (text != "")
                                IsPasswordProtected = true;
                            if (this.ddlBanks.SelectedItem.Value == "HDFC")
                            {
                                DataSet excel = this.Export_HDFC_PDFToExcel(str, IsPasswordProtected, text);
                                DataTable table = excel.Tables[1];
                                this.Session["dt"] = (object)table;
                                if (table.Rows.Count >= 2)
                                {
                                    int DocumentID = 0;
                                    DocumentID = this.SAVE_HDFC_RECORD(control1.PostedFile.FileName, excel);
                                    mataInfo = new METADATA_INFO();
                                    mataInfo.GET_DOCUMENT_METAINFO(str, DocumentID);
                                }
                                else
                                {
                                    this.lblMessage.Text = "Wrong PDF Format.";
                                    this.lblMessage.ForeColor = Color.Red;
                                }
                                this.COUNT_FILES = this.COUNT_FILES + 1;
                            }
                            if (this.ddlBanks.SelectedItem.Value == "SBI")
                            {
                                DataSet excel = this.Export_SBI_PDFToExcel(str, IsPasswordProtected, text);
                                DataTable table = excel.Tables[1];
                                this.Session["dt"] = (object)table;
                                if (table.Rows.Count >= 2)
                                {
                                    int DocumentID = 0;
                                    this.SAVE_SBI_RECORD(control1.PostedFile.FileName, excel);
                                    mataInfo = new METADATA_INFO();
                                    mataInfo.GET_DOCUMENT_METAINFO(str, DocumentID);
                                }
                                else
                                {
                                    this.lblMessage.Text = "Wrong PDF Format.";
                                    this.lblMessage.ForeColor = Color.Red;
                                }
                                this.COUNT_FILES = this.COUNT_FILES + 1;
                            }
                            if (this.ddlBanks.SelectedItem.Value == "ICICI")
                            {
                                DataSet excel = this.Export_ICICI_PDFToExcel(str, IsPasswordProtected, text);
                                DataTable table = excel.Tables[1];
                                this.Session["dt"] = (object)table;
                                if (table.Rows.Count >= 2)
                                {
                                    int DocumentID = 0;
                                    DocumentID = this.SAVE_ICICI_RECORD(control1.PostedFile.FileName, excel);
                                    mataInfo = new METADATA_INFO();
                                    mataInfo.GET_DOCUMENT_METAINFO(str, DocumentID);
                                }
                                else
                                {
                                    this.lblMessage.Text = "Wrong PDF Format.";
                                    this.lblMessage.ForeColor = Color.Red;
                                }
                                this.COUNT_FILES = this.COUNT_FILES + 1;
                            }
                            if (this.ddlBanks.SelectedItem.Value == "KMB")
                            {
                                DataSet excel = this.Export_Kotak_PDFToExcel(str, IsPasswordProtected, text);
                                DataTable table = excel.Tables[1];
                                this.Session["dt"] = (object)table;
                                if (table.Rows.Count >= 2)
                                {
                                    int DocumentID = 0;
                                    DocumentID = this.SAVE_KOTAK_RECORD(control1.PostedFile.FileName, excel);
                                    mataInfo = new METADATA_INFO();
                                    mataInfo.GET_DOCUMENT_METAINFO(str, DocumentID);

                                }
                                else
                                {
                                    this.lblMessage.Text = "Wrong PDF Format.";
                                    this.lblMessage.ForeColor = Color.Red;
                                }
                                this.COUNT_FILES = this.COUNT_FILES + 1;
                            }
                        }
                        else
                        {
                            this.lblMessage.Text = "No Statement is uploaded. Please upload a statement";
                            this.lblMessage.ForeColor = Color.Red;
                        }
                    }
                }
                catch (Exception Ex)
                {
                    //SendErrorEmailToAdmin(Ex.Message, FileName);
                }
                if (this.COUNT_FILES == 1)
                {
                    if (((DataTable)this.Session["dt"]).Rows.Count >= 2)
                    {
                        this.Session["DOCUMENT_ID"] = (object)this.DOCUMENT_ID;
                        this.Session["PARSER_ID"] = (object)this.PARSER_ID;
                        this.Response.Redirect("UpdateCategory.aspx");
                    }
                    else
                    {
                        this.lblMessage.Text = "Wrong PDF Format.";
                        this.lblMessage.ForeColor = Color.Red;
                    }
                }
                else if (this.COUNT_FILES == 0)
                {
                    this.lblMessage.Text = "There is Some problem in PDF format. An email is sent to Administrator regarding the issue.";
                    this.lblMessage.ForeColor = Color.Red;
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("<script type='text/javascript'>");
                    stringBuilder.Append("$('#myModal').modal('show');");
                    stringBuilder.Append("</script>");
                    ((Label)this.Master.FindControl("sp1")).Text = stringBuilder.ToString();
                    this.rptDOCUMENTS.DataSource = (object)(DataTable)this.Session["Multi"];
                    this.rptDOCUMENTS.DataBind();
                }
            }
            else if (IsFileUploaded())
            {
                this.lblMessage.Text = "Please upload Files First.";
                this.lblMessage.ForeColor = Color.Red;
            }
            else
            {
                this.lblMessage.Text = "You are only permitted to upload PDF file.";
                this.lblMessage.ForeColor = Color.Red;
            }
        }

        public void SaveUserBankCategory(string UserId, string DocumentType)
        {
            USER_CATEGORY category = dbcontext.USER_CATEGORies.FirstOrDefault(x => x.DOCUMENT_TYPE.ToLower() == DocumentType);
            if (category == null)
            {
                IEnumerable<DOCUMENT_CATEGORY> Category= dbcontext.DOCUMENT_CATEGORies.Where(x => x.DOCUMENT_TYPE.ToLower() == DocumentType);
                foreach (DOCUMENT_CATEGORY cat in Category)
                {
                    USER_CATEGORY C = new USER_CATEGORY();
                    C.CATEGORY_TYPE = cat.CATEGORY_TYPE;
                    C.DOCUMENT_TYPE = cat.DOCUMENT_TYPE;
                    C.IDENTIFIER_1 = cat.IDENTIFIER_1;
                    C.IDENTIFIER_2 = cat.IDENTIFIER_2;
                    C.IDENTIFIER_3 = cat.IDENTIFIER_3;
                    C.IS_CREDIT = cat.IS_CREDIT;
                    C.IS_DEDIT = cat.IS_DEBIT;
                    C.NAME = cat.NAME;
                    C.USER_ID = Convert.ToInt32(UserId);
                    dbcontext.USER_CATEGORies.InsertOnSubmit(C);
                }
            }
        }

        private bool IsFileUploaded()
        {
            bool isValidFile = true;
            foreach (RepeaterItem repeaterItem in this.rptFiles.Items)
            {
                FileUpload control1 = (FileUpload)repeaterItem.FindControl("id_input_file_1");
                string[] validFileTypes = { "pdf" };
                string ext = System.IO.Path.GetExtension(control1.PostedFile.FileName);
                if (string.IsNullOrEmpty(control1.PostedFile.FileName))
                {
                    isValidFile = false;
                    break;
                }
            }
            return isValidFile;
        }

        private bool CheckIsAllPDF()
        {
            bool isValidFile = false;
            foreach (RepeaterItem repeaterItem in this.rptFiles.Items)
            {
                FileUpload control1 = (FileUpload)repeaterItem.FindControl("id_input_file_1");
                string[] validFileTypes = { "pdf" };
                string ext = System.IO.Path.GetExtension(control1.PostedFile.FileName).ToLower();
                
                for (int i = 0; i < validFileTypes.Length; i++)
                {
                    if (ext != "." + validFileTypes[i])
                    {
                        isValidFile = false;
                        break;
                    }
                    else
                    {
                        isValidFile = true;
                    }
                }
            }
            return isValidFile;
        }

        private void SendErrorEmailToAdmin(string Name, string FileName)
        {
            using (MailMessage mm = new MailMessage("piush.bca@gmail.com", "mudracube@gmail.com"))
            {
                mm.Bcc.Add("piush190388@gmail.com");
                mm.Subject = "Error on PDF Parsing";
                mm.Body = "Hi admin<br/>An error comes ehile parsing PDF. <br/><br/>Error: " + Name;
                if (FileName!="")
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(FileName);
                    mm.Attachments.Add(attachment);
                }
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("deaninfotest@gmail.com", "deanpassword@123");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }

        }

        private DataSet Export_Kotak_PDFToExcel(string FilePath, bool IsPasswordProtected, string Password)
        {
            return new Kotak().Export_HDFC_PDFToExcel(FilePath, IsPasswordProtected, Password);
        }

        private DataSet Export_ICICI_PDFToExcel(string FilePath, bool IsPasswordProtected, string Password)
        {
            return new ICICI().ProcessParsingPDF(FilePath, IsPasswordProtected, Password);
        }

        public DataSet Export_SBI_PDFToExcel(string FilePath, bool IsPasswordProtected, string Password)
        {
            DataSet dataSet = new SBI().ProcessPdfAndWriteCSVSBI(FilePath, IsPasswordProtected, Password);
            int count = dataSet.Tables[0].Rows.Count;
            return dataSet;
        }

        private int SAVE_KOTAK_RECORD(string FileName, DataSet ds)
        {
            DataTable dataTable;
            if (this.Session["Multi"] == null)
            {
                dataTable = new DataTable();
                dataTable.Columns.Add("FileName");
                dataTable.Columns.Add("DOCUMENT_ID");
                dataTable.Columns.Add("PARSER_ID");
            }
            else
                dataTable = (DataTable)this.Session["Multi"];
            DOCUMENT entity1 = new DOCUMENT();
            entity1.USER_ID = new int?(Convert.ToInt32(this.User.Identity.Name));
            entity1.DOCUMENT_NAME = FileName;
            entity1.DOCUMENT_TYPE = this.ddlBanks.SelectedItem.Value;
            entity1.IS_PASSWORD_PROTECTED = new bool?(false);
            entity1.ACCOUNT_TYPE = "";
            entity1.UPLOAD_DATE = new DateTime?(DateTime.Now);
            this.dbcontext.DOCUMENTs.InsertOnSubmit(entity1);
            this.dbcontext.SubmitChanges();
            this.DOCUMENT_ID = entity1.DOCUMENT_ID;
            PARSER_DOCUMENT entity2 = new PARSER_DOCUMENT();
            entity2.DOCUMENT_ID = new int?(this.DOCUMENT_ID);
            entity2.ACCOUNT_BRANCH = ds.Tables[0].Rows[0]["Branch"].ToString();
            entity2.ACCOUNT_OPEN_DATE = "";
            entity2.ACCOUNT_STATUS = "";
            entity2.ACCOUNTNO = ds.Tables[0].Rows[0]["AccountNo"].ToString();
            entity2.ADDRESS1 = ds.Tables[0].Rows[0]["Address"].ToString();
            entity2.ADDRESS2 = ds.Tables[0].Rows[0]["Address"].ToString();
            entity2.ADDRESS3 = ds.Tables[0].Rows[0]["Address"].ToString();
            entity2.BRANCH_ADDRESS = ds.Tables[0].Rows[0]["Branch"].ToString();
            entity2.BRANCH_CITY = ds.Tables[0].Rows[0]["Branch"].ToString();
            entity2.BRANCH_CODE = "";
            entity2.BRANCH_PHONENO = "";
            entity2.BRANCH_STATE = ds.Tables[0].Rows[0]["Branch"].ToString();
            entity2.CITY = "";
            entity2.CURRENCY = ds.Tables[0].Rows[0]["Currency"].ToString();
            entity2.CUSTID = ds.Tables[0].Rows[0]["CustRelnNo"].ToString();
            entity2.EMAIL = "";
            entity2.NOMINATION = ds.Tables[0].Rows[0]["NomineeRegistered"].ToString();
            entity2.PARSER_NAME = "";
            entity2.PRODUCT_CODE = "";
            entity2.STATE = "";
            this.dbcontext.PARSER_DOCUMENTs.InsertOnSubmit(entity2);
            this.dbcontext.SubmitChanges();
            this.PARSER_ID = entity2.PARSER_ID;
            DataTable table = ds.Tables[1];
            for (int index = table.Rows.Count - 1; index >= 0; --index)
            {
                if (table.Rows[index][7].ToString() == "CR")
                {
                    PARSER_SHEET entity3 = new PARSER_SHEET();
                    entity3.PARSER_ID = new int?(this.PARSER_ID);
                    DateTime exact1 = DateTime.ParseExact(DateTime.ParseExact(table.Rows[index][1].ToString(), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                    entity3.DATE = new DateTime?(exact1);
                    entity3.NARRATION = table.Rows[index][2].ToString();
                    entity3.CHQ_NO = table.Rows[index][3].ToString();
                    DateTime exact2 = DateTime.ParseExact(DateTime.ParseExact(table.Rows[index][1].ToString(), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                    entity3.VALUE_DATE = new DateTime?(exact2);
                    if (!string.IsNullOrEmpty(table.Rows[index][4].ToString()))
                    {
                        if (table.Rows[index][5].ToString() == "DR")
                            entity3.WITHDRAWAL_AMOUNT = new Decimal?(Convert.ToDecimal(table.Rows[index][4].ToString()));
                        else
                            entity3.DEPOSIT_AMOUNT = new Decimal?(Convert.ToDecimal(table.Rows[index][4].ToString()));
                    }
                    if (!string.IsNullOrEmpty(table.Rows[index][6].ToString()))
                        entity3.CLOSING_BALANCE = new Decimal?(Convert.ToDecimal(table.Rows[index][6].ToString()));

                    entity3.CATEGORY_ID = GetCategoryId(table.Rows[index][2].ToString(), User.Identity.Name, false, "kmb");
                    this.dbcontext.PARSER_SHEETs.InsertOnSubmit(entity3);
                    this.dbcontext.SubmitChanges();
                }
            }
            DataRow row = dataTable.NewRow();
            row[0] = (object)FileName;
            row[1] = (object)this.DOCUMENT_ID;
            row[2] = (object)this.PARSER_ID;
            dataTable.Rows.Add(row);
            this.Session["Multi"] = (object)dataTable;
            return DOCUMENT_ID;
        }

        private int SAVE_SBI_RECORD(string FileName, DataSet ds)
        {
            DataTable dataTable;
            if (this.Session["Multi"] == null)
            {
                dataTable = new DataTable();
                dataTable.Columns.Add("FileName");
                dataTable.Columns.Add("DOCUMENT_ID");
                dataTable.Columns.Add("PARSER_ID");
            }
            else
                dataTable = (DataTable)this.Session["Multi"];
            DOCUMENT entity1 = new DOCUMENT();
            entity1.USER_ID = new int?(Convert.ToInt32(this.User.Identity.Name));
            entity1.DOCUMENT_NAME = FileName;
            entity1.DOCUMENT_TYPE = this.ddlBanks.SelectedItem.Value;
            entity1.IS_PASSWORD_PROTECTED = new bool?(false);
            entity1.ACCOUNT_TYPE = "";
            entity1.UPLOAD_DATE = new DateTime?(DateTime.Now);
            this.dbcontext.DOCUMENTs.InsertOnSubmit(entity1);
            this.dbcontext.SubmitChanges();
            this.DOCUMENT_ID = entity1.DOCUMENT_ID;
            PARSER_DOCUMENT entity2 = new PARSER_DOCUMENT();
            entity2.DOCUMENT_ID = new int?(this.DOCUMENT_ID);
            entity2.ACCOUNT_BRANCH = ds.Tables[0].Rows[0]["Branch"].ToString();
            entity2.ACCOUNT_OPEN_DATE = "";
            entity2.ACCOUNT_STATUS = "";
            entity2.ACCOUNTNO = ds.Tables[0].Rows[0]["AccountNo"].ToString();
            entity2.ADDRESS1 = ds.Tables[0].Rows[0]["Address"].ToString();
            entity2.ADDRESS2 = ds.Tables[0].Rows[0]["Address"].ToString();
            entity2.ADDRESS3 = ds.Tables[0].Rows[0]["Address"].ToString();
            entity2.BRANCH_ADDRESS = ds.Tables[0].Rows[0]["Branch"].ToString();
            entity2.BRANCH_CITY = ds.Tables[0].Rows[0]["Branch"].ToString();
            entity2.BRANCH_CODE = "";
            entity2.BRANCH_PHONENO = "";
            entity2.BRANCH_STATE = "";
            entity2.CITY = "";
            entity2.CURRENCY = "";
            entity2.CUSTID = "";
            entity2.EMAIL = "";
            entity2.NOMINATION = "";
            entity2.PARSER_NAME = ds.Tables[0].Rows[0]["AccountName"].ToString();
            entity2.PRODUCT_CODE = "";
            entity2.STATE = "";
            this.dbcontext.PARSER_DOCUMENTs.InsertOnSubmit(entity2);
            this.dbcontext.SubmitChanges();
            decimal TBal = Convert.ToDecimal(ds.Tables[0].Rows[0]["Balance"].ToString());
            string CL1 = "";
            string CL2 = "";
            string CL3 = "";
            string CL4 = "";
            string CL5 = "";
            this.PARSER_ID = entity2.PARSER_ID;
            foreach (DataRow row in (InternalDataCollectionBase)ds.Tables[1].Rows)
            {

                PARSER_SHEET entity3;

                if (!string.IsNullOrEmpty(row[0].ToString()))
                {
                    entity3 = new PARSER_SHEET();;
                    entity3.PARSER_ID = new int?(this.PARSER_ID);
                    string[] strArray1 = row[0].ToString().Trim().Split(' ');
                    string[] str1 = row[0].ToString().Trim().Split('/');
                    if (str1.Length != 3)
                    {
                        str1 = row[0].ToString().Trim().Split(' ');
                    }
                    //DateTime exact1 = DateTime.ParseExact(DateTime.ParseExact(str11 + "/" + GetMonth(str1[1]) + "/" + str1[2], "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                    string str11 = str1[0];
                    if (str1[0].ToString().Length == 1)
                        str11 = "0" + str1[0];
                    DateTime exact1 = new DateTime();
                    try
                    {
                        exact1 = DateTime.ParseExact(DateTime.ParseExact(str11 + "/" + GetMonth(str1[1]) + "/" + str1[2], "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                    }
                    catch { exact1 = DateTime.ParseExact(DateTime.ParseExact(str11 + "/" + str1[1] + "/" + str1[2], "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture); }
                    entity3.DATE = new DateTime?(exact1);
                    string[] ddd = row[2].ToString().Replace("NEFT INB:", "^").Split('^');
                    string Narr = "";
                    if (ddd.Length == 2)
                    {
                        entity3.NARRATION = ddd[0].ToString();
                        Narr = ddd[0].ToString();
                        entity3.CHQ_NO = "NEFT INB: " + ddd[1].ToString();
                    }
                    else
                    {
                        ddd = row[2].ToString().Replace("RTGS INB:", "^").Split('^');
                        if (ddd.Length == 2)
                        {
                            entity3.NARRATION = ddd[0].ToString();
                            entity3.CHQ_NO = "RTGS INB: " + ddd[1].ToString();
                        }
                        else
                        {
                            ddd = row[2].ToString().Replace("TRANSFER FROM", "^").Split('^');
                            if (ddd.Length == 2)
                            {
                                entity3.NARRATION = ddd[0].ToString();
                                entity3.CHQ_NO = "TRANSFER FROM " + ddd[1].ToString();
                            }
                            else
                            {
                                ddd = row[2].ToString().Replace("TRANSFER TO", "^").Split('^');
                                if (ddd.Length == 2)
                                {
                                    entity3.NARRATION = ddd[0].ToString();
                                    entity3.CHQ_NO = "TRANSFER TO " + ddd[1].ToString();
                                }
                                else
                                {
                                    entity3.NARRATION = row[2].ToString();
                                    //entity3.CHQ_NO = row[3].ToString();
                                    entity3.CHQ_NO = "";
                                }
                            }
                        }
                    }

                    str1 = row[1].ToString().Trim().Split('/');
                    if (str1.Length != 3)
                    {
                        str1 = row[1].ToString().Trim().Split(' ');
                    }
                    DateTime exact2 = new DateTime();
                    try
                    {
                        exact2 = DateTime.ParseExact(DateTime.ParseExact(str11 + "/" + GetMonth(str1[1]) + "/" + str1[2], "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                    }
                    catch { exact2 = DateTime.ParseExact(DateTime.ParseExact(str11 + "/" + str1[1] + "/" + str1[2], "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture); }
                    entity3.VALUE_DATE = new DateTime?(exact2);
                    decimal bal = 0;
                    try
                    {
                        bal = Convert.ToDecimal(row[7].ToString());
                        entity3.CLOSING_BALANCE = new Decimal?(Convert.ToDecimal(bal));
                        if (TBal <= bal)
                        {
                            entity3.DEPOSIT_AMOUNT = new Decimal?(Convert.ToDecimal(row[6].ToString()));
                        }
                        else
                        {
                            entity3.WITHDRAWAL_AMOUNT = new Decimal?(Convert.ToDecimal(row[5].ToString()));
                        }
                        TBal = bal;
                        this.dbcontext.PARSER_SHEETs.InsertOnSubmit(entity3);
                        this.dbcontext.SubmitChanges();
                    }
                    catch {
                        Session["En"] = entity3;
                    }
                }
                else
                {
                    entity3 = (PARSER_SHEET)Session["En"];
                    entity3.CHQ_NO = entity3.CHQ_NO + row[2].ToString();
                    decimal bal = 0;
                    bal = Convert.ToDecimal(row[7].ToString());
                    entity3.CLOSING_BALANCE = new Decimal?(Convert.ToDecimal(bal));
                    if (TBal <= bal)
                    {
                        entity3.DEPOSIT_AMOUNT = new Decimal?(Convert.ToDecimal(row[6].ToString()));
                    }
                    else
                    {
                        entity3.WITHDRAWAL_AMOUNT = new Decimal?(Convert.ToDecimal(row[5].ToString()));
                    }
                    TBal = bal;
                    entity3.CATEGORY_ID = GetCategoryId(row[1].ToString(), User.Identity.Name, false, "sbi");
                    this.dbcontext.PARSER_SHEETs.InsertOnSubmit(entity3);
                    this.dbcontext.SubmitChanges();
                }
            }
            DataRow row1 = dataTable.NewRow();
            row1[0] = (object)FileName;
            row1[1] = (object)this.DOCUMENT_ID;
            row1[2] = (object)this.PARSER_ID;
            dataTable.Rows.Add(row1);
            this.Session["Multi"] = (object)dataTable;
            return DOCUMENT_ID;
        }

        private string GetMonth(string p)
        {
            if (p.Trim().ToLower().Contains("jan"))
            {
                return "01";
            }
            if (p.Trim().ToLower().Contains("feb"))
            {
                return "02";
            }
            if (p.Trim().ToLower().Contains("mar"))
            {
                return "03";
            }
            if (p.Trim().ToLower().Contains("apr"))
            {
                return "04";
            }
            if (p.Trim().ToLower().Contains("may"))
            {
                return "05";
            }
            if (p.Trim().ToLower().Contains("jun"))
            {
                return "06";
            }
            if (p.Trim().ToLower().Contains("jul"))
            {
                return "07";
            }
            if (p.Trim().ToLower().Contains("aug"))
            {
                return "08";
            }
            if (p.Trim().ToLower().Contains("sep"))
            {
                return "09";
            }
            if (p.Trim().ToLower().Contains("oct"))
            {
                return "10";
            }
            if (p.Trim().ToLower().Contains("nov"))
            {
                return "11";
            }
            if (p.Trim().ToLower().Contains("dec"))
            {
                return "12";
            }
            else
                return "1";
        }

        private int SAVE_HDFC_RECORD(string FileName, DataSet ds)
        {
            DataTable dataTable;
            if (this.Session["Multi"] == null)
            {
                dataTable = new DataTable();
                dataTable.Columns.Add("FileName");
                dataTable.Columns.Add("DOCUMENT_ID");
                dataTable.Columns.Add("PARSER_ID");
            }
            else
                dataTable = (DataTable)this.Session["Multi"];
            DOCUMENT entity1 = new DOCUMENT();
            entity1.USER_ID = new int?(Convert.ToInt32(this.User.Identity.Name));
            entity1.DOCUMENT_NAME = FileName;
            entity1.DOCUMENT_TYPE = this.ddlBanks.SelectedItem.Value;
            entity1.IS_PASSWORD_PROTECTED = new bool?(false);
            entity1.ACCOUNT_TYPE = "";
            entity1.UPLOAD_DATE = new DateTime?(DateTime.Now);
            this.dbcontext.DOCUMENTs.InsertOnSubmit(entity1);
            this.dbcontext.SubmitChanges();
            this.DOCUMENT_ID = entity1.DOCUMENT_ID;
            PARSER_DOCUMENT entity2 = new PARSER_DOCUMENT();
            entity2.DOCUMENT_ID = new int?(this.DOCUMENT_ID);
            entity2.ACCOUNT_BRANCH = ds.Tables[0].Rows[0]["ACCOUNT_BRANCH"].ToString();
            entity2.ACCOUNT_OPEN_DATE = ds.Tables[0].Rows[0]["ACCOUNT_OPEN_DATE"].ToString();
            entity2.ACCOUNT_STATUS = ds.Tables[0].Rows[0]["ACCOUNT_STATUS"].ToString();
            entity2.ACCOUNTNO = ds.Tables[0].Rows[0]["ACCOUNTNO"].ToString();
            entity2.ADDRESS1 = ds.Tables[0].Rows[0]["ADDRESS1"].ToString();
            entity2.ADDRESS2 = ds.Tables[0].Rows[0]["ADDRESS2"].ToString();
            entity2.ADDRESS3 = ds.Tables[0].Rows[0]["ADDRESS3"].ToString();
            entity2.BRANCH_ADDRESS = ds.Tables[0].Rows[0]["BRANCH_ADDRESS"].ToString();
            entity2.BRANCH_CITY = ds.Tables[0].Rows[0]["BRANCH_CITY"].ToString();
            entity2.BRANCH_CODE = ds.Tables[0].Rows[0]["BRANCH_CODE"].ToString();
            entity2.BRANCH_PHONENO = ds.Tables[0].Rows[0]["BRANCH_PHONENO"].ToString();
            entity2.BRANCH_STATE = ds.Tables[0].Rows[0]["BRANCH_STATE"].ToString();
            entity2.CITY = ds.Tables[0].Rows[0]["CITY"].ToString();
            entity2.CURRENCY = ds.Tables[0].Rows[0]["CURRENCY"].ToString();
            entity2.CUSTID = ds.Tables[0].Rows[0]["CUSTID"].ToString();
            entity2.EMAIL = ds.Tables[0].Rows[0]["EMAIL"].ToString();
            entity2.NOMINATION = ds.Tables[0].Rows[0]["NOMINATION"].ToString();
            entity2.PARSER_NAME = ds.Tables[0].Rows[0]["PARSER_NAME"].ToString();
            entity2.PRODUCT_CODE = ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString();
            entity2.STATE = ds.Tables[0].Rows[0]["STATE"].ToString();
            this.dbcontext.PARSER_DOCUMENTs.InsertOnSubmit(entity2);
            this.dbcontext.SubmitChanges();
            this.PARSER_ID = entity2.PARSER_ID;
            foreach (DataRow row in (InternalDataCollectionBase)ds.Tables[1].Rows)
            {
                PARSER_SHEET entity3 = new PARSER_SHEET();
                entity3.PARSER_ID = new int?(this.PARSER_ID);
                DateTime exact1 = DateTime.ParseExact(DateTime.ParseExact(row[0].ToString(), "dd/MM/yy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                entity3.DATE = new DateTime?(exact1);
                entity3.NARRATION = row[1].ToString();
                entity3.CHQ_NO = row[2].ToString();
                DateTime exact2 = DateTime.ParseExact(DateTime.ParseExact(row[3].ToString(), "dd/MM/yy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                entity3.VALUE_DATE = new DateTime?(exact2);
                bool chk= false;
                if (!string.IsNullOrEmpty(row[4].ToString()))
                {
                    entity3.WITHDRAWAL_AMOUNT = new Decimal?(Convert.ToDecimal(row[4].ToString()));
                    chk=true;
                }
                if (!string.IsNullOrEmpty(row[5].ToString()))
                    entity3.DEPOSIT_AMOUNT = new Decimal?(Convert.ToDecimal(row[5].ToString()));
                if (!string.IsNullOrEmpty(row[6].ToString()))
                    entity3.CLOSING_BALANCE = new Decimal?(Convert.ToDecimal(row[6].ToString()));

                entity3.CATEGORY_ID = GetCategoryId(row[1].ToString(), User.Identity.Name, chk,"hdfc");
                this.dbcontext.PARSER_SHEETs.InsertOnSubmit(entity3);
                this.dbcontext.SubmitChanges();

            }
            DataRow row1 = dataTable.NewRow();
            row1[0] = (object)FileName;
            row1[1] = (object)this.DOCUMENT_ID;
            row1[2] = (object)this.PARSER_ID;
            dataTable.Rows.Add(row1);
            this.Session["Multi"] = (object)dataTable;
            return DOCUMENT_ID;
        }

        private int GetCategoryId(string NARRATION, string USERID, bool chk, string category)
        {
            int Cat = 0;
           IEnumerable<USER_CATEGORY> cat = dbcontext.USER_CATEGORies.Where(x => x.USER_ID == Convert.ToInt32(USERID) && x.DOCUMENT_TYPE.ToLower() == category);
            if (cat != null)
            {
                foreach (USER_CATEGORY CAT in cat)
                {
                    string Identifier1 = CAT.IDENTIFIER_1.Replace("\n", "").Replace("\r", "").Replace("\t", "");
                    string Identifier2 = CAT.IDENTIFIER_2.Replace("\n", "").Replace("\r", "").Replace("\t", "");
                    string Identifier3 = CAT.IDENTIFIER_3.Replace("\n", "").Replace("\r", "").Replace("\t", "");
                    if (NARRATION.Contains(Identifier1) && Identifier1 != "")
                    {
                        Cat = CAT.ID;
                    }
                    if (NARRATION.Contains(Identifier2) && Identifier2 != "")
                    {
                        Cat = CAT.ID;
                    }
                    if (NARRATION.Contains(Identifier3) && Identifier3 != "")
                    {
                        Cat = CAT.ID;
                    }
                }
                
            }
            return Cat;
        }

        private int SAVE_ICICI_RECORD(string FileName, DataSet ds)
        {
            DataTable dataTable;
            if (this.Session["Multi"] == null)
            {
                dataTable = new DataTable();
                dataTable.Columns.Add("FileName");
                dataTable.Columns.Add("DOCUMENT_ID");
                dataTable.Columns.Add("PARSER_ID");
            }
            else
                dataTable = (DataTable)this.Session["Multi"];
            DOCUMENT entity1 = new DOCUMENT();
            entity1.USER_ID = new int?(Convert.ToInt32(this.User.Identity.Name));
            entity1.DOCUMENT_NAME = FileName;
            entity1.DOCUMENT_TYPE = this.ddlBanks.SelectedItem.Value;
            entity1.IS_PASSWORD_PROTECTED = new bool?(false);
            entity1.ACCOUNT_TYPE = "";
            entity1.UPLOAD_DATE = new DateTime?(DateTime.Now);
            this.dbcontext.DOCUMENTs.InsertOnSubmit(entity1);
            this.dbcontext.SubmitChanges();
            this.DOCUMENT_ID = entity1.DOCUMENT_ID;
            PARSER_DOCUMENT entity2 = new PARSER_DOCUMENT();
            entity2.DOCUMENT_ID = new int?(this.DOCUMENT_ID);
            entity2.ACCOUNT_BRANCH = ds.Tables[0].Rows[0]["ACCOUNT_BRANCH"].ToString();
            entity2.ACCOUNT_OPEN_DATE = "";
            entity2.ACCOUNT_STATUS = ds.Tables[0].Rows[0]["ACCOUNT_TYPE"].ToString();
            entity2.ACCOUNTNO = ds.Tables[0].Rows[0]["ACCOUNT_NO"].ToString();
            entity2.ADDRESS1 = ds.Tables[0].Rows[0]["ADDRESS1"].ToString();
            entity2.ADDRESS2 = ds.Tables[0].Rows[0]["ADDRESS2"].ToString();
            entity2.ADDRESS3 = ds.Tables[0].Rows[0]["ADDRESS3"].ToString();
            entity2.BRANCH_ADDRESS = ds.Tables[0].Rows[0]["BRANCH_ADDRESS"].ToString();
            entity2.BRANCH_CITY = ds.Tables[0].Rows[0]["BRANCH_CITY"].ToString();
            entity2.BRANCH_STATE = ds.Tables[0].Rows[0]["BRANCH_STATE"].ToString();
            entity2.CITY = ds.Tables[0].Rows[0]["CITY"].ToString();
            entity2.CUSTID = ds.Tables[0].Rows[0]["CUST_ID"].ToString();
            entity2.NOMINATION = ds.Tables[0].Rows[0]["NOMINATION"].ToString();
            entity2.PARSER_NAME = ds.Tables[0].Rows[0]["NAME"].ToString();
            this.dbcontext.PARSER_DOCUMENTs.InsertOnSubmit(entity2);
            this.dbcontext.SubmitChanges();
            this.PARSER_ID = entity2.PARSER_ID;

            bool cch = false;
            foreach (DataColumn dc in ds.Tables[1].Columns)
            {
                if (dc.ColumnName == "TransRemarks")
                {
                    cch = true;
                }
            }
            if (cch == false)
            {
                if (ds.Tables[0].Rows[0]["ACCOUNT_TYPE"] != "OD")
                {
                    #region if Account type not equals to OD
                    foreach (DataRow row in (InternalDataCollectionBase)ds.Tables[1].Rows)
                    {
                        if (row[0] != (object)"")
                        {
                            PARSER_SHEET entity3 = new PARSER_SHEET();
                            entity3.PARSER_ID = new int?(this.PARSER_ID);
                            DateTime exact1 = new DateTime();
                            try
                            {
                                exact1 = DateTime.ParseExact(DateTime.ParseExact(row[0].ToString(), "dd-MM-yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                            }
                            catch
                            {
                                exact1 = DateTime.ParseExact(DateTime.ParseExact(row[0].ToString(), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                            }
                            entity3.DATE = new DateTime?(exact1);
                            entity3.NARRATION = row[2].ToString();
                            entity3.CHQ_NO = row[3].ToString();
                            try
                            {
                                //DateTime exact2 = DateTime.ParseExact(DateTime.ParseExact(row[1].ToString(), "dd-MM-yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                                entity3.VALUE_DATE = new DateTime?(exact1);
                            }
                            catch
                            {
                            }

                            if (!string.IsNullOrEmpty(row[5].ToString()) && row[5].ToString() != "0.0")
                                entity3.WITHDRAWAL_AMOUNT = new Decimal?(Convert.ToDecimal(row[5].ToString()));
                            if (!string.IsNullOrEmpty(row[6].ToString()) && row[6].ToString() != "0.0")
                                entity3.DEPOSIT_AMOUNT = new Decimal?(Convert.ToDecimal(row[6].ToString()));
                            if (!string.IsNullOrEmpty(row[7].ToString()) && row[7].ToString() != "0.0")
                                entity3.CLOSING_BALANCE = new Decimal?(Convert.ToDecimal(row[7].ToString()));
                            entity3.CATEGORY_ID = GetCategoryId(row[2].ToString(), User.Identity.Name, false, "icici");
                            this.dbcontext.PARSER_SHEETs.InsertOnSubmit(entity3);
                            this.dbcontext.SubmitChanges();
                        }
                    }
                    #endregion
                }
                else
                {
                    int ppp = 0;
                    #region if Account type equals to OD
                    foreach (DataRow row in (InternalDataCollectionBase)ds.Tables[1].Rows)
                    {
                        ppp = ppp + 1;
                        if (ppp == 340)
                        {
                            string pp = "0";
                        }
                        if (row[0] != (object)"")
                        {
                            string Narration2 = row[2].ToString().Trim().Replace(" Dr", "^");
                            if (Narration2.Split('^').Length >= 2)
                            {
                                string[] splitstatement = Narration2.Split('^');
                                foreach (string gettext in splitstatement)
                                {
                                    if (gettext != "" && !gettext.Contains("Legends for transactions in your account statement"))
                                    {
                                        string Narration = gettext + "Dr";
                                        PARSER_SHEET entity3 = new PARSER_SHEET();
                                        entity3.PARSER_ID = new int?(this.PARSER_ID);
                                        DateTime exact1 = DateTime.ParseExact(DateTime.ParseExact(row[0].ToString(), "dd-MM-yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                                        entity3.DATE = new DateTime?(exact1);
                                        entity3.VALUE_DATE = new DateTime?(exact1);

                                        string[] NarrationList = Narration.Split(' ');
                                        string Balance = string.Empty;
                                        string Deposit = string.Empty;
                                        string Withdrawal = string.Empty;
                                        string chqNo = string.Empty;
                                        string Particular = string.Empty;
                                        for (int RowId = NarrationList.Length - 1; RowId >= 0; RowId--)
                                        {
                                            string value = NarrationList[RowId];
                                            if (value.Contains("Dr"))
                                            {
                                                Balance = value.Replace("Dr", "").Trim();
                                            }
                                            else if (value.Trim() == "B/F")
                                            {
                                                Deposit = "0";
                                                Withdrawal = "0";
                                                Particular = value;
                                            }
                                            else
                                            {

                                                if (value.Trim().Contains(".") && value.Trim().Split('.').Length == 2)
                                                {
                                                    if (Deposit == string.Empty)
                                                    {
                                                        Deposit = value.Trim();
                                                    }
                                                    else if (Withdrawal == string.Empty)
                                                    {
                                                        Withdrawal = value.Trim();
                                                    }
                                                    else
                                                    {
                                                        Particular = value.Trim() + " " + Particular;
                                                    }
                                                }
                                                else
                                                {
                                                    Particular = value.Trim() + " " + Particular;
                                                }

                                            }
                                        }



                                        entity3.NARRATION = Particular;
                                        entity3.CHQ_NO = chqNo;

                                        if (Withdrawal != "0.00")
                                            entity3.WITHDRAWAL_AMOUNT = new Decimal?(Convert.ToDecimal(Withdrawal));
                                        if (Deposit != "0.00")
                                            entity3.DEPOSIT_AMOUNT = new Decimal?(Convert.ToDecimal(Deposit));
                                        if (Balance != "")
                                            entity3.CLOSING_BALANCE = new Decimal?(Convert.ToDecimal(Balance));
                                        entity3.CATEGORY_ID = GetCategoryId(Particular, User.Identity.Name, false, "icici");
                                        this.dbcontext.PARSER_SHEETs.InsertOnSubmit(entity3);
                                        this.dbcontext.SubmitChanges();
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            else
            {
                #region if Account type not equals to OD
                foreach (DataRow row in (InternalDataCollectionBase)ds.Tables[1].Rows)
                {
                    if (row[0] != (object)"")
                    {
                        PARSER_SHEET entity3 = new PARSER_SHEET();
                        entity3.PARSER_ID = new int?(this.PARSER_ID);
                        DateTime exact1 = new DateTime();
                        try
                        {
                            exact1 = DateTime.ParseExact(DateTime.ParseExact(row[0].ToString(), "dd-MM-yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            exact1 = DateTime.ParseExact(DateTime.ParseExact(row[0].ToString(), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                        }
                        entity3.DATE = new DateTime?(exact1);
                        entity3.NARRATION = row[6].ToString();
                        entity3.CHQ_NO = row[2].ToString();
                        try
                        {
                            //DateTime exact2 = DateTime.ParseExact(DateTime.ParseExact(row[1].ToString(), "dd-MM-yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"), "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                            entity3.VALUE_DATE = new DateTime?(exact1);
                        }
                        catch
                        {
                        }

                        if (!string.IsNullOrEmpty(row[5].ToString()) && row[5].ToString() != "0.0")
                            entity3.WITHDRAWAL_AMOUNT = new Decimal?(Convert.ToDecimal(row[5].ToString()));
                        if (!string.IsNullOrEmpty(row[4].ToString()) && row[4].ToString() != "0.0")
                            entity3.DEPOSIT_AMOUNT = new Decimal?(Convert.ToDecimal(row[4].ToString()));
                        if (!string.IsNullOrEmpty(row[3].ToString()) && row[3].ToString() != "0.0")
                            entity3.CLOSING_BALANCE = new Decimal?(Convert.ToDecimal(row[3].ToString()));
                        entity3.CATEGORY_ID = GetCategoryId(row[2].ToString(), User.Identity.Name, false, "icici");
                        this.dbcontext.PARSER_SHEETs.InsertOnSubmit(entity3);
                        this.dbcontext.SubmitChanges();
                    }
                }
                #endregion
            }
            DataRow row1 = dataTable.NewRow();
            row1[0] = (object)FileName;
            row1[1] = (object)this.DOCUMENT_ID;
            row1[2] = (object)this.PARSER_ID;
            dataTable.Rows.Add(row1);
            this.Session["Multi"] = (object)dataTable;
            return DOCUMENT_ID;
        }

        private DataSet Export_HDFC_PDFToExcel(string fileName, bool IsPasswordProtected, string Password)
        {
            return new HDFC().Export_HDFC_PDFToExcel(fileName, IsPasswordProtected, Password);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
        }

        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            this.Response.Redirect("/AddDoc.aspx");
        }

        protected void rptDOCUMENTS_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!(e.CommandName == "dnld"))
                return;
            int int32 = Convert.ToInt32(e.CommandArgument);
            Label control = (Label)this.rptDOCUMENTS.Items[int32].FindControl("lblParser_ID");
            this.Session["DOCUMENT_ID"] = (object)((Label)this.rptDOCUMENTS.Items[int32].FindControl("lblDOCUMENT_ID")).Text;
            this.Session["PARSER_ID"] = (object)control.Text;
            this.Response.Redirect("UpdateCategory.aspx");
        }

        protected void rptFiles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "add")
            {
                int int32 = Convert.ToInt32(e.CommandArgument);
                DataTable dataTable1 = new DataTable();
                DataTable dataTable2;
                if (this.Session["dt_statement"] == null)
                {
                    dataTable2 = new DataTable();
                    dataTable2.Columns.Add("Id");
                    dataTable2.Columns.Add("Statement");
                    DataRow row = dataTable2.NewRow();
                    row[0] = (object)"1";
                    row[1] = (object)"Statement 1";
                    dataTable2.Rows.Add(row);
                    this.Session["dt_statement"] = (object)dataTable2;
                }
                else
                {
                    dataTable2 = (DataTable)this.Session["dt_statement"];
                    DataRow row = dataTable2.NewRow();
                    row[0] = (object)(int32 + 2).ToString();
                    row[1] = (object)("Statement " + (int32 + 2).ToString());
                    dataTable2.Rows.Add(row);
                    this.Session["dt_statement"] = (object)dataTable2;
                }
                this.rptFiles.DataSource = (object)dataTable2;
                this.rptFiles.DataBind();
            }
            if (e.CommandName == "del")
            {
                int int32 = Convert.ToInt32(e.CommandArgument);
                DataTable dataTable1 = new DataTable();
                DataTable dataTable2;
                if (this.Session["dt_statement"] == null)
                {
                    dataTable2 = new DataTable();
                    dataTable2.Columns.Add("Id");
                    dataTable2.Columns.Add("Statement");
                    DataRow row = dataTable2.NewRow();
                    row[1] = (object)"1";
                    row[1] = (object)"Statement 1";
                    dataTable2.Rows.Add(row);
                    this.Session["dt_statement"] = (object)dataTable2;
                }
                else
                    dataTable2 = (DataTable)this.Session["dt_statement"];
                DataTable dataTable3 = new DataTable();
                dataTable3.Columns.Add("Id");
                dataTable3.Columns.Add("Statement");
                foreach (DataRow row in (InternalDataCollectionBase)dataTable2.Rows)
                {
                    if (row[0].ToString() != int32.ToString())
                        dataTable3.Rows.Add(row.ItemArray);
                }
                this.Session["dt_statement"] = (object)dataTable3;
                this.rptFiles.DataSource = (object)dataTable3;
                this.rptFiles.DataBind();
            }
            this.CheckStstement();
        }
    }

}