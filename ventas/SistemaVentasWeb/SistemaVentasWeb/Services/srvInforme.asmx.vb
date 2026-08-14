Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class srvInforme
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerInformeCuentaCorriente(ByVal strCodigoCliente As String) As List(Of clsInforme)
        Dim clsInformeListado As New clsInformeListado()
        Dim clsInforme As New List(Of clsInforme)
        clsInforme = clsInformeListado.ObtenerInformeCuentaCorriente(strCodigoCliente)
        Return clsInforme
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerInformeVentaMensual(ByVal intUsuario As String, ByVal strCodigoCliente As String, ByVal intAño As Integer, ByVal intMes As Integer) As List(Of clsInforme)
        Dim clsInformeListado As New clsInformeListado()
        Dim clsInforme As New List(Of clsInforme)
        clsInforme = clsInformeListado.ObtenerInformeVentaMensual(intUsuario, strCodigoCliente, intAño, intMes)
        Return clsInforme
    End Function
End Class