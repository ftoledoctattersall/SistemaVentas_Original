Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvMenuSistema
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerMenuSistema(ByVal intOpcion As Integer, ByVal intPerfil As Integer, ByVal strUser As String) As List(Of clsMenuSistema)
        Dim Lista As New clsMenuSistemaListado()
        Dim MenuSistema As New List(Of clsMenuSistema)
        MenuSistema = Lista.ObtenerMenuSistema(intOpcion, intPerfil, strUser)
        Return MenuSistema
    End Function
End Class