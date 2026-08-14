Imports System.Data.SqlClient
<Serializable()>
Public Class clsParametroListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerParametro(ByVal strParametro As String)
        Dim ParametroLista As New List(Of clsParametro)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_parametro 10,'" + strParametro.ToString.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Parametro As New clsParametro()
                Parametro.ParCodigo = rdr.Item("ParCodigo")
                Parametro.ParEnlace = rdr.Item("ParEnlace")
                Parametro.ParNombre = rdr.Item("ParNombre")
                ParametroLista.Add(Parametro)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return ParametroLista
    End Function
End Class
