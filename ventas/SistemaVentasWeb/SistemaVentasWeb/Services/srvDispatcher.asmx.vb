Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvDispatcher
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ObtenerDispatcher(ByVal intDocCodigo As Integer) As List(Of clsDispatcher)
        Dim Lista As New clsDispatcherListado()
        Dim Dispatcher As New List(Of clsDispatcher)
        Dispatcher = Lista.ObtenerDispatcher(intDocCodigo)
        Return Dispatcher
    End Function
End Class