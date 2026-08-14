Imports System.Data.SqlClient
<Serializable()>
Public Class clsMonitorAutorizacionListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerMonitorAutorizacionResumen(ByVal strCodigoCliente As String, ByVal strCodigoEstado As String, ByVal strFechaDesde As String, ByVal strFechaHasta As String, ByVal strOperador As String, ByVal intPagina As Integer)
        Dim MonitorAutorizacionLista As New List(Of clsMonitorAutorizacion)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_monitor_autorizacion_resumen 20,'" + strCodigoCliente.Trim + "','" + strCodigoEstado.Trim + "','" + strFechaDesde.Trim + "','" + strFechaHasta.Trim + "','" + strOperador.Trim + "'," + intPagina.ToString
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim MonitorAutorizacion As New clsMonitorAutorizacion()
                MonitorAutorizacion.DocEntry = rdr.Item("DocEntry")
                MonitorAutorizacion.CliNombre = rdr.Item("CliNombre")
                MonitorAutorizacion.DocFecha = rdr.Item("DocFecha")
                MonitorAutorizacion.EntFecha = rdr.Item("EntFecha")
                MonitorAutorizacion.VenFecha = rdr.Item("VenFecha")
                MonitorAutorizacion.DocTotal = rdr.Item("DocTotal")
                MonitorAutorizacion.AutTipo = rdr.Item("AutTipo")
                MonitorAutorizacion.DisCodigo = rdr.Item("DisCodigo")
                MonitorAutorizacion.DisConcepto = rdr.Item("DisConcepto")
                MonitorAutorizacion.DisTipo = rdr.Item("DisTipo")
                MonitorAutorizacion.VenTipo = rdr.Item("VenTipo")
                MonitorAutorizacion.OfiNombre = rdr.Item("OfiNombre")
                MonitorAutorizacionLista.Add(MonitorAutorizacion)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return MonitorAutorizacionLista
    End Function
    Public Function ObtenerMonitorAutorizacionPagina(ByVal strCodigoCliente As String, ByVal strCodigoEstado As String, ByVal strFechaDesde As String, ByVal strFechaHasta As String, ByVal strOperador As String, ByVal intPagina As Integer)
        Dim MonitorAutorizacionLista As New List(Of clsMonitorAutorizacion)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_monitor_autorizacion_resumen 10,'" + strCodigoCliente.Trim + "','" + strCodigoEstado.Trim + "','" + strFechaDesde.Trim + "','" + strFechaHasta.Trim + "','" + strOperador.Trim + "'," + intPagina.ToString
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim MonitorAutorizacion As New clsMonitorAutorizacion()
                MonitorAutorizacion.DocPagina = rdr.Item("DocPagina")
                MonitorAutorizacionLista.Add(MonitorAutorizacion)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return MonitorAutorizacionLista
    End Function
End Class