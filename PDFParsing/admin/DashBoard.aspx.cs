using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFParsing.admin
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
                    string UserName = User.Identity.Name;
                    if (UserName == "admin")
                    {
                        IEnumerable<USER> users = dbcontext.USERs.Where(x => x.IS_ADMIN == false);
                        TotalUsers.InnerText = users.Count().ToString();
                        IEnumerable<DOCUMENT> docs = dbcontext.DOCUMENTs;
                        TotalDol.InnerText = docs.Count().ToString();

                        var data = dbcontext.GetReport_admin();
                        rptReport.DataSource = data;
                        rptReport.DataBind();
                    }
                    else
                    {
                        Response.Redirect("/Default.aspx");
                    }
                }
                else
                {
                    Response.Redirect("/Default.aspx");
                }
            }
        }
    }
}