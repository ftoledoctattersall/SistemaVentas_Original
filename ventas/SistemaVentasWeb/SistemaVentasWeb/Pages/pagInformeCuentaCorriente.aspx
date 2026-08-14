<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pagInformeCuentaCorriente.aspx.vb" Inherits="SistemaVentasWeb.pagInformeCuentaCorriente" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta http-equiv="Expires" content="0"/>
        <meta http-equiv="Last-Modified" content="0"/>
        <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <title>Ventas Web - Informe Cuenta Corriente</title>

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"></script>

        <link type="text/css" rel="stylesheet" href="../JQuery/ui/jquery-ui.css" />
        <script type="text/javascript" src="../JQuery/js/jquery-3.3.1.js"></script>
        <script type="text/javascript" src="../JQuery/ui/jquery-ui.js"></script>

        <link type="text/css" rel="stylesheet" href="../Styles/stySitio.css"/>
        <script type="text/javascript" src="../Scripts/scrFuncion.js"></script>
        <script type="text/javascript" src="../Scripts/scrInformeCuentaCorriente.js"></script>
    </head>
    <body style="zoom:75%">
        <form id="frmInformeCuentaCorriente" runat="server">
            <div class="container-fluid">
                <p>INFORME DE CUENTAS CORRIENTES</p>
                <div id="row1" class="row">
                    <div class="col-sm-4">
                        <table border="0">
                            <tr>
                                <td>Buscar Cliente</td>
                                <td>:</td>
                                <td><input id="txtCodigoCliente" type="text" class="textboxLonger" onclick="LimpiarTexto(this);"/><input id="hdnCodigoCliente" type="hidden"/><input id="hdnNombreCliente" type="hidden"/></td>
                                <td class="tdRight"><input id="btnConsultar" type="button" value="Consultar" onclick="Consultar();"/></td>
                            </tr>
                            <tr><td class="tdCenter" colspan="4"><div id="divLoader"><img src="../Images/loader.gif"/></div></td></tr>
                            <tr></tr>
                        </table>
                    </div>
                </div>
                <div id="row2" class="row">
                    <div class="col-sm-4">
                        <table border="0">
                            <tr><td class="tdLeft"   colspan="4"><div id="divCuadroTotalizado"></div></td></tr>
                        </table>
                    </div>
                </div>
                <div id="row3" class="row">
                    <div class="col-sm-12">
                        <table border="0">
                            <tr><td style="vertical-align:top"> <div id="divCuadroResumen"></div></td></tr>
                            <tr><td><br/></td></tr>
                            <tr><td style="vertical-align:top;"><div id="divCuadroDetalle"></div></td></tr>
                        </table>
                    </div>
                </div>
            </div>
        </form>
        <script type="text/javascript" src="../Scripts/scrVersionamiento.js"></script>
    </body>
</html>