Imports DevExpress.Web
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Diagnostics
Imports DataUtil
Imports System.Net

Partial Class WebSpecials
	Inherits System.Web.UI.Page

	Private chk As ASPxCheckBox

	Sub page_init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
		'Response.Expires = 0
		'Response.Cache.SetNoStore()
		'Response.AppendHeader("Pragma", "no-cache")
		'ja("Init")
	End Sub

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		'http://localhost:63510/Streamline/webspecials.aspx?email=RZHAO@MAIL.COM&addressid=4165712&psid=29075
		'http://localhost:14019/Streamline/webspecials.aspx?email=LISACSHIEH@GMAIL.COM&addressid=3634680&psid=29075

		txtZip.Value = Left(Replace(txtZip.Value, " ", ""), 5)

		If ismt(txtState.Value) Then txtState.Value = "CA"

		' 11/3/14 - i now have these variables loaded into memtory, don't need the session variables any more.
		Session("MaxDaysToSchedule") = SQLExecuteScalar("select MaxDaysInAdvanceToScheduleWebSpecials from tblAppSettings", "Notify Web User", "WebSpecials, Page_Load")
		Session("UseWebSpecialsPickupDateAlgorithm") = SQLExecuteScalar("select UseWebSpecialsPickupDateAlgorithm from tblAppSettings", "Notify Web User", "WebSpecials, Page_Load")

		Dim i As Integer = 1
		Dim sql As String = "select * from tlkDonorSources order by SortOrder"
		Dim rsql As SqlDataReader = Nothing
		Dim connSql As SqlConnection = New SqlConnection(vConnStr)
		Try
			If Not SqlQueryOpen(connSql, rsql, sql, "Notify Web User", "WebSpecials, Page_Load") Then
				Return
			End If
			While rsql.Read()
				chk = FindControl("chkDonor" & i)
				If Not chk Is Nothing Then chk.Text = rsql("DonorSource")
				i += 1
			End While
			SqlQueryClose(connSql, rsql)
		Catch ex As Exception
			LogFields(1, "")
			Dim errorID As Integer = LogProgramError(ex.Message, sql, ex.StackTrace, "Notify Web User")
			AddErrorToPageLog(errorID)
		End Try

		Select Case Request.HttpMethod
			Case "POST"
			Case "GET"
				Try
					hfPageLogID.Value = 0

					sql = "INSERT INTO tSysWebSpecialsPageLog " & _
						"(PageDateTime, HttpMethod, BrowserType, ReferrerAbsoluteUri, UserAgent, UserHostAddress, QueryString, UrlAbsoluteUri) VALUES ("
					sql += "GETDATE(), "
					sql += "'" & Request.HttpMethod & "', "
					Try
						sql += "'" & Request.Browser.Type & "', "
					Catch ex As Exception
						sql += "'', "
					End Try
					Try
						sql += "'" & Request.UrlReferrer.AbsoluteUri & "', "
					Catch ex As Exception
						sql += "'', "
					End Try
					Try
						sql += "'" & Request.UserAgent & "', "
					Catch ex As Exception
						sql += "'', "
					End Try
					Try
						sql += "'" & Request.UserHostAddress & "', "
					Catch ex As Exception
						sql += "'', "
					End Try
					Try
						sql += "'" & Request.QueryString.ToString() & "', "
					Catch ex As Exception
						sql += "'', "
					End Try
					Try
						sql += "'" & Request.Url.AbsoluteUri.ToString() & "')"
					Catch ex As Exception
						sql += "'')"
					End Try

					hfPageLogID.Value = SqlNonQueryIdentity(sql)

					Try
						hfCharityAbbr.Value = UCase(Request.QueryString("Charity"))

						Select Case hfCharityAbbr.Value
							Case "DAV"
								litPhoneNbr.Text = "(800) 238-8387"
								PhoneToCall.Value = litPhoneNbr.Text
							Case "VVA"
								litPhoneNbr.Text = "(866) 241-8387"
								PhoneToCall.Value = litPhoneNbr.Text
							Case "UCP"
								litPhoneNbr.Text = "(855) 228-1190"
								PhoneToCall.Value = litPhoneNbr.Text
							Case Else
								litPhoneNbr.Text = "(800) 555-5555"
								PhoneToCall.Value = litPhoneNbr.Text
						End Select

						qsEmailAddress.Value = Request.QueryString("email")
						qsAddressID.Value = CInt(Request.QueryString("addressid"))
						qsPickupScheduleID.Value = CInt(Request.QueryString("psid"))
						sql = "SELECT TOP (1) PickupDate, Email, MailNR, EmailNR " & _
							"FROM tblPickupSchedule AS PS " & _
							"INNER JOIN tblPickupScheduleDetail AS PSD ON PSD.PickupScheduleID = PS.PickupScheduleID " & _
							"WHERE PS.PickupScheduleID = " & qsPickupScheduleID.Value & " " & _
								"AND PSD.AddressID = " & qsAddressID.Value
						Try
							If Not SqlQueryOpen(connSql, rsql, sql, "Notify Web User", "WebSpecials, Page_Load") Then
								Return
							End If
							If rsql.Read() Then
								calPickups.SelectedDate = CDate(rsql("PickupDate").ToString())
								qsAddressLoad()
								qsPickupDate.Value = CDate(rsql("PickupDate").ToString())
								Dim email As Boolean = CBool(rsql("Email").ToString())
								Dim mailNR As Boolean = CBool(rsql("MailNR").ToString())
								Dim emailNR As Boolean = CBool(rsql("EmailNR").ToString())

								qsSource.Value = ""
								If email Then
									qsSource.Value = "Email"
								End If
								If mailNR Then
									qsSource.Value = "MailNR"
								End If
								If emailNR Then
									qsSource.Value = "EmailNR"
								End If
							End If
							'
							' WE STILL HAVE TO HANDLE MAILNR PROPERLY AS IT WILL NOT BE COMING IN WITH A QUERY STRING
							' BUT WITH A SPECIFIC WEB PAGE NAME
							'


							SqlQueryClose(connSql, rsql)
						Catch ex As Exception
							LogFields(1, "")
							Dim errorID As Integer = LogProgramError(ex.Message, sql, ex.StackTrace, "Notify Web User", "WebSpecials, Page_Load")
							AddErrorToPageLog(errorID)
						End Try
					Catch
						qsEmailAddress.Value = ""
						qsAddressID.Value = 0
						qsPickupScheduleID.Value = 0
					End Try

					If qsEmailAddress.Value = "" Then
						mvPickups.SetActiveView(EmailPhone)
					Else
						If qsAddressLoad() Then
							If qsSource.Value = "Email" Then
								btnContinue.Visible = False
								tblMain.Visible = False

								If qsPickupDate.Value > Today Or (qsPickupDate.Value = Today And DateTime.Now.Hour < 8) Then
									btnConfirmConfirm.Visible = True
									spnExplain.InnerText = "Please confirm that you will have a donation on " & _
										qsPickupDate.Value & ". Thank you."
									mvPickups.SetActiveView(ConfirmConfirm)
								ElseIf qsPickupDate.Value = Today Then
									btnConfirmConfirm.Visible = False
									spnExplain.InnerText = "Your pickup date is today. Please call " & _
										PhoneToCall.Value & " to confirm your pickup. Thank you."
									mvPickups.SetActiveView(ConfirmNoPhone)
								Else
									btnConfirmConfirm.Visible = False
									spnExplain.InnerText = "Your pickup date has passed. Please call " & _
										PhoneToCall.Value & " to schedule a pickup. Thank you."
									mvPickups.SetActiveView(ConfirmNoPhone)
								End If
							Else 'It's NR
								tblMain.Visible = True
								mvPickups.SetActiveView(qsAddress)
							End If
						Else
							mvPickups.SetActiveView(EmailPhone)
						End If
					End If
				Catch ex As Exception
					LogFields(2, "")
					Dim errorID As Integer = LogProgramError(ex.Message, sql, ex.StackTrace, "Notify Web User", "WebSpecials, Page_Load")
					AddErrorToPageLog(errorID)
				End Try
			Case "HEAD"
		End Select

		Select Case hfCharityAbbr.Value
			Case "DAV"
				btnContinue.Image.Url = "~/Resources/images/btnContinue-DAV.jpg"
				btnContinueTop.Image.Url = "~/Resources/images/btnContinue-DAV.jpg"
				btnSchedule.Image.Url = "~/Resources/images/btnScheduleNow-DAV.png"
				btnScheduleTop.Image.Url = "~/Resources/images/btnScheduleNow-DAV.png"
				lnkCSS.Href = "~/Styles/DAV.css"
				tblMain.Width = "585px"
				litPhoneNbr.Text = "(800) 238-8387"
				Literal1.Text = litPhoneNbr.Text
				'Literal2.Text = litPhoneNbr.Text
				PhoneToCall.Value = litPhoneNbr.Text
				calPickups.HeaderStyle.BackColor = Drawing.Color.Gray
			Case "VVA"
				btnContinue.Image.Url = "~/Resources/images/btnContinue-VVA.png"
				btnContinueTop.Image.Url = "~/Resources/images/btnContinue-VVA.png"
				btnSchedule.Image.Url = "~/Resources/images/btnScheduleNow-VVA.png"
				btnScheduleTop.Image.Url = "~/Resources/images/btnScheduleNow-VVA.png"
				lnkCSS.Href = "~/Styles/VVA.css"
				tblMain.Width = "585px"
				litPhoneNbr.Text = "(866) 241-8387"
				Literal1.Text = litPhoneNbr.Text
				'Literal2.Text = litPhoneNbr.Text
				PhoneToCall.Value = litPhoneNbr.Text
				calPickups.HeaderStyle.BackColor = Drawing.Color.Gray
			Case "UCP"
				btnContinue.Image.Url = "~/Resources/images/btnContinue-UCP.jpg"
				btnContinueTop.Image.Url = "~/Resources/images/btnContinue-UCP.jpg"
				btnSchedule.Image.Url = "~/Resources/images/btnScheduleNow-UCP.jpg"
				btnScheduleTop.Image.Url = "~/Resources/images/btnScheduleNow-UCP.jpg"
				lnkCSS.Href = "~/Styles/UCP.css"
				tblMain.Width = "585px"
				litPhoneNbr.Text = "(855) 228-1190"
				Literal1.Text = litPhoneNbr.Text
				'Literal2.Text = litPhoneNbr.Text
				PhoneToCall.Value = litPhoneNbr.Text
				calPickups.HeaderStyle.BackColor = Drawing.ColorTranslator.FromHtml("#EB6D00")
			Case Else
				btnContinue.Image.Url = "~/Resources/images/btnContinue-OrangeGrey.jpg"
				btnContinueTop.Image.Url = "~/Resources/images/btnContinue-OrangeGrey.jpg"
				btnSchedule.Image.Url = "~/Resources/images/btnScheduleNow-OrangeGrey.jpg"
				btnScheduleTop.Image.Url = "~/Resources/images/btnScheduleNow-OrangeGrey.jpg"
				lnkCSS.Href = "~/Styles/Orange.css"
				tblMain.Width = "585px"
				litPhoneNbr.Text = "(800) 555-5555"
				Literal1.Text = litPhoneNbr.Text
				'Literal2.Text = litPhoneNbr.Text
				PhoneToCall.Value = litPhoneNbr.Text
				calPickups.HeaderStyle.BackColor = Drawing.ColorTranslator.FromHtml("#EB6D00")
		End Select

	End Sub

	Sub btnConfirmConfirm_Click(s As Object, e As EventArgs)
		btnConfirmConfirm.Visible = False
		If qsSource.Value = "Email" Then ' It's a confirm
			tblMain.Visible = False
			ConfirmPickup(qsPickupScheduleID.Value, qsAddressID.Value, qsEmailAddress.Value)
			If DateAdd("d", -5, CDate(qsPickupDate.Value)) < Today Then
				mvPickups.SetActiveView(ConfirmNoPhone)
				spnExplain.InnerText = "Your pickup is confirmed. Thank you."
			Else
				mvPickups.SetActiveView(ConfirmWithPhone)
				spnExplain.InnerText = "Your pickup is confirmed. If you would like a reminder call on the day before your pickup, please enter your phone number below and click save. Thank you."
				btnConfirmPhone.Visible = True
			End If
			btnContinue.Visible = False
		Else
			tblMain.Visible = True
			mvPickups.SetActiveView(qsAddress)
		End If
	End Sub

	Sub btnConfirmPhone_Click(s As Object, e As EventArgs)
		Dim phone As String = confirmPhone.Text
		Dim phoneFormatted = Mid(phone, 1, 3) & "-" & Mid(phone, 4, 3) & "-" & Mid(phone, 7, 4)
		If Len(phone) > 10 Then
			phoneFormatted &= " x" & Mid(phone, 11)
		End If
		Dim comments As String = phoneFormatted & "; " & qsEmailAddress.Value
		ConfirmPickup(qsPickupScheduleID.Value, qsAddressID.Value, comments)

		btnConfirmPhone.Visible = False
		mvPickups.SetActiveView(ConfirmPhoneSaved)

	End Sub

	Sub ConfirmPickup(ByVal pickupScheduleID As Integer, ByVal addressID As Integer, ByVal comments As String)
		Dim sql As String = "UPDATE tblPickupScheduleDetail " & _
			"SET Confirmed = 1, " & _
				"Comments = '" & comments & "' " & _
			"WHERE PickupScheduleID = " & pickupScheduleID & " " & _
				"AND AddressID = " & addressID
		SqlNonQuery(sql, "WebSpecials, ConfirmPickup")

		' Log it
		LogFields(21, "Confirm")
	End Sub

	Sub vbNextPage(s As Object, e As EventArgs)
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		Dim errorMessage As String = ""

		Dim activeView As String
		activeView = mvPickups.GetActiveView().ID

		Try
			Select Case activeView
				Case "EmailPhone"
					Dim vEmail As String = Trim(txtEmail.Value)
					Dim vPhone As String = Trim(txtPhone.Value)

					hfAddressID.Value = ""

					vPhone = Replace(vPhone, "(", "")
					vPhone = Replace(vPhone, ")", "")
					vPhone = Replace(vPhone, "-", "")
					vPhone = Replace(vPhone, " ", "")

					Dim r As New RegexUtilities

					If Not r.IsValidEmail(vEmail) Then
						ja("Please enter a valid Email Address.")
						Return
					End If

					If Not r.IsValidPhone(vPhone) Then
						ja("Please enter a valid Phone Number.")
						Return
					End If

					sql = "SELECT TOP (1) PickupID, FirstName, LastName, Address, ApartmentSuite, City, State, Zip, AddressID " & _
						"FROM tblSpecials " & _
						"WHERE ((PhoneWork <> '' AND PhoneWork=@phone) " & _
							"OR (PhoneHome <> '' AND PhoneHome=@phone)) " & _
						"ORDER BY PickupDate DESC"

					Dim cmd As New SqlCommand(sql, connSQL)
					cmd.Parameters.Add(DataUtil.CreateParameter("@phone", System.Data.ParameterDirection.Input, System.Data.DbType.String, vPhone))
					If Not SqlQueryOpenWithParms(connSQL, cmd, rsql, sql, "Notify Web User", "WebSpecials, vbNextPage") Then
						Return
					End If

					If rsql.Read() Then
						txtPickupID.Value = rsql("PickupID").ToString
						txtFirstName.Value = rsql("FirstName").ToString
						txtLastName.Value = rsql("LastName").ToString
						txtStreet.Value = rsql("Address").ToString
						txtAptSuite.Value = rsql("ApartmentSuite").ToString
						txtCity.Value = rsql("City").ToString
						txtState.Value = rsql("State").ToString
						txtZip.Value = CleanZip5(rsql("Zip").ToString)
						hfAddressID.Value = rsql("AddressID").ToString
					Else
						txtPickupID.Value = ""
						txtFirstName.Value = ""
						txtLastName.Value = ""
						txtStreet.Value = ""
						txtAptSuite.Value = ""
						txtCity.Value = ""
						txtState.Value = "CA"
						txtZip.Value = ""
						hfAddressID.Value = ""
					End If
					SqlQueryClose(connSQL, rsql)

					Try
						lblEmail.Text = vEmail
					Catch
						errorMessage = "lblEmail.Text = vEmail, vPhone=" & vPhone
					End Try

					Try
						'vPhone = String.Format("{0:(###) ###-#### x###}", Long.Parse(vPhone))
						Dim phone As String = "(" & Mid(vPhone, 1, 3) & ") " & Mid(vPhone, 4, 3) & "-" & Mid(vPhone, 7, 4)
						If Len(vPhone) > 10 Then
							phone &= " x" & Mid(vPhone, 11)
						End If
						vPhone = phone
					Catch ex As Exception
						errorMessage = "vPhone = String.Format({0:(###) ###-####}, Long.Parse(vPhone)), vPhone=" & vPhone
					End Try

					Try
						lblPhone.Text = vPhone
					Catch ex As Exception
						errorMessage = "lblPhone.Text = vPhone"
					End Try

					Try
						cProgress_lblStep1.Attributes.Add("Class", "boxStepsHiLite")
						cProgress_lblStep2.Attributes.Add("Class", "boxStepsSmall")
						cProgress_lblStep3.Attributes.Add("Class", "boxStepsSmall")
						spnExplain.InnerText = "Please enter the following information:"
						spnExplain.Visible = True
						spnExplain2.Visible = False
						spnExplain3.Visible = False
						mvPickups.SetActiveView(Address)
					Catch ex As Exception
						errorMessage = "Other, vPhone=" & vPhone
					End Try
				Case "Address"
					' Check Name and Address Entries
					Dim vEmail As String = lblEmail.Text
					Dim vPhone As String = lblPhone.Text
					vPhone = Replace(vPhone, "(", "")
					vPhone = Replace(vPhone, ")", "")
					vPhone = Replace(vPhone, "-", "")
					vPhone = Replace(vPhone, " ", "")

					Dim addressID As Integer = 0
					If hfAddressID.Value.ToString <> "" Then
						addressID = CInt(hfAddressID.Value)
					Else
						' Only do the query if we didn't get the addressID already from the phone number
						Dim cnt As Integer = 0
						Dim message As String = ""
						'If txtAptSuite.Value <> "" Then
						'    txtStreet.Value += " " + txtAptSuite.Value
						'End If
						cnt = SearchAddress("WebSpecials", 0, False, True, True, _
											txtStreet.Value, txtCity.Value, txtZip.Value, message, addressID, "WebSpecials, vbNextPage")

						If cnt < 0 And txtAptSuite.Value = "" Then
							ja(message)
							Exit Sub
						ElseIf txtAptSuite.Value <> "" Then
							message = "Sorry, addresses with apartments and suites must call " & litPhoneNbr.Text & " to schedule pickups."
							ja(message)
							Exit Sub
						ElseIf cnt > 1 Then
							message = "Sorry, the address entered matches more than one address. Are you missing an Apartment or Suite number? If so, please call " & litPhoneNbr.Text & " to schedule a pickup."
							ja(message)
							Exit Sub
						End If
					End If

					' UCP has a limited set of zip codes
					If hfCharityAbbr.Value = "UCP" Then
						sql = "SELECT TOP (1) Zip5 " & _
							"FROM tblAddresses AS A " & _
							"INNER JOIN tblSections AS S ON S.SectionID = A.SectionID " & _
							"INNER JOIN tblRoutes AS R ON R.RouteID = S.RouteID " & _
							"INNER JOIN tblPickupCycleTemplatesDetail AS PCTD ON PCTD.routeid = R.routeid " & _
							"INNER JOIN tblPickupCycleTemplates AS PCT ON PCT.PickupCycleTemplateID = PCTD.PickupCycleTemplateID " & _
							"INNER JOIN tblPickupCycles AS PC ON PC.PickupCycleTemplateID = PCT.PickupCycleTemplateID " & _
							"INNER JOIN tblpermits AS P ON P.PermitID = PC.PermitID " & _
							"INNER JOIN tblCharities AS C ON C.CharityID = P.CharityID " & _
							"WHERE C.CharityAbbr = 'UCP' " & _
								"AND Zip5 = @zip" & _
								"AND S.Active = 1 " & _
								"AND R.Active = 1 " & _
								"AND PCT.Active = 1 " & _
								"AND PC.Active = 1"

						Dim cmd As New SqlCommand(sql, connSQL)
						cmd.Parameters.Add(DataUtil.CreateParameter("@zip", System.Data.ParameterDirection.Input, System.Data.DbType.String, CleanZip5(txtZip.Value)))
						If Not SqlQueryOpenWithParms(connSQL, cmd, rsql, sql, "Notify Web User", "WebSpecials, vbNextPage") Then
							SqlQueryClose(connSQL, rsql)
							Exit Sub
						End If

						If Not rsql.Read() Then
							Dim message As String = "Sorry, we do not currently service addresses in Zip Code " & CleanZip5(txtZip.Value) & "."
							ja(message)
							SqlQueryClose(connSQL, rsql)
							Exit Sub
						End If
						SqlQueryClose(connSQL, rsql)
					End If

					sql = "SELECT COALESCE(COUNT(*), 0) as ct FROM tblAddresses WHERE SUBSTRING(Zip, 1, 5) = '" & CleanZip5(txtZip.Value) & "'"
					Dim vCt As Integer = SQLExecuteScalar(sql, vConnStr, "WebSpecials, vbNextPage")
					If vCt = 0 Then
						sql = "SELECT COALESCE(COUNT(*), 0) as ct FROM tblZipGroupsDetail WHERE Zip5 = '" & CleanZip5(txtZip.Value) & "'"
						vCt = SQLExecuteScalar(sql, vConnStr, "WebSpecials, vbNextPage")
					End If
					If vCt = 0 Then
						spnExplain.InnerText = "We're sorry, but we do not currently service your zip code.  "
						spnExplain.Visible = True
						spnExplain2.Visible = False
						spnExplain3.Visible = False
						btnContinue.Visible = False
						mvPickups.SetActiveView([Error])
					Else
						cProgress_lblStep1.Attributes.Add("Class", "boxStepsSmall")
						cProgress_lblStep2.Attributes.Add("Class", "boxStepsHiLite")
						cProgress_lblStep3.Attributes.Add("Class", "boxStepsSmall")
						spnExplain.InnerText = "Please check off items you will be donating. Check all that apply:"
						spnExplain.Visible = True
						spnExplain2.Visible = False
						spnExplain3.Visible = False
						mvPickups.SetActiveView(CheckItems)
					End If
				Case "CheckItems"
					btnContinue.Visible = False
					btnSchedule.Visible = True
					btnScheduleTop.Visible = True
					cProgress_lblStep1.Attributes.Add("Class", "boxStepsSmall")
					cProgress_lblStep2.Attributes.Add("Class", "boxStepsSmall")
					cProgress_lblStep3.Attributes.Add("Class", "boxStepsHiLite")
					If qsEmailAddress.Value = "" Then
						spnExplain.InnerText = "Choose your Pickup Date by clicking on one of the bolded dates. Please call " & litPhoneNbr.Text & " if you would like a different date."
					Else
						spnExplain.InnerText = "Please verify your Pickup Date or choose a different one by clicking on one of the bolded dates:"
					End If
					spnExplain.Visible = True
					spnExplain2.Visible = False
					spnExplain3.Visible = False
					mvPickups.SetActiveView(ChooseDate)
					'hfAddressID.Value = "abc"
				Case "ChooseDate"
					' I don't think this code is ever used. RCC 1/9/15
					btnContinue.Visible = False
					btnSchedule.Visible = True
					btnScheduleTop.Visible = True
					cProgress_lblStep1.Attributes.Add("Class", "boxStepsSmall")
					cProgress_lblStep2.Attributes.Add("Class", "boxStepsSmall")
					cProgress_lblStep3.Attributes.Add("Class", "boxStepsHiLite")
					spnExplain.InnerText = "Please enter the following information:"
					spnExplain.Visible = True
					spnExplain2.Visible = False
					spnExplain3.Visible = False
				Case "qsAddress"
					cProgress_lblStep1.Attributes.Add("Class", "boxStepsSmall")
					cProgress_lblStep2.Attributes.Add("Class", "boxStepsHiLite")
					cProgress_lblStep3.Attributes.Add("Class", "boxStepsSmall")
					spnExplain.InnerText = "Please check off items you will be donating. Check all that apply:"
					spnExplain.Visible = True
					spnExplain2.Visible = False
					spnExplain3.Visible = False
					mvPickups.SetActiveView(CheckItems)
				Case "Confirm"
					' I don't think this code is ever used. RCC 1/9/15
					btnContinue.Visible = False
					btnSchedule.Visible = True
					btnScheduleTop.Visible = True
					cProgress_lblStep1.Attributes.Add("Class", "boxStepsSmall")
					cProgress_lblStep2.Attributes.Add("Class", "boxStepsSmall")
					cProgress_lblStep3.Attributes.Add("Class", "boxStepsHiLite")
					spnExplain.InnerText = "Please enter the following information:"
					spnExplain.Visible = True
					spnExplain2.Visible = False
					spnExplain3.Visible = False
			End Select
		Catch ex As Exception
			If errorMessage = "" Then
				errorMessage = activeView
			End If
			LogFields(3, errorMessage)
			Dim errorID As Integer = LogProgramError(ex.Message, sql, ex.StackTrace, "Notify Web User", "WebSpecials, vbNextPage")
			AddErrorToPageLog(errorID)
			mvPickups.SetActiveView([Error])
			spnExplain.Visible = False
			spnExplain2.Visible = True
			spnExplain3.Visible = True
			btnContinue.Visible = False
			Return
		End Try
	End Sub

	Sub vbSchedule(s As Object, e As EventArgs)
		Dim addressID As Integer = 0
		Dim pickupID As Integer = 0
		Dim source As String = ""

		If ismt(calPickups.Value) Then
			ja("Please select a Pickup Date using the calendar.")
			mvPickups.SetActiveView(ChooseDate)
			Exit Sub
		End If
		If IsDate(calPickups.Value) AndAlso CDate(calPickups.Value) <= DateValue(Now) Then
			ja("Please select a Pickup Date using the calendar.")
			mvPickups.SetActiveView(ChooseDate)
			Exit Sub
		End If



		Dim sql As String
		Dim rsql As SqlDataReader = Nothing
		Try
			'Get CharityID
			hfCharityID.Value = "0"
			Dim connSql As SqlConnection = New SqlConnection(vConnStr)
			sql = "SELECT CharityID FROM tblCharities WHERE CharityAbbr = '" & hfCharityAbbr.Value & "'"
			rsql = Nothing
			If Not SqlQueryOpen(connSql, rsql, sql, "Notify Web User", "WebSpecials, vbSchedule") Then
				Return
			End If
			If rsql.Read() Then
				hfCharityID.Value = rsql("CharityID")
			End If
			SqlQueryClose(connSql, rsql)

			Dim conn As SqlConnection = New SqlConnection(vConnStr)
			Dim myCmd As SqlCommand = New SqlCommand()
			myCmd.Connection = conn
			myCmd.CommandText = "spSpecialsSave"
			myCmd.CommandType = System.Data.CommandType.StoredProcedure

			Dim zip As String = ""
			If qsEmailAddress.Value = "" Then
				zip = txtZip.Value
			Else
				zip = qsZip.Value
			End If

			Dim cmd As SqlCommand = New SqlCommand()
			cmd.Connection = conn
			cmd.CommandText = "spSpecials_GetDriverLocationCount"
			cmd.CommandType = System.Data.CommandType.StoredProcedure

			cmd.Parameters.Add(DataUtil.CreateParameter("@zip", System.Data.ParameterDirection.Input, System.Data.DbType.String, zip))
			Dim locationCnt As Integer
			cmd.Parameters.Add(DataUtil.CreateParameter("@cnt", System.Data.ParameterDirection.Output, System.Data.DbType.Int32, locationCnt))
			Dim errorID As Integer = 0
			cmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

			Try
				conn.Open()
				cmd.ExecuteNonQuery()
				locationCnt = cmd.Parameters("@cnt").Value
				errorID = cmd.Parameters("@RETURN_VALUE").Value
				If errorID > 0 Then
					vbHandleProgramError(errorID, "WebSpecials, vbSchedule2")
					AddErrorToPageLog(errorID)
				End If
			Catch ex As Exception
				errorID = LogProgramError(ex.Message, sql, ex.StackTrace, "Notify User", "WebSpecials, vbSchedule")
				AddErrorToPageLog(errorID)
			Finally
				conn.Close()
			End Try

			hfLocation.Value = ""
			If locationCnt = 1 Then
				Dim cmd2 As SqlCommand = New SqlCommand()
				cmd2.Connection = conn
				cmd2.CommandText = "spSpecials_GetDriverLocation"
				cmd2.CommandType = System.Data.CommandType.StoredProcedure

				cmd2.Parameters.Add(DataUtil.CreateParameter("@zip", System.Data.ParameterDirection.Input, System.Data.DbType.String, zip))
				Dim driverLocation As String = ""
				cmd2.Parameters.Add(DataUtil.CreateParameterSQL("@driverLocation", System.Data.ParameterDirection.Output, System.Data.SqlDbType.VarChar, driverLocation, 50))
				cmd2.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

				Try
					conn.Open()
					cmd2.ExecuteNonQuery()
					driverLocation = cmd2.Parameters("@driverLocation").Value
					errorID = cmd2.Parameters("@RETURN_VALUE").Value
					If errorID > 0 Then
						vbHandleProgramError(errorID, "WebSpecials, vbSchedule3")
						AddErrorToPageLog(errorID)
					End If
				Catch ex As Exception
					errorID = LogProgramError(ex.Message, sql, ex.StackTrace, "Notify User", "WebSpecials, vbSchedule")
					AddErrorToPageLog(errorID)
				Finally
					conn.Close()
				End Try

				If Not ismt(driverLocation) Then
					hfLocation.Value = driverLocation
				End If
			End If

			source = Request.QueryString("Source")
			If qsEmailAddress.Value <> "" Then
				addressID = CInt(hfAddressID.Value)
				source = qsSource.Value
			Else
				' Only do the query if we didn't get the addressID already from the phone number
				Dim cnt As Integer = 0
				Dim message As String = ""
				cnt = SearchAddress("WebSpecials", 0, False, True, True, _
									txtStreet.Value, txtCity.Value, txtZip.Value, message, addressID)

				If cnt < 0 Then
					ja(message)
					Exit Sub
				ElseIf cnt > 1 Then
					If InStr(txtStreet.Text, " APT ") Then
						mvPickups.SetActiveView(Apartment)
						spnExplain.Visible = False
						spnExplain2.Visible = True
						spnExplain3.Visible = True
						btnContinue.Visible = False
					Else
						ja("Sorry, the address entered matches more than one address. Please try again.")
					End If
					Exit Sub
				End If
			End If

			' Check is user has already schedule a special on the chosen date
			Dim conn3 As SqlConnection = New SqlConnection(vConnStr)
			Dim cmd3 As SqlCommand = New SqlCommand()
			cmd3.Connection = conn3
			cmd3.CommandText = "spSpecialsValidateDayChosen"
			cmd3.CommandType = System.Data.CommandType.StoredProcedure

			cmd3.Parameters.Add(CreateParameter("@addressID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, addressID))
			cmd3.Parameters.Add(CreateParameter("@pickupDate", System.Data.ParameterDirection.Input, System.Data.DbType.Date, CDate(calPickups.Value)))
			Dim result As Integer = 0
			cmd3.Parameters.Add(DataUtil.CreateParameter("@result", System.Data.ParameterDirection.Output, SqlDbType.Int, result))

			Try
				conn3.Open()
				cmd3.ExecuteNonQuery()

				result = cmd3.Parameters("@result").Value
				If result > 0 Then
					ja("There is a pickup already scheduled for your address on the Pickup Date chosen. Please select a different Pickup Date using the calendar.")
					mvPickups.SetActiveView(ChooseDate)
					Exit Sub
				End If
			Catch ex As Exception
				errorID = LogProgramError(ex.Message, myCmd.CommandText, ex.StackTrace, "Notify User", "WebSpecials, vbSchedule")
				AddErrorToPageLog(errorID)
			Finally
				conn3.Close()
			End Try

			Try
				If txtStreet.Text = "" And qsStreet.Text = "" Then
					Throw New System.Exception("An exception has occurred.")
				End If
				myCmd.Parameters.Add(CreateParameter("@pickupID", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, pickupID))
				myCmd.Parameters.Add(CreateParameter("@specialType", System.Data.ParameterDirection.Input, System.Data.DbType.String, "WEB"))
				myCmd.Parameters.Add(CreateParameter("@addressID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, addressID))
				myCmd.Parameters.Add(CreateParameter("@phoneWork", System.Data.ParameterDirection.Input, System.Data.DbType.String, ""))
				myCmd.Parameters.Add(CreateParameter("@crossStreet", System.Data.ParameterDirection.Input, System.Data.DbType.String, ""))
				myCmd.Parameters.Add(CreateParameter("@pickupDate", System.Data.ParameterDirection.Input, System.Data.DbType.Date, CDate(calPickups.Value)))
				myCmd.Parameters.Add(CreateParameter("@status", System.Data.ParameterDirection.Input, System.Data.DbType.String, "NOT REVIEWED"))
				myCmd.Parameters.Add(CreateParameter("@itemLocation", System.Data.ParameterDirection.Input, System.Data.DbType.String, CStr(rdoLocations.Value)))
				myCmd.Parameters.Add(CreateParameter("@promisedOther", System.Data.ParameterDirection.Input, System.Data.DbType.String, CStr(rdoSizes.Value)))
				myCmd.Parameters.Add(CreateParameter("@reminder", System.Data.ParameterDirection.Input, System.Data.DbType.Boolean, CBool(True)))
				myCmd.Parameters.Add(CreateParameter("@grid", System.Data.ParameterDirection.Input, System.Data.DbType.String, ""))
				myCmd.Parameters.Add(CreateParameter("@gate", System.Data.ParameterDirection.Input, System.Data.DbType.String, ""))
				myCmd.Parameters.Add(CreateParameter("@donorSource", System.Data.ParameterDirection.Input, System.Data.DbType.String, ""))
				myCmd.Parameters.Add(CreateParameter("@driverLocation", System.Data.ParameterDirection.Input, System.Data.DbType.String, hfLocation.Value))
				myCmd.Parameters.Add(CreateParameter("@charityID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(hfCharityID.Value)))
				myCmd.Parameters.Add(CreateParameter("@comment", System.Data.ParameterDirection.Input, System.Data.DbType.String, Replace(CStr(mmoComments.Value), "'", "''")))
				myCmd.Parameters.Add(CreateParameter("@notReviewedEmailComment", System.Data.ParameterDirection.Input, System.Data.DbType.String, ""))
				myCmd.Parameters.Add(CreateParameter("@createdOn", System.Data.ParameterDirection.Input, System.Data.DbType.Date, CDate(Today())))
				myCmd.Parameters.Add(CreateParameter("@createdBy", System.Data.ParameterDirection.Input, System.Data.DbType.String, "INTERNET"))
				myCmd.Parameters.Add(CreateParameter("@scheduledOn", System.Data.ParameterDirection.Input, System.Data.DbType.Date, CDate(Today())))
				myCmd.Parameters.Add(CreateParameter("@scheduledBy", System.Data.ParameterDirection.Input, System.Data.DbType.String, "INTERNET"))
				myCmd.Parameters.Add(CreateParameter("@startTime", System.Data.ParameterDirection.Input, System.Data.DbType.Date, CDate(Today())))
				myCmd.Parameters.Add(CreateParameter("@endTime", System.Data.ParameterDirection.Input, System.Data.DbType.Date, CDate(Today())))
				myCmd.Parameters.Add(CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, 0))
				myCmd.Parameters.Add(CreateParameter("@source", System.Data.ParameterDirection.Input, System.Data.DbType.String, source))
				Dim errorID2 As Integer = 0
				myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID2))

				If qsEmailAddress.Value = "" Then
					myCmd.Parameters.Add(CreateParameter("@firstName", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtFirstName.Text))
					myCmd.Parameters.Add(CreateParameter("@lastName", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtLastName.Text))
					myCmd.Parameters.Add(CreateParameter("@phoneHome", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtPhone.Text))
					myCmd.Parameters.Add(CreateParameter("@email", System.Data.ParameterDirection.Input, System.Data.DbType.String, RemoveEmailPunctuation(txtEmail.Text)))
					myCmd.Parameters.Add(CreateParameter("@streetAddress", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtStreet.Text))
					myCmd.Parameters.Add(CreateParameter("@city", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtCity.Text))
					myCmd.Parameters.Add(CreateParameter("@state", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtState.Text))
					myCmd.Parameters.Add(CreateParameter("@zip", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtZip.Text))
				Else
					myCmd.Parameters.Add(CreateParameter("@firstName", System.Data.ParameterDirection.Input, System.Data.DbType.String, qsFirstName.Text))
					myCmd.Parameters.Add(CreateParameter("@lastName", System.Data.ParameterDirection.Input, System.Data.DbType.String, qsLastName.Text))
					myCmd.Parameters.Add(CreateParameter("@phoneHome", System.Data.ParameterDirection.Input, System.Data.DbType.String, qsPhone.Text))
					myCmd.Parameters.Add(CreateParameter("@email", System.Data.ParameterDirection.Input, System.Data.DbType.String, RemoveEmailPunctuation(qsEmail.Text)))
					myCmd.Parameters.Add(CreateParameter("@streetAddress", System.Data.ParameterDirection.Input, System.Data.DbType.String, qsStreet.Text))
					myCmd.Parameters.Add(CreateParameter("@city", System.Data.ParameterDirection.Input, System.Data.DbType.String, qsCity.Text))
					myCmd.Parameters.Add(CreateParameter("@state", System.Data.ParameterDirection.Input, System.Data.DbType.String, qsState.Text))
					myCmd.Parameters.Add(CreateParameter("@zip", System.Data.ParameterDirection.Input, System.Data.DbType.String, qsZip.Text))
				End If

				conn.Open()
				myCmd.ExecuteNonQuery()
				errorID2 = myCmd.Parameters("@RETURN_VALUE").Value
				pickupID = myCmd.Parameters("@pickupID").Value
				If errorID2 > 0 Then
					vbHandleProgramError(errorID, "WebSpecials, vbSchedule")
					AddErrorToPageLog(errorID)
				End If

				If qsEmailAddress.Value = "" Then
					LogFields(101, "OK")
				Else
					LogFields(102, "OK QS")
				End If


			Catch ex As Exception
				LogFields(4, "")
				errorID = LogProgramError("After spSpecialsSave", "dtPickupDate: " & calPickups.Value.ToString, "", "Notify Web User", "WebSpecials, vbSchedule")
				AddErrorToPageLog(errorID)
				dwsf("Date: " & Now.ToShortDateString())
				dwsf("PickupID: " & pickupID)
				dwsf("AddressID: " & addressID)
				dwsf("PickupDate: " & CDate(calPickups.Value))
				dwsf("ItemLocation: " & CStr(rdoLocations.Value))
				dwsf("PromisedOther:" & CStr(rdoSizes.Value))
				dwsf("DriverLocation: " & hfLocation.Value)
				dwsf("CharityID:" & hfCharityID.Value)
				dwsf("Comment:" & mmoComments.Value)
				If qsEmailAddress.Value = "" Then
					dwsf("TYPE: txt")
					dwsf("FirstName: " & txtFirstName.Text)
					dwsf("LastName: " & txtLastName.Text)
					dwsf("PhoneHome: " & txtPhone.Text)
					dwsf("Email: " & RemoveEmailPunctuation(txtEmail.Text))
					dwsf("StreetAddress: " & txtStreet.Text)
					dwsf("City: " & txtCity.Text)
					dwsf("State: " & txtState.Text)
					dwsf("Zip: " & txtZip.Text)
					dwsf("--------------------------------------------")
				Else
					dwsf("TYPE: qs")
					dwsf("FirstName: " & qsFirstName.Text)
					dwsf("LastName: " & qsLastName.Text)
					dwsf("PhoneHome: " & qsPhone.Text)
					dwsf("Email: " & RemoveEmailPunctuation(qsEmail.Text))
					dwsf("StreetAddress: " & qsStreet.Text)
					dwsf("City: " & qsCity.Text)
					dwsf("State: " & qsState.Text)
					dwsf("Zip: " & qsZip.Text)
					dwsf("--------------------------------------------")
				End If

				mvPickups.SetActiveView([Error])
				spnExplain.Visible = False
				spnExplain2.Visible = True
				spnExplain3.Visible = True
				btnSchedule.Visible = False
				btnScheduleTop.Visible = False
				btnContinue.Visible = False
				btnContinueTop.Visible = False
				Exit Sub
			Finally
				conn.Close()
			End Try

			' Update tSysWebSpecialsPageLog with PickupID
			If hfPageLogID.Value > 0 And pickupID > 0 Then
				sql = "UPDATE tSysWebSpecialsPageLog " & _
					"SET PickupID = " & pickupID & ", " & _
						"PickupIDDateTime = GETDATE() " & _
					"WHERE PageLogID = " & hfPageLogID.Value
				SqlNonQuery(sql, "WebSpecials, vbSchedule")
			End If

			Dim rpt As Repeater = FindControl("rptWebItems")
			For Each itm As RepeaterItem In rpt.Items
				chk = itm.FindControl("chkWebItem")
				If chk.Checked Then
					sql = "INSERT INTO tblSpecialsWebItems (PickupID,WebItem) " & _
						"VALUES (" & pickupID & ",'" & chk.Text & "')"
					SqlNonQuery(sql, "WebSpecials, vbSchedule")
				End If
			Next

			mvPickups.SetActiveView(Scheduled)

			btnContinue.Visible = False
			btnSchedule.Visible = False
			btnScheduleTop.Visible = False
			spnExplain.InnerText = "Pickup Scheduled"
			spnExplain.Visible = True
			spnExplain2.Visible = False
			spnExplain3.Visible = False

			Dim thankYouURL As String = ""
			Dim thankYouPageName As String = "ThankYou-" & hfCharityAbbr.Value & "-" & Request.QueryString("Source")
			thankYouURL = ConfigurationManager.AppSettings(thankYouPageName)
			If thankYouURL <> "" Then
				ConsumePage(thankYouURL)
			End If
		Catch ex As Exception
			LogFields(5, "")
			Dim errorID As Integer = LogProgramError(ex.Message, "dtPickupDate: " & calPickups.Value.ToString, ex.StackTrace, "Notify Web User", "WebSpecials, vbSchedule")
			AddErrorToPageLog(errorID)
			dwsf("Date: " & Now.ToShortDateString())
			dwsf("PickupID: " & pickupID)
			dwsf("hfAddressID: " & hfAddressID.Value)
			dwsf("PickupDate: " & CDate(calPickups.Value))
			dwsf("ItemLocation: " & CStr(rdoLocations.Value))
			dwsf("PromisedOther:" & CStr(rdoSizes.Value))
			dwsf("DriverLocation: " & hfLocation.Value)
			dwsf("CharityID:" & hfCharityID.Value)
			dwsf("Comment:" & mmoComments.Value)
			If qsEmailAddress.Value = "" Then
				dwsf("TYPE: txt")
				dwsf("FirstName: " & txtFirstName.Text)
				dwsf("LastName: " & txtLastName.Text)
				dwsf("PhoneHome: " & txtPhone.Text)
				dwsf("Email: " & RemoveEmailPunctuation(txtEmail.Text))
				dwsf("StreetAddress: " & txtStreet.Text)
				dwsf("City: " & txtCity.Text)
				dwsf("State: " & txtState.Text)
				dwsf("Zip: " & txtZip.Text)
				dwsf("--------------------------------------------")
			Else
				dwsf("TYPE: qs")
				dwsf("FirstName: " & qsFirstName.Text)
				dwsf("LastName: " & qsLastName.Text)
				dwsf("PhoneHome: " & qsPhone.Text)
				dwsf("Email: " & RemoveEmailPunctuation(qsEmail.Text))
				dwsf("StreetAddress: " & qsStreet.Text)
				dwsf("City: " & qsCity.Text)
				dwsf("State: " & qsState.Text)
				dwsf("Zip: " & qsZip.Text)
				dwsf("--------------------------------------------")
			End If

			mvPickups.SetActiveView([Error])
			spnExplain.Visible = False
			spnExplain2.Visible = True
			spnExplain3.Visible = True
			btnContinue.Visible = False
			btnSchedule.Visible = False
			btnScheduleTop.Visible = False
			Return
		End Try
	End Sub

	Sub vbDisableCell(e As CalendarDayCellPreparedEventArgs)
		e.Cell.Enabled = False
		e.Cell.Attributes("disabled") = "disable"
		e.Cell.Attributes("style") = "pointer-events:none"
		e.Cell.ForeColor = Drawing.Color.LightGray
		e.Cell.Font.Bold = False
	End Sub

	Sub vbEnableCell(e As CalendarDayCellPreparedEventArgs)
		e.Cell.Font.Size = FontSize.XLarge
		e.Cell.ForeColor = Drawing.Color.Black
		e.Cell.Font.Bold = True
	End Sub

	Sub vbOnDayCellPrepared(s As Object, e As CalendarDayCellPreparedEventArgs)
		Try
			Dim zip As String = ""
			If qsEmailAddress.Value = "" Then
				zip = Left(Trim(txtZip.Value), 5)
			Else
				zip = Left(Trim(qsZip.Value), 5)
			End If

			' DEBUG
			hfZipDebug.Value = zip

			' This is an experiment - RCC - 9/14/15
			If zip = "" Then
				zip = Left(Trim(txtZip.Value), 5)
				qsEmailAddress.Value = ""
			End If

			Dim vToday As Date = Today()
			Dim zipCount As Integer = 0
			Dim holidayCount As Integer = 0

			If Not Session("UseWebSpecialsPickupDateAlgorithm") Then
				vbEnableCell(e)
				Exit Sub
			End If

			If (e.Date <= vToday Or e.Date > DateAdd(DateInterval.Day, Session("MaxDaysToSchedule"), vToday)) Then
				' Not in correct date range
				vbDisableCell(e)
				Return
			End If

			Dim conn As SqlConnection = New SqlConnection(vConnStr)
			Dim myCmd As SqlCommand = New SqlCommand()
			myCmd.Connection = conn
			myCmd.CommandText = "spSpecialsValidateDayToSchedule"
			myCmd.CommandType = System.Data.CommandType.StoredProcedure

			myCmd.Parameters.Add(CreateParameter("@date", System.Data.ParameterDirection.Input, System.Data.DbType.Date, e.Date))
			myCmd.Parameters.Add(CreateParameter("@zip5", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(zip)))
			Dim retVal As Integer = 0
			myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, retVal))

			conn.Open()
			myCmd.ExecuteNonQuery()
			retVal = myCmd.Parameters("@RETURN_VALUE").Value

			If retVal > 0 Then
				vbEnableCell(e)
			Else
				vbDisableCell(e)
			End If

		Catch ex As Exception
			LogFields(6, hfZipDebug.Value)
			Dim errorID As Integer = LogProgramError(ex.Message, "dtPickupDate " & GetDateString(e.Date), ex.StackTrace, "Notify Web User", "WebSpecials, vbOnDayCellPrepared")
			AddErrorToPageLog(errorID)
			spnExplain.Visible = False
			spnExplain2.Visible = True
			spnExplain3.Visible = True
			btnContinue.Visible = False
			btnScheduleTop.Visible = False
			mvPickups.SetActiveView([Error])
			Return
		End Try

		' 10/20/17 - Move this statement into its own try block to try to identify where string "" to double is not valid error.
		Try
			If qsAddressID.Value > 0 AndAlso e.Date = CDate(qsPickupDate.Value) Then
				' If we told them we would be there on this date, we had better be there.
				vbEnableCell(e)
			End If
		Catch ex As Exception
			LogFields(6, "qs Block Error")
			Dim errorID As Integer = LogProgramError(ex.Message, "dtPickupDate " & GetDateString(e.Date), ex.StackTrace, "Notify Web User", "WebSpecials, vbOnDayCellPrepared")
			AddErrorToPageLog(errorID)
			spnExplain.Visible = False
			spnExplain2.Visible = True
			spnExplain3.Visible = True
			btnContinue.Visible = False
			btnScheduleTop.Visible = False
			mvPickups.SetActiveView([Error])
			Return
		End Try


	End Sub

	Function qsAddressLoad() As Boolean
		Dim sql As String
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		Try
			sql = "SELECT TOP (1) AE.Email, A.StreetAddress, A.City, A.State, A.Zip, A.AddressID, C.CharityID, C.CharityAbbr, " & _
					"CASE WHEN SP1.PhoneHome IS NOT NULL THEN SP1.PhoneHome WHEN SP2.PhoneWork IS NOT NULL THEN SP2.PhoneWork ELSE '' END AS Phone, " & _
					"CASE WHEN AE.FirstName <> '' OR AE.LastName <> '' THEN AE.FirstName ELSE SP3.FirstName END AS FirstName, " & _
					"CASE WHEN AE.FirstName <> '' OR AE.LastName <> '' THEN AE.LastName ELSE SP3.LastName END AS LastName " & _
				"FROM tblAddresses AS A " & _
				"INNER JOIN tblAddressEmails AS AE ON AE.AddressID = A.AddressID " & _
				"INNER JOIN tblPickupScheduleDetail AS PSD ON PSD.AddressID = A.AddressID " & _
				"INNER JOIN tblPickupSchedule AS PS ON PS.PickupScheduleID = PSD.PickupScheduleID " & _
				"INNER JOIN tblPickupCycles AS PC ON PC.PickupCycleID = PS.PickupCycleID " & _
				"INNER JOIN tblPermits AS P ON P.PermitID = PC.PermitID " & _
				"INNER JOIN tblCharities AS C ON C.CharityID = P.CharityID " & _
				"OUTER APPLY (SELECT TOP 1 PhoneHome FROM tblSpecials AS SP1 WHERE SP1.AddressID = A.AddressID AND PhoneHome <> '' ORDER BY PickupDate DESC) AS SP1 " & _
				"OUTER APPLY (SELECT TOP 1 PhoneWork FROM tblSpecials AS SP2 WHERE SP2.AddressID = A.AddressID AND PhoneWork <> '' ORDER BY PickupDate DESC) AS SP2 " & _
				"OUTER APPLY (SELECT TOP 1 FirstName, LastName FROM tblSpecials AS SP3 WHERE SP3.AddressID = A.AddressID AND (FirstName <> '' OR LastName <> '') ORDER BY PickupDate DESC) AS SP3 " & _
				"WHERE A.AddressID = @qsAddressID " & _
					"AND AE.Email = @qsEmailAddress " & _
					"AND PSD.PickupScheduleID = @qsPickupScheduleID"
			Dim cmd As New SqlCommand(sql, connSQL)
			cmd.Parameters.Add(DataUtil.CreateParameter("@qsAddressID", System.Data.ParameterDirection.Input, System.Data.DbType.String, qsAddressID.Value))
			cmd.Parameters.Add(DataUtil.CreateParameter("@qsEmailAddress", System.Data.ParameterDirection.Input, System.Data.DbType.String, qsEmailAddress.Value))
			cmd.Parameters.Add(DataUtil.CreateParameter("@qsPickupScheduleID", System.Data.ParameterDirection.Input, System.Data.DbType.String, qsPickupScheduleID.Value))
			If Not SqlQueryOpenWithParms(connSQL, cmd, rsql, sql, "Notify Web User", "WebSpecials, qsAddressLoad") Then
				Return False
			End If

			If rsql.Read() Then
				txtPickupID.Value = ""
				qsEmail.Value = rsql("Email").ToString
				qsPhone.Value = rsql("Phone").ToString
				qsFirstName.Value = rsql("FirstName").ToString
				qsLastName.Value = rsql("LastName").ToString
				qsStreet.Value = rsql("StreetAddress").ToString
				qsAptSuite.Value = ""
				qsCity.Value = rsql("City").ToString
				qsState.Value = rsql("State").ToString
				qsZip.Value = CleanZip5(rsql("Zip").ToString)
				hfAddressID.Value = rsql("AddressID").ToString
				hfCharityID.Value = rsql("CharityID").ToString
				hfCharityAbbr.Value = rsql("CharityAbbr").ToString
			Else
				'ja("Could not find the pickup")
				Return False
			End If
			SqlQueryClose(connSQL, rsql)

			cProgress_lblStep1.Attributes.Add("Class", "boxStepsHiLite")
			cProgress_lblStep2.Attributes.Add("Class", "boxStepsSmall")
			cProgress_lblStep3.Attributes.Add("Class", "boxStepsSmall")
			spnExplain.InnerText = "Please ensure that your Email, Phone, and Name are correct. If your Address is incorrect, please start over by clicking 'Donation Pickup Up'."
			spnExplain.Visible = True
			spnExplain2.Visible = False
			spnExplain3.Visible = False
			Return True
		Catch ex As Exception
			LogFields(7, "")
			Dim errorID As Integer = LogProgramError(ex.Message, sql, ex.StackTrace, "Notify Web User", "WebSpecials, qsAddressLoad")
			AddErrorToPageLog(errorID)
			mvPickups.SetActiveView([Error])
			spnExplain.Visible = False
			spnExplain2.Visible = True
			spnExplain3.Visible = True
			btnContinue.Visible = False
			btnScheduleTop.Visible = False
			Return False
		End Try
	End Function

	Private Sub LogFields(ByVal pos As Integer, ByVal activeView As String)
		'10/16/2017 11:16:17 AM|mandybladl@gmail.com|9137429193|Snyder|Mandy|3312 FERNSIDE BLVD||ALAMEDA|94501|||||||||4132003|6|94501|
		LogInfo(Now & "|" & txtEmail.Text & "|" & txtPhone.Text & "|" & txtLastName.Text & "|" & txtFirstName.Text & "|" & _
		   txtStreet.Text & "|" & txtAptSuite.Text & "|" & txtCity.Text & "|" & txtZip.Text & "|" &
		   qsEmail.Text & "|" & qsPhone.Text & "|" & qsLastName.Text & "|" & qsFirstName.Text & "|" & _
		   qsStreet.Text & "|" & qsAptSuite.Text & "|" & qsCity.Text & "|" & qsZip.Text & "|" & _
		   hfAddressID.Value.ToString & "|" & pos & "|" & activeView & "|")
	End Sub

	Private Function ConsumePage(ByVal URL As String) As Boolean
		Dim didConsume As Boolean = False
		Try
			Dim wc As New WebClient
			Dim data As Stream = wc.OpenRead(New Uri(URL))
			Dim reader As New StreamReader(data)
			Dim s As String = reader.ReadToEnd()
			data.Close()
			reader.Close()
			wc = Nothing
			didConsume = True
		Catch ex As Exception
			didConsume = False
		End Try
		Return didConsume
	End Function

	Private Sub AddErrorToPageLog(ByVal errorID As Integer)
		' Update tSysWebSpecialsPageLog with ErrorID
		If hfPageLogID.Value > 0 Then
			Dim sql As String = "UPDATE tSysWebSpecialsPageLog " & _
				"SET ErrorIDs = CASE WHEN COALESCE(LEN(ErrorIDs), 0) = 0 THEN CAST(" & errorID.ToString & " AS VARCHAR) " & _
					"ELSE ErrorIDs + ', ' + CAST(" & errorID.ToString & " AS VARCHAR) END " & _
				"WHERE PageLogID = " & hfPageLogID.Value
			SqlNonQuery(sql, "WebSpecials, AddErrorToPageLog")
		End If
	End Sub

End Class
