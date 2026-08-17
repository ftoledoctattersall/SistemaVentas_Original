# Engineering Index

## Enlaces directos

- [Architecture.md](Architecture.md)
- [API.md](API.md)
- [Database.md](Database.md)
- [Validation.md](Validation.md)
- [Error-Handling.md](Error-Handling.md)
- [Logging.md](Logging.md)
- [Testing.md](Testing.md)
- [Dependencies.md](Dependencies.md)
- [Authentication.md](Authentication.md)
- [Authorization.md](Authorization.md)
- [Naming.md](Naming.md)

## Objetivo

Este documento permite identificar rápidamente qué estándares del módulo **Engineering** deben consultarse antes de implementar una funcionalidad.

No contiene reglas de ingeniería.

Su única responsabilidad es dirigir a la IA hacia los estándares técnicos adecuados según la tarea que va a realizar.

---

## Cómo utilizar este índice

Antes de generar código:

1. Identificar la funcionalidad que se implementará.
2. Buscar dicha funcionalidad en este índice.
3. Leer todos los estándares indicados.
4. Aplicar las reglas obligatorias correspondientes.
5. Completar la auto verificación de cada estándar utilizado.

Cuando una funcionalidad involucre varios aspectos de ingeniería, deberán consultarse todos los documentos correspondientes.

---

# Arquitectura

## Diseñar arquitectura

Consultar:

- Architecture.md

---

## Crear componentes o capas

Consultar:

- Architecture.md

---

## Definir límites entre componentes

Consultar:

- Architecture.md

---

## Definir dependencias entre componentes

Consultar:

- Architecture.md

---

## Modificar arquitectura existente

Consultar:

- Architecture.md

---

# Diseño de APIs

## Crear una API REST

Consultar:

- API.md
- Validation.md
- Error-Handling.md
- Logging.md
- Testing.md

---

## Crear un endpoint

Consultar:

- API.md
- Validation.md
- Error-Handling.md

---

## Versionar una API

Consultar:

- API.md

---

## Diseñar contratos HTTP

Consultar:

- API.md
- Error-Handling.md

---

## Implementar paginación

Consultar:

- API.md
- Validation.md

---

## Implementar filtros

Consultar:

- API.md
- Validation.md

---

## Implementar ordenamiento

Consultar:

- API.md
- Validation.md

---

# Persistencia

## Crear una base de datos

Consultar:

- Database.md

---

## Crear tablas

Consultar:

- Database.md

---

## Crear entidades

Consultar:

- Database.md

---

## Implementar repositorios

Consultar:

- Database.md
- Testing.md

---

## Crear migraciones

Consultar:

- Database.md

---

## Optimizar consultas

Consultar:

- Database.md

---

## Implementar transacciones

Consultar:

- Database.md
- Error-Handling.md

---

## Implementar acceso a datos

Consultar:

- Database.md
- Testing.md

---

## Modelar datos

Consultar:

- Database.md

---

# Validaciones

## Validar entrada

Consultar:

- Validation.md

---

## Validar modelos

Consultar:

- Validation.md

---

## Validar parámetros

Consultar:

- Validation.md

---

## Validar reglas técnicas

Consultar:

- Validation.md

---

# Manejo de errores

## Manejar excepciones

Consultar:

- Error-Handling.md
- Logging.md

---

## Problem Details

Consultar:

- Error-Handling.md

---

## Respuestas HTTP

Consultar:

- API.md
- Error-Handling.md

---

# Logging

## Agregar logging

Consultar:

- Logging.md

---

## Agregar auditoría técnica

Consultar:

- Logging.md

---

## Correlation ID

Consultar:

- Logging.md

---

## Observabilidad

Consultar:

- Logging.md

---

# Testing

## Crear pruebas unitarias

Consultar:

- Testing.md

---

## Crear pruebas de integración

Consultar:

- Testing.md

---

## Mocking

Consultar:

- Testing.md

---

## Cobertura

Consultar:

- Testing.md

---

## Crear pruebas funcionales

Consultar:

- Testing.md

---

## Crear pruebas de componentes

Consultar:

- Testing.md

---

# Dependencias

## Agregar librerías

Consultar:

- Dependencies.md

---

## Actualizar paquetes

Consultar:

- Dependencies.md

---

## Administrar dependencias

Consultar:

- Dependencies.md

---

## Reemplazar una dependencia

Consultar:

- Dependencies.md

---

## Eliminar una dependencia

Consultar:

- Dependencies.md

---

# Autenticación

## Implementar JWT

Consultar:

- Authentication.md
- API.md

Consultar también:

- security/A02-Cryptographic-Failures.md
- security/A07-Identification-and-Authentication-Failures.md

---

## Implementar OAuth2

Consultar:

- Authentication.md

Consultar también:

- security/A07-Identification-and-Authentication-Failures.md

---

## Implementar OpenID Connect

Consultar:

- Authentication.md

Consultar también:

- security/A07-Identification-and-Authentication-Failures.md

---

## Implementar Cookies

Consultar:

- Authentication.md

Consultar también:

- security/A07-Identification-and-Authentication-Failures.md

---

## Implementar API Keys

Consultar:

- Authentication.md

Consultar también:

- security/A02-Cryptographic-Failures.md
- security/A07-Identification-and-Authentication-Failures.md

---

## Implementar Refresh Tokens

Consultar:

- Authentication.md

Consultar también:

- security/A02-Cryptographic-Failures.md
- security/A07-Identification-and-Authentication-Failures.md

---

## Implementar Single Sign-On

Consultar:

- Authentication.md

Consultar también:

- security/A07-Identification-and-Authentication-Failures.md

---

# Autorización

## Implementar Roles

Consultar:

- Authorization.md

Consultar también:

- security/A01-Broken-Access-Control.md

---

## Implementar Permisos

Consultar:

- Authorization.md

Consultar también:

- security/A01-Broken-Access-Control.md

---

## Implementar Policies

Consultar:

- Authorization.md

Consultar también:

- security/A01-Broken-Access-Control.md

---

## Recursos protegidos

Consultar:

- Authorization.md

Consultar también:

- security/A01-Broken-Access-Control.md

---

# Organización del código

## Nombrar clases

Consultar:

- Naming.md

---

## Nombrar métodos

Consultar:

- Naming.md

---

## Organizar proyectos

Consultar:

- Architecture.md
- Naming.md

---

## Organizar carpetas

Consultar:

- Naming.md

---

# Regla general

Si una funcionalidad requiere más de un estándar, deberán consultarse todos los documentos correspondientes antes de generar código.

Los estándares del módulo **Engineering** definen la implementación técnica.

Los requisitos de seguridad continúan siendo responsabilidad exclusiva del módulo **Security**.

Cuando una funcionalidad requiera ambos módulos, deberán aplicarse conjuntamente.
