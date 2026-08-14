Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Net.ServicePointManager
Public Class pagVisualizarOrdenVenta
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.intUsuario = CInt(Request.QueryString("intUsuario"))
        Me.intDocNum = CInt(Request.QueryString("intDocNum"))
        If Not (IsPostBack) Then
            chkImprimirFactura.Visible = False
            btnCrearFactura.Visible = False
            btnVerFactura.Visible = False
            strHtmlOrdenVenta = GenerarHTMLOrdenVenta()
            If (strHtmlOrdenVenta.Trim <> "") Then
                chkImprimirFactura.Visible = True
                btnCrearFactura.Visible = True
            End If
        End If
    End Sub
    Public Function GenerarHTMLOrdenVenta() As String
        Dim strHtml As String = ""
        Dim clsTransaccionListado As New clsTransaccionListado()
        For Each clsTransaccionResumen As clsTransaccionResumen In clsTransaccionListado.ObtenerTransaccionResumen(2, Me.intDocNum)
            If (clsTransaccionResumen.blnExiste) Then
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
                    btnCrearFactura.Visible = False
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

                If Me.strEmision = "B" Then
                    chkImprimirFactura.Text = "Imprimir Boleta"
                    btnCrearFactura.Text = "Crear Boleta"
                    btnVerFactura.Text = "Ver Boleta"
                Else
                    chkImprimirFactura.Text = "Imprimir Factura"
                    btnCrearFactura.Text = "Crear Factura"
                    btnVerFactura.Text = "Ver Factura"
                End If

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
    Protected Sub btnCrearFactura_Click(sender As Object, e As EventArgs) Handles btnCrearFactura.Click
        Dim blnSalir As Boolean = False
        Dim intParaCiclo As Integer = 0
        Dim intCicloFolio As Integer = CInt(My.Resources.rscSistemaVentasWeb.wWaitCicloFolio)
        Dim strTexto As String = "Se ha producido un error al intentar crear el documento, intente nuevamente."
        strHtmlOrdenVenta = GenerarHTMLOrdenVenta()
        Dim strTipoEmision As String = If(Me.strEmision = "B", "boleta", "factura")
        Try
            If Not (Me.blnFacturado) Then 'NO esta facturado
                Dim blnImprimir As Boolean = chkImprimirFactura.Checked
                Dim clsOrdenVenta As New WebServices.clsOrdenVenta()
                Dim strRetorno As Tuple(Of Integer, Integer, Integer, String) = clsOrdenVenta.RegistrarOrdenVentaEnFacturaVenta(Me.intUsuario, Me.intDocEntry, blnImprimir)
                Dim intStatus As Integer = strRetorno.Item1
                Dim intDocEntry As Integer = strRetorno.Item2
                Dim intDocNum As Integer = strRetorno.Item3
                Dim strStatus As String = strRetorno.Item4
                If (intStatus = 0) Then
                    If (Me.strTipoVenta.Trim = "ventaPuestoFundo" Or Me.strTipoVenta.Trim = "ventaCalzadaProveedor") Then
                        ObtenerProveedores()
                    End If
                    Do Until blnSalir
                        Threading.Thread.Sleep(CInt(My.Resources.rscSistemaVentasWeb.wWaitForFolio) * 1000)
                        Me.intFolio = fnc.ObtenerFolioFactura(intDocEntry)
                        intParaCiclo += 1
                        If Me.intFolio <> 0 Or intParaCiclo = intCicloFolio Then
                            blnSalir = True
                        End If
                    Loop
                    If (Me.intFolio > 0) Then 'SI se creo folio
                        strTexto = "Se ha creado la " + strTipoEmision + " N° " + CStr(intDocNum) + " (id: " + CStr(intDocEntry) + ")  correctamente con el folio N° " + CStr(Me.intFolio) + "."
                        btnVerFactura.Visible = True
                    Else                      'NO se creo folio
                        strTexto = "Se ha creado la " + strTipoEmision + " N° " + CStr(intDocNum) + " (id: " + CStr(intDocEntry) + ") sin folio."
                        btnVerFactura.Visible = False
                    End If
                    'ToLog.write("Tipo Docto.: " + strTipoEmision, "D")
                    Dim blnRespuesta As Boolean = False
                    If (Me.strEmision = "B") Then
                        blnRespuesta = ActualizarBoletaVenta(intDocEntry, intDocNum)
                        'ToLog.write("Actualizo Boleta: " + blnRespuesta.ToString, "D")
                    End If
                    btnCrearFactura.Visible = False
                Else
                    strTexto = "No se pudo crear " + strTipoEmision + ", " + strStatus.Trim.ToLower + "."
                    btnCrearFactura.Visible = False
                End If
            Else 'SI esta facturado
            End If
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        Finally
        End Try
        lblMensaje.Text = strTexto
    End Sub
    Protected Sub btnVerFactura_Click(sender As Object, e As EventArgs) Handles btnVerFactura.Click
        strHtmlOrdenVenta = GenerarHTMLOrdenVenta()

        ''Dim intResolucion As Integer = 0
        ''If (My.Resources.rscSistemaVentasWeb.zModalidad = "P") Then
        ''    intResolucion = If(Me.strEmision = "B", 80, 79)
        ''End If
        ''Dim intTipoDocto As Integer = If(Me.strEmision = "B", 39, 33)
        ''Dim strURLFacturacion As String = If(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.URLFacturacionPRD, My.Resources.rscSistemaVentasWeb.URLFacturacionTST)
        ''strURLFacturacion = strURLFacturacion.Replace("%r", CStr(intResolucion))
        ''strURLFacturacion = strURLFacturacion.Replace("%f", Me.intFolio)
        ''strURLFacturacion = strURLFacturacion.Replace("%t", CStr(intTipoDocto))
        ''Response.Redirect(strURLFacturacion, False)

        System.Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12
        Try
            Dim objRequest As New PDFE.VisualizacionRequest
            Dim objIntermediary As New PDFE.VisualizacionService
            Dim objResponse As New PDFE.VisualizacionResponse
            objRequest = New PDFE.VisualizacionRequest
            objIntermediary = New PDFE.VisualizacionService
            objResponse = New PDFE.VisualizacionResponse
            objRequest.apiKey = If(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.ApiKeyPRD, My.Resources.rscSistemaVentasWeb.ApyKeyTST)
            objRequest.numeroFolio = Me.intFolio
            objRequest.tipoDocumento = If(Me.strEmision = "B", 39, 33)
            objRequest.resolucionSii = If(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.URLResolucionPRD, My.Resources.rscSistemaVentasWeb.URLResolucionTST)
            objRequest.rutEmpresa = 96775400
            objRequest.numeroFolioSpecified = True
            objRequest.rutEmpresaSpecified = True
            objRequest.tipoDocumentoSpecified = True
            objRequest.resolucionSiiSpecified = True
            Try
                objResponse = objIntermediary.visualizacionPDF(objRequest)
                'If objResponse.codigoRespuesta = "1" Then
                '    ToLog.write("Resultado Solicitud: " & objResponse.respuesta, "E")
                'Else
                '    ToLog.write("Resultado Solicitud: " & objResponse.respuesta, "D")
                '    ToLog.write("URL Obtenido       : " & objResponse.urlDocumento, "D")
                'End If
                Response.Redirect(objResponse.urlDocumento, False)
            Catch ex As Exception
                ToLog.write("2.- " & ex.Message, "E")
            End Try
        Catch ex As Exception
            ToLog.write("1.- " & ex.Message, "E")
        End Try
    End Sub
    Protected Sub imgRetroceder_Click(sender As Object, e As ImageClickEventArgs) Handles imgRetroceder.Click
        Response.Redirect("pagMonitorDocumento.aspx")
    End Sub


    Public Sub ObtenerProveedores()
        Dim clsProveedorListado As New clsProveedorListado
        Dim strProveedor As String = ""
        Dim intDiasCompra As Integer = 0
        Dim blnRegistro = False
        For Each clsListado As clsProveedor In clsProveedorListado.ObtenerProveedorOrdenCompra(Me.intDocEntry, Me.intDocNum)
            strProveedor = clsListado.ProCodigo
            intDiasCompra = clsListado.DiaCompra
            RegistraDocumentoCompra(strProveedor, intDiasCompra)
        Next
    End Sub

    Public Sub RegistraDocumentoCompra(ByVal strProveedor As String, ByVal intDiaCompra As Integer)
        ToLog.write("RegistraDocumentoCompra: " + Me.intUsuario.ToString + ", " + Me.intDocEntry.ToString() + ", " + Me.intDocNum.ToString + ", " + strProveedor + ", " + intDiaCompra.ToString, "D")
        Dim clsOrdenCompra As New WebServices.clsOrdenCompra
        Dim strRetornoOC As Tuple(Of Integer, Integer, Integer, String) = clsOrdenCompra.RegistrarOrdenCompra(Me.intUsuario, Me.intDocEntry, Me.intDocNum, strProveedor, intDiaCompra)
        Dim intStatusOC As Integer = strRetornoOC.Item1
        Dim intDocEntryOC As Integer = strRetornoOC.Item2
        Dim intDocNumOC As Integer = strRetornoOC.Item3
        Dim strStatusOC As String = strRetornoOC.Item4
        If (intStatusOC = 0) Then
            ToLog.write("Orden compra creada: " + intDocEntryOC.ToString + ", " + intDocNumOC.ToString() + ", " + strProveedor + ", " + intDiaCompra.ToString, "D")

            '-----------------------------
            '--- INI : ENVIO DE CORREO ---
            '-----------------------------
            Dim blnCorreoOC As Boolean = False
            If (blnCorreoOC) Then
                Dim strHtmlOrdenCompra = GenerarHTMLOrdenCompra(intDocEntryOC, intDocNumOC)
                If (strHtmlOrdenCompra.Trim <> "") Then
                    Try
                        Dim mail As New ToMail()
                        Dim mensaje As MailMessage
                        Dim av1 As AlternateView
                        If My.Resources.rscSistemaVentasWeb.zModalidad = "P" Then
                            mensaje = New MailMessage(My.Resources.rscSistemaVentasWeb.wCorreoEmisor, My.Resources.rscSistemaVentasWeb.CorreoReceptorPRD)
                        Else
                            mensaje = New MailMessage(My.Resources.rscSistemaVentasWeb.wCorreoEmisor, My.Resources.rscSistemaVentasWeb.CorreoReceptorTST)
                        End If
                        mensaje.Subject = "ORDEN DE COMPRA N° " + intDocNumOC.ToString
                        av1 = AlternateView.CreateAlternateViewFromString(strHtmlOrdenCompra, Nothing, MediaTypeNames.Text.Html)
                        mensaje.AlternateViews.Add(av1)
                        mensaje.IsBodyHtml = True
                        mail.mensaje = mensaje
                        mail.EnviarCorreo()
                    Catch ex As Exception
                        ToLog.write(ex.Message, "E")
                    End Try
                End If
            End If
            '-----------------------------
            '--- TER : ENVIO DE CORREO ---
            '-----------------------------

        Else
            ToLog.write("Orden Compra no creada para el proveedor : " + strProveedor, "D")
        End If
    End Sub
    Public Function GenerarHTMLOrdenCompra(ByVal intDocEntryOC As Integer, ByVal intDocNumOC As Integer) As String
        Dim strHtml As String = ""
        Dim clsTransaccionListado As New clsTransaccionListado
        For Each clsTransaccionResumen As clsTransaccionResumen In clsTransaccionListado.ObtenerTransaccionResumen(3, intDocNumOC)
            If (clsTransaccionResumen.blnExiste) Then
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
                    strHtml += "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'>"
                    strHtml += "<html>"
                    strHtml += "    <head>"
                    strHtml += "        <title>SISTEMA DE VENTAS</title>"

                    strHtml += "        <style type='text/css'>"
                    strHtml += "            .trTituloSeccion"
                    strHtml += "            {"
                    strHtml += "                height:10px;"
                    strHtml += "                color:white;"
                    strHtml += "                font-family:Arial, Lucida Sans Unicode, Lucida Grande, Sans-Serif;"
                    strHtml += "                font-size:small;"
                    strHtml += "                font-style:normal;"
                    strHtml += "                text-align:left;"
                    strHtml += "                background-color:#003300;"
                    strHtml += "            }"
                    strHtml += "            .trTituloCabecera"
                    strHtml += "            {"
                    strHtml += "                height:10px;"
                    strHtml += "                color:white;"
                    strHtml += "                font-family:Arial, Lucida Sans Unicode, Lucida Grande, Sans-Serif;"
                    strHtml += "                font-size:small;"
                    strHtml += "                font-style:normal;"
                    strHtml += "                background-color:green;"
                    strHtml += "            }"
                    strHtml += "            .trTituloItem"
                    strHtml += "            {"
                    strHtml += "                height:10px;"
                    strHtml += "                color:black;"
                    strHtml += "                font-family:Arial, Lucida Sans Unicode, Lucida Grande, Sans-Serif;"
                    strHtml += "                font-size:small;"
                    strHtml += "                font-style:normal;"
                    strHtml += "            }"

                    strHtml += "            .tdRight"
                    strHtml += "            {"
                    strHtml += "                text-align:right;"
                    strHtml += "                vertical-align:central;"
                    strHtml += "                white-space: nowrap;"
                    strHtml += "            }"
                    strHtml += "            .tdCenter"
                    strHtml += "            {"
                    strHtml += "                text-align:center;"
                    strHtml += "                vertical-align:central;"
                    strHtml += "                white-space: nowrap;"
                    strHtml += "            }"
                    strHtml += "            .tdLeft"
                    strHtml += "            {"
                    strHtml += "                text-align:left;"
                    strHtml += "                vertical-align:central;"
                    strHtml += "                white-space: nowrap;"
                    strHtml += "            }"

                    strHtml += "            .tdRightCortado"
                    strHtml += "            {"
                    strHtml += "                text-align:right;"
                    strHtml += "                vertical-align:central;"
                    strHtml += "            }"
                    strHtml += "            .tdCenterCortado"
                    strHtml += "            {"
                    strHtml += "                text-align:center;"
                    strHtml += "                vertical-align:central;"
                    strHtml += "            }"
                    strHtml += "            .tdLeftCortado"
                    strHtml += "            {"
                    strHtml += "                text-align:left;"
                    strHtml += "                vertical-align:central;"
                    strHtml += "            }"
                    strHtml += "        </style>"

                    strHtml += "        <body>"

                    strHtml += "            <table width='100%' border='0px'>"
                    strHtml += "                <tr class='trTituloSeccion'><td colspan='6'>ORDEN DE COMPRA N° " + clsTransaccionResumen.intDocNum.ToString + " " + strTituloTipoVenta + "</td></tr>"
                    strHtml += "                <tr><td colspan='6'><br></td></tr>"

                    strHtml += "                <tr class='trTituloSeccion'><td colspan='6'>DATOS DEL PROVEEDOR</td></tr>"
                    strHtml += "                <tr class='trTituloItem'><td class='tdLeft'>NOMBRE  </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strNombreCliente + "  </td><td class='tdLeft'>RUT  </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strRutCliente + "   </td><td class='tdLeft'>CODIGO</td><td class='tdLeftCortado'>" + clsTransaccionResumen.strCodigoCliente + "</td></tr>"
                    strHtml += "                <tr class='trTituloItem'><td class='tdLeft'>TELEFONO</td><td class='tdLeftCortado'>" + clsTransaccionResumen.strTelefonoCliente + "</td><td class='tdLeft'>EMAIL</td><td class='tdLeftCortado'>" + clsTransaccionResumen.strCorreoCliente + "</td><td class='tdLeft'>GIRO  </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strGiroCliente + "  </td></tr>"

                    strHtml += "                <tr class='trTituloSeccion'><td colspan='6'>DATOS DEL NEGOCIO</td></tr>"
                    strHtml += "                <tr class='trTituloItem'><td class='tdLeft'>OPE.COMERCIAL </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strNombreOperador + "  </td><td class='tdLeft'>OFICINA       </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strSerie + "          </td><td class='tdLeft'>TIPO DESPACHO</td><td class='tdLeftCortado'>" + clsTransaccionResumen.strTipoDespacho + "</td></tr>"
                    strHtml += "                <tr class='trTituloItem'><td class='tdLeft'>CONDICION PAGO</td><td class='tdLeftCortado'>" + clsTransaccionResumen.strNombrePlazoVenta + "</td><td class='tdLeft'>FEC.ORDEN VTA.</td><td class='tdLeftCortado'>" + clsTransaccionResumen.strFechaOrdenVenta + "</td><td class='tdLeft'>MONEDA PAGO  </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strMonedaPago + "  </td></tr>"

                    strHtml += "                <tr class='trTituloSeccion'><td colspan='6'>COMENTARIO FACTURACION</td></tr>"
                    strHtml += "                <tr class='trTituloItem'><td colspan='6' class='tdLeftCortado'>" + IIf(clsTransaccionResumen.strComentario1.Trim <> "", clsTransaccionResumen.strComentario1.Trim.ToUpper, ".") + "</td></tr>"

                    strHtml += "                <tr class='trTituloSeccion'><td colspan='6'>COMENTARIO AUTORIZADOR</td></tr>"
                    strHtml += "                <tr class='trTituloItem'><td colspan='6' class='tdLeftCortado'>" + IIf(clsTransaccionResumen.strComentario2.Trim <> "", clsTransaccionResumen.strComentario2.Trim.ToUpper, ".") + "</td></tr>"

                    strHtml += "                <tr class='trTituloSeccion'><td colspan='6'>COMENTARIO ABASTECIMIENTO</td></tr>"
                    strHtml += "                <tr class='trTituloItem'><td colspan='6' class='tdLeftCortado'>" + IIf(clsTransaccionResumen.strComentario3.Trim <> "", clsTransaccionResumen.strComentario3.Trim.ToUpper, ".") + "</td></tr>"
                    Try 'DETALLE
                        strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>DATOS DEL PEDIDO</td></tr>"
                        strHtml += "            <tr>"
                        strHtml += "                <td colspan='6'>"
                        strHtml += "                    <table id='tblPedido' width='100%'>"
                        strHtml += "                        <tr class='trTituloCabecera'>"
                        strHtml += "                            <td class='tdCenter'>Codigo</td>"
                        strHtml += "                            <td class='tdCenter'>Descripcion</td>"
                        strHtml += "                            <td class='tdCenter'>Cantidad</td>"
                        strHtml += "                            <td class='tdCenter'>Dias Compra</td>"
                        strHtml += "                            <td class='tdCenter'>Moneda</td>"
                        strHtml += "                            <td class='tdCenter'>Precio Unitario</td>"

                        strHtml += "                            <td class='tdCenter'>Condicion</td>"
                        strHtml += "                            <td class='tdCenter'>Motivo</td>"

                        'strHtml += "                           <td class='tdCenter'>Proveedor</td>"
                        strHtml += "                        </tr>"
                        For Each clsTransaccionDetalle As clsTransaccionDetalle In clsTransaccionListado.ObtenerTransaccionDetalle(3, intDocEntryOC, "", 0)
                            strHtml += "                    <tr class='trTituloItem'>"
                            strHtml += "                        <td class='tdLeft'>" + clsTransaccionDetalle.strCodigoProducto + "</td>"
                            strHtml += "                        <td class='tdLeft'>" + clsTransaccionDetalle.strNombreProducto + "</td>"
                            strHtml += "                        <td class='tdRight'>" + clsTransaccionDetalle.intCantidadProducto.ToString + "</td>"
                            strHtml += "                        <td class='tdRight'>" + clsTransaccionDetalle.intDiasCompra.ToString + "</td>"
                            strHtml += "                        <td class='tdCenter'>" + clsTransaccionDetalle.strMonedaProductoCompra + "</td>"
                            strHtml += "                        <td class='tdRight'>" + (FormatNumber(clsTransaccionDetalle.dblPrecioUnitarioProductoCompra, 2)).ToString + "</td>"

                            strHtml += "                        <td class='tdLeft'>" + clsTransaccionDetalle.strCondicionProducto + "</td>"
                            strHtml += "                        <td class='tdLeft'>" + clsTransaccionDetalle.strMotivoProducto + "</td>"

                            'strHtml += "                       <td class='tdRight'>" + clsTransaccionDetalle.strCodigoProveedorCompra + "</td>"
                            strHtml += "                    </tr>"
                        Next
                        strHtml += "                    </table>"
                        strHtml += "                </td>"
                        strHtml += "            </tr>"
                        strHtml += "        </table>"
                        strHtml += "        <p><strong><em>Mensaje: Si en 24 Horas no se recibe alguna objeci&oacute;n de las condiciones comerciales establecidos en este correo electr&oacute;nico, se asume que esta aprobada la orden de compra.</em></strong></p>"
                        strHtml += "        <p>&nbsp;</p>"
                        strHtml += "    </body>"
                        strHtml += "</head>"
                        strHtml += "</html>"
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
    Public Function ActualizarBoletaVenta(ByVal intDocEntry As Integer, ByVal intDocNum As Integer) As Boolean
        ToLog.write("intDocEntry: " + intDocEntry.ToString() + ",  intDocNum: " + intDocNum.ToString(), "D")
        Dim blnRespuesta As Boolean = False
        Dim intRespuesta As Integer = 0
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            cmd = New SqlCommand("dbo.tai_vw_sp2_update_boleta_venta", cnx)
            cmd.CommandTimeout = 600
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocEntry", SqlDbType.Int).Value = intDocEntry
            cmd.Parameters.Add("@DocNum", SqlDbType.Int).Value = intDocNum
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