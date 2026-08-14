<Serializable()>
Public Class clsInforme
    Public Property DocEntry As Integer
    Public Property DocNum As Integer
    Public Property FolioPref As String
    Public Property FolioNum As Integer
    Public Property DocDate As String
    Public Property DocDueDate As String
    Public Property Doc4taCopia As String
    Public Property CardCode As String
    Public Property CardName As String
    Public Property DocNeto As Integer


    'Public Property DocFecha As String
    Public Property DocTipo As String
    Public Property AbrDoc As String
    Public Property DueDate As String
    Public Property DocDebito As Integer
    Public Property DocCredito As Integer
    Public Property DocSaldo As Integer
    Public Property DocEstado As String


    Public Property DocMonedaPago As String
    Public Property DocTipoCambio As Decimal
    Public Property DocSaldoME As Decimal



    Sub New()
    End Sub
End Class