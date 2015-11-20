using LPS.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;

namespace LPS.Pages
{
    public partial class Users : System.Web.UI.Page
    {
        public  HashEncryption haenClass = new HashEncryption();
        private lafien_products_dbEntities db = new lafien_products_dbEntities();
        public List<LPS.Account> dataList, viewList = new List<LPS.Account>();
        Account logged;
        protected void Page_Load(object sender, EventArgs e)
        {
           // Get Current logged user, check for permissson
           logged = (Account)Session["LoggedAccount"];
           if (logged != null && logged.AccountType != Common.TypeAdmin)
               Response.Redirect("~/CMS/");

            txtMess.Text = string.Empty;
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        private void LoadData()
        {
            dataList = db.Accounts.ToList();
            gvListItem.DataSource = dataList;
            gvListItem.DataBind();
            MakeGridViewPrinterFriendly(gvListItem);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input value
                if (string.IsNullOrEmpty(txtUserName.Value.Trim())
                    || string.IsNullOrEmpty(txtPassword.Value.Trim())
                    || txtPassword.Value != txtPasswordConfirm.Value
                    || string.IsNullOrEmpty(txtEmail.Value.Trim())
                    )
                {
                    txtMess.ForeColor = Color.Red;
                    txtMess.Text = "Missing input data!";
                    return;
                }

                // Check username if exist

                if (db.Accounts.FirstOrDefault(x => x.UserName == txtUserName.Value.Trim()) != null) {
                    txtMess.ForeColor = Color.Red;
                    txtMess.Text = "This Username was existed";
                    return;
                }


                LPS.Account obj = new LPS.Account();
                if(radioAdmin.Checked) 
                    obj.AccountType = Common.TypeAdmin;
                else
                    obj.AccountType = Common.TypeUser;
                obj.Active = true;
                obj.CreateBy = logged.UserName;
                obj.CreateDate = DateTime.Now;
                obj.Email = txtEmail.Value.Trim();
                obj.FullName = txtFullName.Value.Trim();
                obj.ModifyDate = DateTime.Now;
                obj.Password = haenClass.SHA256Hash(txtPassword.Value.Trim());
                obj.UserName = txtUserName.Value.Trim();
               
                db.Accounts.Add(obj);
                db.SaveChanges();
                LoadData();

                txtMess.ForeColor = Color.Green;
                txtMess.Text = "The account with username "+obj.UserName+" was created successful";

            }
            catch (Exception ex)
            {
                txtMess.ForeColor = Color.Red;
                txtMess.Text = ex.Message;
                return;

            }
        }
        /// <summary>
        /// Make draw THEAD tag for gridview
        /// </summary>
        /// <param name="gridView"></param>
        private void MakeGridViewPrinterFriendly(GridView gridView)
        {
            if (gridView.Rows.Count > 0)
            {
                gridView.UseAccessibleHeader = true;
                gridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvListItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Int32.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

            if (e.CommandName.ToLower() == "deleteitem")
            {
                // Don't allow deleted it seft
                if (logged.AccountId == id)
                {
                    txtMess.ForeColor = Color.Red;
                    txtMess.Text = "You can't delete your self";
                    return;
                }

                LPS.Account obj = db.Accounts.Find(id);
                db.Accounts.Remove(obj);
                db.SaveChanges();
                LoadData();

                txtMess.ForeColor = Color.Green;
                txtMess.Text = "User account has been deleted";
                
            }
        }

     
    }
}