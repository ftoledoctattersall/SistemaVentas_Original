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
```

Frontend:

```powershell
cd pos\frontend\pos-web
npm install
npm run build
```

El resultado mínimo aceptable es `0 errores`.

## Documentación viva

> Todo cambio que modifique instalación, configuración, ejecución, arquitectura, dependencias o comportamiento operativo debe actualizar la documentación correspondiente en el mismo lote.

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
