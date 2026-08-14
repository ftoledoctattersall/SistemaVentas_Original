<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pagMonitorDocumento.aspx.vb" Inherits="SistemaVentasWeb.pagMonitorDocumento" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta http-equiv="Expires" content="0"/>
        <meta http-equiv="Last-Modified" content="0"/>
        <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <title>Ventas Web - Monitor Documento</title>

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"></script>

        <link type="text/css" rel="stylesheet" href="../JQuery/ui/jquery-ui.css" />
        <script type="text/javascript" src="../JQuery/js/jquery-3.3.1.js"></script>
        <script type="text/javascript" src="../JQuery/ui/jquery-ui.js"></script>

        <link type="text/css" rel="stylesheet" href="../Styles/stySitio.css"/>
        <script type="text/javascript" src="../Scripts/scrFuncion.js"></script>
        <script type="text/javascript" src="../Scripts/scrMonitorDocumento.js"></script>
    </head>
    <body style="zoom:75%">
        <form id="frmMonitorDocumento" runat="server">
            <input id="hdnPaginaActual"      name="hdnPaginaActual"      type="hidden"/>
            <input id="hdnPaginaFinal"       name="hdnPaginaFinal"       type="hidden"/>
            <input id="hdnCodigoCliente"     name="hdnCodigoCliente"     type="hidden"/>
            <input id="hdnTotalDocumento"    name="hdnTotalDocumento"    type="hidden"/>
            <input id="hdnEstadoDocumento"   name="hdnEstadoDocumento"   type="hidden"/>
            <input id="hdnCreditoDisponible" name="hdnCreditoDisponible" type="hidden"/>
            <input id="hdnDiasExtras"        name="hdnDiasExtras"        type="hidden"/>
            <div class="container-fluid">
                <p>MONITOR DE DOCUMENTOS</p>
                <div id="row1" class="row">
                    <div class="col-sm-4">
                        <table border="0">
                            <tr>
                                <td>Periodo</td>
                                <td>:</td>
                                <td><input id="txtFechaDesde" type="text" class="textboxSmallReadonly pq required date error" readonly="true" onchange="ValidarFechaDesde();"/>-<input id="txtFechaHasta" type="text" class="textboxSmallReadonly pq required date error" readonly="true" onchange="ValidarFechaHasta();"/></td>
                                <td>Cliente</td>
                                <td>:</td>
                                <td><input id="txtCodigoCliente" type="text" class="textboxLong"/></td>
                                <td>Estado </td>
                                <td>:</td>
                                <td><select id="cmbEstado" name="cmbEstado" class="textboxLong"></select></td>
                                <td class="tdCenter"><input id="btnConsultar" type="button" value="Consultar" onclick="Consultar();"/></td>
                            </tr>
                            <tr><td colspan="10"><br /></td></tr>
                        </table>
                    </div>
                </div>
                <div id="row2" class="row">
                    <div class="col-sm-4">
                        <table border="0">
                            <tr>
                                <td colspan="3">
                                    <div id="divCuadroResumen"></div>
                                </td>
                                <td colspan="3">
                                    <div id="divCuadroAvanzar"></div>
                                </td>
                            </tr>
                            <tr><td class="tdCenter" colspan="3"><div id="divLoader"><img src="../Images/loader.gif"/></div></td></tr>
                        </table>
                    </div>
                </div>
            </div>
        </form>
        <script type="text/javascript" src="../Scripts/scrVersionamiento.js"></script>
    </body>
</html>