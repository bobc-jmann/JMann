<%@ Page Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="ChangeMailPref.aspx.vb" Inherits="ChangeMailPref" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Mailing Preference</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Streamline.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" runat="server" id="lnkCSS" />
    <script src="/Scripts/__AppJavascript.js"></script>
    <script>
        var isClicked = false;

        function jsSetFlag(s, e) {
            isClicked = true;
        }
        function jsProcessRow(s, e) {
            if (!isClicked) { return; }
            s.GetRowValues(s.GetFocusedRowIndex(), 'StreetAddress;City;State;ZIP;AddressID;MailingType', gridProcessValue);
            pnlAddresses.SetVisible(false);
            if (typeof (pnlAddrOK) != "undefined") { pnlAddrOK.SetVisible(true); }
            if (typeof (pnlButtons) != "undefined") { pnlButtons.SetVisible(false); }
        }
        function gridProcessValue(value) {
            if (value != null) {
                var myvar = value;
                var myarray = [];
                for (var l = 0; l < myvar.length; l++) {
                    if (l == 0) { txtStreetAddr.SetValue(myvar[l]); }
                    if (l == 1) { txtCity.SetValue(myvar[l]); }
                    if (l == 2) { txtState.SetValue(myvar[l]); }
                    if (l == 3) { txtZip.SetValue(myvar[l]); }
                    if (l == 4) { AddressID.SetValue(myvar[l]); }
                    if (l == 5) { cmbMailingType.SetValue(myvar[l]); }
                }
            }
        }
    </script>
    <style>
        body {
            font-family: helvetica,Arial,'Trebuchet MS';
        }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table runat="server" id="tblMain" align="center" cellpadding="3" style="border:0px solid gray">
        <tr>
            <td align="left">
                <center><span class="lblBigTitle">
                    Change Your Mailing Preference
                </span></center>
                <hr />
                <br />
                <center><div id="MainCaption" runat="server" class="lblMainCaption">Enter all or part of your address and press Search Addresses.</div></center>
                <br />
                <table align="center" border="0" cellpadding="0" cellspacing="1">
                    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Border-BorderStyle="None" Border-BorderColor="Silver">
                        <PanelCollection>
                            <dx:PanelContent>
                                <%--  
                                    Enter the reference number you've received and hit Enter or <br />put in all or part of your address and press Search Addresses
                                <tr>
                                    <td nowrap align="right">
                                        <span class="boxLabel">Reference Number:</span>
                                    </td>
                                    <td class="boxFrame" >
                                        <dx:aspxtextbox ClientVisible="false" ID="txtReferenceNbr" MaskSettings-Mask="\d+(\R.\d{0})?" Font-Size="Large" Height="20" Border-BorderStyle="None" runat="server" OnValueChanged="vbCheckRefNbr" AutoPostBack="true" Size="25">
                                            <MaskSettings Mask="9999999999" IncludeLiterals="None" PromptChar=" "></MaskSettings><Border BorderStyle="None"></Border>
                                        </dx:aspxtextbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td><span class="lblLightCaption">- or -</span></td>
                                </tr>
                                 --%>
                                <tr>
                                    <td align="right">
                                        <dx:aspxtextbox ClientVisible="false" ID="txtReferenceNbr" MaskSettings-Mask="\d+(\R.\d{0})?" Font-Size="Large" Height="20" Border-BorderStyle="None" runat="server" OnValueChanged="vbCheckRefNbr" AutoPostBack="true" Size="25">
                                            <MaskSettings Mask="9999999999" IncludeLiterals="None" PromptChar=" "></MaskSettings><Border BorderStyle="None"></Border>
                                        </dx:aspxtextbox>
                                        <span class="boxLabel">Street Address:</span>
                                    </td>
                                    <td class="boxFrame" >
                                        <dx:aspxtextbox ID="txtStreetAddr" ClientInstanceName="txtStreetAddr" Font-Size="Large" Width="200" Height="27" Border-BorderStyle="None" runat="server">
                                        </dx:aspxtextbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap>
                                        <span class="boxLabel">City / State / Zip:</span>
                                    </td>
                                    <td style="height:30px">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="123" class="boxFrame">
                                                    <dx:aspxtextbox ClientInstanceName="txtCity" ID="txtCity" Font-Size="Large" Height="27" width="180" Border-BorderStyle="None" runat="server">
                                                    </dx:aspxtextbox>
                                                </td>
                                                <td width="43" class="boxFrame">
                                                    <dx:aspxtextbox ClientInstanceName="txtState" ID="txtState" Font-Size="Large" Height="27" width="50" Border-BorderStyle="None" runat="server">
                                                    </dx:aspxtextbox>
                                                </td>
                                                <td width="58" class="boxFrame">
                                                    <dx:aspxtextbox ClientInstanceName="txtZip" ID="txtZip" Font-Size="Large" Height="27" Width="150" Border-BorderStyle="None" runat="server" ValidationSettings-Display="None">
                                                        <MaskSettings Mask="9999999999999999999999999999999999" ErrorText="Error in Zip Code" PromptChar=" " />
                                                    </dx:aspxtextbox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <dx:ASPxPanel runat="server" id="pnlButtons" ClientInstanceName="pnlButtons" ClientVisible="true">
                                            <PanelCollection>
                                                <dx:PanelContent>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxButton runat="server" ID="btnSearchAddr" OnClick="vbSearchAddr" Font-Size="Large" Text="Search Addresses"></dx:ASPxButton>
                                                            </td>
                                                            <td colspan="2">
                                                                <dx:ASPxButton runat="server" ID="btnClearForm" OnClick="vbClearForm" Font-Size="Large" Text="Clear Form"></dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxPanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <dx:ASPxPanel runat="server" id="pnlAddresses" ClientInstanceName="pnlAddresses" ClientVisible="false">
                                            <PanelCollection>
                                                <dx:PanelContent>
                                                    <dx:ASPxLabel ID="lblMessage" runat="server" Font-Size="12pt" Text="Click on an address to select.  If your address is not in this list, enter more of your address and search again."></dx:ASPxLabel>
                                                    <asp:SqlDataSource ID="sqlAddresses" SelectCommand="" runat="server" ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>"></asp:SqlDataSource>
                                                    <dx:ASPxGridView ID="gridMain" OnDataBound="vbOnDataBound" EnableCallBacks="true" ClientInstanceName="gridMain" runat="server" DataSourceID="sqlAddresses" Width="100%" KeyFieldName="AddressID">
                                                        <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="False" />
                                                        <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="false" />
                                                        <SettingsPager PageSize="4"></SettingsPager>
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="StreetAddress" />
                                                            <dx:GridViewDataTextColumn FieldName="City" />
                                                            <dx:GridViewDataTextColumn FieldName="State" Width="50px" />
                                                            <dx:GridViewDataTextColumn FieldName="ZIP" Width="500px" />
                                                            <dx:GridViewDataTextColumn FieldName="AddressID" Visible="false"/>
                                                            <dx:GridViewDataTextColumn FieldName="MailingType" Visible="false" />
                                                        </Columns>
                                                        <ClientSideEvents RowClick="jsSetFlag" FocusedRowChanged="jsProcessRow" />
                                                    </dx:ASPxGridView>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxPanel>
                                    </td>
                                </tr>
                                    <dx:ASPxPanel runat="server" id="pnlAddrOK" ClientInstanceName="pnlAddrOK" ClientVisible="false">
                                        <PanelCollection>
                                            <dx:PanelContent>
                                <tr>
                                    <td colspan="2"><hr /></td>
                                </tr>
                                <tr>
                                                <td align="right" nowrap>
                                                    <span class="boxLabel">Mailing Preference:</span>
                                                </td>
                                                <td class="boxFrame">
                                                    <dx:aspxcombobox ID="cmbMailingType" Font-Size="Large" Height="27" 
														Border-BorderStyle="None" ClientInstanceName="cmbMailingType" width="250" runat="server">
                                                        <Items>
                                                            <dx:ListEditItem Text="Email" Value="Email" />
                                                            <dx:ListEditItem Text="US Mail" Value="US Mail" />
                                                            <dx:ListEditItem Text="US Mail & Email" Value="US Mail & Email" />
                                                        </Items>
                                                    </dx:aspxcombobox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" nowrap>
                                                    <span class="boxLabel">Enter Email:</span>
                                                </td>
                                                <td class="boxFrame">
                                                    <dx:aspxtextbox ID="txtEmail" ClientInstanceName="txtEmail" Font-Size="Large" Height="27" Border-BorderStyle="None" width="250" runat="server">
                                                        <ValidationSettings ErrorText="!" SetFocusOnError="true" ErrorImage-Url="~/Resources/Images/iconError.png" RegularExpression-ErrorText="Invalid e-mail" RegularExpression-ValidationExpression="[ ]*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*[ ]*"><ErrorImage Url="~/Resources/Images/iconError.png"></ErrorImage>
                                                            <RegularExpression ErrorText="!" ValidationExpression="[ ]*\w+([-+.&#39;]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*[ ]*"></RegularExpression>
                                                        </ValidationSettings>
                                                    </dx:aspxtextbox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" nowrap>
                                                    <span class="boxLabel">Confirm Email:</span>
                                                </td>
                                                <td class="boxFrame">
                                                    <dx:aspxtextbox ID="txtConfirmEmail" Font-Size="Large" Height="27" Border-BorderStyle="None" width="250" runat="server">
                                                        <ValidationSettings ErrorText="!" SetFocusOnError="true" ErrorImage-Url="~/Resources/Images/iconError.png" RegularExpression-ErrorText="Invalid e-mail" RegularExpression-ValidationExpression="[ ]*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*[ ]*"><ErrorImage Url="~/Resources/Images/iconError.png"></ErrorImage>
                                                            <RegularExpression ErrorText="!" ValidationExpression="[ ]*\w+([-+.&#39;]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*[ ]*"></RegularExpression>
                                                        </ValidationSettings>
                                                        <clientsideevents Validation="function(s, e) { var vEM1 = txtEmail.GetText(); var vEM2 = s.GetText(); e.isValid = (vEM1.toUpperCase().trim()  == vEM2.toUpperCase().trim() );}" />
                                                    </dx:aspxtextbox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <dx:ASPxButton runat="server" ID="btnSaveMailType" Font-Size="Large" Text="Save Mailing Preference" OnClick="vbSaveMailType">
                                                        <ClientSideEvents Click="function(s, e) { m=cmbMailingType.GetValue(); if (m=='US Mail') { e.processOnServer=true; return; } v=txtEmail.GetValue(); if (v==null) { alert('Please enter your email address.'); e.processOnServer=false; } else { e.processOnServer=true;} }"></ClientSideEvents>
                                                    </dx:ASPxButton>
                                                    <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadingPanel" Modal="True"></dx:ASPxLoadingPanel>
                                                </td>
                                            </tr>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxPanel>
                                <dx:aspxtextbox ID="AddressID" ClientInstanceName="AddressID" ClientVisible="false" runat="server"></dx:aspxtextbox>
                        </dx:PanelContent>
                    </PanelCollection>
                    <Border BorderColor="Silver" BorderStyle="None"></Border>
                </dx:ASPxPanel>
                </table>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
