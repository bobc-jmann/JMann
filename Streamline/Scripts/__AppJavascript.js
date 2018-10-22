var IE4 = (document.all) ? 1 : 0;
var NS4 = (document.layers) ? 1 : 0;
var ver4 = (IE4 || NS4) ? 1 : 0;
var mBadNumber = 'This does not appear to be a valid number.  It should have no letters or characters other than commas and periods. Please re-enter.'
var mBadCurrency = 'This does not appear to be a valid currency.  It should have no letters or characters other than commas and periods. Please re-enter.'
var mBadZip = 'This does not appear to be a valid Zip Code.  It should be in the form 99999 or 99999-9999. Please re-enter.'
var mBadEmail = 'This does not appear to be a valid Email.  It should be in the form name@domain.xxx. Please re-enter.'
var mBadURL = 'This does not appear to be a valid web URL.  It should be in the form [www or server name].site.xxx.  Please re-enter.'
var mBadDate = 'This does not appear to be a valid date.  It should be a real date in the format of MM/DD/YY or MM/DD/YYYY. Please re-enter.'
var mBadPhone = 'This does not appear to be a valid Phone Number.  It should be in the form 999-9999 or (999)999-9999 . Please re-enter.'
var jPos="Positive";
var jNeg="Negative";
var jBrowserType;
var jIsMac;
var jIsSafari;
var preloadFlag = false;
var jRows=new Array
var jCurrentField
var jIsMac;
var jBrowserType;
var jIsOpera;
var jDtSize;

if (navigator.appName.indexOf("Netscape")!=-1) { jBrowserType="NS" } else { jBrowserType="IE" } 
v=navigator.userAgent.toLowerCase()
if (v.indexOf("mac")>-1) { jIsMac=true } else { jIsMac=false } 
if (v.indexOf("safari")!=-1) { jIsSafari=true } else { jIsSafari=false } 
if (v.indexOf("opera")>-1) { jIsOpera=true } else { jIsOpera=false } 
if (jIsOpera) {	jDtSize=11 } else { jDtSize=8 }

var jIsSubmitting=false
var jUserID
var jUserName
var jUserLevel
var jUserAdminLevel
var jUserGroup
var jUserClientID
var jUserEmail

function jsShowDates(t) {
    var dt = new Date()
    var m = dt.getMonth() + 1
    var d = dt.getDate()
    var dy = dt.getDay()
    var y = dt.getUTCFullYear()
    if (t == "Quarter1") {
        dt1 = new Date("1/1/" + y)
        dt2 = new Date("3/31/" + y)
        return [jsFormatDate(dt1), jsFormatDate(dt2)]
    }
    if (t == "Quarter2") {
        dt1 = new Date("4/1/" + y)
        if (m < 4) { y = y - 1 }
        dt1 = new Date("4/1/" + y)
        dt2 = new Date("6/30/" + y)
        return [jsFormatDate(dt1), jsFormatDate(dt2)]
    }
    if (t == "Quarter3") {
        dt1 = new Date("7/1/" + y)
        if (m < 7) { y = y - 1 }
        dt1 = new Date("7/1/" + y)
        dt2 = new Date("9/30/" + y)
        return [jsFormatDate(dt1), jsFormatDate(dt2)]
    }
    if (t == "Quarter4") {
        dt1 = new Date("10/1/" + y)
        if (m < 10) { y = y - 1 }
        dt1 = new Date("10/1/" + y)
        dt2 = new Date("12/31/" + y)
        return [jsFormatDate(dt1), jsFormatDate(dt2)]
    }
    if (t == "All") {
        return [null, null]
    }
    if (t == "Today") {
        dt1 = jsFormatDate(dt)
        return [dt1, dt1]
    }
    if (t == "Yesterday") {
        dt1 = jsDateMath(dt, -1)
        return [jsFormatDate(dt1), jsFormatDate(dt1)]
    }
    if (t == "Last2Days") {
        dt1 = jsDateMath(dt, -1)
        return [jsFormatDate(dt1), jsFormatDate(dt)]
    }
    if (t == "ThisWeek") {
        dt1 = dt
        dt2 = dt
        while (dt1.getDay() > 0) { dt1 = jsDateMath(dt1, -1) }
        while (dt2.getDay() < 6) { dt2 = jsDateMath(dt2, 1) }
        return [jsFormatDate(dt1), jsFormatDate(dt2)]
    }
    if (t == "LastWeek") {
        dt = jsDateMath(dt, -7)
        dt1 = dt
        dt2 = dt
        while (dt1.getDay() > 0) { dt1 = jsDateMath(dt1, -1) }
        while (dt2.getDay() < 6) { dt2 = jsDateMath(dt2, 1) }
        return [jsFormatDate(dt1), jsFormatDate(dt2)]
    }
    if (t == "Last2Weeks") {
        dt1 = jsDateMath(dt, -7)
        dt2 = dt
        while (dt1.getDay() > 0) { dt1 = jsDateMath(dt1, -1) }
        while (dt2.getDay() < 6) { dt2 = jsDateMath(dt2, 1) }
        return [jsFormatDate(dt1), jsFormatDate(dt2)]
    }
    if (t == "ThisMonth") {
        dt1 = dt
        dt2 = dt
        while (dt1.getDate() > 1) { dt1 = jsDateMath(dt1, -1) }
        jMax = jsMaxDay(m, y)
        while (dt2.getDate() < jMax) { dt2 = jsDateMath(dt2, 1) }
        return [jsFormatDate(dt1), jsFormatDate(dt2)]
    }
    if (t == "LastMonth") {
        m = m - 1
        if (m < 1) { m = 12; y = y - 1 }
        dt = new Date(m + "/" + d + "/" + y)
        dt1 = dt
        dt2 = dt
        while (dt1.getDate() > 1) { dt1 = jsDateMath(dt1, -1) }
        jMax = jsMaxDay(m, y)
        while (dt2.getDate() < jMax) { dt2 = jsDateMath(dt2, 1) }
        return [jsFormatDate(dt1), jsFormatDate(dt2)]
    }
    if (t == "Last2Months") {
        dt1 = jsDateMath(dt, -30)
        dt2 = dt
        while (dt1.getDate() > 1) { dt1 = jsDateMath(dt1, -1) }
        jMax = jsMaxDay(m, y)
        while (dt2.getDate() < jMax) { dt2 = jsDateMath(dt2, 1) }
        return [jsFormatDate(dt1), jsFormatDate(dt2)]
    }
    if (t == "Last3Months") {
        dt1 = jsDateMath(dt, -60)
        dt2 = dt
        while (dt1.getDate() > 1) { dt1 = jsDateMath(dt1, -1) }
        jMax = jsMaxDay(m, y)
        while (dt2.getDate() < jMax) { dt2 = jsDateMath(dt2, 1) }
        return [jsFormatDate(dt1), jsFormatDate(dt2)]
    }
    if (t == "ThisYear") {
        dt1 = new Date("1/1/" + y)
        dt2 = new Date("12/31/" + y)
        return [jsFormatDate(dt1), jsFormatDate(dt2)]
    }
    if (t == "LastYear") {
        y = y - 1
        dt1 = new Date("1/1/" + y)
        dt2 = new Date("12/31/" + y)
        return [jsFormatDate(dt1), jsFormatDate(dt2)]
    }
    if (t == "Last12Mths") {
        dt = new Date(m + "/" + d + "/" + y)
        dt2 = dt
        y = y - 1
        dt = new Date(m + "/" + d + "/" + y)
        dt1 = dt
        return [jsFormatDate(dt1), jsFormatDate(dt2)]
    }
}
function isLeapYear(year) {
    return (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) ? 1 : 0;
}
function jsMaxDay(m, y) {
    v = isLeapYear(y)
    if ((m == 2) && (v != 1)) { return 28 }
    if ((m == 2) && (v == 1)) { return 29 }
    if ((m == 1) || (m == 3) || (m == 5) || (m == 7) || (m == 8) || (m == 10) || (m == 12)) { return 31 }
    if ((m == 4) || (m == 6) || (m == 9) || (m == 11)) { return 30 }
}
function jsSetScreenWidth() {
    jWidth = document.body.clientWidth
    var jWidth = document.body.clientWidth
    ob_post.AddParam('vScreenWidth', jWidth);
    ob_post.post(null, 'setSessWidth');
}
function jsFormatDate(dt) {
    var m = dt.getMonth() + 1
    var d = dt.getDate()
    var y = dt.getFullYear()
    var newDt = new Date(m + "/" + d + "/" + y)
    return newDt
}
var jDtMS = 86400000
function jsDateMath(d, a) {
    a = a * jDtMS
    d = d.valueOf()
    d = new Date(d + a)
    return d
}
function jsGetDates(sender, args) {
    var v = cbDateRanges.GetValue()
    if (v != "NONE") {
        rv = jsShowDates(v)
        txtStartDate.SetValue(rv[0])
        txtEndDate.SetValue(rv[1])
        jsSetSession()
    }
}
function jsSetSession() {
    //alert(cbOrgID.GetSelectedIndex())
    vStartDate = ""
    vEndDate = ""
    vOrgID = cbOrgID.GetValue()
    d = txtStartDate.GetDate()
    if (d != null) { vStartDate = d.getYear() + "/" + (d.getMonth()+1) + "/" + d.getDate() }
    d = txtEndDate.GetDate()
    if (d != null) { vEndDate = d.getYear() + "/" + (d.getMonth()+1) + "/" + d.getDate() }
    try {
        var vScreenWidth = parent.iframeContent.document.body.clientWidth
        ob_post.AddParam('vScreenWidth', vScreenWidth);
    }
    catch (e) { }
    Callback1.PerformCallback(vOrgID);
    Callback2.PerformCallback(vStartDate);
    Callback3.PerformCallback(vEndDate);
    document.iframeContent.location.reload()
}
function OnCallbackComplete(s, e) { }

function jsForgotEmail() {
o=document.frmPay.emailforgot
if (o.value=="") { alert("Please enter an email address."); o.focus(); return; }
rv=isEmail(o);
if (rv!="") { alert(mBadEmail + ":" + o.name + ":" + rv); o.focus(); return; }
document.frmPay.submit()
}
function jsSideLogin(f,t,un,pw) {
o=un
if (o.value=="") { alert("Please enter an email address."); o.focus(); return; }
rv=isEmail(o);
if (rv!="") { alert(mBadEmail + ":" + o.name + ":" + rv); o.focus(); return; }
f.hdnVal1.value=o.value
o=pw
if (o.value=="") { alert("Please enter a Password."); o.focus(); return; }
f.hdnVal2.value=o.value
f.hdnNextOp.value='LOGIN'
f.submit()
}
function jsLoadSub(v) {
document.frmPay.SubscriptionID.value=v
}
function jsSubmitJoin(t) {
if (jIsSubmitting) { alert("We are in the process of submitting your transaction.  Please wait."); return 0; }
o=document.frmPay.SubscriptionID
if (o.value=="") { 
	alert("Please select a subscription");
	return
	}
o=document.frmPay.Address1
if (o.value=="") { alert("Please enter an address"); o.focus(); return }
o=document.frmPay.City
if (o.value=="") { alert("Please enter a city"); o.focus(); return }
o=document.frmPay.State
if (o.value=="") { alert("Please enter a state"); o.focus(); return }
o=document.frmPay.Postal
if (o.value=="") { alert("Please enter a Postal Code"); o.focus(); return }
o=document.frmPay.Country
if (o.value=="") { alert("Please enter a Country"); o.focus(); return }
o=document.frmPay.isAdmin
if (o.value!="ADMIN") {
	o=document.frmPay.ccType
	if (o.value=="") { alert("Please enter a Credit Card Type"); o.focus(); return }
	o=document.frmPay.ccNumber
	if (o.value=="") { alert("Please enter a Credit Card Number"); o.focus(); return }
	o=document.frmPay.ccMonth
	if (o.value=="") { alert("Please enter a Expiration Month"); o.focus(); return }
	o=document.frmPay.ccYear
	if (o.value=="") { alert("Please enter a Expiration Year"); o.focus(); return }
	}
o=document.frmPay.chkagreeToAbideByMarkLicense
if (!o.checked) { alert("You must agree to abide by the License Agreement relative to the use of the Collaborative Practice 'Mark'."); return; }
jIsSubmitting=true
document.frmPay.hdnSaveThis.value="SAVE"
document.frmPay.submit()
//rv=jsSaveFormRec(document.frmPay,this,'','')
}
function jsCheckRenew(t) {
if (jIsSubmitting) { alert("We are in the process of submitting your transaction.  Please wait."); return 0; }
o=document.frmPay.SubscriptionID
if (o.value=="") { 
	alert("Please select a subscription");
	return
	}
o=document.frmPay.Address1
if (o.value=="") { alert("Please enter an address"); o.focus(); return }
o=document.frmPay.City
if (o.value=="") { alert("Please enter a city"); o.focus(); return }
o=document.frmPay.State
if (o.value=="") { alert("Please enter a state"); o.focus(); return }
o=document.frmPay.Postal
if (o.value=="") { alert("Please enter a Postal Code"); o.focus(); return }
o=document.frmPay.Country
if (o.value=="") { alert("Please enter a Country"); o.focus(); return }
o=document.frmPay.isAdmin
if (o.value!="ADMIN") {
	o=document.frmPay.ccType
	if (o.value=="") { alert("Please enter a Credit Card Type"); o.focus(); return }
	o=document.frmPay.ccNumber
	if (o.value=="") { alert("Please enter a Credit Card Number"); o.focus(); return }
	o=document.frmPay.ccMonth
	if (o.value=="") { alert("Please enter a Expiration Month"); o.focus(); return }
	o=document.frmPay.ccYear
	if (o.value=="") { alert("Please enter a Expiration Year"); o.focus(); return }
	}
o=document.frmPay.chkagreeToAbideByMarkLicense
if (!o.checked) { alert("You must agree to abide by the License Agreement relative to the use of the Collaborative Practice 'Mark'."); return; }
jIsSubmitting=true
document.frmPay.hdnSaveThis.value="SAVEPAY"
document.frmPay.submit()
}
function jsEmailThisPage() {
document.frmPageInfo.hdnThisTitle.value=document.title;
document.frmPageInfo.hdnThisPage.value=window.location;
document.frmPageInfo.submit();
}
function jsFormLogin(f,t) {
o=f.MainUN
if (o.value=="") { alert("Please enter an valid username."); o.focus(); return; }
o=f.MainPW
if (o.value=="") { alert("Please enter a Password."); o.focus(); return; }
f.hdnNextOp.value='LOGIN'
f.submit()
}
function jsFormLoginRenew(f,t) {
o=f.MainUN
if (o.value=="") { alert("Please enter an valid username."); o.focus(); return; }
o=f.MainPW
if (o.value=="") { alert("Please enter a Password."); o.focus(); return; }
f.hdnNextOp.value='RENEW'
f.submit()
}
function jsLostPW(f) {
o=f.MainUN
if (o.value=="") { alert("Please enter an valid username and we will send your password to your registered email address."); o.focus(); return; }
f.hdnNextOp.value='FINDPW'
f.submit()
}
function jsRequestInfo(f,t) {
o=f.vInfoEmail
rv=isEmail(o);
if (rv!="") { alert(mBadEmail + ":" + o.name + ":" + rv); o.focus(); return; }
if (o.value=="") { alert("Please enter a valid email address."); o.focus(); event.preventDefault; return; }
o=f.vInfoCountry
if (o.value=="") { alert("Please select a valid Country."); o.focus(); return; }
if ((o.value!="Hong Kong") && (o.value!="Ireland")) {
	o=f.vInfoZip
	if (o.value=="") { alert("Please enter a valid Zip/Postal Code."); o.focus(); return; }
}
o=f.vInfoCountry
if (o.value=="") { alert("Please select a valid Country."); o.focus(); return; }
f.hdnNextOp.value='REQUESTINFO'
f.submit()
}
function jsSubmitEmail(f) {
o=f.RecipName
if (o.value=="") { alert("Please enter a Recipient name."); o.focus(); return; }
o=f.RecipEmail
if (o.value=="") { alert("Please enter an email for the recipient."); o.focus(); return; }
rv=isEmail(o);
if (rv!="") { alert(mBadEmail + ":" + o.name + ":" + rv); o.focus(); return; }
o=f.SenderName
if (o.value=="") { alert("Please enter your name."); o.focus(); return; }
o=f.SenderEmail
if (o.value=="") { alert("Please enter your email."); o.focus(); return; }
rv=isEmail(o);
if (rv!="") { alert(mBadEmail + ":" + o.name + ":" + rv); o.focus(); return; }
f.hdnSaveThis.value="SAVE"
f.submit()
}
function jsOpenLocate() {
vHeight=700
vWidth=850
vProps='resizable=yes,location=yes,menubar=yes,menu=yes,scrollbars=yes,height=' + vHeight + ',width=' + vWidth
vThisWin=window.open("http://www.collaborativepractice.com/_loc.asp",'View',vProps)
vThisWin.focus()
}
function jsSubmitSearch(si) {
document.forms("searchbox_008873985823673333676:tza5yk10keo").submit()
}
function jsCheckMember() {
document.frmPay.hdnNextOp.value="CHECKMEMBER"
document.frmPay.submit()
}
function jsShowMaintenance() {
alert('Sorry, but we are currently working on this function.  Please check back soon.')
}
function jsDoSort(sf,sd) {
document.frmPay.hdnSortFld.value=sf
document.frmPay.hdnSortDir.value=sd
document.frmPay.hdnNextOp.value="LIST"
document.frmPay.submit()
}
function InsertSampleMovie(path,file,w,h) {
if (file.indexOf('.swf') != -1) {
	document.write('<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0" width="' + w + '" height="' + h + '">\n');
	document.write('<param name="movie" value="'  + path + file + '" />\n');
	document.write('<param name="quality" value="high"><param name="menu" value=true"><PARAM NAME="wmode" VALUE="transparent"> />\n');
	document.write('<embed src="'  + path + file + '" quality="high"  wmode="transparent" menu="true" pluginspage="https://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="' + w + '" height="' + h + '"></embed>\n');
	document.write('</object>');
	return;
	}
if (file.indexOf('.wmv') != -1) {
	document.write('<EMBED src="' + file + '" width="320" height="240" autoplay="true" CONTROLLER="false" PLUGINSPAGE="http://www.apple.com/quicktime/download/"></EMBED>');
	return;
	}
var ua = navigator.userAgent.toLowerCase();
  if (ua.indexOf('safari') != - 1) { 
	document.write('<object classid="clsid: 02BF25D5-8C17-4B23-BC80-D3488ABDDC6B" codebase="https://www.apple.com/qtactivex/qtplugin.cab" width="' + w + '" height="' + h + '">\n');
	document.write('<param name="src" value="'  + path + file + '" />\n');
	document.write('<param name="controller" value="true" />\n');
	document.write('</object>\n');

  } else 

	if (navigator.plugins && navigator.plugins.length) { // Not IE browser
		document.write(' <object type="video/quicktime" data="'  + path + file + '" width="' + w + '" height="' + h + ' class="mov">\n');
	    	document.write('<param name="controller" value="true" />\n');
		document.write('</object>\n');
	}

	else { // IE / PC browser
		document.write('<object classid="clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B" codebase="https://www.apple.com/qtactivex/qtplugin.cab" width="' + w + '" height="' + h + '">\n');
		document.write('<param name="src" value="'  + path + file + '" />\n');
		document.write('<param name="menu" value="true" />\n');
		document.write('<param name="controller" value="true" />\n');
 		document.write('</object>\n');
  }
	
}
function jsJumpToThis(t) {
if (t.value!="") {
	window.location="#" + t.value
	}
}
function jsEditMember(t) {
window.location="../intranet_main.asp?T=MEMBER&MID=" + t
}
function jsplay(v) {
document.frmVideo.SRC.value=v
document.frmVideo.submit()
}
function stateChanged()  { 
if (xmlHttp.readyState==4) { document.getElementById(jFld).value=xmlHttp.responseText; }
}
function GetXmlHttpObject() {
var xmlHttp=null;
try  {  // Internet Explorer
    		xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
//    	xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");
  }
catch (e) { // Firefox, Opera 8.0+, Safari
	try {
  		xmlHttp=new XMLHttpRequest();
    		}
	catch (e) {
    		xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
    		}
  	}
return xmlHttp;
}
function jsLogDownload(lp) {
xmlHttp=GetXmlHttpObject();
if (xmlHttp==null) {  alert ("Your browser does not support AJAX!");  return;  } 
var url="__GetData.asp";
url=url+"?ACT=ADD&SQL=KIT";
xmlHttp.open("GET",url,true);
xmlHttp.send(null);
window.open(lp)
}
function jsChgClr(t,c) {
	t.style.color=c
}
function jsShowHideLogin(t) {
if (t.style.visibility=="visible") {
	t.style.visibility='hidden'
	}
else {
	t.style.visibility='visible'
	}
}
function jsMsg() {
alert(viewportheight)
//gotoThis("intranet_main.asp?T=SHOMNU")
}
function gotoThis(p) {
	alert(viewportheight)
}
function jsAddUp(t) {
	vComp=document.getElementById("nbrComputers").value
	if (vComp!="") {
		if (isNaN(parseInt(vComp))) { alert("Please enter only numbers."); event.returnValue=null; document.getElementById("nbrComputers").focus(); event.returnValue=null;return; }
	}
	vServ=document.getElementById("nbrServers").value
	vStaff=document.getElementById("nbrStaff").value
	if (vStaff!="") {
		if (isNaN(parseInt(vStaff))) { alert("Please enter only numbers."); event.returnValue=null; document.getElementById("nbrStaff").focus(); event.returnValue=null;return; }
	}
	vHourly=document.getElementById("StaffHrly").value
	vHourly=parseInt(vHourly)
	if ((vComp!="") && (vStaff!="")) {
		vComp=parseInt(vComp)
		vStaff=parseInt(vStaff)
		if ((vServ!="")) { 
			if (isNaN(parseInt(vServ))) { alert("Please enter only numbers."); event.returnValue=null; document.getElementById("nbrServers").focus(); event.returnValue=null;return; }
			vServ=parseInt(vServ)
			}

		//dcm

		vSetup=4995
		vServCost=0
		vCompCost=0
		//lookup price per desktop count
		if (vComp<1000) { vCompCost=7.2 }
		if ((vComp>=1000) && (vComp<5000)) { vCompCost=6.4 }
		if ((vComp>5000) && (vComp<15000)) { vCompCost=6 }
		if ((vComp>15000)) { vCompCost=3.2 }
		vCompCost=vComp*vCompCost
		vServCost=22*vServ

		//lookup size factor
		vSizeFactor=1
		if (vComp>=5000) { vSizeFactor=1.1 }
		if (vComp>99999) { vSizeFactor=1.15 }
			
		if (vHourly=="") { vHourly=45 }
		vLoaded=vHourly*1.2

		vStdRate=60
		vDiffStdLoaded=vLoaded/vStdRate

		vSTCost=vSetup+vCompCost+vServCost
		vSTFactor=3

		vDIYCost=vDiffStdLoaded*(vSTCost*vSTFactor)
		vDIYCost=vDIYCost*vSizeFactor
		
		vLoadedMth=((vLoaded*2002)/12)
		//*.25
		vClientTtlMths=vDIYCost/vLoadedMth
		vClientManMths=vClientTtlMths/vStaff
		
		vSTMthBilling=25000
		vSTMths=vSTCost/vSTMthBilling
		vSTStaff=vComp/10000
		vSTClientStaff=vLoadedMth*(vSTMths*vSTStaff)*.5
		vSTCost=vSTCost+vSTClientStaff
		vSavingsPct=(1-(vSTCost/vDIYCost))*100

		document.getElementById("divTitle").style.visibility="visible"
		document.getElementById("divFooter").style.visibility="visible"
		document.getElementById("DYI").style.visibility="visible"
		document.getElementById("DIYCost").innerHTML=formatCurrency(vDIYCost)
		document.getElementById("DIYTime").innerHTML=FormatNumber(vClientManMths,1,false,false,false) + " month(s)"
		document.getElementById("sUs").style.visibility="visible"
		document.getElementById("OurCost").innerHTML=formatCurrency(vSTCost)
		document.getElementById("OurTime").innerHTML=FormatNumber(vSTMths,1,true,false,false) + " month(s)"

		vSavingsCost=vDIYCost-vSTCost
		vSavingsTime=vClientManMths-vSTMths
		document.getElementById("Svg").style.visibility="visible"
		document.getElementById("SvgPct").style.visibility="visible"
		document.getElementById("SaveCost").innerHTML="<b>" + formatCurrency(vSavingsCost)
		document.getElementById("SaveTime").innerHTML="<b>" + FormatNumber(vSavingsTime,1,false,false,false) + " month(s)"
		document.getElementById("SavePct").innerHTML="<b>&nbsp;&nbsp;" + FormatNumber(vSavingsPct,0,false,false,false) + "%"

		//vCostFines=.75*(vComp*100)
		
		//document.getElementById("Assmpts").style.visibility="visible"
		//document.getElementById("avgAR").innerHTML="Average number of Add/Remove Programs per system: <b>" + FormatNumber(parseInt(vARPs),1,false,false,true) + "</b>"
		//document.getElementById("totAR").innerHTML="Total Add/Remove Programs in environment: <b>" + FormatNumber(parseInt(vARPDataVol),1,false,false,true) + "</b>"
		//document.getElementById("unqAR").innerHTML="Unique Add/Remove Programs: <b>" + FormatNumber(parseInt(vARPUniqueRecs),1,false,false,true) + "</b>"
		//document.getElementById("avgFile").innerHTML="Average number of EXE files per system: <b>" + FormatNumber(parseInt(vFiles),1,false,false,true) + "</b>"
		//document.getElementById("totFile").innerHTML="Total EXE files in environment: <b>" + FormatNumber(parseInt(vFileDataVol),1,false,false,true) + "</b>"
		//document.getElementById("unqFile").innerHTML="Unique EXE files: <b>" + FormatNumber(parseInt(vFileUniqueRecs),1,false,false,true) + "</b>"
		//document.getElementById("orfFile").innerHTML="Orphan EXE files: <b>" + FormatNumber(parseInt(vFileOrphanRecs),1,false,false,true) + "</b>"
		//document.getElementById("Loaded").innerHTML="Staff hourly rate ($" + vHourly + " per hour plus a 20% load factor): <b>" + formatCurrency(vLoaded) + "</b>"


		vARPs=150
		vFiles=1500
		vUnique=.10
		vFileOrphans=.01
		vFilesPctIDd=.2
		
		vARPDataVol=(vComp)*vARPs
		vARPUniqueRecs=vARPDataVol*vUnique
		vARPDataNormalizationMinutesPer=.5
		vARPDataNormalization=vARPDataNormalizationMinutesPer*vARPUniqueRecs
		vARPRecIDRate=2.5
		vARPRecID=vARPRecIDRate*vARPUniqueRecs
		vARPTtlHrs=(vARPDataNormalization+vARPRecID)/60
		
		vFileDataVol=(vComp)*vFiles
		vFileUniqueRecs=vFileDataVol*vUnique*vFilesPctIDd
		vFileOrphanRecs=vFileUniqueRecs*vFileOrphans
		vFileAnalysisMinutesPer=.5
		vFileAnalysis=vFileAnalysisMinutesPer*vFileUniqueRecs
		vFileOrphanAnalysisMinutesPer=10
		vFileOrphanAnalysis=vFileOrphanAnalysisMinutesPer*vFileOrphanRecs
		vFileRecIDTime=vFileUniqueRecs*3
		vFileTtlHrs=(vFileAnalysis+vFileOrphanAnalysis+vFileRecIDTime)/60
		vTtlHrs=vFileTtlHrs

		vStaffMths=(vTtlHrs/(2002/12)/vStaff)
		vStaffCost=vTtlHrs*vLoaded
		vDIYCost=vStaffCost

		vSTAnalysisPerMth=20000
		vSTMths=vUnique/vSTAnalysisPerMth
		vSTMths=vFileUniqueRecs/vSTAnalysisPerMth
		
		vSTRate=78
		vSTRate=65
		
		vSTFTE=vSTMths*((vSTRate*2002)/12)
		vSTCost=((vSTRate*2002)/12)*vSTMths
		vSTCost=vCompCost+vServCost+vSetup+vSTFTE
	}
}
function jsReportIACP(r) {
parent.textFrame.location="intranet_main.asp?T=" + r + "&SD=" + document.getElementById("txtStartDate").value  + "&ED=" + document.getElementById("txtEndDate").value 
}
function jsShowMember(t) {
parent.textFrame.location="intranet_main.asp?T=MEMBER&MID=" + t.value
}
function jaMakeTable() {
document.frmPay.hdnNextOp.value="MAKETABLE"
document.frmPay.submit()
}
function jsEditGroup(t) {
window.location="system/sysDBMS.asp?MAINMENU=Y&TBL=tblGroups&FEXC=NO&FFLD=NONE&FVAL=N&SFLD=NONE&MODE=EDIT&ID=" + t.value
}
function jsEditSubs(t) {
window.location="system/sysDBMS.asp?MAINMENU=Y&TBL=tblMemSubscriptions&FEXC=NO&FFLD=NONE&FVAL=N&SFLD=NONE&MODE=EDIT&ID=" + t.value
}
function jsRefreshPage(cid) {
window.opener.location="intranet_main.asp?T=MEMBER&MID=" + cid
}
function jsEditNote(id) {
n="EDIT"
w="intranet_main.asp?T=CONTACTNOTES&EDIT=Y&ID=" + id
rv=jsOpenSmallWin(n,w)
}
function jsAddNote(t) {
n=""
w="intranet_main.asp?T=CONTACTNOTES&MID=" + t
rv=jsOpenSmallWin(n,w)
}
function jsPW(t,un,pw) {
if (t.value==un) {
	document.getElementById("UserPW").value=pw
	}
}
function jsSaveUserRec() {
e=document.frmPay.UserName; if (e.value=="") { alertm("",e); return }; if (e.value=="NEW") { alertm("Please provide a valid User Name.",e); return };
e=document.frmPay.Phone; 
if (e.value!="") { rv=isPhone(e); if (rv!="") { alertm("Please provide a valid phone.  " + rv,e); return } }
e=document.frmPay.Email; if (e.value=="") { alertm("",e); return };
rv=isEmail(e); if (rv!="") { alertm("Please provide a valid email address.  " + rv,e); return }
e=document.frmPay.ConfirmEmail; if (e.value=="") { alertm("",e); return };
rv=isEmail(e); if (rv!="") { alertm("Please provide a valid email address.  " + rv,e); return }
if (e.value!=document.frmPay.Email.value) { alertm("Your email and your confirm email values do not match.",e); return }
e=document.frmPay.UserPW; if (e.value=="") { alertm("",e); return };
e=document.frmPay.ConfirmPW; if (e.value=="") { alertm("",e); return };
if (e.value!=document.frmPay.UserPW.value)  { alertm("Your password and your confirm password values do not match.",e); return }
document.frmPay.hdnNextMode.value="SAVE";
document.frmPay.submit();
}
function jsForgotPW() {
jUN=document.frmPay.Email;
if (jUN.value=="") { alert("Please enter an Email Address"); jUN.focus(); return; }
rv=isEmail(jUN)
if (rv!="") { alert("Please enter a valid Email Address " + rv); jUN.focus(); return; }
document.frmPay.hdnNextOp.value="SENDPW"
document.frmPay.submit();
}
function jsRegister() {
document.frmPay.hdnNextOp.value="REGISTER"
document.frmPay.submit();
}
function openCombobox1() { cbo1.open() } 
function getWordDescription1() { var sWord = cbo1.getText();ob_post.AddParam("sWord", sWord);    }

function openCombobox2() { cbo2.open() } 
function getWordDescription2() { var sWord = cbo2.getText();ob_post.AddParam("sWord", sWord);    }

function openCombobox3() { cbo3.open() } 
function getWordDescription3() { var sWord = cbo3.getText();ob_post.AddParam("sWord", sWord);    }

function openCombobox4() { cbo4.open() } 
function getWordDescription4() { var sWord = cbo4.getText();ob_post.AddParam("sWord", sWord);    }

function openCombobox5() { cbo5.open() } 
function getWordDescription5() { var sWord = cbo5.getText();ob_post.AddParam("sWord", sWord);    }

function openCombobox6() { cbo6.open() } 
function getWordDescription6() { var sWord = cbo6.getText();ob_post.AddParam("sWord", sWord);    }

function openCombobox7() { cbo7.open() } 
function getWordDescription7() { var sWord = cbo7.getText();ob_post.AddParam("sWord", sWord);    }

function openCombobox8() { cbo8.open() } 
function getWordDescription8() { var sWord = cbo8.getText();ob_post.AddParam("sWord", sWord);    }

function openCombobox9() { cbo9.open() } 
function getWordDescription9() { var sWord = cbo9.getText();ob_post.AddParam("sWord", sWord);    }

function openCombobox10() { cbo10.open() } 
function getWordDescription10() { var sWord = cbo10.getText();ob_post.AddParam("sWord", sWord);    }

function loadWordDescription(sWordDescription) {
	document.getElementById("divWordInformation").innerHTML = "<span class='tdText' style='white-space: wrap;width:325px;'><b>" + cbo1.getText() + "</b><br /><br /><br />" + sWordDescription.replace(/\n/g, "<br />").replace(/\r\n/g, "<br />") + "</span>";        
	}            
function jsGoogle(q) {
	jsOpenMedWin("http://www.google.com/search?q=" + q)
}
function jsBing(q) {
	jsOpenMedWin("http://www.bing.com/search?q=" + q)
}
function jsChkMT(o) {
if (o.value=="") { alert(o.name + " requires a value."); o.focus; return false; } else { return true }
}
function jsPickClient() {
document.forms[0].submit()
}
function jsPickProject() {
document.forms[0].submit()	
}
function jsRunReport() {
if (!jsChkMT(document.frmPay.cmbProject)) { return false }
document.frmPay.submit()	
}
function jsPDF() {
document.frmPay.hdnNextOp.value="PDF";
document.frmPay.submit()	
}
function jsCallAliases() {
jCID=document.frmPay.cmbClient.value
jSRC=document.frmPay.txtSearch.value
if (jSRC=='') { jSRC="%" }
jMAX=document.frmPay.txtMaxAppr.value
jSHO=1
jUSR=document.frmPay.hdnUserID.value
//alert("MainGrid.aspx?RL=Y&RS=alias_List&KF=idAlias&P1=" + jCID + "&P2=" + jSRC + "&P3=" + jMAX + "&P4=" + jUSR)
top["textFrame"].location.href="MainGrid.aspx?RL=Y&RS=alias_List&PT=Aliases&KF=idAlias&P1=" + jCID + "&P2=" + jSRC + "&P3=" + jMAX + "&P4=" + jUSR
}
function jsCallPubs() {
jCID=document.frmPay.cmbClient.value
if (jCID=='') { jCID=0 }
jPID=document.frmPay.cmbProject.value
if (jPID=='') { jPID=0 }
jSRC=document.frmPay.txtSearch.value
if (jSRC=='') { jSRC="%" }
jMAX=document.frmPay.txtMaxAppr.value
jUSR=document.frmPay.hdnUserID.value
top["textFrame"].location.href="MainGrid.aspx?RL=Y&RS=publisher_List&PT=Publishers&KF=idPublisher&P1=" + jCID + "&P2=" + jPID + "&P3=" + jSRC + "&P4=" + jMAX + "&P5=" + jUSR
}
function jsCallApps() {
jCID=document.frmPay.cmbClient.value
if (jCID=='') { jCID=0 }
jPID=document.frmPay.cmbProject.value
if (jPID=='') { jPID=0 }
jSRC=document.frmPay.txtSearch.value
if (jSRC=='') { jSRC="%" }
jMAX=document.frmPay.txtMaxAppr.value
jUSR=document.frmPay.hdnUserID.value
top["textFrame"].location.href="MainGrid.aspx?RL=Y&RS=application_List&PT=Applications&KF=idApplication&P1=" + jSRC + "&P2=" + jMAX + "&P3=" + jUSR
}
function jsCallARP() {
jCID=document.frmPay.cmbClient.value
if (jCID=='') { jCID=0 }
jPID=document.frmPay.cmbProject.value
if (jPID=='') { jPID=0 }
jSRC=document.frmPay.txtSearch.value
if (jSRC=='') { jSRC="%" }
jMAX=document.frmPay.txtMaxAppr.value
jSHO=1
jUSR=document.frmPay.hdnUserID.value
top["textFrame"].location="MainGrid.aspx?RL=Y&RS=arp_List&PT=ARP&KF=idImported&P1=" + jCID + "&P2=" + jPID + "&P3=" + jSRC + "&P4=" + jMAX + "&P5=" + jUSR
}
function jsCallARP2() {
jCID=document.frmPay.cmbClient.value
if (jCID=='') { jCID=0 }
jPID=document.frmPay.cmbProject.value
if (jPID=='') { jPID=0 }
jSRC=document.frmPay.txtSearch.value
if (jSRC=='') { jSRC="%" }
jMAX=document.frmPay.txtMaxAppr.value
jSHO=1
jUSR=document.frmPay.hdnUserID.value
top["textFrame"].location="MainGrid.aspx?RL=Y&RS=arp_List&TT=ARP2&PT=ARP&KF=idImported&P1=" + jCID + "&P2=" + jPID + "&P3=" + jSRC + "&P4=" + jMAX + "&P5=" + jUSR
}
function jsCallARPForm() {
jCID=document.frmPay.cmbClient.value
if (jCID=='') { jCID=0 }
jPID=document.frmPay.cmbProject.value
if (jPID=='') { jPID=0 }
jSRC=document.frmPay.txtSearch.value
if (jSRC=='') { jSRC="%" }
jMAX=document.frmPay.txtMaxApprLvl.value
jUSR=document.frmPay.hdnUserID.value
document.frmPay.hdnRL.value="Y"
document.frmPay.hdnRS.value="arp_list"
document.frmPay.hdnKF.value="idImported"
document.frmPay.hdnP1.value=jCID
document.frmPay.hdnP2.value=jPID
document.frmPay.hdnP3.value=jSRC
document.frmPay.hdnP4.value=jMAX
document.frmPay.hdnP5.value=jUSR
document.frmPay.submit()
}
function jsCallFiles() {
jCID=document.frmPay.cmbClient.value
if (jCID=='') { jCID=0 }
jPID=document.frmPay.cmbProject.value
if (jPID=='') { jPID=0 }
jSRC=document.frmPay.txtSearch.value
if (jSRC=='') { jSRC="%" }
jMAX=document.frmPay.txtMaxAppr.value
jSHO=1
jUSR=document.frmPay.hdnUserID.value
//alert("MainGrid.aspx?RL=Y&RS=file_List&KF=idImported&P1=" + jCID + "&P2=" + jPID + "&P3=" + jSRC + "&P4=" + jMAX + "&P5=" + jUSR)
top["textFrame"].location.href="MainGrid.aspx?RL=Y&RS=file_List&PT=Files&KF=idImported&P1=" + jCID + "&P2=" + jPID + "&P3=" + jSRC + "&P4=" + jMAX + "&P5=" + jUSR
}
function decode(str) {
var result = "";
for (var i = 0; i < str.length; i++) {
	if (str.charAt(i) == "+") {
  		result += " ";
  		}
  	else {
	  	result += str.charAt(i);
		}
	}
return unescape(result);
}
function jsClearCrit(jLvl) {
f=document.frmPay;
f.txtMaxAppr.value=jLvl;
f.txtSearch.value='';
f.cmbProject.value='';
}
function jsRecalcStats() {
	document.frmPay.hdnNextOp.value="RECALCSTATS"
	document.frmPay.submit()
}

var jsTblName;
var jsAttachTo;
var jClientText="";
var jProjectText="";
var jMaxApprText="";
var jCurrentOp="Edit"

function OnDelete(record) {
	//grid1.refresh()
}
function jsChgPubInfoID(jSQL,jID) {
rv=ob_post.post(null, "vbSQLSP");

jsAjaxData(jID,jSQL,'publisherName','publisherWebSite')
if ((frames.iFrameFly.document.getElementById("publisherWebSite").value!='') && (frames.iFrameFly.document.getElementById("publisherWebSite").value!='n/a')) { frames.iFrameFly.document.getElementById("btnPubWebSite").disabled=false } else  { frames.iFrameFly.document.getElementById("btnPubWebSite").disabled=true }
}
function zjsCheckAppPub(jPubID,jAppName,jFlds) {
//for (i in jFlds) { alert(jFlds[i]) }
jSQL="publisherAppName_Search"
jParam="vKeyFld=idPublisher|vMainID=" + jPubID.value + "|vKeyFld2=appName|vMainID2=" + jAppName.value
ob_post.AddParam("vSQL", jSQL); 
ob_post.AddParam("vParam", jParam); 
rv=ob_post.post(null, "vbSQLSP");
try {
    o=frames.iFrameFly.document
    isFrame=true;
    }
catch(e) {
    o=document
    }
for (i in rv) { 
	//alert(frames.iFrameFly.document.getElementById(jFlds[i]).value)
	if (o.getElementById(jFlds[i])) { o.getElementById(jFlds[i]).value=rv[i] }
	}
}
function jsAjaxData(jID,jSQL,jCtrl1,jCtrl2) {
ob_post.AddParam ("vID", jID); 
jSQL=decode(jSQL)
ob_post.AddParam ("vSQL", jSQL); 
rv=ob_post.post(null, "vbGetSQLData");
if (jCtrl1!="") { frames.iFrameFly.document.getElementById(jCtrl1).value=rv[0] }
if (jCtrl2!="") { frames.iFrameFly.document.getElementById(jCtrl2).value=rv[1] }
}
function jsAlert(s) {
windowalert(s,'Information','error');
}
function jsAliases() {
jCID=document.getElementById("txtClientID").value
jPID=document.getElementById("txtProjectID").value
jSRC=document.getElementById("txtSearch").value
if (jSRC=='') { jSRC="M%" }
jMAX=document.getElementById("txtMaxApprLvl").value
jSHO=1
jUSR=document.getElementById("txtUserID").value
s="MainGrid.aspx?RL=Y&RS=alias_List&KF=idAlias&P1=" + jCID + "&P2=" + jSRC + "&P3=" + jMAX + "&P4=" + jSHO + "&P5=" + jUSR + "&CID=" + jCID + "&PID=" + jPID + "&MAX=" + jMAX + "&SRC=" + jSRC + "&TTL=ALIASES"
window.location=s
}
function jsPublishers() {
jCID=document.getElementById("txtClientID").value
jPID=document.getElementById("txtProjectID").value
jSRC=document.getElementById("txtSearch").value
if (jSRC=='') { jSRC="M%" }
jMAX=document.getElementById("txtMaxApprLvl").value
jSHO=1
jUSR=document.getElementById("txtUserID").value
s="MainGrid.aspx?RL=Y&RS=publisher_Maint&KF=idPublisher&P1=" + jSRC + "&CID=" + jCID + "&PID=" + jPID + "&MAX=" + jMAX + "&SRC=" + jSRC + "&TTL=PUBS"
window.location=s
}
function jsARPs() {
jCID=document.getElementById("txtClientID").value
jPID=document.getElementById("txtProjectID").value
jSRC=document.getElementById("txtSearch").value
if (jSRC=='') { jSRC="%" }
jMAX=document.getElementById("txtMaxApprLvl").value
jSHO=1
jUSR=document.getElementById("txtUserID").value
s="MainGrid.aspx?RL=Y&RS=arp_List&KF=idImported&P1=" + jCID + "&P2=" + jPID + "&P3=" + jSRC + "&P4=" + jMAX + "&P5=" + jUSR + "&CID=" + jCID + "&PID=" + jPID + "&MAX=" + jMAX + "&SRC=" + jSRC + "&TTL=ARP"
window.location=s
}
function jsFiles() {
jCID=document.getElementById("txtClientID").value
jPID=document.getElementById("txtProjectID").value
jSRC=document.getElementById("txtSearch").value
if (jSRC=='') { jSRC="%" }
jMAX=document.getElementById("txtMaxApprLvl").value
jSHO=1
jUSR=document.getElementById("txtUserID").value
s="MainGrid.aspx?RL=Y&RS=file_List&KF=idImported&P1=" + jCID + "&P2=" + jPID + "&P3=" + jSRC + "&P4=" + jSHO + "&P5=" + jUSR + "&CID=" + jCID + "&PID=" + jPID + "&MAX=" + jMAX + "&SRC=" + jSRC + "&TTL=FILES"
window.location=s
}
function jsChgPubInfoName(v,jTyp) {
jsGetSQLData('publisherName','publisherName',v,'str','idPublisher','idPublisher')
o=frames.iFrameFly.document
v=o.getElementById('idPublisher').value
jsGetSQLData('publisher','idPublisher',v,'int','webSite','publisherWebSite')
}
function jsGetSQLData(jRS,jIDFld,jID,jTyp,jFld,jCtrl) {
ob_post.AddParam ("vRS", jRS); 
ob_post.AddParam ("vIDFld", jIDFld); 
ob_post.AddParam ("vID", jID); 
ob_post.AddParam ("vTyp", jTyp); 
ob_post.AddParam ("vFld", jFld); 
rv=ob_post.post(null, "vbGetData");
o=frames.iFrameFly.document
if (rv!=undefined) { o.getElementById(jCtrl).value=rv } else { alert("Could not find a match.") }
}
function stopEventPropagation(e) {
	if(!e){e=window.event;}
	if(!e){return false;}
	e.cancelBubble=true;
	if(e.stopPropagation){e.stopPropagation();}
	}
function zjsSelectReport(t,e) {
	alert(t.contentCell.innerHTML)
	}
function jsSelectExport(t,e) {
	alert(t.contentCell.innerHTML)
	}
function jsSelectClient(t,e) {
	v=t.contentCell.innerHTML
	s=e.innerHTML
	jNewClientText="Client: " + v
	if (jClientText!="") {
		s=s.replace(jClientText, jNewClientText);
		}
	else	{
		s=s.replace("Select Client", jNewClientText);
		}
	jClientText=jNewClientText
	e.innerHTML=s
	s="select idClient from client where name=^zzID^"
	rv=jsAjaxData(v,s,document.getElementById("txtClientID"),"")
	}
function jsSelectProject(t,e) {
	v=t.contentCell.innerHTML
	s=e.innerHTML
	jNewProjectText="Project: " + v
	if (jProjectText!="") {
		s=s.replace(jProjectText, jNewProjectText);
		}
	else	{
		s=s.replace("Select Project", jNewProjectText);
		}
	jProjectText=jNewProjectText
	e.innerHTML=s
	s="select idProject from project where name=^zzID^"
	rv=jsAjaxData(v,s,document.getElementById("txtProjectID"),"")
	}
function jsSelectMaxApprLvl(t,e) {
	v=t.contentCell.innerHTML
	s=e.innerHTML
	jNewMaxApprText="Approval Level: " + v
	if (jMaxApprText!="") {
		s=s.replace(jMaxApprText, jNewMaxApprText);
		}
	else	{
		s=s.replace("Max Approval Level", jNewMaxApprText);
		}
	jMaxApprText=jNewMaxApprText
	e.innerHTML=s
	s="select ApprovalLevel from tlkApprovalLevels where ApprovalLevelDesc=^zzID^"
	rv=jsAjaxData(v,s,document.getElementById("txtMaxApprLvl"),"")
	}
function jsCallPage(t) {
    window.location="?T=" + (t.contentCell.innerHTML)
    }
function exportToWord() {
    window.location="?OP=Word_Export"
	}
function printGrid() {
    grid1.print();
    }	    
function jsCopyRecs(t) {
	}
function jsDeleteRecs(t) {
	if (confirm("Delete the selected records?")) {
		alert("Alright, you asked for it")
		}
	}
function jsSetLevels() {
    try {
        o=frames.iFrameFly.document
        isFrame=true;
        }
    catch(e) {
        o=document
        }
    jDoAppLevels=true
    if (o.getElementById("pageType")) { if (o.getElementById("pageType").value=="PUBLISHER_LIST") { jDoAppLevels=false } }
    if (jDoAppLevels) {
		if (o.getElementById("appLastUserNameToIncrementApprovalLevel")) { o.getElementById("appLastUserNameToIncrementApprovalLevel").value="" }
		var jImpAppr=0; jAppAppr=0;
		if (o.getElementById("impApprovalLevel")) { jImpAppr=parseInt(o.getElementById("impApprovalLevel").value); }
		if (o.getElementById("appApprovalLevel")) { jAppAppr=parseInt(o.getElementById("appApprovalLevel").value); }
		if (jAppAppr>jUserAdminLevel) { 
			if (o.getElementById("idPublisher")) { o.getElementById("idPublisher").disabled=true }
			if (o.getElementById("publisherName")) { o.getElementById("publisherName").disabled=true }
			if (o.getElementById("idApplicationType")) { o.getElementById("idApplicationType").disabled=true }
			if (o.getElementById("idLicenseType")) { o.getElementById("idLicenseType").disabled=true }
			if (o.getElementById("idCategory")) { o.getElementById("idCategory").disabled=true }
			if (o.getElementById("appName")) { o.getElementById("appName").disabled=true }
			}
		if (jImpAppr>jUserAdminLevel) { 
			if (o.getElementById("majorVersion")) { o.getElementById("majorVersion").disabled=true }
			if (o.getElementById("releaseVersion")) { o.getElementById("releaseVersion").disabled=true }
			}
	    var jDisabled=false;
		if (o.getElementById("AL")) { 
			//o.getElementById("AL").value=jAppAppr
			//if (jImpAppr<jAppAppr) { o.getElementById("AL").value=jImpAppr }
			var oimp=0; oapp=0;
			if (o.getElementById("impLastIdUserToIncrementApprovalLevel")) { oimp=o.getElementById("impLastIdUserToIncrementApprovalLevel").value; }
			if (o.getElementById("appLastIdUserToIncrementApprovalLevel")) { oapp=o.getElementById("appLastIdUserToIncrementApprovalLevel").value }
			if (parseInt(o.getElementById("AL").value)>=jUserAdminLevel) { 
				jDisabled=true 
				} 
			else if ((jUserID==oimp) || (jUserID==oapp)) { 
				jDisabled=true 
				} 
			}
		}
	jUserLevel=getCookie("vUserLevel")
	if (jUserLevel.indexOf("ADMIN")>-1) { jDisabled=false; } 
	if (o.getElementById("btnIncrementLevel")) { o.getElementById("btnIncrementLevel").disabled=jDisabled }
}
function randomString() {
	var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";
	var string_length = 8;
	var randomstring = '';
	for (var i=0; i<string_length; i++) {
		var rnum = Math.floor(Math.random() * chars.length);
		randomstring += chars.substring(rnum,rnum+1);
	}
	return randomstring;
}
function jsValChecks(f) {
for (eli=0;eli<f.elements.length;eli++) { 
	var e=f.elements[eli]
	if ((!e.alt=="") || (!e.dataFld=="")) { 
		vc=e.dataFld
		if (vc==undefined) { vc=e.alt }
		vv=e.value
		vn=e.name
		if (vn.substr(0,4)=="DOM_") { vn=vn.substr(4) }
		if ((vv=="") && (vc.indexOf("VC_REQUIRED")>-1)) { 
			alert("Sorry.  " + vn + " requires a entry."); e.focus(); return false;
			}
		if ((vv!="") && (vc.indexOf("VC_EQ")>-1)) { 
			p1=vc.indexOf("[")
			p2=vc.indexOf("]")
			fld=vc.substr(p1+1,p2-p1-1)
			if (vv!=document.getElementById(fld).value) {
				alert("Your confirmation entries do not match."); document.getElementById(fld).focus(); return false;
				}
			}
		if ((vv!="") && (vc.indexOf("VC_NUMBER")>-1)) { 
			rv=isNumber(e.value);
			if (rv!="") { alert(mBadNumber + ":" + e.name + ":" + rv); e.focus(); return false; }
			e.value=MakeNumber(e)
			}
		if ((vv!="") && (vc.indexOf("VC_CURRENCY")>-1)) { 
			rv=isCurrency(e.value);
			if (rv!="") { alert(mBadCurrency + ":" + e.name + ":" + rv); e.focus(); return false; }
			e.value=MakeNumber(e)
			}
		if ((vv!="") && (vc.indexOf("VC_DATE")>-1)) { 
			rv=couldBeDate(e.value);
			if (rv!="") { alert(mBadDate + ":" + e.name + ":" + rv); e.focus(); return false; }
			}
		if ((vv!="") && (vc.indexOf("VC_ZIP")>-1)) { 
			rv=isZip(e);
			if (rv!="") { alert(mBadZip + ":" + e.name + ":" + rv); e.focus(); return false; }
			}
		if ((vv!="") && (vc.indexOf("VC_PHONE")>-1)) { 
			rv=isPhone(e);
			if (rv!="") { alert(mBadPhone + ":" + e.name + ":" + rv); e.focus(); return false; }
			}
		if ((vv!="") && (vc.indexOf("VC_EMAIL")>-1)) { 
			rv=isEmail(e);
			if (rv!="") { alert(mBadEmail + ":" + e.name + ":" + rv); e.focus(); return false; }
			}
		if ((vv!="") && (vc.indexOf("VC_URL")>-1)) { 
			rv=isURL(e);
			if (rv!="") { alert(mBadURL + ":" + e.name + ":" + rv); e.focus(); return false; }
			}
		if ( (vv!="") && (vc.indexOf("VC_STRIPQUOTES")>-1) ) { 
			v=e.value;
			x=""
			for (iz=0;iz<v.length;iz++) {
				if ((v.charCodeAt(iz)!=34) && (v.charCodeAt(iz)!=39)) { x=x + v.charAt(iz) }
				}
			e.value=x;
			}
		}
	}
f.submit();
return true
}
function jsSaveRec(f,jSP) {
var flds="";
var vals="";
for (eli=0;eli<f.elements.length;eli++) { 
	if (f.elements[eli].name!="") {
		flds=flds + "'" + f.elements[eli].name + "',"
		vals=vals + "'" + f.elements[eli].value + "',"
		}
	}
flds=flds.substr(0,flds.length-1)+""
vals=vals.substr(0,vals.length-1)+""
ob_post.AddParam("vRS",jSP);
ob_post.AddParam("vFlds",flds);
ob_post.AddParam("vVals",vals);
ob_post.post(null, "vbSaveRecordFromArrays")
return true;
}
function getCookie(c_name) {
if (document.cookie.length>0)
  {
  c_start=document.cookie.indexOf(c_name + "=");
  if (c_start!=-1)
    {
    c_start=c_start + c_name.length+1;
    c_end=document.cookie.indexOf(";",c_start);
    if (c_end==-1) c_end=document.cookie.length;
    return unescape(document.cookie.substring(c_start,c_end));
    }
  }
return "";
}
function jsLoadProjects(jClientID) {
if (document.getElementById("cmbProject")) {
	ob_post.AddParam("vSQL", "spGetProjects"); 
	ob_post.AddParam("vP1",jClientID);
	rv=ob_post.post(null, "vbSQLSP");
	var vals = new Array()
	var txts = new Array()
	ct=0
	o=document.getElementById("cmbProject")
	o.options.length=1
	for (i in rv) { 
		vals[i]=rv[i]; 
		}
	ct=i
	for (i=0;i<=ct;i++) {
		var oOption = document.createElement("OPTION"); 
		x=(i%2)
		if (x==1) { 
			oOption.text=vals[i];
			oOption.value=vals[i-1];
			o.add(oOption); 	
			}
		}
	}
if (document.getElementById("lstValidators")) {
	ob_post.AddParam("vSQL", "spGetValidators"); 
	ob_post.AddParam("vP1",jClientID);
	rv=ob_post.post(null, "vbSQLSP");
	var vals = new Array()
	var txts = new Array()
	ct=0
	o=document.getElementById("lstValidators")
	o.options.length=1
	for (i in rv) { 
		vals[i]=rv[i]; 
		}
	ct=i
	for (i=0;i<=ct;i++) {
		var oOption = document.createElement("OPTION"); 
		x=(i%2)
		if (x==1) { 
			oOption.text=vals[i];
			oOption.value=vals[i-1];
			o.add(oOption); 	
			}
		}
	}
}
function jsSaveContact() {
f=document.forms['frmContact']
rv=jsValChecks(f);
if (rv) {
	rv=jsSaveRec(f,"spSaveContacts")
	vFrom="web@softwareidtech.com"
	vFrom="info@softwareidtech.com"
	vSubject="New Contact Entry on Website"
	ob_post.AddParam("vFrom", vFrom); 
	ob_post.AddParam("vTo","doug@gneo.net");
	ob_post.AddParam("vSubject",vSubject);
	vBody="<font face=Arial size=2>"
	vBody=vBody + "New Contact record.<br><br>Name: " + f.ContactName.value + "<br><br>Email: " + f.Email.value + "<br><br>Comment: " + f.Comments.value + "<br>"
	ob_post.AddParam("vBody",vBody);
	ob_post.AddParam("vPriority","HIGH");
	rv=ob_post.post(null, "vbSendEmail");
	alert('Thank you for contacting us.  We will be in touch.');
	window.location='?P='
	}
}
function jsSubmitDemoCode() {
dc=document.getElementById("DemoCode").value
x=document.getElementById("hdnNextID").value
rv=ob_post.AddParam("vSQL", "select WebDemoID from tWebDemo where WebDemoID=" + x + " and RandomCode='" + dc + "'"); 
rv=ob_post.post(null, "vbSQLRun");
if (rv==false) {
	alert("Sorry, but I cannot find this evaluation code in our tables.")
	return
	}
document.forms[1].hdnNextVal.value=3
document.forms[1].submit()
}
function jsSubmitDemo() {
f=document.forms["frmDemo"]
jsRandom=randomString()
o=f.Name; if (o.value=="") { AlertEm("Please enter your name.",o); return; }
o=f.Email; if (o.value=="") { AlertEm("Please enter your email.",o); return; }
o=f.Email; rv=isEmail(o); if (rv!="") { AlertEm(rv,o); return; }
ob_post.AddParam("vSQL", "spWebDemo"); 
ob_post.AddParam("vP1",f.Name.value);
ob_post.AddParam("vP2",f.Email.value);
ob_post.AddParam("vP3",jsRandom);
rv=ob_post.post(null, "vbSQLSPOut");
if (rv[1]==99) {
	alert(rv[2])
	return;
	}
if (rv[1]==0) {
	vID=rv[2]
	}
f.hdnNextID.value=vID
vFrom="info@softwareidtech.com"
vSubject="SiTech Demo Evaluation Code"
vBody="Test"
vPriority="High"
ob_post.AddParam("vFrom", vFrom); 
ob_post.AddParam("vTo",f.Email.value);
ob_post.AddParam("vSubject",vSubject);
vBody="<font face=Arial size=2>"
vBody=vBody + f.Name.value + "<br><br>Thank you for registering for SiTech's demo evaluation.<br><br>Your evaluation code is <b>" + jsRandom + "</b><br><br>Please enter it in the field provided.<br><br>Thank you for your interest.<br>Sitech Web Support"
ob_post.AddParam("vBody",vBody);
ob_post.AddParam("vPriority","HIGH");
rv=ob_post.post(null, "vbSendEmail");
if (rv=="Your email has been sent.") {
	rv="An email has been sent to this address.  Please use the Demo Evaluation code provided to access the demo."
	alert(rv)
	}
f.hdnNextVal.value=2
f.submit()
}
function SelectProjectTab(itemID,jPage,jID1,jID2,jKF1,jKF2) {
	if (jID1=="")  { alert("Please specify criteria."); return; }
	ob_em_SelectItem(itemID);
	document.getElementById('tabIframe').src="ProjectPages.aspx?WNM=<%=vWindowName%>&T=" + itemID + "&P=" + jPage + "&ID1=" + jID1 + "&ID2=" + jID2 + "&KF1=" + jKF1 + "&KF2=" + jKF2;
}
function SelectDemoTab(itemID,jPage,jID1,jID2,jKF1,jKF2) {
	ob_em_SelectItem(itemID);
	document.getElementById('tabIframe').src="DemoPages.aspx?WNM=<%=vWindowName%>&T=" + itemID + "&P=" + jPage + "&ID1=" + jID1 + "&ID2=" + jID2 + "&KF1=" + jKF1 + "&KF2=" + jKF2;
}
function jsTryEmail() {
	document.getElementById("hdnNextVal").value=2
	document.forms[0].submit() 
}
function jsSearchOrgs(t) {
if ((alltrim(t.value)=="") || (t.value=="")) { alert("Please enter a search value."); return; }
window.location="?P=SEARCHORGS&SV=" + t.value
}
function jsTriggerFind(t) {
if (event.keyCode=='13') {
	jsFindOrg(t)
	}
}
function jsShowDiv(jDiv,jDivID,jMsg) {
if (document.getElementById(jDiv).style.display=="block") {
	document.getElementById(jDivID).innerHTML=jMsg
	document.getElementById(jDiv).style.display="none"
	}
else {
	document.getElementById(jDivID).innerHTML="> Click to hide."
	document.getElementById(jDiv).style.display="block"
	}
}
function jsChgRdoVal(t,f) {
v=t.dataFld
v=t.value
document.getElementById(f).value=v
}
function isZip(vField) {
v = vField.value;
for (iz=0;iz<v.length;iz++) {
	if ((v.charCodeAt(iz) != 45) && ((v.charCodeAt(iz)<48) || (v.charCodeAt(iz)>57))) {
		return ' (not a number)'
		}
	}
if (v.length<5) {
	return ' (length is less than 5)'
	}
if (v.length>5) {
	if (v.indexOf("-")==-1) { 
		v=v.substr(0,5) + "-" + v.substr(5) 
		vField.value=v;
		}
	if (v.length != 10) {
		return ' (length is less than 10)'
		}
	if (v.indexOf('-') != 5) {
		return ' (no dash)'
		}
	}
return "";
}	
function isURL(vField) {
v=vField.value
u = v.toUpperCase();
if ((u.substr(0,7) != 'HTTP://') || (u.substr(0,7) != 'HTTP:\\')) {
	vField.value = 'http://' + vField.value;
	}
ct = 0;
for (i=0;i<v.length;i++) {
	if (v.charAt(i) == '.') {
		ct = ct + 1;
		}
	}
if (ct<2) {
	return " (missing periods) ";
	}
return "";
}
function isPhone(vField) {
v = vField.value;
n = ''
for (i=0;i<v.length;i++) {
	if ((v.charCodeAt(i)!=32) && (v.charCodeAt(i)>47) && (v.charCodeAt(i)<58)) {
		n = n + v.charAt(i);
		}
	}
if ((n.length != 7) && (n.length != 10)) { 
	return "Phone numbers should be 7 or 10 digits long"
	}
if (n.length==7) {
	n = n.substr(0,3) + '-' + n.substr(3,4);
	vField.value=n;
	}
else { 
	if (n.length==10) {
		n = '(' + n.substr(0,3) + ')' + n.substr(3,3) + '-' + n.substr(6,4);
		vField.value=n;
		}
	}
return "";
}
function isEmail(v) {
n=v.value
if (n.indexOf('.')==-1) { return " (missing period)" }
if (n.indexOf('@')==-1) { return " (missing @)" }
return "";
}
function isNumber(v) {
for (iN=0;iN<v.length;iN++) {
	x=v.charCodeAt(iN)
	if ( (x!=44) && (x!=46) && ( (x<48) || (x>57) ) ) {
		return "Non-numeric character.";
		}
	}
return "";
}
function isCurrency(v) {
for (iN=0;iN<v.length;iN++) {
	x=v.charCodeAt(iN)
	if ( (x!=36) && (x!=44) && (x!=46) && ( (x<48) || (x>57) ) ) {
		return "Non-currency character.";
		}
	}
return "";
}
function isChar(v) {
for (iN=0;iN<v.length;iN++) {
	if ((v.charCodeAt(iN)<65) || (v.charCodeAt(iN)>90)) {
		if ((v.charCodeAt(iN)<97) || (v.charCodeAt(iN)>122)) {
			return 0;
			}
		}
	}
return 1;
}
function couldBeDate(v) {
if (v.length>10) {
	return "Too Long";
	}
nn=""
for (iN=0;iN<v.length;iN++) {
	if ((v.charCodeAt(iN)<48) || (v.charCodeAt(iN)>57)) {
		if ((v.charCodeAt(iN) != 32) && (v.charCodeAt(iN) != 47) && (v.charCodeAt(iN) != 92) && (v.charCodeAt(iN) != 46) && (v.charCodeAt(iN) != 45)) { 		//back/forward slash, period, dash
			return "Illegal Characters (" + v.charCodeAt(iN) + ")";
			}
		else
			nn = nn + "/";
		}
	else	{
		nn = nn + v.charAt(iN);
		}
	}
p = nn.indexOf("/");
m = nn.substr(0,p);
if ((m<1) || (m>12)) { return "Bad Month"; }
y = nn.substr(nn.lastIndexOf("/")+1)
if (y.length==3) { return "Bad Year"; }
var ny = new Number(y)
if (y.length<3) {
	if ((ny>50) && (ny<100) && (y.length=2)) { y = "19" + y }
	if ((ny<50) && (ny<100) && (y.length=2)) { y = "20" + y }
	}
var ny = new Number(y)
if (y.length==4) {
	if ((ny<1900) || (ny>2100)) { return "Bad Year"; }
	}
d = nn.substr(nn.indexOf("/")+1,nn.lastIndexOf("/")-nn.indexOf("/")-1);
var ny = new Number(d)
if ((ny>31) || (ny<1)) { return "Day out of range"; }
if (d == "31") { if ((m != 1) && (m != 3) && (m != 5) && (m != 7) && (m != 8) && (m != 10) && (m != 12)) { return "Day out of range"; } else { return ""; }	}
if (d == "30") { if ((m!=3) && (m != 4) && (m != 6) && (m != 9) && (m != 11)) { return "Day out of range"; }	else { return ""; }	}
if (d == "29") { if ((m == 2)) { return "Day out of range"; }	}
return "";
}
function AlertEm(m,o) {
alert(m);
try { o.focus(); } catch(e) { }
}
function jsRegSubmit(f) {
f=document.forms[0]
if (f.ConfirmYN) {
	o=f.ConfirmYN; if (!o.checked) { AlertEm("Please confirm that you understand and agree with the Terms and Conditions.",o); return; }
	}
if (f.StudentID) {
	o=f.StudentID; if (o.value=="") { AlertEm("Please enter your 5-digit Student ID.",o); return; }
	if (o.value.length!=5) { AlertEm("Please correctly enter your 5-digit Student ID.",o); return; }
	}
if (f.FirstName) { o=f.FirstName; if (o.value=="") { AlertEm("Please enter your first name.",o); return; } }
if (f.LastName) { o=f.LastName; if (o.value=="") { AlertEm("Please enter your last name.",o); return; } }
if (f.SocSecNbr4) { 
	o=f.SocSecNbr4; if (o.value=="") { AlertEm("Please enter the last 4 digits of your social security number.",o); return; }
	if (o.value.length>4) { AlertEm("Please correctly enter the last 4 digits of your social security number.",o); return; }
	if (o.value.length<4) { AlertEm("Please correctly enter the last 4 digits of your social security number.",o); return; }
	rv=isNumber(o.value); if (rv!="") { AlertEm("Please correctly enter the last 4 digits of your social security number.",o); return; }
	}
if (f.EmailAddress) {
	o=f.EmailAddress; if (o.value=="") { AlertEm("Please enter a valid email.",o); return; }
	vEMail=o.value
	rv=isEmail(o); if (rv!="") { AlertEm("Please enter a valid email.",o); return; }
	if (f.EmailAddress2) {
		o2=f.EmailAddress2
		vEMail2=o2.value
		rv=isEmail(o2); if (rv!="") { AlertEm("Please enter a valid email.",o2); return; }
		if (vEMail!=vEMail2) { AlertEm("The two e-mail addresses you entered do not match.",o2); return; }
		}
	}
if (f.DayPhone) { 
	if ((f.DayPhone.value=="") && (f.EvePhone.value=="") && (f.OthPhone.value=="")) {
		AlertEm("Please enter at least one phone number.",f.DayPhone); return; 
		}
	}		
if (f.MacPC) { o=f.MacPC; if (o.value=="") { AlertEm("Please select your laptop platform.",o); return; } }
f.submit();
}
function jsEnableContinue(t) {
document.forms[0].btnContinue.disabled=false
}
function generateCC(v,e){
	var cc_number = new Array(16);
	var cc_len = 16;
	var start = 0;
	var rand_number = Math.random();
	switch(v.value)
    {
		case "Visa":
			cc_number[start++] = 4;
			break;
		case "Discover":
			cc_number[start++] = 6;
			cc_number[start++] = 0;
			cc_number[start++] = 1;
			cc_number[start++] = 1;
			break;
		case "MasterCard":
			cc_number[start++] = 5;
			cc_number[start++] = Math.floor(Math.random() * 5) + 1;
			break;
		case "Amex":
			cc_number[start++] = 3;
			cc_number[start++] = Math.round(Math.random()) ? 7 : 4 ;
			cc_len = 15;
			break;
    }
    
    for (var i = start; i < (cc_len - 1); i++) {
		cc_number[i] = Math.floor(Math.random() * 10);
    }
	
	var sum = 0;
	for (var j = 0; j < (cc_len - 1); j++) {
		var digit = cc_number[j];
		if ((j & 1) == (cc_len & 1)) digit *= 2;
		if (digit > 9) digit -= 9;
		sum += digit;
	}
	
	var check_digit = new Array(0, 9, 8, 7, 6, 5, 4, 3, 2, 1);
	cc_number[cc_len - 1] = check_digit[sum % 10];
	
	e.value = "";
	for (var k = 0; k < cc_len; k++) {
		e.value += cc_number[k];
	}
}
function zjsPayNow() {
o=document.frmPay.PayMethod
if (o.value=="") { alert("Please specify Pay Method."); o.focus(); return; }
o=document.frmPay.CreditCardNbr
if (o.value=="") { alert("Please specify a correct Credit Card number."); o.focus(); return; }
o=document.frmPay.ExpirationMth
if (o.value=="") { alert("Please specify Expiration Month."); o.focus(); return; }
o=document.frmPay.ExpirationYear
if (o.value=="") { alert("Please specify Expiration Year."); o.focus(); return; }
document.frmPay.imgPayNow.style.visibility='hidden';
document.frmPay.hdnPayThis.value='PAY';
document.getElementById('divPayNow').innerHTML="<font size=1 color=red>Please wait while we <br>process your payment..</font>";
document.frmPay.submit()
}
function alltrim(s) {
return s.replace(/^\s+|\s+$/g,"")
}
function jsConfirmInstructions() {
if (document.getElementById("divConfirmInstructions")) {
	document.getElementById("divConfirmInstructions").style.display="none";
	document.getElementById("divDownloadLink").style.display="block";
	}
}
function jsShowRow(n) {
o=document.getElementById(n)
o.style.display="block"
}
function jsHideRow(n) {
o=document.getElementById(n)
o.style.display="none"
}
function jsOpenDiv(n) {
o=document.getElementById(n)
if (o.style.display=="none") {
	o.style.display="block"
	}
else {
	if (o.style.display=="block") {
		o.style.display="none"
		}
	}
}
function jsShowHideComments() {
o=document.getElementById("FAQID")
if (o.value!="") { document.getElementById("divComment").style.visibility="hidden"; } else { document.getElementById("divComment").style.visibility="visible"; }
}
function jsLoadFAQs(t,n) {
o=document.getElementById(n)
ob_post.AddParam("vSQL", "spGetFAQs"); 
ob_post.AddParam("vP1",t.value);
rv=ob_post.post(null, "vbSQLGetRecs");
var vals = new Array()
var txts = new Array()
ct=0
o.options.length=1
for (i in rv) { 
	vals[i]=rv[i]; 
	ct=ct+1
	}
for (i=0;i<=ct;i++) {
	var oOption = document.createElement("OPTION"); 
	x=(i%2)
	if (x==1) { 
		oOption.text=vals[i];
		oOption.value=vals[i-1];
		o.add(oOption); 	
		}
	}
jsCheckAndShowRow(t,'trFAQ','FAQID')
}
function jsCheckAndShowRow(t,n,m) {
if ((t.id=="SecuritySoftware") && (t.value=="Other (specify)")) { jsShowRow(n); document.getElementById(m).focus() } else { jsHideRow(n) }
if ((t.id=="FAQCategory") && (t.value!="")) { jsShowRow(n); document.getElementById(m).focus() } else { jsHideRow(n) }
}
function jsSupportAdmin(n,jSP) {
f=document.forms[n]
rv=jsValChecks(f);
rv=true
if (rv) {
	rv=jsSaveRec(f,jSP)
	if (rv) {
		ob_post.AddParam("vFromName", "Website query"); 
		ob_post.AddParam("vFrom", "adminsupport@extegrity.com"); 
		ob_post.AddParam("vTo","doug@gneo.net");
		ob_post.AddParam("vSubject","New " + f.Category.value + " Question");
		vBody="<font face=Arial size=2>"
		vBody=vBody + "Contact Info.<br><br>Name: " + f.EmailFromName.value + "<br><br>Email: " + f.EmailFromAddr.value + "<br><br>Comment: " + f.QuestionComment.value + "<br>"
		ob_post.AddParam("vBody",vBody);
		ob_post.AddParam("vPriority","HIGH");
		rv=ob_post.post(null, "vbSendEmail");
		alert('Thank you for contacting us.  We will be in touch.');
		window.location='index.aspx?P='
		}
	}
}
function jsSupportUsers(n,jSP) {
f=document.forms[n]
rv=jsValChecks(f);
rv=true
if (rv) {
	rv=jsSaveRec(f,jSP)
	if (rv) {
		ob_post.AddParam("vFromName", "Website query"); 
		ob_post.AddParam("vFrom", "usersupport@extegrity.com"); 
		ob_post.AddParam("vTo","doug@gneo.net");
		ob_post.AddParam("vSubject","New User Support Query");
		vBody="<font face=Arial size=2>"
		vBody=vBody + "Contact Info.<br><br>Name: " + f.EmailFromName.value + "<br><br>Email: " + f.EmailFromAddr.value + "<br><br>Comment: " + f.Question.value + "<br>"
		ob_post.AddParam("vBody",vBody);
		ob_post.AddParam("vPriority","HIGH");
		rv=ob_post.post(null, "vbSendEmail");
		alert('Thank you for contacting us.  We will be in touch.');
		window.location='index.aspx?P='
		}
	}
}
function jsPayCC() {
var jResp
f=document.forms[0]
o=f.FirstName; if (o.value=="") { alert("Please specify First Name."); o.focus(); return; }
o=f.LastName; if (o.value=="") { alert("Please specify Last Name."); o.focus(); return; }
o=f.ZipCode; if (o.value=="") { alert("Please specify Zip Code."); o.focus(); return; }
o=f.PayMethod; if (o.value=="") { alert("Please specify Pay Method."); o.focus(); return; }
o=f.CreditCardNbr; if (o.value=="") { alert("Please specify a correct Credit Card number."); o.focus(); return; }
o=f.ExpirationMth; if (o.value=="") { alert("Please specify Expiration Month."); o.focus(); return; }
o=f.ExpirationYear; if (o.value=="") { alert("Please specify Expiration Year."); o.focus(); return; }
f.imgPayNow.style.visibility="hidden";
document.getElementById("divPayNow").innerHTML="<font size=1 color=red>Please wait while we <br>process your payment..</font>";
ob_post.AddParam("vFirstName",f.FirstName.value);
ob_post.AddParam("vLastName",f.LastName.value);
ob_post.AddParam("vPaymentAmount",f.PaymentAmount.value);
ob_post.AddParam("vCreditCardNbr",f.CreditCardNbr.value);
ob_post.AddParam("vExpirationMth",f.ExpirationMth.value);
ob_post.AddParam("vExpirationYear",f.ExpirationYear.value);
ob_post.AddParam("vZip",f.ZipCode.value);
jResp=ob_post.post("index.aspx", "vbSubmitCCPayment2");
if (jResp=="Approved") {
	f.hdnNextStep.value=4
	}
else {
	alert("Sorry.  The credit card process has returned an error - " + jResp + ".   Please fix the problem and try again.")
	f.hdnNextStep.value=3
	}
f.submit()
}
function jsEnableTakehomeButton(t,b) {
if (t.checked) {
	o=document.getElementById("ExamID")
	if (o.value=="") { alert("Please select an Exam ID."); t.checked=false; o.focus(); return; }
	o=document.getElementById(b)
	o.disabled=false
	}
}
function jsCheckExamID() {
o1=document.getElementById("TakeHomeID")
o2=document.getElementById("ExamID")
if (o2.value=="") { alert("Please enter an Exam ID"); o2.focus; return; }
ob_post.AddParam("vTakehomeID",o1.value);
ob_post.AddParam("vExamID",o2.value);
rv=ob_post.post(null, "vbCheckExamIDs");
if (!rv) { 
	alert("We could not associate the Exam ID you entered with this course.  Please correct the problem and try again.")
	}
else {
	divGetExamID.style.display="none"
	divShowExamID.style.display="block"
	divConfirm.style.display="block"
	divExamID.innerHTML="2.  Exam ID: <b>" + o2.value + "</b>"
	}
}
function jsLogin(un,pw) {
f=document.forms("frmLogin")
if (un.value=="") { alert("Please enter your email address."); un.focus(); return; }
if (pw.value=="") { alert("Please enter a password."); pw.focus(); return; }
ob_post.AddParam("vUN",un.value);
ob_post.AddParam("vPW",pw.value);
rv=ob_post.post("index.aspx", "vbLogin");
if (rv=="Sorry, we could not find your credentials.  Please try again.") { 
	alert(rv); 
	return;
	} 
else { 
	if (rv!=null) { 
		window.location="index.aspx?P=SHOWORGS" 
		}
	}
}
function n20(v) {
    if (isNaN(parseInt(v))) { return 0 } else { return parseInt(v) }
}
