Public Class TableSchema
    Public Columns As New LinkedList(Of ColumnSchema)
    Public TableName As String

    Public Function HasDBKeyColumns() As Boolean
        For Each Column In Columns
            If Column.IsKey = True Then
                Return True
            End If
        Next
        Return False
    End Function
End Class
