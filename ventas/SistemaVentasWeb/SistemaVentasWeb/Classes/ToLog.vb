Imports System.IO
Imports System.Reflection
Imports NLog
<Serializable()>
Module ToLog
    Private Property file As String = "e:\logs\sistemaVentas.log"
    Public Sub write(ByVal message As String)
        Try
            Dim st As New StackTrace
            message = st.GetFrame(1).GetMethod.ReflectedType.Name + "." + st.GetFrame(1).GetMethod.Name + "| " + message
            Dim logger As Logger
            logger = LogManager.GetCurrentClassLogger()
            logger.Debug(message)
        Catch ex As Exception
            'ToLog.write(ex.Message, "E")
        End Try
    End Sub
    Public Sub write(ByVal message As String, ByVal tipo As String)
        Try
            Dim st As New StackTrace
            message = st.GetFrame(1).GetMethod.ReflectedType.Name + "." + st.GetFrame(1).GetMethod.Name + "| " + message
            tipo = tipo.ToUpper
            Dim logger As Logger
            logger = LogManager.GetCurrentClassLogger()
            If tipo = "T" Then
                logger.Trace(message)
            ElseIf tipo = "D" Then
                logger.Debug(message)
            ElseIf tipo = "I" Then
                logger.Info(message)
            ElseIf tipo = "W" Then
                logger.Warn(message)
            ElseIf tipo = "E" Then
                logger.Error(message)
            ElseIf tipo = "F" Then
                logger.Fatal(message)
            End If
        Catch ex As Exception
            'ToLog.write(ex.Message, "E")
        End Try
    End Sub
End Module