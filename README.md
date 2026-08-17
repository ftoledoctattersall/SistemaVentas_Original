# Sistema de Ventas y Punto de Venta

Este repositorio reúne el sistema legado de referencia, la normativa transversal de desarrollo y el nuevo Punto de Venta corporativo.

## Estructura

```text
/
├── AGENTS.md
├── ACTIVE-SKILLS.md
├── AI-DEVELOPMENT-GUIDE.md
├── ai-rules/
├── branding/
├── docs/
├── pos/
├── ventas/
└── wssap/
```

- `AGENTS.md`: punto de entrada para agentes IA.
- `ACTIVE-SKILLS.md`: manifiesto contractual de skills activas.
- `AI-DEVELOPMENT-GUIDE.md`: guía breve para desarrolladores que trabajan con agentes IA.
- `ai-rules/`: normativa técnica, seguridad, engineering, skills y reviewers.
- `branding/`: identidad visual, datos y assets corporativos.
- `docs/`: documentación transversal y específica de cada área.
- `pos/`: implementación del nuevo Punto de Venta.
- `ventas/` y `wssap/`: sistema de ventas e integrador legado de referencia.

La separación de estas piezas es deliberada: descubrimiento, configuración contractual, normativa, guía de trabajo e identidad visual tienen responsabilidades distintas.

## Documentación y gobernanza

Antes de trabajar en el repositorio, consultar [`AGENTS.md`](AGENTS.md). Desde allí se accede a la normativa de [`ai-rules/`](ai-rules/), al manifiesto [`ACTIVE-SKILLS.md`](ACTIVE-SKILLS.md), a la guía [`AI-DEVELOPMENT-GUIDE.md`](AI-DEVELOPMENT-GUIDE.md) y a las referencias de arquitectura y branding.

La documentación específica está organizada así:

- [`pos/README.md`](pos/README.md): orientación del nuevo POS.
- [`docs/pos/`](docs/pos/): arquitectura, desarrollo, configuración, setup y runbook del POS.
- [`docs/pos/SETUP.md`](docs/pos/SETUP.md): preparación del entorno POS.
- [`docs/pos/RUNBOOK.md`](docs/pos/RUNBOOK.md): ejecución y verificación del POS.
- [`docs/legado/`](docs/legado/): documentación funcional y evidencia del sistema legado.

## Estado

El nuevo POS se encuentra en FASE 1A. La documentación especializada mantiene el detalle técnico y funcional correspondiente a cada área.
