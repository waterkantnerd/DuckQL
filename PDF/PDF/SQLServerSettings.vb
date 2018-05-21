Public Class SQLServerSettings
    '---------------Serversettings------------------------------------
    Public Servername As String
    Public ConnMode As String
    Public User As String
    Public Password As String
    Public SQLDB As String
    Public Servertype As String
    '-----------------------------------------------------------------

    '-------------Tablesettings---------------------------------------
    Public IDColumn As String
    Public SQLTable As String
    Public FilterColumn As String
    Public FilterValue As String

    '-----------------------------------------------------------------

    '-------------Jobsettings-----------------------------------------
    Public ID As String
    Public TimestampField As String
    Public InsertAllowed As Boolean
    Public UpdateAllowed As Boolean
    Public Direction As String 'This Parameter defines if it is a target or source setting
    Public TargetID As String
    '-----------------------------------------------------------------

    '------------Filtersettings---------------------------------------
    Public SQLFilter As String
    Public Filtertype As String

    '-----------------------------------------------------------------

    '-----------Mapping-----------------------------------------------
    Public MapTargetIDColumnValue As String
    Public StringSeperator As String
    Public StringPart As String
    '-----------------------------------------------------------------








End Class
