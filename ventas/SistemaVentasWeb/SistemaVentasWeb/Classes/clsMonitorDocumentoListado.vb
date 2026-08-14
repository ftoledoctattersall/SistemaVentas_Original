Imports System.Data.SqlClient
<Serializable()>
Public Class clsMonitorDocumentoListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerMonitorDocumentoResumen(ByVal strCodigoCliente As String, ByVal strCodigoProducto As String, ByVal strCodigoEstado As String, ByVal strFechaDesde As String, ByVal strFechaHasta As String, ByVal intOperador As Integer, ByVal intPagina As Integer)
        Dim MonitorDocumentoLista As New List(Of clsMonitorDocumento)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_monitor_documento_resumen 20,'" + strCodigoCliente.Trim + "','" + strCodigoProducto.Trim + "','" + strCodigoEstado.Trim + "','" + strFechaDesde.Trim + "','" + strFechaHasta.Trim + "'," + intOperador.ToString + "," + intPagina.ToString
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim MonitorDocumento As New clsMonitorDocumento()
                If strCodigoEstado = "Facturados" Then
                    MonitorDocumento.DocCodigo = fnc.ObtenerFolioFactura(fnc.ObtenerDocEntryConDocNum(rdr.Item("DocCodigo"), "OINV"))
                Else
                    MonitorDocumento.DocCodigo = rdr.Item("DocCodigo")
                End If
                MonitorDocumento.CliNombre = rdr.Item("CliNombre")
                MonitorDocumento.DocFecha = rdr.Item("DocFecha")
                MonitorDocumento.EntFecha = rdr.Item("EntFecha")
                MonitorDocumento.VenFecha = rdr.Item("VenFecha")
                MonitorDocumento.DocTipo = rdr.Item("DocTipo")
                MonitorDocumento.DocEstado = rdr.Item("DocEstado")
                MonitorDocumentoLista.Add(MonitorDocumento)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return MonitorDocumentoLista
    End Function
    Public Function ObtenerMonitorDocumentoDetalle(ByVal strDocTipo As String, ByVal intDocCodigo As Integer)
        Dim MonitorDocumentoLista As New List(Of clsMonitorDocumento)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_monitor_documento_detalle '" + strDocTipo.Trim + "'," + intDocCodigo.ToString
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim MonitorDocumento As New clsMonitorDocumento()
                MonitorDocumento.ArtCodigo = rdr.Item("ArtCodigo")
                MonitorDocumento.ArtNombre = rdr.Item("ArtNombre")
                MonitorDocumento.ArtCantidad = rdr.Item("ArtCantidad")
                MonitorDocumento.ArtMoneda = rdr.Item("ArtMoneda")
                MonitorDocumento.ArtPrecioUnitario = rdr.Item("ArtPrecioUnitario")
                MonitorDocumento.ArtPrecioTotal = rdr.Item("ArtPrecioTotal")
                MonitorDocumento.PorDescuento = rdr.Item("PorDescuento")
                MonitorDocumento.ArtInteres = rdr.Item("ArtInteres")
                MonitorDocumento.FecEntrega = rdr.Item("FecEntrega")
                If (Year(rdr.Item("FecEntrega")) = 1900) Then MonitorDocumento.FecEntrega = ""
                MonitorDocumentoLista.Add(MonitorDocumento)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return MonitorDocumentoLista
    End Function
    Public Function ObtenerMonitorDocumentoPagina(ByVal strCodigoCliente As String, ByVal strCodigoProducto As String, ByVal strCodigoEstado As String, ByVal strFechaDesde As String, ByVal strFechaHasta As String, ByVal intOperador As Integer, ByVal intPagina As Integer)
        Dim MonitorDocumentoLista As New List(Of clsMonitorDocumento)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_monitor_documento_resumen 10,'" + strCodigoCliente.Trim + "','" + strCodigoProducto + "','" + strCodigoEstado.Trim + "','" + strFechaDesde.Trim + "','" + strFechaHasta.Trim + "'," + intOperador.ToString + "," + intPagina.ToString
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim MonitorDocumento As New clsMonitorDocumento()
                MonitorDocumento.DocPagina = rdr.Item("DocPagina")
                MonitorDocumentoLista.Add(MonitorDocumento)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return MonitorDocumentoLista
    End Function























































































































































































































































































































































































































































































    Public Function CancelarOrdenVenta(ByVal intDocNum As String, ByVal intUsuario As String) As Tuple(Of Integer, Integer, String)
        Dim intStatus As Integer = -100000
        Dim strStatus As String = "Error al cancelar orden de venta."
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-CL")
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
            Dim strRetorno As Tuple(Of String, String) = fnc.ObtenerUsuarioSap(intUsuario)
            Dim strSAPUsername As String = strRetorno.Item1.Trim
            Dim strSAPPassword As String = strRetorno.Item2.Trim
            Dim oCompany As New SAPbobsCOM.Company
            Dim intCodigoRetorno As Integer = 0
            Dim intDocEntry As Integer = 0
            oCompany.Server = If(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.ServerPRD, My.Resources.rscSistemaVentasWeb.ServerTST)
            oCompany.DbUserName = If(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.UserPRD, My.Resources.rscSistemaVentasWeb.UserTST)
            oCompany.DbPassword = If(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.PasswordPRD, My.Resources.rscSistemaVentasWeb.PasswordTST)
            oCompany.LicenseServer = My.Resources.rscSistemaVentasWeb.ServerLICENCIAS
            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016
            oCompany.UseTrusted = False
            oCompany.CompanyDB = If(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.DataPRD, My.Resources.rscSistemaVentasWeb.DataTST)
            oCompany.UserName = strSAPUsername
            oCompany.Password = strSAPPassword
            intCodigoRetorno = oCompany.Connect
            If intCodigoRetorno <> 0 Then
                oCompany.GetLastError(intStatus, strStatus)
                ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
            Else
                Dim oOrders As SAPbobsCOM.Documents
                oOrders = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders)
                oOrders.DocObjectCode = SAPbobsCOM.BoObjectTypes.oOrders
                intDocEntry = fnc.ObtenerDocEntryConDocNum(intDocNum, "ORDR")
                If oOrders.GetByKey(CInt(intDocEntry)) Then
                    intStatus = oOrders.Cancel()
                    If intStatus <> 0 Then
                        oCompany.GetLastError(intStatus, strStatus)
                        ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
                    Else
                        ToLog.write("Se cancelo correctamente la orden de venta : " + intDocNum.ToString, "D")
                    End If
                End If
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompany)
                oCompany = Nothing
            End If
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return New Tuple(Of Integer, Integer, String)(intDocNum, intStatus, strStatus)
    End Function
End Class