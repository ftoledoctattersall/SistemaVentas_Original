$(document).ready(function () {
    CrearMenuSistema();
    CargarMenuSistema();
    CargarMarcoInicial();
});
function CrearMenuSistema() {
    var intOpcion = 10;
    var intPerfil = document.getElementById("hdnPerfil").value;
    var strUser = document.getElementById("hdnUser").value;
    var decDolar = document.getElementById("hdnDolar").value;
    var decEuro = document.getElementById("hdnEuro").value;
    var strModAnterior = "";
    var strTabla = "";
    strTabla += "<table class='tableMenu'>";
    strTabla += "<tr>";
    strTabla += "<td class='tdLeft'><img src='../../Images/logo.png' height='50' width='200'/></td>";
    strTabla += "<td class='tdLeft'>";
    strTabla += "<div id='menu'>";
    strTabla += "<ul id='nav'>";
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvMenuSistema.asmx/ObtenerMenuSistema",
        dataType: "xml",
        data: { "intOpcion": intOpcion, "intPerfil": intPerfil, "strUser": strUser },
        success: function (xml) {
            $(xml).find("ArrayOfClsMenuSistema").each(function () {
                $(this).find("clsMenuSistema").each(function () {
                    var $registro = $(this);
                    var strModActual = $registro.find("ModNombre").text();
                    var strOpcNombre = $registro.find("OpcNombre").text();
                    var strOpcPagina = $registro.find("OpcPagina").text();
                    if (strModActual != strModAnterior) {
                        if (strModAnterior != "") {
                            strTabla += "</ul>";
                            strTabla += "</li>";
                        }
                        strTabla += "<li>";
                        strTabla += "<a href='#'>" + strModActual + "</a>";
                        strTabla += "<ul class='submenu'>";
                    }
                    strTabla += "<li>";
                    strTabla += "<a href='" + strOpcPagina + "' target='ifrMuestraPagina'>" + strOpcNombre + "</a>";
                    strTabla += "</li>";
                    strModAnterior = strModActual;
                });
            });
        }
    });
    strTabla += "</ul>";
    strTabla += "</li>";
    strTabla += "</ul>";
    strTabla += "</div>";
    strTabla += "</td>";
    strTabla += "<td class='tdRight'>Dolar:</td>";
    strTabla += "<td class='tdLeft'>[" + decDolar + "]</td>";
    strTabla += "<td class='tdRight'>Euro:</td>";
    strTabla += "<td class='tdLeft'>[" + decEuro + "]</td>";
    strTabla += "<td class='tdRight'>Usuario:</td>";
    strTabla += "<td class='tdLeft'>[" + strUser.toUpperCase() + "]</td>";
    strTabla += "<td class='tdRight'><img id='imgCargar' onclick='CargarGestion();' src='../Images/gestion.png' width='20' height='20' title='Haga clic para ir a sistema de gestion.' /></td>";
    strTabla += "<td class='tdRight'><img id='imgSalir'  onclick='SalirSistema();'  src='../Images/salir.jpg'   width='20' height='20' title='Haga clic para salir del sistema.' /></td>";
    strTabla += "</tr>"
    //strTabla += "<tr>"
    //strTabla += "<td colspan='10' align='center'>";
    //strTabla += "user:" + document.getElementById("hdnUser").value + "|password:" + document.getElementById("hdnPassword").value + "|id_empleado:" + document.getElementById("hdnEmpleado").value + "|id_usuario:" + document.getElementById("hdnUsuario").value + "|id_operador:" + document.getElementById("hdnOperador").value + "|id_perfil:" + document.getElementById("hdnPerfil").value + "|id_oficina:" + document.getElementById("hdnOficina").value + "|dolar:" + document.getElementById("hdnDolar").value + "|Euro:" + document.getElementById("hdnEuro").value + "|id_nivel:" + document.getElementById("hdnNivel").value + "|id_rol:" + document.getElementById("hdnRol").value + "|activo:" + document.getElementById("hdnActivo").value + "|Cotizador:" + document.getElementById("hdnCotizador").value;
    //strTabla += "</td>";
    //strTabla += "</tr>"
    strTabla += "</table>"
    $("#divMenu").html(strTabla);
}
function CargarMenuSistema(){ 
    $(" #nav li").hover(function(){
        $(this).find('ul:first:hidden').css({visibility: "visible",display: "none"}).slideDown(100);
    },function(){
        $(this).find('ul:first').slideUp(100);
    });
}
function CargarMarcoInicial() {
    var intPerfil = parseInt(document.getElementById("hdnPerfil").value);
    var strCotizador = document.getElementById("hdnCotizador").value;
    if (strCotizador == 'N') 
        var arrPagina = new Array("pagBlanco.aspx", "pagVentaBodegaPropia.aspx", "pagVentaBodegaPropia.aspx", "pagMonitorAutorizacion.aspx");
    else
        var arrPagina = new Array("pagBlanco.aspx", "pagCotizacion.aspx", "pagCotizacion.aspx", "pagMonitorAutorizacion.aspx");
    document.getElementById("ifrMuestraPagina").src = arrPagina[intPerfil];
}
function CargarGestion() {
    document.getElementById("frmMenuSistema").method = "post"; 
    document.getElementById("frmMenuSistema").target = "_blank"; 
    document.getElementById("frmMenuSistema").action = "http://gestion_pre.agroinsumos.cl/Pages/Informe/pagMenu.aspx"; 
    document.getElementById("frmMenuSistema").submit(); 
}