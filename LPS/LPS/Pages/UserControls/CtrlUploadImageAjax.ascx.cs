using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LPS.Pages.UserControls
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        public string guidId = string.Empty;

        public string UploadFolder = string.Empty;
        public string TitleUpload = string.Empty;

        public string TextboxUpload
        {
            set { txtUpload.Value = value; }
            get { return txtUpload.Value; }
        }
       
        public string ImageUpload
        {
            set { imageUpload.Src = value; }
            get { return imageUpload.Src; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                guidId = System.Guid.NewGuid().ToString().Substring(23, 13);
            }

        }

    }
}