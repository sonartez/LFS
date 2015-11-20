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

public partial class Cms_UserControls_PagingControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    // Fields
    private int _CurrentPage;
    private string _FirstString;
    private string _LastString;
    private string _NextString;
    private int _PageSize;
    private string _PrevString;
    private int _TotalNumberPaging = 5;
    private int _TotalPages;
    private int _TotalRecord;



    // Events
    public event CommandEventHandler Paging_Click;

    // Methods
    public void DataLoad()
    {
        this.lnkFirst.CommandName = "FirstPage";
        this.lnkPrev.CommandName = "PrevPage";
        this.lnkNext.CommandName = "NextPage";
        this.lnkLast.CommandName = "FirstPage";
        this.SetControls();
        if (this._TotalRecord > 0)
        {
            DataRow row;
            this._TotalPages = this._TotalRecord / this._PageSize;
            if ((this._TotalPages * this._PageSize) != this._TotalRecord)
            {
                this._TotalPages++;
            }
            this.lnkFirst.CommandArgument = "1";
            this.lnkPrev.CommandArgument = Convert.ToString((int)(this._CurrentPage - 1));
            this.lnkNext.CommandArgument = Convert.ToString((int)(this._CurrentPage + 1));
            this.lnkLast.CommandArgument = this._TotalPages.ToString();
            DataTable table = new DataTable();
            new DataView();
            table.Columns.Add(new DataColumn("PageNumber"));
            table.Columns.Add(new DataColumn("ISDisplayCurrent", typeof(bool)));
            table.Columns.Add(new DataColumn("ISDisplayLink", typeof(bool)));
            if (this._CurrentPage > this._TotalPages)
            {
                this._CurrentPage = 1;
            }
            int num = 1;
            int num2 = 1;
            int num3 = 0;
            int num4 = 0;
            if (this._CurrentPage == 1)
            {
                num = 1;
            }
            if (this._CurrentPage > 1)
            {
                num3 = this._TotalNumberPaging / 2;
                num = this._CurrentPage - num3;
                if (num < 1)
                {
                    num = 1;
                }
            }
            num3 = num;
            while (num <= this._TotalPages)
            {
                if (num2 > this._TotalNumberPaging)
                {
                    break;
                }
                if (num4 == 0)
                {
                    num4 = num;
                }
                row = table.NewRow();
                row["PageNumber"] = num.ToString();
                table.Rows.Add(row);
                num2++;
                num++;
            }
            if (num2 <= this._TotalNumberPaging)
            {
                int num5 = this._CurrentPage - 1;
                while ((num2 <= this._TotalNumberPaging) && (num5 > 0))
                {
                    num3--;
                    if (num3 > 0)
                    {
                        row = table.NewRow();
                        row["PageNumber"] = num3.ToString();
                        table.Rows.Add(row);
                        num4 = num3;
                    }
                    num5--;
                    num2++;
                }
            }
            table.AcceptChanges();
            this.rptPaging.DataSource = table.DefaultView;
            this.rptPaging.DataBind();
            if (this._CurrentPage == 1)
            {
                this.ShowFirstLink(false);
                this.ShowPrevLink(false);
                this.pnl_First.CssClass = "nav";
                this.pnl_Prev.CssClass = "nav";
            }
            else
            {
                this.ShowFirstLink(true);
                this.ShowPrevLink(true);
                this.pnl_First.CssClass = "";
                this.pnl_Prev.CssClass = "";
            }
            if (this._CurrentPage == this._TotalPages)
            {
                this.ShowLastLink(false);
                this.ShowNextLink(false);
                this.pnl_Next.CssClass = "nav";
                this.pnl_Last.CssClass = "nav";
            }
            else
            {
                this.ShowLastLink(true);
                this.ShowNextLink(true);
                this.pnl_Next.CssClass = "";
                this.pnl_Last.CssClass = "";
            }
            RepeaterItemCollection items = this.rptPaging.Items;
            num3 = 0;
            while (num3 < items.Count)
            {
                LinkButton button = (LinkButton)items[num3].FindControl("lnkPaging");
                Label label = (Label)items[num3].FindControl("lblPaging");
                if (num4 == this._CurrentPage)
                {
                    label.Visible = true;
                    button.Visible = false;
                }
                else
                {
                    label.Visible = false;
                    button.Visible = true;
                }
                label.Text = Convert.ToString(num4);
                label.ToolTip ="Trang "+ Convert.ToString(num4);
                button.CommandArgument = Convert.ToString(num4);
                button.Text = Convert.ToString(num4);
                button.ToolTip ="Trang "+ Convert.ToString(num4);
                button.CommandName = "SelectPaging";
                num3++;
                num4++;
            }
        }

         
    }
    


    protected void lnkPaging_Click(object sender, CommandEventArgs e)
    {
        string commandName = e.CommandName;
        if (commandName != null)
        {
            if (!(commandName == "FirstPage"))
            {
                if (!(commandName == "PrevPage"))
                {
                    if (!(commandName == "NextPage"))
                    {
                        if (commandName == "LastPage")
                        {
                            this._CurrentPage = this._TotalRecord;
                            this.Paging_Click(this, e);
                        }
                        return;
                    }
                    this._CurrentPage++;
                    this.Paging_Click(this, e);
                    return;
                }
            }
            else
            {
                this._CurrentPage = 1;
                this.Paging_Click(this, e);
                return;
            }
            this._CurrentPage--;
            this.Paging_Click(this, e);
        }
    }

    protected void rptPaging_OnItemCommand(object sender, CommandEventArgs e)
    {
        if ((e.CommandName == "SelectPaging") && (this.Paging_Click != null))
        {
            this.Paging_Click(this, e);
        }
    }

    public void SetControls()
    {
        if (this._FirstString == "")
        {
            this.pnl_First.Visible = false;
        }
        this.lnkFirst.Text = this._FirstString;
        this.lblFirst.Text = this._FirstString;
        if (this._PrevString == "")
        {
            this.pnl_Prev.Visible = false;
        }
        this.lnkPrev.Text = this._PrevString;
        this.lblPrev.Text = this._PrevString;
        if (this._NextString == "")
        {
            this.pnl_Next.Visible = false;
        }
        this.lnkNext.Text = this._NextString;
        this.lblNext.Text = this._NextString;
        if (this._LastString == "")
        {
            this.pnl_Last.Visible = false;
        }
        this.lnkLast.Text = this._LastString;
        this.lblLast.Text = this._LastString;
    }

    public void ShowFirstLink(bool Status)
    {
        this.lnkFirst.Visible = Status;
        this.lblFirst.Visible = !Status;
    }

    public void ShowLastLink(bool Status)
    {
        this.lnkLast.Visible = Status;
        this.lblLast.Visible = !Status;
    }

    public void ShowNextLink(bool Status)
    {
        this.lnkNext.Visible = Status;
        this.lblNext.Visible = !Status;
    }

    public void ShowPrevLink(bool Status)
    {
        this.lnkPrev.Visible = Status;
        this.lblPrev.Visible = !Status;
    }

  

    public int CurrentPage
    {
        get
        {
            return this._CurrentPage;
        }
        set
        {
            this._CurrentPage = value;
        }
    }

    public string FirstString
    {
        set
        {
            this._FirstString = value;
        }
    }

    public string LastString
    {
        set
        {
            this._LastString = value;
        }
    }

    public string NextString
    {
        set
        {
            this._NextString = value;
        }
    }

    public int PageSize
    {
        set
        {
            this._PageSize = value;
        }
    }

    public string PrevString
    {
        set
        {
            this._PrevString = value;
        }
    }



    public int TotalNumberPaging
    {
        set
        {
            this._TotalNumberPaging = value;
        }
    }

    public int TotalPages
    {
        get
        {
            return this._TotalPages;
        }
    }

    public int TotalRecord
    {
        set
        {
            this._TotalRecord = value;
        }
    }


}


