<%@ Page Title="Containers" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="Containers.aspx.vb" Inherits="Containers" %>

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
        }

        function CmdSaveValidate() {
            if (dtPickupDate.GetDate() == null) {
                if (document.getElementById('ddlStatus').value != ' ' && document.getElementById('ddlStatus').value != 'NOT REVIEWED') {
                        alert('Email and Web Specials must have a Pickup Date after they have been Scheduled.');
                        return false;
                }
            }
            else {
                var curTime = new Date();
                var curDate = curTime.getMonth() + 1 + "/" + curTime.getDate() + "/" + curTime.getFullYear(); //Todays Date
                var dd = DateDiff(dtPickupDate.GetDate(), new Date(curDate).getTime());
                //if (document.getElementById('ddlStatus').value == 'SCHEDULED') {
                //    if (dd < 0) {
                //        alert('Containers cannot be scheduled in the past.');
                //        return false;
                //    }
                //}
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
            if (document.getElementById('txtAddress').value == '') {
                alert('Please enter a container address.');
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
   			if (document.getElementById('cmdSave').value == "Save Old Container") {
   				alert('Pickup Dates of Old Container Pickups cannot be modified.');
   				dtPickupDate.SetDate(new Date(document.getElementById('hfPickupDate').value));
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
    <asp:HiddenField ID="hfPickupDate" runat="server" Value=""></asp:HiddenField>
    <asp:ScriptManager ID="ScriptManager122" runat="server" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="Scripts/jquery-1.4.1.min.js" />
        </Scripts>
    </asp:ScriptManager>
    <div style="position:relative;top:0;left:0;">
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Containers" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
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
				                <asp:Button ID="cmdSearchByLocation" runat="server" Text="Search by Location" width="250px"/>
    	                    </td>
    	                </tr>
    	                <tr>
    	                    <td>
				                <asp:Button ID="cmdSearchByDateLocationCharity" runat="server" Text="Search by Date, Location & Charity" width="250px"/>
    	                    </td>
    	                </tr>
    	                <tr>
    	                    <td>
				                <asp:Button ID="cmdContainerPickupsSheet" runat="server" Text="Container Pickups Sheet" width="250px"/>
                                <a runat="server" target="_new" id="aContainerPickupsSheet" visible="false" href="#">Show Report</a>
    	                    </td>
                        </tr>
    	                <tr>
    	                    <td>
				                <asp:Button ID="cmdContainerPickupsForRouting" runat="server" Text="Container Pickups For Routing Report" width="250px"/>
    	                    </td>
    	                </tr>
   	                    <tr>
    	                    <td>
				                <asp:Button ID="cmdContainerPickupsNotGradedReport" runat="server" Text="Container Pickups Not Graded Report" width="250px"/>
    	                    </td>
    	                </tr>
                        <tr>
        	                <td>
				                <asp:Button ID="cmdShowGrid" runat="server" Text="Show Grid" width="250px" Visible="False" />
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
                                <dx:ASPxCheckBox ID="ckMissed" runat="server" Text="Missed" Forecolor="DarkCyan" EnableViewState="true" Visible="False" />
                            </td>
                            <td align="right" style="width: 70px">
                                <asp:Label ID="lblDetailComments" runat="server" Text="Comments:" Forecolor="DarkCyan" Width="50px" Font-Names="Arial" Font-Size="X-Small" Visible="False" />
                            </td>
                            <td style="width: 250px">
                                <dx:ASPxTextBox ID="txtDetailComments" runat="server" Forecolor="DarkCyan" Width="240" Visible="False" />
                            </td>
                            <td>
                                <asp:Button ID="btnScheduleDetailSave" runat="server" Text="Save" Forecolor="DarkCyan" Width="50px" Visible="False" />
                                <asp:HiddenField ID="hfPickupScheduleDetailID" runat="server" Value=""></asp:HiddenField>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td colspan="2" align="right">
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
                                            </td>
                                            <td style="width:35%">
                                                <asp:Button ID="btnTextDriver" runat="server" Text="Send Text" Forecolor="DarkCyan" Width="100%" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
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
                    <div id="divGridSpecials" runat="server" title="Previous Container Pickups" style="border-width:thin; border-style:solid; border-color:Black;">
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
				                    <asp:Button ID="cmdNewContainer" runat="server" Text="New Pickup" width="250px"/>
 				                    <asp:Button ID="cmdNewContainerAddress" runat="server" Text="New Pickup for New Container" width="250px"/>
				                    <asp:Button ID="cmdDeleteContainer" runat="server" Text="Delete Pickup" width="250px"/>
    	                        </td>
    	                    </tr>
			                <tr>
			                    <td>
			                        <div id="div2" runat="server" title="Address Information" style="border-width:thin; border-color:Black; border-style:solid">
				                        <table class="specials">
				                            <tr>
    	                                        <td></td>
				                                <td colspan="2" style="width: 181px">Address:
					                                <asp:TextBox ID="txtAddress" runat="server" ToolTip="Pickup Address (Street # and name)" Width="180"></asp:TextBox>
				                                </td>
				                                <td style="width: 125px">Cross street:
					                                <asp:TextBox ID="txtCrossStreet" runat="server" ToolTip="Cross Street name" Width="120"></asp:TextBox>
				                                </td>
                                                <td style="width: 125px">City:
					                                <asp:TextBox ID="txtCity" runat="server" ToolTip="City" Width="120"></asp:TextBox>
				                                </td>
				                                <td style="width: 305px">ZIP:
					                                <asp:TextBox ID="txtZIP" runat="server" ToolTip="ZIP" Width="100"></asp:TextBox>
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
				                                <td style="width: 115px">Driver:
					                                <asp:DropDownList ID="ddlDrivers2" runat="server" ToolTip="Driver" Width="100%" OnDataBound="vbAddBlank"
                                                        DataSourceID="dsDrivers2" DataTextField="DriverName" DataValueField="DriverID">
					                                </asp:DropDownList>
				                                </td>
				                                <td style="width: 35px">Soft Carts:
					                                <asp:TextBox ID="txtSoftCarts" runat="server" ToolTip="Soft Carts" Width="100%" style="text-align: right"></asp:TextBox>
				                                </td>
				                                <td style="width: 35px">Hard Carts:
					                                <asp:TextBox ID="txtHardCarts" runat="server" ToolTip="Hard Carts" Width="100%" style="text-align: right"></asp:TextBox>
				                                </td>
				                                <td style="width: 115px"></td>
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
				                                <td align="center" style="width: 115px"></td>
				                            </tr>
				                            <tr>
				                                <td></td>
				                                <td colspan="4">Comment:
					                                <asp:TextBox ID="txtComment" runat="server" ToolTip="Comment" 
						                                TextMode="MultiLine" Width="100%"></asp:TextBox>
				                                </td>
                                                <td>Grade:
					                                <asp:DropDownList ID="ddlGrade" runat="server" ToolTip="Grade" Width="100%" OnDataBound="vbAddBlank"
                                                        DataSourceID="SqlDataSourceGrades" DataTextField="Grade" DataValueField="Grade">
					                                </asp:DropDownList>
				                                    <asp:SqlDataSource ID="SqlDataSourceGrades" runat="server"  
                                                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>" 
                                                        SelectCommand="SELECT [Grade] FROM [tlkGrades] ORDER BY [SortCode]">
                                                    </asp:SqlDataSource>
                                                </td>
                                                <td>Status:
					                                <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Status" Width="100%" OnDataBound="vbAddBlank"
                                                        DataSourceID="SqlDataSourceStatus" DataTextField="SpecialStatus" 
                                                        DataValueField="SpecialStatus" ClientIDMode="Static" >
					                                </asp:DropDownList>
				                                    <asp:SqlDataSource ID="SqlDataSourceStatus" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>" 
                                                        SelectCommand="SELECT [SpecialStatus] FROM [tlkSpecialStatuses] ORDER BY [SortCode]">
                                                    </asp:SqlDataSource>
				                                </td>
				                                <td>
					                                <asp:Button ID="cmdSave" runat="server" Text="Save Pickup"  Width="100%" 
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
				                                <td>Device Name:
					                                <asp:TextBox ID="txtDeviceName" runat="server" ToolTip="Device Name" Width="100%"></asp:TextBox>
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
				                                <td></td>
   				                                <td>Scheduled by:
					                                <asp:Label ID="lblScheduledBy" runat="server" Width="100%" Font-Bold="False" 
                                                        Font-Names="Arial" Font-Size="Small"></asp:Label>
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

        <asp:SqlDataSource ID="dsDrivers2" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
        </asp:SqlDataSource>
 
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
    </form>
</body>
</html>
