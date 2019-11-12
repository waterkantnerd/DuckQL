Public Class SQLServerSettings
    '---------------Serversettings------------------------------------
    Public Servername As String
    Public ConnMode As String
    Public User As String
    Public Password As String
    Public APIKey As String
    Public Port As String
    Public SQLDB As String
    Public Index As String
    Public Servertype As String
    Public FilePath As String
    Public BatchQueryAllowed As Boolean = True
    Public MaxConnections As Integer = 0            ' ToDo: Do I really need this?
    Public Max_Paket As Integer = 524288000         ' MAX Packet Size depending on Target Server Configuration --> For Bulk Updloads... ToDo: Add this in Configuration Dialog...
    Public NotificationRows As Integer = 2500       ' Get notifications from SQL Server after this amount has been processed - ToDo: Set this into Configuration Dialouge!!
    '-----------------------------------------------------------------

    '-------------Tablesettings---------------------------------------
    Public IDColumn As String
    Public IDColumnDataType As String
    Public SQLTable As String
    Public TmpSQLTable As String
    Public FilterColumn As String
    Public FilterValue As String
    Public XMLStartLayerLookup As Integer = 0       ' In case an XML-File -> on which Layer/Level the Reader should store attributes 
    '-----------------------------------------------------------------

    '-------------Jobsettings-----------------------------------------
    Public ID As String
    Public InsertAllowed As Boolean = False
    Public UpdateAllowed As Boolean = False
    Public DeleteAllowed As Boolean = False         ' ToDo: Implement Delete Options
    Public DropIndex As Boolean = False             ' EL Option
    Public UpdateItems As Boolean = False           ' EL Option
    Public DeleteItems As Boolean = False           ' EL Option
    Public Direction As String                      ' This Parameter defines if it is a target or source setting
    Public TargetID As String                       ' ToDo: Enable more than one target table
    Public SessionTimestampField As String
    Public TmpTableAllowed As Boolean = False
    Public UseOwnTmpTable As Boolean = False
    Public PredefinedTmpTable As String = ""
    '-----------------------------------------------------------------

    '------------Filtersettings---------------------------------------
    Public SQLFilter As String
    Public Filtertype As String

    '-----------------------------------------------------------------

    '-----------Mapping-----------------------------------------------
    Public MapTargetIDColumnValue As Boolean = False
    Public StringSeperator As String
    Public StringPart As String
    '-----------------------------------------------------------------

    '-----------Config Info-------------------------------------------
    Public Tested As Boolean = False
    Public Worked As Boolean = False
    '-----------------------------------------------------------------
End Class
