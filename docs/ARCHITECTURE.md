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
- API mínima con `GET /health`.

Frontend:

- React 19;
- TypeScript;
- Vite.

Actualmente sólo existen `Pos.Api` y `pos-web`. No se han creado capas adicionales, persistencia ni integraciones.

El quality baseline está implementado mediante un proyecto de tests de API y pruebas de componentes del frontend. Testing no constituye una capa arquitectónica.

## DECISIÓN FUTURA

Arquitectura objetivo aprobada, todavía no implementada:

```text
React
   ↓
ASP.NET Core API
   ↓
Application / Domain
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
