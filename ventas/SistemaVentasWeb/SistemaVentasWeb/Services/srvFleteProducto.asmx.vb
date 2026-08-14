Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvFleteProducto
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ObtenerFleteProducto(ByVal strBodega As String, ByVal strProducto As String, ByVal intCantidadProducto As Integer, ByVal intTotalUnitarioProducto As Double) As List(Of clsFleteProducto)
        Dim Lista As New clsFleteProductoListado()
        Dim FleteProducto As New List(Of clsFleteProducto)
        FleteProducto = Lista.ObtenerFleteProducto(strBodega, strProducto, intCantidadProducto, intTotalUnitarioProducto)
        Return FleteProducto
    End Function
End Class