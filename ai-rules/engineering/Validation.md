# Validation Standard

## Objetivo

Este documento define el estándar de ingeniería que toda IA debe seguir para diseñar, implementar y mantener mecanismos de validación consistentes durante el procesamiento de datos.

Su objetivo es garantizar que todas las validaciones sean predecibles, reutilizables, deterministas e independientes de la lógica de negocio.

No define controles de seguridad.

Las reglas de seguridad pertenecen exclusivamente al módulo **Security**.

---

## Prioridad

**Nivel:** Crítico

Toda validación implementada dentro de un proyecto que utilice este repositorio deberá cumplir este estándar.

---

## Propósito

Garantizar que todas las validaciones del sistema mantengan un comportamiento uniforme, sean fáciles de mantener y eviten inconsistencias entre distintos componentes de la aplicación.

---

## Cuándo consultar este documento

Consultar este documento antes de implementar o modificar:

- Validaciones de entrada.
- Validaciones de parámetros.
- Validaciones de modelos.
- Validaciones de solicitudes HTTP.
- Validaciones de comandos.
- Validaciones de consultas.
- Validaciones de DTO.
- Validaciones de formularios.
- Reglas técnicas de validación.
- Procesamiento de datos de entrada.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- API.md
- Error-Handling.md
- Database.md
- Testing.md

Consultar también cuando corresponda:

- security/A03-Injection.md

---

## No cubre

Este documento no define:

- Reglas de negocio.
- Autenticación.
- Autorización.
- Persistencia.
- Logging.md.
- Manejo de excepciones.
- Controles de seguridad.
- Restricciones específicas de una base de datos.

Estos aspectos pertenecen a sus respectivos estándares.

---

## Principios

Toda estrategia de validación debe ser:

- Determinista.
- Consistente.
- Reutilizable.
- Independiente de la lógica de negocio.
- Independiente de la persistencia.
- Fácil de mantener.
- Fácil de extender.

---

## Reglas obligatorias

### Diseño

- Definir las validaciones antes de ejecutar la lógica de negocio.
- Mantener las validaciones separadas de la lógica de negocio.
- Mantener las validaciones independientes de la persistencia.
- Diseñar validaciones reutilizables.
- Mantener una única responsabilidad por componente de validación.

### Consistencia

- Aplicar los mismos criterios de validación para datos equivalentes.
- Mantener reglas uniformes entre distintos puntos de entrada.
- Evitar comportamientos diferentes para la misma validación.

### Determinismo

- Toda validación debe producir siempre el mismo resultado para la misma entrada.
- Evitar dependencias del estado interno del sistema.
- Evitar dependencias del momento de ejecución cuando no sean estrictamente necesarias.

### Entrada de datos

- Validar todos los datos externos antes de utilizarlos.
- Validar parámetros obligatorios.
- Validar formatos.
- Validar rangos.
- Validar longitudes.
- Validar tipos de datos.
- Validar colecciones cuando corresponda.

### Errores de validación

- Informar claramente qué validación falló.
- Mantener una estructura uniforme para los errores de validación.
- Evitar mensajes ambiguos.
- Mantener consistencia entre todos los errores de validación.

### Reutilización

- Evitar duplicar reglas de validación.
- Centralizar reglas comunes.
- Reutilizar componentes de validación cuando sea posible.

### Mantenibilidad

- Mantener las reglas organizadas.
- Evitar validaciones dispersas.
- Evitar lógica compleja dentro de las validaciones.
- Mantener las validaciones fáciles de revisar y modificar.

---

## Acciones prohibidas

- Nunca ejecutar lógica de negocio antes de completar las validaciones.
- Nunca utilizar validaciones para modificar el estado del sistema.
- Nunca mezclar validaciones técnicas con reglas de negocio.
- Nunca duplicar la misma validación en múltiples componentes sin una justificación.
- Nunca depender exclusivamente de validaciones del cliente.
- Nunca ocultar errores de validación.
- Nunca generar respuestas inconsistentes para errores equivalentes.
- Nunca acceder a la base de datos únicamente para validar reglas técnicas.
- Nunca incorporar efectos secundarios durante una validación.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] Todas las entradas externas son validadas.
- [ ] Las validaciones se ejecutan antes de la lógica de negocio.
- [ ] Las validaciones son independientes de la persistencia.
- [ ] No existen validaciones duplicadas.
- [ ] Las reglas son reutilizables.
- [ ] Los errores de validación son consistentes.
- [ ] Las validaciones no modifican el estado del sistema.
- [ ] No existen dependencias innecesarias entre validaciones.
- [ ] Las reglas técnicas permanecen separadas de las reglas de negocio.
- [ ] Los estándares relacionados fueron consultados cuando correspondía.

---

## Referencias

- RFC 9110 — HTTP Semantics
- OpenAPI Specification
- OWASP ASVS
- OWASP Input Validation Cheat Sheet
- NIST Secure Software Development Framework (SSDF)