<Serializable()>
Public Class clsCliente
    Public Property CliCodigo As String
    Public Property CliNombre As String
    Public Property CliRut As String
    Public Property CliBloqueado As Boolean

    Public Property CliTelefono As String
    Public Property CliCorreo As String
    Public Property CliGiro As String

    Public Property CatCodigo As String
    Public Property CatNombre As String

    Public Property CliPlazoVentaCodigo As Integer
    Public Property CliPlazoVentaNombre As String
    Public Property CliMoneda As String


    Public Property GruCodigo As Integer
    Public Property GruNombre As String


    Public Property CliAcuerdo As String
    Public Property CliAutorizado As Double
    Public Property CliUtilizado As Double
    Public Property CliDisponible As Double

    Public Property VenCodigo As Integer
    Public Property VenNombre As String

    Public Property Direccion As Integer
    Public Property UsuCodigo As Integer
    Public Property DirTipo As String
    Public Property DirCalle As String
    Public Property DirNumero As String
    Public Property DirComuna As String
    Public Property DirCiudad As String
    Public Property DirCompleta As String
    Public Property DirRegion As String
    Public Property DirPais As String

    Public Property CheNumero As String
    Public Property CheFecha As String
    Public Property CheValor As Double

    Public Property FacNumero As String
    Public Property FacFecha As String
    Public Property FacValor As Double

    Sub New()
    End Sub
End Class