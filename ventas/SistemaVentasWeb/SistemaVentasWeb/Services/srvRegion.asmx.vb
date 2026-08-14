Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvRegion
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ObtenerRegion() As List(Of clsRegion)
        Dim Lista As New clsRegionListado()
        Dim Region As New List(Of clsRegion)
        Region = Lista.ObtenerRegion()
        Return Region
    End Function
End Class