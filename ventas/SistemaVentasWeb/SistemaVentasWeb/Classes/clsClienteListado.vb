Imports System.Data.SqlClient
<Serializable()>
Public Class clsClienteListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerCliente(ByVal strCliente As String)
        Dim ClienteLista As New List(Of clsCliente)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_cliente 10,'" + strCliente.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Cliente As New clsCliente()
                Cliente.CliCodigo = rdr.Item("CliCodigo")
                Cliente.CliNombre = rdr.Item("CliNombre")
                Cliente.CliRut = rdr.Item("CliRut")
                Cliente.CliBloqueado = True
                If (rdr.Item("CliBloqueado") = "N") Then Cliente.CliBloqueado = False

                Cliente.CliTelefono = rdr.Item("CliTelefono")
                Cliente.CliCorreo = rdr.Item("CliCorreo")
                Cliente.CliGiro = rdr.Item("CliGiro")

                Cliente.CatCodigo = rdr.Item("CatCodigo")
                Cliente.CatNombre = rdr.Item("CatNombre")
                Cliente.CliPlazoVentaCodigo = rdr.Item("CliPlazoVentaCodigo")
                Cliente.CliPlazoVentaNombre = rdr.Item("CliPlazoVentaNombre")

                Cliente.GruNombre = rdr.Item("GruNombre")

                ClienteLista.Add(Cliente)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return ClienteLista
    End Function
    Public Function ObtenerClienteLineaCredito(ByVal strCliente As String)
        Dim ClienteLista As New List(Of clsCliente)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_cliente 20, '" + strCliente.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Cliente As New clsCliente()
                Cliente.CliAcuerdo = rdr.Item("CliAcuerdo")
                Cliente.CliAutorizado = rdr.Item("CliAutorizado")
                Cliente.CliUtilizado = rdr.Item("CliUtilizado")
                Cliente.CliDisponible = rdr.Item("CliDisponible")
                ClienteLista.Add(Cliente)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return ClienteLista
    End Function
    Public Function ObtenerClienteVendedorAsociado(ByVal strCliente As String)
        Dim ClienteLista As New List(Of clsCliente)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_cliente 30, '" + strCliente.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Cliente As New clsCliente()
                Cliente.VenCodigo = rdr.Item("VenCodigo")
                Cliente.VenNombre = rdr.Item("VenNombre")
                ClienteLista.Add(Cliente)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return ClienteLista
    End Function
    Public Function ObtenerClienteDireccion(ByVal intOpcion As Integer, ByVal strCliente As String)
        Dim ClienteLista As New List(Of clsCliente)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_cliente " + intOpcion.ToString + ",'" + strCliente.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Cliente As New clsCliente()
                Cliente.DirTipo = rdr.Item("DirTipo")
                Cliente.DirCalle = rdr.Item("DirCalle")
                Cliente.DirComuna = rdr.Item("DirComuna")
                Cliente.DirCiudad = rdr.Item("DirCiudad")
                Cliente.DirRegion = rdr.Item("DirRegion")
                Cliente.DirPais = rdr.Item("DirPais")
                Cliente.DirCompleta = rdr.Item("DirCompleta")
                ClienteLista.Add(Cliente)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return ClienteLista
    End Function
    Function RegistrarDireccion(ByVal direccion As clsCliente) As String
        Dim intStatus As Integer = -100000
        Dim strStatus As String = "Error al crear direccion."
        Dim strRespuesta As String = "-200"
        Try
            Dim strRetorno As Tuple(Of String, String) = fnc.ObtenerUsuarioSap(direccion.UsuCodigo)
            Dim strSAPUsername As String = strRetorno.Item1.Trim
            Dim strSAPPassword As String = strRetorno.Item2.Trim
            Dim oCompany As New SAPbobsCOM.Company
            Dim intCodigoRetorno As Integer = 0
            oCompany.Server = If(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.ServerPRD, My.Resources.rscSistemaVentasWeb.ServerTST)
            oCompany.DbUserName = If(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.UserPRD, My.Resources.rscSistemaVentasWeb.UserTST)
            oCompany.DbPassword = If(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.PasswordPRD, My.Resources.rscSistemaVentasWeb.PasswordTST)
            oCompany.LicenseServer = My.Resources.rscSistemaVentasWeb.ServerLICENCIAS
            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016
            oCompany.UseTrusted = False
            oCompany.CompanyDB = If(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.DataPRD, My.Resources.rscSistemaVentasWeb.DataTST)
            oCompany.UserName = strSAPUsername
            oCompany.Password = strSAPPassword
            intCodigoRetorno = oCompany.Connect
            If intCodigoRetorno <> 0 Then
                oCompany.GetLastError(intStatus, strStatus)
                strRespuesta = CStr(intStatus) '-1
                ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
            Else
                Dim oBP As SAPbobsCOM.BusinessPartners
                oBP = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners)
                If oBP.GetByKey(direccion.CliCodigo) = True Then
                    oBP.Valid = SAPbobsCOM.BoYesNoEnum.tYES
                    oBP.Addresses.SetCurrentLine(0)
                    oBP.Addresses.Add()
                    If (direccion.Direccion = 1) Then
                        oBP.Addresses.AddressType = SAPbobsCOM.BoAddressType.bo_BillTo
                    Else
                        oBP.Addresses.AddressType = SAPbobsCOM.BoAddressType.bo_ShipTo
                    End If
                    oBP.Addresses.AddressName = direccion.DirTipo
                    oBP.Addresses.Street = direccion.DirCalle + " " + direccion.DirNumero
                    oBP.Addresses.City = direccion.DirCiudad
                    oBP.Addresses.County = direccion.DirComuna
                    oBP.Addresses.State = direccion.DirRegion
                    oBP.Addresses.Country = "CL"
                    oBP.Addresses.Add()
                    intCodigoRetorno = oBP.Update()
                    If intCodigoRetorno <> 0 Then
                        oCompany.GetLastError(intStatus, strStatus)
                        strRespuesta = CStr(intStatus)
                        ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
                    Else
                        strRespuesta = direccion.CliCodigo
                    End If
                End If
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBP)
                oBP = Nothing
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompany)
            oCompany = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return strRespuesta
    End Function
    Public Function ObtenerClienteFacturaImpaga(ByVal strCliente As String)
        Dim ClienteLista As New List(Of clsCliente)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_cliente 60, '" + strCliente.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Cliente As New clsCliente()
                Cliente.FacNumero = rdr.Item("DocNumero")
                Cliente.FacFecha = rdr.Item("DocFecha")
                Cliente.FacValor = rdr.Item("DocValor")
                ClienteLista.Add(Cliente)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return ClienteLista
    End Function
    Public Function ObtenerClienteChequeProtestado(ByVal strCliente As String)
        Dim ClienteLista As New List(Of clsCliente)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_cliente 70, '" + strCliente.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Cliente As New clsCliente()
                Cliente.CheNumero = rdr.Item("DocNumero")
                Cliente.CheFecha = rdr.Item("DocFecha")
                Cliente.CheValor = rdr.Item("DocValor")
                ClienteLista.Add(Cliente)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return ClienteLista
    End Function
    Function RegistrarCliente(ByVal intUsuario As Integer, ByVal intOperador As Integer, ByVal strRut As String, ByVal strDV As String, ByVal strNombre As String, ByVal strCorreo As String, ByVal intTelefono As String, ByVal strGiro As String, ByVal strMoneda As String, ByVal strCalle As String, ByVal strNumero As String, ByVal strComuna As String, ByVal strCiudad As String, ByVal intRegion As Integer) As String
        Dim intStatus As Integer = -100000
        Dim strStatus As String = "Error al crear cliente."
        Dim intRespuesta As Integer = -200
        Dim strRespuesta As String = ""
        Dim strRutFormateado As String = ""
        Try
            Dim strRetorno As Tuple(Of String, String) = fnc.ObtenerUsuarioSap(intUsuario)
            Dim strSAPUsername As String = strRetorno.Item1.Trim
            Dim strSAPPassword As String = strRetorno.Item2.Trim
            Dim oCompany As New SAPbobsCOM.Company
            Dim intCodigoRetorno As Integer = 0
            oCompany.Server = If(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.ServerPRD, My.Resources.rscSistemaVentasWeb.ServerTST)
            oCompany.DbUserName = If(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.UserPRD, My.Resources.rscSistemaVentasWeb.UserTST)
            oCompany.DbPassword = If(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.PasswordPRD, My.Resources.rscSistemaVentasWeb.PasswordTST)
            oCompany.LicenseServer = My.Resources.rscSistemaVentasWeb.ServerLICENCIAS
            ToLog.write(oCompany.Server + ", " + oCompany.DbUserName + ", " + oCompany.DbPassword, "D")
            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016
            oCompany.UseTrusted = False
            oCompany.CompanyDB = If(My.Resources.rscSistemaVentasWeb.zModalidad = "P", My.Resources.rscSistemaVentasWeb.DataPRD, My.Resources.rscSistemaVentasWeb.DataTST)
            oCompany.UserName = strSAPUsername
            oCompany.Password = strSAPPassword
            ToLog.write(oCompany.CompanyDB + ", " + oCompany.UserName + ", " + oCompany.Password, "D")
            intCodigoRetorno = oCompany.Connect
            If intCodigoRetorno <> 0 Then
                oCompany.GetLastError(intStatus, strStatus)
                intRespuesta = -200
                ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
            Else
                Dim oBP As SAPbobsCOM.BusinessPartners
                oBP = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners)
                If (Len(strRut.Trim) = 8) Then strRutFormateado = "0" + strRut.Trim
                If (Len(strRut.Trim) = 7) Then strRutFormateado = "00" + strRut.Trim
                oBP.CardCode = "C" + strRutFormateado.Trim
                oBP.CardName = strNombre.Trim.ToUpper
                oBP.CardType = SAPbobsCOM.BoCardTypes.cCustomer
                oBP.FederalTaxID = strRutFormateado.Trim + "-" + strDV.Trim.ToUpper
                oBP.Addresses.Street = strCalle.Trim.ToUpper + " " + strNumero
                oBP.Addresses.AddressType = SAPbobsCOM.BoAddressType.bo_BillTo
                oBP.Addresses.AddressName = "FACTURA"
                oBP.Addresses.Country = "CL"
                oBP.Addresses.City = strCiudad.Trim.ToUpper
                oBP.Addresses.County = strComuna.Trim.ToUpper
                oBP.Addresses.State = intRegion
                oBP.Addresses.Add()
                oBP.Addresses.Street = strCalle.Trim.ToUpper + " " + strNumero
                oBP.Addresses.AddressType = SAPbobsCOM.BoAddressType.bo_ShipTo
                oBP.Addresses.AddressName = "DESPACHO"
                oBP.Addresses.Country = "CL"
                oBP.Addresses.City = strCiudad.Trim.ToUpper
                oBP.Addresses.County = strComuna.Trim.ToUpper
                oBP.Addresses.State = intRegion
                oBP.Addresses.Add()
                oBP.Currency = strMoneda.Trim
                oBP.GroupCode = 100
                oBP.Phone1 = intTelefono
                oBP.EmailAddress = strCorreo.Trim.ToLower
                oBP.Notes = strGiro.Trim.ToUpper
                oBP.UserFields.Fields.Item("U_TAI_Categoria").Value = "D"
                oBP.SalesPersonCode = intOperador
                intCodigoRetorno = oBP.Add()
                If intCodigoRetorno <> 0 Then
                    oCompany.GetLastError(intStatus, strStatus)
                    If (intStatus = -10) Then
                        intRespuesta = -1
                    Else
                        intRespuesta = -100
                    End If
                    ToLog.write(CStr(intStatus) + ", " + strStatus, "E")
                Else
                    intRespuesta = 0
                End If
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBP)
                oBP = Nothing
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompany)
            oCompany = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Select Case intRespuesta
            Case 0
                strRespuesta = "Cliente creado satisfactoriamente."
            Case -1
                strRespuesta = "Cliente ya existe en base de datos."
            Case -100
                strRespuesta = "Error desconocido al crear cliente."
            Case -200
                strRespuesta = "Error al conectarse a SAP."
            Case Else
                strRespuesta = "Error desconocido de sistema."
        End Select
        Return strRespuesta
    End Function
End Class