# Naming Standard

## Objetivo

Este documento define el estándar de ingeniería que toda IA debe seguir para nombrar de forma consistente los distintos elementos de un proyecto de software.

Su objetivo es garantizar que la nomenclatura utilizada sea uniforme, descriptiva, mantenible y fácil de comprender.

No define controles de seguridad.

Las reglas de seguridad pertenecen exclusivamente al módulo **Security**.

---

## Prioridad

**Nivel:** Alto

Todo elemento creado dentro de un proyecto que utilice este repositorio deberá cumplir este estándar.

---

## Propósito

Garantizar una estrategia uniforme para asignar nombres a los distintos componentes del sistema, facilitando la comprensión, navegación y mantenimiento del código.

---

## Cuándo consultar este documento

Consultar este documento antes de crear o modificar:

- Clases.
- Interfaces.
- Métodos.
- Funciones.
- Variables.
- Constantes.
- Enumeraciones.
- DTO.
- Entidades.
- Recursos.
- Endpoints.
- Archivos.
- Carpetas.
- Proyectos.
- Paquetes.
- Espacios de nombres.
- Componentes reutilizables.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- API.md
- Validation.md
- Database.md
- Authentication.md
- Authorization.md

Consultar también cuando corresponda:

- No aplica.

---

## No cubre

Este documento no define:

- Arquitectura del proyecto.
- Reglas de negocio.
- Organización de equipos.
- Convenciones específicas de un lenguaje.
- Formato del código.
- Controles de seguridad.

Estos aspectos pertenecen a sus respectivos estándares.

---

## Principios

Todo nombre debe ser:

- Descriptivo.
- Consistente.
- Determinista.
- Fácil de comprender.
- Fácil de mantener.
- Independiente de implementaciones temporales.
- Alineado con el dominio del problema.

---

## Reglas obligatorias

### Claridad

- Utilizar nombres que describan claramente la responsabilidad del elemento.
- Evitar abreviaturas innecesarias.
- Evitar nombres ambiguos.
- Favorecer nombres completos cuando mejoren la comprensión.

### Consistencia

- Utilizar las mismas convenciones para elementos equivalentes.
- Mantener una terminología uniforme en todo el proyecto.
- Reutilizar el mismo término para representar el mismo concepto.

### Dominio

- Utilizar nombres alineados con el lenguaje del dominio.
- Evitar nombres basados en detalles internos de implementación.
- Evitar nombres temporales o circunstanciales.

### Responsabilidad

- Cada nombre debe representar una única responsabilidad.
- Evitar nombres que describan múltiples propósitos.
- Mantener correspondencia entre el nombre y el comportamiento del componente.

### Evolución

- Diseñar nombres que continúen siendo válidos cuando el sistema evolucione.
- Evitar referencias a tecnologías específicas cuando no formen parte del dominio.
- Minimizar la necesidad de renombrar componentes durante la evolución del proyecto.

---

## Acciones prohibidas

- Nunca utilizar nombres ambiguos.
- Nunca utilizar abreviaturas sin una convención definida.
- Nunca utilizar nombres que contradigan la responsabilidad del componente.
- Nunca reutilizar el mismo nombre para conceptos distintos.
- Nunca utilizar nombres dependientes de una implementación temporal.
- Nunca mezclar idiomas dentro de un mismo contexto funcional sin una convención explícita.
- Nunca incorporar información redundante al nombre de un componente.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] Todos los nombres describen claramente su responsabilidad.
- [ ] La terminología es consistente en todo el proyecto.
- [ ] No existen nombres ambiguos.
- [ ] No existen abreviaturas innecesarias.
- [ ] Los nombres permanecen alineados con el dominio.
- [ ] Los nombres continúan siendo válidos ante futuras evoluciones del sistema.
- [ ] Los estándares relacionados fueron consultados cuando correspondía.

---

## Referencias

- ISO/IEC 25010 — Systems and Software Quality Models
- Domain-Driven Design Reference
- Clean Code
- Code Complete