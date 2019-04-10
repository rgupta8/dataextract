using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFParsing.admin
{
    public partial class NewUser : System.Web.UI.Page
    {
        PDFParseDataContext dbcontext = new PDFParseDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    try
                    {
                        int Id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                        USER user = dbcontext.USERs.FirstOrDefault(x => x.USER_ID == Id);
                        if (user != null)
                        {
                            txtUserName.Text = user.USERNAME;
                            txtEmailAddress.Text = user.EMAIL_ADDRESS;
                            txtPassword.Text = user.PASSWORD;
                            chkIs_Active.Checked = (bool)user.IS_ACTIVE;
                        }

                    }
                    catch
                    {
                        Response.Redirect("VIEW_USERS.aspx");
                    }
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtEmailAddress.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                if (Request.QueryString["Id"] == null)
                {
                    USER user = dbcontext.USERs.FirstOrDefault(x => x.EMAIL_ADDRESS == txtEmailAddress.Text);
                    if (user != null)
                    {
                        lblMessage.Text = "Email Adress Already Exists.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        try
                        {
                            user = new USER();
                            user.CREATE_DATE = DateTime.Now;
                            user.EMAIL_ADDRESS = txtEmailAddress.Text;
                            user.IS_ACTIVE = chkIs_Active.Checked;
                            user.IS_ADMIN = false;
                            user.PASSWORD = txtPassword.Text;
                            user.USERNAME = txtUserName.Text;
                            dbcontext.USERs.InsertOnSubmit(user);
                            dbcontext.SubmitChanges();
                            lblMessage.Text = "User Added Successfully.";
                            lblMessage.ForeColor = System.Drawing.Color.Green;
                            Create_New_Document_Folder(txtEmailAddress.Text);
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message.ToString();
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                else
                {
                    int Id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                    USER user = dbcontext.USERs.FirstOrDefault(x => x.USER_ID == Id);
                    if (user != null)
                    {
                        try
                        {
                            user = new USER();
                            user.CREATE_DATE = DateTime.Now;
                            user.EMAIL_ADDRESS = txtEmailAddress.Text;
                            user.IS_ACTIVE = chkIs_Active.Checked;
                            user.IS_ADMIN = false;
                            user.PASSWORD = txtPassword.Text;
                            user.USERNAME = txtUserName.Text;
                            dbcontext.SubmitChanges();
                            lblMessage.Text = "User Updated Successfully.";
                            lblMessage.ForeColor = System.Drawing.Color.Green;
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message.ToString();
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            else
            {
                lblMessage.Text = "Please fill all mandatory fields.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void Create_New_Document_Folder(string Foldername)
        {
            if (!Directory.Exists(Server.MapPath("~") + "/DOCUMENTS/" + Foldername.Replace(".", "-").Replace("@", "-")))
            {
                Directory.CreateDirectory(Server.MapPath("~") + "/DOCUMENTS/" + Foldername.Replace(".", "-").Replace("@", "-"));
            }
        }
    }
}