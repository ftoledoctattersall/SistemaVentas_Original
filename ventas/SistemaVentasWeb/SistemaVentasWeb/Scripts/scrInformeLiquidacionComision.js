$(document).ready(function () {
    CargarComboAño();
    CargarComboMes();
    $("#divLoader").css({
        display: "none"
    });
});
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
    $("#divCuadroDetalleLinea").html("");
    $("#divCuadroDetalleDocumento").html("");
    CargarCuadroResumen();
}
function CargarCuadroResumen() {
    var intEmpleado = parseInt(parent.document.getElementById("hdnEmpleado").value);
    var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);
    var intAño = document.getElementById("cmbAño").options[document.getElementById("cmbAño").selectedIndex].value;
    var intMes = document.getElementById("cmbMes").options[document.getElementById("cmbMes").selectedIndex].value;
    var intIdentificador = 0;
    var strOficina = '';
    var intLinea = 0;
    var strSubLinea = "";
    var strDocto = "";
    var strTabla = "";
    strTabla += '<table id="tblCuadroResumen" border="0px">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="13">Resumen de Pago(s)</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdLeft">Tipo Liquidacion</td>';
    strTabla += '       <td class="tdCenter">Fec.Liquidacion</td>';
    strTabla += '       <td class="tdCenter">Periodo Vta.</td>';
    strTabla += '       <td class="tdRight">Tot.Neto $</td>';
    strTabla += '       <td class="tdRight">Tot.Margen $</td>';
    strTabla += '       <td class="tdRight">Margen %</td>';
    strTabla += '       <td class="tdRight">Com.Individual $</td>';
    strTabla += '       <td class="tdRight">Com.Grupal $</td>';
    strTabla += '       <td class="tdRight">Com.Final $</td>';
    strTabla += '       <td class="tdRight">Prom.Vacaciones $</td>';
    strTabla += '       <td class="tdRight">Sem.Corrida $</td>';
    strTabla += '       <td class="tdRight">Tot.Comision $</td>';
    strTabla += '       <td class="tdCenter">Ver</td>';
    strTabla += '   </tr>';
    var blnHayDatos = false;
    MostrarProgreso();
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvLiquidacionComision.asmx/ObtenerLiquidacionComisionResumen",
        dataType: "xml",
        data: { "intEmpleado": intEmpleado, "intOperador": intOperador, "intAño": intAño, "intMes": intMes, "intIdentificador": intIdentificador, "strOficina": strOficina, "intLinea": intLinea, "strSubLinea": strSubLinea, "strDocto": strDocto },
        success: function (xml) {
            $(xml).find("ArrayOfClsLiquidacionComisionResumen").each(function () {
                $(this).find("clsLiquidacionComisionResumen").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var intLiqIdentificador = $registro.find("LiqIdentificador").text();
                    var intCodOperador = $registro.find("CodOperador").text();
                    var intNomOperador = $registro.find("NomOperador").text();
                    var intLiqAño = $registro.find("LiqAño").text();
                    var intLiqMes = $registro.find("LiqMes").text();
                    var intTotNeto = FormatearNumero($registro.find("TotNeto").text());
                    var intTotCosto = $registro.find("TotCosto").text();
                    var intTotMargen = FormatearNumero($registro.find("TotMargen").text());
                    var intPorMargen = $registro.find("PorMargen").text();
                    var intIndComision = FormatearNumero($registro.find("IndComision").text());
                    var intGruComision = FormatearNumero($registro.find("GruComision").text());
                    var intFinComision = FormatearNumero($registro.find("FinComision").text());
                    var intProVacaciones = FormatearNumero($registro.find("ProVacaciones").text());
                    var intSemCorrida = FormatearNumero($registro.find("SemCorrida").text());
                    var intTotComision = FormatearNumero($registro.find("TotComision").text());
                    var strFecLiquidacion = $registro.find("FecLiquidacion").text();
                    var strLiqOrigen = $registro.find("LiqOrigen").text();
                    var strLiqTipo = $registro.find("LiqTipo").text();
                    strTabla += '<tr id="tr' + intLiqIdentificador + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);">';
                    strTabla += '   <td class="tdLeft">' + strLiqTipo + '</td>';
                    strTabla += '   <td class="tdCenter">' + strFecLiquidacion + '</td>';
                    strTabla += '   <td class="tdCenter">' + ObtenerNombreMesCorto(intLiqMes) + '-' + intLiqAño + '</td>';
                    strTabla += '   <td class="tdRight">' + intTotNeto + '</td>';
                    strTabla += '   <td class="tdRight">' + intTotMargen + '</td>';
                    strTabla += '   <td class="tdRight">' + intPorMargen + '</td>';
                    strTabla += '   <td class="tdRight">' + intIndComision + '</td>';
                    strTabla += '   <td class="tdRight">' + intGruComision + '</td>';
                    strTabla += '   <td class="tdRight">' + intFinComision + '</td>';
                    strTabla += '   <td class="tdRight">' + intProVacaciones + '</td>';
                    strTabla += '   <td class="tdRight">' + intSemCorrida + '</td>';
                    strTabla += '   <td class="tdRight">' + intTotComision + '</td>';
                    if (strLiqOrigen=="E" || strLiqOrigen=="O"){
                        strTabla += '<td class="tdCenter"><img id="img' + intLiqIdentificador + '" width="12px" height="12px" title="Haga clic para visualizar cuadro resumen." alt="Expandir" src="../Images/visualizar.png" onclick="CargarCuadroDetalleLinea(' + intLiqIdentificador + ');"/></td>';
                    }
                    else{
                        strTabla += '<td class="tdCenter"><img id="img' + intLiqIdentificador + '" width="12px" height="12px" title="Haga clic para visualizar cuadro resumen." alt="Expandir" src="../Images/visualizar.png" onclick="CargarCuadroDetalleLineaEspecial(' + intLiqIdentificador + ');"/></td>';
                    }
                    strTabla += '</tr>';
                });
            });
            if (!blnHayDatos) {
                strTabla += '<tr><td colspan="13">No hay informacion segun criterios de busquedas...</td></tr>';
            }
            strTabla += '<tr><td colspan="13"><hr></td></tr>';
            OcultarProgreso();
        }
    });
    strTabla += '</table>';
    $("#divCuadroResumen").html(strTabla);
    $("#divCuadroDetalleLinea").html("");   
}
function CargarCuadroDetalleLinea(intIdentificador) {
    var intEmpleado = parseInt(parent.document.getElementById("hdnEmpleado").value);
    var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);
    var intAño = document.getElementById("cmbAño").options[document.getElementById("cmbAño").selectedIndex].value;
    var intMes = document.getElementById("cmbMes").options[document.getElementById("cmbMes").selectedIndex].value;
    var strOficina = '';
    var intLinea = 0;
    var strSubLinea = "";
    var strDocto = "";
    var intSumaTotNeto = 0;
    var intSumaFinComision = 0;
    var strTabla = "";
    strTabla += '<table id="tblCuadroDetalleLinea" border="0px">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="6">Resumen Pagos por Linea</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdLeft">Linea</td>';
    strTabla += '       <td class="tdLeft">SubLinea</td>';
    strTabla += '       <td class="tdCenter">Tipo Docto.</td>';
    strTabla += '       <td class="tdRight">Tot.Neto $</td>';
    strTabla += '       <td class="tdRight">Com.Final $</td>';
    strTabla += '       <td class="tdCenter">Ver</td>';
    strTabla += '   </tr>';
    var blnHayDatos = false;
    MostrarProgreso();
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvLiquidacionComision.asmx/ObtenerLiquidacionComisionDetalleLinea",
        dataType: "xml",
        data: { "intEmpleado": intEmpleado, "intOperador": intOperador, "intAño": intAño, "intMes": intMes, "intIdentificador": intIdentificador, "strOficina": strOficina, "intLinea": intLinea, "strSubLinea": strSubLinea, "strDocto": strDocto },
        success: function (xml) {
            $(xml).find("ArrayOfClsLiquidacionComisionDetalle").each(function () {
                $(this).find("clsLiquidacionComisionDetalle").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var intLiqIdentificador = $registro.find("LiqIdentificador").text();
                    var intLinCodigo = $registro.find("LinCodigo").text();
                    var strLinNombre = $registro.find("LinNombre").text();
                    var strSubNombre = $registro.find("SubNombre").text();
                    var strDocTipo = $registro.find("DocTipo").text();
                    var intTotNeto = $registro.find("TotNeto").text();
                    var intFinComision = $registro.find("FinComision").text();
                    intSumaTotNeto += parseInt(intTotNeto);
                    intSumaFinComision += parseInt(intFinComision);
                    strTabla += '<tr id="tr' + intLiqIdentificador + intLinCodigo + strSubNombre + strDocTipo + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);">';
                    strTabla += '   <td class="tdLeft">' + strLinNombre + '</td>';
                    strTabla += '   <td class="tdLeft">' + strSubNombre + '</td>';
                    strTabla += '   <td class="tdCenter">' + strDocTipo + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intTotNeto) + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intFinComision) + '</td>';
                    strTabla += '   <td class="tdCenter"><img id="img' + intLiqIdentificador + intLinCodigo + strSubNombre + strDocTipo + '" width="12px" height="12px" title="Haga clic para visualizar cuadro detalle." alt="Expandir" src="../Images/visualizar.png" onclick="CargarCuadroDetalleDocumento(' + "'" + intLiqIdentificador + "'" + "," + "'" + intLinCodigo + "'" + "," + "'" + strSubNombre + "'" + "," + "'" + strDocTipo + "'" + ');"/></td>';
                    strTabla += '</tr>';
                });
            });
            OcultarProgreso();
        }
    });
    if (!blnHayDatos) {
        strTabla += '<tr><td colspan="6">No hay informacion segun criterios de busquedas...</td></tr>';
        strTabla += '<tr><td colspan="6"><hr></td></tr>';
    }
    else {
        strTabla += '<tr><td colspan="6"><hr></td></tr>';
        strTabla += '<tr><td colspan="3" class="tdLeft">TOTALES</td><td class="tdRight">' + FormatearNumero(intSumaTotNeto) + '</td><td class="tdRight">' + FormatearNumero(intSumaFinComision) + '</td></tr>';
    } 
    strTabla += '</table>';
    $("#divCuadroDetalleLinea").html(strTabla);
    $("#divCuadroDetalleDocumento").html("");
}
function CargarCuadroDetalleDocumento(intIdentificador, intLinea, strSubLinea, strDocto) {
    var intEmpleado = parseInt(parent.document.getElementById("hdnEmpleado").value);
    var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);
    var intAño = document.getElementById("cmbAño").options[document.getElementById("cmbAño").selectedIndex].value;
    var intMes = document.getElementById("cmbMes").options[document.getElementById("cmbMes").selectedIndex].value;
    var strOficina = '';
    var intSumaTotNeto = 0;
    var intSumaTotMargen = 0;
    var intSumaFinComision = 0;
    var strTabla = "";
    strTabla += '<table id="tblCuadroDetalleDocumento" border="0px">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="16">Detalle Pagos Doctos. por Linea</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdLeft">Oficina</td>';
    strTabla += '       <td class="tdCenter">Fec.Venta</td>';
    strTabla += '       <td class="tdCenter">Docto.</td>';
    strTabla += '       <td class="tdRight">Folio N°</td>';
    strTabla += '       <td class="tdCenter">Fec.4ta.Copia</td>';
    strTabla += '       <td class="tdLeft">Cliente</td>';
    strTabla += '       <td class="tdLeft">Bodega</td>';
    strTabla += '       <td class="tdLeft">Articulo</td>';
    strTabla += '       <td class="tdLeft">Envase</td>';
    strTabla += '       <td class="tdRight">Cant.#</td>';
    strTabla += '       <td class="tdRight">Neto $</td>';
    strTabla += '       <td class="tdRight">Tot.Neto $</td>';
    strTabla += '       <td class="tdRight">Tot.Margen $</td>';
    strTabla += '       <td class="tdRight">Comision %</td>';
    strTabla += '       <td class="tdRight">Base Pago</td>';
    strTabla += '       <td class="tdRight">Com.Final $</td>';
    strTabla += '   </tr>';
    var blnHayDatos = false;
    MostrarProgreso();
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvLiquidacionComision.asmx/ObtenerLiquidacionComisionDetalleDocumento",
        dataType: "xml",
        data: { "intEmpleado": intEmpleado, "intOperador": intOperador, "intAño": intAño, "intMes": intMes, "intIdentificador": intIdentificador, "strOficina": strOficina, "intLinea": intLinea, "strSubLinea": strSubLinea, "strDocto": strDocto },
        success: function (xml) {
            $(xml).find("ArrayOfClsLiquidacionComisionDetalle").each(function () {
                $(this).find("clsLiquidacionComisionDetalle").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var intLiqIdentificador = $registro.find("LiqIdentificador").text();
                    var intLinCodigo = $registro.find("LinCodigo").text();
                    var strLinNombre = $registro.find("LinNombre").text();
                    var strSubNombre = $registro.find("SubNombre").text();
                    var strDocTipo = $registro.find("DocTipo").text();
                    var intTotNeto = $registro.find("TotNeto").text();
                    var intFinComision = $registro.find("FinComision").text();
                    var strOfiNombre = $registro.find("OfiNombre").text();
                    var strDocFecha = $registro.find("DocFecha").text();
                    var intFolNumero = $registro.find("FolNumero").text();
                    var strDoc4taCopia = $registro.find("Doc4taCopia").text();
                    var strCliNombre = $registro.find("CliNombre").text();
                    var strBodNombre = $registro.find("BodNombre").text();
                    var strArtNombre = $registro.find("ArtNombre").text();
                    var strArtMedida = $registro.find("ArtMedida").text();
                    var intArtCantidad = $registro.find("ArtCantidad").text();
                    var intVenNeto = $registro.find("VenNeto").text();
                    var intVenMargen = $registro.find("VenMargen").text();
                    var dblPorComision = $registro.find("PorComision").text();
                    var strBasComision = $registro.find("BasComision").text();
                    intSumaTotNeto += parseInt(intTotNeto);
                    intSumaTotMargen += parseInt(intVenMargen);
                    intSumaFinComision += parseInt(intFinComision);
                    strTabla += '<tr id="tr' + intLiqIdentificador + intLinCodigo + intFolNumero + strDocTipo + strArtNombre + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);">';
                    strTabla += '   <td class="tdLeft">' + strOfiNombre + '</td>';
                    strTabla += '   <td class="tdCenter">' + strDocFecha + '</td>';
                    strTabla += '   <td class="tdCenter">' + strDocTipo + '</td>';
                    strTabla += '   <td class="tdRight">' + intFolNumero + '</td>';
                    strTabla += '   <td class="tdCenter">' + strDoc4taCopia + '</td>';
                    strTabla += '   <td class="tdLeft">' + strCliNombre + '</td>';
                    strTabla += '   <td class="tdLeft">' + strBodNombre + '</td>';
                    strTabla += '   <td class="tdLeft">' + strArtNombre + '</td>';
                    strTabla += '   <td class="tdLeft">' + strArtMedida + '</td>';
                    strTabla += '   <td class="tdRight">' + intArtCantidad + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intVenNeto) + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intTotNeto) + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intVenMargen) + '</td>';
                    strTabla += '   <td class="tdRight">' + dblPorComision + '</td>';
                    strTabla += '   <td class="tdCenter">' + strBasComision + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intFinComision) + '</td>';
                    strTabla += '</tr>';
                });
            });
            OcultarProgreso();
        }
    });
    if (!blnHayDatos) {
        strTabla += '<tr><td colspan="16">No hay informacion segun criterios de busquedas...</td></tr>';
        strTabla += '<tr><td colspan="16"><hr></td></tr>';
    }
    else {
        strTabla += '<tr><td colspan="16"><hr></td></tr>';
        strTabla += '<tr><td colspan="11" class="tdLeft">TOTALES</td><td class="tdRight">' + FormatearNumero(intSumaTotNeto) + '</td><td class="tdRight">' + FormatearNumero(intSumaTotMargen)+'</td><td colspan="2"></td><td class="tdRight">' + FormatearNumero(intSumaFinComision) + '</td></tr>';
    } 
    strTabla += '</table>';
    $("#divCuadroDetalleDocumento").html(strTabla);
}
function CargarCuadroDetalleLineaEspecial(intIdentificador) {
    var intEmpleado = parseInt(parent.document.getElementById("hdnEmpleado").value);
    var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);
    var intAño = document.getElementById("cmbAño").options[document.getElementById("cmbAño").selectedIndex].value;
    var intMes = document.getElementById("cmbMes").options[document.getElementById("cmbMes").selectedIndex].value;
    var strOficina = '';
    var intLinea = 0;
    var strSubLinea = "";
    var strDocto = "";
    var intSumaTotNeto = 0;
    var intSumaTotMargen = 0;
    var intSumaFinComision = 0;
    var strTabla = "";
    strTabla += '<table id="tblCuadroDetalleLinea" border="0px">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="8">Resumen Pagos por Linea</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdLeft">Linea</td>';
    strTabla += '       <td class="tdRight">Tot.Neto $</td>';
    strTabla += '       <td class="tdRight">Tot.Margen $</td>';
    strTabla += '       <td class="tdRight">Comision %</td>';
    strTabla += '       <td class="tdCenter">Base Pago</td>';
    strTabla += '       <td class="tdLeft">Oficina</td>';
    strTabla += '       <td class="tdRight">Com.Final $</td>';
    strTabla += '       <td class="tdCenter">Ver</td>';
    strTabla += '   </tr>';
    var blnHayDatos = false;
    MostrarProgreso();
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvLiquidacionComision.asmx/ObtenerLiquidacionComisionDetalleLineaEspecial",
        dataType: "xml",
        data: { "intEmpleado": intEmpleado, "intOperador": intOperador, "intAño": intAño, "intMes": intMes, "intIdentificador": intIdentificador, "strOficina": strOficina, "intLinea": intLinea, "strSubLinea": strSubLinea, "strDocto": strDocto },
        success: function (xml) {
            $(xml).find("ArrayOfClsLiquidacionComisionDetalle").each(function () {
                $(this).find("clsLiquidacionComisionDetalle").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var intLiqIdentificador = $registro.find("LiqIdentificador").text();
                    var intLinCodigo = $registro.find("LinCodigo").text();
                    var strLinNombre = $registro.find("LinNombre").text();
                    var intOfiCodigo = $registro.find("OfiCodigo").text();
                    var strOfiNombre = $registro.find("OfiNombre").text();
                    var intTotNeto = $registro.find("TotNeto").text();
                    var intTotMargen = $registro.find("TotMargen").text();
                    var dblPorComision = $registro.find("PorComision").text();
                    var strBasComision = $registro.find("BasComision").text();
                    var strNivComision = $registro.find("NivComision").text();
                    var intFinComision = $registro.find("FinComision").text();
                    intSumaTotNeto += parseInt(intTotNeto);
                    intSumaTotMargen += parseInt(intTotMargen);
                    intSumaFinComision += parseInt(intFinComision);
                    strTabla += '<tr id="tr' + intLiqIdentificador + strOfiNombre + intLinCodigo + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);">';
                    strTabla += '   <td class="tdLeft">' + strLinNombre + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intTotNeto) + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intTotMargen) + '</td>';
                    strTabla += '   <td class="tdRight">' + dblPorComision + '</td>';
                    strTabla += '   <td class="tdCenter">' + strBasComision + '</td>';
                    strTabla += '   <td class="tdLeft">' + strOfiNombre + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intFinComision) + '</td>';
                    strTabla += '   <td class="tdCenter"><img id="img' + intLiqIdentificador + strOfiNombre + intLinCodigo + '" width="12px" height="12px" title="Haga clic para visualizar cuadro detalle." alt="Expandir" src="../Images/visualizar.png" onclick="CargarCuadroDetalleDocumentoEspecial(' + "'" + intLiqIdentificador + "'" + "," + "'" + strOfiNombre + "'" + "," + "'" + intLinCodigo + "'" + ');"/></td>';
                    strTabla += '</tr>';
                });
            });
            OcultarProgreso();
        }
    });
    if (!blnHayDatos) {
        strTabla += '<tr><td colspan="8">No hay informacion segun criterios de busquedas...</td></tr>';
        strTabla += '<tr><td colspan="8"><hr></td></tr>';
    }
    else {
        strTabla += '<tr><td colspan="8"><hr></td></tr>';
        strTabla += '<tr><td colspan="1" class="tdLeft">TOTALES</td><td class="tdRight">' + FormatearNumero(intSumaTotNeto) + '</td><td class="tdRight">' + FormatearNumero(intSumaTotMargen) + '</td><td colspan="3"></td><td class="tdRight">' + FormatearNumero(intSumaFinComision) + '</td></tr>';
    }
    strTabla += '</table>';
    $("#divCuadroDetalleLinea").html(strTabla);
    $("#divCuadroDetalleDocumento").html("");
}
function CargarCuadroDetalleDocumentoEspecial(intIdentificador, strOficina, intLinea) {
    var intEmpleado = parseInt(parent.document.getElementById("hdnEmpleado").value);
    var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);
    var intAño = document.getElementById("cmbAño").options[document.getElementById("cmbAño").selectedIndex].value;
    var intMes = document.getElementById("cmbMes").options[document.getElementById("cmbMes").selectedIndex].value;
    var strSubLinea = "";
    var strDocto = "";
    var intSumaTotNeto = 0;
    var intSumaTotMargen = 0;
    var intSumaFinComision = 0;
    var strTabla = "";
    strTabla += '<table id="tblCuadroDetalleDocumento" border="0px">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="16">Detalle Pagos Doctos. por Linea</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdLeft">Oficina</td>';
    strTabla += '       <td class="tdCenter">Fec.Venta</td>';
    strTabla += '       <td class="tdCenter">Docto.</td>';
    strTabla += '       <td class="tdRight">Folio N°</td>';
    strTabla += '       <td class="tdCenter">Fec.4ta.Copia</td>';
    strTabla += '       <td class="tdLeft">Cliente</td>';
    strTabla += '       <td class="tdLeft">Bodega</td>';
    strTabla += '       <td class="tdLeft">Articulo</td>';
    strTabla += '       <td class="tdLeft">Envase</td>';
    strTabla += '       <td class="tdRight">Cant.#</td>';
    strTabla += '       <td class="tdRight">Neto $</td>';
    strTabla += '       <td class="tdRight">Tot.Neto $</td>';
    strTabla += '       <td class="tdRight">Tot.Margen $</td>';
    strTabla += '       <td class="tdRight">Comision %</td>';
    strTabla += '       <td class="tdRight">Base Pago</td>';
    strTabla += '       <td class="tdRight">Com.Final $</td>';
    strTabla += '   </tr>';
    var blnHayDatos = false;
    MostrarProgreso();
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvLiquidacionComision.asmx/ObtenerLiquidacionComisionDetalleDocumentoEspecial",
        dataType: "xml",
        data: { "intEmpleado": intEmpleado, "intOperador": intOperador, "intAño": intAño, "intMes": intMes, "intIdentificador": intIdentificador, "strOficina": strOficina, "intLinea": intLinea, "strSubLinea": strSubLinea, "strDocto": strDocto },
        success: function (xml) {
            $(xml).find("ArrayOfClsLiquidacionComisionDetalle").each(function () {
                $(this).find("clsLiquidacionComisionDetalle").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var intLiqIdentificador = $registro.find("LiqIdentificador").text();
                    var intLinCodigo = $registro.find("LinCodigo").text();
                    var strLinNombre = $registro.find("LinNombre").text();
                    var strSubNombre = $registro.find("SubNombre").text();
                    var strDocTipo = $registro.find("DocTipo").text();
                    var intTotNeto = $registro.find("TotNeto").text();
                    var intFinComision = $registro.find("FinComision").text();
                    var strOfiNombre = $registro.find("OfiNombre").text();
                    var strDocFecha = $registro.find("DocFecha").text();
                    var intFolNumero = $registro.find("FolNumero").text();
                    var strDoc4taCopia = $registro.find("Doc4taCopia").text();
                    var strCliNombre = $registro.find("CliNombre").text();
                    var strBodNombre = $registro.find("BodNombre").text();
                    var strArtNombre = $registro.find("ArtNombre").text();
                    var strArtMedida = $registro.find("ArtMedida").text();
                    var intArtCantidad = $registro.find("ArtCantidad").text();
                    var intVenNeto = $registro.find("VenNeto").text();
                    var intVenMargen = $registro.find("VenMargen").text();
                    var dblPorComision = $registro.find("PorComision").text();
                    var strBasComision = $registro.find("BasComision").text();
                    intSumaTotNeto += parseInt(intTotNeto);
                    intSumaTotMargen += parseInt(intVenMargen);
                    intSumaFinComision += parseInt(intFinComision);
                    strTabla += '<tr id="tr' + intLiqIdentificador + intLinCodigo + intFolNumero + strDocTipo + strArtNombre + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);">';
                    strTabla += '   <td class="tdLeft">' + strOfiNombre + '</td>';
                    strTabla += '   <td class="tdCenter">' + strDocFecha + '</td>';
                    strTabla += '   <td class="tdCenter">' + strDocTipo + '</td>';
                    strTabla += '   <td class="tdRight">' + intFolNumero + '</td>';
                    strTabla += '   <td class="tdCenter">' + strDoc4taCopia + '</td>';
                    strTabla += '   <td class="tdLeft">' + strCliNombre + '</td>';
                    strTabla += '   <td class="tdLeft">' + strBodNombre + '</td>';
                    strTabla += '   <td class="tdLeft">' + strArtNombre + '</td>';
                    strTabla += '   <td class="tdLeft">' + strArtMedida + '</td>';
                    strTabla += '   <td class="tdRight">' + intArtCantidad + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intVenNeto) + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intTotNeto) + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intVenMargen) + '</td>';
                    strTabla += '   <td class="tdRight">' + dblPorComision + '</td>';
                    strTabla += '   <td class="tdCenter">' + strBasComision + '</td>';
                    strTabla += '   <td class="tdRight">' + FormatearNumero(intFinComision) + '</td>';
                    strTabla += '</tr>';
                });
            });
            OcultarProgreso();
        }
    });
    if (!blnHayDatos) {
        strTabla += '<tr><td colspan="16">No hay informacion segun criterios de busquedas...</td></tr>';
        strTabla += '<tr><td colspan="16"><hr></td></tr>';
    }
    else {
        strTabla += '<tr><td colspan="16"><hr></td></tr>';
        strTabla += '<tr><td colspan="11" class="tdLeft">TOTALES</td><td class="tdRight">' + FormatearNumero(intSumaTotNeto) + '</td><td class="tdRight">' + FormatearNumero(intSumaTotMargen) + '</td><td colspan="2"></td><td class="tdRight">' + FormatearNumero(intSumaFinComision) + '</td></tr>';
    }
    strTabla += '</table>';
    $("#divCuadroDetalleDocumento").html(strTabla);
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