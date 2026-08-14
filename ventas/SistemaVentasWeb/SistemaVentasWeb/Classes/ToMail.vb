Imports System.Net.Mail
<Serializable()>
Public Class ToMail
    Public Property mensaje As MailMessage
    Sub New()
    End Sub
    Function EnviarCorreo() As Boolean
        Dim blnRetorno As Boolean = False
        Try
            Dim mailSender As New SmtpClient("smtpoffice365.tattersall.cl", 25)
            mailSender.Credentials = New Net.NetworkCredential("reportestai@tattersall.cl", "Xux04256")
            mailSender.EnableSsl = False
            mailSender.DeliveryMethod = SmtpDeliveryMethod.Network

            'Dim mailSender As New SmtpClient("outlook.office365.com", 25)
            'mailSender.Credentials = New System.Net.NetworkCredential("reportestai@tattersall.cl", "Xux04256")
            'mailSender.EnableSsl = True
            'mailSender.DeliveryMethod = SmtpDeliveryMethod.Network

            Try
                mailSender.Send(Me.mensaje)
                blnRetorno = True
            Catch ex As System.ArgumentNullException
                ToLog.write("Problema al enviar el correo, message is null: " + ex.Message, "E")
            Catch ex As System.Net.Mail.SmtpFailedRecipientsException
                ToLog.write("Problema al enviar el correo, The message could not be delivered to one or more of the recipients in System.Net.Mail.MailMessage.To, System.Net.Mail.MailMessage.CC, or System.Net.Mail.MailMessage.Bcc.: " + ex.Message, "E")
            Catch ex As System.ObjectDisposedException
                ToLog.write("Problema al enviar el correo, This object has been disposed: " + ex.Message, "E")
            Catch ex As System.Net.Mail.SmtpException
                ToLog.write("Problema al enviar el correo, The connection to the SMTP server failed.-or-Authentication failed.-or-The operation timed out.-or-System.Net.Mail.SmtpClient.EnableSsl is set to true but the System.Net.Mail.SmtpClient.DeliveryMethod property is set to System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory or System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis.-or-System.Net.Mail.SmtpClient.EnableSsl is set to true, but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command. : " + ex.Message, "E")
            Catch ex As System.InvalidOperationException
                ToLog.write("Problema al enviar el correo, This System.Net.Mail.SmtpClient has a Overload:System.Net.Mail.SmtpClient.SendAsync call in progress.-or- System.Net.Mail.MailMessage.From is null.-or- There are no recipients specified in System.Net.Mail.MailMessage.To, System.Net.Mail.MailMessage.CC, and System.Net.Mail.MailMessage.Bcc properties.-or- System.Net.Mail.SmtpClient.DeliveryMethod property is set to System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Host is null.-or-System.Net.Mail.SmtpClient.DeliveryMethod property is set to System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Host is equal to the empty string ("").-or- System.Net.Mail.SmtpClient.DeliveryMethod property is set to System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Port is zero, a negative number, or greater than 65,535.: " + ex.Message, "E")
            Catch ex As Exception
                ToLog.write(ex.Message, "E")
            End Try
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return blnRetorno
    End Function
End Class