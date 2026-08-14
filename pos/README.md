# Punto de Venta

## Propósito

`pos/` contiene la implementación completamente nueva del Punto de Venta corporativo.

## Regla de aislamiento

El sistema legado existe sólo como referencia funcional, evidencia, apoyo de auditoría y fuente de conocimiento. El nuevo POS no puede depender en runtime del legado.

## Estado

FASE 1A — Empresa y arquitectura mínima de dominio

## Stack inicial

- ASP.NET Core / .NET 10
- React + TypeScript + Vite

## Exclusiones actuales

Existe una primera arquitectura `Pos.Api → Pos.Application → Pos.Domain` y el concepto mínimo `Empresa`. La resolución real de empresa todavía NO está implementada.

Todavía no existen persistencia, SAP, AWS, autenticación, reglas comerciales ni autorizaciones.

## Referencias

- La [documentación transversal](../docs/README.md) contiene las instrucciones operativas y decisiones arquitectónicas.
- [`ai-rules/`](../ai-rules/) contiene la normativa obligatoria de desarrollo seguro y permanece aislada del runtime del producto.
