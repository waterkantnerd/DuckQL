Public Class DataDefinitions
    'This Class handles defining Datastructures like the DDL Part in SQL
    Private Log As LOG = Module1.Core.CurrentLog
    Private Core As Core = Module1.Core
    Private ENV As ENV = Module1.Core.CurrentENV

    Public Sub CreateTmpTable()
        Dim Target As MyDataConnector = Core.GetTarget
        Dim SB As New System.Text.StringBuilder
        Target.GetTableColumnNames()

        SB.Append("CREATE TABLE ")
        SB.Append("[" & Target.Setting.SQLTable & "_" & Core.SessionStamp & "](")
        For Each Column In ENV.Mappings
            SB.Append("[" & Column.Targetname & "] ")
            SB.Append("[" & Target.GetColumnSchema(Column.Targetname).GetDataTypeName & "] ")
            If Target.GetColumnSchema(Column.Targetname).GetDataTypeName.Contains("varchar") Then
                SB.Append("(" & Target.GetColumnSchema(Column.Targetname).GetSize & ")")
            End If
            If Target.GetColumnSchema(Column.Targetname).AllowNull = True Then
                SB.Append(" NULL ")
            Else
                SB.Append(" NOT NULL ")
            End If
            SB.Append(",")
        Next
        If Target.CountPKs = 0 Then
        Else
            SB.Append(" CONSTRAINT [PK_" & Target.Setting.SQLTable & "_" & Core.SessionStamp & "] PRIMARY KEY CLUSTERED (")
            For Each Column In Target.TableSchema.Columns
                If Column.IsKey = True Then
                    SB.Append("[" & Column.Name & "]")
                End If
            Next
            SB.Append(")")
        End If

        SB.Append(")")

        Target.ExecuteQuery(SB.ToString)

        Target.Setting.TmpSQLTable = Target.Setting.SQLTable & "_" & Core.SessionStamp
    End Sub


    Public Sub DropTmpTable()
        Dim Target As MyDataConnector = Core.GetTarget
        Dim SQLrq As String = "DROP TABLE [" & Target.Setting.SQLTable & "_" & Core.SessionStamp & "]"

        Target.ExecuteQuery(SQLrq)

    End Sub

End Class
