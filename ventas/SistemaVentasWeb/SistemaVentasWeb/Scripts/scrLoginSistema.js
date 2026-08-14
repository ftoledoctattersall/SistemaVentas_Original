if (window.screen.availWidth == 1366) {
    window.parent.document.body.style.zoom = "80%";
} else if (window.screen.availWidth == 1280) {
    window.parent.document.body.style.zoom = "120%";
} else if (window.screen.availWidth == 1152) {
    window.parent.document.body.style.zoom = "108%";
} else if (window.screen.availWidth == 1024) {
    window.parent.document.body.style.zoom = "96%";
} else if (window.screen.availWidth == 800) {
    window.parent.document.body.style.zoom = "75%";
} else if (window.screen.availWidth == 640) {
    window.parent.document.body.style.zoom = "60%";
} else if (window.screen.availWidth == 600) {
    window.parent.document.body.style.zoom = "60%";
}
function LimpiarCredenciales() {
    document.getElementById("txtUser").value = "";
    document.getElementById("txtPassword").value = "";
    document.getElementById("txtUser").focus();
}
function ValidarCredenciales() {
    if (document.getElementById("txtUser").value == "") {
        alert("Debe digitar usuario.");
        document.getElementById("txtUser").focus();
    }
    else {
        if (document.getElementById("txtPassword").value == "") {
            alert("Debe digitar password.");
            document.getElementById("txtPassword").focus();
        }
        else {
            var strRespuesta=ValidarUsuario();
            if (strRespuesta == "Valido.") {
                if (ObtenerIdesUsuario()) {
                    if (ObtenerPerfilUsuario()) {
                        if (ObtenerTipoCambio()) {
                            document.getElementById("hdnUser").value = document.getElementById("txtUser").value;
                            document.getElementById("hdnPassword").value = document.getElementById("txtPassword").value;
                            document.getElementById("frmLoginSistema").submit();
                        }
                        else {
                            alert("Sistema no cuenta con tipo de cambio ingresado.");
                        }
                    }
                    else {
                        alert("Usuario no tiene asignado perfil de acceso.");
                    }
                }
                else {
                    alert("Usuario no cumple con requisitos de acceso.");
                }
            }
            else {
                alert(strRespuesta);
            }
        }
    }
}
function ValidarUsuario() {
    var strRespuesta = "Existe un inconveniente con las credenciales digitadas.";
    var strUser = document.getElementById("txtUser").value;
    var strPassword = document.getElementById("txtPassword").value;
    try {
        $.ajax({
            type: "POST",
            async: false,
            url: "/Services/srvUsuarioSistema.asmx/ValidarUsuarioSistema",
            dataType: "xml",
            data: { "intOpcion": 10, "strUser": strUser, "strPassword": strPassword },
            success: function (xml) {
                strRespuesta = $(xml).find("string").text();
            }
        });
        return strRespuesta;
    }
    catch (err) {
        alert(err.description);
    }
}
function ObtenerIdesUsuario() {
    var blnHayRegistro = false;
    var strUser = document.getElementById("txtUser").value;
    var strPassword = document.getElementById("txtPassword").value;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvUsuarioSistema.asmx/ObtenerIdesUsuarioSistema",
        dataType: "xml",
        data: { "intOpcion": 20, "strUser": strUser, "strPassword": strPassword },
        success: function (xml) {
            $(xml).find("ArrayOfClsUsuarioSistema").each(function () {
                $(this).find("clsUsuarioSistema").each(function () {
                    blnHayRegistro = true;
                    var $registro = $(this);
                    document.getElementById("hdnEmpleado").value = $registro.find("EmpCodigo").text();
                    document.getElementById("hdnUsuario").value = $registro.find("UsuCodigo").text();
                    document.getElementById("hdnOperador").value = $registro.find("OpeCodigo").text();
                    document.getElementById("hdnOficina").value = $registro.find("OfiCodigo").text();
                  
                    document.getElementById("hdnNivel").value = $registro.find("NivCodigo").text();
                    document.getElementById("hdnRol").value = $registro.find("RolCodigo").text();
                    document.getElementById("hdnActivo").value = $registro.find("UsuActivo").text();
                });
            });
        }
    });
    return blnHayRegistro;
}
function ObtenerPerfilUsuario() {
    var blnHayRegistro = false;
    var strUser = document.getElementById("txtUser").value;
    var strPassword = document.getElementById("txtPassword").value;
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvUsuarioSistema.asmx/ObtenerPerfilUsuarioSistema",
        dataType: "xml",
        data: { "intOpcion": 30, "strUser": strUser, "strPassword": strPassword },
        success: function (xml) {
            $(xml).find("ArrayOfClsUsuarioSistema").each(function () {
                $(this).find("clsUsuarioSistema").each(function () {
                    blnHayRegistro = true;
                    var $registro = $(this);
                    document.getElementById("hdnPerfil").value = $registro.find("PerCodigo").text();
                    document.getElementById("hdnCotizador").value=$registro.find("PerCotizador").text();
                });
            });
        }
    });
    return blnHayRegistro;
}
function ObtenerTipoCambio() {
    var blnHayRegistro = false;
    var intOpcion = 10;
    var strMoneda = ''
    var strFecha = ObtenerFechaActual();
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvTipoCambio.asmx/ObtenerTipoCambio",
        dataType: "xml",
        data: { "intOpcion": 10, "strMoneda": strMoneda, "strFecha": strFecha },
        success: function (xml) {
            $(xml).find("ArrayOfClsTipoCambio").each(function () {
                $(this).find("clsTipoCambio").each(function () {
                    blnHayRegistro = true;
                    var $registro = $(this);
                    var CamMoneda = $registro.find("CamMoneda").text();
                    if (CamMoneda == "USD") { document.getElementById("hdnDolar").value = $registro.find("CamValor").text(); }
                    if (CamMoneda == "EUR") { document.getElementById("hdnEuro").value = $registro.find("CamValor").text(); }
                });
            });
        }
    });
    return blnHayRegistro;
}