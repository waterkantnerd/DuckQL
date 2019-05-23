Public Class ColumnSchema
    Public Name As String
    Public DataType As String
    Public SchemaInfo As DataRow
    Public SchemaCaption As DataColumnCollection
    Public ParentTable As DataTable

    Public Sub MergeDataType()
        If Me.DataType.IndexOf(".") >= 0 Then
            Me.DataType = Me.DataType.Substring(Me.DataType.IndexOf(".") + 1)
        End If
    End Sub

    Public Function GetSize() As String
        Select Case Module1.Core.GetTarget.Setting.Servertype
            Case "MS-SQL"
                If SchemaInfo("ColumnSize").ToString = "2147483647" Then
                    Return "max"
                Else
                    Return SchemaInfo("ColumnSize").ToString
                End If
            Case "MSSQL"
                If SchemaInfo("ColumnSize").ToString = "2147483647" Then
                    Return "max"
                Else
                    Return SchemaInfo("ColumnSize").ToString
                End If
            Case Else
                Return SchemaInfo("ColumnSize").ToString
        End Select
    End Function

    Public Function GetNumericPrecision() As String
        Return SchemaInfo("NumericPrecision").ToString
    End Function

    Public Function GetNumericScale() As String
        Return SchemaInfo("NumericScale").ToString
    End Function

    Public Function IsUnique() As Boolean
        If SchemaInfo("IsUnique") = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function IsKey() As Boolean
        If IsNothing(SchemaInfo("IsKey")) Or IsDBNull(SchemaInfo("IsKey")) Then
            Return False
        End If

        If SchemaInfo("IsKey") = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetDatatype() As String
        Return SchemaInfo("DataType").ToString
    End Function

    Public Function GetProviderSpecificDataType() As String
        Return SchemaInfo("ProviderSpecificDataType").ToString
    End Function

    Public Function GetDataTypeName() As String
        Return SchemaInfo("DataTypeName").ToString
    End Function

    Public Function GetProviderType() As String
        Return SchemaInfo("ProviderType").ToString
    End Function

    Public Function AllowNull() As Boolean
        If SchemaInfo("AllowDBNull") = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function IsIdentity() As Boolean
        If SchemaInfo("IsIdentity") = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function IsAutoIncrement() As Boolean
        If SchemaInfo("IsAutoIncrement") = True Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
