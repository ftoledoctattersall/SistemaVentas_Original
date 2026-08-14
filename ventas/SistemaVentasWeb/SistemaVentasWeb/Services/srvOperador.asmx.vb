Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvOperador
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerOperador(ByVal intOperador As Integer, ByVal intOficina As Integer) As List(Of clsOperador)
        Dim Lista As New clsOperadorListado()
        Dim Operador As New List(Of clsOperador)
        Operador = Lista.ObtenerOperador(intOperador, intOficina)
        Return Operador
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerOperadorOficina(ByVal intOperador As Integer, ByVal intOficina As Integer) As List(Of clsOperador)
        Dim Lista As New clsOperadorListado()
        Dim Operador As New List(Of clsOperador)
        Operador = Lista.ObtenerOperadorOficina(intOperador, intOficina)
        Return Operador
    End Function
End Class