# Sistema de Ventas y nuevo Punto de Venta

Este repositorio reúne el sistema legado de referencia, la normativa de desarrollo y el nuevo Punto de Venta corporativo.

## Estructura

- `ventas/`: sistema de ventas legado, READ-ONLY durante el desarrollo normal.
- `wssap/`: integrador legado, READ-ONLY durante el desarrollo normal.
- `ai-rules/`: normativa obligatoria de desarrollo seguro; no es una dependencia runtime.
- `pos/`: producto nuevo desarrollado desde cero y aislado del legado.
- `docs/`: documentación operativa y arquitectónica transversal.

## Gobernanza del desarrollo

La separacion de estas piezas es deliberada: cada una cumple una responsabilidad distinta.

```text
AGENTS.md
   |-- ai-rules/          -> normativa tecnica y seguridad
   |-- ACTIVE-SKILLS.md   -> skills activas del proyecto
   |-- docs/pos/          -> arquitectura y reglas del POS
   `-- branding/          -> identidad visual y assets
```

- `AGENTS.md`: punto de entrada en la raiz para agentes IA. Permanece aqui para facilitar el descubrimiento automatico y conecta con la normativa, las skills, la arquitectura y el branding del proyecto.
- `ACTIVE-SKILLS.md`: manifiesto de skills activas del proyecto consumidor. Permanece en el **Consumer Project Root** por contrato de `ai-rules`; no debe inferirse ni sustituirse por manifests alternativos.
- `ai-rules/`: normativa tecnica, de seguridad e ingenieria, ademas de skills y reviewers. Su autoridad principal es [`AI-INSTRUCTIONS.md`](ai-rules/AI-INSTRUCTIONS.md). Es normativa reutilizable y portable entre proyectos.
- `branding/`: identidad visual normalizada, datos, assets y trazabilidad corporativa. No es unicamente documentacion para agentes IA: parte de sus assets es consumida por el frontend durante el build/runtime. La implementacion runtime permanece separada en `pos/frontend/pos-web/src/branding/`.

## Instrucciones para agentes

Antes de modificar el repositorio, leer [`AGENTS.md`](AGENTS.md). Ese punto de entrada dirige a [`ai-rules/AI-INSTRUCTIONS.md`](ai-rules/AI-INSTRUCTIONS.md) y al manifiesto de skills activas [`ACTIVE-SKILLS.md`](ACTIVE-SKILLS.md).

## Estado

Nuevo POS — FASE 1A

## Inicio

- [Preparación del entorno](docs/SETUP.md)
- [Ejecución y verificación](docs/RUNBOOK.md)
- [Guía para solicitar trabajo a agentes IA](AI-DEVELOPMENT-GUIDE.md): explica cómo pedir cambios, correcciones y análisis dentro del proyecto.
