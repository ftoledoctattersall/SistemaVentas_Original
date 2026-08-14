<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pagLoginSistema.aspx.vb" Inherits="SistemaVentasWeb.pagLoginSistema" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta http-equiv="Expires" content="0"/>
        <meta http-equiv="Last-Modified" content="0"/>
        <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <title>Ventas Web - Login Sistema</title>
    <%--<link type="text/css" href="../Styles/stySitio.css" rel="stylesheet"/>--%>
    <%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.3.js"></script>--%>
        <script type="text/javascript" src="../JQuery/js/jquery-3.3.1.min.js"></script>

        <link type="text/css" rel="stylesheet" href="../Styles/stySitio.css"/>
        <script type="text/javascript" src="../Scripts/scrFuncion.js"></script>
        <script type="text/javascript" src="../Scripts/scrLoginSistema.js"></script>
    </head>
    <body style="zoom:75%">
        <form id="frmLoginSistema" runat="server" method="post" action="pagMenuSistema.aspx">
            <center>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:HiddenField ID="hdnUser" runat="server" Value="..." /><asp:HiddenField ID="hdnPassword" runat="server" /><asp:HiddenField ID="hdnEmpleado" runat="server" /><asp:HiddenField ID="hdnUsuario" runat="server" /><asp:HiddenField ID="hdnOperador" runat="server" /><asp:HiddenField ID="hdnOficina" runat="server" /><asp:HiddenField ID="hdnPerfil" runat="server" /><asp:HiddenField ID="hdnDolar" runat="server" /><asp:HiddenField ID="hdnEuro" runat="server" /><asp:HiddenField ID="hdnNivel" runat="server" /><asp:HiddenField ID="hdnRol" runat="server" /><asp:HiddenField ID="hdnActivo" runat="server" /><asp:HiddenField ID="hdnCotizador" runat="server" />
                <table class="tableLogin">
                    <tr><td colspan="3" class="tdTituloLogin">*** SISTEMA DE VENTAS ***</td></tr>
                    <tr><td rowspan="3"><img id="imgLogo" src="../../Images/logo.png" height="50" width="200" /></td></tr>
                    <tr><td class="tdTituloItem">Usuario</td><td><input id="txtUser" type="text" maxlength="10" class="textboxMedium" value="" /></td></tr>
                    <tr><td class="tdTituloItem">Password</td><td><input id="txtPassword" type="password" maxlength="20" class="textboxMedium" value="" /></td></tr>
                    <tr><td colspan="3"><hr /></td></tr>
                    <tr><td colspan="3" class="tdRight"><input id="btnLimpiar" type="button" value="Limpiar" onclick="LimpiarCredenciales();" /><input id="btnAceptar" type="button" value="Aceptar" onclick="ValidarCredenciales();" /></td></tr>
                </table>
                <br />
                <asp:Label ID="lblAmbiente" runat="server" Text=""></asp:Label>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </center>
        </form>
        <script type="text/javascript" src="../Scripts/scrVersionamiento.js"></script>
    </body>
</html>