# Testing Standard

## Objetivo

Este documento define el estándar de ingeniería que toda IA debe seguir para diseñar, implementar y mantener pruebas consistentes durante el ciclo de vida del software.

Su objetivo es garantizar que las implementaciones puedan verificarse de forma automática, repetible y mantenible.

No define controles de seguridad.

Las reglas de seguridad pertenecen exclusivamente al módulo **Security**.

---

## Prioridad

**Nivel:** Crítico

Toda estrategia de pruebas implementada dentro de un proyecto que utilice este repositorio deberá cumplir este estándar.

---

## Propósito

Garantizar que el software pueda validarse continuamente mediante pruebas claras, confiables y fáciles de mantener.

---

## Cuándo consultar este documento

Consultar este documento antes de implementar o modificar:

- Pruebas unitarias.
- Pruebas de integración.
- Pruebas funcionales.
- Pruebas de componentes.
- Pruebas automatizadas.
- Casos de prueba.
- Mocking.
- Dobles de prueba.
- Validación de comportamiento.
- Automatización de pruebas.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- API.md
- Validation.md
- Error-Handling.md
- Logging.md
- Database.md

Consultar también cuando corresponda:

- security/A01-Broken-Access-Control.md
- security/A03-Injection.md
- security/A07-Identification-and-Authentication-Failures.md
- security/A09-Security-Logging-and-Monitoring-Failures.md

---

## No cubre

Este documento no define:

- Frameworks de testing.
- Herramientas específicas.
- CI/CD.
- Controles de seguridad.
- Reglas de negocio.
- Pruebas manuales.

Estos aspectos pertenecen a sus respectivos estándares.

---

## Principios

Toda estrategia de pruebas debe ser:

- Determinista.
- Automatizable.
- Repetible.
- Independiente.
- Mantenible.
- Clara.
- Fácil de ejecutar.

---

## Reglas obligatorias

### Diseño

- Diseñar las pruebas junto con la implementación.
- Mantener una estrategia uniforme de pruebas.
- Verificar comportamiento observable.
- Evitar depender de detalles internos de implementación.

### Independencia

- Cada prueba debe poder ejecutarse de forma independiente.
- Cada prueba debe preparar su propio contexto.
- Cada prueba debe limpiar los recursos que utilice cuando corresponda.
- Evitar dependencias entre pruebas.

### Cobertura

- Probar los escenarios principales.
- Probar escenarios límite.
- Probar escenarios de error.
- Probar comportamiento esperado.
- Probar comportamiento inesperado cuando corresponda.

### Calidad

- Mantener nombres descriptivos.
- Mantener una única responsabilidad por prueba.
- Evitar pruebas excesivamente largas.
- Evitar duplicación de lógica de prueba.

### Mantenibilidad

- Reutilizar componentes comunes cuando sea apropiado.
- Mantener pruebas fáciles de comprender.
- Mantener datos de prueba organizados.
- Eliminar pruebas obsoletas.

### Ejecución

- Las pruebas deben producir siempre el mismo resultado bajo las mismas condiciones.
- Evitar dependencias de fecha, hora o entorno cuando no sean necesarias.
- Evitar dependencias de servicios externos cuando puedan aislarse.

---

## Acciones prohibidas

- Nunca depender del orden de ejecución.
- Nunca compartir estado entre pruebas.
- Nunca utilizar datos impredecibles sin control.
- Nunca validar detalles internos innecesarios.
- Nunca omitir pruebas para funcionalidades críticas.
- Nunca mantener pruebas que fallen de forma intermitente.
- Nunca duplicar pruebas equivalentes sin justificación.
- Nunca modificar la implementación únicamente para satisfacer una prueba incorrecta.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] Existen pruebas para el comportamiento principal.
- [ ] Las pruebas son independientes.
- [ ] No existen dependencias entre pruebas.
- [ ] Las pruebas son deterministas.
- [ ] Los escenarios de error fueron cubiertos.
- [ ] Las pruebas son fáciles de mantener.
- [ ] No existe lógica duplicada.
- [ ] Los estándares relacionados fueron consultados cuando correspondía.

---

## Referencias

- ISO/IEC 25010 — Systems and Software Quality Models
- ISTQB Glossary
- Martin Fowler — Test Pyramid
- xUnit Test Patterns
- NIST Secure Software Development Framework (SSDF)