Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Net.Mail
Imports System.Net.Mime

Public Class pagGenerarOrdenCompra
    Inherits System.Web.UI.Page
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (IsPostBack) Then
            Dim strProceso As String = Request.QueryString("prmProceso")
            If (strProceso = "G") Then
                LeerDatosOrdenVenta()
            Else
                EnviarCorreo()   'A
            End If
        End If
    End Sub

    Public Sub LeerDatosOrdenVenta()
        Dim intUsuario As Integer = 0
        Dim intDocEntry As Integer = 0
        Dim intDocNum As Integer = 0
        Dim strProveedor As String = ""
        Dim intDiaCompra As Integer = 0
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_orden_compra_genera 10"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                intUsuario = rdr.Item("Usuario")
                intDocEntry = rdr.Item("DocEntry")
                intDocNum = rdr.Item("DocNum")
                strProveedor = rdr.Item("Proveedor")
                intDiaCompra = rdr.Item("DiaCompra")
                GenerarOrdenCompra(intUsuario, intDocEntry, intDocNum, strProveedor, intDiaCompra)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
    End Sub

    Public Sub GenerarOrdenCompra(ByVal intUsuario As Integer, ByVal intDocEntry As Integer, ByVal intDocNum As Integer, ByVal strProveedor As String, ByVal intDiaCompra As Integer)
        ToLog.write("RegistraDocumentoCompra: " + intUsuario.ToString + ", " + intDocEntry.ToString() + ", " + intDocNum.ToString + ", " + strProveedor + ", " + intDiaCompra.ToString, "D")
        Dim clsOrdenCompra As New WebServices.clsOrdenCompra
        Dim strRetornoOC As Tuple(Of Integer, Integer, Integer, String) = clsOrdenCompra.RegistrarOrdenCompra(intUsuario, intDocEntry, intDocNum, strProveedor, intDiaCompra)
        Dim intStatusOC As Integer = strRetornoOC.Item1
        Dim intDocEntryOC As Integer = strRetornoOC.Item2
        Dim intDocNumOC As Integer = strRetornoOC.Item3
        Dim strStatusOC As String = strRetornoOC.Item4
        If (intStatusOC = 0) Then
            ToLog.write("Orden compra creada: " + intDocEntryOC.ToString + ", " + intDocNumOC.ToString() + ", " + strProveedor + ", " + intDiaCompra.ToString, "D")
        Else
            ToLog.write("Orden compra no creada para el proveedor : " + strProveedor, "D")
        End If
    End Sub

    Public Sub EnviarCorreo()
        Dim conCorreo As New System.Net.Mail.MailMessage()
        Dim cnfCorreo As New System.Net.Mail.SmtpClient
        Dim strAsunto As String = ""
        Dim strCuerpo As String = ""
        Dim datHoy As Date = Date.Now
        Dim datAyer As Date = datHoy.AddDays(-1)
        Dim intHora As Integer = Replace(Left(Date.Now.ToString("HH:mm:ss"), 2), ":", "")
        strAsunto = "Ordenes de Compras Generadas con Fecha " + datAyer.ToString("dd/MM/yyyy") 'IIf(intHora > 6, datHoy.ToString("dd/MM/yyyy"), datAyer.ToString("dd/MM/yyyy"))
        strCuerpo = GenerarHTMLOrdenCompra()
        conCorreo.From = New MailAddress(My.Resources.rscSistemaVentasWeb.wCorreoEmisor, "Depto. de Logistica y Abastecimientos", System.Text.Encoding.UTF8)
        If My.Resources.rscSistemaVentasWeb.zModalidad = "P" Then
            conCorreo.To.Add(My.Resources.rscSistemaVentasWeb.CorreoReceptorPRD)
            conCorreo.CC.Add("dpalma@tattersall.cl")
            conCorreo.CC.Add("smancilla@tattersall.cl")
            conCorreo.Bcc.Add(My.Resources.rscSistemaVentasWeb.CorreoReceptorTST)
        Else
            conCorreo.To.Add(My.Resources.rscSistemaVentasWeb.CorreoReceptorTST)
        End If
        conCorreo.Subject = strAsunto.Trim()
        conCorreo.SubjectEncoding = System.Text.Encoding.UTF8
        conCorreo.Body = strCuerpo.Trim()
        conCorreo.BodyEncoding = System.Text.Encoding.UTF8
        conCorreo.IsBodyHtml = True
        cnfCorreo.Credentials = New System.Net.NetworkCredential("reportestai@tattersall.cl", "Xux04256")
        cnfCorreo.Port = 25
        cnfCorreo.Host = "smtpoffice365.tattersall.cl"
        cnfCorreo.EnableSsl = False
        Try
            cnfCorreo.Send(conCorreo)
        Catch ex As System.Net.Mail.SmtpException
            ToLog.write(ex.Message, "E")
        End Try
    End Sub

    Public Function GenerarHTMLOrdenCompra() As String
        Dim strHtml As String = ""
        Dim strHtmlD As String = ""
        strHtml += "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'>"
        strHtml += "<html>"
        strHtml += "    <head>"
        strHtml += "        <title>SISTEMA DE VENTAS</title>"
        strHtml += "        <style type='text/css'>"
        strHtml += "            .trTituloSeccion"
        strHtml += "            {"
        strHtml += "                height:10px;"
        strHtml += "                color:white;"
        strHtml += "                font-family:Arial, Lucida Sans Unicode, Lucida Grande, Sans-Serif;"
        strHtml += "                font-size:small;"
        strHtml += "                font-style:normal;"
        strHtml += "                text-align:left;"
        strHtml += "                background-color:#003300;"
        strHtml += "            }"
        strHtml += "            .trTituloCabecera"
        strHtml += "            {"
        strHtml += "                height:10px;"
        strHtml += "                color:white;"
        strHtml += "                font-family:Arial, Lucida Sans Unicode, Lucida Grande, Sans-Serif;"
        strHtml += "                font-size:small;"
        strHtml += "                font-style:normal;"
        strHtml += "                background-color:green;"
        strHtml += "            }"
        strHtml += "            .trTituloItem"
        strHtml += "            {"
        strHtml += "                height:10px;"
        strHtml += "                color:black;"
        strHtml += "                font-family:Arial, Lucida Sans Unicode, Lucida Grande, Sans-Serif;"
        strHtml += "                font-size:small;"
        strHtml += "                font-style:normal;"
        strHtml += "            }"
        strHtml += "            .tdRight"
        strHtml += "            {"
        strHtml += "                text-align:right;"
        strHtml += "                vertical-align:central;"
        strHtml += "                white-space: nowrap;"
        strHtml += "            }"
        strHtml += "            .tdCenter"
        strHtml += "            {"
        strHtml += "                text-align:center;"
        strHtml += "                vertical-align:central;"
        strHtml += "                white-space: nowrap;"
        strHtml += "            }"
        strHtml += "            .tdLeft"
        strHtml += "            {"
        strHtml += "                text-align:left;"
        strHtml += "                vertical-align:central;"
        strHtml += "                white-space: nowrap;"
        strHtml += "            }"
        strHtml += "            .tdRightCortado"
        strHtml += "            {"
        strHtml += "                text-align:right;"
        strHtml += "                vertical-align:central;"
        strHtml += "            }"
        strHtml += "            .tdCenterCortado"
        strHtml += "            {"
        strHtml += "                text-align:center;"
        strHtml += "                vertical-align:central;"
        strHtml += "            }"
        strHtml += "            .tdLeftCortado"
        strHtml += "            {"
        strHtml += "                text-align:left;"
        strHtml += "                vertical-align:central;"
        strHtml += "            }"
        strHtml += "        </style>"
        strHtml += "        <body>"

        strHtml += "            Estimados,"
        strHtml += "            <br>"
        strHtml += "            <br>"
        strHtml += "            Informamos que se han generado las siguientes ordenes de compras de manera automatica."
        strHtml += "            <br>"
        strHtml += "            <br>"

        strHtml += "            <table width='100%' border='0px'>"
        strHtml += "                <tr class='trTituloSeccion'><td colspan='8'>ORDENES DE COMPRAS</td></tr>"
        strHtml += "                <tr class='trTituloCabecera'>"
        'strHtml += "                   <td class='tdCenter' colspan='3'>Datos OV</td>"
        strHtml += "                    <td class='tdCenter' colspan='5'>Factura de Venta Referenciada</td>"
        strHtml += "                    <td class='tdCenter' colspan='3'>Orden de Compra Creada</td>"
        strHtml += "                </tr>"
        strHtml += "                <tr class='trTituloCabecera'>"
        'strHtml += "                   <td class='tdCenter'>Fecha</td>"
        'strHtml += "                   <td class='tdCenter'>Nro.SAP</td>"
        strHtml += "                    <td class='tdCenter'>Tipo Venta</td>"
        strHtml += "                    <td class='tdCenter'>Nro.SAP</td>"
        strHtml += "                    <td class='tdCenter'>Folio</td>"
        strHtml += "                    <td class='tdCenter'>Cod.Cliente</td>"
        strHtml += "                    <td class='tdCenter'>Nombre Cliente</td>"
        strHtml += "                    <td class='tdCenter'>Nro.SAP</td>"
        strHtml += "                    <td class='tdCenter'>Cod.Proveedor</td>"
        strHtml += "                    <td class='tdCenter'>Nombre Proveedor</td>"
        strHtml += "                </tr>"
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_orden_compra_genera 20"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                strHtmlD += "       <tr class='trTituloItem'>"
                'strHtmlD += "          <td class='tdLeft'>" + rdr.Item("FechaOV").ToString() + "</td>"
                'strHtmlD += "          <td class='tdRight'>" + rdr.Item("DocNumOV").ToString() + "</td>"
                strHtmlD += "           <td class='tdLeft'>" + rdr.Item("TipoVenta") + "</td>"
                strHtmlD += "           <td class='tdRight'>" + rdr.Item("DocNumFV").ToString() + "</td>"
                strHtmlD += "           <td class='tdRight'>" + rdr.Item("FolioFV").ToString() + "</td>"
                strHtmlD += "           <td class='tdLeft'>" + rdr.Item("CardCodeFV") + "</td>"
                strHtmlD += "           <td class='tdLeft'>" + rdr.Item("CardNameFV") + "</td>"
                strHtmlD += "           <td class='tdRight'>" + rdr.Item("DocNumOC").ToString() + "</td>"
                strHtmlD += "           <td class='tdLeft'>" + rdr.Item("CardCodeOC") + "</td>"
                strHtmlD += "           <td class='tdLeft'>" + rdr.Item("CardNameOC") + "</td>"
                strHtmlD += "       </tr>"
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try

        If (strHtmlD.Trim() = "") Then
            strHtmlD += "           <tr class='trTituloItem'>"
            strHtmlD += "               <td class='tdCenter' colspan='8'>No hay informacion de OC generadas.</td>"
            strHtmlD += "           </tr>"
        End If

        strHtml = strHtml + strHtmlD

        strHtml += "                <tr class='trTituloSeccion'><td colspan='8'></td></tr>"
        strHtml += "            </table>"

        strHtml += "            <br>"
        strHtml += "            Nota : este correo es generado de manera automatica, no responder."
        strHtml += "            <br>"
        strHtml += "            <br>"
        strHtml += "            Atte.,"
        strHtml += "            <br>"
        strHtml += "            Depto. de Logistica y Abastecimientos."

        strHtml += "        </body>"
        strHtml += "    </head>"
        strHtml += "</html>"
        Return strHtml
    End Function
End Class