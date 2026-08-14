Imports System.Data.SqlClient
<Serializable()>
Public Class clsSerieListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerSerie(ByVal strObjeto As String, ByVal strSubObjeto As String, ByVal intUsuario As Integer)
        Dim SerieLista As New List(Of clsSerie)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_serie 10,'" + strObjeto.Trim + "','" + strSubObjeto.Trim + "'," + intUsuario.ToString.Trim
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Serie As New clsSerie()
                Serie.SerCodigo = rdr.Item("SerCodigo")
                Serie.SerNombre = rdr.Item("SerNombre")
                SerieLista.Add(Serie)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return SerieLista
    End Function
End Class