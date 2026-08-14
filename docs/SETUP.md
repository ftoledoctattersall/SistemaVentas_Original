# Preparación del entorno

## Requisitos validados

Las siguientes son las versiones actualmente validadas para el proyecto:

- Windows
- .NET SDK 10.0.302
- Node.js v22.14.0
- npm 10.9.2
- Git

## Backend

Desde la raíz del repositorio:

```powershell
cd pos\backend
dotnet restore
dotnet build
```

## Frontend

Desde la raíz del repositorio:

```powershell
cd pos\frontend\pos-web
npm install
npm run build
```
