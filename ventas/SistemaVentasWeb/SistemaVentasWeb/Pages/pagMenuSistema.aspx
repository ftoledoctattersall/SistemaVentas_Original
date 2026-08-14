<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pagMenuSistema.aspx.vb" Inherits="SistemaVentasWeb.pagMenuSistema" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta http-equiv="Expires" content="0"/>
        <meta http-equiv="Last-Modified" content="0"/>
        <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <title>Ventas Web - Menu Sistema</title>

        <link type="text/css" rel="stylesheet" href="../JQuery/ui/jquery-ui.css" />
        <script type="text/javascript" src="../JQuery/js/jquery-3.3.1.js"></script>

        <link rel="stylesheet" href="../Styles/stySitio.css"/>
        <script type="text/javascript" src="../../Scripts/scrFuncion.js"></script>
        <script type="text/javascript" src="../../Scripts/scrMenuSistema.js"></script>
    </head>
    <body style="zoom:75%">
        <form id="frmMenuSistema" name="frmMenuSistema" runat="server">
            <center>
                <asp:HiddenField ID="hdnUser" runat="server" Value="..." /><asp:HiddenField ID="hdnPassword" runat="server" /><asp:HiddenField ID="hdnEmpleado" runat="server" /><asp:HiddenField ID="hdnUsuario" runat="server" /><asp:HiddenField ID="hdnOperador" runat="server" /><asp:HiddenField ID="hdnOficina" runat="server" /><asp:HiddenField ID="hdnPerfil" runat="server" /><asp:HiddenField ID="hdnDolar" runat="server" /><asp:HiddenField ID="hdnEuro" runat="server" /><asp:HiddenField ID="hdnNivel" runat="server" /><asp:HiddenField ID="hdnRol" runat="server" /><asp:HiddenField ID="hdnActivo" runat="server" /><asp:HiddenField ID="hdnCotizador" runat="server" />
                <div id="divMenu"></div>
                <iframe id="ifrMuestraPagina" name="ifrMuestraPagina" runat="server"/>
            </center>
        </form>
        <script type="text/javascript" src="../Scripts/scrVersionamiento.js"></script>
    </body>
</html>