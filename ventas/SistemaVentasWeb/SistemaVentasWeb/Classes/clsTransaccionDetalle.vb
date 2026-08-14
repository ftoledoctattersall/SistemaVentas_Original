<Serializable()>
Public Class clsTransaccionDetalle
    Public Property strCodigoProducto As String
    Public Property strNombreProducto As String
    Public Property intCantidadProducto As Integer
    Public Property strMonedaProducto As String
    Public Property dblPrecioUnitarioProducto As Decimal
    Public Property intTotalUnitarioProducto As Integer
    Public Property dblDescuentoProducto As Decimal
    Public Property intInteresProducto As Integer
    Public Property intFleteProducto As Integer
    Public Property strFechaEntregaProducto As String
    Public Property intPrecioFinalProducto As Integer
    Public Property dblCostoComercialProducto As Decimal
    Public Property dblMonedaProducto As Decimal
    Public Property dblDescuentoMaximo As Decimal
    Public Property dblCostoReposicionProducto As Decimal

    Public Property intDiasCompra As Integer
    Public Property strMonedaProductoCompra As String
    Public Property dblPrecioUnitarioProductoCompra As Decimal
    Public Property dblTotalUnitarioProductoCompra As Decimal
    Public Property strCodigoProveedorCompra As String

    Public Property strBodegaProducto As String
    Public Property dblTasaInteresCompra As Double
    Public Property strMonedaCostoComercialProducto As String

    Public Property strCondicionProducto As String
    Public Property intMotivoProducto As Integer
    Public Property strMotivoProducto As String

    Sub New()
    End Sub
End Class