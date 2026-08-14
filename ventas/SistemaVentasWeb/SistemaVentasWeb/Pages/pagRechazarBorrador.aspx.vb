Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Public Class pagRechazarBorrador
    Inherits System.Web.UI.Page
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public strHtmlDispatcher As String = ""
    Private Property strCodigo As String
    Private Property intDocEntry As Integer
    Private Property chrAccion As String
    Private Property strAutorizador As String
    Private Property chrTipoAutorizador As String
    Private Property strConcepto As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.strCodigo = Request.QueryString("i")
        Me.intDocEntry = Right(Me.strCodigo.Trim, 6)
        Me.chrAccion = Request.QueryString("p")
        Me.strAutorizador = Request.QueryString("a")
        Me.chrTipoAutorizador = Request.QueryString("t")
        Me.strConcepto = Request.QueryString("c")
        Dim strLogo As String = IIf(IsNothing(Request.QueryString("x")), "", Request.QueryString("x"))
        If (strLogo.Trim = "x") Then
            imgLogo.Visible = False
        End If
        lblTituloSeccion.Text = "Rechazo de Pedido N° " + Me.intDocEntry.ToString
        strHtmlDispatcher = GenerarHTMLDispatcher()
        'If (strHtmlDispatcher.Trim = "") Then
        '    btnAceptar.Enabled = False
        'End If
    End Sub
    Public Function GenerarHTMLDispatcher()
        Dim strHtml As String = ""
        Dim strStyle As String = ""
        Try
            strHtml += "<table id='tblAutorizacion' border='1' style='width:800px;'>"
            strHtml += "    <tr class='trTituloCabecera'>"
            strHtml += "       <td class='tdLeft'>Autorizador</td>"
            strHtml += "       <td class='tdLeft'>Cargo</td>"
            strHtml += "       <td class='tdCenter'>Tipo</td>"
            strHtml += "       <td class='tdCenter'>Estado</td>"
            strHtml += "       <td class='tdLeft'>Backup</td>"
            strHtml += "       <td class='tdCenter'>Fecha Creacion</td>"
            strHtml += "       <td class='tdCenter'>Fecha Respuesta</td>"
            strHtml += "       <td class='tdLeft'>Comentario</td>"
            strHtml += "    </tr>"
            Dim clsDispatcherListado As New clsDispatcherListado()
            For Each clsDispatcher As clsDispatcher In clsDispatcherListado.ObtenerDispatcher(Me.intDocEntry)
                strStyle = ""
                If (Me.strCodigo.Trim = clsDispatcher.DisCodigo.Trim) And ((Me.chrTipoAutorizador.Trim = "0" And fnc.ObtenerAutorizadorNombre(Me.strAutorizador.Trim) = clsDispatcher.DisOficial.Trim) Or (Me.chrTipoAutorizador.Trim = "1" And fnc.ObtenerAutorizadorNombre(Me.strAutorizador.Trim) = clsDispatcher.DisBackup.Trim)) Then
                    strStyle = "style='font-weight:bold;'"
                    If (clsDispatcher.DisEstado.Trim <> "PENDIENTE") Then
                        btnAceptar.Enabled = False
                        lblMensaje.Text = "Pedido fue " + clsDispatcher.DisEstado.Trim.ToLower + "."
                    End If
                End If
                strHtml += "<tr class='trTituloDetalle' " + strStyle + ">"
                strHtml += "    <td class='tdLeft'>" + clsDispatcher.DisOficial + "</td>"
                strHtml += "    <td class='tdLeft'>" + clsDispatcher.DisCargo + "</td>"
                strHtml += "    <td class='tdCenter'>" + clsDispatcher.DisTipo + "</td>"
                strHtml += "    <td class='tdCenter'>" + clsDispatcher.DisEstado + "</td>"
                strHtml += "    <td class='tdLeft'>" + clsDispatcher.DisBackup + "</td>"
                strHtml += "    <td class='tdCenter'>" + clsDispatcher.DisFechaCreacion + "</td>"
                strHtml += "    <td class='tdCenter'>" + clsDispatcher.DisFechaActualizacion + "</td>"
                strHtml += "    <td class='tdLeft'>" + clsDispatcher.DisComentario + "</td>"
                strHtml += "</tr>"
            Next
            strHtml += "</table>"
        Catch ex As Exception
            strHtml = ""
            ToLog.write(ex.Message, "E")
        End Try
        Return strHtml
    End Function
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Dim blnRespuesta As Boolean = False
        Dim intBloqueaGD As Integer = 0
        If (txtComentario.Value.Trim <> "") Then
            If (ActualizarDispatcher()) Then
                If (ActualizarAutorizaciones()) Then
                    btnAceptar.Enabled = False
                    strHtmlDispatcher = GenerarHTMLDispatcher()
                    blnRespuesta = New clsAutorizacionesListado().ActualizarEstadoBorradorVenta(Me.intDocEntry, intBloqueaGD)
                    If (blnRespuesta) Then
                        lblMensaje.Text = "Proceso de rechazo ha finalizado con exito."
                    Else
                        lblMensaje.Text = "Ha ocurrido un error al intentar rechazar el pedido."
                    End If
                Else
                    lblMensaje.Text = "Ha ocurrido un error al intentar actualizar autorizaciones."
                End If
            Else
                lblMensaje.Text = "Ha ocurrido un error al intentar actualizar dispatcher."
            End If
        Else
            lblMensaje.Text = "Se requiere que comente motivo por el cual rechaza la solicitud de pedido."
        End If
    End Sub
    Public Function ActualizarDispatcher() As Boolean
        Dim blnRespuesta As Boolean = False
        Dim strComentario As String = ""
        If (txtComentario.Value.Trim <> "") Then strComentario = Replace(txtComentario.Value, Chr(10), "")
        blnRespuesta = New clsDispatcherListado().ActualizarDispatcher(Me.strCodigo.Trim, Me.strAutorizador.Trim, CInt(Me.chrTipoAutorizador.Trim), Me.strConcepto.Trim, CInt(Me.chrAccion.Trim), strComentario.Trim)
        Return blnRespuesta
    End Function
    Public Function ActualizarAutorizaciones() As Boolean
        Dim blnRespuesta As Boolean = False
        Dim strComentario As String = ""
        If (txtComentario.Value.Trim <> "") Then strComentario = Replace(txtComentario.Value, Chr(10), "")
        blnRespuesta = New clsAutorizacionesListado().ActualizarAutorizaciones(CInt(Me.intDocEntry), Me.strConcepto.Trim, CInt(Me.chrAccion.Trim), strComentario.Trim, Me.strAutorizador.Trim)
        Return blnRespuesta
    End Function
End Class