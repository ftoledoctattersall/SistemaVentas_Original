# Architecture Standard

## Objetivo

Este documento define el estándar de ingeniería que toda IA debe seguir para organizar los componentes internos de software y las dependencias entre ellos.

Su objetivo es mantener responsabilidades claras, límites verificables, bajo acoplamiento y una evolución segura del sistema.

No define controles de seguridad.

Las reglas de seguridad pertenecen exclusivamente al módulo **Security**.

---

## Prioridad

**Nivel:** Crítico

Toda creación o modificación de la arquitectura interna de un sistema que utilice este repositorio deberá cumplir este estándar.

Su incumplimiento puede generar inconsistencias arquitectónicas relevantes, propagar cambios innecesarios y dificultar la evolución del sistema.

---

## Propósito

Garantizar una estrategia uniforme y verificable para asignar responsabilidades, definir límites y dirigir dependencias entre componentes internos sin imponer una arquitectura o patrón específico.

---

## Cuándo consultar este documento

Consultar este documento antes de:

- Diseñar la arquitectura interna de una solución.
- Crear componentes o capas.
- Asignar responsabilidades entre componentes.
- Definir límites entre componentes.
- Definir dependencias entre componentes.
- Integrar detalles externos con reglas o políticas estables.
- Modificar la arquitectura existente.
- Sustituir detalles de implementación.
- Revisar acoplamiento o ciclos entre componentes.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- Naming.md
- Dependencies.md
- API.md
- Database.md
- Testing.md

Consultar también cuando corresponda:

- security/A04-Insecure-Design.md

---

## No cubre

Este documento no define:

- Arquitectura empresarial.
- Infraestructura.
- Cloud.
- DevOps.
- Topología física o de despliegue.
- Selección de productos o tecnologías.
- Dependencias externas de paquetes o librerías.
- Nombres, terminología o nomenclatura de archivos, carpetas y proyectos.
- Patrones arquitectónicos específicos.
- Reglas de negocio.
- Controles o propiedades de seguridad del diseño.

Estos aspectos pertenecen a sus respectivos estándares.

---

## Principios

Toda arquitectura interna debe ser:

- Intencional.
- Cohesiva.
- Mínima.
- Verificable.
- Evolutiva.
- Independiente de detalles externos cuando estos no sean necesarios para expresar reglas estables.

---

## Reglas obligatorias

### Responsabilidades

- Asignar a cada componente una responsabilidad claramente identificable.
- Mantener relacionadas las responsabilidades agrupadas dentro de un mismo componente.
- Separar responsabilidades no relacionadas en componentes distintos cuando su evolución o propósito sean independientes.
- Mantener alta cohesión entre los elementos internos de cada componente.
- Documentar la responsabilidad de cada componente cuando no pueda derivarse claramente de su contrato y estructura.

### Límites

- Definir explícitamente los límites entre componentes con responsabilidades distintas.
- Definir contratos o interfaces claros para las interacciones que atraviesen un límite arquitectónico.
- Exponer únicamente las capacidades necesarias para colaborar a través de cada límite.
- Mantener los detalles internos inaccesibles para otros componentes salvo que formen parte explícita del contrato.
- Verificar que cada interacción entre componentes respete el límite y el contrato definidos.

### Dependencias

- Incorporar únicamente dependencias entre componentes que respondan a una necesidad identificada.
- Justificar la responsabilidad y dirección de cada dependencia arquitectónica.
- Minimizar la cantidad de dependencias entre componentes.
- Mantener explícito y auditable el grafo de dependencias entre componentes.
- Eliminar dependencias arquitectónicas que hayan dejado de ser necesarias.
- Mantener el grafo de dependencias libre de ciclos.

### Dirección de dependencias

- Identificar qué componentes contienen reglas de negocio o políticas estables y qué componentes contienen detalles externos o de implementación.
- Mantener las reglas de negocio y políticas estables independientes de detalles de infraestructura, transporte, persistencia, frameworks y mecanismos externos cuando esos detalles no sean necesarios para expresarlas.
- Definir en el componente más estable los contratos requeridos para interactuar con detalles externos cuando sea necesario preservar dicha independencia.
- Orientar la dependencia del detalle externo hacia esos contratos cuando corresponda.
- Mantener la autoridad sobre las reglas de negocio en los componentes responsables de dichas reglas.
- Impedir que un detalle de implementación determine o sustituya reglas de negocio por conveniencia técnica.

### Complejidad

- Crear únicamente componentes, capas y abstracciones con una responsabilidad actual y demostrable.
- Mantener la arquitectura mínima necesaria para satisfacer los requisitos conocidos.
- Justificar cada límite adicional por una necesidad de responsabilidad, aislamiento o evolución.
- Eliminar componentes o abstracciones que no aporten una responsabilidad o límite verificable.

### Evolución

- Diseñar los límites para localizar los cambios en el componente responsable.
- Minimizar el impacto de cambios en detalles externos sobre componentes que contienen reglas estables.
- Permitir la sustitución de detalles de implementación sin propagar cambios innecesarios cuando resulte razonable.
- Revisar responsabilidades, límites y dirección de dependencias después de cada cambio arquitectónico.
- Mantener actualizada la representación utilizada para auditar la arquitectura cuando cambie el grafo de dependencias.

---

## Acciones prohibidas

- Nunca mezclar responsabilidades no relacionadas dentro de un mismo componente sin una justificación explícita.
- Nunca acceder arbitrariamente a detalles internos de otro componente.
- Nunca introducir una dependencia arquitectónica sin una necesidad identificada.
- Nunca mantener dependencias circulares entre componentes.
- Nunca hacer depender reglas de negocio o políticas estables de detalles externos por conveniencia de implementación.
- Nunca permitir que detalles de infraestructura, transporte, persistencia, frameworks o mecanismos externos se conviertan en autoridad sobre reglas de negocio.
- Nunca crear capas sin una responsabilidad demostrable.
- Nunca crear abstracciones para requisitos hipotéticos.
- Nunca introducir componentes únicamente para anticipar necesidades futuras.
- Nunca propagar contratos de implementación a través de límites que no los requieran.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] Cada componente posee una responsabilidad claramente identificable.
- [ ] No existen componentes que mezclen responsabilidades no relacionadas.
- [ ] Los límites arquitectónicos son explícitos y poseen contratos definidos cuando corresponde.
- [ ] Ningún componente accede arbitrariamente a detalles internos de otro componente.
- [ ] Cada dependencia arquitectónica es necesaria, intencional y justificable.
- [ ] El grafo de dependencias es explícito, auditable y no contiene ciclos.
- [ ] Las reglas de negocio y políticas estables no dependen innecesariamente de detalles externos.
- [ ] Los detalles de implementación no determinan reglas de negocio.
- [ ] Los cambios en detalles externos permanecen localizados cuando resulta razonable.
- [ ] No existen capas, componentes o abstracciones sin una responsabilidad demostrable.
- [ ] La arquitectura es la mínima necesaria para los requisitos conocidos.
- [ ] Los estándares relacionados fueron consultados cuando correspondía.

---

## Referencias

- ISO/IEC/IEEE 42010 — Systems and Software Engineering — Architecture Description
- ISO/IEC 25010 — Systems and Software Quality Models
- IEEE 1016 — Systems Design Description
