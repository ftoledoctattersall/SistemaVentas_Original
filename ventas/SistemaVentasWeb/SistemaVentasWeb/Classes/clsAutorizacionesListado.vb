Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
<Serializable()>
Public Class clsAutorizacionesListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader

    Public Property DocEntry As Integer
    Public Property OpCom As String

    Public Property AutorizacionCredito As Integer
    Public Property AutorizacionDocProtestado As Integer
    Public Property AutorizacionMargen As Integer
    Public Property AutorizacionCosto As Integer
    Public Property AutorizacionFactura As Integer
    Public Property AutorizacionBM1 As Integer
    Public Property AutorizacionBM2 As Integer
    Public Property AutorizacionBM3 As Integer
    Public Property AutorizacionBC1 As Integer
    Public Property AutorizacionBC2 As Integer
    Public Property AutorizacionBC3 As Integer
    Public Property AutorizacionTasa

    Public Property ContadorCredito As Integer
    Public Property ContadorDocProtestado As Integer
    Public Property ContadorMargen As Integer
    Public Property ContadorCosto As Integer
    Public Property ContadorFactura As Integer
    Public Property ContadorBM1 As Integer
    Public Property ContadorBM2 As Integer
    Public Property ContadorBM3 As Integer
    Public Property ContadorBC1 As Integer
    Public Property ContadorBC2 As Integer
    Public Property ContadorBC3 As Integer
    Public Property ContadorTasa As Integer

    Public Property Mje_Op As String
    Public Property Mje_Aut As String
    Public Property Rechaza As String
    Public Function RegistrarAutorizaciones(ByVal intDocEntry As Integer, ByVal strCodigoOperador As String, ByVal intCredito As Integer, ByVal intProtesto As Integer, ByVal intMargen As Integer, ByVal intCosto As Integer, ByVal intFactura As Integer, ByVal intBM1 As Integer, ByVal intBM2 As Integer, ByVal intBM3 As Integer, ByVal intBC1 As Integer, ByVal intBC2 As Integer, ByVal intBC3 As Integer, ByVal strComentario2 As String, ByVal intTasa As Integer) As Integer
        Dim intRespuesta As Integer = 0
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            cmd = New SqlCommand("dbo.tai_vw_sp2_insert_autorizaciones", cnx)
            cmd.CommandTimeout = 600
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocEntry", SqlDbType.Int).Value = intDocEntry
            cmd.Parameters.Add("@OpCom", SqlDbType.VarChar).Value = strCodigoOperador
            cmd.Parameters.Add("@AutorizacionCredito", SqlDbType.Int).Value = intCredito
            cmd.Parameters.Add("@AutorizacionDocProtestado", SqlDbType.Int).Value = intProtesto
            cmd.Parameters.Add("@AutorizacionMargen", SqlDbType.Int).Value = intMargen
            cmd.Parameters.Add("@AutorizacionCosto", SqlDbType.Int).Value = intCosto
            cmd.Parameters.Add("@ContadorCredito", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@ContadorDocProtestado", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@ContadorMargen", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@ContadorCosto", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@AutorizacionFactura", SqlDbType.Int).Value = intFactura
            cmd.Parameters.Add("@ContadorFactura", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@AutorizacionBM1", SqlDbType.Int).Value = intBM1
            cmd.Parameters.Add("@ContadorBM1", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@AutorizacionBM2", SqlDbType.Int).Value = intBM2
            cmd.Parameters.Add("@ContadorBM2", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@AutorizacionBM3", SqlDbType.Int).Value = intBM3
            cmd.Parameters.Add("@ContadorBM3", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@AutorizacionBC1", SqlDbType.Int).Value = intBC1
            cmd.Parameters.Add("@ContadorBC1", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@AutorizacionBC2", SqlDbType.Int).Value = intBC2
            cmd.Parameters.Add("@ContadorBC2", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@AutorizacionBC3", SqlDbType.Int).Value = intBC3
            cmd.Parameters.Add("@ContadorBC3", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@Mje_Op", SqlDbType.VarChar).Value = strComentario2.Trim
            cmd.Parameters.Add("@Mje_Aut", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@Rechaza", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@AutorizacionTasa", SqlDbType.Int).Value = intTasa
            cmd.Parameters.Add("@ContadorTasa", SqlDbType.Int).Value = 0
            intRespuesta = cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return intRespuesta
    End Function
    Public Function ActualizarAutorizaciones(ByVal intDocEntry As Integer, ByVal strConcepto As String, ByVal intAccion As Integer, ByVal strComentario As String, ByVal strAutorizador As String) As Boolean
        Dim blnRespuesta As Boolean = False
        Dim intRespuesta As Integer = 0
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            cmd = New SqlCommand("dbo.tai_vw_sp2_update_autorizaciones", cnx)
            cmd.CommandTimeout = 600
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocEntry", SqlDbType.Int).Value = intDocEntry
            cmd.Parameters.Add("@Concepto", SqlDbType.VarChar).Value = strConcepto.Trim
            cmd.Parameters.Add("@Estado_autorizacion", SqlDbType.Int).Value = intAccion
            cmd.Parameters.Add("@Mje_Aut", SqlDbType.VarChar).Value = strComentario.Trim
            cmd.Parameters.Add("@Rechaza", SqlDbType.VarChar).Value = strAutorizador.Trim
            intRespuesta = cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
            blnRespuesta = True
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return blnRespuesta
    End Function
    Public Function ObtenerAutorizaciones(ByVal intDocCodigo As Integer) As Boolean
        Dim blnExiste As Boolean = False
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_autorizaciones " + intDocCodigo.ToString
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            If rdr.Read Then

                Me.DocEntry = rdr.Item("DocEntry")
                Me.OpCom = rdr.Item("OpCom")

                Me.AutorizacionCredito = rdr.Item("AutorizacionCredito")
                Me.AutorizacionDocProtestado = rdr.Item("AutorizacionDocProtestado")
                Me.AutorizacionMargen = rdr.Item("AutorizacionMargen")
                Me.AutorizacionBM1 = rdr.Item("AutorizacionBM1")
                Me.AutorizacionBM2 = rdr.Item("AutorizacionBM2")
                Me.AutorizacionBM3 = rdr.Item("AutorizacionBM3")
                Me.AutorizacionCosto = rdr.Item("AutorizacionCosto")
                Me.AutorizacionBC1 = rdr.Item("AutorizacionBC1")
                Me.AutorizacionBC2 = rdr.Item("AutorizacionBC2")
                Me.AutorizacionBC3 = rdr.Item("AutorizacionBC3")
                Me.AutorizacionFactura = rdr.Item("AutorizacionFactura")
                Me.AutorizacionTasa = rdr.Item("AutorizacionTasa")

                Me.ContadorCredito = rdr.Item("ContadorCredito")
                Me.ContadorDocProtestado = rdr.Item("ContadorDocProtestado")
                Me.ContadorMargen = rdr.Item("ContadorMargen")
                Me.ContadorBM1 = rdr.Item("ContadorBM1")
                Me.ContadorBM2 = rdr.Item("ContadorBM2")
                Me.ContadorBM3 = rdr.Item("ContadorBM3")
                Me.ContadorCosto = rdr.Item("ContadorCosto")
                Me.ContadorBC1 = rdr.Item("ContadorBC1")
                Me.ContadorBC2 = rdr.Item("ContadorBC2")
                Me.ContadorBC3 = rdr.Item("ContadorBC3")
                Me.ContadorFactura = rdr.Item("ContadorFactura")
                Me.ContadorTasa = rdr.Item("ContadorTasa")

                Me.Mje_Op = rdr.Item("Mje_Op")
                Me.Mje_Aut = rdr.Item("Mje_Aut")
                Me.Rechaza = rdr.Item("Rechaza")

                blnExiste = True
            End If
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return blnExiste
    End Function
    Public Function ValidarAutorizaciones() As Boolean
        Dim blnValido As Boolean = False
        If (Me.AutorizacionCredito = Me.ContadorCredito) And
            (Me.AutorizacionDocProtestado = Me.ContadorDocProtestado) And
            (Me.AutorizacionMargen = Me.ContadorMargen) And
            (Me.AutorizacionCosto = Me.ContadorCosto) And
            (Me.AutorizacionFactura = Me.ContadorFactura) And
            (Me.AutorizacionBM1 = Me.ContadorBM1) And
            (Me.AutorizacionBM2 = Me.ContadorBM2) And
            (Me.AutorizacionBM3 = Me.ContadorBM3) And
            (Me.AutorizacionBC1 = Me.ContadorBC1) And
            (Me.AutorizacionBC2 = Me.ContadorBC2) And
            (Me.AutorizacionBC3 = Me.ContadorBC3) And
            (Me.AutorizacionTasa = Me.ContadorTasa) Then
            blnValido = True
        End If
        Return blnValido
    End Function
    Public Function ActualizarEstadoBorradorVenta(ByVal intDocEntry As Integer, ByVal intBloqueaGD As Integer) As Boolean
        Dim blnRespuesta As Boolean = False
        Dim intRespuesta As Integer = 0
        Dim strEstado = New clsDispatcherListado().ObtenerDispatcherEstadoFinal(intDocEntry)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            cmd = New SqlCommand("dbo.tai_vw_sp2_update_estado_borrador", cnx)
            cmd.CommandTimeout = 600
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocEntry", SqlDbType.Int).Value = intDocEntry
            cmd.Parameters.Add("@U_Estado_Aut", SqlDbType.VarChar).Value = strEstado
            cmd.Parameters.Add("@U_TAI_BloqueaGD", SqlDbType.Int).Value = intBloqueaGD
            intRespuesta = cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
            blnRespuesta = True
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return blnRespuesta
    End Function
    Public Function ActualizarEstadoOrdenVenta(ByVal intDraftKey As Integer, ByVal intDocNum As Integer) As Boolean
        Dim blnRespuesta As Boolean = False
        Dim intRespuesta As Integer = 0
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            cmd = New SqlCommand("dbo.tai_vw_sp2_update_estado_orden", cnx)
            cmd.CommandTimeout = 600
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DraftKey", SqlDbType.Int).Value = intDraftKey
            cmd.Parameters.Add("@DocNum", SqlDbType.Int).Value = intDocNum
            intRespuesta = cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
            blnRespuesta = True
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return blnRespuesta
    End Function
End Class