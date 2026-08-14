function TablaHtml() {
    var arrFilas = new Array();
    var porCosto = false;
    var porMargen = false;
    var porTasa = false;
    var porCredito = "";
    var porDeuda = "";
    var porProtesto = "";
    this.buscaLineaProducto = function (id) {
        var lineaProducto = new LineaProducto();
        for (i = 0; i < this.arrFilas.length; i++) {
            var lineaProductoBak = this.arrFilas[i];
            if (lineaProductoBak.id == id) {
                return lineaProductoBak;
            }
        }
        return null;
    };
    this.eliminaLineaProducto = function (id) {
        for (i = 0; i < this.arrFilas.length; i++) {
            var lineaProductoBak = this.arrFilas[i];
            if (lineaProductoBak.id == id) {
                this.arrFilas.splice(i, 1);
                return true;
            }
        }
        return false;
    };
    this.rendering = function () {
        var intCantidadProductos = 0;
        var strStyle = "";
        var dblTipoCambio = 1.00;
        var intSumaTotalUnitarioProducto = 0;
        var intSumaTotalCostoComercial = 0.00;
        var dblTotalMargenComercial = 0.00;
        var intTotalNeto = 0;
        var intTotalIva = 0;
        var intTotalBruto = 0;
        if (this.arrFilas.length > 0) {
            var $html = "";
            $html = '<table id="tblPedido" border="1px">';

            $html += '<tr class="trTituloSeccion">';
            $html += '<td class="tdLeftCortado" colspan="12">Datos de la Venta</td>';
            $html += '<td class="tdLeftCortadoCompra" colspan="8">Datos de la Compra</td>';
            $html += '<td class="tdLeftCortado" colspan="2"></td>';
            $html += '</tr>';

            $html += '<tr class="trTituloCabecera">';
            $html += '<td class="tdCenterCortado">Codigo</td>';
            $html += '<td class="tdCenterCortado">Descripcion</td>';
            $html += '<td class="tdCenterCortado">Cantidad</td>';
            $html += '<td class="tdCenterCortado">Moneda</td>';
            $html += '<td class="tdCenterCortado">Precio Unitario</td>';
            $html += '<td class="tdCenterCortado">Total Unitario CLP</td>';
            $html += '<td class="tdCenterCortado">Descto. %</td>';
            $html += '<td class="tdCenterCortado">Margen %</td>';
            $html += '<td class="tdCenterCortado">Interes CLP</td>';
            $html += '<td class="tdCenterCortado">Flete CLP</td>';
            $html += '<td class="tdCenterCortado">Fecha Entrega</td>';
            $html += '<td class="tdCenterCortado">Precio Final CLP</td>';

            $html += '<td class="tdCenterCortadoCompra">Dias Compra</td>';
            $html += '<td class="tdCenterCortadoCompra">Moneda</td>';
            $html += '<td class="tdCenterCortadoCompra">Precio Unitario</td>';
            $html += '<td class="tdCenterCortadoCompra">Total Unitario CLP</td>';
            $html += '<td class="tdCenterCortadoCompra">Proveedor</td>';
            $html += '<td class="tdCenterCortadoCompra">Tasa Interes %</td>';
            $html += '<td class="tdCenterCortadoCompra">Condicion</td>';
            $html += '<td class="tdCenterCortadoCompra">Motivo</td>';

            $html += '<td class="tdCenterCortado">Costo Reposicion</td>';
            $html += '<td class="tdCenterCortado"></td>';

            $html += '</tr>';
            ActualizarFlete(this);
            for (i = 0; i < this.arrFilas.length; i++) {
                var lineaProducto = this.arrFilas[i];
                intCantidadProductos++;
/*
                switch (lineaProducto.auxCargoAutorizador.toLowerCase()) {
                    case "jefe oficina":
                        strStyle = 'style="font-size: 0.8em;background-color:#82d882;"'
                        break;
                    case "gerente zona norte":
                        strStyle = 'style="font-size: 0.8em;background-color:yellow;"'
                        break;
                    case "gerente zona zur":
                        strStyle = 'style="font-size: 0.8em;background-color:yellow;"'
                        break;
                    case "gerente distribución":
                        strStyle = 'style="font-size: 0.8em;background-color:orange;"'
                        break;
                    case "gerente especialidad":
                        strStyle = 'style="font-size: 0.8em;background-color:orange;"'
                        break;
                    case "gerente maquinaria":
                        strStyle = 'style="font-size: 0.8em;background-color:orange;"'
                        break;
                    case "gerente comercial":
                        strStyle = 'style="font-size: 0.8em;background-color:red;"'
                        break;
                    default:
                        strStyle = 'style="font-size: 0.8em;"';
                }
*/
                dblTipoCambio = 1.00;
                if (lineaProducto.txtMonedaProducto == "USD")
                    dblTipoCambio = parseFloat(parent.document.getElementById("hdnDolar").value);
                else if (document.getElementById("txtMonedaProducto").value == "EUR") {
                    dblTipoCambio = parseFloat(parent.document.getElementById("hdnEuro").value);
                }

                intSumaTotalUnitarioProducto += (parseFloat(lineaProducto.txtPrecioUnitarioProducto) * parseInt(lineaProducto.txtCantidadProducto) * dblTipoCambio);
                //intSumaTotalCostoComercial += (parseFloat(lineaProducto.hdnCostoComercialProducto) * parseInt(lineaProducto.txtCantidadProducto) * dblTipoCambio);
                intSumaTotalCostoComercial += (parseFloat(lineaProducto.txtPrecioUnitarioProductoCompra) * parseInt(lineaProducto.txtCantidadProducto) * dblTipoCambio);
                dblTotalMargenComercial = (((intSumaTotalUnitarioProducto - intSumaTotalCostoComercial) / intSumaTotalUnitarioProducto) * 100.0).toFixed(2);
                lineaProducto.txtPrecioFinalProducto = parseInt(lineaProducto.txtTotalUnitarioProducto) + parseInt(lineaProducto.txtInteresProducto) + parseInt(lineaProducto.txtFleteProducto);

                intTotalNeto += parseInt(lineaProducto.txtPrecioFinalProducto);
                intTotalIva = parseInt(Math.round(intTotalNeto * 0.19));
                intTotalBruto = intTotalNeto + intTotalIva;
                var cadena = '<tr class="trTituloDetalle" ' + strStyle + ' >';

                cadena = cadena + '<td class="tdCenterCortado">' + lineaProducto.txtCodigoProducto + '</td>';
                cadena = cadena + '<td class="tdCenterCortado">' + lineaProducto.txtNombreProducto + '</td>';
                cadena = cadena + '<td class="tdRightCortado">' + lineaProducto.txtCantidadProducto + '</td>';
                cadena = cadena + '<td class="tdCenterCortado">' + lineaProducto.txtMonedaProducto + '</td>';
                cadena = cadena + '<td class="tdRightCortado">' + lineaProducto.txtPrecioUnitarioProducto + '</td>';
                cadena = cadena + '<td class="tdRightCortado">' + lineaProducto.txtTotalUnitarioProducto + '</td>';
                cadena = cadena + '<td class="tdRightCortado">' + lineaProducto.txtDescuentoProducto + '</td>';
                cadena = cadena + '<td class="tdRightCortado">' + lineaProducto.txtMargenComercialProducto + '</td>';
                cadena = cadena + '<td class="tdRightCortado">' + lineaProducto.txtInteresProducto + '</td>';
                cadena = cadena + '<td class="tdRightCortado">' + lineaProducto.txtFleteProducto + '</td>';
                cadena = cadena + '<td class="tdCenterCortado">' + lineaProducto.txtFechaEntregaProducto + '</td>';
                cadena = cadena + '<td class="tdRightCortado">' + lineaProducto.txtPrecioFinalProducto + '</td>';

                cadena = cadena + '<td class="tdRightCortado">' + lineaProducto.hdnDiasCompra + '</td>';
                cadena = cadena + '<td class="tdCenterCortado">' + lineaProducto.txtMonedaProductoCompra + '</td>';
                cadena = cadena + '<td class="tdRightCortado">' + lineaProducto.txtPrecioUnitarioProductoCompra + '</td>';
                cadena = cadena + '<td class="tdRightCortado">' + lineaProducto.txtTotalUnitarioProductoCompra + '</td>';
                cadena = cadena + '<td class="tdCenterCortado">' + lineaProducto.txtNombreProveedorCompra + '</td>';
                cadena = cadena + '<td class="tdRightCortado">' + lineaProducto.txtTasaInteresCompra + '</td>';
                cadena = cadena + '<td class="tdLeftCortado">' + lineaProducto.txtCondicion + '</td>';
                cadena = cadena + '<td class="tdLeftCortado">' + lineaProducto.txtMotivo + '</td>';
                               
                cadena = cadena + '<td class="tdRightCortado">' + lineaProducto.hdnCostoReposicionProducto + '</td>';
                cadena = cadena + '<td class="tdRightCortado"><a href="#" onclick="EliminarLineaProducto(' + lineaProducto.id + ');"><img class="imgEliminar" alt="Borrar Pedido" title="Haga clic para borrar pedido." src="../Images/borrar.png"/></a></td>';

                cadena = cadena + '</tr>'
                $html += cadena;
            }
            $html += '<tr><td class="tdLeft">           </td><td class="tdLeft" colspan="9">                                 </td><td class="tdLeft">Total Neto </td><td class="tdRight">' + intTotalNeto + ' </td><td colspan="10"></td></tr>';
            $html += '<tr><td class="tdLeft">Cant.Items:</td><td class="tdLeft" colspan="9">' + intCantidadProductos + '     </td><td class="tdLeft">Total IVA  </td><td class="tdRight">' + intTotalIva + '  </td><td colspan="10"></td></tr>';
            $html += '<tr><td class="tdLeft">Marg.Final:</td><td class="tdLeft" colspan="9">' + dblTotalMargenComercial + ' %</td><td class="tdLeft">Total Bruto</td><td class="tdRight">' + intTotalBruto + '</td><td colspan="10"></td></tr>';

            $html += '</table>';
            return $html;
        }
        return '';
    };
}
function LineaProducto() {
    var id;
    var hdnBodegaProducto;
    var txtCodigoProducto;
    var txtNombreProducto;
    var hdnHibridoProducto;
    var txtCantidadProducto;
    var txtMonedaProducto;
    var txtPrecioUnitarioProducto;
    var txtTotalUnitarioProducto;
    var txtDescuentoProducto;
    var txtMargenComercialProducto;
    var txtInteresProducto;
    var hdnDescuentoProducto;
    var txtFleteProducto;
    var txtFechaEntregaProducto;
    var txtPrecioFinalProducto;
    var hdnPrecioUnitarioRealProducto;
    var hdnCostoComercialProducto;
    var hdnCostoReposicionProducto;
    var hdnInventariableProducto;
    var auxPrecioUnitarioProducto;
    var auxFleteMinimoProducto;
    var auxPorcentajeFleteProducto;
    var auxTipoAutorizacion;
    var auxCargoAutorizador;

    var hdnDiasCompra;
    var txtTasaInteresCompra;
    var txtMonedaProductoCompra;
    var txtPrecioUnitarioProductoCompra;
    var txtTotalUnitarioProductoCompra;
    var txtNombreProveedorCompra;
    var txtCodigoProveedorCompra;

    var txtCondicion;
    var txtMotivo;
    var hdnMotivo;

    var auxFechaCompra;
}