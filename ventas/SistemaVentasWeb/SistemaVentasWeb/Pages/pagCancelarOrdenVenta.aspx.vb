Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net.Mime
Public Class pagCancelarOrdenVenta
    Inherits System.Web.UI.Page
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (IsPostBack) Then
            Dim strProceso As String = Request.QueryString("prmProceso")
            LeerCancelacion(strProceso)
        End If
    End Sub
    Public Sub LeerCancelacion(ByVal strProceso As String)
        Dim clsOrdenVenta As New WebServices.clsOrdenVenta()
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_cancela_orden_venta '" + strProceso.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim intUsuCodigo = rdr.Item("UsuCodigo")
                Dim intOpeCodigo = rdr.Item("OpeCodigo")
                Dim strOpeCorreo = rdr.Item("OpeCorreo")
                Dim intDocEntry = rdr.Item("DocEntry")
                Dim intDocNumero = rdr.Item("DocNumero")
                Dim strCliCodigo = rdr.Item("CliCodigo")
                Dim strCliNombre = rdr.Item("CliNombre")
                Dim intDocTotal = rdr.Item("DocTotal")
                Dim strVenTipo = rdr.Item("VenTipo")
                If (strProceso = "A") Then
                    EnviarCorreo(strOpeCorreo, intDocNumero, intDocEntry, strCliCodigo, strCliNombre, intDocTotal, strVenTipo)
                Else
                    Dim strRetorno As Tuple(Of Integer, Integer, String) = clsOrdenVenta.CancelarOrdenVenta(intDocNumero, intUsuCodigo)
                    Dim intDocNum As Integer = strRetorno.Item1
                    Dim intStatus As Integer = strRetorno.Item2
                    Dim strStatus As String = strRetorno.Item3
                    If (intStatus <> 0) Then
                        ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
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
    End Sub
    Public Sub EnviarCorreo(ByVal strOpeCorreo As String, ByVal intDocNumero As Integer, ByVal DocEntry As Integer, ByVal strCliCodigo As String, ByVal strCliNombre As String, ByVal intDocTotal As Integer, ByVal strVenTipo As String)
        Dim conCorreo As New System.Net.Mail.MailMessage()
        Dim cnfCorreo As New System.Net.Mail.SmtpClient
        Dim strAsunto As String = ""
        Dim strCuerpo As String = ""
        strAsunto = "Cancelacion de orden de venta N°" + intDocNumero.ToString
        strCuerpo = "Estimado, " + vbCrLf + vbCrLf
        strCuerpo = strCuerpo + "Informamos que la venta " + strVenTipo + " asociada a la orden de venta N° " + intDocNumero.ToString + " por un monto de $" + (FormatNumber(intDocTotal, 0)).ToString + ".-, perteneciente al cliente " + strCliNombre + " codigo " + strCliCodigo + ", sera cancelada mañana de manera automatica por encontrase pendiente de facturacion por mas del tiempo maximo permitido." + vbCrLf + vbCrLf
        strCuerpo = strCuerpo + "NOTA: Este correo es generado en forma automática (no responder)."
        strCuerpo = strCuerpo + vbCrLf + vbCrLf
        strCuerpo = strCuerpo + "Sin otro particular," + vbCrLf
        strCuerpo = strCuerpo + "Atte.," + vbCrLf
        strCuerpo = strCuerpo + "Depto. de Logistica y Abastecimientos."
        conCorreo.From = New MailAddress(My.Resources.rscSistemaVentasWeb.wCorreoEmisor, "Depto. de Logistica y Abastecimientos", System.Text.Encoding.UTF8)
        If My.Resources.rscSistemaVentasWeb.zModalidad = "P" Then
            If (strOpeCorreo.Trim <> "") Then conCorreo.To.Add(strOpeCorreo)
            conCorreo.Bcc.Add(My.Resources.rscSistemaVentasWeb.CorreoReceptorTST)
        Else
            conCorreo.To.Add(My.Resources.rscSistemaVentasWeb.CorreoReceptorTST)
        End If
        conCorreo.Subject = strAsunto.Trim()
        conCorreo.SubjectEncoding = System.Text.Encoding.UTF8
        conCorreo.Body = strCuerpo.Trim()
        conCorreo.BodyEncoding = System.Text.Encoding.UTF8
        conCorreo.IsBodyHtml = False
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
End Class