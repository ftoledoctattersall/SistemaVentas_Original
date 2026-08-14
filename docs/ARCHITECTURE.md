# Arquitectura

## Propósito

El producto es un nuevo Punto de Venta corporativo desarrollado desde cero.

## Fronteras

```text
Legacy
   ↓ referencia

ai-rules
   ↓ normativa

pos/
   ↓ producto nuevo
```

El legado no es una dependencia runtime. `ai-rules/` gobierna el desarrollo, pero tampoco es una dependencia runtime.

## IMPLEMENTADO

Backend:

- ASP.NET Core;
- .NET 10;
- `Pos.Api`, con `GET /health` y `GET /api/context/empresa`;
- `Pos.Application`, con el caso de uso técnico `ObtenerEmpresaDemo`;
- `Pos.Domain`, con la entidad `Empresa`.

La dirección implementada de dependencias es:

```text
Pos.Api
   ↓
Pos.Application
   ↓
Pos.Domain
```

`Pos.Domain` no referencia otras capas POS. `Pos.Application` sólo referencia `Pos.Domain`, y `Pos.Api` referencia `Pos.Application`.

Frontend:

- React 19;
- TypeScript;
- Vite.

La resolución actual de empresa es un baseline técnico determinista. La resolución real de empresa todavía NO está implementada.

No existen `Pos.Infrastructure`, `Pos.Integrations`, persistencia ni integraciones externas.

El quality baseline está implementado mediante un proyecto de tests de API y pruebas de componentes del frontend. Testing no constituye una capa arquitectónica.

## DECISIÓN FUTURA

Arquitectura objetivo aprobada, todavía no implementada:

```text
React
   ↓
ASP.NET Core API
   ↓
Application / Domain (implementados inicialmente)
   ↓
Infrastructure / Integrations
   ↓
AWS / SAP RISE
```

Decisiones aprobadas para la evolución futura:

- modular monolith inicialmente;
- REST inicialmente;
- Entra ID como identidad futura;
- AWS como plataforma objetivo;
- SAP RISE detrás de una ACL;
- sin acceso SQL directo a SAP;
- sin SAP DI API;
- multiempresa considerada desde el diseño;
- sin microservicios inicialmente;
- sin Kubernetes inicialmente.

Estas decisiones no implican que las capas, plataformas o integraciones ya existan.
