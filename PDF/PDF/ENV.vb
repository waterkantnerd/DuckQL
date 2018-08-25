'----------------------------------------------Explanation ENV (Summary)-----------------------------------------------------------------------------------
' The ENV object contains all environmental options that has been set up in the xml file
'----------------------------------------------------------------------------------------------------------------------------------------------------------

Imports System.IO
Public Class ENV
    Protected JobName As String
    Public SQLServer As New LinkedList(Of SQLServerSettings)

    Public FilterColumn As String
    Public FilterType As String
    Public FilterValue As String
    Public SQLFilter As String

    Public Mappings As New LinkedList(Of Mapping)

    Protected LogPath As String
    Protected LogFile As String
    Public LogLevel As String
    Public Log As New LOG
    Public LogSilent As Boolean = False

    Protected Timestamp As String

    Public Overridable Function GetENV() As ENV
        GetENV = Me
    End Function

    Public Sub InitialisiereLog()
        Log.SetENV(Me)
    End Sub

    Public Sub SetLogObject(LogObj As LOG)
        Me.Log = Log
    End Sub

    Public Function GetLogObject() As LOG
        GetLogObject = Me.Log
    End Function

    Public Sub SetLogPath(Path As String)
        Me.LogPath = Path
    End Sub

    Public Sub SetLogFile(File As String)
        Me.LogFile = File
    End Sub

    Public Function GetLogFile() As String
        GetLogFile = Me.LogFile
    End Function

    Public Function GetLogPath() As String
        GetLogPath = Me.LogPath
    End Function

    Public Sub CreateName(sJobName As String)
        Me.JobName = sJobName
    End Sub

    Public Function GetObject() As ENV
        GetObject = Me
    End Function


    Public Function GetName() As String
        GetName = Me.JobName
    End Function

    
    Public Sub SetTimeStamp()
        Dim Jetzt As DateTime
        Jetzt = Now()
        Me.TimeStamp = Jetzt.Year & Jetzt.Month & Jetzt.Day & "-" & Jetzt.Hour & ":" & Jetzt.Minute
    End Sub

    Public Function GetTimeStamp() As String
        GetTimeStamp = Me.TimeStamp
    End Function

    Public Sub SetLockFile(sPath As String)
        Try
            Dim myWriter As New StreamWriter(sPath & "\" & "Lock.close", True)
            myWriter.WriteLine(Now)
            myWriter.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Function Locked(sPath As String) As Boolean
        Dim value As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Computer.FileSystem.GetFiles(sPath)
        Dim Log As New LOG
        Log.Write(1, value.Count & " im Jobverzeichnis!")
        If value.Count > 0 Then
            Dim JobFiles(value.Count) As String
            value.CopyTo(JobFiles, 0)
            Dim i As Integer
            For i = 0 To JobFiles.Length - 1
                If JobFiles(i) = sPath & "\Lock.close" Then
                    Locked = True
                    Exit Function
                Else
                    Locked = False
                    Exit Function
                End If
            Next
        Else
            Locked = False
            Exit Function
        End If
        Locked = True
        Exit Function
    End Function

    Public Sub DeleteLock(sPath As String)
        Try
            File.Delete(sPath & "\Lock.close")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub DeleteWorkingDirectory(Directory As String)
        Dim value As System.Collections.ObjectModel.ReadOnlyCollection(Of String)
        Try
            value = My.Computer.FileSystem.GetFiles(Directory)
        Catch ex As Exception
            Log.Write(0, ex.Message)
            Exit Sub
        End Try

        If value.Count > 0 Then
            Dim JobFiles(value.Count) As String
            value.CopyTo(JobFiles, 0)
            Dim i As Integer
            For i = 0 To JobFiles.Length - 1
                DeleteFile(JobFiles(i))
            Next
        Else

        End If
    End Sub

    Public Sub DeleteFile(sFile As String)
        Try
            File.Delete(sFile)
        Catch ex As Exception

        End Try
    End Sub
End Class

