# OWASP A06 – Vulnerable and Outdated Components

## Objetivo del documento

Este documento forma parte del repositorio **ai-rules** y define las reglas obligatorias que toda IA debe aplicar al seleccionar, instalar, actualizar, mantener o reemplazar componentes de terceros utilizados por una aplicación.

Las reglas de este documento complementan las instrucciones del proyecto y deben aplicarse antes de incorporar cualquier dependencia al proyecto.

---

## Prioridad

**Nivel:** Crítico

El uso de componentes vulnerables, obsoletos o sin mantenimiento puede comprometer completamente una aplicación, independientemente de la calidad del código desarrollado.

---

## Propósito

Garantizar que todos los componentes de terceros utilizados por la solución sean confiables, mantenidos activamente, conocidos por el equipo y gestionados durante todo su ciclo de vida.

---

## Cuándo consultar este documento

Consultar este documento antes de:

- Instalar librerías.
- Instalar paquetes NuGet.
- Instalar paquetes NPM.
- Instalar paquetes Maven.
- Instalar paquetes Gradle.
- Instalar paquetes PyPI.
- Incorporar SDKs.
- Incorporar frameworks.
- Incorporar componentes Open Source.
- Incorporar imágenes Docker.
- Incorporar imágenes base.
- Utilizar Actions de GitHub.
- Utilizar módulos Terraform.
- Incorporar servicios de terceros.
- Actualizar dependencias.
- Publicar una nueva versión del sistema.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- security/A04-Insecure-Design.md
- security/A08-Software-and-Data-Integrity-Failures.md

Consultar también cuando corresponda:

- engineering/Dependencies.md

---

## No cubre

- Diseño general de la arquitectura.
- Integridad de datos de negocio.
- Autenticación y autorización.
- Configuración operativa de infraestructura.

---

## Riesgos mitigados

La aplicación de este documento busca reducir los riesgos asociados a:

- Componentes vulnerables.
- Dependencias obsoletas.
- Dependencias sin mantenimiento.
- Vulnerabilidades conocidas (CVE).
- Dependencias maliciosas.
- Ataques a la cadena de suministro (Supply Chain).
- Dependencias transitivas vulnerables.
- Versiones no soportadas.
- Componentes sin origen verificable.

---

## Reglas obligatorias

### Selección de componentes

- Seleccionar únicamente componentes provenientes de fuentes confiables.
- Preferir componentes mantenidos activamente.
- Verificar la reputación del proyecto antes de incorporarlo.
- Evaluar el nivel de mantenimiento antes de adoptar una dependencia.
- Documentar el propósito de cada componente incorporado.

### Gestión de dependencias

- Mantener un inventario actualizado de dependencias directas.
- Identificar todas las dependencias transitivas detectadas por la herramienta de inventario.
- Eliminar dependencias que ya no sean utilizadas.
- Minimizar la cantidad total de dependencias del proyecto.
- Evitar incorporar múltiples componentes que resuelvan el mismo problema.

### Versiones

- Utilizar versiones estables.
- Mantener las dependencias dentro de versiones soportadas.
- Revisar la disponibilidad de actualizaciones según la frecuencia definida por el proyecto.
- Planificar la actualización de componentes antes de que finalice su soporte.
- Mantener consistencia de versiones entre ambientes.

### Vulnerabilidades

- Revisar vulnerabilidades conocidas antes de incorporar una dependencia.
- Evaluar el impacto de cada vulnerabilidad detectada.
- Actualizar componentes vulnerables cuando exista una corrección disponible.
- Documentar cualquier excepción cuando una actualización no pueda aplicarse inmediatamente.
- Definir controles compensatorios mientras exista una vulnerabilidad pendiente.

### Imágenes y contenedores

- Utilizar imágenes base oficiales o verificadas.
- Mantener imágenes base actualizadas.
- Reducir el contenido de las imágenes al mínimo necesario.
- Eliminar herramientas innecesarias de las imágenes de producción.
- Revisar vulnerabilidades de las imágenes antes del despliegue.

### Integraciones

- Validar el origen de SDKs y bibliotecas de terceros.
- Revisar los permisos solicitados por componentes externos.
- Limitar el acceso de componentes externos a los recursos estrictamente necesarios.
- Revisar la necesidad de mantener cada integración según la frecuencia definida por el proyecto.

### Auditoría

- Registrar cambios importantes de dependencias.
- Registrar actualizaciones de componentes críticos.
- Mantener evidencia de cada revisión de seguridad registrada para una dependencia.

---

## Acciones prohibidas

- Nunca utilizar componentes sin conocer su origen.
- Nunca incorporar dependencias abandonadas cuando exista una alternativa mantenida.
- Nunca utilizar versiones sin soporte.
- Nunca ignorar vulnerabilidades críticas conocidas.
- Nunca descargar dependencias desde fuentes no verificadas.
- Nunca instalar paquetes innecesarios.
- Nunca incorporar componentes únicamente por comodidad.
- Nunca utilizar imágenes Docker sin revisar su procedencia.
- Nunca utilizar imágenes con privilegios innecesarios.
- Nunca mantener dependencias vulnerables sin una justificación documentada.
- Nunca instalar dependencias duplicadas con funcionalidades equivalentes.
- Nunca asumir que un componente popular es automáticamente seguro.
- Nunca actualizar componentes críticos directamente en producción sin validación previa.
- Nunca eliminar controles de seguridad para mantener compatibilidad con componentes antiguos.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] Todas las dependencias provienen de fuentes confiables.
- [ ] Todas las dependencias se encuentran soportadas.
- [ ] No existen dependencias innecesarias.
- [ ] Las vulnerabilidades conocidas fueron revisadas.
- [ ] Los componentes críticos están actualizados.
- [ ] Existe un inventario de dependencias.
- [ ] Las imágenes base fueron revisadas.
- [ ] Las dependencias transitivas fueron evaluadas cuando corresponde.
- [ ] Las excepciones de seguridad están documentadas.
- [ ] Los componentes externos poseen únicamente los permisos necesarios.
- [ ] Las actualizaciones fueron evaluadas antes del despliegue.

---

## Referencias

- OWASP Top 10 2021 – A06: Vulnerable and Outdated Components
- OWASP ASVS
- OWASP Dependency-Check
- OWASP Software Component Verification Standard (SCVS)
- OWASP CycloneDX
- NIST Secure Software Development Framework (SSDF)
- CWE-937
- CWE-1035
- CWE-1104
