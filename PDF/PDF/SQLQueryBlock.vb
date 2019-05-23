Public Class SQLQueryBlock
    Private Querys As New LinkedList(Of SQLQuery)
    Private Rows As New LinkedList(Of Reihe)
    Private LOG As LOG = Module1.Core.CurrentLog
    Public e As Exception
    Public Success As Boolean = False
    Public HasBeenSendToSQLServer As Boolean = False
    Public SQLqueryRunning As Boolean = False
    Public QBID As Integer = Module1.Core.GenerateQueryBlockID()
    Public WithEvents Type As String = ""                                    '---> INSERT OR UPDATE
    Private Overhead As Long = 0
    Private QueryString As String = ""

    Public Sub AddQuery(ByRef Reihe As Reihe)
        Rows.AddLast(Reihe)
        Dim SQLQ As New SQLQuery
        Select Case Me.Type
            Case "INSERT"
                SQLQ.QueryString = Reihe.InsertString
            Case "UPDATE"
                SQLQ.QueryString = Reihe.UpdateString
            Case Else
                LOG.Write(0, "WARNING Query Block Type has not been defined!")
        End Select
        Querys.AddLast(SQLQ)
    End Sub

    Public Function CountQuerysInThisBlock() As Long
        Return Querys.Count
    End Function

    'ToDo: Handle schreiben, dass bei Änderung von Type automatisch rechnet und Sub auf private schalten
    Public Sub CalcOverhead()
        Select Case Me.Type
            Case "INSERT"
                Dim SB As System.Text.StringBuilder = CreateMySQLQueryOverhead()
                Me.Overhead = System.Text.ASCIIEncoding.Unicode.GetByteCount(SB.ToString)
            Case "UPDATE"
                Dim SB As System.Text.StringBuilder = CreateMySQLQueryOverhead()
                Me.Overhead = System.Text.ASCIIEncoding.Unicode.GetByteCount(SB.ToString)
            Case Else

        End Select
    End Sub

    Public Async Function SendToSQLServer() As Task(Of Boolean)
        Dim Target As MyDataConnector = Module1.Core.GetTarget

        If IsNothing(Target) Then
            LOG.Write(0, "Could not get any target connector. Will not send a query to target.")
            Return Nothing
        End If
        LOG.Write(1, "Sending QueryBlock " & QBID & " to server")
        Dim SQLTask As Task(Of Boolean) = Target.ExecuteQueryAsync(Me)
        Success = Await SQLTask

        LOG.Write(1, "Received QueryBlock " & QBID & " from server with result " & Success)
        Return Success
    End Function

    Public Sub CopyQueryToBlock(Query As SQLQuery)
        Querys.AddLast(Query)
    End Sub

    Public Sub MarkAllAsSuccessful()
        For Each Query In Querys
            Query.Worked = True
        Next
    End Sub

    Public Function Size() As Long
        Dim Bytesize As Long = Me.Overhead
        For Each SQLQuery In Me.Querys
            Bytesize = Bytesize + System.Text.ASCIIEncoding.Unicode.GetByteCount(SQLQuery.QueryString)
        Next
        Return Bytesize
    End Function

    Private Function CreateMySQLQueryOverhead() As System.Text.StringBuilder
        Dim SB As New System.Text.StringBuilder
        Dim Target As MyDataConnector = Module1.Core.GetTarget

        SB.Append("INSERT INTO ")
        SB.Append(Target.Setting.SQLTable)
        SB.Append(" (")
        Dim i As Integer
        For i = 0 To Module1.Core.Mappings.Count - 1
            SB.Append(Module1.Core.Mappings(i).Targetname)
            If i < Module1.Core.Mappings.Count - 1 Then
                SB.Append(",")
            End If
        Next
        SB.Append(") VALUES ")
        Return SB
    End Function

    Public Function GetQueryString() As String
        Dim SB As New System.Text.StringBuilder
        Dim Target As MyDataConnector = Module1.Core.GetTarget

        Select Case Target.Setting.Servertype
            Case "MySQL"
                Select Case Me.Type
                    Case "INSERT"
                        SB.Append(CreateMySQLQueryOverhead())
                        Dim i As Long = 0
                        For i = 0 To Querys.Count - 1
                            If i = Querys.Count - 1 Then
                                SB.Append(Querys(i).QueryString & ";")
                            Else
                                SB.Append(Querys(i).QueryString & ",")
                            End If
                        Next
                    Case "UPDATE"
                        SB.Append(CreateMySQLQueryOverhead())
                        Dim i As Long = 0
                        For i = 0 To Querys.Count - 1
                            If i = Querys.Count - 1 Then
                                SB.Append(Querys(i).QueryString)
                            Else
                                SB.Append(Querys(i).QueryString & ",")
                            End If

                        Next
                        SB.Append("ON DUPLICATE KEY UPDATE ")
                        i = 0
                        For Each Mapping In Module1.Core.Mappings
                            If i = Module1.Core.Mappings.Count - 1 Then
                                SB.Append(Mapping.Targetname)
                                SB.Append("=")
                                SB.Append("VALUES(" & Mapping.Targetname & ");")
                            Else
                                SB.Append(Mapping.Targetname)
                                SB.Append("=")
                                SB.Append("VALUES(" & Mapping.Targetname & "), ")
                            End If
                            i = i + 1
                        Next
                    Case Else
                        LOG.Write(0, "Query Block Type has not beend defined")
                        Return Nothing
                End Select
                Me.QueryString = SB.ToString
                LOG.Write(1, Me.QueryString, True)

                Return Me.QueryString
            Case "MS-SQL"
                'MS-SQL is using a DS for BULK Methods...
            Case "MSSQL"
                'MSSQL is using a DS for BULK Methods...
            Case Else
                'BULK Methods for other connectors have not been defined...
        End Select
        Return Nothing
    End Function

    Public Function Split(ByVal QuantityOfBlocks As Integer) As LinkedList(Of SQLQueryBlock)
        Dim Splitpoint As Integer = Me.Size / QuantityOfBlocks
        Dim i As Integer = 1
        Dim Blocks As New LinkedList(Of SQLQueryBlock)
        Dim k As Long = 0

        For i = 1 To i = Splitpoint
            Dim Block As New SQLQueryBlock
            For Each Query In Querys
                While k < Splitpoint
                    Block.CopyQueryToBlock(Query)
                    Querys.Remove(Query)
                    k = k + 1
                End While
            Next
            Blocks.AddLast(Block)
        Next
        If Querys.Count > 0 Then
            Dim Block As New SQLQueryBlock
            For Each Query In Querys
                Block.CopyQueryToBlock(Query)
                Querys.Remove(Query)
            Next
            Blocks.AddLast(Block)
        End If
        Return Blocks
    End Function

End Class
