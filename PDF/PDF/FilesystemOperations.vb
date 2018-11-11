Imports System.IO.File
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
End Class
