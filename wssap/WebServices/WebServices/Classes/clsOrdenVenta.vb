Imports System.Data.SqlClient
Imports System.Globalization
<Serializable>
Public Class clsOrdenVenta
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Property strDocEntry As String
    Public Property strDocNum As String
    Public Function RegistrarBorradorVenta(ByVal intUsuario As Integer, ByVal strTipoVenta As String, ByVal strCliente As String, ByVal strMonedaPago As String, ByVal strOrdenCompra As String, ByVal intVendedor As Integer, ByVal intOperador As Integer, ByVal strFechaOrdenVenta As String, ByVal strFechaEntrega As String, ByVal intSerie As Integer, ByVal intTipoDespacho As Integer, ByVal strNotaPedido As String, ByVal intCodigoPlazoVenta As Integer, ByVal strFechaVencimiento As String, ByVal intCodigoPlazoCompra As Integer, ByVal strDireccionFactura As String, ByVal strDireccionDespacho As String, ByVal strComentario1 As String, ByVal strComentario2 As String, ByVal strComentario3 As String, ByVal intMotivo As Integer, ByVal strEmision As String, ByVal strGuiaDespacho As String, ByVal strPedido As String) As Tuple(Of Integer, Integer, Integer, String)
        Dim intStatus As Integer = -100000
        Dim strStatus As String = "Error al crear borrador de venta."
        Dim intDocNum As Integer = -1
        Dim intDocEntry As Integer = -1
        Dim intIndice As Integer = 0
        Dim intTotalInteres As Integer = 0
        Dim intTotalDescuento As Integer = 0
        Dim intTotalFlete As Integer = 0
        Dim strBodega As String = ""
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
            oCompany.Server = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.ServerPRD, My.Resources.rscServicioWeb.ServerTST)
            oCompany.DbUserName = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.UserPRD, My.Resources.rscServicioWeb.UserTST)
            oCompany.DbPassword = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.PasswordPRD, My.Resources.rscServicioWeb.PasswordTST)
            oCompany.LicenseServer = My.Resources.rscServicioWeb.ServerLICENCIAS
            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016
            oCompany.UseTrusted = False
            oCompany.CompanyDB = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.DataPRD, My.Resources.rscServicioWeb.DataTST)
            oCompany.UserName = strSAPUsername
            oCompany.Password = strSAPPassword
            intCodigoRetorno = oCompany.Connect
            If intCodigoRetorno <> 0 Then
                oCompany.GetLastError(intStatus, strStatus)
                ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
            Else
                Dim oDraft As SAPbobsCOM.Documents
                oDraft = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts)
                oDraft.DocObjectCode = SAPbobsCOM.BoObjectTypes.oOrders
                oDraft.CardCode = strCliente
                oDraft.Series = intSerie
                oDraft.TaxDate = DateTime.ParseExact(strFechaOrdenVenta, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                oDraft.DocDate = DateTime.ParseExact(strFechaOrdenVenta, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                oDraft.DocDueDate = DateTime.ParseExact(strFechaVencimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                oDraft.UserFields.Fields.Item("U_TAI_Despacho").Value = Integer.Parse(intTipoDespacho)
                Dim datFechaVencimiento As Date = DateTime.ParseExact(strFechaVencimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                Dim datFechaOrdenVenta As Date = DateTime.ParseExact(strFechaOrdenVenta, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                oDraft.ExtraDays = (datFechaVencimiento - datFechaOrdenVenta).TotalDays
                oDraft.SalesPersonCode = Integer.Parse(intVendedor)
                oDraft.DocumentsOwner = fnc.ObtenerCodigoEmpleadoPorCodigoOperador(Integer.Parse(intOperador))
                oDraft.PaymentGroupCode = Integer.Parse(intCodigoPlazoVenta)
                oDraft.Comments = Left(strComentario1.Trim.ToUpper, 250)
                oDraft.UserFields.Fields.Item("U_Estado_Aut").Value = "P"
                oDraft.UserFields.Fields.Item("U_VK_CertOrigen").Value = "WEB"
                oDraft.PayToCode = strDireccionFactura.Trim
                oDraft.ShipToCode = strDireccionDespacho.Trim
                oDraft.NumAtCard = strOrdenCompra.Trim
                oDraft.UserFields.Fields.Item("U_TAI_NotaPedidoC").Value = strNotaPedido.Trim
                oDraft.UserFields.Fields.Item("U_TAI_GuiaDespacho").Value = strGuiaDespacho.Trim
                oDraft.UserFields.Fields.Item("U_TAI_TipoVenta").Value = strTipoVenta.Trim
                oDraft.UserFields.Fields.Item("U_TAI_MonedaProducto").Value = strMonedaPago.Trim
                Dim strGlosaEspecial As String = ""
                If (strMonedaPago = "USD") Then strGlosaEspecial = "La condicion de venta de la presente factura es en DOLARES, por tanto la factura se debera pagar en dolares, o en su defecto se debera pagar en pesos segun el tipo de cambio observado del dia de pago."
                If (strMonedaPago = "EUR") Then strGlosaEspecial = "La condicion de venta de la presente factura es en EUROS, por tanto la factura se debera pagar en euros, o en su defecto se debera pagar en pesos segun el tipo de cambio observado del dia de pago....."
                oDraft.UserFields.Fields.Item("U_TAI_GlosaEspecial").Value = Left(strGlosaEspecial.Trim, 200)
                If (strTipoVenta.Trim = "ventaPuestoFundo" Or strTipoVenta.Trim = "ventaCalzadaProveedor" Or strTipoVenta.Trim = "ventaCostoEspecial" Or strTipoVenta.Trim = "ventaLiquidacion") Then
                    oDraft.UserFields.Fields.Item("U_TAI_CondicionPagOC").Value = intCodigoPlazoCompra
                    oDraft.UserFields.Fields.Item("U_TAI_ComentarioOC").Value = Left(strComentario3.Trim.ToUpper, 250)
                End If
                oDraft.UserFields.Fields.Item("U_TAI_Motivo").Value = Integer.Parse(intMotivo)
                oDraft.UserFields.Fields.Item("U_TAI_Emision").Value = strEmision.Trim
                Dim a As String = Chr(34) + "["
                Dim b As String = "]" + Chr(34)
                Dim aa As String = Chr(39) + "["
                Dim bb As String = "]" + Chr(39)
                Dim auxPedido As String = Replace(Replace(strPedido, a, aa), b, bb)
                Dim objPedido As JObject = JObject.Parse(auxPedido)
                Dim arrFilaPedido As JArray = objPedido("arrFilas")
                'ToLog.write("intUsuario:" + intUsuario.ToString + ",strTipoVenta:" + strTipoVenta.ToString + ",strCliente:" + strCliente.ToString + ",strMonedaPago:" + strMonedaPago.ToString + ",strOrdenCompra:" + strOrdenCompra.ToString + ",intVendedor:" + intVendedor.ToString + ",intOperador:" + intOperador.ToString + ",strFechaOrdenVenta:" + strFechaOrdenVenta.ToString + ",strFechaEntrega:" + strFechaEntrega.ToString + ",intSerie:" + intSerie.ToString + ",intTipoDespacho:" + intTipoDespacho.ToString + ",strNotaPedido:" + strNotaPedido.ToString + ",intCodigoPlazoVenta:" + intCodigoPlazoVenta.ToString + ",strFechaVencimiento:" + strFechaVencimiento.ToString + ",intCodigoPlazoCompra:" + intCodigoPlazoCompra.ToString + ",strDireccionFactura:" + strDireccionFactura.ToString + ",strDireccionDespacho:" + strDireccionDespacho.ToString + ",strComentario1:" + strComentario1.ToString + ",strComentario2:" + strComentario2.ToString + ",strComentario3:" + strComentario3.ToString + ",auxPedido:" + auxPedido.ToString, "D")
                For intIndice = 0 To arrFilaPedido.Count - 1 Step 1
                    Dim FilaPedido As JObject = arrFilaPedido(intIndice)
                    oDraft.Lines.ItemCode = FilaPedido("txtCodigoProducto")
                    oDraft.Lines.Quantity = Integer.Parse(FilaPedido("txtCantidadProducto"))
                    oDraft.Lines.WarehouseCode = FilaPedido("hdnBodegaProducto")
                    oDraft.Lines.UnitPrice = Double.Parse(FilaPedido("txtPrecioUnitarioProducto"))
                    oDraft.Lines.Currency = FilaPedido("txtMonedaProducto")
                    oDraft.Lines.LineTotal = Double.Parse(FilaPedido("txtTotalUnitarioProducto"))
                    oDraft.Lines.UserFields.Fields.Item("U_TAI_Interes").Value = Integer.Parse(FilaPedido("txtInteresProducto"))
                    oDraft.Lines.UserFields.Fields.Item("U_TAI_Descuento").Value = Integer.Parse(FilaPedido("hdnDescuentoProducto"))
                    oDraft.Lines.UserFields.Fields.Item("U_TAI_ImpteComis").Value = 0
                    oDraft.Lines.UserFields.Fields.Item("U_TAI_PrcDsctoMax").Value = fnc.ObtenerDescuentoMaximoProducto(FilaPedido("hdnBodegaProducto"), FilaPedido("txtCodigoProducto"), strFechaOrdenVenta)
                    oDraft.Lines.UserFields.Fields.Item("U_TAI_PUFinal").Value = Double.Parse(FilaPedido("txtTotalUnitarioProducto")) + Double.Parse(FilaPedido("txtInteresProducto"))
                    oDraft.Lines.UserFields.Fields.Item("U_TAI_FechaEntrega").Value = DateTime.ParseExact(FilaPedido("txtFechaEntregaProducto"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    oDraft.Lines.UserFields.Fields.Item("U_TAI_DescripcionAd").Value = FilaPedido("hdnHibridoProducto")
                    oDraft.Lines.UserFields.Fields.Item("U_TAI_Flete").Value = Double.Parse(FilaPedido("txtFleteProducto"))
                    oDraft.Lines.UserFields.Fields.Item("U_TAI_PorcVenta").Value = Double.Parse(FilaPedido("auxPorcentajeFleteProducto"))
                    oDraft.Lines.UserFields.Fields.Item("U_TAI_MonedaCosto").Value = FilaPedido("txtMonedaProducto")
                    oDraft.Lines.UserFields.Fields.Item("U_TAI_CostoComercial").Value = Double.Parse(FilaPedido("hdnCostoComercialProducto"))
                    oDraft.Lines.UserFields.Fields.Item("U_TAI_CostoReposicio").Value = Double.Parse(FilaPedido("hdnCostoReposicionProducto"))
                    If (strTipoVenta.Trim = "ventaPuestoFundo" Or strTipoVenta.Trim = "ventaCalzadaProveedor" Or strTipoVenta.Trim = "ventaCostoEspecial" Or strTipoVenta.Trim = "ventaLiquidacion") Then
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_CardCode").Value = FilaPedido("txtCodigoProveedorCompra")
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_DiasCompra").Value = Integer.Parse(FilaPedido("hdnDiasCompra"))
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_PreCompraPF").Value = Double.Parse(FilaPedido("txtPrecioUnitarioProductoCompra"))
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_MonedaPF").Value = FilaPedido("txtMonedaProductoCompra")
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_PrecioCompraO").Value = Double.Parse(Replace((Double.Parse(FilaPedido("txtTotalUnitarioProductoCompra")) / Integer.Parse(FilaPedido("txtCantidadProducto"))).ToString, ",", "."))
                        If (Integer.Parse(FilaPedido("hdnInventariableProducto")) = 1) Then oDraft.Lines.UserFields.Fields.Item("U_TAI_TasaInteres").Value = Double.Parse(Replace(FilaPedido("txtTasaInteresCompra"), ",", "."))
                        If (strTipoVenta.Trim = "ventaCalzadaProveedor") Then
                            oDraft.Lines.UserFields.Fields.Item("U_TAI_CondicionProducto").Value = FilaPedido("txtCondicion").ToString.ToUpper
                            oDraft.Lines.UserFields.Fields.Item("U_TAI_MotivoProducto").Value = Integer.Parse(FilaPedido("hdnMotivo"))
                        End If
                    Else
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_MonedaPF").Value = FilaPedido("txtMonedaProducto")
                        If (Integer.Parse(FilaPedido("hdnInventariableProducto")) = 1) Then oDraft.Lines.UserFields.Fields.Item("U_TAI_TasaInteres").Value = fnc.ObtenerTasaInteresProducto("TASAINTM")
                    End If
                    oDraft.Lines.Add()
                    intTotalInteres += Integer.Parse(FilaPedido("txtInteresProducto"))
                    intTotalDescuento += Integer.Parse(FilaPedido("hdnDescuentoProducto"))
                    intTotalFlete += Integer.Parse(FilaPedido("txtFleteProducto"))
                    strBodega = FilaPedido("hdnBodegaProducto")
                Next
                If intTotalInteres > 0 Then
                    oDraft.Lines.ItemCode = "INTERESES"
                    oDraft.Lines.Quantity = 1
                    oDraft.Lines.WarehouseCode = "00"
                    oDraft.Lines.UnitPrice = intTotalInteres
                    oDraft.Lines.Currency = "CLP"
                    oDraft.Lines.Add()
                End If
                If intTotalDescuento > 0 And (strTipoVenta.Trim = "ventaPuestoFundo" Or strTipoVenta.Trim = "ventaCalzadaProveedor" Or strTipoVenta.Trim = "ventaCostoEspecial" Or strTipoVenta.Trim = "ventaLiquidacion") Then
                    oDraft.Lines.ItemCode = "DESCUENTO"
                    oDraft.Lines.Quantity = 1
                    oDraft.Lines.WarehouseCode = strBodega.Trim '"00"
                    oDraft.Lines.UnitPrice = intTotalDescuento
                    oDraft.Lines.Currency = "CLP"
                    oDraft.Lines.Add()
                    oDraft.Lines.ItemCode = "Z8200003000000"
                    oDraft.Lines.Quantity = -1
                    oDraft.Lines.WarehouseCode = strBodega.Trim '"00"
                    oDraft.Lines.UnitPrice = intTotalDescuento
                    oDraft.Lines.Currency = "CLP"
                    oDraft.Lines.Add()
                End If
                If intTotalFlete > 0 Then
                    oDraft.Lines.ItemCode = fnc.ObtenerCuentaContable(strBodega)
                    oDraft.Lines.Quantity = 1
                    oDraft.Lines.WarehouseCode = "00"
                    oDraft.Lines.LineTotal = intTotalFlete
                    oDraft.Lines.Currency = "CLP"
                    oDraft.Lines.Add()
                End If
                intCodigoRetorno = oDraft.Add()
                If intCodigoRetorno <> 0 Then
                    oCompany.GetLastError(intStatus, strStatus)
                    ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
                Else
                    intDocEntry = oCompany.GetNewObjectKey()
                    intDocNum = fnc.ObtenerDocNumConDocEntry(intDocEntry, "ODRF")
                    Me.strDocEntry = CStr(intDocEntry)
                    Me.strDocNum = CStr(intDocNum)
                    intStatus = 0
                    strStatus = "Vacio"
                End If
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oDraft)
                oDraft = Nothing
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompany)
            oCompany = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return New Tuple(Of Integer, Integer, Integer, String)(intStatus, intDocNum, intDocEntry, strStatus)
    End Function
    Public Function RegistrarOrdenVenta(ByVal intUsuario As Integer, ByVal strTipoVenta As String, ByVal strCliente As String, ByVal strMonedaPago As String, ByVal strOrdenCompra As String, ByVal intVendedor As Integer, ByVal intOperador As Integer, ByVal strFechaOrdenVenta As String, ByVal strFechaEntrega As String, ByVal intSerie As Integer, ByVal intTipoDespacho As Integer, ByVal strNotaPedido As String, ByVal intCodigoPlazoVenta As Integer, ByVal strFechaVencimiento As String, ByVal intCodigoPlazoCompra As Integer, ByVal strDireccionFactura As String, ByVal strDireccionDespacho As String, ByVal strComentario1 As String, ByVal strComentario2 As String, ByVal strComentario3 As String, ByVal intMotivo As Integer, ByVal strEmision As String, ByVal strGuiaDespacho As String, ByVal strPedido As String) As Tuple(Of Integer, Integer, Integer, String)
        Dim intStatus As Integer = -100000
        Dim strStatus As String = "Error al crear orden de venta."
        Dim intDocNum As Integer = -1
        Dim intDocEntry As Integer = -1
        Dim intIndice As Integer = 0
        Dim intTotalInteres As Integer = 0
        Dim intTotalDescuento As Integer = 0
        Dim intTotalFlete As Integer = 0
        Dim strBodega As String = ""
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
            oCompany.Server = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.ServerPRD, My.Resources.rscServicioWeb.ServerTST)
            oCompany.DbUserName = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.UserPRD, My.Resources.rscServicioWeb.UserTST)
            oCompany.DbPassword = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.PasswordPRD, My.Resources.rscServicioWeb.PasswordTST)
            oCompany.LicenseServer = My.Resources.rscServicioWeb.ServerLICENCIAS
            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016
            oCompany.UseTrusted = False
            oCompany.CompanyDB = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.DataPRD, My.Resources.rscServicioWeb.DataTST)
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
                oOrders.CardCode = strCliente
                oOrders.Series = intSerie
                oOrders.TaxDate = DateTime.ParseExact(strFechaOrdenVenta, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                oOrders.DocDate = DateTime.ParseExact(strFechaOrdenVenta, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                oOrders.DocDueDate = DateTime.ParseExact(strFechaVencimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                oOrders.UserFields.Fields.Item("U_TAI_Despacho").Value = Integer.Parse(intTipoDespacho)
                Dim datFechaVencimiento As Date = DateTime.ParseExact(strFechaVencimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                Dim datFechaOrdenVenta As Date = DateTime.ParseExact(strFechaOrdenVenta, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                oOrders.ExtraDays = (datFechaVencimiento - datFechaOrdenVenta).TotalDays
                oOrders.SalesPersonCode = Integer.Parse(intVendedor)
                oOrders.DocumentsOwner = fnc.ObtenerCodigoEmpleadoPorCodigoOperador(Integer.Parse(intOperador))
                oOrders.PaymentGroupCode = Integer.Parse(intCodigoPlazoVenta)
                oOrders.Comments = Left(strComentario1.Trim.ToUpper, 250)
                oOrders.UserFields.Fields.Item("U_Estado_Aut").Value = "P"
                oOrders.UserFields.Fields.Item("U_VK_CertOrigen").Value = "WEB"
                oOrders.PayToCode = strDireccionFactura.Trim
                oOrders.ShipToCode = strDireccionDespacho.Trim
                oOrders.NumAtCard = strOrdenCompra.Trim
                oOrders.UserFields.Fields.Item("U_TAI_NotaPedidoC").Value = strNotaPedido.Trim
                oOrders.UserFields.Fields.Item("U_TAI_GuiaDespacho").Value = strGuiaDespacho.Trim
                oOrders.UserFields.Fields.Item("U_TAI_TipoVenta").Value = strTipoVenta.Trim
                oOrders.UserFields.Fields.Item("U_TAI_MonedaProducto").Value = strMonedaPago.Trim
                Dim strGlosaEspecial As String = ""
                If (strMonedaPago = "USD") Then strGlosaEspecial = "La condicion de venta de la presente factura es en DOLARES, por tanto la factura se debera pagar en dolares, o en su defecto se debera pagar en pesos segun el tipo de cambio observado del dia de pago."
                If (strMonedaPago = "EUR") Then strGlosaEspecial = "La condicion de venta de la presente factura es en EUROS, por tanto la factura se debera pagar en euros, o en su defecto se debera pagar en pesos segun el tipo de cambio observado del dia de pago....."
                oOrders.UserFields.Fields.Item("U_TAI_GlosaEspecial").Value = Left(strGlosaEspecial.Trim, 200)
                If (strTipoVenta.Trim = "ventaPuestoFundo" Or strTipoVenta.Trim = "ventaCalzadaProveedor" Or strTipoVenta.Trim = "ventaCostoEspecial" Or strTipoVenta.Trim = "ventaLiquidacion") Then
                    oOrders.UserFields.Fields.Item("U_TAI_CondicionPagOC").Value = intCodigoPlazoCompra
                    oOrders.UserFields.Fields.Item("U_TAI_ComentarioOC").Value = Left(strComentario3.Trim.ToUpper, 250)
                End If
                oOrders.UserFields.Fields.Item("U_TAI_Motivo").Value = Integer.Parse(intMotivo)
                oOrders.UserFields.Fields.Item("U_TAI_Emision").Value = strEmision.Trim
                Dim a As String = Chr(34) + "["
                Dim b As String = "]" + Chr(34)
                Dim aa As String = Chr(39) + "["
                Dim bb As String = "]" + Chr(39)
                Dim auxPedido As String = Replace(Replace(strPedido, a, aa), b, bb)
                Dim objPedido As JObject = JObject.Parse(auxPedido)
                Dim arrFilaPedido As JArray = objPedido("arrFilas")
                'ToLog.write("intUsuario:" + intUsuario.ToString + ",strTipoVenta:" + strTipoVenta.ToString + ",strCliente:" + strCliente.ToString + ",strMonedaPago:" + strMonedaPago.ToString + ",strOrdenCompra:" + strOrdenCompra.ToString + ",intVendedor:" + intVendedor.ToString + ",intOperador:" + intOperador.ToString + ",strFechaOrdenVenta:" + strFechaOrdenVenta.ToString + ",strFechaEntrega:" + strFechaEntrega.ToString + ",intSerie:" + intSerie.ToString + ",intTipoDespacho:" + intTipoDespacho.ToString + ",strNotaPedido:" + strNotaPedido.ToString + ",intCodigoPlazoVenta:" + intCodigoPlazoVenta.ToString + ",strFechaVencimiento:" + strFechaVencimiento.ToString + ",intCodigoPlazoCompra:" + intCodigoPlazoCompra.ToString + ",strDireccionFactura:" + strDireccionFactura.ToString + ",strDireccionDespacho:" + strDireccionDespacho.ToString + ",strComentario1:" + strComentario1.ToString + ",strComentario2:" + strComentario2.ToString + ",strComentario3:" + strComentario3.ToString + ",auxPedido:" + auxPedido.ToString, "D")
                For intIndice = 0 To arrFilaPedido.Count - 1 Step 1
                    Dim FilaPedido As JObject = arrFilaPedido(intIndice)
                    oOrders.Lines.ItemCode = FilaPedido("txtCodigoProducto")
                    oOrders.Lines.Quantity = Integer.Parse(FilaPedido("txtCantidadProducto"))
                    oOrders.Lines.WarehouseCode = FilaPedido("hdnBodegaProducto")
                    oOrders.Lines.UnitPrice = Double.Parse(FilaPedido("txtPrecioUnitarioProducto"))
                    oOrders.Lines.Currency = FilaPedido("txtMonedaProducto")
                    oOrders.Lines.LineTotal = Double.Parse(FilaPedido("txtTotalUnitarioProducto"))
                    oOrders.Lines.UserFields.Fields.Item("U_TAI_Interes").Value = Integer.Parse(FilaPedido("txtInteresProducto"))
                    oOrders.Lines.UserFields.Fields.Item("U_TAI_Descuento").Value = Integer.Parse(FilaPedido("hdnDescuentoProducto"))
                    oOrders.Lines.UserFields.Fields.Item("U_TAI_ImpteComis").Value = 0
                    oOrders.Lines.UserFields.Fields.Item("U_TAI_PrcDsctoMax").Value = fnc.ObtenerDescuentoMaximoProducto(FilaPedido("hdnBodegaProducto"), FilaPedido("txtCodigoProducto"), strFechaOrdenVenta)
                    oOrders.Lines.UserFields.Fields.Item("U_TAI_PUFinal").Value = Double.Parse(FilaPedido("txtTotalUnitarioProducto")) + Double.Parse(FilaPedido("txtInteresProducto"))
                    oOrders.Lines.UserFields.Fields.Item("U_TAI_FechaEntrega").Value = DateTime.ParseExact(FilaPedido("txtFechaEntregaProducto"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    oOrders.Lines.UserFields.Fields.Item("U_TAI_DescripcionAd").Value = FilaPedido("hdnHibridoProducto") 'REVISAR
                    oOrders.Lines.UserFields.Fields.Item("U_TAI_Flete").Value = Double.Parse(FilaPedido("txtFleteProducto"))
                    oOrders.Lines.UserFields.Fields.Item("U_TAI_PorcVenta").Value = Double.Parse(FilaPedido("auxPorcentajeFleteProducto"))
                    oOrders.Lines.UserFields.Fields.Item("U_TAI_MonedaCosto").Value = FilaPedido("txtMonedaProducto")
                    oOrders.Lines.UserFields.Fields.Item("U_TAI_CostoComercial").Value = Double.Parse(FilaPedido("hdnCostoComercialProducto"))
                    oOrders.Lines.UserFields.Fields.Item("U_TAI_CostoReposicio").Value = Double.Parse(FilaPedido("hdnCostoReposicionProducto"))
                    If (strTipoVenta.Trim = "ventaPuestoFundo" Or strTipoVenta.Trim = "ventaCalzadaProveedor" Or strTipoVenta.Trim = "ventaCostoEspecial" Or strTipoVenta.Trim = "ventaLiquidacion") Then
                        oOrders.Lines.UserFields.Fields.Item("U_TAI_CardCode").Value = FilaPedido("txtCodigoProveedorCompra")
                        oOrders.Lines.UserFields.Fields.Item("U_TAI_DiasCompra").Value = Integer.Parse(FilaPedido("hdnDiasCompra"))
                        oOrders.Lines.UserFields.Fields.Item("U_TAI_PreCompraPF").Value = Double.Parse(FilaPedido("txtPrecioUnitarioProductoCompra"))
                        oOrders.Lines.UserFields.Fields.Item("U_TAI_MonedaPF").Value = FilaPedido("txtMonedaProductoCompra")
                        oOrders.Lines.UserFields.Fields.Item("U_TAI_PrecioCompraO").Value = Double.Parse(Replace((Double.Parse(FilaPedido("txtTotalUnitarioProductoCompra")) / Integer.Parse(FilaPedido("txtCantidadProducto"))).ToString, ",", "."))
                        If (Integer.Parse(FilaPedido("hdnInventariableProducto")) = 1) Then oOrders.Lines.UserFields.Fields.Item("U_TAI_TasaInteres").Value = Double.Parse(Replace(FilaPedido("txtTasaInteresCompra"), ",", "."))
                        If (strTipoVenta.Trim = "ventaCalzadaProveedor") Then
                            oOrders.Lines.UserFields.Fields.Item("U_TAI_CondicionProducto").Value = FilaPedido("txtCondicion").ToString.ToUpper
                            oOrders.Lines.UserFields.Fields.Item("U_TAI_MotivoProducto").Value = Integer.Parse(FilaPedido("hdnMotivo"))
                        End If
                    Else
                        oOrders.Lines.UserFields.Fields.Item("U_TAI_MonedaPF").Value = FilaPedido("txtMonedaProducto")
                        If (Integer.Parse(FilaPedido("hdnInventariableProducto")) = 1) Then oOrders.Lines.UserFields.Fields.Item("U_TAI_TasaInteres").Value = fnc.ObtenerTasaInteresProducto("TASAINTM")
                    End If
                    oOrders.Lines.Add()
                    intTotalInteres += Integer.Parse(FilaPedido("txtInteresProducto"))
                    intTotalDescuento += Integer.Parse(FilaPedido("hdnDescuentoProducto"))
                    intTotalFlete += Integer.Parse(FilaPedido("txtFleteProducto"))
                    strBodega = FilaPedido("hdnBodegaProducto")
                Next
                If intTotalInteres > 0 Then
                    oOrders.Lines.ItemCode = "INTERESES"
                    oOrders.Lines.Quantity = 1
                    oOrders.Lines.WarehouseCode = strBodega.Trim '"00"
                    oOrders.Lines.UnitPrice = intTotalInteres
                    oOrders.Lines.Currency = "CLP"
                    oOrders.Lines.Add()
                End If
                If intTotalDescuento > 0 And (strTipoVenta.Trim = "ventaPuestoFundo" Or strTipoVenta.Trim = "ventaCalzadaProveedor" Or strTipoVenta.Trim = "ventaCostoEspecial" Or strTipoVenta.Trim = "ventaLiquidacion") Then
                    oOrders.Lines.ItemCode = "DESCUENTO"
                    oOrders.Lines.Quantity = 1
                    oOrders.Lines.WarehouseCode = strBodega.Trim '"00"
                    oOrders.Lines.UnitPrice = intTotalDescuento
                    oOrders.Lines.Currency = "CLP"
                    oOrders.Lines.Add()
                    oOrders.Lines.ItemCode = "Z8200003000000"
                    oOrders.Lines.Quantity = -1
                    oOrders.Lines.WarehouseCode = strBodega.Trim '"00"
                    oOrders.Lines.UnitPrice = intTotalDescuento
                    oOrders.Lines.Currency = "CLP"
                    oOrders.Lines.Add()
                End If
                If intTotalFlete > 0 Then
                    oOrders.Lines.ItemCode = fnc.ObtenerCuentaContable(strBodega)
                    oOrders.Lines.Quantity = 1
                    oOrders.Lines.WarehouseCode = "00"
                    oOrders.Lines.LineTotal = intTotalFlete
                    oOrders.Lines.Currency = "CLP"
                    oOrders.Lines.Add()
                End If
                intCodigoRetorno = oOrders.Add()
                If intCodigoRetorno <> 0 Then
                    oCompany.GetLastError(intStatus, strStatus)
                    ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
                Else
                    intDocEntry = oCompany.GetNewObjectKey()
                    intDocNum = fnc.ObtenerDocNumConDocEntry(intDocEntry, "ORDR")
                    Me.strDocNum = CStr(intDocEntry)
                    Me.strDocEntry = CStr(intDocNum)
                    intStatus = 0
                    strStatus = "x"
                End If
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
        Return New Tuple(Of Integer, Integer, Integer, String)(intStatus, intDocNum, intDocEntry, strStatus)
    End Function
    Public Function RegistrarBorradorVentaEnOrdenVenta(ByVal intUsuario As Integer, ByVal intDraftKey As Integer) As Tuple(Of Integer, String)
        Dim intStatus As Integer = -100000
        Dim strStatus As String = "Error al pasar borrador de venta a orden de venta, intente nuevamente."
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-CL")
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
            Dim strRetorno As Tuple(Of String, String) = fnc.ObtenerUsuarioSap(intUsuario)
            Dim strSAPUsername As String = strRetorno.Item1.Trim
            Dim strSAPPassword As String = strRetorno.Item2.Trim
            Dim oCompany1 As New SAPbobsCOM.Company
            Dim intCodigoRetorno As Integer = 0
            oCompany1.Server = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.ServerPRD, My.Resources.rscServicioWeb.ServerTST)
            oCompany1.DbUserName = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.UserPRD, My.Resources.rscServicioWeb.UserTST)
            oCompany1.DbPassword = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.PasswordPRD, My.Resources.rscServicioWeb.PasswordTST)
            oCompany1.LicenseServer = My.Resources.rscServicioWeb.ServerLICENCIAS
            oCompany1.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016
            oCompany1.UseTrusted = False
            oCompany1.CompanyDB = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.DataPRD, My.Resources.rscServicioWeb.DataTST)
            oCompany1.UserName = strSAPUsername
            oCompany1.Password = strSAPPassword
            intCodigoRetorno = oCompany1.Connect
            If intCodigoRetorno <> 0 Then
                oCompany1.GetLastError(intStatus, strStatus)
                ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
            Else
                Dim oDraft As SAPbobsCOM.Documents
                oDraft = oCompany1.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts)
                If oDraft.GetByKey(intDraftKey) Then
                    intCodigoRetorno = oDraft.SaveDraftToDocument()
                    If intCodigoRetorno <> 0 Then
                        oCompany1.GetLastError(intStatus, strStatus)
                        ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
                    Else
                        intStatus = fnc.ObtenerDocNumConDraftKey(intDraftKey, "ORDR")
                    End If
                Else
                    intStatus = -1
                    strStatus = "Borrador de venta no existe."
                    ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
                End If
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oDraft)
                oDraft = Nothing
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompany1)
            oCompany1 = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        Finally
        End Try
        Return New Tuple(Of Integer, String)(intStatus, strStatus)
    End Function
    'Public Function RegistrarBorradorVentaEnFacturaVenta(ByVal intUsuario As Integer, ByVal intDraftKey As Integer) As Tuple(Of Integer, String)
    '    Dim intStatus As Integer = -100000
    '    Dim strStatus As String = "Error al pasar borrador de venta a factura de venta."
    '    Try
    '        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-CL")
    '        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
    '        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
    '        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
    '        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
    '        Dim strRetorno As Tuple(Of String, String) = fnc.ObtenerUsuarioSap(intUsuario)
    '        Dim strSAPUsername As String = strRetorno.Item1.Trim
    '        Dim strSAPPassword As String = strRetorno.Item2.Trim
    '        Dim oCompany As New SAPbobsCOM.Company
    '        Dim intCodigoRetorno As Integer = 0
    '        oCompany.Server = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.ServerPRD, My.Resources.rscServicioWeb.ServerTST)
    '        oCompany.DbUserName = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.UserPRD, My.Resources.rscServicioWeb.UserTST)
    '        oCompany.DbPassword = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.PasswordPRD, My.Resources.rscServicioWeb.PasswordTST)
    '        oCompany.LicenseServer = My.Resources.rscServicioWeb.ServerLICENCIAS
    '        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016
    '        oCompany.UseTrusted = False
    '        oCompany.CompanyDB = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.DataPRD, My.Resources.rscServicioWeb.DataTST)
    '        oCompany.UserName = strSAPUsername
    '        oCompany.Password = strSAPPassword
    '        intCodigoRetorno = oCompany.Connect
    '        If intCodigoRetorno <> 0 Then
    '            oCompany.GetLastError(intStatus, strStatus)
    '            ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
    '        Else
    '            Dim oDraft As SAPbobsCOM.Documents
    '            oDraft = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts)
    '            If oDraft.GetByKey(intDraftKey) Then
    '                intCodigoRetorno = oDraft.SaveDraftToDocument()
    '                If intCodigoRetorno <> 0 Then
    '                    oCompany.GetLastError(intStatus, strStatus)
    '                    ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
    '                Else
    '                    intStatus = fnc.ObtenerDocNumConDraftKey(intDraftKey, "OINV")
    '                End If
    '            Else
    '                intStatus = -1
    '                strStatus = "Borrador de venta no existe."
    '                ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
    '            End If
    '            System.Runtime.InteropServices.Marshal.ReleaseComObject(oDraft)
    '            oDraft = Nothing
    '        End If
    '        System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompany)
    '        oCompany = Nothing
    '        GC.Collect()
    '        GC.WaitForPendingFinalizers()
    '        GC.Collect()
    '    Catch ex As Exception
    '        ToLog.write(ex.Message, "E")
    '    End Try
    '    Return New Tuple(Of Integer, String)(intStatus, strStatus)
    'End Function
    Public Function RegistrarOrdenVentaEnFacturaVenta(ByVal intUsuario As Integer, ByVal intDocEntry As Integer, ByVal blnImprimir As Boolean) As Tuple(Of Integer, Integer, Integer, String)
        Dim intStatus As Integer = -100000
        Dim strStatus As String = "Error al pasar orden de venta a factura de venta, intente nuevamente."
        Dim blnActualizo As Boolean = False
        Dim intDocNum As Integer = -1
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-CL")
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
            Dim strRetorno As Tuple(Of String, String) = fnc.ObtenerUsuarioSap(intUsuario)
            Dim strSAPUsername As String = strRetorno.Item1.Trim
            Dim strSAPPassword As String = strRetorno.Item2.Trim
            Dim oCompany2 As New SAPbobsCOM.Company
            Dim intCodigoRetorno As Integer = 0
            oCompany2.Server = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.ServerPRD, My.Resources.rscServicioWeb.ServerTST)
            oCompany2.DbUserName = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.UserPRD, My.Resources.rscServicioWeb.UserTST)
            oCompany2.DbPassword = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.PasswordPRD, My.Resources.rscServicioWeb.PasswordTST)
            oCompany2.LicenseServer = My.Resources.rscServicioWeb.ServerLICENCIAS
            oCompany2.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016
            oCompany2.UseTrusted = False
            oCompany2.CompanyDB = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.DataPRD, My.Resources.rscServicioWeb.DataTST)
            oCompany2.UserName = strSAPUsername
            oCompany2.Password = strSAPPassword
            intCodigoRetorno = oCompany2.Connect
            If intCodigoRetorno <> 0 Then
                oCompany2.GetLastError(intStatus, strStatus)
                ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
            Else
                Dim oOrders As SAPbobsCOM.Documents
                Dim oInvoice As SAPbobsCOM.Documents
                oOrders = oCompany2.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders)
                oInvoice = oCompany2.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices)
                If oOrders.GetByKey(intDocEntry) Then
                    oInvoice.CardCode = oOrders.CardCode
                    oInvoice.UserFields.Fields.Item("U_TAI_FactElec").Value = "Y"
                    oInvoice.TaxDate = oOrders.TaxDate
                    oInvoice.DocDate = oOrders.DocDate
                    oInvoice.ReserveInvoice = SAPbobsCOM.BoYesNoEnum.tYES
                    If (oOrders.UserFields.Fields.Item("U_TAI_Emision").Value = "B") Then
                        oInvoice.Series = fnc.ObtenerCodigoSerie("13", "IB", intUsuario)
                        oInvoice.Indicator = "BE"
                        oInvoice.UserFields.Fields.Item("U_TAI_DocExitoso").Value = "Y"
                        oInvoice.UserFields.Fields.Item("U_TAI_cuartacopia").Value = oOrders.DocDate
                        oInvoice.DocumentSubType = SAPbobsCOM.BoDocumentSubType.bod_Bill
                    Else
                        oInvoice.Series = fnc.ObtenerCodigoSerie("13", "--", intUsuario)
                        oInvoice.Indicator = "FE"
                        oInvoice.DocumentSubType = SAPbobsCOM.BoDocumentSubType.bod_None
                    End If
                    oInvoice.DocumentsOwner = fnc.ObtenerCodigoEmpleadoPorCodigoUsuario(intUsuario)
                    If blnImprimir Then
                        oInvoice.UserFields.Fields.Item("U_TAI_Imprimir").Value = "1"
                    Else
                        oInvoice.UserFields.Fields.Item("U_TAI_Imprimir").Value = "0"
                    End If

                    oInvoice.UserFields.Fields.Item("U_TAI_BloqueaGD").Value = oOrders.UserFields.Fields.Item("U_TAI_BloqueaGD").Value

                    Dim U_TAI_MonedaCosto As String = ""
                    oOrders.Lines.SetCurrentLine(0)
                    oInvoice.Lines.BaseType = SAPbobsCOM.BoObjectTypes.oOrders
                    oInvoice.Lines.BaseEntry = intDocEntry
                    oInvoice.Lines.BaseLine = 0
                    oInvoice.Lines.UserFields.Fields.Item("U_TAI_MonedaPF").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_MonedaCosto").Value
                    U_TAI_MonedaCosto = oOrders.Lines.UserFields.Fields.Item("U_TAI_MonedaCosto").Value
                    oInvoice.Lines.LineTotal = oOrders.Lines.LineTotal
                    For i = 1 To oOrders.Lines.Count - 1
                        oInvoice.Lines.Add()
                        oOrders.Lines.SetCurrentLine(i)
                        oInvoice.Lines.BaseType = SAPbobsCOM.BoObjectTypes.oOrders
                        oInvoice.Lines.BaseEntry = intDocEntry
                        oInvoice.Lines.BaseLine = i
                        oInvoice.Lines.LineTotal = oOrders.Lines.LineTotal
                    Next
                    intCodigoRetorno = oInvoice.Add()
                    If intCodigoRetorno <> 0 Then
                        oCompany2.GetLastError(intStatus, strStatus)
                        ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
                    Else
                        intDocEntry = oCompany2.GetNewObjectKey()
                        intDocNum = fnc.ObtenerDocNumConDocEntry(intDocEntry, "OINV")
                        Me.strDocEntry = CStr(intDocNum)
                        Me.strDocNum = CStr(intDocEntry)
                        intStatus = 0
                        strStatus = ""
                    End If
                Else
                    intStatus = -1
                    strStatus = "orden de venta no existe."
                    ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
                End If
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oOrders)
                oOrders = Nothing
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oInvoice)
                oInvoice = Nothing
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompany2)
            oCompany2 = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        Finally
        End Try
        Return New Tuple(Of Integer, Integer, Integer, String)(intStatus, intDocEntry, intDocNum, strStatus)
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
            oCompany.Server = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.ServerPRD, My.Resources.rscServicioWeb.ServerTST)
            oCompany.DbUserName = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.UserPRD, My.Resources.rscServicioWeb.UserTST)
            oCompany.DbPassword = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.PasswordPRD, My.Resources.rscServicioWeb.PasswordTST)
            oCompany.LicenseServer = My.Resources.rscServicioWeb.ServerLICENCIAS
            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016
            oCompany.UseTrusted = False
            oCompany.CompanyDB = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.DataPRD, My.Resources.rscServicioWeb.DataTST)
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
    Public Function RegistrarOrdenVentaEnBorradorVenta(ByVal intUsuario As Integer, ByVal intDocEntry As Integer) As Tuple(Of Integer, Integer, Integer, String)
        Dim intStatus As Integer = -100000
        Dim strStatus As String = "Error al pasar orden de venta a borrador de venta, intente nuevamente."
        Dim blnActualizo As Boolean = False
        Dim intDocNum As Integer = -1
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-CL")
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
            Dim strRetorno As Tuple(Of String, String) = fnc.ObtenerUsuarioSap(intUsuario)
            Dim strSAPUsername As String = strRetorno.Item1.Trim
            Dim strSAPPassword As String = strRetorno.Item2.Trim
            Dim oCompany3 As New SAPbobsCOM.Company
            Dim intCodigoRetorno As Integer = 0
            oCompany3.Server = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.ServerPRD, My.Resources.rscServicioWeb.ServerTST)
            oCompany3.DbUserName = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.UserPRD, My.Resources.rscServicioWeb.UserTST)
            oCompany3.DbPassword = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.PasswordPRD, My.Resources.rscServicioWeb.PasswordTST)
            oCompany3.LicenseServer = My.Resources.rscServicioWeb.ServerLICENCIAS
            oCompany3.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016
            oCompany3.UseTrusted = False
            oCompany3.CompanyDB = If(My.Resources.rscServicioWeb.zModalidad = "P", My.Resources.rscServicioWeb.DataPRD, My.Resources.rscServicioWeb.DataTST)
            oCompany3.UserName = strSAPUsername
            oCompany3.Password = strSAPPassword
            intCodigoRetorno = oCompany3.Connect
            If intCodigoRetorno <> 0 Then
                oCompany3.GetLastError(intStatus, strStatus)
                ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
            Else
                Dim oOrders As SAPbobsCOM.Documents
                Dim oDraft As SAPbobsCOM.Documents
                oOrders = oCompany3.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders)
                oDraft = oCompany3.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts)
                If oOrders.GetByKey(intDocEntry) Then
                    oDraft.DocObjectCode = SAPbobsCOM.BoObjectTypes.oOrders
                    oDraft.CardCode = oOrders.CardCode
                    oDraft.Series = oOrders.Series
                    oDraft.TaxDate = oOrders.TaxDate
                    oDraft.DocDate = oOrders.DocDate
                    oDraft.DocDueDate = oOrders.DocDueDate
                    oDraft.UserFields.Fields.Item("U_TAI_Despacho").Value = oOrders.UserFields.Fields.Item("U_TAI_Despacho").Value
                    oDraft.ExtraDays = oOrders.ExtraDays
                    oDraft.SalesPersonCode = oOrders.SalesPersonCode
                    oDraft.DocumentsOwner = oOrders.DocumentsOwner
                    oDraft.PaymentGroupCode = oOrders.PaymentGroupCode
                    oDraft.Comments = oOrders.Comments
                    oDraft.UserFields.Fields.Item("U_Estado_Aut").Value = "P"
                    oDraft.UserFields.Fields.Item("U_VK_CertOrigen").Value = "WEB"
                    oDraft.PayToCode = oOrders.PayToCode
                    oDraft.ShipToCode = oOrders.ShipToCode
                    oDraft.NumAtCard = oOrders.NumAtCard
                    oDraft.UserFields.Fields.Item("U_TAI_NotaPedidoC").Value = oOrders.UserFields.Fields.Item("U_TAI_NotaPedidoC").Value
                    oDraft.UserFields.Fields.Item("U_TAI_GuiaDespacho").Value = oOrders.UserFields.Fields.Item("U_TAI_GuiaDespacho").Value
                    oDraft.UserFields.Fields.Item("U_TAI_TipoVenta").Value = oOrders.UserFields.Fields.Item("U_TAI_TipoVenta").Value
                    oDraft.UserFields.Fields.Item("U_TAI_MonedaProducto").Value = oOrders.UserFields.Fields.Item("U_TAI_MonedaProducto").Value
                    oDraft.UserFields.Fields.Item("U_TAI_GlosaEspecial").Value = oOrders.UserFields.Fields.Item("U_TAI_GlosaEspecial").Value
                    oDraft.UserFields.Fields.Item("U_TAI_CondicionPagOC").Value = oOrders.UserFields.Fields.Item("U_TAI_CondicionPagOC").Value
                    oDraft.UserFields.Fields.Item("U_TAI_ComentarioOC").Value = oOrders.UserFields.Fields.Item("U_TAI_ComentarioOC").Value
                    oDraft.UserFields.Fields.Item("U_TAI_Motivo").Value = oOrders.UserFields.Fields.Item("U_TAI_Motivo").Value
                    oDraft.UserFields.Fields.Item("U_TAI_Emision").Value = oOrders.UserFields.Fields.Item("U_TAI_Emision").Value
                    oDraft.UserFields.Fields.Item("U_TAI_BloqueaGD").Value = 0
                    For i = 0 To oOrders.Lines.Count - 1
                        oOrders.Lines.SetCurrentLine(i)
                        oDraft.Lines.ItemCode = oOrders.Lines.ItemCode
                        oDraft.Lines.Quantity = oOrders.Lines.Quantity
                        oDraft.Lines.WarehouseCode = oOrders.Lines.WarehouseCode
                        oDraft.Lines.UnitPrice = oOrders.Lines.UnitPrice
                        oDraft.Lines.Currency = oOrders.Lines.Currency
                        oDraft.Lines.LineTotal = oOrders.Lines.LineTotal
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_Interes").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_Interes").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_Descuento").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_Descuento").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_ImpteComis").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_ImpteComis").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_PrcDsctoMax").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_PrcDsctoMax").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_PUFinal").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_PUFinal").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_FechaEntrega").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_FechaEntrega").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_DescripcionAd").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_DescripcionAd").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_Flete").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_Flete").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_PorcVenta").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_PorcVenta").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_MonedaCosto").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_MonedaCosto").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_CostoComercial").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_CostoComercial").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_CostoReposicio").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_CostoReposicio").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_CardCode").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_CardCode").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_DiasCompra").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_DiasCompra").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_PreCompraPF").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_PreCompraPF").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_MonedaPF").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_MonedaPF").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_PrecioCompraO").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_PrecioCompraO").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_TasaInteres").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_TasaInteres").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_CondicionProducto").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_CondicionProducto").Value
                        oDraft.Lines.UserFields.Fields.Item("U_TAI_MotivoProducto").Value = oOrders.Lines.UserFields.Fields.Item("U_TAI_MotivoProducto").Value
                        oDraft.Lines.Add()
                    Next
                    intCodigoRetorno = oDraft.Add()
                    If intCodigoRetorno <> 0 Then
                        oCompany3.GetLastError(intStatus, strStatus)
                        ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
                    Else
                        intDocEntry = oCompany3.GetNewObjectKey()
                        intDocNum = fnc.ObtenerDocNumConDocEntry(intDocEntry, "ODRF")
                        intStatus = 0
                        strStatus = ""
                    End If
                Else
                    intStatus = -1
                    strStatus = "orden de venta no existe."
                    ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
                End If
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oOrders)
                oOrders = Nothing
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oDraft)
                oDraft = Nothing
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompany3)
            oCompany3 = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        Finally
        End Try
        Return New Tuple(Of Integer, Integer, Integer, String)(intStatus, intDocEntry, intDocNum, strStatus)
    End Function
End Class