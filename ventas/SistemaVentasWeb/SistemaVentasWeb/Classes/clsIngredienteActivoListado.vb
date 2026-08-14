Imports System.Data.SqlClient
<Serializable()>
Public Class clsIngredienteActivoListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerIngredienteActivo(ByVal intOpcion As Integer)
        Dim IngredienteActivoLista As New List(Of clsIngredienteActivo)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_ingrediente_activo " + intOpcion.ToString
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim IngredienteActivo As New clsIngredienteActivo()
                IngredienteActivo.IngCodigo = rdr.Item("IngCodigo")
                IngredienteActivo.IngNombre = rdr.Item("IngNombre")
                IngredienteActivoLista.Add(IngredienteActivo)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return IngredienteActivoLista
    End Function
End Class