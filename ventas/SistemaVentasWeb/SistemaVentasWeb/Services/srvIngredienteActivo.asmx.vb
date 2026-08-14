Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class srvIngredienteActivo
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerIngredienteActivo(ByVal intOpcion As Integer) As List(Of clsIngredienteActivo)
        Dim Lista As New clsIngredienteActivoListado()
        Dim IngredienteActivo As New List(Of clsIngredienteActivo)
        IngredienteActivo = Lista.ObtenerIngredienteActivo(intOpcion)
        Return IngredienteActivo
    End Function
End Class