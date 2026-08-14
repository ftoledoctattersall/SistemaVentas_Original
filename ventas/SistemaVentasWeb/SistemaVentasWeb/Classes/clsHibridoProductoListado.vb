Imports System.Data.SqlClient
<Serializable()>
Public Class clsHibridoProductoListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerHibridoProducto(ByVal strProducto As String)
        Dim HibridoProductoLista As New List(Of clsHibridoProducto)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_hibrido_producto 10,'" + strProducto.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim HibridoProducto As New clsHibridoProducto()
                HibridoProducto.HibCodigo = rdr.Item("HibCodigo")
                HibridoProducto.HibNombre = rdr.Item("HibNombre")
                HibridoProductoLista.Add(HibridoProducto)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return HibridoProductoLista
    End Function
End Class