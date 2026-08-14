Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class srvCotizacion
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerCotizacionResumen(ByVal intOpcion As Integer, ByVal intOperador As Integer, ByVal strEstado As String, ByVal intCotizacion As Integer) As List(Of clsCotizacionResumen)
        Dim Lista As New clsCotizacionListado()
        Dim CotizacionResumen As New List(Of clsCotizacionResumen)
        CotizacionResumen = Lista.ObtenerCotizacionResumen(intOpcion, intOperador, strEstado, intCotizacion)
        Return CotizacionResumen
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerCotizacionDetalle(ByVal intOpcion As Integer, ByVal intOperador As Integer, ByVal strEstado As String, ByVal intCotizacion As Integer) As List(Of clsCotizacionDetalle)
        Dim Lista As New clsCotizacionListado()
        Dim CotizacionResumen As New List(Of clsCotizacionDetalle)
        CotizacionResumen = Lista.ObtenerCotizacionDetalle(intOpcion, intOperador, strEstado, intCotizacion)
        Return CotizacionResumen
    End Function
End Class