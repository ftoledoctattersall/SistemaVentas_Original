# OWASP A07 – Identification and Authentication Failures

## Objetivo del documento

Este documento forma parte del repositorio **ai-rules** y define las reglas obligatorias que toda IA debe aplicar cuando implemente mecanismos de identificación, autenticación, administración de sesiones o gestión de identidad.

Las reglas de este documento complementan las instrucciones del proyecto y deben aplicarse antes de generar cualquier código relacionado con usuarios, sesiones o credenciales.

---

## Prioridad

**Nivel:** Crítico

Una autenticación implementada sin los controles definidos puede permitir suplantación de identidad, secuestro de sesiones, acceso no autorizado y compromiso total de la aplicación.

---

## Propósito

Garantizar que toda identidad sea autenticada mediante los controles definidos, que las sesiones sean administradas con controles de seguridad y que únicamente usuarios válidamente autenticados puedan acceder a recursos protegidos.

---

## Cuándo consultar este documento

Consultar este documento antes de implementar o modificar:

- Inicio de sesión.
- Registro de usuarios.
- Recuperación de contraseñas.
- Cambio de contraseña.
- MFA (Autenticación Multifactor).
- OAuth2.
- OpenID Connect.
- SAML.
- JWT.
- Refresh Tokens.
- Cookies de autenticación.
- Gestión de sesiones.
- APIs autenticadas.
- Single Sign-On (SSO).
- Integraciones con proveedores de identidad.
- Servicios de autenticación externos.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- security/A01-Broken-Access-Control.md
- security/A02-Cryptographic-Failures.md
- security/A09-Security-Logging-and-Monitoring-Failures.md

Consultar también cuando corresponda:

- engineering/Authentication.md
- engineering/Logging.md

---

## No cubre

- Autorización de recursos.
- Diseño de permisos.
- Gestión general de secretos.
- Configuración de infraestructura.

---

## Riesgos mitigados

La aplicación de este documento busca reducir los riesgos asociados a:

- Suplantación de identidad.
- Robo de sesiones.
- Robo de credenciales.
- Session Fixation.
- Session Hijacking.
- Fuerza bruta.
- Credential Stuffing.
- Password Spraying.
- Uso indebido de tokens.
- Controles de autenticación ausentes o incompletos.
- Gestión insegura de sesiones.

---

## Reglas obligatorias

### Identidad

- Asignar una identidad única a cada usuario.
- Validar la identidad antes de crear una sesión autenticada.
- Obtener la identidad únicamente desde mecanismos oficiales de autenticación.
- Evitar identidades compartidas cuando exista una alternativa individual.

### Autenticación

- Autenticar todas las operaciones que requieran identidad.
- Requerir credenciales válidas antes de generar una sesión.
- Validar todas las credenciales en el servidor.
- Invalidar inmediatamente credenciales revocadas.
- Aplicar autenticación multifactor cuando el nivel de riesgo lo requiera.
- Proteger mediante reautenticación las operaciones clasificadas como críticas por el proyecto.

### Contraseñas

- Exigir contraseñas conforme a la política de seguridad del proyecto.
- Validar la contraseña actual antes de permitir su modificación.
- Permitir el cambio de contraseña únicamente al usuario autenticado o mediante un proceso de recuperación seguro.
- Invalidar sesiones activas cuando una contraseña sea modificada si la política del proyecto así lo establece.

### Sesiones

- Generar identificadores de sesión utilizando mecanismos criptográficamente seguros.
- Regenerar el identificador de sesión después de una autenticación exitosa.
- Invalidar la sesión al cerrar sesión.
- Invalidar sesiones expiradas.
- Limitar la duración de las sesiones según el riesgo de la operación.
- Configurar tiempos de inactividad para el cierre automático de sesión.
- Validar la sesión en cada solicitud autenticada.

### Tokens

- Validar firma, vigencia y emisor de todos los tokens que incluyan esos campos.
- Rechazar tokens expirados.
- Rechazar tokens revocados.
- Limitar la vida útil de los tokens.
- Utilizar canales seguros para transmitir tokens.

### Protección contra ataques

- Limitar intentos consecutivos de autenticación fallidos.
- Registrar intentos fallidos de autenticación.
- Detectar comportamientos anómalos de autenticación mediante reglas de detección configuradas.
- Aplicar controles contra ataques automatizados en los endpoints de autenticación.

### Auditoría

- Registrar autenticaciones exitosas.
- Registrar autenticaciones fallidas.
- Registrar bloqueos de cuentas.
- Registrar cambios de contraseña.
- Registrar cierres de sesión.
- Registrar revocación de sesiones.
- Registrar eventos relacionados con MFA.

---

## Acciones prohibidas

- Nunca almacenar contraseñas en texto plano.
- Nunca reutilizar identificadores de sesión después de autenticar un usuario.
- Nunca mantener sesiones indefinidamente.
- Nunca aceptar tokens expirados.
- Nunca aceptar tokens con firmas inválidas.
- Nunca transmitir credenciales mediante canales inseguros.
- Nunca registrar contraseñas en archivos de log.
- Nunca registrar tokens completos.
- Nunca permitir autenticación utilizando credenciales revocadas.
- Nunca permitir sesiones compartidas sin una justificación documentada.
- Nunca revelar si un usuario existe durante el proceso de autenticación cuando ello incremente el riesgo de enumeración.
- Nunca omitir la invalidación de sesiones después del cierre de sesión.
- Nunca utilizar preguntas de seguridad como único mecanismo de recuperación de cuenta.
- Nunca deshabilitar controles de autenticación por motivos de desarrollo en ambientes productivos.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] Todas las autenticaciones se realizan en el servidor.
- [ ] Todas las sesiones poseen identificadores seguros.
- [ ] Los identificadores de sesión se regeneran después del inicio de sesión.
- [ ] Las sesiones expiran automáticamente.
- [ ] Las sesiones se invalidan al cerrar sesión.
- [ ] Los tokens validan firma y vigencia.
- [ ] Las contraseñas nunca se almacenan en texto plano.
- [ ] Los intentos fallidos de autenticación son registrados.
- [ ] Existen controles contra intentos repetitivos cuando corresponde.
- [ ] Las operaciones críticas requieren reautenticación cuando el riesgo lo justifica.

---

## Referencias

- OWASP Top 10 2021 – A07: Identification and Authentication Failures
- OWASP ASVS
- OWASP Authentication Cheat Sheet
- OWASP Session Management Cheat Sheet
- OWASP Password Storage Cheat Sheet
- NIST SP 800-63 Digital Identity Guidelines
- NIST Secure Software Development Framework (SSDF)
- CWE-287
- CWE-307
- CWE-384
- CWE-521
