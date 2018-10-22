<%@ Page Title="Map Pickups Driver" Language="VB" AutoEventWireup="false" CodeFile="MappingPickupsDriver.aspx.vb" Inherits="MappingPickupsDriver" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
	<style type="text/css">   
		html { height: 100% }   
		body { height: 100%; margin: 0px; padding: 0px; font-family: Arial; font-size: 12px; }  
		#map_canvas { height: 100% } 
 	</style> 
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyAGkJEupThN8OF0TlUo9qCNgzD4mYmYFjk"></script>
    <script type="text/javascript" src="https://cdn.rawgit.com/googlemaps/v3-utility-library/master/infobox/src/infobox.js"></script>
    <script src="Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript">
        var map;
        var mapCenter;
        var mapBounds = new google.maps.LatLngBounds();
        var debug = false;


        function initialize() {
        	var mapOptions =
			{
				zoom: 16,
				center: mapCenter,
				mapTypeId: google.maps.MapTypeId.ROADMAP
			};
        	map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);
        	var infowindow = new google.maps.InfoWindow();

        	var mapSize = document.getElementById("hfMapSize");
        	var googleCoordsString = document.getElementById("googleCoordinates").value;
        	if (googleCoordsString.length > 0) {
				// Sections
                var googleCoords = $.parseJSON(googleCoordsString);
              
                for (i = 0; i < googleCoords.length; i++) {
                    var polyCoords = [];
                    var routeSection = googleCoords[i][0][0];
                    var polyBounds = new google.maps.LatLngBounds();
                    var color = googleCoords[i][0][1];
                    for (j = 1; j < googleCoords[i].length; j++) {
                        var ll = new google.maps.LatLng(googleCoords[i][j][0], googleCoords[i][j][1]);
                        polyCoords.push(ll);
                        polyBounds.extend(ll);
                        mapBounds.extend(ll);
                    }

                    var strokeOpacity = 0.2;
                    var strokeWeight = 1;
                    var fillOpacity = 0.05;

                   	strokeOpacity = 0.6;
                   	strokeWeight = 3;
                   	fillOpacity = 0.2;
			
                    // Construct the Sections polygon.
                    var sectionPolygon;

                    sectionPolygon = new google.maps.Polygon({
                        paths: polyCoords,
                        strokeColor: color,
                        strokeOpacity: strokeOpacity,
                        strokeWeight: strokeWeight,
                        fillColor: color,
                        fillOpacity: fillOpacity
                    });

                    sectionPolygon.setMap(map);

                    var myOptions = {
                        content: googleCoords[i][0][0]
		                    , boxStyle: {
		                        border: "1px solid ".concat(color)
		                      , textAlign: "center"
		                      , fontSize: "10pt"
		                      , width: "75px"
		                    }
		                    , disableAutoPan: true
                            , pixelOffset: new google.maps.Size(-25, 0)
                            , position: polyBounds.getCenter()
		                    , closeBoxURL: ""
		                    , isHidden: false
		                    , pane: "mapPane"
		                    , enableEventPropagation: true
                    };
                    
                    var ibLabel = new InfoBox(myOptions);
                    ibLabel.open(map);
                }

                map.panTo(mapBounds.getCenter());
                map.fitBounds(mapBounds); // Don't change the zoom when resetting map size for printing.

        		// SectionAddresses
                var sectionAddressesCoordsString = document.getElementById("sectionAddressesCoordinates").value;
                if (sectionAddressesCoordsString.length > 0) {
                	var sectionAddressesCoords = $.parseJSON(sectionAddressesCoordsString);

                	image = "Resources/images/green-dot.png"; // Pickup marker
                	for (i = 0; i < sectionAddressesCoords.length; i++) {
                		var color = sectionAddressesCoords[i][2];
                		var pickup = sectionAddressesCoords[i][4];

                		if (pickup == 0) {
                			var myLatlng1 = new google.maps.LatLng(sectionAddressesCoords[i][0] + .00003, sectionAddressesCoords[i][1] - 0.000045);
                			var myLatlng2 = new google.maps.LatLng(sectionAddressesCoords[i][0] - .00003, sectionAddressesCoords[i][1] + 0.000045);

                			var strokeOpacity = 0.4;
                			var strokeWeight = 1;
                			var fillOpacity = 0.05;

                			strokeOpacity = 0.4;
                			strokeWeight = 2;
                			fillOpacity = 0.2;

                			var rectangle = new google.maps.Rectangle({
                				strokeColor: color,
                				strokeOpacity: strokeOpacity,
                				strokeWeight: strokeWeight,
                				fillColor: color,
                				fillOpacity: fillOpacity,
                				map: map,
                				bounds: new google.maps.LatLngBounds(myLatlng1, myLatlng2)
                			});
                		} else {
                			var myLatlng = new google.maps.LatLng(sectionAddressesCoords[i][0], sectionAddressesCoords[i][1]);
                			// add pickup to section
                			var marker = new google.maps.Marker({
                				position: myLatlng,
                				map: map,
                				title: sectionAddressesCoords[i][3],
                				icon: image,
                				visible: true
                			});
                		}
                	}
                }

        		// DriverLogs
                var driverCoordsString = document.getElementById("driverCoordinates").value;
                if (driverCoordsString.length > 0) {
                	var driverCoords = $.parseJSON(driverCoordsString);

                	for (i = 0; i < driverCoords.length; i++) {
                		var driverLocations = [];
                		for (j = 0; j < driverCoords[i].length; j++) {
                			var point = new google.maps.LatLng(driverCoords[i][j][0], driverCoords[i][j][1])
                			driverLocations.push(point);
                		}
                		var driverPath = new google.maps.Polyline({
                			path: driverLocations,
							geodesic: true,
                			strokeColor: "black",
                			strokeOpacity: 0.4,
                			strokeWeight: 2,
                		});
                		driverPath.setMap(map);
                	}
                }


        		// Play Addresses
                //var myLatlng = { lat: 34.021674, lng: -118.1484 };

                //var marker = new google.maps.Marker({
                //	position: myLatlng,
                //	map: map,
                //	title: 'My Test Address',
                //	icon: "Resources/images/red-pushpin.png",
                //});
         	}

        	// Restore DrawMap button
        	var btn = document.getElementById("btnDrawMap");
        	btn.Text = "Draw Map";
        }
        google.maps.event.addDomListener(window, 'load', initialize);

        function ButtonDrawMap(id) {
        	if (id.value == "Processing...")
        		return;	// prevent repeated button pushing
        	id.value = "Processing...";
		}

	</script>
    <style>
        .customBox {
            background: yellow;
            border: 1px solid black;
            position: absolute;

        }
    </style>

</head>

<body">
    <form runat="server" style="width:100%">
	<div id="divMain" style="width:100%;">
		<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
		<asp:HiddenField ID="googleCoordinates" runat="server" ClientIDMode="Static" />
		<asp:HiddenField ID="sectionAddressesCoordinates" runat="server" ClientIDMode="Static" />
		<asp:HiddenField ID="driverCoordinates" runat="server" ClientIDMode="Static" />
	</div> 

    <div id="divSearch" class="specials" runat="server">
        <table width="60%">
			<tr>
                <td style="width: 25%">Driver Location:
				    <asp:DropDownList ID="ddlDriverLocations" runat="server" ToolTip="Driver Location" Width="100%" AutoPostBack="True">
					</asp:DropDownList>
                    <asp:HiddenField ID="hfSQL_ddlDriverLocations" runat="server" Value=""></asp:HiddenField>
                    <asp:SqlDataSource ID="dsDriverLocations" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
				</td>
				<td style="width: 25%">
                    <asp:Label ID="lblPickupDateFrom" runat="server" Text="Pickup Date From:"></asp:Label>
                    <dx:ASPxDateEdit ID="dtPickupDateFrom" ClientInstanceName="dtPickupDateFrom"
                        Width="100%" ToolTip="Pickup Date From" EditFormat="Custom" 
                        EditFormatString="MM/dd/yyyy" ValidationSettings-SetFocusOnError="True" 
                        ValidationSettings-RegularExpression-ErrorText="Invalid date" 
                        ValidationSettings-ErrorImage-Url="~/Resources/Images/iconError.png" 
                        runat="server" AutoPostBack="True">
                        <ValidationSettings SetFocusOnError="True">
                            <ErrorImage Url="~/Resources/Images/iconError.png"></ErrorImage>
                            <RegularExpression ErrorText="Invalid date"></RegularExpression>
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
 			    </td>
				<td style="width: 25%">
                    <asp:Label ID="Label1" runat="server" Text="Pickup Date To:"></asp:Label>
                    <dx:ASPxDateEdit ID="dtPickupDateTo" ClientInstanceName="dtPickupDateTo"
                        Width="100%" ToolTip="Pickup Date To" EditFormat="Custom" 
                        EditFormatString="MM/dd/yyyy" ValidationSettings-SetFocusOnError="True" 
                        ValidationSettings-RegularExpression-ErrorText="Invalid date" 
                        ValidationSettings-ErrorImage-Url="~/Resources/Images/iconError.png" 
                        runat="server" AutoPostBack="True">
                        <ValidationSettings SetFocusOnError="True">
                            <ErrorImage Url="~/Resources/Images/iconError.png"></ErrorImage>
                            <RegularExpression ErrorText="Invalid date"></RegularExpression>
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
 			    </td>
				<td style="width: 25%">
					<dx:ASPxCheckBox ID="ckShowDriverLogs" runat="server" Text="Show Driver Logs" TextAlign="Left"></dx:ASPxCheckBox>
				</td>
            </tr>
        </table>

        <table id="tblPickupSchedules" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 100%" colspan="5">
                   <dx:ASPxGridView ID="grdDriverAssignments" KeyFieldName="DriverAssignmentID" runat="server" 
					   DataSourceID="dsDriverAssignments" Width="100%" EnableRowsCache="true">
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Width="50" Caption=" " ShowClearFilterButton="true" ShowSelectCheckbox="True" />
                            <dx:GridViewDataColumn FieldName="DriverAssignmentID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="RegionID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="RouteID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="SectionID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="PickupDate" VisibleIndex="1" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="RouteCode" VisibleIndex="4" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="SectionCode" VisibleIndex="5" HeaderStyle-Wrap="True"  />
                            <dx:GridViewDataColumn FieldName="DriverName" VisibleIndex="6" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataTextColumn FieldName="CntPickupsDriver" VisibleIndex="21" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CntPickupsAddresses" VisibleIndex="22" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="SoftCarts" VisibleIndex="31" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#.00" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="HardCarts" VisibleIndex="32" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#.00" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="TotalCarts" VisibleIndex="33" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#.00" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CntPutOuts" VisibleIndex="33" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="PickupPercent" VisibleIndex="41" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#.00%" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CartsPer1000" VisibleIndex="42" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#.00" />               
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior ColumnResizeMode="Control" />
                            <SettingsPager PageSize="10">
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="False" ShowFooter="True" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsDriverAssignments" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
				</td>
            </tr>
		</table>   

		<table id="tblDrawMap" runat="server" style="width:20%">
			<tr>
				<td>
					<asp:Button ID="btnDrawMap" Text="Draw Map" runat="server" OnClientClick="ButtonDrawMap(this);" />
				</td>
			</tr>
		</table>
	</div>
    </form>
	<div id="map_canvas" runat="server" style="height: 100%; width: 100%"></div>
</body>
</html>


