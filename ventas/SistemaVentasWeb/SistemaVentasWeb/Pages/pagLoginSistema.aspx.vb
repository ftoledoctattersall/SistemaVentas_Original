Public Class pagLoginSistema
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (My.Resources.rscSistemaVentasWeb.zModalidad = "P") Then
            lblAmbiente.Text = "Ambiente Productivo - Ver " & System.Environment.Version.ToString()
        ElseIf (My.Resources.rscSistemaVentasWeb.zModalidad = "T") Then
            lblAmbiente.Text = "Ambiente Testing - Ver " & System.Environment.Version.ToString()
        Else
            lblAmbiente.Text = "Ambiente Sin Clasificar - Ver " & System.Environment.Version.ToString()
        End If
    End Sub

End Class