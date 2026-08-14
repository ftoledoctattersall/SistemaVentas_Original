Imports System.Data.SqlClient
<Serializable()>
Public Class clsOperadorListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerOperador(ByVal intOperador As Integer, ByVal intOficina As Integer)
        Dim OperadorLista As New List(Of clsOperador)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_operador 10," + intOperador.ToString.Trim + "," + intOficina.ToString.Trim
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Operador As New clsOperador()
                Operador.OpeCodigo = rdr.Item("OpeCodigo")
                Operador.OpeNombre = rdr.Item("OpeNombre")
                OperadorLista.Add(Operador)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return OperadorLista
    End Function
    Public Function ObtenerOperadorOficina(ByVal intOperador As Integer, ByVal intOficina As Integer)
        Dim OperadorLista As New List(Of clsOperador)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_operador 20," + intOperador.ToString.Trim + "," + intOficina.ToString.Trim
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Operador As New clsOperador()
                Operador.OpeCodigo = rdr.Item("OpeCodigo")
                Operador.OpeNombre = rdr.Item("OpeNombre")
                OperadorLista.Add(Operador)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return OperadorLista
    End Function
End Class