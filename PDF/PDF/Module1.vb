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
                    Dim ConfigurationForm As New Konfiguration
                    ConfigurationForm.ShowDialog()
                    If Core.JobXMLPath <> "" Then
                        System.Console.WriteLine("Found a Job File.")
                        System.Console.WriteLine("Hit r if you want to run it.")
                        System.Console.WriteLine("Type exit to end this program.")
                        Select Case System.Console.In.ReadLine
                            Case "r"
                                Jobdir = Core.JobXMLPath
                            Case "exit"
                                Exit Sub
                            Case Else

                        End Select
                    End If
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
        For Each ENV In Jobliste
            If IsNothing(ENV) = True Then
            Else
                ' Every filled ENV object will initiate the program logic
                Start(ENV)
            End If

        Next
        '---------------------------------------------------------------------------------------------------------------------------------------------------------------

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
        Catch ex As Exception
            System.Console.WriteLine(ex.Message)
            Exit Sub
        End Try

        Try
            ' initilizes the sql object and tests the connection to the sql servers
            For Each SQLSetting In Core.CurrentENV.SQLServer
                Dim SQL As New MyDataConnector
                SQL.SetENV(Core.CurrentENV)
                SQL.SQLLog = (Core.CurrentLog)
                SQL.Setting = SQLSetting
                SQL.CreateSQLCon()
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
        Log.Write(1, "Job started at " & mytime)
        Core.JobStartTime = mytime
        ' loads data from the datasource
        LadeDatenVonQuelle()
        ' writes data to targed
        SchreibeDatenInZiel()

        Log.Write(1, "Batch done...")
        mytime = Now()
        Core.JobEndTime = mytime
        Log.Write(1, "Job ended at " & mytime)

        Dim Jobtime As Long = DateDiff(DateInterval.Second, Core.JobStartTime, Core.JobEndTime)
        Dim Sekunden As Long = Jobtime Mod 60
        Dim Minuten As Long = Jobtime / 60
        Dim Stunden As Long = Minuten / 60
        Dim Tage As Long = Stunden / 24
        Log.Write(1, "Job took " & Tage & " days " & Stunden & " hours " & Minuten & " minutes " & Sekunden & " seconds.")

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
        Dim SQLop As New SQLOperations



        '----------------------------------------------This was more or less experimental...-----------------------------------------------------------------------
        'Dim LokaleFiles As New LokaleDaten
        'Dim Web As New web

        'If IsNothing(Core.CurrentENV.IndexFilePath) Or Core.CurrentENV.IndexFilePath = "" Then
        'Else
        'LokaleFiles.IndexDateiAuslesen(Core.CurrentENV.IndexFilePath)
        'web.DownloadDateiliste()
        'End If

        'If IsNothing(Core.CurrentENV.LokalWorkingDirectoryPath) Or Core.CurrentENV.LokalWorkingDirectoryPath = "" Then
        'Else
        'LokaleFiles.LadeDateienAusVerzeichnis(Core.CurrentENV.LokalWorkingDirectoryPath)
        'LokaleFiles.SchreibeDateienInDatenbank()
        'End If
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------

        For Each Connector In Core.SQLServer
            If Connector.Setting.Direction = "Source" Or Connector.Setting.Direction = "source" Then
                SQLop.Load(Connector)
            End If
        Next


    End Sub

    Private Sub SchreibeDatenInZiel()
        '----------------------------------------------Summary----------------------------------------------------------------------------------------------------
        ' This sub writes data from the program core to the target database
        '---------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim SQLop As New SQLOperations
        For Each Connector In Core.SQLServer
            If Connector.Setting.Direction = "Target" Or Connector.Setting.Direction = "target" Then
                SQLop.Fire(Connector)
            End If
        Next
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
