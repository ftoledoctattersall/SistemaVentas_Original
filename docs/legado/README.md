# Documentación funcional del sistema legado de Agroinsumos

## Objetivo

Esta carpeta explica cómo funciona el sistema de ventas legado de Agroinsumos. Está orientada a gerencia, usuarios de negocio, analistas y equipos técnicos sin conocimiento previo de la aplicación.

El punto de entrada recomendado es el [manual funcional](MANUAL-FUNCIONAL-SISTEMA-ACTUAL.md). La [presentación gerencial](PRESENTACION-GERENCIAL-SISTEMA-ACTUAL.md) resume el alcance y los riesgos para revisión ejecutiva. La documentación de migración y del nuevo POS se encuentra fuera de esta carpeta, en `docs/pos/`.

## Sistema inspeccionado

El levantamiento cubre los dos proyectos Visual Basic .NET del repositorio:

- `ventas/SistemaVentasWeb`: aplicación web ASP.NET WebForms usada por los usuarios y sus servicios internos.
- `wssap/WebServices`: servicio web que crea y modifica documentos comerciales en SAP Business One.

Aunque el sistema no es WinForms, conserva una interacción de tipo formulario: páginas de venta, monitores, búsquedas, botones de acción y llamadas AJAX a servicios ASMX.

## Alcance

Se inspeccionaron la solución y proyectos, páginas y navegación, eventos de servidor, JavaScript de negocio, clases de consulta, procedimientos almacenados referenciados, configuración, servicios web, SAP DI API, correo, PDF, impresión y visualización de documentos tributarios.

El inventario inicial se conserva como referencia del levantamiento:

- [01-catalogo-funcional.md](01-catalogo-funcional.md): capacidades confirmadas y sus dependencias.
- [02-flujos-funcionales.md](02-flujos-funcionales.md): procesos principales de punta a punta.
- [03-integraciones.md](03-integraciones.md): sistemas externos, datos, documentos y comunicaciones.
- [04-brechas-y-pendientes.md](04-brechas-y-pendientes.md): decisiones que requieren validación fuera del código.

El detalle funcional consolidado se encuentra en [funcionalidades/](funcionalidades/). La matriz legado → POS se mantiene en `docs/pos/05-matriz-migracion-pos.md`.

## Cómo utilizar esta documentación

El catálogo responde qué hace el sistema. Los flujos permiten revisar el trabajo diario con usuarios. El inventario de integraciones orienta la arquitectura del nuevo POS. Las brechas constituyen una lista de entrevistas y pruebas que deben cerrarse antes de definir alcance definitivo.

Los identificadores `FUN-xxx` son estables y relacionan catálogo, flujos e integraciones. `Confirmado` significa que existe evidencia de interfaz y/o ejecución en el código. No significa que el proceso haya sido probado contra un ambiente operativo.

## Limitaciones del análisis estático

Este levantamiento se realizó sin ejecutar el sistema ni acceder a SAP, SQL Server, servicios externos o tareas programadas. El repositorio referencia numerosos procedimientos almacenados, pero no contiene su definición; por ello, parte de las reglas reside fuera del código disponible. Los menús se obtienen desde base de datos y no es posible reconstruir con certeza su asignación real por perfil. Todo comportamiento insuficientemente sustentado se marca `PENDIENTE DE VALIDACIÓN FUNCIONAL`.
