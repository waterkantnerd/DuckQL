Public Class SQLServerSettings
    '---------------Serversettings------------------------------------
    Public Servername As String
    Public ConnMode As String
    Public User As String
    Public Password As String
    Public SQLDB As String
    Public Servertype As String
    Public FilePath As String
    Public BatchQueryAllowed As Boolean = True
    '-----------------------------------------------------------------

    '-------------Tablesettings---------------------------------------
    Public IDColumn As String
    Public IDColumnDataType As String
    Public SQLTable As String
    Public FilterColumn As String
    Public FilterValue As String
    Public XMLStartLayerLookup As Integer = 0 'In case an XML-File -> on which Layer/Level the Reader should store attributes 
    '-----------------------------------------------------------------

    '-------------Jobsettings-----------------------------------------
    Public ID As String
    Public InsertAllowed As Boolean = False
    Public UpdateAllowed As Boolean = False
    Public DeleteAllowed As Boolean = False 'Has to be implemented...
    Public Direction As String 'This Parameter defines if it is a target or source setting
    Public TargetID As String 'Has to be implemented -> more than one target allowed
    Public SessionTimestampField As String
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
