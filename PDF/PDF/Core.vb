﻿'----------------------------------------------Explanation Core (Summary)-------------------------------------------------------------------------
' Core is the central storrage for the program.
'----------------------------------------------------------------------------------------------------------------------------------------------------------
Public Class Core
    Public CurrentENV As ENV                        ' Environment Object
    Public CurrentLog As LOG                        ' Log Object 
    Public SQLServer As New LinkedList(Of MyDataConnector)      ' List of all SQL Server Objects. At the moment you only need two (source and target), but I plan to extend this to multiple sources/targets
    Public Reihen As New Queue(Of Reihe)            ' Queue of Datarows
    Public SessionStamp As String = ""              ' Identifier for the session
    Public TimeStamp As String = Now()              ' Timestamp for the session
    Public JobXMLPath As String = ""                ' If the config has written a job xml it is stored here for testing
    Public SQLCommands As New LinkedList(Of SQLQueryBlock) ' List of SQL Commands that will be send to SQL Target Server
    Public JobStartTime As Date
    Public JobEndTime As Date
    Public MaxLayers As Integer = 0 'If you convert a xml 2 csv you have to know the maximum of layers of an row, to prevent dataloss. -> will be written to the header
    Public Files As New LinkedList(Of String)       ' List of Files that should be process. -> Will be filled if a directory with flatfiles should be processed
    Public QueryCountWorked As Long = 100000000
    Private QueryBlockID As Integer = 0
    Public Mappings(256) As Mapping
    Public NoSourceMappings(256) As Mapping
    Public SourceMappings(256) As Mapping
    Public TargetIndex As DataSet                   ' The DataSet to check if a row already exists on target
    Public TargetDataTable As New DataTable         ' For Bulk Uploads to MS-SQL Servers...
    Public NoRowsInTargetTable As Boolean = False   ' This minimizes the checks if a row already exists in target db
    Public QueryBlock As New System.Threading.EventWaitHandle(True, Threading.EventResetMode.ManualReset)
    Public AllQueryBlocksFinished = False
    Public DataTransferFinished = False

    Public Sub QueryBlockHandler()
        While DataTransferFinished = False
            If Me.AllQueryBlocksFinished = True Then
            Else
                CheckQueryBlocks()
            End If
        End While
    End Sub

    Private Sub CheckQueryBlocks()
        'Checks if all Queryblocks have been sent successfully to the server...

        If SQLCommands.Count = 0 Then
            Exit Sub
        End If
        SyncLock QueryBlock
            For Each QB In SQLCommands
                If QB.Success = False Then
                    Exit Sub
                End If
            Next
        End SyncLock
        CurrentLog.Write(1, "Found that all Queryblock have finished...")
        Me.AllQueryBlocksFinished = True
    End Sub

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

    Public Sub Clear()
        Me.CurrentENV = Nothing
        Me.CurrentLog = Nothing
        Me.SQLServer.Clear()
        Me.Reihen.Clear()
        Me.SessionStamp = ""
        Me.TimeStamp = Now()
        Me.JobXMLPath = ""
        Me.SQLCommands.Clear()
        Me.JobStartTime = Nothing
        Me.JobEndTime = Nothing
        Me.QueryBlockID = 0
        Me.Files.Clear()

    End Sub

    Public Function GetTarget() As MyDataConnector
        For Each Connector In SQLServer
            If Connector.Setting.Direction.ToUpper = "TARGET" Then
                Return Connector
            End If
        Next
        Return Nothing
    End Function

    Public Function GenerateQueryBlockID() As Integer
        Me.QueryBlockID = Me.QueryBlockID + 1
        Return Me.QueryBlockID
    End Function

    Public Sub SetUpMappings()
        Dim SourceMappingsList As New LinkedList(Of Mapping)
        Dim NoSourceMappingsList As New LinkedList(Of Mapping)

        For Each Mapping In Me.CurrentENV.Mappings
            If Mapping.NoSource = True Then
                NoSourceMappingsList.AddLast(Mapping)
            Else
                SourceMappingsList.AddLast(Mapping)
            End If
        Next

        ReDim Me.Mappings(SourceMappingsList.Count - 1)
        ReDim Me.NoSourceMappings(NoSourceMappingsList.Count - 1)
        SourceMappingsList.CopyTo(Me.Mappings, 0)
        NoSourceMappingsList.CopyTo(Me.NoSourceMappings, 0)
    End Sub

End Class
