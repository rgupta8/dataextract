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
    public partial class Default : System.Web.UI.Page
    {
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
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            USER login = dbcontext.USERs.FirstOrDefault(x => x.EMAIL_ADDRESS == username && x.PASSWORD == password);
            if (login != null)
            {
                if (login.IS_ADMIN == true)
                {
                    FormsAuthentication.RedirectFromLoginPage("admin", true);
                    Response.Redirect("/admin/Dashboard.aspx");
                }
                else
                {
                    Session["UserId"] = login.USER_ID.ToString();
                    FormsAuthentication.RedirectFromLoginPage(login.USER_ID.ToString(), true);
                    Response.Redirect("/Dashboard.aspx");
                }
            }
            else
            {
                lblMsg.Text = "User and Password do not match. Please try again.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnLogin2_Click1(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            USER login = dbcontext.USERs.FirstOrDefault(x => x.EMAIL_ADDRESS == username && x.PASSWORD == password);
            if (login != null)
            {
                if (login.IS_ADMIN == true)
                {
                    FormsAuthentication.RedirectFromLoginPage("admin", true);
                    Response.Redirect("/admin/Dashboard.aspx");
                }
                else
                {
                    Session["UserId"] = login.USER_ID.ToString();
                    FormsAuthentication.RedirectFromLoginPage(login.USER_ID.ToString(), true);
                    Response.Redirect("/Dashboard.aspx");
                }
            }
            else
            {
                lblMsg.Text = "User and Password do not match. Please try again.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}