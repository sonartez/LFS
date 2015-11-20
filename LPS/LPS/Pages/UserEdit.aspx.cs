using LPS.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LPS.Pages
{
    public partial class UserEdit : System.Web.UI.Page
    {
        public Common comClass = new Common();
        public HashEncryption haenClass = new HashEncryption();
        private lafien_products_dbEntities db = new lafien_products_dbEntities();
        public List<LPS.Account> dataList, viewList = new List<LPS.Account>();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Account logged = (Account)Session["LoggedAccount"];

            //if (logged.AccountType != Common.TypeAdmin)
            //    Response.Redirect("~/CMS/");
            txtMess.Text = string.Empty;

            if (!IsPostBack)
            {
                LoadData();
                BackUrl.Value = Request.UrlReferrer.ToString();
            }
        }
        private void LoadData()
        {

            if (!comClass.IsNumeric(Request.QueryString.Get("Id")))
            {
                Response.Redirect(BackUrl.Value);
            }
            else
            {
                int Id = Int32.Parse(Request.QueryString.Get("Id"));
                LPS.Account obj = new LPS.Account();
                obj = db.Accounts.FirstOrDefault(x => x.AccountId == Id);

                AccountID.Value = obj.AccountId.ToString();
                txtEmail.Value = obj.Email;
                txtFullName.Value = obj.FullName;
                txtUserName.Value = obj.UserName;
                txtUserName.Disabled = true;
                txtPassword.Value = string.Empty;
                txtPasswordConfirm.Value = string.Empty;
                if (obj.AccountType.Trim()== Common.TypeAdmin) radioAdmin.Checked = true;
                else
                    radioUser.Checked = true;
                //chkPCActive.Checked = obj.Active;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input value
                if (string.IsNullOrEmpty(txtUserName.Value.Trim())                   
                    || string.IsNullOrEmpty(txtEmail.Value.Trim())
                    )
                {
                    txtMess.ForeColor = Color.Red;
                    txtMess.Text = "Missing input data!";
                    return;
                }


                // Account logged = (Account)Session["LoggedAccount"];
                int Id = Int32.Parse(AccountID.Value);
                LPS.Account obj = db.Accounts.FirstOrDefault(x => x.AccountId == Id);
                if (obj != null)
                {
                    if (radioAdmin.Checked)
                        obj.AccountType = Common.TypeAdmin;
                    else
                        obj.AccountType = Common.TypeUser;
                    obj.Active = true;
                    //obj.CreateBy = logged.UserName ;
                    obj.Email = txtEmail.Value.Trim();
                    obj.FullName = txtFullName.Value.Trim();
                    obj.ModifyDate = DateTime.Now;
                    if (!string.IsNullOrEmpty(txtPassword.Value.Trim())){
                        if (txtPassword.Value.Trim().ToLower() == txtPasswordConfirm.Value.Trim().ToLower())
                        {
                            obj.Password = haenClass.SHA256Hash(txtPassword.Value.Trim());
                        }
                        else {
                            txtMess.ForeColor = Color.Red;
                            txtMess.Text = "Password confirm not match!";
                            return;
                        }
                    }
                   
                    db.SaveChanges();
                    LoadData();

                    txtMess.ForeColor = Color.Green;
                    txtMess.Text = "The account with username " + obj.UserName + " was updated successful";
                }
            }
            catch (Exception ex)
            {
                txtMess.ForeColor = Color.Red;
                txtMess.Text = ex.Message;
                
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(BackUrl.Value);
        }

    }
}