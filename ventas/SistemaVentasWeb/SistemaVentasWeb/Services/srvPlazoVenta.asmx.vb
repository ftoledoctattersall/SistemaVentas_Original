Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvPlazoVenta
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerPlazoVenta(ByVal intOpcion As Integer) As List(Of clsPlazoVenta)
        Dim Lista As New clsPlazoVentaListado()
        Dim PlazoVenta As New List(Of clsPlazoVenta)
        PlazoVenta = Lista.ObtenerPlazoVenta(intOpcion)
        Return PlazoVenta
    End Function
End Class