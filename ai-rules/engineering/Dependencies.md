# Dependencies Standard

## Objetivo

Este documento define el estándar de ingeniería que toda IA debe seguir para seleccionar, incorporar, mantener y retirar dependencias externas durante el desarrollo de software.

Su objetivo es minimizar el acoplamiento innecesario, reducir el riesgo técnico y facilitar la evolución del sistema.

No define controles de seguridad.

Las reglas de seguridad pertenecen exclusivamente al módulo **Security**.

---

## Prioridad

**Nivel:** Alto

Toda dependencia incorporada a un proyecto que utilice este repositorio deberá cumplir este estándar.

---

## Propósito

Garantizar una estrategia uniforme para la gestión del ciclo de vida de dependencias externas utilizadas por la aplicación.

---

## Cuándo consultar este documento

Consultar este documento antes de:

- Agregar librerías.
- Incorporar paquetes.
- Actualizar dependencias.
- Reemplazar componentes externos.
- Eliminar dependencias.
- Evaluar nuevas herramientas.
- Incorporar SDK.
- Incorporar frameworks.
- Incorporar clientes externos.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- API.md
- Logging.md
- Database.md
- Testing.md

Consultar también cuando corresponda:

- security/A06-Vulnerable-and-Outdated-Components.md
- security/A08-Software-and-Data-Integrity-Failures.md

---

## No cubre

Este documento no define:

- Configuración específica de gestores de paquetes.
- Versionado del proyecto.
- Arquitectura del sistema.
- Reglas de negocio.
- Controles de seguridad.

Estos aspectos pertenecen a sus respectivos estándares.

---

## Principios

Toda dependencia debe ser:

- Justificada.
- Mantenible.
- Sustituible.
- Compatible.
- Documentada.
- Estable.
- Fácil de actualizar.

---

## Reglas obligatorias

### Selección

- Incorporar únicamente dependencias que resuelvan una necesidad claramente identificada.
- Evaluar si la funcionalidad puede implementarse razonablemente sin agregar una nueva dependencia.
- Favorecer componentes ampliamente utilizados y mantenidos.

### Incorporación

- Mantener una estrategia uniforme para incorporar dependencias.
- Registrar las dependencias mediante los mecanismos definidos por el proyecto.
- Evitar incorporar múltiples dependencias con la misma responsabilidad.

### Compatibilidad

- Verificar compatibilidad con la plataforma objetivo.
- Verificar compatibilidad con las dependencias existentes.
- Mantener coherencia entre versiones relacionadas.

### Actualización

- Actualizar dependencias de forma controlada.
- Evaluar el impacto antes de realizar una actualización.
- Validar el funcionamiento del sistema después de cada actualización.

### Sustitución

- Diseñar la aplicación para facilitar el reemplazo de dependencias cuando sea necesario.
- Minimizar el acoplamiento con implementaciones específicas.

### Mantenibilidad

- Eliminar dependencias que ya no se utilicen.
- Evitar dependencias transitivas innecesarias cuando puedan controlarse.
- Mantener documentadas las decisiones relevantes sobre dependencias.

---

## Acciones prohibidas

- Nunca agregar dependencias sin una necesidad identificada.
- Nunca incorporar múltiples librerías para resolver el mismo problema sin justificación.
- Nunca depender innecesariamente de características específicas de un proveedor.
- Nunca mantener dependencias obsoletas sin una decisión documentada.
- Nunca introducir dependencias que dificulten el mantenimiento del sistema.
- Nunca ocultar el uso de una dependencia dentro de componentes reutilizables.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] La incorporación de la dependencia está justificada.
- [ ] No existe otra dependencia equivalente ya utilizada por el proyecto.
- [ ] La dependencia es compatible con el resto del sistema.
- [ ] El acoplamiento con la dependencia es mínimo.
- [ ] La solución puede evolucionar sin depender permanentemente de dicha dependencia.
- [ ] Los estándares relacionados fueron consultados cuando correspondía.

---

## Referencias

- Semantic Versioning 2.0.0
- ISO/IEC 25010 — Systems and Software Quality Models
- NIST Secure Software Development Framework (SSDF)