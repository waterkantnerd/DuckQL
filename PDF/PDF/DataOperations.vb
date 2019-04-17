﻿'----------------------------------------------Explanation SQLoperations (Summary)-------------------------------------------------------------------------
' I put database operation in two layers. 
' One Layer for generic database operations and types, like querys, sql connectors, etc. which is in the sql class.
' However the program specific logic is in the sql operations class. 
'----------------------------------------------------------------------------------------------------------------------------------------------------------
Imports System.Xml
Imports HtmlAgilityPack

Public Class DataOperations
    Private SQL As MyDataConnector
    Private Log As LOG = Module1.Core.CurrentLog
    Private ReadOnly ENV As ENV = Module1.Core.CurrentENV
    Private Setting As SQLServerSettings
    Private INSERTBlock As SQLQueryBlock
    Private UPDATEBlock As SQLQueryBlock
    Private ThreadList As New LinkedList(Of Threading.Thread)
    Private DTHasFinished As Boolean = False
    Private AllBlocksFinished As Boolean = False
    Private UsingQueryBlocks As Boolean = False


    Public Sub Setup(SQLEnvoirenment As MyDataConnector) 'Change this to real constructor method in the near future
        Me.SQL = SQLEnvoirenment
        Me.Setting = SQLEnvoirenment.Setting
    End Sub


    Public Sub Load(SQLEnvoirenment As MyDataConnector)
        ' Loads data from source database.

        ' Setting up the source db into the method
        Me.SQL = SQLEnvoirenment
        Me.Setting = SQLEnvoirenment.Setting
        Module1.Core.SetUpMappings()

        Dim i As Long = 0

        Select Case Me.Setting.Servertype
            Case "XML"
                LoadXML()
            Case "CSV"

            Case "XLS"
                '2be implemented soon...
            Case "HTML"
                LoadHTML()
            Case Else 'Every SQL Engine, which is supported...
                LoadSQL()
                'CheckRows()
        End Select

    End Sub

    Private Sub CheckRows()
        Dim i As Long = 0
        Dim MyRows As New Queue(Of Reihe)
        Dim TmpRow As Reihe

        While Module1.Core.Reihen.Count > 0
            If IsNothing(Module1.Core.Reihen.Peek) = True Then
                Log.Write(0, "WARNING MISSING ROW on " & i)
                TmpRow = Module1.Core.Reihen.Dequeue
                Module1.Core.Reihen.Enqueue(TmpRow)
            Else
                MyRows.Enqueue(Module1.Core.Reihen.Dequeue)
            End If
            i = i + 1
        End While
        Module1.Core.Reihen = MyRows
    End Sub

    Private Sub LoadSQL()
        Dim SQLrq As String
        Dim DS As New DataSet

        Dim i As Long = 0
        Dim TargetSQL As MyDataConnector
        Dim Target As SQLServerSettings
        Target = GetTargetSetting()
        TargetSQL = GetTargetSQL()
        Select Case Target.Servertype
            Case "MS-SQL"
                InitiateTargetDataTable()
            Case "MSSQL"
                InitiateTargetDataTable()
        End Select
        GetTargetIDList()
        SQLrq = CreateSelectStatement()
        '--------------------------------------------------------------Getting the rows from the result-----------------------------------------------------------------------------------------
        DS = SQL.CreateDataAdapter(SQLrq)
        If IsNothing(DS) = True Then
            Log.Write(0, "No results found. Is the filter correct?")
            Exit Sub
        End If
        Dim Max As Long = DS.Tables(0).Rows.Count - 1
        Log.Write(1, Max & " rows found")
        Try
            For Each Row In DS.Tables(0).Rows
                Dim ResultRow As DataRow = Row
                If IsDBNull(ResultRow(Setting.IDColumn)) = True Then
                    Log.Write(1, "Filtering empty row.")
                Else
                    Dim Reihe As New Reihe
                    Reihe.SetUp(SQL, TargetSQL)
                    If Target.MapTargetIDColumnValue = True Then ' If the Identifier has to be modified before matching with the target 
                        Reihe.IDValue = ResultRow(Setting.IDColumn).ToString
                        Reihe.MapIdentifier()
                    Else
                        Reihe.IDValue = ResultRow(Setting.IDColumn).ToString
                    End If
                    Reihe.IDValueDataType = Setting.IDColumnDataType
                    Dim RowObj As New RowObj
                    RowObj.SetUp(Reihe, DS.Tables(0).Columns, ResultRow, Max, i)
                    Log.Write(1, "Row " & i & " has ID value " & Reihe.IDValue)
                    Dim t As New Threading.Thread(AddressOf SetUpSQLRow)
                    t.Start(RowObj)
                End If
                i = i + 1
            Next
            '------------------------------------------------------------------------------------------------------------------------------------------------
        Catch ex As Exception
            Log.Write(0, "Error while searching for ID: " & ex.Message)
        End Try
    End Sub

    Private Sub GetTargetIDList()
        'This sub loads the list of ID Values from the target server to match them later in the process with the values we have in the source DB.
        Dim SQLrq As String
        Dim TargetSQL As MyDataConnector
        Dim DS As New DataSet

        TargetSQL = GetTargetSQL()

        SQLrq = "SELECT " & TargetSQL.Setting.IDColumn & " FROM " & TargetSQL.Setting.SQLTable
        DS = TargetSQL.CreateDataAdapter(SQLrq)
        Module1.Core.TargetIndex = DS
        If DS.Tables(0).Rows.Count = 0 Then
            Module1.Core.NoRowsInTargetTable = True
        End If
    End Sub

    Private Sub InitiateTargetDataTable()
        'This is neccessary for Bulk Inserts on MS-SQL Servers
        For Each Mapping In Module1.Core.Mappings
            Dim Col As New DataColumn With {
                .DataType = System.Type.GetType(Mapping.Targettype),
                .ColumnName = Mapping.Targetname
            }
            Module1.Core.TargetDataTable.Columns.Add(Col)
        Next
    End Sub

    Private Class RowObj
        Public Reihe As Reihe
        Public Columns As DataColumnCollection
        Public ResultRow As DataRow
        Public MaxRows As Long
        Public CurrentRow As Long

        Public Sub SetUp(Reihe As Reihe, Columns As DataColumnCollection, ResultRow As DataRow, ByVal MaxRows As Long, ByVal CurrentRow As Long)
            Me.Reihe = Reihe
            Me.Columns = Columns
            Me.ResultRow = ResultRow
            Me.MaxRows = MaxRows
            Me.CurrentRow = CurrentRow
        End Sub
    End Class
    Private Sub SetUpSQLRow(RowObj As RowObj)
        Dim TargetSQL As MyDataConnector
        TargetSQL = GetTargetSQL()
        Dim Target As SQLServerSettings
        Target = GetTargetSetting()
        Dim Reihe As Reihe = RowObj.Reihe
        Dim Columns As DataColumnCollection = RowObj.Columns
        Dim ResultRow As DataRow = RowObj.ResultRow
        Dim MaxRows As Long = RowObj.MaxRows
        Dim CurrentRow As Long = RowObj.CurrentRow
        Dim i As Integer

        For i = 0 To Columns.Count - 1

            Dim Daten As New Daten
            With Daten
                .SetUp(SQL, TargetSQL)
                If Module1.Core.CurrentENV.HasMultipleIdentifiers = False Then
                    .IdentifierCol = Setting.IDColumn
                    .IdentifierVal = ResultRow(Setting.IDColumn).ToString
                End If


                .SourceKey = Columns(i).ToString
                If ResultRow(.SourceKey).ToString = "" Or IsNothing(ResultRow(.SourceKey).ToString) Or IsDBNull(ResultRow(.SourceKey).ToString) Or ResultRow(.SourceKey).ToString = " " Then
                    Log.Write(1, "Filtering empty value in row " & Columns(i).ToString & " with identifier " & Setting.IDColumn & "=" & .IdentifierCol)
                    .Wert = ""
                Else
                    .Wert = ResultRow(Columns(i)).ToString
                End If

                .Mapping = Module1.Core.Mappings(i)
                .MapDaten()
                If Module1.Core.CurrentENV.HasMultipleIdentifiers = True Then
                    If Module1.Core.Mappings(i).UseAsIdentifier = True Then
                        Reihe.IDSpalten.AddLast(Daten)
                    End If
                End If
                Log.Write(1, "Tupel " & .SourceKey & " with value of " & .Wert & " and Mapping " & .Mapping.Sourcename & " created")

                'If Daten.GetMapping() = True Then
                'Daten.MapDaten()
                'Log.Write(1, "Tupel has been added to row.")
                'Reihe.Spalten.AddLast(Daten)
                'End If
            End With

            Reihe.Spalten.AddLast(Daten)
        Next

        If Module1.Core.NoRowsInTargetTable = True Then
            Reihe.LookedUp = True
        Else
            Dim t As New System.Threading.Thread(AddressOf Reihe.FindInTarget)
            t.Start()
        End If


        If Module1.Core.NoSourceMappings.Count > 0 Then
            For Each Mapping In Module1.Core.NoSourceMappings
                If Mapping.NoSource = True Then
                    Dim Daten As New Daten
                    Daten.SetUp(SQL, TargetSQL)
                    Daten.IdentifierCol = Setting.IDColumn
                    Daten.IdentifierVal = ResultRow(Setting.IDColumn).ToString
                    Daten.Wert = Mapping.StaticValue
                    Daten.Mapping = Mapping
                    Daten.TargetDatatype = Mapping.Targettype
                    Log.Write(1, "Static Tupel for " & Mapping.Targetname & " with value of " & Mapping.StaticValue & " created")
                    Reihe.Spalten.AddLast(Daten)
                End If
            Next
        Else
        End If

        If Target.SessionTimestampField <> "" Then
            Dim Daten As New Daten
            Daten.SetUp(SQL, TargetSQL)
            Daten.IdentifierCol = Setting.IDColumn
            Daten.IdentifierVal = ResultRow(Setting.IDColumn).ToString
            Daten.SourceKey = Target.SessionTimestampField
            Daten.Wert = Module1.Core.TimeStamp
            Dim Mapping As New Mapping With {
                .Sourcename = Target.SessionTimestampField,
                .Sourcetype = "DateTime",
                .Targettype = "DateTime",
                .Targetname = Target.SessionTimestampField
            }
            Daten.Mapping = Mapping
            Reihe.Spalten.AddLast(Daten)
        End If

        'If Target is XML, the SourceTablenme will define the Startelement of the XML
        If Reihe.Table = "" Then
            If Target.Servertype = "XML" Then
                Reihe.Table = SQL.Setting.SQLTable
            End If
        End If

        Select Case Target.Servertype
            Case "MSSQL"
                If Reihe.Found = False Then
                    Dim t As New System.Threading.Thread(AddressOf Reihe.MakeInsertString)
                    t.Start()
                Else
                    Dim t As New System.Threading.Thread(AddressOf Reihe.MakeUpdateString)
                    t.Start()
                End If
            Case "MS-SQL"
                If Reihe.Found = False Then
                    Dim t As New System.Threading.Thread(AddressOf Reihe.MakeInsertString)
                    t.Start()
                Else
                    Dim t As New System.Threading.Thread(AddressOf Reihe.MakeUpdateString)
                    t.Start()
                End If
            Case "MySQL"
                If Reihe.Found = False Then
                    Dim t As New System.Threading.Thread(AddressOf Reihe.MakeInsertString)
                    t.Start()
                Else
                    Dim t As New System.Threading.Thread(AddressOf Reihe.MakeUpdateString)
                    t.Start()
                End If

            Case "Access"
                If Reihe.Found = False Then
                    Dim t As New System.Threading.Thread(AddressOf Reihe.MakeInsertString)
                    t.Start()
                Else
                    Dim t As New System.Threading.Thread(AddressOf Reihe.MakeUpdateString)
                    t.Start()
                End If
            Case Else

        End Select

        Log.Write(1, "Row " & CurrentRow & " from " & MaxRows & " has beend added to data core")
        If IsNothing(Reihe) Then
            Log.Write(0, "WARNING empty row detected on " & CurrentRow & " !!")
        Else
            Module1.Core.Reihen.Enqueue(Reihe)
        End If


    End Sub


    Private Sub LoadHTML()
        Dim i As Long
        i = 0
        Dim htmlDoc As New HtmlDocument
        For Each f In Module1.Core.Files
            Log.Write(1, "Reading File " & i & " from " & Module1.Core.Files.Count)
            Log.Write(1, "Filename: " & f)
            htmlDoc.DetectEncodingAndLoad(f)
            Dim Reihe As New Reihe
            For Each Mapping In Module1.Core.CurrentENV.Mappings
                Dim HTMLKnoten As HtmlNodeCollection
                HTMLKnoten = htmlDoc.DocumentNode.SelectNodes(Mapping.XPath)
                If IsNothing(HTMLKnoten) = True Then
                    Log.Write(1, "Path " & Mapping.XPath & " was not found in Document " & f)
                Else
                    For Each Node In HTMLKnoten
                        Dim Spalte As New Daten
                        Spalte.SetUp(Me.SQL, Me.GetTargetSQL)
                        Spalte.Mapping = Mapping
                        Dim TempWert As String = Node.InnerText
                        TempWert.Trim()
                        TempWert.TrimEnd()
                        Spalte.Wert = TempWert
                        Reihe.Spalten.AddLast(Spalte)
                    Next
                End If
            Next
        Next
    End Sub

    Private Sub LoadXML()
        Dim ReaderDepth As Integer = 0
        Dim TargetSQL As MyDataConnector
        Dim Target As SQLServerSettings

        Target = GetTargetSetting()
        TargetSQL = GetTargetSQL()



        If IsNothing(Me.Setting.FilePath) = True Then
            Log.Write(0, "Could not read XML File: No Path was found!")
            Exit Sub
        End If
        Try
            Dim XMLReader As Xml.XmlReader = New Xml.XmlTextReader(Me.Setting.FilePath)
            With XMLReader
                Dim PrevDepth As Integer = 0
                Dim Reihe As Reihe = Nothing
                While .Read
                    Dim CurrentDepth As Integer = .Depth
                    If Me.Setting.XMLStartLayerLookup >= CurrentDepth Then
                        Log.Write(1, "Current Depth is " & CurrentDepth & " ignoring entrys since I should start at " & Me.Setting.XMLStartLayerLookup)
                    Else
                        'Everytime a row is not yet initiated, it should start a new one...
                        If IsNothing(Reihe) Then
                            Reihe = New Reihe
                            Reihe.SetUp(SQL, TargetSQL)
                        End If
                        If Reihe.IsComplete Then
                            'If a row is complete, it should be written into core
                            Module1.Core.Reihen.Enqueue(Reihe)
                            Reihe = New Reihe
                            Reihe.SetUp(SQL, TargetSQL)
                        End If
                        For Each Mapping In Module1.Core.CurrentENV.Mappings
                            If Mapping.Sourcename = .Name Then
                                Dim Daten As New Daten
                                Daten.SetUp(SQL, TargetSQL)
                                Daten.SourceKey = .Name
                                Daten.Layer = .Depth
                                'If The current Layer is higher than the Layer stored in the core it will be overwritten
                                'Later the Max Layer Value will help to set up a correct header (if export in csv is choosen in the config)
                                If Daten.Layer > Module1.Core.MaxLayers Then
                                    Module1.Core.MaxLayers = Daten.Layer
                                End If
                                Select Case .NodeType
                                    Case Xml.XmlNodeType.Element
                                        If .AttributeCount > 0 Then
                                            While .MoveToNextAttribute
                                                If .Name = Mapping.XMLAttributeName Then
                                                    Daten.Wert = .Value
                                                End If
                                            End While
                                        Else
                                            If CurrentDepth = 0 Then
                                            Else
                                                Dim CurrentNodeType As XmlNodeType = .NodeType
                                                Daten.Wert = CheckInnerXML4XML(.ReadInnerXml, CurrentNodeType)
                                            End If
                                        End If
                                        If Daten.GetMapping = True Then
                                            Daten.MapDaten()
                                            Log.Write(1, "Tupel " & Daten.Mapping.Targetname & " with the value " & Daten.Wert & " has been added to row.")
                                            Reihe.Spalten.AddLast(Daten)
                                        End If
                                End Select
                            End If
                        Next
                    End If
                End While
                .Close()
            End With
        Catch ex As Exception
            Log.Write(0, "Error while reading XML File: " & ex.Message)
        End Try
    End Sub

    Private Function CheckInnerXML4XML(XMLString As String, XMLKnotenTyp As Xml.XmlNodeType) As String
        'Has to check if is in this inner xml is still xml code and replace it with actual value...
        Select Case XMLKnotenTyp
            Case Xml.XmlNodeType.Whitespace

            Case Xml.XmlNodeType.SignificantWhitespace

            Case Else
                Dim XMLReader As Xml.XmlReader = New Xml.XmlTextReader(XMLString, XMLKnotenTyp, Nothing)
                While XMLReader.Read
                    Select Case XMLReader.NodeType
                        Case Xml.XmlNodeType.Element

                        Case Xml.XmlNodeType.CDATA
                            XMLString = XMLReader.Value
                        Case Xml.XmlNodeType.Comment

                        Case Xml.XmlNodeType.Document

                        Case Xml.XmlNodeType.DocumentFragment

                        Case Xml.XmlNodeType.Entity

                        Case Xml.XmlNodeType.Text

                    End Select
                End While
        End Select

        CheckInnerXML4XML = XMLString
    End Function

    Private Function CreateSelectStatement() As String
        Dim SQLrq As String = ""

        '--------------------------------------------------------------Defining the SQL-query----------------------------------------------------------
        Log.Write(1, "Loading data from datasource...")
        Dim SelectStatement As String = "SELECT "
        Dim SB As New System.Text.StringBuilder

        For Each Map In Module1.Core.Mappings
            SB.Append(Map.Sourcename & ",")
        Next
        SelectStatement = SelectStatement & SB.ToString
        SelectStatement = SelectStatement.Remove(SelectStatement.Length - 1)


        'For compatibility reasons there are serveral cases that are doing the same. Sorry for the mess :P
        Select Case Setting.Filtertype.ToUpper
            Case ""
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable
            Case "NONE" ' No filter criteria is set in the xml file
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable
            Case "DEFAULT" ' Per default the sql filter is just a simple statement e.g. columnvalue = 5 
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable & " WHERE " & Setting.FilterColumn & "=" & SQL.CSQL(Setting.FilterValue)
            Case "STANDARD" ' Per default the sql filter is just a simple statement e.g. columnvalue = 5 
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable & " WHERE " & Setting.FilterColumn & "=" & SQL.CSQL(Setting.FilterValue)
            Case "SIMPLE" ' Per default the sql filter is just a simple statement e.g. columnvalue = 5 
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable & " WHERE " & Setting.FilterColumn & "=" & SQL.CSQL(Setting.FilterValue)
            Case "ONE COLUMN MATCH"
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable & " WHERE " & Setting.FilterColumn & "=" & SQL.CSQL(Setting.FilterValue)
            Case "SQL FILTER" ' For more complex statements you can set up a sql filter setting on your own.
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable & " WHERE " & Setting.SQLFilter
            Case Else ' If nothing has been defined, the program just loads the whole table.
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable
        End Select

        Log.Write(1, "Current query is: " & SQLrq)
        '------------------------------------------------------------------------------------------------------------------------------------------------
        CreateSelectStatement = SQLrq
    End Function

    Public Function CountRows() As Long
        Dim SQLrq As String
        Dim DS As DataSet
        Dim Max As Long = 0


        '!!! Warning at the moment it is not looking for filters.

        SQLrq = "SELECT " & Setting.IDColumn & " FROM " & Setting.SQLTable
        '--------------------------------------------------------------Getting the rows from the result--------------------------------------------------
        DS = SQL.CreateDataAdapter(SQLrq)
        If IsNothing(DS) = True Then
            Log.Write(0, "No rows found. Is the filter correct?")
            CountRows = Max
            Exit Function
        End If
        Max = DS.Tables(0).Rows.Count
        Log.Write(1, Max & " rows found")
        CountRows = Max
    End Function

    Public Function GetDataSet() As DataSet
        Dim SQLrq As String
        Dim DS As DataSet


        '!!! Warning at the moment it is not looking for filters.

        SQLrq = "SELECT " & Setting.IDColumn & " FROM " & Setting.SQLTable
        '--------------------------------------------------------------Getting the rows from the result--------------------------------------------------
        DS = SQL.CreateDataAdapter(SQLrq)

        Return DS

    End Function

    Public Sub Fire(SQL As MyDataConnector)
        ' Writes the data into target database.
        ' Matches data by identifier column.
        ' Therefore first it looks up the identifier in the target database.
        ' It creates INSERT and UPDATE Statements.
        ' The function checks if the user has defined the corrosponding rights 
        ' for INSERT Or UPDATE Statements And sends it To the target SQL-Server
        ' For optimized performance querys are send every 12.000 rows to the server,
        ' this is a compromize between performance -> one big query and timeout handling 
        ' of the several servers which the program can not influence.


        ' NOTE: UPDATE Command is only possible if:
        ' - The Programm is allowed to UPDATE
        ' - The Target Datasource is an SQL Server --> Flatfiles like XML, CSV OR XLS will alway be rewritten!!
        ' - IDless Batch Mode is deactivated

        Me.SQL = SQL
        Me.Setting = SQL.Setting


        Dim SQLrq As String = ""


        Dim i As Long = 0
        Select Case Me.Setting.Servertype
            Case "XML"
                WriteXML()
            Case "CSV"
                WriteCSV()
            Case "XLS"
                'ToDo: implement
            Case "HTML"
                'ToDo: implement
            Case Else
                WriteSQL()
        End Select
    End Sub

    Private Sub WriteSQL()
        Dim SQLrq As String = ""

        'ToDo: UPDATE Handling of Query Blocks for MySQL + INSERT & UPDATE Handling for MS-SQL

        Log.Write(1, "Writing to Target...")
        If ENV.IDLessBatch = True Then
            Log.Write(1, "NOTE: IDless Batch Mode is enabled")
        Else
            Log.Write(1, "NOTE: IDless Batch Mode is disabled")
        End If

        If SQL.Setting.BatchQueryAllowed = True Then
            Dim k As New System.Threading.Thread(AddressOf Module1.Core.QueryBlockHandler)
            k.Start()
        End If

        If Module1.Core.NoRowsInTargetTable = True Then
            Log.Write(1, "NOTE: Target Table is empty --> Only INSERT Statements will be used")
        End If
        Dim i As Long = 0
        While Module1.Core.Reihen.Count > 0
            Dim y As Integer = 0
            While IsNothing(Module1.Core.Reihen.Peek) = True

                If y = 3 Then
                    Log.Write(1, "Putting Row back on queue...")
                    Dim TmpReihe As Reihe = Module1.Core.Reihen.Dequeue
                    Module1.Core.Reihen.Enqueue(TmpReihe)
                Else
                    Log.Write(1, "I have " & Module1.Core.Reihen.Count & " Rows left")
                    Log.Write(1, "Waiting 5 Seconds for Row to fill...")
                    System.Threading.Thread.Sleep(5000)
                End If
                y = y + 1
            End While

            Dim Reihe As Reihe = Module1.Core.Reihen.Dequeue


            While Reihe.IsComplete = False Or Reihe.LookedUp = False
                Log.Write(1, "Waiting for Row to finish...")
                System.Threading.Thread.Sleep(5000)
            End While
            'The Program checks only for an existing ID if IDless Batch Mode is deactivated

            'If it can't find Records with the corresponding ID OR IDless Batch Mode is activated, the programs creates an insert string (if allowed)
            If ENV.IDLessBatch = True Or Module1.Core.NoRowsInTargetTable = True Then
                If Setting.InsertAllowed = True Then
                    Log.Write(1, "So far the Identifier didn't exist --> INSERT")
                    If SQL.Setting.Servertype = "Access" Or SQL.Setting.BatchQueryAllowed = False Then
                        SQL.ExecuteQuery(Reihe.InsertString)
                    Else
                        Select Case SQL.Setting.Servertype
                            Case "MySQL"
                                Me.AddQueryToINSERTBlock(Reihe)
                            Case "MSSQL"
                                'This is already handled while creating the INSERT String --> Data Table in Core
                            Case "MS-SQL"
                                'This is already handled while creating the INSERT String --> Data Table in Core
                            Case Else
                                Me.AddQueryToINSERTBlock(Reihe)
                        End Select
                    End If
                Else
                    Log.Write(1, "So far the Identifier didn't exist --> INSERT not allowed!")
                End If
            Else
                If Reihe.Found = False Then
                    If Setting.InsertAllowed = True Then
                        Log.Write(1, "So far the Identifier didn't exist --> INSERT")
                        If SQL.Setting.Servertype = "Access" Or SQL.Setting.BatchQueryAllowed = False Then
                            SQL.ExecuteQuery(Reihe.InsertString)
                        Else
                            Select Case SQL.Setting.Servertype
                                Case "MySQL"
                                    Me.AddQueryToINSERTBlock(Reihe)
                                Case "MSSQL"
                                'This is already handled while creating the INSERT String --> Data Table in Core
                                Case "MS-SQL"
                                    'This is already handled while creating the INSERT String --> Data Table in Core
                                Case Else
                                    Me.AddQueryToINSERTBlock(Reihe)
                            End Select
                        End If
                    Else
                        Log.Write(1, "So far the Identifier didn't exist --> INSERT not allowed!")
                    End If
                Else
                    If Setting.UpdateAllowed = True Then
                        Log.Write(1, "Identifier already exists --> UPDATE")
                        Reihe.MakeUpdateString()
                        If SQL.Setting.Servertype = "Access" Then
                            SQL.ExecuteQuery(Reihe.UpdateString)
                        Else
                            Me.AddQueryToUPDATEBlock(Reihe)
                        End If
                    Else
                        Log.Write(1, "Identifier already exists --> UPDATE not allowed!")
                    End If
                End If
            End If
            i = i + 1
        End While

        SyncLock Module1.Core.QueryBlock
            'If until now no queryblock has been sent to the server

            If IsNothing(Me.INSERTBlock) = True Then
            Else
                If Me.INSERTBlock.CountQuerysInThisBlock > 0 Then
                    Module1.Core.SQLCommands.AddLast(Me.INSERTBlock)
                End If
            End If


            If IsNothing(Me.UPDATEBlock) = True Then
            Else
                If Me.UPDATEBlock.CountQuerysInThisBlock > 0 Then
                    Module1.Core.SQLCommands.AddLast(Me.UPDATEBlock)
                End If
            End If

        End SyncLock

        SendQueryBlockToSQLServer()

        If UsingQueryBlocks = True Then
            'After creating every Query the programm has to wait for the SQL Server
            While Module1.Core.AllQueryBlocksFinished = False
                Log.Write(1, "Not all Blocks have been finished by now, waiting 5 seconds.")
                System.Threading.Thread.Sleep(5000)
            End While
        End If
        'This ends the Query Block Handler
        Module1.Core.DataTransferFinished = True
    End Sub



    Private Sub AddQueryToINSERTBlock(Row As Reihe)
        Me.UsingQueryBlocks = True
        If IsNothing(Me.INSERTBlock) = True Then
            Log.Write(1, "Creating New Query Block")
            Dim QB As New SQLQueryBlock
            Me.INSERTBlock = QB
            Me.INSERTBlock.Type = "INSERT"
            Me.INSERTBlock.CalcOverhead()
        Else
            If Me.INSERTBlock.Size + System.Text.ASCIIEncoding.Unicode.GetByteCount(Row.InsertString) >= Setting.Max_Paket Then
                Log.Write(1, "Query Block " & Me.INSERTBlock.QBID & " reached maximum size")
                SyncLock Module1.Core.QueryBlock
                    Module1.Core.SQLCommands.AddLast(Me.INSERTBlock)
                End SyncLock
                Log.Write(1, "Added Query Block to queue")
                SendQueryBlockToSQLServer()
                Dim QB As New SQLQueryBlock
                Me.INSERTBlock = QB
                Me.INSERTBlock.Type = "INSERT"
                Me.INSERTBlock.CalcOverhead()
                Log.Write(1, "Generated new Block " & Me.INSERTBlock.QBID)
            End If
        End If
        INSERTBlock.AddQuery(Row)
        Log.Write(1, "Added query " & Row.InsertString & " to Block " & INSERTBlock.QBID)
    End Sub

    Private Sub AddQueryToUPDATEBlock(Row As Reihe)
        Me.UsingQueryBlocks = True
        If IsNothing(Me.UPDATEBlock) = True Then
            Log.Write(1, "Creating New Query Block")
            Dim QB As New SQLQueryBlock
            Me.UPDATEBlock = QB
            Me.UPDATEBlock.Type = "UPDATE"
            Me.UPDATEBlock.CalcOverhead()
        Else
            If Me.UPDATEBlock.Size + System.Text.ASCIIEncoding.Unicode.GetByteCount(Row.UpdateString) >= Setting.Max_Paket Then
                Log.Write(1, "Query Block " & Me.UPDATEBlock.QBID & " reached maximum size")
                SyncLock Module1.Core.QueryBlock
                    Module1.Core.SQLCommands.AddLast(Me.UPDATEBlock)
                End SyncLock
                Log.Write(1, "Added Query Block to queue")
                SendQueryBlockToSQLServer()
                Dim QB As New SQLQueryBlock
                Me.UPDATEBlock = QB
                Me.UPDATEBlock.Type = "UPDATE"
                Me.UPDATEBlock.CalcOverhead()
                Log.Write(1, "Generated new Block " & Me.UPDATEBlock.QBID)
            End If
        End If
        UPDATEBlock.AddQuery(Row)
        Log.Write(1, "Added query " & Row.UpdateString & " to Block " & UPDATEBlock.QBID)
    End Sub

    Private Sub SendQueryBlockToSQLServer()
        For Each Block In Module1.Core.SQLCommands
            If Block.HasBeenSendToSQLServer = False Then
                Dim T As New System.Threading.Thread(AddressOf Block.SendToSQLServer)
                ThreadList.AddLast(T)
                Log.Write(1, "Sending Query Block " & Block.QBID & " to server.")
                T.Start()
            End If
        Next
    End Sub

    Private Sub WriteCSV()
        'Preparing rows for rewriting the header of the csv
        SQL.RewriteCSVForXMLData(GetSourceSetting.XMLStartLayerLookup)
        For Each Reihe In Module1.Core.Reihen
            Reihe.MakeInsertString()
            SQL.ExecuteQuery(Reihe.InsertString)
        Next
    End Sub
    Private Sub WriteXML()
        Dim enc As New System.Text.UnicodeEncoding
        Dim XMLobj As Xml.XmlTextWriter = New Xml.XmlTextWriter(Me.Setting.FilePath, enc) With {
            .Formatting = Xml.Formatting.Indented,
            .Indentation = 4
        }
        XMLobj.WriteStartDocument()
        Dim y As Integer = 0
        XMLobj.WriteStartElement("Rows")
        For Each Reihe In Module1.Core.Reihen
            With XMLobj
                .WriteStartElement(Reihe.Table)
                For Each Daten In Reihe.Spalten
                    .WriteAttributeString(Daten.Mapping.Targetname, Daten.Wert)
                Next
                .WriteEndElement()
            End With
        Next
        XMLobj.WriteEndElement()
        XMLobj.Close()
    End Sub
    Private Function GetTargetSetting() As SQLServerSettings
        If IsNothing(SQL) Then
            Return Nothing
        End If
        For Each SQLobject In Module1.Core.SQLServer
            If SQLobject.Setting.Direction = "Target" Or SQLobject.Setting.Direction = "target" Then
                If Setting.TargetID = SQLobject.Setting.ID Then
                    Return SQLobject.Setting
                End If
            End If
        Next
        Return Nothing
    End Function
    Private Function GetSourceSetting() As SQLServerSettings
        If IsNothing(SQL) Then
            Return Nothing
        End If
        For Each SQLobject In Module1.Core.SQLServer
            If SQLobject.Setting.Direction = "Source" Or SQLobject.Setting.Direction = "source" Then
                If Setting.TargetID = SQLobject.Setting.ID Then
                    Return SQLobject.Setting
                End If
            End If
        Next
        Return Nothing
    End Function

    Private Function GetSourceSQL() As MyDataConnector
        If IsNothing(Setting) = True Then
            Return Nothing
        End If
        For Each SQLobject In Module1.Core.SQLServer
            If SQLobject.Setting.Direction = "Source" Or SQLobject.Setting.Direction = "source" Then
                If Setting.TargetID = SQLobject.Setting.ID Then
                    Return SQLobject
                End If
            End If
        Next
        Return Nothing
    End Function


    Private Function GetTargetSQL() As MyDataConnector
        If IsNothing(Setting) = True Then
            Return Nothing
        End If
        For Each SQLobject In Module1.Core.SQLServer
            If SQLobject.Setting.Direction = "Target" Or SQLobject.Setting.Direction = "target" Then
                If Setting.TargetID = SQLobject.Setting.ID Then
                    Return SQLobject
                End If
            End If
        Next
        Return Nothing
    End Function

    Private Function GetAnyTargetSQL() As MyDataConnector
        For Each SQLobject In Module1.Core.SQLServer
            If SQLobject.Setting.Direction = "Target" Then
                Return SQLobject
            End If
        Next
        Return Nothing
    End Function
End Class
