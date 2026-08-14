Imports System.Data.SqlClient
<Serializable()>
Public Class clsBodegaListado
    Dim fnc As clsFuncion = New clsFuncion()
    Dim sql As String
    Dim cnx As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public Function ObtenerBodega(ByVal strOperador As String)
        Dim BodegaLista As New List(Of clsBodega)
        Try
            Dim strConeccion As String = fnc.ObtenerConeccion()
            cnx = New SqlConnection(strConeccion)
            cnx.Open()
            sql = "exec tai_vw_sp2_select_bodega 10,'" + strOperador.Trim + "'"
            cmd = New SqlCommand(sql, cnx)
            cmd.CommandTimeout = 600
            rdr = cmd.ExecuteReader()
            While rdr.Read
                Dim Bodega As New clsBodega()
                Bodega.BodCodigo = rdr.Item("BodCodigo")
                Bodega.BodNombre = rdr.Item("BodNombre")
                Bodega.BodDefecto = rdr.Item("BodDefecto")
                Bodega.BodTipoVenta = "VentaBodegaPropia"


                If (strOperador.Trim.ToLower = "amozo" Or strOperador.Trim.ToLower = "machondo" Or strOperador.Trim.ToLower = "mundurra") Then


                    If (Bodega.BodCodigo = "GAKAMPRT" Or Bodega.BodCodigo = "GATRTKAM") Then
                        Bodega.BodTipoVenta = "VentaBodegaPropia"
                    ElseIf (Bodega.BodCodigo = "GAKAMCOT" Or Bodega.BodCodigo = "GAKAMTRT") Then
                        Bodega.BodTipoVenta = "VentaConsignada"
                    ElseIf (Bodega.BodCodigo = "GAPROTRT") Then
                        Bodega.BodTipoVenta = "VentaCalzadaProveedor"
                    ElseIf (Bodega.BodCodigo = "GAKAMPFT") Then
                        Bodega.BodTipoVenta = "VentaPuestoFundo"
                    End If


                    If (rdr.Item("BodConsignado") = "Y") Then
                        Bodega.BodTipoVenta = "VentaConsignada"
                    ElseIf (InStr(Bodega.BodCodigo, "TRT") > 0 And Bodega.BodCodigo <> "GATRTKAM") Then
                        Bodega.BodTipoVenta = "VentaCalzadaProveedor"
                    ElseIf (rdr.Item("BodVtaCalzada") = "Y") Then
                        Bodega.BodTipoVenta = "VentaPuestoFundo"
                    ElseIf (InStr(Bodega.BodCodigo, "MET") > 0) Then
                        Bodega.BodTipoVenta = "VentaLiquidacion"
                    End If


                Else


                    If (rdr.Item("BodConsignado") = "Y") Then
                        Bodega.BodTipoVenta = "VentaConsignada"
                    ElseIf (InStr(Bodega.BodCodigo, "TRT") > 0) Then
                        Bodega.BodTipoVenta = "VentaCalzadaProveedor"
                    ElseIf (rdr.Item("BodVtaCalzada") = "Y") Then
                        Bodega.BodTipoVenta = "VentaPuestoFundo"
                    ElseIf (InStr(Bodega.BodCodigo, "MET") > 0) Then
                        Bodega.BodTipoVenta = "VentaLiquidacion"
                    End If


                End If


                BodegaLista.Add(Bodega)
            End While
            rdr.Close()
            cmd.Dispose()
            cnx.Close()
            cnx.Dispose()
        Catch ex As Exception
            ToLog.write(ex.Message, "E")
        End Try
        Return BodegaLista
    End Function
End Class