Imports System.IO
Public Class FilesystemOperations
    Private Log As LOG

    Public Function FileExists(sPath As String) As Boolean
        If FileIO.FileSystem.FileExists(sPath) = True Then
            'Log.Write(1, "The File " & sPath & " does exist.")
            Return True
        Else
            'Log.Write(1, "The File " & sPath & " does NOT exist.")
            Return False
        End If
    End Function

    Public Function FileDelete(sPath As String) As Boolean
        Try
            'Log.Write(1, "Deleting " & sPath)
            FileIO.FileSystem.DeleteFile(sPath)
            Return True
        Catch ex As Exception
            'Log.Write(1, "Deleting File failed - " & ex.Message & " Path was: " & sPath)
            Return False
        End Try
    End Function

    Public Sub GetFiles(Optional Path As String = "")
        Dim Core As Core = Module1.Core
        Dim Log As LOG = Module1.Core.CurrentLog
        Dim RootDir As String = Core.CurrentENV.ContentDirectory
        Dim d As DirectoryInfo
        Dim f As FileInfo

        If Path = "" Then
            d = New DirectoryInfo(RootDir)
        Else
            d = New DirectoryInfo(Path)
        End If

        If d.Exists = False Then
            Exit Sub
        End If

        Log.Write(1, "Reading " & d.FullName)
        For Each Directory In d.GetDirectories
            GetFiles(Directory.FullName)
        Next

        For Each f In d.GetFiles
            For Each extension In Core.CurrentENV.FileExtenstions
                If f.Extension = extension Then
                    System.Console.WriteLine("Adding " & f.FullName)
                    Core.Files.AddLast(f.FullName)
                End If
            Next
        Next
    End Sub
End Class
