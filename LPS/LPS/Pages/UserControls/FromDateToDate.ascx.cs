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

public partial class CMS_UserControls_FromDateToDate : System.Web.UI.UserControl
{
    public string FromDateStr
    {
        set { txtFromDate.Text = value; }
        get { return txtFromDate.Text; }
    }
    public string ToDateStr
    {
        set { txtToDate.Text = value; }
        get { return txtToDate.Text; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
