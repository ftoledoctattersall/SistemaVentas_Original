# Database Standard

## Objetivo

Este documento define el estándar de ingeniería que toda IA debe seguir para diseñar, implementar y mantener mecanismos de persistencia de datos consistentes, mantenibles y escalables.

Su objetivo es garantizar que el acceso a los datos permanezca desacoplado, predecible y fácil de evolucionar.

No define controles de seguridad.

Las reglas de seguridad pertenecen exclusivamente al módulo **Security**.

---

## Prioridad

**Nivel:** Crítico

Todo componente relacionado con persistencia de datos implementado dentro de un proyecto que utilice este repositorio deberá cumplir este estándar.

---

## Propósito

Garantizar una estrategia uniforme para el modelado, acceso, modificación y evolución de los datos durante todo el ciclo de vida de la aplicación.

---

## Cuándo consultar este documento

Consultar este documento antes de implementar o modificar:

- Bases de datos.
- Entidades.
- Tablas.
- Repositorios.
- Consultas.
- Persistencia.
- Migraciones.
- Transacciones.
- Operaciones CRUD.
- Acceso a datos.
- Modelado de datos.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- API.md
- Validation.md
- Error-Handling.md
- Logging.md
- Testing.md

Consultar también cuando corresponda:

- security/A03-Injection.md

---

## No cubre

Este documento no define:

- Reglas de negocio.
- Validaciones.
- Autenticación.
- Autorización.
- Logging.md.
- Controles de seguridad.
- Configuración específica de motores de base de datos.
- Optimización específica de un proveedor.

Estos aspectos pertenecen a sus respectivos estándares.

---

## Principios

Todo mecanismo de persistencia debe ser:

- Consistente.
- Determinista.
- Mantenible.
- Escalable.
- Desacoplado.
- Fácil de evolucionar.
- Independiente del motor de base de datos cuando sea posible.

---

## Reglas obligatorias

### Diseño

- Diseñar el modelo de datos antes de implementar la persistencia.
- Mantener una separación clara entre el modelo de dominio y el mecanismo de almacenamiento.
- Mantener responsabilidades claramente definidas entre acceso a datos y lógica de negocio.
- Favorecer modelos fáciles de evolucionar.

### Modelado

- Mantener nombres consistentes para entidades y atributos.
- Utilizar identificadores estables.
- Evitar estructuras redundantes cuando puedan normalizarse.
- Mantener relaciones explícitas entre entidades.

### Acceso a datos

- Centralizar el acceso a los datos mediante componentes claramente definidos.
- Evitar acceso directo a la persistencia desde la lógica de presentación.
- Mantener una estrategia uniforme para consultas y modificaciones.
- Reutilizar mecanismos comunes de acceso a datos.

### Consultas

- Recuperar únicamente la información necesaria para cada operación.
- Diseñar consultas fáciles de comprender y mantener.
- Evitar consultas excesivamente complejas cuando puedan dividirse sin afectar la consistencia.
- Mantener un comportamiento determinista para consultas equivalentes.

### Transacciones

- Mantener límites claros para cada transacción.
- Garantizar la consistencia de los datos durante operaciones transaccionales.
- Finalizar correctamente las transacciones exitosas.
- Revertir completamente las operaciones cuando la transacción falle.

### Evolución

- Gestionar los cambios del modelo de datos mediante mecanismos controlados.
- Mantener compatibilidad durante procesos de migración cuando el proyecto lo requiera.
- Evitar modificaciones destructivas sin una estrategia definida.

### Rendimiento

- Diseñar el acceso a datos considerando el volumen esperado de información.
- Evitar operaciones repetitivas innecesarias.
- Minimizar accesos redundantes a la persistencia.
- Mantener una estrategia uniforme para optimizar consultas frecuentes.

### Mantenibilidad

- Mantener el modelo de datos organizado.
- Evitar dependencias innecesarias entre componentes de persistencia.
- Favorecer estructuras fáciles de revisar y modificar.
- Documentar cambios relevantes del modelo de datos cuando corresponda.

---

## Acciones prohibidas

- Nunca mezclar lógica de negocio con acceso a datos.
- Nunca acceder directamente a la persistencia desde la capa de presentación.
- Nunca duplicar reglas de persistencia sin una justificación.
- Nunca depender de comportamientos implícitos del motor de base de datos.
- Nunca modificar datos fuera de los mecanismos definidos por la aplicación.
- Nunca implementar transacciones parciales que puedan dejar el sistema en un estado inconsistente.
- Nunca diseñar estructuras difíciles de mantener.
- Nunca crear dependencias innecesarias con un proveedor específico cuando puedan evitarse.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] El modelo de datos fue diseñado antes de la implementación.
- [ ] La persistencia permanece separada de la lógica de negocio.
- [ ] Las entidades mantienen una estructura consistente.
- [ ] Las consultas recuperan únicamente la información necesaria.
- [ ] Las transacciones mantienen la consistencia del sistema.
- [ ] Los cambios del modelo de datos pueden evolucionar de forma controlada.
- [ ] No existen dependencias innecesarias con un motor específico.
- [ ] Los estándares relacionados fueron consultados cuando correspondía.

---

## Referencias

- ISO/IEC 25010 — Systems and Software Quality Models
- NIST Secure Software Development Framework (SSDF)
- Martin Fowler — Patterns of Enterprise Application Architecture
- Domain-Driven Design Reference