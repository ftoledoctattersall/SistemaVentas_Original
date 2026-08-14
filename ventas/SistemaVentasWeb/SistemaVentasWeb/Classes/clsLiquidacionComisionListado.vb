Imports System.Data.SqlClient
<Serializable()>
Public Class clsLiquidacionComisionListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerLiquidacionComisionResumen(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String)
        Dim LiquidacionComisionResumenLista As New List(Of clsLiquidacionComisionResumen)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_liquidacion_comision 10," + intEmpleado.ToString + "," + intOperador.ToString + "," + intAño.ToString + "," + intMes.ToString + "," + intIdentificador.ToString + ",'" + strOficina.Trim + "'," + intLinea.ToString + ",'" + strSubLinea.Trim + "','" + strDocto.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim LiquidacionComisionResumen As New clsLiquidacionComisionResumen()
                LiquidacionComisionResumen.LiqIdentificador = rdr.Item("LiqIdentificador")
                LiquidacionComisionResumen.CodOperador = rdr.Item("CodOperador")
                LiquidacionComisionResumen.NomOperador = rdr.Item("NomOperador")
                LiquidacionComisionResumen.LiqAño = rdr.Item("LiqAño")
                LiquidacionComisionResumen.LiqMes = rdr.Item("LiqMes")
                LiquidacionComisionResumen.TotNeto = rdr.Item("TotNeto")
                LiquidacionComisionResumen.TotCosto = rdr.Item("TotCosto")
                LiquidacionComisionResumen.TotMargen = rdr.Item("TotMargen")
                LiquidacionComisionResumen.PorMargen = rdr.Item("PorMargen")
                LiquidacionComisionResumen.IndComision = rdr.Item("IndComision")
                LiquidacionComisionResumen.GruComision = rdr.Item("GruComision")
                LiquidacionComisionResumen.FinComision = rdr.Item("FinComision")
                LiquidacionComisionResumen.ProVacaciones = rdr.Item("ProVacaciones")
                LiquidacionComisionResumen.SemCorrida = rdr.Item("SemCorrida")
                LiquidacionComisionResumen.TotComision = rdr.Item("TotComision")
                LiquidacionComisionResumen.FecLiquidacion = rdr.Item("FecLiquidacion")
                LiquidacionComisionResumen.LiqOrigen = rdr.Item("LiqOrigen")
                LiquidacionComisionResumen.LiqTipo = rdr.Item("LiqTipo")
                LiquidacionComisionResumenLista.Add(LiquidacionComisionResumen)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return LiquidacionComisionResumenLista
    End Function

    Public Function ObtenerLiquidacionComisionDetalleLinea(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String)
        Dim LiquidacionComisionDetalleLista As New List(Of clsLiquidacionComisionDetalle)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_liquidacion_comision 20," + intEmpleado.ToString + "," + intOperador.ToString + "," + intAño.ToString + "," + intMes.ToString + "," + intIdentificador.ToString + ",'" + strOficina.Trim + "'," + intLinea.ToString + ",'" + strSubLinea.Trim + "','" + strDocto.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim LiquidacionComisionDetalle As New clsLiquidacionComisionDetalle()
                LiquidacionComisionDetalle.LiqIdentificador = rdr.Item("LiqIdentificador")
                LiquidacionComisionDetalle.LinCodigo = rdr.Item("LinCodigo")
                LiquidacionComisionDetalle.LinNombre = rdr.Item("LinNombre")
                LiquidacionComisionDetalle.SubNombre = rdr.Item("SubNombre")
                LiquidacionComisionDetalle.DocTipo = rdr.Item("DocTipo")
                LiquidacionComisionDetalle.TotNeto = rdr.Item("TotNeto")
                LiquidacionComisionDetalle.FinComision = rdr.Item("FinComision")
                LiquidacionComisionDetalleLista.Add(LiquidacionComisionDetalle)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return LiquidacionComisionDetalleLista
    End Function
    Public Function ObtenerLiquidacionComisionDetalleDocumento(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String)
        Dim LiquidacionComisionDetalleLista As New List(Of clsLiquidacionComisionDetalle)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_liquidacion_comision 21," + intEmpleado.ToString + "," + intOperador.ToString + "," + intAño.ToString + "," + intMes.ToString + "," + intIdentificador.ToString + ",'" + strOficina.Trim + "'," + intLinea.ToString + ",'" + strSubLinea.Trim + "','" + strDocto.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim LiquidacionComisionDetalle As New clsLiquidacionComisionDetalle()
                LiquidacionComisionDetalle.LiqIdentificador = rdr.Item("LiqIdentificador")
                LiquidacionComisionDetalle.LinCodigo = rdr.Item("LinCodigo")
                LiquidacionComisionDetalle.LinNombre = rdr.Item("LinNombre")
                LiquidacionComisionDetalle.SubNombre = rdr.Item("SubNombre")
                LiquidacionComisionDetalle.DocTipo = rdr.Item("DocTipo")
                LiquidacionComisionDetalle.TotNeto = rdr.Item("TotNeto")
                LiquidacionComisionDetalle.FinComision = rdr.Item("FinComision")
                LiquidacionComisionDetalle.OfiNombre = rdr.Item("OfiNombre")
                LiquidacionComisionDetalle.DocFecha = rdr.Item("DocFecha")
                LiquidacionComisionDetalle.FolNumero = rdr.Item("FolNumero")
                LiquidacionComisionDetalle.Doc4taCopia = rdr.Item("Doc4taCopia")
                LiquidacionComisionDetalle.CliNombre = rdr.Item("CliNombre")
                LiquidacionComisionDetalle.BodNombre = rdr.Item("BodNombre")
                LiquidacionComisionDetalle.ArtNombre = rdr.Item("ArtNombre")
                LiquidacionComisionDetalle.ArtMedida = rdr.Item("ArtMedida")
                LiquidacionComisionDetalle.ArtCantidad = rdr.Item("ArtCantidad")
                LiquidacionComisionDetalle.VenNeto = rdr.Item("VenNeto")
                LiquidacionComisionDetalle.VenMargen = rdr.Item("VenMargen")
                LiquidacionComisionDetalle.PorComision = rdr.Item("PorComision")
                LiquidacionComisionDetalle.BasComision = rdr.Item("BasComision")
                LiquidacionComisionDetalleLista.Add(LiquidacionComisionDetalle)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return LiquidacionComisionDetalleLista
    End Function
    Public Function ObtenerLiquidacionComisionDetalleLineaEspecial(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String)
        Dim LiquidacionComisionDetalleLista As New List(Of clsLiquidacionComisionDetalle)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_liquidacion_comision 30," + intEmpleado.ToString + "," + intOperador.ToString + "," + intAño.ToString + "," + intMes.ToString + "," + intIdentificador.ToString + ",'" + strOficina.Trim + "'," + intLinea.ToString + ",'" + strSubLinea.Trim + "','" + strDocto.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim LiquidacionComisionDetalle As New clsLiquidacionComisionDetalle()
                LiquidacionComisionDetalle.LiqIdentificador = rdr.Item("LiqIdentificador")
                LiquidacionComisionDetalle.LinCodigo = rdr.Item("LinCodigo")
                LiquidacionComisionDetalle.LinNombre = rdr.Item("LinNombre")
                LiquidacionComisionDetalle.OfiCodigo = rdr.Item("OfiCodigo")
                LiquidacionComisionDetalle.OfiNombre = rdr.Item("OfiNombre")
                LiquidacionComisionDetalle.TotNeto = rdr.Item("TotNeto")
                LiquidacionComisionDetalle.TotMargen = rdr.Item("TotMargen")
                LiquidacionComisionDetalle.PorComision = rdr.Item("PorComision")
                LiquidacionComisionDetalle.BasComision = rdr.Item("BasComision")
                LiquidacionComisionDetalle.NivComision = rdr.Item("NivComision")
                LiquidacionComisionDetalle.FinComision = rdr.Item("FinComision")
                LiquidacionComisionDetalleLista.Add(LiquidacionComisionDetalle)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return LiquidacionComisionDetalleLista
    End Function
    Public Function ObtenerLiquidacionComisionDetalleDocumentoEspecial(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String)
        Dim LiquidacionComisionDetalleLista As New List(Of clsLiquidacionComisionDetalle)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_liquidacion_comision 31," + intEmpleado.ToString + "," + intOperador.ToString + "," + intAño.ToString + "," + intMes.ToString + "," + intIdentificador.ToString + ",'" + strOficina.Trim + "'," + intLinea.ToString + ",'" + strSubLinea.Trim + "','" + strDocto.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim LiquidacionComisionDetalle As New clsLiquidacionComisionDetalle()
                LiquidacionComisionDetalle.LiqIdentificador = rdr.Item("LiqIdentificador")
                LiquidacionComisionDetalle.LinCodigo = rdr.Item("LinCodigo")
                LiquidacionComisionDetalle.LinNombre = rdr.Item("LinNombre")
                LiquidacionComisionDetalle.SubNombre = rdr.Item("SubNombre")
                LiquidacionComisionDetalle.DocTipo = rdr.Item("DocTipo")
                LiquidacionComisionDetalle.TotNeto = rdr.Item("TotNeto")
                LiquidacionComisionDetalle.FinComision = rdr.Item("FinComision")
                LiquidacionComisionDetalle.OfiNombre = rdr.Item("OfiNombre")
                LiquidacionComisionDetalle.DocFecha = rdr.Item("DocFecha")
                LiquidacionComisionDetalle.FolNumero = rdr.Item("FolNumero")
                LiquidacionComisionDetalle.Doc4taCopia = IIf(IsDBNull(rdr.Item("Doc4taCopia")), "----------", rdr.Item("Doc4taCopia"))
                LiquidacionComisionDetalle.CliNombre = rdr.Item("CliNombre")
                LiquidacionComisionDetalle.BodNombre = rdr.Item("BodNombre")
                LiquidacionComisionDetalle.ArtNombre = rdr.Item("ArtNombre")
                LiquidacionComisionDetalle.ArtMedida = rdr.Item("ArtMedida")
                LiquidacionComisionDetalle.ArtCantidad = rdr.Item("ArtCantidad")
                LiquidacionComisionDetalle.VenNeto = rdr.Item("VenNeto")
                LiquidacionComisionDetalle.VenMargen = rdr.Item("VenMargen")
                LiquidacionComisionDetalle.PorComision = rdr.Item("PorComision")
                LiquidacionComisionDetalle.BasComision = rdr.Item("BasComision")
                LiquidacionComisionDetalleLista.Add(LiquidacionComisionDetalle)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return LiquidacionComisionDetalleLista
    End Function

    Public Function ValidarUsuarioComisionProvisorio(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String) As String
        'Dim LiquidacionComisionDetalleLista As New List(Of clsLiquidacionComisionDetalle)
        Dim blnUsuarioEspecial As String = "False"
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_liquidacion_comision 11," + intEmpleado.ToString + "," + intOperador.ToString + "," + intAño.ToString + "," + intMes.ToString + "," + intIdentificador.ToString + ",'" + strOficina.Trim + "'," + intLinea.ToString + ",'" + strSubLinea.Trim + "','" + strDocto.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            If (rdr.HasRows) Then
                If (rdr.Read) Then
                    blnUsuarioEspecial = "True"
                End If
            Else
                blnUsuarioEspecial = "False"
            End If
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return blnUsuarioEspecial
    End Function

    Public Function ObtenerComisionDetalleProvisorioLinea(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String)
        Dim LiquidacionComisionDetalleLista As New List(Of clsLiquidacionComisionDetalle)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_liquidacion_comision 40," + intEmpleado.ToString + "," + intOperador.ToString + "," + intAño.ToString + "," + intMes.ToString + "," + intIdentificador.ToString + ",'" + strOficina.Trim + "'," + intLinea.ToString + ",'" + strSubLinea.Trim + "','" + strDocto.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim LiquidacionComisionDetalle As New clsLiquidacionComisionDetalle()
                LiquidacionComisionDetalle.LiqIdentificador = rdr.Item("LiqIdentificador")
                LiquidacionComisionDetalle.LinCodigo = rdr.Item("LinCodigo")
                LiquidacionComisionDetalle.LinNombre = rdr.Item("LinNombre")
                LiquidacionComisionDetalle.SubNombre = rdr.Item("SubNombre")
                LiquidacionComisionDetalle.DocTipo = rdr.Item("DocTipo")
                LiquidacionComisionDetalle.TotNeto = rdr.Item("TotNeto")
                LiquidacionComisionDetalle.FinComision = rdr.Item("FinComision")
                LiquidacionComisionDetalleLista.Add(LiquidacionComisionDetalle)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return LiquidacionComisionDetalleLista
    End Function
    Public Function ObtenerComisionDetalleProvisorioDocumento(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String)
        Dim LiquidacionComisionDetalleLista As New List(Of clsLiquidacionComisionDetalle)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_liquidacion_comision 41," + intEmpleado.ToString + "," + intOperador.ToString + "," + intAño.ToString + "," + intMes.ToString + "," + intIdentificador.ToString + ",'" + strOficina.Trim + "'," + intLinea.ToString + ",'" + strSubLinea.Trim + "','" + strDocto.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim LiquidacionComisionDetalle As New clsLiquidacionComisionDetalle()
                LiquidacionComisionDetalle.LiqIdentificador = rdr.Item("LiqIdentificador")
                LiquidacionComisionDetalle.LinCodigo = rdr.Item("LinCodigo")
                LiquidacionComisionDetalle.LinNombre = rdr.Item("LinNombre")
                LiquidacionComisionDetalle.SubNombre = rdr.Item("SubNombre")
                LiquidacionComisionDetalle.DocTipo = rdr.Item("DocTipo")
                LiquidacionComisionDetalle.TotNeto = rdr.Item("TotNeto")
                LiquidacionComisionDetalle.FinComision = rdr.Item("FinComision")
                LiquidacionComisionDetalle.OfiNombre = rdr.Item("OfiNombre")
                LiquidacionComisionDetalle.DocFecha = rdr.Item("DocFecha")
                LiquidacionComisionDetalle.FolNumero = rdr.Item("FolNumero")
                LiquidacionComisionDetalle.Doc4taCopia = IIf(IsDBNull(rdr.Item("Doc4taCopia")), "----------", rdr.Item("Doc4taCopia"))
                LiquidacionComisionDetalle.CliNombre = rdr.Item("CliNombre")
                LiquidacionComisionDetalle.BodNombre = rdr.Item("BodNombre")
                LiquidacionComisionDetalle.ArtNombre = rdr.Item("ArtNombre")
                LiquidacionComisionDetalle.ArtMedida = rdr.Item("ArtMedida")
                LiquidacionComisionDetalle.ArtCantidad = rdr.Item("ArtCantidad")
                LiquidacionComisionDetalle.VenNeto = rdr.Item("VenNeto")
                LiquidacionComisionDetalle.VenMargen = rdr.Item("VenMargen")
                LiquidacionComisionDetalle.PorComision = rdr.Item("PorComision")
                LiquidacionComisionDetalle.BasComision = rdr.Item("BasComision")
                LiquidacionComisionDetalleLista.Add(LiquidacionComisionDetalle)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return LiquidacionComisionDetalleLista
    End Function
    Public Function ObtenerComisionDetalleProvisorioLineaEspecial(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String)
        Dim LiquidacionComisionDetalleLista As New List(Of clsLiquidacionComisionDetalle)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_liquidacion_comision 50," + intEmpleado.ToString + "," + intOperador.ToString + "," + intAño.ToString + "," + intMes.ToString + "," + intIdentificador.ToString + ",'" + strOficina.Trim + "'," + intLinea.ToString + ",'" + strSubLinea.Trim + "','" + strDocto.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim LiquidacionComisionDetalle As New clsLiquidacionComisionDetalle()
                LiquidacionComisionDetalle.LiqIdentificador = rdr.Item("LiqIdentificador")
                LiquidacionComisionDetalle.LinCodigo = rdr.Item("LinCodigo")
                LiquidacionComisionDetalle.LinNombre = rdr.Item("LinNombre")
                LiquidacionComisionDetalle.OfiCodigo = rdr.Item("OfiCodigo")
                LiquidacionComisionDetalle.OfiNombre = rdr.Item("OfiNombre")
                LiquidacionComisionDetalle.TotNeto = rdr.Item("TotNeto")
                LiquidacionComisionDetalle.TotMargen = rdr.Item("TotMargen")
                LiquidacionComisionDetalle.PorComision = rdr.Item("PorComision")
                LiquidacionComisionDetalle.BasComision = rdr.Item("BasComision")
                LiquidacionComisionDetalle.NivComision = rdr.Item("NivComision")
                LiquidacionComisionDetalle.FinComision = rdr.Item("FinComision")
                LiquidacionComisionDetalleLista.Add(LiquidacionComisionDetalle)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return LiquidacionComisionDetalleLista
    End Function
    Public Function ObtenerComisionDetalleProvisorioDocumentoEspecial(ByVal intEmpleado As Integer, ByVal intOperador As Integer, ByVal intAño As Integer, ByVal intMes As Integer, ByVal intIdentificador As Integer, ByVal strOficina As String, ByVal intLinea As Integer, ByVal strSubLinea As String, ByVal strDocto As String)
        Dim LiquidacionComisionDetalleLista As New List(Of clsLiquidacionComisionDetalle)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_liquidacion_comision 51," + intEmpleado.ToString + "," + intOperador.ToString + "," + intAño.ToString + "," + intMes.ToString + "," + intIdentificador.ToString + ",'" + strOficina.Trim + "'," + intLinea.ToString + ",'" + strSubLinea.Trim + "','" + strDocto.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim LiquidacionComisionDetalle As New clsLiquidacionComisionDetalle()
                LiquidacionComisionDetalle.LiqIdentificador = rdr.Item("LiqIdentificador")
                LiquidacionComisionDetalle.LinCodigo = rdr.Item("LinCodigo")
                LiquidacionComisionDetalle.LinNombre = rdr.Item("LinNombre")
                LiquidacionComisionDetalle.SubNombre = rdr.Item("SubNombre")
                LiquidacionComisionDetalle.DocTipo = rdr.Item("DocTipo")
                LiquidacionComisionDetalle.TotNeto = rdr.Item("TotNeto")
                LiquidacionComisionDetalle.FinComision = rdr.Item("FinComision")
                LiquidacionComisionDetalle.OfiNombre = rdr.Item("OfiNombre")
                LiquidacionComisionDetalle.DocFecha = rdr.Item("DocFecha")
                LiquidacionComisionDetalle.FolNumero = rdr.Item("FolNumero")
                LiquidacionComisionDetalle.Doc4taCopia = IIf(IsDBNull(rdr.Item("Doc4taCopia")), "----------", rdr.Item("Doc4taCopia"))
                LiquidacionComisionDetalle.CliNombre = rdr.Item("CliNombre")
                LiquidacionComisionDetalle.BodNombre = rdr.Item("BodNombre")
                LiquidacionComisionDetalle.ArtNombre = rdr.Item("ArtNombre")
                LiquidacionComisionDetalle.ArtMedida = rdr.Item("ArtMedida")
                LiquidacionComisionDetalle.ArtCantidad = rdr.Item("ArtCantidad")
                LiquidacionComisionDetalle.VenNeto = rdr.Item("VenNeto")
                LiquidacionComisionDetalle.VenMargen = rdr.Item("VenMargen")
                LiquidacionComisionDetalle.PorComision = rdr.Item("PorComision")
                LiquidacionComisionDetalle.BasComision = rdr.Item("BasComision")
                LiquidacionComisionDetalleLista.Add(LiquidacionComisionDetalle)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return LiquidacionComisionDetalleLista
    End Function
End Class