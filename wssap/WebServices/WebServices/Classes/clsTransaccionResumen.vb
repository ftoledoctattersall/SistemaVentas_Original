<Serializable()>
Public Class clsTransaccionResumen
    Public Property intDocEntry As Integer
    Public Property intDocNum As String
    Public Property strNombreCliente As String
    Public Property strRutCliente As String
    Public Property strCodigoCliente As String
    Public Property strCategoriaCliente As String
    Public Property strTelefonoCliente As String
    Public Property strCorreoCliente As String
    Public Property strGiroCliente As String
    Public Property strNombreOperador As String
    Public Property strSerie As String
    Public Property strNombrePlazoVenta As String
    Public Property strFechaOrdenVenta As String
    Public Property strFechaVencimiento As String
    Public Property intTipoDespacho As Integer
    Public Property strTipoDespacho As String
    Public Property strMonedaPago As String
    Public Property strTipoVenta As String
    Public Property intOperador As Integer
    Public Property strComentario1 As String
    Public Property strComentario2 As String
    Public Property strComentario3 As String
    Public Property strMotivo As String
    Public Property strEmision As String

    Public Property blnFacturado As Boolean = False
    Public Property intFolio As Integer
    Public Property intTotalDocumento As Integer
    Public Property strCorreoVendedor As String
    Public Property strOrdenCompra As String

    Public Property intCodigoPlazoCompra As Integer
    Public Property strNumeroPedido As String
    Public Property intDiasExtras As Integer
    Public Property intEmpleado As Integer
    Public Property strGlosaEspecial As String
    Public Property strEstadoDocumento As String

    Public Property blnExiste As Boolean = False
    Sub New()
    End Sub
End Class