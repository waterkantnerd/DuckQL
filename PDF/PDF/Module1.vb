'--------------------------------------------------Summary--------------------------------------------------
' This is my first projekt. It's primary for learning coding and algorithms. 
' I develop this in my spare time and personally used it on some occasions in my work environment.
' I'LL GIVE NO WARRANTY!! YOU MAY USE IT AT YOUR OWN RISK!! 
' This program reads data from one database and puts it into another.
' Common usecases: Migrating data into a new schema, sync data between two independent datasources.
' Development: I'll add some more data adapters from time to time and try to optimize performance.
' I'm looking forward to your addtions, comments or ideas :)
'----------------------------------------------------------------------------------------------------------

Module Module1
    Public Core As New Core
    Sub Main()
        '------------------------------------------Start up procedure--------------------------------------------------------------------------------------------------
        ' The progam uses one or more xml files for all neccessary parameters. Every job.xml will be proccessed sequencely. 
        ' The path of this job.xml folder will be passed by parameter with start up of the program.
        ' If the program could read a xml file it'll try to connect to both database servers and try to write into the logging directory, which is passed by the 
        ' job.xml file, too. 
        ' Finally it starts the core program sequence (read, match, modify, write...).
        '-------------------------------------------Program tries to read the Job directory from parameter--------------------------------------------------------------
        Dim args() As String = Environment.GetCommandLineArgs.ToArray
        Dim Jobdir As String = ""
        Dim ENVPath As String = ""

        If args.Length < 2 Then
            ShowHelp()
            Select Case System.Console.In.ReadLine
                Case Else
                    Exit Sub
            End Select
        End If
        If IsNothing(args(0)) = True Then
            System.Console.WriteLine("No Jobdirectory found!")
            Exit Sub
        Else
            Select Case args(1)
                Case "-c"
                    Jobdir = ShowConfigDialogue()
                Case "-h"
                    ShowHelp()
                    If System.Console.In.ReadLine <> "" Then
                        Exit Sub
                    End If
                Case Else
                    If System.IO.Directory.Exists(args(1)) = True Then
                        Jobdir = args(1)
                    Else
                        System.Console.WriteLine("No valid Jobdirectory found!")
                        Exit Sub
                    End If
            End Select
        End If
        '---------------------------------------------------------------------------------------------------------------------------------------------------------------

        '------------------------------------------The program reads the xml files from the folder----------------------------------------------------------------------
        ' For each xml file the program starts the logic.
        ' In the first step it feeds all parameters from the xml into an environment object.
        Try
            ENVPath = Jobdir
        Catch ex As Exception
            System.Console.WriteLine("No 'Option XML' found!")
            Exit Sub
        End Try

        Dim XMLFiles As New XMLFiles
        Dim Jobliste As New LinkedList(Of ENV)
        Jobliste = XMLFiles.Read(ENVPath)
        Jobliste = SortJobList(Jobliste)
        For Each ENV In Jobliste
            If IsNothing(ENV) = True Then
            Else
                ' Every filled ENV object will initiate the program logic
                Start(ENV)
                EndAndClear()
            End If

        Next
        '---------------------------------------------------------------------------------------------------------------------------------------------------------------

    End Sub

    Private Function ShowConfigDialogue() As String
        Dim ConfigurationForm As New Konfiguration
        ConfigurationForm.ShowDialog()
        If Core.JobXMLPath <> "" Then
            System.Console.WriteLine("Found a Job File.")
            System.Console.WriteLine("Hit r if you want to run it.")
            System.Console.WriteLine("Hit c if you want to start the config GUI again.")
            System.Console.WriteLine("Type exit to end this program.")
            Select Case System.Console.In.ReadLine
                Case "r"
                    ShowConfigDialogue = Core.JobXMLPath
                    Exit Function
                Case "c"
                    ShowConfigDialogue()
                Case "exit"
                    ShowConfigDialogue = ""
                    Exit Function
                Case Else

            End Select
        End If
        ShowConfigDialogue = ""
    End Function

    Private Function SortJobList(Jobliste As LinkedList(Of ENV)) As LinkedList(Of ENV)
        Dim JobArr(Jobliste.Count) As ENV
        Dim Sorted As Boolean = False
        Jobliste.CopyTo(JobArr, 0)
        Dim i As Integer = 0
        While Sorted = False
            For i = 0 To Jobliste.Count - 1
                If IsNothing(JobArr(i + 1)) Then

                Else
                    If JobArr(i).OrderID > JobArr(i + 1).OrderID Then
                        Dim tmp As ENV = JobArr(i)
                        JobArr(i) = JobArr(i + 1)
                        JobArr(i + 1) = tmp
                    End If
                End If
            Next
            Sorted = True
            For i = 0 To Jobliste.Count - 1
                If IsNothing(JobArr(i + 1)) Then

                Else
                    If JobArr(i).OrderID > JobArr(i + 1).OrderID Then
                        Sorted = False
                    End If
                End If
            Next
        End While
        Jobliste.Clear()
        For Each ENV In JobArr
            If IsNothing(ENV) Then
            Else
                Jobliste.AddLast(ENV)
            End If
        Next
        SortJobList = Jobliste
    End Function

    Private Sub EndAndClear()
        '------------------------------------------------Summary--------------------------------------------------------------------------------------------------------
        ' This Routine is tidying up everything, so that the next jobfile can be processed.
        '---------------------------------------------------------------------------------------------------------------------------------------------------------------
        Core.Dispose()

    End Sub

    Private Sub Start(EnvoirementObject As ENV)
        '------------------------------------------------Summary--------------------------------------------------------------------------------------------------------
        ' The Start Sub is the central rountine to initialize all neccessary elements of the program.
        ' It loads the ENV into the core programm, so that is available from every part of the program, it initializes the log object which handles the log output
        ' and, most important, it tries to connect to the sql servers.
        ' If everything runs smoothly here, it'll jump into the "custom code" routine.
        Dim Log As New LOG


        ' initializes the central program core
        Core.CoreStart(EnvoirementObject, Log)

        Try
            ' initializes the log object
            Log.SetENV(EnvoirementObject)
            EnvoirementObject.SetTimeStamp()
            Log.StartLogger()
        Catch ex As Exception
            System.Console.WriteLine(ex.Message)
            Exit Sub
        End Try

        Try
            ' initilizes the sql object and tests the connection to the sql servers
            Log.Write(1, "DuckQL by waterkantnerd v. " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build)
            For Each SQLSetting In Core.CurrentENV.SQLServer
                Dim SQL As New MyDataConnector
                SQL.SetENV(Core.CurrentENV)
                SQL.SQLLog = (Core.CurrentLog)
                SQL.Setting = SQLSetting
                SQL.CreateSQLCon()
                If SQL.ConnectionTestSuccessful = False Then
                    Select Case SQL.Setting.Servertype
                        Case "XML"
                            Log.Write(0, "Could not connect to the specified Datafile " & SQL.Setting.FilePath)
                        Case "CSV"
                            Log.Write(0, "Could not connect to the specified Datafile " & SQL.Setting.FilePath)
                        Case "Access"
                            Log.Write(0, "Could not connect to the specified Datafile " & SQL.Setting.FilePath)
                        Case Else
                            Log.Write(0, "Could not connect to the specified Host " & SQL.Setting.Servername)
                    End Select
                End If
                Core.SQLServer.AddLast(SQL)
            Next
        Catch ex As Exception
            System.Console.WriteLine(ex.Message)
            Log.Write(0, ex.Message)
            Exit Sub
        End Try

        '----------------------------------------------Jumps into "custom code" with all core functions of the program--------------------------------------------
        'Beispiel
        StartBatch()
        '---------------------------------------------------------------------------------------------------------------------------------------------------------
    End Sub

    Private Sub StartBatch()
        '----------------------------------------------Summary----------------------------------------------------------------------------------------------------
        ' This sub is basically just the start point for the core functions.
        ' For better reading I've clustered the functions in terms of detail.
        '---------------------------------------------------------------------------------------------------------------------------------------------------------

        ' Initilizes the log object for this sub
        Dim Log As LOG = Core.CurrentLog
        Dim mytime As Date = Now()
        Dim Target As MyDataConnector = Core.GetTarget
        Log.Write(1, "Job started at " & mytime)
        Core.JobStartTime = mytime
        Core.CreateSessionStamp()

        '----------------------------------------Create a TMP Table for MS-SQL Upsert Method-----------------------------------------------------------------------
        If Target.Setting.Servertype = "MSSQL" Or Target.Setting.Servertype = "MS-SQL" Then
            If Target.Setting.TmpTableAllowed = True Then
                If Target.Setting.UseOwnTmpTable = True Then
                    Log.Write(1, "Found that a temporary table should be used...")
                    Log.Write(1, "Creating a temporary table...")
                    Dim DD As New DataDefinitions
                    DD.CreateTmpTable()
                End If
            End If
        End If
        '----------------------------------------------------------------------------------------------------------------------------------------------------------

        ' loads data from the datasource
        LadeDatenVonQuelle()
        ' writes data to targed
        SchreibeDatenInZiel()

        If Core.CurrentENV.ConsistenceCheck = True Then
            Log.Write(1, "Checking if Job is consistent...")
            If ConsistenceCheck() = True Then
            Else
                Log.Write(0, "Warning! Job was not consistent. Source and Target rows are not equal.")

            End If
        Else

        End If

        '----------------------------------------Drop TMP Table ---------------------------------------------------------------------------------------------------
        If Target.Setting.Servertype = "MSSQL" Or Target.Setting.Servertype = "MS-SQL" Then
            If Target.Setting.TmpTableAllowed = True Then
                If Target.Setting.UseOwnTmpTable = True Then
                    Log.Write(1, "Found that a temporary table should be used...")
                    Log.Write(1, "Creating a temporary table...")
                    Dim DD As New DataDefinitions
                    DD.DropTmpTable()
                End If
            End If
        End If
        '----------------------------------------------------------------------------------------------------------------------------------------------------------


        Log.Write(1, "Batch done...")
        mytime = Now()
        Core.JobEndTime = mytime
        Log.Write(1, "Job ended at " & mytime)

        'Calculate Job Time
        Dim Jobtime As TimeSpan = Core.JobEndTime - Core.JobStartTime
        Dim Sekunden As Long = Jobtime.Seconds
        Dim Minuten As Long = Jobtime.Minutes
        Dim Stunden As Long = Jobtime.Hours
        Dim Tage As Long = Jobtime.Days
        Log.Write(1, "Job took " & Tage & " days " & Stunden & " hours " & Minuten & " minutes " & Sekunden & " seconds.")
        Log.ProgramEnd = True
    End Sub

    Private Function ConsistenceCheck() As Boolean
        Dim Consistent As Boolean = False
        Dim Source As MyDataConnector = GetConnector("source")
        Dim Target As MyDataConnector = GetConnector("target")
        Dim Log As LOG = Core.CurrentLog

        If IsNothing(Source) Or IsNothing(Target) Then
            Log.Write(0, "Could not findet Source- or Target Connector. Can not check consistency!")
            ConsistenceCheck = False
            Exit Function
        End If

        Dim SQLopSource As New DataOperations
        SQLopSource.Setup(Source)
        Dim SQLopTarget As New DataOperations
        SQLopTarget.Setup(Target)
        Dim SourceRows As Long = SQLopSource.CountRows
        Dim TargetRows As Long = SQLopTarget.CountRows

        If SourceRows = TargetRows Then
            Log.Write(1, "Source Rows=" & SourceRows & " <=> Target Rows=" & TargetRows)
            Consistent = True
        Else
            Log.Write(1, "Source Rows=" & SourceRows & " != Target Rows=" & TargetRows)
            Consistent = False
            'CheckForMissingRecords(SQLopSource, SQLopTarget)
        End If

        ConsistenceCheck = Consistent
    End Function

    Private Sub CheckForMissingRecords(Source As DataOperations, Target As DataOperations)
        'Auxallary Sub ToDo: DELETE THIS SUB!
        Dim SourceDS As DataSet = Source.GetDataSet()
        Dim TargetDS As DataSet = Target.GetDataSet()
        Dim MissingRowID As Integer = 0

        For Each Item In SourceDS.Tables(0).Rows()
            Dim TargetRow() As DataRow = TargetDS.Tables(0).Select("id=" & Item(0).ToString & "")
            If TargetRow.Count = 0 Then
                Core.CurrentLog.Write(1, "!!!! FAIL !!!!")
                Core.CurrentLog.Write(1, "Source " & Item(0).ToString & " | Target --- FAIL")
                MissingRowID = CInt(Item(0).ToString)

            Else
                Core.CurrentLog.Write(1, "Source " & Item(0).ToString & " | Target " & TargetRow(0).Item(0) & " --- SUCCESS")
            End If
        Next
    End Sub

    Private Sub LadeDatenVonQuelle()
        '----------------------------------------------Summary----------------------------------------------------------------------------------------------------
        ' This sub loads the data from the source into the core.
        '---------------------------------------------------------------------------------------------------------------------------------------------------------


        '----------------------------------------------Explanation SQLoperations----------------------------------------------------------------------------------
        ' I put database operation in two layers. 
        ' One Layer for generic database operations and types, like querys, sql connectors, etc. which is in the sql class.
        ' However the program specific logic is in the sql operations class. 
        '---------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim SQLop As New DataOperations
        Dim Log As LOG = Core.CurrentLog

        Dim Connector As MyDataConnector = GetConnector("source")

        If IsNothing(Connector) Then
            Log.Write(0, "Critical Error - Could not read Source Job Connector vom Job File!")
        Else
            SQLop.Load(Connector)
        End If

    End Sub

    Private Function GetConnector(Direction As String) As MyDataConnector
        '----------------------------------------------Summary----------------------------------------------------------------------------------------------------
        ' Gets specified connector from the connectorlist in the central core
        '---------------------------------------------------------------------------------------------------------------------------------------------------------
        For Each Connector In Core.SQLServer
            If Connector.Setting.Direction.ToUpper = Direction.ToUpper Then
                GetConnector = Connector
                Exit Function
            End If
        Next
        GetConnector = Nothing
    End Function

    Private Sub SchreibeDatenInZiel()
        '----------------------------------------------Summary----------------------------------------------------------------------------------------------------
        ' This sub writes data from the program core to the target database
        '---------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim SQLop As New DataOperations
        Dim Log As LOG = Core.CurrentLog
        Dim Connector As MyDataConnector = GetConnector("target")
        If IsNothing(Connector) Then
            Log.Write(0, "Critical Error - Could not read Target Job Connector vom Job File!")
        Else
            SQLop.Fire(Connector)
        End If
    End Sub

    Private Sub ShowHelp()
        System.Console.WriteLine("Copies Data from one Database into another Database")
        System.Console.WriteLine("i.E. MS-SQL Server to MySQL Server." & vbLf & vbLf)
        System.Console.WriteLine("duckql.exe [-c] [PATH] [-h]" & vbLf)
        System.Console.WriteLine("PATH" & vbTab & "Path to configuration files. This has to be a folder.")
        System.Console.WriteLine("-c" & vbTab & "Opens configuration form.")
        System.Console.WriteLine("-h" & vbTab & "Opens help." & vbLf & vbLf)
        System.Console.WriteLine("Example to run with a config file:")
        System.Console.WriteLine("duckql.exe " & Chr(34) & "C:\Users\waterkantnerd\config files\" & Chr(34) & vbLf & vbLf)
        System.Console.WriteLine("Hit any key for exit...")
    End Sub
End Module
