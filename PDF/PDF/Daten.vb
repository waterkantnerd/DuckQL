'----------------------------------------------Explanation Daten (Summary)---------------------------------------------------------------------------------
' This class is for storing the tuples with their key and values.
' Methods of this class can manipulate the date as well for example build substrings...
' Additional manipulation methods might be added here.
'----------------------------------------------------------------------------------------------------------------------------------------------------------
Public Class Daten
    Public IdentifierCol As String = ""
    Public IdentifierVal As String = ""
    Public Mapping As Mapping
    Public Mapping2 As Mapping
    Public Wert As String
    Public Log As LOG = Module1.Core.CurrentLog
    Public SourceKey As String
    Public SourceDatatype As String
    Public TargetDatatype As String
    Public Source As MyDataConnector
    Public Target As MyDataConnector
    Public Layer As Integer = 0 'If XML Layer has to be written...
    Public Occasion As Integer = 0 'If a Source Name occurs more than one time in a row (i.e. n-XML-Tree)

    Public Sub SetUp(SourceSQL As MyDataConnector, TargetSQL As MyDataConnector)
        Me.Source = SourceSQL
        Me.Target = TargetSQL
    End Sub



    Public Sub MapDaten()
        ' This method builds a substring defined by the user.
        ' The User can define the indicator char and if the returned substring
        ' should be on the right Or on the left side
        ' of the indicator char. 
        If IsNothing(Mapping.SeperatorDirection) Then
            Exit Sub
        End If

        Select Case Mapping.SeperatorDirection
            Case "right"
                Wert = Wert.Substring(Wert.IndexOf(Mapping.Separator) + 1)
            Case "left"
                Wert = Wert.Substring(0, Wert.IndexOf(Mapping.Separator))
        End Select
    End Sub

    Public Function IsTargetIDColumn() As Boolean
        ' This function detects if the current column is an identifier defined by the user.
        Dim Found As Boolean = False
        If Me.Mapping.Targetname = Target.Setting.IDColumn Then
            Found = True
        Else
            Found = False
        End If
        IsTargetIDColumn = Found
    End Function

    Public Function GetMapping() As Boolean
        ' This function detects if the user has defined a mapping.
        Dim MappingFound As Boolean = False
        Dim ENV As ENV = Module1.Core.CurrentENV
        Dim Mappings(ENV.Mappings.Count) As Mapping
        ENV.Mappings.CopyTo(Mappings, 0)
        Dim i As Integer = 0
        While i < ENV.Mappings.Count
            If Mappings(i).Sourcename.Equals(Me.SourceKey) Then
                Me.Mapping = Mappings(i)
                i = ENV.Mappings.Count
                MappingFound = True
                Return MappingFound
            End If
            i = i + 1
        End While
        GetMapping = MappingFound
    End Function

    Public Sub CheckAndSetMaxOccurance()
        Log.Write(1, "CheckAndSetMaxOccurance - Start " & Me.Mapping.Targetname & " - " & Me.Wert)
        For Each Mapping In Module1.Core.CurrentENV.Mappings
            'If Mapping.Sourcename.Equals(Me.SourceKey) Then
            'If Me.Occasion <= Mapping.HighestOccurance Then
            'Mapping.HighestOccurance = Me.Occasion
            'End If
            'End If
        Next
        Log.Write(1, "CheckAndSetMaxOccurance - End " & Me.Mapping.Targetname & " - " & Me.Wert)
    End Sub

End Class
