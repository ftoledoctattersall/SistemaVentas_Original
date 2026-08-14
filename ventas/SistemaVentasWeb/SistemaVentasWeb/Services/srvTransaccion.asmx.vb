Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvTransaccion
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerTransaccionResumen(ByVal intConsulta As Integer, ByVal intDocCodigo As Integer) As List(Of clsTransaccionResumen)
        Dim clsTransaccionListado As New clsTransaccionListado()
        Dim clsTransaccionResumen As New List(Of clsTransaccionResumen)
        clsTransaccionResumen = clsTransaccionListado.ObtenerTransaccionResumen(intConsulta, intDocCodigo)
        Return clsTransaccionResumen
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerTransaccionDetalle(ByVal intConsulta As Integer, ByVal intDocCodigo As Integer, ByVal strProveedor As String, ByVal intDiaCompra As Integer) As List(Of clsTransaccionDetalle)
        Dim clsTransaccionListado As New clsTransaccionListado()
        Dim clsTransaccionDetalle As New List(Of clsTransaccionDetalle)
        clsTransaccionDetalle = clsTransaccionListado.ObtenerTransaccionDetalle(intConsulta, intDocCodigo, strProveedor, intDiaCompra)
        Return clsTransaccionDetalle
    End Function
End Class