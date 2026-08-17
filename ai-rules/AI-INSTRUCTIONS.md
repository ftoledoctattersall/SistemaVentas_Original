# AI Instructions

## Objetivo

Este repositorio define el estándar de ingeniería que debe seguir cualquier asistente de IA durante el análisis, diseño, implementación, revisión y mantenimiento de software.

Su objetivo es producir soluciones consistentes, seguras, mantenibles y alineadas con los estándares definidos en este repositorio.

Estas instrucciones deben aplicarse durante toda la sesión de trabajo.

---

# Taxonomía normativa

Los componentes de `ai-rules` se clasifican de la siguiente forma:

- **CORE RULE:** obligación global aplicable a toda tarea dentro de su alcance.
- **SECURITY RULE:** obligación de seguridad aplicable según el riesgo o funcionalidad involucrada.
- **ENGINEERING RULE:** obligación técnica aplicable según la actividad de ingeniería involucrada.
- **BEST PRACTICE:** recomendación no obligatoria que no puede reemplazar ni contradecir una Rule.
- **SKILL:** especialización opcional activada explícitamente para una tecnología o disciplina.
- **AGENT:** rol que ejecuta una responsabilidad delimitada utilizando las Rules aplicables y las Skills activas.
- **META / ORCHESTRATION:** instrucciones para descubrir, seleccionar, ordenar y aplicar los componentes del repositorio.
- **DOCUMENTATION:** contenido informativo que explica el repositorio y no crea obligaciones por sí mismo.

`RULE` significa obligación. `PRACTICE` significa recomendación no obligatoria. Una `SKILL` especializa una ejecución cuando está activa. Un `AGENT` organiza una responsabilidad de ejecución y no constituye una nueva fuente normativa.

La clasificación de un documento no puede inferirse únicamente por su ubicación o nombre; debe estar declarada por este contrato o por el gobierno del módulo correspondiente.

---

# Prioridad de instrucciones

Las instrucciones deben aplicarse en el siguiente orden:

1. Requisitos explícitos del usuario.
2. Restricciones técnicas del proyecto.
3. Este repositorio (`ai-rules`).
4. Buenas prácticas de ingeniería.
5. Preferencias de estilo del asistente.

Cuando exista un conflicto, deberá prevalecer el elemento de mayor prioridad.

Esta prioridad no autoriza a una Skill, un Agent, una práctica o una preferencia de implementación a debilitar Rules aplicables de Security o Engineering. Los requisitos de negocio y las decisiones de autorización deben provenir de una fuente explícita y verificable; nunca deben inventarse para resolver un conflicto o completar información ausente.

Dentro de `ai-rules`, aplicar la siguiente precedencia simple:

1. Security Rules aplicables.
2. Engineering Rules aplicables.
3. Skills activas como especialización.
4. Best Practices como recomendaciones.
5. Agents como roles de ejecución sujetos a todos los elementos anteriores.

Un Agent no puede redefinir Rules ni activar Skills. Ante un conflicto ambiguo, aplicar la alternativa segura que preserve las obligaciones superiores, informar el conflicto y no inventar la decisión ausente.

---

# Flujo obligatorio de trabajo

Antes de escribir código:

1. Comprender completamente el problema.
2. Identificar restricciones técnicas.
3. Identificar riesgos.
4. Consultar los módulos correspondientes.
5. Planificar la solución.
6. Implementar.
7. Ejecutar la auto verificación.
8. Informar cualquier incumplimiento detectado.

Nunca comenzar implementando código sin comprender previamente el problema.

---

# Selección de módulos

Consultar únicamente los módulos necesarios para la tarea.

## Seguridad

Consultar:

[`security/SECURITY-INDEX.md`](./security/SECURITY-INDEX.md)

e identificar los documentos aplicables.

---

## Ingeniería

Consultar:

[`engineering/ENGINEERING-INDEX.md`](./engineering/ENGINEERING-INDEX.md)

e identificar los estándares correspondientes.

---

## Skills

Las skills son especializaciones opcionales y no forman parte de las reglas globales del repositorio.

La existencia de una skill en el catálogo no implica su activación. Identificar una tecnología, dependencia, extensión, framework, archivo, paquete, import o directorio tampoco activa skills.

La única fuente estándar de activación es [`ACTIVE-SKILLS.md`](../ACTIVE-SKILLS.md), ubicado exactamente en el **Consumer Project Root** definido para la ejecución. No buscar este manifest de forma recursiva, ascendente, descendente ni aproximada.

Si el archivo no existe en esa ubicación, existen cero skills activas.

Cuando exista, cargar únicamente las skills cuyos identificadores estén declarados explícitamente. Una skill no activa otras skills y sus relaciones son únicamente informativas.

Las reglas aplicables de Security y Engineering continúan siendo obligatorias. Una skill activa puede especializarlas, concretarlas o complementarlas, pero nunca omitirlas, relajarlas, reemplazarlas, neutralizarlas ni redefinirlas.

El orden de declaración no concede precedencia. Ante un conflicto entre skills, informar el conflicto, suspender la decisión afectada y continuar únicamente con trabajo independiente y seguro.

Consultar [`skills/README.md`](./skills/README.md) para el gobierno completo del subsistema.

---

## Agents

Los Agents son roles de ejecución opcionales y delimitados. Utilizan las Rules aplicables y las Skills ya activas, pero no crean autoridad normativa, no activan Skills y no pueden redefinir requisitos, arquitectura, Security ni Engineering.

Consultar [`agents/README.md`](./agents/README.md) para el contrato de futuros subagentes.

---

## Plantillas

Consultar:

[`templates/README.md`](./templates/README.md)

cuando sea necesario crear nuevos documentos.

---

# Principios generales

Durante cualquier implementación la IA deberá:

- Comprender antes de implementar.
- Diseñar antes de programar.
- Preferir soluciones simples.
- Minimizar complejidad.
- Minimizar acoplamiento.
- Maximizar cohesión.
- Aplicar separación de responsabilidades.
- Favorecer mantenibilidad.
- Favorecer legibilidad.
- Favorecer consistencia.
- Favorecer reutilización.

---

# Seguridad

Toda implementación deberá cumplir las reglas del módulo Security.

No se podrán omitir controles de seguridad para acelerar el desarrollo.

Cuando una regla de seguridad entre en conflicto con una decisión de implementación, deberá prevalecer la alternativa más segura.

---

# Cambios

Los cambios deberán ser:

- mínimos
- localizados
- trazables
- justificables

No modificar componentes no relacionados.

No introducir refactorizaciones innecesarias.

No modificar contratos públicos salvo autorización explícita.

---

# Supuestos

Nunca asumir:

- reglas de negocio
- permisos
- configuraciones
- formatos
- contratos
- valores
- comportamientos

Si una información crítica no existe deberá indicarse explícitamente.

Cuando la información crítica ausente corresponda a una decisión de autorización, reportarla no equivale a resolverla. La decisión deberá permanecer pendiente conforme a [`security/A01-Broken-Access-Control.md`](./security/A01-Broken-Access-Control.md) y [`engineering/Authorization.md`](./engineering/Authorization.md).

---

# Código

Todo código generado deberá ser:

- consistente
- mantenible
- legible
- determinista
- validable
- revisable

Evitar soluciones excesivamente complejas.

---

# Dependencias

Antes de incorporar nuevas dependencias verificar:

- necesidad
- mantenimiento
- compatibilidad
- licencia
- impacto

No incorporar dependencias innecesarias.

---

# Errores

Los errores deberán:

- ser manejados
- ser registrados cuando corresponda
- no exponer información sensible
- mantener trazabilidad

---

# Validaciones

Toda entrada externa deberá validarse antes de utilizarse.

Nunca confiar en datos provenientes del cliente.

---

# Revisión final

Antes de finalizar cualquier tarea verificar:

- Se cumplieron los requisitos.
- Se aplicaron las reglas del módulo Security.
- Se aplicaron los estándares de Engineering.
- No existen cambios innecesarios.
- No existen riesgos evidentes.
- No existen inconsistencias.
- La solución puede mantenerse a largo plazo.

---

# Si existe un conflicto

Cuando dos reglas parezcan contradictorias:

1. Aplicar la más restrictiva desde el punto de vista de la seguridad.
2. Aplicar la que preserve la estabilidad del sistema.
3. Informar el conflicto al usuario.

Nunca resolver conflictos mediante suposiciones.

---

# Objetivo final

El objetivo no es únicamente generar código.

El objetivo es producir soluciones de calidad profesional, seguras, mantenibles y alineadas con los estándares definidos por este repositorio.
