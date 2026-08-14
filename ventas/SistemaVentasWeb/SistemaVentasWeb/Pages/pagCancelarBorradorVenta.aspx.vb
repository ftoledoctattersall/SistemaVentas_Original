Imports System.Data.SqlClient
Public Class pagCancelarBorradorVenta
    Inherits System.Web.UI.Page
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (IsPostBack) Then
            LeerCancelacion()
        End If
    End Sub
    Public Sub LeerCancelacion()
        Dim intRespuesta As Integer = 0
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            cmd = New SqlCommand("dbo.tai_vw_sp2_select_cancela_borrador_venta", cnx)
            cmd.CommandTimeout = 600
            cmd.CommandType = CommandType.StoredProcedure
            intRespuesta = cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
            ToLog.write("intRespuesta: " + intRespuesta.ToString, "D")
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
    End Sub
End Class