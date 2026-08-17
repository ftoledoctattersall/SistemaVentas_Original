# Security Index

## Enlaces directos

- [A01 Broken Access Control](A01-Broken-Access-Control.md)
- [A02 Cryptographic Failures](A02-Cryptographic-Failures.md)
- [A03 Injection](A03-Injection.md)
- [A04 Insecure Design](A04-Insecure-Design.md)
- [A05 Security Misconfiguration](A05-Security-Misconfiguration.md)
- [A06 Vulnerable and Outdated Components](A06-Vulnerable-and-Outdated-Components.md)
- [A07 Identification and Authentication Failures](A07-Identification-and-Authentication-Failures.md)
- [A08 Software and Data Integrity Failures](A08-Software-and-Data-Integrity-Failures.md)
- [A09 Security Logging and Monitoring Failures](A09-Security-Logging-and-Monitoring-Failures.md)
- [A10 Server-Side Request Forgery](A10-Server-Side-Request-Forgery.md)

## Objetivo

Este documento permite identificar rápidamente qué documentos del módulo **Security** deben consultarse antes de implementar una funcionalidad.

No contiene reglas de seguridad.

Su única responsabilidad es dirigir a la IA hacia los documentos adecuados según la funcionalidad que se desarrollará.

---

## Cómo utilizar este índice

Antes de generar código:

1. Identificar la funcionalidad que se implementará.
2. Buscar dicha funcionalidad en este índice.
3. Leer todos los documentos indicados.
4. Aplicar todas las reglas obligatorias de esos documentos.
5. Ejecutar la auto verificación de cada documento antes de finalizar la implementación.

---

# Funcionalidades

## Control de acceso

Consultar:

- security/A01-Broken-Access-Control.md

---

## Roles y permisos

Consultar:

- security/A01-Broken-Access-Control.md

---

## Usuarios

Consultar:

- security/A01-Broken-Access-Control.md
- security/A07-Identification-and-Authentication-Failures.md

---

## Multiempresa

Consultar:

- security/A01-Broken-Access-Control.md

---

## Multitenancy

Consultar:

- security/A01-Broken-Access-Control.md

---

## Inicio de sesión

Consultar:

- security/A07-Identification-and-Authentication-Failures.md

---

## Registro de usuarios

Consultar:

- security/A07-Identification-and-Authentication-Failures.md

---

## Recuperación de contraseña

Consultar:

- security/A07-Identification-and-Authentication-Failures.md

---

## Cambio de contraseña

Consultar:

- security/A07-Identification-and-Authentication-Failures.md

---

## Autenticación

Consultar:

- security/A07-Identification-and-Authentication-Failures.md

---

## JWT

Consultar:

- security/A02-Cryptographic-Failures.md
- security/A07-Identification-and-Authentication-Failures.md

---

## Refresh Tokens

Consultar:

- security/A02-Cryptographic-Failures.md
- security/A07-Identification-and-Authentication-Failures.md

---

## Cookies de autenticación

Consultar:

- security/A02-Cryptographic-Failures.md
- security/A07-Identification-and-Authentication-Failures.md

---

## API Keys

Consultar:

- security/A02-Cryptographic-Failures.md

---

## Secretos

Consultar:

- security/A02-Cryptographic-Failures.md

---

## Certificados

Consultar:

- security/A02-Cryptographic-Failures.md

---

## HTTPS

Consultar:

- security/A02-Cryptographic-Failures.md
- security/A05-Security-Misconfiguration.md

---

## TLS

Consultar:

- security/A02-Cryptographic-Failures.md
- security/A05-Security-Misconfiguration.md

---

## SQL

Consultar:

- security/A03-Injection.md

---

## Entity Framework

Consultar:

- security/A03-Injection.md

---

## Dapper

Consultar:

- security/A03-Injection.md

---

## ADO.NET

Consultar:

- security/A03-Injection.md

---

## Stored Procedures

Consultar:

- security/A03-Injection.md

---

## NoSQL

Consultar:

- security/A03-Injection.md

---

## LDAP

Consultar:

- security/A03-Injection.md

---

## XPath

Consultar:

- security/A03-Injection.md

---

## XML

Consultar:

- security/A03-Injection.md

---

## GraphQL

Consultar:

- security/A03-Injection.md

---

## HTML generado dinámicamente

Consultar:

- security/A03-Injection.md

---

## JavaScript generado dinámicamente

Consultar:

- security/A03-Injection.md

---

## Arquitectura

Consultar:

- security/A04-Insecure-Design.md

---

## Diseño de soluciones

Consultar:

- security/A04-Insecure-Design.md

---

## Modelado de procesos

Consultar:

- security/A04-Insecure-Design.md

---

## APIs REST

Consultar:

- security/A01-Broken-Access-Control.md
- security/A03-Injection.md
- security/A05-Security-Misconfiguration.md
- security/A09-Security-Logging-and-Monitoring-Failures.md

---

## APIs GraphQL

Consultar:

- security/A01-Broken-Access-Control.md
- security/A03-Injection.md
- security/A05-Security-Misconfiguration.md
- security/A09-Security-Logging-and-Monitoring-Failures.md

---

## Microservicios

Consultar:

- security/A04-Insecure-Design.md
- security/A05-Security-Misconfiguration.md
- security/A09-Security-Logging-and-Monitoring-Failures.md
- security/A10-Server-Side-Request-Forgery.md

---

## Configuración de aplicaciones

Consultar:

- security/A05-Security-Misconfiguration.md

---

## CORS

Consultar:

- security/A05-Security-Misconfiguration.md

---

## Variables de entorno

Consultar:

- security/A05-Security-Misconfiguration.md

---

## Docker

Consultar:

- security/A05-Security-Misconfiguration.md
- security/A06-Vulnerable-and-Outdated-Components.md

---

## Kubernetes

Consultar:

- security/A05-Security-Misconfiguration.md
- security/A06-Vulnerable-and-Outdated-Components.md

---

## Dependencias

Consultar:

- security/A06-Vulnerable-and-Outdated-Components.md

---

## NuGet

Consultar:

- security/A06-Vulnerable-and-Outdated-Components.md

---

## NPM

Consultar:

- security/A06-Vulnerable-and-Outdated-Components.md

---

## Maven

Consultar:

- security/A06-Vulnerable-and-Outdated-Components.md

---

## Gradle

Consultar:

- security/A06-Vulnerable-and-Outdated-Components.md

---

## Imágenes Docker

Consultar:

- security/A06-Vulnerable-and-Outdated-Components.md

---

## GitHub Actions

Consultar:

- security/A08-Software-and-Data-Integrity-Failures.md

---

## Azure DevOps

Consultar:

- security/A08-Software-and-Data-Integrity-Failures.md

---

## GitLab CI

Consultar:

- security/A08-Software-and-Data-Integrity-Failures.md

---

## Jenkins

Consultar:

- security/A08-Software-and-Data-Integrity-Failures.md

---

## CI/CD

Consultar:

- security/A08-Software-and-Data-Integrity-Failures.md

---

## Supply Chain

Consultar:

- security/A08-Software-and-Data-Integrity-Failures.md

---

## Logging

Consultar:

- security/A09-Security-Logging-and-Monitoring-Failures.md

---

## Auditoría

Consultar:

- security/A09-Security-Logging-and-Monitoring-Failures.md

---

## Observabilidad

Consultar:

- security/A09-Security-Logging-and-Monitoring-Failures.md

---

## Monitoreo

Consultar:

- security/A09-Security-Logging-and-Monitoring-Failures.md

---

## Alertas

Consultar:

- security/A09-Security-Logging-and-Monitoring-Failures.md

---

## HTTP Client

Consultar:

- security/A10-Server-Side-Request-Forgery.md

---

## HttpClient (.NET)

Consultar:

- security/A10-Server-Side-Request-Forgery.md

---

## Webhooks

Consultar:

- security/A08-Software-and-Data-Integrity-Failures.md
- security/A10-Server-Side-Request-Forgery.md

---

## Integraciones REST

Consultar:

- security/A10-Server-Side-Request-Forgery.md

---

## Integraciones SOAP

Consultar:

- security/A10-Server-Side-Request-Forgery.md

---

## Descarga de archivos

Consultar:

- security/A10-Server-Side-Request-Forgery.md

---

## URLs proporcionadas por usuarios

Consultar:

- security/A10-Server-Side-Request-Forgery.md

---

## DNS

Consultar:

- security/A10-Server-Side-Request-Forgery.md

---

## Reverse Proxy

Consultar:

- security/A05-Security-Misconfiguration.md
- security/A10-Server-Side-Request-Forgery.md

---

## Balanceadores

Consultar:

- security/A05-Security-Misconfiguration.md
- security/A10-Server-Side-Request-Forgery.md

---

# Cobertura del módulo

| Categoría OWASP Top 10 | Documento |
|-------------------------|-----------|
| Categoría OWASP Top 10 | Documento |
|-------------------------|-----------|
| A01 – Broken Access Control | security/A01-Broken-Access-Control.md |
| A02 – Cryptographic Failures | security/A02-Cryptographic-Failures.md |
| A03 – Injection | security/A03-Injection.md |
| A04 – Insecure Design | security/A04-Insecure-Design.md |
| A05 – Security Misconfiguration | security/A05-Security-Misconfiguration.md |
| A06 – Vulnerable and Outdated Components | security/A06-Vulnerable-and-Outdated-Components.md |
| A07 – Identification and Authentication Failures | security/A07-Identification-and-Authentication-Failures.md |
| A08 – Software and Data Integrity Failures | security/A08-Software-and-Data-Integrity-Failures.md |
| A09 – Security Logging and Monitoring Failures | security/A09-Security-Logging-and-Monitoring-Failures.md |
| A10 – Server-Side Request Forgery | security/A10-Server-Side-Request-Forgery.md |

---

## Regla general

Si una funcionalidad involucra varias categorías de seguridad, deberán consultarse **todos los documentos correspondientes** antes de generar código.

Cuando dos documentos parezcan aplicar al mismo problema, deberá utilizarse el documento cuya responsabilidad principal corresponda a dicho tema y utilizar los demás únicamente como complemento.

En caso de duda, deberá prevalecer la alternativa más restrictiva desde el punto de vista de la seguridad.
