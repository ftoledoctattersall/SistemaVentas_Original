<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pagListarProducto.aspx.vb" Inherits="SistemaVentasWeb.pagListarProducto" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta name="viewport" charset="utf-8" content="width=device-width, initial-scale=1"/>
        <meta http-equiv="Expires" content="0"/>
        <meta http-equiv="Last-Modified" content="0"/>
        <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <title>Ventas Web - Consulta Producto</title>

        <link type="text/css" rel="stylesheet" href="../JQuery/ui/jquery-ui.css"/>
        <script type="text/javascript" src="../JQuery/js/jquery-3.3.1.js"></script>
        <script type="text/javascript" src="../JQuery/ui/jquery-ui.js"></script>

        <link type="text/css" rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.2.1/css/bootstrap.min.css"/>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.6/umd/popper.min.js"></script>
        <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/4.2.1/js/bootstrap.min.js"></script>
       
        <link type="text/css" rel="stylesheet" href="../Styles/stySitio.css"/>
        <script type="text/javascript" src="../Scripts/scrFuncion.js"></script>
        <script type="text/javascript" src="../Scripts/scrListarProducto.js"></script>
    </head>
    <body style="zoom:75%">
        <form id="frmListarProducto" runat="server"> 
            <div class="container-fluid">
                <p>CONSULTA DE PRODUCTO</p>
                <div id="row1" class="row">
                    <div class="col-sm-8">
                        <table border="0">
                            <tr>
                                <td colspan="2">Tipo de Filtro</td>
                                <td>:</td>
                                <td colspan="4">
                                    <label><input type="radio" name="rdoFiltro" value="0" onchange="CambiarEstado(0);" checked="checked"/>Busqueda</label>
                                    <label><input type="radio" name="rdoFiltro" value="1" onchange="CambiarEstado(1);"/>Seleccion</label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">Buscar Producto</td>
                                <td>:</td>
                                <td colspan="4"><input id="txtBuscaProducto" type="text" class="textboxLonger" onclick="LimpiarTexto(this);"/></td>
                            </tr>
                            <tr>
                                <td colspan="2">Seleccionar Ingrediente</td>
                                <td>:</td>
                                <td colspan="4"><select id="cmbIngrediente" name="cmbIngrediente" class="textboxLonger" onchange="CargarComboProducto();" disabled="disabled"></select></td>
                            </tr>
                            <tr>
                                <td colspan="2">Seleccionar Producto</td>
                                <td>:</td>
                                <td colspan="4"><select id="cmbProducto" name="cmbProducto" class="textboxLonger" onchange="MostrarInventario();" disabled="disabled"></select></td>
                            </tr>
                        </table>
                        <table id="tblDatos" style="display:none;">
                            <tr>
                                <td colspan="11"><br /></td>
                            </tr>
                            <tr class="trTituloSeccion">
                                <td colspan="11">Datos del Producto</td>
                            </tr>
                            <tr>
                                <td>Codigo</td>
                                <td>:</td>
                                <td><div id="divCodigoProducto"></div></td>
                                <td>   |   </td>
                                <td>Descripcion</td>
                                <td>:</td>
                                <td><div id="divNombreProducto"></div></td>
                                <td colspan="4"></td>
                            </tr>
                            <tr>
                                <td>Grupo de Articulo</td>
                                <td>:</td>
                                <td><div id="divNombreGrupo"></div></td>
                                <td>   |   </td>
                                <td>Proveedor</td>
                                <td>:</td>
                                <td><div id="divNombreProveedor"></div></td>
                                <td colspan="4"></td>
                            </tr>
                            <tr>
                                <td>Unidad de Medida</td>
                                <td>:</td>
                                <td><div id="divEnvaseProducto"></div></td>
                                <td>   |   </td>
                                <td>Dias Pago</td>
                                <td>:</td>
                                <td><div id="divDiasPago"></div></td>
                                <td>   |   </td>
                                <td>Precio de Lista</td>
                                <td>:</td>
                                <td><div id="divPrecioLista"></div></td>
                            </tr>
                        </table>
                    </div>       
                </div>
                <div id="row2" class="row">
                    <div class="col-sm-12">
                        <table border="0">
                            <tr>
                                <td>
                                    <div id="divInventario" title="Inventario de Productos en Bodega(s)"></div>
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