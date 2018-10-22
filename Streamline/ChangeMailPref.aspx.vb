Imports AddressIDEncryption
Imports DataUtil
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web
Imports System.Windows.Forms

Partial Class ChangeMailPref
    Inherits System.Web.UI.Page
    Dim vFound As Boolean
    Dim userErrorText As String = "We're sorry, the application has generated an error which has been reported." & _
        "Please try again later, send us an email, or call the number on the Home page. Thank you for your patience."
    Private sql As SqlDataSource

    Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load, Me.Load
		Dim v As System.Web.UI.Control
		Select Case Request.HttpMethod
			Case "POST"
			Case "GET"
				AddressID.Value = 0
			Case "HEAD"
		End Select

        If ismt(txtState) Then txtState.Value = "CA"
        If IsPostBack Then
            v = GetPostBackControl(Me.Page)
            If Not v Is Nothing Then
                Select Case v.ID
                    Case "txtReferenceNbr"
                End Select
            End If
        End If
        Select Case UCase(Request.QueryString("Charity"))
            Case "DAV"
                lnkCSS.Href = "~/Styles/DAV.css"
                tblMain.Width = "585px"
            Case "VVA"
                lnkCSS.Href = "~/Styles/VVA.css"
                tblMain.Width = "585px"
            Case Else
                lnkCSS.Href = "~/Styles/Orange.css"
                tblMain.Width = "585px"
        End Select
    End Sub

    Sub vbCheckRefNbr(s As Object, e As EventArgs)
        Try
            If GetPostBackControl(Me.Page).ID = "btnClearForm" Then Exit Sub
            Dim vNbr As Integer = CInt(s.value)
            Dim vStreetAddr As String = ""
            Dim vCity As String = ""
            Dim vState As String = ""
            Dim vZip As String = ""
            If Not ismt(vNbr) Then
                Dim x As Integer = vNbr
                x = AddressIDEncryption.Decrypt(x)
                's = "select * from tblAddresses where AddressID=" & x
                vFound = False

                Dim sql As String = "SELECT * FROM tblAddresses WHERE AddressID = " & x
                Dim rsql As SqlDataReader = Nothing
                Dim connSql As SqlConnection = New SqlConnection(vConnStr)

                If Not SqlQueryOpen(connSql, rsql, sql, "Notify Web User") Then
                    Return
                End If
                While rsql.Read()
                    s = ""
                    vStreetAddr = rsql("StreetAddress")
                    vCity = rsql("City")
                    vState = rsql("State")
                    vZip = rsql("Zip")
                    s += vbCrLf & vbCrLf & vStreetAddr & vbCrLf & vCity & ", " & vState & " " & Left(vZip, 5)
                    vFound = True
                End While
                If vFound Then
                    txtReferenceNbr.Value = vNbr
                    txtStreetAddr.Value = vStreetAddr
                    txtCity.Value = vCity
                    txtState.Value = vState
                    txtZip.Value = vZip
                    AddressID.Value = x
                    txtReferenceNbr.Enabled = True
                    pnlButtons.ClientVisible = True
                    pnlAddrOK.ClientVisible = True
                    vbLoadMailingType()
                Else
                    ja("Sorry, we could not find your address.  Please try again.")
                    vbClearForm()
                    txtReferenceNbr.Value = vNbr
                End If
                    End If
                Catch ex As Exception
                    LogProgramError(ex.Message, "", ex.StackTrace, "Notify Web User")
                    MainCaption.InnerText = userErrorText
                    MainCaption.Attributes("class") = "boxExplain"
                    MainCaption.Attributes("height") = "30"
                End Try
    End Sub

	Sub vbSearchAddr()
		Dim x As Integer
		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		Dim cnt As Integer = 0
		Try
			Dim message As String = ""
			cnt = SearchAddress("ChangeMailPref", 0, False, True, False, _
								txtStreetAddr.Value, txtCity.Value, txtZip.Value, message)
			If cnt < 0 Then
				ja(message)
				Exit Sub
			ElseIf cnt = 0 Then
				ja("Sorry, we could not find your address. Please try again.")
				Exit Sub
			ElseIf cnt > 1 Then
				ja("Sorry, the address entered matches more than one address. Are you missing an Apartment or Suite number?")
				Exit Sub
			End If

			Dim vAddressID As Integer
			Dim vStreetAddr = txtStreetAddr.Value
			Dim vCity = txtCity.Value
			Dim vState = txtState.Value
			Dim vZip = txtZip.Value
			Dim s = ""
			If Not ismt(vStreetAddr) Then
				s = s & " and StreetAddress like '" & vStreetAddr & "%'"
			End If
			If Not ismt(vCity) Then
				s = s & " and City like '" & vCity & "%'"
			End If
			If Not ismt(vState) Then
				s = s & " and State like '" & vState & "%'"
			End If
			If Not ismt(CleanZip(vZip)) Then
				s = s & " and ZIP like '" & CleanZip(Left(vZip, 5)) & "%'"
			End If
			If Not ismt(s) Then
				Dim vCheck = "select count(*) as ct from tblAddresses where (1=1) " & s
				x = SQLExecuteScalar(vCheck, "")
				If x = 1 Then
					s = "SELECT TOP (1) AddressID, StreetAddress, City, [State], Zip " & _
						"FROM tblAddresses " & _
						"WHERE (1=1) " & s
					If Not SqlQueryOpen(connSQL, rs, s, "Notify Web User") Then
						Return
					End If
					vFound = False
					While rs.Read
						txtStreetAddr.Value = rs("StreetAddress")
						txtCity.Value = rs("City")
						txtState.Value = rs("State")
						txtZip.Value = rs("Zip")
						vAddressID = rs("AddressID")
						vFound = True
					End While
					SqlQueryClose(connSQL, rs)
					If Not vFound Then
						ja("Sorry, we could not find your address.  Please try again.")
					Else
						AddressID.Value = vAddressID
						pnlAddresses.ClientVisible = False
						txtReferenceNbr.Enabled = False
						pnlAddrOK.ClientVisible = True
						vbLoadMailingType()
					End If
				Else
					sql = pnlAddresses.FindControl("sqlAddresses")
					If Not sql Is Nothing Then
						sql.SelectCommand = "SELECT TOP (4) AddressID, StreetAddress, City, [State], Zip " & _
						"FROM tblAddresses " & _
						"WHERE (1=1) " & s
						pnlAddresses.ClientVisible = True
					End If
				End If
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify Web User")
			MainCaption.InnerText = userErrorText
			MainCaption.Attributes("class") = "boxExplain"
			MainCaption.Attributes("height") = "30"
		End Try
	End Sub

    Sub vbClearForm()
        txtReferenceNbr.Value = Nothing
        txtReferenceNbr.Enabled = True
        txtStreetAddr.Value = Nothing
        txtCity.Value = Nothing
        txtState.Value = Nothing
        txtZip.Value = Nothing
        pnlAddresses.ClientVisible = False
        pnlAddrOK.ClientVisible = False
    End Sub

    Sub vbLoadAddr(s As Object, e As EventArgs)
        txtStreetAddr.Value = gridMain.GetSelectedFieldValues("StreetAddress").Item(0).ToString()
        txtCity.Value = gridMain.GetSelectedFieldValues("City").Item(0).ToString()
        txtState.Value = gridMain.GetSelectedFieldValues("State").Item(0).ToString()
        txtZip.Value = gridMain.GetSelectedFieldValues("ZIP").Item(0).ToString()
        pnlAddresses.ClientVisible = False
        vbLoadMailingType()
    End Sub

    Sub vbLoadMailingType()
        Dim mail As Boolean
        Dim email As Boolean
        Dim rsql As SqlDataReader = Nothing
        Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
        Try
            txtStreetAddr.Value = RemoveStandardPunctuation(txtStreetAddr.Value, True)

			Dim sql As String = "SELECT Mail_OK, Email_OK FROM tblAddresses WHERE StreetAddress='" & txtStreetAddr.Value & "'"
            If Not SqlQueryOpen(connSQL, rsql, sql, "Notify Web User") Then
                Exit Sub
            End If

            If rsql.Read() Then
				mail = CBool(rsql("Mail_OK").ToString)
				email = CBool(rsql("Email_OK").ToString)
            End If
            SqlQueryClose(connSQL, rsql)

            If mail And Not email Then
                cmbMailingType.Value = "US Mail"
            ElseIf mail And email Then
                cmbMailingType.Value = "US Mail & Email"
            ElseIf Not mail And email Then
				cmbMailingType.Value = "Email"
			Else
				cmbMailingType.Value = "US Mail & Email"
			End If

            'Dim sql As String = "SELECT MailingType FROM tblAddresses WHERE StreetAddress='" & txtStreetAddr.Value & "'"
            'v = SQLExecuteScalar(sql, "Notify Web User")
            'If Not ismt(v) Then cmbMailingType.Value = v

        Catch ex As Exception
            LogProgramError(ex.Message, "", ex.StackTrace, "Notify Web User")
            MainCaption.InnerText = userErrorText
            MainCaption.Attributes("class") = "boxExplain"
            MainCaption.Attributes("height") = "30"
        End Try
    End Sub

    Sub vbAddClientFunc(grd As ASPxGridView, e As EventArgs)
        grd.ClientSideEvents.FocusedRowChanged = "function(s,e) { jsProcessRow(s,e) } "
    End Sub

    Sub vbSaveMailType()
        If AddressID.Value = 0 Then
			ja("Please enter all or part of your address and press Search Addresses.")
            Return
		End If

		If cmbMailingType.Value <> "Email" And cmbMailingType.Value <> "US Mail" And cmbMailingType.Value <> "US Mail & Email" Then
			ja("Please choose a Mailing Preference from the drop-down list.")
			Return
		End If

		Dim r As New RegexUtilities
		Dim email As String = RemoveEmailPunctuation(txtEmail.Value)

		If Not r.IsValidEmail(email) Then
			ja("Please enter a valid Email.")
			Return
		End If

		If email <> RemoveEmailPunctuation(txtConfirmEmail.Value) Then
			ja("Confirm Email does not match Email.")
			Return
		End If

		Dim errorID As Integer = 0
		Try
			Dim conn As SqlConnection = New SqlConnection(vConnStr)
			Dim myCmd As SqlCommand = New SqlCommand()
			myCmd.Connection = conn
			myCmd.CommandText = "spChangeMailingPrefSave"
			myCmd.CommandType = System.Data.CommandType.StoredProcedure

			myCmd.Parameters.Add(CreateParameter("@addressID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, AddressID.Value))
			myCmd.Parameters.Add(CreateParameter("@mailingType", System.Data.ParameterDirection.Input, System.Data.DbType.String, cmbMailingType.Value))
			myCmd.Parameters.Add(CreateParameter("@email", System.Data.ParameterDirection.Input, System.Data.DbType.String, email))
			myCmd.Parameters.Add(CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, 0))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

			conn.Open()
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value

		Catch ex As Exception
			LogProgramError(ex.Message, "AID:" & AddressID.Value & ", MT:" & cmbMailingType.Value & ", EM:" & txtEmail.Value & ", EID:" & errorID, ex.StackTrace, "Notify Web User")
			MainCaption.InnerText = userErrorText
			MainCaption.Attributes("class") = "boxExplain"
			MainCaption.Attributes("height") = "30"
			errorID = 1
		End Try

		If errorID > 0 Then
			LogProgramError("Error in spChangeMailingPrefSave", "", "", "Notify Web User")
			MainCaption.InnerText = userErrorText
			MainCaption.Attributes("class") = "boxExplain"
			MainCaption.Attributes("height") = "30"
		Else
			rd("system/sys_Message.aspx?MSG=Your mailing preference has been changed.  Thank you.|" & _
				"Please note that we schedule our mail up to six weeks in the future so it may take that long for your changes to take effect.")
		End If

	End Sub

    Sub vbOnDataBound(s As ASPxGridView, e As EventArgs)
        If Not IsPostBack And Not s.IsCallback Then
            s.JSProperties("cpSkipFRCEvent") = True
            s.FocusedRowIndex = 2
        End If
    End Sub
End Class
