Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvProducto
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerProducto(ByVal intOpcion As Integer, ByVal strProducto As String, ByVal strBodega As String) As List(Of clsProducto)
        Dim Lista As New clsProductoListado()
        Dim Producto As New List(Of clsProducto)
        Producto = Lista.ObtenerProducto(intOpcion, strProducto, strBodega)
        Return Producto
    End Function
    '<WebMethod(EnableSession:=True)>
    '<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    'Public Function ListarProducto(ByVal strProducto As String) As List(Of clsProducto)
    '    Dim Lista As New clsProductoListado()
    '    Dim Producto As New List(Of clsProducto)
    '    Producto = Lista.ListarProducto(strProducto)
    '    Return Producto
    'End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ListarProducto(ByVal intOpcion As Integer, ByVal strProducto As String, ByVal strBodega As String, ByVal strIngrediente As String) As List(Of clsProducto)
        Dim Lista As New clsProductoListado()
        Dim Producto As New List(Of clsProducto)
        Producto = Lista.ListarProducto(intOpcion, strProducto, strBodega, strIngrediente)
        Return Producto
    End Function
End Class