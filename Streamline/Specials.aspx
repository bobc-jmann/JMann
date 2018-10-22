<%@ Page Title="Specials" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="Specials.aspx.vb" Inherits="Specials" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml"
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
	<style type="text/css">   
		html { height: 100% }   
		body { height: 100%; margin: 0px; padding: 0px; font-family: Arial; font-size: 12px; }  
	    .auto-style1 {
            width: 104px;
        }
        .auto-style2 {
            width: 161px;
        }
        .auto-style3 {
            width: 120px;
        }
	</style> 
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function OnRowClick(s, e) {
            _aspxClearSelection();
            s._selectAllRowsOnPage(false);
            s.SelectRow(e.visibleIndex, true);
        }
        function ToggleCalendar() {
            return false;
        }
 
        function OnConfirmInit(s, e) {
            var checked = ckConfirmed.GetChecked();
            var prevPickupIsToday = document.getElementById("hfPrevPickupIsToday").value;

            if (checked && prevPickupIsToday == "True")
                document.getElementById("confirmWrapper").style.display = "block";
            else
                document.getElementById("confirmWrapper").style.display = "none";
        }

        function OnConfirmValueChanged(s, e) {
        	var checked = ckConfirmed.GetChecked();
        	var prevPickupIsToday = document.getElementById("hfPrevPickupIsToday").value;

        	if (checked && prevPickupIsToday == "True")
        		document.getElementById("confirmWrapper").style.display = "block";
        	else
        		document.getElementById("confirmWrapper").style.display = "none";

        	OnDetailValueChanged(s, e);
        }

        function OnDetailValueChanged(s, e) {
        	try
        	{
        		var checkedConfirm = ckConfirmed.GetChecked();
        		var checkedMissed = ckMissed.GetChecked();
        		var checkedRedTagged = ckRedTagged.GetChecked();
        		var textDetailComments = txtDetailComments.GetValue();
        		if (textDetailComments == null)
					textDetailComments = ""

        		if (checkedConfirm || checkedMissed || checkedRedTagged || textDetailComments != "")
        		{
        			document.getElementById("btnScheduleDetailSave").style.borderColor = "red";
        		}
        		else
        		{
        			document.getElementById("btnScheduleDetailSave").style.borderColor = "gray";
        		}
        	}
        	catch (err)
        	{}
        }

        var ck_email = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i

        function CmdSaveValidate() {
            if (rbSpecialType.GetValue() !== 'PHONE' && rbSpecialType.GetValue() !== 'EMAIL' && rbSpecialType.GetValue() !== 'WEB' && rbSpecialType.GetValue() !== 'ROUTE') {
                alert('Specials must have a type (Phone, Email, Web, or Route).');
                return false;
            }

            if (dtPickupDate.GetDate() == null) {
                if (rbSpecialType.GetValue() == 'PHONE') {
                    alert('Phone Specials must have a Pickup Date.');
                    return false;
                }
                else {
                    if (document.getElementById('ddlStatus').value != ' ' && document.getElementById('ddlStatus').value != 'NOT REVIEWED') {
                        alert('Email and Web Specials must have a Pickup Date after they have been Scheduled.');
                        return false;
                    }
                }
            }
            else {
                var curTime = new Date();
                var curDate = curTime.getMonth() + 1 + "/" + curTime.getDate() + "/" + curTime.getFullYear(); //Todays Date
                var dd = DateDiff(dtPickupDate.GetDate(), new Date(curDate).getTime());
                if (document.getElementById('ddlStatus').value == 'SCHEDULED') {
                    if (dd < 0) {
                        alert('Specials cannot be scheduled in the past.');
                        return false;
                    }
                }
                if (dd > 15) {
                    if (!confirm('The Pickup Date is more than 15 days in the future. Are you sure you want to save it?')) {
                        return false;
                    }
                }
                if (document.getElementById('ddlStatus').value != 'SCHEDULED') {
                    if (!confirm('The Status is not SCHEDULED. Are you sure you want to save it?')) {
                        return false;
                    }
                }
            }

            var email = document.getElementById('txtEmail').value;
            if ((rbSpecialType.GetValue() == 'EMAIL') || (email !== '')) {
                if (!ck_email.test(email)) {
                    alert('Please enter a valid Email Address');
                    return false;
                }
            }

            function DateDiff(date1, date2) {
                var datediff = date1 - date2; //store the getTime diff - or +
                return (datediff / (24 * 60 * 60 * 1000)); //Convert values to -/+ days and return value      
            } 

            if (document.getElementById('ddlLocation').value == ' ') {
                alert('Please select a Location');
                return false;
            }
            if (document.getElementById('ddlCharity').value == ' ') {
                alert('Please select a Charity');
                return false;
            }

            var grade = document.getElementById('ddlGrade').value;
            var status = document.getElementById('ddlStatus').value;
            if ((grade.substring(0, 1) == 'D' || grade.substring(0, 1) == 'X') && status == 'SCHEDULED') {
                alert('When Grade is D or X, Status cannot be SCHEDULED.');
                return false;
            }
            if ((grade.substring(0, 1) == 'A' || grade.substring(0, 1) == 'B' || grade.substring(0, 1) == 'C') && status == 'SCHEDULED') {
                document.getElementById('ddlStatus').value = 'PICKED';
            }

            return true;
        }

        var prevIndex = -1;
        function toggle() {
            var list = window.event.srcElement;
            if (list) {
                if (list.selectedIndex == prevIndex) list.selectedIndex = -1;
                prevIndex = list.selectedIndex;
            }
        }

    </script>
</head>

<body>
    <script type="text/javascript">
        function DateChanged() {
            if (dtPickupDate.GetDate() == null) {
                return false;
            }
            if (dtPickupDate.GetDate() != null &&
                (document.getElementById('ddlStatus').value == ' ' || document.getElementById('ddlStatus').value == 'NOT REVIEWED')) {
                    document.getElementById('ddlStatus').value = 'SCHEDULED';
            }

            var pickupID = txtPickupID.GetText();
            var pickupDate = dtPickupDate.GetDate();
            var address = document.getElementById('txtAddress').value;
            var city = document.getElementById('txtCity').value;

            if (pickupID == '') {
                pickupID = '0';
            }
            PageMethods.CheckDuplicateSpecial(pickupID, pickupDate, address, city, processResult);
            return false;
        } // end DateChanged 

        function processResult(result) {
            if (result) {
                alert('There is already a Special scheduled for this address on this day');
            }
        } // end processResult

        // <![CDATA[
        var textSeparator = ";";
        function OnListBoxSelectionChanged(listBox, args) {
            UpdateText();
        }
        function UpdateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(GetSelectedItemsText(selectedItems));
        }
        function SynchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = GetValuesByTexts(texts);
            checkListBox.SelectValues(values);
            UpdateText(); // for remove non-existing texts
        }
        function GetSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function GetValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
        // ]]>

    </script>
    <form id="Form1" runat="server">        
        <asp:HiddenField ID="qsPickupID" runat="server" />

    <asp:ScriptManager ID="ScriptManager122" runat="server" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="Scripts/jquery-1.4.1.min.js" />
        </Scripts>
    </asp:ScriptManager>
    <div style="position:relative;top:0;left:0;">
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Specials" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table>
        <tr><td>
    	<table class="specials" align="left" border="0">
            <tr>
                <td colspan="3" nowrap="nowrap" align="right">Email like:</td>
                <td>
			        <asp:TextBox ID="txtEmailAddressLike" runat="server" Width="200" ToolTip="Full or part of the email address"></asp:TextBox>
    	        </td>
            	<td valign="bottom" style="padding-left: 5px">
		    	    <asp:Button ID="cmdSearchByEmail" runat="server" Width="160" Text="Search by email address" />
    	        </td>
            </tr>
    	    <tr>
    	        <td colspan="3" nowrap="nowrap" align="right">Street name like: </td>
                <td>
				    <asp:TextBox ID="txtStreetNameLike" runat="server" Width="200" ToolTip="Full or part of the street name"></asp:TextBox>
    	        </td>
    	        <td valign="bottom" style="padding-left: 5px">
				    <asp:Button ID="cmdSearchByStreet" runat="server" Width="160" Text="Search by street name" />
    	        </td>
                <td valign="middle" rowspan="2" width="200">
        	        <asp:Button ID="cmdSearchByStreetAndCity" runat="server" Text="Search by street name and City" width="100%"/>
                </td>
    	    </tr>
    	    <tr>
    	        <td colspan="3" nowrap="nowrap" align="right">City name like: </td>
                <td>
				    <asp:TextBox ID="txtCityNameLike" runat="server" Width="200" ToolTip="Full or part of the city name"></asp:TextBox>
    	        </td>
    	        <td valign="bottom" style="padding-left: 5px">
				    <asp:Button ID="cmdSearchByCity" runat="server" Width="160" Text="Search by city name" />
    	        </td>
    	    </tr>
        </table>
        </td></tr>
        <tr><td>
        <table>
    	    <tr>
    	        <td class="auto-style1">
    	            <table class="specials">
    	                <tr>
    	                    <td class="style1">For date: <a id="SelectedPickupDate" runat="server"></a></td>
    	                </tr>
    	                <tr>
    	                    <td>
			                    <asp:Calendar id="calSpecials" runat="server"	Width="180px" Height="180px" Font-Size="X-Small" Font-Names="Tahoma">
				                    <TodayDayStyle BorderStyle="Solid"></TodayDayStyle>
			                    </asp:Calendar>
     	                    </td>
    	                </tr>
      	            </table>
    	        </td>
    	        <td class="auto-style3">
    	            <table class="specials">
    	                <tr>
    	                    <td class="auto-style2">For location: <a id="SelectedLocation" runat="server"></a></td>
    	                </tr>
    	                <tr>
    	                    <td class="auto-style2">
                                <asp:ListBox ID="ddlLocations" runat="server" Width="120" Height="180px" Font-Size="X-Small" Font-Names="Tahoma" SelectionMode="Multiple" DataSourceID="sqlLocations" DataTextField="RegionDesc" DataValueField="RegionId" AutoPostBack="False"></asp:ListBox>
                                <asp:TextBox ID="txtSQL_ddlLocations" runat="server" Visible="false"></asp:TextBox>
                                <asp:SqlDataSource ID="sqlLocations" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
                            </td>
    	                </tr>
    	            </table>
    	        </td>
    	        <td style="width: 205px">
    	            <table class="specials">
    	                <tr>
        	                <td>For charity: <a id="SelectedCharity" runat="server"></a></td>
    	                </tr>
    	                <tr>
    	                    <td>
    		                    <asp:ListBox ID="ddlCharities" runat="server" Width="200" Height="180px" Font-Size="X-Small" Font-Names="Tahoma" SelectionMode="Multiple" DataSourceID="sqlCharities" DataTextField="CharityDescription" DataValueField="CharityId" AutoPostBack="False"></asp:ListBox>
				                <asp:TextBox ID="txtSQL_ddlCharities" runat="server" Visible="false"></asp:TextBox>
                                <asp:SqlDataSource ID="sqlCharities" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
    	                    </td>
    	                </tr>
    	            </table>
    	        </td>
    	        <td>
    	            <table class="specials">
    	                <tr>
    	                    <td>
				                <asp:Button ID="cmdSearchByDateLocationCharity" runat="server" Text="Search by date, location & charity" width="250px"/>
    	                    </td>
    	                </tr>
                        <tr>
    	                    <td>
				                <asp:Button ID="cmdUnreviewedSpecials" runat="server" Text="Show 'Not Reviewed' Specials" width="250px"/>
                            </td>
    	                </tr>
    	                <tr>
    	                    <td>
				                <asp:Button ID="cmdSpecialsSheet" runat="server" Text="Specials Sheet" width="250px"/>
                                <a runat="server" target="_new" id="aSpecialsSheet" visible="false" href="#">Show Report</a>
    	                    </td>
                        </tr>
    	                <tr>
    	                    <td>
				                <asp:Button ID="cmdSummaryReport" runat="server" Text="Summary Report" width="250px"/>
    	                    </td>
    	                </tr>
    	                <tr>
    	                    <td>
				                <asp:Button ID="cmdAddressesForRouting" runat="server" Text="Addresses For Routing Report" width="250px"/>
    	                    </td>
    	                </tr>
   	                    <tr>
    	                    <td>
				                <asp:Button ID="cmdConfirmRedTagReport" runat="server" Text="Confirm and Do Not Red Tag Report" width="250px"/>
    	                    </td>
    	                </tr>
   	                    <tr>
    	                    <td>
				                <asp:Button ID="cmdSpecialsNotGradedReport" runat="server" Text="Specials Not Graded Report" width="250px"/>
    	                    </td>
    	                </tr>
                        <tr>
        	                <td>
				                <asp:Button ID="cmdShowGrid" runat="server" Text="Show Grid" width="250px" Visible="False" />
    	                    </td>
     	                </tr>
                        <tr>
        	                <td>
				                <asp:Button ID="cmdShowNextPickups" runat="server" Text="Show Next Pickups" width="250px" Visible="True" />
    	                    </td>
     	                </tr>
                    </table>
    	        </td>
    	    </tr>
        </table>
        <table>
            <tr>
                <td style="width: 540px">
                    <asp:Label ID="qryRouteSection" runat="server" Text="RouteSection" Width="200px" Forecolor="DarkMagenta" Font-Names="Arial" Font-Size="Small" Font-Bold="true" />
                    <asp:Label ID="qryNextScheduledPickup" runat="server" Text="NextPickup" Width="250px" Forecolor="DarkMagenta" Font-Names="Arial" Font-Size="Small" Font-Bold="true" />
                    <asp:Button ID="btnPrevPickup" runat="server" Text="Prev" Forecolor="DarkMagenta" Width="50px" Visible="False" />
                    <asp:HiddenField ID="hfSyskey" runat="server" Value=""></asp:HiddenField>
                    <asp:HiddenField ID="hfType" runat="server" Value=""></asp:HiddenField>
                    <asp:HiddenField ID="hfPrevPickupDate" runat="server" Value=""></asp:HiddenField>
                    <input type="hidden" id="hfPrevPickupIsToday" value="False" runat="server" />                  
               </td>
                <td>
                    <asp:Label ID="qryResults" runat="server" Text="" Width="250px" Font-Names="Arial" Font-Size="Small" />
              </td>
            </tr>
		</table>
		<table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxCheckBox ID="ckConfirmed" runat="server" Text="Confirmed" Forecolor="DarkCyan" EnableViewState="true" Visible="False">
                                    <ClientSideEvents ValueChanged="OnConfirmValueChanged" Init="OnConfirmInit" />
                                </dx:ASPxCheckBox>
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="ckMissed" runat="server" Text="Missed" Forecolor="DarkCyan" EnableViewState="true" Visible="False">
									<ClientSideEvents ValueChanged="OnDetailValueChanged" />
								</dx:ASPxCheckBox>
                            </td>
                           <td>
                                <dx:ASPxCheckBox ID="ckRedTagged" runat="server" Text="Red Tagged" Forecolor="DarkCyan" EnableViewState="true" Visible="False">
 									<ClientSideEvents ValueChanged="OnDetailValueChanged" />
								</dx:ASPxCheckBox>
                            </td>
                            <td align="right" style="width: 70px">
                                <asp:Label ID="lblDetailComments" runat="server" Text="Driver Comments:" Forecolor="DarkCyan" Width="50px" Font-Names="Arial" Font-Size="X-Small" Visible="False" />
                            </td>
                            <td style="width: 250px">
                                <dx:ASPxTextBox ID="txtDetailComments" runat="server" Forecolor="DarkCyan" Width="240" Text="" Visible="False">
									<ClientSideEvents ValueChanged="OnDetailValueChanged" />
								</dx:ASPxtextBox>
                             </td>
                            <td>
                                <asp:Button ID="btnScheduleDetailSave" runat="server" Text="Save" Forecolor="DarkCyan" Width="50px" Visible="False" />
                                <asp:HiddenField ID="hfPickupScheduleDetailID" runat="server" Value=""></asp:HiddenField>
                            </td>
						</tr>
                        <tr>
                            <td style="text-align: right">
                               <asp:Label ID="lblDeliveryDate" runat="server" Text="Delivery Date:" Forecolor="DarkCyan" Font-Names="Arial" Font-Size="Small" Visible="False" />
                            </td>
                            <td colspan="2">
                              <dx:ASPxDateEdit ID="calDeliveryDate" runat="server" Width="100" EditFormat="Custom" UseMaskBehavior="true" Visible="false" />
                            </td>
                            <td colspan="2" style="text-align: right">
                                <div id="confirmWrapper" style="display: none;" runat="server">
                                    <table>
                                        <tr>
                                            <td style="width:100%">
                                                <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="ddlDrivers" Width="100%" runat="server" AnimationType="None">
                                                    <DropDownWindowTemplate>
                                                        <dx:ASPxListBox Width="100%" ID="listBox" DataSourceID="dsDrivers" ClientInstanceName="checkListBox" SelectionMode="CheckColumn" runat="server" ValueType="System.Int32" ValueField="DriverID" TextField="DriverName">
                                                            <Border BorderStyle="None" />
                                                            <BorderBottom BorderStyle="Solid" BorderWidth="1px" />                                   
                                                            <ClientSideEvents SelectedIndexChanged="OnListBoxSelectionChanged" />
                                                        </dx:ASPxListBox>
                                                        <table style="width: 100%" cellspacing="0" cellpadding="4">
                                                            <tr>
                                                                <td align="right">
                                                                    <dx:ASPxButton ID="btnListBoxClose" AutoPostBack="False" runat="server" Text="Close">
                                                                        <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                                                    </dx:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </DropDownWindowTemplate>
                                                    <ClientSideEvents TextChanged="SynchronizeListBoxValues" DropDown="SynchronizeListBoxValues" />
                                                </dx:ASPxDropDownEdit>
                                                <asp:SqlDataSource ID="dsDrivers" runat="server" ProviderName="System.Data.SqlClient"
                                                    ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                                                </asp:SqlDataSource>                    
                                                <asp:HiddenField ID="hfDrivers" runat="server" Value=""></asp:HiddenField>
                                                <asp:SqlDataSource ID="dsDriver" runat="server" ProviderName="System.Data.SqlClient"
                                                    ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                                                </asp:SqlDataSource>                    
                                                <asp:HiddenField ID="hfDriver" runat="server" Value=""></asp:HiddenField>
                                            </td>
                                            <td style="width:35%">
                                                <asp:Button ID="btnTextDriver" runat="server" Text="Send Text" Forecolor="DarkCyan" Width="100%" />
												<asp:Label ID="lblTextsSent" runat="server" Text="0 texts sent" Font-Names="Arial" Font-Size="Small" />
                                          </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100px">
                                <asp:Label ID="lblOtherComments" runat="server" Text="Other Comments:" Forecolor="DarkCyan" Width="50px" Font-Names="Arial" Font-Size="X-Small" Visible="False" />
                            </td>
                            <td colspan="4">
                               <dx:ASPxTextBox ID="txtOtherComments" runat="server" Forecolor="DarkCyan" Width="400" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </td>
	            <td style="width: 250px">
                    <dx:ASPxImage ID="imgMail" ImageUrl="~/Resources/images/mail.png" Height="30" Width="30" visible="false" runat="server" />
                    <dx:ASPxImage ID="imgEmail"  ImageUrl="~/Resources/images/email.gif"  Height="30" Width="30" visible="false" runat="server" />
                    <dx:ASPxImage ID="imgBag"  ImageUrl="~/Resources/images/bag.png" Height="30" Width="30" visible="false" runat="server" />
                    <dx:ASPxImage ID="imgPostcard"  ImageUrl="~/Resources/images/postcard.png" Height="23" Width="30" visible="false" runat="server" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="width: 800px">
                    <div id="divGridSpecials" runat="server" title="Previous Specials" style="border-width:thin; border-style:solid; border-color:Black;">
                        <asp:GridView ID="grdSpecials" runat="server" DataKeyNames="PickupID" 
                            BackColor="LightSteelBlue" AllowPaging="True" AllowSorting="True" PageSize="5" 
                            AutoGenerateColumns="false" Width="800" OnRowCreated="grdSpecials_RowCreated" >
                            <RowStyle Font-Size="X-Small" Font-Names="Tahoma" Wrap="false" />
                            <HeaderStyle Font-Names="Tahoma" Font-Size="XX-Small" Wrap="false" />
                            <PagerStyle Font-Size="X-Small" Font-Names="Tahoma"/>
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <th colspan="8">this is the primary header</th>
                                        <tr class="gvHeader">
                                            <th>Col1</th>
                                            <th>Col2</th>
                                        </tr>
                                    </HeaderTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
				        <asp:TextBox ID="txtSQL_grdSpecials" runat="server" Visible="false"></asp:TextBox>
                        <asp:SqlDataSource ID="sqlSpecials" runat="server" ProviderName="System.Data.SqlClient">
 
                        </asp:SqlDataSource>
                    </div>
                    <div id="divGridAddresses" runat="server" title="Addresses" style="border-width:thin; border-style:solid; border-color:Black;">
                        <asp:GridView ID="grdAddresses" runat="server" DataKeyNames="AddressID" 
                            BackColor="LightGreen" AllowPaging="True" AllowSorting="True" PageSize="5" 
                            AutoGenerateColumns="false" Width="800" OnRowCreated="grdAddresses_RowCreated">
                            <RowStyle Font-Size="X-Small" Font-Names="Tahoma" Wrap="false" />
                            <HeaderStyle Font-Names="Tahoma" Font-Size="XX-Small" Wrap="false" />
                            <PagerStyle Font-Size="X-Small" Font-Names="Tahoma"/>
                        </asp:GridView>
				        <asp:TextBox ID="txtSQL_grdAddresses" runat="server" Visible="false"></asp:TextBox>
                        <asp:SqlDataSource ID="sqlAddresses" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
                    </div>
                    <div id="divGridNextPickups" runat="server" title="Next Pickups" style="border-width:thin; border-style:solid; border-color:Black;">
                        <asp:GridView ID="grdNextPickups" runat="server" DataKeyNames="StreetName,City,Zip,PickupDate,RouteSection" 
                            BackColor="Violet" AllowPaging="True" AllowSorting="True" PageSize="15" 
                            AutoGenerateColumns="false" Width="800" OnRowCreated="grdNextPickups_RowCreated">
                            <RowStyle Font-Size="X-Small" Font-Names="Tahoma" Wrap="false" />
                            <HeaderStyle Font-Names="Tahoma" Font-Size="XX-Small" Wrap="false" />
                            <PagerStyle Font-Size="X-Small" Font-Names="Tahoma"/>
                        </asp:GridView>
				        <asp:TextBox ID="txtSQL_grdNextPickups" runat="server" Visible="false"></asp:TextBox>
                        <asp:SqlDataSource ID="sqlNextPickups" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
        </table>
        </td></tr>
        <tr><td>
        <table>
            <tr>
                <td>
                    <div id="div1" runat="server" title="Specials" style="border-width:thin; border-color:Black; border-style:solid">
			            <table class="specials">
    	                    <tr>
    	                        <td>
				                    <asp:Button ID="cmdNewSpecial" runat="server" Text="New Special" width="250px"/>
 				                    <asp:Button ID="cmdNewAddress" runat="server" Text="New Special with New Address" width="250px"/>
				                    <asp:Button ID="cmdDeleteSpecial" runat="server" Text="Delete Special" width="250px"/>
    	                        </td>
    	                    </tr>
			                <tr>
			                    <td>
                                    <div id="divNotReviewed" runat="server" style="border-width:thin; border-color:Black; border-style:solid">
                                        <table class="specials">
                                            <tr>
                                                <td style="font-weight: bold">'Not Reviewed' Specials:</td>
              	                                <td style="width: 400px">Comment for Email:
					                                <asp:TextBox ID="txtNotReviewed" runat="server" ToolTip="Comment for Email" 
						                                TextMode="MultiLine" Width="100%"></asp:TextBox>
				                                </td>
                                                <td>
                                                    <asp:Button ID="btnNotReviewedConfirm" Text="Confirm" Width="80" runat="server" ForeColor="Green" ToolTip="Confirm"></asp:Button>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnNotReviewedChange" Text="Change" Width="80" runat="server" ForeColor="Purple" ToolTip="Confirm"></asp:Button>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnNotReviewedCancel" Text="Cancel" Width="80" runat="server" ForeColor="Red" ToolTip="Reject"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
			                        <div id="div2" runat="server" title="Address Information" style="border-width:thin; border-color:Black; border-style:solid">
				                        <table class="specials">
				                            <tr>
    	                                        <td rowspan="2">
                                                    <dx:ASPxRadioButtonList ID="rbSpecialType" ClientInstanceName="rbSpecialType" runat="server" ToolTip="Special originated from an Phone call or an Email">
                                                        <Items>
                                                            <dx:ListEditItem Text="Phone" Value="PHONE" />
                                                            <dx:ListEditItem Text="Email" Value="EMAIL" />
                                                            <dx:ListEditItem Text="Web" Value="WEB" />
                                                            <dx:ListEditItem Text="Route" Value="ROUTE" />
                                                        </Items>
                                                    </dx:ASPxRadioButtonList>
    	                                        </td>
				                                <td colspan="2" style="width: 181px">Address: 
					                                <asp:TextBox ID="txtAddress" runat="server" ToolTip="Pickup Address (Street # and name)" Width="180"></asp:TextBox>
				                                </td>

				                                <td style="width: 125px">Cross street:
					                                <asp:TextBox ID="txtCrossStreet" runat="server" ToolTip="Cross Street name" Width="120"></asp:TextBox>
				                                </td>
                                                <td style="width: 125px">City:
					                                <asp:TextBox ID="txtCity" runat="server" ToolTip="City" Width="120"></asp:TextBox>
				                                </td>
				                                <td style="width: 200px">ZIP:
					                                <asp:TextBox ID="txtZIP" runat="server" ToolTip="ZIP" Width="100"></asp:TextBox>
				                                </td>
				                                <td style="width: 305px">Address Status:
					                                <asp:TextBox ID="txtAddressStatus" runat="server" ToolTip="Address Status" Width="150"></asp:TextBox>
				                                </td>
				                                <td style="width: 305px">
					                                <asp:TextBox ID="txtNearestSection" runat="server" Width="200"></asp:TextBox>
				                                </td>
    			                            </tr>
				                            <tr>
				                                <td style="width: 88px">First name:
					                                <asp:TextBox ID="txtFirstName" runat="server" ToolTip="First name" Width="87"></asp:TextBox>
				                                </td>
				                                <td style="width: 88px">Last name:
					                                <asp:TextBox ID="txtLastName" runat="server" ToolTip="Last name" Width="87"></asp:TextBox>
				                                </td>
				                                <td style="width: 130px">Home phone:
                                                    <dx:ASPxTextBox ID="txtHomePhone" runat="server" ToolTip="Home phone" Width="125">
                                                        <MaskSettings Mask="(999) 000-0000 x999" IncludeLiterals="None" />
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" />
                                                    </dx:ASPxTextBox> 
  			                                    </td>
				                                <td style="width: 130px">Work phone:
                                                    <dx:ASPxTextBox ID="txtWorkPhone" runat="server" ToolTip="Work phone" Width="125">
                                                        <MaskSettings Mask="(999) 000-0000 x999" IncludeLiterals="None" />
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" />
                                                    </dx:ASPxTextBox> 
  				                                </td>
				                                <td style="width: 130px">Mobile phone:
                                                    <dx:ASPxTextBox ID="txtMobilePhone" runat="server" ToolTip="Mobile phone" Width="125">
                                                        <MaskSettings Mask="(999) 000-0000 x999" IncludeLiterals="None" />
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" />
                                                    </dx:ASPxTextBox> 
  				                                </td>
				                                <td style="width: 115px">Language Preference:
					                                <asp:DropDownList ID="ddlLanguagePreference" runat="server" ToolTip="Language Preference" Width="100%" DataSourceID="dsLanguagePreference" DataTextField="Language" DataValueField="Language" AppendDataBoundItems="True"></asp:DropDownList>
				                                    <asp:SqlDataSource ID="dsLanguagePreference" runat="server" ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>" SelectCommand="SELECT Language FROM tlkLanguages ORDER BY SortCode"></asp:SqlDataSource>
				                                </td>
				                                <td>Email:
 					                                <asp:TextBox ID="txtEmail" runat="server" ToolTip="Email address" Width="150"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="btnEmailPop" Text="Select" Width="80" runat="server"></dx:ASPxButton>
                                                </td>
       		                                </tr>
				                        </table>
			                        </div>
			                    </td>
			                </tr>
			                <tr>
			                    <td>
			                        <div id="div3" runat="server" title="Special Information" style="border-width:thin; border-color:Black; border-style:solid">
				                        <table class="specials">
				                            <tr>
				                                <td style="width: 115px">Item Location:
					                                <asp:DropDownList ID="ddlItemLocation" runat="server" ToolTip="Item Location" OnDataBound="vbAddBlank" Width="100%" DataSourceID="SqlDataSourceItemLocation" DataTextField="ItemLocation" DataValueField="ItemLocation" AppendDataBoundItems="True"></asp:DropDownList>
				                                    <asp:SqlDataSource ID="SqlDataSourceItemLocation" runat="server" ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>" SelectCommand="SELECT [ItemLocation] FROM [tlkItemLocations] ORDER BY SortCode"></asp:SqlDataSource>
				                                </td>
				                                <td style="width: 35px">Bags:
					                                <asp:TextBox ID="txtPromisedBags" runat="server" ToolTip="Promised Bags" Width="100%"></asp:TextBox>
				                                </td>
				                                <td style="width: 35px">Boxes:
					                                <asp:TextBox ID="txtPromisedBoxes" runat="server" ToolTip="Promised Boxes" Width="100%"></asp:TextBox>
				                                </td>
				                                <td style="width: 115px">Promised Other:
					                                <asp:TextBox ID="txtPromisedOther" runat="server" ToolTip="Promised Other" Width="100%"></asp:TextBox>
				                                </td>
				                                <td style="width: 115px">Pickup date:
                                                    <dx:ASPxDateEdit ID="dtPickupDate" ClientInstanceName="dtPickupDate" 
                                                        Width="100%" ToolTip="Pickup date" EditFormat="Custom" 
                                                        EditFormatString="MM/dd/yyyy" ValidationSettings-SetFocusOnError="True" 
                                                        ValidationSettings-RegularExpression-ErrorText="Invalid date" 
                                                        ValidationSettings-ErrorImage-Url="~/Resources/Images/iconError.png" 
                                                        runat="server">
                                                        <ClientSideEvents ValueChanged="function(s,e) { DateChanged(); }" />
                                                        <ValidationSettings SetFocusOnError="True">
                                                        <ErrorImage Url="~/Resources/Images/iconError.png"></ErrorImage>
                                                        <RegularExpression ErrorText="Invalid date"></RegularExpression>
                                                        </ValidationSettings>
                                                    </dx:ASPxDateEdit>
				                                </td>
				                                <td style="width: 115px">Location:
					                                <asp:DropDownList ID="ddlLocation" runat="server" ToolTip="Driver Location" Width="100%" OnDataBound="vbAddBlank"
                                                        DataSourceID="sqlLocations" DataTextField="RegionDesc" DataValueField="RegionDesc">
					                                </asp:DropDownList>
                                                </td>
				                                <td style="width: 115px">Charity:
					                                <asp:DropDownList ID="ddlCharity" runat="server" ToolTip="Charity" Width="100%" OnDataBound="vbAddBlank"
                                                        DataSourceID="sqlCharities" DataTextField="CharityAbbr" DataValueField="CharityId">
					                                </asp:DropDownList>
  				                                </td>
				                                <td align="center" style="width: 115px">
					                                Receipt:<asp:CheckBox ID="ckReceipt" runat="server" ToolTip="Receipt Required" Width="60" Text=""></asp:CheckBox>
					                                Reminder:<asp:CheckBox ID="ckReminder" runat="server" ToolTip="Reminder Call" Width="60" Text=""></asp:CheckBox>
				                                </td>
				                            </tr>
				                            <tr>
				                                <td>Gate:
					                                <asp:TextBox ID="txtGate" runat="server" ToolTip="Gate" Width="100%"></asp:TextBox>
				                                </td>
				                                <td colspan="4">Comment:
					                                <asp:TextBox ID="txtComment" runat="server" ToolTip="Comment" 
						                                TextMode="MultiLine" Width="100%"></asp:TextBox>
				                                </td>
                                                <td>Grade:
					                                <asp:DropDownList ID="ddlGrade" runat="server" ToolTip="Grade" Width="100%" OnDataBound="vbAddBlank"
                                                        DataSourceID="SqlDataSourceGrades" DataTextField="Grade" DataValueField="Grade">
					                                </asp:DropDownList>
				                                    <asp:SqlDataSource ID="SqlDataSourceGrades" runat="server"  
                                                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>">
                                                    </asp:SqlDataSource>
                                                </td>
                                                <td>Status:
					                                <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Status" Width="100%" OnDataBound="vbAddBlank"
                                                        DataSourceID="SqlDataSourceStatus" DataTextField="SpecialStatus" 
                                                        DataValueField="SpecialStatus" ClientIDMode="Static" >
					                                </asp:DropDownList>
				                                    <asp:SqlDataSource ID="SqlDataSourceStatus" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>">
                                                    </asp:SqlDataSource>
				                                </td>
				                                <td>
					                                <asp:Button ID="cmdSave" runat="server" Text="Save Special"  Width="100%" 
						                                Height="100%" OnClientClick="return CmdSaveValidate()" />
				                                </td>
 				                                <td>
                	                                <asp:Label ID="lblInitialStatus" runat="server" Visible="false"></asp:Label>
				                                </td>
                                            </tr>
                                            <tr>
				                                <td style="width: 115px">Grid:
					                                <asp:TextBox ID="txtGrid" runat="server" ToolTip="Grid" Width="100%"></asp:TextBox>
				                                </td>
                                               <td>Bags:
					                                <asp:TextBox ID="txtBags" runat="server" ToolTip="Bags" Width="100%"></asp:TextBox>
				                                </td>
				                                <td>Boxes:
					                                <asp:TextBox ID="txtBoxes" runat="server" ToolTip="Boxes" Width="100%"></asp:TextBox>
				                                </td>
				                                <td>Actual Other:
					                                <asp:TextBox ID="txtOther" runat="server" ToolTip="Other" Width="100%"></asp:TextBox>
				                                </td>
				                                <td>Driver:
					                                <asp:DropDownList ID="ddlDriver" runat="server" ToolTip="Pickup Driver" Width="100%" OnDataBound="vbAddBlankZero"
                                                        DataSourceID="dsDriver" DataTextField="DriverName" DataValueField="DriverId">
					                                </asp:DropDownList>
                                                    <asp:HiddenField ID="hfDeviceName" runat="server" Value=""></asp:HiddenField>
                                                    <asp:HiddenField ID="hfDriverID" runat="server" Value=""></asp:HiddenField>
				                                </td>
                                                <td>
                                                <table>
                                                <tr>
				                                <td style="width: 50%">Start Time:
                                                    <dx:ASPxTimeEdit ID="dtStartTime" runat="server" DateTime="" Width="100%" EditFormat="Time" EditFormatString="HH:mm" DisplayFormatString="HH:mm">
                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                    </dx:ASPxTimeEdit>
                                                </td>
                                                <td style="width: 50%">End Time:
                                                    <dx:ASPxTimeEdit ID="dtEndTime" runat="server" DateTime="" Width="100%" EditFormat="Time" EditFormatString="HH:mm" DisplayFormatString="HH:mm">
                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                    </dx:ASPxTimeEdit>
				                                </td>
                                                </tr>
                                                </table>
                                                </td>
				                                <td>DonorSource:
					                                <asp:TextBox ID="txtDonorSource" runat="server" ToolTip="Donor Source(s)" Width="100%"></asp:TextBox>
				                                </td>
   				                                <td>
				                                </td>   
                                             </tr>
				                        </table>
			                        </div>
			                    </td>
			                </tr>
			                <tr>
			                    <td>
			                        <div id="div4" runat="server" title="User Information" style="border-width:thin; border-color:Black; border-style:solid">
				                        <table class="specials">
				                            <tr>
				                                <td style="width: 15%">Scheduled by:
					                                <asp:Label ID="lblScheduledBy" runat="server" Width="100%" Font-Bold="False" 
                                                        Font-Names="Arial" Font-Size="Small"></asp:Label>
				                                </td>
				                                <td style="width: 30%">Scheduled on:
					                                <asp:Label ID="lblScheduledOn" runat="server" Width="100%" Font-Bold="False" 
                                                        Font-Names="Arial" Font-Size="Small"></asp:Label>
				                                </td>
				                                <td style="width: 15%">Modified by:
					                                <asp:Label ID="lblModifiedBy" runat="server" Width="100%" Font-Bold="False" 
                                                        Font-Names="Arial" Font-Size="Small"></asp:Label>
				                                </td>
				                                <td style="width: 30%">Modified on:
					                                <asp:Label ID="lblModifiedOn" runat="server" Width="100%" Font-Bold="False" 
                                                        Font-Names="Arial" Font-Size="Small"></asp:Label>
				                                </td>
				                                <td style="width: 10%">
				                                </td>
                                             </tr>
				                        </table>
			                        </div>
			                    </td>
			                </tr>
			                <tr>
			                    <td>
			                        <div id="div5" runat="server" title="Dialer Information" style="border-width:thin; border-color:Black; border-style:solid">
				                        <table class="specials">
				                            <tr>
				                                <td style="width: 15%">Dialer Date/Time:
					                                <asp:TextBox ID="txtDialerDate" runat="server" ToolTip="Dialer Date/Time" Width="100%"></asp:TextBox>
				                                </td>
				                                <td style="width: 15%">Dialer Status:
					                                <asp:TextBox ID="txtDialerStatus" runat="server" ToolTip="Dialer Status" Width="100%"></asp:TextBox>
				                                </td>
				                                <td style="width: 70%">Dialer Comment:
					                                <asp:TextBox ID="txtDialerComment" runat="server" ToolTip="Dialer Comment" Width="100%"></asp:TextBox>
				                                </td>
                                             </tr>
				                        </table>
			                        </div>
			                    </td>
			                </tr>
			            </table>
                    </div>
                </td>
            </tr>
        </table>
        </td></tr>
        </table>

        <br /><br /><br />

        <table class="hidden" runat="server" visible="True">
            <tr>
                <td>Pickup ID:
    		         <dx:ASPxTextBox ClientVisible="True" ID="txtPickupID" runat="server"  Visible="True"></dx:ASPxTextBox>
		        </td>
                <td>Address ID:
			        <dx:ASPxTextBox ClientVisible="True" ID="txtAddressID" runat="server" Visible="True"></dx:ASPxTextBox>
		        </td>
                <td>
		             <dx:ASPxTextBox ClientVisible="False" ID="txtSpecialsRows" runat="server" Visible="False"></dx:ASPxTextBox>
		        </td>
                <td>
			        <dx:ASPxTextBox ClientVisible="False" ID="txtAddressesRows" runat="server" Visible="False"></dx:ASPxTextBox>
		        </td>
                <td>
			        <dx:ASPxTextBox ClientVisible="False" ID="txtNextPickupsRows" runat="server" Visible="False"></dx:ASPxTextBox>
		        </td>
                <td>
			        <dx:AspxCheckBox ID="ckSpecialsOnly" runat="server" Visible="False"></dx:AspxCheckBox>
		        </td>
            </tr>
        </table>
    </div>
    <dx:ASPxPopupControl ID="popID" ClientInstanceName="popID" PopupElementID="btnEmailPop" HeaderText="Emails" FooterText="" Width="600px" Height="300px" EncodeHtml="false" PopupVerticalAlign="Below" PopupHorizontalAlign="Center" AllowResize="true" runat="server" CloseAction="OuterMouseClick" AllowDragging="True" ShowFooter="true">
        <ContentCollection>
            <dx:PopupControlContentControl>
			    <asp:SqlDataSource ID="sql_gridEmails" 
                    UpdateCommand="update tblAddressEmails set Unsubscribed=@Unsubscribed where AddressEmailID=@AddressEmailID" 
                    DeleteCommand="delete from tblAddressEmails where AddressEmailID=@AddressEmailID" 
                    SelectCommand="SELECT * FROM [tblAddressEmails] where AddressID=@AddressID AND [Deleted] = 0 ORDER BY [email]" 
                    InsertCommand="insert into tblAddressEmails (AddressID,Email,Unsubscribed) values (@AddressID,@Email,@Unsubscribed)" 
                    runat="server" ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtAddressID" Name="AddressID" Type="Int32" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="AddressEmailID" Type="Int32" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="AddressEmailID" Type="Int32" />
                        <asp:Parameter Name="Unsubscribed" Type="Boolean" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:ControlParameter ControlID="txtAddressID" Name="AddressID" Type="Int32" />
                        <asp:Parameter Name="Email" Type="String" />
                        <asp:Parameter Name="Unsubscribed" Type="Boolean" />
                    </InsertParameters>
			    </asp:SqlDataSource>
                <dx:ASPxGridView EnableCallBacks="false" ID="gridEmails" KeyFieldName="AddressEmailID" ClientInstanceName="gridEmails"  runat="server" SettingsEditing-Mode="Inline" DataSourceID="sql_gridEmails" Width="100%">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" ShowEditButton="True" ShowDeleteButton="True" ShowNewButton="True"/>
                        <dx:GridViewDataTextColumn Visible="false" Caption="AddressEmailID" Name="AddressEmailID" FieldName="AddressEmailID"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Email" FieldName="Email"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="Unsubscribed" FieldName="Unsubscribed" Name="Unsubscribed"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn Caption="Origin" FieldName="Origin" ReadOnly="true" PropertiesTextEdit-ReadOnlyStyle-BackColor="Silver">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle BackColor="Silver"></ReadOnlyStyle>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Last Date Updated" FieldName="LastDateUpdated" ReadOnly="true" PropertiesTextEdit-ReadOnlyStyle-BackColor="Silver">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle BackColor="Silver"></ReadOnlyStyle>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="Confirmed" FieldName="Confirmed" ReadOnly="true"></dx:GridViewDataCheckColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
                    <SettingsEditing Mode="Inline"></SettingsEditing>
                    <SettingsCommandButton>
                        <DeleteButton Text="Del"/>
                        <UpdateButton Text="Save"/>
                        <CancelButton Text="Quit"/>
                        <DeleteButton Text="Del"/>
                        <CancelButton Text="Quit"/>
                        <UpdateButton Text="Save"/>
                    </SettingsCommandButton>
                </dx:ASPxGridView>
                <asp:Button ID="SelectEmail" Text="Select Email" runat="server"></asp:Button>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    </form>
</body>
</html>
