'----------------------------------------------Explanation SQLoperations (Summary)-------------------------------------------------------------------------
' I put database operation in two layers. 
' One Layer for generic database operations and types, like querys, sql connectors, etc. which is in the sql class.
' However the program specific logic is in the sql operations class. 
'----------------------------------------------------------------------------------------------------------------------------------------------------------
Public Class SQLOperations
    Private SQL As MyDataConnector
    Private Log As LOG = Module1.Core.CurrentLog
    Private ReadOnly ENV As ENV = Module1.Core.CurrentENV
    Private Setting As SQLServerSettings

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

        '--------------------------------------------------------------Defining the SQL-query----------------------------------------------------------
        Log.Write(1, "Loading data from datasource...")
        Dim SelectStatement As String = "SELECT "

        For Each Map In Module1.Core.CurrentENV.Mappings
            SelectStatement = SelectStatement & " " & Map.Sourcename & ","
        Next

        SelectStatement = SelectStatement.Remove(SelectStatement.Length - 1)

        Select Case Setting.Filtertype
            Case ""
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable
            Case "none" ' No filter criteria is set in the xml file
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable
            Case "default" ' Per default the sql filter is just a simple statement e.g. columnvalue > 5 
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable & " WHERE " & Setting.FilterColumn & "=" & SQL.CSQL(Setting.FilterValue)
            Case "standard" ' Per default the sql filter is just a simple statement e.g. columnvalue > 5 
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable & " WHERE " & Setting.FilterColumn & "=" & SQL.CSQL(Setting.FilterValue)
            Case "simple" ' Per default the sql filter is just a simple statement e.g. columnvalue > 5 
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable & " WHERE " & Setting.FilterColumn & "=" & SQL.CSQL(Setting.FilterValue)
            Case "Simple" ' Per default the sql filter is just a simple statement e.g. columnvalue > 5 
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable & " WHERE " & Setting.FilterColumn & "=" & SQL.CSQL(Setting.FilterValue)
            Case "SQL Filter" ' For more complex statements you can set up a sql filter setting on your own.
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable & " WHERE " & Setting.SQLFilter
            Case Else ' If nothing has been defined, the program just loads the whole table.
                SQLrq = SelectStatement & " FROM " & Setting.SQLTable
        End Select

        Log.Write(1, "Current query is: " & SQLrq)
        '------------------------------------------------------------------------------------------------------------------------------------------------

        '--------------------------------------------------------------Getting the rows from the result--------------------------------------------------
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
                    '------------------------------------------------------------------Let's give it a try-----------------------------------------------------------------------------------------

                    '------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

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
            Module1.Core.CurrentLog.Write(0, "Error while searching for ID: " & ex.Message)
        End Try
        SQLrq = ""
    End Sub

    Public Sub Fire(SQL As MyDataConnector)
        ' Writes the data into target database.
        ' Matches data by identifier column.
        ' Therefore first it looks up the identifier in the target database.
        ' It creates INSERT and UPDATE Statements.
        ' The function checks if the user has defined the corrosponding rights 
        ' for INSERT Or UPDATE Statements And sends it To the target SQL-Server
        ' For optimized performance querys are send every 12.000 rows to the server,
        ' this is a compromize between performance -> one big query and timeouthandling 
        ' of the several servers which the program can not influence.
        Me.SQL = SQL
        Me.Setting = SQL.Setting


        Dim SQLrq As String = ""
        Dim DS As New DataSet

        Dim i As Long = 0

        For Each Reihe In Module1.Core.Reihen
            SQLrq = "SELECT * FROM " & Setting.SQLTable & " WHERE " & Setting.IDColumn & "=" & SQL.CSQL(Reihe.IDValue, Reihe.GetIDValueDataType)
            DS = SQL.CreateDataAdapter(SQLrq)
            SQLrq = ""
            If IsNothing(DS) = True Then
                If Setting.InsertAllowed = True Then
                    Log.Write(1, "So far the Identifier didn't exist --> INSERT")
                    Reihe.MakeInsertString()
                    If SQL.Setting.Servertype = "Access" Then
                        SQL.ExecuteQuery(Reihe.InsertString)
                    Else
                        Module1.Core.SQLCommands.AddLast(Reihe.InsertString)
                        If Module1.Core.SQLCommands.Count > 12000 Then
                            Log.Write(1, "Have more than 12.000 Commandlines --> Writing to Database")
                            For Each Line In Module1.Core.SQLCommands
                                If SQLrq = "" Then
                                    SQLrq = Line & ";"
                                Else
                                    SQLrq = SQLrq & Line & ";"
                                End If
                            Next
                            SQL.ExecuteQuery(SQLrq)
                            Log.Write(1, "Clearing Command Cache...")
                            Module1.Core.SQLCommands.Clear()
                            SQLrq = ""
                        End If
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
                                Log.Write(1, "Have more than 12.000 Commandlines --> Writing to Database")
                                For Each Line In Module1.Core.SQLCommands
                                    If SQLrq = "" Then
                                        SQLrq = Line & ";"
                                    Else
                                        SQLrq = SQLrq & Line & ";"
                                    End If
                                Next
                                SQL.ExecuteQuery(SQLrq)
                                Log.Write(1, "Clearing Command Cache...")
                                Module1.Core.SQLCommands.Clear()
                                SQLrq = ""
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
                                Log.Write(1, "Have more than 12.000 Commandlines --> Writing to Database")
                                For Each Line In Module1.Core.SQLCommands
                                    If SQLrq = "" Then
                                        SQLrq = Line & ";"
                                    Else
                                        SQLrq = SQLrq & Line & ";"
                                    End If
                                Next
                                SQL.ExecuteQuery(SQLrq)
                                Log.Write(1, "Clearing Command Cache...")
                                Module1.Core.SQLCommands.Clear()
                                SQLrq = ""
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
            Log.Write(1, "Last iteration of command lines --> Writing to Database")
            For Each Line In Module1.Core.SQLCommands
                If SQLrq = "" Then
                    SQLrq = Line & ";"
                Else
                    SQLrq = SQLrq & Line & ";"
                End If
            Next
            SQL.ExecuteQuery(SQLrq)
            Log.Write(1, "Final clearing Command Cache...")
            Module1.Core.SQLCommands.Clear()
            SQLrq = ""
        End If
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
