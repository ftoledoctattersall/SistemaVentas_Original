Imports System.Data.SqlClient
<Serializable()>
Public Class clsPlazoCompraListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerPlazoCompra(ByVal intOpcion As Integer)
        Dim PlazoCompraLista As New List(Of clsPlazoCompra)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_plazo_compra " + intOpcion.ToString
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            Dim PlazoCompraDefault As New clsPlazoCompra()
            PlazoCompraDefault.PlaCodigo = "0"
            PlazoCompraDefault.PlaDias = "0"
            PlazoCompraDefault.PlaItem = "0_0"
            PlazoCompraDefault.PlaNombre = "SELECCIONE"
            PlazoCompraLista.Add(PlazoCompraDefault)
            While rdr.Read
                Dim PlazoCompra As New clsPlazoCompra()
                PlazoCompra.PlaCodigo = rdr.Item("PlaCodigo")
                PlazoCompra.PlaDias = rdr.Item("PlaDias")
                PlazoCompra.PlaItem = rdr.Item("PlaCodigo").ToString.Trim + "_" + rdr.Item("PlaDias").ToString.Trim
                PlazoCompra.PlaNombre = rdr.Item("PlaNombre")
                PlazoCompraLista.Add(PlazoCompra)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return PlazoCompraLista
    End Function
End Class