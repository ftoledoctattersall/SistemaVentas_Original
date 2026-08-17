# MVP ejecutable de Agroinsumos

## 1. Objetivo del MVP

El primer MVP operativo debe permitir que un empleado de Agroinsumos prepare una venta de bodega propia, consulte al cliente y su situación comercial, seleccione productos con disponibilidad, calcule las condiciones de la operación y solicite autorización cuando corresponda.

Una venta aprobada debe poder confirmarse, enviarse de forma controlada a SAP RISE para crear el documento comercial y continuar a factura o boleta. El usuario debe conocer en todo momento el estado de la operación y sus documentos relacionados.

El MVP preserva el proceso de negocio comprobado, pero no replica las tecnologías ni los acoplamientos del legado. Las modalidades especiales, procesos administrativos y capacidades aún no validadas quedan fuera del alcance inicial.

## 2. Flujo operativo objetivo

```mermaid
flowchart LR
    A[Acceso con cuenta Microsoft] --> B[Preparar venta]
    B --> C[Consultar cliente y riesgo]
    C --> D[Seleccionar productos y stock]
    D --> E[Calcular condiciones comerciales]
    E --> F{¿Requiere autorización?}
    F -- Sí --> G[Aprobar o rechazar]
    F -- No --> H[Confirmar venta]
    G -- Aprobada --> H
    G -- Rechazada --> I[Cerrar operación]
    H --> J[Integrar con SAP RISE]
    J --> K[Facturar o emitir boleta]
    K --> L[Monitorear operación]
    L --> M[Cancelar si corresponde]
```

## 3. Épicas P0

### EP-01 — Identidad y acceso de empleados

**Objetivo**

Permitir que empleados identificados con su cuenta Microsoft accedan únicamente a las capacidades que les corresponden.

**Funcionalidades P0 relacionadas**

- `FUN-001`
- `FUN-002`

**Criterios de aceptación MVP**

- Un empleado autenticado con cuenta Microsoft puede ingresar al POS desde el CRM corporativo.
- Una persona no autenticada o no reconocida como empleado no puede operar el POS.
- El sistema muestra sólo las acciones habilitadas para el rol vigente del empleado.
- Cada operación registra la identidad del empleado que la ejecuta.

**Dependencias**

- Identidad corporativa Microsoft.
- Integración del POS con el CRM corporativo.
- Matriz de roles y permisos de Agroinsumos.

**Estado**

PARCIALMENTE BLOQUEADA

**Bloqueo pendiente**

Definir la matriz inicial de roles y permisos; el acceso autenticado puede construirse antes.

### EP-02 — Productos, stock y parámetros operativos

**Objetivo**

Proveer los productos, existencias y parámetros necesarios para preparar una venta con información vigente.

**Funcionalidades P0 relacionadas**

- `FUN-012`
- `FUN-035`

**Criterios de aceptación MVP**

- El empleado puede buscar y seleccionar un producto comercializable.
- Para cada producto seleccionado se informa la disponibilidad por ubicación aplicable.
- El sistema entrega los parámetros necesarios para completar la venta, como moneda, plazo y tipo de despacho.
- Si la información maestra no está disponible o está desactualizada, la venta no se confirma silenciosamente y el usuario recibe un estado comprensible.

**Dependencias**

- Fuentes corporativas de productos, stock y parámetros.
- Responsables de calidad y actualización de datos.

**Estado**

PARCIALMENTE BLOQUEADA

**Bloqueo pendiente**

Confirmar las fuentes productivas y la frecuencia requerida de actualización; los modelos y casos de uso pueden avanzar con contratos simulados.

### EP-03 — Cliente y evaluación de riesgo

**Objetivo**

Identificar al cliente y presentar los antecedentes comerciales necesarios para decidir si una venta puede continuar o requiere autorización.

**Funcionalidades P0 relacionadas**

- `FUN-011`

**Criterios de aceptación MVP**

- El empleado puede buscar y seleccionar un cliente válido.
- Se muestran sus datos comerciales y direcciones aplicables a la venta.
- Se informa su situación de crédito, deuda vencida y protestos cuando esos datos estén disponibles.
- Una condición de riesgo no se omite: bloquea la confirmación directa o deriva la operación a autorización según la política aprobada.

**Dependencias**

- Fuente maestra de clientes.
- Fuentes de riesgo y política de Crédito.

**Estado**

PARCIALMENTE BLOQUEADA

**Bloqueo pendiente**

Confirmar fuentes y criterios de riesgo; la selección y visualización básica de cliente puede implementarse antes.

### EP-04 — Preparación y borrador de venta

**Objetivo**

Permitir que el empleado construya y conserve una venta de bodega propia antes de confirmarla.

**Funcionalidades P0 relacionadas**

- `FUN-003`
- `FUN-014`

**Criterios de aceptación MVP**

- El empleado puede crear una venta en preparación para un cliente seleccionado.
- Puede agregar, modificar y quitar productos con cantidad, ubicación, precio y fecha aplicables.
- Puede registrar condiciones de pago, despacho, direcciones, referencias y comentarios sustentados por el flujo actual.
- Puede guardar la operación como borrador del POS y retomarla sin crear todavía una orden definitiva en SAP.
- El sistema impide confirmar una venta incompleta e identifica los datos faltantes.

**Dependencias**

- EP-01, EP-02 y EP-03.
- Definición mínima de estados de la venta.

**Estado**

LISTA

### EP-05 — Cálculo de condiciones comerciales

**Objetivo**

Calcular de forma consistente y auditable los importes y condiciones que determinan el valor y la viabilidad comercial de la venta.

**Funcionalidades P0 relacionadas**

- `FUN-010`

**Criterios de aceptación MVP**

- Cada línea presenta precio, cantidad, descuento, recargos, impuesto y total calculados con las reglas aprobadas.
- El total de la venta se recalcula ante cualquier cambio relevante.
- Moneda, tipo de cambio y redondeos producen resultados reproducibles para la misma entrada.
- El sistema identifica qué condición comercial excede la política y requiere autorización.
- El usuario puede comprender los componentes principales del total antes de confirmar.

**Dependencias**

- Política formal de precios, descuentos, interés, flete, margen, impuestos, monedas y redondeo.
- EP-02, EP-03 y EP-04.

**Estado**

BLOQUEADA

**Bloqueo pendiente**

Negocio y Crédito deben aprobar las fórmulas y umbrales que reemplazarán las reglas no documentadas del legado.

### EP-06 — Autorizaciones comerciales

**Objetivo**

Derivar excepciones comerciales a responsables habilitados y consolidar una decisión trazable antes de confirmar la venta.

**Funcionalidades P0 relacionadas**

- `FUN-016`
- `FUN-017`
- `FUN-018`
- `FUN-019`
- `FUN-020`

**Criterios de aceptación MVP**

- El sistema genera una solicitud por cada condición que requiere autorización e informa su motivo.
- Los responsables habilitados reciben y pueden consultar sus solicitudes pendientes.
- Un autorizador puede aprobar o rechazar y registrar un comentario.
- La venta sólo avanza cuando se cumple la política completa de aprobación.
- Un rechazo cierra la solicitud e impide confirmar la venta sin una nueva evaluación.
- Solicitud, decisiones, responsables y fechas quedan trazables.

**Dependencias**

- EP-01 y EP-05.
- Política de autorizaciones, jerarquías, reemplazos y canal de notificación.

**Estado**

BLOQUEADA

**Bloqueo pendiente**

Crédito y Negocio deben definir conceptos, umbrales, combinación de decisiones y responsables iniciales.

### EP-07 — Confirmación e integración con SAP RISE

**Objetivo**

Confirmar una venta aprobada y obtener en SAP RISE una orden comercial correlacionada con la operación del POS.

**Funcionalidades P0 relacionadas**

- `FUN-015`

**Criterios de aceptación MVP**

- Sólo una venta completa y, cuando corresponda, autorizada puede enviarse a SAP.
- Una confirmación exitosa devuelve el identificador del documento SAP y lo asocia a la venta del POS.
- Reintentar una solicitud no crea órdenes duplicadas.
- Un rechazo o indisponibilidad de SAP deja la operación en un estado recuperable y comprensible.
- El usuario puede distinguir una venta pendiente de integración, integrada o fallida.

**Dependencias**

- EP-04, EP-05 y EP-06.
- Contrato productivo de integración SAP RISE y mapeo de datos comerciales.

**Estado**

BLOQUEADA

**Bloqueo pendiente**

SAP/Integraciones y Arquitectura deben definir el contrato, datos obligatorios, series, impuestos, errores y mecanismo de correlación, sin usar DI API.

### EP-08 — Facturación y boleta

**Objetivo**

Generar desde una venta confirmada el documento tributario que corresponda y conservar su resultado.

**Funcionalidades P0 relacionadas**

- `FUN-023`

**Criterios de aceptación MVP**

- El empleado habilitado puede solicitar factura o boleta para una orden elegible.
- El sistema valida que la orden no haya sido facturada previamente.
- Una emisión exitosa registra tipo, folio, fecha, estado e identificador relacionado.
- Un fallo de emisión no marca la venta como facturada y permite seguimiento o recuperación controlada.
- El estado tributario puede consultarse desde la operación de origen.

**Dependencias**

- EP-07.
- Definición de factura, boleta, DTE, contingencia e integración SAP RISE.

**Estado**

BLOQUEADA

**Bloqueo pendiente**

Facturación y SAP/Integraciones deben acordar tipos documentales, reglas de elegibilidad, folio, contingencia y sistema responsable de la emisión.

### EP-09 — Monitoreo y cancelación operativa

**Objetivo**

Permitir el seguimiento punta a punta de ventas, autorizaciones y documentos, y controlar órdenes sin facturar que excedan la política.

**Funcionalidades P0 relacionadas**

- `FUN-021`
- `FUN-022`
- `FUN-034`

**Criterios de aceptación MVP**

- El empleado puede localizar sus ventas y consultar su estado actual.
- El detalle muestra la relación entre venta, autorizaciones, orden SAP y documento tributario.
- Las operaciones detenidas o fallidas son visibles y señalan la acción posible.
- El sistema identifica órdenes sin facturar que cumplen la política de vencimiento.
- La advertencia y cancelación quedan registradas; una cancelación SAP fallida permanece visible para recuperación.

**Dependencias**

- Estados definidos por EP-04, EP-06, EP-07 y EP-08.
- Política de vencimiento, aviso, excepciones y cancelación en SAP.

**Estado**

PARCIALMENTE BLOQUEADA

**Bloqueo pendiente**

El monitoreo puede avanzar con los estados del POS; la automatización de cancelación requiere una política aprobada y la operación correspondiente en SAP RISE.

## 4. Orden recomendado de implementación

| Orden | Épica | Razón | Dependencia previa |
|---:|---|---|---|
| 1 | EP-04 — Preparación y borrador de venta | Establece el agregado funcional central y permite probar el ciclo sin integraciones externas. | Modelo mínimo de empleado, cliente y producto simulado. |
| 2 | EP-01 — Identidad y acceso de empleados | Sustituye identidades simuladas y asegura quién puede operar desde el CRM. | Configuración corporativa Microsoft/CRM. |
| 3 | EP-02 — Productos, stock y parámetros | Habilita datos reutilizados por preparación, cálculo y SAP. | Contratos iniciales con fuentes maestras. |
| 4 | EP-03 — Cliente y evaluación de riesgo | Completa la selección de cliente y prepara la derivación a autorización. | Contratos iniciales con clientes y riesgo. |
| 5 | EP-09 — Monitoreo y cancelación operativa | Implementar primero el monitoreo fuerza estados y trazabilidad coherentes; la cancelación puede incorporarse después. | EP-04; estados iniciales. |
| 6 | EP-05 — Cálculo de condiciones comerciales | Convierte el borrador en una operación comercial evaluable. | EP-02, EP-03, EP-04 y reglas aprobadas. |
| 7 | EP-06 — Autorizaciones comerciales | Habilita el tratamiento de excepciones antes de confirmar. | EP-01, EP-05 y política aprobada. |
| 8 | EP-07 — Confirmación e integración con SAP RISE | Valida temprano la frontera corporativa de mayor riesgo externo. | EP-04 a EP-06 y contrato SAP. |
| 9 | EP-08 — Facturación y boleta | Completa el ciclo tributario sobre una orden SAP confirmada. | EP-07 y definición DTE. |

## 5. Decisiones realmente bloqueantes

### Bloquean implementación inmediata

| Área | Pregunta concreta | Qué impide avanzar | Responsable sugerido |
|---|---|---|---|
| Cálculo comercial | ¿Cuáles son las fórmulas y umbrales aprobados para precio, descuento, interés, flete, margen, impuestos, moneda y redondeo? | Finalizar EP-05 y determinar excepciones. | Negocio / Crédito |
| Autorizaciones | ¿Qué conceptos se autorizan, quién decide y qué combinación de respuestas permite confirmar o rechazar? | Diseñar el comportamiento correcto de EP-06. | Crédito / Negocio |
| SAP RISE | ¿Cuál será el contrato para crear y consultar órdenes, incluidos datos obligatorios, series, impuestos, correlación y errores? | Integrar y cerrar EP-07. | SAP/Integraciones / Arquitectura |
| Facturación/DTE | ¿Qué sistema emite factura o boleta y cuáles son elegibilidad, folio, contingencia y resultado esperado? | Diseñar y cerrar EP-08. | Facturación / SAP/Integraciones |

### Pueden resolverse después

| Área | Pregunta concreta | Qué impide avanzar | Responsable sugerido |
|---|---|---|---|
| Acceso | ¿Cuál es la matriz definitiva de roles y permisos de Agroinsumos? | Cerrar autorización fina de EP-01; no impide autenticar empleados. | Seguridad / Negocio |
| Cancelación | ¿Qué plazo, aviso, excepciones y autorización aplican a órdenes sin facturar? | Completar cancelación automática de EP-09; no impide monitorear. | Negocio / Facturación |
| Pagos | ¿El POS incorporará medios de pago, caja y cierre? | Definir una fase posterior; no forma parte de las P0 documentadas. | Negocio |
| Despacho | ¿Hasta dónde cubre el POS preparación, guía, bloqueo y entrega? | Ampliar el proceso posterior; el MVP conserva datos básicos de despacho. | Logística / Negocio |
| Postventa | ¿Devoluciones y notas de crédito pertenecerán al POS? | Definir alcance posterior a la venta. | Facturación / Negocio |

## 6. Fuera del MVP

Quedan deliberadamente fuera las modalidades consignada, puesto fundo, calzada proveedor, calzada propia, costo especial y liquidación hasta confirmar su vigencia y reglas. También se postergan compras asociadas, notificación a proveedores, cotizaciones, voucher, cuenta corriente como informe, ventas mensuales, comisiones, cuarta copia y arquitectura completa de reportes.

Los pagos/caja, el despacho integral y la postventa requieren decisiones de alcance adicionales. Estar fuera del MVP significa que no bloquean la primera operación de venta de bodega propia; no implica que estén descartados del producto corporativo.

## 7. Definition of Done del MVP

- Un empleado autorizado accede desde el CRM con su cuenta Microsoft y sus acciones quedan identificadas.
- Puede crear, guardar y retomar una venta de bodega propia para un cliente válido.
- Puede consultar datos del cliente y los antecedentes de riesgo definidos para el MVP.
- Puede seleccionar productos y conocer su disponibilidad y parámetros comerciales vigentes.
- Los importes, impuestos, moneda, redondeos y excepciones se calculan con reglas aprobadas y pruebas de negocio.
- Las ventas que lo requieran completan un flujo trazable de aprobación o rechazo.
- Una venta elegible se confirma una sola vez en SAP RISE y conserva su correlación con el POS.
- Una orden elegible puede generar factura o boleta y conservar su resultado tributario.
- Ventas, autorizaciones, órdenes y documentos pueden monitorearse de punta a punta; los pendientes cancelables son identificables.
- Los errores de datos, autorización, SAP o facturación son comprensibles, no producen duplicados y dejan la operación en un estado recuperable.

## 8. Resumen ejecutivo

- Se definieron **9 épicas** que cubren las **18 funcionalidades P0** sin replicar la estructura técnica del legado.
- **1 épica está LISTA**: preparación y borrador de venta.
- **4 épicas están PARCIALMENTE BLOQUEADAS**: acceso, datos maestros, cliente/riesgo y monitoreo/cancelación.
- **4 épicas están BLOQUEADAS**: cálculo comercial, autorizaciones, SAP RISE y facturación/DTE.
- Las decisiones inmediatas son cuatro: fórmulas comerciales, política de autorización, contrato SAP RISE y definición de facturación/DTE.
- Roles definitivos, política de cancelación, pagos, despacho y postventa pueden resolverse después sin detener el primer incremento.
- La primera épica recomendada es **EP-04 — Preparación y borrador de venta**.
- El primer incremento debe operar con dependencias simuladas y estados explícitos, para sustituirlas progresivamente por identidad y fuentes corporativas reales.
