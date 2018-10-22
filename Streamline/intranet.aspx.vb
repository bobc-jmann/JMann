Imports DevExpress.Web
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports DataUtil
Imports System.Diagnostics
Imports System.Runtime.InteropServices

Partial Class sys_ContentFrame
	Inherits System.Web.UI.Page

	Class userRegion
		Public RegionID As Integer
		Public defaultRegion As Boolean
	End Class

	Private userRegions As List(Of userRegion) = New List(Of userRegion)

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		Dim vID As String
		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim sql As String
		vID = Request.QueryString("ID")
		If Not ismt(vID) Then
			sql = "SELECT * FROM users.Users WHERE UserID=" & vID
			If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
				Return
			End If
			While rs.Read()
				Session("vUserID") = rs("UserID")
				Session("vUserName") = rs("UserName")
				Session("vUserFirstName") = rs("UserFirstName")
				Session("vUserLastName") = rs("UserLastName")
				Session("vUserEmailAddress") = rs("EmailAddress")
				Session("vUserLevel") = rs("UserLevel")
				Session("vUserGroup") = rs("UserGroup")
			End While
			SqlQueryClose(connSQL, rs)
			rd("intranet.aspx")
			' this did not help either
			'ElseIf vID = 1001 Then
			'	Session("vUserName") = ""
		End If
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
		vbLoadAppVars()
		SplitterContentControl1.BackColor = System.Drawing.ColorTranslator.FromHtml(vAppNavBackColor)

		' NEW USER GROUP MENU SECURITY
		' Disable each Menu Group
		For Each grp In ASPxNavBar1.Groups
			grp.Visible = False
		Next

		' Disable each Menu Item
		For Each item In ASPxNavBar1.Groups
			item.Visible = False
		Next

		Try
			sql = "SELECT SMI.MenuItemName, SMI.MenuItemType, SMI.SortCode " & _
				"FROM [system].MenuItems AS SMI " & _
				"INNER JOIN users.MenuItems AS UMI ON UMI.SystemMenuItemID = SMI.SystemMenuItemID " & _
				"WHERE UMI.UserID = " & Session("vUserID") & " " & _
				"UNION " & _
				"SELECT SMI.MenuItemName, SMI.MenuItemType, SMI.SortCode " & _
				"FROM [system].MenuItems AS SMI " & _
				"INNER JOIN [system].MenuGroupItems AS SMGI ON SMGI.SystemMenuItemID = SMI.SystemMenuItemID " & _
				"INNER JOIN users.MenuGroups AS UMG ON UMG.SystemMenuGroupID = SMGI.SystemMenuGroupID " & _
				"WHERE UMG.UserID = " & Session("vUserID") & " " & _
				"ORDER BY SortCode"

			If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
				Return
			End If
			Session("MailSchedulerSpecialDelete") = "Not Visible"
			While rs.Read()
				If rs("MenuItemType") = "Group" Then
					ASPxNavBar1.Groups.FindByName("" & rs("MenuItemName") & "").Visible = True
				Else
					If rs("MenuItemName") = "AddressAddOverride" Then
						Session("AddressAddOverride") = "Override"
					ElseIf rs("MenuItemName") = "ProductionUpdate" Then
						Session("ProductionUpdate") = "Visible"
					ElseIf rs("MenuItemName") = "MappingUpdate" Then
						Session("MappingUpdate") = "Update"
					ElseIf rs("MenuItemName") = "SectionAddressesAddAddresses" Then
						Session("SectionAddressesAddAddresses") = "Update"
					ElseIf rs("MenuItemName") = "MailSchedulerSpecialDelete" Then
						Session("MailSchedulerSpecialDelete") = "Visible"
					Else
						ASPxNavBar1.Items.FindByName("" & rs("MenuItemName") & "").Visible = True
					End If
				End If
			End While
			SqlQueryClose(connSQL, rs)
		Catch ex As Exception
			Client_Alert(rs("MenuItemName") & " not found.")
		End Try

		Try
			userRegions.Clear()
			sql = "SELECT URG.SystemRegionID, URG.DefaultRegion " & _
				"FROM users.Regions AS URG " & _
				"WHERE URG.UserID = " & Session("vUserID")

			If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
				Return
			End If
			While rs.Read()
				Dim ur As New userRegion
				ur.RegionID = rs("SystemRegionID")
				ur.defaultRegion = CBool(rs("DefaultRegion"))
				userRegions.Add(ur)
			End While
			SqlQueryClose(connSQL, rs)
		Catch ex As Exception
			Client_Alert("Region error on main menu.")
		End Try

		Session("userRegionsList") = ""
		Session("userRegionDefault") = 0
		For i As Integer = 1 To userRegions.Count
			If Session("userRegionsList") = "" Then
				Session("userRegionsList") = "("
			Else
				Session("userRegionsList") &= ", "
			End If
			Session("userRegionsList") &= userRegions(i - 1).RegionID
			If userRegions(i - 1).defaultRegion Then
				Session("userRegionDefault") = userRegions(i - 1).RegionID
			End If
		Next
		If Session("userRegionsList") <> "" Then
			Session("userRegionsList") &= ")"
		End If

		' Populate Browser Type and Version
		browserType.Value = Request.Browser.Type
		browserVersion.Value = Request.Browser.Version

		' If query string has a PickupID (pid), load specials.aspx with PickupID
		Dim pickupID As String = Request.QueryString("pid")
		If pickupID <> "" Then
			ASPxSplitter1.GetPaneByName("ContentPane").ContentUrl = "specials.aspx?pid=" & pickupID
		End If
	End Sub

	Private PerfCounters(13) As PerformanceCounter
	Private connection As SqlConnection = New SqlConnection

	Private var As Integer = 0

	Protected Sub btnPerformanceCounters_Click(sender As Object, e As EventArgs)
		If Session("vUserName") = "admin" Or Session("vUserName") = "iZlimen" Then
			SetUpPerformanceCounters()
			WritePerformanceCounters()
		Else
			ja("Not started")
		End If
	End Sub

	Private Enum ADO_Net_Performance_Counters
		NumberOfActiveConnectionPools
		NumberOfReclaimedConnections
		HardConnectsPerSecond
		HardDisconnectsPerSecond
		NumberOfActiveConnectionPoolGroups
		NumberOfInactiveConnectionPoolGroups
		NumberOfInactiveConnectionPools
		NumberOfNonPooledConnections
		NumberOfPooledConnections
		NumberOfStasisConnections
		' The following performance counters are more expensive to track.
		' Enable ConnectionPoolPerformanceCounterDetail in your config file.
		SoftConnectsPerSecond
		SoftDisconnectsPerSecond
		NumberOfActiveConnections
		NumberOfFreeConnections
	End Enum

	Private Sub SetUpPerformanceCounters()
		connection.Close()
		Me.PerfCounters(13) = New PerformanceCounter()

		Dim instanceName As String = GetInstanceName()
		Dim apc As Type = GetType(ADO_Net_Performance_Counters)
		Dim i As Integer = 0
		Dim s As String = ""
		For Each s In [Enum].GetNames(apc)
			Me.PerfCounters(i) = New PerformanceCounter()
			Me.PerfCounters(i).CategoryName = ".NET Data Provider for SqlServer"
			Me.PerfCounters(i).CounterName = s
			Me.PerfCounters(i).InstanceName = instanceName
			i = (i + 1)
		Next
	End Sub

	Private Declare Function GetCurrentProcessId Lib "kernel32.dll" () As Integer

	Private Function GetInstanceName() As String
		'This works for Winforms apps. 
		'Dim instanceName As String = _
		'   System.Reflection.Assembly.GetEntryAssembly.GetName.Name

		' Must replace special characters like (, ), #, /, \\ 
		Dim instanceName As String = _
		   AppDomain.CurrentDomain.FriendlyName.ToString.Replace("(", "[") _
		   .Replace(")", "]").Replace("#", "_").Replace("/", "_").Replace("\\", "_")

		'For ASP.NET applications your instanceName will be your CurrentDomain's 
		'FriendlyName. Replace the line above that sets the instanceName with this: 
		'instanceName = AppDomain.CurrentDomain.FriendlyName.ToString.Replace("(", "[") _
		'    .Replace(")", "]").Replace("#", "_").Replace("/", "_").Replace("\\", "_")

		Dim pid As String = GetCurrentProcessId.ToString
		instanceName = (instanceName + ("[" & (pid & "]")))
		Return instanceName
	End Function

	Private Sub WritePerformanceCounters()
		Dim s As String = ""
		For Each p As PerformanceCounter In Me.PerfCounters
			s &= p.CounterName & " = " & p.NextValue.ToString() & vbCrLf
		Next
		ja(s)
	End Sub



End Class
