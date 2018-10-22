<%@ Page Title="Specials Assignments" Language="VB" AutoEventWireup="false" CodeFile="SpecialAssignments.aspx.vb" Inherits="SpecialAssignments" %>

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
        var debug = true;
        var markers = {};


        // Toggle
        function toggleMarkerIcons(marker) {
        	for (var key in markers) {
        		if (markers[key].marker.addressID == marker.addressID) {
        			if (markers[key].selected) {
        				marker.setIcon(markers[key].icon);
        				markers[key].selected = false;
        			}
        			else {
        				marker.setIcon("Resources/images/red.png");
        				markers[key].selected = true;
        			}
        			break;
        		}
        	}
        }

    	// Deselect all set marker icons
        function deselectMarkerIcons() {
        	for (var key in markers) {
        		if (markers[key].selected) {
        			markers[key].marker.setIcon(markers[key].icon)
        			markers[key].selected = false;
        		}
            }
        }

        // Change selected marker icons to image
        function changeMarkerIcons(image) {
        	for (var key in markers) {
        		if (markers[key].selected) {
        			markers[key].marker.setIcon(image)
                    title = markers[key].marker.title.replace(/\(.*?\)/, "(".concat(markers[key].marker.selectedDriverName).concat(")"));
        			markers[key].marker.title = title;
        			markers[key].selected = false;
        		}
        	}
        }

        // Create a list of selected AddressIDs
        function GetSelectedAddressIDs() {
        	var addressIDs = ""
        	for (var key in markers) {
        		if (markers[key].selected) {
        			if (addressIDs != "")
        				addressIDs = addressIDs.concat(",");
        			addressIDs = addressIDs.concat(key);
        		}
        	}
        	return addressIDs;
        }

       $(document).ready(function () {
        	$(document).on('click', '#btnUpdate', function () {
     			var addressIDs = GetSelectedAddressIDs();
        		if (addressIDs == "") {
        			alert("Please select at least one address.");
        			return;
        		}
                PageMethods.UpdateSpecials(hf.Get("selectedDriverID"), addressIDs, OnUpdateSpecialsSuccess, OnUpdateSpecialsError)
                return false;
        	});

        	function OnUpdateSpecialsSuccess(result)
        	{
        		if (result >= 0) {
        			var addressIDs = GetSelectedAddressIDs();
        			var icon = "Resources/images/green-dot.png";
        			changeMarkerIcons(icon);
        		}
        		else
                    alert('UpdateSpecials Error');
                drivers.Refresh();
        	}
 
        	function OnUpdateSpecialsError(result)
        	{
       			alert('OnUpdateSpecials Error');
        	}

        	$(document).on('click', '#btnDeselect', function () {
                deselectMarkerIcons();
                return false;
        	});
		});




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

        		// SpecialAddresses
                var specialAddressesCoordsString = document.getElementById("specialAddressesCoordinates").value;
                if (specialAddressesCoordsString.length > 0) {
                	var specialAddressesCoords = $.parseJSON(specialAddressesCoordsString);
                    for (i = 0; i < specialAddressesCoords.length; i++) {
                	    var addressID = specialAddressesCoords[i][5];
                        var selectedDriverName = specialAddressesCoords[i][2];
                        var image = "Resources/images/" + specialAddressesCoords[i][4];
                 		var myLatlng = new google.maps.LatLng(specialAddressesCoords[i][0], specialAddressesCoords[i][1]);
                		var marker = new google.maps.Marker({
                			position: myLatlng,
                			map: map,
                            title: specialAddressesCoords[i][3],
                            addressID: addressID,
                            selectedDriverName: selectedDriverName,
                			icon: image,
                			visible: true
                         });

                        // add marker to object
                	    markers[addressID] = {};
                        markers[addressID].icon = image;
                        markers[addressID].selected = false;
                		markers[addressID].marker = marker;

                        google.maps.event.addListener(marker, 'click', function () {
                 			toggleMarkerIcons(this);
                		});

                        mapBounds.extend(myLatlng);
                    }
                }

                map.panTo(mapBounds.getCenter());
                map.fitBounds(mapBounds); // Don't change the zoom when resetting map size for printing.
         	}

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
        .right-element {
            position: absolute;
            left: 21%;
            top: 0;
            height: 100%;
            width: 79%;
        }

    </style>

</head>

<body">
    <form runat="server" style="width:20%">
	<div id="divMain" style="width:100%;">
		<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
		<asp:HiddenField ID="googleCoordinates" runat="server" ClientIDMode="Static" />
		<asp:HiddenField ID="specialAddressesCoordinates" runat="server" ClientIDMode="Static" />
 		<dx:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hf" />
    </div>
    <div id="divSearch" class="specials" runat="server">
        <table style="width: 100%">
			<tr>
                <td style="width: 100%">Driver Location:
				    <asp:DropDownList ID="ddlDriverLocations" runat="server" ToolTip="Driver Location" Width="100%" AutoPostBack="True">
					</asp:DropDownList>
                    <asp:HiddenField ID="hfSQL_ddlDriverLocations" runat="server" Value=""></asp:HiddenField>
                    <asp:SqlDataSource ID="dsDriverLocations" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
				</td>
            </tr>
            <tr>
				<td>
                    <asp:Label ID="lblPickupDate" runat="server" Text="Pickup Date:"></asp:Label>
                    <dx:ASPxDateEdit ID="dtPickupDate" ClientInstanceName="dtPickupDate"
                        Width="100%" ToolTip="Pickup Date" EditFormat="Custom" 
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
            </tr>
            <tr>
                <td>
                    <asp:Label ID="totalSpecials" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="unassignedSpecials" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Click on Driver Name to display map.
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxGridView ID="drivers" ClientInstanceName="drivers" runat="server" 
						DataSourceID="dsDrivers" KeyFieldName="DriverID" Width="100%" 
						EnableRowsCache="True" EnableViewState="false"
                        OnCustomCallback="drivers_CustomCallback1"
                        EnableCallBacks="True">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="DriverName" Width="70%" /> 
                            <dx:GridViewDataColumn FieldName="CntSections" Caption="Sect" Width="15%" HeaderStyle-HorizontalAlign="Right" /> 
                            <dx:GridViewDataColumn FieldName="CntSpecials" Caption="Spec" Width="15%" HeaderStyle-HorizontalAlign="Right" /> 
                        </Columns>
                        <SettingsEditing Mode="EditForm" />
                        <SettingsPager PageSize="20" AlwaysShowPager="False" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="500" ShowFooter="true" />
                        <TotalSummary>
                            <dx:ASPxSummaryItem FieldName="CntSections" SummaryType="Sum" DisplayFormat="{0}" />
                            <dx:ASPxSummaryItem FieldName="CntSpecials" SummaryType="Sum" DisplayFormat="{0}" />
                        </TotalSummary>
                        <ClientSideEvents RowClick="function(s, e){ s.PerformCallback(e.visibleIndex); }" />
    					<ClientSideEvents EndCallback="function(s, e){ __doPostBack('Map', 'Draw'); }" />
	                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsDrivers" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
 			    </td>
            </tr>
        </table>
        <br />
        <table id="tblDrawMap" runat="server" style="width:100%">
			<tr>
				<td style="width: 50%">
					<asp:Button ID="btnUpdate" Text="Update" runat="server" Width="100%" Height="50px" />
				</td>
				<td style="width: 50%">
					<asp:Button ID="btnDeselect" Text="Unselect All" runat="server" Width="100%" Height="50px" />
				</td>
			</tr>
		</table>
        <br/>
        <table style="width: 100%">
            <tr>
                <td style="width: 33%; text-align: center">
                    <img src="Resources/images/ylw-pushpin.png" />
                </td>
                <td style="width: 33%; text-align: center">
                    <img src="Resources/images/green-dot.png" />
                </td>
                <td style="width: 34%; text-align: center">
                    <img src="Resources/images/ltblue-dot.png" />
                </td>
            </tr>
            <tr>
                <td style="width: 33%; text-align: center">
                    Unassigned
                </td>
                <td style="width: 33%; text-align: center">
                    Assigned to Selected Driver
                </td>
                <td style="width: 33%; text-align: center">
                    Assigned to Other Driver
                </td>
            </tr>
        </table>

	</div>
    </form>
    <div id="map_canvas" runat="server" class="right-element" />
</body>
</html>


