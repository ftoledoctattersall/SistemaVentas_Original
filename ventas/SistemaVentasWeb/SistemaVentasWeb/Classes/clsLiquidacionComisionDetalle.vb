<Serializable()>
Public Class clsLiquidacionComisionDetalle
    Public Property LiqIdentificador As Integer
    Public Property LinCodigo As Integer
    Public Property LinNombre As String
    Public Property SubNombre As String
    Public Property DocTipo As String
    Public Property TotNeto As Integer
    Public Property TotMargen As Integer
    Public Property FinComision As Integer
    Public Property BasComision As String
    Public Property NivComision As String

    Public Property OfiCodigo As Integer
    Public Property OfiNombre As String
    Public Property DocFecha As String
    Public Property FolNumero As Integer
    Public Property Doc4taCopia As String
    Public Property CliNombre As String
    Public Property BodNombre As String
    Public Property ArtCodigo As String
    Public Property ArtNombre As String
    Public Property ArtMedida As String
    Public Property ArtCantidad As Integer
    Public Property VenNeto As Integer
    Public Property VenMargen As Integer
    Public Property PorComision As Decimal
    Sub New()
    End Sub
End Class