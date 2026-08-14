Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvInteresProducto
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerInteresProducto(ByVal intOpcion As Integer, ByVal strCliente As String, ByVal strBodega As String, ByVal strProducto As String, ByVal intPlazoVenta As Integer, ByVal intTotalUnitarioProducto As Double, ByVal fltTasaInteres As Double, ByVal strFechaVencimiento As String, ByVal strFechaCompra As String) As List(Of clsInteresProducto)
        Dim Lista As New clsInteresProductoListado()
        Dim InteresProducto As New List(Of clsInteresProducto)
        InteresProducto = Lista.ObtenerInteresProducto(intOpcion, strCliente, strBodega, strProducto, intPlazoVenta, intTotalUnitarioProducto, fltTasaInteres, strFechaVencimiento, strFechaCompra)
        Return InteresProducto
    End Function
End Class