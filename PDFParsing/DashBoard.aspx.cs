using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFParsing
{
    public partial class DashBoard : System.Web.UI.Page
    {
        PDFParseDataContext dbcontext = new PDFParseDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    int USER_ID = Convert.ToInt32(User.Identity.Name);
                    var data = dbcontext.GetReport_USER(USER_ID);
                    rptReport.DataSource = data;
                    rptReport.DataBind();

                    TotalDocs.InnerText = dbcontext.DOCUMENTs.Where(x => x.USER_ID == USER_ID).Count().ToString();
                }
            }
        }
    }
}