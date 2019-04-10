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
    public partial class ContactUs : System.Web.UI.Page
    {
        StoreToDB cls = new StoreToDB();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string mobile = txtMobile.Text;
            string message = txtMessage.Text;
            try
            {
                cls.SendContactUsEmail(name, email, mobile, message);
                lblMsg.Text = "Your Query submitted successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                txtName.Text = "";
                txtEmail.Text = "";
                txtMobile.Text = "";
                txtMessage.Text = "";
            }
            catch (Exception ex){
                lblMsg.Text = ex.Message.ToString(); ;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

        }
    }
}