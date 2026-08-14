Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvMonitorDocumento
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerMonitorDocumentoResumen(ByVal strCodigoCliente As String, ByVal strCodigoProducto As String, ByVal strCodigoEstado As String, ByVal strFechaDesde As String, ByVal strFechaHasta As String, ByVal intOperador As Integer, ByVal intPagina As Integer) As List(Of clsMonitorDocumento)
        Dim clsMonitorDocumentoListado As New clsMonitorDocumentoListado()
        Dim clsMonitorDocumento As New List(Of clsMonitorDocumento)
        clsMonitorDocumento = clsMonitorDocumentoListado.ObtenerMonitorDocumentoResumen(strCodigoCliente, strCodigoProducto, strCodigoEstado, strFechaDesde, strFechaHasta, intOperador, intPagina)
        Return clsMonitorDocumento
    End Function
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ObtenerMonitorDocumentoDetalle(ByVal strDocTipo As String, ByVal intDocCodigo As Integer) As List(Of clsMonitorDocumento)
        Dim clsMonitorDocumentoListado As New clsMonitorDocumentoListado()
        Dim clsMonitorDocumento As New List(Of clsMonitorDocumento)
        clsMonitorDocumento = clsMonitorDocumentoListado.ObtenerMonitorDocumentoDetalle(strDocTipo, intDocCodigo)
        Return clsMonitorDocumento
    End Function
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ProcesarBorradorVentaEnOrdenVenta(ByVal intUsuario As Integer, ByVal intDocCodigo As Integer) As String
        Dim strRetorno As String = "-1||-1||Error de sesión."
        Dim clsAutorizacionesListado As New clsAutorizacionesListado()
        clsAutorizacionesListado.DocEntry = intDocCodigo
        If (clsAutorizacionesListado.ObtenerAutorizaciones(intDocCodigo)) Then
            If (clsAutorizacionesListado.ValidarAutorizaciones()) Then
                Dim clsOrdenVenta As New WebServices.clsOrdenVenta()
                Dim strDatos As Tuple(Of Integer, String) = clsOrdenVenta.RegistrarBorradorVentaEnOrdenVenta(intUsuario, clsAutorizacionesListado.DocEntry)
                'Dim strDatos As Tuple(Of Integer, String) = New clsOrdenVenta().RegistrarBorradorVentaEnOrdenVenta(intUsuario, AutorizacionesListado.DocEntry)
                Dim intStatus As Integer = strDatos.Item1
                Dim strStatus As String = strDatos.Item2
                If intStatus > 0 Then
                    Dim intBloqueaGD As Integer = 2
                    clsAutorizacionesListado.ActualizarEstadoBorradorVenta(clsAutorizacionesListado.DocEntry, intBloqueaGD)
                    clsAutorizacionesListado.ActualizarEstadoOrdenVenta(clsAutorizacionesListado.DocEntry, intStatus)
                    'AutorizacionesListado.ActualizarEstadoBorradorVenta(My.Resources.rscSistemaVentasWeb.wBorradorEstadoAutorizado)
                    strRetorno = "0||" + CStr(intStatus) + "||" + strStatus
                Else
                    strRetorno = "-2||" + CStr(intStatus) + "||Error SAP: " + CStr(intStatus) + ", " + strStatus
                End If
            Else
                strRetorno = "-3||-1||Documento no autorizado aún."
            End If
        Else
            strRetorno = "-4||-1||No se puede obtener la información de las autorizaciones."
        End If
        Return strRetorno
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ProcesarBorradorVentaEnFacturaVenta(ByVal intUsuario As Integer, ByVal intDocCodigo As Integer) As String
        Dim strRetorno As String = "-1||-1||Error de sesión."
        Dim clsAutorizacionesListado As New clsAutorizacionesListado()
        clsAutorizacionesListado.DocEntry = intDocCodigo
        If (clsAutorizacionesListado.ObtenerAutorizaciones(intDocCodigo)) Then
            If (clsAutorizacionesListado.ValidarAutorizaciones()) Then
                Dim clsOrdenVenta As New WebServices.clsOrdenVenta()
                Dim strDatos As Tuple(Of Integer, String) = clsOrdenVenta.RegistrarBorradorVentaEnOrdenVenta(intUsuario, clsAutorizacionesListado.DocEntry)
                'Dim strDatos As Tuple(Of Integer, String) = New clsOrdenVenta().RegistrarBorradorVentaEnFacturaVenta(intUsuario, AutorizacionesListado.DocEntry)
                Dim intStatus As Integer = strDatos.Item1
                Dim strStatus As String = strDatos.Item2
                If intStatus > 0 Then
                    Dim intBloqueaGD As Integer = 2
                    clsAutorizacionesListado.ActualizarEstadoBorradorVenta(clsAutorizacionesListado.DocEntry, intBloqueaGD)
                    'AutorizacionesListado.ActualizarEstadoBorradorVenta(My.Resources.rscSistemaVentasWeb.wBorradorEstadoAutorizado)
                    strRetorno = "0||" + CStr(intStatus) + "||" + strStatus
                Else
                    strRetorno = "-2||" + CStr(intStatus) + "||Error SAP: " + CStr(intStatus) + ", " + strStatus
                End If
            Else
                strRetorno = "-3||-1||Documento no autorizado aún."
            End If
        Else
            strRetorno = "-4||-1||No se puede obtener la información de las autorizaciones."
        End If
        Return strRetorno
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerMonitorDocumentoPagina(ByVal strCodigoCliente As String, ByVal strCodigoProducto As String, ByVal strCodigoEstado As String, ByVal strFechaDesde As String, ByVal strFechaHasta As String, ByVal intOperador As Integer, ByVal intPagina As Integer) As List(Of clsMonitorDocumento)
        Dim clsMonitorDocumentoListado As New clsMonitorDocumentoListado()
        Dim clsMonitorDocumento As New List(Of clsMonitorDocumento)
        clsMonitorDocumento = clsMonitorDocumentoListado.ObtenerMonitorDocumentoPagina(strCodigoCliente, strCodigoProducto, strCodigoEstado, strFechaDesde, strFechaHasta, intOperador, intPagina)
        Return clsMonitorDocumento
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function CancelarOrdenVenta(ByVal intDocCodigo As String, ByVal intUsuario As String) As String
        Dim clsMonitorDocumentoListado As New clsMonitorDocumentoListado()
        Dim clsMonitorDocumento As New List(Of clsMonitorDocumento)
        Dim strRetorno As String = ""
        Dim Pedido As Tuple(Of Integer, Integer, String)
        Pedido = clsMonitorDocumentoListado.CancelarOrdenVenta(intDocCodigo, intUsuario)
        strRetorno = Pedido.Item1.ToString + "|" + Pedido.Item2.ToString + "|" + Pedido.Item3.ToString
        Return strRetorno
    End Function
End Class