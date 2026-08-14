Imports System.Data.SqlClient
<Serializable()>
Public Class clsInteresProductoListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerInteresProducto(ByVal intOpcion As Integer, ByVal strCliente As String, ByVal strBodega As String, ByVal strProducto As String, ByVal intPlazoVenta As Integer, ByVal intTotalUnitarioProducto As Double, ByVal fltTasaInteres As Double, ByVal strFechaVencimiento As String, ByVal strFechaCompra As String)
        Dim InteresProductoLista As New List(Of clsInteresProducto)
        Dim strTipoCalculo As String = "Final"
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_interes_producto " + intOpcion.ToString + ",'" + strCliente.Trim + "','" + strBodega.Trim + "','" + strProducto.Trim + "'," + intPlazoVenta.ToString + "," + intTotalUnitarioProducto.ToString + ",'" + strTipoCalculo.Trim + "'," + Replace(fltTasaInteres.ToString, ",", ".") + ",'" + strFechaVencimiento.Trim + "','" + strFechaCompra.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim InteresProducto As New clsInteresProducto()
                InteresProducto.ArtInteres = rdr.Item("ArtInteres")
                InteresProductoLista.Add(InteresProducto)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return InteresProductoLista
    End Function
End Class