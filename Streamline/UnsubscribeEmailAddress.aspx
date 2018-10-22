<%@ Page Language="VB" Theme="DevEx"  AutoEventWireup="false" CodeFile="UnsubscribeEmailAddress.aspx.vb" Inherits="UnsubscribeEmailAddress" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Streamline.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" runat="server" id="lnkCSS" />
    <style>
        body {
            font-family: helvetica,Arial,'Trebuchet MS';
        }
		div {
			position : absolute;    
			width    : 600px;
			height   : 50px;
			left     : 50%;
			top      : 50%;
			text-align: center;
			font-size:large;
			margin-left : -300px; /* half of the width  */
			margin-top  : -25px; /* half of the height */
		}
    </style>
</head>
<body>
    <div id="content" runat="server" />
</body>
</html>
