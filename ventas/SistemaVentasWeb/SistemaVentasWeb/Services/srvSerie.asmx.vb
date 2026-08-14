Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvSerie
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerSerie(ByVal strObjeto As String, ByVal strSubObjeto As String, ByVal intUsuario As Integer) As List(Of clsSerie)
        Dim Lista As New clsSerieListado()
        Dim Serie As New List(Of clsSerie)
        Serie = Lista.ObtenerSerie(strObjeto, strSubObjeto, intUsuario)
        Return Serie
    End Function
End Class