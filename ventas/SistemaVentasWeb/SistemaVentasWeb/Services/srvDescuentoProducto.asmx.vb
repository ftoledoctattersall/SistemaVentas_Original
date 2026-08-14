Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class srvDescuentoProducto
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerDescuentoProducto(ByVal intOpcion As Integer, ByVal strProducto As String, ByVal intPlazoVenta As Integer, ByVal intTotalUnitarioProducto As Double, ByVal strFechaVencimiento As String, ByVal strFechaCompra As String) As List(Of clsDescuentoProducto)
        Dim Lista As New clsDescuentoProductoListado()
        Dim DescuentoProducto As New List(Of clsDescuentoProducto)
        DescuentoProducto = Lista.ObtenerDescuentoProducto(intOpcion, strProducto, intPlazoVenta, intTotalUnitarioProducto, strFechaVencimiento, strFechaCompra)
        Return DescuentoProducto
    End Function
End Class