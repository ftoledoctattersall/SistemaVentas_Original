Imports System.Data.SqlClient
<Serializable()>
Public Class clsFleteProductoListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerFleteProducto(ByVal strBodega As String, ByVal strProducto As String, ByVal intCantidadProducto As Integer, ByVal intTotalUnitarioProducto As Double)
        Dim FleteProductoLista As New List(Of clsFleteProducto)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_flete_producto 10,'" + strBodega.Trim + "','" + strProducto.Trim + "'," + intCantidadProducto.ToString + "," + intTotalUnitarioProducto.ToString

            ToLog.write(sql, "D")

            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim FleteProducto As New clsFleteProducto()
                FleteProducto.ArtFlete = rdr.Item("ArtFlete")
                FleteProductoLista.Add(FleteProducto)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return FleteProductoLista
    End Function
End Class
