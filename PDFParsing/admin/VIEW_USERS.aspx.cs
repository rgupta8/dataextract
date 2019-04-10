using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PDFParsing.admin
{
    public partial class VIEW_USERS : System.Web.UI.Page
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
            var data = from a in dbcontext.USERs.Where(x => x.IS_ADMIN == false)
                       select new
                       {
                           USERNAME = a.USERNAME,
                           IS_ACTIVE = a.IS_ACTIVE == true ? "ACTIVE" : "IN-ACTIVE",
                           EMAIL_ADDRESS = a.EMAIL_ADDRESS,
                           CREATE_DATE = a.CREATE_DATE,
                           DOCUMENT_UPLOADED = getDOCUMENT_COUNTS(a.USER_ID),
                           USER_ID = a.USER_ID
                       };

            rptUsers.DataSource = data;
            rptUsers.DataBind();
        }

        public int getDOCUMENT_COUNTS(int USER_ID)
        {
            int count = 0;
            IEnumerable<DOCUMENT> doc = dbcontext.DOCUMENTs.Where(x => x.USER_ID == USER_ID);
            if (doc != null)
            {
                count = doc.Count();
            }
            return count;
        }
    }
}