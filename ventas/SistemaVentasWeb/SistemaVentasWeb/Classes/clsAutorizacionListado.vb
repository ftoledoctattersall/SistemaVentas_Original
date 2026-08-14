Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports System.Globalization
Imports System.Net.Mail
Imports System.Net.Mime
<Serializable()>
Public Class clsAutorizacionListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader

    Public Property dblValorDolar As Double
    Public Property dblValorEuro As Double
    Public Property intDocEntry As Integer
    Public Property strTipoVenta As String
    Public Property strCodigoCliente As String

    Public Property strNombreCliente As String
    Public Property strRutCliente As String
    Public Property strTelefonoCliente As String
    Public Property strGiroCliente As String
    Public Property strCorreoCliente As String
    Public Property strGrupoCliente As String

    Public Property strMonedaPago As String
    Public Property strOrdenCompra As String
    Public Property intVendedor As Integer
    Public Property intOperador As Integer
    Public Property strCodigoOperador As String
    Public Property strNombreOperador As String
    Public Property strFechaOrdenVenta As String
    Public Property strFechaEntrega As String
    Public Property intSerie As Integer
    Public Property strSerie As String
    Public Property intTipoDespacho As Integer
    Public Property strTipoDespacho As String
    Public Property strNotaPedido As String
    Public Property intCodigoPlazoVenta As Integer
    Public Property strNombrePlazoVenta As String
    Public Property strFechaVencimiento As String
    Public Property strDireccionFactura As String
    Public Property strDireccionDespacho As String
    Public Property strComentario1 As String
    Public Property strComentario2 As String
    Public Property strComentario3 As String
    Public Property strMotivo As String
    Public Property strEmision As String
    Public Property strGuiaDespacho As String
    Public Property strCategoria As String
    Public Property strVendedor As String

    Public Property intDiasExtras As Integer

    Public Function ObtenerAutorizacion(ByVal intOpcion As Integer, ByVal strOperador As String, ByVal strBodega As String, ByVal strProducto As String, ByVal strFechaOrdenVenta As String, ByVal dblPrecioUnitarioProducto As Double, ByVal strTipoVenta As String) As String
        Dim AutorizacionLista As New List(Of IList)
        Dim strCargo As String = ""
        Dim strBackup As String = ""
        Dim strRetorno As String = "0"
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_autorizacion " + intOpcion.ToString + ",'" + strOperador.Trim + "','" + strBodega.Trim + "','" + strProducto.Trim + "','" + strFechaOrdenVenta.Trim + "'," + Replace(dblPrecioUnitarioProducto.ToString, ",", ".").ToString + ",'" + strTipoVenta.Trim + "'"

            ToLog.write(sql, "D")

            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Autorizacion As New List(Of String)
                strCargo = fnc.ObtenerAutorizadorCargo(rdr.Item("DisOficial"))
                strBackup = IIf(IsDBNull(rdr.Item("DisBackup")), "", rdr.Item("DisBackup"))
                Autorizacion.Add(rdr.Item("DisOficial"))
                Autorizacion.Add(rdr.Item("DisTipo"))
                Autorizacion.Add(strBackup)
                Autorizacion.Add(strCargo)
                AutorizacionLista.Add(Autorizacion)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
            If AutorizacionLista.Count > 0 Then
                Dim serializer As New JavaScriptSerializer()
                strRetorno = serializer.Serialize(AutorizacionLista)
            End If
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return strRetorno
    End Function
    Public Function ObtenerAutorizacionModal(ByVal intOpcion As Integer, ByVal strOperador As String, ByVal strBodega As String, ByVal strProducto As String, ByVal strFechaOrdenVenta As String, ByVal dblPrecioUnitarioProducto As Double, ByVal strTipoVenta As String) As String
        Dim strNombre As String = ""
        Dim strCargo As String = ""
        Dim strTipo As String = ""
        Dim strBackup As String = ""
        Dim strRetorno As String = "0"
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_autorizacion " + intOpcion.ToString + ",'" + strOperador.Trim + "','" + strBodega.Trim + "','" + strProducto.Trim + "','" + strFechaOrdenVenta.Trim + "'," + Replace(dblPrecioUnitarioProducto.ToString, ",", ".").ToString + ",'" + strTipoVenta.Trim + "'"

            ToLog.write(sql, "D")

            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                strNombre = fnc.ObtenerAutorizadorNombre(rdr.Item("DisOficial"))
                strCargo = fnc.ObtenerAutorizadorCargo(rdr.Item("DisOficial"))
                Select Case rdr.Item("DisTipo")
                    Case "BM1"
                        strTipo = "Bajo Margen 1"
                    Case "BM2"
                        strTipo = "Bajo Margen 2"
                    Case "BM3"
                        strTipo = "Bajo Margen 3"
                    Case "BC1"
                        strTipo = "Bajo Costo 1"
                    Case "BC2"
                        strTipo = "Bajo Costo 2"
                    Case "BC3"
                        strTipo = "Bajo Costo 3"
                    Case Else
                        strTipo = rdr.Item("DisTipo")
                End Select
                strBackup = IIf(IsDBNull(rdr.Item("DisBackup")), "", rdr.Item("DisBackup"))
                If (strBackup <> "") Then
                    strBackup = fnc.ObtenerAutorizadorNombre(rdr.Item("DisBackup"))
                Else
                    strBackup = "SIN DATO"
                End If
                strRetorno += "|" + strNombre + "&" + strCargo + "&" + strTipo + "&" + strBackup
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return strRetorno
    End Function
    Public Function ObtenerAutorizacionEspecial(ByVal strOperador As String, ByVal strTipo As String) As String
        Dim strNombre As String = ""
        Dim strCargo As String = ""
        Dim strCodigoAutorizacion As String = ""
        Dim strTipoAutorizacion As String = ""
        Dim strBackup As String = ""
        Dim strRetorno As String = ""
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_autorizacion_especial 10,'" + strOperador.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                strCodigoAutorizacion = rdr.Item("AutCodigo")
                If strTipo = "credito" Then
                    If strCodigoAutorizacion.Contains(My.Resources.rscSistemaVentasWeb.wAutorizacionCredito) Then
                        strNombre = fnc.ObtenerAutorizadorNombre(rdr.Item("AutNombre"))
                        strCargo = fnc.ObtenerAutorizadorCargo(rdr.Item("AutNombre"))
                        strTipoAutorizacion = My.Resources.rscSistemaVentasWeb.wAutorizacionCredito
                        strBackup = "."
                        strRetorno += "<tr><td>" + strNombre + "</td><td>" + strCargo + "</td><td>" + strTipoAutorizacion.ToString.ToUpper + "</td><td>" + strBackup + "</td></tr>" + rdr.Item("AutNombre").ToString.Trim
                    End If
                End If
                If strTipo = "deuda" Then
                    If strCodigoAutorizacion.Contains(My.Resources.rscSistemaVentasWeb.wAutorizacionDeuda) Then
                        strNombre = fnc.ObtenerAutorizadorNombre(rdr.Item("AutNombre"))
                        strCargo = fnc.ObtenerAutorizadorCargo(rdr.Item("AutNombre"))
                        strTipoAutorizacion = My.Resources.rscSistemaVentasWeb.wAutorizacionDeuda
                        strBackup = "."
                        strRetorno += "<tr><td>" + strNombre + "</td><td>" + strCargo + "</td><td>" + strTipoAutorizacion.ToString.ToUpper + "</td><td>" + strBackup + "</td></tr>" + rdr.Item("AutNombre").ToString.Trim
                    End If
                End If
                If strTipo = "protesto" Then
                    If strCodigoAutorizacion.Contains(My.Resources.rscSistemaVentasWeb.wAutorizacionProtesto) Then
                        strNombre = fnc.ObtenerAutorizadorNombre(rdr.Item("AutNombre"))
                        strCargo = fnc.ObtenerAutorizadorCargo(rdr.Item("AutNombre"))
                        strTipoAutorizacion = My.Resources.rscSistemaVentasWeb.wAutorizacionProtesto
                        strBackup = "."
                        strRetorno += "<tr><td>" + strNombre + "</td><td>" + strCargo + "</td><td>" + strTipoAutorizacion.ToString.ToUpper + "</td><td>" + strBackup + "</td></tr>" + rdr.Item("AutNombre").ToString.Trim
                    End If
                End If
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return strRetorno
    End Function


    Public Function EnviarAutorizacionCorreo(ByVal dblValorDolar As Double, ByVal dblValorEuro As Double, ByVal intDocEntry As Integer, ByVal strTipoVenta As String, ByVal strCliente As String, ByVal strNombreCliente As String, ByVal strRutCliente As String, ByVal strTelefonoCliente As String, ByVal strGiroCliente As String, ByVal strCorreoCliente As String, ByVal strGrupoCliente As String, ByVal strMonedaPago As String, ByVal strOrdenCompra As String, ByVal intVendedor As Integer, ByVal intOperador As Integer, ByVal strCodigoOperador As String, ByVal strNombreOperador As String, ByVal strFechaOrdenVenta As String, ByVal strFechaEntrega As String, ByVal intSerie As Integer, ByVal strSerie As String, ByVal intTipoDespacho As Integer, ByVal strTipoDespacho As String, ByVal strNotaPedido As String, ByVal intCodigoPlazoVenta As Integer, ByVal strNombrePlazoVenta As String, ByVal strFechaVencimiento As String, ByVal strDireccionFactura As String, ByVal strDireccionDespacho As String, ByVal strComentario1 As String, ByVal strComentario2 As String, ByVal strComentario3 As String, ByVal strMotivo As String, ByVal strEmision As String, ByVal strGuiaDespacho As String, ByVal strCategoria As String, ByVal strVendedor As String, ByVal strPedido As String)
        Dim strRetorno As String = "0"
        Dim intCadena As Integer = 0
        Dim strCadena As String = ""
        Dim intSubCadena As Integer = 0
        Dim strSubCadena As String = ""
        Dim strAutorizadorOficial As String = ""
        Dim strCorreoOficial As String = ""
        Dim strConcepto As String = ""
        Dim strAutorizadorBackup As String = ""
        Dim strCorreoBackup As String = ""

        Dim strRandom As String = ""
        Dim strCodigo As String = ""

        Dim strHtmlOficial As String = ""
        Dim strHtmlBackup As String = ""

        Dim intRespuesta As Integer = 0

        Dim intCredito As Integer = 0
        Dim intFactura As Integer = 0
        Dim intProtesto As Integer = 0
        Dim intMargen As Integer = 0
        Dim intCosto As Integer = 0


        Dim intBM1 As Integer = 0
        Dim intBM2 As Integer = 0
        Dim intBM3 As Integer = 0
        Dim intBC1 As Integer = 0
        Dim intBC2 As Integer = 0
        Dim intBC3 As Integer = 0

        Dim intTasa As Integer = 0


        Try
            Me.dblValorDolar = dblValorDolar
            Me.dblValorEuro = dblValorEuro
            Me.intDocEntry = intDocEntry
            Me.strTipoVenta = strTipoVenta
            Me.strCodigoCliente = strCliente

            Me.strNombreCliente = strNombreCliente
            Me.strRutCliente = strRutCliente
            Me.strTelefonoCliente = strTelefonoCliente
            Me.strGiroCliente = strGiroCliente
            Me.strCorreoCliente = strCorreoCliente
            Me.strGrupoCliente = strGrupoCliente

            Me.strMonedaPago = strMonedaPago
            Me.strOrdenCompra = strOrdenCompra
            Me.intVendedor = intVendedor

            Me.intOperador = intOperador
            Me.strCodigoOperador = strCodigoOperador
            Me.strNombreOperador = strNombreOperador

            Me.strFechaOrdenVenta = strFechaOrdenVenta
            Me.strFechaEntrega = strFechaEntrega
            Me.intSerie = intSerie
            Me.strSerie = strSerie
            Me.intTipoDespacho = intTipoDespacho
            Me.strTipoDespacho = strTipoDespacho
            Me.strNotaPedido = strNotaPedido
            Me.intCodigoPlazoVenta = intCodigoPlazoVenta
            Me.strNombrePlazoVenta = strNombrePlazoVenta
            Me.strFechaVencimiento = strFechaVencimiento
            Me.strDireccionFactura = strDireccionFactura
            Me.strDireccionDespacho = strDireccionDespacho
            Me.strComentario1 = strComentario1
            Me.strComentario2 = strComentario2
            Me.strComentario3 = strComentario3
            Me.strMotivo = strMotivo
            Me.strEmision = strEmision
            Me.strGuiaDespacho = strGuiaDespacho
            Me.strCategoria = strCategoria
            Me.strVendedor = strVendedor

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-CL")
            Dim datFechaVencimiento As Date = DateTime.ParseExact(Me.strFechaVencimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture)
            Dim datFechaOrdenVenta As Date = DateTime.ParseExact(Me.strFechaOrdenVenta, "dd/MM/yyyy", CultureInfo.InvariantCulture)
            Me.intDiasExtras = (datFechaVencimiento - datFechaOrdenVenta).TotalDays

            Dim objPedido As JObject = JObject.Parse(strPedido)
            Dim arrFilaPedido As JArray = objPedido("arrFilas")
            Dim AutorizacionLista As New List(Of clsAutorizacion)

            If (objPedido("porCredito").ToString.Trim <> "") Then
                Dim Autorizacion As New clsAutorizacion()
                Autorizacion.AutOficialNombre = objPedido("porCredito")
                Autorizacion.AutOficialCorreo = fnc.ObtenerAutorizadorCorreo(objPedido("porCredito"))
                Autorizacion.AutConcepto = My.Resources.rscSistemaVentasWeb.wAutorizacionCredito.ToUpper
                Autorizacion.AutBackupNombre = ""
                Autorizacion.AutBackupCorreo = ""
                Autorizacion.AutOrden = 1
                AutorizacionLista.Add(Autorizacion)
                intCredito = 1
            End If
            If (objPedido("porDeuda").ToString.Trim <> "") Then
                Dim Autorizacion As New clsAutorizacion()
                Autorizacion.AutOficialNombre = objPedido("porDeuda")
                Autorizacion.AutOficialCorreo = fnc.ObtenerAutorizadorCorreo(objPedido("porDeuda"))
                Autorizacion.AutConcepto = My.Resources.rscSistemaVentasWeb.wAutorizacionDeuda.ToUpper
                Autorizacion.AutBackupNombre = ""
                Autorizacion.AutBackupCorreo = ""
                Autorizacion.AutOrden = 2
                AutorizacionLista.Add(Autorizacion)
                intFactura = 1
            End If

            If (objPedido("porProtesto").ToString.Trim <> "") Then
                Dim Autorizacion As New clsAutorizacion()
                Autorizacion.AutOficialNombre = objPedido("porProtesto")
                Autorizacion.AutOficialCorreo = fnc.ObtenerAutorizadorCorreo(objPedido("porProtesto")) 'fnc.ObtenerAutorizadorCorreo(strAutorizadorOficial.Trim)fnc.ObtenerAutorizadorCorreo(strAutorizadorOficial.Trim)
                Autorizacion.AutConcepto = My.Resources.rscSistemaVentasWeb.wAutorizacionProtesto.ToUpper
                Autorizacion.AutBackupNombre = ""
                Autorizacion.AutBackupCorreo = ""
                Autorizacion.AutOrden = 3
                AutorizacionLista.Add(Autorizacion)
                intProtesto = 1
            End If

            If (objPedido("porCosto").ToString.Trim = "True" Or objPedido("porMargen").ToString.Trim = "True" Or objPedido("porTasa").ToString.Trim = "True") Then
                For intCadena = 0 To arrFilaPedido.Count - 1 Step 1
                    Dim FilaPedido As JObject = arrFilaPedido(intCadena)
                    strCadena = Replace(Replace(FilaPedido("autoriza"), "[[", "["), "]]", "]")
                    Dim arrCadena = Split(strCadena, "[")
                    For intSubCadena = 1 To arrCadena.Count - 1 Step 1
                        strSubCadena = Replace(Replace(arrCadena(intSubCadena), "]", ""), Chr(34), "")
                        Dim arrSubCadena = Split(strSubCadena, ",")
                        Dim Autorizacion As New clsAutorizacion()
                        Autorizacion.AutOficialNombre = arrSubCadena(0)
                        Autorizacion.AutOficialCorreo = fnc.ObtenerAutorizadorCorreo(arrSubCadena(0).Trim)
                        Autorizacion.AutConcepto = arrSubCadena(1)
                        Autorizacion.AutBackupNombre = arrSubCadena(2)
                        Autorizacion.AutBackupCorreo = fnc.ObtenerAutorizadorCorreo(arrSubCadena(2).Trim)
                        Select Case strConcepto
                            Case "BM1"
                                Autorizacion.AutOrden = 4
                            Case "BM2"
                                Autorizacion.AutOrden = 5
                            Case "BM3"
                                Autorizacion.AutOrden = 6
                            Case "BC1"
                                Autorizacion.AutOrden = 7
                            Case "BC2"
                                Autorizacion.AutOrden = 8
                            Case "BC3"
                                Autorizacion.AutOrden = 9
                            Case "TASA"
                                Autorizacion.AutOrden = 10
                            Case Else
                                Autorizacion.AutOrden = 11
                        End Select
                        AutorizacionLista.Add(Autorizacion)
                    Next
                Next
            End If

            Dim ojNuevaLista = From objLista In AutorizacionLista Select New With {Key objLista.AutOrden, Key objLista.AutConcepto, Key objLista.AutOficialCorreo, objLista.AutOficialNombre, objLista.AutBackupNombre, objLista.AutBackupCorreo} Distinct.ToList
            Dim objOrdenaLista = ojNuevaLista.OrderBy(Function(k) k.AutOrden).ToList()
            AutorizacionLista.Clear()
            For x As Integer = 0 To objOrdenaLista.Count - 1 Step 1
                Dim autorizador_temp As New clsAutorizacion
                autorizador_temp.AutOficialCorreo = objOrdenaLista.Item(x).AutOficialCorreo
                autorizador_temp.AutOficialNombre = objOrdenaLista.Item(x).AutOficialNombre
                autorizador_temp.AutConcepto = objOrdenaLista.Item(x).AutConcepto
                autorizador_temp.AutBackupNombre = objOrdenaLista.Item(x).AutBackupNombre
                autorizador_temp.AutBackupCorreo = objOrdenaLista.Item(x).AutBackupCorreo
                'ToLog.write("-----------------1RA FASE------------------", "D")
                'ToLog.write("strAutorizadorOficial: " + autorizador_temp.AutOficialNombre, "D")
                'ToLog.write("strCorreoOficial: " + autorizador_temp.AutOficialCorreo, "D")
                'ToLog.write("strConcepto: " + autorizador_temp.AutConcepto, "D")
                'ToLog.write("strAutorizadorBackup: " + autorizador_temp.AutBackupNombre, "D")
                'ToLog.write("strCorreoBackup: " + autorizador_temp.AutBackupCorreo, "D")
                'ToLog.write("-------------------------------------------", "D")
                AutorizacionLista.Add(autorizador_temp)
            Next (x)

            For Each Autorizacion As clsAutorizacion In AutorizacionLista

                strAutorizadorOficial = Autorizacion.AutOficialNombre
                strCorreoOficial = Autorizacion.AutOficialCorreo
                strConcepto = Autorizacion.AutConcepto
                strAutorizadorBackup = Autorizacion.AutBackupNombre
                strCorreoBackup = Autorizacion.AutBackupCorreo

                strRandom = fnc.CifrarMD5(CStr(fnc.ObtenerRandom(0, 1000000000)))
                strCodigo = strRandom.Substring(0, 26) + Me.intDocEntry.ToString

                intRespuesta = New clsDispatcherListado().RegistrarDispatcher(strCodigo, intDocEntry, strAutorizadorOficial, strConcepto, strAutorizadorBackup)
                strHtmlOficial = GenerarHTMLCorreoAutorizacion(strAutorizadorOficial, 0, strConcepto, strPedido, strCodigo)
                If (strAutorizadorBackup.Trim <> "") Then
                    strHtmlBackup = GenerarHTMLCorreoAutorizacion(strAutorizadorBackup, 1, strConcepto, strPedido, strCodigo)
                End If

                Select Case strConcepto
                    Case "BM1"
                        intBM1 += 1
                    Case "BM2"
                        intBM2 += 1
                    Case "BM3"
                        intBM3 += 1
                    Case "BC1"
                        intBC1 += 1
                    Case "BC2"
                        intBC2 += 1
                    Case "BC3"
                        intBC3 += 1
                    Case "TASA"
                        intTasa += 1
                End Select

                Try
                    Dim mail As New ToMail()
                    Dim mail_backup As New ToMail()
                    Dim av1 As AlternateView
                    Dim av2 As AlternateView

                    Dim mensaje As MailMessage
                    Dim mensaje_backup As MailMessage
                    Dim contentIdButtonOk As String = "buttonOk"
                    Dim contentIdButtonCancel As String = "buttonCancel"
                    Dim botonOk As String = HttpContext.Current.Server.MapPath("~") & "Images\" & "button_ok_32x32.png"
                    Dim botonCancel As String = HttpContext.Current.Server.MapPath("~") & "Images\" & "button_cancel_32x32.png"
                    Dim resourceButtonOk As LinkedResource = New LinkedResource(botonOk)
                    Dim resourceButtonCancel As LinkedResource = New LinkedResource(botonCancel)
                    resourceButtonOk.ContentId = contentIdButtonOk
                    resourceButtonOk.ContentType.Name = botonOk
                    resourceButtonCancel.ContentId = contentIdButtonCancel
                    resourceButtonCancel.ContentType.Name = botonCancel

                    If My.Resources.rscSistemaVentasWeb.zModalidad = "P" Then
                        mensaje = New MailMessage(My.Resources.rscSistemaVentasWeb.wCorreoEmisor, strCorreoOficial)
                        mensaje.Subject = Me.strSerie + " - BORRADOR DE VENTA N° " + Me.intDocEntry.ToString + " - " + Me.strNombreCliente
                        If strCorreoBackup <> "" Then
                            mensaje_backup = New MailMessage(My.Resources.rscSistemaVentasWeb.wCorreoEmisor, strCorreoBackup)
                            mensaje_backup.Subject = "(Backup) " + Me.strSerie + " - BORRADOR DE VENTA N° " + Me.intDocEntry.ToString + " - " + Me.strNombreCliente
                        End If
                    Else
                        mensaje = New MailMessage(My.Resources.rscSistemaVentasWeb.wCorreoEmisor, My.Resources.rscSistemaVentasWeb.CorreoReceptorTST)
                        mensaje.Subject = "TEST (Oficial) " + Me.strSerie + " - BORRADOR DE VENTA N° " + Me.intDocEntry.ToString + " - " + Me.strNombreCliente
                        mensaje_backup = New MailMessage(My.Resources.rscSistemaVentasWeb.wCorreoEmisor, My.Resources.rscSistemaVentasWeb.CorreoReceptorTST)
                        mensaje_backup.Subject = "TEST (Backup) " + Me.strSerie + " - BORRADOR DE VENTA N° " + Me.intDocEntry.ToString + " - " + Me.strNombreCliente
                    End If

                    av1 = AlternateView.CreateAlternateViewFromString(strHtmlOficial, Nothing, MediaTypeNames.Text.Html)
                    av1.LinkedResources.Add(resourceButtonOk)
                    av1.LinkedResources.Add(resourceButtonCancel)
                    mensaje.AlternateViews.Add(av1)
                    mensaje.IsBodyHtml = True
                    If strCorreoBackup <> "" Then
                        av2 = AlternateView.CreateAlternateViewFromString(strHtmlBackup, Nothing, MediaTypeNames.Text.Html)
                        av2.LinkedResources.Add(resourceButtonOk)
                        av2.LinkedResources.Add(resourceButtonCancel)
                        mensaje_backup.AlternateViews.Add(av2)
                        mensaje_backup.IsBodyHtml = True
                    End If

                    If strHtmlOficial <> "" Then
                        mail.mensaje = mensaje
                        mail.EnviarCorreo()
                        If strHtmlBackup <> "" Then
                            mail_backup.mensaje = mensaje_backup
                            mail_backup.EnviarCorreo()
                        End If
                    End If
                Catch ex As Exception
                    ToLog.write(ex.Message, "E")
                End Try
            Next
            intRespuesta = New clsAutorizacionesListado().RegistrarAutorizaciones(intDocEntry, strCodigoOperador, intCredito, intProtesto, intMargen, intCosto, intFactura, intBM1, intBM2, intBM3, intBC1, intBC2, intBC3, strComentario2, intTasa)
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return strRetorno
    End Function
    Public Function GenerarHTMLCorreoAutorizacion(ByVal strAutorizador As String, ByVal intTipoAutorizador As Integer, ByVal strConcepto As String, ByVal strPedido As String, ByVal strCodigo As String) As String
        Dim strServerWeb As String = IIf(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.ServerWebPRD, My.Resources.rscSistemaVentasWeb.ServerWebTST)
        Dim strURLAprobar As String = strServerWeb.Trim + "/Pages/pagAprobarBorrador.aspx?i=" + strCodigo.Trim + "&p=1&a=" + strAutorizador + "&t=" + intTipoAutorizador.ToString + "&c=" + strConcepto.Trim
        Dim strURLRechazar As String = strServerWeb.Trim + "/Pages/pagRechazarBorrador.aspx?i=" + strCodigo.Trim + "&p=2&a=" + strAutorizador + "&t=" + intTipoAutorizador.ToString + "&c=" + strConcepto.Trim

        Dim strHtml As String = ""
        Dim intIndice As Integer = 0

        Dim dblTipoCambio As Double = 1.0

        Dim intSumaTotalUnitarioProducto As Integer = 0
        Dim intSumaTotalCostoComercial As Double = 0.0
        Dim dblTotalMargenComercial As Double = 0.0

        Dim intCantidadProductos As Integer = 0

        Dim intTotalNeto As Integer = 0
        Dim intTotalIva As Integer = 0
        Dim intTotalBruto As Integer = 0

        Dim strTituloTipoVenta = "SIN TIPO DE VENTA"
        Dim strTituloConcepto = "SIN CONCEPTO"

        Select Case strConcepto.Trim
            Case "BM1", "BM2", "BM3"
                strTituloConcepto = "VENTAS"
            Case "BC1", "BC2", "BC3"
                strTituloConcepto = "VENTAS"
            Case Else
                strTituloConcepto = strConcepto.Trim.ToUpper
        End Select

        Select Case Me.strTipoVenta.Trim
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

        '--------------------------------------------------------------------
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
            strHtml += "            white-space: nowrap;"
            strHtml += "        }"
            strHtml += "        .tdCenter"
            strHtml += "        {"
            strHtml += "            text-align:center;"
            strHtml += "            vertical-align:central;"
            strHtml += "            white-space: nowrap;"
            strHtml += "        }"
            strHtml += "        .tdLeft"
            strHtml += "        {"
            strHtml += "            text-align:left;"
            strHtml += "            vertical-align:central;"
            strHtml += "            white-space: nowrap;"
            strHtml += "        }"

            strHtml += "        .tdRightCortado"
            strHtml += "        {"
            strHtml += "            text-align:right;"
            strHtml += "            vertical-align:central;"
            strHtml += "        }"
            strHtml += "        .tdCenterCortado"
            strHtml += "        {"
            strHtml += "            text-align:center;"
            strHtml += "            vertical-align:central;"
            strHtml += "        }"
            strHtml += "        .tdLeftCortado"
            strHtml += "        {"
            strHtml += "            text-align:left;"
            strHtml += "            vertical-align:central;"
            strHtml += "        }"

            strHtml += "        .tdRightCortadoCompra"
            strHtml += "        {"
            strHtml += "            text-align:right;"
            strHtml += "            vertical-align:central;"
            strHtml += "            background-color:  #0000FF;"
            strHtml += "        }"
            strHtml += "        .tdCenterCortadoCompra"
            strHtml += "        {"
            strHtml += "            text-align:center;"
            strHtml += "            vertical-align:central;"
            strHtml += "            background-color:  #0000FF;"
            strHtml += "        }"
            strHtml += "        .tdLeftCortadoCompra"
            strHtml += "        {"
            strHtml += "            text-align:left;"
            strHtml += "            vertical-align:central;"
            strHtml += "            background-color:  #191970;"
            strHtml += "        }"
            strHtml += "    </style>"

            strHtml += "    <body>"

            'strHtml += "    <div>"
            strHtml += "        <table width='100%' border='1px'>"
            strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>BORRADOR DE VENTA N° " + Me.intDocEntry.ToString + " " + strTituloTipoVenta + " - AUTORIZACION POR " + strTituloConcepto + "   (" + Me.strEmision + ")</td></tr>"
            strHtml += "            <tr>"
            strHtml += "                <td colspan='6'>"
            strHtml += "                    <a href='" + strURLAprobar + "'   ><img src=""cid:buttonOk""     width='32' height='32' border='0' alt='Aprobar'   ></a>"
            strHtml += "                    <a href='" + strURLRechazar + "'  ><img src=""cid:buttonCancel"" width='32' height='32' border='0' alt='Rechazar'  ></a>"
            strHtml += "                </td>"
            strHtml += "            </tr>"
            strHtml += "            <tr><td colspan='6'><br></td></tr>"
            'strHtml += "        </table>"
            'strHtml += "    </div>"

            'strHtml += "    <div>"
            'strHtml += "        <table width='100%' border='1px'>"
            strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>DATOS DEL CLIENTE</td></tr>"
            strHtml += "            <tr class='trTituloItem'><td class='tdLeft'>NOMBRE</td>  <td class='tdLeftCortado'>" + Me.strNombreCliente + "  </td><td class='tdLeft'>RUT  </td><td class='tdLeftCortado'>" + Me.strRutCliente + " (" + Me.strCodigoCliente + ") </td><td class='tdLeft'>CATEGORIA</td><td class='tdLeftCortado'>" + Me.strCategoria + "</td></tr>"
            strHtml += "            <tr class='trTituloItem'><td class='tdLeft'>TELEFONO</td><td class='tdLeftCortado'>" + Me.strTelefonoCliente + "</td><td class='tdLeft'>EMAIL</td><td class='tdLeftCortado'>" + Me.strCorreoCliente + "</td><td class='tdLeft'>GIRO  </td><td class='tdLeftCortado'>" + Me.strGiroCliente + "  </td></tr>"
            'strHtml += "        </table>"
            'strHtml += "    </div>"

            'strHtml += "    <div>"
            'strHtml += "        <table width='100%' border='1px'>"
            strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>DATOS DEL NEGOCIO</td></tr>"
            strHtml += "            <tr class='trTituloItem'><td class='tdLeft'>OPE.COMERCIAL      </td><td class='tdLeftCortado'>" + Me.strVendedor + "     </td><td class='tdLeft'>OFICINA       </td><td class='tdLeftCortado'>" + Me.strSerie + "          </td><td class='tdLeft'>TIPO DESPACHO</td><td class='tdLeftCortado'> " + Me.strTipoDespacho + "</td></tr>"
            strHtml += "            <tr class='trTituloItem'><td class='tdLeft'>CONDICION PAGO     </td><td class='tdLeftCortado'>" + Me.strNombrePlazoVenta + "   </td><td class='tdLeft'>FEC.ORDEN VTA.</td><td class='tdLeftCortado'>" + Me.strFechaOrdenVenta + "</td><td class='tdLeft'>MONEDA PAGO  </td><td class='tdLeftCortado'> " + Me.strMonedaPago + "  </td></tr>"
            strHtml += "            <tr class='trTituloItem'><td class='tdLeft'>DIAS PLAZO VTA.REAL</td><td class='tdLeftCortado'>" + Me.intDiasExtras.ToString + "</td><td class='tdLeft'>FEC.VENCTO.   </td><td class='tdLeftCortado'>" + Me.strFechaVencimiento + "</td><td class='tdLeft'>.</td><td class='tdLeftCortado'>" + "." + "</td></tr>"
            'strHtml += "        </table>"
            'strHtml += "    </div>"





            If (strTituloConcepto = "CREDITOS") Then
                strHtml += "        <tr class='trTituloSeccion'><td colspan='6'>LINEA DE CREDITO</td></tr>"
                Dim clsClienteListado As New clsClienteListado()
                For Each clsCliente As clsCliente In clsClienteListado.ObtenerClienteLineaCredito(Me.strCodigoCliente)
                    strHtml += "    <tr class='trTituloItem'><td class='tdLeft'>AUTORIZADO</td><td class='tdLeftCortado'>" + (FormatNumber(clsCliente.CliAutorizado, 0)).ToString + "</td><td class='tdLeft'>UTILIZADO</td><td class='tdLeftCortado'>" + (FormatNumber(clsCliente.CliUtilizado, 0)).ToString + "</td><td class='tdLeft'>DISPONIBLE</td><td class='tdLeftCortado'>" + (FormatNumber(clsCliente.CliDisponible, 0)).ToString + "</td></tr>"
                Next
            End If

            If (strTituloConcepto = "DEUDA VENCIDA") Then
                strHtml += "        <tr class='trTituloSeccion'><td colspan='6'>DEUDAS VENCIDAS</td></tr>"
                strHtml += "        <tr class='trTituloItem'><td class='tdLeft' colspan='2'>N°DOCUMENTO</td><td class='tdCenter' colspan='2'>FECHA DOCUMENTO</td><td class='tdRight' colspan='2'>MONTO CLP</td></tr>"
                Dim clsClienteListado As New clsClienteListado()
                For Each clsCliente As clsCliente In clsClienteListado.ObtenerClienteFacturaImpaga(Me.strCodigoCliente)
                    strHtml += "    <tr class='trTituloItem'><td class='tdLeftCortado' colspan='2'>" + clsCliente.FacNumero.ToString + "</td><td class='tdCenterCortado' colspan='2'>" + clsCliente.FacFecha.ToString + "</td><td class='tdRightCortado' colspan='2'>" + (FormatNumber(clsCliente.FacValor, 0)).ToString + "</td></tr>"
                Next
            End If

            If (strTituloConcepto = "PROTESTOS") Then
                strHtml += "        <tr class='trTituloSeccion'><td colspan='6'>PROTESTOS</td></tr>"
                strHtml += "        <tr class='trTituloItem'><td class='tdLeft' colspan='2'>N°DOCUMENTO</td><td class='tdCenter' colspan='2'>FECHA DOCUMENTO</td><td class='tdRight' colspan='2'>MONTO CLP</td></tr>"
                Dim clsClienteListado As New clsClienteListado()
                For Each clsCliente As clsCliente In clsClienteListado.ObtenerClienteChequeProtestado(Me.strCodigoCliente)
                    strHtml += "    <tr class='trTituloItem'><td class='tdLeftCortado' colspan='2'>" + clsCliente.CheNumero.ToString + "</td><td class='tdCenterCortado' colspan='2'>" + clsCliente.CheFecha.ToString + "</td><td class='tdRightCortado' colspan='2'>" + (FormatNumber(clsCliente.CheValor, 0)).ToString + "</td></tr>"
                Next
            End If





            'strHtml += "    <div>"
            strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>COMENTARIO FACTURACION</td></tr>"
            strHtml += "            <tr><td colspan='6' class='trTituloItem tdLeftCortado'>" + IIf(Me.strComentario1.Trim <> "", Me.strComentario1.Trim.ToUpper, ".") + "</td></tr>"
            strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>COMENTARIO AUTORIZADOR</td></tr>"
            strHtml += "            <tr><td colspan='6' class='trTituloItem tdLeftCortado'>" + IIf(Me.strComentario2.Trim <> "", Me.strComentario2.Trim.ToUpper, ".") + "</td></tr>"


            If (Me.strTipoVenta.Trim = "ventaPuestoFundo" Or Me.strTipoVenta.Trim = "ventaCalzadaProveedor") Then
                strHtml += "        <tr class='trTituloSeccion'><td colspan='6'>COMENTARIO ABASTECIMIENTO</td></tr>"
                'strHtml += "       <div class='trTituloSeccion'>COMENTARIO ABASTECIMIENTO</div>"
                strHtml += "        <tr><td colspan='6' class='trTituloItem tdLeftCortado'>" + IIf(Me.strComentario3.Trim <> "", Me.strComentario3.Trim.ToUpper, ".") + "</td></tr>"
            End If

            'strHtml += "    </div>"




            'If (strTituloConcepto = "VENTAS" Or strTituloConcepto = "TASA") Then   ---I N I C I O-------------------------------------------------------------------------------------------------------
            'strHtml += "    <div>"
            'strHtml += "            <tr>"
            'strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>PRODUCTOS SOLICITADOS</td></tr>"

            'strHtml += "                    <table width='100%' border='1px'>"

            strHtml += "            <tr>"
            strHtml += "                <td colspan='6'>"
            strHtml += "                    <table id='tblPedido' width='100%'>"





            strHtml += "                        <tr class='trTituloSeccion'>"
            strHtml += "                            <td class='tdLeftCortado' colspan='12'>DATOS DE LA VENTA</td>"
            If (Me.strTipoVenta.Trim = "ventaPuestoFundo" Or Me.strTipoVenta.Trim = "ventaCalzadaProveedor" Or Me.strTipoVenta.Trim = "ventaCostoEspecial" Or Me.strTipoVenta.Trim = "ventaLiquidacion") Then
                strHtml += "                        <td class='tdLeftCortadoCompra' colspan='8'>DATOS DE LA COMPRA</td>"
            End If
            strHtml += "                            <td class='tdLeftCortado' colspan='1'></td>"
            strHtml += "                        </tr>"





            strHtml += "                        <tr class='trTituloCabecera'>"
            strHtml += "                            <td class='tdCenterCortado'>Codigo</td>"
            strHtml += "                            <td class='tdCenterCortado'>Descripcion</td>"
            strHtml += "                            <td class='tdCenterCortado'>Cantidad</td>"
            strHtml += "                            <td class='tdCenterCortado'>Moneda</td>"
            strHtml += "                            <td class='tdCenterCortado'>Precio Unitario</td>"
            strHtml += "                            <td class='tdCenterCortado'>Total Unitario CLP</td>"
            strHtml += "                            <td class='tdCenterCortado'>Descto. %</td>"
            strHtml += "                            <td class='tdCenterCortado'>Margen %</td>"
            strHtml += "                            <td class='tdCenterCortado'>Interes CLP</td>"
            strHtml += "                            <td class='tdCenterCortado'>Flete CLP</td>"
            strHtml += "                            <td class='tdCenterCortado'>Fecha Entrega</td>"
            strHtml += "                            <td class='tdCenterCortado'>Precio Final CLP</td>"


            If (Me.strTipoVenta.Trim = "ventaPuestoFundo" Or Me.strTipoVenta.Trim = "ventaCalzadaProveedor" Or Me.strTipoVenta.Trim = "ventaCostoEspecial" Or Me.strTipoVenta.Trim = "ventaLiquidacion") Then
                strHtml += "                        <td class='tdCenterCortadoCompra'>Dias Compra</td>"
                strHtml += "                        <td class='tdCenterCortadoCompra'>Moneda</td>"
                strHtml += "                        <td class='tdCenterCortadoCompra'>Precio Unitario</td>"
                strHtml += "                        <td class='tdCenterCortadoCompra'>Total Unitario CLP</td>"
                strHtml += "                        <td class='tdCenterCortadoCompra'>Proveedor</td>"
                strHtml += "                        <td class='tdCenterCortadoCompra'>Tasa Interes %</td>"

                strHtml += "                        <td class='tdCenterCortadoCompra'>Condicion</td>"
                strHtml += "                        <td class='tdCenterCortadoCompra'>Motivo</td>"
            End If


            strHtml += "                            <td class='tdCenterCortado'>Costo Reposicion</td>"

            strHtml += "                        </tr>"


            Dim objPedido As JObject = JObject.Parse(strPedido)
            Dim arrFilaPedido As JArray = objPedido("arrFilas")
            For intIndice = 0 To arrFilaPedido.Count - 1 Step 1
                Dim FilaPedido As JObject = arrFilaPedido(intIndice)

                intTotalNeto += CInt(FilaPedido("txtPrecioFinalProducto"))
                intTotalIva = CInt(Math.Round(intTotalNeto * 0.19))
                intTotalBruto = intTotalNeto + intTotalIva

                intCantidadProductos += 1

                dblTotalMargenComercial = 0.0

                dblTipoCambio = 1.0
                If (FilaPedido("txtMonedaProducto") = "USD") Then
                    dblTipoCambio = CDbl(dblValorDolar)
                ElseIf (FilaPedido("txtMonedaProducto") = "EUR") Then
                    dblTipoCambio = CDbl(dblValorEuro)
                End If


                If (Me.strTipoVenta.Trim = "ventaPuestoFundo" Or Me.strTipoVenta.Trim = "ventaCalzadaProveedor" Or Me.strTipoVenta.Trim = "ventaCostoEspecial" Or Me.strTipoVenta.Trim = "ventaLiquidacion") Then
                    intSumaTotalUnitarioProducto += (CDec(FilaPedido("txtPrecioUnitarioProducto")) * CInt(FilaPedido("txtCantidadProducto")) * dblTipoCambio)
                    intSumaTotalCostoComercial += (CDec(FilaPedido("txtPrecioUnitarioProductoCompra")) * CInt(FilaPedido("txtCantidadProducto")) * dblTipoCambio)
                    dblTotalMargenComercial = (((intSumaTotalUnitarioProducto - intSumaTotalCostoComercial) / intSumaTotalUnitarioProducto) * 100.0)
                Else
                    intSumaTotalUnitarioProducto += (CDec(FilaPedido("txtPrecioUnitarioProducto")) * CInt(FilaPedido("txtCantidadProducto")) * dblTipoCambio)
                    intSumaTotalCostoComercial += (CDec(FilaPedido("hdnCostoComercialProducto")) * CInt(FilaPedido("txtCantidadProducto")) * dblTipoCambio)
                    dblTotalMargenComercial = (((intSumaTotalUnitarioProducto - intSumaTotalCostoComercial) / intSumaTotalUnitarioProducto) * 100.0)
                End If


                strHtml += "                    <tr class='trTituloItem'>"
                strHtml += "                        <td class='tdLeft'>" + FilaPedido("txtCodigoProducto").ToString + "</td>"
                strHtml += "                        <td class='tdLeft'>" + FilaPedido("txtNombreProducto").ToString + "</td>"
                strHtml += "                        <td class='tdRight'>" + FilaPedido("txtCantidadProducto").ToString + "</td>"
                strHtml += "                        <td class='tdCenter'>" + FilaPedido("txtMonedaProducto").ToString + "</td>"
                strHtml += "                        <td class='tdRight'>" + (FormatNumber(CDec(FilaPedido("txtPrecioUnitarioProducto")), 2)).ToString + "</td>"
                strHtml += "                        <td class='tdRight'>" + (FormatNumber(CDec(FilaPedido("txtTotalUnitarioProducto")), 0)).ToString + "</td>"
                strHtml += "                        <td class='tdRight'>" + (FormatNumber(CDec(FilaPedido("txtDescuentoProducto")), 2)).ToString + "</td>"
                strHtml += "                        <td class='tdRight'>" + (FormatNumber(CDec(FilaPedido("txtMargenComercialProducto")), 2)).ToString + "</td>"
                strHtml += "                        <td class='tdRight'>" + (FormatNumber(CDec(FilaPedido("txtInteresProducto")), 0)).ToString + "</td>"
                strHtml += "                        <td class='tdRight'>" + (FormatNumber(CDec(FilaPedido("txtFleteProducto")), 0)).ToString + "</td>"
                strHtml += "                        <td class='tdCenter'>" + FilaPedido("txtFechaEntregaProducto").ToString + "</td>"
                strHtml += "                        <td class='tdRight'>" + (FormatNumber(CDec(FilaPedido("txtPrecioFinalProducto")), 0)).ToString + "</td>"


                If (Me.strTipoVenta.Trim = "ventaPuestoFundo" Or Me.strTipoVenta.Trim = "ventaCalzadaProveedor" Or Me.strTipoVenta.Trim = "ventaCostoEspecial" Or Me.strTipoVenta.Trim = "ventaLiquidacion") Then
                    strHtml += "                    <td class='tdRight'>" + FilaPedido("hdnDiasCompra").ToString + "</td>"
                    strHtml += "                    <td class='tdCenter'>" + FilaPedido("txtMonedaProductoCompra").ToString + "</td>"
                    strHtml += "                    <td class='tdRight'>" + (FormatNumber(CDec(FilaPedido("txtPrecioUnitarioProductoCompra")), 2)).ToString + "</td>"
                    strHtml += "                    <td class='tdRight'>" + (FormatNumber(CDec(FilaPedido("txtTotalUnitarioProductoCompra")), 0)).ToString + "</td>"
                    strHtml += "                    <td class='tdLeft'>" + FilaPedido("txtNombreProveedorCompra").ToString + "</td>"
                    strHtml += "                    <td class='tdRight'>" + (FormatNumber(CDec(FilaPedido("txtTasaInteresCompra")), 2)).ToString + "</td>"

                    strHtml += "                    <td class='tdLeft'>" + FilaPedido("txtCondicion").ToString + "</td>"
                    strHtml += "                    <td class='tdLeft'>" + FilaPedido("txtMotivo").ToString + "</td>"
                    'strHtml += "                   <td class='tdLeft'>" + FilaPedido("txtFechaCompra").ToString + "</td>"
                End If


                strHtml += "                        <td class='tdRight'>" + (FormatNumber(CDec(FilaPedido("hdnCostoReposicionProducto")), 2)).ToString + "</td>"


                strHtml += "                    </tr>"
            Next



            'Dim intColumnas As Integer = 0
            'If (Me.strTipoVenta.Trim = "ventaPuestoFundo" Or Me.strTipoVenta.Trim = "ventaCalzadaProveedor" Or Me.strTipoVenta.Trim = "ventaCostoEspecial" Or Me.strTipoVenta.Trim = "ventaLiquidacion") Then intColumnas = 6



            If (Me.strTipoVenta.Trim = "ventaPuestoFundo" Or Me.strTipoVenta.Trim = "ventaCalzadaProveedor" Or Me.strTipoVenta.Trim = "ventaCostoEspecial" Or Me.strTipoVenta.Trim = "ventaLiquidacion") Then
                strHtml += "                    <tr><td colspan='21'><br></td></tr>"
                strHtml += "                    <tr class='trTituloItem'><td class='tdLeft'>                 </td><td class='tdLeft' colspan='9'>                                                             </td><td class='tdLeft'>Total Neto</td><td class='tdRight'> " + (FormatNumber(intTotalNeto, 0)).ToString + " </td><td colspan='9'></td></tr>"
                strHtml += "                    <tr class='trTituloItem'><td class='tdLeft'>Cantidad Items:  </td><td class='tdLeft' colspan='9'>" + intCantidadProductos.ToString + "                        </td><td class='tdLeft'>Total IVA</td><td class='tdRight'>  " + (FormatNumber(intTotalIva, 0)).ToString + "  </td><td colspan='9'></td></tr>"
                strHtml += "                    <tr class='trTituloItem'><td class='tdLeft'>Margen Comercial:</td><td class='tdLeft' colspan='9'>" + (FormatNumber(dblTotalMargenComercial, 2)).ToString + " %</td><td class='tdLeft'>Total Bruto</td><td class='tdRight'>" + (FormatNumber(intTotalBruto, 0)).ToString + "</td><td colspan='9'></td></tr>"
            Else
                strHtml += "                    <tr><td colspan='13'><br></td></tr>"
                strHtml += "                    <tr class='trTituloItem'><td class='tdLeft'>                 </td><td class='tdLeft' colspan='9'>                                                             </td><td class='tdLeft'>Total Neto</td><td class='tdRight'> " + (FormatNumber(intTotalNeto, 0)).ToString + " </td><td colspan='1'></td></tr>"
                strHtml += "                    <tr class='trTituloItem'><td class='tdLeft'>Cantidad Items:  </td><td class='tdLeft' colspan='9'>" + intCantidadProductos.ToString + "                        </td><td class='tdLeft'>Total IVA</td><td class='tdRight'>  " + (FormatNumber(intTotalIva, 0)).ToString + "  </td><td colspan='1'></td></tr>"
                strHtml += "                    <tr class='trTituloItem'><td class='tdLeft'>Margen Comercial:</td><td class='tdLeft' colspan='9'>" + (FormatNumber(dblTotalMargenComercial, 2)).ToString + " %</td><td class='tdLeft'>Total Bruto</td><td class='tdRight'>" + (FormatNumber(intTotalBruto, 0)).ToString + "</td><td colspan='1'></td></tr>"
            End If



            strHtml += "                    </table>"
            strHtml += "                </td>"
            strHtml += "            </tr>"
            'strHtml += "        </table>"
            'strHtml += "    </td>"
            'strHtml += "</tr>"
            'strHtml += "    </div>"

            'End If    ---F I N-------------------------------------------------------------------------------------------------------





            strHtml += "        </table>"
            strHtml += "    </body>"
            strHtml += "</head>"
            strHtml += "</html>"

            '--------------------------------------------------------------------

        Catch ex As Exception
            strHtml = ""
            ToLog.write(ex.Message, "E")
        End Try

        Return strHtml
    End Function


    Public Function EnviarAutorizacionCorreoCredito(ByVal strOperador As String, ByVal intDocEntry As Integer)
        Dim strRetorno As String = "0"

        'Dim intCadena As Integer = 0
        'Dim strCadena As String = ""
        'Dim intSubCadena As Integer = 0
        'Dim strSubCadena As String = ""

        Dim strAutorizadorOficial As String = ""
        Dim strCorreoOficial As String = ""
        Dim strConcepto As String = ""
        Dim strAutorizadorBackup As String = ""
        Dim strCorreoBackup As String = ""

        Dim strRandom As String = ""
        Dim strCodigo As String = ""

        Dim strHtmlOficial As String = ""
        Dim strHtmlBackup As String = ""

        Dim intRespuesta As Integer = 0

        Dim intCredito As Integer = 0
        Dim intFactura As Integer = 0
        Dim intProtesto As Integer = 0
        Dim intMargen As Integer = 0
        Dim intCosto As Integer = 0

        Dim intBM1 As Integer = 0
        Dim intBM2 As Integer = 0
        Dim intBM3 As Integer = 0
        Dim intBC1 As Integer = 0
        Dim intBC2 As Integer = 0
        Dim intBC3 As Integer = 0

        Dim intTasa As Integer = 0


        Try
            '' Dim clsTransaccionListado As New clsTransaccionListado()
            'Dim a As clsTransaccionResumen = New clsTransaccionListado().ObtenerTransaccionResumen(1, intDocEntry)
            'strNombreCliente = a.strNombreCliente
            'strSerie = a.strSerie

            Dim clsTransaccionListado As New clsTransaccionListado()
            For Each clsTransaccionResumen As clsTransaccionResumen In clsTransaccionListado.ObtenerTransaccionResumen(1, intDocEntry)
                strNombreCliente = clsTransaccionResumen.strNombreCliente
                strSerie = clsTransaccionResumen.strSerie
                strComentario2 = clsTransaccionResumen.strComentario2
            Next


            'Me.dblValorDolar = dblValorDolar
            'Me.dblValorEuro = dblValorEuro
            'Me.intDocEntry = intDocEntry
            'Me.strTipoVenta = strTipoVenta
            'Me.strCodigoCliente = strCliente

            'Me.strNombreCliente = strNombreCliente
            'Me.strRutCliente = strRutCliente
            'Me.strTelefonoCliente = strTelefonoCliente
            'Me.strGiroCliente = strGiroCliente
            'Me.strCorreoCliente = strCorreoCliente
            'Me.strGrupoCliente = strGrupoCliente

            'Me.strMonedaPago = strMonedaPago
            'Me.strOrdenCompra = strOrdenCompra
            'Me.intVendedor = intVendedor

            'Me.intOperador = intOperador
            'Me.strCodigoOperador = strCodigoOperador
            'Me.strNombreOperador = strNombreOperador

            'Me.strFechaOrdenVenta = strFechaOrdenVenta
            'Me.strFechaEntrega = strFechaEntrega
            'Me.intSerie = intSerie
            'Me.strSerie = strSerie
            'Me.intTipoDespacho = intTipoDespacho
            'Me.strTipoDespacho = strTipoDespacho
            'Me.strNotaPedido = strNotaPedido
            'Me.intCodigoPlazoVenta = intCodigoPlazoVenta
            'Me.strNombrePlazoVenta = strNombrePlazoVenta
            'Me.strFechaVencimiento = strFechaVencimiento
            'Me.strDireccionFactura = strDireccionFactura
            'Me.strDireccionDespacho = strDireccionDespacho
            'Me.strComentario1 = strComentario1
            'Me.strComentario2 = strComentario2
            'Me.strComentario3 = strComentario3
            'Me.strMotivo = strMotivo
            'Me.strEmision = strEmision
            'Me.strGuiaDespacho = strGuiaDespacho
            'Me.strCategoria = strCategoria
            'Me.strVendedor = strVendedor

            'System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-CL")
            'Dim datFechaVencimiento As Date = DateTime.ParseExact(Me.strFechaVencimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture)
            'Dim datFechaOrdenVenta As Date = DateTime.ParseExact(Me.strFechaOrdenVenta, "dd/MM/yyyy", CultureInfo.InvariantCulture)
            'Me.intDiasExtras = (datFechaVencimiento - datFechaOrdenVenta).TotalDays




            Dim strAutorizador As String = ObtenerAutorizacionEspecial(strOperador, "credito")
            Dim arrAutorizador = Split(strAutorizador, "</tr>")
            Dim strUsuarioCredito = arrAutorizador(1)

            'Dim objPedido As JObject = JObject.Parse(strPedido)
            'Dim arrFilaPedido As JArray = objPedido("arrFilas")
            Dim AutorizacionLista As New List(Of clsAutorizacion)

            'If (objPedido("porCredito").ToString.Trim <> "") Then
            Dim Autorizacion As New clsAutorizacion()
            Autorizacion.AutOficialNombre = strUsuarioCredito
            Autorizacion.AutOficialCorreo = fnc.ObtenerAutorizadorCorreo(strUsuarioCredito)
            Autorizacion.AutConcepto = My.Resources.rscSistemaVentasWeb.wAutorizacionCredito.ToUpper
            Autorizacion.AutBackupNombre = ""
            Autorizacion.AutBackupCorreo = ""
            'Autorizacion.AutOrden = 1
            'AutorizacionLista.Add(Autorizacion)
            intCredito = 1
            'End If

            'If (objPedido("porDeuda").ToString.Trim <> "") Then
            '    Dim Autorizacion As New clsAutorizacion()
            '    Autorizacion.AutOficialNombre = objPedido("porDeuda")
            '    Autorizacion.AutOficialCorreo = fnc.ObtenerAutorizadorCorreo(objPedido("porDeuda"))
            '    Autorizacion.AutConcepto = My.Resources.rscSistemaVentasWeb.wAutorizacionDeuda.ToUpper
            '    Autorizacion.AutBackupNombre = ""
            '    Autorizacion.AutBackupCorreo = ""
            '    Autorizacion.AutOrden = 2
            '    AutorizacionLista.Add(Autorizacion)
            '    intFactura = 1
            'End If

            'If (objPedido("porProtesto").ToString.Trim <> "") Then
            '    Dim Autorizacion As New clsAutorizacion()
            '    Autorizacion.AutOficialNombre = objPedido("porProtesto")
            '    Autorizacion.AutOficialCorreo = fnc.ObtenerAutorizadorCorreo(objPedido("porProtesto")) 'fnc.ObtenerAutorizadorCorreo(strAutorizadorOficial.Trim)fnc.ObtenerAutorizadorCorreo(strAutorizadorOficial.Trim)
            '    Autorizacion.AutConcepto = My.Resources.rscSistemaVentasWeb.wAutorizacionProtesto.ToUpper
            '    Autorizacion.AutBackupNombre = ""
            '    Autorizacion.AutBackupCorreo = ""
            '    Autorizacion.AutOrden = 3
            '    AutorizacionLista.Add(Autorizacion)
            '    intProtesto = 1
            'End If

            'If (objPedido("porCosto").ToString.Trim = "True" Or objPedido("porMargen").ToString.Trim = "True" Or objPedido("porTasa").ToString.Trim = "True") Then
            '    For intCadena = 0 To arrFilaPedido.Count - 1 Step 1
            '        Dim FilaPedido As JObject = arrFilaPedido(intCadena)
            '        strCadena = Replace(Replace(FilaPedido("autoriza"), "[[", "["), "]]", "]")
            '        Dim arrCadena = Split(strCadena, "[")
            '        For intSubCadena = 1 To arrCadena.Count - 1 Step 1
            '            strSubCadena = Replace(Replace(arrCadena(intSubCadena), "]", ""), Chr(34), "")
            '            Dim arrSubCadena = Split(strSubCadena, ",")
            '            Dim Autorizacion As New clsAutorizacion()
            '            Autorizacion.AutOficialNombre = arrSubCadena(0)
            '            Autorizacion.AutOficialCorreo = fnc.ObtenerAutorizadorCorreo(arrSubCadena(0).Trim)
            '            Autorizacion.AutConcepto = arrSubCadena(1)
            '            Autorizacion.AutBackupNombre = arrSubCadena(2)
            '            Autorizacion.AutBackupCorreo = fnc.ObtenerAutorizadorCorreo(arrSubCadena(2).Trim)
            '            Select Case strConcepto
            '                Case "BM1"
            '                    Autorizacion.AutOrden = 4
            '                Case "BM2"
            '                    Autorizacion.AutOrden = 5
            '                Case "BM3"
            '                    Autorizacion.AutOrden = 6
            '                Case "BC1"
            '                    Autorizacion.AutOrden = 7
            '                Case "BC2"
            '                    Autorizacion.AutOrden = 8
            '                Case "BC3"
            '                    Autorizacion.AutOrden = 9
            '                Case "TASA"
            '                    Autorizacion.AutOrden = 10
            '                Case Else
            '                    Autorizacion.AutOrden = 11
            '            End Select
            '            AutorizacionLista.Add(Autorizacion)
            '        Next
            '    Next
            'End If

            'Dim ojNuevaLista = From objLista In AutorizacionLista Select New With {Key objLista.AutOrden, Key objLista.AutConcepto, Key objLista.AutOficialCorreo, objLista.AutOficialNombre, objLista.AutBackupNombre, objLista.AutBackupCorreo} Distinct.ToList
            'Dim objOrdenaLista = ojNuevaLista.OrderBy(Function(k) k.AutOrden).ToList()
            'AutorizacionLista.Clear()
            'For x As Integer = 0 To objOrdenaLista.Count - 1 Step 1
            '    Dim autorizador_temp As New clsAutorizacion
            '    autorizador_temp.AutOficialCorreo = objOrdenaLista.Item(x).AutOficialCorreo
            '    autorizador_temp.AutOficialNombre = objOrdenaLista.Item(x).AutOficialNombre
            '    autorizador_temp.AutConcepto = objOrdenaLista.Item(x).AutConcepto
            '    autorizador_temp.AutBackupNombre = objOrdenaLista.Item(x).AutBackupNombre
            '    autorizador_temp.AutBackupCorreo = objOrdenaLista.Item(x).AutBackupCorreo
            '    ToLog.write("-----------------1RA FASE------------------", "D")
            '    ToLog.write("strAutorizadorOficial: " + autorizador_temp.AutOficialNombre, "D")
            '    ToLog.write("strCorreoOficial: " + autorizador_temp.AutOficialCorreo, "D")
            '    ToLog.write("strConcepto: " + autorizador_temp.AutConcepto, "D")
            '    ToLog.write("strAutorizadorBackup: " + autorizador_temp.AutBackupNombre, "D")
            '    ToLog.write("strCorreoBackup: " + autorizador_temp.AutBackupCorreo, "D")
            '    ToLog.write("-------------------------------------------", "D")
            '    AutorizacionLista.Add(autorizador_temp)
            'Next (x)

            'For Each Autorizacion As clsAutorizacion In AutorizacionLista

            strAutorizadorOficial = Autorizacion.AutOficialNombre
            strCorreoOficial = Autorizacion.AutOficialCorreo
            strConcepto = Autorizacion.AutConcepto
            strAutorizadorBackup = Autorizacion.AutBackupNombre
            strCorreoBackup = Autorizacion.AutBackupCorreo

            strRandom = fnc.CifrarMD5(CStr(fnc.ObtenerRandom(0, 1000000000)))
            strCodigo = strRandom.Substring(0, 26) + intDocEntry.ToString

            intRespuesta = New clsDispatcherListado().RegistrarDispatcher(strCodigo, intDocEntry, strAutorizadorOficial, strConcepto, strAutorizadorBackup)
            strHtmlOficial = GenerarHTMLCorreoAutorizacionCredito(intDocEntry, strCodigo, strAutorizadorOficial, 0, strConcepto)
            If (strAutorizadorBackup.Trim <> "") Then
                strHtmlBackup = GenerarHTMLCorreoAutorizacionCredito(intDocEntry, strCodigo, strAutorizadorBackup, 1, strConcepto)
            End If

            'Select Case strConcepto
            '    Case "BM1"
            '        intBM1 += 1
            '    Case "BM2"
            '        intBM2 += 1
            '    Case "BM3"
            '        intBM3 += 1
            '    Case "BC1"
            '        intBC1 += 1
            '    Case "BC2"
            '        intBC2 += 1
            '    Case "BC3"
            '        intBC3 += 1
            '    Case "TASA"
            '        intTasa += 1
            'End Select

            Try
                Dim mail As New ToMail()
                Dim mail_backup As New ToMail()
                Dim av1 As AlternateView
                Dim av2 As AlternateView

                Dim mensaje As MailMessage
                Dim mensaje_backup As MailMessage
                Dim contentIdButtonOk As String = "buttonOk"
                Dim contentIdButtonCancel As String = "buttonCancel"
                Dim botonOk As String = HttpContext.Current.Server.MapPath("~") & "Images\" & "button_ok_32x32.png"
                Dim botonCancel As String = HttpContext.Current.Server.MapPath("~") & "Images\" & "button_cancel_32x32.png"
                Dim resourceButtonOk As LinkedResource = New LinkedResource(botonOk)
                Dim resourceButtonCancel As LinkedResource = New LinkedResource(botonCancel)
                resourceButtonOk.ContentId = contentIdButtonOk
                resourceButtonOk.ContentType.Name = botonOk
                resourceButtonCancel.ContentId = contentIdButtonCancel
                resourceButtonCancel.ContentType.Name = botonCancel

                If My.Resources.rscSistemaVentasWeb.zModalidad = "P" Then
                    mensaje = New MailMessage(My.Resources.rscSistemaVentasWeb.wCorreoEmisor, strCorreoOficial)
                    mensaje.Subject = strSerie + " - BORRADOR DE VENTA N° " + intDocEntry.ToString + " - " + strNombreCliente
                    If strCorreoBackup <> "" Then
                        mensaje_backup = New MailMessage(My.Resources.rscSistemaVentasWeb.wCorreoEmisor, strCorreoBackup)
                        mensaje_backup.Subject = "(Backup) " + strSerie + " - BORRADOR DE VENTA N° " + intDocEntry.ToString + " - " + strNombreCliente
                    End If
                Else
                    mensaje = New MailMessage(My.Resources.rscSistemaVentasWeb.wCorreoEmisor, My.Resources.rscSistemaVentasWeb.CorreoReceptorTST)
                    mensaje.Subject = "TEST (Oficial) " + strSerie + " - BORRADOR DE VENTA N° " + intDocEntry.ToString + " - " + strNombreCliente
                    mensaje_backup = New MailMessage(My.Resources.rscSistemaVentasWeb.wCorreoEmisor, My.Resources.rscSistemaVentasWeb.CorreoReceptorTST)
                    mensaje_backup.Subject = "TEST (Backup) " + strSerie + " - BORRADOR DE VENTA N° " + intDocEntry.ToString + " - " + strNombreCliente
                End If

                av1 = AlternateView.CreateAlternateViewFromString(strHtmlOficial, Nothing, MediaTypeNames.Text.Html)
                av1.LinkedResources.Add(resourceButtonOk)
                av1.LinkedResources.Add(resourceButtonCancel)
                mensaje.AlternateViews.Add(av1)
                mensaje.IsBodyHtml = True
                If strCorreoBackup <> "" Then
                    av2 = AlternateView.CreateAlternateViewFromString(strHtmlBackup, Nothing, MediaTypeNames.Text.Html)
                    av2.LinkedResources.Add(resourceButtonOk)
                    av2.LinkedResources.Add(resourceButtonCancel)
                    mensaje_backup.AlternateViews.Add(av2)
                    mensaje_backup.IsBodyHtml = True
                End If

                If strHtmlOficial <> "" Then
                    mail.mensaje = mensaje
                    mail.EnviarCorreo()
                    If strHtmlBackup <> "" Then
                        mail_backup.mensaje = mensaje_backup
                        mail_backup.EnviarCorreo()
                    End If
                End If
            Catch ex As Exception
                ToLog.write(ex.Message, "E")
            End Try
            'Next

            intRespuesta = New clsAutorizacionesListado().RegistrarAutorizaciones(intDocEntry, strOperador, intCredito, intProtesto, intMargen, intCosto, intFactura, intBM1, intBM2, intBM3, intBC1, intBC2, intBC3, strComentario2, intTasa)
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return strRetorno
    End Function
    Public Function GenerarHTMLCorreoAutorizacionCredito(ByVal intDocEntry As Integer, ByVal strCodigo As String, ByVal strAutorizador As String, ByVal intTipoAutorizador As Integer, ByVal strConcepto As String) As String
        Dim strServerWeb As String = IIf(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.ServerWebPRD, My.Resources.rscSistemaVentasWeb.ServerWebTST)
        Dim strURLAprobar As String = strServerWeb.Trim + "/Pages/pagAprobarBorrador.aspx?i=" + strCodigo.Trim + "&p=1&a=" + strAutorizador + "&t=" + intTipoAutorizador.ToString + "&c=" + strConcepto.Trim
        Dim strURLRechazar As String = strServerWeb.Trim + "/Pages/pagRechazarBorrador.aspx?i=" + strCodigo.Trim + "&p=2&a=" + strAutorizador + "&t=" + intTipoAutorizador.ToString + "&c=" + strConcepto.Trim

        Dim strHtml As String = ""
        Dim clsTransaccionListado As New clsTransaccionListado()
        For Each clsTransaccionResumen As clsTransaccionResumen In clsTransaccionListado.ObtenerTransaccionResumen(1, intDocEntry)
            If (clsTransaccionResumen.blnExiste) Then
                Dim intIndice As Integer = 0

                Dim strTituloTipoVenta = "SIN TIPO DE VENTA"
                Dim strTituloConcepto = "SIN CONCEPTO"

                Select Case strConcepto.Trim
                    Case "BM1", "BM2", "BM3"
                        strTituloConcepto = "VENTAS"
                    Case "BC1", "BC2", "BC3"
                        strTituloConcepto = "VENTAS"
                    Case Else
                        strTituloConcepto = strConcepto.Trim.ToUpper
                End Select

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
                Try 'RESUMEN
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
                    strHtml += "            white-space: nowrap;"
                    strHtml += "        }"
                    strHtml += "        .tdCenter"
                    strHtml += "        {"
                    strHtml += "            text-align:center;"
                    strHtml += "            vertical-align:central;"
                    strHtml += "            white-space: nowrap;"
                    strHtml += "        }"
                    strHtml += "        .tdLeft"
                    strHtml += "        {"
                    strHtml += "            text-align:left;"
                    strHtml += "            vertical-align:central;"
                    strHtml += "            white-space: nowrap;"
                    strHtml += "        }"

                    strHtml += "        .tdRightCortado"
                    strHtml += "        {"
                    strHtml += "            text-align:right;"
                    strHtml += "            vertical-align:central;"
                    strHtml += "        }"
                    strHtml += "        .tdCenterCortado"
                    strHtml += "        {"
                    strHtml += "            text-align:center;"
                    strHtml += "            vertical-align:central;"
                    strHtml += "        }"
                    strHtml += "        .tdLeftCortado"
                    strHtml += "        {"
                    strHtml += "            text-align:left;"
                    strHtml += "            vertical-align:central;"
                    strHtml += "        }"

                    strHtml += "        .tdRightCortadoCompra"
                    strHtml += "        {"
                    strHtml += "            text-align:right;"
                    strHtml += "            vertical-align:central;"
                    strHtml += "            background-color:  #0000FF;"
                    strHtml += "        }"
                    strHtml += "        .tdCenterCortadoCompra"
                    strHtml += "        {"
                    strHtml += "            text-align:center;"
                    strHtml += "            vertical-align:central;"
                    strHtml += "            background-color:  #0000FF;"
                    strHtml += "        }"
                    strHtml += "        .tdLeftCortadoCompra"
                    strHtml += "        {"
                    strHtml += "            text-align:left;"
                    strHtml += "            vertical-align:central;"
                    strHtml += "            background-color:  #191970;"
                    strHtml += "        }"
                    strHtml += "    </style>"

                    strHtml += "    <body>"

                    strHtml += "        <table style='width:100%;' border='0px'>"
                    strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>BORRADOR DE VENTA N° " + intDocEntry.ToString + " " + strTituloTipoVenta + " - AUTORIZACION POR " + strTituloConcepto + "   (" + clsTransaccionResumen.strEmision + ")</td></tr>"
                    strHtml += "            <tr>"
                    strHtml += "                <td colspan='6'>"
                    strHtml += "                    <a href='" + strURLAprobar + "'   ><img src=""cid:buttonOk""     width='32' height='32' border='0' alt='Aprobar'   ></a>"
                    strHtml += "                    <a href='" + strURLRechazar + "'  ><img src=""cid:buttonCancel"" width='32' height='32' border='0' alt='Rechazar'  ></a>"
                    strHtml += "                </td>"
                    strHtml += "            </tr>"

                    strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>DATOS DEL CLIENTE</td></tr>"
                    strHtml += "            <tr class='trTituloItem'><td class='tdLeft'>NOMBRE  </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strNombreCliente + "  </td><td class='tdLeft'>RUT  </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strRutCliente + " (" + clsTransaccionResumen.strCodigoCliente + ")</td><td class='tdLeft'>CATEGORIA</td><td class='tdLeftCortado'>" + clsTransaccionResumen.strCategoriaCliente + "</td></tr>"
                    strHtml += "            <tr class='trTituloItem'><td class='tdLeft'>TELEFONO</td><td class='tdLeftCortado'>" + clsTransaccionResumen.strTelefonoCliente + "</td><td class='tdLeft'>EMAIL</td><td class='tdLeftCortado'>" + clsTransaccionResumen.strCorreoCliente + "                                              </td><td class='tdLeft'>GIRO     </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strGiroCliente + "     </td></tr>"

                    strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>DATOS DEL NEGOCIO</td></tr>"
                    strHtml += "            <tr class='trTituloItem'><td class='tdLeft'>OPE.COMERCIAL      </td><td class='tdLeftCortado'> " + clsTransaccionResumen.strNombreOperador + "    </td><td class='tdLeft'>OFICINA       </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strSerie + "           </td><td class='tdLeft'>TIPO DESPACHO</td><td class='tdLeftCortado'>" + clsTransaccionResumen.strTipoDespacho + "</td></tr>"
                    strHtml += "            <tr class='trTituloItem'><td class='tdLeft'>CONDICION PAGO     </td><td class='tdLeftCortado'> " + clsTransaccionResumen.strNombrePlazoVenta + "  </td><td class='tdLeft'>FEC.ORDEN VTA.</td><td class='tdLeftCortado'>" + clsTransaccionResumen.strFechaOrdenVenta + " </td><td class='tdLeft'>MONEDA PAGO  </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strMonedaPago + "  </td></tr>"
                    strHtml += "            <tr class='trTituloItem'><td class='tdLeft'>DIAS PLAZO VTA.REAL</td><td class='tdLeftCortado'>" + clsTransaccionResumen.intDiasExtras.ToString + "</td><td class='tdLeft'>FEC.VENCTO.   </td><td class='tdLeftCortado'>" + clsTransaccionResumen.strFechaVencimiento + "</td><td class='tdLeft'>.</td><td class='tdLeftCortado'>" + "." + "</td></tr>"



                    If (strTituloConcepto = "CREDITOS") Then
                        strHtml += "        <tr class='trTituloSeccion'><td colspan='6'>LINEA DE CREDITO</td></tr>"
                        Dim clsClienteListado As New clsClienteListado()
                        For Each clsCliente As clsCliente In clsClienteListado.ObtenerClienteLineaCredito(clsTransaccionResumen.strCodigoCliente)
                            strHtml += "    <tr class='trTituloItem'><td class='tdLeft'>AUTORIZADO</td><td class='tdLeftCortado'>" + (FormatNumber(clsCliente.CliAutorizado, 0)).ToString + "</td><td class='tdLeft'>UTILIZADO</td><td class='tdLeftCortado'>" + (FormatNumber(clsCliente.CliUtilizado, 0)).ToString + "</td><td class='tdLeft'>DISPONIBLE</td><td class='tdLeftCortado'>" + (FormatNumber(clsCliente.CliDisponible, 0)).ToString + "</td></tr>"
                        Next
                    End If

                    If (strTituloConcepto = "DEUDA VENCIDA") Then
                        strHtml += "        <tr class='trTituloSeccion'><td colspan='6'>DEUDAS VENCIDAS</td></tr>"
                        strHtml += "        <tr class='trTituloItem'><td class='tdLeft' colspan='2'>N°DOCUMENTO</td><td class='tdCenter' colspan='2'>FECHA DOCUMENTO</td><td class='tdRight' colspan='2'>MONTO CLP</td></tr>"
                        Dim clsClienteListado As New clsClienteListado()
                        For Each clsCliente As clsCliente In clsClienteListado.ObtenerClienteFacturaImpaga(clsTransaccionResumen.strCodigoCliente)
                            strHtml += "    <tr class='trTituloItem'><td class='tdLeftCortado' colspan='2'>" + clsCliente.FacNumero.ToString + "</td><td class='tdCenterCortado' colspan='2'>" + clsCliente.FacFecha.ToString + "</td><td class='tdRightCortado' colspan='2'>" + (FormatNumber(clsCliente.FacValor, 0)).ToString + "</td></tr>"
                        Next
                    End If

                    If (strTituloConcepto = "PROTESTOS") Then
                        strHtml += "        <tr class='trTituloSeccion'><td colspan='6'>PROTESTOS</td></tr>"
                        strHtml += "        <tr class='trTituloItem'><td class='tdLeft' colspan='2'>N°DOCUMENTO</td><td class='tdCenter' colspan='2'>FECHA DOCUMENTO</td><td class='tdRight' colspan='2'>MONTO CLP</td></tr>"
                        Dim clsClienteListado As New clsClienteListado()
                        For Each clsCliente As clsCliente In clsClienteListado.ObtenerClienteChequeProtestado(clsTransaccionResumen.strCodigoCliente)
                            strHtml += "    <tr class='trTituloItem'><td class='tdLeftCortado' colspan='2'>" + clsCliente.CheNumero.ToString + "</td><td class='tdCenterCortado' colspan='2'>" + clsCliente.CheFecha.ToString + "</td><td class='tdRightCortado' colspan='2'>" + (FormatNumber(clsCliente.CheValor, 0)).ToString + "</td></tr>"
                        Next
                    End If



                    strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>COMENTARIO FACTURACION</td></tr>"
                    strHtml += "            <tr class='trTituloItem'><td colspan='6' class='tdLeftCortado'>" + IIf(clsTransaccionResumen.strComentario1.Trim <> "", clsTransaccionResumen.strComentario1.Trim.ToUpper, ".") + "</td></tr>"
                    strHtml += "            <tr class='trTituloSeccion'><td colspan='6'>COMENTARIO AUTORIZADOR</td></tr>"
                    strHtml += "            <tr class='trTituloItem'><td colspan='6' class='tdLeftCortado'>" + IIf(clsTransaccionResumen.strComentario2.Trim <> "", clsTransaccionResumen.strComentario2.Trim.ToUpper, ".") + "</td></tr>"
                    If (clsTransaccionResumen.strTipoVenta.Trim = "ventaPuestoFundo" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCalzadaProveedor") Then
                        strHtml += "        <tr class='trTituloSeccion'><td colspan='6'>COMENTARIO ABASTECIMIENTO</td></tr>"
                        strHtml += "        <tr class='trTituloItem'><td colspan='6' class='tdLeftCortado'>" + IIf(clsTransaccionResumen.strComentario3.Trim <> "", clsTransaccionResumen.strComentario3.Trim.ToUpper, ".") + "</td></tr>"
                    End If
                    Try 'DETALLE
                        'strHtml += "        <tr class='trTituloSeccion'><td colspan='6'>PRODUCTOS SOLICITADOS</td></tr>"
                        strHtml += "        <tr>"
                        strHtml += "            <td colspan='6'>"
                        strHtml += "                <table id='tblPedido' width='100%' border='1px'>"





                        strHtml += "                    <tr class='trTituloSeccion'>"
                        strHtml += "                        <td class='tdLeftCortado' colspan='12'>DATOS DE LA VENTA</td>"
                        If (clsTransaccionResumen.strTipoVenta.Trim = "ventaPuestoFundo" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCalzadaProveedor" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCostoEspecial" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaLiquidacion") Then
                            strHtml += "                    <td class='tdLeftCortadoCompra' colspan='8'>DATOS DE LA COMPRA</td>"
                        End If
                        strHtml += "                        <td class='tdLeftCortado' colspan='1'></td>"
                        strHtml += "                    </tr>"





                        strHtml += "                    <tr Class='trTituloCabecera'>"
                        strHtml += "                        <td class='tdCenterCortado'>Codigo</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Descripcion</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Cantidad</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Moneda</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Precio Unitario</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Total Unitario CLP</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Descto. %</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Margen %</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Interes CLP</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Flete CLP</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Fecha Entrega</td>"
                        strHtml += "                        <td class='tdCenterCortado'>Precio Final CLP</td>"
                        If (clsTransaccionResumen.strTipoVenta.Trim = "ventaPuestoFundo" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCalzadaProveedor" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCostoEspecial" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaLiquidacion") Then
                            strHtml += "                    <td class='tdCenterCortadoCompra'>Dias Compra</td>"
                            strHtml += "                    <td class='tdCenterCortadoCompra'>Moneda</td>"
                            strHtml += "                    <td class='tdCenterCortadoCompra'>Precio Unitario</td>"
                            strHtml += "                    <td class='tdCenterCortadoCompra'>Total Unitario CLP</td>"
                            strHtml += "                    <td class='tdCenterCortadoCompra'>Proveedor</td>"
                            strHtml += "                    <td class='tdCenterCortadoCompra'>Tasa Interes %</td>"

                            strHtml += "                    <td class='tdCenterCortadoCompra'>Condicion</td>"
                            strHtml += "                    <td class='tdCenterCortadoCompra'>Motivo</td>"
                        End If
                        strHtml += "                        <td class='tdCenterCortado'>Costo Reposicion</td>"
                        strHtml += "                    </tr>"
                        Dim intTotalNeto As Integer = 0
                        Dim intTotalIva As Integer = 0
                        Dim intTotalBruto As Integer = 0
                        Dim dblTipoCambio As Double = 0.0
                        Dim intCantidadProductos As Integer = 0
                        Dim dblMargenComercialProducto As Double = 0.0
                        Dim intSumaTotalUnitarioProducto As Integer = 0
                        Dim intSumaTotalCostoComercial As Double = 0.0
                        Dim dblTotalMargenComercial As Double = 0.0
                        For Each clsTransaccionDetalle As clsTransaccionDetalle In clsTransaccionListado.ObtenerTransaccionDetalle(1, intDocEntry, "", 0)
                            If (clsTransaccionDetalle.strCodigoProducto <> "DESCUENTO" And clsTransaccionDetalle.strCodigoProducto <> "Z8200003000000") Then

                                intTotalNeto += CInt(clsTransaccionDetalle.intTotalUnitarioProducto)
                                intTotalIva = CInt(Math.Round(intTotalNeto * 0.19))
                                intTotalBruto = intTotalNeto + intTotalIva
                                intCantidadProductos += 1
                                dblTotalMargenComercial = 0.0
                                dblTipoCambio = 1.0
                                If (clsTransaccionDetalle.strMonedaProducto = "USD") Then
                                    dblTipoCambio = CDbl(clsTransaccionDetalle.dblMonedaProducto)
                                ElseIf (clsTransaccionDetalle.strMonedaProducto = "EUR") Then
                                    dblTipoCambio = CDbl(clsTransaccionDetalle.dblMonedaProducto)
                                End If
                                If (clsTransaccionResumen.strTipoVenta.Trim = "ventaPuestoFundo" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCalzadaProveedor" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCostoEspecial" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaLiquidacion") Then
                                    dblMargenComercialProducto = Format(100 - ((clsTransaccionDetalle.dblPrecioUnitarioProductoCompra * 100) / clsTransaccionDetalle.dblPrecioUnitarioProducto), "0.00")
                                    intSumaTotalUnitarioProducto += (CDbl(clsTransaccionDetalle.dblPrecioUnitarioProducto) * CInt(clsTransaccionDetalle.intCantidadProducto)) * dblTipoCambio
                                    intSumaTotalCostoComercial += (CDbl(clsTransaccionDetalle.dblPrecioUnitarioProductoCompra) * CInt(clsTransaccionDetalle.intCantidadProducto)) * dblTipoCambio
                                    dblTotalMargenComercial = (((intSumaTotalUnitarioProducto - intSumaTotalCostoComercial) / intSumaTotalUnitarioProducto) * 100.0)
                                Else
                                    dblMargenComercialProducto = Format(100 - ((clsTransaccionDetalle.dblCostoComercialProducto * 100) / clsTransaccionDetalle.dblPrecioUnitarioProducto), "0.00")
                                    intSumaTotalUnitarioProducto += (CDbl(clsTransaccionDetalle.dblPrecioUnitarioProducto) * CInt(clsTransaccionDetalle.intCantidadProducto)) * dblTipoCambio
                                    intSumaTotalCostoComercial += (CDbl(clsTransaccionDetalle.dblCostoComercialProducto) * CInt(clsTransaccionDetalle.intCantidadProducto)) * dblTipoCambio
                                    dblTotalMargenComercial = (((intSumaTotalUnitarioProducto - intSumaTotalCostoComercial) / intSumaTotalUnitarioProducto) * 100.0)
                                End If
                                strHtml += "                <tr class='trTituloItem'>"
                                strHtml += "                    <td class='tdLeftCortado'>" + clsTransaccionDetalle.strCodigoProducto + "</td>"
                                strHtml += "                    <td class='tdLeftCortado'>" + clsTransaccionDetalle.strNombreProducto + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + clsTransaccionDetalle.intCantidadProducto.ToString + "</td>"
                                strHtml += "                    <td class='tdCenterCortado'>" + clsTransaccionDetalle.strMonedaProducto + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.dblPrecioUnitarioProducto, 2)).ToString + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.intTotalUnitarioProducto, 0)).ToString + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.dblDescuentoProducto, 2)).ToString + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(dblMargenComercialProducto, 2)).ToString + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.intInteresProducto, 0)).ToString + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.intFleteProducto, 0)).ToString + "</td>"
                                strHtml += "                    <td class='tdCenterCortado'>" + clsTransaccionDetalle.strFechaEntregaProducto + "</td>"
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.intTotalUnitarioProducto, 0)).ToString + "</td>"
                                If (clsTransaccionResumen.strTipoVenta.Trim = "ventaPuestoFundo" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCalzadaProveedor" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCostoEspecial" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaLiquidacion") Then
                                    strHtml += "                <td class='tdRightCortado'>" + clsTransaccionDetalle.intDiasCompra.ToString + "</td>"
                                    strHtml += "                <td class='tdCenterCortado'>" + clsTransaccionDetalle.strMonedaProductoCompra + "</td>"
                                    strHtml += "                <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.dblPrecioUnitarioProductoCompra, 2)).ToString + "</td>"
                                    strHtml += "                <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.dblTotalUnitarioProductoCompra, 0)).ToString + "</td>"
                                    strHtml += "                <td class='tdCenterCortado'>" + fnc.ObtenerProveedorNombre(clsTransaccionDetalle.strCodigoProveedorCompra) + "</td>"
                                    strHtml += "                <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.dblTasaInteresCompra, 2)).ToString + "</td>"


                                    strHtml += "                <td class='tdCenterCortado'>" + clsTransaccionDetalle.strCondicionProducto + "</td>"
                                    strHtml += "                <td class='tdCenterCortado'>" + clsTransaccionDetalle.strMotivoProducto + "</td>"
                                End If
                                strHtml += "                    <td class='tdRightCortado'>" + (FormatNumber(clsTransaccionDetalle.dblCostoReposicionProducto, 2)).ToString + "</td>"
                                strHtml += "                </tr>"

                            End If
                        Next
                        If (clsTransaccionResumen.strTipoVenta.Trim = "ventaPuestoFundo" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCalzadaProveedor" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaCostoEspecial" Or clsTransaccionResumen.strTipoVenta.Trim = "ventaLiquidacion") Then
                            strHtml += "                    <tr><td colspan='21'><br></td></tr>"
                            strHtml += "                    <tr class='trTituloItem'><td class='tdLeft'>                 </td><td class='tdLeft' colspan='9'>                                                             </td><td class='tdLeft'>Total Neto</td><td class='tdRight'> " + (FormatNumber(intTotalNeto, 0)).ToString + " </td><td colspan='9'></td></tr>"
                            strHtml += "                    <tr class='trTituloItem'><td class='tdLeft'>Cantidad Items:  </td><td class='tdLeft' colspan='9'>" + intCantidadProductos.ToString + "                        </td><td class='tdLeft'>Total IVA</td><td class='tdRight'>  " + (FormatNumber(intTotalIva, 0)).ToString + "  </td><td colspan='9'></td></tr>"
                            strHtml += "                    <tr class='trTituloItem'><td class='tdLeft'>Margen Comercial:</td><td class='tdLeft' colspan='9'>" + (FormatNumber(dblTotalMargenComercial, 2)).ToString + " %</td><td class='tdLeft'>Total Bruto</td><td class='tdRight'>" + (FormatNumber(intTotalBruto, 0)).ToString + "</td><td colspan='9'></td></tr>"
                        Else
                            strHtml += "                    <tr><td colspan='13'><br></td></tr>"
                            strHtml += "                    <tr class='trTituloItem'><td class='tdLeft'>                 </td><td class='tdLeft' colspan='9'>                                                             </td><td class='tdLeft'>Total Neto</td><td class='tdRight'> " + (FormatNumber(intTotalNeto, 0)).ToString + " </td><td colspan='1'></td></tr>"
                            strHtml += "                    <tr class='trTituloItem'><td class='tdLeft'>Cantidad Items:  </td><td class='tdLeft' colspan='9'>" + intCantidadProductos.ToString + "                        </td><td class='tdLeft'>Total IVA</td><td class='tdRight'>  " + (FormatNumber(intTotalIva, 0)).ToString + "  </td><td colspan='1'></td></tr>"
                            strHtml += "                    <tr class='trTituloItem'><td class='tdLeft'>Margen Comercial:</td><td class='tdLeft' colspan='9'>" + (FormatNumber(dblTotalMargenComercial, 2)).ToString + " %</td><td class='tdLeft'>Total Bruto</td><td class='tdRight'>" + (FormatNumber(intTotalBruto, 0)).ToString + "</td><td colspan='1'></td></tr>"
                        End If
                        'strHtml += "                    <tr><td colspan='" + (12 + intColumnas).ToString + "'><br></td></tr>"
                        'strHtml += "                    <tr class='trTituloItem'><td class='tdLeft'>                 </td><td class='tdLeft' colspan='" + (9 + intColumnas).ToString + "'>                                                             </td><td class='tdLeft'>Total Neto</td><td class='tdRight'> " + intTotalNeto.ToString + " </td></tr>"
                        'strHtml += "                    <tr class='trTituloItem'><td class='tdLeft'>Cantidad Items:  </td><td class='tdLeft' colspan='" + (9 + intColumnas).ToString + "'>" + intCantidadProductos.ToString + "                        </td><td class='tdLeft'>Total IVA</td><td class='tdRight'>  " + intTotalIva.ToString + "  </td></tr>"
                        'strHtml += "                    <tr class='trTituloItem'><td class='tdLeft'>Margen Comercial:</td><td class='tdLeft' colspan='" + (9 + intColumnas).ToString + "'>" + (FormatNumber(dblTotalMargenComercial, 2)).ToString + " %</td><td class='tdLeft'>Total Bruto</td><td class='tdRight'>" + intTotalBruto.ToString + "</td></tr>"
                        strHtml += "                </table>"
                        strHtml += "            </td>"
                        strHtml += "        </tr>"
                        strHtml += "    </table>"


                        'strHtml += "        </table>"
                        strHtml += "    </body>"
                        strHtml += "</head>"
                        strHtml += "</html>"

                    Catch ex As Exception
                        strHtml = ""
                        ToLog.write("lectura de detalle: " + ex.Message, "E")
                    End Try
                Catch ex As Exception
                    strHtml = ""
                    ToLog.write("lectura de resumen: " + ex.Message, "E")
                End Try
            End If
        Next
        Return strHtml
    End Function


End Class