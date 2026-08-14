Imports System.Web.Services
Imports System.ComponentModel
Imports System.Web.Script.Services
<ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class srvCuartaCopia
    Inherits WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerCuartaCopia(ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer) As List(Of clsCuartaCopia)
        Dim Lista As New clsCuartaCopiaListado()
        Dim CuartaCopia As New List(Of clsCuartaCopia)
        CuartaCopia = Lista.ObtenerCuartaCopia(intOperador, intAño, intMes)
        Return CuartaCopia
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ActualizarCuartaCopia(ByVal intOperador As Integer, ByVal strCliente As String, ByVal strTipo As String, ByVal intDocto As Integer, ByVal intFolio As Integer, ByVal intIngreso As Integer) As String
        Dim Lista As New clsCuartaCopiaListado()
        Dim strRetorno As String = "Sin mensaje proveniente de la creacion."
        strRetorno = Lista.ActualizarCuartaCopia(intOperador, strCliente, strTipo, intDocto, intFolio, intIngreso)
        Return strRetorno
    End Function
End Class