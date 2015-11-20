using LPS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LPS.CMS
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtError.Visible = false;
            txtError.Text = string.Empty;
            //Session.Abandon();
            if (IsPostBack) {
                lafien_products_dbEntities db = new lafien_products_dbEntities();
                HashEncryption hasEn = new HashEncryption();
                string enPass = hasEn.SHA256Hash(txtPassword.Value);
                Account obj = db.Accounts.FirstOrDefault(x => x.UserName == txtUsername.Value && x.Password == enPass);
                if (obj != null)
                {
                    HttpContext.Current.Session["LoggedAccount"] = obj;
                    Response.Redirect("~/Pages/Default.aspx");
                }
                else {
                    txtError.Visible = true;
                    txtError.Text = "Invalid Username or Password!";
                }
            }
        }
    }
}