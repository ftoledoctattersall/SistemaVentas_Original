Imports System.Data.SqlClient
<Serializable()>
Public Class clsTipoDespachoListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerTipoDespacho()
        Dim TipoDespachoLista As New List(Of clsTipoDespacho)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_tipo_despacho 10"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            Dim TipoDespachoDefault As New clsTipoDespacho()
            TipoDespachoDefault.DesCodigo = "0"
            TipoDespachoDefault.DesNombre = "SELECCIONE"
            TipoDespachoLista.Add(TipoDespachoDefault)
            While rdr.Read
                Dim TipoDespacho As New clsTipoDespacho()
                TipoDespacho.DesCodigo = rdr.Item("DesCodigo")
                TipoDespacho.DesNombre = rdr.Item("DesNombre")
                TipoDespachoLista.Add(TipoDespacho)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return TipoDespachoLista
    End Function
End Class