Imports System.Data.SqlClient
<Serializable()>
Public Class clsRegionListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerRegion()
        Dim RegionLista As New List(Of clsRegion)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_region 10"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Region As New clsRegion()
                Region.RegCodigo = rdr.Item("RegCodigo")
                Region.RegNombre = rdr.Item("RegNombre")
                RegionLista.Add(Region)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return RegionLista
    End Function
End Class