# Error Handling Standard

## Objetivo

Este documento define el estándar de ingeniería que toda IA debe seguir para detectar, representar, propagar y manejar errores de forma consistente durante la ejecución de una aplicación.

Su objetivo es garantizar que todos los errores mantengan un comportamiento uniforme, predecible, mantenible y trazable.

No define controles de seguridad.

Las reglas de seguridad pertenecen exclusivamente al módulo **Security**.

---

## Prioridad

**Nivel:** Crítico

Todo mecanismo de manejo de errores implementado dentro de un proyecto que utilice este repositorio deberá cumplir este estándar.

---

## Propósito

Garantizar una estrategia uniforme para el tratamiento de errores en toda la aplicación, facilitando el mantenimiento, la observabilidad y la evolución del sistema.

---

## Cuándo consultar este documento

Consultar este documento antes de implementar o modificar:

- APIs.
- Servicios.
- Casos de uso.
- Controladores.
- Middleware.
- Filtros.
- Pipelines.
- Procesos Batch.
- Workers.
- Integraciones.
- Procesamiento asíncrono.
- Componentes reutilizables.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- API.md
- Validation.md
- Logging.md
- Testing.md

Consultar también cuando corresponda:

- security/A03-Injection.md
- security/A05-Security-Misconfiguration.md

---

## No cubre

Este documento no define:

- Logging.md.
- Auditoría.
- Validaciones.
- Persistencia.
- Reglas de negocio.
- Autenticación.
- Autorización.
- Controles de seguridad.

Estos aspectos pertenecen a sus respectivos estándares.

---

## Principios

Todo error debe:

- Ser consistente.
- Ser determinista.
- Ser trazable.
- Ser clasificable.
- Ser mantenible.
- Preservar el contexto necesario para el diagnóstico.
- Exponer únicamente la información necesaria al consumidor.

---

## Reglas obligatorias

### Estrategia

- Mantener una estrategia uniforme para el manejo de errores.
- Definir una única representación pública de errores para toda la aplicación.
- Mantener un comportamiento consistente entre todos los componentes.

### Clasificación

- Diferenciar errores de validación.
- Diferenciar errores de negocio.
- Diferenciar errores de infraestructura.
- Diferenciar errores inesperados.
- Mantener criterios uniformes de clasificación.

### Propagación

- Capturar únicamente los errores que puedan manejarse correctamente.
- Permitir la propagación cuando la capa actual no pueda resolver el problema.
- Mantener el contexto del error durante toda la propagación.
- Evitar transformar innecesariamente un error en otro equivalente.

### Contratos públicos

- Mantener una estructura uniforme para todas las respuestas de error.
- Mantener códigos consistentes.
- Mantener mensajes claros.
- Mantener contratos estables entre versiones.

### Información expuesta

- Exponer únicamente información necesaria para el consumidor.
- Evitar detalles internos de implementación.
- Evitar información sensible.
- Evitar información que dificulte futuras modificaciones del sistema.

### Recuperación

- Recuperar la ejecución únicamente cuando el sistema pueda continuar de forma consistente.
- Finalizar la operación cuando la recuperación no sea posible.
- Evitar estados parciales o inconsistentes.

### Mantenibilidad

- Centralizar el manejo de errores cuando sea posible.
- Evitar lógica duplicada.
- Mantener reglas uniformes entre componentes.
- Mantener una clasificación fácil de extender.

---

## Acciones prohibidas

- Nunca ignorar errores.
- Nunca ocultar excepciones.
- Nunca utilizar excepciones como mecanismo normal de control de flujo.
- Nunca devolver estructuras diferentes para errores equivalentes.
- Nunca exponer detalles internos mediante respuestas públicas.
- Nunca perder el contexto del error.
- Nunca capturar excepciones sin una acción justificada.
- Nunca generar respuestas ambiguas.
- Nunca mezclar errores técnicos con errores funcionales.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] Existe una estrategia uniforme para todos los errores.
- [ ] Los errores mantienen una estructura consistente.
- [ ] Los errores técnicos permanecen separados de los funcionales.
- [ ] No se exponen detalles internos.
- [ ] Se preserva el contexto del error.
- [ ] La clasificación es consistente.
- [ ] No existen capturas innecesarias.
- [ ] Los contratos permanecen estables.
- [ ] Los estándares relacionados fueron consultados cuando correspondía.

---

## Referencias

- RFC 9457 — Problem Details for HTTP APIs
- RFC 9110 — HTTP Semantics
- OpenAPI Specification
- OWASP Error Handling Cheat Sheet
- NIST Secure Software Development Framework (SSDF)