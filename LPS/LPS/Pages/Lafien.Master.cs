using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LPS.Pages
{
    public partial class Lafien : System.Web.UI.MasterPage
    {
        public Account loggedAccount;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["LoggedAccount"] != null)
            {
                try
                {
                    loggedAccount = (Account)HttpContext.Current.Session["LoggedAccount"];
                    if (loggedAccount == null)
                    {
                        Response.Redirect("~/CMS/");
                    }
                }
                catch
                {
                    Response.Redirect("~/CMS/");
                }
            }
            else
            {
                Response.Redirect("~/CMS/");
            }
        }
    }
}