# AGENTS.md

## Objetivo

Este archivo define el comportamiento obligatorio que debe seguir cualquier agente de inteligencia artificial al trabajar en un proyecto que utilice el repositorio **ai-rules**.

No contiene estándares de ingeniería.

No contiene reglas de seguridad.

Su única responsabilidad es indicar cómo utilizar correctamente este repositorio.

---

# Punto de entrada obligatorio

Antes de realizar cualquier tarea, leer obligatoriamente:

1. `AI-INSTRUCTIONS.md`

Ese documento constituye la autoridad principal para el comportamiento del agente.

---

# Flujo obligatorio

Para cada nueva tarea:

1. Comprender completamente la solicitud.
2. Identificar las tecnologías involucradas.
3. Identificar los módulos aplicables.
4. Aplicar el flujo de skills opcionales definido en `AI-INSTRUCTIONS.md`.
5. Consultar los estándares correspondientes.
6. Planificar la solución.
7. Implementar.
8. Ejecutar la auto verificación.
9. Informar riesgos o incumplimientos detectados.

Nunca comenzar implementando código sin comprender previamente el problema.

Identificar una tecnología no activa ninguna skill. La existencia de una skill tampoco implica su activación.

---

# Selección de estándares

## Seguridad

Consultar:

`security/SECURITY-INDEX.md`

y leer todos los documentos indicados.

---

## Ingeniería

Consultar:

`engineering/ENGINEERING-INDEX.md`

cuando exista.

---

## Skills

Las skills son opcionales y se aplican exclusivamente mediante el flujo de activación explícita definido en `AI-INSTRUCTIONS.md`.

Consultar `skills/README.md` para su gobierno detallado.

---

# Restricciones permanentes

El agente nunca deberá:

- Inventar requisitos de negocio.
- Inventar contratos públicos.
- Inventar configuraciones.
- Inventar permisos.
- Ignorar estándares del repositorio.
- Omitir controles de seguridad.
- Introducir dependencias innecesarias.
- Modificar componentes no relacionados.
- Implementar soluciones sin comprender previamente el problema.

---

# Conflictos

Si dos documentos parecen contradictorios:

1. Aplicar la alternativa más segura.
2. Preservar la estabilidad del sistema.
3. Informar el conflicto al usuario.
4. Nunca resolver conflictos mediante suposiciones.

Cuando una decisión de autorización requerida no pueda derivarse de requisitos o autorizaciones existentes, tratarla como pendiente según `security/A01-Broken-Access-Control.md` y `engineering/Authorization.md`. Nunca resolverla mediante suposiciones.

---

# Finalización

Antes de finalizar una tarea verificar que:

- Se aplicaron los estándares correspondientes.
- No existen cambios innecesarios.
- No existen riesgos evidentes.
- La solución es consistente con el proyecto.
- Se completó la auto verificación correspondiente.

---

# Alcance

Este archivo únicamente define el comportamiento del agente.

Las reglas técnicas, de seguridad y de ingeniería pertenecen exclusivamente a los módulos del repositorio y no deben duplicarse aquí.
