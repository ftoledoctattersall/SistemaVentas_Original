<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pagInformeLiquidacionComision.aspx.vb" Inherits="SistemaVentasWeb.pagInformeLiquidacionComision" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta http-equiv="Expires" content="0"/>
        <meta http-equiv="Last-Modified" content="0"/>
        <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <title>Ventas Web - Informe Liquidacion Comision</title>

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"></script>

        <link type="text/css" rel="stylesheet" href="../JQuery/ui/jquery-ui.css" />
        <script type="text/javascript" src="../JQuery/js/jquery-3.3.1.js"></script>
        <script type="text/javascript" src="../JQuery/ui/jquery-ui.js"></script>

        <link type="text/css" rel="stylesheet" href="../Styles/stySitio.css"/>
        <script type="text/javascript" src="../Scripts/scrFuncion.js"></script>
        <script type="text/javascript" src="../Scripts/scrInformeLiquidacionComision.js"></script>
    </head>
    <body style="zoom:75%">
        <form id="frmInformeLiquidacionComision" runat="server">
            <div class="container-fluid">
                <p>INFORME DE LIQUIDACION DE COMISIONES</p>
                <div id="row1" class="row">
                    <div class="col-sm-4">
                        <table border="0">
                            <tr>
                                <td class="tdLeft">Mes / Año de Liquidacion</td>
                                <td class="tdLeft">:</td>
                                <td class="tdLeft"><select id="cmbMes" name="cmbMes"/></td>
                                <td class="tdLeft"><select id="cmbAño" name="cmbAño"/></td>
                                <td class="tdRight"><input id="btnConsultar" type="button" value="Consultar" onclick="Consultar();"/></td>
                            </tr>                         
                            <tr><td class="tdCenter" colspan="5"><div id="divLoader"><img src="../Images/loader.gif"/></div></td></tr>
                        </table>
                    </div>
                </div>
                <div id="row2" class="row">
                    <div class="col-sm-4">
                        <table border="0">
                            <tr>
                                <td colspan="5">
                                    <div id="divCuadroResumen"></div>
                                    <div id="divCuadroDetalleLinea"></div>
                                    <div id="divCuadroDetalleDocumento"></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </form>
        <script type="text/javascript" src="../Scripts/scrVersionamiento.js"></script>
    </body>
</html>