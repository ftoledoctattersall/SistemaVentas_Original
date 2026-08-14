Imports System.Data.SqlClient
<Serializable()>
Public Class clsPeriodoContableListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerPeriodoContable() As String
        'Dim PeriodoContableLista As New List(Of clsPeriodoContable)
        Dim blnPrimeraVez As Boolean = True
        Dim strPeriodoContable As String = ""
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_periodo_contable 10"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                If (blnPrimeraVez) Then
                    blnPrimeraVez = False
                    strPeriodoContable = rdr.Item("PerNombre")
                Else
                    strPeriodoContable += "|" + rdr.Item("PerNombre")
                End If
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
            'strPeriodoContable = strPeriodoContable.Replace("-", "")
            'Dim PeriodoContable As New clsPeriodoContable()
            'PeriodoContable.PerNombre = strPeriodoContable
            'PeriodoContableLista.Add(PeriodoContable)
            strPeriodoContable = strPeriodoContable.Replace("-", "")
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        'Return PeriodoContableLista
        Return strPeriodoContable
    End Function
End Class