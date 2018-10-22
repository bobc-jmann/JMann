<%@ Page Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="EmailSignup.aspx.vb" Inherits="EmailSignup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Email Signup</title>
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
    <table runat="server" id="tblMain" align="center" cellpadding="10" style="border:0px solid gray">
        <tr>
            <td align="left">
                <center><span class="lblBigTitle">
                    Email Signup
                </span></center>
                <hr />
                <br />
                <asp:Multiview ID="mvEmail" runat="server" ActiveViewIndex="0">
                    <asp:view ID="mvEmail_1" runat="server">
                        <center><div id="MainCaption" runat="server" class="lblMainCaption">Please ensure that your email address is correctly entered<br />and then give us your mailing address.</br> We will never share this information with anyone else.</div></center>
                        <br />
                        <dx:ASPxPanel ID="ASPxPanel1" runat="server" Border-BorderStyle="None" Border-BorderColor="Silver">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <table align="center" border="0" cellpadding="0">
                                        <tr>
                                            <td align="right">
                                                <span class="boxLabel">Enter Email:</span>
                                            </td>
                                            <td class="boxFrame">
                                                <dx:aspxtextbox ID="txtEmail" ClientInstanceName="txtEmail" Font-Size="Large" Height="27" Border-BorderStyle="None" width="250" runat="server">
                                                    <ValidationSettings ErrorText="!" RequiredField-IsRequired="true" ErrorFrameStyle-Font-Size="Small" ErrorFrameStyle-ErrorTextPaddings-Padding="0" RequiredField-ErrorText="Required" SetFocusOnError="true" ErrorImage-Url="~/Resources/Images/iconError.png" 
                                                        RegularExpression-ErrorText="Invalid e-mail" 
                                                        RegularExpression-ValidationExpression="[ ]*[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?[ ]*">
                                                    </ValidationSettings>
                                                </dx:aspxtextbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span class="boxLabel">Confirm Email:</span>
                                            </td>
                                            <td class="boxFrame">
                                                <dx:aspxtextbox ID="txtConfirmEmail" Font-Size="Large" Height="27" Border-BorderStyle="None" width="250" runat="server">
                                                    <ValidationSettings ErrorText="!" RequiredField-IsRequired="true" ErrorFrameStyle-Font-Size="Small" ErrorFrameStyle-ErrorTextPaddings-Padding="0" RequiredField-ErrorText="Required" SetFocusOnError="true" ErrorImage-Url="~/Resources/Images/iconError.png" 
                                                        RegularExpression-ErrorText="Invalid e-mail" 
                                                        RegularExpression-ValidationExpression="[ ]*[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?[ ]*">
                                                    </ValidationSettings>
                                                    <clientsideevents Validation="function(s, e) { var vEM1 = txtEmail.GetText(); var vEM2 = s.GetText(); e.isValid = (vEM1.toUpperCase().trim()  == vEM2.toUpperCase().trim() );}" />
                                                </dx:aspxtextbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span class="boxLabel">Street Address:</span>
                                            </td>
                                            <td class="boxFrame" >
                                                <dx:aspxtextbox ID="txtStreetAddr" ClientInstanceName="txtStreetAddr" Font-Size="Large" Width="300" Height="27" Border-BorderStyle="None" runat="server">
                                                    <ValidationSettings ErrorText="!" RequiredField-IsRequired="true" ErrorFrameStyle-Font-Size="Small" ErrorFrameStyle-ErrorTextPaddings-Padding="0" RequiredField-ErrorText="Required" SetFocusOnError="true" ErrorImage-Url="~/Resources/Images/iconError.png" ></ValidationSettings>
                                                </dx:aspxtextbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" nowrap>
                                                <span class="boxLabel">City / State / Zip:</span>
                                            </td>
                                            <td style="height:30px;" align="left">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="153" class="boxFrame">
                                                            <dx:aspxtextbox ClientInstanceName="txtCity" ID="txtCity" Font-Size="Large" Height="27" width="200" Border-BorderStyle="None" runat="server">
                                                                <ValidationSettings ErrorText="!" RequiredField-IsRequired="true" ErrorFrameStyle-Font-Size="Small" ErrorFrameStyle-ErrorTextPaddings-Padding="0" RequiredField-ErrorText="Required" SetFocusOnError="true" ErrorImage-Url="~/Resources/Images/iconError.png"></ValidationSettings>
                                                            </dx:aspxtextbox>
                                                        </td>
                                                        <td width="43" class="boxFrame">
                                                            <dx:aspxtextbox ClientInstanceName="txtState" ID="txtState" Font-Size="Large" Height="27" width="50" Border-BorderStyle="None" runat="server">
                                                            </dx:aspxtextbox>
                                                        </td>
                                                        <td width="78" class="boxFrame">
                                                            <dx:aspxtextbox ClientInstanceName="txtZip" ID="txtZip" MaskSettings-PromptChar=" " MaskSettings-Mask="99999" Font-Size="Large" Height="27" Width="80" Border-BorderStyle="None" runat="server">
                                                                <MaskSettings Mask="9999999999999999999999999999999999" ErrorText="Error in Zip Code" PromptChar=" " />
                                                                <ValidationSettings ErrorText="!" RequiredField-IsRequired="true" ErrorFrameStyle-Font-Size="Small" ErrorFrameStyle-ErrorTextPaddings-Padding="0" RequiredField-ErrorText="*" SetFocusOnError="true"></ValidationSettings>
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
                                                                        <dx:ASPxButton runat="server" ID="btnSearchAddr" CausesValidation="true" OnClick="vbSearchAddr" Font-Size="Large" Text="Save Address"></dx:ASPxButton>
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
                                    </table>
                                    <br />
                                    <br />
                                    <table id="addressNotFound" runat="server" align="center">
                                        <tr>
                                            <td align="center"><span class="boxExplain"><asp:TextBox width="400" ID="boxExplain" runat="server"></asp:TextBox></span>
                                                <br /><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <span class="lblLightCaption">Please try again or call <asp:Literal runat="server" ID="litPhoneNbr"></asp:Literal> for assistance.</span>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                    </asp:view>
                    <asp:view ID="mvEmail_OK" runat="server">
                        <table align="center">
                        <tr>
                            <td align="center"><span class="boxExplain">Email registered.</span>
                                <br /><br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" colspan=4 align="center">
                                <span class="lblLightCaption">Thank you for signing up with us! We'll now send email reminders to *email address* before our donation pick up trucks will be on your street.</span>
                            </td>
                        </tr>
                        </table>
                    </asp:view>
                </asp:Multiview>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
