Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net.Mime
<Serializable()>
Public Class clsDispatcherListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function RegistrarDispatcher(ByVal strCodigo As String, ByVal intDocEntry As Integer, ByVal strAutorizadorOficial As String, ByVal strConcepto As String, ByVal strAutorizadorBackup As String) As Integer
        Dim intRespuesta As Integer = 0
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            cmd = New SqlCommand("dbo.tai_vw_sp2_insert_dispatcher", cnx)
            cmd.CommandTimeout = 600
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = strCodigo.Trim
            cmd.Parameters.Add("@codigo_borrador", SqlDbType.Int).Value = intDocEntry
            cmd.Parameters.Add("@codigo_autorizador", SqlDbType.VarChar).Value = strAutorizadorOficial.Trim
            cmd.Parameters.Add("@tipo_autorizacion", SqlDbType.VarChar).Value = strConcepto.Trim
            cmd.Parameters.Add("@aut_backup", SqlDbType.VarChar).Value = strAutorizadorBackup.Trim
            intRespuesta = cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return intRespuesta
    End Function
    Public Function ActualizarDispatcher(ByVal strCodigo As String, ByVal strAutorizador As String, ByVal intTipoAutorizador As Integer, ByVal strConcepto As String, ByVal intAccion As Integer, ByVal strComentario As String) As Boolean
        Dim blnRespuesta As Boolean = False
        Dim intRespuesta As Integer = 0
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            cmd = New SqlCommand("dbo.tai_vw_sp2_update_dispatcher", cnx)
            cmd.CommandTimeout = 600
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = strCodigo.Trim
            cmd.Parameters.Add("@Codigo_Autorizador", SqlDbType.VarChar).Value = strAutorizador.Trim
            cmd.Parameters.Add("@Tipo_Autorizador", SqlDbType.Int).Value = intTipoAutorizador
            cmd.Parameters.Add("@Tipo_Autorizacion", SqlDbType.VarChar).Value = strConcepto.Trim
            cmd.Parameters.Add("@Estado_Autorizacion", SqlDbType.Int).Value = intAccion
            cmd.Parameters.Add("@ComentarioAutoriza", SqlDbType.VarChar).Value = strComentario.Trim
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
    Public Function ObtenerDispatcher(ByVal intDocCodigo As Integer)
        Dim DispatcherLista As New List(Of clsDispatcher)
        Dim strBackup As String = ""
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_dispatcher " + intDocCodigo.ToString
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Dispatcher As New clsDispatcher()
                Dispatcher.DisCodigo = rdr.Item("DisCodigo")
                Dispatcher.DisOficial = fnc.ObtenerAutorizadorNombre(rdr.Item("DisOficial"))
                Select Case rdr.Item("DisTipo")
                    Case "BM1", "BM2", "BM3"
                        Dispatcher.DisTipo = "VENTAS"
                    Case "BC1", "BC2", "BC3"
                        Dispatcher.DisTipo = "VENTAS"
                    Case Else
                        Dispatcher.DisTipo = rdr.Item("DisTipo").ToString.ToUpper
                End Select

                strBackup = IIf(IsDBNull(rdr.Item("DisBackup")), "", rdr.Item("DisBackup"))
                If (strBackup <> "") Then
                    Dispatcher.DisBackup = fnc.ObtenerAutorizadorNombre(rdr.Item("DisBackup"))
                Else
                    Dispatcher.DisBackup = "NO APLICA"
                End If
                Dispatcher.DisCargo = fnc.ObtenerAutorizadorCargo(rdr.Item("DisOficial"))
                Select Case rdr.Item("DisEstado")
                    Case 0
                        Dispatcher.DisEstado = "PENDIENTE"
                    Case 1
                        Dispatcher.DisEstado = "APROBADO"
                    Case 2
                        Dispatcher.DisEstado = "RECHAZADO"
                    Case Else
                        Dispatcher.DisEstado = "NO APLICA"
                End Select
                Dispatcher.DisFechaCreacion = rdr.Item("DisFechaCreacion").ToString
                Dispatcher.DisFechaActualizacion = IIf(IsDBNull(rdr.Item("DisFechaActualizacion")), "", rdr.Item("DisFechaActualizacion").ToString)
                Dispatcher.DisComentario = rdr.Item("DisComentario")
                DispatcherLista.Add(Dispatcher)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return DispatcherLista
    End Function
    Public Function ObtenerDispatcherEstadoFinal(ByVal intDocCodigo As Integer) As String
        Dim strEstado As String = "P"
        Dim intTotal As Integer = 0
        Dim intAprobado As Integer = 0
        Dim intRechazado As Integer = 0
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_dispatcher " + intDocCodigo.ToString
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                intTotal += 1
                If (rdr.Item("DisEstado") = 1) Then
                    intAprobado += 1
                ElseIf (rdr.Item("DisEstado") = 2) Then
                    intRechazado += 1
                End If
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
            If (intTotal = intAprobado + intRechazado) Then
                If (intTotal = intAprobado) Then
                    strEstado = "A"
                ElseIf (intRechazado > 0) Then
                    strEstado = "R"
                End If
                Dim blnExito = GenerarHTMLCorreoDispatcher(intDocCodigo, strEstado)
                If (blnExito) Then
                    'ToLog.write("Se ha enviado correo exitoso al operador comercial.", "D")
                Else
                    'ToLog.write("Hubo problema para enviar correo al operador comercial.", "D")
                End If
            End If
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return strEstado
    End Function
    Public Function GenerarHTMLCorreoDispatcher(ByVal intDocEntry As Integer, ByVal strEstado As String) As Boolean
        Dim strHtml As String = ""
        Dim strStyle As String = ""
        Dim strTipoVenta As String = ""
        Dim strTituloPedido As String = ""
        Dim strTituloTipoVenta As String = ""
        'Dim strNombreCliente As String = ""
        'Dim strCodigoCliente As String = ""
        'Dim strCorreoVendedor As String = ""
        'Dim intTotalDocumento As Integer = 0
        Dim blnContinuar As Boolean = False
        'Try

        'Dim clsTransaccionListado As New clsTransaccionListado()

        'Dim clsTransaccionResumen As New clsTransaccionResumen
        'clsTransaccionResumen = clsTransaccionListado.ObtenerTransaccionResumen(1, intDocEntry)



        Dim clsTransaccionListado As New WebServices.clsTransaccionListado()
        For Each clsTransaccionResumen As WebServices.clsTransaccionResumen In clsTransaccionListado.ObtenerTransaccionResumen(1, intDocEntry)

            If (clsTransaccionResumen.blnExiste) Then
                blnContinuar = True
            End If

            If (blnContinuar) Then
                Select Case clsTransaccionResumen.strTipoVenta.Trim
                    Case "ventaBodega"
                        strTituloTipoVenta = "VENTA BODEGA PROPIA"
                    Case "ventaConsignada"
                        strTituloTipoVenta = "VENTA CONSIGNADA"
                    Case "ventaPuestoFundo"
                        strTituloTipoVenta = "VENTA PUESTO FUNDO"
                    Case "ventaCalzadaProveedor"
                        strTituloTipoVenta = "VENTA CALZADA PROVEEDOR"
                    Case "ventaCostoEspecial"
                        strTituloTipoVenta = "VENTA COSTO ESPECIAL"
                    Case "ventaLiquidacion"
                        strTituloTipoVenta = "VENTA LIQUIDACION"
                End Select
                strTituloPedido = IIf(strEstado = "A", "APROBACION DE PEDIDO N° " + clsTransaccionResumen.intDocEntry.ToString, "RECHAZO DE PEDIDO N° " + clsTransaccionResumen.intDocEntry.ToString)
                Try
                    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-CL")
                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ","
                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = "."
                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ","
                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = "."

                    strHtml += "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'>"
                    strHtml += "<html>"
                    strHtml += "<head>"
                    strHtml += "    <title>SISTEMA DE VENTAS</title>"

                    strHtml += "    <style type='text/css'>"
                    strHtml += "        .trTituloSeccion"
                    strHtml += "        {"
                    strHtml += "            height:10px;"
                    strHtml += "            color:white;"
                    strHtml += "            font-family:Arial, Lucida Sans Unicode, Lucida Grande, Sans-Serif;"
                    strHtml += "            font-size:small;"
                    strHtml += "            font-style:normal;"
                    strHtml += "            text-align:left;"
                    strHtml += "            background-color:#003300;"
                    strHtml += "        }"
                    strHtml += "        .trTituloCabecera"
                    strHtml += "        {"
                    strHtml += "            height:10px;"
                    strHtml += "            color:white;"
                    strHtml += "            font-family:Arial, Lucida Sans Unicode, Lucida Grande, Sans-Serif;"
                    strHtml += "            font-size:small;"
                    strHtml += "            font-style:normal;"
                    strHtml += "            background-color:green;"
                    strHtml += "        }"
                    strHtml += "        .trTituloItem"
                    strHtml += "        {"
                    strHtml += "            height:10px;"
                    strHtml += "            color:black;"
                    strHtml += "            font-family:Arial, Lucida Sans Unicode, Lucida Grande, Sans-Serif;"
                    strHtml += "            font-size:small;"
                    strHtml += "            font-style:normal;"
                    strHtml += "        }"
                    strHtml += "        .tdRight"
                    strHtml += "        {"
                    strHtml += "            text-align:right;"
                    strHtml += "            vertical-align:central;"
                    strHtml += "        }"
                    strHtml += "        .tdCenter"
                    strHtml += "        {"
                    strHtml += "            text-align:center;"
                    strHtml += "            vertical-align:central;"
                    strHtml += "        }"
                    strHtml += "        .tdLeft"
                    strHtml += "        {"
                    strHtml += "            text-align:left;"
                    strHtml += "            vertical-align:central;"
                    strHtml += "        }"
                    strHtml += "    </style>"
                    strHtml += "    <body>"
                    strHtml += "        <div>"
                    strHtml += "            <table width='100%'>"
                    strHtml += "                <tr class='trTituloSeccion'><td class='tdCenter' colspan='8'>" + strTituloPedido.Trim + "</td></tr>"
                    strHtml += "                <tr><td colspan='8'><br></td></tr>"
                    strHtml += "                <tr class='trTituloItem'>"
                    strHtml += "                    <td>TIPO DE VENTA</td><td>" + strTituloTipoVenta.Trim + "</td>"
                    strHtml += "                    <td>CLIENTE</td><td>" + clsTransaccionResumen.strNombreCliente.Trim + "</td>"
                    strHtml += "                    <td>CODIGO </td><td>" + clsTransaccionResumen.strCodigoCliente.Trim + "</td>"
                    strHtml += "                    <td>TOTAL DOCTO.</td><td>" + (FormatNumber(clsTransaccionResumen.intTotalDocumento, 0)).ToString + "</td>"
                    strHtml += "                </tr>"
                    strHtml += "                <tr><td colspan='8'><br></td></tr>"
                    strHtml += "            </table>"
                    strHtml += "        </div>"
                    strHtml += "        <div>"
                    strHtml += "            <table width='100%' border='1'>"
                    strHtml += "                <tr class='trTituloCabecera'>"
                    strHtml += "                    <td class='tdLeft'>Autorizador</td>"
                    strHtml += "                    <td class='tdLeft'>Cargo</td>"
                    strHtml += "                    <td class='tdCenter'>Tipo</td>"
                    strHtml += "                    <td class='tdCenter'>Estado</td>"
                    strHtml += "                    <td class='tdLeft'>Backup</td>"
                    strHtml += "                    <td class='tdCenter'>Fecha Creacion</td>"
                    strHtml += "                    <td class='tdCenter'>Fecha Respuesta</td>"
                    strHtml += "                    <td class='tdLeft'>Comentario</td>"
                    strHtml += "                </tr>"
                    For Each Dispatcher As clsDispatcher In ObtenerDispatcher(intDocEntry)
                        strStyle = ""
                        If (strEstado = "R" And Dispatcher.DisEstado = "RECHAZADO") Then
                            strStyle = "" 'style='font-weight:bold;'"
                        End If
                        strHtml += "            <tr class='trTituloItem' " + strStyle + ">"
                        strHtml += "                <td class='tdLeft'>" + Dispatcher.DisOficial + "</td>"
                        strHtml += "                <td class='tdLeft'>" + Dispatcher.DisCargo + "</td>"
                        strHtml += "                <td class='tdCenter'>" + Dispatcher.DisTipo + "</td>"
                        strHtml += "                <td class='tdCenter'>" + Dispatcher.DisEstado + "</td>"
                        strHtml += "                <td class='tdLeft'>" + Dispatcher.DisBackup + "</td>"
                        strHtml += "                <td class='tdCenter'>" + Dispatcher.DisFechaCreacion + "</td>"
                        strHtml += "                <td class='tdCenter'>" + Dispatcher.DisFechaActualizacion + "</td>"
                        strHtml += "                <td class='tdLeft'>" + Dispatcher.DisComentario + "</td>"
                        strHtml += "            </tr>"
                    Next
                    strHtml += "            </table>"
                    strHtml += "        </div>"
                    strHtml += "    </body>"
                    strHtml += "</head>"
                    strHtml += "</html>"

                Catch ex As Exception
                    blnContinuar = False
                    ToLog.write(ex.Message, "E")
                End Try
            End If
            If (blnContinuar) Then
                Try
                    Dim mail As New ToMail()
                    Dim mensaje As MailMessage
                    Dim av1 As AlternateView
                    If My.Resources.rscSistemaVentasWeb.zModalidad = "P" Then
                        mensaje = New MailMessage(My.Resources.rscSistemaVentasWeb.wCorreoEmisor, clsTransaccionResumen.strCorreoVendedor)
                    Else
                        mensaje = New MailMessage(My.Resources.rscSistemaVentasWeb.wCorreoEmisor, My.Resources.rscSistemaVentasWeb.CorreoReceptorTST)
                    End If
                    mensaje.Subject = strTituloPedido.Trim
                    av1 = AlternateView.CreateAlternateViewFromString(strHtml, Nothing, MediaTypeNames.Text.Html)
                    mensaje.AlternateViews.Add(av1)
                    mensaje.IsBodyHtml = True
                    If strHtml <> "" Then
                        mail.mensaje = mensaje
                        mail.EnviarCorreo()
                    End If
                Catch ex As Exception
                    blnContinuar = False
                    ToLog.write(ex.Message, "E")
                End Try
            End If
        Next
        Return blnContinuar
    End Function
End Class