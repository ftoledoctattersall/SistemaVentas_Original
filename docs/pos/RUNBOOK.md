# Runbook

## Backend

Desde la raíz del repositorio:

```powershell
cd pos\backend
dotnet run --project src\Pos.Api
```

El perfil HTTP definido en `launchSettings.json` publica actualmente la API en:

```text
http://localhost:5279
```

Para verificarla:

```http
GET http://localhost:5279/health
```

Respuesta:

```json
{
  "status": "ok"
}
```

## Frontend

Desde la raíz del repositorio:

```powershell
cd pos\frontend\pos-web
npm run dev
```

Vite publica actualmente el frontend en su URL local predeterminada:

```text
http://localhost:5173/
```

## Detención

Para detener cualquiera de los procesos, presionar `Ctrl+C` en su terminal.

## PowerShell y npm

Si PowerShell bloquea el wrapper `npm.ps1`, la alternativa validada es ejecutar `npm.cmd`. No es necesario deshabilitar las políticas de seguridad de PowerShell.

## Validación rápida

Backend:

```powershell
cd pos\backend
dotnet build
dotnet test
```

Frontend:

```powershell
cd pos\frontend\pos-web
npm run build
npm test
```
