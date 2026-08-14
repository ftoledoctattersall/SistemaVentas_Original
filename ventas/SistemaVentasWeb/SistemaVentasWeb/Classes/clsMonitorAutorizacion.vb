<Serializable()>
Public Class clsMonitorAutorizacion
    Public Property DocEntry As Integer
    Public Property DocCodigo As Integer
    Public Property DocPagina As String
    Public Property CliNombre As String
    Public Property DocFecha As String
    Public Property EntFecha As String
    Public Property VenFecha As String
    Public Property DocTotal As Integer
    Public Property AutTipo As String

    Public Property DisCodigo As String
    Public Property DisConcepto As String
    Public Property DisTipo As String

    Public Property VenTipo As String
    Public Property OfiNombre As String
    Sub New()
    End Sub
End Class