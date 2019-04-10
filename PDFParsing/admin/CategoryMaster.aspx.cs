using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFParsing.admin
{
    public partial class CategoryMaster : System.Web.UI.Page
    {
        PDFParseDataContext dbcontext = new PDFParseDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("/Default.aspx");
                }
                BindUsers();
            }
        }

        private void BindUsers()
        {
            var data = from a in dbcontext.CATEGORies
                       select new
                       {
                           CATEGORY_NAME = a.CATEGORY_NAME,
                           CATEGORYTYPE = a.CATEGORY_TYPE == 1 ? "DEBIT" : "CREDIT",
                           CATEGORY_ID = a.CATEGORY_ID
                       };

            rptCategory.DataSource = data;
            rptCategory.DataBind();
        }

    }
}