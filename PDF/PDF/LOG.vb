'----------------------------------------------Explanation Log (Summary)-----------------------------------------------------------------------------------
' This object handles all logging within the program... once this object is initiated
'----------------------------------------------------------------------------------------------------------------------------------------------------------
Imports System.IO
Public Class LOG
    Public Logpath As String = System.AppDomain.CurrentDomain.BaseDirectory()
    Public Logfile As String = "Undefinied.log"
    Public ErrLogFile As String = "ERR_" & Me.Logfile
    Public Testmode As Boolean = False
    Private CurrENV As ENV
    Private ReadOnly LogLines As New LinkedList(Of String)
    Private ReadOnly LogLinesErr As New LinkedList(Of String)
    Public ProgramEnd As Boolean = False
    Private t As New Threading.Thread(AddressOf Logger)
    Private FCheck As New Threading.Thread(AddressOf FileSizeChecker)
    Private EventHandle As New System.Threading.EventWaitHandle(True, Threading.EventResetMode.ManualReset)
    Private LogEventID As Long = 0
    Private LogFileSize As Long = 0
    Private ErrLogFileSize As Long = 0

    Public Function CTOR() As Boolean
        CTOR = True
    End Function

    Public Sub SetENV(EnvoirementObject As ENV)
        Me.CurrENV = EnvoirementObject
        Me.Logpath = Module1.Core.CurrentENV.GetLogPath
        Me.Logfile = Module1.Core.CurrentENV.GetName & ".log"
        If Logpath.LastIndexOf("\") = Logpath.Length Then
        Else
            Logpath = Logpath & "\"
        End If
        Me.ErrLogFile = "ERR_" & Me.Logfile
    End Sub

    Public Sub StartLogger()
        t.Start()
        FCheck.Start()
    End Sub

    Private Sub FileSizeChecker()
        'This Thread refreshes the filesize
        While ProgramEnd = False
            Me.ErrLogFileSize = CheckSize(Me.Logpath & Me.ErrLogFile)
            Me.LogFileSize = CheckSize(Me.Logpath & Me.Logfile)
            System.Threading.Thread.Sleep(5000)
        End While
    End Sub


    Private Sub Logger()
        While ProgramEnd = False
            EventHandle.WaitOne()
            If Me.LogLines.Count > 1000 Or Me.LogLinesErr.Count > 1000 Then
                WriteToFile()
            End If
        End While
        WriteToFile()
    End Sub

    Private Sub WriteToFile()


        Try
            If Me.ErrLogFileSize > 20000000 Then
                LogRename(Me.ErrLogFile, Me.Logpath)
            End If
        Catch ex As Exception
            System.Console.WriteLine("Log Line 50:" & ex.Message)
        End Try
        Try
            SyncLock EventHandle
                Dim myWriter As New StreamWriter(Logpath & ErrLogFile, True)
                For Each Line In Me.LogLinesErr
                    myWriter.WriteLine(Line)

                Next
                myWriter.Close()
                Me.LogLinesErr.Clear()
            End SyncLock
        Catch ex As Exception
            System.Console.WriteLine("Log Line 64:" & ex.Message)
        End Try


        Try
            If Me.LogFileSize > 20000000 Then
                LogRename(Me.Logfile, Me.Logpath)
            End If
        Catch ex As Exception
            System.Console.WriteLine("Log Line 74:" & ex.Message)
        End Try
        Try
            SyncLock EventHandle
                Dim LogWriter As New StreamWriter(Logpath & Logfile, True)
                Dim i As Integer = 0
                For Each Line In Me.LogLines
                    LogWriter.WriteLine(Line)
                Next

                Me.LogLines.Clear()

                LogWriter.Close()
            End SyncLock
        Catch ex As Exception
            System.Console.WriteLine("Log Line 89:" & ex.Message)
        End Try
    End Sub

    Public Function Write(ByVal sCode As Integer, ByVal sMessage As String, Optional ByVal ForceWriteToFile As Boolean = False) As Boolean
        Try
            Me.LogEventID = Me.LogEventID + 1
        Catch ex As Exception
            System.Console.WriteLine(ex.Message)
        End Try


        If Me.Testmode = True Then
            Write = True
            Exit Function
        End If

        Write = False
        If Logpath.LastIndexOf("\") = Logpath.Length - 1 Then
        Else
            Logpath = Logpath & "\"
        End If
        If sCode = 0 Then
            Me.Logfile = "ERR_" & Logfile
        Else
            Me.Logfile = CurrENV.GetName & ".log"
        End If


        ' ToDO: SyncLock Uses a lot of CPU Time!
        Select Case CurrENV.LogLevel
            ' The user can define how much log output he/she needs. At the moment there are two options: Debug Level and Error Log.
            Case 0
                'Log Level 0 = Just write the Errors (...and hope there are none ;-))
                If sCode = 0 Then
                    Try
                        If CurrENV.LogSilent = True Then
                        Else
                            System.Console.WriteLine(Me.LogEventID & " - " & sCode & " - " & Now & " - " & sMessage)
                        End If
                        SyncLock EventHandle
                            Me.LogLinesErr.AddLast(Me.LogEventID & " - " & sCode & " - " & Now & " - " & CurrENV.GetName & " - " & sMessage)
                        End SyncLock
                        If ForceWriteToFile = True Then
                            Me.WriteToFile()
                        End If
                    Catch ex As Exception
                        System.Console.WriteLine(ex.Message)
                        System.Console.WriteLine(Me.LogEventID & "-" & Now & "-" & CurrENV.OrderID & "-" & CurrENV.GetName & "-" & sMessage)
                        Exit Function
                    End Try
                Else

                End If
            Case 1
                If sCode = 0 Then
                    'Log Level 1 = Give me the complete debug --> caution this is a lot of noise.
                    Try
                        If CurrENV.LogSilent = True Then
                        Else
                            System.Console.WriteLine(Me.LogEventID & " - " & sCode & " - " & Now & " - " & sMessage)
                        End If
                        SyncLock EventHandle
                            Me.LogLinesErr.AddLast(Me.LogEventID & " - " & sCode & " - " & Now & " - " & CurrENV.GetName & " - " & sMessage)
                        End SyncLock
                        If ForceWriteToFile = True Then
                            Me.WriteToFile()
                        End If
                    Catch ex As Exception
                        System.Console.WriteLine(ex.Message)
                        System.Console.WriteLine(Me.LogEventID & "-" & Now & "-" & CurrENV.OrderID & "-" & CurrENV.GetName & "-" & sMessage)
                        Exit Function
                    End Try
                Else
                    If CurrENV.LogSilent = True Then
                    Else
                        Try
                            System.Console.WriteLine(Me.LogEventID & " - " & sCode & " - " & Now & " - " & sMessage)
                            SyncLock EventHandle
                                Me.LogLines.AddLast(Me.LogEventID & " - " & sCode & " - " & Now & " - " & CurrENV.GetName & " - " & sMessage)
                            End SyncLock
                            If ForceWriteToFile = True Then
                                Me.WriteToFile()
                            End If
                        Catch ex As Exception
                            System.Console.WriteLine(ex.Message)
                            System.Console.WriteLine(Me.LogEventID & "-" & Now & "-" & CurrENV.OrderID & "-" & CurrENV.GetName & "-" & sMessage)
                            Exit Function
                        End Try
                    End If
                End If
        End Select
        Write = True
    End Function

    Private Function CheckSize(logfile As String) As Long
        Dim I As Long
        Try
            I = My.Computer.FileSystem.GetFileInfo(logfile).Length
        Catch ex As Exception
            CheckSize = "0"
            Exit Function
        End Try
        CheckSize = I
    End Function

    Private Sub LogRename(logfile As String, LogPath As String)
        Dim NewName As String
        Dim Seppoint As Integer
        Dim Zeitstempel As String
        Seppoint = logfile.LastIndexOf(".")
        Zeitstempel = Now().ToString.Replace(":", "_")
        If Seppoint > 0 Then

            NewName = logfile.Substring(0, Seppoint) & "-" & Zeitstempel & logfile.Substring(Seppoint)
        Else
            NewName = logfile & "-" & Now() & ".log"
        End If
        Try
            Rename(LogPath & logfile, LogPath & NewName)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LogDelete(logfilePath As String)
        Try
            File.Delete(logfilePath)
        Catch ex As Exception

        End Try
    End Sub

    Private Function CheckAge(logfile As String) As Long
        ' This helper function can be implement to clean up old logs.

        Dim ChangeDate As Date
        Dim Age As Long
        Try
            ChangeDate = My.Computer.FileSystem.GetFileInfo(logfile).LastWriteTime
        Catch ex As Exception
            CheckAge = 0
            Exit Function
        End Try
        If IsNothing(ChangeDate) = False Then
            Age = DateDiff(DateInterval.Day, ChangeDate, Now())
        Else
            CheckAge = 0
            Exit Function
        End If
        CheckAge = Age
    End Function

    Private Function IsErrLog(Logfile As String) As Boolean
        Dim Seppoint As Integer
        Seppoint = Logfile.LastIndexOf("\")
        If Seppoint > 0 Then
            Logfile.Substring(Seppoint)
        End If
        If Logfile.StartsWith("err") = True Then
            IsErrLog = True
        Else
            IsErrLog = False
        End If
    End Function

    Public Sub CleanUpLoggingDirectory(LoggingDirectory As String)
        Dim value As System.Collections.ObjectModel.ReadOnlyCollection(Of String)

        Try
            value = My.Computer.FileSystem.GetFiles(LoggingDirectory)
        Catch ex As Exception
            System.Console.WriteLine(ex.Message)
            Exit Sub
        End Try

        If value.Count > 0 Then
            Dim LogFiles(value.Count) As String
            value.CopyTo(LogFiles, 0)
            For Each item In LogFiles
                If IsArchived(item) = True Then
                    If IsErrLog(item) = True Then
                        If CheckAge(item) > 90 Then
                            LogDelete(item)
                        End If
                    Else
                        If CheckAge(item) > 2 Then
                            LogDelete(item)
                        End If
                    End If

                End If
            Next
        Else

        End If
    End Sub

    Private Function IsArchived(LogFile As String) As Boolean
        Dim Seppoint1 As Integer
        Dim Seppoint2 As Integer
        If IsNothing(LogFile) = True Then
            IsArchived = False
            Exit Function
        End If
        Seppoint1 = LogFile.LastIndexOf("\")
        If Seppoint1 > 0 Then
            LogFile = LogFile.Substring(Seppoint1)
        End If

        Seppoint1 = LogFile.LastIndexOf("-")
        Seppoint2 = LogFile.IndexOf("-")

        If Seppoint1 > 0 And Seppoint2 > 0 Then
            IsArchived = True
        Else
            IsArchived = False
        End If
    End Function
    
End Class
