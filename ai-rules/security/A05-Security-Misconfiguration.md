# OWASP A05 – Security Misconfiguration

## Objetivo del documento

Este documento forma parte del repositorio **ai-rules** y define las reglas obligatorias que toda IA debe aplicar al configurar aplicaciones, servidores, frameworks, librerías, infraestructura, contenedores y servicios.

Las reglas de este documento complementan las instrucciones del proyecto y deben aplicarse antes de generar cualquier código, configuración o proceso de despliegue.

---

## Prioridad

**Nivel:** Crítico

Una configuración insegura puede comprometer completamente una aplicación incluso cuando el código ha sido implementado correctamente.

---

## Propósito

Garantizar que toda aplicación sea desplegada utilizando configuraciones seguras, minimizando la superficie de ataque y evitando configuraciones por defecto, componentes innecesarios o información expuesta.

---

## Cuándo consultar este documento

Consultar este documento antes de implementar o modificar:

- APIs.
- Aplicaciones Web.
- Microservicios.
- Contenedores Docker.
- Kubernetes.
- Servidores Web.
- Reverse Proxy.
- Balanceadores.
- Frameworks.
- Middleware.
- CORS.
- HTTPS.
- TLS.
- Variables de entorno.
- Configuración de aplicaciones.
- Archivos JSON.
- Archivos YAML.
- Archivos XML.
- Configuración de despliegues.
- Ambientes Development.
- Ambientes QA.
- Ambientes Staging.
- Ambientes Production.
- Servicios Cloud.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- security/A02-Cryptographic-Failures.md
- security/A06-Vulnerable-and-Outdated-Components.md
- security/A09-Security-Logging-and-Monitoring-Failures.md
- security/A10-Server-Side-Request-Forgery.md

Consultar también cuando corresponda:

- engineering/API.md
- engineering/Error-Handling.md
- engineering/Logging.md
- engineering/Dependencies.md

---

## No cubre

- Diseño de controles de seguridad.
- Implementación de autorización.
- Gestión de vulnerabilidades de dependencias.
- Validación de entradas de usuario.

---

## Riesgos mitigados

La aplicación de este documento busca reducir los riesgos asociados a:

- Configuraciones por defecto inseguras.
- Servicios innecesarios expuestos.
- Exposición de información sensible.
- Errores de configuración.
- CORS inseguro.
- Headers HTTP inseguros.
- TLS incorrectamente configurado.
- Directorios públicos innecesarios.
- Consolas administrativas expuestas.
- Ambientes de desarrollo desplegados en producción.

---

## Reglas obligatorias

### Configuración general

- Configurar la aplicación utilizando el principio de configuración segura por defecto.
- Mantener configuraciones independientes para cada ambiente.
- Revisar todas las configuraciones antes del despliegue.
- Eliminar configuraciones obsoletas.
- Eliminar parámetros no utilizados.
- Mantener únicamente funcionalidades necesarias.

### Servicios

- Deshabilitar servicios innecesarios.
- Deshabilitar endpoints de prueba.
- Deshabilitar funcionalidades experimentales en producción.
- Limitar la exposición de servicios administrativos.
- Restringir el acceso a interfaces de administración.

### Frameworks

- Utilizar únicamente funcionalidades necesarias del framework.
- Revisar las configuraciones de seguridad del framework antes de publicar la aplicación.
- Mantener habilitados los mecanismos de protección documentados por el framework.
- Documentar cualquier excepción de configuración.

### HTTP

- Obligar el uso de HTTPS cuando exista información sensible.
- Configurar los encabezados HTTP de seguridad definidos por los requisitos del proyecto.
- Configurar políticas CORS siguiendo el principio de mínimo acceso.
- Limitar los métodos HTTP permitidos.
- Configurar tiempos de espera definidos por los requisitos operativos del servicio.

### Errores

- Mostrar únicamente mensajes de error genéricos al cliente.
- Registrar el detalle técnico únicamente en los sistemas de auditoría.
- Evitar revelar versiones, rutas internas o configuraciones.

### Archivos

- Restringir el acceso a archivos de configuración.
- Proteger archivos de respaldo.
- Proteger archivos temporales.
- Eliminar archivos utilizados únicamente durante el desarrollo.
- Restringir el acceso a documentación interna.

### Despliegue

- Automatizar las configuraciones repetitivas definidas en el proceso de despliegue.
- Validar la configuración antes de cada despliegue.
- Verificar que el ambiente de producción no utilice configuraciones de desarrollo.
- Mantener configuraciones bajo control de versiones cuando no contengan información sensible.

### Infraestructura

- Aplicar el principio de mínimo privilegio a todos los servicios.
- Limitar la comunicación entre componentes.
- Restringir puertos innecesarios.
- Configurar registros de auditoría para cambios de configuración, accesos administrativos y fallos de controles de seguridad.
- Mantener sincronización horaria entre los componentes.

---

## Acciones prohibidas

- Nunca utilizar configuraciones por defecto en producción.
- Nunca publicar credenciales de ejemplo.
- Nunca habilitar modo Debug en producción.
- Nunca desplegar funcionalidades experimentales sin autorización.
- Nunca habilitar CORS para cualquier origen sin una justificación documentada.
- Nunca exponer paneles administrativos públicamente.
- Nunca publicar documentación técnica interna.
- Nunca exponer archivos de configuración.
- Nunca almacenar información sensible en archivos públicos.
- Nunca permitir listado de directorios.
- Nunca dejar habilitados usuarios de prueba.
- Nunca utilizar certificados expirados o inválidos.
- Nunca revelar versiones del software cuando no sea necesario.
- Nunca mantener endpoints utilizados únicamente para pruebas.
- Nunca reutilizar configuraciones de desarrollo en producción.
- Nunca asumir que la configuración por defecto del framework cumple los requisitos de seguridad del proyecto.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] No existen configuraciones por defecto inseguras.
- [ ] No existen servicios innecesarios habilitados.
- [ ] No existen endpoints de prueba publicados.
- [ ] No existen usuarios de prueba habilitados.
- [ ] La aplicación utiliza HTTPS cuando corresponde.
- [ ] CORS se encuentra restringido.
- [ ] Los encabezados HTTP de seguridad están configurados.
- [ ] Los mensajes de error no revelan información técnica.
- [ ] Los archivos de configuración están protegidos.
- [ ] No existen archivos temporales publicados.
- [ ] El ambiente de producción utiliza una configuración específica.
- [ ] No existen paneles administrativos expuestos públicamente.
- [ ] Las configuraciones fueron revisadas antes del despliegue.

---

## Referencias

- OWASP Top 10 2021 – A05: Security Misconfiguration
- OWASP ASVS
- OWASP Secure Headers Project
- OWASP Docker Security Cheat Sheet
- OWASP Kubernetes Top 10
- OWASP Transport Layer Security Cheat Sheet
- CWE-2
- CWE-16
- CWE-611
- CWE-933
- NIST Secure Software Development Framework (SSDF)
