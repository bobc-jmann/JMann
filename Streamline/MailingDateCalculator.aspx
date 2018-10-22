<%@ Page Title="Mailing Date Calculator" Language="VB" Theme="DevEx" AutoEventWireup="true" CodeFile="MailingDateCalculator.aspx.vb" Inherits="MailingDateCalculator" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>

<body>
    <form id="Form1" runat="server">
		<dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
		<div style="position:relative;top:0;left:0;">
			<table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
				<tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
					<td>
						<dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" 
							Text="Mailing Date Calculator" runat="server"></dx:ASPxLabel>
					</td>
				</tr>
			</table>
		</div>
		<br />
		<br />
		<br />
		<dx:ASPxFormLayout ID="flCalculate" runat="server" ColCount="2" 
			EnableTheming="True" Theme="DevEx" Width="500px">
			<Items>
				<dx:LayoutGroup Caption="Inputs" ColCount="2" ColSpan="2">
					<Items>
						<dx:LayoutItem Caption="Select Route">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxComboBox ID="cmbRoutes" runat="server" AutoPostBack="False" ClientInstanceName="cmbRoutes" DataSourceID="dsRoutes_SelectList" IncrementalFilteringMode="StartsWith" SelectedIndex="0" TextField="RouteCode" UseSubmitBehavior="false" ValueField="RouteID" ValueType="System.Int32" Width="150px">
										<ClearButton Visibility="Auto">
										</ClearButton>
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
						<dx:LayoutItem Caption="Select Pickup Date">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxDateEdit ID="dtPickupDate" runat="server" Width="150px">
										<TimeSectionProperties>
											<TimeEditProperties>
												<ClearButton Visibility="Auto">
												</ClearButton>
											</TimeEditProperties>
										</TimeSectionProperties>
										<ClearButton Visibility="Auto">
										</ClearButton>
									</dx:ASPxDateEdit>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem ShowCaption="False">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server">
									<dx:ASPxButton ID="btnCalculate" runat="server" Text="Calculate"
										OnClick="btnCalculate_Click">
									</dx:ASPxButton>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutGroup Caption="Optimum Delivery Days" ColCount="2" ColSpan="2">
					<Items>
						<dx:LayoutItem Caption="Pickup Day of Week">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
									<dx:ASPxTextBox ID="tbPickupDOW" runat="server" Width="150px"	
										ReadOnly="true" BackColor="WhiteSmoke">
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
						<dx:LayoutItem Caption="Optimum Delivery Day of Week">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
									<dx:ASPxTextBox ID="tbOptimumDeliveryDOW" runat="server" Width="150px"	
										ReadOnly="true" BackColor="WhiteSmoke">
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutGroup Caption="Primary City and Zip" ColCount="2" ColSpan="2">
					<Items>
						<dx:LayoutItem Caption="Primary City in Route">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
									<dx:ASPxTextBox ID="tbPrimaryCity" runat="server" Width="150px"	
										ReadOnly="true" BackColor="WhiteSmoke">
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
						<dx:LayoutItem Caption="Primary Zip5 in Route">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
									<dx:ASPxTextBox ID="tbPrimaryZip5" runat="server" Width="150px"	
										ReadOnly="true" BackColor="WhiteSmoke">
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutGroup Caption="Mailing Date Rule" ColCount="3" ColSpan="2">
					<Items>
						<dx:LayoutItem Caption="Rule Matched - City" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
									<dx:ASPxTextBox ID="tbRuleMatchedCity" runat="server" Width="150px"	
										ReadOnly="true" BackColor="WhiteSmoke">
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem Caption="Zip">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
									<dx:ASPxTextBox ID="tbRuleMatchedZip" runat="server" Width="80px" HorizontalAlign="Left">
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem Caption="Delivery Days (Weekdays)" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
									<dx:ASPxTextBox ID="tbDeliveryDays" runat="server" Width="150px"	
										ReadOnly="true" BackColor="WhiteSmoke">
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
						<dx:LayoutItem Caption="Post Office" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
									<dx:ASPxTextBox ID="tbPostOffice" runat="server" Width="150px" BackColor="WhiteSmoke" ReadOnly="True">
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutItem Caption="Tentative Mailing Date" ColSpan="2">
					<LayoutItemNestedControlCollection>
						<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
							<dx:ASPxTextBox ID="tbTenativeMailingDate" runat="server" Width="150px" BackColor="WhiteSmoke" ReadOnly="True">
							</dx:ASPxTextBox>
						</dx:LayoutItemNestedControlContainer>
					</LayoutItemNestedControlCollection>
					<CaptionCellStyle>
						<Paddings PaddingLeft="15px" />
					</CaptionCellStyle>
				</dx:LayoutItem>
				<dx:LayoutGroup Caption="Holiday Adjustment" ColCount="2" ColSpan="2">
					<Items>
						<dx:LayoutItem Caption="Holiday">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
									<dx:ASPxTextBox ID="tbHoliday" runat="server" Width="150px"	
										ReadOnly="true" BackColor="WhiteSmoke">
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
						<dx:LayoutItem Caption="Holiday Date">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">
									<dx:ASPxTextBox ID="tbHolidaytDate" runat="server" Width="150px"	
										ReadOnly="true" BackColor="WhiteSmoke">
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
						<dx:LayoutItem Caption="Slow Down Period Start">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
									<dx:ASPxTextBox ID="tbSlowDownPeriodStart" runat="server" Width="150px"	
										ReadOnly="true" BackColor="WhiteSmoke">
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
						<dx:LayoutItem Caption="Slow Down Period End">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">
									<dx:ASPxTextBox ID="tbSlowDownPeriodEnd" runat="server" Width="150px"	
										ReadOnly="true" BackColor="WhiteSmoke">
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
						<dx:LayoutItem Caption="Slow Down Days">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
									<dx:ASPxTextBox ID="tbSlowDownDays" runat="server" Width="150px"	
										ReadOnly="true" BackColor="WhiteSmoke">
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutItem Caption="Mailing Date Adjusted for Holiday" ColSpan="2">
					<LayoutItemNestedControlCollection>
						<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
							<dx:ASPxTextBox ID="tbMailingDateAdjustedForHoliday" runat="server" Width="150px" BackColor="WhiteSmoke" ReadOnly="True">
							</dx:ASPxTextBox>
						</dx:LayoutItemNestedControlContainer>
					</LayoutItemNestedControlCollection>
					<CaptionCellStyle>
						<Paddings PaddingLeft="15px" />
					</CaptionCellStyle>
				</dx:LayoutItem>
				<dx:LayoutItem Caption="Final Mailing Date (Adjusted for Weekends, Holidays, and Post Office)" ColSpan="2">
					<LayoutItemNestedControlCollection>
						<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
							<dx:ASPxTextBox ID="tbFinalMailingDate" runat="server" Width="150px" BackColor="WhiteSmoke" ReadOnly="True">
							</dx:ASPxTextBox>
						</dx:LayoutItemNestedControlContainer>
					</LayoutItemNestedControlCollection>
					<CaptionCellStyle>
						<Paddings PaddingLeft="15px" />
					</CaptionCellStyle>
				</dx:LayoutItem>
			</Items>
		</dx:ASPxFormLayout>
		<dx:ASPxLabel runat="server" ID="errorMessageLabel" Visible="false" ForeColor="Red" EnableViewState="false" Font-Size="Medium" />
		<br />
		<br />
		<asp:SqlDataSource ID="dsRoutes_SelectList" runat="server"
			ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
			SelectCommand="SELECT RouteID, RouteCode FROM tblRoutes WHERE Active = 1 ORDER BY RouteCode">
		</asp:SqlDataSource>
	</form>
</body>
</html>
