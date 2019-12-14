'--------------------------------------------------Summary--------------------------------------------------
' This is the SQL Base Function Class. Here all generic SQL Handling is coded.
'----------------------------------------------------------------------------------------------------------

Imports System.Data.SqlClient
Imports System.Collections
Imports System
Imports System.IO
Imports MySql
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports Nest
Imports Elasticsearch.Net

Public Class MyDataConnector
    'Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Public SQLCon As SqlConnection
    Private CurrentSQLDataadapter As SqlDataAdapter
    Public ENV As ENV
    Public SQLLog As LOG

    Public MySQLCon As MySqlConnection
    Private MySQLcmd As New MySqlCommand
    Private mySQLReader As MySqlDataReader
    Private CurrentMySQLDataadapter As MySqlDataAdapter

    Public AccessCon As OleDb.OleDbConnection
    Private AccessCmd As OleDb.OleDbCommand
    Private AccessReader As OleDb.OleDbDataReader
    Private CurrentAccessDataadapter As OleDb.OleDbDataAdapter

    'CSV could work with Access Object as well, but I implemented a more strict sepearation, in order to have a better overview and handling of the code
    Public CSVCon As OleDb.OleDbConnection
    Private CSVCmd As OleDb.OleDbCommand
    Private CSVReader As OleDb.OleDbDataReader
    Private CurrentCSVDataadapter As OleDb.OleDbDataAdapter

    Private ELClient As ElasticClient
    Private ELNode As Uri
    Private ELConfig As ConnectionSettings


    Public Setting As SQLServerSettings
    Public Testmode As Boolean = False

    Public TableSchema As New TableSchema
    Public Tables As New LinkedList(Of TableSchema)

    '--------------------------------------------------Initializing the class-------------------------------------------------
    Public Sub SetENV(ENV)
        Me.ENV = ENV
    End Sub

    Public Sub SetLog(LOG)
        Me.SQLLog = LOG
    End Sub
    '--------------------------------------------------------------------------------------------------------------------------

    '--------------------------------------------------SQL Server Connection----------------------------------------------------------------------------------------------
    ' More or less every Procect uses it's own connection Strings and Parameters, with all kinds of different options.
    ' I used https://www.connectionstrings.com/ to look up the connection strings for all supported products.

    '--------------------------------------------------MySQL-------------------------------------------------------------------
    Public Function ConnectMySQL(sServer As String, sDB As String, sUsername As String, Optional sPW As String = "") As MySqlConnection
        If sPW = "" Then
            MySQLCon = New MySqlConnection("Server=" & sServer & ";Database=" & sDB & ";Uid=" & sUsername & ";" & "SslMode=None;Convert Zero Datetime=True;")
        Else
            MySQLCon = New MySqlConnection("Server=" & sServer & ";Database=" & sDB & ";Uid=" & sUsername & ";Pwd=" & sPW & ";" & "SslMode=None;Convert Zero Datetime=True;")
        End If

        Try
            MySQLCon.Open()
            SQLLog.Write(1, "Connected with " & sServer & "\" & sDB)
            MySQLCon.Close()
            ConnectMySQL = MySQLCon
        Catch ex As Exception
            SQLLog.Write(0, ex.Message)
            ConnectMySQL = Nothing
        End Try
    End Function
    '-------------------------------------------------------------------------------------------------------------------------

    '--------------------------------------------------Microsoft SQL Server with Username and Password------------------------
    Private Function ConnectDBByUser(sServer As String, sUser As String, sPw As String, sDB As String) As SqlConnection
        'Dim SQLLog As LOG = Module1.Core.CurrentLog
        SQLLog.Write(1, "Connecting to SQL Server...")
        Try
            'Create a Connection object.
            SQLCon = New SqlConnection("Initial Catalog=" & sDB & ";" &
                    "Data Source=" & sServer & ";User Id=" & sUser & "; Password=" & sPw & ";")
            'Create a Command object.
            myCmd = SQLCon.CreateCommand
            myCmd.CommandText = "USE " & sDB

            SQLCon.Open()
            SQLLog.Write(1, "Established connection to:" & sServer & "." & sDB)
            ConnectDBByUser = SQLCon
        Catch ex As Exception
            SQLLog.Write(0, "ERROR!: " & Err.Description)
            ConnectDBByUser = Nothing
            Exit Function
        End Try
        SQLCon.Close()
    End Function
    '------------------------------------------------------------------------------------------------------------------------

    '--------------------------------------------------Microsoft SQL Server with Windows Authentification--------------------
    Private Function ConnectDBTrusted(sServer, sDB) As SqlConnection
        'Attention: To use this, the program has to run under the same user you intend to use for authentificate on the sql server

        SQLLog.Write(1, "Connecting to SQL Server...")
        Try
            'Create a Connection object.
            Me.SQLCon = New SqlConnection("Server=" & sServer & ";Database=" & sDB & ";Trusted_Connection=True;;")
            'Create a Command object.
            myCmd = SQLCon.CreateCommand
            myCmd.CommandText = "USE " & sDB

            SQLCon.Open()
            SQLLog.Write(1, "Established connection to:" & sServer & "." & sDB)
            ConnectDBTrusted = SQLCon
        Catch ex As Exception
            SQLLog.Write(0, "ERROR!: " & ex.Message)
            ConnectDBTrusted = Nothing
            Exit Function
        End Try
        SQLCon.Close()
    End Function
    '------------------------------------------------------------------------------------------------------------------------

    '--------------------------------------------------Microsoft Access------------------------------------------------------
    Private Function ConnectDBAccess(Path As String, sDB As String) As OleDb.OleDbConnection
        ' This one restrictes the application to x86 Architecture (32 bit Application) 
        ' After some research it seems, that the Microsoft.ACE.OLEDB.12.0 is only availabe in a 32bit Version
        ' Found the solution in this blog post https://blogs.technet.microsoft.com/austria/2014/02/06/der-microsoft-ace-oledb-12-0-provider-fehlt/
        ' 
        SQLLog.Write(1, "Connecting to Access DB...")
        Try
            'Create a Connection object.
            Me.AccessCon = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Path & ";Persist Security Info=False;")
            'Create a Command object.
            Me.AccessCmd = Me.AccessCon.CreateCommand
            Me.AccessCmd.CommandText = "USE " & sDB

            Me.AccessCon.Open()
            SQLLog.Write(1, "Established connection to:" & Path)
            ConnectDBAccess = Me.AccessCon
        Catch ex As Exception
            SQLLog.Write(0, "ERROR!: " & ex.Message)
            ConnectDBAccess = Nothing
            Exit Function
        End Try
        Me.AccessCon.Close()
    End Function
    '------------------------------------------------------------------------------------------------------------------------

    '--------------------------------------------------CSV------------------------------------------------------
    Private Function ConnectDBCSV(Path As String, sDB As String) As OleDb.OleDbConnection
        ' This one restrictes the application to x86 Architecture (32 bit Application) 
        ' After some research it seems, that the Microsoft.ACE.OLEDB.12.0 is only availabe in a 32bit Version
        ' Found the solution in this blog post https://blogs.technet.microsoft.com/austria/2014/02/06/der-microsoft-ace-oledb-12-0-provider-fehlt/
        ' 
        SQLLog.Write(1, "Connecting to CSV...")

        If IsNothing(Setting.Direction) = True Then
        Else
            If Me.Setting.Direction.ToUpper = "TARGET" Then
                Try
                    Dim myWriter As New StreamWriter(Path, False)
                    Dim headerString As String = ""
                    For Each Mapping In Me.ENV.Mappings
                        If headerString = "" Then
                            headerString = Chr(34) & Mapping.Targetname & Chr(34)
                        Else
                            headerString = headerString & ";" & Chr(34) & Mapping.Targetname & Chr(34)
                        End If
                    Next
                    headerString = headerString & ";" & vbCrLf
                    myWriter.WriteLine(headerString)
                    myWriter.Close()
                Catch ex As Exception
                    SQLLog.Write(0, "ERROR while writing csv file: " & ex.Message)
                    ConnectDBCSV = Nothing
                    Exit Function
                End Try
            Else
            End If
        End If
        Path = Path.Substring(0, Path.LastIndexOf("\"))

        Try
            'Create a Connection object.
            Me.CSVCon = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Path & ";Extended Properties='text;HDR=Yes;FMT=Delimited';")
            'Create a Command object.
            Me.CSVCmd = Me.CSVCon.CreateCommand
            Me.CSVCmd.CommandText = "USE " & sDB

            Me.CSVCon.Open()
            SQLLog.Write(1, "Established connection to:" & Path)
            ConnectDBCSV = Me.CSVCon
        Catch ex As Exception
            SQLLog.Write(0, "ERROR!: " & ex.Message)
            ConnectDBCSV = Nothing
            Exit Function
        End Try
        Me.CSVCon.Close()
    End Function

    Public Sub RewriteCSVForXMLData(SourceXMLStartLayerLookup As Integer)
        'This Sub Rewrites the csv header for XML Data which is normally spread in several layers
        Dim myWriter As StreamWriter
        Dim headerString As String = ""
        Try
            myWriter = New StreamWriter(Setting.FilePath, False)

        Catch ex As Exception
            SQLLog.Write(0, "ERROR while rewriting csv file: " & ex.Message)
            Exit Sub
        End Try

        If Module1.Core.Reihen.Count <= 0 Then
            Exit Sub
        End If


        'The Starting Point for i is defined by the starting layer from the source setting
        Dim i As Integer = SourceXMLStartLayerLookup

        'From the starting point to the maximum depth, which has been extracted from the xml, there will be columns written
        'The target layout should be in a schematic way look like: column1/layer1,column2/layer1,column1/layer2,column2/layer2...and so on...
        While i <= Module1.Core.MaxLayers
            For Each Mapping In Module1.Core.CurrentENV.Mappings
                If headerString = "" Then
                    headerString = Mapping.Targetname & "_" & i
                Else
                    headerString = headerString & "," & Mapping.Targetname & "_" & i
                End If
            Next
            i = i + 1
        End While

        Try
            headerString = headerString & "," & vbCrLf
            myWriter.WriteLine(headerString)
            myWriter.Close()
        Catch ex As Exception
            SQLLog.Write(0, "ERROR while rewriting csv file: " & ex.Message)
            Exit Sub
        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------------------

    Public Sub CreateSQLCon()
        ' Creates SQL connection depending on the servertype on the Settings-Object.
        ' Note: No XML Connection will be done here, since the whole XML Datafile Handling is done within the SQLOperations Class.
        Select Case Setting.Servertype
            Case "MSSQL"
                Select Case Setting.ConnMode
                    Case "Normal"
                        Me.SQLCon = ConnectDBByUser(Setting.Servername, Setting.User, Setting.Password, Setting.SQLDB)
                        Exit Sub

                    Case "Trusted"
                        Me.SQLCon = ConnectDBTrusted(Setting.Servername, Setting.SQLDB)
                        Exit Sub

                    Case Else
                        ' Microsoft provides several different mehtods to connect to you sql server
                        ' for the start I provided only two: Username & Password or Trusted
                        SQLLog.Write(0, "Unkown Connection Method")
                        Exit Sub
                End Select
            Case "MySQL"
                ' MySQL provides the option to not use a password for the DB User. So I've implemented two options:
                ' Username and Password or just a User, assume that there is no Password for the User you want to use.
                If IsNothing(Setting.Password) Or Setting.Password = "" Then
                    Me.MySQLCon = ConnectMySQL(Setting.Servername, Setting.SQLDB, Setting.User)
                Else
                    Me.MySQLCon = ConnectMySQL(Setting.Servername, Setting.SQLDB, Setting.User, Setting.Password)
                End If
            Case "MS-SQL"
                Select Case Setting.ConnMode
                    Case "Normal"
                        Me.SQLCon = ConnectDBByUser(Setting.Servername, Setting.User, Setting.Password, Setting.SQLDB)
                        Exit Sub

                    Case "Trusted"
                        Me.SQLCon = ConnectDBTrusted(Setting.Servername, Setting.SQLDB)
                        Exit Sub

                    Case Else
                        ' Microsoft provides several different mehtods to connect to you sql server
                        ' for the start I provided only two: Username & Password or Trusted
                        SQLLog.Write(0, "Unkown Connection Method")
                        Exit Sub
                End Select
            Case "Access"
                Select Case Setting.ConnMode
                    Case "Normal"
                        Me.AccessCon = ConnectDBAccess(Setting.FilePath, Setting.SQLDB)
                    Case "Standard Security"
                        Me.AccessCon = ConnectDBAccess(Setting.FilePath, Setting.SQLDB)
                End Select
            Case "CSV"
                Select Case Setting.ConnMode
                    Case Else
                        Me.CSVCon = ConnectDBCSV(Setting.FilePath, Setting.SQLDB)
                End Select
            Case "Elastic Search"
                ConnectToElasticSearch()
            Case Else
                SQLLog.Write(1, Setting.Servertype & " choosen no Dataconnection neccessary.")
        End Select
    End Sub

    Private Function ConnectToElasticSearch() As Boolean

        Dim TMPNode As New Uri(Setting.Servername)
        Dim TMPConfig As New ConnectionSettings(TMPNode)
        TMPConfig.DefaultIndex(Setting.SQLTable)
        Dim Client As New ElasticClient(TMPConfig)

        Dim response As PingResponse = Client.Ping

        If response.IsValid = True Then
            Me.ELClient = Client
            Me.ELConfig = TMPConfig
            Me.ELNode = TMPNode

            SQLLog.Write(1, "Connected to " & Setting.Servername)
            Return True
        Else
            SQLLog.Write(0, response.OriginalException.Message)
        End If


        Return False
    End Function

    '----------------------------------------------------------------------------------------------------------------------------------------------------------------

    '--------------------------------------------------Query and Data Handling---------------------------------------------------------------------------------------
    ' Here it all gets a bit messed up... but it works ;)
    ' I grew up in an Access/VBA environment, so the Recordset Object was top of the line for me.
    ' I recently read about some other methods to read data from a table, some might be faster in batch mode, than the method I used here.
    ' Feel free to improve this. I would love to learn from you guys!
    Public Function CreateDataAdapter(SQLrq As String) As DataSet
        ' Of course diffenrent Products need differnt objects here as well.
        ' SQLrq is the query variable
        Select Case Setting.Servertype
            Case "MSSQL"
                Dim dataadapter As New SqlDataAdapter(SQLrq, SQLCon)
                Dim ds As New DataSet()
                Try
                    SQLCon.Open()
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                    CreateDataAdapter = Nothing
                    Exit Function
                End Try
                Try
                    dataadapter.Fill(ds, Module1.Core.CurrentENV.GetName())
                    SQLCon.Close()
                    Me.CurrentSQLDataadapter = dataadapter
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                    CreateDataAdapter = Nothing
                    Exit Function
                End Try
                dataadapter = Nothing
                Return ds
            Case "MS-SQL"
                Dim dataadapter As New SqlDataAdapter(SQLrq, SQLCon)
                Dim ds As New DataSet()
                Try
                    SQLCon.Open()
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                    CreateDataAdapter = Nothing
                    Exit Function
                End Try

                Try
                    dataadapter.Fill(ds, Module1.Core.CurrentENV.GetName())
                    SQLCon.Close()
                    Me.CurrentSQLDataadapter = dataadapter
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                    CreateDataAdapter = Nothing
                    Exit Function
                End Try
                dataadapter = Nothing
                Return ds
            Case "MySQL"
                Try
                    Dim TmpCon As New MySqlConnection

                    If Not MySQLCon.State = ConnectionState.Open Or Not MySQLCon.State = ConnectionState.Closed Then
                        SQLLog.Write(1, "Main Connection is working, creating temporary connection...")
                        Select Case Setting.ConnMode
                            Case "Normal"
                                TmpCon = ConnectMySQL(Setting.Servername, Setting.SQLDB, Setting.User, Setting.Password)
                        End Select
                        If TmpCon.State = ConnectionState.Open Then
                            TmpCon.Close()
                        End If
                        Dim dataadapter As New MySqlDataAdapter(SQLrq, TmpCon)
                        Dim ds As New DataSet
                        dataadapter.Fill(ds)
                        CreateDataAdapter = ds
                        Me.CurrentMySQLDataadapter = dataadapter
                    Else
                        If MySQLCon.State = ConnectionState.Open Then
                            MySQLCon.Close()
                        End If
                        Dim dataadapter As New MySqlDataAdapter(SQLrq, MySQLCon)
                        Dim ds As New DataSet
                        dataadapter.Fill(ds)
                        CreateDataAdapter = ds
                        Me.CurrentMySQLDataadapter = dataadapter
                    End If
                    Exit Function
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                End Try
            Case "Access"
                Try
                    If AccessCon.State = ConnectionState.Open Then
                        AccessCon.Close()
                    End If
                    Dim dataadapter As New OleDb.OleDbDataAdapter(SQLrq, AccessCon)
                    Dim ds As New DataSet
                    dataadapter.Fill(ds)
                    CreateDataAdapter = ds
                    Me.CurrentAccessDataadapter = dataadapter
                    Exit Function
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                End Try
            Case "CSV"
                Try
                    If CSVCon.State = ConnectionState.Open Then
                        CSVCon.Close()
                    End If
                    Dim dataadapter As New OleDb.OleDbDataAdapter(SQLrq, CSVCon)
                    Dim ds As New DataSet
                    dataadapter.Fill(ds)
                    CreateDataAdapter = ds
                    Me.CurrentCSVDataadapter = dataadapter
                    Exit Function
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                End Try

            Case "Elastic Search"
                Try

                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                End Try

        End Select
        CreateDataAdapter = Nothing
    End Function

    Public Function WriteDataset(DS As DataSet) As Integer
        Dim RowsChanged As Integer = 0
        Select Case Setting.Servertype
            Case "MSSQL"
                Try
                    SQLCon.Open()
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                    Return Nothing
                End Try
                Try
                    CurrentSQLDataadapter.TableMappings.Add(Setting.SQLTable, DS.Tables(0).TableName)
                    RowsChanged = CurrentSQLDataadapter.Update(DS, Setting.SQLTable)
                    SQLCon.Close()
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                    Return Nothing
                End Try
            Case "MS-SQL"
                Try
                    SQLCon.Open()
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                    Return Nothing
                End Try
                Try
                    Dim builder As SqlCommandBuilder = New SqlCommandBuilder(CurrentSQLDataadapter)
                    builder.QuotePrefix = "["
                    builder.QuoteSuffix = "]"
                    builder.SetAllValues = True
                    SQLLog.Write(1, builder.GetInsertCommand().CommandText)
                    CurrentSQLDataadapter.TableMappings.Add(Setting.SQLTable, DS.Tables(0).TableName)
                    RowsChanged = CurrentSQLDataadapter.Update(DS, Setting.SQLTable)
                    SQLCon.Close()
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                    Return Nothing
                End Try
            Case "MySQL"
                Try
                    If MySQLCon.State = ConnectionState.Open Then
                        MySQLCon.Close()
                    End If
                    RowsChanged = CurrentMySQLDataadapter.Update(DS, Setting.SQLTable)
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                    Return Nothing
                End Try
            Case "Access"
                Try
                    If AccessCon.State = ConnectionState.Open Then
                        AccessCon.Close()
                    End If
                    RowsChanged = CurrentAccessDataadapter.Update(DS, Setting.SQLTable)
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                    Return Nothing
                End Try
            Case "CSV"
                Try
                    If CSVCon.State = ConnectionState.Open Then
                        CSVCon.Close()
                    End If
                    RowsChanged = CurrentCSVDataadapter.Update(DS, Setting.SQLTable)
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                    Return Nothing
                End Try
            Case "Elastic Search"

        End Select
        Return RowsChanged
    End Function

    Public Sub CopyDataToMSSQL()
        If SQLCon.State = ConnectionState.Open Then
            SQLCon.Close()
        End If
        SQLCon.Open()

        Using BulkCopy As New SqlBulkCopy(SQLCon) With {
            .NotifyAfter = Me.Setting.NotificationRows,
            .BatchSize = Me.Setting.Max_Paket,
            .BulkCopyTimeout = 0
        }

            If Me.Setting.UpdateAllowed = True Then
                If Me.Setting.TmpTableAllowed = True Then
                    BulkCopy.DestinationTableName = "[" & Me.Setting.TmpSQLTable & "]"
                Else
                    SQLLog.Write(0, "Config ERROR: No TmpTables allowed for BULK Update")
                End If
            Else
                BulkCopy.DestinationTableName = Me.Setting.SQLTable
            End If

            AddHandler BulkCopy.SqlRowsCopied, AddressOf BulkRowsCopied_Notifier
            For Each Mapping In Module1.Core.Mappings
                Dim BCMapping As New SqlBulkCopyColumnMapping With {
                    .DestinationColumn = Mapping.Targetname,
                    .DestinationOrdinal = Mapping.TargetOrdinal,
                    .SourceColumn = Mapping.Targetname,
                    .SourceOrdinal = Mapping.SourceOrdinal
                }
                BulkCopy.ColumnMappings.Add(BCMapping)
            Next
            Try
                BulkCopy.WriteToServer(Module1.Core.TargetDataTable)
            Catch ex As Exception
                SQLLog.Write(0, "Error while syncing data to sql server: " & ex.Message)
            End Try
            BulkCopy.Close()
        End Using
        If SQLCon.State = ConnectionState.Open Then
            SQLCon.Close()
        End If

        If Me.Setting.UpdateAllowed = True Then
            If Me.Setting.TmpTableAllowed = True Then
                MergeMSSQLTables()
            Else
                SQLLog.Write(0, "Config ERROR: No TmpTables allowed for BULK Update")
            End If
        End If


    End Sub

    Private Sub MergeMSSQLTables()
        Dim SB As New System.Text.StringBuilder

        SB.Append("MERGE [" & Me.Setting.SQLTable & "] AS TARGET ")
        SB.Append("USING [" & Me.Setting.TmpSQLTable & "] As SOURCE ")
        If Me.TableSchema.HasDBKeyColumns = True Then                                               'By default the predefined Key Columns of the Tableschema will be used
            For Each Column In Me.TableSchema.Columns                                               'If these Keys are not available, the program uses the Columns that have been
                If Column.IsKey Then                                                                'defined as Identifier by the user    
                    SB.Append("On (TARGET.[" & Column.Name & "] = SOURCE.[" & Column.Name & "]")
                End If
            Next
        Else
            For Each Column In Me.TableSchema.Columns
                If Column.IsUserDefinedIdentifier Then
                    SB.Append("On (TARGET.[" & Column.Name & "] = SOURCE.[" & Column.Name & "]")
                End If
            Next
        End If

        SB.Append(")")
        SB.Append(" WHEN MATCHED AND ")
        Dim i As Integer = 0
        For Each Column In ENV.Mappings
            If i + 1 = ENV.Mappings.Count Then
                SB.Append("Target.[" & Column.Targetname & "] <>" & "SOURCE.[" & Column.Targetname & "] OR ")
                SB.Append("Source.[" & Column.Targetname & "] IS NULL OR ")
                SB.Append("Target.[" & Column.Targetname & "] IS NULL ")
            Else
                SB.Append("Target.[" & Column.Targetname & "] <>" & "SOURCE.[" & Column.Targetname & "] OR ")
                SB.Append("Source.[" & Column.Targetname & "] IS NULL OR ")
                SB.Append("Target.[" & Column.Targetname & "] IS NULL OR ")
            End If
            i = i + 1
        Next
        i = 0
        SB.Append(" THEN UPDATE SET ")
        For Each Column In ENV.Mappings
            If i + 1 = ENV.Mappings.Count Then
                SB.Append("Target.[" & Column.Targetname & "] = " & "SOURCE.[" & Column.Targetname & "]")
            Else
                SB.Append("Target.[" & Column.Targetname & "] = " & "SOURCE.[" & Column.Targetname & "],")
            End If
            i = i + 1
        Next
        i = 0
        If Me.Setting.InsertAllowed = True Then
            SB.Append(" WHEN Not MATCHED BY TARGET THEN INSERT (")
            For Each Column In ENV.Mappings
                If i + 1 = ENV.Mappings.Count Then
                    SB.Append("[" & Column.Targetname & "]")
                Else
                    SB.Append("[" & Column.Targetname & "], ")
                End If
                i = i + 1
            Next
            i = 0
            SB.Append(") VALUES (")
            For Each Column In ENV.Mappings
                If i + 1 = ENV.Mappings.Count Then
                    SB.Append("SOURCE.[" & Column.Targetname & "]")
                Else
                    SB.Append("SOURCE.[" & Column.Targetname & "], ")
                End If
                i = i + 1
            Next
            SB.Append(")")
        End If
        If Me.Setting.DeleteAllowed = True Then
            SB.Append(" WHEN NOT MATCHED BY SOURCE ")
            SB.Append(" THEN DELETE ")
        End If
        SB.Append(";")
        ExecuteQuery(SB.ToString)
    End Sub


    Private Sub BulkRowsCopied_Notifier(Sender As Object, e As SqlRowsCopiedEventArgs)
        SQLLog.Write(1, e.RowsCopied & " rows copied to SQL Server")
    End Sub


    Public Async Function ExecuteQueryAsync(SQLQueryBlock As SQLQueryBlock) As Task(Of Boolean)
        'Writes one ore more querys asynchron to database
        'Note: No XML Datafile handling here, since the XML Datafiles are handled in the SQLoperations-Class
        Dim Res As String = ""

        Dim RowsAffected As Integer = 0

        SQLLog.Write(1, "Executing Query " & SQLQueryBlock.GetQueryString)

        Select Case Setting.Servertype
        ' Of course diffenrent Products need differnt objects here as well.
            Case "MSSQL"
                Try
                    If SQLCon.State = ConnectionState.Open Then
                        SQLCon.Close()
                    End If
                    myCmd.CommandText = SQLQueryBlock.GetQueryString
                    SQLCon.Open()
                    SQLQueryBlock.HasBeenSendToSQLServer = True
                    RowsAffected = Await myCmd.ExecuteNonQueryAsync
                    SQLLog.Write(1, RowsAffected & " Row(s) affected.")
                    SQLCon.Close()
                    Return True
                Catch e As Exception
                    SQLQueryBlock.e = e
                    SQLQueryBlock.Success = False

                    SQLLog.Write(0, e.Message & " - Query was: " & SQLQueryBlock.GetQueryString)
                    SQLCon.Close()
                    Return False
                    Exit Function
                End Try
            Case "MS-SQL"
                Try
                    If SQLCon.State = ConnectionState.Open Then
                        SQLCon.Close()
                    End If
                    myCmd.CommandText = SQLQueryBlock.GetQueryString
                    SQLCon.Open()
                    SQLQueryBlock.HasBeenSendToSQLServer = True
                    RowsAffected = Await myCmd.ExecuteNonQueryAsync
                    SQLLog.Write(1, RowsAffected & " Row(s) affected.")
                    SQLCon.Close()
                    Return True
                Catch e As Exception
                    SQLQueryBlock.e = e
                    SQLQueryBlock.Success = False

                    SQLLog.Write(0, e.Message & " - Query was: " & SQLQueryBlock.GetQueryString)
                    SQLCon.Close()
                    Return False
                    Exit Function
                End Try
            Case "MySQL"
                Try
                    If MySQLCon.State = ConnectionState.Open Then
                        MySQLCon.Close()
                    End If

                    MySQLcmd.CommandText = SQLQueryBlock.GetQueryString
                    MySQLCon.Open()
                    If SQLQueryBlock.SQLqueryRunning = True Then
                        Return False
                    Else
                        SQLQueryBlock.SQLqueryRunning = True
                    End If
                    SQLQueryBlock.HasBeenSendToSQLServer = True
                    MySQLcmd.Connection = MySQLCon
                    RowsAffected = Await MySQLcmd.ExecuteNonQueryAsync
                    SQLQueryBlock.SQLqueryRunning = False
                    SQLLog.Write(1, RowsAffected & " Row(s) affected.")
                    MySQLCon.Close()
                    Return True
                Catch e As Exception
                    SQLQueryBlock.e = e
                    SQLQueryBlock.Success = False
                    SQLLog.Write(0, e.Message & " - Query was: " & SQLQueryBlock.GetQueryString)
                    MySQLCon.Close()
                    Return False
                    Exit Function
                End Try
            Case "Access"
                Try
                    If AccessCon.State = ConnectionState.Open Then
                        AccessCon.Close()
                    End If
                    AccessCmd.CommandText = SQLQueryBlock.GetQueryString
                    AccessCon.Open()
                    SQLQueryBlock.HasBeenSendToSQLServer = True
                    RowsAffected = Await AccessCmd.ExecuteNonQueryAsync
                    SQLLog.Write(1, RowsAffected & " Row(s) affected.")
                    AccessCon.Close()
                    Return True
                Catch e As Exception
                    SQLQueryBlock.e = e
                    SQLQueryBlock.Success = False

                    SQLLog.Write(0, e.Message & " - Query was: " & SQLQueryBlock.GetQueryString)
                    AccessCon.Close()
                    Return False
                    Exit Function
                End Try
            Case "CSV"
                Try
                    If CSVCon.State = ConnectionState.Open Then
                        CSVCon.Close()
                    End If
                    CSVCmd.CommandText = SQLQueryBlock.GetQueryString
                    CSVCon.Open()
                    SQLQueryBlock.HasBeenSendToSQLServer = True
                    RowsAffected = Await CSVCmd.ExecuteNonQueryAsync
                    SQLLog.Write(1, RowsAffected & " Row(s) affected.")
                    CSVCon.Close()
                    Return True
                Catch e As Exception
                    SQLQueryBlock.e = e
                    SQLQueryBlock.Success = False

                    SQLLog.Write(0, e.Message & " - Query was: " & SQLQueryBlock.GetQueryString)
                    CSVCon.Close()
                    Return False
                    Exit Function
                End Try
            Case "Elastic Search"
                Try

                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                    Return False
                End Try
            Case Else
                SQLLog.Write(0, "Execute Query - Unknown (Target) Servertype. 0 Querys were send.")
                Return Nothing
        End Select
        RowsAffected = 0
        Return RowsAffected
    End Function

    Public Function ExecuteQuery(SQLrq As String) As Boolean
        'Writes one ore more querys to database
        'Note: No XML Datafile handling here, since the XML Datafiles are handled in the SQLoperations-Class
        Dim Res As String = ""

        Dim RowsAffected As Integer = 0

        Select Case Setting.Servertype
        ' Of course diffenrent Products need differnt objects here as well.
        ' SQLrq is the query variable
            Case "MSSQL"
                Try
                    If SQLCon.State = ConnectionState.Open Then
                        SQLCon.Close()
                    End If
                    myCmd.CommandText = SQLrq
                    SQLCon.Open()
                    RowsAffected = myCmd.ExecuteNonQuery
                    SQLLog.Write(1, RowsAffected & " Row(s) affected.")
                    SQLCon.Close()
                    ExecuteQuery = True
                Catch e As Exception
                    ExecuteQuery = False
                    SQLLog.Write(0, e.Message & " - Query was: " & SQLrq)
                    SQLCon.Close()
                    Exit Function
                End Try
            Case "MS-SQL"
                Try
                    If SQLCon.State = ConnectionState.Open Then
                        SQLCon.Close()
                    End If
                    myCmd.CommandText = SQLrq
                    SQLCon.Open()

                    RowsAffected = myCmd.ExecuteNonQuery
                    SQLLog.Write(1, RowsAffected & " Row(s) affected.")
                    SQLCon.Close()
                    ExecuteQuery = True
                Catch e As Exception
                    ExecuteQuery = False
                    SQLLog.Write(0, e.Message & " - Query was: " & SQLrq)
                    SQLCon.Close()
                    Exit Function
                End Try
            Case "MySQL"
                Try
                    If MySQLCon.State = ConnectionState.Open Then
                        MySQLCon.Close()
                    End If
                    MySQLcmd.CommandText = SQLrq
                    MySQLCon.Open()
                    MySQLcmd.Connection = MySQLCon
                    RowsAffected = MySQLcmd.ExecuteNonQuery
                    SQLLog.Write(1, RowsAffected & " Row(s) affected.")
                    MySQLCon.Close()
                    ExecuteQuery = True
                Catch e As Exception
                    ExecuteQuery = False
                    SQLLog.Write(0, e.Message & " - Query was: " & SQLrq)
                    MySQLCon.Close()
                    Exit Function
                End Try
            Case "Access"
                Try
                    If AccessCon.State = ConnectionState.Open Then
                        AccessCon.Close()
                    End If
                    AccessCmd.CommandText = SQLrq

                    AccessCon.Open()
                    RowsAffected = AccessCmd.ExecuteNonQuery
                    SQLLog.Write(1, RowsAffected & " Row(s) affected.")
                    AccessCon.Close()
                    ExecuteQuery = True
                Catch e As Exception
                    ExecuteQuery = False
                    SQLLog.Write(0, e.Message & " - Query was: " & SQLrq)
                    AccessCon.Close()
                    Exit Function
                End Try
            Case "CSV"
                Try
                    If CSVCon.State = ConnectionState.Open Then
                        CSVCon.Close()
                    End If
                    CSVCmd.CommandText = SQLrq
                    CSVCon.Open()
                    RowsAffected = CSVCmd.ExecuteNonQuery
                    SQLLog.Write(1, RowsAffected & " Row(s) affected.")
                    CSVCon.Close()
                    ExecuteQuery = True
                Catch e As Exception
                    ExecuteQuery = False
                    SQLLog.Write(0, e.Message & " - Query was: " & SQLrq)
                    CSVCon.Close()
                    Exit Function
                End Try
            Case "Elastic Search"
                Try
                    Dim Resp As IndexResponse = ELClient.LowLevel.Index(Of IndexResponse)(Setting.SQLTable, SQLrq)
                    If Resp.IsValid = False Then
                        SQLLog.Write(0, Resp.OriginalException.Message)
                        Return False
                    Else
                        Return True
                    End If
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                    Return False
                End Try
            Case Else
                SQLLog.Write(0, "Execute Query - Unknown (Target) Servertype. 0 Querys were send.")
                Return False
        End Select
        Return False
    End Function

    Public Function Elastic_IDQuery(ID As String) As DataSet
        Try
            Dim searchResponse = ELClient.LowLevel.Search(Of StringResponse)(Setting.SQLTable, PostData.Serializable(New With {Key .from = 0, Key .size = 10, Key .query = New With {Key .query_string = New With {Key .query = ID, Key .default_field = Setting.IDColumn}}}))
            If searchResponse.Success = False Then
                SQLLog.Write(0, searchResponse.OriginalException.Message)
                Return Nothing
            End If
            Dim Docs As New List(Of String)
            Dim JSONString As String = searchResponse.Body
            Dim JSONConv As New JSON_Serialization
            Dim DS As DataSet = JSONConv.Deserialize_EL_JSONtoDataset(JSONString)

            Return DS

        Catch ex As Exception
            SQLLog.Write(0, "ElasticID_Query - Error: " & ex.Message)
            Return Nothing
        End Try

        Return Nothing
    End Function

    '----------------------------------------------------------------------------------------------------------------------------------------------------------------

    '---------------------------------------------------------Masking Strings and Dates------------------------------------------------------------------------------
    ' To be honest, I saw this in a few code samples - unknown source.
    ' I think this is a very elegant and simple way to mask text, dates and NULL values.
    ' On this way Querys a much easier to read and write. 

    Public Function CSQL(ByVal SQL_Attribut As String, Optional ByVal VariableType As String = "String") As String
        ' This function gets a value and a target data type
        ' and masks the value, so that is sql inline for using it within the query

        If IsNothing(VariableType) = True Then
            VariableType = "String"
        End If

        If IsNothing(SQL_Attribut) Or SQL_Attribut = vbNullString Then
            CSQL = "NULL"
            Exit Function
        Else
            Select Case VariableType.ToUpper

                Case "STRING", "8"
                    If InStr(1, SQL_Attribut, "\") <> 0 Then
                        SQL_Attribut = Replace(CStr(SQL_Attribut), "\", "\\")
                    Else

                    End If

                    If InStr(1, SQL_Attribut, "'") <> 0 Then
                        CSQL = "'" & Replace(CStr(SQL_Attribut), "'", "''") & "'"
                        Exit Function
                    Else
                        CSQL = "'" & CStr(SQL_Attribut) & "'"
                        Exit Function
                    End If

                Case "INTEGER", "DOUBLE", "FLOAT", "3", "INT32", "INT64"
                    If IsNumeric(SQL_Attribut) Then
                        CSQL = CStr(SQL_Attribut)
                        Exit Function
                    Else
                        CSQL = "'" & CStr(SQL_Attribut) & "'"
                        Exit Function
                    End If

                Case "BOOLEAN"
                    If IsNumeric(SQL_Attribut) Then
                        If CBool(SQL_Attribut) Then
                            CSQL = "TRUE"
                            Exit Function
                        Else
                            CSQL = "FALSE"
                            Exit Function
                        End If
                    Else
                        Select Case SQL_Attribut
                            Case "Ja", "Wahr", "True", "Yes"
                                CSQL = "TRUE"
                                Exit Function
                            Case Else
                                CSQL = "FALSE"
                                Exit Function
                        End Select
                    End If

                Case "DATE"
                    If IsDate(SQL_Attribut) Then
                        CSQL = Format(CDate(SQL_Attribut), "\#mm\/dd\/yyyy\#")
                        Exit Function
                    Else
                        CSQL = "'" & SQL_Attribut & "'"
                        Exit Function
                    End If
                Case "TIME", "4"
                    If IsDate(SQL_Attribut) Then
                        CSQL = Format(CDate(SQL_Attribut), "\#hh:mm:ss#\")
                        Exit Function
                    Else
                        CSQL = "'" & SQL_Attribut & "'"
                        Exit Function
                    End If
                Case "DATETIME", "7"
                    If IsDate(SQL_Attribut) Then
                        Select Case Setting.Servertype
                            Case "MySQL"
                                CSQL = "'" & Format(CDate(SQL_Attribut), "yyyy/MM/dd H:mm:ss") & "'"
                            Case Else
                                CSQL = "'" & SQL_Attribut.Replace(".", "-") & "'"
                        End Select
                        Exit Function
                    Else
                        CSQL = "'" & SQL_Attribut & "'"
                        Exit Function
                    End If
            End Select

        End If
        CSQL = SQL_Attribut
    End Function
    '----------------------------------------------------------------------------------------------------------------------------------------------------------------

    Public Sub GetTableColumnNames()
        Dim DT As New DataTable
        Dim Fieldlist As New List(Of String)

        'ToDo: Do we really need to load a 150GB Table?  

        '---------------------------------Depending on the product----------------------------------------------------
        Select Case Setting.Servertype
            Case "MS-SQL"
                If SQLCon.State = ConnectionState.Open Then
                    SQLCon.Close()
                End If
                SQLCon.Open()
                myCmd.Connection = SQLCon
                myCmd.CommandTimeout = 0
                myCmd.CommandText = "SELECT * FROM " & Setting.SQLTable
                myReader = myCmd.ExecuteReader
                DT = myReader.GetSchemaTable
                myReader.Close()
                SQLCon.Close()
            Case "MSSQL"
                If SQLCon.State = ConnectionState.Open Then
                    SQLCon.Close()
                End If
                SQLCon.Open()
                myCmd.Connection = SQLCon
                myCmd.CommandText = "SELECT * FROM " & Setting.SQLTable
                myReader = myCmd.ExecuteReader
                DT = myReader.GetSchemaTable
                myReader.Close()
                SQLCon.Close()
            Case "MySQL"
                If MySQLCon.State = ConnectionState.Open Then
                    MySQLCon.Close()
                End If
                MySQLCon.Open()
                MySQLcmd.Connection = MySQLCon
                MySQLcmd.CommandText = "SELECT * FROM " & Setting.SQLTable
                mySQLReader = MySQLcmd.ExecuteReader
                DT = mySQLReader.GetSchemaTable
                mySQLReader.Close()
                MySQLCon.Close()
            Case "Access"
                If AccessCon.State = ConnectionState.Open Then
                    AccessCon.Close()
                End If
                AccessCon.Open()
                AccessCmd.Connection = AccessCon
                AccessCmd.CommandText = "SELECT * FROM " & Setting.SQLTable
                AccessReader = AccessCmd.ExecuteReader
                DT = AccessReader.GetSchemaTable
                AccessReader.Close()
                AccessCon.Close()
            Case "CSV"
                If CSVCon.State = ConnectionState.Open Then
                    CSVCon.Close()
                End If
                CSVCon.Open()
                CSVCmd.Connection = CSVCon
                CSVCmd.CommandText = "SELECT * FROM " & Setting.SQLTable
                CSVReader = CSVCmd.ExecuteReader
                DT = CSVReader.GetSchemaTable
                CSVReader.Close()
                CSVCon.Close()
        End Select
        '------------------------------------------------------------------------------------------------------------------------------------------

        Dim myField As DataRow
        Dim myFieldProperty As DataColumn
        TableSchema.Columns.Clear()
        For Each myField In DT.Rows
            Dim Column As New ColumnSchema
            For Each myFieldProperty In DT.Columns
                Select Case myFieldProperty.ColumnName
                    Case "ColumnName"
                        Column.Name = myField(myFieldProperty).ToString
                    Case "DataType"
                        Column.DataType = myField(myFieldProperty).ToString
                        Column.MergeDataType()
                End Select
            Next
            Column.SchemaInfo = myField
            Column.SchemaCaption = DT.Columns
            Dim SchemaInfo As String = ""
            SQLLog.Write(1, SchemaInfo)
            TableSchema.Columns.AddLast(Column)
        Next
    End Sub

    Public Sub GetTableNamesFromDatabase()
        Dim TmpTables As New DataTable
        Select Case Setting.Servertype
            Case "MS-SQL"
                If SQLCon.State = ConnectionState.Open Then
                    SQLCon.Close()
                End If
                SQLCon.Open()
                TmpTables = SQLCon.GetSchema("Tables")
                For Each Row In TmpTables.Rows
                    If Row("TABLE_TYPE").ToString.ToUpper = "BASE TABLE" Then
                        Dim TBS As New TableSchema With {
                            .TableName = Row("TABLE_NAME".ToString)
                        }
                        Tables.AddLast(TBS)
                    Else
                    End If
                Next
                SQLCon.Close()
            Case "MSSQL"
                If SQLCon.State = ConnectionState.Open Then
                    SQLCon.Close()
                End If
                SQLCon.Open()
                TmpTables = SQLCon.GetSchema("Tables")
                For Each Row In TmpTables.Rows
                    If Row("TABLE_TYPE").ToString.ToUpper = "TABLE" Then
                        Dim TBS As New TableSchema With {
                            .TableName = Row("TABLE_NAME".ToString)
                        }
                        Tables.AddLast(TBS)
                    Else
                    End If
                Next
                SQLCon.Close()
            Case "MySQL"
                If MySQLCon.State = ConnectionState.Open Then
                    MySQLCon.Close()
                End If
                MySQLCon.Open()
                TmpTables = MySQLCon.GetSchema("Tables")
                For Each Row In TmpTables.Rows
                    If Row("TABLE_TYPE").ToString.ToUpper = "BASE TABLE" Then
                        Dim TBS As New TableSchema With {
                            .TableName = Row("TABLE_NAME".ToString)
                        }
                        Tables.AddLast(TBS)
                    Else
                    End If
                Next
                MySQLCon.Close()
            Case "Access"
                If AccessCon.State = ConnectionState.Open Then
                    AccessCon.Close()
                End If
                AccessCon.Open()
                TmpTables = AccessCon.GetSchema("Tables")
                For Each Row In TmpTables.Rows
                    If Row("TABLE_TYPE").ToString.ToUpper = "TABLE" Then
                        Dim TBS As New TableSchema With {
                            .TableName = Row("TABLE_NAME".ToString)
                        }
                        Tables.AddLast(TBS)
                    Else
                    End If
                Next
                AccessCon.Close()
            Case "CSV"
                If CSVCon.State = ConnectionState.Open Then
                    CSVCon.Close()
                End If
                CSVCon.Open()
                TmpTables = CSVCon.GetSchema("Tables")
                For Each Row In TmpTables.Rows
                    If Row("TABLE_TYPE").ToString.ToUpper = "TABLE" Then
                        Dim TBS As New TableSchema With {
                            .TableName = Row("TABLE_NAME".ToString)
                        }
                        Tables.AddLast(TBS)
                    Else
                    End If
                Next
                CSVCon.Close()
        End Select
    End Sub

    Public Function GetColumnSchema(ByVal ColumnName As String) As ColumnSchema
        For Each Schema In Me.TableSchema.Columns
            If Schema.Name = ColumnName Then
                Return Schema
            End If
        Next
        Return Nothing
    End Function

    Public Function CountPKs() As Integer
        Dim PKs As Integer = 0
        For Each Schema In Me.TableSchema.Columns
            If Schema.IsKey = True Then
                PKs = PKs + 1
            End If
        Next
        Return PKs
    End Function

End Class
