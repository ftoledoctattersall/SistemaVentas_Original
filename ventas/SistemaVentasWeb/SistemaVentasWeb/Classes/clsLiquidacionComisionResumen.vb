<Serializable()>
Public Class clsLiquidacionComisionResumen
    Public Property LiqIdentificador As Integer
    Public Property CodOperador As Integer
    Public Property NomOperador As String
    Public Property LiqAño As Integer
    Public Property LiqMes As Integer
    Public Property TotNeto As Integer
    Public Property TotCosto As Integer
    Public Property TotMargen As Integer
    Public Property PorMargen As Decimal
    Public Property IndComision As Integer
    Public Property GruComision As Integer
    Public Property FinComision As Integer
    Public Property ProVacaciones As Integer
    Public Property SemCorrida As Integer
    Public Property TotComision As Integer
    Public Property FecLiquidacion As String
    Public Property LiqOrigen As String
    Public Property LiqTipo As String
    Sub New()
    End Sub
End Class