$(document).ready(function () {
    CargarComboIngrediente();
    CargarComboProducto();
    var intPerfil = parseInt(parent.document.getElementById("hdnPerfil").value);
    if (intPerfil == 1 || intPerfil == 2) {
        CargarTextoProducto();
    }else{
        CargarTextoProductoEspecial();
    }    
});
function CambiarEstado(intOrigen) {
    document.getElementById("txtBuscaProducto").value = "";
    document.getElementById("cmbIngrediente").selectedIndex = 0;
    CargarComboProducto();
    if (intOrigen == 0) {
        document.getElementById("txtBuscaProducto").disabled = false;
        document.getElementById("cmbIngrediente").disabled = true;
        document.getElementById("cmbProducto").disabled = true;
    }else{
        document.getElementById("txtBuscaProducto").disabled = true;
        document.getElementById("cmbIngrediente").disabled = false;
        document.getElementById("cmbProducto").disabled = false;
    }
}
function CargarComboIngrediente() {
    var intOpcion = 10;
    var intIndex = 0;
    var cmbIngrediente = document.getElementById("cmbIngrediente");
    cmbIngrediente.options.length = 0;
    cmbIngrediente.options[intIndex] = new Option("SELECCIONE", "0", true, true);
    intIndex++;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvIngredienteActivo.asmx/ObtenerIngredienteActivo",
        dataType: "xml",
        data: { "intOpcion": intOpcion },
        success: function (xml) {
            $(xml).find("ArrayOfClsIngredienteActivo").each(function () {
                $(this).find("clsIngredienteActivo").each(function () {
                    var $registro = $(this);
                    var IngCodigo = $registro.find("IngCodigo").text();
                    var IngNombre = $registro.find("IngNombre").text();
                    cmbIngrediente.options[intIndex] = new Option(IngNombre, IngCodigo);
                    intIndex++;
                });
            });
        }
    });
}
function CargarComboProducto() {
    var intOpcion = 30;
    var strProducto = '';
    var strBodega = '';
    var strIngrediente = document.getElementById("cmbIngrediente").options[document.getElementById("cmbIngrediente").selectedIndex].value;
    var intIndex = 0;
    var cmbProducto = document.getElementById("cmbProducto");
    cmbProducto.options.length = 0;
    cmbProducto.options[intIndex] = new Option("SELECCIONE", "0", true, true);
    if (strIngrediente != "0") {
       intIndex++;
        $.ajax({
            type: "post",
            async: false,
            url: "/Services/srvProducto.asmx/ListarProducto",
            dataType: "xml",
            data: { "intOpcion": intOpcion, "strProducto": strProducto, "strBodega": strBodega, "strIngrediente": strIngrediente },
            success: function (xml) {
                $(xml).find("ArrayOfClsProducto").each(function () {
                    $(this).find("clsProducto").each(function () {
                        var $registro = $(this);
                        var ArtCodigo = $registro.find("ArtCodigo").text();
                        var ArtNombre = $registro.find("ArtNombre").text();
                        cmbProducto.options[intIndex] = new Option(ArtNombre, ArtCodigo);
                        intIndex++;
                    });
                });
            }
        });
    }
}
function MostrarInventario() {
    var strProducto = document.getElementById("cmbProducto").options[document.getElementById("cmbProducto").selectedIndex].value;
    var intPerfil = parseInt(parent.document.getElementById("hdnPerfil").value);
    if (intPerfil == 1 || intPerfil == 2) {
        ObtenerDatosProducto(strProducto);
    }else{
        ObtenerDatosProductoEspecial(strProducto);
    }
}

function ObtenerDatosProducto(strProducto) {
    var intOpcion = 10;
    var strBodega = CargarBodega();
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvProducto.asmx/ObtenerProducto",
        dataType: "xml",
        data: { "intOpcion": intOpcion, "strProducto": strProducto, "strBodega": strBodega },
        success: function (xml) {
            $(xml).find("ArrayOfClsProducto").each(function () {
                $(this).find("clsProducto").each(function () {
                    var $registro = $(this);
                    $("#txtBuscaProducto").html(" ");
                    $("#divNombreProveedor").html($registro.find("ProNombre").text());
                    $("#divNombreGrupo").html($registro.find("GruNombre").text());
                    $("#divEnvaseProducto").html($registro.find("ArtEnvase").text());
                    $("#divCodigoProducto").html($registro.find("ArtCodigo").text());
                    $("#divNombreProducto").html($registro.find("ArtNombre").text());
                    $("#divDiasPago").html($registro.find("ArtDiasPago").text());
                    $("#divPrecioLista").html($registro.find("ArtMonedaCompra").text() + " " + $registro.find("ArtPrecioCompra").text());
                    $("#tblDatos").show();
                    CargarCuadroInventario(strProducto);
                });
            });
        }
    });
}
function ObtenerDatosProductoEspecial(strProducto) {
    var intOpcion = 20;
    var strBodega = "";
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvProducto.asmx/ObtenerProducto",
        dataType: "xml",
        data: { "intOpcion": intOpcion, "strProducto": strProducto, "strBodega": strBodega },
        success: function (xml) {
            $(xml).find("ArrayOfClsProducto").each(function () {
                $(this).find("clsProducto").each(function () {
                    var $registro = $(this);
                    $("#txtBuscaProducto").html(" ");
                    $("#divNombreProveedor").html($registro.find("ProNombre").text());
                    $("#divNombreGrupo").html($registro.find("GruNombre").text());
                    $("#divEnvaseProducto").html($registro.find("ArtEnvase").text());
                    $("#divCodigoProducto").html($registro.find("ArtCodigo").text());
                    $("#divNombreProducto").html($registro.find("ArtNombre").text());
                    $("#divDiasPago").html($registro.find("ArtDiasPago").text());
                    $("#divPrecioLista").html($registro.find("ArtMonedaCompra").text() + " " + $registro.find("ArtPrecioCompra").text());
                    $("#tblDatos").show();
                    CargarCuadroInventarioEspecial(strProducto);
                });
            });
        }
    });
}

function CargarBodega() {
    var strOperador = parent.document.getElementById("hdnUser").value;
    var strBodega = "";
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
                    var BodDefecto = $registro.find("BodDefecto").text();
                    var BodTipoVenta = $registro.find("BodTipoVenta").text();
                    if (BodTipoVenta == "VentaBodegaPropia") {
                        if (BodDefecto == "Y") {
                            strBodega = BodCodigo;
                        }
                    }
                });
            });
        }
    });
    return strBodega;
}

function CargarTextoProducto() {
    var intOpcion = 10;
    var strBodega = CargarBodega();
    $("#txtBuscaProducto").autocomplete({
        max: 10,
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/Services/srvProducto.asmx/ObtenerProducto",
                dataType: "json",
                data: "{ 'intOpcion':'" + intOpcion + "', 'strProducto':'" + request.term + "', 'strBodega':'" + strBodega + "'}",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.ArtCodigo + ' ' + item.ArtNombre + ' ' + '(F:' + item.ArtStockFisico + ' - D:' + item.ArtStockDisponible + ")",
                            ArtCodigo: item.ArtCodigo,
                            ArtNombre: item.ArtNombre,
                            ArtEnvase: item.ArtEnvase,
                            GruNombre: item.GruNombre,
                            ProNombre: item.ProNombre,
                            ArtMoneda: item.ArtMoneda,
                            ArtPrecioUnitario: item.ArtPrecioUnitario,
                            ArtCostoComercial: item.ArtCostoComercial,
                            ArtStockFisico: item.ArtStockFisico,
                            ArtStockDisponible: item.ArtStockDisponible,
                            ArtInventariable: item.ArtInventariable,
                            ArtDiasPago: item.ArtDiasPago,
                            ArtPrecioCompra: item.ArtPrecioCompra,
                            ArtMonedaCompra: item.ArtMonedaCompra
                        }
                    }))
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            $("#txtBuscaProducto").html(" ");
            $("#divNombreProveedor").html(ui.item.ProNombre);
            $("#divNombreGrupo").html(ui.item.GruNombre);
            $("#divEnvaseProducto").html(ui.item.ArtEnvase);
            $("#divCodigoProducto").html(ui.item.ArtCodigo);
            $("#divNombreProducto").html(ui.item.ArtNombre);
            $("#divDiasPago").html(ui.item.ArtDiasPago);
            $("#divPrecioLista").html(ui.item.ArtMonedaCompra+" "+ui.item.ArtPrecioCompra);
            $("#tblDatos").show();
            CargarCuadroInventario(ui.item.ArtCodigo);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}
function CargarCuadroInventario(strProducto) {
    var strOperador = parent.document.getElementById("hdnUser").value;
    var intTotalStockFisico = 0;
    var intTotalStockPedido = 0;
    var intTotalStockOrdenado = 0;
    var intTotalStockDisponible = 0;
    var strTabla = "";
    strTabla += '<table id="tblInventario" border="0px">';
    strTabla += '<tr class="trTituloSeccion"><td class="tdLeft" colspan="9">Inventario de Productos en Bodega(s)</td></tr>';
    strTabla += '<tr class="trTituloCabecera">';
    strTabla += '<td class="tdLeft" ></td>';
    strTabla += '<td class="tdLeft" >Codigo</td>';
    strTabla += '<td class="tdLeft" >Bodega</td>';
    strTabla += '<td class="tdRight">Fisico #</td>';
    strTabla += '<td class="tdRight">Comprometido #</td>';
    strTabla += '<td class="tdRight">Ordenado #</td>';
    strTabla += '<td class="tdRight">Disponible #</td>';
    strTabla += '<td class="tdRight">Moneda</td>';
    strTabla += '<td class="tdRight">Costo Comercial</td>';
    strTabla += '</tr>';
    var intEntro = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvInventario.asmx/ListarInventario",
        dataType: "xml",
        data: { "strProducto": strProducto,"strOperador": strOperador },
        success: function (xml) {
            $(xml).find("ArrayOfClsInventario").each(function () {
                $(this).find("clsInventario").each(function () {
                    intEntro = 1;
                    var $registro = $(this);
                    var strBodAutoriza = $registro.find("BodAutoriza").text();
                    var strBodCodigo = $registro.find("BodCodigo").text();
                    var strBodNombre = $registro.find("BodNombre").text();
                    var intArtStockFisico = $registro.find("ArtStockFisico").text();
                    var intArtStockPedido = $registro.find("ArtStockPedido").text();
                    var intArtStockOrdenado = $registro.find("ArtStockOrdenado").text();
                    var intArtStockDisponible = $registro.find("ArtStockDisponible").text();
                    var strArtMoneda = $registro.find("ArtMoneda").text();
                    var strArtCosto = $registro.find("ArtCosto").text();
                    intTotalStockFisico += parseInt(intArtStockFisico);
                    intTotalStockPedido += parseInt(intArtStockPedido);
                    intTotalStockOrdenado += parseInt(intArtStockOrdenado);
                    intTotalStockDisponible += parseInt(intArtStockDisponible);
                    strTabla += '<tr id="tr' + strBodCodigo + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);" class="trTituloDetalle">';
                    if (strBodAutoriza == "0") {
                        strTabla += '<td class="tdLeft">*</td>';
                    }else{
                        strTabla += '<td></td>';
                    }
                    strTabla += '<td class="tdLeft">' + strBodCodigo + '</td>';
                    strTabla += '<td class="tdLeft">' + strBodNombre + '</td>';
                    strTabla += '<td class="tdRight">' + intArtStockFisico + '</td>';
                    strTabla += '<td class="tdRight">' + intArtStockPedido + '</td>';
                    strTabla += '<td class="tdRight">' + intArtStockOrdenado + '</td>';
                    strTabla += '<td class="tdRight">' + intArtStockDisponible + '</td>';
                    if (strBodAutoriza == "1" && !(strOperador.toLowerCase() == 'mvallejo' || strOperador.toLowerCase() == 'reyheram' || strOperador.toLowerCase() == 'rdelabar' || strOperador.toLowerCase() == 'framos')) {
                        strTabla += '<td class="tdRight">---</td>';
                        strTabla += '<td class="tdRight">---</td>';
                    }else{
                        strTabla += '<td class="tdRight">' + strArtMoneda + '</td>';
                        strTabla += '<td class="tdRight">' + strArtCosto + '</td>';
                    }
                    strTabla += '</tr>';
                });
            });
            if (intEntro == 0) {
                strTabla += "<tr><td colspan='9'>No hay informacion...</td></tr>";
            }else{
                strTabla += '<tr><td colspan="9"><hr></td></tr>';
                strTabla += '<tr class="trTituloDetalle">';
                strTabla += '<td class="tdLeft"></td>';
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

function CargarTextoProductoEspecial() {
    var intOpcion = 20;
    var strBodega = "";
    $("#txtBuscaProducto").autocomplete({
        max: 10,
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/Services/srvProducto.asmx/ObtenerProducto",
                dataType: "json",
                data: "{ 'intOpcion':'" + intOpcion + "', 'strProducto':'" + request.term + "', 'strBodega':'" + strBodega + "'}",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.ArtCodigo + ' ' + item.ArtNombre,
                            ArtCodigo: item.ArtCodigo,
                            ArtNombre: item.ArtNombre,
                            ArtEnvase: item.ArtEnvase,
                            GruNombre: item.GruNombre,
                            ProNombre: item.ProNombre,
                            ArtInventariable: item.ArtInventariable,
                            ArtDiasPago: item.ArtDiasPago,
                            ArtPrecioCompra: item.ArtPrecioCompra,
                            ArtMonedaCompra: item.ArtMonedaCompra
                        }
                    }))
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            $("#txtBuscaProducto").html(" ");
            $("#divNombreProveedor").html(ui.item.ProNombre);
            $("#divNombreGrupo").html(ui.item.GruNombre);
            $("#divEnvaseProducto").html(ui.item.ArtEnvase);
            $("#divCodigoProducto").html(ui.item.ArtCodigo);
            $("#divNombreProducto").html(ui.item.ArtNombre);
            $("#divDiasPago").html(ui.item.ArtDiasPago);
            $("#divPrecioLista").html(ui.item.ArtMonedaCompra + " " + ui.item.ArtPrecioCompra);
            $("#tblDatos").show();
            CargarCuadroInventarioEspecial(ui.item.ArtCodigo);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}
function CargarCuadroInventarioEspecial(strProducto) {
    var strOperador = parent.document.getElementById("hdnUser").value;
    var intTotalStockFisico = 0;
    var intTotalStockPedido = 0;
    var intTotalStockOrdenado = 0;
    var intTotalStockDisponible = 0;
    var strTabla = "";
    strTabla += '<table id="tblInventario" border="0px">';
    strTabla += '<tr class="trTituloSeccion"><td class="tdLeft" colspan="9">Inventario de Productos en Bodega(s)</td></tr>';
    strTabla += '<tr class="trTituloCabecera">';
    strTabla += '<td class="tdLeft" ></td>';
    strTabla += '<td class="tdLeft" >Codigo</td>';
    strTabla += '<td class="tdLeft" >Bodega</td>';
    strTabla += '<td class="tdRight">Fisico #</td>';
    strTabla += '<td class="tdRight">Comprometido #</td>';
    strTabla += '<td class="tdRight">Ordenado #</td>';
    strTabla += '<td class="tdRight">Disponible #</td>';
    strTabla += '<td class="tdRight">Moneda</td>';
    strTabla += '<td class="tdRight">Costo Comercial</td>';
    strTabla += '</tr>';
    var intEntro = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvInventario.asmx/ListarInventario",
        dataType: "xml",
        data: { "strProducto": strProducto, "strOperador": strOperador },
        success: function (xml) {
            $(xml).find("ArrayOfClsInventario").each(function () {
                $(this).find("clsInventario").each(function () {
                    intEntro = 1;
                    var $registro = $(this);
                    var strBodAutoriza = $registro.find("BodAutoriza").text();
                    var strBodCodigo = $registro.find("BodCodigo").text();
                    var strBodNombre = $registro.find("BodNombre").text();
                    var intArtStockFisico = $registro.find("ArtStockFisico").text();
                    var intArtStockPedido = $registro.find("ArtStockPedido").text();
                    var intArtStockOrdenado = $registro.find("ArtStockOrdenado").text();
                    var intArtStockDisponible = $registro.find("ArtStockDisponible").text();
                    var strArtMoneda = $registro.find("ArtMoneda").text();
                    var strArtCosto = $registro.find("ArtCosto").text();
                    intTotalStockFisico += parseInt(intArtStockFisico);
                    intTotalStockPedido += parseInt(intArtStockPedido);
                    intTotalStockOrdenado += parseInt(intArtStockOrdenado);
                    intTotalStockDisponible += parseInt(intArtStockDisponible);
                    strTabla += '<tr id="tr' + strBodCodigo + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);" class="trTituloDetalle">';
                    strTabla += '<td></td>';
                    strTabla += '<td class="tdLeft">' + strBodCodigo + '</td>';
                    strTabla += '<td class="tdLeft">' + strBodNombre + '</td>';
                    strTabla += '<td class="tdRight">' + intArtStockFisico + '</td>';
                    strTabla += '<td class="tdRight">' + intArtStockPedido + '</td>';
                    strTabla += '<td class="tdRight">' + intArtStockOrdenado + '</td>';
                    strTabla += '<td class="tdRight">' + intArtStockDisponible + '</td>';
                    if (!(strOperador.toLowerCase() == 'kscherpf' || strOperador.toLowerCase() == 'ptornqui')) {
                        strTabla += '<td class="tdRight">---</td>';
                        strTabla += '<td class="tdRight">---</td>';
                    }else{
                        strTabla += '<td class="tdRight">' + strArtMoneda + '</td>';
                        strTabla += '<td class="tdRight">' + strArtCosto + '</td>';
                    }
                    strTabla += '</tr>';
                });
            });
            if (intEntro == 0) {
                strTabla += "<tr><td colspan='9'>No hay informacion...</td></tr>";
            }else{
                strTabla += '<tr><td colspan="9"><hr></td></tr>';
                strTabla += '<tr class="trTituloDetalle">';
                strTabla += '<td class="tdLeft"></td>';
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