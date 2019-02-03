'----------------------------------------------Explanation Reihe (Summary)---------------------------------------------------------------------------------
' This object stores the whole row of the data extract.
' It's also the point where INSERT AND UPDATE Strings are build.
'----------------------------------------------------------------------------------------------------------------------------------------------------------
Public Class Reihe
    Public Spalten As New LinkedList(Of Daten)
    Public IDValue As String = ""
    Public IDValueDataType As String = ""
    Public UpdateString As String = ""
    Public InsertString As String = ""
    Public Log As LOG = Module1.Core.CurrentLog
    Public Source As MyDataConnector
    Public Target As MyDataConnector
    Public Table As String
    Public HasMaxColumns As Boolean = False
    Public SourceIdentifier As String = ""
    Public TargetIdentifier As String = ""



    Public Sub SetUp(SourceSQL As MyDataConnector, TargetSQL As MyDataConnector)
        Me.Source = SourceSQL
        Me.Target = TargetSQL
    End Sub

    Public Sub MapIdentifier()
        If IsNothing(Target.Setting.StringSeperator) Then
            Exit Sub
        End If

        Select Case Target.Setting.StringPart
            Case "right"
                IDValue = IDValue.Substring(IDValue.IndexOf(Target.Setting.StringSeperator) + 1)
            Case "left"
                IDValue = IDValue.Substring(0, IDValue.IndexOf(Target.Setting.StringSeperator))
        End Select
        Log.Write(1, "Mapped ID Value:" & IDValue)
    End Sub

    Public Sub CreateIdentifierStrings()
        Dim TargetIDString As String = ""
        Dim SourceIDString As String = ""
        If Module1.Core.CurrentENV.HasMultipleIdentifiers = True Then
            For Each Spalte In Spalten
                If Spalte.Mapping.UseAsIdentifier = True Then
                    If TargetIDString = "" Then
                        TargetIDString = " WHERE " & Spalte.Mapping.Targetname & "=" & Target.CSQL(Spalte.Wert, Spalte.Mapping.Targettype)
                        SourceIDString = " WHERE " & Spalte.Mapping.Sourcename & "=" & Target.CSQL(Spalte.Wert, Spalte.Mapping.Sourcetype)
                    Else
                        TargetIDString = " AND " & Spalte.Mapping.Targetname & "=" & Target.CSQL(Spalte.Wert, Spalte.Mapping.Targettype)
                        SourceIDString = " AND " & Spalte.Mapping.Sourcename & "=" & Target.CSQL(Spalte.Wert, Spalte.Mapping.Sourcetype)
                    End If
                End If
            Next
            Me.TargetIdentifier = TargetIDString
            Me.SourceIdentifier = SourceIDString
        Else
            Me.TargetIdentifier = " WHERE " & Target.Setting.IDColumn & "=" & Target.CSQL(Me.IDValue, Me.GetIDValueDataType)
            Me.TargetIdentifier = " WHERE " & Source.Setting.IDColumn & "=" & Target.CSQL(Me.IDValue, Me.GetIDValueDataType)
        End If
    End Sub

    Public Sub MakeUpdateString()
        Log.Write(1, "Creating SQL UPDATE-string...")
        Dim SQL As MyDataConnector = Target
        Dim SQLrq As String = "UPDATE "
        Dim i As Integer = 0
        SQLrq = SQLrq & SQL.Setting.SQLTable & " SET "

        For Each Spalte In Spalten
            If Spalte.IsTargetIDColumn = True Then

            Else
                SQLrq = SQLrq & Spalte.Mapping.Targetname & "=" & SQL.CSQL(Spalte.Wert, GetColumnDataType(Spalte))
                If i = Spalten.Count - 1 Then
                    SQLrq = SQLrq & " WHERE " & SQL.Setting.IDColumn & "=" & SQL.CSQL(Me.IDValue, GetIDValueDataType)
                Else
                    SQLrq = SQLrq & ","
                End If
            End If

            i = i + 1
        Next
        Me.UpdateString = SQLrq
        Log.Write(1, "UPDATE STRING: " & Me.UpdateString)
    End Sub


    Public Sub MakeInsertString()
        Select Case Me.Target.Setting.Servertype
            Case "CSV"
                MakeCSVInsertString()
            Case Else
                MakeSQLInsertString()
        End Select
    End Sub

    Private Sub MakeCSVInsertString()
        Log.Write(1, "Creating SQL INSERT string...")
        Dim SQL As MyDataConnector = Target
        Dim SQLrq As String = "INSERT INTO "
        Dim i As Integer = 0
        SQLrq = SQLrq & SQL.Setting.SQLTable & " ("
        For Each Spalte In Spalten
            Log.Write(1, "Using " & Spalte.Mapping.Targetname & " for " & Spalte.Wert)
            SQLrq = SQLrq & Spalte.Mapping.Targetname & "_" & Spalte.Layer
            If i = Spalten.Count - 1 Then
                SQLrq = SQLrq & ") "
            Else
                SQLrq = SQLrq & ","
            End If
            i = i + 1
        Next

        i = 0
        SQLrq = SQLrq & "VALUES ("

        For Each Spalte In Spalten
            SQLrq = SQLrq & SQL.CSQL(Spalte.Wert, GetColumnDataType(Spalte))
            If i = Spalten.Count - 1 Then
                SQLrq = SQLrq & ") "
            Else
                SQLrq = SQLrq & ","
            End If
            i = i + 1
        Next

        Me.InsertString = SQLrq
        Log.Write(1, "INSERT STRING: " & Me.InsertString)
    End Sub

    Private Sub MakeSQLInsertString()
        Log.Write(1, "Creating SQL INSERT string...")
        Dim SQL As MyDataConnector = Target
        Dim SQLrq As String = "INSERT INTO "
        Dim i As Integer = 0
        SQLrq = SQLrq & SQL.Setting.SQLTable & " ("
        For Each Spalte In Spalten
            SQLrq = SQLrq & Spalte.Mapping.Targetname
            If i = Spalten.Count - 1 Then
                SQLrq = SQLrq & ") "
            Else
                SQLrq = SQLrq & ","
            End If
            i = i + 1
        Next

        i = 0
        SQLrq = SQLrq & "VALUES ("

        For Each Spalte In Spalten
            SQLrq = SQLrq & SQL.CSQL(Spalte.Wert, GetColumnDataType(Spalte))
            If i = Spalten.Count - 1 Then
                SQLrq = SQLrq & ") "
            Else
                SQLrq = SQLrq & ","
            End If
            i = i + 1
        Next

        Me.InsertString = SQLrq
        Log.Write(1, "INSERT STRING: " & Me.InsertString)
    End Sub

    Public Function GetIDValueDataType() As String
        Select Case Me.IDValueDataType.ToLower
            Case "integer"
                GetIDValueDataType = vbInteger
            Case "int"
                GetIDValueDataType = vbInteger
            Case "datetime"
                GetIDValueDataType = vbDate
            Case "time"
                GetIDValueDataType = vbShortTime
            Case "date"
                GetIDValueDataType = vbDate
            Case "string"
                GetIDValueDataType = vbString
            Case "varchar"
                GetIDValueDataType = vbString
            Case "nvarchar"
                GetIDValueDataType = vbString
            Case "bit"
                GetIDValueDataType = vbString
            Case "timestamp"
                GetIDValueDataType = vbString
            Case "binary"
                GetIDValueDataType = vbString
            Case "binär"
                GetIDValueDataType = vbString
            Case Else
                GetIDValueDataType = vbString
        End Select
    End Function

    Private Function GetColumnDataType(Spalte As Daten) As String
        Select Case Spalte.Mapping.Targettype.ToLower
            Case "integer"
                GetColumnDataType = vbInteger
            Case "int"
                GetColumnDataType = vbInteger
            Case "datetime"
                GetColumnDataType = vbDate
            Case "time"
                GetColumnDataType = vbShortTime
            Case "date"
                GetColumnDataType = vbDate
            Case "string"
                GetColumnDataType = vbString
            Case "varchar"
                GetColumnDataType = vbString
            Case "nvarchar"
                GetColumnDataType = vbString
            Case "bit"
                GetColumnDataType = vbString
            Case "timestamp"
                GetColumnDataType = vbString
            Case "binary"
                GetColumnDataType = vbString
            Case "binär"
                GetColumnDataType = vbString
            Case Else
                GetColumnDataType = vbString
        End Select
    End Function

    Public Function IsComplete() As Boolean
        'A row is complete if all defined Mappings has been used 
        If Spalten.Count <= 0 Then
            Return False
            Exit Function
        End If

        Dim MappingsUsed(Module1.Core.CurrentENV.Mappings.Count - 1) As Mapping
        Module1.Core.CurrentENV.Mappings.CopyTo(MappingsUsed, 0)
        For Each Spalte In Me.Spalten
            Dim i As Integer = 0
            While i < MappingsUsed.Count
                If Spalte.Mapping.Targetname = MappingsUsed(i).Targetname Then
                    If MappingsUsed(i).UsedForColumn = True Then
                    Else
                        MappingsUsed(i).UsedForColumn = True
                    End If
                End If
                i = i + 1
            End While
        Next
        Dim AllComplete As Boolean = True
        For Each Mapping In MappingsUsed
            If IsNothing(Mapping) Then
            Else
                If Mapping.UsedForColumn = False Then
                    AllComplete = False
                End If
            End If
        Next

        For Each Mapping In MappingsUsed
            Mapping.UsedForColumn = False
        Next

        If AllComplete = True Then
            Return True
            Exit Function
        End If

        Return False
    End Function

End Class
