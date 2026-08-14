Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class srvPlazoCompra
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerPlazoCompra(ByVal intOpcion As Integer) As List(Of clsPlazoCompra)
        Dim Lista As New clsPlazoCompraListado()
        Dim PlazoCompra As New List(Of clsPlazoCompra)
        PlazoCompra = Lista.ObtenerPlazoCompra(intOpcion)
        Return PlazoCompra
    End Function
End Class