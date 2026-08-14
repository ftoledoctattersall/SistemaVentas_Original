var tablaHtml = new TablaHtml();
tablaHtml.arrFilas = new Array();
var productosAgregados = 0;
function AgregarLineaProducto() {
    var productosAgregados = tablaHtml.arrFilas.length;
    if (productosAgregados == 10) {
        alert("Cantidad maximo de pedidos son diez.");
        $("#imgAgregar").hide();
    }
    else{
        var lineaProducto = new LineaProducto();
        lineaProducto.id = Math.random() * 1000000;
        lineaProducto.hdnBodegaProducto = $("#hdnBodegaProducto").val();
        lineaProducto.txtCodigoProducto = $("#txtCodigoProducto").val();
        lineaProducto.txtNombreProducto = $("#txtNombreProducto").val();
        lineaProducto.hdnHibridoProducto = $("#hdnHibridoProducto").val();
        lineaProducto.txtCantidadProducto = $("#txtCantidadProducto").val();
        lineaProducto.txtMonedaProducto = $("#txtMonedaProducto").val();
        lineaProducto.txtPrecioUnitarioProducto = $("#txtPrecioUnitarioProducto").val();
        lineaProducto.txtTotalUnitarioProducto = $("#txtTotalUnitarioProducto").val();
        lineaProducto.txtDescuentoProducto = $("#txtDescuentoProducto").val();
        lineaProducto.txtMargenComercialProducto = $("#txtMargenComercialProducto").val();
        lineaProducto.txtInteresProducto = $("#txtInteresProducto").val();
        lineaProducto.hdnDescuentoProducto = $("#hdnDescuentoProducto").val();
        lineaProducto.txtFleteProducto = $("#txtFleteProducto").val();
        lineaProducto.txtFechaEntregaProducto = $("#txtFechaEntregaProducto").val();
        lineaProducto.txtPrecioFinalProducto = $("#txtPrecioFinalProducto").val();
        lineaProducto.hdnPrecioUnitarioRealProducto = $("#hdnPrecioUnitarioRealProducto").val();
        lineaProducto.hdnCostoComercialProducto = $("#hdnCostoComercialProducto").val();
        lineaProducto.hdnCostoReposicionProducto = $("#hdnCostoReposicionProducto").val();
        lineaProducto.hdnInventariableProducto = $("#hdnInventariableProducto").val();
        lineaProducto.auxFleteMinimoProducto = lineaProducto.txtFleteProducto;
        lineaProducto.auxPrecioUnitarioProducto = parseInt(lineaProducto.txtTotalUnitarioProducto) / parseInt(lineaProducto.txtCantidadProducto);
        lineaProducto.auxPorcentajeFleteProducto = 0.00;
        RestaurarFleteProducto();
        if(EsInventariableProducto){
            var strAutoriza = ObtenerAutorizacion(lineaProducto.hdnBodegaProducto, lineaProducto.txtCodigoProducto, lineaProducto.auxPrecioUnitarioProducto);
            var strAutorizaModal = ObtenerAutorizacionModal(lineaProducto.hdnBodegaProducto, lineaProducto.txtCodigoProducto, lineaProducto.auxPrecioUnitarioProducto);
            var obj_parse = $.parseJSON(strAutoriza);
            lineaProducto.autoriza = strAutoriza;
            lineaProducto.autoriza_mod = strAutorizaModal;
            if (strAutoriza == "0") {
                lineaProducto.auxTipoAutorizacion = "0";
                lineaProducto.auxCargoAutorizador = "";
            }
            else {
                for (x = 0; x <= obj_parse.length - 1; x++) {
                    if (obj_parse[x][1] == "BM1" || obj_parse[x][1] == "BM2" || obj_parse[x][1] == "BM3") {
                        lineaProducto.auxTipoAutorizacion = 1;
                        lineaProducto.auxCargoAutorizador = obj_parse[x][3];
                    }
                    else if (obj_parse[x][1] == "BC1" || obj_parse[x][1] == "BC2" || obj_parse[x][1] == "BC3") {
                        lineaProducto.auxTipoAutorizacion = 2;
                        lineaProducto.auxCargoAutorizador = obj_parse[x][3];
                    }
                }
            }
        }


        lineaProducto.txtCondicion = "";   //$("#txtCondicion").val().toUpperCase();
        lineaProducto.txtMotivo = "";      //$("#txtMotivo").val();
        lineaProducto.hdnMotivo = 0;       //$("#hdnMotivo").val();

        lineaProducto.auxFechaCompra = "";

        tablaHtml.arrFilas.push(lineaProducto);
        $("#divPedido").html(tablaHtml.rendering());
    }
}
function EliminarLineaProducto(id) {
    var lineaProducto = tablaHtml.buscaLineaProducto(id);
    if (lineaProducto != null) {
        if (tablaHtml.eliminaLineaProducto(id)) {
            alert("Producto eliminado correctamente.");
            $("#divPedido").html(tablaHtml.rendering());
            RestaurarFleteProducto();
            ActualizarVistaFleteMinimo();
            if (tablaHtml.arrFilas.length == 0 && EsDespachoTAI()) {
                $('#divFlete').hide();
            }
            $("#imgAgregar").show();
        } else {
            alert("Problema al eliminar el producto.");
        }
    }
}
function EsInventariableProducto() {
    var strInventariableProducto = document.getElementById("hdnInventariableProducto").value;
    if (strInventariableProducto == 1) {
        return true;
    }
    return false;
}
function EsDespachoTAI() {
    var intDespacho = document.getElementById("cmbTipoDespacho").options[document.getElementById("cmbTipoDespacho").selectedIndex].value;
    if (intDespacho == "3" || intDespacho == "5") {
        return true;
    }
    return false;
}
function ActualizarFlete(obj) {
    var objTabla = obj;
    var divFlete = document.getElementById('divFlete');
    var dblTotalFleteActualizado = 0;
    if (EsDespachoTAI()) {
        $('#divFlete').show();
        for (i = 0; i < objTabla.arrFilas.length; i++) {
            var lineaProducto = objTabla.arrFilas[i];
            dblTotalFleteActualizado += parseFloat(lineaProducto.txtFleteProducto);
        }
        if (objTabla.arrFilas.length > 0) {
            var strTotalFlete = dblTotalFleteActualizado;
            var html = '<table border="1px" style="float:left">';
            html += '<tbody>';
            html += '<tr>';
            html += '<td style="width:105px;">Flete Minimo</td>';
            html += '<td style="width:210px;"><div id="divFleteMinimo">' + parseInt(ObtenerTotalFleteMinimo()) + ' CLP</div></td>';
            html += '</tr>';
            html += '<tr>';
            html += '<td style="width:105px;">Flete Propuesto</td>';
            html += '<td style="width:210px;"><div><input type="text" id="txtFlete" value="' + strTotalFlete + '" class="textboxSmall" maxlength="8" onkeypress="return ValidarNumero(event);" onchange="ModificarFlete(this.value);"> CLP</div></td>';
            html += '</tr>';
            html += '</tbody>';
            html += '</table>';
            divFlete.innerHTML = html;
        } else {
            $('#divFlete').hide();
        }
    } else {
        dblTotalFleteActualizado = 0;
        $('#divFlete').hide();
    }
}
function ActualizarVistaFleteMinimo() {
    if (EsDespachoTAI()) {
        $('#divFleteMinimo').empty();
        $('#divFleteMinimo').html(function () {
            return parseInt(ObtenerTotalFleteMinimo()) + " CLP";
        });
    }
}
function ModificarFlete(dblTotalFleteDigitado) {
    var txtFlete = document.getElementById('txtFlete');
    var dblTotalFleteMinimo = parseFloat(ObtenerTotalFleteMinimo());
    var dblTotalFleteActualizado = ObtenerTotalFleteActualizado();
    if (parseFloat(dblTotalFleteDigitado) < dblTotalFleteMinimo){
        alert("Valor flete no puede ser menor a valor flete minimo.");
        txtFlete.value = dblTotalFleteMinimo;
        for (i = 0; i < tablaHtml.arrFilas.length; i++) {
            if (dblTotalFleteActualizado > 0) {
                tablaHtml.arrFilas[i].auxPorcentajeFleteProducto = parseFloat(tablaHtml.arrFilas[i].txtFleteProducto) / parseFloat(dblTotalFleteActualizado);
                tablaHtml.arrFilas[i].txtFleteProducto = Math.round(parseFloat(tablaHtml.arrFilas[i].auxPorcentajeFleteProducto * dblTotalFleteMinimo));
                tablaHtml.arrFilas[i].txtFleteProducto += "";
            }
        }
        $("#divPedido").html(tablaHtml.rendering());
    }
    else {
        for (i = 0; i < tablaHtml.arrFilas.length; i++) {
            if (dblTotalFleteActualizado > 0) {
                tablaHtml.arrFilas[i].auxPorcentajeFleteProducto = parseFloat(tablaHtml.arrFilas[i].txtFleteProducto) / parseFloat(dblTotalFleteActualizado);
                tablaHtml.arrFilas[i].txtFleteProducto = Math.round(parseFloat(tablaHtml.arrFilas[i].auxPorcentajeFleteProducto * dblTotalFleteDigitado));
                tablaHtml.arrFilas[i].txtFleteProducto += "";
            }
        }
        $("#divPedido").html(tablaHtml.rendering());
    }
}
function ObtenerTotalFleteMinimo() {
    var dblTotalFleteMinimo = 0;
    if (EsDespachoTAI()) {
        for (i = 0; i < tablaHtml.arrFilas.length; i++) {
            dblTotalFleteMinimo += parseFloat(tablaHtml.arrFilas[i].auxFleteMinimoProducto);
        }
    }
    return dblTotalFleteMinimo;
}
function ObtenerTotalFleteActualizado() {
    var dblTotalFleteActualizado = 0;
    if (EsDespachoTAI()) {
        for (i = 0; i < tablaHtml.arrFilas.length; i++) {
            dblTotalFleteActualizado += parseFloat(tablaHtml.arrFilas[i].txtFleteProducto);
        }
    }
    return dblTotalFleteActualizado;
}
function ObtenerFleteProducto(codigoBodega, codigoProducto, cantidad, monto) {
    var strBodega = codigoBodega;
    var strProducto = codigoProducto;
    var intCantidadProducto = parseInt(cantidad);
    var intTotalUnitarioProducto = parseInt(monto);
    var dblFleteReal = 0.0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvFleteProducto.asmx/ObtenerFleteProducto",
        dataType: "xml",
        data: { "strBodega": strBodega, "strProducto": strProducto, "intCantidadProducto": intCantidadProducto, "intTotalUnitarioProducto": intTotalUnitarioProducto },
        success: function (xml) {
            $(xml).find("ArrayOfClsFleteProducto").each(function () {
                $(this).find("clsFleteProducto").each(function () {
                    var $registro = $(this);
                    dblFleteReal = $registro.find("ArtFlete").text();
                });
            });
        }
    });
    return dblFleteReal;
}
function RestaurarFleteProducto() {
    if (EsDespachoTAI()) {
        for (i = 0; i < tablaHtml.arrFilas.length; i++) {
            var lineaProducto = tablaHtml.arrFilas[i];
            lineaProducto.txtFleteProducto = ObtenerFleteProducto(lineaProducto.hdnBodegaProducto, lineaProducto.txtCodigoProducto, lineaProducto.txtCantidadProducto, lineaProducto.txtTotalUnitarioProducto);
            lineaProducto.auxFleteMinimoProducto = lineaProducto.txtFleteProducto;
        }
    }
    else {
        for (i = 0; i < tablaHtml.arrFilas.length; i++) {
            var lineaProducto = tablaHtml.arrFilas[i]; 
            lineaProducto.txtFleteProducto = 0;
            lineaProducto.auxFleteMinimoProducto = lineaProducto.txtFleteProducto;
        }
    }
    $("#divPedido").html(tablaHtml.rendering());
    ActualizarVistaFleteMinimo();
}
function ObtenerInteresProducto(codigoBodega, codigoProducto, monto) {
    var intOpcion = 10;
    var strCliente = document.getElementById("txtCodigoCliente").value;
    var strBodega = codigoBodega;
    var strProducto = codigoProducto;
    var strPlazoVenta = document.getElementById("cmbPlazoVenta").options[document.getElementById("cmbPlazoVenta").selectedIndex].value;
    var arrPlazoVenta = strPlazoVenta.split("_");
    var intPlazoVenta = arrPlazoVenta[0];
    var intTotalUnitarioProducto = parseInt(monto);
    var fltTasaInteres = 0.0;
    var strFechaVencimiento = "";
    var strFechaCompra = "";
    var dblInteresReal = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvInteresProducto.asmx/ObtenerInteresProducto",
        dataType: "xml",
        data: { "intOpcion": intOpcion, "strCliente": strCliente, "strBodega": strBodega, "strProducto": strProducto, "intPlazoVenta": intPlazoVenta, "intTotalUnitarioProducto": intTotalUnitarioProducto, "fltTasaInteres": fltTasaInteres, "strFechaVencimiento": strFechaVencimiento, "strFechaCompra": strFechaCompra },
        success: function (xml) {
            $(xml).find("ArrayOfClsInteresProducto").each(function () {
                $(this).find("clsInteresProducto").each(function () {
                    var $registro = $(this);
                    dblInteresReal = $registro.find("ArtInteres").text();
                });
            });
        }
    });
    return dblInteresReal;
}
function RestaurarInteresProducto() {
    for (i = 0; i < tablaHtml.arrFilas.length; i++) {
        var lineaProducto = tablaHtml.arrFilas[i];
        lineaProducto.txtInteresProducto = ObtenerInteresProducto(lineaProducto.hdnBodegaProducto, lineaProducto.txtCodigoProducto, lineaProducto.txtTotalUnitarioProducto);
    }
    $("#divPedido").html(tablaHtml.rendering());
}
function ActualizarFechaEntregaProducto() {
    var strFechaEntregaOrden = document.getElementById("txtFechaEntregaOrden").value;
    for (i = 0; i < tablaHtml.arrFilas.length; i++) {
        var lineaProducto = tablaHtml.arrFilas[i];
        lineaProducto.txtFechaEntregaProducto = strFechaEntregaOrden;
    }
    $("#divPedido").html(tablaHtml.rendering());
}
function ObtenerAutorizacion(strBodega, strProducto, dblPrecioUnitarioProducto) {
    var intOpcion = 10;
    var strOperador = parent.document.getElementById("hdnUser").value;
    var strFechaOrdenVenta = FormatearFechaAñoMesDia(document.getElementById("txtFechaOrdenVenta").value);
    var strTipoVenta = "VBP";
    var strRetorno = "0";
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvAutorizacion.asmx/ObtenerAutorizacion",
        dataType: "xml",
        data: { "intOpcion": intOpcion, "strOperador": strOperador, "strBodega": strBodega, "strProducto": strProducto, "strFechaOrdenVenta": strFechaOrdenVenta, "dblPrecioUnitarioProducto": dblPrecioUnitarioProducto, "strTipoVenta": strTipoVenta },
        success: function (data) {
            strRetorno = $(data).find("string").text();
        }
    });
    return strRetorno;
}
function ObtenerAutorizacionModal(strBodega, strProducto, dblPrecioUnitarioProducto) {
    var intOpcion = 10;
    var strOperador = parent.document.getElementById("hdnUser").value;
    var strFechaOrdenVenta = FormatearFechaAñoMesDia(document.getElementById("txtFechaOrdenVenta").value);
    var strTipoVenta = "VBP";
    var strRetorno = "0";
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvAutorizacion.asmx/ObtenerAutorizacionModal",
        dataType: "xml",
        data: { "intOpcion": intOpcion, "strOperador": strOperador, "strBodega": strBodega, "strProducto": strProducto, "strFechaOrdenVenta": strFechaOrdenVenta, "dblPrecioUnitarioProducto": dblPrecioUnitarioProducto, "strTipoVenta": strTipoVenta },
        success: function (data) {
            strRetorno = $(data).find("string").text();
        }
    });
    return strRetorno;
}