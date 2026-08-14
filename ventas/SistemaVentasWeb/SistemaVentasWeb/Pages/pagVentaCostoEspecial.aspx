<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pagVentaCostoEspecial.aspx.vb" Inherits="SistemaVentasWeb.pagVentaCostoEspecial" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta name="viewport" charset="utf-8" content="width=device-width, initial-scale=1"/>
        <meta http-equiv="Expires" content="0"/>
        <meta http-equiv="Last-Modified" content="0"/>
        <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <title>Ventas Web - Venta Costo Especial</title>

        <link type="text/css" rel="stylesheet" href="../JQuery/ui/jquery-ui.css"/>
        <script type="text/javascript" src="../JQuery/js/jquery-3.3.1.js"></script>
        <script type="text/javascript" src="../JQuery/ui/jquery-ui.js"></script>

        <link type="text/css" rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.2.1/css/bootstrap.min.css"/>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.6/umd/popper.min.js"></script>
        <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/4.2.1/js/bootstrap.min.js"></script>
       
        <link type="text/css" rel="stylesheet" href="../Styles/stySitio.css"/>
        <script type="text/javascript" src="../Scripts/scrFuncion.js"></script>
        <script type="text/javascript" src="../Scripts/scrVentaCostoEspecialProducto.js"></script>
        <script type="text/javascript" src="../Scripts/scrVentaCostoEspecialProductoCarga.js"></script>
        <script type="text/javascript" src="../Scripts/scrVentaCostoEspecial.js"></script>
    </head>
    <body style="zoom:75%">
        <form id="frmVentaCostoEspecial" runat="server">
            <asp:HiddenField ID="hdnTipo"   runat="server"/>
            <asp:HiddenField ID="hdnCodigo" runat="server"/>
            <input id="hdnAutorizacionProducto"       type="hidden"/>
            <input id="hdnAutorizacionTasa"           type="hidden"/>
            <input id="hdnAutorizacionCredito"        type="hidden"/>
            <input id="hdnAutorizacionDeuda"          type="hidden"/>
            <input id="hdnAutorizacionProtesto"       type="hidden"/>
            <input id="hdnPeriodoContable"            type="hidden"/>
            <input id="hdnBodegaProducto"             type="hidden"/>
            <input id="hdnHibridoProducto"            type="hidden"/>
            <input id="hdnPrecioUnitarioRealProducto" type="hidden"/>
            <input id="hdnCostoComercialProducto"     type="hidden"/>
            <input id="hdnCostoReposicionProducto"    type="hidden"/>
            <input id="hdnInventariableProducto"      type="hidden"/>
            <input id="hdnTelefonoCliente"            type="hidden"/>
            <input id="hdnGiroCliente"                type="hidden"/>
            <input id="hdnCorreoCliente"              type="hidden"/>
            <input id="hdnGrupoCliente"               type="hidden"/>
            <input id="hdnDireccion"                  type="hidden"/>
            <input id="hdnDescuentoProducto"          type="hidden"/>
            <div class="container-fluid">
                <p>VENTA COSTO ESPECIAL</p>
                <div id="row1" class="row">
                    <div class="col-sm-4">
                        <table border="0">

                            <tr><td>N°Cotizacion</td><td>:</td><td><asp:TextBox ID="txtCotizacion" runat="server" class="textboxSmallerReadonly" readonly="true"></asp:TextBox></td></tr>

                            <tr><td>Docto.Venta</td><td>:</td><td><select id="cmbEmision" name="cmbEmision" class="textboxMedium" onchange="BloquearEmision();"></select></td></tr>

                            <tr><td>Buscar Cliente</td><td>:</td><td><input id="txtBuscaCliente" type="text" class="textboxLong" onclick="LimpiarTexto(this);"/></td></tr>
                            <tr><td colspan="3"><br /></td></tr>
                            <tr class="trTituloSeccion"><td colspan="3">Datos del Cliente</td></tr>
                            <tr><td>Cliente</td><td>:</td><td><input id="txtNombreCliente" type="text" class="textboxLongReadonly" readonly="true"/></td></tr>
                            <tr><td>Rut</td><td>:</td><td><input id="txtRutCliente" type="text"  class="textboxSmallReadonly" readonly="true"/></td></tr>
                            <tr><td>Codigo</td><td>:</td><td><input id="txtCodigoCliente" type="text" class="textboxSmallReadonly" readonly="true"/></td></tr>
                            <tr class="trTituloSeccion"><td colspan="3">Linea de Credito</td></tr>

                            <tr><td>Acuerdo Inscrito</td><td>:</td><td><input id="txtAcuerdo" type="text" class="textboxLongReadonly" readonly="true"/></td></tr>

                            <tr><td>Categoria</td><td>:</td><td><input id="txtCategoria" type="text" class="textboxLongReadonly" readonly="true"/></td></tr>
                            <tr><td>Autorizado</td><td>:</td><td><input id="txtAutorizado" type="text" class="textboxSmallReadonly" readonly="true"/></td></tr>
                            <tr><td>Utilizado</td><td>:</td><td><input id="txtUtilizado" type="text" class="textboxSmallReadonly" readonly="true"/></td></tr>
                            <tr><td>Disponible</td><td>:</td><td><input id="txtDisponible" type="text" class="textboxSmallReadonly" readonly="true"/></td></tr>
                            <tr><td colspan="3"><div id="divCuadroResumenFacturaImpaga"></div></td></tr>
                            <tr><td colspan="3"><div id="divCuadroResumenChequeProtestado"></div></td></tr>
                        </table>
                    </div>
                    <div class="col-sm-8">
                        <table border="0">
                            <tr class="trTituloSeccion"><td colspan="3">Datos de la Venta</td></tr>
                            <tr>
                                <td>Moneda de Pago</td>
                                <td>:</td>
                                <td><select id="cmbMonedaPago" name="cmbMonedaPago" class="textboxSmall" onchange="BloquearMonedaPago();"></select></td>
                            </tr>
                            <tr>
                                <td>Vendedor Asociado a Cliente</td>
                                <td>:</td>
                                <td><select id="cmbVendedor" name="cmbVendedor" class="textboxLong"></select></td>
                            </tr>
                            <tr><td>Vendedor Asociado a Venta</td><td>:</td><td><input id="txtOperador" type="text" class="textboxLongReadonly" readonly="true"/></td></tr>
                            <tr>
                                <td>Fecha Orden de Venta</td>
                                <td>:</td>
                                <td><input id="txtFechaOrdenVenta" type="text" class="textboxSmallReadonly pq required date error" readonly="true" onchange="ValidarFechaOrdenVenta();"/></td>
                            </tr>
                            <tr>
                                <td>Fecha Entrega</td>
                                <td>:</td>
                                <td><input id="txtFechaEntregaOrden" type="text" class="textboxSmallReadonly pq  date error" readonly="true" onchange="ValidarFechaEntregaOrden();"/></td>
                            </tr>
                            <tr>
                                <td>Oficina</td>
                                <td>:</td>
                                <td><select id="cmbSerie" name="cmbSerie" class="textboxSmall"></select></td>
                            </tr>
                            <tr>
                                <td>Tipo de Despacho</td>
                                <td>:</td>
                                <td><select id="cmbTipoDespacho" name="cmbTipoDespacho" class="textboxLong" onchange="ActualizarIncluyeFlete();"></select></td>
                            </tr>
                            <tr>
                                <td>N° Orden Compra(801)</td>
                                <td>:</td>
                                <td><input id="txtOrdenCompra" type="text" class="textboxSmall" maxlength="10" onkeypress="return ValidarNumero(event);"/></td>
                            </tr>
                            <tr>
                                <td>N° Nota Pedido Cliente(802)</td>
                                <td>:</td>
                                <td><input id="txtNotaPedido" type="text" class="textboxSmall" maxlength="10" onkeypress="return ValidarNumero(event);"/></td>
                            </tr>
                            <tr>
                                <td>N° Guia Despacho(52)</td>
                                <td>:</td>
                                <td><input id="txtGuiaDespacho" type="text" class="textboxSmall" maxlength="10" onkeypress="return ValidarNumero(event);"/></td>
                            </tr>
                            <tr><td colspan="3"><br /></td></tr>
                            <tr><td class="tdCenter" colspan="3"><img id="imgAvanzar" alt="Avanzar" title="Avanzar." src="../Images/avanzar.png" onclick="ValidarDatosCabecera();"/></td></tr>
                        </table>
                    </div>
                </div>
                <div id="row2" class="row" style="display:none">
                    <div class="col-sm-6">
                        <table border="0">
                            <tr class="trTituloSeccion"><td colspan="7">Condiciones del Negocio</td></tr>
                            <tr>
                                <td class="tdLeft">Plazo Venta</td>
                                <td class="tdLeft"><select id="cmbPlazoVenta" name="cmbPlazoVenta" class="textboxLong" onchange="ActualizarFechaVencimiento();"></select><input id="hdnPlazoVenta" type="hidden"/></td>
                                <td class="tdLeft">Vecto.Venta</td>
                                <td class="tdLeft"><input id="txtFechaVencimiento" type="text" class="textboxSmallReadonly xs required date" readonly="true"/></td>
                                <td class="tdLeft"></td>
                                <td class="tdLeft"></td>
                                <td class="tdLeft"></td>
                            </tr>
                            <tr>
                                <td class="tdLeft">Plazo Compra</td>
                                <td class="tdLeft"><select id="cmbPlazoCompra" name="cmbPlazoCompra"       class="textboxLong" onchange="ActualizarFechaCompra();"></select><input id="hdnPlazoCompra" type="hidden"/><input id="hdnDiasCompra" type="hidden"/></td>
                                <td class="tdLeft">Vecto.Compra</td>
                                <td class="tdLeft"><input id="txtFechaCompra" type="text" class="textboxSmallReadonly xs required date" readonly="true"/></td>
                                <td class="tdLeft">Motivo</td>
                                <td class="tdLeft"></td>
                                <td class="tdLeft"></td>
                            </tr>
                            <tr>
                                <td class="tdLeft">Proveedor</td>
                                <td class="tdLeft"><input id="txtBuscaProveedorCompra" type="text" class="textboxLong" onclick="LimpiarTextoProveedor();"/></td>
                                <td class="tdLeft">Nombre</td>
                                <td class="tdLeft"><input id="txtNombreProveedorCompra" type="text" class="textboxLongReadonly" readonly="true"/></td>
                                <td class="tdLeft">Codigo</td>
                                <td class="tdLeft"><input id="txtCodigoProveedorCompra" type="text" class="textboxSmallReadonly" readonly="true"/></td>
                                <td class="tdLeft"></td>
                            </tr>
                            <tr class="trTituloSeccion"><td colspan="7">Solicitud de Producto</td></tr>
                            <tr>
                                <td class="tdLeft"><a href="#" id="openerConsultarProducto"><img id="imgVerProducto" alt="Producto" title="Haga clic para visualizar productos solicitados desde cotizacion." src="../Images/visualizar.png"/></a>&nbsp;Bodega</td>
                                <td class="tdLeft"><select id="cmbBodega" name="cmbBodega" class="textboxMedium" onchange="ActualizarBodegaProducto();"></select></td>
                                <td class="tdLeft">Producto</td>
                                <td class="tdLeft"><input id="txtBuscaProducto" type="text" class="textboxLong" onclick="LimpiarTexto(this);"/></td>
                                <td class="tdLeft">Precio Unitario Venta C/Flete</td>
                                <td class="tdLeft"><input id="txtMonedaProductoAuxiliar" type="text" class="textboxSmallerReadonly" readonly="true"/><input id="txtPrecioUnitarioProductoAuxiliar" type="text" class="textboxSmallReadonly" readonly="true"  maxlength="8" onkeypress="return ValidarNumeroDecimal(event);"/><input id="hdnIncluyeFlete" type="hidden" value="0"/></td>
                                <td class="tdRight"><div id="divConsultarInventario">&nbsp;<a href="#" id="openerConsultarInventario"><img id="imgVerInventario" alt="Inventario" title="Haga clic para visualizar inventario de productos en bodega(s)." src="../Images/visualizar.png"/></a></div></td>
                            </tr>
                            <tr style="display:none;">
                                <td class="tdLeft">Proveedor</td>
                                <td class="tdLeft"><input id="txtNombreProveedor" type="text" class="textboxLongReadonly" readonly="true"/></td>
                                <td class="tdLeft">Grupo</td>
                                <td class="tdLeft"><input id="txtNombreGrupo" type="text" class="textboxLongReadonly" readonly="true"/></td>
                                <td class="tdLeft">Envase</td>
                                <td class="tdLeft"><input id="txtEnvaseProducto" type="text" class="textboxSmallReadonly" readonly="true"/></td>
                                <td class="tdLeft"></td>
                            </tr>
                            <tr style="display:none;">
                                <td class="tdLeft">#Fis</td>
                                <td class="tdLeft"><input id="txtStockFisico" type="text" class="textboxSmallerReadonly" readonly="true"/></td>
                                <td class="tdLeft">#Dis</td>
                                <td class="tdLeft"><input id="txtStockDisponible" type="text" class="textboxSmallerReadonly" readonly="true"/></td>
                                <td class="tdLeft"></td>
                                <td class="tdLeft"></td>
                                <td class="tdLeft"></td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divInventario" title="Inventario de Productos en Bodega(s)"></div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divProducto" title="Productos Solicitados desde Cotizacion"></div>
                                </td>
                            </tr>
                            <tr class="trTituloSeccion"><td colspan="7">Datos del Pedido</td></tr>
                            <tr>
                                <td class="tdLeft">Codigo</td>
                                <td class="tdLeft"><input id="txtCodigoProducto"          type="text" class="textboxMediumReadonly"  readonly="true"/></td>
                                <td class="tdLeft">Descripcion</td>   
                                <td class="tdLeft"><input id="txtNombreProducto"          type="text" class="textboxLongReadonly"    readonly="true"/></td>                               
                                <td class="tdLeft">Var.Hibrido</td>
                                <td class="tdLeft"><select id="cmbHibridoProducto" name="cmbHibridoProducto" class="textboxMedium"></select></td>
                                <td class="tdLeft"></td>
                            </tr>
                            <tr>
                                <td class="tdLeft">Cantidad</td>
                                <td class="tdLeft"><input id="txtCantidadProducto"        type="text" class="textboxSmall"           maxlength="6" onkeypress="return ValidarNumero(event);"/></td>
                                <td class="tdLeft">Precio Unitario Venta</td>
                                <td class="tdLeft"><input id="txtMonedaProducto"          type="text" class="textboxSmallerReadonly" readonly="true"/><input id="txtPrecioUnitarioProducto"  type="text" class="textboxSmall"           maxlength="13" onkeypress="return ValidarNumeroDecimal(event);"/></td>
                                <td class="tdLeft">Total Unitario CLP</td>
                                <td class="tdLeft"><input id="txtTotalUnitarioProducto"   type="text" class="textboxSmallReadonly"   readonly="true"/></td>
                                <td class="tdLeft"></td>
                            </tr>
                            <tr>
                                <td class="tdLeft">Descto.%</td>
                                <td class="tdLeft"><input id="txtDescuentoProducto"       type="text" class="textboxSmallerReadonly" readonly="true"/></td>
                                <td class="tdLeft">Margen %</td>
                                <td class="tdLeft"><input id="txtMargenComercialProducto" type="text" class="textboxSmallerReadonly" readonly="true"/></td>
                                <td class="tdLeft">Interes CLP</td>
                                <td class="tdLeft"><input id="txtInteresProducto"         type="text" class="textboxSmallReadonly"   readonly="true"/></td>
                                <td class="tdLeft"></td>
                            </tr>
                            <tr>
                                <td class="tdLeft">Flete CLP</td>
                                <td class="tdLeft"><input id="txtFleteProducto"           type="text" class="textboxSmallReadonly"   readonly="true"/> %<input id="hdnPorcentajeFlete" type="text" class="textboxSmallerReadonly" readonly="true"/></td>
                                <td class="tdLeft">Fecha Entrega</td>
                                <td class="tdLeft"><input id="txtFechaEntregaProducto"    type="text" class="textboxSmall"           readonly="true" onchange="ValidarFechaEntregaProducto();"/></td>
                                <td class="tdLeft">Precio Final CLP</td>
                                <td class="tdLeft"><input id="txtPrecioFinalProducto"     type="text" class="textboxSmallReadonly"   readonly="true"/></td>
                                <td class="tdRight"></td>
                            </tr>
                            <tr>
                                <td class="tdLeft">Tasa Interes %</td>
                                <td class="tdLeft"><input id="txtTasaInteresCompra"            type="text" class="textboxSmaller"         maxlength="4" onkeypress="return ValidarNumeroDecimal(event);"  value="1.80"/></td>
                                <td class="tdLeft">Precio Unitario Compra</td>
                                <td class="tdLeft"><input id="txtMonedaProductoCompra"         type="text" class="textboxSmallerReadonly" readonly="true"/><input id="txtPrecioUnitarioProductoCompra" type="text" class="textboxSmall" maxlength="13" onkeypress="return ValidarNumeroDecimal(event);"/></td>
                                <td class="tdLeft">Total Unitario CLP</td>
                                <td class="tdLeft"><input id="txtTotalUnitarioProductoCompra"  type="text" class="textboxSmallReadonly"   readonly="true"/></td>
                                <td class="tdRight"><img id="imgAgregar" alt="Agregar Pedido" title="Haga clic para agregar pedido." src="../Images/agregar.gif" onmouseover="ValidarLineaProducto();" onclick="ConfirmarVentaProducto();"/></td>
                            </tr>
                            <tr>
                                <td colspan="7" style="width:100%;">
                                    <div id="divPedido"></div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7" style="width:100%;">
                                    <div id="divFlete"></div>
                                </td>
                            </tr>
                            <tr><td colspan="7"><br /></td></tr>
                            <tr>
                                <td class="tdCenter" colspan="7">
                                    <img id="imgRetroceder" alt="Retroceder" title="Retroceder." src="../Images/retroceder.png" onclick="Retroceder();"/>
                                    <img id="imgAvanzar2"   alt="Avanzar"    title="Avanzar."    src="../Images/avanzar.png"    onclick="CargarCuadroAutorizacion();"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="row3" class="row" style="display:none">
                    <div class="col-sm-6">
                        <table border="0">
                            <tr class="trTituloSeccion"><td colspan="2">Direcciones</td></tr>
                            <tr><td>Facturacion</td><td><select id="cmbDireccionFactura"  name="cmbDireccionFactura"  class="textboxLonger"></select>&nbsp; <a href="#" id="openerAgregarDireccionFacturacion"><img id="img1" alt="Facturacion" title="Haga clic para agregar direccion para facturacion." src="../Images/agregar.gif"/></a></td></tr>
                            <tr><td>Despacho   </td><td><select id="cmbDireccionDespacho" name="cmbDireccionDespacho" class="textboxLonger"></select>&nbsp; <a href="#" id="openerAgregarDireccionDespacho">   <img id="img2" alt="Despacho"    title="Haga clic para agregar direccion para despacho."    src="../Images/agregar.gif"/></a></td></tr>
                            <tr>
                                <td>
                                    <div id="divDireccion" title="Crear Direccion">
                                        <table border="0">
                                            <tr><td colspan="2"><br /></td></tr>
                                            <tr><td>Tipo  </td><td><input id="txtDireccion" type="text" class="textboxLong"  readonly="true"/></td></tr>  
                                            <tr><td>Calle </td><td><input id="txtCalle"     type="text" class="textboxLong"  maxlength="50" onkeypress="return ValidarAlfanumerico(event);"/></td></tr>
                                            <tr><td>Numero</td><td><input id="txtNumero"    type="text" class="textboxSmall" maxlength="6"  onkeypress="return ValidarNumero(event);"/></td></tr>
                                            <tr><td>Comuna</td><td><input id="txtComuna"    type="text" class="textboxLong"  maxlength="30" onkeypress="return ValidarTexto(event);"/></td></tr>
                                            <tr><td>Ciudad</td><td><input id="txtCiudad"    type="text" class="textboxLong"  maxlength="30" onkeypress="return ValidarTexto(event);"/></td></tr>
                                            <tr><td>Region</td><td><select id="cmbRegion"  name="cmbRegion" class="textboxLong"></select></td></tr>
                                            <tr><td colspan="2"><br /></td></tr>                           
                                            <tr><td class="tdCenter" colspan="2"><input id="btnCrearDireccion" type="button" value="Grabar" onclick="CrearDireccion();"/></td></tr>
                                            <tr><td class="tdCenter" colspan="2"><div id="divLoader1"><img src="../Images/loader.gif"/></div></td></tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr class="trTituloSeccion"><td colspan="2">Comentarios</td></tr>
                            <tr><td>Facturacion<div id="divMsg1"></div></td><td><textarea id="txtComentario1" cols="62" rows="4" onKeyPress="return ValidarComentario(event);" onKeyDown="ValidarLongitudComentario(this,'#divMsg1');" onKeyUp="ValidarLongitudComentario(this,'#divMsg1');" onPaste="return false;"></textarea></td></tr>
                            <tr><td>Autorizador<div id="divMsg2"></div></td><td><textarea id="txtComentario2" cols="62" rows="4" onKeyPress="return ValidarComentario(event);" onKeyDown="ValidarLongitudComentario(this,'#divMsg2');" onKeyUp="ValidarLongitudComentario(this,'#divMsg2');" onPaste="return false;"></textarea></td></tr>
<%--                        <tr><td>Abastecimiento</td><td><textarea id="txtComentario3" cols="62" rows="4"></textarea></td></tr>--%>
                            <tr><td colspan="2"><br /></td></tr>
                            <tr id="trAutorizaciones" class="trTituloSeccion"><td colspan="2">Autorizaciones</td></tr>
                            <tr>
                                <td colspan="2" style="width:100%;">
                                    <div id="divAutorizacion"></div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="tdCenter">
                                    <input id="btnCargarPedido" type="button" value="Crear Borrador" onclick="CargarPedido();"/>
                                    <div id="divLoader2"><img src="../Images/loader.gif"/></div>
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <div id="divVoucher">
                                        <asp:Button ID="btnCrearVoucher" runat="server" CausesValidation="false" Text="Crear Voucher" OnClick="btnCrearVoucher_Click" OnClientClick="HabilitarVoucher();"/>
                                    </div>
                                    <div id="divCrearPedido"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdCenter" colspan="2">
                                    <img id="imgRetroceder2" alt="Retroceder" title="Retroceder." src="../Images/retroceder.png" onclick="Retroceder2();"/>
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