$(document).ready(function () {
    CargarTextoCliente();
    CargarComboAño();
    CargarComboMes();
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
                        }
                    }))
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            document.getElementById("hdnCodigoCliente").value = ui.item.CliCodigo;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}
function CargarComboMes() {
    var datHoy = new Date();
    var intMes = parseInt(datHoy.getMonth()) + 1;
    var intIndex = 0;
    var cmbMes = document.getElementById("cmbMes");
    cmbMes.options.length = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvMes.asmx/ObtenerMes",
        dataType: "xml",
        data: {},
        success: function (xml) {
            intIndex = 0;
            $(xml).find("ArrayOfClsMes").each(function () {
                $(this).find("clsMes").each(function () {
                    var $registro = $(this);
                    var MesCodigo = $registro.find("MesCodigo").text();
                    var MesNombre = $registro.find("MesNombre").text();
                    cmbMes.options[intIndex] = new Option(MesNombre, MesCodigo);
                    intIndex++;
                });
                $("#cmbMes > option[value='" + intMes + "']").attr("selected", "selected");
            });
        }
    });
}
function CargarComboAño() {
    var datFecha = new Date();
    var intAño = datFecha.getFullYear();
    var cmbAño = document.getElementById("cmbAño");
    cmbAño.options.length = 0;
    for (intIndex = 0; intIndex < 2; intIndex++) {
        cmbAño.options[intIndex] = new Option(intAño, intAño);
        intAño--;
    }
    $("#cmbAño > option[value='1']").attr("selected", "selected");
}
function Consultar() {
    var strCliente = document.getElementById("txtCodigoCliente").value;
    if (strCliente == "") {
        document.getElementById("hdnCodigoCliente").value = "";
    }
    $("#divCuadroResumen").html("");
    $("#divCuadroDetalle").html("");
    CargarCuadroResumen();
}
function CargarCuadroResumen() {
    var intUsuario = parseInt(parent.document.getElementById("hdnUsuario").value);
    var strCodigoCliente = document.getElementById("hdnCodigoCliente").value;
    var intAño = document.getElementById("cmbAño").options[document.getElementById("cmbAño").selectedIndex].value;
    var intMes = document.getElementById("cmbMes").options[document.getElementById("cmbMes").selectedIndex].value;
    var strFolioPref = "";
    var intCuentaFactura = 0;
    var intCuentaBoleta = 0;
    var intCuentaNotaCredito = 0;
    var intSumaFactura = 0;
    var intSumaBoleta = 0;
    var intSumaNotaCredito = 0;
    var strTabla = "";
    strTabla += '<table id="tblResumen" border="0px">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="9">Resumen Doctos.</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdCenter">Tipo Docto.</td>';
    strTabla += '       <td class="tdCenter">Folio</td>';
    strTabla += '       <td class="tdCenter">Fec.Docto.</td>';
    strTabla += '       <td class="tdCenter">Fec.Vecto.</td>';
    strTabla += '       <td class="tdCenter">Fec.4ta.Copia</td>';
    strTabla += '       <td class="tdCenter">Código</td>';
    strTabla += '       <td class="tdCenter">Nombre</td>';
    strTabla += '       <td class="tdCenter">Tot.Neto $</td>';
    strTabla += '       <td class="tdCenter">Ver</td>';
    strTabla += '   </tr>';
    var blnHayDatos = false;  
    MostrarProgreso();
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvInforme.asmx/ObtenerInformeVentaMensual",
        dataType: "xml",
        data: { "intUsuario": intUsuario, "strCodigoCliente": strCodigoCliente, "intAño": intAño, "intMes": intMes },
        success: function (xml) {
            $(xml).find("ArrayOfClsInforme").each(function () {
                $(this).find("clsInforme").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var intDocEntry = $registro.find("DocEntry").text();
                    var intDocNum = $registro.find("DocNum").text();
                    var intFolioPref = $registro.find("FolioPref").text();
                    var intFolioNum = $registro.find("FolioNum").text();
                    var strDocDate = $registro.find("DocDate").text();
                    var strDocDueDate = $registro.find("DocDueDate").text();
                    var strDoc4taCopia = $registro.find("Doc4taCopia").text();
                    var strCardCode = $registro.find("CardCode").text();
                    var strCardName = $registro.find("CardName").text();
                    var intDocNeto = $registro.find("DocNeto").text();
                     if (intFolioPref == 33) {
                        strFolioPref = "FVR";
                        intCuentaFactura++;
                        intSumaFactura += parseInt(intDocNeto);
                     }
                     else if (intFolioPref == 39) {
                         strFolioPref = "BV";
                         intCuentaBoleta++;
                         intSumaBoleta += parseInt(intDocNeto);
                     }
                    else if (intFolioPref == 61) {
                        strFolioPref = "NCR";
                        intCuentaNotaCredito++;
                        intSumaNotaCredito += parseInt(intDocNeto);
                    }
                    strTabla += '<tr id="tr' + strFolioPref+intFolioNum + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);">';
                    strTabla += '   <td class="tdCenter">' + strFolioPref + '</td>';
                    strTabla += '   <td class="tdCenter">' + intFolioNum + '</td>';
                    strTabla += '   <td class="tdRight">' + strDocDate + '</td>';
                    strTabla += '   <td class="tdRight">' + strDocDueDate + '</td>';
                    strTabla += '   <td class="tdCenter">' + strDoc4taCopia + '</td>';
                    strTabla += '   <td class="tdCenter">' + strCardCode + '</td>';
                    strTabla += '   <td class="tdLeft">' + strCardName + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intDocNeto) + '</td>';
                    strTabla += '   <td class="tdCenter"><img id="img' + strFolioPref + intFolioNum + '" width="12px" height="12px" title="Haga clic para visualizar cuadro detalle." alt="Expandir" src="../Images/visualizar.png" onclick="CargarCuadroDetalle(this,' + "'" + strFolioPref + "'" + "," + intFolioNum + ');"/></td>';
                    strTabla += '</tr>';
                });
            });
            OcultarProgreso();
        }
    });
    if (!blnHayDatos) {
        strTabla += '<tr><td colspan="9">No hay informacion segun criterios de busquedas...</td></tr>';
        strTabla += '<tr><td colspan="9"><hr></td></tr>';
    }
    else {
        strTabla += '<tr><td colspan="9"><hr></td></tr>';
        strTabla += '<tr><td class="tdLeft" colspan="7">Total Resumen Doctos.</td><td class="tdRight">' + FormatearNumero(intSumaFactura + intSumaBoleta - intSumaNotaCredito) + '</td></tr>';
    }
    strTabla += '</table>';
    $("#divCuadroResumen").html(strTabla);
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
    strTabla += '   </tr>';
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
                    intSumaArtPrecioTotal += parseInt(intArtPrecioTotal);
                    strTabla += '<tr>';
                    strTabla += '   <td class="tdLeft">' + strArtCodigo + '</td>';
                    strTabla += '   <td class="tdLeft">' + strArtNombre + '</td>';
                    strTabla += '   <td class="tdRight">' + intArtCantidad + '</td>';
                    strTabla += '   <td class="tdCenter">' + strArtMoneda + '</td>';
                    strTabla += '   <td class="tdRight">' + decArtPrecioUnitario + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intArtPrecioTotal) + '</td>';
                    strTabla += '   <td class="tdRight">' + decPorDescuento + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intArtInteres) + '</td>';
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