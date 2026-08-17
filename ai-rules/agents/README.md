# Agents

## Propósito

Un **Agent** es un rol de ejecución con una responsabilidad delimitada. Utiliza las Rules aplicables y las Skills activas para realizar una tarea o parte de ella.

Un Agent no es una Rule, una Skill ni una fuente de requisitos.

## Cuándo utilizar un Agent

Utilizar un Agent cuando una responsabilidad pueda delegarse con alcance, entradas, procedimiento, validación y salida claramente definidos. No crear Agents para tareas triviales ni para anticipar necesidades futuras.

Los Agents deben utilizarse selectivamente, según el riesgo real del cambio. Usar `backend-reviewer` para cambios backend, `frontend-reviewer` para cambios frontend, `security-reviewer` para cambios sensibles y `architecture-reviewer` para cambios estructurales o transversales. Un cambio puede requerir más de un reviewer cuando su alcance lo justifique; no ejecutar los cuatro por defecto.

## Relación con Rules

Todo Agent debe cumplir las Core, Security y Engineering Rules aplicables. No puede omitirlas, debilitarlas, reemplazarlas, neutralizarlas ni redefinirlas.

Un Agent tampoco puede inventar requisitos de negocio, permisos, contratos, configuraciones o decisiones arquitectónicas. Si una decisión necesaria no está definida, debe reportarla y mantenerla pendiente.

## Relación con Skills

Un Agent puede requerir Skills, pero sólo puede utilizar las que ya estén activas explícitamente mediante `ACTIVE-SKILLS.md` en el Consumer Project Root.

Declarar una skill en un `AGENT.md` no la activa. Un Agent no activa skills transitivamente y `Related Skills` continúa siendo informativo.

## Autoridad y conflictos

La precedencia aplicable es:

```text
Security Rules
→ Engineering Rules
→ Skills activas
→ Agent
```

Ante un conflicto, el Agent debe conservar la alternativa segura, respetar las Rules superiores, reportar el conflicto y no inventar la decisión ausente.

## Estructura recomendada de futuros AGENT.md

Todo futuro `AGENT.md` debería contener:

- Metadata
- Role
- Purpose
- Applies When
- Required Rules
- Required Skills
- Procedure
- Do Not Decide
- Validation
- Output Contract

No existen actualmente Agents individuales en el catálogo.
