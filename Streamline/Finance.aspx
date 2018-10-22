<%@ Page Title="Finance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="Finance.aspx.vb" Inherits="Finance" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
	<style type="text/css">   
		html { height: 100% }   
		body { height: 100%; margin: 0px; padding: 0px; font-family: Arial; font-size: 12px; }  
        #tblAddressParameters tr:nth-child(odd) { background-color: #E0E0E0; border-color: #E0E0E0; border: 0px; margin: 0px; padding: 0px; border-collapse: collapse }
        #tblAddressParameters tr:nth-child(even) { background-color: #F0F0F0; border-color: #F0F0F0; Border: 0px; margin: 0px; padding: 0px;  border-collapse: collapse }
	    .parm1 { width: 40%; text-align: right; font-weight:bold }
	    .parm2 { width: 40%; text-align: right; font-weight:bold }
	    .parm3 { width: 20%; text-align: right; font-weight:bold }
	</style> 
	<script type="text/javascript">
        function jsCallHideLoadingPanel() {
            parent.jsHideLoadingPanel();
        }
	</script>
  	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script>
	    $(document).ready(function() {
	        var url = $('input[name=hfURL]').val();
	        var userName = $('input[name=hfUserName]').val();
	        var iframe = $('#finance');
	        $(iframe).attr('src', url + userName);
	    });
    </script>

</head>

<body>
    <form id="Form1" runat="server">
    <dx:ASPxGlobalEvents ID="glob" runat="server">
        <ClientSideEvents ControlsInitialized="function (s, e) { jsCallHideLoadingPanel(); }" />
    </dx:ASPxGlobalEvents>
    <div style="position:relative;top:0;left:0;">
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Finance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <asp:HiddenField ID="hfURL" runat="server" Value=""></asp:HiddenField>
        <asp:HiddenField ID="hfUserName" runat="server" Value=""></asp:HiddenField>
        <iframe ID="finance" src="" width="1200" height="800"></iframe>
    </div>
    </form>
</body>
</html>
