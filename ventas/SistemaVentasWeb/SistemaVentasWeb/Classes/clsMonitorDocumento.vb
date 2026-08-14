<Serializable()>
Public Class clsMonitorDocumento
    Public Property DocCodigo As Integer
    Public Property CliNombre As String
    Public Property DocFecha As String
    Public Property EntFecha As String
    Public Property VenFecha As String
    Public Property DocTipo As String
    Public Property DocEstado As String
    Public Property DocPagina As String
    Public Property ArtCodigo As String
    Public Property ArtNombre As String
    Public Property ArtCantidad As Integer
    Public Property ArtMoneda As String
    Public Property ArtPrecioUnitario As Decimal
    Public Property ArtPrecioTotal As Integer
    Public Property PorDescuento As Decimal
    Public Property ArtInteres As Integer
    Public Property FecEntrega As String
    Sub New()
    End Sub
End Class