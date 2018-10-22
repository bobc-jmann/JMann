<%@ Page Language="VB" Theme="DevEx"  AutoEventWireup="false" CodeFile="WebSpecials.aspx.vb" Inherits="WebSpecials" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Streamline.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" runat="server" id="lnkCSS" />
    <script>
        var ck_email = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([A-Za-z]{2,6}(?:\.[a-z]{2})?)$/i
 
        function jsValidateForm(s, e) {
            if (typeof txtPhone != 'undefined') {
                var phone = document.getElementById('PhoneToCall').value;
                if (txtPhone.GetValue() == null)
                {
                    lblError.SetText("Phone and Email are required. Please call " + phone + " if you do not have both.");
                    lblError.SetVisible(true); e.processOnServer = false;
                    return;
                }
            }
            if (typeof txtEmail != 'undefined') {
                if (txtEmail.GetValue() == null)
                {
                    lblError.SetText("Phone and Email are required. Please call " + phone + " if you do not have both.");
                    lblError.SetVisible(true);
                    e.processOnServer = false;
                    return;
                }
                var email = (txtEmail.GetValue().trim());
                if (!ck_email.test(email)) {
                    lblError.SetText("Please enter a valid email address.");
                    lblError.SetVisible(true);
                    e.processOnServer = false;
                    return;
                }

            }

            if (typeof txtStreet != 'undefined') { if (txtStreet.GetValue() == null) { lblError.SetText("Street Address is required."); lblError.SetVisible(true); e.processOnServer = false; return; } }
            if (typeof txtStreet != 'undefined') {
                if (txtStreet.GetValue() != null) {
                    x = txtStreet.GetValue();
                    x = x.substr(0, 1);
                    if (isNaN(Number(x))) {
                        lblError.SetText("Street Address must start with a number.");
                        lblError.SetVisible(true);
                        e.processOnServer = false;
                        return;
                    }
                }
            }
            if (typeof txtCity != 'undefined') { if (txtCity.GetValue() == null) { lblError.SetText("City is required."); lblError.SetVisible(true); e.processOnServer = false; return; } }
            if (typeof txtZip != 'undefined') {
                if (txtZip.GetValue() == null) {
                    lblError.SetText("Zip Code is required.");
                    lblError.SetVisible(true);
                    e.processOnServer = false;
                    return;
                }
                if (txtZip.GetValue() > 99999) {
                    lblError.SetText("Please enter a 5-digit Zip Code.");
                    lblError.SetVisible(true);
                    e.processOnServer = false;
                    return;
                }
            }
            if (typeof txtFirstName != 'undefined') { if ((txtFirstName.GetValue() == null) && (txtFirstName.GetValue() == null)) { lblError.SetText("Either First or Last Name are required."); lblError.SetVisible(true); e.processOnServer = false; return; } }
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
        <asp:HiddenField ID="hfZipDebug" runat="server"></asp:HiddenField>

        <asp:HiddenField ID="hfPageLogID" runat="server"></asp:HiddenField>

        <asp:HiddenField ID="qsEmailAddress" runat="server" />
        <asp:HiddenField ID="qsAddressID" runat="server" />
        <asp:HiddenField ID="qsPickupScheduleID" runat="server" />
		<asp:HiddenField ID="qsSource" runat="server" />
		<asp:HiddenField ID="qsPickupDate" runat="server" />

        <asp:HiddenField ID="PhoneToCall" runat="server" />
        <table align="center" border="0">
            <tr>
                <td>
                    <table width="585px" runat="server" border="0" id="tblMain" style="margin-left:5px; margin-right:5px;" >
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <span class="lblBigTitle">
                                                Schedule a Pickup:
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="lblMainCaption">
                                                Scheduling a donation pickup is as easy as 1-2-3!
                                            </span> 
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" border="0" width="260px">
                                    <tr>
                                        <td style="height: 30px; font-size: 13pt; font-weight:bold; color:Black;  ">
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td id="cProgress_tdStep1" align="center">
                                                        Step <span id="cProgress_lblStep1" runat="server" class="boxSteps">1</span>
                                                    </td>
                                                    <td id="cProgress_tdStep2" align="center">
                                                        Step <span id="cProgress_lblStep2" runat="server" class="boxSteps">2</span>
                                                    </td>
                                                    <td id="cProgress_tdStep3" align="center">
                                                        Step <span id="cProgress_lblStep3" runat="server" class="boxSteps">3</span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr> 
                    </table> 
                    <hr class="clsHR"/>
                    <br />
                    <div class="boxExplain">
                        <span runat="server" id="spnExplain" >Please enter your email address and phone number contact information:</span>
                    </div>
                    <br />
                    <table cellpadding="0" cellspacing="5" border="0" width="100%" align="center">
                        <asp:Multiview ID="mvPickups" runat="server" ActiveViewIndex="0">
                            <asp:view ID="EmailPhone" runat="server">
                                <tr>
                                    <td>
                                        <table width="70%" align="center" border="0">
                                            <tr>
                                                <td align="right">
                                                    <span class="boxLabel">Email:</span>
                                                </td>
                                                <td class="boxFrame">
                                                    <dx:aspxtextbox ID="txtEmail" Font-Size="Large" Width="500" Height="27" ClientInstanceName="txtEmail" runat="server" 
														Border-BorderStyle="None">
                                                        <ValidationSettings ErrorText="A valid email is required" ErrorDisplayMode="None" RequiredField-IsRequired="true" 
                                                            SetFocusOnError="true" ErrorFrameStyle-Font-Size="X-Small" 
                                                            ErrorImage-Url="~/Resources/Images/iconError.png" 
                                                            RegularExpression-ErrorText="Invalid email" 
                                                            RegularExpression-ValidationExpression="[ ]*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*[ ]*" 
                                                            RequiredField-ErrorText="A valid email is required">
                                                        </ValidationSettings>
                                                    </dx:aspxtextbox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="boxLabel">Phone:</span>
                                                </td>
                                                <td class="boxFrame">
                                                    <dx:aspxtextbox ID="txtPhone" Font-Size="Large" Width="500" Height="27" ClientInstanceName="txtPhone" runat="server" 
                                                        Border-BorderStyle="None" >
                                                        <MaskSettings Mask="(999) 000-0000 x999" IncludeLiterals="None" PromptChar=" " ErrorText="A valid phone is required" />
                                                        <ValidationSettings ErrorText="A valid phone is required" ErrorDisplayMode="None" RequiredField-IsRequired="true" 
                                                            SetFocusOnError="true" ErrorFrameStyle-Font-Size="X-Small" 
                                                            ErrorImage-Url="~/Resources/Images/iconError.png" 
                                                            RequiredField-ErrorText="A valid phone is required">
                                                        </ValidationSettings>
                                                        <ClientSideEvents GotFocus=
                                                            "function(s,e) {
                                                                window.setTimeout( function() {
                                                                    s.SetCaretPosition(1);
                                                                }, 0);
                                                             }" />
                                                    </dx:aspxtextbox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:view>
                            <asp:view ID="Address" runat="server">
                                <tr>
                                    <td align="right">
                                        <span class="boxLabel">Email:</span>
                                    </td>
                                    <td class="lblBlack">
                                        <dx:ASPxLabel cssclass="lblReadOnly" runat="server" ID="lblEmail"></dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="boxLabel">Phone:</span>
                                    </td>
                                    <td class="lblBlack">
                                        <dx:ASPxLabel cssclass="lblReadOnly" runat="server" ID="lblPhone" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="boxLabel">First Name:</span>
                                    </td>
                                    <td class="boxFrame">
                                        <dx:aspxtextbox ID="txtFirstName" Font-Size="Large" ClientInstanceName="txtFirstName" width="350" Height="27" runat="server" Border-BorderStyle="None"></dx:aspxtextbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="boxLabel">Last Name:</span>
                                    </td>
                                    <td class="boxFrame">
                                        <dx:aspxtextbox ID="txtLastName" Font-Size="Large" ClientInstanceName="txtLastName" width="350" Height="27" runat="server" Border-BorderStyle="None"></dx:aspxtextbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="boxLabel">Street:</span>
                                    </td>
                                    <td class="boxFrame">
                                        <dx:aspxtextbox ID="txtStreet" Font-Size="Large" ClientInstanceName="txtStreet" width="350" Height="27" runat="server" Border-BorderStyle="None"></dx:aspxtextbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="boxLabel">Apartment/Suite:</span>
                                    </td>
                                    <td>
                                        <table border="0" width="100%">
                                            <tr>
                                                <td class="boxFrame">
                                                    <dx:aspxtextbox ID="txtAptSuite" Font-Size="Large" ClientInstanceName="txtAptSuite" width="100" Height="27" runat="server" Border-BorderStyle="None"></dx:aspxtextbox>
                                                </td>
                                                <td width="50%"></td>
                                                <td align="right">
                                                    <span class="boxLabel">City:</span>
                                                </td>
                                                <td class="boxFrame">
                                                    <dx:aspxtextbox ID="txtCity" Font-Size="Large" ClientInstanceName="txtCity" width="217" Height="27" runat="server" Border-BorderStyle="None"></dx:aspxtextbox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="boxLabel">State:</span>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td class="boxFrame">
                                                    <dx:aspxtextbox ID="txtState" Font-Size="Large" ReadOnly="true" ClientInstanceName="txtState" width="30" Height="27" runat="server" Border-BorderStyle="None"></dx:aspxtextbox>
                                                </td>
                                                <td width="50%"></td>
                                                <td align="right">
                                                    <span class="boxLabel">ZipCode:</span>
                                                </td>
                                                <td class="boxFrame" width="200">
                                                    <dx:aspxtextbox ID="txtZip" Font-Size="Large" ClientInstanceName="txtZip" width="170" Height="27" runat="server" 
														Border-BorderStyle="None">
                                                        <ValidationSettings ErrorFrameStyle-Font-Size="X-Small" ErrorDisplayMode="None" ></ValidationSettings>
                                                        <MaskSettings Mask="00000" ErrorText="5-digit Zip Code is required" PromptChar=" " />
                                                        <ClientSideEvents GotFocus=
                                                            "function(s,e) {
                                                                window.setTimeout( function() {
                                                                    s.SetCaretPosition(0);
                                                                }, 0);
                                                             }" />

                                                    </dx:aspxtextbox>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="hfAddressID" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hfLocation" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hfCharityID" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hfCharityAbbr" runat="server"></asp:HiddenField>
                                    </td>
                                </tr>
                            </asp:view>
                            <asp:view ID="CheckItems" runat="server">
                                <tr><td colspan="2" align="center" class="lblFurnitureCaption">Sorry, we cannot accept furniture or large appliances</td></tr>
                                <asp:SqlDataSource runat="server" ID="sqlWebItems" ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>" SelectCommand="select * from tlkSpecialsWebItems order by SortOrder"></asp:SqlDataSource>
                                <tr>
                                    <td align="center" colspan="2">
                                        <dx:ASPxButton AutoPostBack="false" CausesValidation="true" CssClass="none" OnClick="vbNextPage" Border-BorderStyle="None" Border-BorderColor="White" BackColor="white" runat="server" ID="btnContinueTop">
                                            <Image Url="~/resources/images/btnContinue-OrangeGrey.jpg"></Image>
                                            <Border BorderStyle="None" BorderWidth="0" />
                                            <FocusRectPaddings PaddingBottom="0" Padding="0" />
                                            <ClientSideEvents Click="function(s,e) { jsValidateForm(s,e); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td align="center" colspan="2">
                                        <table align="center" width="50%" >
                                            <asp:Repeater ID="rptWebItems" runat="server" DataSourceID="sqlWebItems">
                                                <ItemTemplate>
                                                    <tr><td align="left" nowrap><dx:AspxCheckBox ID="chkWebItem" cssclass="lblReadOnly" runat="server" Text='<%# Container.DataItem("WebItem")%>'></dx:AspxCheckBox></td><td></td></tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </td>
                                </tr>
                            </asp:view>
                            <asp:view ID="ChooseDate" runat="server">
                                <table width="70%" align="center" border="0">
                                <tr>
                                    <td align="center" colspan="2">
                                        <dx:ASPxButton AutoPostBack="false" OnClick="vbSchedule" CausesValidation="true" 
											Visible="false" Border-BorderStyle="None" 
											Border-BorderColor="White" runat="server" 
											ID="btnScheduleTop" Paddings-PaddingBottom="12" BackColor="White">
                                            <Image Url="~/resources/images/btnScheduleNow-OrangeGrey.jpg"></Image>
                                            <Border BorderStyle="None" BorderWidth="0" />
                                            <FocusRectPaddings PaddingBottom="0" Padding="0" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <dx:ASPxCalendar Width="500" ID="calPickups" runat="server" Columns="2" AutoPostBack="true"  
                                            OnDayCellPrepared="vbOnDayCellPrepared" ShowTodayButton="false" ShowClearButton="false" ShowWeekNumbers="false" EnableMonthNavigation="False" EnableYearNavigation="False">
                                            <HeaderStyle ForeColor="white" BackColor="#EB6D00" Font-Bold="true" Font-Size="12pt" />
                                            <DaySelectedStyle Border-BorderStyle="Solid"></DaySelectedStyle>
                                        </dx:ASPxCalendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td>What is the approximate size of your donation?</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:SqlDataSource SelectCommand="select * from tlkSpecialsWebSizes order by SortOrder" runat="server" ID="sqlSizes" ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>"></asp:SqlDataSource>
                                        <dx:ASPxRadioButtonList SelectedIndex="0" runat="server" ID="rdoSizes" ValueField="SpecialsWebSize" TextField="SpecialsWebSize" RepeatDirection="Horizontal" DataSourceID="sqlSizes"></dx:ASPxRadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Where will you leave your donation for us to pick-up?</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:SqlDataSource SelectCommand="select * from tlkItemLocations order by SortCode" runat="server" ID="sqlLocations" ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>"></asp:SqlDataSource>
                                        <dx:ASPxRadioButtonList SelectedIndex="0" runat="server" ID="rdoLocations" ValueField="ItemLocation" TextField="ItemLocation" RepeatDirection="Horizontal" DataSourceID="sqlLocations"></dx:ASPxRadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="boxGraySmaller">
                                            <tr>
                                                <td>Special Instruction or Comments:</td>
                                            </tr>
                                            <tr>
                                                <td width="100">
                                                    <dx:ASPxMemo runat="server" ID="mmoComments" Rows="2" Columns="80"></dx:ASPxMemo>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                </table>
                            </asp:view>
                            <asp:view ID="PleaseCall" runat="server">
                                <tr>
                                    <td></td>
                                    <td width="100%" colspan=4 align="center">
                                        <span class="lblLightCaption">Please call <asp:Literal runat="server" ID="litPhoneNbr"></asp:Literal> to schedule a pickup.</span>
                                    </td>
                                </tr>
                            </asp:view>
                            <asp:view ID="Scheduled" runat="server">
                                <tr>
                                    <td></td>
                                    <td width="100%" colspan=4 align="center">
                                        <span class="lblMainCaption">Your pickup has been scheduled.</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%" colspan=4 align="center">
                                        <span class="lblMainCaption">It will be reviewed and you will receive a confirmation by email or phone.</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%" colspan=4 align="center">
                                        <span class="lblMainCaption">Thank you.</span>
                                    </td>
                                </tr>
                            </asp:view>
                            <asp:view ID="Apartment" runat="server">
                                <tr>
                                    <td></td>
                                    <td width="100%" colspan=4 align="center">
                                        <span class="lblLightCaption">We're sorry. Your address needs to be scheduled by calling <asp:Literal runat="server" ID="Literal1"></asp:Literal> to schedule a pickup.</span>
                                    </td>
                                </tr>
                            </asp:view>
                            <asp:View ID="Error" runat="server">
                            </asp:View>




                            <asp:view ID="qsAddress" runat="server">
                                <tr>
                                    <td align="right">
                                        <span class="boxLabel">Email:</span>
                                    </td>
                                    <td class="boxFrame">
                                        <dx:aspxtextbox ID="qsEmail" Font-Size="Large" Width="500" Height="27" ClientInstanceName="txtEmail" runat="server" Border-BorderStyle="None">
                                            <ValidationSettings ErrorText="!" ErrorDisplayMode="None" RequiredField-IsRequired="true" 
                                                SetFocusOnError="true" ErrorFrameStyle-Font-Size="X-Small" 
                                                ErrorImage-Url="~/Resources/Images/iconError.png" 
                                                RegularExpression-ErrorText="Invalid e-mail" 
                                                RegularExpression-ValidationExpression="[ ]*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*[ ]*">
                                            </ValidationSettings>
                                        </dx:aspxtextbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="boxLabel">Phone:</span>
                                    </td>
                                    <td class="boxFrame">
                                        <dx:aspxtextbox ID="qsPhone" Font-Size="Large" Width="500" Height="27" ClientInstanceName="txtPhone" runat="server" Border-BorderStyle="None">
                                            <MaskSettings Mask="(999) 000-0000 x999" IncludeLiterals="None" PromptChar=" " />
                                            <ValidationSettings ErrorText="!" ErrorDisplayMode="None" RequiredField-IsRequired="true" SetFocusOnError="true" ErrorFrameStyle-Font-Size="X-Small" ErrorImage-Url="~/Resources/Images/iconError.png"></ValidationSettings>
                                        </dx:aspxtextbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="boxLabel">First Name:</span>
                                    </td>
                                    <td class="boxFrame">
                                        <dx:aspxtextbox ID="qsFirstName" Font-Size="Large" ClientInstanceName="txtFirstName" width="350" Height="27" runat="server" Border-BorderStyle="None"></dx:aspxtextbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="boxLabel">Last Name:</span>
                                    </td>
                                    <td class="boxFrame">
                                        <dx:aspxtextbox ID="qsLastName" Font-Size="Large" ClientInstanceName="txtLastName" width="350" Height="27" runat="server" Border-BorderStyle="None"></dx:aspxtextbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="boxLabel">Street:</span>
                                    </td>
                                    <td class="boxFrame">
                                        <dx:aspxtextbox ID="qsStreet" Font-Size="Large" ReadOnly="true" ClientInstanceName="txtStreet" width="350" Height="27" runat="server" Border-BorderStyle="None"></dx:aspxtextbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="boxLabel">Apartment/Suite:</span>
                                    </td>
                                    <td>
                                        <table border="0" width="100%">
                                            <tr>
                                                <td class="boxFrame">
                                                    <dx:aspxtextbox ID="qsAptSuite" Font-Size="Large" ReadOnly="true" ClientInstanceName="txtAptSuite" width="100" Height="27" runat="server" Border-BorderStyle="None"></dx:aspxtextbox>
                                                </td>
                                                <td width="50%"></td>
                                                <td align="right">
                                                    <span class="boxLabel">City:</span>
                                                </td>
                                                <td class="boxFrame">
                                                    <dx:aspxtextbox ID="qsCity" Font-Size="Large" ReadOnly="true" ClientInstanceName="txtCity" width="217" Height="27" runat="server" Border-BorderStyle="None"></dx:aspxtextbox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="boxLabel">State:</span>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td class="boxFrame">
                                                    <dx:aspxtextbox ID="qsState" Font-Size="Large" ReadOnly="true" ClientInstanceName="txtState" width="30" Height="27" runat="server" Border-BorderStyle="None"></dx:aspxtextbox>
                                                </td>
                                                <td width="50%"></td>
                                                <td align="right">
                                                    <span class="boxLabel">ZipCode:</span>
                                                </td>
                                                <td class="boxFrame" width="200">
                                                    <dx:aspxtextbox ID="qsZip" Font-Size="Large" ReadOnly="true" ClientInstanceName="txtZip" width="170" Height="27" runat="server" 
														Border-BorderStyle="None">
                                                        <MaskSettings Mask="99999999999999999999" ErrorText="Please enter a 5-digit Zip Code" PromptChar=" " />
                                                    </dx:aspxtextbox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:view>
                            <asp:view ID="ConfirmConfirm" runat="server">
                            </asp:view>
                            <asp:view ID="ConfirmWithPhone" runat="server">
                                <table width="70%" align="center" border="0">
                                <tr>
                                    <td align="right">
                                        <span class="boxLabel">Phone:</span>
                                    </td>
                                    <td class="boxFrame">
                                        <dx:aspxtextbox ID="confirmPhone" Font-Size="Large" Width="500" Height="27" 
											runat="server" Border-BorderStyle="None">
                                            <MaskSettings Mask="(999) 000-0000 x999" IncludeLiterals="None" PromptChar=" " />
                                            <ValidationSettings ErrorText="!" ErrorDisplayMode="None" RequiredField-IsRequired="true" 
												SetFocusOnError="true" ErrorFrameStyle-Font-Size="X-Small" 
												ErrorImage-Url="~/Resources/Images/iconError.png"></ValidationSettings>
                                        </dx:aspxtextbox>
                                    </td>
								</tr>
                            </asp:view>
							<asp:view ID="ConfirmNoPhone" runat="server">
                            </asp:view>
                            <asp:view ID="ConfirmPhoneSaved" runat="server">
                                <tr>
                                    <td></td>
                                    <td width="100%" colspan=4 align="center">
                                        <span class="lblMainCaption">Your phone number has been saved. Thank you.</span>
                                    </td>
                                </tr>
                            </asp:view>

                        </asp:Multiview>
                        <tr>
                            <td colspan="2" align="center">
                                <dx:ASPxLabel runat="server" ID="lblError" ForeColor="Red" Font-Bold="true" Font-Size="Large" ClientInstanceName="lblError" ClientVisible="false"></dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <dx:ASPxButton AutoPostBack="false" CausesValidation="true" CssClass="none" 
									OnClick="vbNextPage" Border-BorderStyle="None" 
									Border-BorderColor="White" BackColor="white" runat="server" ID="btnContinue">
                                    <Image Url="~/resources/images/btnContinue-OrangeGrey.jpg"></Image>
                                    <Border BorderStyle="None" BorderWidth="0" />
                                    <FocusRectPaddings PaddingBottom="0" Padding="0" />
                                    <ClientSideEvents Click="function(s,e) { jsValidateForm(s,e); }" />
                                </dx:ASPxButton>
                                <dx:ASPxButton AutoPostBack="false" OnClick="vbSchedule" CausesValidation="true" 
									Visible="false" Border-BorderStyle="None" 
									Border-BorderColor="White" runat="server" ID="btnSchedule">
                                    <Image Url="~/resources/images/btnScheduleNow-OrangeGrey.jpg"></Image>
                                    <Border BorderStyle="None" BorderWidth="0" />
                                    <FocusRectPaddings PaddingBottom="0" Padding="0" />
                                </dx:ASPxButton>
                                <dx:ASPxButton AutoPostBack="false" Visible="false" CausesValidation="true" CssClass="boxFrame" 
									OnClick="btnConfirmPhone_Click" Border-BorderStyle="Solid" 
									Text="Save Phone Number"
									Border-BorderColor="#D7D7D7" BackColor="#e6e4e5" runat="server" 
									ID="btnConfirmPhone" Font-Size="12" Width="200" Height="40" 
									Border-BorderWidth="3" Font-Bold="True">
                                </dx:ASPxButton>
                                 <dx:ASPxButton AutoPostBack="false" Visible="false" CausesValidation="true" CssClass="boxFrame" 
									OnClick="btnConfirmConfirm_Click" Border-BorderStyle="Solid" 
									Text="Confirm Pickup"
									Border-BorderColor="#D7D7D7" BackColor="#e6e4e5" runat="server" 
									ID="btnConfirmConfirm" Font-Size="12" Width="200" Height="40" 
									Border-BorderWidth="3" Font-Bold="True">
                                </dx:ASPxButton>
                               <asp:HiddenField ID="txtPickupID" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr height="30">
                            <td style="text-align:center; width:800px;" >
                                <span runat="server" id="spnExplain2" visible="false" class="boxExplain">We're sorry, the application has generated an error which has been reported.</span>
                            </td>
                        </tr>
                        <tr height="30">
                            <td style="text-align:center; width:800px;" >
                                <span runat="server" id="spnExplain3" visible="false" class="boxExplain">Please try again later, send us an email, or call the number above. Thank you for your patience.</span>
                            </td>
                        </tr>

                    </table>
                    <br />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
