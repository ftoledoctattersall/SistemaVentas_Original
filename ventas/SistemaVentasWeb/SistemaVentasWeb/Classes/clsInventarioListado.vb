Imports System.Data.SqlClient
<Serializable()>
Public Class clsInventarioListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerInventario(ByVal strProducto As String)
        Dim InventarioLista As New List(Of clsInventario)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_inventario 10,'" + strProducto.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Inventario As New clsInventario()
                Inventario.BodCodigo = rdr.Item("BodCodigo")
                Inventario.BodNombre = rdr.Item("BodNombre")
                Inventario.ArtStockFisico = rdr.Item("ArtStockFisico")
                Inventario.ArtStockPedido = rdr.Item("ArtStockPedido")
                Inventario.ArtStockOrdenado = rdr.Item("ArtStockOrdenado")
                Inventario.ArtStockDisponible = rdr.Item("ArtStockDisponible")
                InventarioLista.Add(Inventario)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return InventarioLista
    End Function
    Public Function ListarInventario(ByVal strProducto As String, ByVal strOperador As String)
        Dim InventarioLista As New List(Of clsInventario)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_inventario 20,'" + strProducto.Trim + "','" + strOperador.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Inventario As New clsInventario()
                Inventario.BodAutoriza = rdr.Item("BodAutoriza")
                Inventario.BodCodigo = rdr.Item("BodCodigo")
                Inventario.BodNombre = rdr.Item("BodNombre")
                Inventario.ArtStockFisico = rdr.Item("ArtStockFisico")
                Inventario.ArtStockPedido = rdr.Item("ArtStockPedido")
                Inventario.ArtStockOrdenado = rdr.Item("ArtStockOrdenado")
                Inventario.ArtStockDisponible = rdr.Item("ArtStockDisponible")
                Inventario.ArtMoneda = rdr.Item("ArtMoneda")
                Inventario.ArtCosto = rdr.Item("ArtCosto")
                InventarioLista.Add(Inventario)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return InventarioLista
    End Function
End Class