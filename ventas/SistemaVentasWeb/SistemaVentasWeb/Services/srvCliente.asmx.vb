Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvCliente
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ObtenerCliente(ByVal strCliente As String) As List(Of clsCliente)
        Dim Lista As New clsClienteListado()
        Dim Cliente As New List(Of clsCliente)
        Cliente = Lista.ObtenerCliente(strCliente)
        Return Cliente
    End Function
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ObtenerClienteLineaCredito(ByVal strCliente As String) As List(Of clsCliente)
        Dim Lista As New clsClienteListado()
        Dim ClienteLineaCredito As New List(Of clsCliente)
        ClienteLineaCredito = Lista.ObtenerClienteLineaCredito(strCliente)
        Return ClienteLineaCredito
    End Function
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ObtenerClienteVendedorAsociado(ByVal strCliente As String) As List(Of clsCliente)
        Dim Lista As New clsClienteListado()
        Dim ClienteVendedorAsociado As New List(Of clsCliente)
        ClienteVendedorAsociado = Lista.ObtenerClienteVendedorAsociado(strCliente)
        Return ClienteVendedorAsociado
    End Function
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ObtenerClienteDireccion(ByVal intOpcion As Integer, ByVal strCliente As String) As List(Of clsCliente)
        Dim Lista As New clsClienteListado()
        Dim ClienteDireccion As New List(Of clsCliente)
        ClienteDireccion = Lista.ObtenerClienteDireccion(intOpcion, strCliente)
        Return ClienteDireccion
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function RegistrarDireccion(ByVal intDireccion As Integer, ByVal intUsuario As Integer, ByVal strCliente As String, ByVal strCalle As String, ByVal strNumero As String, ByVal strComuna As String, ByVal strCiudad As String, ByVal intRegion As Integer) As String
        Dim strRetorno As String = "-1"
        Dim fnc As clsFuncion = New clsFuncion()
        Dim Lista As New clsClienteListado()
        Dim ClienteDireccion As New clsCliente()
        ClienteDireccion.Direccion = intDireccion
        ClienteDireccion.UsuCodigo = intUsuario
        ClienteDireccion.CliCodigo = strCliente.Trim
        ClienteDireccion.DirCalle = strCalle.ToUpper.Trim
        ClienteDireccion.DirNumero = strNumero.ToUpper.Trim
        ClienteDireccion.DirComuna = strComuna.ToUpper.Trim
        ClienteDireccion.DirCiudad = strCiudad.ToUpper.Trim
        ClienteDireccion.DirRegion = intRegion.ToString.Trim
        If (intDireccion = 1) Then
            ClienteDireccion.DirTipo = "FacturacionWeb" + CStr(fnc.ObtenerRandom(1, 1000000))
        Else
            ClienteDireccion.DirTipo = "DespachoWeb" + CStr(fnc.ObtenerRandom(1, 1000000))
        End If
        strRetorno = Lista.RegistrarDireccion(ClienteDireccion)
        Return strRetorno
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerClienteFacturaImpaga(ByVal strCliente As String) As List(Of clsCliente)
        Dim clsClienteListado As New clsClienteListado()
        Dim clsCliente As New List(Of clsCliente)
        clsCliente = clsClienteListado.ObtenerClienteFacturaImpaga(strCliente)
        Return clsCliente
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerClienteChequeProtestado(ByVal strCliente As String) As List(Of clsCliente)
        Dim clsClienteListado As New clsClienteListado()
        Dim clsCliente As New List(Of clsCliente)
        clsCliente = clsClienteListado.ObtenerClienteChequeProtestado(strCliente)
        Return clsCliente
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function RegistrarCliente(ByVal intUsuario As Integer, ByVal intOperador As Integer, ByVal strRut As String, ByVal strDV As String, ByVal strNombre As String, ByVal strCorreo As String, ByVal intTelefono As String, ByVal strGiro As String, ByVal strMoneda As String, ByVal strCalle As String, ByVal strNumero As String, ByVal strComuna As String, ByVal strCiudad As String, ByVal intRegion As Integer) As String
        Dim Cliente As New clsClienteListado()
        Dim strRetorno As String = "Sin mensaje proveniente de la creacion."
        strRetorno = Cliente.RegistrarCliente(intUsuario, intOperador, strRut, strDV, strNombre, strCorreo, intTelefono, strGiro, strMoneda, strCalle, strNumero, strComuna, strCiudad, intRegion)
        Return strRetorno
    End Function
End Class