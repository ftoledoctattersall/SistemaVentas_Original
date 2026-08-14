<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pagVisualizarAutorizacion.aspx.vb" Inherits="SistemaVentasWeb.pagVisualizarAutorizacion" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta http-equiv="Expires" content="0"/>
        <meta http-equiv="Last-Modified" content="0"/>
        <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <title>Ventas Web - Visualizacion Autorizacion</title>
<!---
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"></script>

        <link type="text/css" rel="stylesheet" href="../JQuery/ui/jquery-ui.css" />
        <script type="text/javascript" src="../JQuery/js/jquery-3.3.1.js"></script>
        <script type="text/javascript" src="../JQuery/ui/jquery-ui.js"></script>
--->
        <link type="text/css" rel="stylesheet" href="../JQuery/ui/jquery-ui.css"/>
        <script type="text/javascript" src="../JQuery/js/jquery-3.3.1.js"></script>
        <script type="text/javascript" src="../JQuery/ui/jquery-ui.js"></script>

        <link type="text/css" rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.2.1/css/bootstrap.min.css"/>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.6/umd/popper.min.js"></script>
        <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/4.2.1/js/bootstrap.min.js"></script>

        <link type="text/css" rel="stylesheet" href="../Styles/stySitio.css"/>
        <script type="text/javascript" src="../Scripts/scrVisualizarAutorizacion.js"></script>
    </head>
    <body style="zoom:75%">
        <form id="frmVisualizarAutorizacion" runat="server" method="post" defaultbutton="btnDisableEnter">
            <asp:HiddenField ID="hdnDocEntryOV" runat="server"/>  
            <asp:HiddenField ID="hdnDocNumOV"   runat="server"/>           
            <div class="container-fluid">
                <div id="row1" class="row">
                    <div class="col-sm-9">
                        <%=strHtmlOrdenVenta%>
                        <table style="width:100%; align-items:center" border="0"> 
                            <tr><td class="tdCenter"><asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></td></tr>
                            <tr><td><br/></td></tr>
                            <tr>
                                <td class="tdCenter">
                                    <div id="divAutorizacion"></div>
                                </td>
                            </tr>
                            <tr><td><br/></td></tr>
                            <tr>
                                <td class="tdCenter">
                                    <input id="btnCargarPedido" type="button" value="Crear Borrador" onclick="CargarPedido();"/>
                                    <div id="divLoader2"><img src="../Images/loader.gif"/></div>
                                    <div id="divCrearPedido"></div>
                                </td>
                            </tr>
                            <tr><td class="tdCenter"><asp:ImageButton ID="imgRetroceder" runat="server" alt="Retroceder" title="Retroceder" src="../Images/retroceder.png" Width="24px" Height="24px" BorderWidth="0" BorderStyle="NotSet"/></td></tr>
                            <tr><td class="tdCenter"><div id="divLoader"><img src="../Images/loader.gif"/></div></td></tr>
                        </table>
                    </div>
                </div>
            </div>     
            <asp:Button ID="btnDisableEnter" runat="server" Text="" OnClientClick="return false;" style="display:none;"/>
        </form>
        <script type="text/javascript" src="../Scripts/scrVersionamiento.js"></script>
    </body>
</html>