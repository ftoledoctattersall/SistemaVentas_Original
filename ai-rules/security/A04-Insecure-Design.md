# OWASP A04 – Insecure Design

## Objetivo del documento

Este documento forma parte del repositorio **ai-rules** y define las reglas obligatorias que toda IA debe aplicar durante el análisis, diseño y planificación de una solución antes de comenzar su implementación.

Las reglas de este documento complementan las instrucciones del proyecto y deben aplicarse antes de generar cualquier código.

---

## Prioridad

**Nivel:** Crítico

El incumplimiento de cualquiera de las reglas de este documento puede introducir vulnerabilidades estructurales que no podrán corregirse únicamente mediante cambios de implementación.

---

## Propósito

Garantizar que toda solución sea diseñada considerando la seguridad desde su concepción, identificando riesgos, definiendo controles para cada riesgo y evitando decisiones arquitectónicas inseguras.

---

## Cuándo consultar este documento

Consultar este documento antes de:

- Diseñar una nueva aplicación.
- Crear una nueva arquitectura.
- Diseñar una API.
- Crear un nuevo módulo.
- Diseñar un flujo de autenticación.
- Diseñar un flujo de autorización.
- Diseñar integraciones entre sistemas.
- Diseñar procesos de negocio.
- Definir modelos de datos.
- Diseñar procesos automáticos.
- Diseñar procesos batch.
- Diseñar procesos distribuidos.
- Implementar nuevas funcionalidades.
- Modificar arquitectura existente.
- Incorporar servicios de terceros.
- Definir permisos o roles.
- Diseñar operaciones administrativas.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- security/A01-Broken-Access-Control.md
- security/A06-Vulnerable-and-Outdated-Components.md
- security/A09-Security-Logging-and-Monitoring-Failures.md

Consultar también cuando corresponda:

- API.md
- Database.md
- Authentication.md
- Authorization.md

---

## No cubre

- Implementación detallada de autenticación.
- Implementación detallada de autorización.
- Configuración de infraestructura.
- Gestión operativa de dependencias.

---

## Riesgos mitigados

La aplicación de este documento busca reducir los riesgos asociados a:

- Diseño inseguro.
- Ausencia o cobertura incompleta de controles de seguridad.
- Ausencia de validaciones críticas.
- Exposición innecesaria de funcionalidades.
- Escalamiento de privilegios por diseño.
- Exposición innecesaria de información.
- Dependencias inseguras entre componentes.
- Arquitecturas difíciles de proteger.
- Ausencia de controles compensatorios.

---

## Reglas obligatorias

### Análisis

- Identificar los activos que deben protegerse antes de diseñar la solución.
- Identificar los actores que interactúan con la solución.
- Identificar los límites de confianza.
- Identificar los datos sensibles involucrados.
- Identificar los posibles abusos del sistema antes de implementar funcionalidades.

### Diseño

- Diseñar la solución aplicando el principio de mínimo privilegio.
- Diseñar controles de seguridad antes de implementar funcionalidades.
- Diseñar mecanismos de autorización independientes de la interfaz de usuario.
- Diseñar componentes con responsabilidades claramente definidas.
- Diseñar flujos de operaciones críticas con denegación por defecto y controles definidos.
- Diseñar mecanismos de recuperación ante errores de seguridad.
- Diseñar procesos considerando escenarios de uso indebido.
- Diseñar la solución para denegar por defecto y no exponer datos ante fallos de seguridad.

### Arquitectura

- Separar responsabilidades entre componentes.
- Minimizar la superficie de ataque.
- Limitar la exposición de funcionalidades administrativas.
- Limitar la exposición de datos sensibles.
- Definir límites claros entre componentes internos y externos.
- Aplicar el principio de defensa en profundidad.
- Reducir dependencias innecesarias entre componentes.

### Controles

- Definir controles preventivos para operaciones críticas.
- Definir controles detectivos para cada riesgo que requiera detección posterior.
- Definir controles de auditoría para autenticaciones, autorizaciones, cambios administrativos y eventos de seguridad.
- Validar que cada riesgo identificado tenga un control asociado.
- Definir controles compensatorios cuando un riesgo no pueda eliminarse.

### Cambios

- Revisar el impacto de seguridad antes de incorporar nuevas funcionalidades.
- Revisar el impacto de seguridad antes de modificar procesos existentes.
- Revisar dependencias de seguridad antes de integrar nuevos componentes.
- Mantener coherencia entre diseño, implementación y controles.

---

## Acciones prohibidas

- Nunca comenzar la implementación sin comprender el problema de negocio.
- Nunca diseñar funcionalidades sin identificar los riesgos asociados.
- Nunca confiar en controles implementados únicamente en la interfaz de usuario.
- Nunca asumir que un componente externo es seguro por defecto.
- Nunca exponer funcionalidades administrativas sin una justificación explícita.
- Nunca diseñar procesos que dependan de validaciones implícitas.
- Nunca utilizar una única capa de protección para operaciones críticas.
- Nunca omitir controles de seguridad por motivos de complejidad.
- Nunca mezclar responsabilidades de negocio y seguridad.
- Nunca reutilizar diseños inseguros conocidos.
- Nunca introducir excepciones permanentes a controles de seguridad.
- Nunca asumir que la implementación corregirá errores de diseño.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] Se identificaron los activos que requieren protección.
- [ ] Se identificaron los límites de confianza.
- [ ] Se analizaron posibles escenarios de abuso.
- [ ] Todos los riesgos identificados poseen controles definidos.
- [ ] La arquitectura aplica el principio de mínimo privilegio.
- [ ] La solución aplica defensa en profundidad mediante controles independientes.
- [ ] Los componentes tienen responsabilidades claramente definidas.
- [ ] No existen controles implementados únicamente en la interfaz de usuario.
- [ ] Las funcionalidades administrativas están protegidas.
- [ ] El diseño fue revisado antes de comenzar la implementación.

---

## Referencias

- OWASP Top 10 2021 – A04: Insecure Design
- OWASP ASVS
- OWASP Application Security Verification Standard
- OWASP Threat Modeling Cheat Sheet
- OWASP Secure Coding Practices
- NIST Secure Software Development Framework (SSDF)
- CWE-657
- CWE-656
- CWE-693
