$(document).ready(function () {
    var intPerfil = parseInt(parent.document.getElementById("hdnPerfil").value);
    if (intPerfil == 1 || intPerfil == 2) {
        $("#divAgregarCliente").show();
    }
    else {
        $("#divAgregarCliente").hide();
    } 
    $("#divLoader").css({
        display: "none"
    });
    CargarComboMoneda();
    CargarComboRegion();
    CargarTextoCliente();
});
function CargarTextoCliente() {
    $("#txtBuscaCliente").autocomplete({
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
                            CliCodigo:    item.CliCodigo,
                            CliNombre:    item.CliNombre,
                            CliRut:       item.CliRut,
                            CliBloqueado: item.CliBloqueado,
                            CliTelefono:  item.CliTelefono,
                            CliCorreo:    item.CliCorreo,
                            CliGiro:      item.CliGiro,
                            CliMoneda:    item.CliMoneda,
                            CatCodigo:    item.CatCodigo,
                            CatNombre:    item.CatNombre,
                            CliPlazoVentaNombre: item.CliPlazoVentaNombre,
                            GruNombre:   item.GruNombre
                        }
                    }))
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            $("#txtBuscacliente").html(" ");
            $("#divCodigoCliente").html(ui.item.CliCodigo);
            $("#divRutCliente").html(ui.item.CliRut);
            $("#divNombreCliente").html(ui.item.CliNombre);
            if (ui.item.CliBloqueado == false) {
                $("#divBloqueoCliente").html("NO");
            } else {
                $("#divBloqueoCliente").html("SI");
            }
            if (ui.item.CliMoneda == "##") {
                $("#divMonedaCliente").html("CLP");
            } else {
                $("#divMonedaCliente").html(ui.item.CliMoneda);
            }
            $("#divTelefonoCliente").html(ui.item.CliTelefono);
            $("#divCorreoCliente").html(ui.item.CliCorreo);
            $("#divGiroCliente").html(ui.item.CliGiro); 
            $("#divCodigoCategoria").html(ui.item.CatCodigo + " - " + ui.item.CatNombre);
            $("#divPlazoVentaNombre").html(ui.item.CliPlazoVentaNombre);
            $("#divNombreGrupo").html(ui.item.GruNombre);
            $("#tblDatos").show();
            if (ui.item.CliCodigo != "") {
                CargarTextoLineaCredito(ui.item.CliCodigo);
                CargarCuadroCabeceraDireccionFactura();
                CargarCuadroDetalleDireccionFactura(ui.item.CliCodigo);
                CargarCuadroCabeceraDireccionDespacho();
                CargarCuadroDetalleDireccionDespacho(ui.item.CliCodigo);
            }           
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}
function CargarTextoLineaCredito(strCliente) {
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
                    $("#divAutorizado").html($registro.find("CliAutorizado").text());
                    $("#divUtilizado").html($registro.find("CliUtilizado").text());
                    $("#divDisponible").html($registro.find("CliDisponible").text());    
                });
            });
        }
        });
}
function CargarCuadroCabeceraDireccionFactura() {
    var strTabla = "";
    strTabla += '<table id="tblDireccionFacturacion" border="0px" style="width:800px">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="6">Direccion de Facturacion</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdLeft"   style="width:350px">Calle</td>';
    strTabla += '       <td class="tdLeft"   style="width:200px">Comuna</td>';
    strTabla += '       <td class="tdLeft"   style="width:210px">Region</td>';
    strTabla += '       <td class="tdCenter" style="width:40px">Pais</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr><td id="tdDireccionFacturacion" colspan="6">Cargando...</td></tr>';
    strTabla += '</table>';
    $("#divCuadroResumenDireccionFactura").html(strTabla);
}
function CargarCuadroDetalleDireccionFactura(strCodigoCliente) {
    var intOpcion = 40;
    var strTabla = "";
    var blnHayDatos = false;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvCliente.asmx/ObtenerClienteDireccion",
        dataType: "xml", 
        data: { "intOpcion": intOpcion, "strCliente": strCodigoCliente },
        success: function (xml) {
            $(xml).find("ArrayOfClsCliente").each(function () {
                $(this).find("clsCliente").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var strDirCalle = $registro.find("DirCalle").text();
                    var strDirComuna = $registro.find("DirComuna").text();
                    var strDirRegion = $registro.find("DirRegion").text();
                    var strDirPais = $registro.find("DirPais").text();                               
                    strTabla += '<table id="tblDireccionFacturacion" border="0px" style="width:798px">';
                    strTabla += '   <tr id="trDireccionFacturacion" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);">';
                    strTabla += '       <td class="tdLeft"  style="width:355px">' + strDirCalle + '</td>';
                    strTabla += '       <td class="tdLeft"  style="width:200px">' + strDirComuna + '</td>';
                    strTabla += '       <td class="tdLeft"  style="width:205px">' + strDirRegion + '</td>';
                    strTabla += '       <td class="tdRight" style="width:50px">' + strDirPais + '</td>';
                    strTabla += '   </tr>';
                    strTabla += '</table>';
                    document.getElementById("tdDireccionFacturacion").innerHTML = strTabla;
                });
            });
            if (!blnHayDatos) {
                strTabla += "<table id='tblDireccionFacturacion' border='0px' style='width:800px'><tr><td colspan='6'>No existe dirección asociada al cliente</td></tr></table>";
                document.getElementById("tdDireccionFacturacion").innerHTML = strTabla;
            }
        }
    });
}
function CargarCuadroCabeceraDireccionDespacho() {
    var strTabla = "";
    strTabla += '<table id="tblDireccionDespacho" border="0px" style="width:800px">';
    strTabla += '   <tr class="trTituloSeccion">';
    strTabla += '       <td class="tdLeft" colspan="6">Direccion de Despacho</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr class="trTituloCabecera">';
    strTabla += '       <td class="tdLeft"   style="width:350px">Calle</td>';
    strTabla += '       <td class="tdLeft"   style="width:200px">Comuna</td>';
    strTabla += '       <td class="tdLeft"   style="width:210px">Region</td>';
    strTabla += '       <td class="tdCenter" style="width:40px">Pais</td>';
    strTabla += '   </tr>';
    strTabla += '   <tr><td id="tdDireccionDespacho" colspan="6">Cargando...</td></tr>';
    strTabla += '</table>';
    $("#divCuadroResumenDireccionDespacho").html(strTabla);
}
function CargarCuadroDetalleDireccionDespacho(strCodigoCliente) {
    var intOpcion = 50;
    var strTabla = "";
    var blnHayDatos = false;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvCliente.asmx/ObtenerClienteDireccion",
        dataType: "xml",
        data: { "intOpcion": intOpcion, "strCliente": strCodigoCliente },
        success: function (xml) {
            $(xml).find("ArrayOfClsCliente").each(function () {
                $(this).find("clsCliente").each(function () {
                    blnHayDatos = true;
                    var $registro = $(this);
                    var strDirCalle = $registro.find("DirCalle").text();
                    var strDirComuna = $registro.find("DirComuna").text();
                    var strDirRegion = $registro.find("DirRegion").text();
                    var strDirPais = $registro.find("DirPais").text();
                    strTabla += '<table id="tblDireccionDespacho" border="0px" style="width:798px">';
                    strTabla += '   <tr id="trDireccionDespacho" onmouseover="PintarLinea(this,true);" onmouseout="PintarLinea(this,false);">';
                    strTabla += '       <td class="tdLeft"  style="width:355px">' + strDirCalle + '</td>';
                    strTabla += '       <td class="tdLeft"  style="width:200px">' + strDirComuna + '</td>';
                    strTabla += '       <td class="tdLeft"  style="width:205px">' + strDirRegion + '</td>';
                    strTabla += '       <td class="tdRight" style="width:50px">' + strDirPais + '</td>';
                    strTabla += '   </tr>';
                    strTabla += '</table>';
                    document.getElementById("tdDireccionDespacho").innerHTML = strTabla;
                });
            });
            if (!blnHayDatos) {
                strTabla += "<table id='tblDireccionDespacho' border='0px' style='width:800px'><tr><td colspan='6'>No existe dirección asociada al cliente</td></tr></table>";
                document.getElementById("tdDireccionDespacho").innerHTML = strTabla;
            }
        }
    });
}
function AgregarCliente() {
    $("#divAgregarCliente").show();
}
function CargarComboMoneda() {
    var strParametro = "moneda";
    var intIndex = 0;
    var cmbMoneda = document.getElementById("cmbMoneda");
    cmbMoneda.options.length = 0;
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
                    cmbMoneda.options[intIndex] = new Option(ParNombre, ParCodigo);
                    intIndex++;
                });
            });
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
function CrearCliente() {
    if (document.getElementById("txtRut").value == "") {
        alert("Debe digitar rut del cliente.");
        document.getElementById("txtRut").focus();
        return false;
    }
    if (document.getElementById("txtDV").value == "") {
        alert("Debe digitar digito verificador.");
        document.getElementById("txtDV").focus();
        return false;
    }

    var strRut = document.getElementById("txtRut").value;
    var strDV = document.getElementById("txtDV").value;
    var strVerifica = ObtenerDV(strRut);
    if (strDV != strVerifica) {
        alert("Rut o digito verificador no corresponde.");
        document.getElementById("txtRut").focus();
        return false;
    }

    if (document.getElementById("txtNombre").value == "") {
        alert("Debe digitar nombre del cliente.");
        document.getElementById("txtNombre").focus();
        return false;
    }
    if (document.getElementById("txtCorreo").value == "") {
        alert("Debe digitar correo.");
        document.getElementById("txtCorreo").focus();
        return false;
    }
    if (document.getElementById("txtTelefono").value == "") {
        alert("Debe digitar numero de telefono.");
        document.getElementById("txtTelefono").focus();
        return false;
    }
    if (document.getElementById("txtGiro").value == "") {
        alert("Debe digitar giro comercial.");
        document.getElementById("txtGiro").focus();
        return false;
    }

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

    var intUsuario = parseInt(parent.document.getElementById("hdnUsuario").value);
    var intOperador = parseInt(parent.document.getElementById("hdnOperador").value);

    var strNombre = document.getElementById("txtNombre").value;
    var strCorreo = document.getElementById("txtCorreo").value;
    var intTelefono = document.getElementById("txtTelefono").value;
    var strGiro = document.getElementById("txtGiro").value;
    var strMoneda = document.getElementById("cmbMoneda").options[document.getElementById("cmbMoneda").selectedIndex].value;

    var strCalle = document.getElementById("txtCalle").value;
    var strNumero = document.getElementById("txtNumero").value;
    var strComuna = document.getElementById("txtComuna").value;
    var strCiudad = document.getElementById("txtCiudad").value;


    var intRegion = document.getElementById("cmbRegion").options[document.getElementById("cmbRegion").selectedIndex].value;

    //alert(intRegion);

    MostrarProgreso();
    $.ajax({
        type: "post",
        async: true,
        url: "/Services/srvCliente.asmx/RegistrarCliente",
        dataType: "xml",
        data: { 'intUsuario': intUsuario, 'intOperador': intOperador, 'strRut': strRut, 'strDV': strDV, 'strNombre': strNombre, 'strCorreo': strCorreo, 'intTelefono': intTelefono, 'strGiro': strGiro, 'strMoneda': strMoneda, 'strCalle': strCalle, 'strNumero': strNumero, 'strComuna': strComuna, 'strCiudad': strCiudad, 'intRegion': intRegion },
        success: function (xml) {
            OcultarProgreso();
            var strRespuesta = $(xml).find('string').text();
            alert(strRespuesta);
        }
    });
}
function LimpiarDatos() {
    document.getElementById("txtRut").value = "";
    document.getElementById("txtDV").value = "";

    document.getElementById("txtNombre").value = "";
    document.getElementById("txtCorreo").value = "";
    document.getElementById("txtTelefono").value = "";
    document.getElementById("txtGiro").value = "";

    document.getElementById("txtCalle").value = "";
    document.getElementById("txtNumero").value = "";
    document.getElementById("txtComuna").value = "";
    document.getElementById("txtCiudad").value = "";
}

function ConvertirMayuscula() {
    var strDV = document.getElementById("txtDV").value;
    document.getElementById("txtDV").value = strDV.toUpperCase();
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