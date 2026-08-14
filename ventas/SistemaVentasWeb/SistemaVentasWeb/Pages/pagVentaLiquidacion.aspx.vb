'Imports System.Globalization
'Imports iTextSharp
Imports iTextSharp.text
'Imports iTextSharp.text.pdf
'Imports iTextSharp.text.Image
Imports System.IO
Public Class pagVentaLiquidacion
    Inherits System.Web.UI.Page
    Dim fnc As clsFuncion = New clsFuncion()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (IsPostBack) Then
            txtCotizacion.Text = Request.QueryString("prmCotizacion")
        End If
    End Sub
    Protected Sub btnCrearVoucher_Click(sender As Object, e As EventArgs) Handles btnCrearVoucher.Click
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-CL")
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
        Try
            Dim intTipo As Integer = CInt(hdnTipo.Value)
            Dim intCodigo As Integer = CInt(hdnCodigo.Value)
            Dim objDocumento As New Document(PageSize.LETTER, 0, 0, 0, 0)
            Dim strNombreArchivo As String = fnc.CifrarMD5(CStr(System.DateTime.Now.Millisecond)) + ".pdf"
            Dim strRutaImagen As String = Server.MapPath("../Images")
            pdf.PdfWriter.GetInstance(objDocumento, New FileStream(Path.GetTempPath() + strNombreArchivo, FileMode.Create))
            objDocumento.Open()
            mdlVoucherVenta.GenerarPDFVenta(intTipo, intCodigo, strRutaImagen, objDocumento)
            objDocumento.Close()
            MostrarPDF(strNombreArchivo, Path.GetTempPath())
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
    End Sub
    Public Sub MostrarPDF(ByVal strNombreArchivo As String, ByVal strRuta As String)
        Response.ClearContent()
        Response.ClearHeaders()
        Response.ContentType = "application/pdf"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + strNombreArchivo)
        Response.WriteFile(strRuta + strNombreArchivo)
        Response.Flush()
        Response.Clear()
    End Sub
End Class