$(function () {
    $("#txtFechaDesde").datepicker({
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
    var strParametro = "autorizacion";
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
    var strEstado = document.getElementById("cmbEstado").options[document.getElementById("cmbEstado").selectedIndex].value;
    if (strEstado == "Seleccione") {
        alert("Debe seleccionar estado de las autorizaciones a consultar.");
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
    strTabla += '<table id="tblResumen" border="0px" style="width:1250px">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="10">BORRADORES ' + strNombreEstado + '</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdCenter" style="width:50px">N° Docto</td>';
    strTabla += '       <td class="tdLeft"   style="width:400px">Cliente</td>';
    strTabla += '       <td class="tdCenter" style="width:90px">Fecha Docto.</td>';
    strTabla += '       <td class="tdCenter" style="width:90px">Fecha Entrega</td>';
    strTabla += '       <td class="tdCenter" style="width:90px">Fecha Vecto.</td>';
    strTabla += '       <td class="tdRight"  style="width:90px">Total CLP</td>';
    strTabla += '       <td class="tdCenter" style="width:90px">Autorizador</td>';
    strTabla += '       <td class="tdCenter" style="width:50px">Accion</td>';
    strTabla += '       <td class="tdLeft"   style="width:150px">Tipo Venta</td>';
    strTabla += '       <td class="tdLeft"   style="width:150px">Origen</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr><td id="tdResumen" colspan="10">Cargando...</td></tr>';
    strTabla += '   <tr><td id="tdAvanzar" colspan="10" class="tdCenter"></td></tr>';
    strTabla += '</table>';
    $("#divCuadroResumen").html(strTabla);
}
function CargarCuadroResumen(intPagina) {
    var strCodigoCliente = document.getElementById("txtCodigoCliente").value;
    var strCodigoEstado = document.getElementById("cmbEstado").options[document.getElementById("cmbEstado").selectedIndex].value;
    var strFechaDesde = FormatearFechaAñoMesDia(document.getElementById("txtFechaDesde").value);
    var strFechaHasta = FormatearFechaAñoMesDia(document.getElementById("txtFechaHasta").value);
    var strOperador = parent.document.getElementById("hdnUser").value;
    var strTabla = "";
    var blnHayDatos = false;
    MostrarProgreso();
    $.ajax({
        type: "post",
        async: true,
        url: "/Services/srvMonitorAutorizacion.asmx/ObtenerMonitorAutorizacionResumen",
        dataType: "xml",
        data: { "strCodigoCliente": strCodigoCliente, "strCodigoEstado": strCodigoEstado, "strFechaDesde": strFechaDesde, "strFechaHasta": strFechaHasta, "strOperador": strOperador, "intPagina": intPagina },
        success: function (xml) {
            $(xml).find("ArrayOfClsMonitorAutorizacion").each(function () {
                $(this).find("clsMonitorAutorizacion").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var intDocEntry = $registro.find("DocEntry").text();
                    var strCliNombre = $registro.find("CliNombre").text();
                    var strDocFecha = $registro.find("DocFecha").text();
                    var strEntFecha = $registro.find("EntFecha").text();
                    var strVenFecha = $registro.find("VenFecha").text();
                    var intDocTotal = $registro.find("DocTotal").text();
                    var strAutTipo = $registro.find("AutTipo").text();
                    var strDisCodigo = $registro.find("DisCodigo").text();
                    var strDisConcepto = $registro.find("DisConcepto").text();
                    var strDisTipo = $registro.find("DisTipo").text(); 
                    var strVenTipo = $registro.find("VenTipo").text();
                    var strOfiNombre = $registro.find("OfiNombre").text();
                    strTabla += '<table id="tblResumen' + intDocEntry + '" border="0px" style="width:1250px">';
                    strTabla += '   <tr id="tr' + intDocEntry + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);">';
                    strTabla += '       <td class="tdCenter" style="width:50px">' + intDocEntry + '</td>';
                    strTabla += '       <td class="tdLeft"   style="width:400px">' + strCliNombre + '</td>';
                    strTabla += '       <td class="tdCenter" style="width:90px">' + strDocFecha + '</td>';
                    strTabla += '       <td class="tdCenter" style="width:90px">' + strEntFecha + '</td>';
                    strTabla += '       <td class="tdCenter" style="width:90px">' + strVenFecha + '</td>';
                    strTabla += '       <td class="tdRight"  style="width:90px">' + FormatearFloatVista(intDocTotal,0) + '</td>';
                    strTabla += '       <td class="tdCenter" style="width:90px">' + strAutTipo  + '</td>';
                    strTabla += '       <td class="tdCenter" style="width:50px"><img id="img2' + intDocEntry + '" width="12px" height="12px" title="Haga clic para visualizar borrador de venta." alt="Expandir" src="../Images/visualizar.png" onclick="VisualizarBorradorVenta(' + "'" + strDisCodigo + "','" + strOperador + "','" + strDisTipo + "','" + strDisConcepto + "','" + strCodigoEstado + "'" + ');"/></td>';
                    strTabla += '       <td class="tdLeft"   style="width:150px">' + strVenTipo + '</td>';
                    strTabla += '       <td class="tdLeft"   style="width:150px">' + strOfiNombre + '</td>';
                    strTabla += '   </tr>';
                    strTabla += '   <tr><td id="tdDetalle' + intDocEntry + '" colspan="10"></td></tr>'
                    strTabla += '   <tr><td id="tdAutorizacion' + intDocEntry + '" colspan="10"></td></tr>'
                    strTabla += '</table>';
                    document.getElementById("tdResumen").innerHTML = strTabla;
                });
            });
            if (!blnHayDatos) {
                strTabla += "<table id='tblResumen' border='0px' style='width:1250px'><tr><td colspan='10'>No hay informacion segun criterios de busquedas...</td></tr></table>";
                document.getElementById("tdResumen").innerHTML = strTabla;
            }
            OcultarProgreso();
        }
    });
}
function VisualizarBorradorVenta(strDisCodigo ,strOperador, strDisTipo, strDisConcepto,strEstado) {
    var strPagina = "pagVisualizarBorradorVenta.aspx?i=" + strDisCodigo + "&a=" + strOperador + "&t=" + strDisTipo + "&c=" + strDisConcepto + "&e=" + strEstado;
    parent.document.getElementById("ifrMuestraPagina").src = strPagina;
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
function CargarPaginacion() {
    var strCodigoCliente = document.getElementById("txtCodigoCliente").value;
    var strCodigoEstado = document.getElementById("cmbEstado").value;
    var strFechaDesde = FormatearFechaAñoMesDia(document.getElementById("txtFechaDesde").value);
    var strFechaHasta = FormatearFechaAñoMesDia(document.getElementById("txtFechaHasta").value);
    var strOperador = parent.document.getElementById("hdnUser").value;
    var intPagina = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvMonitorAutorizacion.asmx/ObtenerMonitorAutorizacionPagina",
        dataType: "xml",
        data: { "strCodigoCliente": strCodigoCliente, "strCodigoEstado": strCodigoEstado, "strFechaDesde": strFechaDesde, "strFechaHasta": strFechaHasta, "strOperador": strOperador, "intPagina": intPagina },
        success: function (xml) {
            $(xml).find("ArrayOfClsMonitorAutorizacion").each(function () {
                $(this).find("clsMonitorAutorizacion").each(function () {
                    var $registro = $(this);
                    var intNumPagina = $registro.find("DocPagina").text();
                    var strPrimeraPagina = 'A';
                    var strRetrocedePagina = 'B';
                    var strAvanzaPagina = 'C';
                    var strUltimaPagina = Math.ceil(intNumPagina / 10);
                    document.getElementById("hdnPaginaFinal").value = strUltimaPagina;
                    var strTabla = "";
                    if (strUltimaPagina > 1) {
                        strTabla += '<table id="tblAvanzar" border="0px" style="width:1250px">';
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
        strPaginasDesdeHasta = 'Pag. ' +strEstPagina + ' / ' + strEstPagina;
        $("#txtPaginas").html(strPaginasDesdeHasta);
    }
}