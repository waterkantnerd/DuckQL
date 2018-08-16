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

Public Class MyDataConnector
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String
    Public SQLCon As SqlConnection
    Protected RC As DataTable
    Public ENV As ENV
    Public SQLLog As LOG

    Public MySQLCon As MySqlConnection
    Private MySQLcmd As New MySqlCommand

    Public AccessCon As OleDb.OleDbConnection
    Private AccessCmd As OleDb.OleDbCommand

    Public Setting As SQLServerSettings
    Public Testmode As Boolean = False

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
            MySQLCon = New MySqlConnection("Server=" & sServer & ";Database=" & sDB & ";Uid=" & sUsername & ";")
        Else
            MySQLCon = New MySqlConnection("Server=" & sServer & ";Database=" & sDB & ";Uid=" & sUsername & ";Pwd=" & sPW & ";")
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
            myConn = New SqlConnection("Initial Catalog=" & sDB & ";" &
                    "Data Source=" & sServer & ";User Id=" & sUser & "; Password=" & sPw & ";")
            'Create a Command object.
            myCmd = myConn.CreateCommand
            myCmd.CommandText = "USE " & sDB

            myConn.Open()
            SQLLog.Write(1, "Established connection to:" & sServer & "." & sDB)
            ConnectDBByUser = myConn
        Catch ex As Exception
            SQLLog.Write(0, "ERROR!: " & Err.Description)
            ConnectDBByUser = Nothing
            Exit Function
        End Try
        myConn.Close()
    End Function
    '------------------------------------------------------------------------------------------------------------------------

    '--------------------------------------------------Microsoft SQL Server with Windows Authentification--------------------
    Private Function ConnectDBTrusted(sServer, sDB) As SqlConnection
        'Attention: To use this, the program has to run under the same user you intend to use for authentificate on the sql server

        SQLLog.Write(1, "Connecting to SQL Server...")
        Try
            'Create a Connection object.
            Me.myConn = New SqlConnection("Server=" & sServer & ";Database=" & sDB & ";Trusted_Connection=True;;")
            'Create a Command object.
            myCmd = myConn.CreateCommand
            myCmd.CommandText = "USE " & sDB

            myConn.Open()
            SQLLog.Write(1, "Established connection to:" & sServer & "." & sDB)
            ConnectDBTrusted = myConn
        Catch ex As Exception
            SQLLog.Write(0, "ERROR!: " & ex.Message)
            ConnectDBTrusted = Nothing
            Exit Function
        End Try
        myConn.Close()
    End Function
    '------------------------------------------------------------------------------------------------------------------------

    '--------------------------------------------------Microsoft Access------------------------------------------------------
    Private Function ConnectDBAccess(Path As String, sDB As String) As OleDb.OleDbConnection
        ' This one restrictes the application to x86 CPUs (32 bit Application) 
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



    Public Sub CreateSQLCon()
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
                End Select
        End Select

    End Sub
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
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                    CreateDataAdapter = Nothing
                    Exit Function
                End Try
                dataadapter = Nothing
                Return ds
            Case "MySQL"
                Try
                    If MySQLCon.State = ConnectionState.Open Then
                        MySQLCon.Close()
                    End If
                    Dim dataadapter As New MySqlDataAdapter(SQLrq, MySQLCon)
                    Dim ds As New DataSet
                    dataadapter.Fill(ds)
                    CreateDataAdapter = ds
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
                    Exit Function
                Catch ex As Exception
                    SQLLog.Write(0, ex.Message)
                End Try
        End Select

        CreateDataAdapter = Nothing
    End Function

    Public Function ExecuteQuery(SQLrq As String) As Boolean
        Dim Res As String = ""
        Dim RowsAffected As Integer = 0
        Select Case Setting.Servertype
        ' Of course diffenrent Products need differnt objects here as well.
        ' SQLrq is the query variable
            Case "MSSQL"
                Try
                    If myConn.State = ConnectionState.Open Then
                        myConn.Close()
                    End If
                    myCmd.CommandText = SQLrq
                    myConn.Open()
                    RowsAffected = myCmd.ExecuteNonQuery
                    SQLLog.Write(1, RowsAffected & " Row(s) affected.")
                    myConn.Close()
                    ExecuteQuery = True
                Catch e As Exception
                    ExecuteQuery = False
                    SQLLog.Write(0, e.Message & " - Query was: " & SQLrq)
                    Exit Function
                End Try
            Case "MS-SQL"
                Try
                    If myConn.State = ConnectionState.Open Then
                        myConn.Close()
                    End If
                    myCmd.CommandText = SQLrq
                    myConn.Open()
                    RowsAffected = myCmd.ExecuteNonQuery
                    SQLLog.Write(1, RowsAffected & " Row(s) affected.")
                    myConn.Close()
                    ExecuteQuery = True
                Catch e As Exception
                    ExecuteQuery = False
                    SQLLog.Write(0, e.Message & " - Query was: " & SQLrq)
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
                    Exit Function
                End Try
            Case Else
                ExecuteQuery = Nothing
        End Select
        RowsAffected = 0
    End Function

    Public Sub ExecuteStoredProcedure(Params As LinkedList(Of SQLParamter))
        Dim Param(Params.Count) As SQLParamter
        Params.CopyTo(Param, 0)
        Dim i As Integer
        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Connection = Me.SQLCon
        cmd.CommandText = "ExecutedStoredProcedure"
        For i = 0 To Params.Count - 1
            cmd.Parameters.Add(Param(i).Parametername, Param(i).Parametertyp)
            cmd.Parameters(Param(i).Parametername).Value = Param(i).Wert
        Next
        Try
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            SQLLog.Write(0, ex.Message)
        End Try
    End Sub
    '----------------------------------------------------------------------------------------------------------------------------------------------------------------

    '---------------------------------------------------------Masking Strings and Dates------------------------------------------------------------------------------
    ' To be honest, I saw this in a few code samples - unknown source.
    ' I think this is a very elegant and simple way to mask text, dates and NULL values.
    ' On this way Querys a much easier to read and write. 

    Public Function CSQL(SQL_Attribut As String, Optional VariableType As String = "String") As String
        ' This function gets a value and a target data type
        ' and masks the value, so that is sql inline for using it within the query
        If IsNothing(SQL_Attribut) Or SQL_Attribut = vbNullString Then
            CSQL = "NULL"
            Exit Function
        Else
            Select Case VariableType

                Case "String", "8"
                    If InStr(1, SQL_Attribut, "'") <> 0 Then
                        CSQL = "'" & Replace(CStr(SQL_Attribut), "'", "''") & "'"
                        Exit Function
                    Else
                        CSQL = "'" & CStr(SQL_Attribut) & "'"
                        Exit Function
                    End If

                Case "Integer", "Double", "Float", "3"
                    If IsNumeric(SQL_Attribut) Then
                        CSQL = CStr(SQL_Attribut)
                        Exit Function
                    Else
                        CSQL = "'" & CStr(SQL_Attribut) & "'"
                        Exit Function
                    End If

                Case "Boolean"
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

                Case "Date"
                    If IsDate(SQL_Attribut) Then
                        CSQL = Format(CDate(SQL_Attribut), "\#mm\/dd\/yyyy\#")
                        Exit Function
                    Else
                        CSQL = "'" & SQL_Attribut & "'"
                        Exit Function
                    End If
                Case "Time", "4"
                    If IsDate(SQL_Attribut) Then
                        CSQL = Format(CDate(SQL_Attribut), "\#hh:mm:ss#\")
                        Exit Function
                    Else
                        CSQL = "'" & SQL_Attribut & "'"
                        Exit Function
                    End If
                Case "DateTime", "7"
                    If IsDate(SQL_Attribut) Then
                        CSQL = "'" & SQL_Attribut.Replace(".", "-") & "'"
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


End Class
