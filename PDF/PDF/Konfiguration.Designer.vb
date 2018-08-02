<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Konfiguration
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.T_Jobname = New System.Windows.Forms.TextBox()
        Me.L_Jobname = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.T_LoggingDirectory = New System.Windows.Forms.TextBox()
        Me.L_LoggingDirectory = New System.Windows.Forms.Label()
        Me.B_LoggingDirectory = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.PB_Source = New System.Windows.Forms.PictureBox()
        Me.L_SQLFilterStatement = New System.Windows.Forms.Label()
        Me.L_FilterValue = New System.Windows.Forms.Label()
        Me.L_FilterColumn = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.L_SourceConnectionType = New System.Windows.Forms.Label()
        Me.L_SourcePassword = New System.Windows.Forms.Label()
        Me.L_SourceUsername = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.T_SourceSQLFilter = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.T_SourceFilterValue = New System.Windows.Forms.TextBox()
        Me.T_SourceFilterColumn = New System.Windows.Forms.TextBox()
        Me.C_SourceFilterType = New System.Windows.Forms.ComboBox()
        Me.T_SourceIDColumn = New System.Windows.Forms.TextBox()
        Me.T_SourceTable = New System.Windows.Forms.TextBox()
        Me.C_SourceConnMode = New System.Windows.Forms.ComboBox()
        Me.T_SourcePassword = New System.Windows.Forms.TextBox()
        Me.T_SourceUsername = New System.Windows.Forms.TextBox()
        Me.T_SourceDB = New System.Windows.Forms.TextBox()
        Me.C_SourceType = New System.Windows.Forms.ComboBox()
        Me.T_SourceAdress = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.PB_Target = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.C_DeleteAllowed = New System.Windows.Forms.CheckBox()
        Me.C_UpdateAllowed = New System.Windows.Forms.CheckBox()
        Me.C_InsertAllowed = New System.Windows.Forms.CheckBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.C_TargetPartSubstring = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.T_TargetSeperator = New System.Windows.Forms.TextBox()
        Me.C_MapIDValue = New System.Windows.Forms.CheckBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.T_TargetTimestampfield = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.T_TargetIDColumn = New System.Windows.Forms.TextBox()
        Me.C_TargetServerType = New System.Windows.Forms.ComboBox()
        Me.L_TargetConnectionType = New System.Windows.Forms.Label()
        Me.T_TargetServerAdress = New System.Windows.Forms.TextBox()
        Me.L_TargetPassword = New System.Windows.Forms.Label()
        Me.T_TargetDB = New System.Windows.Forms.TextBox()
        Me.L_TargetUsername = New System.Windows.Forms.Label()
        Me.T_TargetUsername = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.T_TargetPassword = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.C_TargetConnectionType = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.T_TargetTable = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.MappingGrid = New System.Windows.Forms.DataGridView()
        Me.SourceColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TargetColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SourceType = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.TargetType = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Seperator = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PartOfSubstring = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.B_Save = New System.Windows.Forms.Button()
        Me.C_DebugLog = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PB_Source, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PB_Target, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MappingGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'T_Jobname
        '
        Me.T_Jobname.Location = New System.Drawing.Point(72, 10)
        Me.T_Jobname.Name = "T_Jobname"
        Me.T_Jobname.Size = New System.Drawing.Size(396, 22)
        Me.T_Jobname.TabIndex = 0
        '
        'L_Jobname
        '
        Me.L_Jobname.AutoSize = True
        Me.L_Jobname.Location = New System.Drawing.Point(13, 13)
        Me.L_Jobname.Name = "L_Jobname"
        Me.L_Jobname.Size = New System.Drawing.Size(56, 13)
        Me.L_Jobname.TabIndex = 1
        Me.L_Jobname.Text = "Jobname:"
        '
        'T_LoggingDirectory
        '
        Me.T_LoggingDirectory.Location = New System.Drawing.Point(587, 10)
        Me.T_LoggingDirectory.Name = "T_LoggingDirectory"
        Me.T_LoggingDirectory.Size = New System.Drawing.Size(501, 22)
        Me.T_LoggingDirectory.TabIndex = 1
        '
        'L_LoggingDirectory
        '
        Me.L_LoggingDirectory.AutoSize = True
        Me.L_LoggingDirectory.Location = New System.Drawing.Point(482, 13)
        Me.L_LoggingDirectory.Name = "L_LoggingDirectory"
        Me.L_LoggingDirectory.Size = New System.Drawing.Size(102, 13)
        Me.L_LoggingDirectory.TabIndex = 3
        Me.L_LoggingDirectory.Text = "Logging Directory:"
        '
        'B_LoggingDirectory
        '
        Me.B_LoggingDirectory.Location = New System.Drawing.Point(1094, 10)
        Me.B_LoggingDirectory.Name = "B_LoggingDirectory"
        Me.B_LoggingDirectory.Size = New System.Drawing.Size(50, 23)
        Me.B_LoggingDirectory.TabIndex = 2
        Me.B_LoggingDirectory.Text = "..."
        Me.B_LoggingDirectory.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PB_Source)
        Me.GroupBox1.Controls.Add(Me.L_SQLFilterStatement)
        Me.GroupBox1.Controls.Add(Me.L_FilterValue)
        Me.GroupBox1.Controls.Add(Me.L_FilterColumn)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.L_SourceConnectionType)
        Me.GroupBox1.Controls.Add(Me.L_SourcePassword)
        Me.GroupBox1.Controls.Add(Me.L_SourceUsername)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.T_SourceSQLFilter)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.T_SourceFilterValue)
        Me.GroupBox1.Controls.Add(Me.T_SourceFilterColumn)
        Me.GroupBox1.Controls.Add(Me.C_SourceFilterType)
        Me.GroupBox1.Controls.Add(Me.T_SourceIDColumn)
        Me.GroupBox1.Controls.Add(Me.T_SourceTable)
        Me.GroupBox1.Controls.Add(Me.C_SourceConnMode)
        Me.GroupBox1.Controls.Add(Me.T_SourcePassword)
        Me.GroupBox1.Controls.Add(Me.T_SourceUsername)
        Me.GroupBox1.Controls.Add(Me.T_SourceDB)
        Me.GroupBox1.Controls.Add(Me.C_SourceType)
        Me.GroupBox1.Controls.Add(Me.T_SourceAdress)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 65)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(590, 359)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Source"
        '
        'PB_Source
        '
        Me.PB_Source.InitialImage = Nothing
        Me.PB_Source.Location = New System.Drawing.Point(568, 18)
        Me.PB_Source.Name = "PB_Source"
        Me.PB_Source.Size = New System.Drawing.Size(16, 88)
        Me.PB_Source.TabIndex = 23
        Me.PB_Source.TabStop = False
        '
        'L_SQLFilterStatement
        '
        Me.L_SQLFilterStatement.AutoSize = True
        Me.L_SQLFilterStatement.Location = New System.Drawing.Point(9, 228)
        Me.L_SQLFilterStatement.Name = "L_SQLFilterStatement"
        Me.L_SQLFilterStatement.Size = New System.Drawing.Size(113, 13)
        Me.L_SQLFilterStatement.TabIndex = 22
        Me.L_SQLFilterStatement.Text = "SQL Filter Statement:"
        Me.L_SQLFilterStatement.Visible = False
        '
        'L_FilterValue
        '
        Me.L_FilterValue.AutoSize = True
        Me.L_FilterValue.Location = New System.Drawing.Point(9, 254)
        Me.L_FilterValue.Name = "L_FilterValue"
        Me.L_FilterValue.Size = New System.Drawing.Size(67, 13)
        Me.L_FilterValue.TabIndex = 21
        Me.L_FilterValue.Text = "Filter Value:"
        Me.L_FilterValue.Visible = False
        '
        'L_FilterColumn
        '
        Me.L_FilterColumn.AutoSize = True
        Me.L_FilterColumn.Location = New System.Drawing.Point(9, 228)
        Me.L_FilterColumn.Name = "L_FilterColumn"
        Me.L_FilterColumn.Size = New System.Drawing.Size(79, 13)
        Me.L_FilterColumn.TabIndex = 20
        Me.L_FilterColumn.Text = "Filter Column:"
        Me.L_FilterColumn.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 201)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 13)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Filter Type:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 152)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 13)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "ID Column:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 126)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Table:"
        '
        'L_SourceConnectionType
        '
        Me.L_SourceConnectionType.AutoSize = True
        Me.L_SourceConnectionType.Location = New System.Drawing.Point(277, 49)
        Me.L_SourceConnectionType.Name = "L_SourceConnectionType"
        Me.L_SourceConnectionType.Size = New System.Drawing.Size(95, 13)
        Me.L_SourceConnectionType.TabIndex = 16
        Me.L_SourceConnectionType.Text = "Connection Type:"
        Me.L_SourceConnectionType.Visible = False
        '
        'L_SourcePassword
        '
        Me.L_SourcePassword.AutoSize = True
        Me.L_SourcePassword.Location = New System.Drawing.Point(288, 87)
        Me.L_SourcePassword.Name = "L_SourcePassword"
        Me.L_SourcePassword.Size = New System.Drawing.Size(59, 13)
        Me.L_SourcePassword.TabIndex = 15
        Me.L_SourcePassword.Text = "Password:"
        Me.L_SourcePassword.Visible = False
        '
        'L_SourceUsername
        '
        Me.L_SourceUsername.AutoSize = True
        Me.L_SourceUsername.Location = New System.Drawing.Point(9, 87)
        Me.L_SourceUsername.Name = "L_SourceUsername"
        Me.L_SourceUsername.Size = New System.Drawing.Size(61, 13)
        Me.L_SourceUsername.TabIndex = 14
        Me.L_SourceUsername.Text = "Username:"
        Me.L_SourceUsername.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Database:"
        '
        'T_SourceSQLFilter
        '
        Me.T_SourceSQLFilter.Location = New System.Drawing.Point(12, 244)
        Me.T_SourceSQLFilter.Multiline = True
        Me.T_SourceSQLFilter.Name = "T_SourceSQLFilter"
        Me.T_SourceSQLFilter.Size = New System.Drawing.Size(553, 109)
        Me.T_SourceSQLFilter.TabIndex = 14
        Me.T_SourceSQLFilter.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Server Type:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(277, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Server Adress:"
        '
        'T_SourceFilterValue
        '
        Me.T_SourceFilterValue.Location = New System.Drawing.Point(80, 251)
        Me.T_SourceFilterValue.Name = "T_SourceFilterValue"
        Me.T_SourceFilterValue.Size = New System.Drawing.Size(206, 22)
        Me.T_SourceFilterValue.TabIndex = 14
        Me.T_SourceFilterValue.Visible = False
        '
        'T_SourceFilterColumn
        '
        Me.T_SourceFilterColumn.Location = New System.Drawing.Point(80, 225)
        Me.T_SourceFilterColumn.Name = "T_SourceFilterColumn"
        Me.T_SourceFilterColumn.Size = New System.Drawing.Size(206, 22)
        Me.T_SourceFilterColumn.TabIndex = 13
        Me.T_SourceFilterColumn.Visible = False
        '
        'C_SourceFilterType
        '
        Me.C_SourceFilterType.FormattingEnabled = True
        Me.C_SourceFilterType.Items.AddRange(New Object() {"none", "one column match", "SQL Filter"})
        Me.C_SourceFilterType.Location = New System.Drawing.Point(80, 198)
        Me.C_SourceFilterType.Name = "C_SourceFilterType"
        Me.C_SourceFilterType.Size = New System.Drawing.Size(206, 21)
        Me.C_SourceFilterType.TabIndex = 12
        '
        'T_SourceIDColumn
        '
        Me.T_SourceIDColumn.Location = New System.Drawing.Point(80, 149)
        Me.T_SourceIDColumn.Name = "T_SourceIDColumn"
        Me.T_SourceIDColumn.Size = New System.Drawing.Size(485, 22)
        Me.T_SourceIDColumn.TabIndex = 11
        '
        'T_SourceTable
        '
        Me.T_SourceTable.Location = New System.Drawing.Point(80, 123)
        Me.T_SourceTable.Name = "T_SourceTable"
        Me.T_SourceTable.Size = New System.Drawing.Size(485, 22)
        Me.T_SourceTable.TabIndex = 10
        '
        'C_SourceConnMode
        '
        Me.C_SourceConnMode.FormattingEnabled = True
        Me.C_SourceConnMode.Location = New System.Drawing.Point(374, 45)
        Me.C_SourceConnMode.Name = "C_SourceConnMode"
        Me.C_SourceConnMode.Size = New System.Drawing.Size(191, 21)
        Me.C_SourceConnMode.TabIndex = 7
        Me.C_SourceConnMode.Visible = False
        '
        'T_SourcePassword
        '
        Me.T_SourcePassword.Location = New System.Drawing.Point(359, 84)
        Me.T_SourcePassword.Name = "T_SourcePassword"
        Me.T_SourcePassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.T_SourcePassword.Size = New System.Drawing.Size(206, 22)
        Me.T_SourcePassword.TabIndex = 9
        Me.T_SourcePassword.Visible = False
        '
        'T_SourceUsername
        '
        Me.T_SourceUsername.Location = New System.Drawing.Point(80, 84)
        Me.T_SourceUsername.Name = "T_SourceUsername"
        Me.T_SourceUsername.Size = New System.Drawing.Size(191, 22)
        Me.T_SourceUsername.TabIndex = 8
        Me.T_SourceUsername.Visible = False
        '
        'T_SourceDB
        '
        Me.T_SourceDB.Location = New System.Drawing.Point(80, 46)
        Me.T_SourceDB.Name = "T_SourceDB"
        Me.T_SourceDB.Size = New System.Drawing.Size(191, 22)
        Me.T_SourceDB.TabIndex = 6
        '
        'C_SourceType
        '
        Me.C_SourceType.FormattingEnabled = True
        Me.C_SourceType.Items.AddRange(New Object() {"MS-SQL", "MySQL"})
        Me.C_SourceType.Location = New System.Drawing.Point(80, 19)
        Me.C_SourceType.Name = "C_SourceType"
        Me.C_SourceType.Size = New System.Drawing.Size(191, 21)
        Me.C_SourceType.TabIndex = 4
        '
        'T_SourceAdress
        '
        Me.T_SourceAdress.Location = New System.Drawing.Point(359, 19)
        Me.T_SourceAdress.Name = "T_SourceAdress"
        Me.T_SourceAdress.Size = New System.Drawing.Size(206, 22)
        Me.T_SourceAdress.TabIndex = 5
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.PB_Target)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.C_DeleteAllowed)
        Me.GroupBox2.Controls.Add(Me.C_UpdateAllowed)
        Me.GroupBox2.Controls.Add(Me.C_InsertAllowed)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Controls.Add(Me.C_TargetPartSubstring)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Controls.Add(Me.T_TargetSeperator)
        Me.GroupBox2.Controls.Add(Me.C_MapIDValue)
        Me.GroupBox2.Controls.Add(Me.Label21)
        Me.GroupBox2.Controls.Add(Me.T_TargetTimestampfield)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.T_TargetIDColumn)
        Me.GroupBox2.Controls.Add(Me.C_TargetServerType)
        Me.GroupBox2.Controls.Add(Me.L_TargetConnectionType)
        Me.GroupBox2.Controls.Add(Me.T_TargetServerAdress)
        Me.GroupBox2.Controls.Add(Me.L_TargetPassword)
        Me.GroupBox2.Controls.Add(Me.T_TargetDB)
        Me.GroupBox2.Controls.Add(Me.L_TargetUsername)
        Me.GroupBox2.Controls.Add(Me.T_TargetUsername)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.T_TargetPassword)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.C_TargetConnectionType)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.T_TargetTable)
        Me.GroupBox2.Location = New System.Drawing.Point(612, 65)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(590, 359)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Target"
        '
        'PB_Target
        '
        Me.PB_Target.Location = New System.Drawing.Point(568, 18)
        Me.PB_Target.Name = "PB_Target"
        Me.PB_Target.Size = New System.Drawing.Size(16, 88)
        Me.PB_Target.TabIndex = 24
        Me.PB_Target.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 320)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(0, 13)
        Me.Label4.TabIndex = 45
        '
        'C_DeleteAllowed
        '
        Me.C_DeleteAllowed.AutoSize = True
        Me.C_DeleteAllowed.Location = New System.Drawing.Point(461, 269)
        Me.C_DeleteAllowed.Name = "C_DeleteAllowed"
        Me.C_DeleteAllowed.Size = New System.Drawing.Size(106, 17)
        Me.C_DeleteAllowed.TabIndex = 29
        Me.C_DeleteAllowed.Text = "DELETE allowed"
        Me.C_DeleteAllowed.UseVisualStyleBackColor = True
        '
        'C_UpdateAllowed
        '
        Me.C_UpdateAllowed.AutoSize = True
        Me.C_UpdateAllowed.Location = New System.Drawing.Point(260, 269)
        Me.C_UpdateAllowed.Name = "C_UpdateAllowed"
        Me.C_UpdateAllowed.Size = New System.Drawing.Size(109, 17)
        Me.C_UpdateAllowed.TabIndex = 28
        Me.C_UpdateAllowed.Text = "UPDATE allowed"
        Me.C_UpdateAllowed.UseVisualStyleBackColor = True
        '
        'C_InsertAllowed
        '
        Me.C_InsertAllowed.AutoSize = True
        Me.C_InsertAllowed.Location = New System.Drawing.Point(81, 266)
        Me.C_InsertAllowed.Name = "C_InsertAllowed"
        Me.C_InsertAllowed.Size = New System.Drawing.Size(105, 17)
        Me.C_InsertAllowed.TabIndex = 27
        Me.C_InsertAllowed.Text = "INSERT allowed"
        Me.C_InsertAllowed.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(507, 178)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(76, 26)
        Me.Label24.TabIndex = 44
        Me.Label24.Text = "...part of " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "the substring"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(310, 181)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(26, 13)
        Me.Label23.TabIndex = 43
        Me.Label23.Text = "Use"
        '
        'C_TargetPartSubstring
        '
        Me.C_TargetPartSubstring.FormattingEnabled = True
        Me.C_TargetPartSubstring.Items.AddRange(New Object() {"left", "right"})
        Me.C_TargetPartSubstring.Location = New System.Drawing.Point(342, 178)
        Me.C_TargetPartSubstring.Name = "C_TargetPartSubstring"
        Me.C_TargetPartSubstring.Size = New System.Drawing.Size(159, 21)
        Me.C_TargetPartSubstring.TabIndex = 25
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(174, 181)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(60, 13)
        Me.Label22.TabIndex = 41
        Me.Label22.Text = "Seperator:"
        '
        'T_TargetSeperator
        '
        Me.T_TargetSeperator.Location = New System.Drawing.Point(236, 178)
        Me.T_TargetSeperator.Name = "T_TargetSeperator"
        Me.T_TargetSeperator.Size = New System.Drawing.Size(68, 22)
        Me.T_TargetSeperator.TabIndex = 24
        '
        'C_MapIDValue
        '
        Me.C_MapIDValue.AutoSize = True
        Me.C_MapIDValue.Location = New System.Drawing.Point(81, 180)
        Me.C_MapIDValue.Name = "C_MapIDValue"
        Me.C_MapIDValue.Size = New System.Drawing.Size(94, 17)
        Me.C_MapIDValue.TabIndex = 23
        Me.C_MapIDValue.Text = "Map ID Value"
        Me.C_MapIDValue.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(10, 231)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(96, 13)
        Me.Label21.TabIndex = 38
        Me.Label21.Text = "Time Stamp Field:"
        '
        'T_TargetTimestampfield
        '
        Me.T_TargetTimestampfield.Location = New System.Drawing.Point(107, 228)
        Me.T_TargetTimestampfield.Name = "T_TargetTimestampfield"
        Me.T_TargetTimestampfield.Size = New System.Drawing.Size(459, 22)
        Me.T_TargetTimestampfield.TabIndex = 26
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(10, 152)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(64, 13)
        Me.Label20.TabIndex = 24
        Me.Label20.Text = "ID Column:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(10, 125)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(36, 13)
        Me.Label13.TabIndex = 36
        Me.Label13.Text = "Table:"
        '
        'T_TargetIDColumn
        '
        Me.T_TargetIDColumn.Location = New System.Drawing.Point(81, 149)
        Me.T_TargetIDColumn.Name = "T_TargetIDColumn"
        Me.T_TargetIDColumn.Size = New System.Drawing.Size(485, 22)
        Me.T_TargetIDColumn.TabIndex = 22
        '
        'C_TargetServerType
        '
        Me.C_TargetServerType.FormattingEnabled = True
        Me.C_TargetServerType.Items.AddRange(New Object() {"MS-SQL", "MySQL"})
        Me.C_TargetServerType.Location = New System.Drawing.Point(81, 18)
        Me.C_TargetServerType.Name = "C_TargetServerType"
        Me.C_TargetServerType.Size = New System.Drawing.Size(191, 21)
        Me.C_TargetServerType.TabIndex = 15
        '
        'L_TargetConnectionType
        '
        Me.L_TargetConnectionType.AutoSize = True
        Me.L_TargetConnectionType.Location = New System.Drawing.Point(278, 48)
        Me.L_TargetConnectionType.Name = "L_TargetConnectionType"
        Me.L_TargetConnectionType.Size = New System.Drawing.Size(95, 13)
        Me.L_TargetConnectionType.TabIndex = 35
        Me.L_TargetConnectionType.Text = "Connection Type:"
        Me.L_TargetConnectionType.Visible = False
        '
        'T_TargetServerAdress
        '
        Me.T_TargetServerAdress.Location = New System.Drawing.Point(360, 18)
        Me.T_TargetServerAdress.Name = "T_TargetServerAdress"
        Me.T_TargetServerAdress.Size = New System.Drawing.Size(206, 22)
        Me.T_TargetServerAdress.TabIndex = 16
        '
        'L_TargetPassword
        '
        Me.L_TargetPassword.AutoSize = True
        Me.L_TargetPassword.Location = New System.Drawing.Point(289, 86)
        Me.L_TargetPassword.Name = "L_TargetPassword"
        Me.L_TargetPassword.Size = New System.Drawing.Size(59, 13)
        Me.L_TargetPassword.TabIndex = 34
        Me.L_TargetPassword.Text = "Password:"
        Me.L_TargetPassword.Visible = False
        '
        'T_TargetDB
        '
        Me.T_TargetDB.Location = New System.Drawing.Point(81, 45)
        Me.T_TargetDB.Name = "T_TargetDB"
        Me.T_TargetDB.Size = New System.Drawing.Size(191, 22)
        Me.T_TargetDB.TabIndex = 17
        '
        'L_TargetUsername
        '
        Me.L_TargetUsername.AutoSize = True
        Me.L_TargetUsername.Location = New System.Drawing.Point(10, 86)
        Me.L_TargetUsername.Name = "L_TargetUsername"
        Me.L_TargetUsername.Size = New System.Drawing.Size(61, 13)
        Me.L_TargetUsername.TabIndex = 33
        Me.L_TargetUsername.Text = "Username:"
        Me.L_TargetUsername.Visible = False
        '
        'T_TargetUsername
        '
        Me.T_TargetUsername.Location = New System.Drawing.Point(81, 83)
        Me.T_TargetUsername.Name = "T_TargetUsername"
        Me.T_TargetUsername.Size = New System.Drawing.Size(191, 22)
        Me.T_TargetUsername.TabIndex = 19
        Me.T_TargetUsername.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(10, 48)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(58, 13)
        Me.Label17.TabIndex = 32
        Me.Label17.Text = "Database:"
        '
        'T_TargetPassword
        '
        Me.T_TargetPassword.Location = New System.Drawing.Point(360, 83)
        Me.T_TargetPassword.Name = "T_TargetPassword"
        Me.T_TargetPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.T_TargetPassword.Size = New System.Drawing.Size(206, 22)
        Me.T_TargetPassword.TabIndex = 20
        Me.T_TargetPassword.Visible = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(10, 21)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(66, 13)
        Me.Label18.TabIndex = 31
        Me.Label18.Text = "Server Type:"
        '
        'C_TargetConnectionType
        '
        Me.C_TargetConnectionType.FormattingEnabled = True
        Me.C_TargetConnectionType.Location = New System.Drawing.Point(375, 44)
        Me.C_TargetConnectionType.Name = "C_TargetConnectionType"
        Me.C_TargetConnectionType.Size = New System.Drawing.Size(191, 21)
        Me.C_TargetConnectionType.TabIndex = 18
        Me.C_TargetConnectionType.Visible = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(278, 21)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(78, 13)
        Me.Label19.TabIndex = 30
        Me.Label19.Text = "Server Adress:"
        '
        'T_TargetTable
        '
        Me.T_TargetTable.Location = New System.Drawing.Point(81, 122)
        Me.T_TargetTable.Name = "T_TargetTable"
        Me.T_TargetTable.Size = New System.Drawing.Size(485, 22)
        Me.T_TargetTable.TabIndex = 21
        '
        'MappingGrid
        '
        Me.MappingGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MappingGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SourceColumn, Me.TargetColumn, Me.SourceType, Me.TargetType, Me.Seperator, Me.PartOfSubstring})
        Me.MappingGrid.Location = New System.Drawing.Point(16, 430)
        Me.MappingGrid.Name = "MappingGrid"
        Me.MappingGrid.Size = New System.Drawing.Size(1185, 156)
        Me.MappingGrid.TabIndex = 30
        '
        'SourceColumn
        '
        Me.SourceColumn.HeaderText = "Source Column"
        Me.SourceColumn.Name = "SourceColumn"
        '
        'TargetColumn
        '
        Me.TargetColumn.HeaderText = "Target Column"
        Me.TargetColumn.Name = "TargetColumn"
        '
        'SourceType
        '
        Me.SourceType.HeaderText = "Source Type"
        Me.SourceType.Items.AddRange(New Object() {"string", "int", "datetime", "timestamp"})
        Me.SourceType.Name = "SourceType"
        '
        'TargetType
        '
        Me.TargetType.HeaderText = "Target Type"
        Me.TargetType.Items.AddRange(New Object() {"string", "int", "datetime", "timestamp"})
        Me.TargetType.Name = "TargetType"
        '
        'Seperator
        '
        Me.Seperator.HeaderText = "Seperator"
        Me.Seperator.Name = "Seperator"
        '
        'PartOfSubstring
        '
        Me.PartOfSubstring.HeaderText = "Part Of Substring"
        Me.PartOfSubstring.Items.AddRange(New Object() {"left", "right"})
        Me.PartOfSubstring.Name = "PartOfSubstring"
        '
        'B_Save
        '
        Me.B_Save.Location = New System.Drawing.Point(1007, 592)
        Me.B_Save.Name = "B_Save"
        Me.B_Save.Size = New System.Drawing.Size(194, 31)
        Me.B_Save.TabIndex = 45
        Me.B_Save.Text = "Save Configuration"
        Me.B_Save.UseVisualStyleBackColor = True
        '
        'C_DebugLog
        '
        Me.C_DebugLog.AutoSize = True
        Me.C_DebugLog.Location = New System.Drawing.Point(587, 36)
        Me.C_DebugLog.Name = "C_DebugLog"
        Me.C_DebugLog.Size = New System.Drawing.Size(145, 17)
        Me.C_DebugLog.TabIndex = 3
        Me.C_DebugLog.Text = "Enable Debug Logging"
        Me.C_DebugLog.UseVisualStyleBackColor = True
        '
        'Konfiguration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1219, 631)
        Me.Controls.Add(Me.C_DebugLog)
        Me.Controls.Add(Me.B_Save)
        Me.Controls.Add(Me.MappingGrid)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.B_LoggingDirectory)
        Me.Controls.Add(Me.L_LoggingDirectory)
        Me.Controls.Add(Me.T_LoggingDirectory)
        Me.Controls.Add(Me.L_Jobname)
        Me.Controls.Add(Me.T_Jobname)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Konfiguration"
        Me.Text = "Configuration"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PB_Source, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PB_Target, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MappingGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents T_Jobname As Windows.Forms.TextBox
    Friend WithEvents L_Jobname As Windows.Forms.Label
    Friend WithEvents SaveFileDialog1 As Windows.Forms.SaveFileDialog
    Friend WithEvents T_LoggingDirectory As Windows.Forms.TextBox
    Friend WithEvents L_LoggingDirectory As Windows.Forms.Label
    Friend WithEvents B_LoggingDirectory As Windows.Forms.Button
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents T_SourceSQLFilter As Windows.Forms.TextBox
    Friend WithEvents T_SourceFilterValue As Windows.Forms.TextBox
    Friend WithEvents T_SourceFilterColumn As Windows.Forms.TextBox
    Friend WithEvents C_SourceFilterType As Windows.Forms.ComboBox
    Friend WithEvents T_SourceIDColumn As Windows.Forms.TextBox
    Friend WithEvents T_SourceTable As Windows.Forms.TextBox
    Friend WithEvents C_SourceConnMode As Windows.Forms.ComboBox
    Friend WithEvents T_SourcePassword As Windows.Forms.TextBox
    Friend WithEvents T_SourceUsername As Windows.Forms.TextBox
    Friend WithEvents T_SourceDB As Windows.Forms.TextBox
    Friend WithEvents C_SourceType As Windows.Forms.ComboBox
    Friend WithEvents T_SourceAdress As Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents L_SQLFilterStatement As Windows.Forms.Label
    Friend WithEvents L_FilterValue As Windows.Forms.Label
    Friend WithEvents L_FilterColumn As Windows.Forms.Label
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents L_SourceConnectionType As Windows.Forms.Label
    Friend WithEvents L_SourcePassword As Windows.Forms.Label
    Friend WithEvents L_SourceUsername As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label23 As Windows.Forms.Label
    Friend WithEvents C_TargetPartSubstring As Windows.Forms.ComboBox
    Friend WithEvents Label22 As Windows.Forms.Label
    Friend WithEvents T_TargetSeperator As Windows.Forms.TextBox
    Friend WithEvents C_MapIDValue As Windows.Forms.CheckBox
    Friend WithEvents Label21 As Windows.Forms.Label
    Friend WithEvents T_TargetTimestampfield As Windows.Forms.TextBox
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents Label13 As Windows.Forms.Label
    Friend WithEvents T_TargetIDColumn As Windows.Forms.TextBox
    Friend WithEvents C_TargetServerType As Windows.Forms.ComboBox
    Friend WithEvents L_TargetConnectionType As Windows.Forms.Label
    Friend WithEvents T_TargetServerAdress As Windows.Forms.TextBox
    Friend WithEvents L_TargetPassword As Windows.Forms.Label
    Friend WithEvents T_TargetDB As Windows.Forms.TextBox
    Friend WithEvents L_TargetUsername As Windows.Forms.Label
    Friend WithEvents T_TargetUsername As Windows.Forms.TextBox
    Friend WithEvents Label17 As Windows.Forms.Label
    Friend WithEvents T_TargetPassword As Windows.Forms.TextBox
    Friend WithEvents Label18 As Windows.Forms.Label
    Friend WithEvents C_TargetConnectionType As Windows.Forms.ComboBox
    Friend WithEvents Label19 As Windows.Forms.Label
    Friend WithEvents T_TargetTable As Windows.Forms.TextBox
    Friend WithEvents FolderBrowserDialog1 As Windows.Forms.FolderBrowserDialog
    Friend WithEvents C_DeleteAllowed As Windows.Forms.CheckBox
    Friend WithEvents C_UpdateAllowed As Windows.Forms.CheckBox
    Friend WithEvents C_InsertAllowed As Windows.Forms.CheckBox
    Friend WithEvents Label24 As Windows.Forms.Label
    Friend WithEvents MappingGrid As Windows.Forms.DataGridView
    Friend WithEvents SourceColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TargetColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SourceType As Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents TargetType As Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Seperator As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PartOfSubstring As Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents B_Save As Windows.Forms.Button
    Friend WithEvents C_DebugLog As Windows.Forms.CheckBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents PB_Source As Windows.Forms.PictureBox
    Friend WithEvents PB_Target As Windows.Forms.PictureBox
End Class
