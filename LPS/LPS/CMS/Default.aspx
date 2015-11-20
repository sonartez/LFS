<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LPS.CMS.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/Site.css" rel="stylesheet" />
</head>
<body style="width:100%;height:100%">
    <form id="form1" runat="server">
    <div style="width:440px;height:200px; margin:auto">
    <div style="width:100%; text-align:center" >
        <img src="http://www.lafien.co.kr/cont_kr/top/20130225093343_logo_en.png" />
    </div>

        <div style="background:url('../Content/signature.png') right center no-repeat; width:80%; padding:20px;line-height:30px; margin:10px; margin-top:30px; border:1px solid #808080 ; border-radius:5px;box-shadow:#110b1d 2px 2px" >
        <label> User Name</label><br />
            <input type="text" id="txtUsername" required runat="server" /><br />
            <label>Password</label><br />
            <input type="password" id="txtPassword" required runat="server" /><br />
            <button style="padding:5px;border-radius:5px;box-shadow: 2px 2px" type="submit" runat="server" >LOGIN TO LAFIEN CMS</button>
        </div>
        <asp:Label runat="server" ID="txtError" Visible="false" ForeColor="Red"></asp:Label>
        <div style="margin-top:30px;">
            <b> LADO FILTER ENGINEERING CO., LTD, </b><br />
            875-1, JINAN DONG, HWASEONG-SI, KYUNGGI-DO 445-390, KOREA / TEL: +82-31-374-0163 / FAX: +82-31-374-0164
2014(c) LADO FILTER ENGINEERING Co.,Ltd. All rights reserved.
        </div>
    </div>
    </form>
</body>
</html>
