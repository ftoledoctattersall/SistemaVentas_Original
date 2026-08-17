# Desarrollo

## Nuevo código

Todo nuevo código productivo debe vivir bajo `pos/`.

## Legado

`ventas/` y `wssap/` son READ-ONLY durante el desarrollo normal. Pueden consultarse como referencia funcional, evidencia, fuente de conocimiento y apoyo de auditoría. El nuevo POS no puede depender de ellos en runtime.

## ai-rules

Antes de cada fase:

1. inspeccionar las reglas relevantes;
2. declarar `ACTIVE AI-RULES`;
3. aplicar sólo las reglas relacionadas con el alcance;
4. mantener `ai-rules/` sin modificaciones.

## Quality gates actuales

Backend:

```powershell
cd pos\backend
dotnet restore
dotnet build
dotnet test
```

Frontend:

```powershell
cd pos\frontend\pos-web
npm install
npm run build
npm test
```

El resultado mínimo aceptable es build y tests en PASS, con `0 errores` y `0 advertencias` en los proyectos .NET nuevos. Los warnings se tratan como errores en todos los proyectos bajo `pos/backend`; esta decisión no afecta al legado.

Los proyectos backend actuales son:

- `src/Pos.Api`;
- `src/Pos.Application`;
- `src/Pos.Domain`;
- `tests/Pos.Api.Tests`;
- `tests/Pos.Application.Tests`;
- `tests/Pos.Domain.Tests`.

Las referencias productivas deben conservar la dirección `Pos.Api → Pos.Application → Pos.Domain`. Los tests de cada capa están ubicados bajo `pos/backend/tests/`.

No se exige ejecutar ambos stacks cuando una fase afecta exclusivamente uno, salvo que exista una razón explícita.

## Criterio de PASS para fases de implementación

Una fase sólo puede considerarse PASS cuando:

1. el scope autorizado está cumplido;
2. las `ai-rules` aplicables están identificadas;
3. el build backend pasa si backend fue afectado;
4. los tests backend pasan si backend fue afectado;
5. el build frontend pasa si frontend fue afectado;
6. los tests frontend pasan si frontend fue afectado;
7. la documentación está actualizada cuando corresponde;
8. no se incorporaron secretos;
9. no existen cambios fuera de scope.

La validación técnica forma parte del cierre normal de una implementación. El commit y el push no son requisitos automáticos de toda tarea: se realizan únicamente cuando la tarea o el flujo autorizado lo permita. Una tarea read-only no debe producir commit ni push, y una instrucción explícita de no publicar prevalece sobre el flujo Git normal.

## Documentación viva

> Todo cambio que altere instalación, configuración, ejecución, arquitectura, dependencias, testing, quality gates o comportamiento operativo debe actualizar la documentación correspondiente en el mismo lote.

## Git

Antes de commit o push deben ejecutarse las verificaciones mínimas:

```powershell
git status --short
git branch --show-current
```

El flujo normal es:

```text
cambio
→ validación
→ revisión de diff
→ commit
→ push
```

Después de configurar correctamente el remoto, el flujo completo será:

1. verificar rama y working tree;
2. implementar el lote autorizado;
3. actualizar documentación;
4. ejecutar quality gates;
5. revisar diff;
6. commit;
7. push;
8. comprobar working tree limpio.

Como actualmente trabaja una sola persona sobre el proyecto, no se exigen auditorías profundas local-vs-remoto en cada cambio. Los análisis de ahead/behind, `git ls-remote`, parentage, hashes e historia se reservan para push rechazado, cambios remotos inesperados, merge/rebase, recuperación o anomalías Git.
