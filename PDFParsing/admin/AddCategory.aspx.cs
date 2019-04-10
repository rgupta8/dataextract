using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFParsing.admin
{
    public partial class AddCategory : System.Web.UI.Page
    {
        PDFParseDataContext dbcontext = new PDFParseDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategoryType();
                if (Request.QueryString["Id"] != null)
                {
                    try
                    {
                        int Id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                        CATEGORY user = dbcontext.CATEGORies.FirstOrDefault(x => x.CATEGORY_ID == Id);
                        if (user != null)
                        {
                            txtCategoryName.Text = user.CATEGORY_NAME;
                            ddlType.Items.FindByValue(user.CATEGORY_TYPE.ToString()).Selected = true;
                        }

                    }
                    catch
                    {
                        Response.Redirect("CategoryMaster.aspx");
                    }
                }
            }
        }

        private void BindCategoryType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");

            DataRow row = dt.NewRow();
            row[0] = "1";
            row[1] = "Debit";
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "2";
            row[1] = "Credit";
            dt.Rows.Add(row);
            ddlType.DataSource = dt;
            ddlType.DataTextField = "Name";
            ddlType.DataValueField = "Id";
            ddlType.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCategoryName.Text))
            {
                if (Request.QueryString["Id"] == null)
                {
                    CATEGORY user = dbcontext.CATEGORies.FirstOrDefault(x => x.CATEGORY_NAME == txtCategoryName.Text);
                    if (user != null)
                    {
                        lblMessage.Text = "Category Name Already Exists.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        try
                        {
                            user = new CATEGORY();
                            user.CATEGORY_NAME = txtCategoryName.Text;
                            user.CATEGORY_TYPE = Convert.ToInt32(ddlType.SelectedItem.Value);
                            dbcontext.CATEGORies.InsertOnSubmit(user);
                            dbcontext.SubmitChanges();
                            lblMessage.Text = "Category Added Successfully.";
                            lblMessage.ForeColor = System.Drawing.Color.Green;
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
                    CATEGORY user = dbcontext.CATEGORies.FirstOrDefault(x => x.CATEGORY_ID == Id);
                    if (user != null)
                    {
                        try
                        {
                            user = new CATEGORY();
                            user.CATEGORY_NAME = txtCategoryName.Text;
                            user.CATEGORY_TYPE = Convert.ToInt32(ddlType.SelectedItem.Value);
                            dbcontext.SubmitChanges();
                            lblMessage.Text = "Category Updated Successfully.";
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
    }
}