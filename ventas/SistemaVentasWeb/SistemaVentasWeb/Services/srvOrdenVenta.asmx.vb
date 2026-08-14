Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class srvOrdenVenta
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function RegistrarBorradorVenta(ByVal intUsuario As Integer, ByVal strTipoVenta As String, ByVal strCliente As String, ByVal strMonedaPago As String, ByVal strOrdenCompra As String, ByVal intVendedor As Integer, ByVal intOperador As Integer, ByVal strFechaOrdenVenta As String, ByVal strFechaEntrega As String, ByVal intSerie As Integer, ByVal intTipoDespacho As Integer, ByVal strNotaPedido As String, ByVal intCodigoPlazoVenta As Integer, ByVal strFechaVencimiento As String, ByVal intCodigoPlazoCompra As Integer, ByVal strDireccionFactura As String, ByVal strDireccionDespacho As String, ByVal strComentario1 As String, ByVal strComentario2 As String, ByVal strComentario3 As String, ByVal intMotivo As Integer, ByVal strEmision As String, ByVal strGuiaDespacho As String, ByVal strPedido As String) As String
        ToLog.write("ESTOY AQUI EN EL SERVICIO srvOrdenVenta (RegistrarBorradorVenta) del PROYECTO..................................................", "D")

        'Dim Lista As New clsOrdenVenta()
        'Dim OrdenVenta As New List(Of clsOrden)
        'OrdenVenta = Lista.RegistrarBorradorVenta(intUsuario, strTipoVenta, strCliente, strMonedaPago, strOrdenCompra, intVendedor, intOperador, strFechaOrdenVenta, strFechaEntrega, intSerie, intTipoDespacho, strNotaPedido, intCodigoPlazoVenta, strFechaVencimiento, intCodigoPlazoCompra, strDireccionFactura, strDireccionDespacho, strComentario1, strComentario2, strComentario3, strPedido)
        'Return OrdenVenta

        Dim OrdenVenta As New clsOrdenVenta()
        Dim strRetorno As String = ""
        Dim Pedido As Tuple(Of Integer, Integer, Integer, String)
        Pedido = OrdenVenta.RegistrarBorradorVenta(intUsuario, strTipoVenta, strCliente, strMonedaPago, strOrdenCompra, intVendedor, intOperador, strFechaOrdenVenta, strFechaEntrega, intSerie, intTipoDespacho, strNotaPedido, intCodigoPlazoVenta, strFechaVencimiento, intCodigoPlazoCompra, strDireccionFactura, strDireccionDespacho, strComentario1, strComentario2, strComentario3, intMotivo, strEmision, strGuiaDespacho, strPedido)
        strRetorno = Pedido.Item1.ToString + "|" + Pedido.Item2.ToString + "|" + Pedido.Item3.ToString + "|" + Pedido.Item4.Trim
        ToLog.write("strRetorno: " + strRetorno.ToString, "D")
        Return strRetorno
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function RegistrarOrdenVenta(ByVal intUsuario As Integer, ByVal strTipoVenta As String, ByVal strCliente As String, ByVal strMonedaPago As String, ByVal strOrdenCompra As String, ByVal intVendedor As Integer, ByVal intOperador As Integer, ByVal strFechaOrdenVenta As String, ByVal strFechaEntrega As String, ByVal intSerie As Integer, ByVal intTipoDespacho As Integer, ByVal strNotaPedido As String, ByVal intCodigoPlazoVenta As Integer, ByVal strFechaVencimiento As String, ByVal intCodigoPlazoCompra As Integer, ByVal strDireccionFactura As String, ByVal strDireccionDespacho As String, ByVal strComentario1 As String, ByVal strComentario2 As String, ByVal strComentario3 As String, ByVal intMotivo As Integer, ByVal strEmision As String, ByVal strGuiaDespacho As String, ByVal strPedido As String) As String
        ToLog.write("ESTOY AQUI EN EL SERVICIO srvOrdenVenta (RegistrarOrdenVenta) del PROYECTO..................................................", "D")

        'Dim Lista As New clsOrdenVenta()
        'Dim OrdenVenta As New List(Of clsOrden)
        'OrdenVenta = Lista.RegistrarOrdenVenta(intUsuario, strTipoVenta, strCliente, strMonedaPago, strOrdenCompra, intVendedor, intOperador, strFechaOrdenVenta, strFechaEntrega, intSerie, intTipoDespacho, strNotaPedido, intCodigoPlazoVenta, strFechaVencimiento, intCodigoPlazoCompra, strDireccionFactura, strDireccionDespacho, strComentario1, strComentario2, strComentario3, strPedido)
        'Return OrdenVenta

        Dim OrdenVenta As New clsOrdenVenta()
        Dim strRetorno As String = ""
        Dim Pedido As Tuple(Of Integer, Integer, Integer, String)
        Pedido = OrdenVenta.RegistrarOrdenVenta(intUsuario, strTipoVenta, strCliente, strMonedaPago, strOrdenCompra, intVendedor, intOperador, strFechaOrdenVenta, strFechaEntrega, intSerie, intTipoDespacho, strNotaPedido, intCodigoPlazoVenta, strFechaVencimiento, intCodigoPlazoCompra, strDireccionFactura, strDireccionDespacho, strComentario1, strComentario2, strComentario3, intMotivo, strEmision, strGuiaDespacho, strPedido)
        strRetorno = Pedido.Item1.ToString + "|" + Pedido.Item2.ToString + "|" + Pedido.Item3.ToString + "|" + Pedido.Item4.Trim
        ToLog.write("strRetorno: " + strRetorno.ToString, "D")
        Return strRetorno
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function CancelarOrdenVenta(ByVal intDocCodigo As String, ByVal intUsuario As String) As String
        Dim OrdenVenta As New clsOrdenVenta
        Dim strRetorno As String = ""
        Dim Pedido As Tuple(Of Integer, Integer, String)
        Pedido = OrdenVenta.CancelarOrdenVenta(intDocCodigo, intUsuario)
        strRetorno = Pedido.Item1.ToString + "|" + Pedido.Item2.ToString + "|" + Pedido.Item3.ToString
        Return strRetorno
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function RegistrarOrdenVentaEnBorradorVenta(ByVal intUsuario As Integer, ByVal intDocEntry As Integer) As String
        Dim OrdenVenta As New clsOrdenVenta()
        Dim strRetorno As String = ""
        Dim Pedido As Tuple(Of Integer, Integer, Integer, String)
        Pedido = OrdenVenta.RegistrarOrdenVentaEnBorradorVenta(intUsuario, intDocEntry)
        strRetorno = Pedido.Item1.ToString + "|" + Pedido.Item2.ToString + "|" + Pedido.Item3.ToString + "|" + Pedido.Item4.Trim
        Return strRetorno
    End Function
End Class