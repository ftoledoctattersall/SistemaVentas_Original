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
function ValidarNumero(e) {
    var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
    return (tecla > 47 && tecla < 58);
}
function ValidarNumeroDecimal(e) {
    var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
    return (tecla > 47 && tecla < 58) || (tecla == 46);
}
function ValidarTexto(e) {
    var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
    return (tecla > 64 && tecla < 91) || (tecla > 96 && tecla < 123) || (tecla==32);
}
function ValidarAlfanumerico(e) {
    var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
    return (tecla > 64 && tecla < 91) || (tecla > 96 && tecla < 123) || (tecla == 32) || (tecla > 47 && tecla < 58);
}
function ValidarVerificador(e) {
    var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
    return (tecla > 47 && tecla < 58) ||(tecla == 75) || (tecla==107);
}
function ValidarCorreo(e) {
    var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
    return (tecla > 64 && tecla < 91) || (tecla > 96 && tecla < 123) || (tecla > 47 && tecla < 58) || (tecla == 95) || (tecla == 46) || (tecla == 64);
}
function ValidarComentario(e) {
    var tecla = (document.all) ? tecla = e.keyCode : tecla=e.which;
    return ((tecla == 32) || (tecla == 44) || (tecla == 46) || (tecla > 47 && tecla < 58) || (tecla > 63 && tecla < 91) || (tecla > 96 && tecla < 123));
}
function ValidarLongitudComentario(obj,objDiv) {
    var intCantidadCaracteres = document.getElementById(obj.id).value.length;
    $(objDiv).html("( "+intCantidadCaracteres+" / 250)");
    if (intCantidadCaracteres > 249) {
        document.getElementById(obj.id).value = strContenido;
    } else {
        strContenido = document.getElementById(obj.id).value;
    }
}
function ObtenerNombreMesCorto(strDato) {
    var intDato = parseInt(strDato);
    var arrMes = ["xxx", "ENE", "FEB", "MAR", "ABR", "MAY", "JUN", "JUL", "AGO", "SEP", "OCT", "NOV", "DIC"];
    var strRetorno = arrMes[intDato];
    return strRetorno;
}
function ObtenerNombreMesLargo(strDato) {
    var intDato = parseInt(strDato);
    var arrMes = ["XXX","ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"];
    var strRetorno = arrMes[intDato];
    return strRetorno;
}
function ObtenerUltimoDiaMes(intDato) {
    var arrMes = ["30", "31", "28", "31", "30", "31", "30", "31", "31", "30", "31", "30", "31"];
    var strRetorno = arrMes[intDato];
    return strRetorno;
}
function ObtenerFechaActual() {
    var datHoy = new Date();
    var intDia = parseInt(datHoy.getDate());
    var intMes = parseInt(datHoy.getMonth()) + 1;
    var intAño = parseInt(datHoy.getFullYear());
    var strFecha = RellenarIzquierda(intDia, 2, 0) + "/" + RellenarIzquierda(intMes, 2, 0) + "/" + intAño;
    return strFecha;
}
function RellenarIzquierda(intDato, intLargo, chrRelleno) {
    chrRelleno = chrRelleno || "0";
    intDato = intDato + "";
    return intDato.length >= intLargo ? intDato : new Array(intLargo - intDato.length + 1).join(chrRelleno) + intDato;
}
function ValidarFormatoFecha(strDato) {
    var strRetorno = false;
    if (strDato.match(/^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[012])\/[0-9]{4}/)) {
        strRetorno = true;
    }
    return strRetorno;
}
function FormatearFechaAñoMes(strDato) {
    var strDatoSplit = strDato.split("/");
    if (strDatoSplit.length != 3) {
        return "";
    }
    var intDia = strDatoSplit[0];
    var intMes = strDatoSplit[1];
    var intAño = strDatoSplit[2];
    return intAño + "" + intMes;
}
function FormatearFechaAñoMesDia(strDato) {
    var strDatoSplit = strDato.split("/");
    if (strDatoSplit.length != 3) {
        return "";
    }
    var intDia = strDatoSplit[0];
    var intMes = strDatoSplit[1];
    var intAño = strDatoSplit[2];
    return intAño + "" + intMes + "" + intDia;
}
function parseDate(strDato) {
    var mdy = strDato.split('/')
    return new Date(mdy[2], mdy[1] - 1, mdy[0]);
}
function SalirSistema() {
    top.location.href = "pagLoginSistema.aspx";
}
function PintarLinea(obj, blnPinta) {
    var strFila = obj.id;
    document.getElementById(strFila).style.background = (blnPinta) ? '#55F760' : '';
}
function ExportarExcel() {
    var datDate = new Date();
    var intDate = datDate.getTime();
    var strArchivo = "E" + intDate + ".xls";
    var strCabecera = "";
    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");
    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {
        strCabecera += "<html xmlns:x='urn:schemas-microsoft-com:office:excel'>";
        strCabecera += " <head>";
        strCabecera += "  <xml>";
        strCabecera += "   <x:ExcelWorkbook>";
        strCabecera += "    <x:ExcelWorksheets>";
        strCabecera += "     <x:ExcelWorksheet>";
        strCabecera += "      <x:Name></x:Name>";
        strCabecera += "       <x:WorksheetOptions>";
        strCabecera += "        <x:Panes></x:Panes>";
        strCabecera += "       </x:WorksheetOptions>";
        strCabecera += "     </x:ExcelWorksheet>";
        strCabecera += "    </x:ExcelWorksheets>";
        strCabecera += "   </x:ExcelWorkbook>";
        strCabecera += "  </xml>";
        strCabecera += " </head>";
        strCabecera += " <body>";
        strCabecera += "  <table border='1px'>";
        strCabecera += $("#divMarco").html();
        strCabecera += "  </table>";
        strCabecera += " </body>";
        strCabecera += "</html>";
        var data_type = "data:application/vnd.ms-excel";
        if (window.navigator.msSaveBlob) {
            var blob = new Blob([strCabecera], {
                type: "application/csv;charset=utf-8;"
            });
            navigator.msSaveBlob(blob, strArchivo);
        }
    }
    else {
        var objEnlace = document.createElement("a");
        var strTipo = "data:application/vnd.ms-excel";
        var objDiv = document.getElementById("divMarco");
        var ObjHtml = objDiv.outerHTML.replace(/ /g, "%20");
        objEnlace.href = strTipo + ", " + ObjHtml;
        objEnlace.download = strArchivo;
        objEnlace.click();
    }
}
function FormatearFloatVista(numero, decimales) {
    //return number_format(parseFloat(numero.replace(",", ".")), decimales, ".", ",");
    //return number_format(parseFloat(numero.replace(".", ",")), decimales, ",", ".");
    return number_format(numero, decimales, ",", ".")
}
function number_format(number, decimals, dec_point, thousands_sep) {
    // http://kevin.vanzonneveld.net
    // +   original by: Jonas Raoni Soares Silva (http://www.jsfromhell.com)
    // +   improved by: Kevin van Zonneveld (http://kevin.vanzonneveld.net)
    // +     bugfix by: Michael White (http://getsprink.com)
    // +     bugfix by: Benjamin Lupton
    // +     bugfix by: Allan Jensen (http://www.winternet.no)
    // +    revised by: Jonas Raoni Soares Silva (http://www.jsfromhell.com)
    // +     bugfix by: Howard Yeend
    // +    revised by: Luke Smith (http://lucassmith.name)
    // +     bugfix by: Diogo Resende
    // +     bugfix by: Rival
    // +      input by: Kheang Hok Chin (http://www.distantia.ca/)
    // +   improved by: davook
    // +   improved by: Brett Zamir (http://brett-zamir.me)
    // +      input by: Jay Klehr
    // +   improved by: Brett Zamir (http://brett-zamir.me)
    // +      input by: Amir Habibi (http://www.residence-mixte.com/)
    // +     bugfix by: Brett Zamir (http://brett-zamir.me)
    // +   improved by: Theriault
    // +      input by: Amirouche
    // +   improved by: Kevin van Zonneveld (http://kevin.vanzonneveld.net)
    // *     example 1: number_format(1234.56);
    // *     returns 1: "1,235"
    // *     example 2: number_format(1234.56, 2, ",", " ");
    // *     returns 2: "1 234,56"
    // *     example 3: number_format(1234.5678, 2, ".", "");
    // *     returns 3: "1234.57"
    // *     example 4: number_format(67, 2, ",", ".");
    // *     returns 4: "67,00"
    // *     example 5: number_format(1000);
    // *     returns 5: "1,000"
    // *     example 6: number_format(67.311, 2);
    // *     returns 6: "67.31"
    // *     example 7: number_format(1000.55, 1);
    // *     returns 7: "1,000.6"
    // *     example 8: number_format(67000, 5, ",", ".");
    // *     returns 8: "67.000,00000"
    // *     example 9: number_format(0.9, 0);
    // *     returns 9: "1"
    // *    example 10: number_format("1.20", 2);
    // *    returns 10: "1.20"
    // *    example 11: number_format("1.20", 4);
    // *    returns 11: "1.2000"
    // *    example 12: number_format("1.2000", 3);
    // *    returns 12: "1.200"
    // *    example 13: number_format("1 000,50", 2, ".", " ");
    // *    returns 13: "100 050.00"
    // Strip all characters but numerical ones.
    number = (number + "").replace(/[^0-9+\-Ee.]/g, "");
    var n = !isFinite(+number) ? 0 : +number,
    prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
    sep = (typeof thousands_sep === "undefined") ? "," : thousands_sep,
    dec = (typeof dec_point === "undefined") ? "." : dec_point,
    s = "",
    toFixedFix = function (n, prec) {
        var k = Math.pow(10, prec);
        return "" + Math.round(n * k) / k;
    };
    // Fix for IE parseFloat(0.55).toFixed(0) = 0;
    s = (prec ? toFixedFix(n, prec) : "" + Math.round(n)).split(".");
    if (s[0].length > 3) {
        s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
    }
    if ((s[1] || "").length < prec) {
        s[1] = s[1] || "";
        s[1] += new Array(prec - s[1].length + 1).join("0");
    }
    return s.join(dec);
}
function ValidarRut(numero, dv) {
    if (!isNaN(numero) || numero.length == 0 || numero.length > 8) {
        return false;
    } else {
        if (ObtenerDV(numero) == dv) return true;
    }
    return false;
}
function ObtenerDV(numero) {
    nuevo_numero = numero.toString().split("").reverse().join("");
    for (i = 0, j = 2, suma = 0; i < nuevo_numero.length; i++, ((j == 7) ? j = 2 : j++)) {
        suma += (parseInt(nuevo_numero.charAt(i)) * j);
    }
    n_dv = 11 - (suma % 11);
    return ((n_dv == 11) ? 0 : ((n_dv == 10) ? "K" : n_dv));
}
function SumarDias(datFecha, intDias) {
    datFecha.setDate(datFecha.getDate() + intDias);
    return datFecha;
}
function FormatearNumero(numero) {
    var resultado = "";
    if (numero != 0) {
        resultado = parseFloat(numero).toLocaleString('es-CL');
    } else {
        resultado = numero;
    }
    return resultado;
}
function AjustarResolucion() {
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
}
function LimpiarTexto(obj) {
    obj.value = "";
}
function ObtenerDiferenciaDias(strDesde, strHasta) {
    var strInicio = strDesde.substring(3, 5) + "/" + strDesde.substring(0, 2) + "/" + strDesde.substring(6, 10);
    var strTermino = strHasta.substring(3, 5) + "/" + strHasta.substring(0, 2) + "/" + strHasta.substring(6, 10);
    var datDesde = new Date(strInicio);
    var datHasta = new Date(strTermino);
    var intTiempo = datHasta.getTime() - datDesde.getTime(); //milisegundos
    var intDiferenciaDias = Math.floor(intTiempo / (1000 * 60 * 60 * 24));
    return intDiferenciaDias;
}