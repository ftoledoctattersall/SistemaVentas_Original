Imports System.Data.SqlClient
<Serializable()>
Public Class clsCotizacionListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerCotizacionResumen(ByVal intOpcion As Integer, ByVal intOperador As Integer, ByVal strEstado As String, ByVal intCotizacion As Integer)
        Dim CotizacionLista As New List(Of clsCotizacionResumen)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_cotizacion " + intOpcion.ToString.Trim + "," + intOperador.ToString.Trim + ",'" + strEstado.Trim + "'," + intCotizacion.ToString.Trim

            ToLog.write("sql: " + sql)

            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Cotizacion As New clsCotizacionResumen()

                Cotizacion.CmrCodigo = rdr.Item("CmrCodigo")
                Cotizacion.CotCodigo = rdr.Item("CotCodigo")
                Cotizacion.CotFecha = rdr.Item("CotFecha")
                Cotizacion.VenTipo = rdr.Item("VenTipo")
                Cotizacion.CliCodigo = rdr.Item("CliCodigo")
                Cotizacion.CliNombre = rdr.Item("CliNombre")
                Cotizacion.DocMoneda = rdr.Item("DocMoneda")
                Cotizacion.DocTotal = rdr.Item("DocTotal")
                Cotizacion.EmiCodigo = rdr.Item("EmiCodigo")
                Cotizacion.EmiNombre = rdr.Item("EmiNombre")
                Cotizacion.PagMoneda = rdr.Item("PagMoneda")
                Cotizacion.DesCodigo = rdr.Item("DesCodigo")
                Cotizacion.DesNombre = rdr.Item("DesNombre")
                Cotizacion.PlaCodigo = rdr.Item("PlaCodigo")
                Cotizacion.PlaNombre = rdr.Item("PlaNombre")

                CotizacionLista.Add(Cotizacion)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return CotizacionLista
    End Function
    Public Function ObtenerCotizacionDetalle(ByVal intOpcion As Integer, ByVal intOperador As Integer, ByVal strEstado As String, ByVal intCotizacion As Integer)
        Dim CotizacionLista As New List(Of clsCotizacionDetalle)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_cotizacion " + intOpcion.ToString.Trim + "," + intOperador.ToString.Trim + ",'" + strEstado.Trim + "'," + intCotizacion.ToString.Trim

            ToLog.write("sql: " + sql)

            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Cotizacion As New clsCotizacionDetalle()

                Cotizacion.BodCodigo = rdr.Item("BodCodigo")
                Cotizacion.ArtCodigo = rdr.Item("ArtCodigo")
                Cotizacion.ArtNombre = rdr.Item("ArtNombre")
                Cotizacion.ArtCantidad = rdr.Item("ArtCantidad")
                Cotizacion.ArtPrecio = rdr.Item("ArtPrecio")
                Cotizacion.ArtMoneda = rdr.Item("ArtMoneda")
                Cotizacion.ArtCambio = rdr.Item("ArtCambio")
                Cotizacion.EntFecha = Left(rdr.Item("EntFecha").ToString.Replace("-", "/"), 10)

                CotizacionLista.Add(Cotizacion)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return CotizacionLista
    End Function
End Class