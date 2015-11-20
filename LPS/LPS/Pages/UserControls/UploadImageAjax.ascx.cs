using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace LPS.Pages.UserControls
{
    public partial class UploadImageAjax : System.Web.UI.UserControl
    {
        public string guidId = string.Empty;

        public string UploadFolder = string.Empty;
        public string TitleUpload = string.Empty;

        public string TextboxUpload
        {
            set { txtUpload.Value = value; }
            get { return txtUpload.Value; }
        }
        public string HyperlinkUpload
        {
            set { hplUpload.HRef = value; }
            get { return hplUpload.HRef; }
        }
        public string ImageUpload
        {
            set { imageUpload.Src = value; }
            get { return imageUpload.Src; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            guidId = System.Guid.NewGuid().ToString().Substring(23, 13);
            
        }

    }
}