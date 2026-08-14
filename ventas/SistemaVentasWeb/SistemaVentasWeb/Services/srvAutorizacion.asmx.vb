Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvAutorizacion
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerAutorizacion(ByVal intOpcion As Integer, ByVal strOperador As String, ByVal strBodega As String, ByVal strProducto As String, ByVal strFechaOrdenVenta As String, ByVal dblPrecioUnitarioProducto As Double, ByVal strTipoVenta As String) As String
        Dim Lista As New clsAutorizacionListado()
        Dim strRetorno As String = "0"
        strRetorno = Lista.ObtenerAutorizacion(intOpcion, strOperador, strBodega, strProducto, strFechaOrdenVenta, dblPrecioUnitarioProducto, strTipoVenta)
        Return strRetorno
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerAutorizacionModal(ByVal intOpcion As Integer, ByVal strOperador As String, ByVal strBodega As String, ByVal strProducto As String, ByVal strFechaOrdenVenta As String, ByVal dblPrecioUnitarioProducto As Double, ByVal strTipoVenta As String) As String
        Dim Lista As New clsAutorizacionListado()
        Dim strRetorno As String = "0"
        strRetorno = Lista.ObtenerAutorizacionModal(intOpcion, strOperador, strBodega, strProducto, strFechaOrdenVenta, dblPrecioUnitarioProducto, strTipoVenta)
        Return strRetorno
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerAutorizacionEspecial(ByVal strOperador As String, ByVal strTipo As String) As String
        Dim Lista As New clsAutorizacionListado()
        Dim strRetorno As String = ""
        strRetorno = Lista.ObtenerAutorizacionEspecial(strOperador, strTipo)
        Return strRetorno
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function EnviarAutorizacionCorreo(ByVal dblValorDolar As Double, ByVal dblValorEuro As Double, ByVal intDocEntry As Integer, ByVal strTipoVenta As String, ByVal strCliente As String, ByVal strNombreCliente As String, ByVal strRutCliente As String, ByVal strTelefonoCliente As String, ByVal strGiroCliente As String, ByVal strCorreoCliente As String, ByVal strGrupoCliente As String, ByVal strMonedaPago As String, ByVal strOrdenCompra As String, ByVal intVendedor As Integer, ByVal intOperador As Integer, ByVal strCodigoOperador As String, ByVal strNombreOperador As String, ByVal strFechaOrdenVenta As String, ByVal strFechaEntrega As String, ByVal intSerie As Integer, ByVal strSerie As String, ByVal intTipoDespacho As Integer, ByVal strTipoDespacho As String, ByVal strNotaPedido As String, ByVal intCodigoPlazoVenta As Integer, ByVal strNombrePlazoVenta As String, ByVal strFechaVencimiento As String, ByVal strDireccionFactura As String, ByVal strDireccionDespacho As String, ByVal strComentario1 As String, ByVal strComentario2 As String, ByVal strComentario3 As String, ByVal strMotivo As String, ByVal strEmision As String, ByVal strGuiaDespacho As String, ByVal strCategoria As String, ByVal strVendedor As String, ByVal strPedido As String) As String
        Dim Lista As New clsAutorizacionListado()
        Dim strRetorno As String = ""
        strRetorno = Lista.EnviarAutorizacionCorreo(dblValorDolar, dblValorEuro, intDocEntry, strTipoVenta, strCliente, strNombreCliente, strRutCliente, strTelefonoCliente, strGiroCliente, strCorreoCliente, strGrupoCliente, strMonedaPago, strOrdenCompra, intVendedor, intOperador, strCodigoOperador, strNombreOperador, strFechaOrdenVenta, strFechaEntrega, intSerie, strSerie, intTipoDespacho, strTipoDespacho, strNotaPedido, intCodigoPlazoVenta, strNombrePlazoVenta, strFechaVencimiento, strDireccionFactura, strDireccionDespacho, strComentario1, strComentario2, strComentario3, strMotivo, strEmision, strGuiaDespacho, strCategoria, strVendedor, strPedido)
        Return strRetorno
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function EnviarAutorizacionCorreoCredito(ByVal strOperador As String, ByVal intDocEntry As Integer) As String
        Dim Lista As New clsAutorizacionListado()
        Dim strRetorno As String = ""
        strRetorno = Lista.EnviarAutorizacionCorreoCredito(strOperador, intDocEntry)
        Return strRetorno
    End Function
End Class