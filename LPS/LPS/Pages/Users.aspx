<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Lafien.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="LPS.Pages.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>LAFIEN :: Accounts Managerment</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Accounts <small>Accounts Management</small>
            </h1>           
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <h2>
                <button type="button" id="btnPCAdd" name="btnPCAdd" onclick=" $('#frmPCEdit').slideDown();$('#btnPCAdd').hide();" class="btn btn-primary">Add New</button>
            </h2>
        </div>
        <div id="frmPCEdit" class="col-lg-12  hidden-form ">
            <div class="panel panel-default">
                <div class="panel-body">
                    <asp:Label runat="server" ID="txtMess"></asp:Label>
                    <br />
                    <div class="form-group">
                        <label>Account type</label><br />
                        <asp:RadioButton ID="radioAdmin" GroupName="AccountType" runat="server" Text="Administrator" />
                        <asp:RadioButton ID="radioUser" GroupName="AccountType" runat="server" Checked="true" Text="User" />
                        <p class="help-block">User is only manager products. Administrator can manager user accounts </p>
                    </div>
                    <div class="form-group">
                        <label>Full Name</label>
                        <input runat="server" id="txtFullName" class="form-control">
                        <p class="help-block">User full-name.</p>
                    </div>
                    <div class="form-group">
                        <label>Email</label>
                        <input runat="server" type="email" id="txtEmail" class="form-control">
                    </div>
                    <div class="form-group">
                        <label>Username</label>
                        <input runat="server" type="text" id="txtUserName" title="" pattern="\w+" class="form-control">
                        <p class="help-block">Username using for login, space or symboys is not allow</p>
                    </div>
                    <div class="form-group">
                        <label>Password</label>
                        <input runat="server" type="password" id="txtPassword" name="pw1" onchange="form.pwd2.pattern = this.value;" title=""  pattern="(?=.*[a-zA-Z0-9~!@#$%^&*())_+]).{6,}" class="form-control">
                        <p class="help-block">
                            Minimun lenght is 6 chars.
                        </p>
                    </div>
                    <div class="form-group">
                        <label>Confirm password</label>
                        <input runat="server" type="password" name="pw2" title="" pattern="(?=.*[a-zA-Z0-9~!@#$%^&*())_+]).{6,}" id="txtPasswordConfirm" class="form-control">
                        <p class="help-block"></p>
                    </div>
                    <asp:Button ID="btnSave" CssClass="btn btn-default" runat="server" Text="Save" OnClick="btnSave_Click" />
                    <button type="reset" class="btn btn-default" onclick="CloseForm()">Cancel</button>
                </div>
            </div>
        </div>
        <div class="col-lg-12 ">
            <div class="panel panel-default">
                <div class="panel-body">
                    <h2>Listing</h2>
                    <div class="table-responsive">
                        <asp:GridView ID="gvListItem" CssClass="table table-hover" runat="server" GridLines="None" AutoGenerateColumns="False" OnRowCommand="gvListItem_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        #ID
                                    </HeaderTemplate>
                                    <HeaderStyle Width="50px" />
                                    <ItemTemplate>
                                        #<asp:Label ID="lblId" runat="server" Text='<%#Eval("AccountId").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="col-center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Name
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("FullName").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        User Name
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Account Type
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccountType" runat="server" Text='<%#Eval("AccountType").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Controls
                                    </HeaderTemplate>
                                    <HeaderStyle Width="115px" CssClass="text-right" />
                                    <ItemTemplate>
                                        <a class="btn btn-primary btn-xs" href="UserEdit.aspx?Id=<%#Eval("AccountId").ToString() %>">Edit
                                        </a>
                                        <asp:Button Text="Delete" ID="btnDeleteItem" CssClass="btn btn-default btn-xs" runat="server" CommandName="DeleteItem" CommandArgument='<%#Bind("AccountId") %>'></asp:Button>
                                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender_btnDeleteItem" runat="server" TargetControlID="btnDeleteItem" ConfirmText='<%# "Are your want to delete : " + Eval("UserName")   %>'>
                                        </ajaxToolkit:ConfirmButtonExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lblEmptyData" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Red"
                                    Text="Không có dữ liệu"></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
