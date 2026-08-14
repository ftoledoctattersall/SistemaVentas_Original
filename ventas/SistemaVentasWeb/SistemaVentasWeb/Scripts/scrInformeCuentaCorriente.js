$(document).ready(function () {
    CargarTextoCliente();
    $("#divLoader").css({
        display: "none"
    });
});
function CargarTextoCliente() {
    $("#txtCodigoCliente").autocomplete({
        max: 10,
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/Services/srvCliente.asmx/ObtenerCliente",
                dataType: "json",
                data: "{ 'strCliente':'" + request.term + "'}",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.CliCodigo + " " + item.CliNombre,
                            CliCodigo: item.CliCodigo,
                            CliNombre: item.CliNombre
                        }
                    }))
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            document.getElementById("hdnCodigoCliente").value = ui.item.CliCodigo;
            document.getElementById("hdnNombreCliente").value = ui.item.CliNombre;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}
function Consultar() {
    var strCliente = document.getElementById("txtCodigoCliente").value;
    if (strCliente == "") {
        alert("Debe ingresar cliente a consultar.");
    }
    else {
        CargarCuadroResumen();
    }
}
function CargarCuadroResumen() {
    var strCodigoCliente = document.getElementById("hdnCodigoCliente").value;
    var intSumaDocDebito = 0;
    var intSumaDocCredito = 0;
    var intCuentaVencida = 0;
    var intSumaVencida = 0;
    var intCuentaPorVencer = 0;
    var intSumaPorVencer = 0;
    var intCuentaCheque = 0;
    var intSumaCheque = 0;
    var decDocTipoCambio = 0.00;
    var decDocSaldoME = 0.00;
    var strTabla = "";
    strTabla += '<table id="tblResumen" border="0px">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="9">Resumen Doctos.</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdRight">Folio</td>';
    strTabla += '       <td class="tdCenter">Tipo Docto.</td>';
    strTabla += '       <td class="tdCenter">Fecha Docto.</td>';
    strTabla += '       <td class="tdCenter">Fecha Vecto.</td>';
    strTabla += '       <td class="tdRight">Total Debito $</td>';
    strTabla += '       <td class="tdRight">Total Credito $</td>';
    strTabla += '       <td class="tdRight">Saldo Docto. $</td>';
    strTabla += '       <td class="tdCenter">Estado Docto.</td>';
    //strTabla += '       <td class="tdRight  trTituloSeccion">Deuda</td>';
    //strTabla += '       <td class="tdCenter trTituloSeccion">Mon.Pago</td>';
    //strTabla += '       <td class="tdCenter trTituloSeccion">T/C</td>';
    strTabla += '       <td class="tdCenter">Ver</td>';
    strTabla += '   </tr>';
    var blnHayDatos = false;
    MostrarProgreso();
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvInforme.asmx/ObtenerInformeCuentaCorriente",
        dataType: "xml",
        data: { "strCodigoCliente": strCodigoCliente },
        success: function (xml) {
            $(xml).find("ArrayOfClsInforme").each(function () {
                $(this).find("clsInforme").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var intDocEntry = $registro.find("DocEntry").text();
                    var intDocNum = $registro.find("DocNum").text();
                    var strDocTipo = $registro.find("DocTipo").text();
                    var strAbrDoc = $registro.find("AbrDoc").text();
                    var strDocDate = $registro.find("DocDate").text();
                    var strDueDate = $registro.find("DueDate").text();
                    var intDocDebito = $registro.find("DocDebito").text();
                    var intDocCredito = $registro.find("DocCredito").text();
                    var intDocSaldo = $registro.find("DocSaldo").text();
                    var strDocEstado = $registro.find("DocEstado").text();
                    var strDocMonedaPago = $registro.find("DocMonedaPago").text();
                    decDocTipoCambio = $registro.find("DocTipoCambio").text();
                    decDocSaldoME = $registro.find("DocSaldoME").text();
                    strTabla += '   <tr id="tr' + intDocEntry + intDocDebito + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);">';
                    strTabla += '       <td class="tdRight">' + intDocEntry + '</td>';
                    strTabla += '       <td class="tdCenter">' + strDocTipo + " - " + strAbrDoc + '</td>';
                    strTabla += '       <td class="tdCenter">' + strDocDate + '</td>';
                    strTabla += '       <td class="tdCenter">' + strDueDate + '</td>';
                    strTabla += '       <td class="tdRight">' + FormatearNumero(intDocDebito) + '</td>';
                    strTabla += '       <td class="tdRight">' + FormatearNumero(intDocCredito) + '</td>';
                    strTabla += '       <td class="tdRight">' + FormatearNumero(intDocSaldo) + '</td>';
                    strTabla += '       <td class="tdCenter">' + strDocEstado + '</td>';
                    //strTabla += '       <td class="tdRight">' + decDocSaldoME + '</td>';
                    //strTabla += '       <td class="tdCenter">' + strDocMonedaPago + '</td>';
                    //strTabla += '       <td class="tdRight">' + decDocTipoCambio + '</td>';
                    if (strAbrDoc == "FV")//(strDocTipo == "FACTURA")
                        strTabla += '   <td class="tdCenter"><img id="img' + intDocEntry + intDocDebito + '" width="12px" height="12px" title="Haga clic para visualizar cuadro detalle." alt="Expandir" src="../Images/visualizar.png" onclick="CargarCuadroDetalle(this,' + "'" + "FV" + "'" + "," + intDocEntry + ');"/></td>';
                    else
                        strTabla += '   <td class="tdCenter"></td>';
                    strTabla += '   </tr>';
                    intSumaDocDebito += parseInt(intDocDebito);
                    intSumaDocCredito += parseInt(intDocCredito);
                    if (strAbrDoc == "FV") {
                        if (strDocEstado == "VENCIDO") {
                            intCuentaVencida++;
                            intSumaVencida += parseInt(intDocSaldo);
                        }
                        else if (strDocEstado == "POR VENCER") {
                            intCuentaPorVencer++;
                            intSumaPorVencer += parseInt(intDocSaldo);
                        }
                    }
                    else if (strAbrDoc == "CH") {
                        intCuentaCheque++;
                        intSumaCheque += parseInt(intDocSaldo);
                    }
                });
            });
            OcultarProgreso();
        }
    });
    if (!blnHayDatos) {
        strTabla += '<tr><td colspan="9">No hay informacion segun criterios de busquedas....</td></tr>';
        strTabla += '<tr><td colspan="9"><hr></td></tr>';
    }
    else {
        strTabla += '<tr><td colspan="9"><hr></td></tr>';
        strTabla += '<tr><td class="tdLeft" colspan="4">Total Resumen Doctos.</td><td class="tdRight">' + FormatearNumero(intSumaDocDebito) + '</td><td class="tdRight">' + FormatearNumero(intSumaDocCredito) + '</td><td colspan="2"></td></tr>';
    }
    strTabla += '</table>';
    $("#divCuadroDetalle").html('');
    $("#divCuadroResumen").html(strTabla);
    CargarCuadroTotalizado(intCuentaVencida, intSumaVencida, intCuentaPorVencer, intSumaPorVencer, intCuentaCheque, intSumaCheque);
}
function CargarCuadroDetalle(obj, strDocTipo, intDocCodigo) {
    var intSumaArtPrecioTotal = 0;
    var strTabla = "";
    strTabla += '<table id="tblDetalle' + intDocCodigo + '" border="0">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="8">Detalle Docto. ' + strDocTipo + ' - ' + intDocCodigo + '</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdLeft">Codigo</td>';
    strTabla += '       <td class="tdLeft">Producto</td>';
    strTabla += '       <td class="tdRight">Cantidad</td>';
    strTabla += '       <td class="tdCenter">Moneda</td>';
    strTabla += '       <td class="tdRight">Precio Unitario</td>';
    strTabla += '       <td class="tdRight">Total Unitario CLP</td>';
    strTabla += '       <td class="tdRight">Descto.%</td>';
    strTabla += '       <td class="tdRight">Interes CLP</td>';
    strTabla += '    </tr>';
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvMonitorDocumento.asmx/ObtenerMonitorDocumentoDetalle",
        dataType: "xml",
        data: { "strDocTipo": strDocTipo, "intDocCodigo": intDocCodigo },
        success: function (xml) {
            $(xml).find("ArrayOfClsMonitorDocumento").each(function () {
                $(this).find("clsMonitorDocumento").each(function () {
                    var $registro = $(this);
                    var strArtCodigo = $registro.find("ArtCodigo").text();
                    var strArtNombre = $registro.find("ArtNombre").text();
                    var intArtCantidad = $registro.find("ArtCantidad").text();
                    var strArtMoneda = $registro.find("ArtMoneda").text();
                    var decArtPrecioUnitario = parseFloat($registro.find("ArtPrecioUnitario").text()).toFixed(2);
                    var intArtPrecioTotal = $registro.find("ArtPrecioTotal").text();
                    var decPorDescuento = parseFloat($registro.find("PorDescuento").text()).toFixed(6);
                    var intArtInteres = $registro.find("ArtInteres").text();
                    intSumaArtPrecioTotal+=parseInt(intArtPrecioTotal);
                    strTabla += '<tr>';
                    strTabla += '    <td class="tdLeft">' + strArtCodigo + '</td>';
                    strTabla += '    <td class="tdLeft">' + strArtNombre + '</td>';
                    strTabla += '    <td class="tdRight">' + intArtCantidad + '</td>';
                    strTabla += '    <td class="tdCenter">' + strArtMoneda + '</td>';
                    strTabla += '    <td class="tdRight">' + decArtPrecioUnitario + '</td>';
                    strTabla += '    <td class="tdRight">' + FormatearNumero(intArtPrecioTotal) + '</td>';
                    strTabla += '    <td class="tdRight">' + decPorDescuento + '</td>';
                    strTabla += '    <td class="tdRight">' + intArtInteres + '</td>';
                    strTabla += '</tr>';
                });
            });
        }
    });
    strTabla += '   <tr><td colspan="8"><hr></td></tr>';
    strTabla += '   <tr><td colspan="5">Total Detalle Docto.</td><td class="tdRight">' + FormatearNumero(intSumaArtPrecioTotal) + '</td><td colspan="2"></td>';
    strTabla += '</table>';
    $("#divCuadroDetalle").html(strTabla);
}
function CargarCuadroTotalizado(intCuentaVencida, intSumaVencida, intCuentaPorVencer, intSumaPorVencer, intCuentaCheque, intSumaCheque) {
    var strCodigoCliente = document.getElementById("hdnCodigoCliente").value;
    var strNombreCliente = document.getElementById("hdnNombreCliente").value;
    var strTabla = "";
    strTabla += '<table id="tblCuentaResumen" border="0px">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="8">Totalizaciones</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdCenter" colspan="2">Cliente</td>';
    strTabla += '       <td class="tdCenter" colspan="2">Facturas Vencidas</td>';
    strTabla += '       <td class="tdCenter" colspan="2">Facturas por Vencer</td>';
    strTabla += '       <td class="tdCenter" colspan="2">Cheques en Cartera</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdLeft">Codigo</td>';
    strTabla += '       <td class="tdLeft">Nombre</td>';
    strTabla += '       <td class="tdRight">Cant.#</td>';
    strTabla += '       <td class="tdRight">Total Deuda Vencida $</td>';
    strTabla += '       <td class="tdRight">Cant.#</td>';
    strTabla += '       <td class="tdRight">Total Deuda x Vencer $</td>';
    strTabla += '       <td class="tdRight">Cant.#</td>';
    strTabla += '       <td class="tdRight">Total Chq. en Cartera $</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr>';
    strTabla += '       <td class="tdLeft">' + strCodigoCliente + '</td>';
    strTabla += '       <td class="tdLeft">' + strNombreCliente + '</td>';
    strTabla += '       <td class="tdRight">' + FormatearNumero(intCuentaVencida) + '</td>';
    strTabla += '       <td class="tdRight">' + FormatearNumero(intSumaVencida) + '</td>';
    strTabla += '       <td class="tdRight">' + FormatearNumero(intCuentaPorVencer) + '</td>';
    strTabla += '       <td class="tdRight">' + FormatearNumero(intSumaPorVencer) + '</td>';
    strTabla += '       <td class="tdRight">' + FormatearNumero(intCuentaCheque) + '</td>';
    strTabla += '       <td class="tdRight">' + FormatearNumero(intSumaCheque) + '</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr><td colspan="8"><br/></td></tr>';
    strTabla += '</table>';
    $("#divCuadroTotalizado").html(strTabla);
}
function MostrarProgreso() {
    $(document).ajaxStart(function () {
        $("#divLoader").show();
    });
}
function OcultarProgreso() {
    $(document).ajaxStop(function () {
        $("#divLoader").hide();
    });
}