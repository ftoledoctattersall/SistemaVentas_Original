$.fx.speeds._default = 600;
$(function () {
    $("#txtFechaOrdenVenta").datepicker({
        showAnim: "clip",
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
        onClose: function (selectedDate) {
            $("#txtFechaEntregaOrden").datepicker("option", "minDate", selectedDate);
        }
    });
    $("#txtFechaEntregaOrden").datepicker({
        showAnim: "clip",
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab']
    });
    $("#txtFechaVencimiento").datepicker({
        beforeShow: EstablecerRangoFechaVencimiento,
        showAnim: "clip",
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab']
    });
    $("#txtFechaEntregaProducto").datepicker({
        showAnim: "clip",
        minDate: 0,
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNamesMin: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab']
    });
    $("#divDireccion").dialog({
        modal: true,
        autoOpen: false,
        show: "blind",
        hide: "explode",
        closeOnEscapeType: true,
        width: 360,
        height: 330,
        beforeClose: function (event, ui) {
            LimpiarDireccion();
        }
    });
    $("#openerAgregarDireccionFacturacion").click(function () {
        document.getElementById("hdnDireccion").value = 1;
        document.getElementById("txtDireccion").value = "FACTURACION";
        $("#divDireccion").dialog("open");
        return false;
    });
    $("#openerAgregarDireccionDespacho").click(function () {
        document.getElementById("hdnDireccion").value = 0;
        document.getElementById("txtDireccion").value = "DESPACHO";
        $("#divDireccion").dialog("open");
        return false;
    });
    $("#divInventario").dialog({
        modal: false,
        autoOpen: false,
        show: "blind",
        hide: "explode",
        closeOnEscapeType: true,
        width: 600
    });
    $("#openerConsultarInventario").click(function () {
        $("#divInventario").dialog("open");
        return false;
    });
    $("#divLoader1").hide();
    $("#divLoader1").bind({
        ajaxStart: function () { $(this).show(); },
        ajaxStop: function () { $(this).hide(); }
    });
    $("#divLoader2").hide();
    $("#divLoader2").bind({
        ajaxStart: function () { $(this).show(); },
        ajaxStop: function () { $(this).hide(); }
    });
    $("#divProducto").dialog({
        modal: false,
        autoOpen: false,
        show: "blind",
        hide: "explode",
        closeOnEscapeType: true,
        width: 680
    });
    $("#openerConsultarProducto").click(function () {
        $("#divProducto").dialog("open");
        return false;
    });
});
$(document).ready(function () {
    $("#row2").hide();
    $("#row3").hide();
    $("#divVoucher").hide();
    CargarComboEmision();
    CargarTextoCliente();
    CargarComboMonedaPago();
    CargarTextoOperador();
    CargarHiddenPeriodoContable();
    CargarTextoFechas();
    CargarComboSerie();
    CargarComboTipoDespacho();
    CargarComboBodega();
    CargarTextoProducto();
    CargarComboRegion();
    $("#divLoader1").css({
        display: "none"
    });
    $("#divLoader2").css({
        display: "none"
    });
    $("#imgVerProducto").hide();
    if ($("#txtCotizacion").val() != "") {
        CargarDatosCRM();
    }
});



function CargarDatosCRM() {
    var intOpcion = 10;
    var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);
    var strEstado = "P";
    var intCotizacion = $("#txtCotizacion").val();
    var blnHayDatos = false;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvCotizacion.asmx/ObtenerCotizacionResumen",
        dataType: "xml",
        data: { "intOpcion": intOpcion, "intOperador": intOperador, "strEstado": strEstado, "intCotizacion": intCotizacion },
        success: function (xml) {
            $(xml).find("ArrayOfClsCotizacionResumen").each(function () {
                $(this).find("clsCotizacionResumen").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    if ($registro.find("EmiCodigo").text() == "F") {
                        document.getElementById("cmbEmision").selectedIndex = 0;
                    } else {
                        document.getElementById("cmbEmision").selectedIndex = 1;
                    }
                    document.getElementById("txtBuscaCliente").value = $registro.find("CliCodigo").text();
                    switch ($registro.find("PagMoneda").text()) {
                        case 'CLP':
                            document.getElementById("cmbMonedaPago").selectedIndex = 0;
                            break;
                        case 'USD':
                            document.getElementById("cmbMonedaPago").selectedIndex = 1;
                            break;
                        case 'EUR':
                            document.getElementById("cmbMonedaPago").selectedIndex = 2;
                            break;
                        default:
                            document.getElementById("cmbMonedaPago").selectedIndex = 0;
                    }
                    document.getElementById("cmbTipoDespacho").selectedIndex = parseInt($registro.find("DesCodigo").text()) - 1;
                    document.getElementById("hdnPlazoVenta").value = $registro.find("PlaCodigo").text();
                });
            });
        }
    });
    if (!blnHayDatos) {
        alert("Cotizacion no se encuentra pendiente.");
    }
    else {
        ObtenerCliente();
        BloquearBuscaCliente();
        BloquearEmision();
        //BloquearMonedaPago();
        //BloquearTipoDespacho();
        //BloquearPlazoVenta();
        $("#imgVerProducto").show();
        //BloquearBodega();
        BloquearBuscaProducto();
        CargarCuadroProducto();
    }
}
function ObtenerCliente() {
    var strCliente = document.getElementById("txtBuscaCliente").value;
    document.getElementById("txtBuscaCliente").value = "";
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvCliente.asmx/ObtenerCliente",
        dataType: "xml",
        data: { "strCliente": strCliente },
        success: function (xml) {
            $(xml).find("ArrayOfClsCliente").each(function () {
                $(this).find("clsCliente").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var CliBloqueado = $registro.find("CliBloqueado").text();
                    if (CliBloqueado=="false") {
                        if ($registro.find("CatCodigo").text() != "F") {
                            document.getElementById("txtNombreCliente").value = $registro.find("CliNombre").text();
                            document.getElementById("txtRutCliente").value = $registro.find("CliRut").text();
                            document.getElementById("txtCodigoCliente").value = $registro.find("CliCodigo").text();
                            document.getElementById("hdnTelefonoCliente").value = $registro.find("CliTelefono").text();
                            document.getElementById("hdnCorreoCliente").value = $registro.find("CliCorreo").text();
                            document.getElementById("hdnGiroCliente").value = $registro.find("CliGiro").text();
                            document.getElementById("txtCategoria").value = $registro.find("CatCodigo").text() + " - " + $registro.find("CatNombre").text();
                            //document.getElementById("hdnPlazoVenta").value = $registro.find("CliPlazoVentaCodigo").text();
                            document.getElementById("hdnGrupoCliente").value = $registro.find("GruNombre").text();
                            CargarTextoLineaCredito();
                            CargarComboVendedorAsociado();
                            CargarComboDirecccionFactura();
                            CargarComboDirecccionDespacho();
                            CargarComboPlazoVenta();
                            CargarCuadroFacturaImpaga();
                            CargarCuadroResumenFacturaImpaga();
                            CargarCuadroChequeProtestado();
                            CargarCuadroResumenChequeProtestado();
                        }
                        else {
                            alert("Cliente posee categoria F, comuniquese con el Depto. de Creditos y Cobranzas.");
                        }
                    }
                    else {
                        alert("Cliente se encuentra bloqueado, no podrá ser seleccionado.");
                    }
                });
            });
        }
    });
}
function BloquearBuscaCliente() {
    document.getElementById("txtBuscaCliente").className = "textboxLongReadonly";
    document.getElementById("txtBuscaCliente").disabled = true;
}
function BloquearTipoDespacho() {
    document.getElementById("cmbTipoDespacho").className = "textboxLongReadonly";
    document.getElementById("cmbTipoDespacho").disabled = true;
}
function BloquearPlazoVenta() {
    document.getElementById("cmbPlazoVenta").className = "textboxLongReadonly";
    document.getElementById("cmbPlazoVenta").disabled = true;
}
function BloquearBodega() {
    document.getElementById("cmbBodega").className = "textboxMediumReadonly";
    document.getElementById("cmbBodega").disabled = true;
}
function BloquearBuscaProducto() {
    document.getElementById("txtBuscaProducto").className = "textboxLongReadonly";
    document.getElementById("txtBuscaProducto").disabled = true;
}
function CargarCuadroProducto() {
    var intOpcion = 20;
    var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);
    var strEstado = "P";
    var intCotizacion = $("#txtCotizacion").val();
    var strTabla = "";
    strTabla = '<table id="tblProducto" border="0px">';
    strTabla += '<tr class="trTituloCabecera">';
    strTabla += '<td class="tdLeft"   style="width:80px;">Bodega</td>';
    strTabla += '<td class="tdLeft"   style="width:150px;">Codigo</td>';
    strTabla += '<td class="tdLeft"   style="width:170px;">Producto</td>';
    strTabla += '<td class="tdRight"  style="width:50px;">Cantidad</td>';
    strTabla += '<td class="tdRight"  style="width:80px;">Precio</td>';
    strTabla += '<td class="tdCenter" style="width:70px;">Moneda</td>';
    strTabla += '<td class="tdCenter" style="width:80px;">Entrega</td>';
    strTabla += '</tr>';
    var intEntro = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvCotizacion.asmx/ObtenerCotizacionDetalle",
        dataType: "xml",
        data: { "intOpcion": intOpcion, "intOperador": intOperador, "strEstado": strEstado, "intCotizacion": intCotizacion },
        success: function (xml) {
            $(xml).find("ArrayOfClsCotizacionDetalle").each(function () {
                $(this).find("clsCotizacionDetalle").each(function () {
                    intEntro = 1;
                    var $registro = $(this);
                    var strBodCodigo = $registro.find("BodCodigo").text();
                    var strArtCodigo = $registro.find("ArtCodigo").text();
                    var strArtNombre = $registro.find("ArtNombre").text();
                    var intArtCantidad = $registro.find("ArtCantidad").text();
                    var dblArtPrecio = $registro.find("ArtPrecio").text();
                    var strArtMoneda = $registro.find("ArtMoneda").text();
                    var strEntFecha = $registro.find("EntFecha").text();
                    strTabla += '<tr id="tr' + strBodCodigo + strArtCodigo + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);" onclick="ObtenerProducto(' + "'" + strBodCodigo + "'" + "," + "'" + strArtCodigo + "'" + "," + "'" + intArtCantidad + "'" + "," + "'" + dblArtPrecio + "'" + "," + "'" + strEntFecha + "'" + ');">';
                    strTabla += '<td class="tdLeft">' + strBodCodigo + '</td>';
                    strTabla += '<td class="tdLeft">' + strArtCodigo + '</td>';
                    strTabla += '<td class="tdLeft">' + strArtNombre + '</td>';
                    strTabla += '<td class="tdRight">' + FormatearNumero(intArtCantidad) + '</td>';
                    strTabla += '<td class="tdRight">' + FormatearNumero(dblArtPrecio) + '</td>';
                    strTabla += '<td class="tdCenter">' + strArtMoneda + '</td>';
                    strTabla += '<td class="tdCenter">' + strEntFecha + '</td>';
                    strTabla += '</tr>';
                });
            });
            if (intEntro == 0) {
                strTabla += "<tr><td colspan='7'>No hay informacion de producto(s) solicitado(s)...</td></tr>";
            }
        }
    });
    strTabla += '</table>';
    $("#divProducto").html(strTabla);
}
function ObtenerProducto(strBodCodigo, strArtCodigo, intArtCantidad, dblArtPrecio, strEntFecha) {
    $("#divProducto").dialog("close");
    var intOpcion = 10;
    var strProducto = strArtCodigo;
    var strBodega = document.getElementById("cmbBodega").options[document.getElementById("cmbBodega").selectedIndex].value;
    //if (strBodega != strBodCodigo) {
    //    alert("Bodega solicitada en cotizacion no corresponde al vendedor y/o tipo de venta.");
    //    return false;
    //}
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvProducto.asmx/ObtenerProducto",
        dataType: "xml",
        data: { "intOpcion": intOpcion, "strProducto": strProducto, "strBodega": strBodega },
        success: function (xml) {
            $(xml).find("ArrayOfClsProducto").each(function () {
                $(this).find("clsProducto").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var strMonedaPago = document.getElementById("cmbMonedaPago").options[document.getElementById("cmbMonedaPago").selectedIndex].value;
                    var strMonedaProducto = $registro.find("ArtMoneda").text();
                    if ((strMonedaPago == 'USD' || strMonedaPago == 'EUR') && (strMonedaPago != strMonedaProducto)) {
                        alert("No puede vender producto con moneda extranjera distinta a moneda de pago del cliente.");
                    }
                    else {
                        document.getElementById("txtNombreProveedor").value = $registro.find("ProNombre").text();
                        document.getElementById("txtNombreGrupo").value = $registro.find("GruNombre").text();
                        document.getElementById("txtEnvaseProducto").value = $registro.find("ArtEnvase").text();
                        document.getElementById("txtStockFisico").value = $registro.find("ArtStockFisico").text();
                        document.getElementById("txtStockDisponible").value = $registro.find("ArtStockDisponible").text();
                        document.getElementById("txtCodigoProducto").value = $registro.find("ArtCodigo").text();
                        document.getElementById("txtNombreProducto").value = $registro.find("ArtNombre").text();
                        document.getElementById("txtMonedaProducto").value = $registro.find("ArtMoneda").text();
                        document.getElementById("txtPrecioUnitarioProducto").value = $registro.find("ArtPrecioUnitario").text();
                        document.getElementById("hdnBodegaProducto").value = strBodega;
                        document.getElementById("hdnPrecioUnitarioRealProducto").value = $registro.find("ArtPrecioUnitario").text();
                        document.getElementById("hdnCostoComercialProducto").value = $registro.find("ArtCostoComercial").text();
                        document.getElementById("hdnInventariableProducto").value = $registro.find("ArtInventariable").text();
                        document.getElementById("hdnPorcentajeFlete").value = $registro.find("PorFlete").text();
                        var strGruCodigo = $registro.find("GruCodigo").text();
                        var dblCostoReposicionProducto = 0.00;
                        if (strGruCodigo == '102' || strGruCodigo == '117') {
                            document.getElementById("hdnCostoReposicionProducto").value = $registro.find("ArtCostoReposicion").text();
                        }
                        else {
                            document.getElementById("hdnCostoReposicionProducto").value = dblCostoReposicionProducto.toFixed(2);
                        }
                        CargarComboHibridoProducto();
                        CargarCuadroInventario();
                        //var intDespacho = document.getElementById("cmbTipoDespacho").options[document.getElementById("cmbTipoDespacho").selectedIndex].value;
                        //if (intDespacho == "3" || intDespacho == "5") {
                        //    if (confirm("¿Entrega al cliente precio unitario de venta con flete incluido?")) {
                        //        CambiarIncluyeFlete(1);
                        //    }
                        //    else {
                                  CambiarIncluyeFlete(0);
                        //    }
                        //}
                        document.getElementById("txtCantidadProducto").value = intArtCantidad;
                        document.getElementById("txtPrecioUnitarioProducto").value = dblArtPrecio;
                        document.getElementById("txtFechaEntregaProducto").value = strEntFecha;
                    }
                });
            });
        }
    });
}



function LimpiarDireccion() {
    document.getElementById("txtCalle").value = "";
    document.getElementById("txtNumero").value = "";
    document.getElementById("txtComuna").value = "";
    document.getElementById("txtCiudad").value = "";
}
function CrearDireccion() {
    if (document.getElementById("txtCalle").value == "") {
        alert("Debe digitar nombre de la calle.");
        document.getElementById("txtCalle").focus();
        return false;
    }
    //if (document.getElementById("txtNumero").value == "") {
    //    alert("Debe digitar numero del domicilio.");
    //    document.getElementById("txtNumero").focus();
    //    return false;
    //}
    if (document.getElementById("txtComuna").value == "") {
        alert("Debe digitar nombre de la comuna.");
        document.getElementById("txtComuna").focus();
        return false;
    }
    if (document.getElementById("txtCiudad").value == "") {
        alert("Debe digitar nombre de la ciudad.");
        document.getElementById("txtCiudad").focus();
        return false;
    }
    var intDireccion = parseInt(document.getElementById("hdnDireccion").value);
    var intUsuario = parseInt(parent.document.getElementById("hdnUsuario").value);
    var strCliente = document.getElementById("txtCodigoCliente").value;
    var strCalle = document.getElementById("txtCalle").value;
    var strNumero = document.getElementById("txtNumero").value;
    var strComuna = document.getElementById("txtComuna").value;
    var strCiudad = document.getElementById("txtCiudad").value;
    var intRegion = document.getElementById("cmbRegion").options[document.getElementById("cmbRegion").selectedIndex].value;
    MostrarProgreso1();
    $.ajax({
        type: "post",
        async: true,
        url: "/Services/srvCliente.asmx/RegistrarDireccion",
        dataType: "xml",
        data: { 'intDireccion': intDireccion, 'intUsuario': intUsuario, 'strCliente': strCliente, 'strCalle': strCalle, 'strNumero': strNumero, 'strComuna': strComuna, 'strCiudad': strCiudad, 'intRegion': intRegion },
        success: function (xml) {
            OcultarProgreso1();
            var strRespuesta = $(xml).find('string').text();
            if (strRespuesta < 0) {
                alert("Hubo un problema al crear la direccion.");
            } else {
                alert("Se ha creado correctamente la direccion.");
                if (intDireccion == 1)
                    CargarComboDirecccionFactura();
                else
                    CargarComboDirecccionDespacho();
            }
            $("#divDireccion").dialog("close");
        }
    });
}
function CargarComboRegion() {
    var intIndex = 0;
    var cmbRegion = document.getElementById("cmbRegion");
    cmbRegion.options.length = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvRegion.asmx/ObtenerRegion",
        dataType: "xml",
        data: { "null": null },
        success: function (xml) {
            $(xml).find("ArrayOfClsRegion").each(function () {
                $(this).find("clsRegion").each(function () {
                    var $registro = $(this);
                    var RegCodigo = $registro.find("RegCodigo").text();
                    var RegNombre = $registro.find("RegNombre").text();
                    cmbRegion.options[intIndex] = new Option(RegNombre, RegCodigo);
                    intIndex++;
                });
            });
        }
    });
}
function Avanzar() {
    $("#row1").hide();
    $("#row2").show();
}
function Retroceder() {
    $("#row1").show();
    $("#row2").hide();
}
function Avanzar2() {
    $("#row2").hide();
    $("#row3").show();
}
function Retroceder2() {
    $("#row2").show();
    $("#row3").hide();
}
function CargarComboEmision() {
    var strParametro = "emision";
    var intIndex = 0;
    var cmbEmision = document.getElementById("cmbEmision");
    cmbEmision.options.length = 0;
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
                    var ParCodigo = $registro.find("ParEnlace").text();
                    var ParNombre = $registro.find("ParNombre").text();
                    cmbEmision.options[intIndex] = new Option(ParNombre, ParCodigo);
                    intIndex++;
                });
            });
        }
    });
}
function BloquearEmision() {
    document.getElementById("cmbEmision").className = "textboxMediumReadonly";
    document.getElementById("cmbEmision").disabled = true;
    CargarComboPlazoVenta();
}
function CargarTextoCliente() {
    $("#txtBuscaCliente").autocomplete({
        max: 10,
        source: function (request, response) {
            $.ajax({
                type: "post",
                async: true,
                contentType: "application/json; charset=utf-8",
                url: "/Services/srvCliente.asmx/ObtenerCliente",
                dataType: "json",
                data: "{ 'strCliente':'" + request.term + "'}",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.CliCodigo + " " + item.CliNombre,
                          //value: '',
                            CliNombre: item.CliNombre,
                            CliRut: item.CliRut,
                            CliCodigo: item.CliCodigo,
                            CliBloqueado: item.CliBloqueado,
                            CliTelefono: item.CliTelefono,
                            CliCorreo: item.CliCorreo,
                            CliGiro: item.CliGiro,
                            CatCodigo: item.CatCodigo,
                            CatNombre: item.CatNombre,
                            CliPlazoVenta: item.CliPlazoVentaCodigo,
                            GruNombre: item.GruNombre
                        }
                    }))
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            //$("#txtBuscaCliente").html(" ");
            //document.getElementById("txtBuscaCliente").value = " ";
            if (!ui.item.CliBloqueado) {
                if (ui.item.CatCodigo != "F") {
                    document.getElementById("txtNombreCliente").value = ui.item.CliNombre;
                    document.getElementById("txtRutCliente").value = ui.item.CliRut;
                    document.getElementById("txtCodigoCliente").value = ui.item.CliCodigo;
                    document.getElementById("hdnTelefonoCliente").value = ui.item.CliTelefono;
                    document.getElementById("hdnCorreoCliente").value = ui.item.CliCorreo;
                    document.getElementById("hdnGiroCliente").value = ui.item.CliGiro;
                    document.getElementById("txtCategoria").value = ui.item.CatCodigo + " - " + ui.item.CatNombre;
                    document.getElementById("hdnPlazoVenta").value = ui.item.CliPlazoVenta;
                    document.getElementById("hdnGrupoCliente").value = ui.item.GruNombre;
                    CargarTextoLineaCredito();
                    CargarComboVendedorAsociado();
                    CargarComboDirecccionFactura();
                    CargarComboDirecccionDespacho();
                    CargarComboPlazoVenta();
                    CargarCuadroFacturaImpaga();
                    CargarCuadroResumenFacturaImpaga();
                    CargarCuadroChequeProtestado();
                    CargarCuadroResumenChequeProtestado();
                }
                else {
                    alert("Cliente posee categoria F, comuniquese con el Depto. de Creditos y Cobranzas.");
                }
            }
            else {
                alert("Cliente se encuentra bloqueado, no podrá ser seleccionado.");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}
function CargarTextoLineaCredito() {
    var strCliente = document.getElementById("txtCodigoCliente").value;
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
                    document.getElementById("txtAcuerdo").value = $registro.find("CliAcuerdo").text();
                    document.getElementById("txtAutorizado").value = FormatearFloatVista($registro.find("CliAutorizado").text(),0);
                    document.getElementById("txtUtilizado").value = FormatearFloatVista($registro.find("CliUtilizado").text(),0);
                    document.getElementById("txtDisponible").value = FormatearFloatVista($registro.find("CliDisponible").text(),0);
                });
            });
        }
    });
}
function CargarComboMonedaPago() {
    var strParametro = "moneda";
    var intIndex = 0;
    var cmbMonedaPago = document.getElementById("cmbMonedaPago");
    cmbMonedaPago.options.length = 0;
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
                    var ParCodigo = $registro.find("ParNombre").text();
                    var ParNombre = $registro.find("ParNombre").text();
                    cmbMonedaPago.options[intIndex] = new Option(ParNombre, ParCodigo);
                    intIndex++;
                });
            });
        }
    });
}
function BloquearMonedaPago() {
    document.getElementById("cmbMonedaPago").className = "textboxSmallReadonly";
    document.getElementById("cmbMonedaPago").disabled = true;
}
function CargarComboVendedorAsociado() {
    var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);
    var strCliente = document.getElementById("txtCodigoCliente").value;
    var intIndex = 0;
    var cmbVendedor = document.getElementById("cmbVendedor");
    cmbVendedor.options.length = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvCliente.asmx/ObtenerClienteVendedorAsociado",
        dataType: "xml",
        data: { "strCliente": strCliente },
        success: function (xml) {
            $(xml).find("ArrayOfClsCliente").each(function () {
                $(this).find("clsCliente").each(function () {
                    var $registro = $(this);
                    var VenCodigo = $registro.find("VenCodigo").text();
                    var VenNombre = $registro.find("VenNombre").text();
                    cmbVendedor.options[intIndex] = new Option(VenNombre, VenCodigo);
                    intIndex++;
                });
            });
        }
    });
    if (intOperador == 190 || intOperador == 195 || intOperador == 352) {
        cmbVendedor.options[intIndex] = new Option("MAQUINARIAS","116");
    }
}
function CargarTextoOperador() {
    var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);
    var intOficina = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvOperador.asmx/ObtenerOperador",
        dataType: "xml",
        data: { "intOperador": intOperador, "intOficina": intOficina },
        success: function (xml) {
            $(xml).find("ArrayOfClsOperador").each(function () {
                $(this).find("clsOperador").each(function () {
                    var $registro = $(this);
                    document.getElementById("txtOperador").value = $registro.find("OpeNombre").text();
                });
            });
        }
    });
}
function CargarComboSerie() {
    var strObjeto = 17;
    var strSubObjeto = "--";
    var intUsuario = parseInt(parent.document.getElementById("hdnUsuario").value);
    var intIndex = 0;
    var cmbSerie = document.getElementById("cmbSerie");
    cmbSerie.options.length = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvSerie.asmx/ObtenerSerie",
        dataType: "xml",
        data: { "strObjeto": strObjeto, "strSubObjeto": strSubObjeto, "intUsuario": intUsuario },
        success: function (xml) {
            $(xml).find("ArrayOfClsSerie").each(function () {
                $(this).find("clsSerie").each(function () {
                    var $registro = $(this);
                    var SerCodigo = $registro.find("SerCodigo").text();
                    var SerNombre = $registro.find("SerNombre").text();
                    cmbSerie.options[intIndex] = new Option(SerNombre, SerCodigo);
                    intIndex++;
                });
            });
        }
    });
}
function CargarComboTipoDespacho() {
    var intIndex = 0;
    var cmbTipoDespacho = document.getElementById("cmbTipoDespacho");
    cmbTipoDespacho.options.length = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvTipoDespacho.asmx/ObtenerTipoDespacho",
        dataType: "xml",
        data: { "null": "null" },
        success: function (xml) {
            $(xml).find("ArrayOfClsTipoDespacho").each(function () {
                $(this).find("clsTipoDespacho").each(function () {
                    var $registro = $(this);
                    var DesCodigo = $registro.find("DesCodigo").text();
                    var DesNombre = $registro.find("DesNombre").text();
                    cmbTipoDespacho.options[intIndex] = new Option(DesNombre, DesCodigo);
                    intIndex++;
                });
            });
        }
    });
}
function CargarHiddenPeriodoContable() {
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvPeriodoContable.asmx/ObtenerPeriodoContable",
        dataType: "xml",
        data: { "null": "null" },
        success: function (xml) {
            var strRespuesta = $(xml).find('string').text();
            document.getElementById("hdnPeriodoContable").value = strRespuesta;
        }
    });
}
function CargarTextoFechas() {
    var strPeriodosValidos = document.getElementById("hdnPeriodoContable").value;
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
    var strPeriodoActual = intAñoActual + intMesActual;
    if (strPeriodosValidos.indexOf(strPeriodoActual) == -1) {
        var intUltimoDia = 0;
        if (parseInt(intMesActual) == 1) {
            document.getElementById("txtFechaOrdenVenta").value = "31/12/" + (parseInt(intAñoActual) - 1).toString();
        }
        else {
            intUltimoDia = ObtenerUltimoDiaMes(parseInt(intMesActual));
            document.getElementById("txtFechaOrdenVenta").value = intUltimoDia + "/" + strPeriodoActual.substring(6, 4) + "/" + strPeriodoActual.substring(0, 4);
        }
    }
    else {
        document.getElementById("txtFechaOrdenVenta").value = intDiaActual + "/" + intMesActual + "/" + intAñoActual;
    }
    document.getElementById("txtFechaEntregaOrden").value = intDiaActual + "/" + intMesActual + "/" + intAñoActual;
    //document.getElementById("txtFechaEntregaProducto").value = intDiaActual + "/" + intMesActual + "/" + intAñoActual;
}
function ValidarFechaOrdenVenta() {
    if (ValidarFormatoFecha(document.getElementById("txtFechaOrdenVenta").value) == true) {
        var datFechaActual = new Date();
        var datFechaOrdenVenta = new Date();
        var strFechaOrdenVenta = document.getElementById("txtFechaOrdenVenta").value;
        var intDiaOrdenVenta = parseInt(strFechaOrdenVenta.substring(0, 2), 10);
        var intMesOrdenVenta = parseInt(strFechaOrdenVenta.substring(3, 5), 10);
        var intAñoOrdenVenta = parseInt(strFechaOrdenVenta.substring(6), 10);
        datFechaOrdenVenta.setFullYear(intAñoOrdenVenta, intMesOrdenVenta - 1, intDiaOrdenVenta);
        if (datFechaOrdenVenta <= datFechaActual) {
            var strPeriodosValidos = document.getElementById("hdnPeriodoContable").value;
            var strFechaFormateada = FormatearFechaAñoMes(document.getElementById("txtFechaOrdenVenta").value);
            if (strPeriodosValidos.search(strFechaFormateada) < 0) {
                alert("Fecha de orden de venta no está en un periodo activo.");
                document.getElementById("txtFechaOrdenVenta").value = "";
            } else {
                alert("...cambiaTasas()");
                //cambiaTasas();
            }
        } else {
            alert("Fecha de orden de venta debe ser menor o igual a la fecha de hoy.");
            document.getElementById("txtFechaOrdenVenta").value = "";
        }
    }
    else {
        alert("Fecha de orden de venta debe cumplir con el formato dd/mm/yyyy.");
        document.getElementById("txtFechaOrdenVenta").value = "";
    }
}
function ValidarFechaEntregaOrden() {
    if (ValidarFormatoFecha(document.getElementById("txtFechaEntregaOrden").value) == true) {
        var strFechaOrdenVenta = document.getElementById("txtFechaOrdenVenta").value;
        var strFechaEntregaOrden = document.getElementById("txtFechaEntregaOrden").value;
        var datFechaOrdenVenta = parseDate(strFechaOrdenVenta);
        var datFechaEntregaOrden = parseDate(strFechaEntregaOrden);
        if (datFechaOrdenVenta.getTime() > datFechaEntregaOrden.getTime()) {
            alert("Fecha de entrega debe ser igual o mayor a " + strFechaOrdenVenta.substring(0, 2) + "/" + strFechaOrdenVenta.substring(3, 5) + "/" + strFechaOrdenVenta.substring(6, 10));
            document.getElementById("txtFechaEntregaOrden").value = "";
            //document.getElementById("txtFechaEntregaProducto").value = "";
        } else {
            //document.getElementById("txtFechaEntregaProducto").value = strFechaEntregaOrden;
            ActualizarFechaEntregaProducto();
        }
    } else {
        alert("Fecha de entrega debe cumplir con el formato dd/mm/yyyy.");
        document.getElementById("txtFechaEntregaOrden").value = "";
    }
}
function ValidarFechaEntregaProducto() {
    if (ValidarFormatoFecha(document.getElementById("txtFechaEntregaProducto").value) == true) {
        var strFechaEntregaProducto = document.getElementById("txtFechaEntregaProducto").value;
        var datFechaActual = new Date();
        var datFechaEntregaProducto = parseDate(strFechaEntregaProducto);
        datFechaActual.setDate(datFechaActual.getDate() - 1);
        if (datFechaActual.getTime() > datFechaEntregaProducto.getTime()) {
            alert("Fecha de entrega de producto debe ser igual o mayor a la fecha de hoy.");
            document.getElementById("txtFechaEntregaProducto").value = "";
            return false;
        }
    } else {
        alert("Fecha de entrega de producto debe cumplir con el formato dd/mm/yyyy.");
        document.getElementById("txtFechaEntregaProducto").value = "";
        return false;
    }
    return true;
}
function ValidarDatosCabecera() {
    if (document.getElementById("txtCodigoCliente").value == "") {
        alert("Debe buscar datos del cliente para continuar.");
        document.getElementById("txtBuscaCliente").focus();
        return false;
    }
    if (document.getElementById("txtFechaOrdenVenta").value == "") {
        alert("Debe digitar o seleccionar fecha de orden de venta.");
        document.getElementById("txtFechaOrdenVenta").focus();
        return false;
    }
    if (document.getElementById("txtFechaEntregaOrden").value == "") {
        alert("Debe digitar o seleccionar fecha de entrega de orden.");
        document.getElementById("txtFechaEntregaOrden").focus();
        return false;
    }
    //var strMonedaPago = document.getElementById("cmbMonedaPago").options[document.getElementById("cmbMonedaPago").selectedIndex].value;
    //var intVendedor = document.getElementById("cmbVendedor").options[document.getElementById("cmbVendedor").selectedIndex].value;
    //var intSerie = document.getElementById("cmbSerie").options[document.getElementById("cmbSerie").selectedIndex].value;
    var intTipoDespacho = document.getElementById("cmbTipoDespacho").options[document.getElementById("cmbTipoDespacho").selectedIndex].value;
    //alert("strMonedaPago: " + strMonedaPago+", intVendedor: " + intVendedor+", intSerie: " + intSerie+", intTipoDespacho: " + intTipoDespacho);
    if (intTipoDespacho == "0") {
        alert("Debe seleccionar tipo de despacho.");
        document.getElementById("cmbTipoDespacho").focus();
        return false;
    }
    BloquearEmision();
    BloquearMonedaPago();
    Avanzar();
    ActualizarFechaVencimiento();
    CargarComboHibridoProducto();
    RestaurarFleteProducto();
}
function CargarComboDirecccionFactura() {
    var intOpcion = 40;
    var strCliente = document.getElementById("txtCodigoCliente").value;
    var intIndex = 0;
    var cmbDireccionFactura = document.getElementById("cmbDireccionFactura");
    cmbDireccionFactura.options.length = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvCliente.asmx/ObtenerClienteDireccion",
        dataType: "xml",
        data: { "intOpcion": intOpcion, "strCliente": strCliente },
        success: function (xml) {
            $(xml).find("ArrayOfClsCliente").each(function () {
                $(this).find("clsCliente").each(function () {
                    var $registro = $(this);
                    var DirCodigo = $registro.find("DirTipo").text();
                    var DirCompleta = $registro.find("DirCompleta").text();
                    cmbDireccionFactura.options[intIndex] = new Option(DirCompleta, DirCodigo);
                    intIndex++;
                });
            });
        }
    });
}
function CargarComboDirecccionDespacho() {
    var intOpcion = 50;
    var strCliente = document.getElementById("txtCodigoCliente").value;
    var intIndex = 0;
    var cmbDireccionDespacho = document.getElementById("cmbDireccionDespacho");
    cmbDireccionDespacho.options.length = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvCliente.asmx/ObtenerClienteDireccion",
        dataType: "xml",
        data: { "intOpcion": intOpcion, "strCliente": strCliente },
        success: function (xml) {
            $(xml).find("ArrayOfClsCliente").each(function () {
                $(this).find("clsCliente").each(function () {
                    var $registro = $(this);
                    var DirCodigo = $registro.find("DirTipo").text();
                    var DirCompleta = $registro.find("DirCompleta").text();
                    cmbDireccionDespacho.options[intIndex] = new Option(DirCompleta, DirCodigo);
                    intIndex++;
                });
            });
        }
    });
}
function CargarComboPlazoVenta() {
    var intOpcion = 10;
    var strEmision = document.getElementById("cmbEmision").options[document.getElementById("cmbEmision").selectedIndex].value;
    if (strEmision == 'B') { intOpcion = 20;}
    var intPlaCodigo = document.getElementById("hdnPlazoVenta").value;
    var intIndex = 0;
    var cmbPlazoVenta = document.getElementById("cmbPlazoVenta");
    cmbPlazoVenta.options.length = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvPlazoVenta.asmx/ObtenerPlazoVenta",
        dataType: "xml",
        data: { "intOpcion": intOpcion },
        success: function (xml) {
            $(xml).find("ArrayOfClsPlazoVenta").each(function () {
                $(this).find("clsPlazoVenta").each(function () {
                    var $registro = $(this);
                    var PlaCodigo = $registro.find("PlaCodigo").text();
                    var PlaItem = $registro.find("PlaItem").text();
                    var PlaNombre = $registro.find("PlaNombre").text();
                    if (PlaCodigo == intPlaCodigo) {
                        cmbPlazoVenta.options[intIndex] = new Option(PlaNombre, PlaItem, true, true);
                    }
                    else {
                        cmbPlazoVenta.options[intIndex] = new Option(PlaNombre, PlaItem);
                    }
                    intIndex++;
                });
            });
        }
    });
}
function ActualizarFechaVencimiento() {
    CargarTextoFechaVencimiento();
    RestaurarInteresProducto();
}
function CargarComboBodega() {
    var strOperador = parent.document.getElementById("hdnUser").value;
    var intIndex = 0;
    var cmbBodega = document.getElementById("cmbBodega");
    cmbBodega.options.length = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvBodega.asmx/ObtenerBodega",
        dataType: "xml",
        data: { "strOperador": strOperador },
        success: function (xml) {
            $(xml).find("ArrayOfClsBodega").each(function () {
                $(this).find("clsBodega").each(function () {
                    var $registro = $(this);
                    var BodCodigo = $registro.find("BodCodigo").text();
                    var BodNombre = $registro.find("BodCodigo").text();
                    var BodDefecto = $registro.find("BodDefecto").text();
                    var BodTipoVenta = $registro.find("BodTipoVenta").text();
                    if (BodTipoVenta == "VentaBodegaPropia") {
                        if (BodDefecto == "Y") {
                            cmbBodega.options[intIndex] = new Option(BodNombre, BodCodigo, true, true);
                        }
                        else {
                            cmbBodega.options[intIndex] = new Option(BodNombre, BodCodigo);
                        }
                        intIndex++;
                    }
                });
                //cmbBodega.options[intIndex] = new Option("00", "00");
            });
        }
    });
}
function ActualizarBodegaProducto() {
    CargarTextoProducto();
}
function CargarTextoProducto() {
    var intOpcion = 10;
    var strBodega = document.getElementById("cmbBodega").options[document.getElementById("cmbBodega").selectedIndex].value;
    $("#txtBuscaProducto").autocomplete({
        max: 10,
        source: function (request, response) {
            $.ajax({
                type: "post",
                async: true,
                contentType: "application/json; charset=utf-8",
                url: "/Services/srvProducto.asmx/ObtenerProducto",
                dataType: "json",
                data: "{ 'intOpcion':'" + intOpcion + "', 'strProducto':'" + request.term + "', 'strBodega':'" + strBodega + "'}",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.ArtCodigo + ' ' + item.ArtNombre + ' ' + '(F:' + item.ArtStockFisico + ' - D:' + item.ArtStockDisponible + ")",
                          //value: '',
                            ArtCodigo: item.ArtCodigo,
                            ArtNombre: item.ArtNombre,
                            ArtEnvase: item.ArtEnvase,
                            GruCodigo: item.GruCodigo,
                            GruNombre: item.GruNombre,
                            ProNombre: item.ProNombre,
                            ArtMoneda: item.ArtMoneda,
                            ArtPrecioUnitario: item.ArtPrecioUnitario,
                            ArtCostoComercial: item.ArtCostoComercial,
                            ArtStockFisico: item.ArtStockFisico,
                            ArtStockDisponible: item.ArtStockDisponible,
                            ArtInventariable: item.ArtInventariable,
                            PorFlete: item.PorFlete,
                            ArtCostoReposicion: item.ArtCostoReposicion
                        }
                    }))
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            $("#txtBuscaProducto").html(" ");
            var strMonedaPago = document.getElementById("cmbMonedaPago").options[document.getElementById("cmbMonedaPago").selectedIndex].value;
            var strMonedaProducto = ui.item.ArtMoneda;
            if ((strMonedaPago=='USD' || strMonedaPago=='EUR') && (strMonedaPago!=strMonedaProducto)){
                alert("No puede vender producto con moneda extranjera distinta a moneda de pago del cliente.");
            }
            else{
                document.getElementById("txtNombreProveedor").value = ui.item.ProNombre;
                document.getElementById("txtNombreGrupo").value = ui.item.GruNombre;
                document.getElementById("txtEnvaseProducto").value = ui.item.ArtEnvase;
                document.getElementById("txtStockFisico").value = ui.item.ArtStockFisico;
                document.getElementById("txtStockDisponible").value = ui.item.ArtStockDisponible;
                document.getElementById("txtCodigoProducto").value = ui.item.ArtCodigo;
                document.getElementById("txtNombreProducto").value = ui.item.ArtNombre;
                document.getElementById("txtMonedaProducto").value = ui.item.ArtMoneda;
                document.getElementById("txtPrecioUnitarioProducto").value = ui.item.ArtPrecioUnitario.toFixed(6);
                document.getElementById("hdnBodegaProducto").value = strBodega;
                document.getElementById("hdnPrecioUnitarioRealProducto").value = ui.item.ArtPrecioUnitario.toFixed(2);
                document.getElementById("hdnCostoComercialProducto").value = ui.item.ArtCostoComercial.toFixed(2);
                document.getElementById("hdnInventariableProducto").value = ui.item.ArtInventariable;

                document.getElementById("hdnPorcentajeFlete").value = ui.item.PorFlete.toFixed(2);

                var strGruCodigo = ui.item.GruCodigo;
                var dblCostoReposicionProducto = 0.00;
                if (strGruCodigo == '102' || strGruCodigo == '117') {
                    document.getElementById("hdnCostoReposicionProducto").value = ui.item.ArtCostoReposicion.toFixed(2);
                }
                else {
                    document.getElementById("hdnCostoReposicionProducto").value = dblCostoReposicionProducto.toFixed(2);
                }

                CargarComboHibridoProducto();
                CargarCuadroInventario();

                var intDespacho = document.getElementById("cmbTipoDespacho").options[document.getElementById("cmbTipoDespacho").selectedIndex].value;
                if (intDespacho == "3" || intDespacho == "5") {
                    if (confirm("¿Entrega al cliente precio unitario de venta con flete incluido?")) {
                        CambiarIncluyeFlete(1);
                    }
                    else {
                        CambiarIncluyeFlete(0);
                    }
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}
function CargarComboHibridoProducto() {
    var strProducto = document.getElementById("txtCodigoProducto").value;
    var intEntro = 0;
    var intIndex = 0;
    var cmbHibridoProducto = document.getElementById("cmbHibridoProducto");
    cmbHibridoProducto.options.length = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvHibridoProducto.asmx/ObtenerHibridoProducto",
        dataType: "xml",
        data: { "strProducto": strProducto },
        success: function (xml) {
            $(xml).find("ArrayOfClsHibridoProducto").each(function () {
                $(this).find("clsHibridoProducto").each(function () {
                    intEntro = 1;
                    var $registro = $(this);
                    var HibCodigo = $registro.find("HibCodigo").text();
                    var HibNombre = $registro.find("HibNombre").text();
                    cmbHibridoProducto.options[intIndex] = new Option(HibNombre, HibCodigo);
                    intIndex++;
                });
            });
            if (intEntro == 0) { cmbHibridoProducto.options[intIndex] = new Option("NO TIENE", ""); }
        }
    });
}
function CargarCuadroInventario() {
    var strProducto = document.getElementById("txtCodigoProducto").value;
    var intTotalStockFisico = 0;
    var intTotalStockPedido = 0;
    var intTotalStockOrdenado = 0;
    var intTotalStockDisponible = 0;
    var strTabla = "";
    strTabla = '<table id="tblInventario" border="0px">';
    strTabla += '<tr class="trTituloCabecera">';
    strTabla += '<td class="tdLeft" colspan="2">Proveedor</td>';
    strTabla += '<td class="tdLeft" colspan="2">Grupo</td>';
    strTabla += '<td class="tdLeft" colspan="2">Envase</td>';
    strTabla += '</tr>';
    strTabla += '<tr class="trTituloDetalle">';
    strTabla += '<td class="tdLeft" colspan="2">' + document.getElementById("txtNombreProveedor").value + '</td>';
    strTabla += '<td class="tdLeft" colspan="2">' + document.getElementById("txtNombreGrupo").value + '</td>';
    strTabla += '<td class="tdLeft" colspan="2">' + document.getElementById("txtEnvaseProducto").value + '</td>';
    strTabla += '</tr>';
    strTabla += '<tr class="trTituloCabecera">';
    strTabla += '<td class="tdLeft"  style="width:100px;">Codigo</td>';
    strTabla += '<td class="tdRight" style="width:220px;">Bodega</td>';
    strTabla += '<td class="tdRight" style="width:70px;">Fisico#</td>';
    strTabla += '<td class="tdRight" style="width:70px;">Comp.#</td>';
    strTabla += '<td class="tdRight" style="width:70px;">Orden#</td>';
    strTabla += '<td class="tdRight" style="width:70px;">Disp.#</td>';
    strTabla += '</tr>';
    var intEntro = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvInventario.asmx/ObtenerInventario",
        dataType: "xml",
        data: { "strProducto": strProducto },
        success: function (xml) {
            $(xml).find("ArrayOfClsInventario").each(function () {
                $(this).find("clsInventario").each(function () {
                    intEntro = 1;
                    var $registro = $(this);
                    var strBodCodigo = $registro.find("BodCodigo").text();
                    var strBodNombre = $registro.find("BodNombre").text();
                    var intArtStockFisico = $registro.find("ArtStockFisico").text();
                    var intArtStockPedido = $registro.find("ArtStockPedido").text();
                    var intArtStockOrdenado = $registro.find("ArtStockOrdenado").text();
                    var intArtStockDisponible = $registro.find("ArtStockDisponible").text();
                    intTotalStockFisico += parseInt(intArtStockFisico);
                    intTotalStockPedido += parseInt(intArtStockPedido);
                    intTotalStockOrdenado += parseInt(intArtStockOrdenado);
                    intTotalStockDisponible += parseInt(intArtStockDisponible);
                    strTabla += '<tr class="trTituloDetalle">';
                    strTabla += '<td class="tdLeft">' + strBodCodigo + '</td>';
                    strTabla += '<td class="tdRight">' + strBodNombre + '</td>';
                    strTabla += '<td class="tdRight">' + intArtStockFisico + '</td>';
                    strTabla += '<td class="tdRight">' + intArtStockPedido + '</td>';
                    strTabla += '<td class="tdRight">' + intArtStockOrdenado + '</td>';
                    strTabla += '<td class="tdRight">' + intArtStockDisponible + '</td>';
                    strTabla += '</tr>';
                });
            });
            if (intEntro == 0) {
                strTabla += "<tr><td colspan='6'>No hay informacion de inventario asociado al producto...</td></tr>";
            }
            else {
                strTabla += '<tr><td colspan="6"><hr></td></tr>';
                strTabla += '<tr class="trTituloDetalle">';
                strTabla += '<td class="tdLeft"></td>';
                strTabla += '<td class="tdRight">Totales</td>';
                strTabla += '<td class="tdRight">' + intTotalStockFisico + '</td>';
                strTabla += '<td class="tdRight">' + intTotalStockPedido + '</td>';
                strTabla += '<td class="tdRight">' + intTotalStockOrdenado + '</td>';
                strTabla += '<td class="tdRight">' + intTotalStockDisponible + '</td>';
                strTabla += '</tr>';
            }
        }
    });
    strTabla += '</table>';
    $("#divInventario").html(strTabla);
}
function EstablecerRangoFechaVencimiento() {
    var strPlazoVenta = document.getElementById("cmbPlazoVenta").options[document.getElementById("cmbPlazoVenta").selectedIndex].value;
    var arrDiasPlazoVenta = strPlazoVenta.split("_");
    var intDiasPlazoVenta = arrDiasPlazoVenta[1];
    if (intDiasPlazoVenta == 0) {
        var intMinimo = parseInt(intDiasPlazoVenta, 10) - 0;
        var intMaximo = parseInt(intDiasPlazoVenta, 10) + 0;
    }
    else {
        var intMinimo = parseInt(intDiasPlazoVenta, 10) - 15;
        var intMaximo = parseInt(intDiasPlazoVenta, 10) + 15;
    }
    return { minDate: intMinimo + "d", maxDate: intMaximo + "d", defaultDate: +intDiasPlazoVenta };
}
function CargarTextoFechaVencimiento() {
    var strPlazoVenta = document.getElementById("cmbPlazoVenta").options[document.getElementById("cmbPlazoVenta").selectedIndex].value;
    var strFechaOrdenVenta = document.getElementById("txtFechaOrdenVenta").value;
    var arrDiasPlazoVenta = strPlazoVenta.split("_");
    var intDiasPlazoVenta = arrDiasPlazoVenta[1];
    var arrFechaOrdenVenta = strFechaOrdenVenta.split("/");
    var datFechaOrdenVenta = new Date(arrFechaOrdenVenta[2], arrFechaOrdenVenta[1] - 1, arrFechaOrdenVenta[0]);
    var datFechaVencimiento = new Date(datFechaOrdenVenta.getTime() + (intDiasPlazoVenta * 24 * 3600 * 1000));
    var intDiaVencimiento = datFechaVencimiento.getDate();
    var intMesVencimiento = datFechaVencimiento.getMonth() + 1;
    var intAñoVencimiento = datFechaVencimiento.getFullYear();
    if (intDiaVencimiento < 10) {
        intDiaVencimiento = "0" + intDiaVencimiento;
    }
    if (intMesVencimiento < 10) {
        intMesVencimiento = "0" + intMesVencimiento;
    }
    document.getElementById("txtFechaVencimiento").value = intDiaVencimiento + "/" + intMesVencimiento + "/" + intAñoVencimiento;
}
function ValidarLineaProducto() {
    if (document.getElementById("hdnIncluyeFlete").value == "1") {
        if (document.getElementById("txtPrecioUnitarioProductoAuxiliar").value == "") {
            alert("Debe digitar precio unitario del producto con flete incluido.");
            document.getElementById("txtPrecioUnitarioProductoAuxiliar").focus();
            return false;
        }
    }
    if (document.getElementById("txtCodigoProducto").value == "") {
        alert("Debe buscar datos del producto para continuar.");
        document.getElementById("txtBuscaProducto").focus();
        return false;
    }
    if (document.getElementById("txtCantidadProducto").value == "") {
        alert("Debe digitar cantidad de producto(s) a vender.");
        document.getElementById("txtCantidadProducto").focus();
        return false;
    }
    if (document.getElementById("hdnInventariableProducto").value == 1) {
        if (parseInt(document.getElementById("txtStockDisponible").value) <= 0) {
            alert("No existe stock disponible en bodega.");
            LimpiarLineaProducto();
            document.getElementById("txtBuscaProducto").focus();
            return false;
        }
        if (parseInt(document.getElementById("txtCantidadProducto").value) > parseInt(document.getElementById("txtStockDisponible").value)) {
            alert("Cantidad de unidades del producto es superior a stock disponible en bodega.");
            document.getElementById("txtCantidadProducto").focus();
            return false;
        }
    }
    if (document.getElementById("txtPrecioUnitarioProducto").value == "") {
        alert("Debe digitar precio unitario de venta del producto o servicio.");
        document.getElementById("txtPrecioUnitarioProducto").focus();
        return false;
    }
    if (parseFloat(document.getElementById("txtPrecioUnitarioProducto").value) <= 0) {
        alert("Debe digitar precio unitario de venta valido del producto o servicio.");
        document.getElementById("txtPrecioUnitarioProducto").focus();
        return false;
    }
    if (document.getElementById("txtFechaEntregaProducto").value == "") {
        alert("Debe seleccionar fecha de entrega de producto.");
        document.getElementById("txtFechaEntregaProducto").focus();
        return false;
    }
    var dblTipoCambio = 1.00;
    var intCantidadProducto = 0;
    var dblPrecioUnitarioProducto = 0.000000;
    var intTotalUnitarioProducto = 0;
    var dblPrecioUnitarioRealProducto = 0.00;
    var dblDescuentoProducto = 0.00;
    var dblCostoComercialProducto = 0.00;
    var dblMargenComercialProducto = 0.00;
    var intInteresProducto = 0;
    var intFleteProducto = 0;
    var intPrecioFinalProducto = 0;
    var dblPrecioUnitarioProductoAuxiliar = 0.000000;
    var dblPorcentajeFlete = 0.00;
    if (document.getElementById("txtMonedaProducto").value == "USD")
        dblTipoCambio = parseFloat(parent.document.getElementById("hdnDolar").value);
    else if (document.getElementById("txtMonedaProducto").value == "EUR") {
        dblTipoCambio = parseFloat(parent.document.getElementById("hdnEuro").value);
    }

       
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    if (document.getElementById("hdnIncluyeFlete").value == "0") {
        intCantidadProducto = parseInt(document.getElementById("txtCantidadProducto").value);
        dblPrecioUnitarioProducto = (parseFloat(document.getElementById("txtPrecioUnitarioProducto").value)).toFixed(6);
    }
    else {
        dblPrecioUnitarioProductoAuxiliar = (parseFloat(document.getElementById("txtPrecioUnitarioProductoAuxiliar").value));
        dblPorcentajeFlete = (parseFloat(document.getElementById("hdnPorcentajeFlete").value)) / 100;

        intCantidadProducto = parseInt(document.getElementById("txtCantidadProducto").value);
        dblPrecioUnitarioProducto = (dblPrecioUnitarioProductoAuxiliar / (1.0000 + dblPorcentajeFlete)).toFixed(6);
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------//


    document.getElementById("txtPrecioUnitarioProducto").value = dblPrecioUnitarioProducto;
    intTotalUnitarioProducto = ((intCantidadProducto * dblPrecioUnitarioProducto) * dblTipoCambio).toFixed(0);
    document.getElementById("txtTotalUnitarioProducto").value = intTotalUnitarioProducto;


    dblPrecioUnitarioRealProducto = (parseFloat(document.getElementById("hdnPrecioUnitarioRealProducto").value)).toFixed(2);
    if (dblPrecioUnitarioRealProducto != 0.00) {
        dblDescuentoProducto = (100 * (1 - intTotalUnitarioProducto / (dblPrecioUnitarioRealProducto * intCantidadProducto * dblTipoCambio))).toFixed(2);
    }
    document.getElementById("txtDescuentoProducto").value = (parseFloat(dblDescuentoProducto)).toFixed(2);


    dblCostoComercialProducto = (parseFloat(document.getElementById("hdnCostoComercialProducto").value)).toFixed(2);
    document.getElementById("hdnCostoComercialProducto").value = dblCostoComercialProducto;


    dblMargenComercialProducto = (100 - ((dblCostoComercialProducto * 100) / dblPrecioUnitarioProducto)).toFixed(2);
    document.getElementById("txtMargenComercialProducto").value = dblMargenComercialProducto;


    document.getElementById("txtInteresProducto").value = 0;
    CargarTextoInteresProducto();
    intInteresProducto = parseInt(document.getElementById("txtInteresProducto").value);


    document.getElementById("hdnDescuentoProducto").value = 0;


    document.getElementById("txtFleteProducto").value = 0;
    var intTipoDespacho = document.getElementById("cmbTipoDespacho").options[document.getElementById("cmbTipoDespacho").selectedIndex].value;
    if (intTipoDespacho == 3 || intTipoDespacho == 5) {
        CargarTextoFleteProducto();
    }
    intFleteProducto = parseInt(document.getElementById("txtFleteProducto").value);


    intPrecioFinalProducto = parseInt(intTotalUnitarioProducto) + parseInt(intInteresProducto) + parseInt(intFleteProducto);
    document.getElementById("txtPrecioFinalProducto").value = intPrecioFinalProducto;


    //if (confirm("¿Desea agregar pedido?")) {
    //    document.getElementById("hdnBodegaProducto").value = document.getElementById("cmbBodega").options[document.getElementById("cmbBodega").selectedIndex].value;
    //    document.getElementById("hdnHibridoProducto").value = document.getElementById("cmbHibridoProducto").options[document.getElementById("cmbHibridoProducto").selectedIndex].value;
    //    AgregarLineaProducto();
    //    LimpiarLineaProducto();
    //}
    return true;
}
function ConfirmarVentaProducto() {
    if (confirm("¿Desea agregar pedido?")) {
        document.getElementById("hdnBodegaProducto").value = document.getElementById("cmbBodega").options[document.getElementById("cmbBodega").selectedIndex].value;
        document.getElementById("hdnHibridoProducto").value = document.getElementById("cmbHibridoProducto").options[document.getElementById("cmbHibridoProducto").selectedIndex].value;
        AgregarLineaProducto();
        LimpiarLineaProducto();
    }
}
function CargarTextoInteresProducto() {
    var intOpcion = 10;
    var strCliente = document.getElementById("txtCodigoCliente").value;
    var strBodega = document.getElementById("cmbBodega").options[document.getElementById("cmbBodega").selectedIndex].value;
    var strProducto = document.getElementById("txtCodigoProducto").value;
    var strPlazoVenta = document.getElementById("cmbPlazoVenta").options[document.getElementById("cmbPlazoVenta").selectedIndex].value;
    var arrPlazoVenta = strPlazoVenta.split("_");
    var intPlazoVenta = arrPlazoVenta[0];
    var intTotalUnitarioProducto = document.getElementById("txtTotalUnitarioProducto").value;
    var fltTasaInteres = 0.0;
    var strFechaVencimiento = "";
    var strFechaCompra = "";
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
                    document.getElementById("txtInteresProducto").value = $registro.find("ArtInteres").text();
                });
            });
        }
    });
}
function CargarTextoFleteProducto() {
    var strBodega = document.getElementById("cmbBodega").options[document.getElementById("cmbBodega").selectedIndex].value;
    var strProducto = document.getElementById("txtCodigoProducto").value;
    var intCantidadProducto = parseInt(document.getElementById("txtCantidadProducto").value);
    var intTotalUnitarioProducto = document.getElementById("txtTotalUnitarioProducto").value;
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
                    document.getElementById("txtFleteProducto").value = $registro.find("ArtFlete").text();
                });
            });
        }
    });
}
function LimpiarLineaProducto() {
    document.getElementById("txtBuscaProducto").value = "";
    document.getElementById("txtNombreProveedor").value = "";
    document.getElementById("txtNombreGrupo").value = "";
    document.getElementById("txtEnvaseProducto").value = "";
    document.getElementById("txtStockFisico").value = "";
    document.getElementById("txtStockDisponible").value = "";
    document.getElementById("txtCodigoProducto").value = "";
    document.getElementById("txtNombreProducto").value = "";
    document.getElementById("txtCantidadProducto").value = "";
    document.getElementById("txtMonedaProducto").value = "";
    document.getElementById("txtPrecioUnitarioProducto").value = "";
    document.getElementById("txtTotalUnitarioProducto").value = "";
    document.getElementById("txtDescuentoProducto").value = "";
    document.getElementById("txtMargenComercialProducto").value = "";
    document.getElementById("txtInteresProducto").value = "";
    document.getElementById("hdnDescuentoProducto").value = "";
    document.getElementById("txtFleteProducto").value = "";
    document.getElementById("txtFechaEntregaProducto").value = "";
    document.getElementById("txtPrecioFinalProducto").value = "";
    document.getElementById("hdnPrecioUnitarioRealProducto").value = "";
    document.getElementById("hdnCostoComercialProducto").value = "";
    document.getElementById("hdnCostoReposicionProducto").value = "";
    document.getElementById("hdnInventariableProducto").value = "";
    CargarComboHibridoProducto();
    $("#divInventario").html("");

    document.getElementById("hdnPorcentajeFlete").value = "";
    CambiarIncluyeFlete(0);
}
function CambiarIncluyeFlete(intIncluyeFlete) {
    document.getElementById("txtMonedaProductoAuxiliar").value = "";
    document.getElementById("txtPrecioUnitarioProductoAuxiliar").value = "";
    document.getElementById("hdnIncluyeFlete").value = intIncluyeFlete;
    if (intIncluyeFlete == 0) {
        document.getElementById("txtPrecioUnitarioProductoAuxiliar").className = "textboxSmallReadonly";
        document.getElementById("txtPrecioUnitarioProductoAuxiliar").readOnly = true;

        document.getElementById("txtPrecioUnitarioProducto").className = "textboxSmall";
        document.getElementById("txtPrecioUnitarioProducto").readOnly = false;
    }
    else {
        document.getElementById("txtMonedaProductoAuxiliar").value = document.getElementById("txtMonedaProducto").value;

        document.getElementById("txtPrecioUnitarioProductoAuxiliar").className = "textboxSmall";
        document.getElementById("txtPrecioUnitarioProductoAuxiliar").readOnly = false;

        document.getElementById("txtPrecioUnitarioProducto").className = "textboxSmallReadonly";
        document.getElementById("txtPrecioUnitarioProducto").readOnly = true;
    }
}
function ValidarVencimientoPedido() {
    var blnRetorno = false;
    var strMonedaPago = document.getElementById("cmbMonedaPago").options[document.getElementById("cmbMonedaPago").selectedIndex].value;
    var strFechaVencimiento = document.getElementById("txtFechaVencimiento").value;
    var strPlazoVenta = document.getElementById("cmbPlazoVenta").options[document.getElementById("cmbPlazoVenta").selectedIndex].value;
    var arrDiasPlazoVenta = strPlazoVenta.split("_");
    intDiasPlazoVenta = arrDiasPlazoVenta[1];
    var intMinimo = parseInt(intDiasPlazoVenta, 10) - 30;
    var intMaximo = parseInt(intDiasPlazoVenta, 10) + 30;
    var strFechaOrdenVenta = document.getElementById("txtFechaOrdenVenta").value;
    var arrFechaOrdenVenta = strFechaOrdenVenta.split("/");
    var datFechaOrdenVenta = new Date(arrFechaOrdenVenta[2] + "-" + arrFechaOrdenVenta[1] + "-" + arrFechaOrdenVenta[0]);
    var datFechaInicio = new Date(datFechaOrdenVenta.getTime() + (intMinimo * 24 * 3600 * 1000));
    datFechaInicio = new Date(datFechaInicio.getFullYear(), datFechaInicio.getMonth(), datFechaInicio.getDate());
    var datFechaFinal = new Date(datFechaOrdenVenta.getTime() + (intMaximo * 24 * 3600 * 1000));
    datFechaFinal = new Date(datFechaFinal.getFullYear(), datFechaFinal.getMonth(), datFechaFinal.getDate());
    var arrFechaVencimiento = strFechaVencimiento.split("/");
    var datFechaVencimiento = new Date(arrFechaVencimiento[2], arrFechaVencimiento[1] - 1, arrFechaVencimiento[0]);
    var strFechaOrdenVentaBase = document.getElementById("txtFechaOrdenVenta").value;
    var arrFechaOrdenVentaBase = strFechaOrdenVentaBase.split("/");
    var datFechaOrdenVentaBase = new Date(arrFechaOrdenVentaBase[2] + "-" + arrFechaOrdenVentaBase[1] + "-" + arrFechaOrdenVentaBase[0]);
    var datFechaMinima = new Date(datFechaOrdenVentaBase.getTime() + (30 * 24 * 3600 * 1000));
    datFechaMinima = new Date(datFechaMinima.getFullYear(), datFechaMinima.getMonth(), datFechaMinima.getDate());
    if (strMonedaPago == "CLP") {
        if (datFechaInicio.getTime() < datFechaVencimiento.getTime()) {
            if (datFechaVencimiento.getTime() <= datFechaFinal.getTime()) {
                blnRetorno = true;
            }
            else {
                alert("Fecha de vencimiento se encuentra fuera de rango maximo permitido.");
            }
        }
        else {
            alert("La fecha de vencimiento se encuentra fuera de rango minimo permitido.");
        }
    }
    else {
        if (datFechaMinima.getTime() < datFechaVencimiento.getTime()) {
            if (datFechaVencimiento.getTime() <= datFechaFinal.getTime()) {
                blnRetorno = true;
            }
            else {
                alert("Fecha de vencimiento se encuentra fuera de rango maximo permitido.");
            }
        }
        else {
            alert("Fecha de vencimiento no puede ser inferior a 30 dias cuando el cliente cancela en moneda extranjera.");
        }
    }
    return blnRetorno;
}
function CargarCuadroAutorizacion() {
    var strAcuerdo = (document.getElementById("txtAcuerdo").value).substring(0, 2);
    var strDesde = ObtenerFechaActual();
    var strHasta = document.getElementById("txtFechaVencimiento").value;
    var intDiferenciaDias = ObtenerDiferenciaDias(strDesde, strHasta);
    if (strAcuerdo == "NO" && intDiferenciaDias > 30) {
        alert("Cliente no esta sujeto al acuerdo de plazo inscrito por ventas superiores a 30 dias.");
        return false;
    }
    document.getElementById("trAutorizaciones").style.visibility = "hidden";
    $('#divAutorizacion').html("");
    document.getElementById("btnCargarPedido").value = "Crear Orden";
    document.getElementById("btnCargarPedido").style.visibility = "hidden";
    if (ValidarVencimientoPedido()) {
        Avanzar2();
        if (tablaHtml.arrFilas.length > 0) {
            var tblModal = '<table style="float:left;width:600px;" border="1px"><tr class="trTituloCabecera"><td>Autorizador</td><td>Cargo</td><td>Tipo</td><td>BackUp</td></tr>';
            var blnConAutorizacion = false;
            //var blnFlagPaso = false;
            //var strMensajeAutorizacion = '<div style="text-align: center; font-size: 15px; color: red;">Esta venta va a necesitar autorizacion por: <br/>';
            if (VerificarAutorizacionPorProducto()) {
                blnConAutorizacion = true;
                //if (tablaHtml.porCosto == true) {
                //    strMensajeAutorizacion += "- Margen ha superado nivel de aprobación de Jefe Oficina.<br/>";
                //} else if (tablaHtml.porMargen == true) {
                //    strMensajeAutorizacion += "- Uno o más productos ha superado el margen minimo.<br/>";
                //}
            }
            if (VerificarAutorizacionPorTasa()) {
                blnConAutorizacion = true;
            }
            if (VerificarAutorizacionPorCredito()) {
                blnConAutorizacion = true;
                //strMensajeAutorizacion += "- Monto total supera el crédito disponible.<br/>";
                //tblModal += ObtenerAutorizacionEspecial("credito");
                var strDatos = ObtenerAutorizacionEspecial("credito");
                var arrDatos = strDatos.split("</tr>");
                tblModal += arrDatos[0];
                tablaHtml.porCredito = arrDatos[1];
            }
            if (VerificarAutorizacionPorDeuda()) {
                blnConAutorizacion = true;
                //strMensajeAutorizacion += "- Monto total supera el crédito disponible.<br/>";
                //tblModal += ObtenerAutorizacionEspecial("deuda");
                var strDatos = ObtenerAutorizacionEspecial("deuda");
                var arrDatos = strDatos.split("</tr>");
                tblModal += arrDatos[0];
                tablaHtml.porDeuda = arrDatos[1];
            }
            if (VerificarAutorizacionPorProtesto()) {
                blnConAutorizacion = true;
                //strMensajeAutorizacion += "- Documentos protestados.<br/>";
                //tblModal += ObtenerAutorizacionEspecial("protesto");
                var strDatos = ObtenerAutorizacionEspecial("protesto");
                var arrDatos = strDatos.split("</tr>");
                tblModal += arrDatos[0];
                tablaHtml.porProtesto = arrDatos[1];
            }
            if (blnConAutorizacion == true) {
                //strMensajeAutorizacion += "</div>";
                //blnFlagPaso = true;
                //$('#divMensaje').html(strMensajeAutorizacion);
                var lineasArray = tablaHtml.arrFilas;
                var array_aut = new Array();
                var array_asc = new Array();
                for (var i = 0; i < lineasArray.length; i++) {
                    var lineaProducto = lineasArray[i];
                    var array_autoriza = lineaProducto.autoriza_mod.split("|");
                    for (x = 1; x <= array_autoriza.length - 1; x++) {
                        var array_tipo = array_autoriza[x].split("&");
                        var aut = array_tipo[0];
                        var index = array_aut.indexOf(aut);
                        if (index == -1) {
                            var array_temp = new Array(4)
                            array_aut.push(array_tipo[0]);
                            array_temp[0] = array_tipo[0];
                            array_temp[1] = array_tipo[1];
                            array_temp[2] = array_tipo[2];
                            array_temp[4] = array_tipo[3];
                            switch (array_tipo[2]) {
                                case "Bajo Margen 1":
                                    array_temp[3] = 1;
                                    break;
                                case "Bajo Margen 2":
                                    array_temp[3] = 2;
                                    break;
                                case "Bajo Margen 3":
                                    array_temp[3] = 3;
                                    break;
                                case "Bajo Costo 1":
                                    array_temp[3] = 4;
                                    break;
                                case "Bajo Costo 2":
                                    array_temp[3] = 5;
                                    break;
                                case "Bajo Costo 3":
                                    array_temp[3] = 6;
                                    break;
                            }
                            array_asc.push(array_temp);
                        }
                    }
                }
                array_asc.sort(function (a, b) {
                    var a1 = a[3], b1 = b[3];
                    if (a1 == b1) return 0;
                    return a1 > b1 ? 1 : -1;
                });
                for (y = 0; y < array_asc.length; y++) {
                    var var_tipo = "";
                    if (array_asc[y][3] >= 1 && array_asc[y][3] <= 6) {
                        var_tipo = "VENTAS";
                    } else {
                        var_tipo = array_asc[y][2];
                    }
                    tblModal += "<tr><td>" + array_asc[y][0] + "</td><td>" + array_asc[y][1] + "</td><td>" + var_tipo + "</td><td>" + array_asc[y][4] + " .</td></tr>"
                }
                tblModal += "</table>";
                document.getElementById("trAutorizaciones").style.visibility = "visible";
                $('#divAutorizacion').html(tblModal);
                document.getElementById("btnCargarPedido").value = "Crear Borrador";
            }
            document.getElementById("btnCargarPedido").style.visibility = "visible";
        }
    }
}
function ObtenerAutorizacionEspecial(strTipo) {
    var strOperador = parent.document.getElementById("hdnUser").value;
    var strRetorno = "";
    $.ajax({
        type: "POST",
        url: "/Services/srvAutorizacion.asmx/ObtenerAutorizacionEspecial",
        dataType: "xml",
        data: { "strOperador": strOperador, "strTipo": strTipo },
        async: false,
        success: function (data) {
            strRetorno = $(data).find("string").text();
        }
    });
    return strRetorno;
}
function VerificarAutorizacionPorProducto() {
    tablaHtml.porCosto = false;
    tablaHtml.porMargen = false;
    $("#hdnAutorizacionProducto").val("");
    var lineasArray = tablaHtml.arrFilas;
    for (var i = 0; i < lineasArray.length; i++) {
        var lineaProducto = lineasArray[i];
        var intTipoAutorizacion = parseInt(lineaProducto.auxTipoAutorizacion, 10);
        if (intTipoAutorizacion == 1) {
            $("#hdnAutorizacionProducto").val("true");
            tablaHtml.porMargen = true;
        } else if (intTipoAutorizacion == 2) {
            $("#hdnAutorizacionProducto").val("true");
            tablaHtml.porCosto = true;
        }
    }
    if (tablaHtml.porMargen == true || tablaHtml.porCosto == true) {
        return true;
    }
    return false;
}
function VerificarAutorizacionPorTasa() {
    tablaHtml.porTasa = false;
    $("#hdnAutorizacionTasa").val("false");
    return false;
}
function VerificarAutorizacionPorCredito() {
    tablaHtml.porCredito = "";
    $("#hdnAutorizacionCredito").val("");
    //var intTotalMonto = 0.0;
    //var intCreditoDisponible = parseInt(document.getElementById("txtDisponible").value);
    //var strPlazoVenta = document.getElementById("cmbPlazoVenta").options[document.getElementById("cmbPlazoVenta").selectedIndex].value;
    //var arrDiasPlazoVenta = strPlazoVenta.split("_");
    //intDiasPlazoVenta = arrDiasPlazoVenta[1];
    //if (parseInt(intDiasPlazoVenta) == 0) {
    //    return false;
    //}
    //for (var i = 0; i < tablaHtml.arrFilas.length; i++) {
    //    var lineaProducto = tablaHtml.arrFilas[i];
    //    intTotalMonto += parseInt(lineaProducto.txtTotalUnitarioProducto) + parseInt(lineaProducto.txtInteresProducto);
    //}
    //intTotalMonto = intTotalMonto * 1.19;
    //if (parseInt(intTotalMonto) > parseInt(intCreditoDisponible)) {
    //    $("#hdnAutorizacionCredito").val("true");
    //    //tablaHtml.porCredito = true;
    //    return true;
    //}
    //else {
    //    return false;
    //}
    return false;
}
function VerificarAutorizacionPorDeuda() {
    tablaHtml.porDeuda = "";
    var deuda = $("#hdnAutorizacionDeuda").val();
    if (deuda == "true") {
        //tablaHtml.porDeuda = true;
        return true;
    }
    return false;
}
function VerificarAutorizacionPorProtesto() {
    tablaHtml.porProtesto = "";
    var protesto = $("#hdnAutorizacionProtesto").val();
    if (protesto == "true") {
        //tablaHtml.porProtesto = true;
        return true;
    }
    return false;
}
function CargarPedido() {
    $("#hdnTipo").val("");
    $("#hdnCodigo").val("");
    //document.getElementById("txtComentario1").value = JSON.stringify(tablaHtml);
    document.getElementById("btnCargarPedido").style.visibility = "hidden";
    if (document.getElementById("btnCargarPedido").value == "Crear Borrador") {
        RegistrarBorradorVenta();
        //document.getElementById("divLoader2").style.display = "block";
        //var intDocEntry = RegistrarBorradorVenta();
        //document.getElementById("divLoader2").style.display = "none";
        //if (intDocEntry!=0) {
        //    EnviarAutorizacionCorreo(intDocEntry);
        //    //alert("Se han enviado correos por autorizaciones.");
        //    $("#divCrearPedido").html("Se ha generado el borrador de venta N° " + intDocEntry + " y se ha(n) enviado correo(s) por autorizacion(es).");
        //}
        //if (HabilitarVoucher()) {
        //    document.getElementById('<%=btnCrearVoucher.ClientID %>').style.visibility = "visible";
        //}
    }
    else {
        RegistrarOrdenVenta();
        //if(HabilitarVoucher()){
        //    document.getElementById('<%=btnCrearVoucher.ClientID %>').style.visibility = "visible";
        //}
    }
}
function RegistrarBorradorVenta() {
    var intUsuario = parseInt(parent.document.getElementById("hdnUsuario").value);
    var strTipoVenta = "ventaBodega";
    var strCliente = document.getElementById("txtCodigoCliente").value;
    var strMonedaPago = document.getElementById("cmbMonedaPago").options[document.getElementById("cmbMonedaPago").selectedIndex].value;
    var strOrdenCompra = document.getElementById("txtOrdenCompra").value;
    var intVendedor = document.getElementById("cmbVendedor").options[document.getElementById("cmbVendedor").selectedIndex].value;
    var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);
    var strFechaOrdenVenta = document.getElementById("txtFechaOrdenVenta").value;
    var strFechaEntrega = document.getElementById("txtFechaEntregaOrden").value;
    var intSerie = document.getElementById("cmbSerie").options[document.getElementById("cmbSerie").selectedIndex].value;
    var intTipoDespacho = document.getElementById("cmbTipoDespacho").options[document.getElementById("cmbTipoDespacho").selectedIndex].value;
    var strNotaPedido = document.getElementById("txtNotaPedido").value;
    var strPlazoVenta = document.getElementById("cmbPlazoVenta").options[document.getElementById("cmbPlazoVenta").selectedIndex].value;
    var arrPlazoVenta = strPlazoVenta.split("_");
    var intCodigoPlazoVenta = arrPlazoVenta[0];
    var strFechaVencimiento = document.getElementById("txtFechaVencimiento").value;
    var intCodigoPlazoCompra = 0;
    var strDireccionFactura = document.getElementById("cmbDireccionFactura").options[document.getElementById("cmbDireccionFactura").selectedIndex].value;
    var strDireccionDespacho = document.getElementById("cmbDireccionDespacho").options[document.getElementById("cmbDireccionDespacho").selectedIndex].value;
    var strComentario1 = document.getElementById("txtComentario1").value;
    var strComentario2 = document.getElementById("txtComentario2").value;
    var strComentario3 = "";
    var intMotivo = 0;
    var strEmision = document.getElementById("cmbEmision").options[document.getElementById("cmbEmision").selectedIndex].value;
    var strGuiaDespacho = document.getElementById("txtGuiaDespacho").value;
    var strCategoria = document.getElementById("txtCategoria").value;
    var strVendedor = document.getElementById("cmbVendedor").options[document.getElementById("cmbVendedor").selectedIndex].text;
    var strPedido = JSON.stringify(tablaHtml);
    //var blnCosto = false;
    //var blnMargen = true;
    //var blnCredito = true;
    //var blnDeuda = false;
    //var blnProtesto = false;
    //alert("EN BORRADOR DE VENTA intUsuario: " + intUsuario + ", strTipoVenta :" + strTipoVenta + ", strCliente: " + strCliente + ", strMonedaPago: " + strMonedaPago + ", strOrdenCompra: " + strOrdenCompra + ", intVendedor: " + intVendedor + ", intOperador: " + intOperador + ", strFechaOrdenVenta: " + strFechaOrdenVenta + ", strFechaEntrega: " + strFechaEntrega + ", intSerie: " + intSerie + ", intTipoDespacho: " + intTipoDespacho + ", strNotaPedido: " + strNotaPedido + ", intCodigoPlazoVenta: " + intCodigoPlazoVenta + ", strFechaVencimiento: " + strFechaVencimiento + ", intCodigoPlazoCompra: " + intCodigoPlazoCompra + ", strDireccionFactura: " + strDireccionFactura + ", strDireccionDespacho: " + strDireccionDespacho + ", strComentario1: " + strComentario1 + ", strComentario2: " + strComentario2 + ", strComentario3: " + strComentario3 + ", intMotivo:" + intMotivo + ", strEmision: " + strEmision + ", strGuiaDespacho: " + strGuiaDespacho + ", strCategoria: " + strCategoria + ", strVendedor: " + strVendedor + ", strPedido: " + strPedido);
    //var blnExito = false;
    //var intRetorno = 0;
    MostrarProgreso2();
    $.ajax({
        type: "post",
        async: true,
        contentType: "application/json; charset=utf-8",
        url: "http://wssap_test.agroinsumos.cl/Services/srvOrdenVenta.asmx/RegistrarBorradorVenta",
        dataType: "json",
        data: "{'intUsuario':'" + intUsuario + "','strTipoVenta':'" + strTipoVenta + "','strCliente':'" + strCliente + "','strMonedaPago':'" + strMonedaPago + "','strOrdenCompra':'" + strOrdenCompra + "','intVendedor':'" + intVendedor + "','intOperador':'" + intOperador + "','strFechaOrdenVenta':'" + strFechaOrdenVenta + "','strFechaEntrega':'" + strFechaEntrega + "','intSerie':'" + intSerie + "','intTipoDespacho':'" + intTipoDespacho + "','strNotaPedido':'" + strNotaPedido + "','intCodigoPlazoVenta':'" + intCodigoPlazoVenta + "','strFechaVencimiento':'" + strFechaVencimiento + "','intCodigoPlazoCompra':'" + intCodigoPlazoCompra + "','strDireccionFactura':'" + strDireccionFactura + "','strDireccionDespacho':'" + strDireccionDespacho + "','strComentario1':'" + strComentario1 + "','strComentario2':'" + strComentario2 + "','strComentario3':'" + strComentario3 + "','intMotivo':'" + intMotivo + "','strEmision':'" + strEmision + "','strGuiaDespacho':'" + strGuiaDespacho + "','strPedido':'" + strPedido + "'}",
        dataFilter: function (data) { return data; },
        success: function (xml) {
            OcultarProgreso2();
            var arrDatos = (xml.d).split("|");
            var intStatus = parseInt(arrDatos[0]);
            var intDocNum = parseInt(arrDatos[1]);
            var intDocEntry = parseInt(arrDatos[2]);
            var strStatusMsg = arrDatos[3];
            if (intStatus == 0) {
                //document.getElementById("btnCargarPedido").style.display = "none";
                $("#divCrearPedido").html("Se ha generado el borrador de venta N° " + intDocEntry);
                if (intDocEntry != 0) {
                    EnviarAutorizacionCorreo(intDocEntry);
                    $("#divCrearPedido").html("Se ha generado el borrador de venta N° " + intDocEntry + " y se ha(n) enviado correo(s) por autorizacion(es).");
                    $("#hdnTipo").val(1);
                    $("#hdnCodigo").val(intDocEntry);
                    $("#divVoucher").show();
                }
            }
            else {
                $("#divCrearPedido").html("Error: " + intStatus + " - " + strStatusMsg + ".");
                document.getElementById("btnCargarPedido").style.visibility = "visible";
            }
        }
    });
}
function EnviarAutorizacionCorreo(intDocEntry) {
    var dblValorDolar = parseFloat(parent.document.getElementById("hdnDolar").value);
    var dblValorEuro = parseFloat(parent.document.getElementById("hdnEuro").value);
    var strTipoVenta = "ventaBodega";
    var strCliente = document.getElementById("txtCodigoCliente").value;
    var strNombreCliente = document.getElementById("txtNombreCliente").value;
    var strRutCliente = document.getElementById("txtRutCliente").value;
    var strTelefonoCliente = document.getElementById("hdnTelefonoCliente").value;
    var strGiroCliente = document.getElementById("hdnGiroCliente").value;
    var strCorreoCliente = document.getElementById("hdnCorreoCliente").value;
    var strGrupoCliente = document.getElementById("hdnGrupoCliente").value;
    var strMonedaPago = document.getElementById("cmbMonedaPago").options[document.getElementById("cmbMonedaPago").selectedIndex].value;
    var strOrdenCompra = document.getElementById("txtOrdenCompra").value;
    var intVendedor = document.getElementById("cmbVendedor").options[document.getElementById("cmbVendedor").selectedIndex].value;
    var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);
    var strCodigoOperador = parent.document.getElementById("hdnUser").value;
    var strNombreOperador = document.getElementById("txtOperador").value;
    var strFechaOrdenVenta = document.getElementById("txtFechaOrdenVenta").value;
    var strFechaEntrega = document.getElementById("txtFechaEntregaOrden").value;
    var intSerie = document.getElementById("cmbSerie").options[document.getElementById("cmbSerie").selectedIndex].value;
    var strSerie = document.getElementById("cmbSerie").options[document.getElementById("cmbSerie").selectedIndex].text;
    var intTipoDespacho = document.getElementById("cmbTipoDespacho").options[document.getElementById("cmbTipoDespacho").selectedIndex].value;
    var strTipoDespacho = document.getElementById("cmbTipoDespacho").options[document.getElementById("cmbTipoDespacho").selectedIndex].text;
    var strNotaPedido = document.getElementById("txtNotaPedido").value;
    var strPlazoVenta = document.getElementById("cmbPlazoVenta").options[document.getElementById("cmbPlazoVenta").selectedIndex].value;
    var arrPlazoVenta = strPlazoVenta.split("_");
    var intCodigoPlazoVenta = arrPlazoVenta[0];
    var strNombrePlazoVenta = document.getElementById("cmbPlazoVenta").options[document.getElementById("cmbPlazoVenta").selectedIndex].text;
    var strFechaVencimiento = document.getElementById("txtFechaVencimiento").value;
    var strDireccionFactura = document.getElementById("cmbDireccionFactura").options[document.getElementById("cmbDireccionFactura").selectedIndex].value;
    var strDireccionDespacho = document.getElementById("cmbDireccionDespacho").options[document.getElementById("cmbDireccionDespacho").selectedIndex].value;
    var strComentario1 = document.getElementById("txtComentario1").value;
    var strComentario2 = document.getElementById("txtComentario2").value;
    var strComentario3 = "";
    var strMotivo = "";
    var strEmision = document.getElementById("cmbEmision").options[document.getElementById("cmbEmision").selectedIndex].value;
    var strGuiaDespacho = document.getElementById("txtGuiaDespacho").value;
    var strCategoria = document.getElementById("txtCategoria").value;
    var strVendedor = document.getElementById("cmbVendedor").options[document.getElementById("cmbVendedor").selectedIndex].text;
    var strPedido = JSON.stringify(tablaHtml);
    //alert("EN AUTORIZACION POR CORREO    " + "dblValorDolar: " + dblValorDolar + ", dblValorEuro: " + dblValorEuro + ", intDocEntry: " + intDocEntry + ", strTipoVenta :" + strTipoVenta + ", strCliente: " + strCliente + ", strNombreCliente:" + strNombreCliente + ", strRutCliente:" + strRutCliente + ", strTelefonoCliente:" + strTelefonoCliente + ", strGiroCliente:" + strGiroCliente + ", strCorreoCliente:" + strCorreoCliente + ", strGrupoCliente:" + strGrupoCliente + ", strMonedaPago: " + strMonedaPago + ", strOrdenCompra: " + strOrdenCompra + ", intVendedor: " + intVendedor + ", intOperador: " + intOperador + ", strCodigoOperador: " + strCodigoOperador + ", strNombreOperador: " + strNombreOperador + ", strFechaOrdenVenta: " + strFechaOrdenVenta + ", strFechaEntrega: " + strFechaEntrega + ", intSerie: " + intSerie + ", strSerie: " + strSerie + ", intTipoDespacho: " + intTipoDespacho + ", strTipoDespacho: " + strTipoDespacho + ", strNotaPedido: " + strNotaPedido + ", intCodigoPlazoVenta: " + intCodigoPlazoVenta + ", strNombrePlazoVenta: " + strNombrePlazoVenta + ", strFechaVencimiento: " + strFechaVencimiento + ", strDireccionFactura: " + strDireccionFactura + ", strDireccionDespacho: " + strDireccionDespacho + ", strComentario1: " + strComentario1 + ", strComentario2: " + strComentario2 + ", strComentario3: " + strComentario3 + ", strMotivo: " + strMotivo + ", strEmision: " + strEmision + ", strGuiaDespacho: " + strGuiaDespacho + ", strCategoria: " + strCategoria + ", strVendedor: " + strVendedor + ", strPedido: " + strPedido);
    MostrarProgreso2();
    $.ajax({
        type: "post",
        async: true,
        url: "/Services/srvAutorizacion.asmx/EnviarAutorizacionCorreo",
        dataType: "xml",
        data: { "dblValorDolar": dblValorDolar, "dblValorEuro": dblValorEuro, "intDocEntry": intDocEntry, "strTipoVenta": strTipoVenta, "strCliente": strCliente, "strNombreCliente": strNombreCliente, "strRutCliente": strRutCliente, "strTelefonoCliente": strTelefonoCliente, "strGiroCliente": strGiroCliente, "strCorreoCliente": strCorreoCliente, "strGrupoCliente": strGrupoCliente, "strMonedaPago": strMonedaPago, "strOrdenCompra": strOrdenCompra, "intVendedor": intVendedor, "intOperador": intOperador, "strCodigoOperador": strCodigoOperador, "strNombreOperador": strNombreOperador, "strFechaOrdenVenta": strFechaOrdenVenta, "strFechaEntrega": strFechaEntrega, "intSerie": intSerie, "strSerie": strSerie, "intTipoDespacho": intTipoDespacho, "strTipoDespacho": strTipoDespacho, "strNotaPedido": strNotaPedido, "intCodigoPlazoVenta": intCodigoPlazoVenta, "strNombrePlazoVenta": strNombrePlazoVenta, "strFechaVencimiento": strFechaVencimiento, "strDireccionFactura": strDireccionFactura, "strDireccionDespacho": strDireccionDespacho, "strComentario1": strComentario1, "strComentario2": strComentario2, "strComentario3": strComentario3, "strMotivo": strMotivo, "strEmision": strEmision, "strGuiaDespacho": strGuiaDespacho, "strCategoria": strCategoria, "strVendedor": strVendedor, "strPedido": strPedido },
        success: function (xml) {
            OcultarProgreso2();
            var strRespuesta = $(xml).find('string').text();
        }
    });
}
function RegistrarOrdenVenta() {
    var intUsuario = parseInt(parent.document.getElementById("hdnUsuario").value);
    var strTipoVenta = "ventaBodega";
    var strCliente = document.getElementById("txtCodigoCliente").value;
    var strMonedaPago = document.getElementById("cmbMonedaPago").options[document.getElementById("cmbMonedaPago").selectedIndex].value;
    var strOrdenCompra = document.getElementById("txtOrdenCompra").value;
    var intVendedor = document.getElementById("cmbVendedor").options[document.getElementById("cmbVendedor").selectedIndex].value;
    var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);
    var strFechaOrdenVenta = document.getElementById("txtFechaOrdenVenta").value;
    var strFechaEntrega = document.getElementById("txtFechaEntregaOrden").value;
    var intSerie = document.getElementById("cmbSerie").options[document.getElementById("cmbSerie").selectedIndex].value;
    var intTipoDespacho = document.getElementById("cmbTipoDespacho").options[document.getElementById("cmbTipoDespacho").selectedIndex].value;
    var strNotaPedido = document.getElementById("txtNotaPedido").value;
    var strPlazoVenta = document.getElementById("cmbPlazoVenta").options[document.getElementById("cmbPlazoVenta").selectedIndex].value;
    var arrPlazoVenta = strPlazoVenta.split("_");
    var intCodigoPlazoVenta = arrPlazoVenta[0];
    var strFechaVencimiento = document.getElementById("txtFechaVencimiento").value;
    var intCodigoPlazoCompra = 0;
    var strDireccionFactura = document.getElementById("cmbDireccionFactura").options[document.getElementById("cmbDireccionFactura").selectedIndex].value;
    var strDireccionDespacho = document.getElementById("cmbDireccionDespacho").options[document.getElementById("cmbDireccionDespacho").selectedIndex].value;
    var strComentario1 = document.getElementById("txtComentario1").value;
    var strComentario2 = document.getElementById("txtComentario2").value;
    var strComentario3 = "";
    var intMotivo = 0;
    var strEmision = document.getElementById("cmbEmision").options[document.getElementById("cmbEmision").selectedIndex].value;
    var strGuiaDespacho = document.getElementById("txtGuiaDespacho").value;
    var strCategoria = document.getElementById("txtCategoria").value;
    var strVendedor = document.getElementById("cmbVendedor").options[document.getElementById("cmbVendedor").selectedIndex].text;
    var strPedido = JSON.stringify(tablaHtml);
    //alert("EN ORDEN DE VENTA  intUsuario: " + intUsuario + ", strTipoVenta :" + strTipoVenta + ", strCliente: " + strCliente + ", strMonedaPago: " + strMonedaPago + ", strOrdenCompra: " + strOrdenCompra + ", intVendedor: " + intVendedor + ", intOperador: " + intOperador + ", strFechaOrdenVenta: " + strFechaOrdenVenta + ", strFechaEntrega: " + strFechaEntrega + ", intSerie: " + intSerie + ", intTipoDespacho: " + intTipoDespacho + ", strNotaPedido: " + strNotaPedido + ", intCodigoPlazoVenta: " + intCodigoPlazoVenta + ", strFechaVencimiento: " + strFechaVencimiento + ", intCodigoPlazoCompra: " + intCodigoPlazoCompra + ", strDireccionFactura: " + strDireccionFactura + ", strDireccionDespacho: " + strDireccionDespacho + ", strComentario1: " + strComentario1 + ", strComentario2: " + strComentario2 + ", strComentario3: " + strComentario3 + ", intMotivo:" + intMotivo + ", strEmision: " + strEmision + ", strGuiaDespacho: " + strGuiaDespacho + ", strCategoria: " + strCategoria + ", strVendedor: " + strVendedor + ", strPedido: " + strPedido);
    MostrarProgreso2();
    $.ajax({
        type: "post",
        async: true,
        contentType: "application/json; charset=utf-8",
        url: "http://wssap_test.agroinsumos.cl/Services/srvOrdenVenta.asmx/RegistrarOrdenVenta",
        dataType: "json",
        data: "{'intUsuario':'" + intUsuario + "','strTipoVenta':'" + strTipoVenta + "','strCliente':'" + strCliente + "','strMonedaPago':'" + strMonedaPago + "','strOrdenCompra':'" + strOrdenCompra + "','intVendedor':'" + intVendedor + "','intOperador':'" + intOperador + "','strFechaOrdenVenta':'" + strFechaOrdenVenta + "','strFechaEntrega':'" + strFechaEntrega + "','intSerie':'" + intSerie + "','intTipoDespacho':'" + intTipoDespacho + "','strNotaPedido':'" + strNotaPedido + "','intCodigoPlazoVenta':'" + intCodigoPlazoVenta + "','strFechaVencimiento':'" + strFechaVencimiento + "','intCodigoPlazoCompra':'" + intCodigoPlazoCompra + "','strDireccionFactura':'" + strDireccionFactura + "','strDireccionDespacho':'" + strDireccionDespacho + "','strComentario1':'" + strComentario1 + "','strComentario2':'" + strComentario2 + "','strComentario3':'" + strComentario3 + "','intMotivo':'" + intMotivo + "','strEmision':'" + strEmision + "','strGuiaDespacho':'" + strGuiaDespacho + "','strPedido':'" + strPedido + "'}",
        success: function (xml) {
            OcultarProgreso2();
            var arrDatos = (xml.d).split("|");
            var intStatus = parseInt(arrDatos[0]);
            var intDocNum = parseInt(arrDatos[1]);
            var intDocEntry = parseInt(arrDatos[2]);
            var strStatusMsg = arrDatos[3];
            if (intStatus == 0) {
                //document.getElementById("btnCargarPedido").style.display = "none";
                $("#divCrearPedido").html("Se ha generado la orden de venta N° " + intDocNum);
                $("#hdnTipo").val(2);
                $("#hdnCodigo").val(intDocNum);
                $("#divVoucher").show();
            }
            else {
                $("#divCrearPedido").html("Error " + intStatus + " - " + strStatusMsg + ".");
                document.getElementById("btnCargarPedido").style.visibility = "visible";
            }
        }
    });
}
function HabilitarVoucher() {
    var intTipo = $("#hdnTipo").val();
    var intCodigo = $("#hdnCodigo").val();
    if (intTipo != "" && intCodigo != "") {
        return true;
    }
    else {
        alert("Debe guardar el documento antes de generar el voucher.");
        return false;
    }
}
function MostrarProgreso1() {
    $(document).ajaxStart(function () {
        $("#divLoader1").show();
    });
}
function OcultarProgreso1() {
    $(document).ajaxStop(function () {
        $("#divLoader1").hide();
    });
}
function MostrarProgreso2() {
    $(document).ajaxStart(function () {
        $("#divLoader2").show();
    });
}
function OcultarProgreso2() {
    $(document).ajaxStop(function () {
        $("#divLoader2").hide();
    });
}
function CargarCuadroFacturaImpaga() {
    var strTabla = "";
    strTabla += '<table id="tblFacturaImpaga" border="0px" style="width:340px">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="3">Deudas Vencidas</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdLeft"   style="width:113px">N° Docto.</td>';
    strTabla += '       <td class="tdCenter" style="width:114px">Fecha Docto.</td>';
    strTabla += '       <td class="tdRight"  style="width:113px">Monto</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr><td id="tdResumenFacturaImpaga" colspan="3">Cargando...</td></tr>';
    strTabla += '</table>';
    $("#divCuadroResumenFacturaImpaga").html(strTabla);
}
function CargarCuadroResumenFacturaImpaga() {
    var strCodigoCliente = document.getElementById("txtCodigoCliente").value;
    $("#hdnAutorizacionDeuda").val("false");
    var strTabla = "";
    var blnHayDatos = false;
    $.ajax({
        type: "post",
        async: true,
        url: "/Services/srvCliente.asmx/ObtenerClienteFacturaImpaga",
        dataType: "xml",
        data: { "strCliente": strCodigoCliente },
        success: function (xml) {
            $(xml).find("ArrayOfClsCliente").each(function () {
                $(this).find("clsCliente").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var intDocCodigo = $registro.find("FacNumero").text();
                    var strDocFecha = $registro.find("FacFecha").text();
                    var strDocValor = $registro.find("FacValor").text();
                    strTabla += '<table id="tblResumenFacturaImpaga' + intDocCodigo + '" border="0px" style="width:340px">';
                    strTabla += '   <tr id="trFacturaImpaga' + intDocCodigo + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);">';
                    strTabla += '       <td class="tdLeft"   style="width:113px">' + intDocCodigo + '</td>';
                    strTabla += '       <td class="tdCenter" style="width:114px">' + strDocFecha + '</td>';
                    strTabla += '       <td class="tdRight"  style="width:113px">' + strDocValor + '</td>';
                    strTabla += '   </tr>';
                    strTabla += '</table>';
                    document.getElementById("tdResumenFacturaImpaga").innerHTML = strTabla;
                });
            });
            if (!blnHayDatos) {
                strTabla += "<table id='tblResumenFacturaImpaga' border='0px' style='width:340px'><tr><td colspan='3'>No hay informacion de facturas impagas...</td></tr></table>";
                document.getElementById("tdResumenFacturaImpaga").innerHTML = strTabla;
            }
            else {
                $("#hdnAutorizacionDeuda").val("true");
            }
        }
    });
}
function CargarCuadroChequeProtestado() {
    var strTabla = "";
    strTabla += '<table id="tblChequeProtestado" border="0px" style="width:340px">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="3">Cheques Protestados</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdLeft"   style="width:113px">N° Docto.</td>';
    strTabla += '       <td class="tdCenter" style="width:114px">Fecha Docto.</td>';
    strTabla += '       <td class="tdRight"  style="width:113px">Monto</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr><td id="tdResumenChequeProtestado" colspan="3">Cargando...</td></tr>';
    strTabla += '</table>';
    $("#divCuadroResumenChequeProtestado").html(strTabla);
}
function CargarCuadroResumenChequeProtestado() {
    var strCodigoCliente = document.getElementById("txtCodigoCliente").value;
    $("#hdnAutorizacionProtesto").val("false");
    var strTabla = "";
    var blnHayDatos = false;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvCliente.asmx/ObtenerClienteChequeProtestado",
        dataType: "xml",
        data: { "strCliente": strCodigoCliente },
        success: function (xml) {
            $(xml).find("ArrayOfClsCliente").each(function () {
                $(this).find("clsCliente").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var intDocCodigo = $registro.find("CheNumero").text();
                    var strDocFecha = $registro.find("CheFecha").text();
                    var strDocValor = $registro.find("CheValor").text();
                    strTabla += '<table id="tblResumenChequeProtestado' + intDocCodigo + '" border="0px" style="width:340px">';
                    strTabla += '   <tr id="trChequeProtestado' + intDocCodigo + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);">';
                    strTabla += '       <td class="tdLeft"   style="width:113px">' + intDocCodigo + '</td>';
                    strTabla += '       <td class="tdCenter" style="width:114px">' + strDocFecha + '</td>';
                    strTabla += '       <td class="tdRight"  style="width:113px">' + strDocValor + '</td>';
                    strTabla += '   </tr>';
                    strTabla += '</table>';
                    document.getElementById("tdResumenChequeProtestado").innerHTML = strTabla;
                });
            });
            if (!blnHayDatos) {
                strTabla += "<table id='tblResumenChequeProtestado' border='0px' style='width:340px'><tr><td colspan='3'>No hay informacion de cheques protestados...</td></tr></table>";
                document.getElementById("tdResumenChequeProtestado").innerHTML = strTabla;
            }
            else {
                $("#hdnAutorizacionProtesto").val("true");
            }
        }
    });
}
function ActualizarIncluyeFlete() {
    CambiarIncluyeFlete(0);
}