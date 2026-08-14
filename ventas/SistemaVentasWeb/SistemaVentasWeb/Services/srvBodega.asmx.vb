Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvBodega
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ObtenerBodega(ByVal strOperador As String) As List(Of clsBodega)
        Dim Lista As New clsBodegaListado()
        Dim Bodega As New List(Of clsBodega)
        Bodega = Lista.ObtenerBodega(strOperador)
        Return Bodega
    End Function
End Class