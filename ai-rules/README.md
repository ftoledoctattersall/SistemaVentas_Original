# AI Rules

> **AI Rules** es un estándar de ingeniería diseñado para que asistentes de inteligencia artificial desarrollen software de forma consistente, segura y mantenible.

Su propósito es proporcionar un conjunto de reglas reutilizables que permitan a herramientas como **Codex**, **Claude Code**, **Cursor**, **GitHub Copilot**, **ChatGPT** y otros asistentes de desarrollo generar soluciones alineadas con buenas prácticas de ingeniería y seguridad.

Este repositorio no contiene prompts.

Contiene **estándares de desarrollo**.

---

# Objetivos

- Reducir errores generados por asistentes de IA.
- Estandarizar el desarrollo de software.
- Incorporar seguridad desde el diseño.
- Mejorar la calidad del código generado.
- Reducir decisiones inconsistentes entre proyectos.
- Permitir reutilizar las mismas reglas en cualquier proyecto.

---

# Filosofía

Toda IA debe:

- Comprender antes de implementar.
- Diseñar antes de programar.
- Aplicar seguridad desde el inicio.
- Minimizar complejidad.
- Maximizar mantenibilidad.
- Evitar suposiciones.
- Justificar excepciones.
- Producir soluciones consistentes.

---

# Estructura del repositorio

```text
ai-rules/

README.md

AI-INSTRUCTIONS.md

AGENTS.md

Security

Engineering

Skills

Templates
```

---

# Flujo de trabajo recomendado

Antes de desarrollar cualquier funcionalidad:

```text
README.md
        │
        ▼
AI-INSTRUCTIONS.md
        │
        ▼
Seleccionar módulos aplicables
        │
        ▼
Security
        │
        ▼
Engineering
        │
        ▼
Skills activas declaradas explícitamente
        │
Implementación
```

---

# Módulos

## Security

Define las reglas de seguridad basadas en **OWASP Top 10 2021**, adaptadas para asistentes de IA.

Incluye:

- security/A01-Broken-Access-Control.md — Broken Access Control
- security/A02-Cryptographic-Failures.md — Cryptographic Failures
- security/A03-Injection.md — Injection
- security/A04-Insecure-Design.md — Insecure Design
- security/A05-Security-Misconfiguration.md — Security Misconfiguration
- security/A06-Vulnerable-and-Outdated-Components.md — Vulnerable and Outdated Components
- security/A07-Identification-and-Authentication-Failures.md — Identification and Authentication Failures
- security/A08-Software-and-Data-Integrity-Failures.md — Software and Data Integrity Failures
- security/A09-Security-Logging-and-Monitoring-Failures.md — Security Logging and Monitoring Failures
- security/A10-Server-Side-Request-Forgery.md — Server-Side Request Forgery

---

## Engineering

Define estándares generales de ingeniería de software.

Incluye los siguientes estándares:

- API.md
- Database.md
- Validation.md
- Error-Handling.md
- Logging.md
- Dependencies.md
- Testing.md
- Authentication.md
- Authorization.md
- Naming.md

---

## Skills

Contiene especializaciones reutilizables y opcionales para tecnologías o disciplinas concretas.

La existencia de una skill no implica su activación. Las skills no se activan automáticamente al detectar tecnologías, dependencias, archivos o frameworks.

La activación se realiza exclusivamente mediante `ACTIVE-SKILLS.md` en el **Consumer Project Root** definido para la ejecución. Este archivo pertenece al proyecto consumidor y no al catálogo central de `ai-rules`.

Si el manifest no existe en esa ubicación, existen cero skills activas.

Consultar:

- `skills/README.md` — gobierno del subsistema.
- `skills/SKILLS-INDEX.md` — catálogo informativo; no activa skills.

---

## Templates

Plantillas para crear nuevos estándares manteniendo la estructura del repositorio.

---

# Cómo utilizar este repositorio

## Para asistentes de IA

1. Leer `AI-INSTRUCTIONS.md`.
2. Identificar la funcionalidad que se desarrollará.
3. Consultar los módulos correspondientes.
4. Aplicar todas las reglas obligatorias.
5. Completar la auto verificación de cada estándar aplicado antes de finalizar.

---

## Para desarrolladores

1. Copiar el repositorio dentro del proyecto.
2. Mantener el repositorio actualizado.
3. Solicitar al asistente de IA que utilice `AI-INSTRUCTIONS.md` antes de comenzar cualquier tarea.
4. Revisar el resultado utilizando la auto verificación de cada estándar aplicado.

---

# Compatibilidad

El repositorio está diseñado para ser utilizado con asistentes como:

- ChatGPT
- Codex
- Claude Code
- Cursor
- GitHub Copilot
- Windsurf

No depende de un proveedor específico.

---

# Principios editoriales

Todos los documentos del repositorio siguen las siguientes reglas:

- Un documento aborda un único tema.
- Las reglas son accionables.
- Las reglas son verificables cuando es posible.
- No existen tutoriales.
- No existen ejemplos de código.
- No existen dependencias tecnológicas innecesarias.
- El lenguaje es consistente en todo el repositorio.

---

# Versionado

El repositorio evoluciona por módulos.

Cada módulo puede publicarse de forma independiente.

Ejemplo:

- Security v1.0
- Engineering v1.0

---

# Estado actual

| Módulo | Estado |
|---------|--------|
| Security | ✅ v1.0 |
| Engineering | ✅ v1.0 |
| Skills | Infraestructura v1; catálogo inicial vacío |
| Templates | 🚧 En construcción |

---

# Licencia

Definida en el archivo `LICENSE`.
