Imports System.Data.SqlClient
<Serializable()>
Public Class clsProveedorListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerProveedor(ByVal strProveedor As String)
        Dim ProveedorLista As New List(Of clsProveedor)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_proveedor 10,'" + strProveedor.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Proveedor As New clsProveedor()
                Proveedor.ProCodigo = rdr.Item("ProCodigo")
                Proveedor.ProNombre = rdr.Item("ProNombre")
                ProveedorLista.Add(Proveedor)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return ProveedorLista
    End Function
    Public Function ObtenerProveedorOrdenCompra(ByVal intDocEntry As Integer, ByVal intDocNum As Integer)
        Dim ProveedorLista As New List(Of clsProveedor)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_proveedor_orden_compra " + intDocEntry.ToString() + "," + intDocNum.ToString()
            ToLog.write("sql: " + sql)
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Proveedor As New clsProveedor()
                Proveedor.ProCodigo = rdr.Item("ProCodigo")
                Proveedor.ProNombre = rdr.Item("ProNombre")
                Proveedor.DiaCompra = rdr.Item("DiaCompra")
                ProveedorLista.Add(Proveedor)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return ProveedorLista
    End Function
End Class