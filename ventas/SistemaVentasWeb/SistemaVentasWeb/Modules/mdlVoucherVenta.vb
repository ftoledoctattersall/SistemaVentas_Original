Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.Image
<Serializable()>
Module mdlVoucherVenta
    Public Sub GenerarPDFVenta(ByVal intTipo As Integer, ByVal intCodigo As Integer, ByVal strRutaImagen As String, ByRef objDocumento As Document)
        Dim dblMargenComercialProducto As Double = 0.0
        Dim intTotalLinea As Integer = 0
        Dim intTotalNeto As Integer = 0
        Dim intDocEntry As Integer = 0
        Dim textoTitulo As String = ""
        Dim imgLogo As iTextSharp.text.Image
        Dim parrafo As New Paragraph
        Dim tablaLogo As New PdfPTable(1)
        Dim tablaCliente As New PdfPTable(4)
        Dim tablaNegocio As New PdfPTable(4)
        Dim tablaComentario As New PdfPTable(1)
        Dim tablaPedidos As PdfPTable
        Dim celdaTitulo As New PdfPCell()
        Dim celdaCuerpo As New PdfPCell()
        Try
            Dim clsTransaccionListado As New WebServices.clsTransaccionListado()
            For Each clsTransaccionResumen As WebServices.clsTransaccionResumen In clsTransaccionListado.ObtenerTransaccionResumen(intTipo, intCodigo)
                intDocEntry = clsTransaccionResumen.intDocEntry
                If (intTipo = 1) Then
                    textoTitulo = "BORRADOR DE VENTA Nº " + intCodigo.ToString
                Else
                    textoTitulo = "ORDEN DE VENTA Nº " + intCodigo.ToString
                End If
                imgLogo = Image.GetInstance(strRutaImagen + "/logo.png")
                imgLogo.ScalePercent(20.0F)
                imgLogo.ScalePercent(100.0F)
                imgLogo.Alignment = Element.ALIGN_LEFT
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Border = 0
                celdaCuerpo.HorizontalAlignment = Element.ALIGN_CENTER
                celdaCuerpo.PaddingTop = 10
                celdaCuerpo.AddElement(imgLogo)
                tablaLogo.SpacingBefore = 10
                tablaLogo.SpacingAfter = 10
                tablaLogo.AddCell(celdaCuerpo)
                objDocumento.Add(tablaLogo)
                parrafo.Alignment = Element.ALIGN_CENTER
                parrafo.Font = FontFactory.GetFont("Arial", 14, ALIGN_CENTER)
                parrafo.Font.SetStyle(Font.BOLD)
                parrafo.Font.SetStyle(Font.UNDERLINE)
                parrafo.Add(textoTitulo)
                objDocumento.Add(parrafo)
                parrafo.Clear()
                objDocumento.Add(New Paragraph(" "))
                celdaTitulo.Phrase = New Phrase("DATOS DEL CLIENTE")
                celdaTitulo.Colspan = 4
                celdaTitulo.HorizontalAlignment = 0 '0=Left, 1=Centre, 2=Right
                celdaTitulo.Border = 0
                tablaCliente.AddCell(celdaTitulo)
                tablaCliente.HeaderRows = 1
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase("Nombre")
                celdaCuerpo.BackgroundColor = GrayColor.LIGHT_GRAY
                tablaCliente.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase(clsTransaccionResumen.strNombreCliente)
                celdaCuerpo.Colspan = 3
                tablaCliente.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase("RUT")
                celdaCuerpo.BackgroundColor = GrayColor.LIGHT_GRAY
                tablaCliente.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase(clsTransaccionResumen.strRutCliente)
                tablaCliente.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase("Código")
                celdaCuerpo.BackgroundColor = GrayColor.LIGHT_GRAY
                tablaCliente.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase(clsTransaccionResumen.strCodigoCliente)
                tablaCliente.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase("Teléfono")
                celdaCuerpo.BackgroundColor = GrayColor.LIGHT_GRAY
                tablaCliente.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase(clsTransaccionResumen.strTelefonoCliente)
                tablaCliente.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase("Email")
                celdaCuerpo.BackgroundColor = GrayColor.LIGHT_GRAY
                tablaCliente.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase(clsTransaccionResumen.strCorreoCliente)
                tablaCliente.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase("Giro")
                celdaCuerpo.BackgroundColor = GrayColor.LIGHT_GRAY
                tablaCliente.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase(clsTransaccionResumen.strGiroCliente)
                tablaCliente.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase("Moneda")
                celdaCuerpo.BackgroundColor = GrayColor.LIGHT_GRAY
                tablaCliente.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase(clsTransaccionResumen.strMonedaPago)
                tablaCliente.AddCell(celdaCuerpo)
                objDocumento.Add(tablaCliente)
                objDocumento.Add(New Paragraph(" "))
                celdaTitulo = New PdfPCell()
                celdaTitulo.Phrase = New Phrase("DATOS DEL NEGOCIO")
                celdaTitulo.Colspan = 4
                celdaTitulo.HorizontalAlignment = 0
                celdaTitulo.Border = 0
                tablaNegocio.AddCell(celdaTitulo)
                tablaNegocio.HeaderRows = 1
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase("Ope.Comercial")
                celdaCuerpo.BackgroundColor = GrayColor.LIGHT_GRAY
                tablaNegocio.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase(clsTransaccionResumen.strNombreOperador)
                celdaCuerpo.Colspan = 3
                tablaNegocio.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase("Oficina")
                celdaCuerpo.BackgroundColor = GrayColor.LIGHT_GRAY
                tablaNegocio.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase(clsTransaccionResumen.strSerie)
                tablaNegocio.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase("Tipo Despacho")
                celdaCuerpo.BackgroundColor = GrayColor.LIGHT_GRAY
                tablaNegocio.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase(clsTransaccionResumen.strTipoDespacho)
                tablaNegocio.AddCell(celdaCuerpo)
                objDocumento.Add(tablaNegocio)
                objDocumento.Add(New Paragraph(" "))
                Dim fuenteComentariosDetalle As New Font()
                fuenteComentariosDetalle = FontFactory.GetFont("Arial", 8, ALIGN_JUSTIFIED)
                celdaTitulo = New PdfPCell()
                celdaTitulo.Phrase = New Phrase("COMENTARIO FACTURACION")
                celdaTitulo.HorizontalAlignment = 0
                celdaTitulo.Border = 0
                tablaComentario.AddCell(celdaTitulo)
                tablaComentario.HeaderRows = 1
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase(" " + clsTransaccionResumen.strComentario1, fuenteComentariosDetalle)
                tablaComentario.AddCell(celdaCuerpo)
                objDocumento.Add(tablaComentario)
                objDocumento.Add(New Paragraph(" "))
                Dim fuenteTituloPedido As New Font()
                fuenteTituloPedido = FontFactory.GetFont("Arial", 6, ALIGN_CENTER)
                fuenteTituloPedido.SetStyle(Font.BOLD)
                Dim fuenteDetallePedido As New Font()
                fuenteDetallePedido = FontFactory.GetFont("Arial", 6, ALIGN_CENTER)
                Dim fuenteDetallePedidoNumero As New Font()
                fuenteDetallePedidoNumero = FontFactory.GetFont("Arial", 6, ALIGN_RIGHT)
                Dim fuenteTituloTotal As New Font()
                fuenteTituloTotal = FontFactory.GetFont("Arial", 8, ALIGN_RIGHT)
                tablaPedidos = New PdfPTable(12)
                celdaTitulo.Phrase = New Phrase("DATOS DEL PEDIDO")
                celdaTitulo.Colspan = 12
                celdaTitulo.HorizontalAlignment = 0
                celdaTitulo.Border = 0
                tablaPedidos.AddCell(celdaTitulo)
                tablaPedidos.HeaderRows = 1
                celdaTitulo = New PdfPCell()
                celdaTitulo.HorizontalAlignment = 1
                celdaTitulo.BackgroundColor = GrayColor.LIGHT_GRAY
                celdaTitulo.Phrase = New Phrase("Codigo", fuenteTituloPedido)
                tablaPedidos.AddCell(celdaTitulo)
                celdaTitulo.Phrase = New Phrase("Producto", fuenteTituloPedido)
                tablaPedidos.AddCell(celdaTitulo)
                celdaTitulo.Phrase = New Phrase("Cantidad", fuenteTituloPedido)
                tablaPedidos.AddCell(celdaTitulo)
                celdaTitulo.Phrase = New Phrase("Moneda", fuenteTituloPedido)
                tablaPedidos.AddCell(celdaTitulo)
                celdaTitulo.Phrase = New Phrase("Precio Unitario", fuenteTituloPedido)
                tablaPedidos.AddCell(celdaTitulo)
                celdaTitulo.Phrase = New Phrase("Total Unitario CLP", fuenteTituloPedido)
                tablaPedidos.AddCell(celdaTitulo)
                celdaTitulo.Phrase = New Phrase("Descto. %", fuenteTituloPedido)
                tablaPedidos.AddCell(celdaTitulo)
                celdaTitulo.Phrase = New Phrase("Margen %", fuenteTituloPedido)
                tablaPedidos.AddCell(celdaTitulo)
                celdaTitulo.Phrase = New Phrase("Interes CLP", fuenteTituloPedido)
                tablaPedidos.AddCell(celdaTitulo)
                celdaTitulo.Phrase = New Phrase("Flete CLP", fuenteTituloPedido)
                tablaPedidos.AddCell(celdaTitulo)
                celdaTitulo.Phrase = New Phrase("Fecha  Entrega", fuenteTituloPedido)
                tablaPedidos.AddCell(celdaTitulo)
                celdaTitulo.Phrase = New Phrase("Precio Final CLP", fuenteTituloPedido)
                tablaPedidos.AddCell(celdaTitulo)
                tablaPedidos.HeaderRows = 1
                For Each clsTransaccionDetalle As WebServices.clsTransaccionDetalle In clsTransaccionListado.ObtenerTransaccionDetalle(intTipo, intDocEntry, "", 0)
                    Dim strCadena As String = clsTransaccionDetalle.strNombreProducto.ToString.Trim
                    If (Microsoft.VisualBasic.Left(strCadena, 9) <> "INTERESES" And Microsoft.VisualBasic.Left(strCadena, 6) <> "FLETES") Then
                        If (clsTransaccionResumen.strTipoVenta = "ventaPuestoFundo" Or clsTransaccionResumen.strTipoVenta = "ventaCalzadaProveedor" Or clsTransaccionResumen.strTipoVenta = "ventaCostoEspecial" Or clsTransaccionResumen.strTipoVenta = "ventaLiquidacion") Then
                            dblMargenComercialProducto = (100 - ((clsTransaccionDetalle.dblPrecioUnitarioProductoCompra * 100) / clsTransaccionDetalle.dblPrecioUnitarioProducto))
                        Else
                            dblMargenComercialProducto = (100 - ((clsTransaccionDetalle.dblCostoComercialProducto * 100) / clsTransaccionDetalle.dblPrecioUnitarioProducto))
                        End If
                        intTotalLinea = (clsTransaccionDetalle.intTotalUnitarioProducto + clsTransaccionDetalle.intInteresProducto + clsTransaccionDetalle.intFleteProducto)
                        celdaCuerpo = New PdfPCell()
                        celdaCuerpo.VerticalAlignment = Element.ALIGN_MIDDLE
                        celdaCuerpo.HorizontalAlignment = Element.ALIGN_CENTER
                        celdaCuerpo.Phrase = New Phrase(clsTransaccionDetalle.strCodigoProducto, fuenteDetallePedido)
                        tablaPedidos.AddCell(celdaCuerpo)
                        celdaCuerpo.HorizontalAlignment = Element.ALIGN_LEFT
                        celdaCuerpo.Phrase = New Phrase(clsTransaccionDetalle.strNombreProducto, fuenteDetallePedido)
                        tablaPedidos.AddCell(celdaCuerpo)
                        celdaCuerpo.HorizontalAlignment = Element.ALIGN_RIGHT
                        celdaCuerpo.Phrase = New Phrase(FormatNumber(clsTransaccionDetalle.intCantidadProducto, 0), fuenteDetallePedidoNumero)
                        tablaPedidos.AddCell(celdaCuerpo)
                        celdaCuerpo.HorizontalAlignment = Element.ALIGN_CENTER
                        celdaCuerpo.Phrase = New Phrase(clsTransaccionDetalle.strMonedaProducto, fuenteDetallePedido)
                        tablaPedidos.AddCell(celdaCuerpo)
                        celdaCuerpo.HorizontalAlignment = Element.ALIGN_RIGHT
                        celdaCuerpo.Phrase = New Phrase(FormatNumber(clsTransaccionDetalle.dblPrecioUnitarioProducto, 2), fuenteDetallePedidoNumero)
                        tablaPedidos.AddCell(celdaCuerpo)
                        celdaCuerpo.HorizontalAlignment = Element.ALIGN_RIGHT
                        celdaCuerpo.Phrase = New Phrase(FormatNumber(clsTransaccionDetalle.intTotalUnitarioProducto, 0), fuenteDetallePedidoNumero)
                        tablaPedidos.AddCell(celdaCuerpo)
                        celdaCuerpo.HorizontalAlignment = Element.ALIGN_RIGHT
                        celdaCuerpo.Phrase = New Phrase(FormatNumber(clsTransaccionDetalle.dblDescuentoMaximo, 2), fuenteDetallePedidoNumero)
                        tablaPedidos.AddCell(celdaCuerpo)
                        celdaCuerpo.HorizontalAlignment = Element.ALIGN_RIGHT
                        celdaCuerpo.Phrase = New Phrase(FormatNumber(dblMargenComercialProducto, 2), fuenteDetallePedidoNumero)
                        tablaPedidos.AddCell(celdaCuerpo)
                        celdaCuerpo.HorizontalAlignment = Element.ALIGN_RIGHT
                        celdaCuerpo.Phrase = New Phrase(FormatNumber(clsTransaccionDetalle.intInteresProducto, 0), fuenteDetallePedidoNumero)
                        tablaPedidos.AddCell(celdaCuerpo)
                        celdaCuerpo.HorizontalAlignment = Element.ALIGN_RIGHT
                        celdaCuerpo.Phrase = New Phrase(FormatNumber(clsTransaccionDetalle.intFleteProducto, 0), fuenteDetallePedidoNumero)
                        tablaPedidos.AddCell(celdaCuerpo)
                        celdaCuerpo.HorizontalAlignment = Element.ALIGN_CENTER
                        celdaCuerpo.Phrase = New Phrase(clsTransaccionDetalle.strFechaEntregaProducto, fuenteDetallePedido)
                        tablaPedidos.AddCell(celdaCuerpo)
                        celdaCuerpo.HorizontalAlignment = Element.ALIGN_RIGHT
                        celdaCuerpo.Phrase = New Phrase(FormatNumber(intTotalLinea, 0), fuenteDetallePedidoNumero)
                        tablaPedidos.AddCell(celdaCuerpo)
                        intTotalNeto += intTotalLinea
                    End If
                Next
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase(" ")
                celdaCuerpo.Border = 0
                celdaCuerpo.Colspan = 9
                tablaPedidos.AddCell(celdaCuerpo)
                Dim celdaTituloTotal = New PdfPCell()
                celdaTituloTotal.Border = 0
                celdaTituloTotal.HorizontalAlignment = Element.ALIGN_RIGHT
                celdaTituloTotal.VerticalAlignment = Element.ALIGN_MIDDLE
                celdaTituloTotal.Phrase = New Phrase("Total Neto", fuenteTituloTotal)
                celdaTituloTotal.Colspan = 2
                tablaPedidos.AddCell(celdaTituloTotal)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.HorizontalAlignment = 2
                celdaCuerpo.VerticalAlignment = Element.ALIGN_MIDDLE
                celdaCuerpo.Phrase = New Phrase(FormatNumber(intTotalNeto, 0), fuenteDetallePedidoNumero)
                tablaPedidos.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase(" ")
                celdaCuerpo.Border = 0
                celdaCuerpo.Colspan = 9
                tablaPedidos.AddCell(celdaCuerpo)
                celdaTituloTotal.Phrase = New Phrase("Total IVA", fuenteTituloTotal)
                celdaTituloTotal.Colspan = 2
                tablaPedidos.AddCell(celdaTituloTotal)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.HorizontalAlignment = 2
                celdaCuerpo.VerticalAlignment = Element.ALIGN_MIDDLE
                celdaCuerpo.Phrase = New Phrase(FormatNumber(intTotalNeto * 0.19, 0), fuenteDetallePedidoNumero)
                tablaPedidos.AddCell(celdaCuerpo)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.Phrase = New Phrase(" ")
                celdaCuerpo.Border = 0
                celdaCuerpo.Colspan = 9
                tablaPedidos.AddCell(celdaCuerpo)
                celdaTituloTotal.Phrase = New Phrase("Total Bruto", fuenteTituloTotal)
                celdaTituloTotal.Colspan = 2
                tablaPedidos.AddCell(celdaTituloTotal)
                celdaCuerpo = New PdfPCell()
                celdaCuerpo.HorizontalAlignment = 2
                celdaCuerpo.VerticalAlignment = Element.ALIGN_MIDDLE
                celdaCuerpo.Phrase = New Phrase(FormatNumber(intTotalNeto * 1.19, 0), fuenteDetallePedidoNumero)
                tablaPedidos.AddCell(celdaCuerpo)
                objDocumento.Add(tablaPedidos)
                objDocumento.Add(New Paragraph(" "))
                parrafo = New Paragraph()
                parrafo.IndentationLeft = 70
                parrafo.Alignment = Element.ALIGN_LEFT
                parrafo.Font = FontFactory.GetFont("Arial", 10, Element.ALIGN_LEFT)
                Dim tipoVenta As String = ""
                If clsTransaccionResumen.strTipoVenta = "ventaBodega" Then
                    tipoVenta = "BODEGA PROPIA"
                ElseIf clsTransaccionResumen.strTipoVenta = "ventaConsignada" Then
                    tipoVenta = "CONSIGNADA"
                ElseIf clsTransaccionResumen.strTipoVenta = "ventaPuestoFundo" Then
                    tipoVenta = "PUESTO FUNDO"
                ElseIf clsTransaccionResumen.strTipoVenta = "ventaCalzadaProveedor" Then
                    tipoVenta = "CALZADA PROVEEDOR"
                ElseIf clsTransaccionResumen.strTipoVenta = "ventaCostoEspecial" Then
                    tipoVenta = "COSTO ESPECIAL"
                ElseIf clsTransaccionResumen.strTipoVenta = "ventaLiquidacion" Then
                    tipoVenta = "LIQUIDACION"
                End If
                parrafo.Add("Tipo de Venta: " + tipoVenta)
                objDocumento.Add(parrafo)
                parrafo.Clear()
                parrafo.Add("Orden de Compra: " + clsTransaccionResumen.strOrdenCompra)
                objDocumento.Add(parrafo)
                parrafo.Clear()
                parrafo.Add("Fecha Documento: " + clsTransaccionResumen.strFechaOrdenVenta)
                objDocumento.Add(parrafo)
                parrafo.Clear()
                parrafo.Add("Fecha Entrega: " + clsTransaccionResumen.strFechaVencimiento)
                objDocumento.Add(parrafo)
                parrafo.Clear()
                parrafo.Add("Fecha Vencimiento: " + clsTransaccionResumen.strFechaVencimiento)
                objDocumento.Add(parrafo)
                parrafo.Clear()
                parrafo.Add("Tipo de Pago: " + clsTransaccionResumen.strNombrePlazoVenta)
                objDocumento.Add(parrafo)
            Next
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
    End Sub
End Module