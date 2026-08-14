# Authorization Standard

## Objetivo

Este documento define el estándar de ingeniería que toda IA debe seguir para diseñar, implementar y mantener mecanismos de autorización consistentes, mantenibles y desacoplados.

Su objetivo es garantizar que las decisiones sobre acceso a recursos permanezcan centralizadas, uniformes y fáciles de evolucionar.

No define controles de seguridad.

Las reglas de seguridad pertenecen exclusivamente al módulo **Security**.

---

## Prioridad

**Nivel:** Crítico

Todo mecanismo de autorización implementado dentro de un proyecto que utilice este repositorio deberá cumplir este estándar.

---

## Propósito

Garantizar una estrategia uniforme para controlar el acceso a funcionalidades, recursos y operaciones de la aplicación.

---

## Cuándo consultar este documento

Consultar este documento antes de implementar o modificar:

- Roles.
- Permisos.
- Policies.
- Claims.
- Control de acceso.
- Recursos protegidos.
- Autorización de operaciones.
- Autorización de APIs.
- Autorización de servicios.
- Autorización basada en recursos.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- API.md
- Validation.md
- Error-Handling.md
- Logging.md
- Testing.md
- Authentication.md

Consultar también cuando corresponda:

- security/A01-Broken-Access-Control.md

---

## No cubre

Este documento no define:

- Autenticación.
- Gestión de identidades.
- Reglas de negocio.
- Controles de seguridad.
- Administración de usuarios.
- Infraestructura de identidad.

Estos aspectos pertenecen a sus respectivos estándares.

---

## Principios

Todo mecanismo de autorización debe ser:

- Consistente.
- Centralizado.
- Desacoplado.
- Extensible.
- Determinista.
- Fácil de mantener.
- Fácil de evolucionar.

---

## Reglas obligatorias

### Precondiciones

- Antes de crear o reutilizar una política, capacidad o mecanismo equivalente, identificar explícitamente la operación, el recurso o ámbito, el criterio de autorización, el sujeto o clase de sujetos y la fuente autoritativa de la decisión.
- Utilizar como fuente autoritativa únicamente requisitos, reglas de negocio o políticas existentes cuyo alcance pueda verificarse.
- Considerar pendiente la decisión de autorización cuando alguno de sus elementos necesarios dependa del negocio y no se encuentre definido.
- Informar la definición ausente y detener el diseño de la autorización afectada hasta disponer de una fuente explícita y verificable.
- No considerar resuelta una decisión únicamente por haber informado su ausencia.

### Diseño

- Mantener la autorización separada de la autenticación.
- Centralizar las decisiones de autorización.
- Mantener una estrategia uniforme para todo el sistema.
- Evitar lógica de autorización distribuida entre múltiples componentes.

### Evaluación

- Evaluar la autorización antes de ejecutar la operación protegida.
- Aplicar los mismos criterios para recursos equivalentes.
- Mantener un comportamiento determinista para solicitudes equivalentes.

### Organización

- Definir mecanismos reutilizables para evaluar permisos.
- Mantener políticas claramente identificables.
- Favorecer componentes desacoplados del proveedor de identidad.

### Reutilización

- Reutilizar una autorización existente únicamente cuando exista correspondencia explícita de propósito, operación, recurso y alcance.
- Mantener sin ampliar el alcance original de toda autorización reutilizada.
- No considerar equivalentes dos autorizaciones únicamente por similitud nominal, proximidad funcional, carácter administrativo, nivel aparente de privilegio o conveniencia técnica.

### Evolución

- Diseñar la autorización para facilitar la incorporación de nuevos permisos.
- Minimizar el impacto de cambios en roles o políticas.
- Mantener compatibilidad durante procesos de evolución del sistema cuando corresponda.

---

## Acciones prohibidas

- Nunca mezclar autenticación con autorización.
- Nunca duplicar reglas de autorización entre componentes.
- Nunca incorporar lógica de autorización dentro de la lógica de negocio.
- Nunca depender de implementaciones específicas del proveedor de identidad.
- Nunca distribuir reglas equivalentes en múltiples ubicaciones.
- Nunca crear o ampliar una política, capacidad o mecanismo equivalente sin una fuente funcional explícita y verificable.
- Nunca inferir una autorización a partir de la autenticación del sujeto.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] La autorización permanece separada de la autenticación.
- [ ] Existe una estrategia uniforme.
- [ ] Las decisiones de autorización están centralizadas.
- [ ] Toda autorización concreta posee una fuente funcional explícita y verificable.
- [ ] Las decisiones con elementos de negocio ausentes permanecen pendientes y fueron informadas.
- [ ] Toda autorización reutilizada coincide explícitamente en propósito, operación, recurso y alcance.
- [ ] No existe lógica duplicada.
- [ ] Los componentes permanecen desacoplados.
- [ ] Los estándares relacionados fueron consultados cuando correspondía.

---

## Referencias

- NIST RBAC Model
- OAuth 2.0
- OpenID Connect
- OWASP Authorization Cheat Sheet
