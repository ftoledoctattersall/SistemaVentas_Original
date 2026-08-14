Imports System.Data.SqlClient
<Serializable()>
Public Class clsTransaccionListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerTransaccionResumen(ByVal intConsulta As Integer, ByVal intDocCodigo As Integer)
        Dim TransaccionLista As New List(Of clsTransaccionResumen)
        Dim strConsulta As String = ""
        Select Case intConsulta
            Case 1
                strConsulta = "exec tai_vw_sp2_select_borrador_venta 10"
            Case 2
                strConsulta = "exec tai_vw_sp2_select_orden_venta 10"
            Case 3
                strConsulta = "exec tai_vw_sp2_select_orden_compra 10"
        End Select
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = strConsulta + "," + intDocCodigo.ToString + ",'',0"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            If rdr.Read Then
                Dim Transaccion As New clsTransaccionResumen()
                Transaccion.intDocEntry = rdr.Item("DocEntry")
                Transaccion.intDocNum = rdr.Item("DocNum")
                Transaccion.strNombreCliente = rdr.Item("CardName")
                Transaccion.strRutCliente = rdr.Item("LicTradNum")
                Transaccion.strCodigoCliente = rdr.Item("CardCode")
                Transaccion.strCategoriaCliente = rdr.Item("U_TAI_Categoria")
                Transaccion.strTelefonoCliente = rdr.Item("phone1")
                Transaccion.strCorreoCliente = rdr.Item("E_Mail")
                Transaccion.strGiroCliente = rdr.Item("Notes")
                Transaccion.strNombreOperador = rdr.Item("SlpName")
                Transaccion.strSerie = rdr.Item("SeriesName")
                Transaccion.strNombrePlazoVenta = rdr.Item("PymntGroup")
                Transaccion.strFechaOrdenVenta = rdr.Item("TaxDate")
                Transaccion.strFechaVencimiento = rdr.Item("DocDueDate")
                Transaccion.intTipoDespacho = rdr.Item("U_TAI_Despacho")
                Transaccion.strTipoDespacho = rdr.Item("TipoDespacho")
                Transaccion.strMonedaPago = rdr.Item("U_TAI_MonedaProducto")
                Transaccion.strTipoVenta = rdr.Item("U_TAI_TipoVenta")
                Transaccion.intOperador = rdr.Item("SlpCode")
                Transaccion.strComentario1 = rdr.Item("Comments")
                Transaccion.strComentario2 = rdr.Item("Mje_OP")
                Transaccion.strComentario3 = rdr.Item("U_TAI_ComentarioOC")
                Transaccion.strMotivo = rdr.Item("motivo")
                Transaccion.strEmision = rdr.Item("emision")

                Transaccion.blnFacturado = IIf(rdr.Item("Facturado") = "Y", True, False)
                Transaccion.intFolio = rdr.Item("FolioNum")
                Transaccion.intTotalDocumento = rdr.Item("DocTotal")
                Transaccion.strCorreoVendedor = rdr.Item("email")
                Transaccion.strOrdenCompra = rdr.Item("NumAtCard")

                Transaccion.intCodigoPlazoCompra = rdr.Item("U_TAI_CondicionPagOC")
                Transaccion.strNumeroPedido = rdr.Item("U_TAI_NotaPedidoC")
                Transaccion.intDiasExtras = rdr.Item("ExtraDays")
                Transaccion.intEmpleado = rdr.Item("OwnerCode")
                Transaccion.strGlosaEspecial = rdr.Item("U_TAI_GlosaEspecial")
                Transaccion.strEstadoDocumento = rdr.Item("U_Estado_Aut")
                Transaccion.blnExiste = True

                TransaccionLista.Add(Transaccion)
            End If
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return TransaccionLista
    End Function
    Public Function ObtenerTransaccionDetalle(ByVal intConsulta As Integer, ByVal intDocCodigo As Integer, ByVal strProveedor As String, ByVal intDiaCompra As Integer)
        Dim TransaccionLista As New List(Of clsTransaccionDetalle)
        Dim strConsulta As String = ""
        Select Case intConsulta
            Case 1
                strConsulta = "exec tai_vw_sp2_select_borrador_venta 20"
            Case 2
                strConsulta = "exec tai_vw_sp2_select_orden_venta 20"
            Case 3
                strConsulta = "exec tai_vw_sp2_select_orden_compra 20"
        End Select
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = strConsulta + "," + intDocCodigo.ToString + ",'" + strProveedor.Trim() + "'," + intDiaCompra.ToString
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Transaccion As New clsTransaccionDetalle()
                Transaccion.strCodigoProducto = rdr.Item("ItemCode")
                Transaccion.strNombreProducto = rdr.Item("Dscription")
                Transaccion.intCantidadProducto = rdr.Item("Quantity")
                Transaccion.strMonedaProducto = rdr.Item("Currency")
                Transaccion.dblPrecioUnitarioProducto = rdr.Item("Price")
                Transaccion.intTotalUnitarioProducto = rdr.Item("LineTotal")
                Transaccion.dblDescuentoProducto = rdr.Item("DiscPrcnt")
                Transaccion.intInteresProducto = rdr.Item("U_TAI_Interes")
                Transaccion.intFleteProducto = rdr.Item("U_TAI_Flete")
                Transaccion.strFechaEntregaProducto = IIf(IsDBNull(rdr.Item("U_TAI_FechaEntrega")), "", rdr.Item("U_TAI_FechaEntrega"))
                Transaccion.intPrecioFinalProducto = rdr.Item("U_TAI_PUFinal")
                Transaccion.dblCostoComercialProducto = rdr.Item("U_TAI_CostoComercial")
                Transaccion.dblMonedaProducto = rdr.Item("Rate")
                Transaccion.dblDescuentoMaximo = rdr.Item("U_TAI_PrcDsctoMax")
                Transaccion.dblCostoReposicionProducto = rdr.Item("U_TAI_CostoReposicion")

                Transaccion.intDiasCompra = rdr.Item("U_TAI_DiasCompra")
                Transaccion.strMonedaProductoCompra = rdr.Item("U_TAI_MonedaPF")
                Transaccion.dblPrecioUnitarioProductoCompra = rdr.Item("U_TAI_PreCompraPF")
                Transaccion.dblTotalUnitarioProductoCompra = rdr.Item("U_TAI_PrecioCompraO")
                Transaccion.strCodigoProveedorCompra = rdr.Item("U_TAI_CardCode")

                Transaccion.strBodegaProducto = rdr.Item("WhsCode")
                Transaccion.dblTasaInteresCompra = rdr.Item("U_TAI_TasaInteres")
                Transaccion.strMonedaCostoComercialProducto = rdr.Item("U_TAI_MonedaCosto")

                Transaccion.strCondicionProducto = rdr.Item("U_TAI_CondicionProducto")
                Transaccion.intMotivoProducto = rdr.Item("codMotivoProducto")
                Transaccion.strMotivoProducto = rdr.Item("nomMotivoProducto")

                TransaccionLista.Add(Transaccion)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return TransaccionLista
    End Function
End Class