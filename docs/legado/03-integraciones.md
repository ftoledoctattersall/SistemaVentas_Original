# Inventario de integraciones

## Sistemas y canales

| ID | Sistema | Propósito | Dirección | Evidencia | Funcionalidades relacionadas |
|---|---|---|---|---|---|
| INT-001 | SQL Server | Autenticación, menús, maestros, reglas comerciales, monitores, informes, estados y relaciones documentales. | Aplicación y servicio → SQL Server | `Classes/*Listado.vb`, `clsFuncion.vb`, recursos de ambiente | FUN-001, FUN-002, FUN-010 a FUN-012, FUN-016 a FUN-022, FUN-027 a FUN-035 |
| INT-002 | SAP Business One DI API | Crear borradores, órdenes de venta, facturas/boletas, órdenes de compra y cancelar órdenes. | Aplicación/integrador → SAP | `wssap/.../clsOrdenVenta.vb`, `clsOrdenCompra.vb`; también existen accesos directos en `ventas/.../Classes` | FUN-014, FUN-015, FUN-020, FUN-023, FUN-025, FUN-034 |
| INT-003 | Servicio ASMX `srvOrdenVenta` | Exponer operaciones SAP a la aplicación web. | Aplicación web → integrador `wssap` | `ventas/.../web.config`, Web Reference `WebServices`, `wssap/.../Services/srvOrdenVenta.asmx.vb` | FUN-014, FUN-015, FUN-020, FUN-023, FUN-025, FUN-034 |
| INT-004 | Servicios ASMX internos | Entregar por AJAX clientes, productos, bodegas, inventario, plazos, proveedores, transacciones y otros maestros. | Navegador → aplicación web | `ventas/SistemaVentasWeb/SistemaVentasWeb/Services/*.asmx.vb` | FUN-001, FUN-002, FUN-010 a FUN-012, FUN-021, FUN-022, FUN-035 |
| INT-005 | PDFE/Azurian | Obtener URL de visualización del PDF de un documento tributario electrónico. | Aplicación web → SOAP externo | Web Reference `PDFE`, `pagVisualizarOrdenVenta.aspx.vb`, endpoint configurable | FUN-024 |
| INT-006 | SMTP corporativo | Enviar solicitudes y resultados de autorización, órdenes de compra y avisos de cancelación. | Aplicación web → servidor SMTP → destinatarios | `ToMail.vb`, `clsAutorizacionListado.vb`, `clsDispatcherListado.vb`, páginas de compra/cancelación | FUN-017 a FUN-020, FUN-026, FUN-034 |
| INT-007 | iTextSharp | Construir voucher comercial en formato PDF. | Aplicación → archivo/respuesta PDF | `Modules/mdlVoucherVenta.vb`, páginas de modalidades de venta | FUN-013 |
| INT-008 | Impresión SAP | Solicitar impresión al crear el documento de venta. El mecanismo final ocurre en SAP/ambiente. | Aplicación → integrador → SAP | Parámetro `blnImprimir` en `RegistrarOrdenVentaEnFacturaVenta` | FUN-023 |
| INT-009 | NLog/archivos de log | Registrar diagnósticos, consultas, resultados y errores en rutas configuradas. | Aplicación/servicio → archivos | `NLog.config`, `Classes/ToLog.vb` en ambos proyectos | Soporte transversal |
| INT-010 | Tarea programada externa | Invocar páginas de cancelación para aviso y ejecución. No está incluida en el repositorio. | Scheduler externo → páginas web | `pagCancelarBorradorVenta.aspx.vb`, `pagCancelarOrdenVenta.aspx.vb` | FUN-033, FUN-034 |

## Procedimientos almacenados referenciados

Las definiciones SQL no están versionadas. Su propósito se infiere únicamente por parámetros consumidos y columnas leídas.

| Grupo | Procedimientos referenciados | Uso funcional |
|---|---|---|
| Acceso y navegación | `tai_vw_sp2_select_usuario_sistema`, `tai_vw_sp2_select_menu_sistema` | Validación de usuario, contexto y menú por perfil |
| Clientes y riesgo | `tai_vw_sp2_select_cliente`, `tai_vw_sp2_select_cuenta_corriente` | Datos, direcciones, vendedor, crédito, deuda, protestos y cuenta corriente |
| Productos y stock | `tai_vw_sp2_select_producto`, `tai_vw_sp2_select_inventario`, `tai_vw_sp2_select_hibrido_producto`, `tai_vw_sp2_select_ingrediente_activo` | Catálogo, atributos y existencias |
| Condiciones comerciales | `tai_vw_sp2_select_descuento_producto`, `tai_vw_sp2_select_descuento_maximo_producto`, `tai_vw_sp2_select_interes_producto`, `tai_vw_sp2_select_tasa_interes_producto`, `tai_vw_sp2_select_flete_producto`, `tai_vw_sp2_select_tipo_cambio` | Cálculo de precio, descuento, interés, flete y conversión monetaria |
| Autorizaciones | `tai_vw_sp2_select_autorizacion`, `tai_vw_sp2_select_autorizacion_especial`, `tai_vw_sp2_select_autorizador`, `tai_vw_sp2_insert_autorizaciones`, `tai_vw_sp2_update_autorizaciones`, `tai_vw_sp2_insert_dispatcher`, `tai_vw_sp2_update_dispatcher`, `tai_vw_sp2_select_dispatcher` | Determinación, asignación y respuesta de aprobaciones |
| Documentos | `tai_vw_sp2_select_borrador_venta`, `tai_vw_sp2_select_orden_venta`, `tai_vw_sp2_select_monitor_documento_resumen`, `tai_vw_sp2_select_monitor_documento_detalle`, `tai_vw_sp2_update_estado_borrador`, `tai_vw_sp2_update_estado_orden`, `tai_vw_sp2_update_boleta_venta` | Consulta y sincronización de estados/documentos |
| Compras | `tai_vw_sp2_select_orden_compra`, `tai_vw_sp2_select_orden_compra_genera`, `tai_vw_sp2_select_proveedor_orden_compra`, `tai_vw_sp2_update_orden_compra`, `tai_vw_sp2_select_cuenta_contable` | Construcción y relación de órdenes de compra |
| Informes y comisiones | `tai_vw_sp2_select_cotizacion`, `tai_vw_sp2_select_venta_mensual`, `tai_vw_sp2_select_liquidacion_comision`, `tai_vw_sp2_select_periodo_contable`, `tai_vw_sp2_select_mes` | Cotizaciones, ventas, períodos y comisiones |
| Cuarta copia | `tai_vw_sp2_select_cuarta_copia`, `tai_vw_sp2_update_cuarta_copia` | Consulta y marca de recepción documental |
| Cancelación | `tai_vw_sp2_select_cancela_borrador_venta`, `tai_vw_sp2_select_cancela_orden_venta` | Selección/actualización de documentos vencidos |
| Maestros | `tai_vw_sp2_select_bodega`, `tai_vw_sp2_select_Empleado`, `tai_vw_sp2_select_operador`, `tai_vw_sp2_select_parametro`, `tai_vw_sp2_select_plazo_compra`, `tai_vw_sp2_select_plazo_venta`, `tai_vw_sp2_select_proveedor`, `tai_vw_sp2_select_region`, `tai_vw_sp2_select_serie`, `tai_vw_sp2_select_tipo_despacho` | Listas de soporte operacional |

## Observaciones para el nuevo POS

- El legado combina tres formas de integración con SAP: DI API dentro del integrador, DI API también dentro de la aplicación web y consultas SQL que aparentan leer datos SAP. El nuevo POS debe concentrar esta frontera en una capa de integración controlada.
- Los endpoints, servidores, usuarios, claves y destinatarios dependen del ambiente. Esta documentación deliberadamente no reproduce secretos encontrados en archivos de recursos o código.
- Las llamadas ASMX y SOAP son contratos heredados; debe confirmarse si seguirán disponibles durante la transición.
