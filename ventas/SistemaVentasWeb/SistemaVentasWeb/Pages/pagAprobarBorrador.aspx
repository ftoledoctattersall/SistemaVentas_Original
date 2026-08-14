<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pagAprobarBorrador.aspx.vb" Inherits="SistemaVentasWeb.pagAprobarBorrador" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta http-equiv="Expires" content="0"/>
        <meta http-equiv="Last-Modified" content="0"/>
        <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <title>Ventas Web - Autorizacion</title>

        <link type="text/css" rel="stylesheet" href="../JQuery/ui/jquery-ui.css"/>
        <script type="text/javascript" src="../JQuery/js/jquery-3.3.1.js"></script>
        <script type="text/javascript" src="../JQuery/ui/jquery-ui.js"></script>

        <link type="text/css" rel="stylesheet" href="../Styles/stySitio.css"/>
        <script type="text/javascript" src="../Scripts/scrFuncion.js"></script>
    </head>
    <body style="zoom:75%">
        <form id="frmAprobarBorrador" runat="server">
            <table border="0">
                <tr><td class="tdLeft"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/logo.png"/></td></tr>
                <tr><td><br/></td></tr>
                <tr class="trTituloSeccion"><td class="tdCenter"><asp:Label ID="lblTituloSeccion" runat="server"/></td></tr>
                <tr><td><br/></td></tr>
                <tr><td class="tdCenter"><textarea id="txtComentario" runat="server" cols="85" rows="4" onKeyPress="return ValidarComentario(event);" onKeyDown="ValidarLongitudComentario(this,'#divMsg');" onKeyUp="ValidarLongitudComentario(this,'#divMsg');" onPaste="return false;"></textarea></td></tr>
                <tr><td class="tdCenter"><div id="divMsg"></div></td></tr>
                <tr><td><br/></td></tr>
                <tr><td><%=strHtmlDispatcher%></td></tr>
                <tr><td class="tdLeft"><asp:CheckBox ID="chkBloquear" runat="server" Text="Bloquear GD." Visible="false"/></td></tr>
                <tr><td class="tdCenter"><asp:Button ID="btnAceptar"  runat="server" Text="Aprobar"/></td></tr>
                <tr><td class="tdCenter"><asp:Label ID="lblMensaje"   runat="server" Text=""></asp:Label></td></tr>
            </table>
        </form>
        <script type="text/javascript" src="../Scripts/scrVersionamiento.js"></script>
    </body>
</html>