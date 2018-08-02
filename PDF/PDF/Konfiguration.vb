Public Class Konfiguration
    Private TestedSourceSetting As SQLServerSettings
    Private TestedTargetSetting As SQLServerSettings

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles L_SQLFilterStatement.Click

    End Sub

    Private Sub T_SourceUsername_TextChanged(sender As Object, e As EventArgs) Handles T_SourceUsername.TextChanged
        Me.T_SourceUsername.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles L_SourceUsername.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles L_SourcePassword.Click

    End Sub

    Private Sub T_SourcePassword_TextChanged(sender As Object, e As EventArgs) Handles T_SourcePassword.TextChanged
        Me.T_SourcePassword.BackColor = Drawing.SystemColors.Window
    End Sub


    Private Sub B_LoggingDirectory_Click(sender As Object, e As EventArgs) Handles B_LoggingDirectory.Click
        FolderBrowserDialog1.ShowDialog()
        Me.T_LoggingDirectory.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub SourceConnectionTypes()
        Select Case Me.C_SourceType.Text
            Case "MS-SQL"
                Me.C_SourceConnMode.Visible = True
                Me.L_SourceConnectionType.Visible = True
                Me.C_SourceConnMode.Items.Clear()
                Me.C_SourceConnMode.Items.Add("Normal")
                Me.C_SourceConnMode.Items.Add("Trusted")
            Case "MySQL"
                Me.C_SourceConnMode.Visible = True
                Me.L_SourceConnectionType.Visible = True
                Me.C_SourceConnMode.Items.Clear()
                Me.C_SourceConnMode.Items.Add("Normal")
            Case Else
                Me.C_SourceConnMode.Visible = False
                Me.L_SourceConnectionType.Visible = False
        End Select
    End Sub

    Private Sub TargetConnectionTypes()
        Select Case Me.C_TargetServerType.Text
            Case "MS-SQL"
                Me.C_TargetConnectionType.Visible = True
                Me.L_TargetConnectionType.Visible = True
                Me.C_TargetConnectionType.Items.Clear()
                Me.C_TargetConnectionType.Items.Add("Normal")
                Me.C_TargetConnectionType.Items.Add("Trusted")
            Case "MySQL"
                Me.C_TargetConnectionType.Visible = True
                Me.L_TargetConnectionType.Visible = True
                Me.C_TargetConnectionType.Items.Clear()
                Me.C_TargetConnectionType.Items.Add("Normal")
            Case Else
                Me.C_TargetConnectionType.Visible = False
                Me.L_TargetConnectionType.Visible = False
        End Select
    End Sub

    Private Sub FilterTypes()
        Select Case Me.C_SourceFilterType.Text
            Case "one column match"
                Me.T_SourceSQLFilter.Visible = False
                Me.T_SourceFilterColumn.Visible = True
                Me.T_SourceFilterValue.Visible = True
                Me.L_FilterColumn.Visible = True
                Me.L_FilterValue.Visible = True
                Me.L_SQLFilterStatement.Visible = False
            Case "SQL Filter"
                Me.T_SourceSQLFilter.Visible = True
                Me.T_SourceFilterColumn.Visible = False
                Me.T_SourceFilterValue.Visible = False
                Me.L_FilterColumn.Visible = False
                Me.L_FilterValue.Visible = False
                Me.L_SQLFilterStatement.Visible = True
            Case "none"
                Me.T_SourceSQLFilter.Visible = False
                Me.T_SourceFilterColumn.Visible = False
                Me.T_SourceFilterValue.Visible = False
                Me.L_FilterColumn.Visible = False
                Me.L_FilterValue.Visible = False
                Me.L_SQLFilterStatement.Visible = False
            Case Else
                Me.T_SourceSQLFilter.Visible = False
                Me.T_SourceFilterColumn.Visible = False
                Me.T_SourceFilterValue.Visible = False
                Me.L_FilterColumn.Visible = False
                Me.L_FilterValue.Visible = False
                Me.L_SQLFilterStatement.Visible = False
        End Select
    End Sub

    Public Sub SourceConnectionTypeChoosed()
        Select Case Me.C_SourceConnMode.Text
            Case "Normal"
                Me.T_SourceUsername.Visible = True
                Me.T_SourcePassword.Visible = True
                Me.L_SourceUsername.Visible = True
                Me.L_SourcePassword.Visible = True
            Case "Trusted"
                Me.T_SourceUsername.Visible = False
                Me.T_SourcePassword.Visible = False
                Me.L_SourceUsername.Visible = False
                Me.L_SourcePassword.Visible = False
            Case "Else"
                Me.T_SourceUsername.Visible = False
                Me.T_SourcePassword.Visible = False
                Me.L_SourceUsername.Visible = False
                Me.L_SourcePassword.Visible = False
        End Select
    End Sub

    Public Sub TargetConnectionTypeChoosed()
        Select Case Me.C_TargetConnectionType.Text
            Case "Normal"
                Me.T_TargetUsername.Visible = True
                Me.T_TargetPassword.Visible = True
                Me.L_TargetUsername.Visible = True
                Me.L_TargetPassword.Visible = True
            Case "Trusted"
                Me.T_TargetUsername.Visible = False
                Me.T_TargetPassword.Visible = False
                Me.L_TargetUsername.Visible = False
                Me.L_TargetPassword.Visible = False
            Case "Else"
                Me.T_TargetUsername.Visible = False
                Me.T_TargetPassword.Visible = False
                Me.L_TargetUsername.Visible = False
                Me.L_TargetPassword.Visible = False
        End Select
    End Sub

    Private Sub B_Save_Click(sender As Object, e As EventArgs) Handles B_Save.Click
        Dim Rows As Integer = MappingGrid.RowCount
        Dim Columns As Integer = MappingGrid.ColumnCount
        Dim ENV As New ENV

        If ValidateUserInput() = False Then
            Exit Sub
        End If

        ENV.CreateName(Me.T_Jobname.Text)
        ENV.SetLogPath(Me.T_LoggingDirectory.Text)

        If Me.C_DebugLog.Checked = True Then
            ENV.LogLevel = "1"
        Else
            ENV.LogLevel = "0"
        End If

        Dim SourceSettings As New SQLServerSettings With {
            .Direction = "source",
            .Servertype = Me.C_SourceType.Text,
            .Servername = Me.T_SourceAdress.Text,
            .SQLDB = Me.T_SourceDB.Text,
            .ConnMode = Me.C_SourceConnMode.Text,
            .User = Me.T_SourceUsername.Text,
            .Password = Me.T_SourcePassword.Text,
            .SQLTable = Me.T_SourceTable.Text,
            .IDColumn = Me.T_SourceIDColumn.Text,
            .Filtertype = Me.C_SourceFilterType.Text,
            .FilterColumn = Me.T_SourceFilterColumn.Text,
            .FilterValue = Me.T_SourceFilterValue.Text,
            .SQLFilter = Me.T_SourceSQLFilter.Text
        }

        Dim TargetSettings As New SQLServerSettings With {
            .Direction = "target",
            .Servertype = Me.C_TargetServerType.Text,
            .Servername = Me.T_TargetServerAdress.Text,
            .SQLDB = Me.T_TargetDB.Text,
            .ConnMode = Me.C_TargetConnectionType.Text,
            .User = Me.T_TargetUsername.Text,
            .Password = Me.T_TargetPassword.Text,
            .SQLTable = Me.T_TargetTable.Text,
            .IDColumn = Me.T_TargetIDColumn.Text
        }

        If Me.C_MapIDValue.Checked = True Then
            TargetSettings.MapTargetIDColumnValue = "YES"
        Else
            TargetSettings.MapTargetIDColumnValue = "NO"
        End If
        TargetSettings.StringSeperator = Me.T_TargetSeperator.Text
        TargetSettings.StringPart = Me.C_TargetPartSubstring.Text

        TargetSettings.SessionTimestampField = Me.T_TargetTimestampfield.Text

        If Me.C_InsertAllowed.Checked = True Then
            TargetSettings.InsertAllowed = True
        Else
            TargetSettings.InsertAllowed = False
        End If

        If Me.C_UpdateAllowed.Checked = True Then
            TargetSettings.UpdateAllowed = True
        Else
            TargetSettings.UpdateAllowed = False
        End If

        If Me.C_DeleteAllowed.Checked = True Then
            TargetSettings.DeleteAllowed = True
        Else
            TargetSettings.DeleteAllowed = False
        End If

        ENV.SQLServer.AddLast(SourceSettings)
        ENV.SQLServer.AddLast(TargetSettings)

        For Rows = 0 To MappingGrid.RowCount - 1
            Dim Mapping As New Mapping
            For Columns = 0 To MappingGrid.ColumnCount - 1
                Select Case MappingGrid.Columns.Item(Columns).Name
                    Case "SourceColumn"
                        Mapping.Sourcename = MappingGrid.Item(Columns, Rows).Value
                    Case "TargetColumn"
                        Mapping.Targetname = MappingGrid.Item(Columns, Rows).Value
                    Case "SourceType"
                        Mapping.Sourcetype = MappingGrid.Item(Columns, Rows).Value
                    Case "TargetType"
                        Mapping.Targettype = MappingGrid.Item(Columns, Rows).Value
                    Case "Seperator"
                        Mapping.Separator = MappingGrid.Item(Columns, Rows).Value
                    Case "PartOfSubstring"
                        Mapping.SeperatorDirection = MappingGrid.Item(Columns, Rows).Value
                    Case Else
                        Mapping.Sourcename = "Fuck that error!"
                End Select
            Next
            ENV.Mappings.AddLast(Mapping)
        Next

        Dim XMLFile As New XMLFiles
        Dim Path As String


        SaveFileDialog1.DefaultExt = ".xml"
        SaveFileDialog1.AddExtension = True
        SaveFileDialog1.Filter = "XML-Files (*.xml)|*.xml"
        Path = SaveFileDialog1.ShowDialog()

        If IsNothing(Path) Or Path = "" Or Path = "2" Then
        Else
            XMLFile.WriteJobFile(SaveFileDialog1.FileName, ENV)
            Module1.Core.JobXMLPath = SaveFileDialog1.FileName.Substring(0, SaveFileDialog1.FileName.LastIndexOf("\"))
            Me.Close()
        End If


    End Sub

    Private Sub C_SourceType_SelectedValueChanged(sender As Object, e As EventArgs) Handles C_SourceType.SelectedValueChanged
        SourceConnectionTypes()
        Me.C_SourceType.BackColor = Drawing.SystemColors.Window
    End Sub



    Private Sub C_SourceConnMode_SelectedValueChanged(sender As Object, e As EventArgs) Handles C_SourceConnMode.SelectedValueChanged
        SourceConnectionTypeChoosed()
        Me.C_SourceConnMode.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub C_TargetConnectionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles C_TargetConnectionType.SelectedIndexChanged
        TargetConnectionTypeChoosed()
        Me.C_TargetConnectionType.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub C_SourceFilterType_SelectedValueChanged(sender As Object, e As EventArgs) Handles C_SourceFilterType.SelectedValueChanged
        FilterTypes()
    End Sub

    Private Sub C_TargetServerType_SelectedValueChanged(sender As Object, e As EventArgs) Handles C_TargetServerType.SelectedValueChanged
        TargetConnectionTypes()
        Me.C_TargetServerType.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Function ValidateUserInput() As Boolean

        If IsNothing(Me.T_Jobname.Text) Or Me.T_Jobname.Text = "" Then
            Me.T_Jobname.BackColor = Drawing.Color.Red
            MsgBox("You have to enter a Jobname!")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNothing(Me.T_LoggingDirectory.Text) Or Me.T_LoggingDirectory.Text = "" Then
            Me.T_LoggingDirectory.BackColor = Drawing.Color.Red
            MsgBox("You have to specify a folder where logfiles can be saved!")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNothing(Me.C_SourceType.Text) Or Me.C_SourceType.Text = "" Then
            Me.C_SourceType.BackColor = Drawing.Color.Red
            MsgBox("Please choose a server type for your datasource!")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNothing(Me.T_SourceAdress.Text) Or Me.T_SourceAdress.Text = "" Then
            Me.T_SourceAdress.BackColor = Drawing.Color.Red
            MsgBox("Please enter the adress of your datasource!")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNothing(Me.T_SourceDB.Text) Or Me.T_SourceDB.Text = "" Then
            Me.T_SourceDB.BackColor = Drawing.Color.Red
            MsgBox("Please enter the database you want to connect to!")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNothing(Me.C_SourceConnMode.Text) Or Me.C_SourceConnMode.Text = "" Then
            Me.C_SourceConnMode.BackColor = Drawing.Color.Red
            MsgBox("Please enter the way your authentication method!")
            ValidateUserInput = False
            Exit Function
        End If

        If Me.C_SourceConnMode.Text = "Normal" And (IsNothing(Me.T_SourceUsername.Text) = True Or Me.T_SourceUsername.Text = "" Or IsNothing(Me.T_SourcePassword.Text) = True Or Me.T_SourceUsername.Text = "") Then
            Me.T_SourceUsername.BackColor = Drawing.Color.Red
            Me.T_SourcePassword.BackColor = Drawing.Color.Red
            MsgBox("You have to enter username and password for this authentication method!" & "Note that anonymous login is not provided in this version.")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNothing(Me.T_SourceTable.Text) Or Me.T_SourceTable.Text = "" Then
            Me.T_SourceTable.BackColor = Drawing.Color.Red
            MsgBox("Please choose a table of your source database!")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNothing(Me.T_SourceIDColumn.Text) Or Me.T_SourceIDColumn.Text = "" Then
            Me.T_SourceIDColumn.BackColor = Drawing.Color.Red
            MsgBox("Please enter the identifier column!")
            ValidateUserInput = False
            Exit Function
        End If

        If Me.C_SourceFilterType.Text = "one column match" And (IsNothing(Me.T_SourceFilterColumn.Text) = True Or Me.T_SourceFilterColumn.Text = "" Or IsNothing(Me.T_SourceFilterValue.Text) = True Or Me.T_SourceFilterValue.Text = "") Then
            Me.T_SourceFilterColumn.BackColor = Drawing.Color.Red
            Me.T_SourceFilterValue.BackColor = Drawing.Color.Red
            MsgBox("Please enter the column you want to use for your filter and the corrosponding value." & vbLf & "If you don't want to filter your data choose" & Chr(34) & "none" & Chr(34) & "in the Filter Type dropdown menu.")
            ValidateUserInput = False
            Exit Function
        End If

        If Me.C_SourceFilterType.Text = "SQL Filter" And (IsNothing(Me.T_SourceSQLFilter.Text) = True Or Me.T_SourceSQLFilter.Text = "") Then
            Me.T_SourceSQLFilter.BackColor = Drawing.Color.Red
            MsgBox("Please enter the SQL Filter." & vbLf & "If you don't want to filter your data choose" & Chr(34) & "none" & Chr(34) & "in the Filter Type dropdown menu.")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNothing(Me.C_TargetServerType.Text) Or Me.C_TargetServerType.Text = "" Then
            Me.C_TargetServerType.BackColor = Drawing.Color.Red
            MsgBox("Please choose a server type for your datatarget!")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNothing(Me.T_TargetServerAdress.Text) Or Me.T_TargetServerAdress.Text = "" Then
            Me.T_TargetServerAdress.BackColor = Drawing.Color.Red
            MsgBox("Please enter the adress of your datatarget!")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNothing(Me.T_TargetDB.Text) Or Me.T_TargetDB.Text = "" Then
            Me.T_TargetDB.BackColor = Drawing.Color.Red
            MsgBox("Please enter the database you want to connect to!")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNothing(Me.C_TargetConnectionType.Text) Or Me.C_TargetConnectionType.Text = "" Then
            Me.C_TargetConnectionType.BackColor = Drawing.Color.Red
            MsgBox("Please enter the way your authentication method!")
            ValidateUserInput = False
            Exit Function
        End If

        If Me.C_TargetConnectionType.Text = "Normal" And (IsNothing(Me.T_TargetUsername.Text) = True Or Me.T_TargetUsername.Text = "" Or IsNothing(Me.T_TargetPassword.Text) = True Or Me.T_TargetPassword.Text = "") Then
            Me.T_TargetUsername.BackColor = Drawing.Color.Red
            Me.T_TargetPassword.BackColor = Drawing.Color.Red
            MsgBox("You have to enter username and password for this authentication method!" & "Note that anonymous login is not provided in this version.")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNothing(Me.T_TargetTable.Text) Or Me.T_TargetTable.Text = "" Then
            Me.T_TargetTable.BackColor = Drawing.Color.Red
            MsgBox("Please choose a table of your target database!")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNothing(Me.T_TargetIDColumn.Text) Or Me.T_TargetIDColumn.Text = "" Then
            Me.T_TargetIDColumn.BackColor = Drawing.Color.Red
            MsgBox("Please enter the identifier column!")
            ValidateUserInput = False
            Exit Function
        End If

        If Me.C_MapIDValue.Checked = True And (IsNothing(Me.T_TargetSeperator.Text) Or Me.T_TargetSeperator.Text = "" Or IsNothing(Me.C_TargetPartSubstring.Text) Or Me.C_TargetPartSubstring.Text = "") Then
            Me.T_TargetSeperator.BackColor = Drawing.Color.Red
            Me.C_TargetPartSubstring.BackColor = Drawing.Color.Red
            MsgBox("You have not defined a seperator char/word or the part of the substring for mapping the ID value from source to target." & vbLf & "If you just want to let the program match the ID values, without reformatting, then uncheck the Map ID Value checkbox.")
            ValidateUserInput = False
            Exit Function
        End If

        If Me.C_InsertAllowed.Checked = False And Me.C_UpdateAllowed.Checked = False And Me.C_DeleteAllowed.Checked = False Then
            Me.C_InsertAllowed.BackColor = Drawing.Color.Red
            Me.C_UpdateAllowed.BackColor = Drawing.Color.Red
            Me.C_DeleteAllowed.BackColor = Drawing.Color.Red
            MsgBox("Please check the statements the application may do in your target database.")
            ValidateUserInput = False
            Exit Function
        End If

        If MappingGrid.RowCount <= 0 Then
            MsgBox("Please specify your column mappings!")
            ValidateUserInput = False
            Exit Function
        End If

        Dim ErrorInGrid As Boolean = False
        Dim ErrorMessages As New LinkedList(Of String)
        Dim EmptyRowsInGrid As Integer = 0
        Dim Rows As Integer = 0
        Dim Columns As Integer = 0
        For Rows = 0 To MappingGrid.RowCount - 1
            If IsEmptyRowInGrid(Rows) = True Then
                EmptyRowsInGrid = EmptyRowsInGrid + 1
                If Rows < MappingGrid.RowCount - 1 Or Rows = 0 Then
                    ErrorInGrid = True
                    ErrorMessages.AddLast("Warning empty row in Grid on Line " & Rows + 1)
                End If
            Else
                Dim SeperatorChecked As Boolean = False
                For Columns = 0 To MappingGrid.ColumnCount - 1
                    Select Case MappingGrid.Columns.Item(Columns).Name
                        Case "SourceColumn"
                            If IsNothing(MappingGrid.Item(Columns, Rows).Value) = True Then
                                ErrorInGrid = True
                                ErrorMessages.AddLast("Missing Source Column on Line " & Rows + 1)
                            End If
                        Case "TargetColumn"
                            If IsNothing(MappingGrid.Item(Columns, Rows).Value) = True Then
                                ErrorInGrid = True
                                ErrorMessages.AddLast("Missing Target Column on Line " & Rows + 1)
                            End If
                        Case "SourceType"
                            If IsNothing(MappingGrid.Item(Columns, Rows).Value) = True Then
                                ErrorInGrid = True
                                ErrorMessages.AddLast("Missing Source Type on Line " & Rows + 1)
                            End If
                        Case "TargetType"
                            If IsNothing(MappingGrid.Item(Columns, Rows).Value) = True Then
                                ErrorInGrid = True
                                ErrorMessages.AddLast("Missing Target Type on Line " & Rows + 1)
                            End If
                        Case "Seperator"
                            If IsNothing(MappingGrid.Item(Columns, Rows).Value) = False Then
                                SeperatorChecked = True
                            End If
                        Case "PartOfSubstring"
                            If SeperatorChecked = True And IsNothing(MappingGrid.Item(Columns, Rows).Value) = True Then
                                ErrorInGrid = True
                                ErrorMessages.AddLast("Missing Part of Substring on Line " & Rows + 1)
                            End If
                    End Select
                Next
                SeperatorChecked = False
            End If
        Next

        If ErrorInGrid = True Then
            Dim ErrorString As String = ""
            For Each Line In ErrorMessages
                If ErrorString = "" Then
                    ErrorString = Line
                Else
                    ErrorString = vbLf & Line
                End If
            Next
            MsgBox("There are some errors in your mapping definitions" & vbLf & ErrorString)
            ValidateUserInput = False
            Exit Function
        End If



        ValidateUserInput = True
    End Function

    Private Function IsEmptyRowInGrid(RowIndex As Integer) As Boolean
        Dim Columns As Integer
        For Columns = 0 To MappingGrid.ColumnCount - 1
            If IsNothing(MappingGrid.Item(Columns, RowIndex).Value) = False Then
                IsEmptyRowInGrid = False
                Exit Function
            End If
        Next
        IsEmptyRowInGrid = True
    End Function

    Private Sub T_Jobname_Click(sender As Object, e As EventArgs) Handles T_Jobname.Click
        Me.T_Jobname.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_LoggingDirectory_Click(sender As Object, e As EventArgs) Handles T_LoggingDirectory.Click
        Me.T_LoggingDirectory.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_LoggingDirectory_TextChanged(sender As Object, e As EventArgs) Handles T_LoggingDirectory.TextChanged
        Me.T_LoggingDirectory.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_SourceAdress_Click(sender As Object, e As EventArgs) Handles T_SourceAdress.Click
        Me.T_SourceAdress.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_SourceDB_Click(sender As Object, e As EventArgs) Handles T_SourceDB.Click
        Me.T_SourceDB.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_SourceTable_TextChanged(sender As Object, e As EventArgs) Handles T_SourceTable.TextChanged
        Me.T_SourceTable.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_SourceIDColumn_TextChanged(sender As Object, e As EventArgs) Handles T_SourceIDColumn.TextChanged
        Me.T_SourceIDColumn.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_SourceFilterColumn_TextChanged(sender As Object, e As EventArgs) Handles T_SourceFilterColumn.TextChanged
        Me.T_SourceFilterColumn.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_SourceSQLFilter_TextChanged(sender As Object, e As EventArgs) Handles T_SourceSQLFilter.TextChanged
        Me.T_SourceSQLFilter.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_SourceFilterValue_TextChanged(sender As Object, e As EventArgs) Handles T_SourceFilterValue.TextChanged
        Me.T_SourceFilterValue.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_TargetServerAdress_TextChanged(sender As Object, e As EventArgs) Handles T_TargetServerAdress.TextChanged
        Me.T_TargetServerAdress.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_TargetDB_TextChanged(sender As Object, e As EventArgs) Handles T_TargetDB.TextChanged
        Me.T_TargetDB.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_TargetUsername_TextChanged(sender As Object, e As EventArgs) Handles T_TargetUsername.TextChanged
        Me.T_TargetUsername.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_TargetPassword_TextChanged(sender As Object, e As EventArgs) Handles T_TargetPassword.TextChanged
        Me.T_TargetPassword.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_TargetTable_TextChanged(sender As Object, e As EventArgs) Handles T_TargetTable.TextChanged

        Me.T_TargetTable.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_TargetIDColumn_TextChanged(sender As Object, e As EventArgs) Handles T_TargetIDColumn.TextChanged
        Me.T_TargetIDColumn.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_TargetSeperator_TextChanged(sender As Object, e As EventArgs) Handles T_TargetSeperator.TextChanged
        Me.T_TargetSeperator.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub C_TargetPartSubstring_SelectedValueChanged(sender As Object, e As EventArgs) Handles C_TargetPartSubstring.SelectedValueChanged
        Me.C_TargetPartSubstring.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub C_InsertAllowed_Click(sender As Object, e As EventArgs) Handles C_InsertAllowed.Click
        Me.C_InsertAllowed.BackColor = Drawing.SystemColors.Control
    End Sub

    Private Sub C_UpdateAllowed_Click(sender As Object, e As EventArgs) Handles C_UpdateAllowed.Click
        Me.C_UpdateAllowed.BackColor = Drawing.SystemColors.Control
    End Sub

    Private Sub C_DeleteAllowed_Click(sender As Object, e As EventArgs) Handles C_DeleteAllowed.Click
        Me.C_DeleteAllowed.BackColor = Drawing.SystemColors.Control
    End Sub

    Private Sub VerifyConnections()

        VerifyDatasourceConnection()
        Me.Refresh()

        VerifyDataTargetConnection()
        Me.Refresh()
    End Sub

    Private Sub VerifyDatasourceConnection()
        If Me.C_SourceType.Text <> "" And Me.T_SourceAdress.Text <> "" And Me.T_SourceDB.Text <> "" And Me.C_SourceType.Text <> "" Then
            If Me.C_SourceConnMode.Text = "Trusted" Then
                Dim SourceSettings As New SQLServerSettings With {
                    .Direction = "source",
                    .Servertype = Me.C_SourceType.Text,
                    .Servername = Me.T_SourceAdress.Text,
                    .SQLDB = Me.T_SourceDB.Text,
                    .ConnMode = Me.C_SourceConnMode.Text
                }

                If IsNothing(Me.TestedSourceSetting) = False Then
                    If Me.TestedSourceSetting.Tested = True Then
                        If Me.TestedSourceSetting.Servername = SourceSettings.Servername And Me.TestedSourceSetting.Servertype = SourceSettings.Servertype And Me.TestedSourceSetting.ConnMode = SourceSettings.ConnMode Then
                            Exit Sub
                        End If
                    End If
                End If

                Dim Log As New LOG With {
                    .Testmode = True
                }
                Module1.Core.CurrentLog = Log
                Dim SQL As New SQL With {
                    .Setting = SourceSettings,
                    .SQLLog = Log
                }

                SQL.CreateSQLCon()
                Me.TestedSourceSetting = SourceSettings
                Me.TestedSourceSetting.Tested = True
                'At the moment only MS-SQL, so just one connection object to check...
                If IsNothing(SQL.SQLCon) = True Then
                    PB_Source.BackColor = Drawing.Color.Red
                Else
                    PB_Source.BackColor = Drawing.Color.Green
                    Me.TestedSourceSetting.Worked = True
                End If
            Else
                If Me.T_SourceUsername.Text <> "" And Me.T_SourcePassword.Text <> "" Then
                    Dim SourceSettings As New SQLServerSettings With {
                        .Direction = "source",
                        .Servertype = Me.C_SourceType.Text,
                        .Servername = Me.T_SourceAdress.Text,
                        .SQLDB = Me.T_SourceDB.Text,
                        .ConnMode = Me.C_SourceConnMode.Text,
                        .User = Me.T_SourceUsername.Text,
                        .Password = Me.T_SourcePassword.Text
                    }

                    If IsNothing(Me.TestedSourceSetting) = False Then
                        If Me.TestedSourceSetting.Tested = True Then
                            If Me.TestedSourceSetting.Servername = SourceSettings.Servername And Me.TestedSourceSetting.Servertype = SourceSettings.Servertype And Me.TestedSourceSetting.ConnMode = SourceSettings.ConnMode And Me.TestedSourceSetting.User = SourceSettings.User And Me.TestedSourceSetting.Password = SourceSettings.Password Then
                                Exit Sub
                            End If
                        End If
                    End If

                    Dim Log As New LOG With {
                        .Testmode = True
                    }
                    Module1.Core.CurrentLog = Log
                    Dim SQL As New SQL With {
                        .Setting = SourceSettings,
                        .SQLLog = Log
                    }

                    SQL.CreateSQLCon()
                    Me.TestedSourceSetting = SourceSettings
                    Me.TestedSourceSetting.Tested = True

                    Select Case SourceSettings.Servertype
                        Case "MS-SQL"
                            If IsNothing(SQL.SQLCon) = True Then
                                PB_Source.BackColor = Drawing.Color.Red
                            Else
                                PB_Source.BackColor = Drawing.Color.Green
                                Me.TestedSourceSetting.Worked = True
                            End If
                        Case "MySQL"
                            If IsNothing(SQL.MySQLCon) = True Then
                                PB_Source.BackColor = Drawing.Color.Red
                            Else
                                PB_Source.BackColor = Drawing.Color.Green
                                Me.TestedSourceSetting.Worked = True
                            End If
                    End Select

                End If
            End If
        End If
    End Sub

    Private Sub VerifyDataTargetConnection()
        If Me.C_TargetServerType.Text <> "" And Me.T_TargetServerAdress.Text <> "" And Me.T_TargetDB.Text <> "" And Me.C_TargetConnectionType.Text <> "" Then
            If Me.C_TargetConnectionType.Text = "Trusted" Then
                Dim TargetSettings As New SQLServerSettings With {
                .Direction = "target",
                .Servertype = Me.C_TargetServerType.Text,
                .Servername = Me.T_TargetServerAdress.Text,
                .SQLDB = Me.T_TargetDB.Text,
                .ConnMode = Me.C_TargetConnectionType.Text
                }

                If IsNothing(Me.TestedTargetSetting) = False Then
                    If Me.TestedTargetSetting.Tested = True Then
                        If Me.TestedTargetSetting.Servername = TargetSettings.Servername And Me.TestedTargetSetting.Servertype = TargetSettings.Servertype And Me.TestedTargetSetting.ConnMode = TargetSettings.ConnMode Then
                            Exit Sub
                        End If
                    End If
                End If

                Dim Log As New LOG With {
                    .Testmode = True
                }
                Module1.Core.CurrentLog = Log
                Dim SQL As New SQL With {
                    .Setting = TargetSettings,
                    .SQLLog = Log
                }

                SQL.CreateSQLCon()
                Me.TestedTargetSetting = TargetSettings
                Me.TestedTargetSetting.Tested = True
                'At the moment only MS-SQL, so just one connection object to check...
                If IsNothing(SQL.SQLCon) = True Then
                    PB_Target.BackColor = Drawing.Color.Red
                Else
                    PB_Target.BackColor = Drawing.Color.Green
                    Me.TestedTargetSetting.Worked = True
                End If
            Else
                If Me.T_TargetUsername.Text <> "" And Me.T_TargetPassword.Text <> "" Then
                    Dim TargetSettings As New SQLServerSettings With {
                    .Servertype = Me.C_TargetServerType.Text,
                    .Servername = Me.T_TargetServerAdress.Text,
                    .SQLDB = Me.T_TargetDB.Text,
                    .ConnMode = Me.C_TargetConnectionType.Text,
                    .User = Me.T_TargetUsername.Text,
                    .Password = Me.T_TargetPassword.Text
                    }

                    If IsNothing(Me.TestedTargetSetting) = False Then
                        If Me.TestedTargetSetting.Tested = True Then
                            If Me.TestedTargetSetting.Servername = TargetSettings.Servername And Me.TestedTargetSetting.Servertype = TargetSettings.Servertype And Me.TestedTargetSetting.ConnMode = TargetSettings.ConnMode And Me.TestedTargetSetting.User = TargetSettings.User And Me.TestedTargetSetting.Password = TargetSettings.Password Then
                                Exit Sub
                            End If
                        End If
                    End If

                    Dim Log As New LOG With {
                        .Testmode = True
                    }
                    Module1.Core.CurrentLog = Log
                    Dim SQL As New SQL With {
                        .Setting = TargetSettings,
                        .SQLLog = Log
                    }

                    SQL.CreateSQLCon()
                    Me.TestedTargetSetting = TargetSettings
                    Me.TestedTargetSetting.Tested = True
                    Select Case TargetSettings.Servertype
                        Case "MS-SQL"
                            If IsNothing(SQL.SQLCon) = True Then
                                PB_Target.BackColor = Drawing.Color.Red
                            Else
                                PB_Target.BackColor = Drawing.Color.Green
                                Me.TestedTargetSetting.Worked = True
                            End If
                        Case "MySQL"
                            If IsNothing(SQL.MySQLCon) = True Then
                                PB_Target.BackColor = Drawing.Color.Red
                            Else
                                PB_Target.BackColor = Drawing.Color.Green
                                Me.TestedTargetSetting.Worked = True
                            End If
                    End Select
                End If
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        VerifyConnections()
    End Sub

    Private Sub C_SourceType_Leave(sender As Object, e As EventArgs) Handles C_SourceType.Leave
        VerifyConnections()
    End Sub

    Private Sub T_SourceAdress_Leave(sender As Object, e As EventArgs) Handles T_SourceAdress.Leave
        VerifyConnections()
    End Sub

    Private Sub T_SourceDB_Leave(sender As Object, e As EventArgs) Handles T_SourceDB.Leave
        VerifyConnections()
    End Sub

    Private Sub C_SourceConnMode_Leave(sender As Object, e As EventArgs) Handles C_SourceConnMode.Leave
        VerifyConnections()
    End Sub

    Private Sub T_SourceUsername_Leave(sender As Object, e As EventArgs) Handles T_SourceUsername.Leave
        VerifyConnections()
    End Sub

    Private Sub T_SourcePassword_Leave(sender As Object, e As EventArgs) Handles T_SourcePassword.Leave
        VerifyConnections()
    End Sub

    Private Sub C_TargetServerType_Leave(sender As Object, e As EventArgs) Handles C_TargetServerType.Leave
        VerifyConnections()
    End Sub

    Private Sub T_TargetServerAdress_Leave(sender As Object, e As EventArgs) Handles T_TargetServerAdress.Leave
        VerifyConnections()
    End Sub

    Private Sub T_TargetDB_Leave(sender As Object, e As EventArgs) Handles T_TargetDB.Leave
        VerifyConnections()
    End Sub

    Private Sub C_TargetConnectionType_Leave(sender As Object, e As EventArgs) Handles C_TargetConnectionType.Leave
        VerifyConnections()
    End Sub

    Private Sub T_TargetUsername_Leave(sender As Object, e As EventArgs) Handles T_TargetUsername.Leave
        VerifyConnections()
    End Sub

    Private Sub T_TargetPassword_Leave(sender As Object, e As EventArgs) Handles T_TargetPassword.Leave
        VerifyConnections()
    End Sub
End Class