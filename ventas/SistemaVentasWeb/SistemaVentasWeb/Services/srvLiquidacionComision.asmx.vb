Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class srvLiquidacionComision
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerLiquidacionComisionResumen(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String) As List(Of clsLiquidacionComisionResumen)
        Dim LiquidacionComisionListado As New clsLiquidacionComisionListado()
        Dim LiquidacionComisionResumen As New List(Of clsLiquidacionComisionResumen)
        LiquidacionComisionResumen = LiquidacionComisionListado.ObtenerLiquidacionComisionResumen(intEmpleado, intOperador, intAño, intMes, intIdentificador, strOficina, intLinea, strSubLinea, strDocto)
        Return LiquidacionComisionResumen
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerLiquidacionComisionDetalleLinea(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String) As List(Of clsLiquidacionComisionDetalle)
        Dim LiquidacionComisionListado As New clsLiquidacionComisionListado()
        Dim LiquidacionComisionDetalle As New List(Of clsLiquidacionComisionDetalle)
        LiquidacionComisionDetalle = LiquidacionComisionListado.ObtenerLiquidacionComisionDetalleLinea(intEmpleado, intOperador, intAño, intMes, intIdentificador, strOficina, intLinea, strSubLinea, strDocto)
        Return LiquidacionComisionDetalle
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerLiquidacionComisionDetalleDocumento(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String) As List(Of clsLiquidacionComisionDetalle)
        Dim LiquidacionComisionListado As New clsLiquidacionComisionListado()
        Dim LiquidacionComisionDetalle As New List(Of clsLiquidacionComisionDetalle)
        LiquidacionComisionDetalle = LiquidacionComisionListado.ObtenerLiquidacionComisionDetalleDocumento(intEmpleado, intOperador, intAño, intMes, intIdentificador, strOficina, intLinea, strSubLinea, strDocto)
        Return LiquidacionComisionDetalle
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerLiquidacionComisionDetalleLineaEspecial(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String) As List(Of clsLiquidacionComisionDetalle)
        Dim LiquidacionComisionListado As New clsLiquidacionComisionListado()
        Dim LiquidacionComisionDetalle As New List(Of clsLiquidacionComisionDetalle)
        LiquidacionComisionDetalle = LiquidacionComisionListado.ObtenerLiquidacionComisionDetalleLineaEspecial(intEmpleado, intOperador, intAño, intMes, intIdentificador, strOficina, intLinea, strSubLinea, strDocto)
        Return LiquidacionComisionDetalle
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerLiquidacionComisionDetalleDocumentoEspecial(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String) As List(Of clsLiquidacionComisionDetalle)
        Dim LiquidacionComisionListado As New clsLiquidacionComisionListado()
        Dim LiquidacionComisionDetalle As New List(Of clsLiquidacionComisionDetalle)
        LiquidacionComisionDetalle = LiquidacionComisionListado.ObtenerLiquidacionComisionDetalleDocumentoEspecial(intEmpleado, intOperador, intAño, intMes, intIdentificador, strOficina, intLinea, strSubLinea, strDocto)
        Return LiquidacionComisionDetalle
    End Function



    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ValidarUsuarioComisionProvisorio(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String) As String
        Dim LiquidacionComisionListado As New clsLiquidacionComisionListado()
        Dim blnUsuarioEspecial As String = "False"
        blnUsuarioEspecial = LiquidacionComisionListado.ValidarUsuarioComisionProvisorio(intEmpleado, intOperador, intAño, intMes, intIdentificador, strOficina, intLinea, strSubLinea, strDocto)
        Return blnUsuarioEspecial
    End Function



    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerComisionDetalleProvisorioLinea(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String) As List(Of clsLiquidacionComisionDetalle)
        Dim LiquidacionComisionListado As New clsLiquidacionComisionListado()
        Dim LiquidacionComisionDetalle As New List(Of clsLiquidacionComisionDetalle)
        LiquidacionComisionDetalle = LiquidacionComisionListado.ObtenerComisionDetalleProvisorioLinea(intEmpleado, intOperador, intAño, intMes, intIdentificador, strOficina, intLinea, strSubLinea, strDocto)
        Return LiquidacionComisionDetalle
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerComisionDetalleProvisorioDocumento(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String) As List(Of clsLiquidacionComisionDetalle)
        Dim LiquidacionComisionListado As New clsLiquidacionComisionListado()
        Dim LiquidacionComisionDetalle As New List(Of clsLiquidacionComisionDetalle)
        LiquidacionComisionDetalle = LiquidacionComisionListado.ObtenerComisionDetalleProvisorioDocumento(intEmpleado, intOperador, intAño, intMes, intIdentificador, strOficina, intLinea, strSubLinea, strDocto)
        Return LiquidacionComisionDetalle
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerComisionDetalleProvisorioLineaEspecial(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String) As List(Of clsLiquidacionComisionDetalle)
        Dim LiquidacionComisionListado As New clsLiquidacionComisionListado()
        Dim LiquidacionComisionDetalle As New List(Of clsLiquidacionComisionDetalle)
        LiquidacionComisionDetalle = LiquidacionComisionListado.ObtenerComisionDetalleProvisorioLineaEspecial(intEmpleado, intOperador, intAño, intMes, intIdentificador, strOficina, intLinea, strSubLinea, strDocto)
        Return LiquidacionComisionDetalle
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerComisionDetalleProvisorioDocumentoEspecial(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String) As List(Of clsLiquidacionComisionDetalle)
        Dim LiquidacionComisionListado As New clsLiquidacionComisionListado()
        Dim LiquidacionComisionDetalle As New List(Of clsLiquidacionComisionDetalle)
        LiquidacionComisionDetalle = LiquidacionComisionListado.ObtenerComisionDetalleProvisorioDocumentoEspecial(intEmpleado, intOperador, intAño, intMes, intIdentificador, strOficina, intLinea, strSubLinea, strDocto)
        Return LiquidacionComisionDetalle
    End Function
End Class