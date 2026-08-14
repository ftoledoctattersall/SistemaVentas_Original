# Security Module

## Propósito

El módulo **Security** define las reglas de seguridad que toda IA debe aplicar durante el análisis, diseño, implementación y mantenimiento de software.

Su objetivo es transformar los principios de seguridad de **OWASP Top 10 2021** en instrucciones operativas, consistentes y reutilizables para asistentes de desarrollo como Codex, Claude Code, Cursor, GitHub Copilot y ChatGPT.

Este módulo no reemplaza OWASP, OWASP ASVS ni otros estándares de seguridad. Su propósito es convertir dichos estándares en reglas prácticas para la generación de software.

---

## Alcance

Este módulo cubre exclusivamente las diez categorías definidas por **OWASP Top 10 2021**.

Cada documento representa una única categoría de seguridad y debe consultarse únicamente cuando la funcionalidad desarrollada se encuentre dentro de su ámbito de aplicación.

---

## Organización del módulo

| Documento | Responsabilidad principal |
|-----------|---------------------------|
| security/A01-Broken-Access-Control.md | Broken Access Control |
| security/A02-Cryptographic-Failures.md | Cryptographic Failures |
| security/A03-Injection.md | Injection |
| security/A04-Insecure-Design.md | Insecure Design |
| security/A05-Security-Misconfiguration.md | Security Misconfiguration |
| security/A06-Vulnerable-and-Outdated-Components.md | Vulnerable and Outdated Components |
| security/A07-Identification-and-Authentication-Failures.md | Identification and Authentication Failures |
| security/A08-Software-and-Data-Integrity-Failures.md | Software and Data Integrity Failures |
| security/A09-Security-Logging-and-Monitoring-Failures.md | Security Logging and Monitoring Failures |
| security/A10-Server-Side-Request-Forgery.md | Server-Side Request Forgery (SSRF) |

La navegación entre documentos se encuentra definida en **security/SECURITY-INDEX.md**.

---

## Principios editoriales

Todos los documentos del módulo siguen las siguientes reglas:

- Una categoría OWASP por documento.
- Un único propósito por documento.
- Lenguaje imperativo.
- Reglas accionables.
- Reglas verificables cuando sea posible.
- Sin ejemplos de implementación.
- Sin tutoriales.
- Sin explicaciones teóricas extensas.
- Independencia de tecnologías específicas, salvo cuando una referencia tecnológica facilite la correcta aplicación de una regla.
- Estructura uniforme en todos los documentos.

---

## Cómo utilizar este módulo

Antes de implementar cualquier funcionalidad:

1. Identificar la funcionalidad que se va a desarrollar.
2. Consultar **security/SECURITY-INDEX.md**.
3. Identificar los documentos aplicables.
4. Leer únicamente los documentos relacionados con la funcionalidad.
5. Aplicar todas las **Reglas obligatorias**.
6. Evitar todas las **Acciones prohibidas**.
7. Completar la **Auto verificación** antes de finalizar la implementación.

---

## Relación entre documentos

Cada documento posee un ámbito de responsabilidad específico.

Las reglas comunes entre varias categorías deberán pertenecer únicamente al documento propietario y el resto de documentos únicamente podrán referenciarlo.

El objetivo es evitar duplicidad, contradicciones y responsabilidades compartidas.

---

## Exclusiones

Este módulo no define:

- Estándares de arquitectura.
- Estándares de desarrollo.
- Convenciones de codificación.
- Patrones de diseño.
- Reglas de negocio.
- Configuración específica de plataformas.
- Procesos DevOps.
- Procedimientos operacionales.

Estos aspectos se documentan en otros módulos del repositorio.

---

## Audiencia

Este módulo está dirigido a:

- Asistentes de desarrollo basados en IA.
- Desarrolladores.
- Arquitectos de software.
- Revisores de código.
- Equipos DevSecOps.
- Auditores técnicos.

---

## Dependencias

Este módulo se complementa con:

- `security/SECURITY-INDEX.md`
- Engineering
- templates/README.md

---

## Convenciones

En todos los documentos del módulo:

- **Reglas obligatorias** definen requisitos que deben cumplirse.
- **Acciones prohibidas** definen comportamientos que nunca deben implementarse.
- **Auto verificación** define los criterios mínimos que deben revisarse antes de considerar una implementación como terminada.

---

## Objetivo del módulo

Al finalizar la implementación de una funcionalidad, una IA debe haber aplicado automáticamente las reglas de seguridad correspondientes sin necesidad de interpretar documentación extensa ni consultar manuales externos.

Este módulo busca proporcionar una guía consistente, práctica y reutilizable para desarrollar software más seguro desde la primera línea de código.
