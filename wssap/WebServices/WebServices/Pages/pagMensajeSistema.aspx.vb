Public Class pagMensajeSistema
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ToLog.write("dentro del aspx.vb", "D")
        Catch ex As Exception
            ToLog.write(ex.Source.ToString + "----" + ex.TargetSite.ToString + "----" + ex.Message, "E")
        End Try
    End Sub
End Class