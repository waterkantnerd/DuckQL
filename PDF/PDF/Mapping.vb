'----------------------------------------------Explanation Mapping (Summary)-------------------------------------------------------------------------------
' This object stores the mapping between target and source columns defined by the user.
'----------------------------------------------------------------------------------------------------------------------------------------------------------
Public Class Mapping
    Public Sourcename As String = ""
    Public Targetname As String = ""
    Public Separator As String = ""
    Public SeperatorDirection As String = ""
    Public Sourcetype As String = ""
    Public Targettype As String = ""
    Public StaticValue As String = ""
    Public NoSource As Boolean = False
    Public HighestOccurance As Integer = 0 'If you convert a xml 2 csv you have to know the maximum of occurances of this mapping in an row, to prevent dataloss. -> Getting the Header right...
    Public UsedForColumn As Boolean = False 'This is a control bit, to check if a row is complete -> If all user defined mappings have been used a row should be complete.
    Public XMLAttributeName As String = ""
    Public XPath As String = ""
    Public UseAsIdentifier As Boolean = False
    Public SourceOrdinal As Integer = 0
    Public TargetOrdinal As Integer = 0

End Class
