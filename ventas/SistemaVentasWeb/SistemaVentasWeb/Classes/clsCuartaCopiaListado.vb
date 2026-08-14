Imports System.Data.SqlClient
<Serializable()>
Public Class clsCuartaCopiaListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerCuartaCopia(ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer)
        Dim CuartaCopiaLista As New List(Of clsCuartaCopia)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_cuarta_copia " + intOperador.ToString.Trim + "," + intAño.ToString.Trim + "," + intMes.ToString.Trim
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim CuartaCopia As New clsCuartaCopia()
                CuartaCopia.OpeCodigo = rdr.Item("OpeCodigo")
                CuartaCopia.OpeNombre = rdr.Item("OpeNombre")
                CuartaCopia.CliCodigo = rdr.Item("CliCodigo")
                CuartaCopia.CliNombre = rdr.Item("CliNombre")
                CuartaCopia.DocFecha = rdr.Item("DocFecha")
                CuartaCopia.DocTipo = rdr.Item("DocTipo")
                CuartaCopia.DocNumero = rdr.Item("DocNumero")
                CuartaCopia.DocFolio = rdr.Item("DocFolio")
                CuartaCopia.DocNeto = rdr.Item("DocNeto")
                CuartaCopia.Doc4taCopia = IIf(Year(rdr.Item("Doc4taCopia")) = 1900, "----------", rdr.Item("Doc4taCopia"))
                CuartaCopiaLista.Add(CuartaCopia)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return CuartaCopiaLista
    End Function
    Public Function ActualizarCuartaCopia(ByVal intOperador As Integer, ByVal strCliente As String, ByVal strTipo As String, ByVal intDocto As Integer, ByVal intFolio As Integer, ByVal intIngreso As Integer) As String
        Dim strRespuesta As String = "Ha habido un error al intentar actualizar la cuarta copia de uno o mas documentos."
        Dim intRespuesta As Integer = 0
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            cmd = New SqlCommand("dbo.tai_vw_sp2_update_cuarta_copia", cnx)
            cmd.CommandTimeout = 600
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@operador", SqlDbType.Int).Value = intOperador
            cmd.Parameters.Add("@cliente", SqlDbType.VarChar).Value = strCliente
            cmd.Parameters.Add("@tipo", SqlDbType.VarChar).Value = strTipo
            cmd.Parameters.Add("@documento", SqlDbType.Int).Value = intDocto
            cmd.Parameters.Add("@folio", SqlDbType.Int).Value = intFolio
            cmd.Parameters.Add("@ingreso", SqlDbType.Int).Value = intIngreso
            intRespuesta = cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
            strRespuesta = "Se ha actualizado la cuarta copia de los documentos visualizados."
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return strRespuesta
    End Function
End Class