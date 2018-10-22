<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="Login" %>
<asp:Content ID="contentAbout" ContentPlaceHolderID="HomeContent" Runat="Server">
    <h1><%=vCompanyName %> Intranet</h1>
    <table valign=top><tr>
        <td valign=top>
            <table border=0 cellpadding=5>
                <tr><td align=right></td><td><dx:aspxlabel ID="lblError" EncodeHtml="false" ForeColor="Red" Font-Bold="true" runat="server"></dx:aspxlabel></td></tr>
 
                <% if request.querystring("T")="RESET" then %>
                    <tr><td></td><td colspan=3><h3>Reset Password</h3></td></tr>
                    <tr><td align=right>User name:</td><td><dx:ASPxTextBox ID="txtUserName2" Width=150 runat="server"></dx:ASPxTextBox></td></tr>
                    <tr><td align=right>Old Password:</td><td><dx:ASPxTextBox ID="txtOldPassword" Width=150 Password="true" runat="server"></dx:ASPxTextBox></td></tr>
                    <tr><td align=right>New Password:</td><td><dx:ASPxTextBox ID="txtNewPassword" Width=150 Password="true" runat="server" ></dx:ASPxTextBox></td>
                    <tr><td align=right>Retype New Password:</td><td><dx:ASPxTextBox ID="txtConfirmNewPassword" Width=150 Password="true" runat="server" ></dx:ASPxTextBox></td>
                    <tr><td></td><td><dx:ASPxButton ID="btnReset" OnClick="btnLogin_Click" Text="Submit" CommandName="Submit" runat="server"></dx:ASPxButton></td></tr>
                <% ElseIf Request.QueryString("T") = "FORGOT" Then%>
                    <tr><td></td><td colspan=3><h3>Forgot Password</h3></td></tr>
                    <tr><td align=right>User name:</td><td><dx:ASPxTextBox ID="txtUserName3" Width=150 runat="server"></dx:ASPxTextBox></td></tr>
                    <tr><td align=right>Email you registered with us:</td><td><dx:ASPxTextBox ID="txtUserEmail" Width=150 runat="server"></dx:ASPxTextBox></td></tr>
                    <div runat="server" id="divQA" visible="false">
                        <tr><td align=right>Password question:</td><td><dx:ASPxLabel Font-Bold="true" ID="txtPasswordQuestion" Width=150 runat="server"></dx:ASPxLabel></td></tr>
                        <tr><td align=right>Your answer:</td><td><dx:ASPxTextBox ID="txtPasswordAnswer" Width=150 runat="server"></dx:ASPxTextBox></td></tr>
                    </div>
                    <tr><td></td><td><dx:ASPxButton ID="btnForgot" OnClick="btnLogin_Click" Text="Submit" CommandName="Submit" runat="server"></dx:ASPxButton></td></tr>
                <% Else%>
                    <tr><td></td><td colspan=3><h3>Login</h3></td></tr>
                    <tr><td align=right>User name:</td><td><dx:ASPxTextBox TabIndex=1 ID="txtUserName" Width=150 runat="server"></dx:ASPxTextBox></td></tr>
                    <tr><td align=right>Password:</td><td><dx:ASPxTextBox TabIndex=2 ID="txtPassword" Width=150 Password="true" runat="server" ></dx:ASPxTextBox></td>
                        <td><table><tr><td style="font-size:xx-small"><a href="?T=FORGOT">Forgot your password?</a></td></tr>
                        <tr><td style="font-size:xx-small"><a href="?T=RESET">Reset your password</a></td></tr></table></td></tr>
                    <tr><td></td><td><dx:ASPxButton ID="btnLogin" TabIndex=3 OnClick="btnLogin_Click" Text="Submit" CommandName="Submit" runat="server"></dx:ASPxButton></td></tr>
                <% End If%>
            </table>
        </td>
    </tr></table>
    <h2>About the <%=vCompanyName%> Intranet</h2>
    <%  vbGetWebContent("About Intranet")%>
</asp:Content>
