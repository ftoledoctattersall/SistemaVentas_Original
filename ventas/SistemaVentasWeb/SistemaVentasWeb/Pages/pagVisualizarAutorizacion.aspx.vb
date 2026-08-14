Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Net.Mail
Imports System.Net.Mime
Public Class pagVisualizarAutorizacion
    Inherits System.Web.UI.Page
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public strHtmlOrdenVenta As String = ""
    Public Property intUsuario As Integer
    Public Property intDocNum As Integer
    Public Property intDocEntry As Integer
    Public Property intFolio As Integer
    Public Property blnFacturado As Boolean
    Public Property strTipoVenta As String

    Public Property strEmision As String

    Public Property strHtmlAutorizacion As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.intUsuario = CInt(Request.QueryString("intUsuario"))
        Me.intDocNum = CInt(Request.QueryString("intDocNum"))
        If Not (IsPostBack) Then
            strHtmlOrdenVenta = GenerarHTMLOrdenVenta()
            lblMensaje.Text = "Linea de credito disponible es insuficiente para cubrir total del documento, debe crear borrador para autorizacion por credito."
        End If
    End Sub
    Public Function GenerarHTMLOrdenVenta() As String
        Dim strHtml As String = ""
        Dim clsTransaccionListado As New clsTransaccionListado()
        For Each clsTransaccionResumen As clsTransaccionResumen In clsTransaccionListado.ObtenerTransaccionResumen(2, Me.intDocNum)
            If (clsTransaccionResumen.blnExiste) Then
                hdnDocEntryOV.Value = clsTransaccionResumen.intDocEntry
                hdnDocNumOV.Value = clsTransaccionResumen.intDocNum
                Me.intDocEntry = clsTransaccionResumen.intDocEntry
                Me.intFolio = clsTransaccionResumen.intFolio
                Me.blnFacturado = clsTransaccionResumen.blnFacturado
                Me.strTipoVenta = clsTransaccionResumen.strTipoVenta
                Me.strEmision = clsTransaccionResumen.strEmision
                Dim clsPeriodoContableListado As New clsPeriodoContableListado()
                Dim strPeriodoContable As String = clsPeriodoContableListado.ObtenerPeriodoContable()
                Dim strFecha As String = ""
                strFecha = clsTransaccionResumen.strFechaOrdenVenta.Substring(6, 4) + clsTransaccionResumen.strFechaOrdenVenta.Substring(3, 2)
                If ((Not strPeriodoContable.Contains(strFecha)) And (Not clsTransaccionResumen.blnFacturado)) Then
                    lblMensaje.Text = "Periodo contable cerrado, realice nuevamente el pedido de cliente por el periodo que corresponda."
                End If
                Dim intIndice As Integer = 0
                Dim strTituloTipoVenta = "SIN TIPO DE VENTA"
                Select Case clsTransaccionResumen.strTipoVenta.Trim
                    Case "ventaBodega"
                        strTituloTipoVenta = "VENTA BODEGA PROPIA"
                    Case "ventaConsignada"
                        strTituloTipoVenta = "VENTA CONSIGNADA"
                    Case "ventaPuestoFundo"
                        strTituloTipoVenta = "VENTA PUESTO FUNDO"
                    Case "ventaCalzadaProveedor"
                        strTituloTipoVenta = "VENTA CALZADA PROVEEDOR"
                    Case "ventaCostoEspecial"
                        strTituloTipoVenta = "VENTA COSTO ESPECIAL"
                    Case "ventaLiquidacion"
                        strTituloTipoVenta = "VENTA LIQUIDACION"
                End Select
                Try 'RESUMEN
                    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-CL")
                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ","
                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = "."
                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ","
                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = "."
                    strHtml += "        <table style='width:100%;' border='0px'>"
                    strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>ORDEN DE VENTA N° " + clsTransaccionResumen.intDocNum.ToString + " " + strTituloTipoVenta + "   (" + clsTransaccionResumen.strEmision + ")</td></tr>"
                    strHtml += "            <tr><td colspan='6'><br></td></tr>"

                    strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>DATOS DEL CLIENTE</td></tr>"
                    strHtml += "            <tr class='trTituloItem'><td class='tdLeft'>NOMBRE  </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strNombreCliente + "  </td><td class='tdLeft'>RUT  </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strRutCliente + " (" + clsTransaccionResumen.strCodigoCliente + ")</td><td class='tdLeft'>CATEGORIA</td><td class='tdLeftCortado'> " + clsTransaccionResumen.strCategoriaCliente + "</td></tr>"
                    strHtml += "            <tr class='trTituloItem'><td class='tdLeft'>TELEFONO</td><td class='tdLeftCortado'>" + clsTransaccionResumen.strTelefonoCliente + "</td><td class='tdLeft'>EMAIL</td><td class='tdLeftCortado'>" + clsTransaccionResumen.strCorreoCliente + "                                              </td><td class='tdLeft'>GIRO     </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strGiroCliente + "      </td></tr>"

                    strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>DATOS DEL NEGOCIO</td></tr>"
                    strHtml += "            <tr class='trTituloItem'><td class='tdLeft'>OPE.COMERCIAL      </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strNombreOperador + "     </td><td class='tdLeft'>OFICINA       </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strSerie + "           </td><td class='tdLeft'>TIPO DESPACHO</td><td class='tdLeftCortado'>" + clsTransaccionResumen.strTipoDespacho + "</td></tr>"
                    strHtml += "            <tr class='trTituloItem'><td class='tdLeft'>CONDICION PAGO     </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strNombrePlazoVenta + "   </td><td class='tdLeft'>FEC.ORDEN VTA.</td><td class='tdLeftCortado'>" + clsTransaccionResumen.strFechaOrdenVenta + " </td><td class='tdLeft'>MONEDA PAGO  </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strMonedaPago + "  </td></tr>"
                    strHtml += "            <tr class='trTituloItem'><td class='tdLeft'>DIAS PLAZO VTA.REAL</td><td class='tdLeftCortado'>" + clsTransaccionResumen.intDiasExtras.ToString + "</td><td class='tdLeft'>FEC.VENCTO.   </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strFechaVencimiento + "</td><td class='tdLeft'>.</td><td class='tdLeftCortado'>" + "." + "</td></tr>"

                    strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>COMENTARIO FACTURACION</td></tr>"
                    strHtml += "            <tr class='trTituloItem'><td colspan='6' class='tdLeftCortado'>" + IIf(clsTransaccionResumen.strComentario1.Trim <> "", clsTransaccionResumen.strComentario1.Trim.ToUpper, ".") + "</td></tr>"
                    strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>COMENTARIO AUTORIZADOR</td></tr>"
                    strHtml += "            <tr class='trTituloItem'><td colspan='6' class='tdLeftCortado'>" + IIf(clsTransaccionResumen.strComentario2.Trim <> "", clsTransaccionResumen.strComentario2.Trim.ToUpper, ".") + "</td></tr>"
                    If (clsTransaccionResumen.strTipoVenta.Trim = "ventaPuestoFundo" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCalzadaProveedor") Then
                        strHtml += "        <tr class='trTituloSeccion'><td colspan='6'>COMENTARIO ABASTECIMIENTO</td></tr>"
                        strHtml += "        <tr class='trTituloItem'><td colspan='6' class='tdLeftCortado'>" + IIf(clsTransaccionResumen.strComentario3.Trim <> "", clsTransaccionResumen.strComentario3.Trim.ToUpper, ".") + "</td></tr>"
                    End If
                    Try 'DETALLE
                        strHtml += "        <tr>"
                        strHtml += "            <td colspan='6'>"
                        strHtml += "                <table id='tblPedido' width='100%' border='1px'>"

                        strHtml += "                    <tr class='trTituloSeccion'>"
                        strHtml += "                        <td class='tdLeftCortado' colspan='12'>DATOS DE LA VENTA</td>"
                        If (clsTransaccionResumen.strTipoVenta.Trim = "ventaPuestoFundo" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCalzadaProveedor" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCostoEspecial" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaLiquidacion") Then
                            strHtml += "                    <td class='tdLeftCortadoCompra' colspan='8'>DATOS DE LA COMPRA</td>"
                        End If
                        strHtml += "                        <td class='tdLeftCortado' colspan='1'></td>"
                        strHtml += "                    </tr>"

                        strHtml += "                    <tr class='trTituloCabecera'>"
                        strHtml += "                        <td class='tdCenterCortado'>Codigo</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Descripcion</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Cantidad</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Moneda</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Precio Unitario</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Total Unitario CLP</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Descto. %</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Margen %</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Interes CLP</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Flete CLP</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Fecha Entrega</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Precio Final CLP</td>"
                        If (clsTransaccionResumen.strTipoVenta.Trim = "ventaPuestoFundo" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCalzadaProveedor" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCostoEspecial" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaLiquidacion") Then
                            strHtml += "                    <td class='tdCenterCortadoCompra'>Dias Compra</td>"
                            strHtml += "                    <td class='tdCenterCortadoCompra'>Moneda</td>"
                            strHtml += "                    <td class='tdCenterCortadoCompra'>Precio Unitario</td>"
                            strHtml += "                    <td class='tdCenterCortadoCompra'>Total Unitario CLP</td>"
                            strHtml += "                    <td class='tdCenterCortadoCompra'>Proveedor</td>"
                            strHtml += "                    <td class='tdCenterCortadoCompra'>Tasa Interes %</td>"

                            strHtml += "                    <td class='tdCenterCortadoCompra'>Condicion</td>"
                            strHtml += "                    <td class='tdCenterCortadoCompra'>Motivo</td>"
                        End If
                        strHtml += "                        <td class='tdCenterCortado'>Costo Reposicion</td>"
                        strHtml += "                    </tr>"
                        Dim intTotalNeto As Integer = 0
                        Dim intTotalIva As Integer = 0
                        Dim intTotalBruto As Integer = 0
                        Dim dblTipoCambio As Double = 0.0
                        Dim intCantidadProductos As Integer = 0
                        Dim dblMargenComercialProducto As Double = 0.0
                        Dim intSumaTotalUnitarioProducto As Integer = 0
                        Dim intSumaTotalCostoComercial As Double = 0.0
                        Dim dblTotalMargenComercial As Double = 0.0
                        For Each clsTransaccionDetalle As clsTransaccionDetalle In clsTransaccionListado.ObtenerTransaccionDetalle(2, Me.intDocEntry, "", 0)
                            If (clsTransaccionDetalle.strCodigoProducto <> "DESCUENTO" And clsTransaccionDetalle.strCodigoProducto <> "Z8200003000000") Then

                                intTotalNeto += CInt(clsTransaccionDetalle.intTotalUnitarioProducto)
                                intTotalIva = CInt(Math.Round(intTotalNeto * 0.19))
                                intTotalBruto = intTotalNeto + intTotalIva
                                intCantidadProductos += 1
                                dblTotalMargenComercial = 0.0
                                dblTipoCambio = 1.0
                                If (clsTransaccionDetalle.strMonedaProducto = "USD") Then
                                    dblTipoCambio = CDbl(clsTransaccionDetalle.dblMonedaProducto)
                                ElseIf (clsTransaccionDetalle.strMonedaProducto = "EUR") Then
                                    dblTipoCambio = CDbl(clsTransaccionDetalle.dblMonedaProducto)
                                End If
                                If (clsTransaccionResumen.strTipoVenta.Trim = "ventaPuestoFundo" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCalzadaProveedor" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCostoEspecial" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaLiquidacion") Then
                                    dblMargenComercialProducto = Format(100 - ((clsTransaccionDetalle.dblPrecioUnitarioProductoCompra * 100) / clsTransaccionDetalle.dblPrecioUnitarioProducto), "0.00")
                                    intSumaTotalUnitarioProducto += (CDbl(clsTransaccionDetalle.dblPrecioUnitarioProducto) * CInt(clsTransaccionDetalle.intCantidadProducto)) * dblTipoCambio
                                    intSumaTotalCostoComercial += (CDbl(clsTransaccionDetalle.dblPrecioUnitarioProductoCompra) * CInt(clsTransaccionDetalle.intCantidadProducto)) * dblTipoCambio
                                    dblTotalMargenComercial = (((intSumaTotalUnitarioProducto - intSumaTotalCostoComercial) / intSumaTotalUnitarioProducto) * 100.0)
                                Else
                                    dblMargenComercialProducto = Format(100 - ((clsTransaccionDetalle.dblCostoComercialProducto * 100) / clsTransaccionDetalle.dblPrecioUnitarioProducto), "0.00")
                                    intSumaTotalUnitarioProducto += (CDbl(clsTransaccionDetalle.dblPrecioUnitarioProducto) * CInt(clsTransaccionDetalle.intCantidadProducto)) * dblTipoCambio
                                    intSumaTotalCostoComercial += (CDbl(clsTransaccionDetalle.dblCostoComercialProducto) * CInt(clsTransaccionDetalle.intCantidadProducto)) * dblTipoCambio
                                    dblTotalMargenComercial = (((intSumaTotalUnitarioProducto - intSumaTotalCostoComercial) / intSumaTotalUnitarioProducto) * 100.0)
                                End If
                                strHtml += "                <tr class='trTituloItem'>"
                                strHtml += "                    <td class='tdLeftCortado'>" + clsTransaccionDetalle.strCodigoProducto + "</td>"
                                strHtml += "                    <td class='tdLeftCortado'>" + clsTransaccionDetalle.strNombreProducto + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + clsTransaccionDetalle.intCantidadProducto.ToString + "</td>"
                                strHtml += "                    <td class='tdCenterCortado'>" + clsTransaccionDetalle.strMonedaProducto + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.dblPrecioUnitarioProducto, 2)).ToString + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.intTotalUnitarioProducto, 0)).ToString + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.dblDescuentoProducto, 2)).ToString + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(dblMargenComercialProducto, 2)).ToString + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.intInteresProducto, 0)).ToString + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.intFleteProducto, 0)).ToString + "</td>"
                                strHtml += "                    <td class='tdCenterCortado'>" + clsTransaccionDetalle.strFechaEntregaProducto + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.intTotalUnitarioProducto, 0)).ToString + "</td>"
                                If (clsTransaccionResumen.strTipoVenta.Trim = "ventaPuestoFundo" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCalzadaProveedor" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCostoEspecial" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaLiquidacion") Then
                                    strHtml += "                <td class='tdRightCortado'>" + clsTransaccionDetalle.intDiasCompra.ToString + "</td>"
                                    strHtml += "                <td class='tdCenterCortado'>" + clsTransaccionDetalle.strMonedaProductoCompra + "</td>"
                                    strHtml += "                <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.dblPrecioUnitarioProductoCompra, 2)).ToString + "</td>"
                                    strHtml += "                <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.dblTotalUnitarioProductoCompra, 0)).ToString + "</td>"
                                    strHtml += "                <td class='tdCenterCortado'>" + fnc.ObtenerProveedorNombre(clsTransaccionDetalle.strCodigoProveedorCompra) + "</td>"
                                    strHtml += "                <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.dblTasaInteresCompra, 2)).ToString + "</td>"

                                    strHtml += "                <td class='tdCenterCortado'>" + clsTransaccionDetalle.strCondicionProducto + "</td>"
                                    strHtml += "                <td class='tdCenterCortado'>" + clsTransaccionDetalle.strMotivoProducto + "</td>"
                                End If
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.dblCostoReposicionProducto, 2)).ToString + "</td>"
                                strHtml += "                </tr>"

                            End If
                        Next
                        If (clsTransaccionResumen.strTipoVenta.Trim = "ventaPuestoFundo" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCalzadaProveedor" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCostoEspecial" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaLiquidacion") Then
                            strHtml += "                <tr><td colspan='21'><br></td></tr>"
                            strHtml += "                <tr class='trTituloItem'><td class='tdLeft'>                 </td><td class='tdLeft' colspan='9'>                                                             </td><td class='tdLeft'>Total Neto</td><td class='tdRight'> " + (FormatNumber(intTotalNeto, 0)).ToString + " </td><td colspan='9'></td></tr>"
                            strHtml += "                <tr class='trTituloItem'><td class='tdLeft'>Cantidad Items:  </td><td class='tdLeft' colspan='9'>" + intCantidadProductos.ToString + "                        </td><td class='tdLeft'>Total IVA</td><td class='tdRight'>  " + (FormatNumber(intTotalIva, 0)).ToString + "  </td><td colspan='9'></td></tr>"
                            strHtml += "                <tr class='trTituloItem'><td class='tdLeft'>Margen Comercial:</td><td class='tdLeft' colspan='9'>" + (FormatNumber(dblTotalMargenComercial, 2)).ToString + " %</td><td class='tdLeft'>Total Bruto</td><td class='tdRight'>" + (FormatNumber(intTotalBruto, 0)).ToString + "</td><td colspan='9'></td></tr>"
                        Else
                            strHtml += "                <tr><td colspan='13'><br></td></tr>"
                            strHtml += "                <tr class='trTituloItem'><td class='tdLeft'>                 </td><td class='tdLeft' colspan='9'>                                                             </td><td class='tdLeft'>Total Neto</td><td class='tdRight'> " + (FormatNumber(intTotalNeto, 0)).ToString + " </td><td colspan='1'></td></tr>"
                            strHtml += "                <tr class='trTituloItem'><td class='tdLeft'>Cantidad Items:  </td><td class='tdLeft' colspan='9'>" + intCantidadProductos.ToString + "                        </td><td class='tdLeft'>Total IVA</td><td class='tdRight'>  " + (FormatNumber(intTotalIva, 0)).ToString + "  </td><td colspan='1'></td></tr>"
                            strHtml += "                <tr class='trTituloItem'><td class='tdLeft'>Margen Comercial:</td><td class='tdLeft' colspan='9'>" + (FormatNumber(dblTotalMargenComercial, 2)).ToString + " %</td><td class='tdLeft'>Total Bruto</td><td class='tdRight'>" + (FormatNumber(intTotalBruto, 0)).ToString + "</td><td colspan='1'></td></tr>"
                        End If
                        strHtml += "                </table>"
                        strHtml += "            </td>"
                        strHtml += "        </tr>"
                        strHtml += "    </table>"
                    Catch ex As Exception
                        strHtml = ""
                        ToLog.write("lectura de detalle: " + ex.Message, "E")
                    End Try
                Catch ex As Exception
                    strHtml = ""
                    ToLog.write("lectura de resumen: " + ex.Message, "E")
                End Try
            End If
        Next
        Return strHtml
    End Function
    Protected Sub imgRetroceder_Click(sender As Object, e As ImageClickEventArgs) Handles imgRetroceder.Click
        Response.Redirect("pagMonitorDocumento.aspx")
    End Sub
End Class