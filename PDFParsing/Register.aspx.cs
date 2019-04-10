using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFParsing
{
    public partial class Register : System.Web.UI.Page
    {
        StoreToDB cls = new StoreToDB();
        PDFParseDataContext dbcontext = new PDFParseDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            //string FilePath = @"C:\Users\sharma\Desktop\backup\SBI\1.pdf";
            //PdfReader reader = new PdfReader(FilePath);
            //string s = reader.Info["Author"];

            //SBI pdf = new SBI();
            //DataSet ds = pdf.ProcessPdfAndWriteCSVSBI(FilePath, false, "");
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {


        }

        protected void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnLogin2_Click(object sender, EventArgs e)
        {
            string username = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string mobile = txtMobile.Text.Trim();
            USER login = dbcontext.USERs.FirstOrDefault(x => x.EMAIL_ADDRESS == email);
            if (login != null)
            {
                lblMsg.Text = "This Email Id already exists. Please try login.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                
            }
            else
            {
                Random ramdom = new Random();
                string password = ramdom.Next(111111, 999999).ToString();
                login = new USER();
                login.CREATE_DATE = DateTime.Now;
                login.EMAIL_ADDRESS = email;
                login.IS_ACTIVE = true;
                login.IS_ADMIN = false;
                login.PASSWORD = password;
                login.USERNAME = username;
                dbcontext.USERs.InsertOnSubmit(login);
                dbcontext.SubmitChanges();
                try
                {
                    cls.SendErrorEmailToAdmin(username, email, password);
                }
                catch { }
                lblMsg.Text = "Account created successfully. You will get password in email";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
        }

        protected void btnLogin2_Click1(object sender, EventArgs e)
        {
            string username = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string mobile = txtMobile.Text.Trim();
            USER login = dbcontext.USERs.FirstOrDefault(x => x.EMAIL_ADDRESS == email);
            if (login != null)
            {
                lblMsg.Text = "This Email Id already exists. Please try login.";
                lblMsg.ForeColor = System.Drawing.Color.Red;

            }
            else
            {
                Random ramdom = new Random();
                string password = ramdom.Next(111111, 999999).ToString();
                login = new USER();
                login.CREATE_DATE = DateTime.Now;
                login.EMAIL_ADDRESS = email;
                login.IS_ACTIVE = true;
                login.IS_ADMIN = false;
                login.PASSWORD = password;
                login.USERNAME = username;
                dbcontext.USERs.InsertOnSubmit(login);
                dbcontext.SubmitChanges();
                try
                {
                    cls.SendErrorEmailToAdmin(username, email, password);
                }
                catch { }
                lblMsg.Text = "Account created successfully. You will get password in email";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
}