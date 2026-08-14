# Punto de Venta

## Propósito

`pos/` contiene la implementación completamente nueva del Punto de Venta corporativo.

## Regla de aislamiento

El sistema legado existe sólo como referencia funcional, evidencia, apoyo de auditoría y fuente de conocimiento. El nuevo POS no puede depender en runtime del legado.

## Estado

FASE 0C — Quality baseline y gobierno técnico

## Stack inicial

- ASP.NET Core / .NET 10
- React + TypeScript + Vite

## Exclusiones actuales

Todavía no existen dominio, persistencia, SAP, AWS, autenticación, reglas comerciales ni autorizaciones.

## Referencias

- La [documentación transversal](../docs/README.md) contiene las instrucciones operativas y decisiones arquitectónicas.
- [`ai-rules/`](../ai-rules/) contiene la normativa obligatoria de desarrollo seguro y permanece aislada del runtime del producto.
