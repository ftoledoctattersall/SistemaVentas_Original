<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pagCotizacion.aspx.vb" Inherits="SistemaVentasWeb.pagCotizacion" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta name="viewport" charset="utf-8" content="width=device-width, initial-scale=1"/>
        <meta http-equiv="Expires" content="0"/>
        <meta http-equiv="Last-Modified" content="0"/>
        <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <title>Ventas Web - Cotizacion</title>

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"></script>

        <link type="text/css" rel="stylesheet" href="../JQuery/ui/jquery-ui.css" />
        <script type="text/javascript" src="../JQuery/js/jquery-3.3.1.js"></script>
        <script type="text/javascript" src="../JQuery/ui/jquery-ui.js"></script>

        <link type="text/css" rel="stylesheet" href="../Styles/stySitio.css"/>
        <script type="text/javascript" src="../Scripts/scrFuncion.js"></script>
        <script type="text/javascript" src="../Scripts/scrCotizacion.js"></script>
    </head>
    <body style="zoom:75%">
        <form id="frmCotizacion" runat="server">
            <div class="container-fluid">
                <p>COTIZACIONES DESDE CRM</p>
                <div id="row1" class="row">
                    <div class="col-sm-4">
                        <table border="0">
                            <tr>
                                <td class="tdLeft">Estado</td>
                                <td class="tdLeft">:</td>
                                <td class="tdLeft"><select id="cmbCotizacion" name="cmbCotizacion" class="textboxMedium" onchange="Consultar();"></select></td>
                            </tr>                         
                        </table>
                    </div>
                </div>
                <div id="row2" class="row">
                    <div class="col-sm-4">
                        <table border="0">
                            <tr>
                                <td>
                                    <div id="divCuadro"></div>
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