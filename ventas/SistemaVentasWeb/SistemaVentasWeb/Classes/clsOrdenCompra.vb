Imports System.Data.SqlClient
<Serializable>
Public Class clsOrdenCompra
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Property intBaseLine As Integer
    Public Property strItemCode As String
    Public Function RegistrarOrdenCompra(ByVal intUsuario As Integer, ByVal intDocEntryOV As Integer, ByVal intDocNumOV As Integer, ByVal strProveedorOV As String, ByVal intDiaCompraOV As Integer) As Tuple(Of Integer, Integer, Integer, String)
        Dim intStatus As Integer = -100000
        Dim strStatus As String = "Error al crear orden de venta."
        Dim blnActualizo As Boolean = False
        Dim intDocNumOC As Integer = -1
        Dim intDocEntryOC As Integer = -1
        Dim intFila As Integer = 0
        Dim strCodigoProveedorCompra As String = ""
        Dim strMonedaProductoCompra As String = ""
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
                oOrders = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseOrders)
                Dim clsTransaccionListado As New clsTransaccionListado()
                For Each clsTransaccionResumen As clsTransaccionResumen In clsTransaccionListado.ObtenerTransaccionResumen(2, intDocNumOV)
                    If (clsTransaccionResumen.blnExiste) Then
                        oOrders.DocObjectCode = SAPbobsCOM.BoObjectTypes.oPurchaseOrders
                        oOrders.Series = 695   '363
                        oOrders.TaxDate = clsTransaccionResumen.strFechaOrdenVenta
                        oOrders.DocDate = clsTransaccionResumen.strFechaOrdenVenta
                        oOrders.DocDueDate = clsTransaccionResumen.strFechaOrdenVenta
                        oOrders.UserFields.Fields.Item("U_TAI_Despacho").Value = clsTransaccionResumen.intTipoDespacho
                        oOrders.ExtraDays = clsTransaccionResumen.intDiasExtras
                        oOrders.SalesPersonCode = clsTransaccionResumen.intOperador
                        oOrders.DocumentsOwner = clsTransaccionResumen.intEmpleado
                        oOrders.PaymentGroupCode = fnc.ObtenerCodigoPlazoPagoOC(intDiaCompraOV) 'clsTransaccionResumen.intCodigoPlazoCompra
                        oOrders.Comments = clsTransaccionResumen.strComentario1
                        oOrders.UserFields.Fields.Item("U_TAI_ComentarioOC").Value = clsTransaccionResumen.strComentario3
                        oOrders.UserFields.Fields.Item("U_TAI_CondicionPagOC").Value = fnc.ObtenerCodigoPlazoPagoOC(intDiaCompraOV) 'clsTransaccionResumen.intCodigoPlazoCompra
                        oOrders.UserFields.Fields.Item("U_TAI_TipoVenta").Value = clsTransaccionResumen.strTipoVenta
                        oOrders.NumAtCard = clsTransaccionResumen.strOrdenCompra
                        oOrders.UserFields.Fields.Item("U_TAI_NotaPedidoC").Value = clsTransaccionResumen.strNumeroPedido
                        oOrders.UserFields.Fields.Item("U_VK_CertOrigen").Value = "WEB"
                        oOrders.UserFields.Fields.Item("U_TAI_MonedaProducto").Value = clsTransaccionResumen.strMonedaPago
                        oOrders.UserFields.Fields.Item("U_TAI_GlosaEspecial").Value = clsTransaccionResumen.strGlosaEspecial
                        oOrders.UserFields.Fields.Item("U_TAI_Emision").Value = clsTransaccionResumen.strEmision
                        For Each clsTransaccionDetalle As clsTransaccionDetalle In clsTransaccionListado.ObtenerTransaccionDetalle(2, intDocEntryOV, strProveedorOV, intDiaCompraOV)
                            If (clsTransaccionDetalle.strCodigoProducto <> "INTERESES" And clsTransaccionDetalle.strCodigoProducto <> "DESCUENTO" And Left(clsTransaccionDetalle.strCodigoProducto, 2) <> "Z6") Then
                                oOrders.Lines.ItemCode = clsTransaccionDetalle.strCodigoProducto
                                oOrders.Lines.Quantity = clsTransaccionDetalle.intCantidadProducto
                                oOrders.Lines.WarehouseCode = clsTransaccionDetalle.strBodegaProducto
                                oOrders.Lines.UnitPrice = clsTransaccionDetalle.dblPrecioUnitarioProductoCompra
                                oOrders.Lines.Currency = clsTransaccionDetalle.strMonedaProductoCompra
                                oOrders.Lines.LineTotal = clsTransaccionDetalle.dblPrecioUnitarioProductoCompra * clsTransaccionDetalle.intCantidadProducto
                                oOrders.Lines.SalesPersonCode = clsTransaccionResumen.intOperador
                                oOrders.Lines.UserFields.Fields.Item("U_TAI_PUFinal").Value = CDbl(clsTransaccionDetalle.dblPrecioUnitarioProductoCompra)
                                oOrders.Lines.UserFields.Fields.Item("U_TAI_FechaEntrega").Value = clsTransaccionDetalle.strFechaEntregaProducto
                                oOrders.Lines.UserFields.Fields.Item("U_TAI_CardCode").Value = clsTransaccionDetalle.strCodigoProveedorCompra
                                oOrders.Lines.UserFields.Fields.Item("U_TAI_DiasCompra").Value = clsTransaccionDetalle.intDiasCompra
                                oOrders.Lines.UserFields.Fields.Item("U_TAI_PreCompraPF").Value = CDbl(clsTransaccionDetalle.dblPrecioUnitarioProductoCompra)
                                oOrders.Lines.UserFields.Fields.Item("U_TAI_MonedaPF").Value = clsTransaccionDetalle.strMonedaProductoCompra
                                oOrders.Lines.UserFields.Fields.Item("U_TAI_PrecioCompraO").Value = CDbl(clsTransaccionDetalle.dblTotalUnitarioProductoCompra)
                                oOrders.Lines.UserFields.Fields.Item("U_TAI_TasaInteres").Value = clsTransaccionDetalle.dblTasaInteresCompra
                                oOrders.Lines.DiscountPercent = 0
                                oOrders.Lines.UserFields.Fields.Item("U_TAI_CostoComercial").Value = CDbl(clsTransaccionDetalle.dblCostoComercialProducto)
                                oOrders.Lines.UserFields.Fields.Item("U_TAI_MonedaCosto").Value = clsTransaccionDetalle.strMonedaCostoComercialProducto
                                If (clsTransaccionResumen.strTipoVenta = "ventaCalzadaProveedor") Then
                                    oOrders.Lines.UserFields.Fields.Item("U_TAI_CondicionProducto").Value = clsTransaccionDetalle.strCondicionProducto
                                    oOrders.Lines.UserFields.Fields.Item("U_TAI_MotivoProducto").Value = clsTransaccionDetalle.intMotivoProducto
                                End If
                                Me.intBaseLine = intFila
                                Me.strItemCode = clsTransaccionDetalle.strCodigoProducto
                                oOrders.Lines.Add()
                                intFila += 1
                                strCodigoProveedorCompra = clsTransaccionDetalle.strCodigoProveedorCompra
                                strMonedaProductoCompra = clsTransaccionDetalle.strMonedaProductoCompra
                            End If
                        Next
                        oOrders.CardCode = strCodigoProveedorCompra
                        oOrders.DocCurrency = strMonedaProductoCompra
                        intCodigoRetorno = oOrders.Add()
                        If intCodigoRetorno <> 0 Then
                            oCompany.GetLastError(intStatus, strStatus)
                            ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
                        Else
                            intDocEntryOC = oCompany.GetNewObjectKey()
                            intDocNumOC = fnc.ObtenerDocNumConDocEntry(intDocEntryOC, "OPOR")
                            intStatus = 0
                            strStatus = ""
                            If (clsTransaccionResumen.strTipoVenta.Trim = "ventaPuestoFundo") Or (clsTransaccionResumen.strTipoVenta.Trim = "ventaCalzadaProveedor") Then
                                blnActualizo = ActualizarOrdenCompra(intDocEntryOC, intDocEntryOV)
                            End If
                        End If
                    End If
                Next
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oOrders)
                oOrders = Nothing
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompany)
            oCompany = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return New Tuple(Of Integer, Integer, Integer, String)(intStatus, intDocEntryOC, intDocNumOC, strStatus)
    End Function
    Public Function ActualizarOrdenCompra(ByVal intDocEntryOC As Integer, ByVal intDocEntryOV As Integer) As Boolean
        Dim blnRespuesta As Boolean = False
        Dim intRespuesta As Integer = 0
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            cmd = New SqlCommand("dbo.tai_vw_sp2_update_orden_compra", cnx)
            cmd.CommandTimeout = 600
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocEntry", SqlDbType.Int).Value = intDocEntryOC
            cmd.Parameters.Add("@ItemCode", SqlDbType.VarChar).Value = Me.strItemCode
            cmd.Parameters.Add("@DocEntryOV", SqlDbType.Int).Value = intDocEntryOV
            cmd.Parameters.Add("@BaseLine", SqlDbType.Int).Value = Me.intBaseLine
            intRespuesta = cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
            blnRespuesta = True
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return blnRespuesta
    End Function
End Class