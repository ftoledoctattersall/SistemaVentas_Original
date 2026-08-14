Imports System.Data.SqlClient
<Serializable()>
Public Class clsInformeListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerInformeCuentaCorriente(ByVal strCodigoCliente As String)
        Dim InformeLista As New List(Of clsInforme)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_cuenta_corriente 10,'" + strCodigoCliente.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Informe As New clsInforme()
                Informe.DocEntry = rdr.Item("DocEntry")
                Informe.DocNum = rdr.Item("DocNum")
                Informe.DocTipo = rdr.Item("DocTipo")
                Informe.AbrDoc = rdr.Item("AbrDoc")
                Informe.DocDate = rdr.Item("DocDate")
                Informe.DueDate = rdr.Item("DueDate")
                Informe.DocDebito = rdr.Item("DocDebito")
                Informe.DocCredito = rdr.Item("DocCredito")
                Informe.DocSaldo = rdr.Item("DocSaldo")
                Informe.DocEstado = rdr.Item("DocEstado")

                Informe.DocMonedaPago = rdr.Item("DocMonedaPago")
                Informe.DocTipoCambio = rdr.Item("DocTipoCambio")
                Informe.DocSaldoME = rdr.Item("DocSaldoME")

                InformeLista.Add(Informe)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return InformeLista
    End Function
    Public Function ObtenerInformeVentaMensual(ByVal intUsuario As String, ByVal strCodigoCliente As String, ByVal intAño As Integer, ByVal intMes As Integer)
        Dim InformeLista As New List(Of clsInforme)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_venta_mensual 10," + intUsuario.ToString + ",'" + strCodigoCliente.Trim + "'," + intAño.ToString + "," + intMes.ToString
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Informe As New clsInforme()
                Informe.DocEntry = rdr.Item("DocEntry")
                Informe.DocNum = rdr.Item("DocNum")
                Informe.FolioPref = rdr.Item("FolioPref")
                Informe.FolioNum = rdr.Item("FolioNum")
                Informe.DocDate = rdr.Item("DocDate")
                Informe.DocDueDate = rdr.Item("DocDueDate")
                Informe.Doc4taCopia = IIf(IsDBNull(rdr.Item("Doc4taCopia")), "----------", rdr.Item("Doc4taCopia"))
                Informe.CardCode = rdr.Item("CardCode")
                Informe.CardName = rdr.Item("CardName")
                Informe.DocNeto = rdr.Item("DocNeto")
                InformeLista.Add(Informe)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return InformeLista
    End Function
End Class