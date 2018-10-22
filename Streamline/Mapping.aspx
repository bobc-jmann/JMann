<%@ Page Title="Mapping" Language="VB" AutoEventWireup="false" CodeFile="Mapping.aspx.vb" Inherits="Mapping" %>

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
        .btn {
		  background: #3498db;
		  background-image: -webkit-linear-gradient(top, #3498db, #2980b9);
		  background-image: -moz-linear-gradient(top, #3498db, #2980b9);
		  background-image: -ms-linear-gradient(top, #3498db, #2980b9);
		  background-image: -o-linear-gradient(top, #3498db, #2980b9);
		  background-image: linear-gradient(to bottom, #3498db, #2980b9);
		  border-radius: 28px;
		  font-family: Arial;
		  color: #ffffff;
		  font-size: 14px;
		  text-align: center;
		  padding: 10px 20px 10px 20px;
		  margin: 4px 7px 2px 4px;
		  text-decoration: none;
		}

		.btn:hover {
		  background: #3cb0fd;
		  background-image: -webkit-linear-gradient(top, #3cb0fd, #3498db);
		  background-image: -moz-linear-gradient(top, #3cb0fd, #3498db);
		  background-image: -ms-linear-gradient(top, #3cb0fd, #3498db);
		  background-image: -o-linear-gradient(top, #3cb0fd, #3498db);
		  background-image: linear-gradient(to bottom, #3cb0fd, #3498db);
		  text-decoration: none;
		}
	</style> 
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyAGkJEupThN8OF0TlUo9qCNgzD4mYmYFjk"></script>
    <script type="text/javascript" src="https://cdn.rawgit.com/googlemaps/v3-utility-library/master/infobox/src/infobox.js"></script>
    <script src="Scripts/jquery-2.1.1.js"></script>
    <script type="text/javascript">
		function jsNewMap(t) {
		    document.thisForm.submit()
		}

		function OnNearbyDistanceValidation(s, e) {
			var distance = e.value;
			if (distance == null || distance == "")
				return;
			var digits = "0123456789";
			var decimal = 0;
			for (var i = 0; i < distance.length; i++) {
				if (digits.indexOf(distance.charAt(i)) == -1) {
					if ((distance.charAt(i) == "." && decimal > 0) || (distance.charAt(i) != ".")) {
						e.isValid = false;
						break;
					}
					else
						decimal += 1;
				}
			}
			if (e.isValid && distance.charAt(0) == '0') {
				distance = distance.replace(/^0+/, "");
				if (distance.length == 0)
					distance = "0";
				e.value = distance;
			}
			if (distance > 5)
				e.isValid = false;
		}
	</script>

    <script type="text/javascript">
        var map;
        var mapCenter;
        var mapBounds = new google.maps.LatLngBounds();
        var markers = {};
        var carrierRoutes = [];
        var carrierRouteLabels = [];
        var debug = false;

        // Toggle Non-Route Address display
        function toggleNonRouteAddresses(s, e) {
        	if (s.GetValue()) {
        		for (var key in markers) {
        			markers[key].marker.setVisible(true);
        		}
        	}
        	else {
        		for (var key in markers) {
        			markers[key].marker.setVisible(false);
        		}
        	}
        }

        // Toggle Carrier Routes display
        function toggleCarrierRoutes(s, e) {
        	if (s.GetValue()) {
        		for (i = 0; i < carrierRoutes.length; i++) {
        			carrierRoutes[i].setMap(map);
        			carrierRouteLabels[i].open(map);
        		}
        	}
        	else {
        		for (i = 0; i < carrierRoutes.length; i++) {
        			carrierRoutes[i].setMap(null);
        			carrierRouteLabels[i].close();
        		}
        	}
		}

        // Get Marker Image by Status
        function getMarkerIcon(statusID) {
        	if (statusID == "1" || statusID == "2") // CURRENT, BACKUP
        		icon = "Resources/images/green-dot.png";
        	else if (statusID == "0" || statusID == "11" || statusID == "14") // <Unknown>, NONE, ROUTED
        		icon = "Resources/images/green.png";
        	else if (statusID == "17") // APARTMENT
        		icon = "Resources/images/lightblue.png";
        	else if (statusID == "31") // PRIVATE PROPERTY
        		icon = "Resources/images/ltblu-pushpin.png";
        	else if (statusID == "9") // GATED
        		icon = "Resources/images/ltblue-dot.png";
        	else if (statusID == "23") // COMMERCIAL
        		icon = "Resources/images/orange.png";
        	else if (statusID == "32") // DOWNTOWN
        		icon = "Resources/images/orange-dot.png";
        	else if (statusID == "20" || status == "39") // COUNTRY RD, DESERT
        		icon = "Resources/images/pink-dot.png";
        	else if (statusID == "19") // NARROW STREET
        		icon = "Resources/images/pink.png";
        	else if (statusID == "25") // M.H.P.
        		icon = "Resources/images/pink-pushpin.png";
        	else if (statusID == "33") // DUPLEX/CONDO
        		icon = "Resources/images/purple-dot.png";
        	else if (statusID == "22") // MOUNTAINOUS
        		icon = "Resources/images/purple.png";
        	else if (statusID == "21") // UNSAFE ROADS
        		icon = "Resources/images/red-dot.png";
        	else if (statusID == "12") // NOT GOOD
        		icon = "Resources/images/red.png";
        	else if (statusID == "27") // NO TURN AROUND
        		icon = "Resources/images/red-pushpin.png";
        	else if (statusID == "28") // MAIN/BUSY RD
        		icon = "Resources/images/yellow-dot.png";
        	else if (statusID == "6") // BAD AREA
        		icon = "Resources/images/red-pushpin.png";
        	else if (statusID == "40") // LONG DRIVEWAY
        		icon = "Resources/images/ylw-pushpin.png";
        	else
        		icon = "Resources/images/purple-pushpin.png";

        	return icon;
        }

        // Toggle
        function toggleMarkerIcons(marker) {
        	var mappingUpdate = document.getElementById("mappingUpdate");
        	if (mappingUpdate.value == "false")
        		return;

        	for (var key in markers) {
        		if (markers[key].marker.addressID == marker.addressID) {
        			if (markers[key].selected) {
        				marker.setIcon(markers[key].icon);
        				markers[key].selected = false;
        			}
        			else {
        				marker.setIcon("Resources/images/flag.png");
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
        			var st = document.getElementById("ddlStatusesUpdate");
        			var status = st[st.selectedIndex].text;
        			title = markers[key].marker.title.replace(/\(.*?\)/, "(".concat(status).concat(")"));
        			markers[key].marker.title = title;
        			markers[key].selected = false;
        		}
        	}
        }

        // Change selected marker icons to Current
        function changeMarkerIconsCurrent() {
        	var sd = document.getElementById("ddlSections");
        	var sdid = sd[sd.selectedIndex].value;
        	var su = document.getElementById("ddlSectionUpdate");
        	var suid = su[su.selectedIndex].value;
        	for (var key in markers) {
        		if (markers[key].selected) {
        			if (sdid == suid) {	// Addresses are going in current section
        				markers[key].marker.setIcon("Resources/images/grn-pushpin.png")
        				title = markers[key].marker.title.replace(/\(.*?\)/, "(".concat("in Current Section").concat(")"));
        				markers[key].marker.title = title;
        				markers[key].selected = false;
        			}
        			else {	// Addresses are going into another section, so remove them from display
       					markers[key].marker.setMap(null);
        				delete markers[key];
   					}
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

         		var sut = document.getElementById("tblStatusesUpdate");
        		if (sut.style.display == "block") {
        			var st = document.getElementById("ddlStatusesUpdate");
        			if (st[st.selectedIndex].value == "0") {
        				alert("Please select a Status.");
        				return;
        			}
        			var addressIDs = GetSelectedAddressIDs();
        			if (addressIDs == "") {
        				alert("Please select at least one address.");
        				return;
        			}
        			PageMethods.UpdateStatuses(st[st.selectedIndex].value, addressIDs, OnUpdateStatusSuccess, OnUpdateStatusError)
        		}
        		else {
        			var su = document.getElementById("ddlSectionUpdate")
        			if (su[su.selectedIndex].value <= 0) {
        				alert("Please select a Template, Route, and Section.");
        				return;
        			}
        			var addressIDs = GetSelectedAddressIDs();
        			if (addressIDs == "") {
        				alert("Please select at least one address.");
        				return;
        			}
        			var userID = document.getElementById("userID");
        			var uncommittedChanges = document.getElementById("hfUncommittedChanges");
        			var uc = false;
        			if (uncommittedChanges.value == '1')
        				uc = true;

        			PageMethods.UpdateSection(su[su.selectedIndex].value, addressIDs, userID.value, uc, OnUpdateSectionSuccess, OnUpdateSectionError)
        		}
        	});

        	function OnUpdateStatusSuccess(result)
        	{
        		if (result >= 0) {
        			var addressIDs = GetSelectedAddressIDs();
        			var icon = getMarkerIcon(result);
        			changeMarkerIcons(icon);
        		}
        		else
        			alert('UpdateStatuses Error');
        	}
 
        	function OnUpdateStatusError(result)
        	{
       			alert('OnUpdateStatus Error');
        	}
			
        	function OnUpdateSectionSuccess(result) {
        		if (result >= 0) {
        			var addressIDs = GetSelectedAddressIDs();
        			changeMarkerIconsCurrent();
        		}
        		else
        			alert('UpdateSection Error');
        	}

        	function OnUpdateSectionError(result) {
        		alert('OnUpdateSection Error');
        	}

        	$(document).on('click', '#btnDeselect', function () {
        		deselectMarkerIcons();
        	});


        	var zoomLevel = document.getElementById("hfZoomLevel");
        	var mapOptions =
			{
				zoom: 16,
				center: mapCenter,
				mapTypeId: google.maps.MapTypeId.ROADMAP
			};
        	map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);
        	var infowindow = new google.maps.InfoWindow();

        	// Add Title
        	mapTitle = document.getElementById("hfMapTitle");

         	var myTitle = document.createElement('p');
         	myTitle.style.color = 'black';
         	myTitle.style.fontFamily = 'Times New Roman';
         	myTitle.style.fontSize = '12';
         	myTitle.innerHTML = document.getElementById("hfMapTitle").value;
        	var myTextDiv = document.createElement('div');
        	myTextDiv.appendChild(myTitle);

        	map.controls[google.maps.ControlPosition.TOP_LEFT].push(myTextDiv);

        	// Start drag rectangle to select markers !!!!!!!!!!!!!!!!
        	var shiftPressed = false;


        	map.addListener('zoom_changed', function (e) {
        		var zoom = map.getZoom();
        		document.getElementById("zoomLevel").innerHTML = zoom;
        		return;
        	});

        	var zoom = map.getZoom();
        	document.getElementById("zoomLevel").innerHTML = zoom;

        //	$(window).keydown(function (evt) {
        //		if (evt.which === 16) { // shift
        //			shiftPressed = true;
        //		}
        //	}).keyup(function (evt) {
        //		if (evt.which === 16) { // shift
        //			shiftPressed = false;
        //		}
        //	});

        //	var mouseDownPos,
		//		gribBoundingBox = null,
		//		mouseIsDown = 0;
        //	var themap = map;

        //	google.maps.event.addListener(themap, 'mousemove', function (e) {
        //		var mappingUpdate = document.getElementById("mappingUpdate");
        //		if (mappingUpdate.value == "False")
        //			return;

        //		if (mouseIsDown && (shiftPressed || gribBoundingBox != null)) {
        //			if (gribBoundingBox != null && debug) {
        //				console.log("1. sp ".concat(shiftPressed).concat(" gb not null"));
        //			}
        //			else {
        //				console.log("1. sp ".concat(shiftPressed).concat(" gb is null"));
        //			}

        //			if (gribBoundingBox !== null) // box exists
        //			{
        //				if (debug)
        //					console.log("move");
        //				var newbounds = new google.maps.LatLngBounds(mouseDownPos, null);
        //				newbounds.extend(e.latLng);
        //				gribBoundingBox.setBounds(newbounds); // If this statement is enabled, I lose mouseUp events

        //			}
        //			else // create bounding box
        //			{
        //				if (debug)
        //					console.log("first move");
        //				gribBoundingBox = new google.maps.Rectangle({
        //					map: themap,
        //					bounds: null,
        //					fillOpacity: 0.15,
        //					strokeWeight: 0.9,
        //					clickable: false
        //				});
        //			}
        //		}
 
        //	});

        //	google.maps.event.addListener(themap, 'mousedown', function (e) {
        //		var mappingUpdate = document.getElementById("mappingUpdate");
        //		if (mappingUpdate.value == "false")
        //			return;
				
		//		if (debug)
	    //    		console.log("in Mousedown");
        //		if (shiftPressed) {
        //			if (debug)
        //				console.log("mouseIsDown = 1");
        //			mouseIsDown = 1;
        //			mouseDownPos = e.latLng;
        //			themap.setOptions({
        //				draggable: false
        //			});
        //		}
        //	});

        //	google.maps.event.addListener(themap, 'mouseup', function (e) {
        //		var mappingUpdate = document.getElementById("mappingUpdate");
        //		if (mappingUpdate.value == "false")
        //			return;

        //		if (debug)
        //			console.log("in Mouseup");
        //		if (gribBoundingBox != null && debug) {
        //			console.log("2. sp ".concat(shiftPressed).concat(" gb not null"));
        //		}
        //		else {
        //			console.log("2. sp ".concat(shiftPressed).concat(" gb is null"));
        //		}

        //		if (mouseIsDown && (shiftPressed)) {
        //			mouseIsDown = 0;
        //			if (gribBoundingBox !== null) // box exists
        //			{
		//				try {
		//					var boundsSelectionArea = new google.maps.LatLngBounds(gribBoundingBox.getBounds().getSouthWest(), gribBoundingBox.getBounds().getNorthEast());

		//					for (var key in markers) { // looping through my Markers Collection	
		//						if (gribBoundingBox.getBounds().contains(markers[key].marker.getPosition())) {
		//							if (!markers[key].selected) {
		//								markers[key].marker.setIcon("Resources/images/flag.png")
		//								markers[key].selected = true;
		//							}
		//						}
		//					}
        //				}
        //				catch (err) {
        //				}

        //				gribBoundingBox.setMap(null); // remove the rectangle
        //			}
        //			if (debug)
        //				console.log("gribBoundingBox = null");
        //			gribBoundingBox = null;
       	//	}

        //		themap.setOptions({
        //			draggable: true
        //		});
	    //   	});
		});


        function initialize() {
        	var mapSize = document.getElementById("hfMapSize");
        	var zoomLevel = document.getElementById("hfZoomLevel");
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

                    if (color == "orange" || color == "yellow") {
                    	strokeOpacity = 0.4;
                    	fillOpacity = 0.2
                    }
                    if (mapSize.value == "0") {
                    	strokeOpacity = 0.6;
                    	strokeWeight = 3;
                    	fillOpacity = 0.2;
                    }
			
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

                    //google.maps.event.addListener(sectionPolygon, 'click', function (event) {
                    //	alert("click on polygon");
                    //});
 
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

            	// Zoom Level
                if (mapSize.value == "0") {
                	map.fitBounds(mapBounds); // Don't change the zoom when resetting map size for printing.
                	zoomLevel.value = map.getZoom()
                	//var zoomText = document.getElementById("txtZoomLevel");
                	//zoomText.value = zoomLevel.value;
                }

                //alert(zoomLevel.value);
                //alert(map.getZoom());

              	// Nearby Sections
                var nearbySectionsCoordsString = document.getElementById("nearbySectionsCoordinates").value;
                if (nearbySectionsCoordsString.length > 0) {
                	var nearbySectionsCoords = $.parseJSON(nearbySectionsCoordsString);

                	for (i = 0; i < nearbySectionsCoords.length; i++) {
                		var polyCoords = [];
                		var routeSection = nearbySectionsCoords[i][0][0];
                		var polyBounds = new google.maps.LatLngBounds();
                		var color = 'Grey';
                		for (j = 1; j < nearbySectionsCoords[i].length; j++) {
                			var ll = new google.maps.LatLng(nearbySectionsCoords[i][j][0], nearbySectionsCoords[i][j][1]);
                			polyCoords.push(ll);
                			polyBounds.extend(ll);
                			mapBounds.extend(ll);
                		}

                		// Construct the polygon.
                		var sectionPolygon;
                		sectionPolygon = new google.maps.Polygon({
                			paths: polyCoords,
                			strokeColor: color,
                			strokeOpacity: 0.6,
                			strokeWeight: 3,
                			fillColor: color,
                			fillOpacity: 0.2
                		});

                		sectionPolygon.setMap(map);

                		var myOptions = {
                			content: nearbySectionsCoords[i][0][0]
								, boxStyle: {
									border: "1px solid ".concat('Grey')
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
                }

            	// SectionAddresses
                var sectionAddressesCoordsString = document.getElementById("sectionAddressesCoordinates").value;
                if (sectionAddressesCoordsString.length > 0) {
                	var sectionAddressesCoords = $.parseJSON(sectionAddressesCoordsString);
                	var image;

                 	for (i = 0; i < sectionAddressesCoords.length; i++) {
                 		//var myLatlng1 = new google.maps.LatLng(sectionAddressesCoords[i][0] + .00004, sectionAddressesCoords[i][1] - 0.00006);
                 		//var myLatlng2 = new google.maps.LatLng(sectionAddressesCoords[i][0] - .00004, sectionAddressesCoords[i][1] + 0.00006);
                 		var myLatlng1 = new google.maps.LatLng(sectionAddressesCoords[i][0] + .00003, sectionAddressesCoords[i][1] - 0.000045);
                 		var myLatlng2 = new google.maps.LatLng(sectionAddressesCoords[i][0] - .00003, sectionAddressesCoords[i][1] + 0.000045);
                 		var color = sectionAddressesCoords[i][2];

                		var strokeOpacity = 0.4;
                		var strokeWeight = 1;
                		var fillOpacity = 0.05;

                		if (color == "orange" || color == "yellow") {
                			strokeOpacity = 0.4;
                			fillOpacity = 0.2
                		}
                		if (mapSize.value == "0") {
                			strokeOpacity = 0.4;
                			strokeWeight = 2;
                			fillOpacity = 0.2;
                		}

                 		var rectangle = new google.maps.Rectangle({
                			strokeColor: color,
                			strokeOpacity: strokeOpacity,
                			strokeWeight: strokeWeight,
                			fillColor: color,
                			fillOpacity: fillOpacity,
                			map: map,
                			bounds: new google.maps.LatLngBounds(myLatlng1, myLatlng2)
                		});
					}
                }

                // Non-Route Addresses
                var nonRouteCoordsString = document.getElementById("nonRouteCoordinates").value;
                if (nonRouteCoordsString.length > 0) {
                	var nonRouteCoords = $.parseJSON(nonRouteCoordsString);
                	var image;

                 	for (i = 0; i < nonRouteCoords.length; i++) {
                		var myLatlng = new google.maps.LatLng(nonRouteCoords[i][0], nonRouteCoords[i][1]);

                		image = getMarkerIcon(nonRouteCoords[i][3]);

                		// add marker to object
                		var addressID = nonRouteCoords[i][4];
                		markers[addressID] = {};
                		markers[addressID].icon = image;
                		markers[addressID].selected = false;

                		var markerVisible = ckNonRouteAddressesNearby.GetValue();
                 		var marker = new google.maps.Marker({
                			position: myLatlng,
                			map: map,
                			title: nonRouteCoords[i][2],
                			icon: image,
                			addressID: nonRouteCoords[i][4],
							visible: markerVisible

                 		});
                 		markers[addressID].marker = marker;

                  		google.maps.event.addListener(marker, 'click', function () {
                 			toggleMarkerIcons(this);
                		});
                	}
                }

            	// Carrier Routes
        //        var carrierRouteCoordsString = document.getElementById("carrierRouteCoordinates").value;
        //        if (carrierRouteCoordsString.length > 0) {
        //        	var carrierRouteCoords = $.parseJSON(carrierRouteCoordsString);

        //        	for (i = 0; i < carrierRouteCoords.length; i++) {
        //        		var polyCoords = [];
        //        		var crrt = carrierRouteCoords[i][0][0];
        //        		var polyBounds = new google.maps.LatLngBounds();
        //        		var color = carrierRouteCoords[i][0][1];
        //        		for (j = 1; j < carrierRouteCoords[i].length; j++) {
        //        			var ll = new google.maps.LatLng(carrierRouteCoords[i][j][0], carrierRouteCoords[i][j][1]);
        //        			polyCoords.push(ll);
        //        			polyBounds.extend(ll);
        //        			mapBounds.extend(ll);
        //        		}

        //        		// Construct the polygon.
        //        		var crrtVisible = ckCarrierRoutes.GetValue();
        //        		var crrtPolygon = new google.maps.Polygon({
        //        			paths: polyCoords,
        //        			strokeColor: 'blue',
        //        			strokeOpacity: 1.0,
        //        			strokeWeight: 1,
        //        			fillColor: 'brown',
        //        			fillOpacity: 0.0
        //        		});
        //        		carrierRoutes.push(crrtPolygon);

        //        		var myOptions = {
        //        			content: carrierRouteCoords[i][0][0]
								//, boxStyle: {
								//	border: "1px solid ".concat('blue')
								//  , textAlign: "center"
								//  , fontSize: "10pt"
								//  , width: "75px"
								//}
								//, disableAutoPan: true
								//, pixelOffset: new google.maps.Size(-25, 0)
								//, position: polyBounds.getCenter()
								//, closeBoxURL: ""
								//, isHidden: false
								//, pane: "mapPane"
								//, enableEventPropagation: true
        //        		};
        //        		var ibLabel = new InfoBox(myOptions);
        //        		carrierRouteLabels.push(ibLabel);

        //        		if (crrtVisible) {
        //        			crrtPolygon.setMap(map);
        //        			ibLabel.open(map);
        //        		}
        //         	}
        //        }

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

        var printMap = function () {
            map.setOptions({
                mapTypeControl: false,
                zoomControl: false,
                streetViewControl: false,
                panControl: false
            });

            var popUpAndPrint = function () {
                dataUrl = [];

                $('#map_canvas canvas').filter(function () {
                    dataUrl.push(this.toDataURL("image/png"));
                })

                var container = document.getElementById('map_canvas');
                var clone = $(container).clone();

                var width = container.clientWidth
                var height = container.clientHeight

                $(clone).find('canvas').each(function (i, item) {
                    $(item).replaceWith(
                        $('<img>')
                        .attr('src', dataUrl[i]))
                        .css('position', 'absolute')
                        .css('left', '0')
                        .css('top', '0')
                        .css('width', width + 'px')
                        .css('height', height + 'px');
                });

            	//mapTitle = document.getElementById("hfMapTitle");
            	//txt = new TxtOverlay(ll, mapTitle.value, "customBox", map)
                //$(clone).prepend("TEST"); //or $(element).appendTo("body");

                var printWindow = window.open('', 'PrintMap',
                    'width=' + width + ',height=' + height);
                printWindow.document.writeln($(clone).html());
				printWindow.document.close();
                printWindow.focus();
            	//printWindow.printMap();
                //alert("This holds the print window open");
                printWindow.print();
                printWindow.close();

                map.setOptions({
                    mapTypeControl: true,
                    zoomControl: true,
                    streetViewControl: true,
                    panControl: true
                });
            };

            setTimeout(popUpAndPrint, 500);
        };


    	// CUSTOM HEADERS

        //adapted from this example http://code.google.com/apis/maps/documentation/javascript/overlays.html#CustomOverlays
        //text overlays
        function TxtOverlay(pos, txt, cls, map) {

        	// Now initialize all properties.
        	this.pos = pos;
        	this.txt_ = txt;
        	this.cls_ = cls;
        	this.map_ = map;

        	// We define a property to hold the image's
        	// div. We'll actually create this div
        	// upon receipt of the add() method so we'll
        	// leave it null for now.
        	this.div_ = null;

        	// Explicitly call setMap() on this overlay
        	this.setMap(map);
        }

        TxtOverlay.prototype = new google.maps.OverlayView();

        TxtOverlay.prototype.onAdd = function () {

        	// Note: an overlay's receipt of onAdd() indicates that
        	// the map's panes are now available for attaching
        	// the overlay to the map via the DOM.

        	// Create the DIV and set some basic attributes.
        	var div = document.createElement('DIV');
        	div.className = this.cls_;

        	div.innerHTML = this.txt_;

        	// Set the overlay's div_ property to this DIV
        	this.div_ = div;
        	var overlayProjection = this.getProjection();
        	var position = overlayProjection.fromLatLngToDivPixel(this.pos);
        	div.style.left = position.x + 'px';
        	div.style.top = position.y + 'px';
        	// We add an overlay to a map via one of the map's panes.

        	var panes = this.getPanes();
        	panes.floatPane.appendChild(div);
        }
        TxtOverlay.prototype.draw = function () {


        	var overlayProjection = this.getProjection();

        	// Retrieve the southwest and northeast coordinates of this overlay
        	// in latlngs and convert them to pixels coordinates.
        	// We'll use these coordinates to resize the DIV.
        	var position = overlayProjection.fromLatLngToDivPixel(this.pos);


        	var div = this.div_;
        	div.style.left = position.x + 'px';
        	div.style.top = position.y + 'px';
        }
        //Optional: helper methods for removing and toggling the text overlay.  
        TxtOverlay.prototype.onRemove = function () {
        	this.div_.parentNode.removeChild(this.div_);
        	this.div_ = null;
        }
        TxtOverlay.prototype.hide = function () {
        	if (this.div_) {
        		this.div_.style.visibility = "hidden";
        	}
        }

        TxtOverlay.prototype.show = function () {
        	if (this.div_) {
        		this.div_.style.visibility = "visible";
        	}
        }

        TxtOverlay.prototype.toggle = function () {
        	if (this.div_) {
        		if (this.div_.style.visibility == "hidden") {
        			this.show();
        		}
        		else {
        			this.hide();
        		}
        	}
        }

        TxtOverlay.prototype.toggleDOM = function () {
        	if (this.getMap()) {
        		this.setMap(null);
        	}
        	else {
        		this.setMap(this.map_);
        	}
        }

        var map;

        function init() {
        	var latlng = new google.maps.LatLng(37.9069, -122.0792);
        	var myOptions = {
        		zoom: 4,
        		center: latlng,
        		mapTypeId: google.maps.MapTypeId.ROADMAP
        	};
        	map = new google.maps.Map(document.getElementById("map"), myOptions);

        	var marker = new google.maps.Marker({
        		position: latlng,
        		map: map,
        		title: "Hello World!"
        	});

        	customTxt = "<div>Blah blah sdfsddddddddddddddd ddddddddddddddddddddd<ul><li>Blah 1<li>blah 2 </ul></div>"
        	txt = new TxtOverlay(latlng, customTxt, "customBox", map)

        }
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
        <asp:HiddenField ID="userID" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="mappingUpdate" runat="server" ClientIDMode="Static" /> <%--Only certain users can update--%>
        <asp:HiddenField ID="googleCoordinates" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="sectionAddressesCoordinates" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="nearbySectionsCoordinates" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="nonRouteCoordinates" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="carrierRouteCoordinates" runat="server" ClientIDMode="Static" />

		<asp:UpdatePanel ID="upMain" runat="server">
			<Triggers> 
				<asp:PostBackTrigger ControlID="btnDrawMap" /> 
			</Triggers>				
	 		<ContentTemplate>
				<table style="width:100%">
					<tr>
						<td style="width: 30%">Map Type:
							<asp:DropDownList ID="ddlMapType" runat="server" AutoPostBack="True">
							</asp:DropDownList>
 						</td>
						<td style="width: 30%">
							<div onclick="printMap();" class="btn" style="width: 70px; float: left">Print</div>
							<div>
							<table id="mapSize" runat="server" style="width:160px;"><tr><td>Map Size:
								<dx:ASPxComboBox ID="cbMapSizes" runat="server" DataSourceID="dsMapSizes"
									Width="160px" SelectedIndex="0" ValueField="id" TextField="mapSize"
									OnSelectedIndexChanged="cbMapSizes_SelectedIndexChanged">
								</dx:ASPxComboBox>
								<asp:SqlDataSource ID="dsMapSizes" runat="server"
									ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
									SelectCommand="SELECT 0 AS id, 'Full Screen' AS mapSize 
										UNION SELECT 1 AS id, 'Portrait (8-1/2 x 11)' AS mapSize
										UNION SELECT 2 AS id, 'Portrait (11 x 17)' AS mapSize
										UNION SELECT 3 AS id, 'Landscape (8-1/2 x 11)' AS mapSize
										UNION SELECT 4 AS id, 'Landscape (11 x 17)' AS mapSize">
								</asp:SqlDataSource>
								<asp:HiddenField ID="hfMapSize" runat="server" />
								<asp:HiddenField ID="hfZoomLevel" runat="server" />
								<asp:HiddenField ID="hfMapTitle" runat="server" />
							</td></tr></table>
							</div>
						</td>
						<td style="width: 40%">Nearby Distance (miles):
							<dx:ASPxTextBox runat="server" ID="txtNearbyDistance" AutoPostBack="False" Width="80%"
								 ClientInstanceName="NearbyDistance" OnValidation="NearbyDistance_Validation">
								<ValidationSettings SetFocusOnError="True" ErrorText="Nearby Distance must not be greater than 5 miles." />
								<ClientSideEvents Validation="OnNearbyDistanceValidation" />
								<InvalidStyle BackColor="LightPink" />
							</dx:ASPxTextBox>
						</td>
					</tr>
				</table>
				<table id="tblRegion" runat="server" style="width:20%; display:none">
					<tr>
						<td>Region:
							<asp:DropDownList ID="ddlRegion" runat="server" Width="80%" >
							</asp:DropDownList>
 						</td>
					</tr>
				</table>
				<table id="tblCity" runat="server" style="width:20%; display:none">
					<tr>
						<td>City:
							<asp:DropDownList ID="ddlCity" runat="server" Width="80%" >
							</asp:DropDownList>
 						</td>
					</tr>
				</table>
				<table id="tblTemplate" runat="server" style="width:100%; display:none">
					<tr>
						<td style="width: 15%">
							<dx:ASPxCheckbox runat="server" ID="ckActiveTemplates" Text="Show Active Templates" AutoPostBack="true"></dx:ASPxCheckbox>
							<dx:ASPxCheckbox runat="server" ID="ckInactiveTemplates" Text="Show Inactive Templates" AutoPostBack="true"></dx:ASPxCheckbox>
						</td>
						<td style="width: 15%">
							<dx:ASPxCheckbox runat="server" ID="ckActiveRoutes" Text="Show Active Routes" AutoPostBack="true"></dx:ASPxCheckbox>
							<dx:ASPxCheckbox runat="server" ID="ckInactiveRoutes" Text="Show Inactive Routes" AutoPostBack="true"></dx:ASPxCheckbox>
						</td>
						<td style="width: 15%">
							<dx:ASPxCheckbox runat="server" ID="ckActiveSections" Text="Show Active Sections" AutoPostBack="true"></dx:ASPxCheckbox>
							<dx:ASPxCheckbox runat="server" ID="ckInactiveSections" Text="Show Inactive Sections" AutoPostBack="true"></dx:ASPxCheckbox>
						</td>
						<td style="width: 15%">
							<table id="tblAddressStatuses" runat="server" style="width:100%; display:none"><tr><td>Address Statuses:
								<dx:ASPxRadioButtonList ID="rbAddressStatuses" runat="server">
									<Items>
										<dx:ListEditItem Text="Current & Backup" Value="CurrentBackup" />
										<dx:ListEditItem Text="All" Value="All" />
									</Items>
								</dx:ASPxRadioButtonList>
							</td></tr></table>
						</td>
						<td style="width: 40%">
							<table id="tblUpdate" runat="server" style="width:100%; display:none"><tr><td>Updates:
								<dx:ASPxRadioButtonList ID="rbUpdateList" runat="server" AutoPostBack="True">
									<Items>
										<dx:ListEditItem Text="Statuses" Value="Statuses" />
										<dx:ListEditItem Text="Route-Section" Value="Route-Section" />
									</Items>
								</dx:ASPxRadioButtonList>
							</td></tr></table>
						</td>
					</tr>
					<tr>
						<td style="width: 15%">Template:
							<asp:DropDownList ID="ddlTemplates" runat="server" ToolTip="Pickup Cycle Template Code" Width="100%" AutoPostBack="True">
							</asp:DropDownList>
						</td>
						<td style="width: 15%">Route:
							<asp:DropDownList ID="ddlRoutes" runat="server" ToolTip="Route Code" Width="100%" AutoPostBack="True">
							</asp:DropDownList>
 						</td>
						<td style="width: 15%">Section:
							<asp:DropDownList ID="ddlSections" runat="server" ToolTip="Section Code" Width="100%"  AutoPostBack="True">
							</asp:DropDownList>
						</td>
						<td style="width: 55%" colspan="2">
							<table id="tblCkNonRoute" runat="server" style="width:100%; display:none"><tr><td>
								<%--<dx:ASPxCheckbox runat="server" ID="ckNonRouteAddressesZip" Text="Show Non-Route Addresses in same Zip(s)" AutoPostBack="True"></dx:ASPxCheckbox>--%>
								<%--<dx:ASPxCheckbox runat="server" ID="ckCarrierRoutes" Text="Show Carrier Routes" AutoPostBack="True">
									<ClientSideEvents CheckedChanged="toggleCarrierRoutes" />
								</dx:ASPxCheckbox>--%>
								<dx:ASPxCheckbox runat="server" ID="ckNonRouteAddressesNearby" Text="Show Nearby Non-Route Addresses" AutoPostBack="True">
									<ClientSideEvents CheckedChanged="toggleNonRouteAddresses" />
								</dx:ASPxCheckbox>
							</td></tr></table> 
						</td>
					</tr>
					<tr>
						<td colspan="3">
							<p><asp:Label ID="lblUcommittedChanges" runat="server" Visible="true" ForeColor="Blue" Text=""></asp:Label></p>
							<asp:HiddenField ID="hfUncommittedChanges" runat="server" />
						</td>
					</tr>
					<tr>
						<td style="width: 15%">
							<table id="tblStatusesUpdate" runat="server" style="width:100%; display:none"><tr><td>Status:
								<asp:DropDownList ID="ddlStatusesUpdate" runat="server" Width="100%">
								</asp:DropDownList>
							</td></tr></table>
							<table id="tblTemplateUpdate" runat="server" style="width:100%; display:none"><tr><td>Template:
								<asp:DropDownList ID="ddlTemplateUpdate" runat="server" ToolTip="Pickup Cycle Template Code" Width="100%" AutoPostBack="True">
								</asp:DropDownList>
							</td></tr></table>
						</td>
						<td style="width: 15%">
							<table id="tblRouteUpdate" runat="server" style="width:100%; display:none"><tr><td>Route:
								<asp:DropDownList ID="ddlRouteUpdate" runat="server" ToolTip="Pickup Cycle Template Code" Width="100%" AutoPostBack="True">
								</asp:DropDownList>
							</td></tr></table>
 						</td>
						<td style="width: 15%">
							<table id="tblSectionUpdate" runat="server" style="width:100%; display:none"><tr><td>Section:
								<asp:DropDownList ID="ddlSectionUpdate" runat="server" ToolTip="Pickup Cycle Template Code" Width="100%">
								</asp:DropDownList>
							</td></tr></table>
						</td>
						<td style="width: 55%" colspan="2">
							<table id="tblUpdateButtons" runat="server" style="width:100%; display:none"><tr><td>
								<asp:Button ID="btnUpdate" Text="Update Selected Addresses" runat="server" />
								<asp:Button ID="btnDeselect" Text="Deselect All Addresses" runat="server" />
							</td></tr></table>
						</td>
					</tr>
					<tr>
						<td colspan="3">
							<p><asp:Label ID="lblMessage" runat="server" Visible="true" ForeColor="Red" Text=""></asp:Label></p>
						</td>
					</tr>
				</table>
				<table id="tblDrawMap" runat="server" style="width:20%; display:none">
					<tr>
						<td>
							<asp:Button ID="btnDrawMap" Text="Draw Map" runat="server" OnClientClick="ButtonDrawMap(this);" />
						</td>
						<td>
							<asp:Label ID="zoomLabel" runat="server">Zoom level:</asp:Label>
							<asp:Label ID="zoomLevel" runat="server" Font-Bold="true"></asp:Label>
						</td>
					</tr>
				</table>
 			</ContentTemplate>
	</asp:UpdatePanel>
	</div> 
    </form>
 	<div id="map_canvas" runat="server"></div>
</body>
</html>


