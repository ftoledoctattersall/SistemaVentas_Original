Imports System.Data.SqlClient
<Serializable()>
Public Class clsPlazoVentaListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerPlazoVenta(ByVal intOpcion As Integer)
        Dim PlazoVentaLista As New List(Of clsPlazoVenta)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_plazo_venta " + intOpcion.ToString
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim PlazoVenta As New clsPlazoVenta()
                PlazoVenta.PlaCodigo = rdr.Item("PlaCodigo")
                PlazoVenta.PlaDias = rdr.Item("PlaDias")
                PlazoVenta.PlaItem = rdr.Item("PlaCodigo").ToString.Trim + "_" + rdr.Item("PlaDias").ToString.Trim
                PlazoVenta.PlaNombre = rdr.Item("PlaNombre")
                PlazoVentaLista.Add(PlazoVenta)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return PlazoVentaLista
    End Function
End Class