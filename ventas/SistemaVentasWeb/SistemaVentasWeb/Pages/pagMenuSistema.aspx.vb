Public Class pagMenuSistema
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (IsPostBack) Then
            hdnUser.Value = Request.Form("hdnUser")
            hdnPassword.Value = Request.Form("hdnPassword")

            hdnEmpleado.Value = Request.Form("hdnEmpleado")
            hdnUsuario.Value = Request.Form("hdnUsuario")
            hdnOperador.Value = Request.Form("hdnOperador")
            hdnOficina.Value = Request.Form("hdnOficina")

            hdnNivel.Value = Request.Form("hdnNivel")
            hdnRol.Value = Request.Form("hdnRol")
            hdnActivo.Value = Request.Form("hdnActivo")

            hdnPerfil.Value = Request.Form("hdnPerfil")

            hdnCotizador.Value = Request.Form("hdnCotizador")

            hdnDolar.Value = Request.Form("hdnDolar")
            hdnEuro.Value = Request.Form("hdnEuro")
        End If
    End Sub
End Class