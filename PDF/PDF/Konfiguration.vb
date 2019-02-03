Public Class Konfiguration
    Private TestedSourceSetting As SQLServerSettings
    Private TestedTargetSetting As SQLServerSettings
    Private SourceSQL As MyDataConnector
    Private TargetSQL As MyDataConnector
    Private OnlineConfig As Boolean = False


    Private Sub T_SourceUsername_TextChanged(sender As Object, e As EventArgs) Handles T_SourceUsername.TextChanged
        Me.T_SourceUsername.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub T_SourcePassword_TextChanged(sender As Object, e As EventArgs) Handles T_SourcePassword.TextChanged
        Me.T_SourcePassword.BackColor = Drawing.SystemColors.Window
    End Sub


    Private Sub B_LoggingDirectory_Click(sender As Object, e As EventArgs) Handles B_LoggingDirectory.Click
        FolderBrowserDialog1.ShowDialog()
        Me.T_LoggingDirectory.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub SourceConnectionTypes()
        Select Case Me.C_SourceType.Text
            Case "MS-SQL"
                Me.L_SourceServerAdress.Visible = True
                Me.T_SourceAdress.Visible = True
                Me.T_SourcePath.Visible = False
                Me.L_SourcePath.Visible = False
                Me.B_SourcePath.Visible = False
                Me.C_SourceConnMode.Visible = True
                Me.L_SourceConnectionType.Visible = True
                Me.C_SourceConnMode.Items.Clear()
                Me.C_SourceConnMode.Items.Add("Normal")
                Me.C_SourceConnMode.Items.Add("Trusted")
                Me.C_SourceConnMode.Text = "Normal"
                Me.L_SourceDB.Visible = True
                Me.T_SourceDB.Visible = True
                Me.T_SourceDB.Text = ""
                Me.MappingGrid_Offline.Columns("XMLAttribute").Visible = False
                Me.MappingGrid_Offline.Columns("SourceXPath").Visible = False
                Me.MappingGrid_Offline.Columns("SourceColumn_Offline").Visible = True
            Case "MySQL"
                Me.L_SourceServerAdress.Visible = True
                Me.T_SourceAdress.Visible = True
                Me.T_SourcePath.Visible = False
                Me.L_SourcePath.Visible = False
                Me.B_SourcePath.Visible = False
                Me.C_SourceConnMode.Visible = True
                Me.L_SourceConnectionType.Visible = True
                Me.C_SourceConnMode.Items.Clear()
                Me.C_SourceConnMode.Items.Add("Normal")
                Me.C_SourceConnMode.Text = "Normal"
                Me.L_SourceDB.Visible = True
                Me.T_SourceDB.Visible = True
                Me.T_SourceDB.Text = ""
                Me.MappingGrid_Offline.Columns("XMLAttribute").Visible = False
                Me.MappingGrid_Offline.Columns("SourceXPath").Visible = False
                Me.MappingGrid_Offline.Columns("SourceColumn_Offline").Visible = True
            Case "Access"
                Me.L_SourceServerAdress.Visible = False
                Me.T_SourceAdress.Visible = False
                Me.T_SourcePath.Visible = True
                Me.L_SourcePath.Visible = True
                Me.B_SourcePath.Visible = True
                Me.C_SourceConnMode.Visible = True
                Me.L_SourceConnectionType.Visible = True
                Me.C_SourceConnMode.Items.Clear()
                Me.C_SourceConnMode.Items.Add("Standard Security")
                Me.C_SourceConnMode.Text = "Standard Security"
                Me.T_SourceUsername.Visible = False
                Me.T_SourcePassword.Visible = False
                Me.T_SourcePassword.Text = "  "
                Me.T_SourceUsername.Text = "  "
                Me.L_SourceUsername.Visible = False
                Me.L_SourcePassword.Visible = False
                Me.L_SourceDB.Visible = True
                Me.T_SourceDB.Visible = True
                Me.T_SourceDB.Text = ""
                Me.MappingGrid_Offline.Columns("XMLAttribute").Visible = False
                Me.MappingGrid_Offline.Columns("SourceXPath").Visible = False
                Me.MappingGrid_Offline.Columns("SourceColumn_Offline").Visible = True
            Case "XML"
                Me.L_SourceServerAdress.Visible = False
                Me.T_SourceAdress.Visible = False
                Me.T_SourcePath.Visible = True
                Me.L_SourcePath.Visible = True
                Me.B_SourcePath.Visible = True
                Me.C_SourceConnMode.Visible = True
                Me.L_SourceConnectionType.Visible = True
                Me.C_SourceConnMode.Items.Clear()
                Me.C_SourceConnMode.Items.Add("Normal")
                Me.C_SourceConnMode.Text = "Normal"
                Me.T_SourceUsername.Visible = False
                Me.T_SourcePassword.Visible = False
                Me.T_SourcePassword.Text = "  "
                Me.T_SourceUsername.Text = "  "
                Me.L_SourceUsername.Visible = False
                Me.L_SourcePassword.Visible = False
                Me.L_SourceDB.Visible = False
                Me.T_SourceDB.Visible = False
                Me.T_SourceDB.Text = "XML"
                Me.MappingGrid_Offline.Columns("XMLAttribute").Visible = True
                Me.MappingGrid_Offline.Columns("SourceXPath").Visible = False
                Me.MappingGrid_Offline.Columns("SourceColumn_Offline").Visible = True
            Case "CSV"
                Me.L_SourceServerAdress.Visible = False
                Me.T_SourceAdress.Visible = False
                Me.T_SourcePath.Visible = True
                Me.L_SourcePath.Visible = True
                Me.B_SourcePath.Visible = True
                Me.C_SourceConnMode.Visible = True
                Me.L_SourceConnectionType.Visible = True
                Me.C_SourceConnMode.Items.Clear()
                Me.C_SourceConnMode.Items.Add("Normal")
                Me.C_SourceConnMode.Text = "Normal"
                Me.T_SourceUsername.Visible = False
                Me.T_SourcePassword.Visible = False
                Me.T_SourcePassword.Text = "  "
                Me.T_SourceUsername.Text = "  "
                Me.L_SourceUsername.Visible = False
                Me.L_SourcePassword.Visible = False
                Me.L_SourceDB.Visible = False
                Me.T_SourceDB.Visible = False
                Me.T_SourceDB.Text = "CSV"
                Me.MappingGrid_Offline.Columns("XMLAttribute").Visible = False
                Me.MappingGrid_Offline.Columns("SourceXPath").Visible = False
                Me.MappingGrid_Offline.Columns("SourceColumn_Offline").Visible = True
            Case "HTML"
                Me.L_SourceServerAdress.Visible = False
                Me.T_SourceAdress.Visible = False
                Me.T_SourcePath.Visible = True
                Me.L_SourcePath.Visible = True
                Me.B_SourcePath.Visible = True
                Me.C_SourceConnMode.Visible = True
                Me.L_SourceConnectionType.Visible = True
                Me.C_SourceConnMode.Items.Clear()
                Me.C_SourceConnMode.Items.Add("Normal")
                Me.C_SourceConnMode.Text = "Normal"
                Me.T_SourceUsername.Visible = False
                Me.T_SourcePassword.Visible = False
                Me.T_SourcePassword.Text = "  "
                Me.T_SourceUsername.Text = "  "
                Me.L_SourceUsername.Visible = False
                Me.L_SourcePassword.Visible = False
                Me.L_SourceDB.Visible = False
                Me.T_SourceDB.Visible = False
                Me.T_SourceDB.Text = "HTML"
                Me.MappingGrid_Offline.Columns("XMLAttribute").Visible = False
                Me.MappingGrid_Offline.Columns("SourceXPath").Visible = True
                Me.MappingGrid_Offline.Columns("SourceColumn_Offline").Visible = False
            Case Else
                Me.L_SourceServerAdress.Visible = True
                Me.T_SourceAdress.Visible = True
                Me.T_SourcePath.Visible = False
                Me.L_SourcePath.Visible = False
                Me.B_SourcePath.Visible = False
                Me.C_SourceConnMode.Visible = False
                Me.L_SourceConnectionType.Visible = False
                Me.L_SourceDB.Visible = True
                Me.T_SourceDB.Visible = True
                Me.T_SourceDB.Text = ""
                Me.MappingGrid_Offline.Columns("XMLAttribute").Visible = False
                Me.MappingGrid_Offline.Columns("SourceXPath").Visible = False
                Me.MappingGrid_Offline.Columns("SourceColumn_Offline").Visible = True
        End Select
    End Sub

    Private Sub TargetConnectionTypes()
        Select Case Me.C_TargetServerType.Text
            Case "MS-SQL"
                Me.L_TargetServerAdress.Visible = True
                Me.T_TargetServerAdress.Visible = True
                Me.T_TargetPath.Visible = False
                Me.L_TargetPath.Visible = False
                Me.B_TargetPath.Visible = False
                Me.C_TargetConnectionType.Visible = True
                Me.L_TargetConnectionType.Visible = True
                Me.C_TargetConnectionType.Items.Clear()
                Me.C_TargetConnectionType.Items.Add("Normal")
                Me.C_TargetConnectionType.Items.Add("Trusted")
                Me.C_TargetConnectionType.Text = ""
                Me.L_TargetDB.Visible = True
                Me.T_TargetDB.Visible = True
                Me.T_TargetDB.Text = ""
                Me.B_TargetSaveFile.Visible = False
            Case "MySQL"
                Me.L_TargetServerAdress.Visible = True
                Me.T_TargetServerAdress.Visible = True
                Me.T_TargetPath.Visible = False
                Me.L_TargetPath.Visible = False
                Me.B_TargetPath.Visible = False
                Me.C_TargetConnectionType.Visible = True
                Me.L_TargetConnectionType.Visible = True
                Me.C_TargetConnectionType.Items.Clear()
                Me.C_TargetConnectionType.Items.Add("Normal")
                Me.C_TargetConnectionType.Text = "Normal"
                Me.L_TargetDB.Visible = True
                Me.T_TargetDB.Visible = True
                Me.T_TargetDB.Text = ""
                Me.B_TargetSaveFile.Visible = False
            Case "Access"
                Me.L_TargetServerAdress.Visible = False
                Me.T_TargetServerAdress.Visible = False
                Me.T_TargetPath.Visible = True
                Me.L_TargetPath.Visible = True
                Me.B_TargetPath.Visible = True
                Me.C_TargetConnectionType.Visible = True
                Me.L_TargetConnectionType.Visible = True
                Me.C_TargetConnectionType.Items.Clear()
                Me.C_TargetConnectionType.Items.Add("Standard Security")
                Me.C_TargetConnectionType.Text = "Standard Security"
                Me.T_TargetUsername.Visible = False
                Me.T_TargetUsername.Text = "  "
                Me.T_TargetPassword.Visible = False
                Me.T_TargetPassword.Text = "  "
                Me.B_TargetPath.Visible = True
                Me.B_TargetSaveFile.Visible = False
            Case "XML"
                Me.L_TargetServerAdress.Visible = False
                Me.T_TargetServerAdress.Visible = False
                Me.T_TargetPath.Visible = True
                Me.L_TargetPath.Visible = True
                Me.B_TargetPath.Visible = True
                Me.C_TargetConnectionType.Visible = True
                Me.L_TargetConnectionType.Visible = True
                Me.C_TargetConnectionType.Items.Clear()
                Me.C_TargetConnectionType.Items.Add("Normal")
                Me.C_TargetConnectionType.Text = "Normal"
                Me.T_TargetUsername.Visible = False
                Me.T_TargetPassword.Visible = False
                Me.T_TargetPassword.Text = "  "
                Me.T_TargetUsername.Text = "  "
                Me.L_TargetUsername.Visible = False
                Me.L_TargetPassword.Visible = False
                Me.L_TargetDB.Visible = False
                Me.T_TargetDB.Visible = False
                Me.T_TargetDB.Text = "XML"
                Me.B_TargetSaveFile.Visible = True
            Case "CSV"
                Me.L_TargetServerAdress.Visible = False
                Me.T_TargetServerAdress.Visible = False
                Me.T_TargetPath.Visible = True
                Me.L_TargetPath.Visible = True
                Me.B_TargetPath.Visible = True
                Me.C_TargetConnectionType.Visible = True
                Me.L_TargetConnectionType.Visible = True
                Me.C_TargetConnectionType.Items.Clear()
                Me.C_TargetConnectionType.Items.Add("Normal")
                Me.C_TargetConnectionType.Text = "Normal"
                Me.T_TargetUsername.Visible = False
                Me.T_TargetPassword.Visible = False
                Me.T_TargetPassword.Text = "  "
                Me.T_TargetUsername.Text = "  "
                Me.L_TargetUsername.Visible = False
                Me.L_TargetPassword.Visible = False
                Me.L_TargetDB.Visible = False
                Me.T_TargetDB.Visible = False
                Me.T_TargetDB.Text = "CSV"
                Me.B_TargetSaveFile.Visible = True
            Case Else
                Me.L_TargetServerAdress.Visible = True
                Me.T_TargetServerAdress.Visible = True
                Me.C_TargetConnectionType.Visible = False
                Me.L_TargetConnectionType.Visible = False
                Me.T_TargetPath.Visible = False
                Me.L_TargetPath.Visible = False
                Me.B_TargetPath.Visible = False
        End Select
    End Sub

    Private Sub FilterTypes()
        Select Case Me.C_SourceFilterType.Text
            Case "one column match"
                Me.T_SourceSQLFilter.Visible = False
                Me.C_SourceFilterColumn.Visible = True
                Me.T_SourceFilterValue.Visible = True
                Me.L_FilterColumn.Visible = True
                Me.L_FilterValue.Visible = True
                Me.L_SQLFilterStatement.Visible = False
            Case "SQL Filter"
                Me.T_SourceSQLFilter.Visible = True
                Me.C_SourceFilterColumn.Visible = False
                Me.T_SourceFilterValue.Visible = False
                Me.L_FilterColumn.Visible = False
                Me.L_FilterValue.Visible = False
                Me.L_SQLFilterStatement.Visible = True
            Case "none"
                Me.T_SourceSQLFilter.Visible = False
                Me.C_SourceFilterColumn.Visible = False
                Me.T_SourceFilterValue.Visible = False
                Me.L_FilterColumn.Visible = False
                Me.L_FilterValue.Visible = False
                Me.L_SQLFilterStatement.Visible = False
            Case Else
                Me.T_SourceSQLFilter.Visible = False
                Me.C_SourceFilterColumn.Visible = False
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
        If Me.T_OrderID.Text = "" Or IsNothing(Me.T_OrderID.Text) = True Then
            ENV.OrderID = 0
        Else
            ENV.OrderID = Me.T_OrderID.Text
        End If

        ENV.IDLessBatch = Me.C_IDlessBatch.Checked
        ENV.HasMultipleIdentifiers = Me.MultipleIdentifier.Checked

        If Me.C_DebugLog.Checked = True Then
            ENV.LogLevel = "1"
        Else
            ENV.LogLevel = "0"
        End If

        If Me.C_Silent.Checked = True Then
            ENV.LogSilent = True
        Else
            ENV.LogSilent = False
        End If

        If Me.C_CheckConsistency.Checked = True Then
            ENV.ConsistenceCheck = True
        Else
            ENV.ConsistenceCheck = False
        End If

        Dim SourceSettings As New SQLServerSettings With {
            .Direction = "source",
            .Servertype = Me.C_SourceType.Text,
            .Servername = Me.T_SourceAdress.Text,
            .FilePath = Me.T_SourcePath.Text,
            .SQLDB = Me.T_SourceDB.Text,
            .ConnMode = Me.C_SourceConnMode.Text,
            .User = Me.T_SourceUsername.Text,
            .Password = Me.T_SourcePassword.Text,
            .SQLTable = Me.C_SourceTable.Text,
            .IDColumn = Me.C_SourceIDColumn.Text,
            .IDColumnDataType = Me.C_SourceIDDatatype.Text,
            .Filtertype = Me.C_SourceFilterType.Text,
            .FilterColumn = Me.C_SourceFilterColumn.Text,
            .FilterValue = Me.T_SourceFilterValue.Text,
            .SQLFilter = Me.T_SourceSQLFilter.Text
        }

        Dim TargetSettings As New SQLServerSettings With {
            .Direction = "target",
            .Servertype = Me.C_TargetServerType.Text,
            .Servername = Me.T_TargetServerAdress.Text,
            .FilePath = Me.T_TargetPath.Text,
            .SQLDB = Me.T_TargetDB.Text,
            .ConnMode = Me.C_TargetConnectionType.Text,
            .User = Me.T_TargetUsername.Text,
            .Password = Me.T_TargetPassword.Text,
            .SQLTable = Me.C_TargetTable.Text,
            .IDColumn = Me.C_TargetIDColumn.Text,
            .IDColumnDataType = Me.C_TargetIDDatatype.Text
        }

        If Me.C_MapIDValue.Checked = True Then
            TargetSettings.MapTargetIDColumnValue = True
        Else
            TargetSettings.MapTargetIDColumnValue = False
        End If
        TargetSettings.StringSeperator = Me.T_TargetSeperator.Text
        TargetSettings.StringPart = Me.C_TargetPartSubstring.Text

        TargetSettings.SessionTimestampField = Me.C_TargetTimestampfield.Text

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

        'Depeding on which MappingGrid is visible to the user:
        'The visible MappingGrid will be used as leading grid in terms of storing the config.
        If Me.MappingGrid.Visible = True Then
            For Rows = 0 To MappingGrid.RowCount - 1
                Dim Mapping As New Mapping
                For Columns = 0 To MappingGrid.ColumnCount - 1
                    Select Case MappingGrid.Columns.Item(Columns).Name
                        Case "SourceColumn"
                            If MappingGrid.Item(Columns, Rows).Value = "" Then
                                Mapping.NoSource = True
                            End If
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
                        Case "StaticValue"
                            Mapping.StaticValue = MappingGrid.Item(Columns, Rows).Value
                        Case "IsIdentity"
                            Mapping.UseAsIdentifier = MappingGrid.Item(Columns, Rows).Value

                        Case Else
                            Mapping.Sourcename = "Error"
                    End Select
                Next
                If Mapping.Sourcename = "" And Mapping.Targetname = "" And Mapping.Sourcetype = "" And Mapping.Targettype = "" Then
                Else
                    ENV.Mappings.AddLast(Mapping)
                End If
            Next
        Else
            For Rows = 0 To MappingGrid_Offline.RowCount - 1
                Dim Mapping As New Mapping
                For Columns = 0 To MappingGrid_Offline.ColumnCount - 1
                    Select Case MappingGrid_Offline.Columns.Item(Columns).Name
                        Case "SourceColumn_Offline"
                            If MappingGrid_Offline.Item(Columns, Rows).Value = "" Then
                                Mapping.NoSource = True
                            End If
                            Mapping.Sourcename = MappingGrid_Offline.Item(Columns, Rows).Value
                        Case "SourceXPath"
                            If MappingGrid_Offline.Item(Columns, Rows).Value = "" Then
                            Else
                                Mapping.NoSource = False
                                Mapping.Sourcename = MappingGrid_Offline.Item(Columns, Rows).Value
                            End If
                        Case "TargetColumn_Offline"
                            Mapping.Targetname = MappingGrid_Offline.Item(Columns, Rows).Value
                        Case "SourceType_Offline"
                            Mapping.Sourcetype = MappingGrid_Offline.Item(Columns, Rows).Value
                        Case "TargetType_Offline"
                            Mapping.Targettype = MappingGrid_Offline.Item(Columns, Rows).Value
                        Case "Seperator_Offline"
                            Mapping.Separator = MappingGrid_Offline.Item(Columns, Rows).Value
                        Case "PartOfSubstring_Offline"
                            Mapping.SeperatorDirection = MappingGrid_Offline.Item(Columns, Rows).Value
                        Case "StaticValue_Offline"
                            Mapping.StaticValue = MappingGrid_Offline.Item(Columns, Rows).Value
                        Case "XMLAttribute"
                            Mapping.XMLAttributeName = MappingGrid_Offline.Item(Columns, Rows).Value
                        Case "IsIdentity_offline"
                            Mapping.UseAsIdentifier = MappingGrid_Offline.Item(Columns, Rows).Value
                        Case Else
                            Mapping.Sourcename = "Error"
                    End Select
                Next
                If Mapping.Sourcename = "" And Mapping.Targetname = "" And Mapping.Sourcetype = "" And Mapping.Targettype = "" Then
                Else
                    ENV.Mappings.AddLast(Mapping)
                End If
            Next
        End If

        Dim XMLFile As New XMLFiles
        Dim Path As String
        Dim FSO As New FilesystemOperations

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
        VerifyConnections()
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
        VerifyConnections()
        Me.C_TargetServerType.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Function ValidateUserInput() As Boolean

        If IsNothing(Me.T_Jobname.Text) Or Me.T_Jobname.Text = "" Then
            Me.T_Jobname.BackColor = Drawing.Color.Red
            MsgBox("You have To enter a Jobname!")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNumeric(Me.T_OrderID.Text) = False And Not (IsNothing(Me.T_OrderID.Text) = True Or Me.T_OrderID.Text="") Then
            Me.T_OrderID.BackColor = Drawing.Color.Red
            MsgBox("Please enter a numeric value To OrderID")
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
            MsgBox("Please choose a server type For your datasource!")
            ValidateUserInput = False
            Exit Function
        End If

        Select Case Me.C_SourceType.Text
            Case "Access"
                If IsNothing(Me.T_SourcePath.Text) Or Me.T_SourcePath.Text = "" Then
                    Me.T_SourcePath.BackColor = Drawing.Color.Red
                    MsgBox("Please enter the path Of your datasource!")
                    ValidateUserInput = False
                    Exit Function
                End If

            Case "XML"
                If IsNothing(Me.T_SourcePath.Text) Or Me.T_SourcePath.Text = "" Then
                    Me.T_SourcePath.BackColor = Drawing.Color.Red
                    MsgBox("Please enter the path Of your datasource!")
                    ValidateUserInput = False
                    Exit Function
                End If

            Case "CSV"
                If IsNothing(Me.T_SourcePath.Text) Or Me.T_SourcePath.Text = "" Then
                    Me.T_SourcePath.BackColor = Drawing.Color.Red
                    MsgBox("Please enter the path Of your datasource!")
                    ValidateUserInput = False
                    Exit Function
                End If

            Case "HTML"
                If IsNothing(Me.T_SourcePath.Text) Or Me.T_SourcePath.Text = "" Then
                    Me.T_SourcePath.BackColor = Drawing.Color.Red
                    MsgBox("Please enter the path Of your datasource!")
                    ValidateUserInput = False
                    Exit Function
                End If

            Case Else
                If IsNothing(Me.T_SourceAdress.Text) Or Me.T_SourceAdress.Text = "" Then
                    Me.T_SourceAdress.BackColor = Drawing.Color.Red
                    MsgBox("Please enter the adress Of your datasource!")
                    ValidateUserInput = False
                    Exit Function
                End If
        End Select



        If IsNothing(Me.T_SourceDB.Text) Or Me.T_SourceDB.Text = "" Then
            Me.T_SourceDB.BackColor = Drawing.Color.Red
            MsgBox("Please enter the database you want To connect to!")
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
            MsgBox("You have To enter username And password For this authentication method!" & "Note that anonymous login Is Not provided In this version.")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNothing(Me.C_SourceTable.Text) Or Me.C_SourceTable.Text = "" Then
            Me.C_SourceTable.BackColor = Drawing.Color.Red
            MsgBox("Please choose a table Of your source database!")
            ValidateUserInput = False
            Exit Function
        End If

        If (IsNothing(Me.C_SourceIDColumn.Text) Or Me.C_SourceIDColumn.Text = "") And (Me.C_IDlessBatch.Checked = False Or Me.MultipleIdentifier.Checked = False) Then
            Me.C_SourceIDColumn.BackColor = Drawing.Color.Red
            MsgBox("Please enter the identifier column!")
            ValidateUserInput = False
            Exit Function
        End If

        If IsNothing(Me.C_SourceIDDatatype.Text) = True And (Me.C_IDlessBatch.Checked = False Or Me.MultipleIdentifier.Checked = False) Then
            Me.C_SourceIDDatatype.BackColor = Drawing.Color.Red
            MsgBox("Please choose the datatype Of you identifier field")
            ValidateUserInput = False
            Exit Function
        End If

        If Me.C_SourceFilterType.Text = "one column match" And (IsNothing(Me.C_SourceFilterColumn.Text) = True Or Me.C_SourceFilterColumn.Text = "" Or IsNothing(Me.T_SourceFilterValue.Text) = True Or Me.T_SourceFilterValue.Text = "") Then
            Me.C_SourceFilterColumn.BackColor = Drawing.Color.Red
            Me.T_SourceFilterValue.BackColor = Drawing.Color.Red
            MsgBox("Please enter the column you want To use For your filter And the corrosponding value." & vbLf & "If you don't want to filter your data choose" & Chr(34) & "none" & Chr(34) & "in the Filter Type dropdown menu.")
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

        Select Case Me.C_TargetServerType.Text
            Case "Access"
                If IsNothing(Me.T_TargetPath.Text) Or Me.T_TargetPath.Text = "" Then
                    Me.T_TargetPath.BackColor = Drawing.Color.Red
                    MsgBox("Please enter the path of your datatarget!")
                    ValidateUserInput = False
                    Exit Function
                End If

            Case "CSV"
                If IsNothing(Me.T_TargetPath.Text) Or Me.T_TargetPath.Text = "" Then
                    Me.T_TargetPath.BackColor = Drawing.Color.Red
                    MsgBox("Please enter the path of your datatarget!")
                    ValidateUserInput = False
                    Exit Function
                End If

            Case "XML"
                If IsNothing(Me.T_TargetPath.Text) Or Me.T_TargetPath.Text = "" Then
                    Me.T_TargetPath.BackColor = Drawing.Color.Red
                    MsgBox("Please enter the path of your datatarget!")
                    ValidateUserInput = False
                    Exit Function
                End If

            Case "HTML"
                If IsNothing(Me.T_TargetPath.Text) Or Me.T_TargetPath.Text = "" Then
                    Me.T_TargetPath.BackColor = Drawing.Color.Red
                    MsgBox("Please enter the path of your datatarget!")
                    ValidateUserInput = False
                    Exit Function
                End If

            Case Else
                If IsNothing(Me.T_TargetServerAdress.Text) Or Me.T_TargetServerAdress.Text = "" Then
                    Me.T_TargetServerAdress.BackColor = Drawing.Color.Red
                    MsgBox("Please enter the adress of your datatarget!")
                    ValidateUserInput = False
                    Exit Function
                End If
        End Select


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

        If IsNothing(Me.C_TargetTable.Text) Or Me.C_TargetTable.Text = "" Then
            Me.C_TargetTable.BackColor = Drawing.Color.Red
            MsgBox("Please choose a table of your target database!")
            ValidateUserInput = False
            Exit Function
        End If

        If (IsNothing(Me.C_TargetIDColumn.Text) Or Me.C_TargetIDColumn.Text = "") And (Me.C_IDlessBatch.Checked = False Or Me.MultipleIdentifier.Checked = False) Then
            Me.C_TargetIDColumn.BackColor = Drawing.Color.Red
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

        If IsNothing(Me.C_TargetIDDatatype.Text) = True And (Me.C_IDlessBatch.Checked = False Or Me.MultipleIdentifier.Checked = False) Then
            Me.C_TargetIDDatatype.BackColor = Drawing.Color.Red
            MsgBox("Please choose the datatype of your target identifier field")
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

        Dim ErrorInGrid As Boolean = False
        Dim EmptySourceColumn As Boolean = False
        Dim EmptySourceXPath As Boolean = False
        Dim ErrorMessages As New LinkedList(Of String)
        Dim EmptyRowsInGrid As Integer = 0
        Dim Rows As Integer = 0
        Dim Columns As Integer = 0
        Dim HasIdentifier As Boolean = False 'Checks if at least one column is marked as ID-Column if Multiple Identifier Usage is checked
        If MappingGrid.Visible = True Then
            If MappingGrid.RowCount <= 0 Then
                MsgBox("Please specify your column mappings!")
                ValidateUserInput = False
                Exit Function
            End If
            RefreshMappingGridDataTypes()
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
                                    EmptySourceColumn = True
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
                            Case "Static Value for Target Column"
                                If EmptySourceColumn = True And IsNothing(MappingGrid.Item(Columns, Rows).Value) = True Then
                                    ErrorInGrid = True
                                    ErrorMessages.AddLast("Missing Source Column or Static Value on Line " & Rows + 1)
                                End If
                            Case "IsIdentity"
                                If MappingGrid.Item(Columns, Rows).Value = True Then
                                    HasIdentifier = True
                                End If
                        End Select
                    Next
                    SeperatorChecked = False
                End If
            Next
            If Me.MultipleIdentifier.Checked = True And HasIdentifier = False Then
                ErrorInGrid = True
                ErrorMessages.AddLast("You selected Multiple Identifier Usage, but selected no column as identifier. Please select columns as identifier")
            End If
        Else
            If MappingGrid_Offline.RowCount <= 0 Then
                MsgBox("Please specify your column mappings!")
                ValidateUserInput = False
                Exit Function
            End If
            For Rows = 0 To MappingGrid_Offline.RowCount - 1
                If IsEmptyRowInGrid(Rows) = True Then
                    EmptyRowsInGrid = EmptyRowsInGrid + 1
                    If Rows < MappingGrid_Offline.RowCount - 1 Or Rows = 0 Then
                        ErrorInGrid = True
                        ErrorMessages.AddLast("Warning empty row in Grid on Line " & Rows + 1)
                    End If
                Else
                    Dim SeperatorChecked As Boolean = False
                    For Columns = 0 To MappingGrid_Offline.ColumnCount - 1
                        Select Case MappingGrid_Offline.Columns.Item(Columns).Name
                            Case "SourceColumn_Offline"
                                If IsNothing(MappingGrid_Offline.Item(Columns, Rows).Value) = True Then
                                    EmptySourceColumn = True
                                End If
                            Case "SourceXPath"
                                If IsNothing(MappingGrid_Offline.Item(Columns, Rows).Value) = True Then
                                    EmptySourceXPath = True
                                End If
                            Case "TargetColumn_Offline"
                                If IsNothing(MappingGrid_Offline.Item(Columns, Rows).Value) = True Then
                                    ErrorInGrid = True
                                    ErrorMessages.AddLast("Missing Target Column on Line " & Rows + 1)
                                End If
                            Case "SourceType_Offline"
                                If IsNothing(MappingGrid_Offline.Item(Columns, Rows).Value) = True Then
                                    ErrorInGrid = True
                                    ErrorMessages.AddLast("Missing Source Type on Line " & Rows + 1)
                                End If
                            Case "TargetType_Offline"
                                If IsNothing(MappingGrid_Offline.Item(Columns, Rows).Value) = True Then
                                    ErrorInGrid = True
                                    ErrorMessages.AddLast("Missing Target Type on Line " & Rows + 1)
                                End If
                            Case "Seperator_Offline"
                                If IsNothing(MappingGrid_Offline.Item(Columns, Rows).Value) = False Then
                                    If MappingGrid_Offline.Item(Columns, Rows).Value <> "" Then
                                        SeperatorChecked = True
                                    End If
                                End If
                            Case "PartOfSubstring_Offline"
                                If SeperatorChecked = True And IsNothing(MappingGrid_Offline.Item(Columns, Rows).Value) = True Then
                                    ErrorInGrid = True
                                    ErrorMessages.AddLast("Missing Part of Substring on Line " & Rows + 1)
                                End If
                            Case "StaticValue_Offline"
                                If EmptySourceColumn = True And IsNothing(MappingGrid_Offline.Item(Columns, Rows).Value) = True Then
                                    ErrorInGrid = True
                                    ErrorMessages.AddLast("Missing Source Column or Static Value on Line " & Rows + 1)
                                End If
                            Case "IsIdentity_offline"
                                If MultipleIdentifier.Checked = True Then
                                    If MappingGrid.Item(Columns, Rows).Value = True Then
                                        HasIdentifier = True
                                    End If
                                Else

                                End If
                        End Select
                    Next
                    SeperatorChecked = False
                End If
            Next
            If Me.MultipleIdentifier.Checked = True And HasIdentifier = False Then
                ErrorInGrid = True
                ErrorMessages.AddLast("You selected Multiple Identifier Usage, but selected no column as identifier. Please select columns as identifier")
            End If
        End If
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
        If Me.MappingGrid.Visible = True Then
            For Columns = 0 To MappingGrid.ColumnCount - 1
                If IsNothing(MappingGrid.Item(Columns, RowIndex).Value) = False Then
                    IsEmptyRowInGrid = False
                    Exit Function
                End If
            Next
        Else
            For Columns = 0 To MappingGrid_Offline.ColumnCount - 1
                If IsNothing(MappingGrid_Offline.Item(Columns, RowIndex).Value) = False Then
                    IsEmptyRowInGrid = False
                    Exit Function
                End If
            Next
        End If
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

    Private Sub C_SourceTable_TextChanged(sender As Object, e As EventArgs) Handles C_SourceTable.TextChanged
        Me.C_SourceTable.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub C_SourceIDColumn_TextChanged(sender As Object, e As EventArgs) Handles C_SourceIDColumn.TextChanged
        Me.C_SourceIDColumn.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub C_SourceFilterColumn_TextChanged(sender As Object, e As EventArgs) Handles C_SourceFilterColumn.TextChanged
        Me.C_SourceFilterColumn.BackColor = Drawing.SystemColors.Window
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

    Private Sub C_TargetTable_TextChanged(sender As Object, e As EventArgs) Handles C_TargetTable.TextChanged
        Me.C_TargetTable.BackColor = Drawing.SystemColors.Window

    End Sub

    Private Sub C_TargetIDColumn_TextChanged(sender As Object, e As EventArgs) Handles C_TargetIDColumn.TextChanged
        Me.C_TargetIDColumn.BackColor = Drawing.SystemColors.Window
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
        Me.C_SourceTable.Items.Clear()
        VerifyDatasourceConnection()
        If IsNothing(SourceSQL) = True Then
            Me.MappingGrid_Offline.Visible = True
            Me.MappingGrid.Visible = False
        Else
            If SourceSQL.Setting.Worked = True Then
                SourceSQL.Tables.Clear()
                SourceSQL.GetTableNamesFromDatabase()
                For Each Table In SourceSQL.Tables
                    Me.C_SourceTable.Items.Add(Table.TableName)
                Next
            Else
                Me.MappingGrid_Offline.Visible = True
                Me.MappingGrid.Visible = False
                Me.OnlineConfig = False
            End If
        End If
        Me.Refresh()

        Me.C_TargetTable.Items.Clear()
        VerifyDataTargetConnection()
        If IsNothing(TargetSQL) = True Then
            Me.MappingGrid_Offline.Visible = True
            Me.MappingGrid.Visible = False
        Else
            TargetSQL.Tables.Clear()
            TargetSQL.GetTableNamesFromDatabase()
            If TargetSQL.Setting.Worked = True Then
                For Each Table In TargetSQL.Tables
                    Me.C_TargetTable.Items.Add(Table.TableName)
                Next
            Else
                Me.MappingGrid_Offline.Visible = True
                Me.MappingGrid.Visible = False
                Me.OnlineConfig = False
            End If
        End If
        If IsNothing(SourceSQL) Or IsNothing(TargetSQL) Then
        Else
            If SourceSQL.Setting.Worked = True And TargetSQL.Setting.Worked = True Then
                Me.MappingGrid_Offline.Visible = False
                Me.MappingGrid.Visible = True
                Me.OnlineConfig = True
            End If
        End If

        Me.Refresh()
    End Sub

    Private Sub VerifyDatasourceConnection()
        If Me.C_SourceType.Text <> "" And (Me.T_SourceAdress.Text <> "" Or Me.T_SourcePath.Text <> "") And Me.T_SourceDB.Text <> "" And Me.C_SourceType.Text <> "" Then
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
                Dim SQL As New MyDataConnector With {
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
                    Me.SourceSQL = SQL
                End If
            Else
                If Me.T_SourceUsername.Text <> "" And Me.T_SourcePassword.Text <> "" Then
                    Dim SourceSettings As New SQLServerSettings With {
                        .Direction = "source",
                        .Servertype = Me.C_SourceType.Text,
                        .Servername = Me.T_SourceAdress.Text,
                        .FilePath = Me.T_SourcePath.Text,
                        .SQLDB = Me.T_SourceDB.Text,
                        .ConnMode = Me.C_SourceConnMode.Text,
                        .User = Me.T_SourceUsername.Text,
                        .Password = Me.T_SourcePassword.Text
                    }

                    If IsNothing(Me.TestedSourceSetting) = False Then
                        If Me.TestedSourceSetting.Tested = True Then
                            If Me.TestedSourceSetting.SQLDB = SourceSettings.SQLDB And Me.TestedSourceSetting.FilePath = SourceSettings.FilePath And Me.TestedSourceSetting.Servername = SourceSettings.Servername And Me.TestedSourceSetting.Servertype = SourceSettings.Servertype And Me.TestedSourceSetting.ConnMode = SourceSettings.ConnMode And Me.TestedSourceSetting.User = SourceSettings.User And Me.TestedSourceSetting.Password = SourceSettings.Password Then
                                Exit Sub
                            End If
                        End If
                    End If

                    Dim Log As New LOG With {
                        .Testmode = True
                    }
                    Module1.Core.CurrentLog = Log
                    Dim SQL As New MyDataConnector With {
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
                                Me.SourceSQL = SQL
                            End If
                        Case "MySQL"
                            If IsNothing(SQL.MySQLCon) = True Then
                                PB_Source.BackColor = Drawing.Color.Red
                            Else
                                PB_Source.BackColor = Drawing.Color.Green
                                Me.TestedSourceSetting.Worked = True
                                Me.SourceSQL = SQL
                            End If
                        Case "Access"
                            If IsNothing(SQL.AccessCon) = True Then
                                PB_Source.BackColor = Drawing.Color.Red
                            Else
                                PB_Source.BackColor = Drawing.Color.Green
                                Me.TestedSourceSetting.Worked = True
                                Me.SourceSQL = SQL
                            End If
                    End Select

                End If
            End If
        End If
    End Sub

    Private Sub VerifyDataTargetConnection()
        If Me.C_TargetServerType.Text <> "" And (Me.T_TargetServerAdress.Text <> "" Or Me.T_TargetPath.Text <> "") And Me.T_TargetDB.Text <> "" And Me.C_TargetConnectionType.Text <> "" Then
            Select Case C_TargetServerType.Text
                Case "XML"
                    Me.TestedTargetSetting = Nothing
                    PB_Target.BackColor = Drawing.Color.Transparent
                    Me.TargetSQL = Nothing
                Case "CSV"
                    Me.TestedTargetSetting = Nothing
                    PB_Target.BackColor = Drawing.Color.Transparent
                    Me.TargetSQL = Nothing
                Case "HTML"
                    Me.TestedTargetSetting = Nothing
                    PB_Target.BackColor = Drawing.Color.Transparent
                    Me.TargetSQL = Nothing
                Case Else
                    If Me.C_TargetConnectionType.Text = "Trusted" Then
                        Dim TargetSettings As New SQLServerSettings With {
                        .Direction = "target",
                        .Servertype = Me.C_TargetServerType.Text,
                        .Servername = Me.T_TargetServerAdress.Text,
                        .FilePath = Me.T_TargetPath.Text,
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
                        Dim SQL As New MyDataConnector With {
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
                            Me.TargetSQL = SQL
                        End If
                    Else
                        If Me.T_TargetUsername.Text <> "" And Me.T_TargetPassword.Text <> "" Then
                            Dim TargetSettings As New SQLServerSettings With {
                            .Servertype = Me.C_TargetServerType.Text,
                            .Servername = Me.T_TargetServerAdress.Text,
                            .FilePath = Me.T_TargetPath.Text,
                            .SQLDB = Me.T_TargetDB.Text,
                            .ConnMode = Me.C_TargetConnectionType.Text,
                            .User = Me.T_TargetUsername.Text,
                            .Password = Me.T_TargetPassword.Text
                            }

                            If IsNothing(Me.TestedTargetSetting) = False Then
                                If Me.TestedTargetSetting.Tested = True Then
                                    If Me.TestedTargetSetting.SQLDB = TargetSettings.SQLDB And Me.TestedTargetSetting.FilePath = TargetSettings.FilePath And Me.TestedTargetSetting.Servername = TargetSettings.Servername And Me.TestedTargetSetting.Servertype = TargetSettings.Servertype And Me.TestedTargetSetting.ConnMode = TargetSettings.ConnMode And Me.TestedTargetSetting.User = TargetSettings.User And Me.TestedTargetSetting.Password = TargetSettings.Password Then
                                        Exit Sub
                                    End If
                                End If
                            End If

                            Dim Log As New LOG With {
                                .Testmode = True
                            }
                            Module1.Core.CurrentLog = Log
                            Dim SQL As New MyDataConnector With {
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
                                        Me.TargetSQL = SQL
                                    End If
                                Case "MySQL"
                                    If IsNothing(SQL.MySQLCon) = True Then
                                        PB_Target.BackColor = Drawing.Color.Red
                                    Else
                                        PB_Target.BackColor = Drawing.Color.Green
                                        Me.TestedTargetSetting.Worked = True
                                        Me.TargetSQL = SQL
                                    End If
                                Case "Access"
                                    If IsNothing(SQL.AccessCon) = True Then
                                        PB_Target.BackColor = Drawing.Color.Red
                                    Else
                                        PB_Target.BackColor = Drawing.Color.Green
                                        Me.TestedTargetSetting.Worked = True
                                        Me.TargetSQL = SQL
                                    End If
                            End Select
                        End If
                    End If
            End Select

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

    Private Sub B_SourcePath_Click(sender As Object, e As EventArgs) Handles B_SourcePath.Click
        Select Case Me.C_SourceType.Text
            Case "Access"
                Me.OpenFileDialog1.Filter = "Access Databases |*.mdb; *.accdb"
            Case "XML"
                Me.OpenFileDialog1.Filter = "XML-Files | *.xml"
            Case "CSV"
                Me.OpenFileDialog1.Filter = "CSV-Files | *.csv"
        End Select

        Me.OpenFileDialog1.FileName = ""

        Me.OpenFileDialog1.ShowDialog()
        Me.T_SourcePath.Text = Me.OpenFileDialog1.FileName
    End Sub

    Private Sub B_TargetPath_Click(sender As Object, e As EventArgs) Handles B_TargetPath.Click
        Me.OpenFileDialog1.ShowDialog()
        Me.T_TargetPath.Text = Me.OpenFileDialog1.FileName
    End Sub

    Private Sub MappingGrid_CellContentClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles MappingGrid.CellContentClick

    End Sub

    Private Sub C_SourceIDDatatype_SelectedValueChanged(sender As Object, e As EventArgs) Handles C_SourceIDDatatype.SelectedValueChanged
        Me.C_SourceIDDatatype.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub C_TargetIDDatatype_SelectedValueChanged(sender As Object, e As EventArgs) Handles C_TargetIDDatatype.SelectedValueChanged
        Me.C_TargetIDDatatype.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub LoadTableSchemas()
        VerifyConnections()
        If Me.PB_Source.BackColor = Drawing.Color.Green Then
            If Me.C_SourceTable.Text <> "" Then
                SourceSQL.Setting.SQLTable = Me.C_SourceTable.Text
                Dim SourceColCombobox As System.Windows.Forms.DataGridViewComboBoxColumn = MappingGrid.Columns(0)
                SourceSQL.GetTableColumnNames()
                SourceColCombobox.Items.Clear()
                C_SourceIDColumn.Items.Clear()
                C_SourceFilterColumn.Items.Clear()
                For Each Column In SourceSQL.TableSchema.Columns
                    SourceColCombobox.Items.Add(Column.Name)
                    C_SourceIDColumn.Items.Add(Column.Name)
                    C_SourceFilterColumn.Items.Add(Column.Name)
                Next
            End If

        End If

        If Me.PB_Target.BackColor = Drawing.Color.Green Then
            If Me.C_TargetTable.Text <> "" Then
                TargetSQL.Setting.SQLTable = Me.C_TargetTable.Text
                Dim TargetColCombobox As System.Windows.Forms.DataGridViewComboBoxColumn = MappingGrid.Columns(1)
                TargetSQL.GetTableColumnNames()
                TargetColCombobox.Items.Clear()
                C_TargetIDColumn.Items.Clear()
                C_TargetTimestampfield.Items.Clear()
                For Each Column In TargetSQL.TableSchema.Columns
                    TargetColCombobox.Items.Add(Column.Name)
                    C_TargetIDColumn.Items.Add(Column.Name)
                    C_TargetTimestampfield.Items.Add(Column.Name)
                Next
            End If
        End If
    End Sub

    Private Sub C_TargetTable_Leave(sender As Object, e As EventArgs) Handles C_TargetTable.Leave
        LoadTableSchemas()
    End Sub

    Private Sub C_SourceTable_Leave(sender As Object, e As EventArgs) Handles C_SourceTable.Leave
        LoadTableSchemas()
    End Sub

    Private Function GetDataTypeForColumn(Columnname As String, Connector As MyDataConnector) As String
        If IsNothing(Connector) Then
        Else
            For Each Column In Connector.TableSchema.Columns
                If Column.Name = Columnname Then
                    GetDataTypeForColumn = Column.DataType
                    Exit Function
                End If
            Next
        End If
        GetDataTypeForColumn = ""
    End Function

    Private Sub C_SourceIDColumn_SelectedValueChanged(sender As Object, e As EventArgs) Handles C_SourceIDColumn.SelectedValueChanged
        Me.C_SourceIDDatatype.Text = GetDataTypeForColumn(Me.C_SourceIDColumn.Text, Me.SourceSQL)
    End Sub
    Private Sub MappingGrid_CellMouseClick(sender As Object, e As Windows.Forms.DataGridViewCellMouseEventArgs) Handles MappingGrid.CellMouseClick
        RefreshMappingGridDataTypes()
    End Sub

    Private Sub RefreshMappingGridDataTypes()
        Dim Rows As Integer = 0
        For Rows = 0 To MappingGrid.RowCount - 1
            If IsEmptyRowInGrid(Rows) = True Then
                If Rows < MappingGrid.RowCount - 1 Or Rows = 0 Then
                End If
            Else
                For Columns = 0 To MappingGrid.ColumnCount - 1
                    Select Case MappingGrid.Columns.Item(Columns).Name
                        Case "SourceColumn"
                            If IsNothing(MappingGrid.Item(Columns, Rows).Value) = True Then
                            Else
                                MappingGrid.Item(2, Rows).Value = GetDataTypeForColumn(MappingGrid(Columns, Rows).Value, Me.SourceSQL)
                            End If
                        Case "TargetColumn"
                            If IsNothing(MappingGrid.Item(Columns, Rows).Value) = True Then
                            Else
                                MappingGrid.Item(3, Rows).Value = GetDataTypeForColumn(MappingGrid(Columns, Rows).Value, Me.TargetSQL)
                            End If
                    End Select
                Next
            End If
        Next
    End Sub

    Private Sub C_TargetIDColumn_SelectedValueChanged(sender As Object, e As EventArgs) Handles C_TargetIDColumn.SelectedValueChanged
        Me.C_TargetIDDatatype.Text = GetDataTypeForColumn(Me.C_TargetIDColumn.Text, Me.TargetSQL)
    End Sub

    Private Sub T_OrderID_TextChanged(sender As Object, e As EventArgs) Handles T_OrderID.TextChanged
        Me.T_OrderID.BackColor = Drawing.SystemColors.Window
    End Sub

    Private Sub B_Load_Click(sender As Object, e As EventArgs) Handles B_Load.Click
        Dim StrFile As String = ""
        Dim OpenENV As ENV
        Dim XMLFile As New XMLFiles
        Dim FSO As New FilesystemOperations


        If Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            StrFile = Me.OpenFileDialog1.FileName
        Else
            Exit Sub

        End If
        'Me.OpenFileDialog1.OpenFile()



        If StrFile <> "" Then
            If FSO.FileExists(StrFile) = True Then
                OpenENV = XMLFile.ReadJobFile(StrFile)
            Else
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        If IsNothing(OpenENV) = False Then
            Me.T_Jobname.Text = OpenENV.GetName()
            Me.T_OrderID.Text = OpenENV.OrderID
            Me.C_CheckConsistency.Checked = OpenENV.ConsistenceCheck
            Me.C_IDlessBatch.Checked = OpenENV.IDLessBatch
            Me.MultipleIdentifier.Checked = OpenENV.HasMultipleIdentifiers

            Me.T_LoggingDirectory.Text = OpenENV.GetLogPath
            Me.C_Silent.Checked = OpenENV.LogSilent
            Me.C_DebugLog.Checked = OpenENV.LogLevel


            For Each Setting In OpenENV.SQLServer
                Select Case Setting.Direction.ToLower
                    Case "source"
                        Me.C_SourceType.Text = Setting.Servertype
                        Me.T_SourceAdress.Text = Setting.Servername
                        Me.T_SourcePath.Text = Setting.FilePath
                        Me.T_SourceDB.Text = Setting.SQLDB
                        Me.C_SourceConnMode.Text = Setting.ConnMode
                        Me.T_SourceUsername.Text = Setting.User
                        Me.T_SourcePassword.Text = Setting.Password
                        Me.C_SourceTable.Text = Setting.SQLTable
                        Me.C_SourceIDColumn.Text = Setting.IDColumn
                        Me.C_SourceIDDatatype.Text = Setting.IDColumnDataType
                        Me.C_SourceFilterType.Text = Setting.Filtertype
                        Me.T_SourceFilterColumn.Text = Setting.FilterColumn
                        Me.T_SourceSQLFilter.Text = Setting.SQLFilter

                    Case "target"
                        Me.C_TargetServerType.Text = Setting.Servertype
                        Me.T_TargetServerAdress.Text = Setting.Servername
                        Me.T_TargetPath.Text = Setting.FilePath
                        Me.T_TargetDB.Text = Setting.SQLDB
                        Me.C_TargetConnectionType.Text = Setting.ConnMode
                        Me.T_TargetUsername.Text = Setting.User
                        Me.T_TargetPassword.Text = Setting.Password
                        Me.C_TargetTable.Text = Setting.SQLTable
                        Me.C_TargetIDColumn.Text = Setting.IDColumn
                        Me.C_TargetIDDatatype.Text = Setting.IDColumnDataType
                        Me.C_MapIDValue.Checked = Setting.MapTargetIDColumnValue
                        Me.T_TargetSeperator.Text = Setting.StringSeperator
                        Me.C_TargetPartSubstring.Text = Setting.StringPart
                        Me.C_TargetTimestampfield.Text = Setting.SessionTimestampField
                        Me.C_InsertAllowed.Checked = Setting.InsertAllowed
                        Me.C_UpdateAllowed.Checked = Setting.UpdateAllowed
                        Me.C_DeleteAllowed.Checked = Setting.DeleteAllowed
                End Select
            Next


            LoadTableSchemas()
            If OpenENV.HasMultipleIdentifiers = True Then
                Me.IsIdentity.Visible = True
                Me.IsIdentity_offline.Visible = True
            End If
            Dim i As Integer = 0
            Me.MappingGrid.SelectAll()
            Me.MappingGrid.ClearSelection()
            Me.MappingGrid_Offline.SelectAll()
            Me.MappingGrid_Offline.ClearSelection()

            If Me.OnlineConfig = True Then
                Me.MappingGrid.Visible = True
                Me.MappingGrid_Offline.Visible = False
            Else
                Me.MappingGrid.Visible = False
                Me.MappingGrid_Offline.Visible = True
            End If

            For Each Mapping In OpenENV.Mappings
                Try
                    Me.MappingGrid.Rows.Add()
                        Me.MappingGrid.Rows(i).Cells("SourceColumn").Value = Mapping.Sourcename
                        Me.MappingGrid.Rows(i).Cells("TargetColumn").Value = Mapping.Targetname
                        Me.MappingGrid.Rows(i).Cells("SourceType").Value = Mapping.Sourcetype
                        Me.MappingGrid.Rows(i).Cells("TargetType").Value = Mapping.Targettype
                        Me.MappingGrid.Rows(i).Cells("Seperator").Value = Mapping.Separator
                        Me.MappingGrid.Rows(i).Cells("StaticValue").Value = Mapping.SeperatorDirection
                        Me.MappingGrid.Rows(i).Cells("StaticValue").Value = Mapping.StaticValue
                        Me.MappingGrid.Rows(i).Cells("IsIdentity").Value = Mapping.UseAsIdentifier

                    Me.MappingGrid_Offline.Rows.Add()
                        Me.MappingGrid_Offline.Rows(i).Cells("SourceColumn_offline").Value = Mapping.Sourcename
                        Me.MappingGrid_Offline.Rows(i).Cells("TargetColumn_offline").Value = Mapping.Targetname
                        Me.MappingGrid_Offline.Rows(i).Cells("SourceType_Offline").Value = Mapping.Sourcetype
                        Me.MappingGrid_Offline.Rows(i).Cells("TargetType_Offline").Value = Mapping.Targettype
                        Me.MappingGrid_Offline.Rows(i).Cells("Seperator_Offline").Value = Mapping.Separator
                        Me.MappingGrid_Offline.Rows(i).Cells("StaticValue_Offline").Value = Mapping.SeperatorDirection
                        Me.MappingGrid_Offline.Rows(i).Cells("StaticValue_Offline").Value = Mapping.StaticValue
                        Me.MappingGrid_Offline.Rows(i).Cells("IsIdentity_Offline").Value = Mapping.UseAsIdentifier



                Catch ex As Exception
                    System.Console.WriteLine(ex.Message)
                End Try
                i = i + 1
            Next
            Me.MappingGrid.Refresh()
            Me.MappingGrid_Offline.Refresh()
            Me.Refresh()
        End If
    End Sub

    Private Sub MappingGrid_DataError(sender As Object, e As Windows.Forms.DataGridViewDataErrorEventArgs) Handles MappingGrid.DataError
        System.Console.WriteLine("Error while Loading while from Job file: Could not validate. Is the connection ok?")
    End Sub

    Private Sub C_IDlessBatch_CheckedChanged(sender As Object, e As EventArgs) Handles C_IDlessBatch.CheckedChanged
        If Me.C_IDlessBatch.Checked = True Then
            Me.C_SourceIDColumn.Visible = False
            Me.C_SourceIDDatatype.Visible = False
            Me.C_TargetIDColumn.Visible = False
            Me.C_TargetIDDatatype.Visible = False
            Me.L_SourceIDColumn.Visible = False
            Me.L_SourceIDDataType.Visible = False
            Me.L_TargetIDColumn.Visible = False
            Me.L_TargetIDDatatype.Visible = False
            Me.C_MapIDValue.Visible = False
            Me.L_TargetSeperator.Visible = False
            Me.T_TargetSeperator.Visible = False
            Me.L_PartOfSubstringUse.Visible = False
            Me.C_TargetPartSubstring.Visible = False
            Me.L_PartOfSubString.Visible = False
            Me.C_UpdateAllowed.Visible = False
            Me.C_DeleteAllowed.Visible = False
            Me.C_InsertAllowed.Checked = True

        Else
            Me.C_SourceIDColumn.Visible = True
            Me.C_SourceIDDatatype.Visible = True
            Me.C_TargetIDColumn.Visible = True
            Me.C_TargetIDDatatype.Visible = True
            Me.C_TargetIDDatatype.Visible = True
            Me.L_SourceIDColumn.Visible = True
            Me.L_SourceIDDataType.Visible = True
            Me.L_TargetIDColumn.Visible = True
            Me.L_TargetIDDatatype.Visible = True
            Me.C_MapIDValue.Visible = True
            Me.L_TargetSeperator.Visible = True
            Me.T_TargetSeperator.Visible = True
            Me.L_PartOfSubstringUse.Visible = True
            Me.C_TargetPartSubstring.Visible = True
            Me.L_PartOfSubString.Visible = True
            Me.C_UpdateAllowed.Visible = True
            Me.C_DeleteAllowed.Visible = True
            Me.C_InsertAllowed.Checked = False
        End If
    End Sub

    Private Sub B_TargetSaveFile_Click(sender As Object, e As EventArgs) Handles B_TargetSaveFile.Click
        Select Case Me.C_TargetServerType.Text
            Case "CSV"
                SaveFileDialog1.DefaultExt = ".csv"
                SaveFileDialog1.AddExtension = True
                SaveFileDialog1.Filter = "CSV-Files (*.csv)|*.csv"
            Case "XML"
                SaveFileDialog1.DefaultExt = ".xml"
                SaveFileDialog1.AddExtension = True
                SaveFileDialog1.Filter = "XML-Files (*.xml)|*.xml"
        End Select
        SaveFileDialog1.ShowDialog()
        Me.T_TargetPath.Text = SaveFileDialog1.FileName
    End Sub

    Private Sub MultipleIdentifier_CheckedChanged(sender As Object, e As EventArgs) Handles MultipleIdentifier.CheckedChanged
        If Me.MultipleIdentifier.Checked = True Then
            Me.C_IDlessBatch.Visible = False
            Me.IsIdentity.Visible = True
            Me.IsIdentity_offline.Visible = True
            Me.C_SourceIDColumn.Visible = False
            Me.C_SourceIDDatatype.Visible = False
            Me.C_TargetIDColumn.Visible = False
            Me.C_TargetIDDatatype.Visible = False
            Me.C_MapIDValue.Visible = False
            Me.T_TargetSeperator.Visible = False
            Me.C_TargetPartSubstring.Visible = False

            Me.L_SourceIDColumn.Visible = False
            Me.L_SourceIDDataType.Visible = False
            Me.L_TargetIDColumn.Visible = False
            Me.L_TargetIDDatatype.Visible = False
            Me.L_TargetSeperator.Visible = False
            Me.L_PartOfSubstringUse.Visible = False
            Me.L_PartOfSubString.Visible = False


        Else
            Me.C_IDlessBatch.Visible = True
            Me.IsIdentity.Visible = False
            Me.IsIdentity_offline.Visible = False
            Me.C_SourceIDColumn.Visible = True
            Me.C_SourceIDDatatype.Visible = True
            Me.C_TargetIDColumn.Visible = True
            Me.C_TargetIDDatatype.Visible = True
            Me.C_MapIDValue.Visible = True
            Me.T_TargetSeperator.Visible = True
            Me.C_TargetPartSubstring.Visible = True

            Me.L_SourceIDColumn.Visible = True
            Me.L_SourceIDDataType.Visible = True
            Me.L_TargetIDColumn.Visible = True
            Me.L_TargetIDDatatype.Visible = True
            Me.L_TargetSeperator.Visible = True
            Me.L_PartOfSubstringUse.Visible = True
            Me.L_PartOfSubString.Visible = True
        End If
    End Sub


End Class