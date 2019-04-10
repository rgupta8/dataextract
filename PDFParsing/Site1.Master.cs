using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFParsing
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        PDFParseDataContext dbcontext = new PDFParseDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    USER user = dbcontext.USERs.FirstOrDefault(x => x.USER_ID == Convert.ToInt32(HttpContext.Current.User.Identity.Name));
                    if (user != null)
                    {
                        UNAME.InnerText = user.USERNAME;
                    }
                }

            }
        }
    }
}