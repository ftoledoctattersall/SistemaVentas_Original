Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class srvEmpleado
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerEmpleado(ByVal intEmpleado As Integer, ByVal intOficina As Integer) As List(Of clsEmpleado)
        Dim Lista As New clsEmpleadoListado()
        Dim Empleado As New List(Of clsEmpleado)
        Empleado = Lista.ObtenerEmpleado(intEmpleado, intOficina)
        Return Empleado
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerEmpleadoOficina(ByVal intEmpleado As Integer, ByVal intOficina As Integer) As List(Of clsEmpleado)
        Dim Lista As New clsEmpleadoListado()
        Dim Empleado As New List(Of clsEmpleado)
        Empleado = Lista.ObtenerEmpleadoOficina(intEmpleado, intOficina)
        Return Empleado
    End Function
End Class