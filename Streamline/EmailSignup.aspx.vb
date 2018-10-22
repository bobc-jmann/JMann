Imports AddressIDEncryption
Imports DataUtil
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web
Imports System.Windows.Forms

Partial Class EmailSignup
    Inherits System.Web.UI.Page
    Dim vFound As Boolean

    Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load, Me.Load
        Select Case Request.HttpMethod
            Case "POST"
                If IsPostBack Then
 
                End If
            Case "GET"
                addressNotFound.Visible = False
        End Select


        If ismt(txtState) Then txtState.Value = "CA"
        If IsPostBack Then
			Dim v As System.Web.UI.Control
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
                litPhoneNbr.Text = "(800) 238-8387"
            Case "VVA"
                lnkCSS.Href = "~/Styles/VVA.css"
                tblMain.Width = "585px"
                litPhoneNbr.Text = "(866) 241-8387"
            Case Else
                lnkCSS.Href = "~/Styles/Orange.css"
                tblMain.Width = "585px"
                litPhoneNbr.Text = "(800) 555-5555"
        End Select
    End Sub

    Sub vbSearchAddr()
        Dim addressID As Integer = 0
        Dim cnt As Integer = 0
        Try
            addressNotFound.Visible = False
            Dim message As String = ""
            cnt = SearchAddress("EmailSignUp", 0, False, True, True, _
                                txtStreetAddr.Value, txtCity.Value, txtZip.Value, message, addressID)
            If cnt < 0 Then
                addressNotFound.Visible = True
                boxExplain.Text = message
                Exit Sub
            ElseIf cnt = 0 Then
                addressNotFound.Visible = True
                boxExplain.Text = "Sorry, we could not find your address. Please try again."
                Exit Sub
            ElseIf cnt > 1 Then
                addressNotFound.Visible = True
                boxExplain.Text = "Sorry, the address entered matches more than one address. Are you missing an Apartment or Suite number?"
                Exit Sub
            End If

            Dim conn As SqlConnection = New SqlConnection(vConnStr)
            Dim myCmd As SqlCommand = New SqlCommand()
            myCmd.Connection = conn
            myCmd.CommandText = "spEmailSignUpSave"
            myCmd.CommandType = System.Data.CommandType.StoredProcedure

			myCmd.Parameters.Add(CreateParameter("@addressID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, addressID))
			myCmd.Parameters.Add(CreateParameter("@email", System.Data.ParameterDirection.Input, System.Data.DbType.String, RemoveEmailPunctuation(txtEmail.Value)))
			myCmd.Parameters.Add(CreateParameter("@address", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtStreetAddr.Value))
			myCmd.Parameters.Add(CreateParameter("@city", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtCity.Value))
			myCmd.Parameters.Add(CreateParameter("@state", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtState.Value))
			myCmd.Parameters.Add(CreateParameter("@zip", System.Data.ParameterDirection.Input, System.Data.DbType.String, CleanZip(txtZip.Value)))
			myCmd.Parameters.Add(CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, 0))
            Dim errorID As Integer = 0
			myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

            conn.Open()
            myCmd.ExecuteNonQuery()
            errorID = myCmd.Parameters("@RETURN_VALUE").Value

            If errorID > 0 Then
                LogProgramError("Error in spEmailSignupSave", "", "", "Notify Web User")
                MainCaption.InnerText = "We're sorry, the application has generated an error which has been reported." & _
                    "Please try again later, send us an email, or call the number on the Home page. Thank you for your patience."
                MainCaption.Attributes("class") = "boxExplain"
                MainCaption.Attributes("height") = "30"
            Else
                mvEmail.ActiveViewIndex = 1
            End If
        Catch ex As Exception
            LogProgramError(ex.Message, "", ex.StackTrace, "Notify Web User")
            MainCaption.InnerText = "We're sorry, the application has generated an error which has been reported." & _
                "Please try again later, send us an email, or call the number on the Home page. Thank you for your patience."
            MainCaption.Attributes("class") = "boxExplain"
            MainCaption.Attributes("height") = "30"
        End Try
    End Sub

    Sub vbClearForm()
        txtStreetAddr.Value = Nothing
        txtEmail.Value = Nothing
        txtConfirmEmail.Value = Nothing
        txtCity.Value = Nothing
        txtState.Value = Nothing
        txtZip.Value = Nothing
    End Sub

    Sub vbOnDataBound(s As ASPxGridView, e As EventArgs)
        If Not IsPostBack And Not s.IsCallback Then
            s.JSProperties("cpSkipFRCEvent") = True
            s.FocusedRowIndex = 2
        End If
    End Sub

End Class
