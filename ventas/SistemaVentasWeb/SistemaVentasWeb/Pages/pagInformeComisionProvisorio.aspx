<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pagInformeComisionProvisorio.aspx.vb" Inherits="SistemaVentasWeb.pagInformeComisionProvisorio" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta http-equiv="Expires" content="0"/>
        <meta http-equiv="Last-Modified" content="0"/>
        <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <title>Ventas Web - Informe Comision Provisorio</title>

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"></script>

        <link type="text/css" rel="stylesheet" href="../JQuery/ui/jquery-ui.css" />
        <script type="text/javascript" src="../JQuery/js/jquery-3.3.1.js"></script>
        <script type="text/javascript" src="../JQuery/ui/jquery-ui.js"></script>

        <link type="text/css" rel="stylesheet" href="../Styles/stySitio.css"/>
        <script type="text/javascript" src="../Scripts/scrFuncion.js"></script>
        <script type="text/javascript" src="../Scripts/scrInformeComisionProvisorio.js"></script>
    </head>
    <body style="zoom:75%">
        <form id="frmInformeComisionProvisorio" runat="server">
            <div class="container-fluid">
                <p>INFORME DE COMISIONES PROVISORIOS</p>
                <div id="row1" class="row">
                    <div class="col-sm-4">
                        <table border="0">
                            <tr>
                                <td class="tdLeft">Mes / Año en Curso</td>
                                <td class="tdLeft">:</td>
                                <td class="tdLeft"><div id="divMesCurso"></div></td>
                            </tr>                         
                        </table>
                    </div>
                </div>
                <div id="row2" class="row">
                    <div class="col-sm-4">
                        <table border="0">
                            <tr>
                                <td>
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