<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FromDateToDate.ascx.cs" Inherits="CMS_UserControls_FromDateToDate" %>

<table class="table-calendar-ajax">
    <tr>
        <td>
        Từ 
            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox-aspnet width15per"></asp:TextBox>
            <asp:LinkButton ID="btnFromDate" runat="server">
                <span class="for_ie6 button_tooltip" title="Chọn ngày dạng dd/mm/yyyy">
                    <img  src="Images/pc16/calendar.png" alt="" style="border:none;" class="fixImagePNG" />
                </span>
            </asp:LinkButton>
        &nbsp;&nbsp;    
        Đến
            <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox-aspnet width15per"></asp:TextBox> 
            <asp:LinkButton ID="btnToDate" runat="server">
                <span class="for_ie6 button_tooltip" title="Chọn ngày dạng dd/mm/yyyy">
                    <img  src="Images/pc16/calendar.png" alt="" style="border:none;" class="fixImagePNG" />
                </span>
            </asp:LinkButton>   
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1_txtFromDate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtFromDate" PopupButtonID="txtFromDate">
            </ajaxToolkit:CalendarExtender> 
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1_btnFromDate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtFromDate" PopupButtonID="btnFromDate">
            </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2_txtToDate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate" PopupButtonID="txtToDate">
            </ajaxToolkit:CalendarExtender> 
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2_btnToDate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate" PopupButtonID="btnToDate">
            </ajaxToolkit:CalendarExtender>
            
        </td>
    </tr>
</table>