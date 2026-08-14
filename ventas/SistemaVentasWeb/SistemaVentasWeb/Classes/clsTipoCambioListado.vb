Imports System.Data.SqlClient
<Serializable()>
Public Class clsTipoCambioListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerTipoCambio(ByVal intOpcion As Integer, ByVal strMoneda As String, ByVal strFecha As String)
        Dim TipoCambioLista As New List(Of clsTipoCambio)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_tipo_cambio " + intOpcion.ToString + ",'" + strMoneda.ToString.Trim + "','" + strFecha.ToString.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim TipoCambio As New clsTipoCambio()
                TipoCambio.CamFecha = rdr.Item("CamFecha")
                TipoCambio.CamMoneda = rdr.Item("CamMoneda")
                TipoCambio.CamValor = rdr.Item("CamValor")
                TipoCambioLista.Add(TipoCambio)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return TipoCambioLista
    End Function
End Class
