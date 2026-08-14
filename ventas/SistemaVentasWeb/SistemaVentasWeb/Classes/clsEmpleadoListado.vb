Imports System.Data.SqlClient
<Serializable()>
Public Class clsEmpleadoListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerEmpleado(ByVal intEmpleado As Integer, ByVal intOficina As Integer)
        Dim EmpleadoLista As New List(Of clsEmpleado)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_Empleado 10," + intEmpleado.ToString.Trim + "," + intOficina.ToString.Trim
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Empleado As New clsEmpleado()
                Empleado.EmpCodigo = rdr.Item("EmpCodigo")
                Empleado.EmpNombre = rdr.Item("EmpNombre")
                EmpleadoLista.Add(Empleado)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return EmpleadoLista
    End Function
    Public Function ObtenerEmpleadoOficina(ByVal intEmpleado As Integer, ByVal intOficina As Integer)
        Dim EmpleadoLista As New List(Of clsEmpleado)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_Empleado 20," + intEmpleado.ToString.Trim + "," + intOficina.ToString.Trim
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Empleado As New clsEmpleado()
                Empleado.EmpCodigo = rdr.Item("EmpCodigo")
                Empleado.EmpNombre = rdr.Item("EmpNombre")
                EmpleadoLista.Add(Empleado)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return EmpleadoLista
    End Function
End Class