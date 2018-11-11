'----------------------------------------------Explanation SQLoperations (Summary)-------------------------------------------------------------------------
' I put database operation in two layers. 
' One Layer for generic database operations and types, like querys, sql connectors, etc. which is in the sql class.
' However the program specific logic is in the sql operations class. 
'----------------------------------------------------------------------------------------------------------------------------------------------------------
Imports System.Xml
Public Class SQLOperations
    Private SQL As MyDataConnector
    Private Log As LOG = Module1.Core.CurrentLog
    Private ReadOnly ENV As ENV = Module1.Core.CurrentENV
    Private Setting As SQLServerSettings

    Public Sub Setup(SQLEnvoirenment As MyDataConnector) 'Change this to real constructor method in the near future
        Me.SQL = SQLEnvoirenment
        Me.Setting = SQLEnvoirenment.Setting
    End Sub


    Public Sub Load(SQLEnvoirenment As MyDataConnector)
        ' Loads data from source database.

        ' Setting up the source db into the method
        Me.SQL = SQLEnvoirenment
        Me.Setting = SQLEnvoirenment.Setting

        Dim SQLrq As String = ""
        Dim DS As New DataSet
        Dim Target As SQLServerSettings
        Dim TargetSQL As MyDataConnector

        Target = GetTargetSetting()
        TargetSQL = GetTargetSQL()
        Dim i As Long = 0

        Select Case Me.Setting.Servertype
            Case "XML"
                Dim ReaderDepth As Integer = 0
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
                                    Module1.Core.Reihen.AddLast(Reihe)
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
            Case "CSV"

            Case "XLS"
                '2be implemented soon...
            Case "HTML"
                '2be implemented soon...
            Case Else 'Every SQL Engine, which is supported...
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
                    For i = 0 To DS.Tables(0).Rows.Count - 1
                        Dim ResultRow As DataRow = DS.Tables(0).Rows(i)
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
                            Log.Write(1, "Row " & i & " has ID value " & Reihe.IDValue)
                            ' Running throught the loaded columns
                            '---------------------- This can be much faster, if we already know the mappings and match the columns in just one step -----------------------------------------------------
                            For Each Column In DS.Tables(0).Columns
                                Dim Daten As New Daten
                                Daten.SetUp(SQL, TargetSQL)
                                Daten.IdentifierCol = Setting.IDColumn
                                Daten.IdentifierVal = ResultRow(Setting.IDColumn).ToString
                                Daten.SourceKey = Column.ToString
                                If ResultRow(Daten.SourceKey).ToString = "" Or IsNothing(ResultRow(Daten.SourceKey).ToString) Or IsDBNull(ResultRow(Daten.SourceKey).ToString) Or ResultRow(Daten.SourceKey).ToString = " " Then
                                    Log.Write(1, "Filtering empty value in row " & Column.ToString & " with identifier " & Setting.IDColumn & "=" & Daten.IdentifierCol)
                                    Daten.Wert = ""
                                Else
                                    Daten.Wert = ResultRow(Column).ToString
                                End If
                                Log.Write(1, "Tupel " & Daten.SourceKey & " with value of " & Daten.Wert & " created")
                                If Daten.GetMapping() = True Then
                                    Daten.MapDaten()
                                    Log.Write(1, "Tupel has been added to row.")
                                    Reihe.Spalten.AddLast(Daten)
                                End If
                            Next
                            '------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                            For Each Mapping In ENV.Mappings
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


                            If Target.SessionTimestampField <> "" Then
                                Dim Daten As New Daten
                                Daten.SetUp(SQL, TargetSQL)
                                Daten.IdentifierCol = Setting.IDColumn
                                Daten.IdentifierVal = ResultRow(Setting.IDColumn).ToString
                                Daten.SourceKey = Target.SessionTimestampField
                                Daten.Wert = Module1.Core.TimeStamp
                                Dim Mapping As New Mapping
                                Mapping.Sourcename = Target.SessionTimestampField
                                Mapping.Sourcetype = "DateTime"
                                Mapping.Targettype = "DateTime"
                                Mapping.Targetname = Target.SessionTimestampField
                                Daten.Mapping = Mapping
                                Reihe.Spalten.AddLast(Daten)
                            End If
                            Log.Write(1, "Row " & i & " from " & Max & " has beend added to data core")
                            Module1.Core.Reihen.AddLast(Reihe)
                        End If
                    Next
                    '------------------------------------------------------------------------------------------------------------------------------------------------
                Catch ex As Exception
                    Log.Write(0, "Error while searching for ID: " & ex.Message)
                End Try
        End Select

        SQLrq = ""
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

        For Each Map In Module1.Core.CurrentENV.Mappings
            SelectStatement = SelectStatement & " " & Map.Sourcename & ","
        Next

        SelectStatement = SelectStatement.Remove(SelectStatement.Length - 1)


        'For compatibility reasons there are serveral cases that are doing the same. Sorry for the mess :P
        Select Case Setting.Filtertype
            Case ""
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable
            Case "none" ' No filter criteria is set in the xml file
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable
            Case "default" ' Per default the sql filter is just a simple statement e.g. columnvalue = 5 
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable & " WHERE " & Setting.FilterColumn & "=" & SQL.CSQL(Setting.FilterValue)
            Case "standard" ' Per default the sql filter is just a simple statement e.g. columnvalue = 5 
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable & " WHERE " & Setting.FilterColumn & "=" & SQL.CSQL(Setting.FilterValue)
            Case "simple" ' Per default the sql filter is just a simple statement e.g. columnvalue = 5 
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable & " WHERE " & Setting.FilterColumn & "=" & SQL.CSQL(Setting.FilterValue)
            Case "Simple" ' Per default the sql filter is just a simple statement e.g. columnvalue = 5 
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable & " WHERE " & Setting.FilterColumn & "=" & SQL.CSQL(Setting.FilterValue)
            Case "one column match"
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable & " WHERE " & Setting.FilterColumn & "=" & SQL.CSQL(Setting.FilterValue)
            Case "SQL Filter" ' For more complex statements you can set up a sql filter setting on your own.
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
        Max = DS.Tables(0).Rows.Count - 1
        Log.Write(1, Max & " rows found")
        CountRows = Max
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
        Dim DS As New DataSet

        Dim i As Long = 0
        Select Case Me.Setting.Servertype
            Case "XML"
                Dim enc As New System.Text.UnicodeEncoding
                Dim XMLobj As Xml.XmlTextWriter = New Xml.XmlTextWriter(Me.Setting.FilePath, enc) With {
                    .Formatting = Xml.Formatting.Indented,
                    .Indentation = 4
                }
                XMLobj.WriteStartDocument()
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
            Case "CSV"
                'Preparing rows for rewriting the header of the csv
                SQL.RewriteCSVForXMLData(GetSourceSetting.XMLStartLayerLookup)
                For Each Reihe In Module1.Core.Reihen
                    Reihe.MakeInsertString()
                    SQL.ExecuteQuery(Reihe.InsertString)
                Next
            Case "XLS"

            Case "HTML"

            Case Else
                Log.Write(1, "Writing to Target...")
                If ENV.IDLessBatch = True Then
                    Log.Write(1, "NOTE: IDless Batch Mode is enabled")
                Else
                    Log.Write(1, "NOTE: IDless Batch Mode is disabled")
                End If
                For Each Reihe In Module1.Core.Reihen
                    'The Program checks only for an existing ID if IDless Batch Mode is deactivated
                    If ENV.IDLessBatch = True Then
                    Else
                        SQLrq = "SELECT * FROM " & Setting.SQLTable & " WHERE " & Setting.IDColumn & "=" & SQL.CSQL(Reihe.IDValue, Reihe.GetIDValueDataType)
                        DS = SQL.CreateDataAdapter(SQLrq)
                        SQLrq = ""
                    End If
                    'If it can't find Records with the corresponding ID OR IDless Batch Mode is activated, the programs creates an insert string (if allowed)
                    If IsNothing(DS) = True Or ENV.IDLessBatch = True Then
                        If Setting.InsertAllowed = True Then
                            Log.Write(1, "So far the Identifier didn't exist --> INSERT")
                            Reihe.MakeInsertString()
                            If SQL.Setting.Servertype = "Access" Then
                                SQL.ExecuteQuery(Reihe.InsertString)
                            Else
                                Module1.Core.SQLCommands.AddLast(Reihe.InsertString)
                            End If
                        Else
                            Log.Write(1, "So far the Identifier didn't exist --> INSERT not allowed!")
                        End If
                    Else
                        If DS.Tables(0).Rows.Count = 0 Then
                            If Setting.InsertAllowed = True Then
                                Log.Write(1, "So far the Identifier didn't exist --> INSERT")
                                Reihe.MakeInsertString()
                                If SQL.Setting.Servertype = "Access" Then
                                    SQL.ExecuteQuery(Reihe.InsertString)
                                Else
                                    Module1.Core.SQLCommands.AddLast(Reihe.InsertString)
                                    If Module1.Core.SQLCommands.Count > 12000 Then
                                        If DoBatchQuery() = False Then
                                            Log.Write(0, "FATAL Error while writing data to target")
                                            Exit Sub
                                        End If
                                    End If
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
                                    Module1.Core.SQLCommands.AddLast(Reihe.UpdateString)
                                    If Module1.Core.SQLCommands.Count > 12000 Then
                                        If DoBatchQuery() = False Then
                                            Log.Write(0, "FATAL Error while writing data to target")
                                            Exit Sub
                                        End If
                                    End If
                                End If
                            Else
                                Log.Write(1, "Identifier already exists --> UPDATE not allowed!")
                            End If
                        End If
                    End If
                Next
                'Executing the last commands...
                If Module1.Core.SQLCommands.Count > 0 Then
                    If DoBatchQuery() = False Then
                        Log.Write(0, "FATAL Error while writing data to target")
                        Exit Sub
                    End If
                End If
        End Select
    End Sub

    Private Function DoBatchQuery() As Boolean
        Dim SQLrq As String = ""
        Dim ErrCount As Integer = 0
        Dim QrySuccess As Boolean = False
        'If Batch Querys are globally allowed the program will try to send one big query and retry with different modes, if it fails
        'If Batch Querys are not allowed, the program will just send the querys one by one.
        If Me.Setting.BatchQueryAllowed = True Then
            Log.Write(1, "Batch Querys allowed...")
            If Module1.Core.SQLCommands.Count > 12000 Then
                Log.Write(1, "Have more than 12.000 Commandlines --> Writing to Database")
                For Each Line In Module1.Core.SQLCommands
                    If SQLrq = "" Then
                        SQLrq = Line & ";"
                    Else
                        SQLrq = SQLrq & Line & ";"
                    End If
                Next
                QrySuccess = SQL.ExecuteQuery(SQLrq)
                If QrySuccess = False Then
                    Log.Write(1, "Query did not succeed in first attempt, trying other load strategies...")
                    While ErrCount < 3
                        Select Case ErrCount
                            Case 0
                                'Just try again...
                                Log.Write(1, "Trying to send the same query again...")
                                QrySuccess = SQL.ExecuteQuery(SQLrq)
                                If QrySuccess = True Then
                                    DoBatchQuery = QrySuccess
                                    Exit Function
                                Else
                                    ErrCount = ErrCount + 1
                                    Log.Write(0, "Could not write batch query with maximum size...")
                                End If
                            Case 1
                                'Split...
                                SQLrq = ""
                                Log.Write(1, "Trying to split query into two querys...")
                                Dim RowCount As Integer = 0
                                Dim TotalLines As Integer = Module1.Core.SQLCommands.Count
                                Dim MaxRows As Integer = Module1.Core.SQLCommands.Count / 2
                                For Each Line In Module1.Core.SQLCommands
                                    If ErrCount <> 1 Then
                                        'If splitting the query into a smaller query didn't work, we do not want to try again...
                                        Log.Write(0, "Could not write batch query in split mode...")
                                        Exit For
                                    Else
                                        If RowCount < MaxRows Then
                                            If SQLrq = "" Then
                                                SQLrq = Line & ";"
                                            Else
                                                SQLrq = SQLrq & Line & ";"
                                            End If
                                        Else
                                            QrySuccess = SQL.ExecuteQuery(SQLrq)
                                            If QrySuccess = True Then
                                                RowCount = 0
                                            Else
                                                ErrCount = ErrCount + 1
                                            End If
                                        End If
                                    End If
                                    RowCount = RowCount + 1
                                Next
                            Case 2
                                'Try sending every row, one by one...
                                For Each Line In Module1.Core.SQLCommands
                                    SQLrq = Line
                                    QrySuccess = SQL.ExecuteQuery(SQLrq)
                                    If QrySuccess = False Then
                                        'Break up, if sending query by query does no work either...
                                        DoBatchQuery = False
                                        Log.Write(0, "Could not write query in single mode. Last query was: " & SQLrq)
                                        Exit Function
                                    End If
                                Next
                        End Select
                    End While
                End If
                Log.Write(1, "Clearing Command Cache...")
                Module1.Core.SQLCommands.Clear()
                SQLrq = ""
                DoBatchQuery = QrySuccess
                Exit Function
            Else
                'Executing the last commands...
                If Module1.Core.SQLCommands.Count > 0 Then
                    Log.Write(1, "Last iteration of command lines --> Writing to Database")
                    For Each Line In Module1.Core.SQLCommands
                        If SQLrq = "" Then
                            SQLrq = Line & ";"
                        Else
                            SQLrq = SQLrq & Line & ";"
                        End If
                    Next
                    QrySuccess = SQL.ExecuteQuery(SQLrq)
                    If QrySuccess = False Then
                        If QrySuccess = False Then
                            Log.Write(1, "Query did not succeed in first attempt, trying other load strategies...")
                            While ErrCount < 3
                                Select Case ErrCount
                                    Case 0
                                        'Just try again...
                                        Log.Write(1, "Trying to send the same query again...")
                                        QrySuccess = SQL.ExecuteQuery(SQLrq)
                                        If QrySuccess = True Then
                                            DoBatchQuery = QrySuccess
                                            Exit Function
                                        Else
                                            ErrCount = ErrCount + 1
                                            Log.Write(0, "Could not write batch query with maximum size...")
                                        End If
                                    Case 1
                                        'Split...
                                        Log.Write(1, "Trying to split query into two querys...")
                                        SQLrq = ""
                                        Dim RowCount As Integer = 0
                                        Dim TotalLines As Integer = Module1.Core.SQLCommands.Count
                                        Dim MaxRows As Integer = Module1.Core.SQLCommands.Count / 2
                                        For Each Line In Module1.Core.SQLCommands
                                            If ErrCount <> 1 Then
                                                'If splitting the query into a smaller query didn't work, we do not want to try again...
                                                Log.Write(0, "Could not write batch query in split mode...")
                                                Exit For
                                            Else
                                                If RowCount < MaxRows Then
                                                    If SQLrq = "" Then
                                                        SQLrq = Line & ";"
                                                    Else
                                                        SQLrq = SQLrq & Line & ";"
                                                    End If
                                                Else
                                                    QrySuccess = SQL.ExecuteQuery(SQLrq)
                                                    If QrySuccess = True Then
                                                        RowCount = 0
                                                    Else
                                                        ErrCount = ErrCount + 1
                                                    End If
                                                End If
                                            End If
                                            RowCount = RowCount + 1
                                        Next
                                    Case 2
                                        'Try sending every row, one by one...
                                        For Each Line In Module1.Core.SQLCommands
                                            SQLrq = Line
                                            QrySuccess = SQL.ExecuteQuery(SQLrq)
                                            If QrySuccess = False Then
                                                'Break up, if sending query by query does no work either...
                                                DoBatchQuery = False
                                                Log.Write(0, "Could not write query in single mode. Last query was: " & SQLrq)
                                                Exit Function
                                            End If
                                        Next
                                End Select
                                ErrCount = ErrCount + 1
                            End While
                        End If
                    End If
                    Log.Write(1, "Final clearing Command Cache...")
                    Module1.Core.SQLCommands.Clear()
                    SQLrq = ""
                End If
            End If
        Else
            Log.Write(1, "Batch Querys NOT allowed...")
            For Each Line In Module1.Core.SQLCommands
                SQLrq = Line
                QrySuccess = SQL.ExecuteQuery(SQLrq)
                If QrySuccess = False Then
                    DoBatchQuery = False
                    Log.Write(0, "Could not write query in single mode. Last query was: " & SQLrq)
                    Exit Function
                End If
            Next
            DoBatchQuery = True
            Exit Function
        End If
        DoBatchQuery = Nothing
    End Function

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
