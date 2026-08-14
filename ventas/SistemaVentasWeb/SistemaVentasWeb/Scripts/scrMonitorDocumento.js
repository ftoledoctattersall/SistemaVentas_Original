$(function () {
    $("#txtFechaDesde").datepicker({
        beforeShow: EstablecerMaximaFechaDesde,
        showAnim: "clip",
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
        onClose: function (selectedDate) {
            $("#txtFechaHasta").datepicker("option", "minDate", selectedDate);
        }
    });
    $("#txtFechaHasta").datepicker({
        beforeShow: EstablecerMaximaFechaHasta,
        showAnim: "clip",
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
    });
    $("#divLoader").hide();
    $("#divLoader").bind({
        ajaxStart: function () { $(this).show(); },
        ajaxStop: function () { $(this).hide(); }
    });
});
$(document).ready(function () {
    CargarTextoFechaDesde();
    CargarTextoFechaHasta();
    CargarTextoCliente();
    CargarComboEstado();
    $("#divLoader").css({
        display: "none"
    });
});
function ValidarFechaDesde() {
}
function ValidarFechaHasta() {
}
function EstablecerMaximaFechaDesde() {
    var intMaximo = 0;
    return { maxDate: intMaximo + "d" };
}
function EstablecerMaximaFechaHasta() {
    var intMaximo = 0;
    return { maxDate: intMaximo + "d" };
}
function CargarTextoFechaDesde() {
    var datFechaActual = new Date();
    datFechaActual = SumarDias(datFechaActual, -7)
    var intDiaActual = ((datFechaActual.getDate())).toString();
    var intMesActual = ((datFechaActual.getMonth()) + 1).toString();
    var intAñoActual = ((datFechaActual.getFullYear())).toString();
    if (intDiaActual < 10) {
        intDiaActual = "0" + intDiaActual;
    }
    if (intMesActual < 10) {
        intMesActual = "0" + intMesActual;
    }
    document.getElementById("txtFechaDesde").value = intDiaActual + "/" + intMesActual + "/" + intAñoActual;
}
function CargarTextoFechaHasta() {
    var datFechaActual = new Date();
    var intDiaActual = ((datFechaActual.getDate())).toString();
    var intMesActual = ((datFechaActual.getMonth()) + 1).toString();
    var intAñoActual = ((datFechaActual.getFullYear())).toString();
    if (intDiaActual < 10) {
        intDiaActual = "0" + intDiaActual;
    }
    if (intMesActual < 10) {
        intMesActual = "0" + intMesActual;
    }
    document.getElementById("txtFechaHasta").value = intDiaActual + "/" + intMesActual + "/" + intAñoActual;
}
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
                            value: item.CliCodigo,
                            CliRut: item.CliRut,
                            CliCodigo: item.CliCodigo,
                        }
                    }))
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            //document.getElementById("hdnCodigoCliente").value = ui.item.CliCodigo;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}
function CargarComboEstado() {
    var strParametro = "documento";
    var intIndex = 0;
    var cmbEstado = document.getElementById("cmbEstado");
    cmbEstado.options.length = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvParametro.asmx/ObtenerParametro",
        dataType: "xml",
        data: { "strParametro": strParametro },
        success: function (xml) {
            $(xml).find("ArrayOfClsParametro").each(function () {
                $(this).find("clsParametro").each(function () {
                    var $registro = $(this);
                    var ParEnlace = $registro.find("ParEnlace").text();
                    var ParNombre = $registro.find("ParNombre").text();
                    cmbEstado.options[intIndex] = new Option(ParNombre, ParEnlace);
                    intIndex++;
                });
            });
        }
    });
}
function Consultar() {
    var strDesde = document.getElementById("txtFechaDesde").value;
    var strHasta = document.getElementById("txtFechaHasta").value;
    var intDiferenciaDias = ObtenerDiferenciaDias(strDesde, strHasta);
    if (intDiferenciaDias>30) {
        alert("Periodo de consulta excede los 30 dias, ha seleccionado " + intDiferenciaDias + " dias.");
        return false;
    }
    else if (document.getElementById("txtCodigoCliente").value=="") {
        alert("Debe digitar o seleccionar cliente a consultar.");
        document.getElementById("txtCodigoCliente").focus();
        return false;
    }
    else if (document.getElementById("cmbEstado").options[document.getElementById("cmbEstado").selectedIndex].value=="Seleccione") {
        alert("Debe seleccionar estado de los documentos a consultar.");
        document.getElementById("cmbEstado").focus();
        return false;
    }
    else {
        var intPagina = 1;
        var strPaginaFinal = "";
        CargarCuadro();
        CargarPaginacion();     
        document.getElementById("hdnPaginaActual").value = 1;
        strPaginaFinal = document.getElementById("hdnPaginaFinal").value;
        CargarCuadroResumen(intPagina);
        strPaginasDesdeHasta = 'Pag. ' + intPagina + ' / ' + strPaginaFinal;
        $("#txtPaginas").html(strPaginasDesdeHasta);
    }
}
function CargarCuadro() {
    var strNombreEstado = document.getElementById("cmbEstado").options[document.getElementById("cmbEstado").selectedIndex].text;
    var strTabla = "";
    strTabla += '<table id="tblResumen" border="0px" style="width:1000px">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="7">' + strNombreEstado + '</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdCenter" style="width:40px">Ver</td>';
    strTabla += '       <td class="tdCenter" style="width:100px">N° Docto</td>';
    strTabla += '       <td class="tdLeft"   style="width:500px">Cliente</td>';
    strTabla += '       <td class="tdCenter" style="width:90px">Fecha Docto.</td>';
  //strTabla += '       <td class="tdCenter" style="width:90px">Fecha Entrega</td>';
    strTabla += '       <td class="tdCenter" style="width:90px">Fecha Vecto.</td>';
    strTabla += '       <td class="tdCenter" style="width:90px">Tipo Docto.</td>';
    strTabla += '       <td class="tdCenter" style="width:90px">Accion</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr><td id="tdResumen" colspan="7">Cargando...</td></tr>';
    strTabla += '   <tr><td id="tdAvanzar" colspan="7" class="tdCenter"></td></tr>';
    strTabla += '</table>';
    $("#divCuadroResumen").html(strTabla);
}
function CargarCuadroResumen(intPagina) {
    var strCodigoCliente = document.getElementById("txtCodigoCliente").value;
    var strCodigoProducto = "";//document.getElementById("hdnCodigoProducto").value;
    var strCodigoEstado = document.getElementById("cmbEstado").options[document.getElementById("cmbEstado").selectedIndex].value;
    var strFechaDesde=FormatearFechaAñoMesDia(document.getElementById("txtFechaDesde").value);
    var strFechaHasta=FormatearFechaAñoMesDia(document.getElementById("txtFechaHasta").value);
    //var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);
    var intUsuario = parseInt(parent.document.getElementById("hdnUsuario").value);
    var intEmpleado = parseInt(parent.document.getElementById("hdnEmpleado").value);
    var strTabla = "";
    var blnHayDatos = false;
    MostrarProgreso();
    $.ajax({
        type: "post",
        async: true,
        url: "/Services/srvMonitorDocumento.asmx/ObtenerMonitorDocumentoResumen",
        dataType: "xml",
        data: { "strCodigoCliente": strCodigoCliente, "strCodigoProducto": strCodigoProducto, "strCodigoEstado": strCodigoEstado, "strFechaDesde": strFechaDesde, "strFechaHasta": strFechaHasta, "intOperador": intEmpleado, "intPagina": intPagina },
        success: function (xml) {
            $(xml).find("ArrayOfClsMonitorDocumento").each(function () {
                $(this).find("clsMonitorDocumento").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var intDocCodigo = $registro.find("DocCodigo").text();
                    var strCliNombre = $registro.find("CliNombre").text();
                    var strDocFecha = $registro.find("DocFecha").text();
                    var strEntFecha = $registro.find("EntFecha").text();
                    var strVenFecha = $registro.find("VenFecha").text();
                    var strDocTipo = $registro.find("DocTipo").text();
                    var strDocEstado = $registro.find("DocEstado").text();
                    strTabla += '<table id="tblResumen' + intDocCodigo + '" border="0px" style="width:1000px">';
                    strTabla += '   <tr id="tr' + intDocCodigo + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);">';
                    strTabla += '       <td class="tdCenter" style="width:40px"><img id="img1' + intDocCodigo + '" width="12px" height="12px" title="Haga clic para visualizar detalle de pedido." alt="Expandir" src="../Images/agregar.gif" onclick="javascript:CargarCuadroDetalle(this,' + "'" + strDocTipo + "'" + "," + intDocCodigo + ');"/></td>';
                    strTabla += '       <td class="tdCenter" style="width:100px">' + intDocCodigo + '</td>';
                    strTabla += '       <td class="tdLeft"   style="width:500px">' + strCliNombre + '</td>';
                    strTabla += '       <td class="tdCenter" style="width:90px">' + strDocFecha + '</td>';
                  //strTabla += '       <td class="tdCenter" style="width:90px">' + strEntFecha + '</td>';
                    strTabla += '       <td class="tdCenter" style="width:90px">' + strVenFecha + '</td>';
                    strTabla += '       <td class="tdCenter" style="width:90px">' + strDocTipo + '</td>';
                    switch (strDocEstado) {
                        case "Pendientes de Autorización":
                            strTabla += '<td class="tdCenter" style="width:90px"><img id="img2' + intDocCodigo + '" width="12px" height="12px" title="Haga clic para visualizar estado de autorizaciones." alt="Expandir" src="../Images/visualizar.png" onclick="CargarCuadroDispatcher(this,' + intDocCodigo + ');"/></td>';
                            break;
                        case "Autorizados No Creados":
                            strTabla += '<td class="tdCenter" style="width:90px"><img id="img2' + intDocCodigo + '" width="12px" height="12px" title="Haga clic para procesar borrador de venta." alt="Procesar" src="../Images/procesar.png" onclick="ProcesarDocumento(this,' + "'" + strDocTipo + "'" + "," + intDocCodigo + ');"/></td>';
                            break;
                        case "Autorización Rechazada":
                            strTabla +='<td class="tdCenter"  style="width:90px"><img id="img2' + intDocCodigo + '" width="12px" height="12px" title="Haga clic para visualizar estado de autorizaciones." alt="Expandir" src="../Images/visualizar.png" onclick="CargarCuadroDispatcher(this,' + intDocCodigo + ');"/></td>';
                            break;
                        case "Pedidos Cancelados":
                            strTabla += '<td class="tdCenter" style="width:90px">---</td>';
                            break;
                        case "Pendientes de Facturación":
                            strTabla += '<td class="tdCenter" style="width:90px"><img id="img2' + intDocCodigo + '" width="12px" height="12px" title="Haga clic para facturar orden de venta." alt="Expandir" src="../Images/visualizar.png" onclick="VisualizarOrdenVenta(' + intDocCodigo + ');"/> <img id="img22' + intDocCodigo + '" width="15px" height="18px" title="Haga clic para cancelar orden de venta." alt="Expandir" src="../Images/cancelar.png" onclick="CancelarPedido(' + "'" + intDocCodigo + "'" + "," + intUsuario + ');"/></td>';
                            break;
                        case "Facturados":
                            strTabla += '<td class="tdCenter" style="width:90px">---</td>';
                            break;
                        default:
                            strTabla += '<td class="tdCenter" style="width:90px">---</td>';
                    }
                    strTabla += '   </tr>';
                    strTabla += '   <tr><td id="tdDetalle' + intDocCodigo + '" colspan="7"></td></tr>'
                    strTabla += '   <tr><td id="tdAutorizacion' + intDocCodigo + '" colspan="7"></td></tr>'
                    strTabla += '</table>';
                    document.getElementById("tdResumen").innerHTML = strTabla;
                });
            });
            if (!blnHayDatos) {
                strTabla += "<table id='tblResumen' border='0px' style='width:1000px'><tr><td colspan='7'>No hay informacion segun criterios de busquedas...</td></tr></table>";
                document.getElementById("tdResumen").innerHTML = strTabla;
            }
            OcultarProgreso();
        }
    });
}
function CargarCuadroDetalle(obj, strDocTipo, intDocCodigo) {
    var strImagen = obj.id;
    if (document.getElementById(strImagen).alt == "Expandir") {
        var strHtml = "";
        strHtml += '<table id="tblDetalle' + intDocCodigo + '" border="1" style="width:1000px">';
        strHtml += '    <tr>';
        strHtml += '       <td class="tdLeft">Codigo</td>';
        strHtml += '       <td class="tdLeft">Producto</td>';
        strHtml += '       <td class="tdRight">Cantidad</td>';
        strHtml += '       <td class="tdCenter">Moneda</td>';
        strHtml += '       <td class="tdRight">Precio Unitario</td>';
        strHtml += '       <td class="tdRight">Total Unitario CLP</td>';
        strHtml += '       <td class="tdRight">Descto.%</td>';
        strHtml += '       <td class="tdRight">Interes CLP</td>';
        strHtml += '       <td class="tdRight">Fec.Entrega</td>';
        strHtml += '    </tr>';
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
                        var strFecEntrega = $registro.find("FecEntrega").text();
                        strHtml += '<tr>';
                        strHtml += '    <td class="tdLeft">' + strArtCodigo + '</td>';
                        strHtml += '    <td class="tdLeft">' + strArtNombre + '</td>';
                        strHtml += '    <td class="tdRight">' + intArtCantidad + '</td>';
                        strHtml += '    <td class="tdCenter">' + strArtMoneda + '</td>';
                        strHtml += '    <td class="tdRight">' + decArtPrecioUnitario + '</td>';
                        strHtml += '    <td class="tdRight">' + intArtPrecioTotal + '</td>';
                        strHtml += '    <td class="tdRight">' + decPorDescuento + '</td>';
                        strHtml += '    <td class="tdRight">' + intArtInteres + '</td>';
                        strHtml += '    <td class="tdRight">' + strFecEntrega + '</td>';
                        strHtml += '</tr>';
                    });
                });
            }
        });
        strHtml += '</table>';
        var strCelda = "tdDetalle" + intDocCodigo;
        var strImagen = "img1" + intDocCodigo;
        document.getElementById(strCelda).innerHTML = strHtml;
        document.getElementById(strImagen).style.width = "12px";
        document.getElementById(strImagen).style.height = "12px";
        document.getElementById(strImagen).src = "../Images/eliminar.gif";
        document.getElementById(strImagen).alt = "Contraer";
        document.getElementById(strImagen).title = 'Haga clic para ocultar detalle de pedido.'
    }
    else {
        var strCelda = "tdDetalle" + intDocCodigo;
        var strHtml = "tblDetalle" + intDocCodigo;
        var parent = document.getElementById(strCelda);
        var child = document.getElementById(strHtml);
        parent.removeChild(child);
        document.getElementById(strImagen).src = "../Images/agregar.gif";
        document.getElementById(strImagen).alt = "Expandir";
        document.getElementById(strImagen).title = 'Haga clic para visualizar detalle de pedido.'
    }
}
function CargarCuadroDispatcher(obj, intDocCodigo) {
    var strImagen = obj.id;
    if (document.getElementById(strImagen).alt == "Expandir") {
        var strHtml = "";
        strHtml += '<table id="tblAutorizacion' + intDocCodigo + '" border="1" style="width:1000px">';
        strHtml += '    <tr>';
        strHtml += '       <td class="tdLeft">Autorizador</td>';
        strHtml += '       <td class="tdLeft">Cargo</td>';
        strHtml += '       <td class="tdCenter">Tipo</td>';
        strHtml += '       <td class="tdCenter">Estado</td>';
        strHtml += '       <td class="tdLeft">Backup</td>';
        strHtml += '       <td class="tdCenter">Fecha Creacion</td>';
        strHtml += '       <td class="tdCenter">Fecha Respuesta</td>';
        strHtml += '       <td class="tdLeft">Comentario</td>';
        strHtml += '    </tr>';
        $.ajax({
            type: "post",
            async: false,
            url: "/Services/srvDispatcher.asmx/ObtenerDispatcher",
            dataType: "xml",
            data: { "intDocCodigo": intDocCodigo },
            success: function (xml) {
                $(xml).find("ArrayOfClsDispatcher").each(function () {
                    $(this).find("clsDispatcher").each(function () {
                        var $registro = $(this);
                        var strDisOficial = $registro.find("DisOficial").text();
                        var strDisCargo = $registro.find("DisCargo").text();
                        var strDisTipo = $registro.find("DisTipo").text();
                        var strDisEstado = $registro.find("DisEstado").text();
                        var intDisBackup = $registro.find("DisBackup").text();
                        var strDisFechaCreacion = $registro.find("DisFechaCreacion").text();
                        var strDisFechaActualizacion = $registro.find("DisFechaActualizacion").text();
                        var strDisComentario = $registro.find("DisComentario").text();
                        strHtml += '<tr>';
                        strHtml += '    <td class="tdLeft">' + strDisOficial + '</td>';
                        strHtml += '    <td class="tdLeft">' + strDisCargo + '</td>';
                        strHtml += '    <td class="tdCenter">' + strDisTipo + '</td>';
                        strHtml += '    <td class="tdCenter">' + strDisEstado + '</td>';
                        strHtml += '    <td class="tdLeft">' + intDisBackup + '</td>';
                        strHtml += '    <td class="tdCenter">' + strDisFechaCreacion + '</td>';
                        strHtml += '    <td class="tdCenter">' + strDisFechaActualizacion + '</td>';
                        strHtml += '    <td class="tdLeft">' + strDisComentario + '</td>';
                        strHtml += '</tr>';
                    });
                });
            }
        });
        strHtml += '</table>';
        var strCelda = "tdAutorizacion" + intDocCodigo;
        var strImagen = "img2" + intDocCodigo;
        document.getElementById(strCelda).innerHTML = strHtml;
        document.getElementById(strImagen).style.width = "12px";
        document.getElementById(strImagen).style.height = "12px";
        document.getElementById(strImagen).src = "../Images/visualizar.png";
        document.getElementById(strImagen).alt = "Contraer";
        document.getElementById(strImagen).title = 'Haga clic para ocultar estado de autorizaciones.'
    }
    else {
        var strCelda = "tdAutorizacion" + intDocCodigo;
        var strHtml = "tblAutorizacion" + intDocCodigo;
        var parent = document.getElementById(strCelda);
        var child = document.getElementById(strHtml);
        parent.removeChild(child);
        document.getElementById(strImagen).src = "../Images/visualizar.png";
        document.getElementById(strImagen).alt = "Expandir";
        document.getElementById(strImagen).title = 'Haga clic para visualizar estado de autorizaciones.'
    }
}
function ProcesarDocumento(obj, strDocTipo, intDocCodigo) {
    document.getElementById(obj.id).style.visibility = "hidden";
    var intUsuario = parseInt(parent.document.getElementById("hdnUsuario").value);
    MostrarProgreso();
    if (strDocTipo === "OV") {
        $.ajax({
            type: "POST",
            async: true,
            dataType: "xml",
            url: "/Services/srvMonitorDocumento.asmx/ProcesarBorradorVentaEnOrdenVenta",
            data: { "intUsuario": intUsuario, "intDocCodigo": intDocCodigo },
            success: function (xml) {
                OcultarProgreso();
                var respuesta = $(xml).find('string').text();
                var mensaje = "";
                var codigo = "";
                var id = "";
                var split = respuesta.split("||");
                codigo = parseInt(split[0]);
                id = split[1];
                mensaje = split[2];
                //console.log('respuesta ProcesarBorradorVentaEnOrdenVenta: ' + respuesta);
                if (codigo === 0) {
                    document.getElementById(obj.id).style.visibility = "hidden";
                    alert('Se ha generado la orden de venta con identificador: ' + id);
                } else {
                    alert('Hubo un error al generar la orden de venta: ' + mensaje);
                }
            }
        });
    } else if (strDocTipo === "FV") {
        $.ajax({
            type: "POST",
            async: false,
            dataType: "xml",
            url: "/Services/srvMonitorDocumento.asmx/ProcesarBorradorVentaEnFacturaVenta",
            data: { "intUsuario": intUsuario, "intDocCodigo": intDocCodigo },
            success: function (xml) {
                OcultarProgreso();
                var respuesta = $(xml).find('string').text();
                var mensaje = "";
                var codigo = "";
                var id = "";
                var split = respuesta.split("||");
                codigo = parseInt(split[0]);
                id = split[1];
                mensaje = split[2];
                //console.log('respuesta ProcesarBorradorVentaEnFacturaVenta: ' + respuesta);
                if (codigo === 0) {
                    document.getElementById(obj.id).style.visibility = "hidden";
                    alert('Se ha generado la factura de venta con identificador: ' + id);
                } else {
                    alert('Hubo un error al generar la factura de venta: ' + mensaje);
                }
            }
        });
    }
}
function VisualizarOrdenVenta(intDocCodigo) {
    var strPagina = "";
    var intUsuario = parseInt(parent.document.getElementById("hdnUsuario").value);
    ObtenerTransaccionResumen(intDocCodigo);
    ObtenerLineaCredito();
    if (HaySuficienteCreditoDisponible() || document.getElementById("hdnDiasExtras").value == "0" || document.getElementById("hdnEstadoDocumento").value=="A") {
        strPagina = "pagVisualizarOrdenVenta.aspx?intUsuario=" + intUsuario + "&intDocNum=" + intDocCodigo;
    }
    else {
        strPagina = "pagVisualizarAutorizacion.aspx?intUsuario=" + intUsuario + "&intDocNum=" + intDocCodigo;
    }
     parent.document.getElementById("ifrMuestraPagina").src = strPagina;
}
function ObtenerTransaccionResumen(intDocCodigo) {
    var intConsulta = 2;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvTransaccion.asmx/ObtenerTransaccionResumen",
        dataType: "xml",
        data: { "intConsulta": intConsulta, "intDocCodigo": intDocCodigo },
        success: function (xml) {
            $(xml).find("ArrayOfClsTransaccionResumen").each(function () {
                $(this).find("clsTransaccionResumen").each(function () {
                    var $registro = $(this);
                    document.getElementById("hdnCodigoCliente").value = $registro.find("strCodigoCliente").text();
                    document.getElementById("hdnTotalDocumento").value = $registro.find("intTotalDocumento").text();
                    document.getElementById("hdnEstadoDocumento").value = $registro.find("strEstadoDocumento").text();
                    document.getElementById("hdnDiasExtras").value = $registro.find("intDiasExtras").text();
                });
            });
        }
    });
}
function ObtenerLineaCredito() {
    var strCliente = document.getElementById("hdnCodigoCliente").value;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvCliente.asmx/ObtenerClienteLineaCredito",
        dataType: "xml",
        data: { "strCliente": strCliente },
        success: function (xml) {
            $(xml).find("ArrayOfClsCliente").each(function () {
                $(this).find("clsCliente").each(function () {
                    var $registro = $(this);
                    document.getElementById("hdnCreditoDisponible").value = $registro.find("CliDisponible").text();
                });
            });
        }
    });
}
function HaySuficienteCreditoDisponible() {
    var intCreditoDisponible = parseInt(document.getElementById("hdnCreditoDisponible").value);
    var intTotalDocto = parseInt(document.getElementById("hdnTotalDocumento").value);
    if (intCreditoDisponible >= intTotalDocto)
        return true;
    else
        return false;
}
function CancelarPedido(intDocCodigo, intUsuario) {
    var blnRespuesta = confirm("¿ Desea cancelar orden de venta " + intDocCodigo + " ?.");
    if (blnRespuesta == true) {
        $.ajax({
            type: "post",
            async: false,
            url: "/Services/srvMonitorDocumento.asmx/CancelarOrdenVenta",
            dataType: "xml",
            data: { "intDocCodigo": intDocCodigo, "intUsuario": intUsuario },
            success: function (xml) {
                var strRespuesta = $(xml).find('string').text();
                var arrDatos = strRespuesta.split("|");
                var intDocNum = arrDatos[0];
                var intStatus = parseInt(arrDatos[1]);
                var strStatus = arrDatos[2];
                Consultar();
                if (intStatus === 0) {
                    alert("Se ha cancelado la orden de venta " + intDocNum + ".");
                } else {
                    alert("Hubo un error al cancelar la orden de venta: " + strStatus + ".");
                }
            }
        });
    }
}
function CargarPaginacion() {
    var strCodigoCliente = document.getElementById("txtCodigoCliente").value;
    var strCodigoProducto = "";
    var strFechaDesde = FormatearFechaAñoMesDia(document.getElementById("txtFechaDesde").value);
    var strFechaHasta = FormatearFechaAñoMesDia(document.getElementById("txtFechaHasta").value);
    var strCodigoEstado = document.getElementById("cmbEstado").options[document.getElementById("cmbEstado").selectedIndex].value;
    //var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);
    //var intUsuario = parseInt(parent.document.getElementById("hdnUsuario").value);
    var intEmpleado = parseInt(parent.document.getElementById("hdnEmpleado").value);
    var intPagina = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvMonitorDocumento.asmx/ObtenerMonitorDocumentoPagina",
        dataType: "xml",
        data: { "strCodigoCliente": strCodigoCliente, "strCodigoProducto": strCodigoProducto, "strCodigoEstado": strCodigoEstado, "strFechaDesde": strFechaDesde, "strFechaHasta": strFechaHasta, "intOperador": intEmpleado, "intPagina": intPagina },
        success: function (xml) {
            $(xml).find("ArrayOfClsMonitorDocumento").each(function () {
                $(this).find("clsMonitorDocumento").each(function () {
                    var $registro = $(this);
                    var intNumPagina = $registro.find("DocPagina").text();
                    var strPrimeraPagina = 'A';
                    var strRetrocedePagina = 'B';
                    var strAvanzaPagina = 'C';
                    var strUltimaPagina = Math.ceil(intNumPagina / 10);
                    document.getElementById("hdnPaginaFinal").value = strUltimaPagina;
                    var strTabla = "";
                    if (strUltimaPagina > 1) {
                        strTabla += '<table id="tblAvanzar" border="0px" style="width:1000px">';
                        strTabla += '   <tr><td colspan="5"><hr></td><tr>';
                        strTabla += '   <tr>';
                        strTabla += '       <td class="tdLeft"   style="width:10px"><img id="imgPrimera"   width="11px" height="14px" title="Primera pagina."    alt="Primera pagina"    src="../Images/primera.png"   onclick="javascript:CargarIndicePaginacion(' + "'" + strPrimeraPagina + "'" + ');"/></td>';
                        strTabla += '       <td class="tdCenter" style="width:10px"><img id="imgRetrocede" width="11px" height="14px" title="Retroceder pagina." alt="Retroceder pagina" src="../Images/anterior.png"  onclick="javascript:CargarIndicePaginacion(' + "'" + strRetrocedePagina + "'" + ');"/></td>';
                        strTabla += '       <td class="tdCenter" style="width:10px" id="txtPaginas"></td>';
                        strTabla += '       <td class="tdCenter" style="width:10px"><img id="imgAvanza"    width="11px" height="14px" title="Avanzar pagina."    alt="Avanzar pagina"    src="../Images/siguiente.png" onclick="javascript:CargarIndicePaginacion(' + "'" + strAvanzaPagina + "'" + ');"/></td>';
                        strTabla += '       <td class="tdRight"  style="width:10px"><img id="imgUltima"    width="11px" height="14px" title="Ultima pagina."     alt="Ultima pagina"     src="../Images/ultima.png"    onclick="javascript:CargarIndicePaginacion(' + "'" + strUltimaPagina + "'" + ');"/></td>';
                        strTabla += '   </tr>';
                        strTabla += '</table>';
                        document.getElementById("tdAvanzar").innerHTML = strTabla;
                    }
                });
            });
        }
    });
}
function CargarIndicePaginacion(strEstPagina) {
    if (strEstPagina == 'A') {
        var strPaginasDesdeHasta = "";
        var strPaginaFinal = document.getElementById("hdnPaginaFinal").value;
        intPagina = 1;
        CargarCuadroResumen(intPagina);
        document.getElementById("hdnPaginaActual").value = intPagina;
        strPaginasDesdeHasta = 'Pag. ' + intPagina + ' / ' + strPaginaFinal;
        $("#txtPaginas").html(strPaginasDesdeHasta);
    }
    if (strEstPagina == 'B') {
        var intRestaPagina = parseInt(document.getElementById("hdnPaginaActual").value);
        var strPaginaFinal = document.getElementById("hdnPaginaFinal").value;
        intRestaPagina = intRestaPagina - 1;
        if (intRestaPagina < 1) {
            intRestaPagina = 1;
        }
        CargarCuadroResumen(intRestaPagina);
        document.getElementById("hdnPaginaActual").value = intRestaPagina;
        strPaginasDesdeHasta = 'Pag. ' + intRestaPagina + ' / ' + strPaginaFinal;
        $("#txtPaginas").html(strPaginasDesdeHasta);
    }
    if (strEstPagina == 'C') {
        var intSumaPagina = parseInt(document.getElementById("hdnPaginaActual").value);
        var strPaginaFinal = parseInt(document.getElementById("hdnPaginaFinal").value);
        intSumaPagina = intSumaPagina + 1;
        if (intSumaPagina >= strPaginaFinal) {
            intSumaPagina = strPaginaFinal;
        }
        CargarCuadroResumen(intSumaPagina);
        document.getElementById("hdnPaginaActual").value = intSumaPagina;
        strPaginasDesdeHasta = 'Pag. ' + intSumaPagina + ' / ' + strPaginaFinal;
        $("#txtPaginas").html(strPaginasDesdeHasta);
    }
    if (strEstPagina != 'A' && strEstPagina != 'B' && strEstPagina != 'C') {
        CargarCuadroResumen(strEstPagina);
        document.getElementById("hdnPaginaActual").value = strEstPagina;
        strPaginasDesdeHasta = 'Pag. ' + strEstPagina + ' / ' + strEstPagina;
        $("#txtPaginas").html(strPaginasDesdeHasta);
    }
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