# API Standard

## Objetivo

Este documento define el estándar de ingeniería que toda IA debe seguir para diseñar, implementar y evolucionar APIs de forma consistente, mantenible y predecible.

Su objetivo es establecer criterios uniformes para el diseño de contratos HTTP, recursos, operaciones y comportamiento de las APIs.

No define controles de seguridad.

Las reglas de seguridad pertenecen exclusivamente al módulo **Security**.

---

## Prioridad

**Nivel:** Crítico

Toda API implementada dentro de un proyecto que utilice este repositorio deberá cumplir este estándar.

---

## Propósito

Garantizar que todas las APIs mantengan un comportamiento uniforme, contratos consistentes y una estructura predecible para clientes, desarrolladores y asistentes de inteligencia artificial.

---

## Cuándo consultar este documento

Consultar este documento antes de implementar o modificar:

- APIs REST.
- APIs HTTP.
- Endpoints.
- Controladores.
- Recursos.
- Contratos públicos.
- Versionado de APIs.
- Operaciones CRUD.
- Paginación.
- Filtros.
- Ordenamiento.
- Documentación OpenAPI.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- Validation.md
- Error-Handling.md
- Logging.md
- Testing.md
- Authentication.md
- Authorization.md

Consultar también cuando corresponda:

- security/A01-Broken-Access-Control.md
- security/A03-Injection.md
- security/A05-Security-Misconfiguration.md

---

## No cubre

Este documento no define:

- Validaciones de entrada.
- Autenticación.
- Autorización.
- Logging.md.
- Manejo interno de excepciones.
- Persistencia.
- Reglas de negocio.
- Controles de seguridad.

Estos aspectos pertenecen a sus respectivos estándares.

---

## Principios

Toda API debe ser:

- Consistente.
- Predecible.
- Evolutiva.
- Desacoplada.
- Fácil de comprender.
- Fácil de consumir.
- Independiente de tecnologías específicas.

---

## Reglas obligatorias

### Diseño

- Diseñar APIs orientadas a recursos.
- Mantener contratos públicos estables.
- Utilizar nombres consistentes para recursos.
- Evitar cambios incompatibles sin versionado.
- Mantener una estructura uniforme entre endpoints.

### Recursos

- Utilizar sustantivos para representar recursos.
- Evitar verbos en las rutas cuando representen operaciones CRUD.
- Mantener convenciones homogéneas de nombres.
- Representar colecciones y recursos individuales de forma consistente.

### Métodos HTTP

- Utilizar el método HTTP que represente correctamente la operación.
- Mantener comportamiento consistente para cada verbo HTTP.
- No utilizar un mismo endpoint para operaciones con responsabilidades distintas.

### Contratos

- Mantener estructuras de respuesta consistentes.
- Mantener estructuras de solicitud consistentes.
- Definir contratos explícitos.
- Evitar cambios incompatibles en contratos públicos.
- Documentar cualquier modificación del contrato.

### Versionado

- Versionar únicamente cuando existan cambios incompatibles.
- Mantener versiones coexistiendo durante el período de transición definido por el proyecto.
- No reutilizar una versión para modificar contratos incompatibles.

### Identificadores

- Utilizar identificadores estables.
- Mantener el mismo identificador durante todo el ciclo de vida del recurso.
- Evitar exponer identificadores temporales como identificadores públicos.

### Paginación

- Utilizar un mecanismo uniforme de paginación.
- Mantener contratos consistentes entre recursos paginados.
- Documentar el comportamiento de la paginación.

### Filtros

- Mantener una sintaxis consistente para filtros.
- Validar filtros antes de ejecutar la operación.
- Evitar parámetros ambiguos.

### Ordenamiento

- Mantener un mecanismo uniforme de ordenamiento.
- Documentar los campos permitidos.
- Rechazar criterios de ordenamiento no soportados.

### Documentación

- Mantener la documentación sincronizada con la implementación.
- Documentar todos los endpoints públicos.
- Documentar parámetros, respuestas y códigos HTTP utilizados.
- Mantener especificaciones OpenAPI actualizadas cuando correspondan.

---

## Acciones prohibidas

- Nunca romper contratos públicos sin versionado.
- Nunca modificar el comportamiento de un endpoint sin actualizar su documentación.
- Nunca utilizar nombres inconsistentes para recursos equivalentes.
- Nunca mezclar responsabilidades distintas en un mismo endpoint.
- Nunca utilizar rutas ambiguas.
- Nunca implementar contratos diferentes para operaciones equivalentes.
- Nunca devolver respuestas inconsistentes para una misma operación.
- Nunca exponer detalles internos de la implementación mediante el contrato público.
- Nunca depender del comportamiento implícito del cliente para interpretar una solicitud.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] La API representa recursos de forma consistente.
- [ ] Los métodos HTTP reflejan correctamente la operación realizada.
- [ ] Los contratos públicos son consistentes.
- [ ] Los cambios incompatibles fueron versionados.
- [ ] La paginación mantiene un comportamiento uniforme.
- [ ] Los filtros son consistentes y documentados.
- [ ] El ordenamiento es uniforme.
- [ ] La documentación refleja la implementación actual.
- [ ] No existen rutas ambiguas.
- [ ] Los estándares relacionados fueron consultados cuando correspondía.

---

## Referencias

- RFC 9110 — HTTP Semantics
- RFC 9111 — HTTP Caching
- RFC 9112 — HTTP/1.1
- RFC 9457 — Problem Details for HTTP APIs
- OpenAPI Specification
- REST Architectural Style