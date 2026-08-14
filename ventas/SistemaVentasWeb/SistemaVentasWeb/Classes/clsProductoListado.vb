Imports System.Data.SqlClient
<Serializable()>
Public Class clsProductoListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerProducto(ByVal intOpcion As Integer, ByVal strProducto As String, ByVal strBodega As String)
        Dim ProductoLista As New List(Of clsProducto)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_producto " + intOpcion.ToString + ",'" + strProducto.Trim + "','" + strBodega.Trim + "'"
            ToLog.write(sql)
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Producto As New clsProducto()
                Producto.ArtCodigo = rdr.Item("ArtCodigo")
                Producto.ArtNombre = rdr.Item("ArtNombre")
                Producto.ArtEnvase = rdr.Item("ArtEnvase")
                Producto.GruCodigo = rdr.Item("GruCodigo")
                Producto.GruNombre = rdr.Item("GruNombre")
                Producto.ProCodigo = rdr.Item("ProCodigo")
                Producto.ProNombre = rdr.Item("ProNombre")
                Producto.ArtMoneda = rdr.Item("ArtMoneda")
                Producto.ArtPrecioUnitario = rdr.Item("ArtPrecioUnitario")
                Producto.ArtCostoComercial = rdr.Item("ArtCostoComercial")
                Producto.ArtStockFisico = rdr.Item("ArtStockFisico")
                Producto.ArtStockDisponible = rdr.Item("ArtStockDisponible")
                Producto.ArtInventariable = 0
                If (rdr.Item("ArtInventariable") = "Y") Then Producto.ArtInventariable = 1
                Producto.PorFlete = rdr.Item("PorFlete")
                Producto.ArtCostoReposicion = rdr.Item("ArtCostoReposicion")
                Producto.ArtDiasPago = rdr.Item("ArtDiasPago")
                Producto.ArtPrecioCompra = rdr.Item("ArtPrecioCompra")
                Producto.ArtMonedaCompra = rdr.Item("ArtMonedaCompra")
                ProductoLista.Add(Producto)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return ProductoLista
    End Function
    'Public Function ListarProducto(ByVal strProducto As String)
    '    Dim ProductoLista As New List(Of clsProducto)
    '    Try
    '        Dim strConeccion As String = fnc.ObtenerConeccion()
    '        cnx = New SqlConnection(strConeccion)
    '        cnx.Open()
    '        sql = "exec tai_vw_sp2_select_producto 30,'" + strProducto.Trim + "'"
    '        cmd = New SqlCommand(sql, cnx)
    '        cmd.CommandTimeout = 600
    '        rdr = cmd.ExecuteReader()
    '        While rdr.Read
    '            Dim Producto As New clsProducto()
    '            Producto.ArtCodigo = rdr.Item("ArtCodigo")
    '            Producto.ArtNombre = rdr.Item("ArtNombre")
    '            Producto.ArtEnvase = rdr.Item("ArtEnvase")
    '            Producto.GruNombre = rdr.Item("GruNombre")
    '            Producto.ProNombre = rdr.Item("ProNombre")
    '            ProductoLista.Add(Producto)
    '        End While
    '        rdr.Close()
    '        cmd.Dispose()
    '        cnx.Close()
    '        cnx.Dispose()
    '    Catch ex As Exception
    '        ToLog.write(ex.Message, "E")
    '    End Try
    '    Return ProductoLista
    'End Function
    Public Function ListarProducto(ByVal intOpcion As Integer, ByVal strProducto As String, ByVal strBodega As String, ByVal strIngrediente As String)
        Dim ProductoLista As New List(Of clsProducto)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_producto " + intOpcion.ToString + ",'" + strProducto.Trim + "','" + strBodega.Trim + "', '" + strIngrediente.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Producto As New clsProducto()
                Producto.ArtCodigo = rdr.Item("ArtCodigo")
                Producto.ArtNombre = rdr.Item("ArtNombre")
                ProductoLista.Add(Producto)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return ProductoLista
    End Function
End Class