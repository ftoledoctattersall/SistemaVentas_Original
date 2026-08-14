$(document).ready(function () {
    CargarComboOperador();
    CargarComboAño();
    CargarComboMes();
    ValidarAdministrativo();
    $("#divLoader").css({
        display: "none"
    });
});
function ValidarAdministrativo() {
    var intEmpleado = parseInt(parent.document.getElementById("hdnEmpleado").value);
    var intOficina = parseInt(parent.document.getElementById("hdnOficina").value);
    var strTabla = "";
    var blnHayDatos = false;
    MostrarProgreso();
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvEmpleado.asmx/ObtenerEmpleadoOficina",
        dataType: "xml",
        data: { "intEmpleado": intEmpleado, "intOficina": intOficina },
        success: function (xml) {
            $(xml).find("ArrayOfClsEmpleado").each(function () {
                $(this).find("clsEmpleado").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var intEmpCodigo = $registro.find("EmpCodigo").text();
                    var strEmpNombre = $registro.find("EmpNombre").text();
                });
            });
            if (!blnHayDatos) {
                strTabla += '<table id="tblResumen" border="0px">';
                strTabla += '   <tr><td>Solo el administrativo de la oficina esta autorizado para ingresar cuartas copias...</td></tr>';
                strTabla += '</table>';
                document.getElementById("btnConsultar").style.visibility = "hidden";
            }
            OcultarProgreso();
        }
    });
    $("#divCuadroResumen").html(strTabla);
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
function CargarComboOperador() {
    var intOperador = 0;
    var intOficina = parent.document.getElementById("hdnOficina").value;
    var intIndex = 0;
    var cmbOperador = document.getElementById("cmbOperador");
    cmbOperador.options.length = 0;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvOperador.asmx/ObtenerOperadorOficina",
        dataType: "xml",
        data: { "intOperador": intOperador, "intOficina": intOficina },
        success: function (xml) {
            intIndex = 0;
            $(xml).find("ArrayOfClsOperador").each(function () {
                $(this).find("clsOperador").each(function () {
                    var $registro = $(this);
                    var OpeCodigo = $registro.find("OpeCodigo").text();
                    var OpeNombre = $registro.find("OpeNombre").text();
                    cmbOperador.options[intIndex] = new Option(OpeNombre, OpeCodigo);
                    intIndex++;
                });
            });
        }
    });
}
function Consultar() {
    CargarCuadroDatos();
}
function CargarCuadroDatos() {
    var intOperador = document.getElementById("cmbOperador").options[document.getElementById("cmbOperador").selectedIndex].value;
    var intAño = document.getElementById("cmbAño").options[document.getElementById("cmbAño").selectedIndex].value;
    var intMes = document.getElementById("cmbMes").options[document.getElementById("cmbMes").selectedIndex].value;
    var strTabla = "";
    strTabla += '<table id="tblResumen" border="0px">';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdLeft">Cliente</td>';
    strTabla += '       <td class="tdCenter">Fec.Venta</td>';
    strTabla += '       <td class="tdCenter">Tipo Docto.</td>';
    strTabla += '       <td class="tdCenter">Nro.Docto.</td>';
    strTabla += '       <td class="tdCenter">Nro.Folio</td>';
    strTabla += '       <td class="tdRight">Total Neto</td>';
    strTabla += '       <td class="tdCenter">Ingresar</td>';
    strTabla += '       <td class="tdCenter">Fec.4ta.Copia</td>';
    strTabla += '   </tr>';
    var blnHayDatos = false;
    MostrarProgreso();
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvCuartaCopia.asmx/ObtenerCuartaCopia",
        dataType: "xml",
        data: { "intOperador": intOperador, "intAño": intAño, "intMes": intMes },
        success: function (xml) {
            $(xml).find("ArrayOfClsCuartaCopia").each(function () {
                $(this).find("clsCuartaCopia").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var intOpeCodigo = $registro.find("OpeCodigo").text();
                    var strOpeNombre = $registro.find("OpeNombre").text();
                    var strCliCodigo = $registro.find("CliCodigo").text();
                    var strCliNombre = $registro.find("CliNombre").text();
                    var strDocFecha = $registro.find("DocFecha").text();
                    var strDocTipo = $registro.find("DocTipo").text();
                    var intDocNumero = $registro.find("DocNumero").text();
                    var intDocFolio = $registro.find("DocFolio").text();
                    var intDocNeto = FormatearNumero($registro.find("DocNeto").text());
                    var strDoc4taCopia = $registro.find("Doc4taCopia").text();
                    var strChecked = "";
                    if (strDoc4taCopia != "----------") {
                        strChecked = "checked";
                    }
                    strTabla += '<tr id="tr' + intDocFolio + '" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);">';
                    strTabla += '   <td class="tdLeft">' + strCliNombre + '</td>';
                    strTabla += '   <td class="tdCenter">' + strDocFecha + '</td>';
                    strTabla += '   <td class="tdCenter">' + strDocTipo + '</td>';
                    strTabla += '   <td class="tdCenter">' + intDocNumero + '</td>';
                    strTabla += '   <td class="tdCenter">' + intDocFolio + '</td>';
                    strTabla += '   <td class="tdRight">' + intDocNeto + '</td>';
                    strTabla += '   <td class="tdCenter"><input name="checkbox" id="chk' + intDocFolio + '" type="checkbox" value="' + intOpeCodigo + "|" + strCliCodigo + "|" + strDocTipo + "|" + intDocNumero + "|" + intDocFolio + '"'+ strChecked + '/></td>';
                    strTabla += '   <td class="tdCenter">' + strDoc4taCopia + '</td>';
                    strTabla += '</tr>';
                });
            });
            if (!blnHayDatos) {
                strTabla += '<tr><td colspan="8">No hay informacion segun criterios de busquedas...</td></tr>';
            }
            else {
                strTabla += '<tr><td colspan="8"><hr></td></tr>';
                strTabla += '<tr><td class="tdRight" colspan="8"><input id="btnGrabar" type="button" value="Registrar" onclick="ChequearCuartaCopia();"/></td></tr>';
            }
            OcultarProgreso();
        }
    });
    strTabla += '</table>';
    $("#divCuadroResumen").html(strTabla);
}
function ChequearCuartaCopia() {
    var strDatos = "";
    var intIngreso = 0;
    var arrCheckBox = document.getElementById("frmIngresoCuartaCopia").checkbox;
    for (var intIndice = 0; intIndice < arrCheckBox.length; intIndice++) {
        strDatos = arrCheckBox[intIndice].value;
        var arrDatos = strDatos.split("|");
        if (arrCheckBox[intIndice].checked) {            
            intIngreso = 1;         
        }
        else {
            intIngreso = 0;
        }
        ActualizarCuartaCopia(arrDatos[0], arrDatos[1], arrDatos[2], arrDatos[3], arrDatos[4], intIngreso)
    }
    CargarCuadroDatos();
}
function ActualizarCuartaCopia(intOperador, strCliente, strTipo, intDocto, intFolio, intIngreso) {
    //alert(intOperador + "," + strCliente + "," + strTipo + "," + intDocto + "," + intFolio + "," + intIngreso);
    MostrarProgreso();
    $.ajax({
        type: "post",
        async: true,
        url: "/Services/srvCuartaCopia.asmx/ActualizarCuartaCopia",
        dataType: "xml",
        data: { 'intOperador': intOperador, 'strCliente': strCliente, 'strTipo': strTipo, 'intDocto': intDocto, 'intFolio': intFolio, 'intIngreso': intIngreso },
        success: function (xml) {
            OcultarProgreso();
            var strRespuesta = $(xml).find('string').text();
            //alert(strRespuesta);
        }
    });
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