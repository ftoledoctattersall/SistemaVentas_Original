$(document).ready(function () {
    CargarComboCotizacion();
    Consultar();
});
function CargarComboCotizacion() {
    var strParametro = "cotizacion";
    var intIndex = 0;
    var cmbCotizacion = document.getElementById("cmbCotizacion");
    cmbCotizacion.options.length = 0;
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
                    cmbCotizacion.options[intIndex] = new Option(ParNombre, ParCodigo);
                    intIndex++;
                });
            });
        }
    });
}
function Consultar() {
    CargarCuadro();
}
function CargarCuadro() {
    var intOpcion = 10;
    var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);
    var strEstado = document.getElementById("cmbCotizacion").options[document.getElementById("cmbCotizacion").selectedIndex].value;
    var intCotizacion = 0;
    var strTitulo = document.getElementById("cmbCotizacion").options[document.getElementById("cmbCotizacion").selectedIndex].text;
    var strTabla = "";
    strTabla += '<table id="tblCuadro" border="0px">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="11">COTIZACIONES EN ESTADO ' + strTitulo+'</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdCenter" colspan="2">Id de Origen</td>';
    strTabla += '       <td class="tdCenter" colspan="2">Datos del Cliente</td>';
    strTabla += '       <td class="tdCenter" colspan="7">Datos de la Venta</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdRight">crm</td>';
    strTabla += '       <td class="tdRight">sap</td>';
    strTabla += '       <td class="tdLeft">Codigo</td>';
    strTabla += '       <td class="tdLeft">Nombre</td>';
    strTabla += '       <td class="tdLeft">Tipo</td>';
    strTabla += '       <td class="tdLeft">Docto.</td>';
    strTabla += '       <td class="tdCenter">Fecha</td>';
    strTabla += '       <td class="tdLeft">Despacho</td>';
    strTabla += '       <td class="tdLeft">Plazo Venta</td>';
    strTabla += '       <td class="tdCenter">Moneda Pago</td>';
    strTabla += '       <td class="tdRight">Total CLP</td>';
    strTabla += '   </tr>';
    var blnHayDatos = false;
    MostrarProgreso();
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvCotizacion.asmx/ObtenerCotizacionResumen",
        dataType: "xml",
        data: { "intOpcion": intOpcion, "intOperador": intOperador, "strEstado": strEstado, "intCotizacion": intCotizacion},
        success: function (xml) {
            $(xml).find("ArrayOfClsCotizacionResumen").each(function () {
                $(this).find("clsCotizacionResumen").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var intCmrCodigo = $registro.find("CmrCodigo").text();
                    var intCotCodigo = $registro.find("CotCodigo").text();
                    var strCliCodigo = $registro.find("CliCodigo").text();
                    var strCliNombre = $registro.find("CliNombre").text();
                    var strEmiNombre = $registro.find("EmiNombre").text();
                    var strCotFecha = $registro.find("CotFecha").text();
                    var strVenTipo = $registro.find("VenTipo").text();
                    var strDesNombre = $registro.find("DesNombre").text();
                    var strPlaNombre = $registro.find("PlaNombre").text();
                    var strPagMoneda = $registro.find("PagMoneda").text();
                    var dblDocTotal = $registro.find("DocTotal").text();
                    strTabla += '<tr id="tr' + intCotCodigo + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);" onclick="CargarPaginaVenta(' + "'" + intCotCodigo + "'" + "," + "'" + strVenTipo + "','" + strEstado + "'" + ');">';
                    strTabla += '   <td class="tdRight">' + intCmrCodigo + '</td>';
                    strTabla += '   <td class="tdRight">' + intCotCodigo + '</td>';
                    strTabla += '   <td class="tdLeft">' + strCliCodigo + '</td>';
                    strTabla += '   <td class="tdLeft">' + strCliNombre + '</td>';
                    strTabla += '   <td class="tdLeft">' + strVenTipo + '</td>';
                    strTabla += '   <td class="tdLeft">' + strEmiNombre + '</td>';
                    strTabla += '   <td class="tdCenter">' + strCotFecha + '</td>';
                    strTabla += '   <td class="tdLeft">' + strDesNombre + '</td>';
                    strTabla += '   <td class="tdLeft">' + strPlaNombre + '</td>';
                    strTabla += '   <td class="tdCenter">' + strPagMoneda + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(dblDocTotal) + '</td>';
                    strTabla += '</tr>';
                });
            });
            OcultarProgreso();
        }
    });
    if (!blnHayDatos) {
        strTabla += '<tr><td colspan="11">No hay informacion segun criterios de busquedas...</td></tr>';
        strTabla += '<tr><td colspan="11"><hr></td></tr>';
    }
    strTabla += '</table>';
    $("#divCuadro").html(strTabla);
}
function CargarPaginaVenta(intCotizacion, strTipoVenta, strEstado) {
    if (strEstado == 'P') {
        var strPagina = "pagVenta" + strTipoVenta.replace(/\s+/g, '') + ".aspx?prmCotizacion=" + intCotizacion;
        //alert(strPagina);
        window.location.href = strPagina;
    }
    else {
        alert("Cualquier estado que no sea pendiente debe Visualizar detalle.");
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