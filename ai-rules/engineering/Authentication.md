# Authentication Standard

## Objetivo

Este documento define el estándar de ingeniería que toda IA debe seguir para implementar mecanismos de autenticación consistentes, mantenibles y desacoplados.

Su objetivo es garantizar una estrategia uniforme para verificar la identidad de usuarios, sistemas y servicios.

No define controles de seguridad.

Las reglas de seguridad pertenecen exclusivamente al módulo **Security**.

---

## Prioridad

**Nivel:** Crítico

Todo mecanismo de autenticación implementado dentro de un proyecto que utilice este repositorio deberá cumplir este estándar.

---

## Propósito

Garantizar una implementación uniforme de autenticación que facilite la evolución, interoperabilidad y mantenimiento del sistema.

---

## Cuándo consultar este documento

Consultar este documento antes de implementar o modificar:

- Inicio de sesión.
- Autenticación de usuarios.
- Autenticación de servicios.
- JWT.
- OAuth2.
- OpenID Connect.
- API Keys.
- Cookies de autenticación.
- Refresh Tokens.
- Single Sign-On.
- Identity Providers.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- API.md
- Validation.md
- Error-Handling.md
- Logging.md
- Testing.md

Consultar también cuando corresponda:

- security/A02-Cryptographic-Failures.md
- security/A07-Identification-and-Authentication-Failures.md

---

## No cubre

Este documento no define:

- Autorización.
- Roles.
- Permisos.
- Reglas de negocio.
- Controles de seguridad.
- Gestión de identidades corporativas.

---

## Principios

Todo mecanismo de autenticación debe ser:

- Consistente.
- Desacoplado.
- Extensible.
- Fácil de reemplazar.
- Independiente del proveedor cuando sea posible.
- Fácil de probar.

---

## Reglas obligatorias

### Diseño

- Separar autenticación de autorización.
- Centralizar la autenticación.
- Mantener una única estrategia de autenticación por contexto funcional.
- Diseñar componentes reutilizables.

### Implementación

- No acoplar la aplicación a un proveedor específico cuando pueda evitarse.
- Utilizar contratos bien definidos entre la aplicación y el mecanismo de autenticación.
- Mantener el contexto del usuario autenticado durante toda la operación.

### Tokens

- Tratar los tokens como datos opacos.
- Evitar dependencias innecesarias con el formato interno de un token.
- Mantener una estrategia uniforme para la propagación de identidad.

### Evolución

- Permitir sustituir el proveedor de autenticación con el menor impacto posible.
- Evitar dependencias permanentes de implementaciones concretas.

---

## Acciones prohibidas

- Nunca mezclar autenticación con autorización.
- Nunca distribuir lógica de autenticación entre múltiples componentes.
- Nunca depender del formato interno de un token.
- Nunca acoplar la lógica de negocio al mecanismo de autenticación.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] La autenticación permanece separada de la autorización.
- [ ] Existe una estrategia uniforme.
- [ ] El mecanismo es desacoplado.
- [ ] Los tokens son tratados como datos opacos.
- [ ] Los estándares relacionados fueron consultados.

---

## Referencias

- OAuth 2.0
- OpenID Connect
- RFC 7519 — JSON Web Token (JWT)
- NIST Digital Identity Guidelines