# Sistema de Ventas y nuevo Punto de Venta

Este repositorio reúne el sistema legado de referencia, la normativa de desarrollo y el nuevo Punto de Venta corporativo.

## Estructura

- `ventas/`: sistema de ventas legado, READ-ONLY durante el desarrollo normal.
- `wssap/`: integrador legado, READ-ONLY durante el desarrollo normal.
- `ai-rules/`: normativa obligatoria de desarrollo seguro; no es una dependencia runtime.
- `pos/`: producto nuevo desarrollado desde cero y aislado del legado.
- `docs/`: documentación operativa y arquitectónica transversal.

## Instrucciones para agentes

Antes de modificar el repositorio, leer [`AGENTS.md`](AGENTS.md). Ese punto de entrada dirige a [`ai-rules/AI-INSTRUCTIONS.md`](ai-rules/AI-INSTRUCTIONS.md) y al manifiesto de skills activas [`ACTIVE-SKILLS.md`](ACTIVE-SKILLS.md).

## Estado

Nuevo POS — FASE 1A

## Inicio

- [Preparación del entorno](docs/SETUP.md)
- [Ejecución y verificación](docs/RUNBOOK.md)
- [Guía para solicitar trabajo a agentes IA](docs/AI-DEVELOPMENT-GUIDE.md): explica cómo pedir cambios, correcciones y análisis dentro del proyecto.
