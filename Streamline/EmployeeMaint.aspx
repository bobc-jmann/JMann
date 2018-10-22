<%@ Page Title="Employee Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="EmployeeMaint.aspx.vb" Inherits="EmployeeMaint" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
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
            var height = Math.max(0, document.documentElement.clientHeight) - 100;
            grdMain.SetHeight(height);
        }
    </script>
    <script type="text/javascript">
        function OnChanging(s, e) {
            e.reloadContentOnCallback = true;
        }
    </script>
    <script type="text/javascript">
        // <![CDATA[
        var isValidUpload;

        function Uploader_OnUploadStart() {
           btnUpload.SetEnabled(false);
        }
        function Uploader_OnFileUploadComplete(args) {
            //var imgSrc = aspxPreviewImgSrc;
            if (args.isValid) {
                var imgSrc
                var date = new Date();
                imgSrc = "/" + args.callbackData + "?dx=" + date.getTime();

            }
        }
        function Uploader_OnFilesUploadComplete(args) {
            UpdateUploadButton();
        }
        function UpdateUploadButton() {
            btnUpload.SetEnabled(uploader.GetText(0) != "");
        }
        // ]]>
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
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Employee Maintenance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divGrid" class="specials" runat="server">
        <table id="tblGrid" runat="server" style="visibility: visible">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnPrint" Text="Print Badges" runat="server" OnClick="btnPrint_Click" />
               </td>
            </tr>
            <tr>
                <td style="width: 70%">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" DataSourceID="dsEmployees" KeyFieldName="EmployeeID" 
                            Width="100%" SettingsBehavior-ConfirmDelete="True" AutoGenerateColumns="false"
                            OnRowUpdating="grdMain_RowUpdating" OnRowInserting="grdMain_RowInserting" OnStartRowEditing="grdMain_StartRowEditing">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Width="80" ShowEditButton="true" ShowDeleteButton="true"/>
                            <dx:GridViewDataColumn FieldName="EmployeeID" Visible="false" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="Company" Visible="False"  EditFormSettings-Visible="True" Caption="COMPANY:" HeaderStyle-HorizontalAlign="Center" >
                                <PropertiesComboBox DataSourceID="dsCompany" TextField="Company" ValueField="Company" ValueType="System.String" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="IdNumber" Caption="ID #" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="Name" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="Position" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="Sex" HeaderStyle-HorizontalAlign="Center" Visible="false" />
                            <dx:GridViewDataColumn FieldName="DOB" HeaderStyle-HorizontalAlign="Center" Visible="false" />
                            <dx:GridViewDataColumn FieldName="Height" HeaderStyle-HorizontalAlign="Center" Visible="false" />
                            <dx:GridViewDataColumn FieldName="Weight" HeaderStyle-HorizontalAlign="Center" Visible="false" />
                            <dx:GridViewDataColumn FieldName="Hair" HeaderStyle-HorizontalAlign="Center" Visible="false" />
                            <dx:GridViewDataColumn FieldName="Eyes" HeaderStyle-HorizontalAlign="Center" Visible="false" />
                            <dx:GridViewDataColumn FieldName="EmergencyNumber" HeaderStyle-HorizontalAlign="Center" Visible="false" />
                            <dx:GridViewDataBinaryImageColumn FieldName="PhotoImage" Width="200" Visible="false" />
                        </Columns>
                        <SettingsPager PageSize="100" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <Templates>
                            <EditForm>
                                <dx:ASPxFormLayout ID="formLayout" runat="server" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="COMPANY:" FieldName="Company">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="cmbCompany" Width="100%" ValueField="Company" TextField="Company" Text='<%# Eval("Company")%>' IncrementalFilteringMode="StartsWith"  ValueType="System.String" DataSourceID="dsCompany" runat="server" AutoPostBack="False" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption=" " FieldName="PhotoImage" RowSpan="9">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxCallbackPanel ID="callbackPanel" runat="server" ClientInstanceName="efPanel" OnCallback="callbackPanel_Callback" Width="260px">
                                                        <PanelCollection>
                                                            <dx:PanelContent ID="PanelContent1" runat="server">
                                                                <dx:ASPxBinaryImage ID="previewImage" runat="server" Visible="True" Width="200px" Value='<%# Eval("PhotoImage")%>'/>
                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxCallbackPanel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
 
                                        <dx:LayoutItem Caption="ID #" FieldName="IdNumber">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox id="txtIdNumber" runat="server" Text='<%# Eval("IdNumber") %>' />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="NAME" FieldName="Name">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox id="ASPxTextBox1" runat="server" Text='<%# Eval("Name")%>' />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="POSITION" FieldName="Position">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox id="ASPxTextBox3" runat="server" Text='<%# Eval("Position")%>' />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="SEX" FieldName="Sex">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox id="txtSex" runat="server" Text='<%# Eval("Sex")%>' />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="DOB" FieldName="DOB">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxDateEdit id="dtDOB" runat="server" Value='<%# Eval("DOB")%>' 
                                                        NullText="MM/dd/yyyy" EditFormat="Date" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="HEIGHT" FieldName="Height">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox id="txtHeight" runat="server" Text='<%# Eval("Height")%>' />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="WEIGHT" FieldName="Weight">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox id="txtWeight" runat="server" Text='<%# Eval("Weight")%>' />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="HAIR" FieldName="Hair">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox id="txtHair" runat="server" Text='<%# Eval("Hair")%>' />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="EYES" FieldName="Eyes">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox id="txtEyes" runat="server" Text='<%# Eval("Eyes")%>' />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                       <dx:LayoutItem Caption=" ">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxUploadControl ID="uplImage" runat="server" ClientInstanceName="uploader" ShowProgressPanel="True"
                                                            NullText="Click here to browse files..." Size="25" OnFileUploadComplete="uplImage_FileUploadComplete"
                                                            UploadMode="Advanced">
                                                        <ClientSideEvents 
                                                            FileUploadComplete="function(s, e) { isValidUpload = e.isValid; Uploader_OnFileUploadComplete(e); }" Init="function(s, e) { isValidUpload = false; }" 
                                                            FilesUploadComplete="function(s, e) { if (isValidUpload) { efPanel.PerformCallback();} Uploader_OnFilesUploadComplete(e); }"
                                                            FileUploadStart="function(s, e) { Uploader_OnUploadStart(); }"
                                                            TextChanged="function(s, e) { UpdateUploadButton(); }">
                                                        </ClientSideEvents>
                                                        <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg,.jpeg,.jpe,.gif" />
                                                    </dx:ASPxUploadControl>
                                               </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="EMERGENCY #" FieldName="EmergencyNumber">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox id="txtEmergencyNumber" runat="server" Text='<%# Eval("EmergencyNumber")%>' />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption=" ">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="btnUpload" runat="server" AutoPostBack="False" Text="Upload" ClientInstanceName="btnUpload"
                                                            Width="100px" ClientEnabled="False" style="margin: 0 auto;">
                                                        <ClientSideEvents Click="function(s, e) { uploader.Upload(); }" />
                                                    </dx:ASPxButton>
                                                 </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                    </Items>
                                </dx:ASPxFormLayout>
                                <div>
                                </div>
                                <div style="text-align: right; padding: 2px 2px 2px 2px">
                                    <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server" />
                                    <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server" />
                                </div>
                            </EditForm>
                        </Templates>
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsEmployees" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="users.spEmployees_SelectAll"
                        SelectCommandType="StoredProcedure"
                        InsertCommand="users.spEmployees_Insert"
                        InsertCommandType="StoredProcedure"
                        UpdateCommand="users.spEmployees_Update"
                        UpdateCommandType="StoredProcedure"
                        DeleteCommand="users.spEmployees_Delete"
                        DeleteCommandType="StoredProcedure">
                        <InsertParameters>
                            <asp:Parameter Name="Company" Type="String" />
                            <asp:Parameter Name="IdNumber" Type="String" />
                            <asp:Parameter Name="Name" Type="String" />
                            <asp:Parameter Name="Position" Type="String" />
                            <asp:Parameter Name="Sex" Type="String" />
                            <asp:Parameter Name="DOB" Type="String" />
                            <asp:Parameter Name="Height" Type="String" />
                            <asp:Parameter Name="Weight" Type="String" />
                            <asp:Parameter Name="Hair" Type="String" />
                            <asp:Parameter Name="Eyes" Type="String" />
                            <asp:Parameter Name="EmergencyNumber" Type="String" />
                            <asp:SessionParameter SessionField="uploadedFileData" Name="PhotoImage" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="Company" Type="String" />
                            <asp:Parameter Name="EmployeeID" Type="Int32" />
                            <asp:Parameter Name="IdNumber" Type="String" />
                            <asp:Parameter Name="Name" Type="String" />
                            <asp:Parameter Name="position" Type="String" />
                            <asp:Parameter Name="Sex" Type="String" />
                            <asp:Parameter Name="DOB" Type="String" />
                            <asp:Parameter Name="Height" Type="String" />
                            <asp:Parameter Name="Weight" Type="String" />
                            <asp:Parameter Name="Hair" Type="String" />
                            <asp:Parameter Name="Eyes" Type="String" />
                            <asp:Parameter Name="EmergencyNumber" Type="String" />
                            <asp:SessionParameter SessionField="uploadedFileData" Name="PhotoImage" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="EmployeeID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>

                   <asp:SqlDataSource ID="dsCompany" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT 'J. MANN INC.' AS Company UNION SELECT 'ECO THRIFT ®' AS Company">
                    </asp:SqlDataSource>


                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
                </td>
                <td style="width: 30%" />
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
