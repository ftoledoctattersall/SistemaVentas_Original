Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvMonitorAutorizacion
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerMonitorAutorizacionResumen(ByVal strCodigoCliente As String, ByVal strCodigoEstado As String, ByVal strFechaDesde As String, ByVal strFechaHasta As String, ByVal strOperador As String, ByVal intPagina As Integer) As List(Of clsMonitorAutorizacion)
        Dim clsMonitorAutorizacionListado As New clsMonitorAutorizacionListado()
        Dim clsMonitorAutorizacion As New List(Of clsMonitorAutorizacion)
        clsMonitorAutorizacion = clsMonitorAutorizacionListado.ObtenerMonitorAutorizacionResumen(strCodigoCliente, strCodigoEstado, strFechaDesde, strFechaHasta, strOperador, intPagina)
        Return clsMonitorAutorizacion
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerMonitorAutorizacionPagina(ByVal strCodigoCliente As String, ByVal strCodigoEstado As String, ByVal strFechaDesde As String, ByVal strFechaHasta As String, ByVal strOperador As String, ByVal intPagina As Integer) As List(Of clsMonitorAutorizacion)
        Dim clsMonitorAutorizacionListado As New clsMonitorAutorizacionListado()
        Dim clsMonitorAutorizacion As New List(Of clsMonitorAutorizacion)
        clsMonitorAutorizacion = clsMonitorAutorizacionListado.ObtenerMonitorAutorizacionPagina(strCodigoCliente, strCodigoEstado, strFechaDesde, strFechaHasta, strOperador, intPagina)
        Return clsMonitorAutorizacion
    End Function
End Class