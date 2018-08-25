
'----------------------------------------------Explanation XML-Files (Summary)-----------------------------------------------------------------------------
' This class is for handling the XML Option file.
' It reads the folder which has been set up in the first parameter on the program start.
' Once there has been a XML file found, it will load all information to an ENV object.
'----------------------------------------------------------------------------------------------------------------------------------------------------------
Public Class XMLFiles
    Private Function LoadJobFolder(directory As String) As String()
        ' Looks up XML Files in the Folder
        Dim value As System.Collections.ObjectModel.ReadOnlyCollection(Of String)
        Dim XMLListe As New LinkedList(Of String)

        Try
            value = My.Computer.FileSystem.GetFiles(directory)
        Catch ex As Exception
            System.Console.WriteLine(ex.Message)
            LoadJobFolder = Nothing
            Exit Function
        End Try

        For Each Item In value
            If Item.LastIndexOf(".xml") = Item.Length - 4 Then
                XMLListe.AddLast(Item)
            End If
        Next Item

        If XMLListe.Count > 0 Then
            Dim JobFiles(XMLListe.Count) As String
            XMLListe.CopyTo(JobFiles, 0)
            LoadJobFolder = JobFiles
        Else
            LoadJobFolder = Nothing
        End If
    End Function

    Public Function Read(directory As String) As LinkedList(Of ENV)
        ' Initiates the reading for every file in the folder and 
        ' returns a list of ENV objects.
        Dim Jobliste As New LinkedList(Of ENV)
        If directory.Length > 0 Then
            For Each Item In LoadJobFolder(directory)
                Jobliste.AddLast(ReadJobFile(Item))
            Next
        End If
        Read = Jobliste
    End Function

    Private Function ReadJobFile(sPath As String) As ENV
        ' Reads the XML File and returns an ENV object.
        Dim ENV As New ENV
        If IsNothing(sPath) = True Then
            ReadJobFile = Nothing
            Exit Function
        End If
        Try
            Dim XMLReader As Xml.XmlReader _
          = New Xml.XmlTextReader(sPath)

            ' Reading from XML File
            With XMLReader

                Do While .Read ' As long as data available

                    ' Which type of data is there
                    Select Case .NodeType

                        Case Xml.XmlNodeType.Element
                            Select Case .Name
                                Case "Job"
                                    If .AttributeCount > 0 Then
                                        ' Reads as long as there are attributes available
                                        While .MoveToNextAttribute
                                            Select Case .Name
                                                Case "Jobname"
                                                    ENV.CreateName(.Value)
                                            End Select
                                        End While

                                    End If
                                Case "SQLServer"
                                    Dim Setting As New SQLServerSettings
                                    If .AttributeCount > 0 Then
                                        While .MoveToNextAttribute ' nächstes 
                                            Select Case .Name
                                                Case "SQL-Server-Adress"
                                                    Setting.Servername = .Value
                                                Case "Filepath"
                                                    Setting.FilePath = .Value
                                                Case "Database"
                                                    Setting.SQLDB = .Value
                                                Case "Username"
                                                    Setting.User = .Value
                                                Case "Password"
                                                    Setting.Password = .Value
                                                Case "ConnMode"
                                                    Setting.ConnMode = .Value
                                                Case "Table"
                                                    Setting.SQLTable = .Value
                                                Case "IDColumn"
                                                    Setting.IDColumn = .Value
                                                Case "IDColumnDataType"
                                                    Setting.IDColumnDataType = .Value
                                                Case "FilterType"
                                                    Setting.Filtertype = .Value
                                                Case "FilterColumn"
                                                    Setting.FilterColumn = .Value
                                                Case "FilterValue"
                                                    Setting.FilterValue = .Value
                                                Case "SQLFilter"
                                                    Setting.SQLFilter = .Value
                                                Case "ID"
                                                    Setting.ID = .Value
                                                Case "Direction"
                                                    Setting.Direction = .Value
                                                Case "TargetID"
                                                    Setting.TargetID = .Value
                                                Case "MapTargetIDColumnValue"
                                                    Setting.MapTargetIDColumnValue = .Value
                                                Case "StringSeparator"
                                                    Setting.StringSeperator = .Value
                                                Case "StringPart"
                                                    Setting.StringPart = .Value
                                                Case "InsertAllowed"
                                                    If .Value = "YES" Or .Value = "True" Then
                                                        Setting.InsertAllowed = True
                                                    Else
                                                        Setting.InsertAllowed = False
                                                    End If
                                                Case "UpdateAllowed"
                                                    If .Value = "YES" Or .Value = "True" Then
                                                        Setting.UpdateAllowed = True
                                                    Else
                                                        Setting.UpdateAllowed = False
                                                    End If
                                                Case "Servertype"
                                                    Setting.Servertype = .Value
                                                Case "SessionTimestampField"
                                                    Setting.SessionTimeStampField = .Value
                                            End Select
                                        End While
                                        ENV.SQLServer.AddLast(Setting)
                                    End If
                                Case "Mapping"
                                    If .AttributeCount > 0 Then
                                        Dim Mapping As New Mapping
                                        While .MoveToNextAttribute
                                            Select Case .Name
                                                Case "SourceColumn"
                                                    Mapping.Sourcename = .Value
                                                Case "TargetColumn"
                                                    Mapping.Targetname = .Value
                                                Case "SourceType"
                                                    Mapping.Sourcetype = .Value
                                                Case "TargetType"
                                                    Mapping.Targettype = .Value
                                                Case "StringSeparator"
                                                    Mapping.Separator = .Value
                                                Case "StringPart"
                                                    Mapping.SeperatorDirection = .Value
                                            End Select
                                        End While
                                        ENV.Mappings.AddLast(Mapping)
                                    End If

                                Case "LoggingDirectory"
                                    If .AttributeCount > 0 Then
                                        While .MoveToNextAttribute
                                            Select Case .Name
                                                Case "Adress"
                                                    ENV.SetLogPath(.Value)
                                                Case "File"
                                                    ENV.SetLogFile(.Value)
                                                Case "LogLevel"
                                                    ENV.LogLevel = .Value
                                                Case "Silent"
                                                    If .Value.ToUpper = "TRUE" Then
                                                        ENV.LogSilent = True
                                                    Else
                                                        ENV.LogSilent = False
                                                    End If
                                            End Select
                                        End While

                                    End If

                            End Select
                    End Select

                Loop
                .Close()  ' XMLTextReader close
            End With
        Catch ex As Exception
            System.Console.WriteLine(ex.Message)
            ReadJobFile = Nothing
            Exit Function
        End Try
        ReadJobFile = ENV
    End Function

    Public Sub WriteJobFile(Path As String, ENV As ENV)
        Dim enc As New System.Text.UnicodeEncoding
        Dim XMLobj As Xml.XmlTextWriter = New Xml.XmlTextWriter(Path, enc) With {
            .Formatting = Xml.Formatting.Indented,
            .Indentation = 4
        }

        XMLobj.WriteStartDocument()

        With XMLobj
            .WriteStartElement("Job")
            .WriteAttributeString("Jobname", ENV.GetName)

            .WriteStartElement("LoggingDirectory")
            .WriteAttributeString("Adress", ENV.GetLogPath)
            .WriteAttributeString("LogLevel", ENV.LogLevel)
            .WriteAttributeString("Silent", ENV.LogSilent.ToString)
            .WriteEndElement()

            Dim Source As New SQLServerSettings

            For Each Setting In ENV.SQLServer
                If Setting.Direction = "source" Then
                    Source = Setting
                End If
            Next


            .WriteStartElement("SQLServer")
            .WriteAttributeString("Direction", "source")
            .WriteAttributeString("SQL-Server-Adress", Source.Servername)
            .WriteAttributeString("Filepath", Source.FilePath)
            .WriteAttributeString("Database", Source.SQLDB)
            .WriteAttributeString("Username", Source.User)
            .WriteAttributeString("Password", Source.Password)
            .WriteAttributeString("ConnMode", Source.ConnMode)
            .WriteAttributeString("Table", Source.SQLTable)
            .WriteAttributeString("IDColumn", Source.IDColumn)
            .WriteAttributeString("IDColumnDataType", Source.IDColumnDataType)
            .WriteAttributeString("FilterType", Source.Filtertype)
            .WriteAttributeString("FilterColumn", Source.FilterColumn)
            .WriteAttributeString("FilterValue", Source.FilterValue)
            .WriteAttributeString("SQLFilter", Source.SQLFilter)
            .WriteAttributeString("Servertype", Source.Servertype)
            .WriteEndElement()

            Dim Target As New SQLServerSettings

            For Each Setting In ENV.SQLServer
                If Setting.Direction = "target" Then
                    Target = Setting
                End If
            Next

            .WriteStartElement("SQLServer")
            .WriteAttributeString("Direction", "target")
            .WriteAttributeString("SQL-Server-Adress", Target.Servername)
            .WriteAttributeString("Filepath", Target.FilePath)
            .WriteAttributeString("Database", Target.SQLDB)
            .WriteAttributeString("Username", Target.User)
            .WriteAttributeString("Password", Target.Password)
            .WriteAttributeString("ConnMode", Target.ConnMode)
            .WriteAttributeString("Table", Target.SQLTable)
            .WriteAttributeString("SessionTimestampField", Target.SessionTimestampField)
            .WriteAttributeString("IDColumn", Target.IDColumn)
            .WriteAttributeString("IDColumnDataType", Target.IDColumnDataType)
            .WriteAttributeString("MapValue", Target.MapTargetIDColumnValue)
            .WriteAttributeString("StringSeparator", Target.StringSeperator)
            .WriteAttributeString("StringPart", Target.StringPart)
            .WriteAttributeString("InsertAllowed", Target.InsertAllowed)
            .WriteAttributeString("UpdateAllowed", Target.UpdateAllowed)
            .WriteAttributeString("Servertype", Target.Servertype)
            .WriteEndElement()

            .WriteStartElement("Mappings")
            For Each Mapping In ENV.Mappings
                .WriteStartElement("Mapping")
                .WriteAttributeString("SourceColumn", Mapping.Sourcename)
                .WriteAttributeString("TargetColumn", Mapping.Targetname)
                .WriteAttributeString("SourceType", Mapping.Sourcetype)
                .WriteAttributeString("TargetType", Mapping.Targettype)
                .WriteAttributeString("StringSeperator", Mapping.Separator)
                .WriteAttributeString("StringPart", Mapping.SeperatorDirection)
                .WriteEndElement()
            Next
            .WriteEndElement()

            .WriteEndElement()
            .Close()

        End With
    End Sub
End Class
