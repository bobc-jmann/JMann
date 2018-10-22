<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Test.aspx.vb" Inherits="Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<dx:ASPxFileManager ID="ASPxFileManager1" runat="server" OnCustomThumbnail="fileManager_CustomThumbnail"
			SettingsEditing-AllowDownload="True" SettingsEditing-AllowCopy="True">
			<Settings RootFolder="~\BobsPortal\" ThumbnailFolder="~\Thumb\" />
			<SettingsUpload Enabled="false"></SettingsUpload>
   			<ClientSideEvents SelectedFileOpened="function(s,e) { 
					e.file.Download();
					e.processOnServer = false;
			}" />
		</dx:ASPxFileManager>
    </div>
    </form>
</body>
</html>
