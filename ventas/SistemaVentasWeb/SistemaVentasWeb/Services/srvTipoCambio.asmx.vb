Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvTipoCambio
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ObtenerTipoCambio(ByVal intOpcion As Integer, ByVal strMoneda As String, ByVal strFecha As String) As List(Of clsTipoCambio)
        Dim Lista As New clsTipoCambioListado()
        Dim TipoCambio As New List(Of clsTipoCambio)
        TipoCambio = Lista.ObtenerTipoCambio(intOpcion, strMoneda, strFecha)
        Return TipoCambio
    End Function
End Class