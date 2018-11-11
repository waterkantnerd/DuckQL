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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Konfiguration))
        Me.T_Jobname = New System.Windows.Forms.TextBox()
        Me.L_Jobname = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.T_LoggingDirectory = New System.Windows.Forms.TextBox()
        Me.L_LoggingDirectory = New System.Windows.Forms.Label()
        Me.B_LoggingDirectory = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.C_SourceTable = New System.Windows.Forms.ComboBox()
        Me.L_SourceIDDataType = New System.Windows.Forms.Label()
        Me.C_SourceIDDatatype = New System.Windows.Forms.ComboBox()
        Me.B_SourcePath = New System.Windows.Forms.Button()
        Me.L_SourcePath = New System.Windows.Forms.Label()
        Me.T_SourcePath = New System.Windows.Forms.TextBox()
        Me.PB_Source = New System.Windows.Forms.PictureBox()
        Me.L_SQLFilterStatement = New System.Windows.Forms.Label()
        Me.L_FilterValue = New System.Windows.Forms.Label()
        Me.L_FilterColumn = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.L_SourceIDColumn = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.L_SourceConnectionType = New System.Windows.Forms.Label()
        Me.L_SourcePassword = New System.Windows.Forms.Label()
        Me.L_SourceUsername = New System.Windows.Forms.Label()
        Me.L_SourceDB = New System.Windows.Forms.Label()
        Me.T_SourceSQLFilter = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.L_SourceServerAdress = New System.Windows.Forms.Label()
        Me.T_SourceFilterValue = New System.Windows.Forms.TextBox()
        Me.C_SourceFilterColumn = New System.Windows.Forms.ComboBox()
        Me.C_SourceFilterType = New System.Windows.Forms.ComboBox()
        Me.C_SourceIDColumn = New System.Windows.Forms.ComboBox()
        Me.C_SourceConnMode = New System.Windows.Forms.ComboBox()
        Me.T_SourcePassword = New System.Windows.Forms.TextBox()
        Me.T_SourceUsername = New System.Windows.Forms.TextBox()
        Me.T_SourceDB = New System.Windows.Forms.TextBox()
        Me.C_SourceType = New System.Windows.Forms.ComboBox()
        Me.T_SourceAdress = New System.Windows.Forms.TextBox()
        Me.T_SourceFilterColumn = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.B_TargetSaveFile = New System.Windows.Forms.Button()
        Me.L_TargetIDDatatype = New System.Windows.Forms.Label()
        Me.C_TargetIDDatatype = New System.Windows.Forms.ComboBox()
        Me.B_TargetPath = New System.Windows.Forms.Button()
        Me.L_TargetPath = New System.Windows.Forms.Label()
        Me.T_TargetPath = New System.Windows.Forms.TextBox()
        Me.PB_Target = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.C_DeleteAllowed = New System.Windows.Forms.CheckBox()
        Me.C_UpdateAllowed = New System.Windows.Forms.CheckBox()
        Me.C_InsertAllowed = New System.Windows.Forms.CheckBox()
        Me.L_PartOfSubString = New System.Windows.Forms.Label()
        Me.L_PartOfSubstringUse = New System.Windows.Forms.Label()
        Me.C_TargetPartSubstring = New System.Windows.Forms.ComboBox()
        Me.L_TargetSeperator = New System.Windows.Forms.Label()
        Me.T_TargetSeperator = New System.Windows.Forms.TextBox()
        Me.C_MapIDValue = New System.Windows.Forms.CheckBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.C_TargetTimestampfield = New System.Windows.Forms.ComboBox()
        Me.L_TargetIDColumn = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.C_TargetIDColumn = New System.Windows.Forms.ComboBox()
        Me.C_TargetServerType = New System.Windows.Forms.ComboBox()
        Me.L_TargetConnectionType = New System.Windows.Forms.Label()
        Me.T_TargetServerAdress = New System.Windows.Forms.TextBox()
        Me.L_TargetPassword = New System.Windows.Forms.Label()
        Me.T_TargetDB = New System.Windows.Forms.TextBox()
        Me.L_TargetUsername = New System.Windows.Forms.Label()
        Me.T_TargetUsername = New System.Windows.Forms.TextBox()
        Me.L_TargetDB = New System.Windows.Forms.Label()
        Me.T_TargetPassword = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.C_TargetConnectionType = New System.Windows.Forms.ComboBox()
        Me.L_TargetServerAdress = New System.Windows.Forms.Label()
        Me.C_TargetTable = New System.Windows.Forms.ComboBox()
        Me.T_TargetTimestampfield = New System.Windows.Forms.TextBox()
        Me.T_TargetTable = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.MappingGrid = New System.Windows.Forms.DataGridView()
        Me.SourceColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.TargetColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.SourceType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TargetType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Seperator = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PartOfSubstring = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.StaticValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.B_Save = New System.Windows.Forms.Button()
        Me.C_DebugLog = New System.Windows.Forms.CheckBox()
        Me.C_Silent = New System.Windows.Forms.CheckBox()
        Me.ToolTipKonfig = New System.Windows.Forms.ToolTip(Me.components)
        Me.T_OrderID = New System.Windows.Forms.TextBox()
        Me.C_CheckConsistency = New System.Windows.Forms.CheckBox()
        Me.C_IDlessBatch = New System.Windows.Forms.CheckBox()
        Me.MappingGrid_Offline = New System.Windows.Forms.DataGridView()
        Me.SourceColumn_Offline = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TargetColumn_Offline = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SourceType_Offline = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TargetType_Offline = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Seperator_Offline = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PartOfSubstring_Offline = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.StaticValue_Offline = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.XMLAttribute = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.L_OrderID = New System.Windows.Forms.Label()
        Me.B_Load = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PB_Source, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PB_Target, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MappingGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MappingGrid_Offline, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'T_Jobname
        '
        Me.T_Jobname.Location = New System.Drawing.Point(72, 10)
        Me.T_Jobname.Name = "T_Jobname"
        Me.T_Jobname.Size = New System.Drawing.Size(396, 26)
        Me.T_Jobname.TabIndex = 0
        Me.ToolTipKonfig.SetToolTip(Me.T_Jobname, "Jobname:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "This defines the name of your job you're about to configure. Jobname wi" &
        "ll be used in the logfiles. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Therefore a precise jobname may help, if something" &
        " is not working as it should be.  ")
        '
        'L_Jobname
        '
        Me.L_Jobname.AutoSize = True
        Me.L_Jobname.Location = New System.Drawing.Point(13, 13)
        Me.L_Jobname.Name = "L_Jobname"
        Me.L_Jobname.Size = New System.Drawing.Size(67, 19)
        Me.L_Jobname.TabIndex = 1
        Me.L_Jobname.Text = "Jobname:"
        '
        'T_LoggingDirectory
        '
        Me.T_LoggingDirectory.Location = New System.Drawing.Point(587, 10)
        Me.T_LoggingDirectory.Name = "T_LoggingDirectory"
        Me.T_LoggingDirectory.Size = New System.Drawing.Size(501, 26)
        Me.T_LoggingDirectory.TabIndex = 4
        Me.ToolTipKonfig.SetToolTip(Me.T_LoggingDirectory, "Logging Directory: Enter the path where logfiles should be stored. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "You can use " &
        "the button next to this field to chose the directory via gui. ")
        '
        'L_LoggingDirectory
        '
        Me.L_LoggingDirectory.AutoSize = True
        Me.L_LoggingDirectory.Location = New System.Drawing.Point(482, 13)
        Me.L_LoggingDirectory.Name = "L_LoggingDirectory"
        Me.L_LoggingDirectory.Size = New System.Drawing.Size(122, 19)
        Me.L_LoggingDirectory.TabIndex = 3
        Me.L_LoggingDirectory.Text = "Logging Directory:"
        '
        'B_LoggingDirectory
        '
        Me.B_LoggingDirectory.Location = New System.Drawing.Point(1094, 10)
        Me.B_LoggingDirectory.Name = "B_LoggingDirectory"
        Me.B_LoggingDirectory.Size = New System.Drawing.Size(50, 23)
        Me.B_LoggingDirectory.TabIndex = 3
        Me.B_LoggingDirectory.Text = "..."
        Me.ToolTipKonfig.SetToolTip(Me.B_LoggingDirectory, "Click here to choose a logging directory")
        Me.B_LoggingDirectory.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.C_SourceTable)
        Me.GroupBox1.Controls.Add(Me.L_SourceIDDataType)
        Me.GroupBox1.Controls.Add(Me.C_SourceIDDatatype)
        Me.GroupBox1.Controls.Add(Me.B_SourcePath)
        Me.GroupBox1.Controls.Add(Me.L_SourcePath)
        Me.GroupBox1.Controls.Add(Me.T_SourcePath)
        Me.GroupBox1.Controls.Add(Me.PB_Source)
        Me.GroupBox1.Controls.Add(Me.L_SQLFilterStatement)
        Me.GroupBox1.Controls.Add(Me.L_FilterValue)
        Me.GroupBox1.Controls.Add(Me.L_FilterColumn)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.L_SourceIDColumn)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.L_SourceConnectionType)
        Me.GroupBox1.Controls.Add(Me.L_SourcePassword)
        Me.GroupBox1.Controls.Add(Me.L_SourceUsername)
        Me.GroupBox1.Controls.Add(Me.L_SourceDB)
        Me.GroupBox1.Controls.Add(Me.T_SourceSQLFilter)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.L_SourceServerAdress)
        Me.GroupBox1.Controls.Add(Me.T_SourceFilterValue)
        Me.GroupBox1.Controls.Add(Me.C_SourceFilterColumn)
        Me.GroupBox1.Controls.Add(Me.C_SourceFilterType)
        Me.GroupBox1.Controls.Add(Me.C_SourceIDColumn)
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
        'C_SourceTable
        '
        Me.C_SourceTable.FormattingEnabled = True
        Me.C_SourceTable.Location = New System.Drawing.Point(80, 123)
        Me.C_SourceTable.Name = "C_SourceTable"
        Me.C_SourceTable.Size = New System.Drawing.Size(228, 27)
        Me.C_SourceTable.TabIndex = 51
        Me.ToolTipKonfig.SetToolTip(Me.C_SourceTable, "Type: The datatype of your identifier column")
        '
        'L_SourceIDDataType
        '
        Me.L_SourceIDDataType.AutoSize = True
        Me.L_SourceIDDataType.Location = New System.Drawing.Point(336, 152)
        Me.L_SourceIDDataType.Name = "L_SourceIDDataType"
        Me.L_SourceIDDataType.Size = New System.Drawing.Size(40, 19)
        Me.L_SourceIDDataType.TabIndex = 49
        Me.L_SourceIDDataType.Text = "Type:"
        '
        'C_SourceIDDatatype
        '
        Me.C_SourceIDDatatype.FormattingEnabled = True
        Me.C_SourceIDDatatype.Items.AddRange(New Object() {"uniqueidentifier", "int", "string", "datetime"})
        Me.C_SourceIDDatatype.Location = New System.Drawing.Point(374, 149)
        Me.C_SourceIDDatatype.Name = "C_SourceIDDatatype"
        Me.C_SourceIDDatatype.Size = New System.Drawing.Size(191, 27)
        Me.C_SourceIDDatatype.TabIndex = 48
        Me.ToolTipKonfig.SetToolTip(Me.C_SourceIDDatatype, "Type: The datatype of your identifier column")
        '
        'B_SourcePath
        '
        Me.B_SourcePath.Font = New System.Drawing.Font("Segoe UI Light", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_SourcePath.Location = New System.Drawing.Point(545, 18)
        Me.B_SourcePath.Name = "B_SourcePath"
        Me.B_SourcePath.Size = New System.Drawing.Size(22, 23)
        Me.B_SourcePath.TabIndex = 47
        Me.B_SourcePath.Text = "..."
        Me.ToolTipKonfig.SetToolTip(Me.B_SourcePath, "Click here to choose a logging directory")
        Me.B_SourcePath.UseVisualStyleBackColor = True
        Me.B_SourcePath.Visible = False
        '
        'L_SourcePath
        '
        Me.L_SourcePath.AutoSize = True
        Me.L_SourcePath.Location = New System.Drawing.Point(275, 22)
        Me.L_SourcePath.Name = "L_SourcePath"
        Me.L_SourcePath.Size = New System.Drawing.Size(40, 19)
        Me.L_SourcePath.TabIndex = 25
        Me.L_SourcePath.Text = "Path:"
        Me.L_SourcePath.Visible = False
        '
        'T_SourcePath
        '
        Me.T_SourcePath.Location = New System.Drawing.Point(314, 19)
        Me.T_SourcePath.Name = "T_SourcePath"
        Me.T_SourcePath.Size = New System.Drawing.Size(226, 26)
        Me.T_SourcePath.TabIndex = 24
        Me.ToolTipKonfig.SetToolTip(Me.T_SourcePath, "Server Adress: The Adress of the SQL instance, this may be a hostname or an IP.")
        Me.T_SourcePath.Visible = False
        '
        'PB_Source
        '
        Me.PB_Source.InitialImage = Nothing
        Me.PB_Source.Location = New System.Drawing.Point(568, 18)
        Me.PB_Source.Name = "PB_Source"
        Me.PB_Source.Size = New System.Drawing.Size(16, 88)
        Me.PB_Source.TabIndex = 23
        Me.PB_Source.TabStop = False
        Me.ToolTipKonfig.SetToolTip(Me.PB_Source, resources.GetString("PB_Source.ToolTip"))
        '
        'L_SQLFilterStatement
        '
        Me.L_SQLFilterStatement.AutoSize = True
        Me.L_SQLFilterStatement.Location = New System.Drawing.Point(9, 228)
        Me.L_SQLFilterStatement.Name = "L_SQLFilterStatement"
        Me.L_SQLFilterStatement.Size = New System.Drawing.Size(138, 19)
        Me.L_SQLFilterStatement.TabIndex = 22
        Me.L_SQLFilterStatement.Text = "SQL Filter Statement:"
        Me.L_SQLFilterStatement.Visible = False
        '
        'L_FilterValue
        '
        Me.L_FilterValue.AutoSize = True
        Me.L_FilterValue.Location = New System.Drawing.Point(9, 254)
        Me.L_FilterValue.Name = "L_FilterValue"
        Me.L_FilterValue.Size = New System.Drawing.Size(79, 19)
        Me.L_FilterValue.TabIndex = 21
        Me.L_FilterValue.Text = "Filter Value:"
        Me.L_FilterValue.Visible = False
        '
        'L_FilterColumn
        '
        Me.L_FilterColumn.AutoSize = True
        Me.L_FilterColumn.Location = New System.Drawing.Point(9, 228)
        Me.L_FilterColumn.Name = "L_FilterColumn"
        Me.L_FilterColumn.Size = New System.Drawing.Size(94, 19)
        Me.L_FilterColumn.TabIndex = 20
        Me.L_FilterColumn.Text = "Filter Column:"
        Me.L_FilterColumn.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 201)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 19)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Filter Type:"
        '
        'L_SourceIDColumn
        '
        Me.L_SourceIDColumn.AutoSize = True
        Me.L_SourceIDColumn.Location = New System.Drawing.Point(9, 152)
        Me.L_SourceIDColumn.Name = "L_SourceIDColumn"
        Me.L_SourceIDColumn.Size = New System.Drawing.Size(78, 19)
        Me.L_SourceIDColumn.TabIndex = 18
        Me.L_SourceIDColumn.Text = "ID Column:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 126)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(42, 19)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Table:"
        '
        'L_SourceConnectionType
        '
        Me.L_SourceConnectionType.AutoSize = True
        Me.L_SourceConnectionType.Location = New System.Drawing.Point(277, 49)
        Me.L_SourceConnectionType.Name = "L_SourceConnectionType"
        Me.L_SourceConnectionType.Size = New System.Drawing.Size(114, 19)
        Me.L_SourceConnectionType.TabIndex = 16
        Me.L_SourceConnectionType.Text = "Connection Type:"
        Me.L_SourceConnectionType.Visible = False
        '
        'L_SourcePassword
        '
        Me.L_SourcePassword.AutoSize = True
        Me.L_SourcePassword.Location = New System.Drawing.Point(288, 87)
        Me.L_SourcePassword.Name = "L_SourcePassword"
        Me.L_SourcePassword.Size = New System.Drawing.Size(70, 19)
        Me.L_SourcePassword.TabIndex = 15
        Me.L_SourcePassword.Text = "Password:"
        Me.L_SourcePassword.Visible = False
        '
        'L_SourceUsername
        '
        Me.L_SourceUsername.AutoSize = True
        Me.L_SourceUsername.Location = New System.Drawing.Point(9, 87)
        Me.L_SourceUsername.Name = "L_SourceUsername"
        Me.L_SourceUsername.Size = New System.Drawing.Size(74, 19)
        Me.L_SourceUsername.TabIndex = 14
        Me.L_SourceUsername.Text = "Username:"
        Me.L_SourceUsername.Visible = False
        '
        'L_SourceDB
        '
        Me.L_SourceDB.AutoSize = True
        Me.L_SourceDB.Location = New System.Drawing.Point(9, 49)
        Me.L_SourceDB.Name = "L_SourceDB"
        Me.L_SourceDB.Size = New System.Drawing.Size(69, 19)
        Me.L_SourceDB.TabIndex = 13
        Me.L_SourceDB.Text = "Database:"
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
        Me.Label2.Size = New System.Drawing.Size(82, 19)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Server Type:"
        '
        'L_SourceServerAdress
        '
        Me.L_SourceServerAdress.AutoSize = True
        Me.L_SourceServerAdress.Location = New System.Drawing.Point(277, 22)
        Me.L_SourceServerAdress.Name = "L_SourceServerAdress"
        Me.L_SourceServerAdress.Size = New System.Drawing.Size(95, 19)
        Me.L_SourceServerAdress.TabIndex = 7
        Me.L_SourceServerAdress.Text = "Server Adress:"
        '
        'T_SourceFilterValue
        '
        Me.T_SourceFilterValue.Location = New System.Drawing.Point(80, 251)
        Me.T_SourceFilterValue.Name = "T_SourceFilterValue"
        Me.T_SourceFilterValue.Size = New System.Drawing.Size(206, 26)
        Me.T_SourceFilterValue.TabIndex = 14
        Me.T_SourceFilterValue.Visible = False
        '
        'C_SourceFilterColumn
        '
        Me.C_SourceFilterColumn.FormattingEnabled = True
        Me.C_SourceFilterColumn.Location = New System.Drawing.Point(80, 224)
        Me.C_SourceFilterColumn.Name = "C_SourceFilterColumn"
        Me.C_SourceFilterColumn.Size = New System.Drawing.Size(206, 27)
        Me.C_SourceFilterColumn.TabIndex = 52
        Me.ToolTipKonfig.SetToolTip(Me.C_SourceFilterColumn, "Type: The datatype of your identifier column")
        Me.C_SourceFilterColumn.Visible = False
        '
        'C_SourceFilterType
        '
        Me.C_SourceFilterType.FormattingEnabled = True
        Me.C_SourceFilterType.Items.AddRange(New Object() {"none", "one column match", "SQL Filter"})
        Me.C_SourceFilterType.Location = New System.Drawing.Point(80, 198)
        Me.C_SourceFilterType.Name = "C_SourceFilterType"
        Me.C_SourceFilterType.Size = New System.Drawing.Size(206, 27)
        Me.C_SourceFilterType.TabIndex = 12
        Me.ToolTipKonfig.SetToolTip(Me.C_SourceFilterType, resources.GetString("C_SourceFilterType.ToolTip"))
        '
        'C_SourceIDColumn
        '
        Me.C_SourceIDColumn.FormattingEnabled = True
        Me.C_SourceIDColumn.Location = New System.Drawing.Point(80, 149)
        Me.C_SourceIDColumn.Name = "C_SourceIDColumn"
        Me.C_SourceIDColumn.Size = New System.Drawing.Size(228, 27)
        Me.C_SourceIDColumn.TabIndex = 11
        Me.ToolTipKonfig.SetToolTip(Me.C_SourceIDColumn, "ID Column: The columns that is used as identifier, i.e. ID. At the moment only on" &
        "e column is supported.")
        '
        'C_SourceConnMode
        '
        Me.C_SourceConnMode.FormattingEnabled = True
        Me.C_SourceConnMode.Location = New System.Drawing.Point(374, 45)
        Me.C_SourceConnMode.Name = "C_SourceConnMode"
        Me.C_SourceConnMode.Size = New System.Drawing.Size(191, 27)
        Me.C_SourceConnMode.TabIndex = 7
        Me.ToolTipKonfig.SetToolTip(Me.C_SourceConnMode, resources.GetString("C_SourceConnMode.ToolTip"))
        Me.C_SourceConnMode.Visible = False
        '
        'T_SourcePassword
        '
        Me.T_SourcePassword.Location = New System.Drawing.Point(359, 84)
        Me.T_SourcePassword.Name = "T_SourcePassword"
        Me.T_SourcePassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.T_SourcePassword.Size = New System.Drawing.Size(206, 26)
        Me.T_SourcePassword.TabIndex = 9
        Me.ToolTipKonfig.SetToolTip(Me.T_SourcePassword, "Password: The corresponding password. Attention! Passwords are stored in clear te" &
        "xt!")
        Me.T_SourcePassword.Visible = False
        '
        'T_SourceUsername
        '
        Me.T_SourceUsername.Location = New System.Drawing.Point(80, 84)
        Me.T_SourceUsername.Name = "T_SourceUsername"
        Me.T_SourceUsername.Size = New System.Drawing.Size(191, 26)
        Me.T_SourceUsername.TabIndex = 8
        Me.ToolTipKonfig.SetToolTip(Me.T_SourceUsername, resources.GetString("T_SourceUsername.ToolTip"))
        Me.T_SourceUsername.Visible = False
        '
        'T_SourceDB
        '
        Me.T_SourceDB.Location = New System.Drawing.Point(80, 46)
        Me.T_SourceDB.Name = "T_SourceDB"
        Me.T_SourceDB.Size = New System.Drawing.Size(191, 26)
        Me.T_SourceDB.TabIndex = 6
        Me.ToolTipKonfig.SetToolTip(Me.T_SourceDB, "Database: The database you want to use.")
        '
        'C_SourceType
        '
        Me.C_SourceType.FormattingEnabled = True
        Me.C_SourceType.Items.AddRange(New Object() {"MS-SQL", "MySQL", "Access", "XML", "CSV"})
        Me.C_SourceType.Location = New System.Drawing.Point(80, 19)
        Me.C_SourceType.Name = "C_SourceType"
        Me.C_SourceType.Size = New System.Drawing.Size(191, 27)
        Me.C_SourceType.TabIndex = 4
        Me.ToolTipKonfig.SetToolTip(Me.C_SourceType, "Server Type: Choose the type of you datasource.")
        '
        'T_SourceAdress
        '
        Me.T_SourceAdress.Location = New System.Drawing.Point(359, 19)
        Me.T_SourceAdress.Name = "T_SourceAdress"
        Me.T_SourceAdress.Size = New System.Drawing.Size(206, 26)
        Me.T_SourceAdress.TabIndex = 5
        Me.ToolTipKonfig.SetToolTip(Me.T_SourceAdress, "Server Adress: The Adress of the SQL instance, this may be a hostname or an IP.")
        '
        'T_SourceFilterColumn
        '
        Me.T_SourceFilterColumn.Location = New System.Drawing.Point(0, 0)
        Me.T_SourceFilterColumn.Name = "T_SourceFilterColumn"
        Me.T_SourceFilterColumn.Size = New System.Drawing.Size(100, 22)
        Me.T_SourceFilterColumn.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.B_TargetSaveFile)
        Me.GroupBox2.Controls.Add(Me.L_TargetIDDatatype)
        Me.GroupBox2.Controls.Add(Me.C_TargetIDDatatype)
        Me.GroupBox2.Controls.Add(Me.B_TargetPath)
        Me.GroupBox2.Controls.Add(Me.L_TargetPath)
        Me.GroupBox2.Controls.Add(Me.T_TargetPath)
        Me.GroupBox2.Controls.Add(Me.PB_Target)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.C_DeleteAllowed)
        Me.GroupBox2.Controls.Add(Me.C_UpdateAllowed)
        Me.GroupBox2.Controls.Add(Me.C_InsertAllowed)
        Me.GroupBox2.Controls.Add(Me.L_PartOfSubString)
        Me.GroupBox2.Controls.Add(Me.L_PartOfSubstringUse)
        Me.GroupBox2.Controls.Add(Me.C_TargetPartSubstring)
        Me.GroupBox2.Controls.Add(Me.L_TargetSeperator)
        Me.GroupBox2.Controls.Add(Me.T_TargetSeperator)
        Me.GroupBox2.Controls.Add(Me.C_MapIDValue)
        Me.GroupBox2.Controls.Add(Me.Label21)
        Me.GroupBox2.Controls.Add(Me.C_TargetTimestampfield)
        Me.GroupBox2.Controls.Add(Me.L_TargetIDColumn)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.C_TargetIDColumn)
        Me.GroupBox2.Controls.Add(Me.C_TargetServerType)
        Me.GroupBox2.Controls.Add(Me.L_TargetConnectionType)
        Me.GroupBox2.Controls.Add(Me.T_TargetServerAdress)
        Me.GroupBox2.Controls.Add(Me.L_TargetPassword)
        Me.GroupBox2.Controls.Add(Me.T_TargetDB)
        Me.GroupBox2.Controls.Add(Me.L_TargetUsername)
        Me.GroupBox2.Controls.Add(Me.T_TargetUsername)
        Me.GroupBox2.Controls.Add(Me.L_TargetDB)
        Me.GroupBox2.Controls.Add(Me.T_TargetPassword)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.C_TargetConnectionType)
        Me.GroupBox2.Controls.Add(Me.L_TargetServerAdress)
        Me.GroupBox2.Controls.Add(Me.C_TargetTable)
        Me.GroupBox2.Location = New System.Drawing.Point(612, 65)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(590, 359)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Target"
        '
        'B_TargetSaveFile
        '
        Me.B_TargetSaveFile.Font = New System.Drawing.Font("Segoe UI Light", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_TargetSaveFile.Location = New System.Drawing.Point(547, 18)
        Me.B_TargetSaveFile.Name = "B_TargetSaveFile"
        Me.B_TargetSaveFile.Size = New System.Drawing.Size(21, 23)
        Me.B_TargetSaveFile.TabIndex = 53
        Me.B_TargetSaveFile.Text = "..."
        Me.ToolTipKonfig.SetToolTip(Me.B_TargetSaveFile, "Click here to choose a logging directory")
        Me.B_TargetSaveFile.UseVisualStyleBackColor = True
        Me.B_TargetSaveFile.Visible = False
        '
        'L_TargetIDDatatype
        '
        Me.L_TargetIDDatatype.AutoSize = True
        Me.L_TargetIDDatatype.Location = New System.Drawing.Point(337, 152)
        Me.L_TargetIDDatatype.Name = "L_TargetIDDatatype"
        Me.L_TargetIDDatatype.Size = New System.Drawing.Size(40, 19)
        Me.L_TargetIDDatatype.TabIndex = 50
        Me.L_TargetIDDatatype.Text = "Type:"
        '
        'C_TargetIDDatatype
        '
        Me.C_TargetIDDatatype.FormattingEnabled = True
        Me.C_TargetIDDatatype.Items.AddRange(New Object() {"uniqueidentifier", "int", "string", "datetime"})
        Me.C_TargetIDDatatype.Location = New System.Drawing.Point(375, 149)
        Me.C_TargetIDDatatype.Name = "C_TargetIDDatatype"
        Me.C_TargetIDDatatype.Size = New System.Drawing.Size(191, 27)
        Me.C_TargetIDDatatype.TabIndex = 49
        Me.ToolTipKonfig.SetToolTip(Me.C_TargetIDDatatype, "Type: The datatype of your identifier column")
        '
        'B_TargetPath
        '
        Me.B_TargetPath.Font = New System.Drawing.Font("Segoe UI Light", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_TargetPath.Location = New System.Drawing.Point(547, 18)
        Me.B_TargetPath.Name = "B_TargetPath"
        Me.B_TargetPath.Size = New System.Drawing.Size(21, 23)
        Me.B_TargetPath.TabIndex = 48
        Me.B_TargetPath.Text = "..."
        Me.ToolTipKonfig.SetToolTip(Me.B_TargetPath, "Click here to choose a logging directory")
        Me.B_TargetPath.UseVisualStyleBackColor = True
        Me.B_TargetPath.Visible = False
        '
        'L_TargetPath
        '
        Me.L_TargetPath.AutoSize = True
        Me.L_TargetPath.Location = New System.Drawing.Point(278, 21)
        Me.L_TargetPath.Name = "L_TargetPath"
        Me.L_TargetPath.Size = New System.Drawing.Size(40, 19)
        Me.L_TargetPath.TabIndex = 26
        Me.L_TargetPath.Text = "Path:"
        Me.L_TargetPath.Visible = False
        '
        'T_TargetPath
        '
        Me.T_TargetPath.Location = New System.Drawing.Point(317, 18)
        Me.T_TargetPath.Name = "T_TargetPath"
        Me.T_TargetPath.Size = New System.Drawing.Size(228, 26)
        Me.T_TargetPath.TabIndex = 25
        Me.ToolTipKonfig.SetToolTip(Me.T_TargetPath, "Server Adress: The Adress of the SQL instance, this may be a hostname or an IP.")
        Me.T_TargetPath.Visible = False
        '
        'PB_Target
        '
        Me.PB_Target.Location = New System.Drawing.Point(568, 18)
        Me.PB_Target.Name = "PB_Target"
        Me.PB_Target.Size = New System.Drawing.Size(16, 88)
        Me.PB_Target.TabIndex = 24
        Me.PB_Target.TabStop = False
        Me.ToolTipKonfig.SetToolTip(Me.PB_Target, resources.GetString("PB_Target.ToolTip"))
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 320)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(0, 19)
        Me.Label4.TabIndex = 45
        '
        'C_DeleteAllowed
        '
        Me.C_DeleteAllowed.AutoSize = True
        Me.C_DeleteAllowed.Location = New System.Drawing.Point(461, 269)
        Me.C_DeleteAllowed.Name = "C_DeleteAllowed"
        Me.C_DeleteAllowed.Size = New System.Drawing.Size(126, 23)
        Me.C_DeleteAllowed.TabIndex = 29
        Me.C_DeleteAllowed.Text = "DELETE allowed"
        Me.ToolTipKonfig.SetToolTip(Me.C_DeleteAllowed, "DELETE allowed: Check if you want that rows, that not exists in you source will b" &
        "e deleted.")
        Me.C_DeleteAllowed.UseVisualStyleBackColor = True
        '
        'C_UpdateAllowed
        '
        Me.C_UpdateAllowed.AutoSize = True
        Me.C_UpdateAllowed.Location = New System.Drawing.Point(260, 269)
        Me.C_UpdateAllowed.Name = "C_UpdateAllowed"
        Me.C_UpdateAllowed.Size = New System.Drawing.Size(131, 23)
        Me.C_UpdateAllowed.TabIndex = 28
        Me.C_UpdateAllowed.Text = "UPDATE allowed"
        Me.ToolTipKonfig.SetToolTip(Me.C_UpdateAllowed, "UPDATE allowed: Check if you want existing rows to be updated")
        Me.C_UpdateAllowed.UseVisualStyleBackColor = True
        '
        'C_InsertAllowed
        '
        Me.C_InsertAllowed.AutoSize = True
        Me.C_InsertAllowed.Location = New System.Drawing.Point(81, 266)
        Me.C_InsertAllowed.Name = "C_InsertAllowed"
        Me.C_InsertAllowed.Size = New System.Drawing.Size(123, 23)
        Me.C_InsertAllowed.TabIndex = 27
        Me.C_InsertAllowed.Text = "INSERT allowed"
        Me.ToolTipKonfig.SetToolTip(Me.C_InsertAllowed, "INSERT allowed: Check if you want new values to be added to your source.")
        Me.C_InsertAllowed.UseVisualStyleBackColor = True
        '
        'L_PartOfSubString
        '
        Me.L_PartOfSubString.AutoSize = True
        Me.L_PartOfSubString.Location = New System.Drawing.Point(507, 187)
        Me.L_PartOfSubString.Name = "L_PartOfSubString"
        Me.L_PartOfSubString.Size = New System.Drawing.Size(90, 38)
        Me.L_PartOfSubString.TabIndex = 44
        Me.L_PartOfSubString.Text = "...part of " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "the substring"
        '
        'L_PartOfSubstringUse
        '
        Me.L_PartOfSubstringUse.AutoSize = True
        Me.L_PartOfSubstringUse.Location = New System.Drawing.Point(310, 190)
        Me.L_PartOfSubstringUse.Name = "L_PartOfSubstringUse"
        Me.L_PartOfSubstringUse.Size = New System.Drawing.Size(32, 19)
        Me.L_PartOfSubstringUse.TabIndex = 43
        Me.L_PartOfSubstringUse.Text = "Use"
        '
        'C_TargetPartSubstring
        '
        Me.C_TargetPartSubstring.FormattingEnabled = True
        Me.C_TargetPartSubstring.Items.AddRange(New Object() {"left", "right"})
        Me.C_TargetPartSubstring.Location = New System.Drawing.Point(342, 187)
        Me.C_TargetPartSubstring.Name = "C_TargetPartSubstring"
        Me.C_TargetPartSubstring.Size = New System.Drawing.Size(159, 27)
        Me.C_TargetPartSubstring.TabIndex = 25
        Me.ToolTipKonfig.SetToolTip(Me.C_TargetPartSubstring, "Use ... part of the substring: You can choose if the program should you the left " &
        "or the right side starting from" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "the seperator, i.e. of you cut 10-12345 to 1234" &
        "5 you have to choose ""right"".")
        '
        'L_TargetSeperator
        '
        Me.L_TargetSeperator.AutoSize = True
        Me.L_TargetSeperator.Location = New System.Drawing.Point(170, 190)
        Me.L_TargetSeperator.Name = "L_TargetSeperator"
        Me.L_TargetSeperator.Size = New System.Drawing.Size(71, 19)
        Me.L_TargetSeperator.TabIndex = 41
        Me.L_TargetSeperator.Text = "Seperator:"
        '
        'T_TargetSeperator
        '
        Me.T_TargetSeperator.Location = New System.Drawing.Point(236, 187)
        Me.T_TargetSeperator.Name = "T_TargetSeperator"
        Me.T_TargetSeperator.Size = New System.Drawing.Size(68, 26)
        Me.T_TargetSeperator.TabIndex = 24
        Me.ToolTipKonfig.SetToolTip(Me.T_TargetSeperator, "Seperator: The char or string which should be used as the indicator to cut the ID" &
        " Value, i.e. if you want have " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "10-12345 in your source db and need 12345 in you" &
        "r target the seperator will be 10-.")
        '
        'C_MapIDValue
        '
        Me.C_MapIDValue.AutoSize = True
        Me.C_MapIDValue.Location = New System.Drawing.Point(81, 189)
        Me.C_MapIDValue.Name = "C_MapIDValue"
        Me.C_MapIDValue.Size = New System.Drawing.Size(114, 23)
        Me.C_MapIDValue.TabIndex = 23
        Me.C_MapIDValue.Text = "Map ID Value"
        Me.ToolTipKonfig.SetToolTip(Me.C_MapIDValue, "Map ID Value: Check this, if you need to match a substring of the source ID value" &
        " with you target IDs, i.e. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "source ID value is 10-12345 and target values shoul" &
        "d be only 12345.")
        Me.C_MapIDValue.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(10, 231)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(116, 19)
        Me.Label21.TabIndex = 38
        Me.Label21.Text = "Time Stamp Field:"
        '
        'C_TargetTimestampfield
        '
        Me.C_TargetTimestampfield.FormattingEnabled = True
        Me.C_TargetTimestampfield.Location = New System.Drawing.Point(107, 228)
        Me.C_TargetTimestampfield.Name = "C_TargetTimestampfield"
        Me.C_TargetTimestampfield.Size = New System.Drawing.Size(459, 27)
        Me.C_TargetTimestampfield.TabIndex = 26
        Me.ToolTipKonfig.SetToolTip(Me.C_TargetTimestampfield, "Time Stamp Field: If you want to set a sync timestamp, to see when the row has sy" &
        "nced, you can add the name")
        '
        'L_TargetIDColumn
        '
        Me.L_TargetIDColumn.AutoSize = True
        Me.L_TargetIDColumn.Location = New System.Drawing.Point(10, 152)
        Me.L_TargetIDColumn.Name = "L_TargetIDColumn"
        Me.L_TargetIDColumn.Size = New System.Drawing.Size(78, 19)
        Me.L_TargetIDColumn.TabIndex = 24
        Me.L_TargetIDColumn.Text = "ID Column:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(10, 125)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(42, 19)
        Me.Label13.TabIndex = 36
        Me.Label13.Text = "Table:"
        '
        'C_TargetIDColumn
        '
        Me.C_TargetIDColumn.FormattingEnabled = True
        Me.C_TargetIDColumn.Location = New System.Drawing.Point(81, 149)
        Me.C_TargetIDColumn.Name = "C_TargetIDColumn"
        Me.C_TargetIDColumn.Size = New System.Drawing.Size(230, 27)
        Me.C_TargetIDColumn.TabIndex = 22
        Me.ToolTipKonfig.SetToolTip(Me.C_TargetIDColumn, "Type: The datatype of your identifier column")
        '
        'C_TargetServerType
        '
        Me.C_TargetServerType.FormattingEnabled = True
        Me.C_TargetServerType.Items.AddRange(New Object() {"MS-SQL", "MySQL", "Access", "XML", "CSV"})
        Me.C_TargetServerType.Location = New System.Drawing.Point(81, 18)
        Me.C_TargetServerType.Name = "C_TargetServerType"
        Me.C_TargetServerType.Size = New System.Drawing.Size(191, 27)
        Me.C_TargetServerType.TabIndex = 15
        Me.ToolTipKonfig.SetToolTip(Me.C_TargetServerType, "Server Type: Choose the type of you target datasource.")
        '
        'L_TargetConnectionType
        '
        Me.L_TargetConnectionType.AutoSize = True
        Me.L_TargetConnectionType.Location = New System.Drawing.Point(278, 48)
        Me.L_TargetConnectionType.Name = "L_TargetConnectionType"
        Me.L_TargetConnectionType.Size = New System.Drawing.Size(114, 19)
        Me.L_TargetConnectionType.TabIndex = 35
        Me.L_TargetConnectionType.Text = "Connection Type:"
        Me.L_TargetConnectionType.Visible = False
        '
        'T_TargetServerAdress
        '
        Me.T_TargetServerAdress.Location = New System.Drawing.Point(360, 18)
        Me.T_TargetServerAdress.Name = "T_TargetServerAdress"
        Me.T_TargetServerAdress.Size = New System.Drawing.Size(206, 26)
        Me.T_TargetServerAdress.TabIndex = 16
        Me.ToolTipKonfig.SetToolTip(Me.T_TargetServerAdress, "Server Adress: The Adress of the SQL instance, this may be a hostname or an IP.")
        '
        'L_TargetPassword
        '
        Me.L_TargetPassword.AutoSize = True
        Me.L_TargetPassword.Location = New System.Drawing.Point(289, 86)
        Me.L_TargetPassword.Name = "L_TargetPassword"
        Me.L_TargetPassword.Size = New System.Drawing.Size(70, 19)
        Me.L_TargetPassword.TabIndex = 34
        Me.L_TargetPassword.Text = "Password:"
        Me.L_TargetPassword.Visible = False
        '
        'T_TargetDB
        '
        Me.T_TargetDB.Location = New System.Drawing.Point(81, 45)
        Me.T_TargetDB.Name = "T_TargetDB"
        Me.T_TargetDB.Size = New System.Drawing.Size(191, 26)
        Me.T_TargetDB.TabIndex = 17
        Me.ToolTipKonfig.SetToolTip(Me.T_TargetDB, "Database: The database you want to use.")
        '
        'L_TargetUsername
        '
        Me.L_TargetUsername.AutoSize = True
        Me.L_TargetUsername.Location = New System.Drawing.Point(10, 86)
        Me.L_TargetUsername.Name = "L_TargetUsername"
        Me.L_TargetUsername.Size = New System.Drawing.Size(74, 19)
        Me.L_TargetUsername.TabIndex = 33
        Me.L_TargetUsername.Text = "Username:"
        Me.L_TargetUsername.Visible = False
        '
        'T_TargetUsername
        '
        Me.T_TargetUsername.Location = New System.Drawing.Point(81, 83)
        Me.T_TargetUsername.Name = "T_TargetUsername"
        Me.T_TargetUsername.Size = New System.Drawing.Size(191, 26)
        Me.T_TargetUsername.TabIndex = 19
        Me.ToolTipKonfig.SetToolTip(Me.T_TargetUsername, resources.GetString("T_TargetUsername.ToolTip"))
        Me.T_TargetUsername.Visible = False
        '
        'L_TargetDB
        '
        Me.L_TargetDB.AutoSize = True
        Me.L_TargetDB.Location = New System.Drawing.Point(10, 48)
        Me.L_TargetDB.Name = "L_TargetDB"
        Me.L_TargetDB.Size = New System.Drawing.Size(69, 19)
        Me.L_TargetDB.TabIndex = 32
        Me.L_TargetDB.Text = "Database:"
        '
        'T_TargetPassword
        '
        Me.T_TargetPassword.Location = New System.Drawing.Point(360, 83)
        Me.T_TargetPassword.Name = "T_TargetPassword"
        Me.T_TargetPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.T_TargetPassword.Size = New System.Drawing.Size(206, 26)
        Me.T_TargetPassword.TabIndex = 20
        Me.ToolTipKonfig.SetToolTip(Me.T_TargetPassword, "Password: The corresponding password. Attention! Passwords are stored in clear te" &
        "xt!")
        Me.T_TargetPassword.Visible = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(10, 21)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(82, 19)
        Me.Label18.TabIndex = 31
        Me.Label18.Text = "Server Type:"
        '
        'C_TargetConnectionType
        '
        Me.C_TargetConnectionType.FormattingEnabled = True
        Me.C_TargetConnectionType.Location = New System.Drawing.Point(375, 44)
        Me.C_TargetConnectionType.Name = "C_TargetConnectionType"
        Me.C_TargetConnectionType.Size = New System.Drawing.Size(191, 27)
        Me.C_TargetConnectionType.TabIndex = 18
        Me.ToolTipKonfig.SetToolTip(Me.C_TargetConnectionType, resources.GetString("C_TargetConnectionType.ToolTip"))
        Me.C_TargetConnectionType.Visible = False
        '
        'L_TargetServerAdress
        '
        Me.L_TargetServerAdress.AutoSize = True
        Me.L_TargetServerAdress.Location = New System.Drawing.Point(278, 21)
        Me.L_TargetServerAdress.Name = "L_TargetServerAdress"
        Me.L_TargetServerAdress.Size = New System.Drawing.Size(95, 19)
        Me.L_TargetServerAdress.TabIndex = 30
        Me.L_TargetServerAdress.Text = "Server Adress:"
        '
        'C_TargetTable
        '
        Me.C_TargetTable.FormattingEnabled = True
        Me.C_TargetTable.Location = New System.Drawing.Point(80, 122)
        Me.C_TargetTable.Name = "C_TargetTable"
        Me.C_TargetTable.Size = New System.Drawing.Size(231, 27)
        Me.C_TargetTable.TabIndex = 52
        Me.ToolTipKonfig.SetToolTip(Me.C_TargetTable, "Type: The datatype of your identifier column")
        '
        'T_TargetTimestampfield
        '
        Me.T_TargetTimestampfield.Location = New System.Drawing.Point(0, 0)
        Me.T_TargetTimestampfield.Name = "T_TargetTimestampfield"
        Me.T_TargetTimestampfield.Size = New System.Drawing.Size(100, 22)
        Me.T_TargetTimestampfield.TabIndex = 0
        '
        'T_TargetTable
        '
        Me.T_TargetTable.Location = New System.Drawing.Point(0, 0)
        Me.T_TargetTable.Name = "T_TargetTable"
        Me.T_TargetTable.Size = New System.Drawing.Size(100, 22)
        Me.T_TargetTable.TabIndex = 0
        '
        'MappingGrid
        '
        Me.MappingGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.MappingGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MappingGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SourceColumn, Me.TargetColumn, Me.SourceType, Me.TargetType, Me.Seperator, Me.PartOfSubstring, Me.StaticValue})
        Me.MappingGrid.Location = New System.Drawing.Point(16, 430)
        Me.MappingGrid.Name = "MappingGrid"
        Me.MappingGrid.Size = New System.Drawing.Size(1185, 406)
        Me.MappingGrid.TabIndex = 30
        Me.ToolTipKonfig.SetToolTip(Me.MappingGrid, "Enter the mappings between source and target tables")
        '
        'SourceColumn
        '
        Me.SourceColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SourceColumn.HeaderText = "Source Column"
        Me.SourceColumn.Name = "SourceColumn"
        Me.SourceColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SourceColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.SourceColumn.Width = 120
        '
        'TargetColumn
        '
        Me.TargetColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TargetColumn.HeaderText = "Target Column"
        Me.TargetColumn.Name = "TargetColumn"
        Me.TargetColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TargetColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.TargetColumn.Width = 117
        '
        'SourceType
        '
        Me.SourceType.HeaderText = "Source Type"
        Me.SourceType.Name = "SourceType"
        Me.SourceType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SourceType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SourceType.Width = 79
        '
        'TargetType
        '
        Me.TargetType.HeaderText = "Target Type"
        Me.TargetType.Name = "TargetType"
        Me.TargetType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TargetType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TargetType.Width = 76
        '
        'Seperator
        '
        Me.Seperator.HeaderText = "Seperator"
        Me.Seperator.Name = "Seperator"
        Me.Seperator.Width = 97
        '
        'PartOfSubstring
        '
        Me.PartOfSubstring.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PartOfSubstring.HeaderText = "Part Of Substring"
        Me.PartOfSubstring.Items.AddRange(New Object() {"left", "right"})
        Me.PartOfSubstring.Name = "PartOfSubstring"
        Me.PartOfSubstring.Width = 109
        '
        'StaticValue
        '
        Me.StaticValue.HeaderText = "Static Value for Target Column"
        Me.StaticValue.Name = "StaticValue"
        Me.StaticValue.ToolTipText = "If there is no fitting source, you can set a static value, which will be set for " &
    "every row, here."
        Me.StaticValue.Width = 159
        '
        'B_Save
        '
        Me.B_Save.Location = New System.Drawing.Point(1007, 842)
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
        Me.C_DebugLog.Size = New System.Drawing.Size(170, 23)
        Me.C_DebugLog.TabIndex = 5
        Me.C_DebugLog.Text = "Enable Debug Logging"
        Me.ToolTipKonfig.SetToolTip(Me.C_DebugLog, "Enable Debug Logging: This is helpful in testing szenarios. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "CAUTION!! This crea" &
        "tes a lot of noise. ")
        Me.C_DebugLog.UseVisualStyleBackColor = True
        '
        'C_Silent
        '
        Me.C_Silent.AutoSize = True
        Me.C_Silent.Location = New System.Drawing.Point(739, 36)
        Me.C_Silent.Name = "C_Silent"
        Me.C_Silent.Size = New System.Drawing.Size(163, 23)
        Me.C_Silent.TabIndex = 6
        Me.C_Silent.Text = "Enable Silent Running"
        Me.ToolTipKonfig.SetToolTip(Me.C_Silent, "Enable Silent Running: This disables the prompt in the shell and adds a little bi" &
        "t of extra" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "performance. Very useful in a productive environment.")
        Me.C_Silent.UseVisualStyleBackColor = True
        '
        'ToolTipKonfig
        '
        Me.ToolTipKonfig.ToolTipTitle = "Configuration"
        '
        'T_OrderID
        '
        Me.T_OrderID.Location = New System.Drawing.Point(72, 36)
        Me.T_OrderID.Name = "T_OrderID"
        Me.T_OrderID.Size = New System.Drawing.Size(49, 26)
        Me.T_OrderID.TabIndex = 1
        Me.ToolTipKonfig.SetToolTip(Me.T_OrderID, "Order ID: In case you want to store multiple files in one job folder, which has t" &
        "o be proccessed in a specific order." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "This has to be a numeric value.")
        '
        'C_CheckConsistency
        '
        Me.C_CheckConsistency.AutoSize = True
        Me.C_CheckConsistency.Location = New System.Drawing.Point(347, 36)
        Me.C_CheckConsistency.Name = "C_CheckConsistency"
        Me.C_CheckConsistency.Size = New System.Drawing.Size(145, 23)
        Me.C_CheckConsistency.TabIndex = 2
        Me.C_CheckConsistency.Text = "Check Consistency"
        Me.ToolTipKonfig.SetToolTip(Me.C_CheckConsistency, resources.GetString("C_CheckConsistency.ToolTip"))
        Me.C_CheckConsistency.UseVisualStyleBackColor = True
        '
        'C_IDlessBatch
        '
        Me.C_IDlessBatch.AutoSize = True
        Me.C_IDlessBatch.Location = New System.Drawing.Point(253, 36)
        Me.C_IDlessBatch.Name = "C_IDlessBatch"
        Me.C_IDlessBatch.Size = New System.Drawing.Size(111, 23)
        Me.C_IDlessBatch.TabIndex = 50
        Me.C_IDlessBatch.Text = "ID-less Batch"
        Me.ToolTipKonfig.SetToolTip(Me.C_IDlessBatch, resources.GetString("C_IDlessBatch.ToolTip"))
        Me.C_IDlessBatch.UseVisualStyleBackColor = True
        '
        'MappingGrid_Offline
        '
        Me.MappingGrid_Offline.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.MappingGrid_Offline.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MappingGrid_Offline.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SourceColumn_Offline, Me.TargetColumn_Offline, Me.SourceType_Offline, Me.TargetType_Offline, Me.Seperator_Offline, Me.PartOfSubstring_Offline, Me.StaticValue_Offline, Me.XMLAttribute})
        Me.MappingGrid_Offline.Location = New System.Drawing.Point(16, 430)
        Me.MappingGrid_Offline.Name = "MappingGrid_Offline"
        Me.MappingGrid_Offline.Size = New System.Drawing.Size(1186, 406)
        Me.MappingGrid_Offline.TabIndex = 51
        Me.ToolTipKonfig.SetToolTip(Me.MappingGrid_Offline, "Enter the mappings between source and target tables")
        '
        'SourceColumn_Offline
        '
        Me.SourceColumn_Offline.HeaderText = "Source Column"
        Me.SourceColumn_Offline.Name = "SourceColumn_Offline"
        Me.SourceColumn_Offline.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SourceColumn_Offline.Width = 120
        '
        'TargetColumn_Offline
        '
        Me.TargetColumn_Offline.HeaderText = "Target Column"
        Me.TargetColumn_Offline.Name = "TargetColumn_Offline"
        Me.TargetColumn_Offline.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TargetColumn_Offline.Width = 117
        '
        'SourceType_Offline
        '
        Me.SourceType_Offline.HeaderText = "Source Type"
        Me.SourceType_Offline.Name = "SourceType_Offline"
        Me.SourceType_Offline.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SourceType_Offline.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SourceType_Offline.Width = 79
        '
        'TargetType_Offline
        '
        Me.TargetType_Offline.HeaderText = "Target Type"
        Me.TargetType_Offline.Name = "TargetType_Offline"
        Me.TargetType_Offline.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TargetType_Offline.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TargetType_Offline.Width = 76
        '
        'Seperator_Offline
        '
        Me.Seperator_Offline.HeaderText = "Seperator"
        Me.Seperator_Offline.Name = "Seperator_Offline"
        Me.Seperator_Offline.Width = 97
        '
        'PartOfSubstring_Offline
        '
        Me.PartOfSubstring_Offline.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PartOfSubstring_Offline.HeaderText = "Part Of Substring"
        Me.PartOfSubstring_Offline.Items.AddRange(New Object() {"left", "right"})
        Me.PartOfSubstring_Offline.Name = "PartOfSubstring_Offline"
        Me.PartOfSubstring_Offline.Width = 109
        '
        'StaticValue_Offline
        '
        Me.StaticValue_Offline.HeaderText = "Static Value for Target Column"
        Me.StaticValue_Offline.Name = "StaticValue_Offline"
        Me.StaticValue_Offline.ToolTipText = "If there is no fitting source, you can set a static value, which will be set for " &
    "every row, here."
        Me.StaticValue_Offline.Width = 159
        '
        'XMLAttribute
        '
        Me.XMLAttribute.HeaderText = "Attribute"
        Me.XMLAttribute.Name = "XMLAttribute"
        Me.XMLAttribute.ToolTipText = "If the source value should be extracted as attribute of a XML-Entity insert the a" &
    "ttribute name here"
        Me.XMLAttribute.Visible = False
        Me.XMLAttribute.Width = 78
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'L_OrderID
        '
        Me.L_OrderID.AutoSize = True
        Me.L_OrderID.Location = New System.Drawing.Point(13, 40)
        Me.L_OrderID.Name = "L_OrderID"
        Me.L_OrderID.Size = New System.Drawing.Size(66, 19)
        Me.L_OrderID.TabIndex = 48
        Me.L_OrderID.Text = "Order ID:"
        '
        'B_Load
        '
        Me.B_Load.Location = New System.Drawing.Point(12, 842)
        Me.B_Load.Name = "B_Load"
        Me.B_Load.Size = New System.Drawing.Size(194, 31)
        Me.B_Load.TabIndex = 49
        Me.B_Load.Text = "Load Configuration..."
        Me.B_Load.UseVisualStyleBackColor = True
        '
        'Konfiguration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1219, 885)
        Me.Controls.Add(Me.MappingGrid_Offline)
        Me.Controls.Add(Me.C_IDlessBatch)
        Me.Controls.Add(Me.C_CheckConsistency)
        Me.Controls.Add(Me.B_Load)
        Me.Controls.Add(Me.L_OrderID)
        Me.Controls.Add(Me.T_OrderID)
        Me.Controls.Add(Me.C_Silent)
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
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Konfiguration"
        Me.Text = "DUCKQL - Configuration"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PB_Source, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PB_Target, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MappingGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MappingGrid_Offline, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents L_SourceServerAdress As Windows.Forms.Label
    Friend WithEvents T_SourceSQLFilter As Windows.Forms.TextBox
    Friend WithEvents T_SourceFilterValue As Windows.Forms.TextBox
    Friend WithEvents T_SourceFilterColumn As Windows.Forms.TextBox
    Friend WithEvents C_SourceFilterType As Windows.Forms.ComboBox
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
    Friend WithEvents L_SourceIDColumn As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents L_SourceConnectionType As Windows.Forms.Label
    Friend WithEvents L_SourcePassword As Windows.Forms.Label
    Friend WithEvents L_SourceUsername As Windows.Forms.Label
    Friend WithEvents L_SourceDB As Windows.Forms.Label
    Friend WithEvents L_PartOfSubstringUse As Windows.Forms.Label
    Friend WithEvents C_TargetPartSubstring As Windows.Forms.ComboBox
    Friend WithEvents L_TargetSeperator As Windows.Forms.Label
    Friend WithEvents T_TargetSeperator As Windows.Forms.TextBox
    Friend WithEvents C_MapIDValue As Windows.Forms.CheckBox
    Friend WithEvents Label21 As Windows.Forms.Label
    Friend WithEvents T_TargetTimestampfield As Windows.Forms.TextBox
    Friend WithEvents L_TargetIDColumn As Windows.Forms.Label
    Friend WithEvents Label13 As Windows.Forms.Label
    Friend WithEvents C_TargetServerType As Windows.Forms.ComboBox
    Friend WithEvents L_TargetConnectionType As Windows.Forms.Label
    Friend WithEvents T_TargetServerAdress As Windows.Forms.TextBox
    Friend WithEvents L_TargetPassword As Windows.Forms.Label
    Friend WithEvents T_TargetDB As Windows.Forms.TextBox
    Friend WithEvents L_TargetUsername As Windows.Forms.Label
    Friend WithEvents T_TargetUsername As Windows.Forms.TextBox
    Friend WithEvents L_TargetDB As Windows.Forms.Label
    Friend WithEvents T_TargetPassword As Windows.Forms.TextBox
    Friend WithEvents Label18 As Windows.Forms.Label
    Friend WithEvents C_TargetConnectionType As Windows.Forms.ComboBox
    Friend WithEvents L_TargetServerAdress As Windows.Forms.Label
    Friend WithEvents T_TargetTable As Windows.Forms.TextBox
    Friend WithEvents FolderBrowserDialog1 As Windows.Forms.FolderBrowserDialog
    Friend WithEvents C_DeleteAllowed As Windows.Forms.CheckBox
    Friend WithEvents C_UpdateAllowed As Windows.Forms.CheckBox
    Friend WithEvents C_InsertAllowed As Windows.Forms.CheckBox
    Friend WithEvents L_PartOfSubString As Windows.Forms.Label
    Friend WithEvents MappingGrid As Windows.Forms.DataGridView
    Friend WithEvents B_Save As Windows.Forms.Button
    Friend WithEvents C_DebugLog As Windows.Forms.CheckBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents PB_Source As Windows.Forms.PictureBox
    Friend WithEvents PB_Target As Windows.Forms.PictureBox
    Friend WithEvents C_Silent As Windows.Forms.CheckBox
    Friend WithEvents ToolTipKonfig As Windows.Forms.ToolTip
    Friend WithEvents B_SourcePath As Windows.Forms.Button
    Friend WithEvents L_SourcePath As Windows.Forms.Label
    Friend WithEvents T_SourcePath As Windows.Forms.TextBox
    Friend WithEvents B_TargetPath As Windows.Forms.Button
    Friend WithEvents L_TargetPath As Windows.Forms.Label
    Friend WithEvents T_TargetPath As Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog1 As Windows.Forms.OpenFileDialog
    Friend WithEvents L_SourceIDDataType As Windows.Forms.Label
    Friend WithEvents C_SourceIDDatatype As Windows.Forms.ComboBox
    Friend WithEvents L_TargetIDDatatype As Windows.Forms.Label
    Friend WithEvents C_TargetIDDatatype As Windows.Forms.ComboBox
    Friend WithEvents C_SourceTable As Windows.Forms.ComboBox
    Friend WithEvents C_SourceIDColumn As Windows.Forms.ComboBox
    Friend WithEvents C_TargetTable As Windows.Forms.ComboBox
    Friend WithEvents C_TargetIDColumn As Windows.Forms.ComboBox
    Friend WithEvents C_SourceFilterColumn As Windows.Forms.ComboBox
    Friend WithEvents C_TargetTimestampfield As Windows.Forms.ComboBox
    Friend WithEvents T_OrderID As Windows.Forms.TextBox
    Friend WithEvents L_OrderID As Windows.Forms.Label
    Friend WithEvents B_Load As Windows.Forms.Button
    Friend WithEvents C_CheckConsistency As Windows.Forms.CheckBox
    Friend WithEvents C_IDlessBatch As Windows.Forms.CheckBox
    Friend WithEvents SourceColumn As Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents TargetColumn As Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents SourceType As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TargetType As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Seperator As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PartOfSubstring As Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents StaticValue As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MappingGrid_Offline As Windows.Forms.DataGridView
    Friend WithEvents B_TargetSaveFile As Windows.Forms.Button
    Friend WithEvents SourceColumn_Offline As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TargetColumn_Offline As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SourceType_Offline As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TargetType_Offline As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Seperator_Offline As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PartOfSubstring_Offline As Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents StaticValue_Offline As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents XMLAttribute As Windows.Forms.DataGridViewTextBoxColumn
End Class
