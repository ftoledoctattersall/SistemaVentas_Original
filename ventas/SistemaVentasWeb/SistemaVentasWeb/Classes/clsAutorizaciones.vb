<Serializable()>
Public Class clsAutorizaciones
    Public Property DocEntry As Integer
    Public Property OpCom As String

    Public Property AutorizacionCredito As Integer
    Public Property AutorizacionDocProtestado As Integer
    Public Property AutorizacionMargen As Integer
    Public Property AutorizacionCosto As Integer
    Public Property AutorizacionFactura As Integer
    Public Property AutorizacionBM1 As Integer
    Public Property AutorizacionBM2 As Integer
    Public Property AutorizacionBM3 As Integer
    Public Property AutorizacionBC1 As Integer
    Public Property AutorizacionBC2 As Integer
    Public Property AutorizacionBC3 As Integer
    Public Property AutorizacionTasa

    Public Property ContadorCredito As Integer
    Public Property ContadorDocProtestado As Integer
    Public Property ContadorMargen As Integer
    Public Property ContadorCosto As Integer
    Public Property ContadorFactura As Integer
    Public Property ContadorBM1 As Integer
    Public Property ContadorBM2 As Integer
    Public Property ContadorBM3 As Integer
    Public Property ContadorBC1 As Integer
    Public Property ContadorBC2 As Integer
    Public Property ContadorBC3 As Integer
    Public Property ContadorTasa As Integer

    Public Property Mje_Op As String
    Public Property Mje_Aut As String
    Public Property Rechaza As String

    Public Property intExiste As Integer
    Sub New()
    End Sub
End Class