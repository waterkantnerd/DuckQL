Public Class ColumnSchema
    Public Name As String
    Public DataType As String


    Public Sub MergeDataType()
        If Me.DataType.IndexOf(".") >= 0 Then
            Me.DataType = Me.DataType.Substring(Me.DataType.IndexOf(".") + 1)
        End If

    End Sub
End Class
