Imports System.Data.SqlClient
<Serializable()>
Public Class clsDescuentoProductoListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerDescuentoProducto(ByVal intOpcion As Integer, ByVal strProducto As String, ByVal intPlazoVenta As Integer, ByVal intTotalUnitarioProducto As Double, ByVal strFechaVencimiento As String, ByVal strFechaCompra As String)
        Dim DescuentoProductoLista As New List(Of clsDescuentoProducto)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_descuento_producto " + intOpcion.ToString + ",'" + strProducto.Trim + "'," + intPlazoVenta.ToString + "," + intTotalUnitarioProducto.ToString + ",'" + strFechaVencimiento.Trim + "','" + strFechaCompra.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim DescuentoProducto As New clsDescuentoProducto()
                DescuentoProducto.ArtDescuento = rdr.Item("ArtDescuento")
                DescuentoProductoLista.Add(DescuentoProducto)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return DescuentoProductoLista
    End Function
End Class
