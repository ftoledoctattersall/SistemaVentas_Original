# Brechas y pendientes de validación

Este documento contiene exclusivamente aspectos que no pueden determinarse con seguridad mediante análisis estático. Cada punto debe tratarse como `PENDIENTE DE VALIDACIÓN FUNCIONAL`.

| ID | Clasificación | Pendiente | Prioridad | Cómo validarlo |
|---|---|---|---|---|
| PEN-001 | Regla de negocio no determinable | Significado, umbrales, jerarquía y vigencia de los conceptos de autorización crédito, protesto, deuda, margen, costo, factura, tasa y bandas `BM1..3`/`BC1..3`. | ALTA | Revisar SP de autorización y entrevistar a Comercial/Crédito. |
| PEN-002 | Comportamiento SAP no verificable | Campos propios SAP, series, cuentas, impuestos y reglas que deben mantenerse al migrar cada modalidad. | ALTA | Ejecutar casos reales en SAP y documentar UDF, series y contabilización. |
| PEN-003 | Regla de negocio no determinable | Diferencia operativa exacta entre bodega propia, consignada, calzada propia, calzada proveedor, puesto fundo, costo especial y liquidación. | ALTA | Taller con Ventas y Abastecimiento usando una operación real por modalidad. |
| PEN-004 | Funcionalidad aparentemente obsoleta | La pantalla `pagVentaCalzadaPropia.aspx` existe, pero no tiene evento servidor de voucher como las otras modalidades; su vigencia no es concluyente. | MEDIA | Confirmar menú productivo y uso con usuarios. |
| PEN-005 | Configuración dependiente del ambiente | Menús y permisos efectivos por perfil están almacenados en SQL y no se encuentran en el repositorio. | ALTA | Exportar matriz perfil–opción del ambiente productivo. |
| PEN-006 | Comportamiento SAP no verificable | Motivo por el que algunas ventas crean orden directa y otras borrador, y todas las condiciones de conversión. | ALTA | Revisar datos productivos, SP y pruebas por modalidad. |
| PEN-007 | Regla de negocio no determinable | Fórmulas completas de descuento, interés, flete, margen, costo de reposición, redondeo, IVA y tipo de cambio. | ALTA | Obtener definiciones de SP y conciliar ejemplos con Finanzas/Comercial. |
| PEN-008 | Proceso manual | Responsabilidad y momento en que Abastecimiento selecciona proveedor/días de compra y confirma la orden asociada. | ALTA | Observar el proceso con Abastecimiento. |
| PEN-009 | Comportamiento SAP no verificable | Efecto real de `Bloquear GD` y cuándo se libera la guía de despacho. | ALTA | Revisar UDF/flujo SAP y entrevistar a Logística/Crédito. |
| PEN-010 | Configuración dependiente del ambiente | Disponibilidad futura de SAP DI API, SQL Server, ASMX, SMTP y servicio PDFE durante la transición al nuevo POS. | ALTA | Confirmar arquitectura de transición y responsables de cada servicio. |
| PEN-011 | Comportamiento SAP no verificable | Impresión final solicitada al facturar: impresora, cantidad de copias, formato y contingencias. | MEDIA | Probar desde ambiente habilitado y entrevistar a usuarios de caja/documentos. |
| PEN-012 | Regla de negocio no determinable | Criterio exacto y plazo máximo para avisar y cancelar órdenes pendientes de facturación. | ALTA | Revisar `tai_vw_sp2_select_cancela_orden_venta` y scheduler productivo. |
| PEN-013 | Configuración dependiente del ambiente | Scheduler que invoca cancelación de borradores/órdenes, frecuencia, monitoreo y recuperación ante fallos. | ALTA | Inventariar IIS/Task Scheduler/jobs externos. |
| PEN-014 | Dependencia de usuarios | Uso real, destinatarios, periodicidad y decisiones tomadas con cotizaciones, cuenta corriente, venta mensual y comisiones. | MEDIA | Entrevistar responsables y observar reportes usados. |
| PEN-015 | Regla de negocio no determinable | Significado legal/operativo de registrar la cuarta copia y consecuencias de marcar ingreso/no ingreso. | MEDIA | Validar con Administración/Cobranza. |
| PEN-016 | Comportamiento SAP no verificable | Distinción completa entre factura, factura de reserva y boleta, incluido folio y sincronización posterior. | ALTA | Conciliar documentos SAP/DTE de casos reales. |
| PEN-017 | Configuración dependiente del ambiente | Resoluciones SII, API keys, tiempos de espera y tratamiento de errores del servicio de visualización tributaria. | ALTA | Revisar configuración segura y contrato vigente de PDFE. |
| PEN-018 | Dependencia del desarrollador original | Razón de duplicar acceso SAP en `ventas` y `wssap`, y cuál ruta se considera vigente. | MEDIA | Entrevistar mantenedor y revisar telemetría/logs de producción. |
| PEN-019 | Código potencialmente muerto | Clases, servicios ASMX y consultas que están compilados pero podrían no ser alcanzables desde el menú o los formularios actuales. | MEDIA | Capturar menú productivo y analizar logs de uso por 60–90 días. |
| PEN-020 | Comportamiento que requiere validación de negocio | La cláusula enviada al proveedor que presume aceptación de la orden de compra después de 24 horas. | ALTA | Validar con Legal y Abastecimiento antes de replicarla. |
| PEN-021 | Configuración dependiente del ambiente | Se observan credenciales y secretos heredados en recursos/código; no es posible confirmar cuáles siguen activos. | ALTA | Rotar/verificar secretos mediante responsables, sin reutilizarlos en el nuevo POS. |
| PEN-022 | Regla de negocio no determinable | No se identificó un flujo explícito de captura de pagos/caja; puede ocurrir enteramente en SAP u otro sistema. | ALTA | Confirmar con usuarios si el nuevo POS debe incorporar medios de pago y cierre de caja. |
| PEN-023 | Regla de negocio no determinable | No se identificó un flujo completo de despacho, preparación o entrega; sólo tipo, dirección, fecha y bloqueo de guía. | ALTA | Mapear con Logística el proceso posterior a la orden. |
| PEN-024 | Comportamiento que requiere validación de negocio | No se encontró creación explícita de notas de crédito/devoluciones; debe confirmarse si ocurre fuera del legado. | ALTA | Entrevistar Facturación y revisar transacciones SAP. |

## Orden recomendado de cierre

1. Cerrar `PEN-001`, `PEN-003`, `PEN-006`, `PEN-007` y `PEN-022` para definir el núcleo funcional del nuevo POS.
2. Cerrar `PEN-002`, `PEN-009`, `PEN-010`, `PEN-016` y `PEN-017` para diseñar la frontera SAP/DTE.
3. Cerrar procesos operativos y alcance complementario con Ventas, Crédito, Abastecimiento, Logística, Facturación y Finanzas.
