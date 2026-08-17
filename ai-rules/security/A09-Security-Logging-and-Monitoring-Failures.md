# OWASP A09 – Security Logging and Monitoring Failures

## Objetivo del documento

Este documento forma parte del repositorio **ai-rules** y define las reglas obligatorias que toda IA debe aplicar al implementar mecanismos de registro, monitoreo, auditoría y detección de eventos de seguridad.

Las reglas de este documento complementan las instrucciones del proyecto y deben aplicarse antes de generar cualquier código relacionado con logs, auditoría, monitoreo, observabilidad o respuesta a incidentes.

---

## Prioridad

**Nivel:** Crítico

El incumplimiento de cualquiera de las reglas de este documento puede impedir la detección de ataques, dificultar investigaciones de incidentes y comprometer la capacidad de respuesta ante eventos de seguridad.

---

## Propósito

Garantizar que los eventos de autenticación, autorización, administración, configuración, errores de seguridad y sesiones sean registrados, monitoreados y preservados de forma confiable, permitiendo detectar comportamientos anómalos, investigar incidentes y mantener trazabilidad de las operaciones.

---

## Cuándo consultar este documento

Consultar este documento antes de implementar o modificar:

- APIs.
- Microservicios.
- Aplicaciones Web.
- Procesos Batch.
- Integraciones.
- Autenticación.
- Autorización.
- Auditoría.
- Logging.
- Observabilidad.
- Telemetría.
- Monitoreo.
- Alertas.
- SIEM.
- Procesos críticos.
- Operaciones administrativas.
- Cambios de configuración.
- Procesos automáticos.
- Servicios Cloud.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- security/A01-Broken-Access-Control.md
- security/A02-Cryptographic-Failures.md
- security/A07-Identification-and-Authentication-Failures.md
- security/A08-Software-and-Data-Integrity-Failures.md

Consultar también cuando corresponda:

- engineering/Logging.md

---

## No cubre

- Autenticación.
- Autorización.
- Gestión de secretos.
- Configuración de infraestructura.

---

## Riesgos mitigados

La aplicación de este documento busca reducir los riesgos asociados a:

- Falta de trazabilidad.
- Incidentes no detectados.
- Ataques sin evidencia.
- Eliminación de evidencias.
- Manipulación de registros.
- Imposibilidad de realizar auditorías.
- Respuesta tardía ante incidentes.
- Eventos sin monitoreo configurado.
- Pérdida de registros de seguridad.

---

## Reglas obligatorias

### Registro de eventos

- Registrar eventos de autenticación, autorización, administración, configuración, errores de seguridad y sesiones.
- Registrar autenticaciones exitosas y fallidas.
- Registrar autorizaciones denegadas.
- Registrar cambios de permisos y roles.
- Registrar operaciones administrativas.
- Registrar cambios de configuración.
- Registrar errores relacionados con controles de seguridad.
- Registrar intentos de acceso a recursos protegidos.
- Registrar bloqueos de cuentas.
- Registrar eventos relacionados con sesiones.

### Información registrada

- Registrar fecha y hora utilizando una fuente de tiempo consistente.
- Registrar el identificador del usuario cuando exista.
- Registrar el identificador de sesión en los eventos asociados a una sesión.
- Registrar el origen de la solicitud cuando esté disponible.
- Registrar el recurso afectado.
- Registrar el resultado de la operación.
- Registrar un identificador de correlación para facilitar la trazabilidad entre componentes.

### Protección de registros

- Proteger los registros contra modificaciones no autorizadas.
- Limitar el acceso a los registros únicamente al personal autorizado.
- Mantener políticas de retención de registros.
- Garantizar la disponibilidad de los registros durante investigaciones.
- Separar los registros de auditoría de los registros funcionales mediante destinos de almacenamiento independientes.

### Monitoreo

- Supervisar eventos críticos de seguridad mediante reglas de alerta configuradas.
- Generar alertas para eventos de alto riesgo.
- Detectar patrones anómalos cuando existan mecanismos disponibles.
- Correlacionar eventos relacionados durante investigaciones.
- Revisar los registros de seguridad según la frecuencia definida por el proyecto.

### Respuesta

- Conservar los registros necesarios para reconstruir cronológicamente un incidente.
- Facilitar la reconstrucción cronológica de incidentes.
- Mantener trazabilidad entre eventos relacionados.
- Registrar las acciones ejecutadas durante la respuesta a incidentes.

---

## Acciones prohibidas

- Nunca registrar contraseñas.
- Nunca registrar secretos.
- Nunca registrar claves criptográficas.
- Nunca registrar tokens completos.
- Nunca registrar información sensible innecesaria.
- Nunca eliminar registros para ocultar errores o incidentes.
- Nunca sobrescribir registros de auditoría sin respetar la política de retención.
- Nunca permitir modificaciones no autorizadas de los registros.
- Nunca deshabilitar el registro de eventos críticos en producción.
- Nunca depender exclusivamente de mensajes mostrados al usuario como mecanismo de auditoría.
- Nunca registrar información que infrinja políticas de privacidad o protección de datos.
- Nunca asumir que un error no necesita ser registrado porque fue controlado.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] Todos los eventos críticos generan registros.
- [ ] Todas las autenticaciones generan auditoría.
- [ ] Todas las autorizaciones denegadas generan auditoría.
- [ ] Todas las operaciones administrativas generan auditoría.
- [ ] Los registros contienen fecha y hora consistentes.
- [ ] Los registros incluyen un identificador de correlación.
- [ ] Los registros no contienen información sensible innecesaria.
- [ ] Los registros están protegidos contra modificaciones.
- [ ] Existen mecanismos para generar alertas ante eventos críticos.
- [ ] Los registros permiten reconstruir un incidente de seguridad.

---

## Referencias

- OWASP Top 10 2021 – A09: Security Logging and Monitoring Failures
- OWASP ASVS
- OWASP Logging Cheat Sheet
- OWASP Application Logging Vocabulary
- NIST SP 800-61 Computer Security Incident Handling Guide
- NIST Secure Software Development Framework (SSDF)
- CWE-117
- CWE-223
- CWE-778
- CWE-779
- CWE-532
