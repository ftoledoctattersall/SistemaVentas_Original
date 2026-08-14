Imports System.Data.SqlClient
<Serializable()>
Public Class clsUsuarioSistemaListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ValidarUsuarioSistema(ByVal intOpcion As Integer, ByVal strUser As String, ByVal strPassword As String) As String
        Dim strRespuesta As String = ""
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_usuario_sistema " + intOpcion.ToString + ",'" + strUser.ToString.Trim.ToLower + "','" + strPassword.ToString.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            If (rdr.HasRows) Then
                If (rdr.Read) Then
                    If (rdr.GetValue(0).ToString = "Y") Then
                        strRespuesta = "Valido."
                    Else
                        strRespuesta = "Usuario no se encuentra activo en el sistema."
                    End If
                End If
            Else
                strRespuesta = "Credenciales de acceso no validas."
            End If
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
            Return strRespuesta
        End Try
        Return strRespuesta
    End Function
    Public Function ObtenerIdesUsuarioSistema(ByVal intOpcion As Integer, ByVal strUser As String, ByVal strPassword As String)
        Dim UsuarioSistemaLista As New List(Of clsUsuarioSistema)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_usuario_sistema " + intOpcion.ToString + ",'" + strUser.ToString.Trim.ToLower + "','" + strPassword.ToString.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim UsuarioSistema As New clsUsuarioSistema()
                UsuarioSistema.EmpCodigo = rdr.Item("EmpCodigo")
                UsuarioSistema.UsuCodigo = rdr.Item("UsuCodigo")
                UsuarioSistema.OpeCodigo = rdr.Item("OpeCodigo")
                UsuarioSistema.OfiCodigo = rdr.Item("OfiCodigo")

                UsuarioSistema.RolCodigo = rdr.Item("RolCodigo")
                UsuarioSistema.NivCodigo = rdr.Item("NivCodigo")
                UsuarioSistema.UsuActivo = rdr.Item("UsuActivo")
                UsuarioSistemaLista.Add(UsuarioSistema)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return UsuarioSistemaLista
    End Function
    Public Function ObtenerPerfilUsuarioSistema(ByVal intOpcion As Integer, ByVal strUser As String, ByVal strPassword As String)
        Dim UsuarioSistemaLista As New List(Of clsUsuarioSistema)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_usuario_sistema " + intOpcion.ToString + ",'" + strUser.ToString.Trim.ToLower + "','" + strPassword.ToString.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim UsuarioSistema As New clsUsuarioSistema()
                UsuarioSistema.PerCodigo = rdr.Item("PerCodigo")
                UsuarioSistema.PerCotizador = rdr.Item("PerCotizador")
                UsuarioSistemaLista.Add(UsuarioSistema)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return UsuarioSistemaLista
    End Function
End Class