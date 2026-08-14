Imports System.Data.SqlClient
Public Class clsMenuSistemaListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerMenuSistema(ByVal intOpcion As Integer, ByVal intPerfil As Integer, ByVal strUser As String)
        Dim MenuSistemaLista As New List(Of clsMenuSistema)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_menu_sistema " + intOpcion.ToString + "," + intPerfil.ToString '+ ",'" + strUser + "'"
            ToLog.write(sql)
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim MenuSistema As New clsMenuSistema()
                MenuSistema.ModNombre = rdr.Item("ModNombre")
                MenuSistema.OpcNombre = rdr.Item("OpcNombre")
                MenuSistema.OpcPagina = rdr.Item("OpcPagina")
                MenuSistemaLista.Add(MenuSistema)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return MenuSistemaLista
    End Function
End Class