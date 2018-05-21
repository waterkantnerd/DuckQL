'----------------------------------------------Explanation SQL Parameter (Summary)---------------------------------------------------------------------------------
' Honestly this is more or less dead code.
' It's an optional class for setting up parameters for SQL querys.
' You can use it via "Execute Stored Procedure" in the SQL Class.
' A corresponding Stored Procedure in the SQL Server is required.
'----------------------------------------------------------------------------------------------------------------------------------------------------------

Public Class SQLParamter
    Public Wert As String
    Public Parametername As String
    Public Parametertyp As SqlDbType


    Public Sub Create(Name As String, Wert As String, Typ As SqlDbType)
        Me.Parametername = Name
        Me.Wert = Wert
        Me.Parametertyp = Typ
    End Sub
End Class
