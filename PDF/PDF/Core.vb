'----------------------------------------------Explanation Core (Summary)-------------------------------------------------------------------------
' Core is the central storrage for the program.
'----------------------------------------------------------------------------------------------------------------------------------------------------------
Public Class Core
    Public CurrentENV As ENV                        ' Environment Object
    Public CurrentLog As LOG                        ' Log Object 
    Public SQLServer As New LinkedList(Of SQL)      ' List of all SQL Server Objects. At the moment you only need two (source and target), but I plan to extend this to multiple sources/targets
    Public Reihen As New LinkedList(Of Reihe)       ' List of Datarows
    Public SessionStamp As String = ""              ' Identifier for the session
    Public TimeStamp As String = Now()              ' Timestamp for the session
    Public JobXMLPath As String = ""                ' If the config has written a job xml it is stored here for testing


    'Initializing the core
    Public Sub CoreStart(ENV As ENV, Log As LOG)
        Me.CurrentENV = ENV
        Me.CurrentLog = Log
    End Sub

    'Create a unique identifier for a session
    Public Sub CreateSessionStamp()
        Dim r As New Random
        Dim i As Integer = 0
        i = r.Next
        Me.SessionStamp = Now() & " - " & i
    End Sub
End Class
