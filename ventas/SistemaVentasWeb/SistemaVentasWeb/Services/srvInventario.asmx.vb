Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvInventario
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerInventario(ByVal strProducto As String) As List(Of clsInventario)
        Dim Lista As New clsInventarioListado()
        Dim Inventario As New List(Of clsInventario)
        Inventario = Lista.ObtenerInventario(strProducto)
        Return Inventario
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ListarInventario(ByVal strProducto As String, ByVal strOperador As String) As List(Of clsInventario)
        Dim Lista As New clsInventarioListado()
        Dim Inventario As New List(Of clsInventario)
        Inventario = Lista.ListarInventario(strProducto, strOperador)
        Return Inventario
    End Function
End Class