Imports System.Net
Imports System.Text

Public Class CURLProcessor
    '-----------------------------------> At the moment completely experimental <-------------------------------------------------------------------

    Public Function SendCurlRequestToTarget(Request As String) As String
        Dim myReq As HttpWebRequest
        Dim myResp As HttpWebResponse
        Dim Target As MyDataConnector = Module1.Core.GetTarget

        myReq = HttpWebRequest.Create(Target.Setting.Servername)

        myReq.Method = "POST"
        myReq.ContentType = "application/json"
        myReq.Headers.Add("Authorization", "Basic " & Convert.ToBase64String(Encoding.UTF8.GetBytes(Target.Setting.User & ":" & Target.Setting.Password)))
        Dim myData As String = Request
        myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(myData), 0, System.Text.Encoding.UTF8.GetBytes(myData).Count)
        myResp = myReq.GetResponse
        Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
        Dim myText As String
        myText = myreader.ReadToEnd

        Return myText
    End Function
    '------------------------------------------------------------------------------------------------------------------------------------------------



End Class
