Imports System.Text
Imports System.Web.Script.Serialization

Public Class JSON_Serialization
    Private LOG As LOG = Module1.Core.CurrentLog

    Public Function SerializeDataTableToJSON(ByRef DT As DataTable) As String
        Dim SB As New StringBuilder
        If DT.Rows.Count > 0 Then
            SB.Append("[")
            Dim RCounter As Integer = 0
            For RCounter = 0 To DT.Rows.Count - 1
                SB.Append("{")
                Dim CCounter As Integer = 0
                For CCounter = 0 To DT.Columns.Count - 1
                    If CCounter < DT.Columns.Count - 1 Then
                        SB.Append(Chr(34) & DT.Columns(CCounter).ColumnName.ToString & Chr(34) & ":" & Chr(34) & CJSON(DT.Rows(RCounter)(CCounter).ToString) & Chr(34) & ",")
                    Else
                        SB.Append(Chr(34) & DT.Columns(CCounter).ColumnName.ToString & Chr(34) & ":" & Chr(34) & CJSON(DT.Rows(RCounter)(CCounter).ToString) & Chr(34))
                    End If
                Next
                If RCounter = DT.Rows.Count - 1 Then
                    SB.Append("}")
                Else
                    SB.Append("},")
                End If
            Next
            SB.Append("]")
        Else
            LOG.Write(1, "Warning in JSON Serialization! - The Data Table is empty!")
            Return Nothing
        End If
        Return SB.ToString
    End Function

    Public Function SerializeDataRowToJSON(ByRef DT As DataTable, ByVal RowNo As Integer) As String
        Dim SB As New StringBuilder
        Dim CCounter As Integer = 0
        SB.Append("{")
        For CCounter = 0 To DT.Columns.Count - 1
            If CCounter < DT.Columns.Count - 1 Then
                SB.Append(Chr(34) & DT.Columns(CCounter).ColumnName.ToString & Chr(34) & ":" & Chr(34) & CJSON(DT.Rows(RowNo)(CCounter).ToString) & Chr(34) & ",")
            Else
                SB.Append(Chr(34) & DT.Columns(CCounter).ColumnName.ToString & Chr(34) & ":" & Chr(34) & CJSON(DT.Rows(RowNo)(CCounter).ToString) & Chr(34))
            End If
        Next
        SB.Append("}")
        LOG.Write(1, "Returning " & SB.ToString)
        Return SB.ToString
    End Function

    Private Function CJSON(Value As String) As String

        If InStr(Value, "\") Then
            Value = Replace(CStr(Value), "\", "\\")
        End If

        If InStr(Value, Chr(8)) Then
            Value = Replace(CStr(Value), Chr(8), "\b")
        End If

        If InStr(Value, Chr(12)) Then
            Value = Replace(CStr(Value), Chr(12), "\f")
        End If

        If InStr(Value, Chr(10)) Then
            Value = Replace(CStr(Value), Chr(10), "\n")
        End If

        If InStr(Value, Chr(13)) Then
            Value = Replace(CStr(Value), Chr(13), "\r")
        End If

        If InStr(Value, Chr(9)) Then
            Value = Replace(CStr(Value), Chr(9), "\t")
        End If

        If InStr(1, Value, Chr(34)) Then
            Value = Replace(CStr(Value), Chr(34), Chr(92) & Chr(34))
        End If

        Return Value
    End Function

    Public Function Deserialize_EL_JSONtoDataset(JSON As String) As DataSet
        Dim DS As New DataSet

        'Find the hit counter
        Dim hits As Integer = 0
        Dim hitstring As String = JSON.Substring(JSON.IndexOf("hits"))
        hitstring = Left(hitstring, hitstring.IndexOf(","))
        hitstring = hitstring.Replace(Chr(34), "")
        hitstring = Right(hitstring, hitstring.IndexOf("value:") - 6)
        hitstring = hitstring.Replace("value:", "")
        hits = CInt(hitstring)

        Dim jss = New JavaScriptSerializer()
        Dim data = jss.Deserialize(Of Object)(JSON)

        Dim DT As DataTable = getRows(data)

        DS.Tables.Add(DT)

        Dim TargetSetting As SQLServerSettings = Module1.Core.GetTarget.Setting


        Return DS
    End Function


    Private Function GetRows(JSONData As Object) As DataTable
        Dim DataHits As Object = JSONData("hits")
        Dim HitList() As Object = DataHits("hits")
        Dim Keys As New List(Of String)
        Dim Values As New List(Of String)

        Dim MyRows As New List(Of Reihe)

        For Each Hit In HitList
            Dim MyRow As New Reihe
            Dim SourceList As Object = Hit("_source")
            Dim i As Integer = 0
            For Each Item In SourceList
                Dim Pair As System.Collections.Generic.KeyValuePair(Of String, Object) = Item
                Dim Data As New Daten With {
                    .SourceKey = Pair.Key.ToString,
                    .Wert = Pair.Value.ToString,
                    .Target = Module1.Core.GetTarget
                }
                If Data.GetMapping(True) = True Then
                    MyRow.Spalten.AddLast(Data)
                End If
            Next
            MyRows.Add(MyRow)
        Next
        Dim DT As New DataTable
        Dim TmpRow As Reihe = MyRows.First
        For Each Spalte In TmpRow.Spalten
            Dim Col As New DataColumn With {
                .DataType = System.Type.GetType("System." & Spalte.Mapping.Targettype.ToString),
                .ColumnName = Spalte.Mapping.Targetname,
                .AllowDBNull = True
            }
            DT.Columns.Add(Col)
        Next
        For Each Row In MyRows
            Dim Drow As DataRow = DT.NewRow
            For Each Spalte In Row.Spalten
                If IsNothing(Spalte.Wert) Or Spalte.Wert = "" Then
                    Drow(Spalte.Mapping.Targetname) = DBNull.Value
                Else
                    Drow(Spalte.Mapping.Targetname) = Spalte.Wert
                End If

            Next
            DT.Rows.Add(Drow)
        Next

        Return DT

    End Function



End Class
