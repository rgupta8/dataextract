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
    public partial class ForgetPassword : System.Web.UI.Page
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
            
        }

        protected void btnLogin2_Click1(object sender, EventArgs e)
        {
            string username = txtEmail.Text.Trim();
            
            USER login = dbcontext.USERs.FirstOrDefault(x => x.EMAIL_ADDRESS == username);
            if (login != null)
            {
                cls.ForgotPasswordEmail(login.USERNAME, login.EMAIL_ADDRESS, login.PASSWORD);
                lblMsg.Text = "An email sent to your registered email address.";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMsg.Text = "No account found. Please try again.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}