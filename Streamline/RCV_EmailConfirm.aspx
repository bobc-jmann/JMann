<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RCV_EmailConfirm.aspx.vb" Inherits="RCV_EmailConfirm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%
            Dim vAddressEmailID As Integer = Request.QueryString("ID")
            If Not ismt(vAddressEmailID) Then
        		Dim s As String = "UPDATE tblAddressEmails SET Confirmed = 1 WHERE AddressEmailID = " & vAddressEmailID
        		DataUtil.SqlNonQuery(s)
            End If
            %>
        Your email address has been confirmed.<br />
        Thank you,<br />
        Streamline Mail Systems</div>
    </form>
</body>
</html>
