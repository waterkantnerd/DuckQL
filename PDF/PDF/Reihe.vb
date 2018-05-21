'----------------------------------------------Explanation Reihe (Summary)---------------------------------------------------------------------------------
' This object stores the whole row of the data extract.
' It's also the point where INSERT AND UPDATE Strings are build.
'----------------------------------------------------------------------------------------------------------------------------------------------------------
Public Class Reihe
    Public Spalten As New LinkedList(Of Daten)
    Public IDValue As String = ""
    Public UpdateString As String = ""
    Public InsertString As String = ""
    Public Log As LOG = Module1.Core.CurrentLog
    Public Source As SQL
    Public Target As SQL

    Public Sub SetUp(SourceSQL As SQL, TargetSQL As SQL)
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
    End Sub
    Public Sub MakeUpdateString()
        Log.Write(1, "Creating SQL UPDATE-string...")
        Dim SQL As SQL = Target
        Dim SQLrq As String = "UPDATE "
        Dim i As Integer = 0
        SQLrq = SQLrq & SQL.Setting.SQLTable & " SET "

        For Each Spalte In Spalten
            If Spalte.IsTargetIDColumn = True Then

            Else
                SQLrq = SQLrq & Spalte.Mapping.Targetname & "=" & SQL.CSQL(Spalte.Wert, GetColumnDataType(Spalte))
                If i = Spalten.Count - 1 Then
                    SQLrq = SQLrq & " WHERE " & SQL.Setting.IDColumn & "=" & SQL.CSQL(Me.IDValue)
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
        Log.Write(1, "Creating SQL INSERT string...")
        Dim SQL As SQL = Target
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

    Private Function GetColumnDataType(Spalte As Daten) As String
        Select Case Spalte.Mapping.Targettype
            Case "Integer"
                GetColumnDataType = vbInteger
            Case "Int"
                GetColumnDataType = vbInteger
            Case "DateTime"
                GetColumnDataType = vbDate
            Case "Time"
                GetColumnDataType = vbShortTime
            Case "Date"
                GetColumnDataType = vbDate
            Case "String"
                GetColumnDataType = vbString
            Case "varchar"
                GetColumnDataType = vbString
            Case "nvarchar"
                GetColumnDataType = vbString
            Case "bit"
                GetColumnDataType = vbString
            Case "Timestamp"
                GetColumnDataType = vbString
            Case "binary"
                GetColumnDataType = vbString
            Case "Binär"
                GetColumnDataType = vbString
            Case Else
                GetColumnDataType = vbString
        End Select
    End Function
End Class
