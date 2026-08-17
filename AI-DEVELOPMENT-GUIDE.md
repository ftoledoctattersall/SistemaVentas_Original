# Guía breve para trabajar con agentes IA

## 1. Objetivo

Los desarrolladores pueden usar Codex, Claude Code, Cursor, Copilot u otros agentes compatibles para trabajar en el proyecto.

No es necesario repetir en cada solicitud la arquitectura, la seguridad, el branding, el testing, las skills ni las convenciones técnicas. El agente debe descubrir esas reglas desde el repositorio y aplicarlas durante el trabajo.

## 2. Regla principal

```text
El desarrollador describe QUÉ necesita.

El agente inspecciona el repositorio y determina CÓMO implementarlo.
```

## 3. Prompt para implementar una funcionalidad

```text
Implementa la siguiente funcionalidad:

[DESCRIBIR QUÉ SE NECESITA]

Inspecciona el repositorio y sigue sus instrucciones.

Implementa, prueba y valida los cambios.

Al finalizar resume los cambios realizados y cualquier riesgo o decisión pendiente.
```

Ejemplo:

```text
Implementa la siguiente funcionalidad:

En el POS, permite aplicar un descuento porcentual a una línea de venta. El descuento no puede superar el máximo autorizado para el usuario y debe verse en el resumen antes de confirmar la venta.

Inspecciona el repositorio y sigue sus instrucciones.

Implementa, prueba y valida los cambios.

Al finalizar resume los cambios realizados y cualquier riesgo o decisión pendiente.
```

## 4. Prompt para corregir un problema

```text
Corrige el siguiente problema:

[DESCRIBIR EL ERROR]

Identifica primero la causa raíz.

Inspecciona el repositorio y sigue sus instrucciones.

Corrige, prueba y valida.

Al finalizar explica la causa encontrada y la solución aplicada.
```

## 5. Prompt para análisis read-only

```text
Analiza lo siguiente:

[DESCRIBIR EL REQUERIMIENTO O PROBLEMA]

No modifiques código.

Inspecciona el repositorio y sigue sus instrucciones.

Entrega hallazgos, riesgos y recomendación.
```

## 6. Qué debe entregar el desarrollador

El desarrollador debe concentrarse en la información funcional o comercial disponible:

- comportamiento esperado;
- reglas de negocio conocidas;
- datos requeridos;
- restricciones;
- criterios de aceptación;
- excepciones funcionales.

Cuanto más concreto sea el resultado esperado, más fácil será validar el trabajo.

## 7. Qué no necesita especificar normalmente

Normalmente, el desarrollador no necesita definir arquitectura, capas, DTOs, servicios, componentes React, Material UI, librerías, colores, tipografías, patrones, OWASP, logging, manejo estándar de errores, testing ni skills.

Estas decisiones deben resolverse desde las instrucciones, la arquitectura y la implementación existente del repositorio.

## 8. Cuándo el agente debe detenerse

El agente no debe inventar decisiones cuando falte información funcional, comercial o de seguridad. Debe identificar claramente la decisión pendiente antes de continuar con la parte afectada.

Esto aplica, por ejemplo, cuando no está definido:

- el significado de un estado;
- quién puede realizar una acción;
- la fuente oficial de un dato;
- una fórmula o cálculo;
- una integración todavía no definida.

## 9. Flujo conceptual

```text
REQUERIMIENTO
     ↓
AGENTS.md
     ↓
ai-rules
     ↓
ACTIVE-SKILLS
     ↓
arquitectura + branding
     ↓
AGENTE
     ↓
IMPLEMENTACIÓN
```

En resumen: el desarrollador describe qué necesita, el repositorio define cómo debe desarrollarse y el agente inspecciona y aplica ambas cosas.
