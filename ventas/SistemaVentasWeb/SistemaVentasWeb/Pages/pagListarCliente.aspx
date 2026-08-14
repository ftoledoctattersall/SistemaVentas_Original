<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pagListarCliente.aspx.vb" Inherits="SistemaVentasWeb.pagListarCliente" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta name="viewport" charset="utf-8" content="width=device-width, initial-scale=1"/>
        <meta http-equiv="Expires" content="0"/>
        <meta http-equiv="Last-Modified" content="0"/>
        <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <title>Ventas Web - Consulta Cliente</title>

        <link type="text/css" rel="stylesheet" href="../JQuery/ui/jquery-ui.css"/>
        <script type="text/javascript" src="../JQuery/js/jquery-3.3.1.js"></script>
        <script type="text/javascript" src="../JQuery/ui/jquery-ui.js"></script>

        <link type="text/css" rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.2.1/css/bootstrap.min.css"/>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.6/umd/popper.min.js"></script>
        <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/4.2.1/js/bootstrap.min.js"></script>
       
        <link type="text/css" rel="stylesheet" href="../Styles/stySitio.css"/>
        <script type="text/javascript" src="../Scripts/scrFuncion.js"></script>
        <script type="text/javascript" src="../Scripts/scrListarCliente.js"></script>
    </head>
    <body style="zoom:75%">
        <form id="frmListarCliente" runat="server">           
            <div class="container-fluid">
                <p>CONSULTA DE CLIENTE</p>
                <div id="row1" class="row">
                    <div class="col-sm-8">
                        <table border="0">
                            <tr>
                                <td>Buscar Cliente</td>
                                <td>:</td>
                                <td colspan="3"><input id="txtBuscaCliente" type="text" class="textboxLonger" onclick="LimpiarTexto(this);"/></td>
<%--                            <td><img id="imgAgregar" alt="Agregar Cliente" title="Haga clic para agregar nuevo cliente." src="../Images/agregar.gif" onclick="AgregarCliente();"/></td>--%>
                            </tr>
                        </table>
                        <table id="tblDatos" style="display:none;">
                            <tr>
                                <td colspan="6"><br /></td>
                            </tr>
                            <tr class="trTituloSeccion">
                                <td colspan="6">Datos del Cliente</td>
                            </tr>
                            <tr>
                                <td>Nombre</td>
                                <td>:</td>
                                <td colspan="4"><div id="divNombreCliente"></div></td>
                            </tr>
                            <tr>
                                <td>Rut</td>
                                <td>:</td>
                                <td><div id="divRutCliente"></div></td>
                                <td>Codigo</td>
                                <td>:</td>
                                <td><div id="divCodigoCliente"></div></td>
                            </tr>
                            <tr>
                                <td>E-Mail</td>
                                <td>:</td>
                                <td colspan="4"><div id="divCorreoCliente"></div></td>
                            </tr>
                            <tr>
                                <td>Telefono</td>
                                <td>:</td>
                                <td><div id="divTelefonoCliente"></div></td>
                                <td>Moneda</td>
                                <td>:</td>
                                <td><div id="divMonedaCliente"></div></td>
                            </tr>
                            <tr>
                                <td>Giro</td>
                                <td>:</td>
                                <td colspan="4"><div id="divGiroCliente"></div></td>
                            </tr>
                            <tr>
                                <td>Plazo de Venta</td>
                                <td>:</td>
                                <td colspan="4"><div id="divPlazoVentaNombre"></div></td>
                            </tr>
                            <tr class="trTituloSeccion">
                                <td colspan="6">Clasificacion del Cliente</td>
                            </tr>
                            <tr>
                                <td>Bloqueado</td>
                                <td>:</td>
                                <td><div id="divBloqueoCliente"></div></td>
                                <td>Grupo Cliente</td>
                                <td>:</td>
                                <td><div id="divNombreGrupo"></div></td>
                            </tr>
                            <tr>
                                <td>Categoria</td>
                                <td>:</td>
                                <td colspan="4"><div id="divCodigoCategoria"></div></td>
                            </tr>
                            <tr class="trTituloSeccion">
                                <td colspan="6">Cuenta Corriente</td>
                            </tr>
                            <tr>
                                <td>Credito Autorizado</td>
                                <td>:</td>
                                <td colspan="4"><div id="divAutorizado"></div></td>
                            </tr>
                            <tr>
                                <td>Credito Utilizado</td>
                                <td>:</td>
                                <td colspan="4"><div id="divUtilizado"></div></td>
                            </tr>
                            <tr>
                                <td>Credito Disponible</td>
                                <td>:</td>
                                <td colspan="4"><div id="divDisponible"></div></td>
                            </tr>
                        </table>
                        <div id="divCuadroResumenDireccionFactura"  title="Direccion de Facturacion"></div>     
                        <div id="divCuadroResumenDireccionDespacho" title="Direccion de Despacho"></div>   
                    </div>                                            
                    <div class="col-sm-4">
                        <div id="divAgregarCliente">
                            <table>
                                <tr class="trTituloSeccion"><td colspan="2">Nuevo Cliente</td></tr>
                                <tr><td>Rut     </td><td><input id="txtRut"      type="text" class="textboxSmall"  maxlength="8"  onkeypress="return ValidarNumero(event);"/>-<input id="txtDV" type="text" class="textboxSmaller"  maxlength="1" onkeypress="return ValidarVerificador(event);" onblur="ConvertirMayuscula();"/></td></tr>
                                <tr><td>Nombre  </td><td><input id="txtNombre"   type="text" class="textboxLong"   maxlength="50" onkeypress="return ValidarTexto(event);"/></td></tr>
                                <tr><td>E-Mail  </td><td><input id="txtCorreo"   type="text" class="textboxLong"   maxlength="35" onkeypress="return ValidarCorreo(event);"/></td></tr>
                                <tr><td>Telefono</td><td><input id="txtTelefono" type="text" class="textboxMedium" maxlength="15" onkeypress="return ValidarNumero(event);"/></td></tr>
                                <tr><td>Giro    </td><td><input id="txtGiro"     type="text" class="textboxLong"   maxlength="50" onkeypress="return ValidarTexto(event);"/></td></tr>
                                <tr><td>Moneda  </td><td><select id="cmbMoneda"  name="cmbMoneda" class="textboxSmall"></select></td></tr>
                                <tr class="trTituloSeccion"><td colspan="2">Direccion</td></tr>
                                <tr><td>Calle  </td><td><input id="txtCalle"     type="text" class="textboxLong"  maxlength="50" onkeypress="return ValidarAlfanumerico(event);"/></td></tr>
                                <tr><td>Numero </td><td><input id="txtNumero"    type="text" class="textboxSmall" maxlength="6"  onkeypress="return ValidarNumero(event);"/></td></tr>
                                <tr><td>Comuna </td><td><input id="txtComuna"    type="text" class="textboxLong"  maxlength="30" onkeypress="return ValidarTexto(event);"/></td></tr>
                                <tr><td>Ciudad </td><td><input id="txtCiudad"    type="text" class="textboxLong"  maxlength="30" onkeypress="return ValidarTexto(event);"/></td></tr>  
                                <tr><td>Region</td><td><select id="cmbRegion"  name="cmbRegion" class="textboxLong"></select></td></tr>
                                <tr><td colspan="2"><br /></td></tr>
                                <tr>
                                    <td class="tdLeft"><input id="btnLimpiarDatos" type="button" value="Limpiar" onclick="LimpiarDatos();"/></td>
                                    <td class="tdRight"><input id="btnCrearCliente" type="button" value="Grabar" onclick="CrearCliente();"/></td>
                                </tr>
                                <tr><td class="tdCenter" colspan="2"><div id="divLoader"><img src="../Images/loader.gif"/></div></td></tr>
                                <tr></tr>
                            </table>                         
                        </div>
                    </div>
                </div>
            </div>
        </form>
        <script type="text/javascript" src="../Scripts/scrVersionamiento.js"></script>
    </body>
</html>