Imports System.Data.SqlClient
Public Class clsFuncion
    Dim sqlAux As String
    Dim cnxAux As SqlConnection
    Dim cmdAux As SqlCommand
    Dim rdrAux As SqlDataReader
    Public Function ObtenerConeccion() As String
        Dim strModalidad As String = ""
        Dim strServer As String = ""
        Dim strData As String = ""
        Dim strUser As String = ""
        Dim strPassword As String = ""
        Dim strConeccion As String = ""
        strModalidad = My.Resources.rscSistemaVentasWeb.zModalidad
        If (strModalidad = "P") Then
            strServer = My.Resources.rscSistemaVentasWeb.ServerPRD
            strData = My.Resources.rscSistemaVentasWeb.DataPRD
            strUser = My.Resources.rscSistemaVentasWeb.UserPRD
            strPassword = My.Resources.rscSistemaVentasWeb.PasswordPRD
        Else
            strServer = My.Resources.rscSistemaVentasWeb.ServerTST
            strData = My.Resources.rscSistemaVentasWeb.DataTST
            strUser = My.Resources.rscSistemaVentasWeb.UserTST
            strPassword = My.Resources.rscSistemaVentasWeb.PasswordTST
        End If
        strConeccion = "Data Source=" + strServer + ";Database=" + strData + ";User ID=" + strUser + "; Password=" + strPassword + "; MultipleActiveResultSets=True"
        Return strConeccion
    End Function
    Public Function ObtenerNombreMes(ByVal intMes As String) As String
        Dim strNombreMes As String
        Dim strMes As String = "ENERO|FEBRERO|MARZO|ABRIL|MAYO|JUNIO|JULIO|AGOSTO|SEPTIEMBRE|OCTUBRE|NOVIEMBRE|DICIEMBRE"
        Dim arrMes As String()
        arrMes = Split(strMes, "|")
        strNombreMes = arrMes(intMes - 1)
        ObtenerNombreMes = strNombreMes
    End Function
    Public Function ObtenerNombreMesCorto(ByVal intMes As String) As String
        Dim strNombreMes As String
        Dim strMes As String = "Ene|Feb|Mar|Abr|May|Jun|Jul|Ago|Sep|Oct|Nov|Dic"
        Dim arrMes As String()
        arrMes = Split(strMes, "|")
        strNombreMes = arrMes(intMes - 1)
        ObtenerNombreMesCorto = strNombreMes
    End Function
    Public Function CifrarMD5(ByVal strData As String) As String
        Dim objMD5 As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim arrData() As Byte
        Dim arrHash() As Byte
        arrData = System.Text.Encoding.UTF8.GetBytes(strData)
        arrHash = objMD5.ComputeHash(arrData)
        objMD5 = Nothing
        Return ObtenerCifradoMD5(arrHash)
    End Function
    Private Function ObtenerCifradoMD5(ByVal arrInput() As Byte) As String
        Dim strOutput As New System.Text.StringBuilder(arrInput.Length)
        For i As Integer = 0 To arrInput.Length - 1
            strOutput.Append(arrInput(i).ToString("X2"))
        Next
        Return strOutput.ToString().ToLower
    End Function
    Public Function ObtenerUsuarioSap(ByVal intUsuario As Integer) As Tuple(Of String, String)
        Dim strSapUser As String = ""
        Dim strSapPassword As String = ""
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "exec tai_vw_sp2_select_usuario_sap 10," + intUsuario.ToString
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                strSapUser = rdrAux.Item("SapUser")
                strSapPassword = rdrAux.Item("SapPassword")
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return New Tuple(Of String, String)(strSapUser, strSapPassword)
    End Function
    Public Function ObtenerAutorizadorCargo(ByVal strAutorizador As String) As String
        Dim strRetorno As String = ""
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "exec tai_vw_sp2_select_autorizador 10,'" + strAutorizador.Trim + "'"
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                strRetorno = rdrAux.Item("AutCargo").ToString.ToUpper
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return strRetorno
    End Function
    Public Function ObtenerAutorizadorNombre(ByVal strAutorizador As String) As String
        Dim strRetorno As String = ""
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "exec tai_vw_sp2_select_autorizador 10,'" + strAutorizador.Trim + "'"
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                strRetorno = rdrAux.Item("AutNombre").ToString.ToUpper
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return strRetorno
    End Function
    Public Function ObtenerAutorizadorCorreo(ByVal strAutorizador As String) As String
        Dim strRetorno As String = ""
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "exec tai_vw_sp2_select_autorizador 10,'" + strAutorizador.Trim + "'"
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                strRetorno = rdrAux.Item("AutCorreo").ToString.ToLower
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return strRetorno
    End Function
    Public Function ObtenerTasaInteresProducto(ByVal strTipoTasa As String) As Double
        Dim dblTasaInteres As Double = 0.0
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "exec tai_vw_sp2_select_tasa_interes_producto 10,'" + strTipoTasa.Trim + "'"
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                dblTasaInteres = rdrAux.Item("TasInteres")
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return dblTasaInteres
    End Function
    Public Function ObtenerDescuentoMaximoProducto(ByVal strBodega As String, ByVal strProducto As String, ByVal strFechaOrdenVenta As String) As Double
        Dim dblDtoMaximo As Double = 0.0
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "exec tai_vw_sp2_select_descuento_maximo_producto 10,'" + strBodega.Trim + "','" + strProducto.Trim + "','" + strFechaOrdenVenta.Trim + "'"
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                dblDtoMaximo = rdrAux.Item("DtoMaximo")
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return dblDtoMaximo
    End Function
    Public Function ObtenerCuentaContable(ByVal strBodega As String) As String
        Dim strCuentaContable As String = ""
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "exec tai_vw_sp2_select_cuenta_contable 10,'" + strBodega.Trim + "'"
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                strCuentaContable = rdrAux.Item("CtaCodigo")
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return strCuentaContable
    End Function
    Public Function ObtenerDocNumConDocEntry(ByRef intEntrada As Integer, ByRef strTabla As String) As String
        Dim intDocNum As Integer = 0
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "select DocNum from " + strTabla + " where DocEntry = " + CStr(intEntrada)
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                intDocNum = rdrAux.Item("DocNum")
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return intDocNum
    End Function
    Public Function ObtenerDocNumConDraftKey(ByRef intEntrada As Integer, ByRef strTabla As String) As String
        Dim intDocNum As Integer = 0
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "select DocNum from " + strTabla + " where DraftKey = " + CStr(intEntrada)
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                intDocNum = rdrAux.Item("DocNum")
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return intDocNum
    End Function
    Public Function ObtenerFolioFactura(ByRef intEntrada As Integer) As Integer
        Dim intFolio As Integer = 0
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "select isnull(FolioNum,0) as FolioNum from oinv where DocEntry=" + CStr(intEntrada)
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                intFolio = rdrAux.Item("FolioNum")
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return intFolio
    End Function
    Public Function ObtenerCodigoEmpleadoPorCodigoOperador(ByRef intEntrada As Integer) As Integer
        Dim intEmpleado As Integer = 0
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "select isnull(empID,0) as empID from ohem where salesPrson = " + CStr(intEntrada)
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                intEmpleado = rdrAux.Item("empID")
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return intEmpleado
    End Function
    Public Function ObtenerRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Static Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function
    Public Function ObtenerProveedorNombre(ByVal strCodigoProveedor As String) As String
        Dim strRetorno As String = ""
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "exec tai_vw_sp2_select_proveedor 20,'" + strCodigoProveedor.Trim + "'"
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                strRetorno = rdrAux.Item("ProNombre").ToString.ToUpper
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return strRetorno
    End Function
    Public Function ObtenerCodigoSerie(ByVal strObjeto As String, ByVal strSubObjeto As String, ByVal intUsuario As Integer) As Integer
        Dim intSerie As Integer = 0
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "exec tai_vw_sp2_select_serie 10,'" + strObjeto.Trim + "','" + strSubObjeto.Trim + "'," + intUsuario.ToString.Trim
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                intSerie = rdrAux.Item("SerCodigo")
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return intSerie
    End Function
    Public Function ObtenerCodigoEmpleadoPorCodigoUsuario(ByRef intEntrada As Integer) As Integer
        Dim intEmpleado As Integer = 0
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "select isnull(empID,0) as empID from ohem where userid = " + CStr(intEntrada)
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                intEmpleado = rdrAux.Item("empID")
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return intEmpleado
    End Function
    Public Function ObtenerDocEntryConDocNum(ByRef intDocNum As Integer, ByRef strTabla As String) As String
        Dim intDocEntry As Integer = 0
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "select DocEntry from " + strTabla + " where DocNum = " + CStr(intDocNum)
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                intDocEntry = rdrAux.Item("DocEntry")
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return intDocEntry
    End Function
    Public Function ObtenerCodigoPlazoPagoOC(ByRef intPlazoPagoOC As Integer) As Integer
        Dim intCodigoPlazoCompra As Integer = 0
        Try
            Dim strConeccion As String = ObtenerConeccion()
            cnxAux = New SqlConnection(strConeccion)
            cnxAux.Open()
            sqlAux = "select top 1 FldValue from UFD1 where TableID='OPOR' and FieldID=59 and substring(Descr,1,3)=" + CStr(intPlazoPagoOC)
            cmdAux = New SqlCommand(sqlAux, cnxAux)
            cmdAux.CommandTimeout = 600
            rdrAux = cmdAux.ExecuteReader()
            If rdrAux.Read Then
                intCodigoPlazoCompra = rdrAux.Item("FldValue")
            End If
            rdrAux.Close()
            cmdAux.Dispose()
            cnxAux.Close()
            cnxAux.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return intCodigoPlazoCompra
    End Function
End Class