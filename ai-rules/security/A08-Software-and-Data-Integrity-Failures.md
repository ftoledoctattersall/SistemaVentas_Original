# OWASP A08 – Software and Data Integrity Failures

## Objetivo del documento

Este documento forma parte del repositorio **ai-rules** y define las reglas obligatorias que toda IA debe aplicar para garantizar la integridad del software, de los datos y de la cadena de construcción, evitando la ejecución o distribución de componentes manipulados o no confiables.

Las reglas de este documento complementan las instrucciones del proyecto y deben aplicarse antes de generar código, automatizaciones, procesos de construcción o despliegues.

---

## Prioridad

**Nivel:** Crítico

El incumplimiento de cualquiera de las reglas de este documento puede permitir la ejecución de software alterado, la distribución de artefactos comprometidos o la modificación no autorizada de datos y procesos de construcción.

---

## Propósito

Garantizar que todo artefacto utilizado o generado por la aplicación conserve su integridad desde el desarrollo hasta la operación, verificando el origen, autenticidad y consistencia del software y de los datos críticos.

---

## Cuándo consultar este documento

Consultar este documento antes de implementar o modificar:

- Pipelines CI/CD.
- GitHub Actions.
- Azure DevOps.
- GitLab CI.
- Jenkins.
- Automatizaciones de despliegue.
- Scripts de instalación.
- Scripts PowerShell.
- Scripts Bash.
- Dockerfiles.
- Imágenes Docker.
- Firmas digitales.
- Publicación de paquetes.
- Publicación de artefactos.
- Actualizaciones automáticas.
- Procesos ETL.
- Importación de datos.
- Exportación de datos.
- Integraciones automáticas.
- Procesos batch.
- Webhooks.
- Sincronización entre sistemas.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- security/A02-Cryptographic-Failures.md
- security/A06-Vulnerable-and-Outdated-Components.md
- security/A09-Security-Logging-and-Monitoring-Failures.md

Consultar también cuando corresponda:

- Dependencies.md
- Testing.md

---

## No cubre

- Gestión general de dependencias vulnerables.
- Autenticación y autorización.
- Logging operativo general.
- Configuración de infraestructura.

---

## Riesgos mitigados

La aplicación de este documento busca reducir los riesgos asociados a:

- Manipulación de artefactos.
- Manipulación de datos.
- Ejecución de software alterado.
- Ataques a la cadena de suministro.
- Dependencias comprometidas.
- Ejecución de scripts maliciosos.
- Publicación de artefactos inseguros.
- Actualizaciones no verificadas.
- Integridad no verificada de datos críticos.

---

## Reglas obligatorias

### Integridad del software

- Verificar el origen de todos los artefactos antes de utilizarlos.
- Utilizar únicamente repositorios oficiales o autorizados.
- Validar la integridad de artefactos críticos cuando existan mecanismos de verificación.
- Firmar digitalmente los artefactos cuando la arquitectura lo requiera.
- Mantener trazabilidad del proceso de construcción.

### Procesos de construcción

- Automatizar la construcción utilizando procesos repetibles.
- Mantener configuraciones de construcción bajo control de versiones.
- Utilizar pipelines reproducibles.
- Validar cada etapa crítica del proceso de construcción.
- Proteger los procesos de publicación de artefactos.

### Automatización

- Revisar todos los scripts antes de incorporarlos al proyecto.
- Mantener los scripts bajo control de versiones.
- Limitar los permisos de ejecución de automatizaciones.
- Validar el origen de cualquier script descargado.
- Revisar las automatizaciones antes de cada modificación significativa.

### Datos

- Validar la integridad de los datos críticos antes de procesarlos.
- Validar la integridad después de procesos de importación.
- Detectar modificaciones no autorizadas mediante controles de integridad configurados.
- Mantener consistencia durante procesos de sincronización.
- Registrar eventos relacionados con modificaciones críticas.

### Actualizaciones

- Verificar el origen de las actualizaciones.
- Validar la autenticidad de los paquetes antes de instalarlos.
- Revisar el impacto de una actualización antes de desplegarla.
- Mantener procedimientos documentados de reversión.
- Validar los artefactos generados antes de publicarlos.

### Auditoría

- Registrar publicaciones de artefactos.
- Registrar cambios en pipelines.
- Registrar modificaciones de automatizaciones.
- Registrar cambios de configuración relacionados con procesos de construcción.
- Registrar eventos que afecten la integridad del software.

---

## Acciones prohibidas

- Nunca ejecutar scripts descargados sin revisión previa.
- Nunca utilizar artefactos cuyo origen no pueda verificarse.
- Nunca modificar artefactos publicados manualmente.
- Nunca ejecutar procesos de construcción fuera del pipeline definido.
- Nunca publicar artefactos sin validación previa.
- Nunca omitir controles de integridad por motivos de velocidad.
- Nunca instalar paquetes desde repositorios no autorizados.
- Nunca utilizar automatizaciones cuyo contenido no haya sido revisado.
- Nunca deshabilitar verificaciones de integridad durante el despliegue.
- Nunca reemplazar artefactos publicados sin mantener trazabilidad.
- Nunca modificar datos críticos sin registrar el evento correspondiente.
- Nunca asumir que un proceso automatizado es seguro únicamente por estar automatizado.
- Nunca ejecutar código recibido desde fuentes externas sin validación.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] Todos los artefactos provienen de fuentes autorizadas.
- [ ] Todos los scripts fueron revisados antes de utilizarse.
- [ ] Los procesos de construcción son reproducibles.
- [ ] Los pipelines se encuentran bajo control de versiones.
- [ ] Los artefactos publicados fueron validados.
- [ ] Existe trazabilidad del proceso de construcción.
- [ ] Las actualizaciones verifican autenticidad.
- [ ] Los datos críticos validan integridad.
- [ ] Las modificaciones de artefactos, pipelines y automatizaciones generan auditoría.
- [ ] No existen procesos manuales que alteren artefactos publicados.

---

## Referencias

- OWASP Top 10 2021 – A08: Software and Data Integrity Failures
- OWASP ASVS
- OWASP Software Component Verification Standard (SCVS)
- OWASP CI/CD Security Guidance
- NIST Secure Software Development Framework (SSDF)
- SLSA (Supply-chain Levels for Software Artifacts)
- CWE-353
- CWE-494
- CWE-829
