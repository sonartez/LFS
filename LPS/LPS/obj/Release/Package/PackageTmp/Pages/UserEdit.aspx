<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Lafien.Master" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="LPS.Pages.UserEdit" %>

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
              <asp:Button ID="btnBack" CssClass="btn btn-default" runat="server" Text="Back to List" OnClick="btnBack_Click" />
                   
            </h2>
        </div>
        <div id="frmPCEdit" class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <asp:Label runat="server" ID="txtMess"></asp:Label>
                    <br />
                    <div class="form-group">
                        <label>Account type</label><br />
                        <asp:RadioButton ID="radioAdmin" GroupName="AccountType" runat="server" Text="Administrator" />
                        <asp:RadioButton ID="radioUser" GroupName="AccountType" runat="server" Text="User" />
                        <p class="help-block">User is only manager products. Administrator can manager user accounts </p>
                    </div>
                    <div class="form-group">
                        <label>Full Name</label>
                        <input runat="server" required id="txtFullName" class="form-control">
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
                        <input runat="server" type="password" id="txtPassword" name="pw1" onchange="form.pwd2.pattern = this.value;" title="" pattern="(?=.*[a-zA-Z0-9~!@#$%^&*())_+]).{6,}" class="form-control">
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
                    <asp:HiddenField ID="AccountID" EnableViewState="true" runat="server" />
                    <asp:HiddenField ID="BackUrl" EnableViewState="true" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
