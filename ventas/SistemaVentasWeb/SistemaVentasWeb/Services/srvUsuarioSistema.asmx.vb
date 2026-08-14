Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class srvUsuarioSistema
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ValidarUsuarioSistema(ByVal intOpcion As Integer, ByVal strUser As String, ByVal strPassword As String) As String
        Dim srv As clsUsuarioSistemaListado = New clsUsuarioSistemaListado()
        Return srv.ValidarUsuarioSistema(intOpcion, strUser, strPassword)
    End Function
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ObtenerIdesUsuarioSistema(ByVal intOpcion As Integer, ByVal strUser As String, ByVal strPassword As String) As List(Of clsUsuarioSistema)
        Dim Lista As New clsUsuarioSistemaListado()
        Dim IdesUsuarioSistema As New List(Of clsUsuarioSistema)
        IdesUsuarioSistema = Lista.ObtenerIdesUsuarioSistema(intOpcion, strUser, strPassword)
        Return IdesUsuarioSistema
    End Function
    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function ObtenerPerfilUsuarioSistema(ByVal intOpcion As Integer, ByVal strUser As String, ByVal strPassword As String) As List(Of clsUsuarioSistema)
        Dim Lista As New clsUsuarioSistemaListado()
        Dim PerfilUsuarioSistema As New List(Of clsUsuarioSistema)
        PerfilUsuarioSistema = Lista.ObtenerPerfilUsuarioSistema(intOpcion, strUser, strPassword)
        Return PerfilUsuarioSistema
    End Function
End Class