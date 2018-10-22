<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportServer.aspx.vb" Inherits="ReportServer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>
<meta http-equiv="X-UA-Compatible" content="IE=10" />
<html>
<head>
    <title>Streamline Reports</title>

      	<style type="text/css">   
		html { 
            height: 100% 

		}   
		body { 
            height: 100%; 
            width: 100%; 
            margin: 0px; 
            padding: 0px;  

		}  
	    #loading {
            display: table;
            position: absolute;
            height: 100%; 
            width: 100%; 
            margin: 0px; 
            padding: 0px; 
        }
  	    #l2 {
  	        display: table-cell;
  	        vertical-align: middle;
  	        text-align: center;
  	    }   
        #l3 {
            display: inline-block;
  	        border-style: solid;
  	        border-width: 1px;
  	        border-color: black;
            padding: 24px;
            background-color: #ece9d8;
        }
  	    #loadingLabel {
            color: black;
            font-family: Arial;
            font-weight: normal;
            font-size: 24px;
            padding-left: 10px;          
  	    }
  	    #loadingImage {
            background-color: #ece9d8;
  	    }
	</style> 

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            var height = $(window).height();
            $("#screenHeight").val(height);
            if ($("#reportDisplayed").val() != "True") {
                __doPostBack();
            }
        });

        function UpdateImg(ctrl) {
            var img = document.getElementById(ctrl);
            try {
                img.src = img.src;
            }
            catch (err) {
            }
        }

        setTimeout(function () { UpdateImg('loadingImage'); }, 50);
     </script>
</head>
<body>
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="loading" runat="server" visible="true">
            <div id="l2" runat="server">
                <div id="l3" runat="server">
                    <asp:Image ID="loadingImage"  runat="server" ImageUrl="~/Resources/images/ajax-loader.gif" />
                    <asp:Label ID="loadingLabel"  runat="server" Text="Loading..." />
                </div>
            </div>
        </div>
        <div>
            <asp:HiddenField ID="screenHeight" runat="server" />
            <asp:HiddenField ID="reportDisplayed" runat="server" />
            <rsweb:ReportViewer ID="rptViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
                Width="100%" AsyncRendering="False" ProcessingMode="Remote" Visible="False" />
        </div>
    </form>
</body>
</html>