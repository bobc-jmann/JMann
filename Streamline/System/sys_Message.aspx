<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sys_Message.aspx.vb" Inherits="sys_Message" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Message</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="~/Scripts/__AppJavascript.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center">
            <tr>
                <td align="center">
                    <% If False and Not ismt(vAppLogo) Then%>
                        <a href="../default.aspx"><img id="img_name" border=0 src="<%: ResolveUrl("~/resources/images/" & vAppLogo)%>" alt="<%=vCompanyName %>" /></a>
                    <h2><%=vCompanyName %>Message</h2>
                    <% end if %>
                    <hr />
                    <br />
                    <dx:aspxlabel Font-Bold="true" Font-Size="Large" ID="ph_Message1" runat="server"></dx:aspxlabel>
                    <br />
                    <dx:aspxlabel Font-Bold="true" Font-Size="Large" ID="ph_Message2" runat="server"></dx:aspxlabel>
                    <br />
                    <dx:aspxlabel Font-Bold="true" Font-Size="Large" ID="ph_Message3" runat="server"></dx:aspxlabel>
                    <br />
                    <dx:aspxlabel Font-Bold="true" Font-Size="Large" ID="ph_Message4" runat="server"></dx:aspxlabel>
                    <br />
                    <dx:aspxlabel Font-Bold="true" Font-Size="Large" ID="ph_Message5" runat="server"></dx:aspxlabel>
                    <br />
                    <dx:aspxlabel Font-Bold="true" Font-Size="Large" ID="ph_Message6" runat="server"></dx:aspxlabel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
