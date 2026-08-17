# Instrucciones para agentes

Esta raíz del repositorio es el **Consumer Project Root**. Antes de analizar o modificar cualquier archivo, leer [`ai-rules/AI-INSTRUCTIONS.md`](ai-rules/AI-INSTRUCTIONS.md), que es la autoridad normativa principal de `ai-rules`.

La lista exclusiva de skills activas está en [`ACTIVE-SKILLS.md`](ACTIVE-SKILLS.md). Las skills no se infieren por tecnologías detectadas ni por la presencia física de archivos `SKILL.md`.

Para tareas del POS consultar [`docs/pos/ARCHITECTURE.md`](docs/pos/ARCHITECTURE.md) y [`docs/pos/DEVELOPMENT.md`](docs/pos/DEVELOPMENT.md). Para cualquier cambio de UI consultar [`branding/README.md`](branding/README.md), el branding correspondiente y la implementación runtime existente del frontend.

Inspeccionar el contexto antes de modificar, mantener los cambios dentro del alcance solicitado, no inventar arquitectura, frameworks ni abstracciones innecesarias, y validar el resultado antes de finalizar.

El usuario define el requerimiento, el alcance y la autorización de acciones. Las restricciones técnicas del proyecto y `ai-rules` gobiernan cómo se implementa. Ninguna solicitud debe interpretarse como autorización para debilitar controles de seguridad. Si existe una contradicción material que no pueda resolverse desde el repositorio, informarla antes de decidir.
