'----------------------------------------------Explanation Core (Summary)-------------------------------------------------------------------------
' Core is the central storrage for the program.
'----------------------------------------------------------------------------------------------------------------------------------------------------------
Public Class Core : Implements IDisposable
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
    Private QueryBlockID As Integer = 0
    Public Mappings(256) As Mapping
    Public NoSourceMappings(256) As Mapping
    Public SourceMappings(256) As Mapping
    Public SourceIndex As DataSet
    Public TargetIndex As DataSet                   ' The DataSet to check if a row already exists on target
    Public TargetDataTable As New DataTable         ' For Bulk Uploads to MS-SQL Servers...
    Public NoRowsInTargetTable As Boolean = False   ' This minimizes the checks if a row already exists in target db
    Public QueryBlock As New System.Threading.EventWaitHandle(True, Threading.EventResetMode.ManualReset)
    Public AllQueryBlocksFinished As Boolean = False
    Public DataTransferFinished As Boolean = False
    Public RowHandle As New System.Threading.EventWaitHandle(True, Threading.EventResetMode.ManualReset)
    Public LoadProccessHasFinished As Boolean = False
    Public Sourcedata As New DataTable
    Public Targetdata As DataTable
    Public Targetdataset As DataSet

    Public Async Sub QueryBlockHandler()
        While DataTransferFinished = False
            If Me.AllQueryBlocksFinished = True Then
            Else
                CheckQueryBlocks()
            End If
            If SQLCommands.Count = 0 Then
            Else
                For Each QB In SQLCommands
                    If QB.SQLqueryRunning = False Then
                        If QB.HasBeenSendToSQLServer = False Then
                            Dim Success As Boolean
                            CurrentLog.Write(1, "Sending Query Block " & QB.QBID & " to server...")
                            Success = Await QB.SendToSQLServer()
                        End If
                    End If
                Next
            End If
        End While
    End Sub

    Public Sub LoadChecker()
        While LoadProccessHasFinished = False
            If SourceIndex.Tables(0).Rows.Count = Reihen.Count Then
                Dim UnFinishedRowFound As Boolean = False
                For Each Reihe In Reihen
                    If IsNothing(Reihe) Then
                    Else
                        If Reihe.Proccessed = True Then
                        Else
                            UnFinishedRowFound = True
                        End If
                    End If
                Next
                If UnFinishedRowFound = True Then
                Else
                    LoadProccessHasFinished = True
                    CurrentLog.Write(1, "Load Process has finished!")
                    Exit Sub
                End If
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

    Public Sub Dispose() Implements IDisposable.Dispose
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
        Me.QueryBlockID = 0
        If IsNothing(Me.QueryBlock) Then
        Else
            Me.QueryBlock.Dispose()
        End If



        ReDim Mappings(256)
        ReDim NoSourceMappings(256)
        ReDim SourceMappings(256)

        If IsNothing(Me.Targetdata) Then
        Else
            Me.Targetdata.Dispose()
        End If
        If IsNothing(Me.TargetDataTable) Then
        Else
            Me.TargetDataTable.Dispose()
        End If
        If IsNothing(Me.TargetIndex) Then
        Else
            Me.TargetIndex.Dispose()
        End If
        If IsNothing(Me.Sourcedata) Then
        Else
            Me.Sourcedata.Dispose()
        End If
        If IsNothing(Me.SourceIndex) Then
        Else
            Me.SourceIndex.Dispose()
        End If
        If IsNothing(Me.Targetdataset) Then
        Else
            Me.Targetdataset.Dispose()
        End If
        NoRowsInTargetTable = False
        AllQueryBlocksFinished = False
        DataTransferFinished = False
        GC.SuppressFinalize(Me)
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
