Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class srvProveedor
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerProveedor(ByVal strProveedor As String) As List(Of clsProveedor)
        Dim Lista As New clsProveedorListado()
        Dim Proveedor As New List(Of clsProveedor)
        Proveedor = Lista.ObtenerProveedor(strProveedor)
        Return Proveedor
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerProveedorOrdenCompra(ByVal intDocEntry As Integer, ByVal intDocNum As Integer) As List(Of clsProveedor)
        Dim Lista As New clsProveedorListado()
        Dim Proveedor As New List(Of clsProveedor)
        Proveedor = Lista.ObtenerProveedorOrdenCompra(intDocEntry, intDocNum)
        Return Proveedor
    End Function
End Class