Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvTipoDespacho
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ObtenerTipoDespacho() As List(Of clsTipoDespacho)
        Dim Lista As New clsTipoDespachoListado()
        Dim TipoDespacho As New List(Of clsTipoDespacho)
        TipoDespacho = Lista.ObtenerTipoDespacho()
        Return TipoDespacho
    End Function
End Class