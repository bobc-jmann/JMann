<%@ Page Title="Update Prices" Language="VB" Theme="Youthful" AutoEventWireup="true" CodeFile="NMUpdatePrices.aspx.vb" Inherits="NMUpdatePrices" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
	<script type="text/javascript">
		function ButtonUpdatePrices(id) {
			if (id.value == "Processing...")
				return;	// prevent repeated button pushing
			id.value = "Processing...";
		}
	</script>
</head>

<body>
    <form id="Form1" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
		</asp:ScriptManager>
 		<dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
		<div style="position:relative;top:0;left:0;">
			<table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
				<tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
					<td>
						<dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" 
							Text="Update Prices" runat="server"></dx:ASPxLabel>
					</td>
				</tr>
			</table>
		</div>
		<br />
		<br />
		<br />
		<div>
		<dx:ASPxFormLayout ID="flLP" runat="server" ColCount="4" 
			EnableTheming="True" Theme="Youthful" Width="20%">
			<Items>
				<dx:LayoutItem ColSpan="2" ShowCaption="False">
					<LayoutItemNestedControlCollection>
						<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
							<asp:Button ID="btnUpdatePrices" Text="Update Prices" runat="server" 
								OnClientClick="ButtonUpdatePrices(this);" />								
						</dx:LayoutItemNestedControlContainer>
					</LayoutItemNestedControlCollection>
				</dx:LayoutItem>
			</Items>
		</dx:ASPxFormLayout>
		<dx:ASPxLabel runat="server" ID="errorMessageLabel" Visible="false" ForeColor="Red" EnableViewState="false" Font-Size="Medium" />
		</div>

		<script type="text/javascript">
			var __oldDoPostBack = __doPostBack;
		 	__doPostBack = CatchExplorerError;

		 	function CatchExplorerError (eventTarget, eventArgument)
		 	{
		 		try
		 		{
		 			return __oldDoPostBack (eventTarget, eventArgument);
		 		} 
		 		catch (ex)
		 		{
		 			// don't want to mask a genuine error
		 			// lets just restrict this to our 'Unspecified' one
		 			if (ex.message.indexOf('Unspecified') == -1)
		 			{
		 				throw ex;
		 			}
		 		}
		 	}
		</script>
    </form>
</body>
</html>
