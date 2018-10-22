<%@ Page Title="ETL Monitor" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="MonitorETL.aspx.vb" Inherits="MonitorETL" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
	<style>
		.dxgvFocusedRow_DevEx td.dxgv {
			background: #FFF none;
		}

	</style>
    <script>
        function jsCallHideLoadingPanel() {
            parent.jsHideLoadingPanel();
        }
    </script>
    <script type="text/javascript">
        function OnInit(s, e) {
            AdjustSize();
        }
        function OnEndCallback(s, e) {
            AdjustSize();
        }
        function OnControlsInitialized(s, e) {
            ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                AdjustSize();
            });
        }
        function AdjustSize() {
            var height = Math.max(0, document.documentElement.clientHeight) - 160;
            grdMain.SetHeight(height);
        }

        function OnEndCallbackDetail(s, e) {
        	grdMain.Refresh();
        }
    </script>
</head>

<body>
    <form id="Form1" runat="server">
        <%  %>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="Scripts/jquery-1.4.1.min.js" />
        </Scripts>
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="glob" runat="server">
        <ClientSideEvents ControlsInitialized="function (s, e) { jsCallHideLoadingPanel(); }" />
    </dx:ASPxGlobalEvents>
    <div style="position:relative;top:0;left:0;">
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding="2" cellspacing="0" style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="ETL Monitor" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
 	<dx:ASPxHiddenField ID="hf" runat="server" />
	<dx:aspxformlayout ID="fl" runat="server" ColCount="1" EnableTheming="True" Theme="DevEx" Width="80%">
		<Items>
			<dx:LayoutItem ShowCaption="False">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer>
						<dx:ASPxGridView ID="grdSMS" ClientInstanceName="grdSMS" runat="server" DataSourceID="dsSMS" 
							KeyFieldName="JobName" Width="100%" EnableRowsCache="false"
                            OnCustomButtonInitialize="grdSMS_CustomButtonInitialize">
							<Columns>
								<dx:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="60" >
									<CustomButtons>
										<dx:GridViewCommandColumnCustomButton ID="cbStartJobSMS" Text="Start Job" Visibility="AllDataRows"/>
									</CustomButtons>
								</dx:GridViewCommandColumn>
								<dx:GridViewDataTextColumn FieldName="JobName" ReadOnly="true" Width="70px">
									<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="Running" ReadOnly="true" Width="70px">
									<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="LastRunDateTime" ReadOnly="true" Width="70px">
									<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="LastRunStatus" ReadOnly="true" Width="70px">
									<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="LastRunDuration (HH:MM:SS)" ReadOnly="true" Width="70px">
									<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="NextRunDateTime" ReadOnly="true" Width="70px">
									<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
							</Columns>
							<SettingsPager PageSize="200" AlwaysShowPager="false" />
						</dx:ASPxGridView>
					</dx:LayoutItemNestedControlContainer>
				</LayoutItemNestedControlCollection>
			</dx:LayoutItem>
			<dx:EmptyLayoutItem />
			<dx:LayoutItem ShowCaption="False">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer>
						<dx:ASPxGridView ID="grdTOS" ClientInstanceName="grdTOS" runat="server" DataSourceID="dsTOS" 
							KeyFieldName="JobName" Width="100%" EnableRowsCache="false"
                            OnCustomButtonInitialize="grdTOS_CustomButtonInitialize">
							<Columns>
								<dx:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="60" >
									<CustomButtons>
										<dx:GridViewCommandColumnCustomButton ID="cbStartJobTOS" Text="Start Job" Visibility="AllDataRows"/>
									</CustomButtons>
								</dx:GridViewCommandColumn>
								<dx:GridViewDataTextColumn FieldName="JobName" ReadOnly="true" Width="70px">
									<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="Running" ReadOnly="true" Width="70px">
									<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="LastRunDateTime" ReadOnly="true" Width="70px">
									<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="LastRunStatus" ReadOnly="true" Width="70px">
									<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="LastRunDuration (HH:MM:SS)" ReadOnly="true" Width="70px">
									<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="NextRunDateTime" ReadOnly="true" Width="70px">
									<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
							</Columns>
							<SettingsPager PageSize="200" AlwaysShowPager="false" />
						</dx:ASPxGridView>
					</dx:LayoutItemNestedControlContainer>
				</LayoutItemNestedControlCollection>
			</dx:LayoutItem>
		</Items>
	</dx:aspxformlayout>

	<asp:HiddenField ID="SMSJobs" Value="JMannETL" runat="server" />
	<asp:HiddenField ID="SMSServer" Value="[JMANN-SQL]" runat="server" />
	<asp:HiddenField ID="TOSJobs" Value="Data Transfer A', 'Data Transfer B', 'Data Transfer C', 'Data Transfer D" runat="server" />
	<asp:HiddenField ID="TOSServer" Value="[JMANN-SQL\THRIFTOS]" runat="server" />

	<asp:SqlDataSource ID="dsSMS" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="spAgentJobStatus"
		SelectCommandType="StoredProcedure">
		<SelectParameters>
			<asp:ControlParameter ControlID="SMSJobs" Name="jobNames" PropertyName="Value" Type="String" />
			<asp:ControlParameter ControlID="SMSServer" Name="serverName" PropertyName="Value" Type="String" />
		</SelectParameters>
	</asp:SqlDataSource>

	<asp:SqlDataSource ID="dsTOS" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
		SelectCommand="spAgentJobStatus"
		SelectCommandType="StoredProcedure">
		<SelectParameters>
			<asp:ControlParameter ControlID="TOSJobs" Name="jobNames" PropertyName="Value" Type="String" />
			<asp:ControlParameter ControlID="TOSServer" Name="serverName" PropertyName="Value" Type="String" />
		</SelectParameters>
	</asp:SqlDataSource>

	<dx:ASPxGlobalEvents ID="ge" runat="server">
		<ClientSideEvents ControlsInitialized="OnControlsInitialized" />
	</dx:ASPxGlobalEvents>

    </form>
</body>
</html>
