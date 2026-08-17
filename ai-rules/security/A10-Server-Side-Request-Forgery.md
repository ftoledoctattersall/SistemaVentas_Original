# OWASP A10 – Server-Side Request Forgery (SSRF)

## Objetivo del documento

Este documento forma parte del repositorio **ai-rules** y define las reglas obligatorias que toda IA debe aplicar cuando implemente funcionalidades que permitan al servidor establecer comunicaciones hacia sistemas internos o externos.

Las reglas de este documento complementan las instrucciones del proyecto y deben aplicarse antes de generar cualquier código que realice solicitudes de red iniciadas por el servidor.

---

## Prioridad

**Nivel:** Crítico

El incumplimiento de cualquiera de las reglas de este documento puede permitir que un atacante utilice el servidor para acceder a recursos internos, servicios protegidos, infraestructura cloud o sistemas que normalmente no serían accesibles desde el exterior.

---

## Propósito

Garantizar que toda comunicación iniciada por el servidor sea validada, controlada y restringida para impedir accesos no autorizados a recursos internos o externos.

---

## Cuándo consultar este documento

Consultar este documento antes de implementar o modificar:

- Clientes HTTP.
- Clientes HTTPS.
- Webhooks.
- Integraciones REST.
- Integraciones SOAP.
- gRPC.
- Descarga de archivos.
- Consumo de APIs externas.
- Consumo de servicios internos.
- Integraciones cloud.
- Azure Storage.
- AWS S3.
- Google Cloud Storage.
- Llamadas entre microservicios.
- Procesamiento de URLs.
- Importación de archivos desde Internet.
- Servicios de notificación.
- Gateways.
- Proxies.
- Reverse Proxies.
- Balanceadores.
- DNS dinámico.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- security/A03-Injection.md
- security/A05-Security-Misconfiguration.md
- security/A09-Security-Logging-and-Monitoring-Failures.md

Consultar también cuando corresponda:

- engineering/API.md
- engineering/Error-Handling.md

---

## No cubre

- Autenticación de usuarios.
- Autorización de recursos de negocio.
- Gestión general de secretos.
- Vulnerabilidades generales de dependencias.

---

## Riesgos mitigados

La aplicación de este documento busca reducir los riesgos asociados a:

- Server-Side Request Forgery (SSRF).
- Acceso a redes internas.
- Escaneo interno de infraestructura.
- Acceso a servicios administrativos.
- Acceso al Metadata Service de plataformas cloud.
- Exposición de servicios internos.
- Exfiltración de información.
- Pivoting dentro de la red.
- Consumo no autorizado de recursos internos.

---

## Reglas obligatorias

### Validación de destinos

- Validar todas las URLs antes de utilizarlas.
- Permitir únicamente protocolos explícitamente autorizados.
- Validar el esquema de la URL.
- Validar el host de destino.
- Validar el puerto contra la lista de puertos permitidos.
- Normalizar la URL antes de procesarla.
- Rechazar URLs inválidas.

### Control de acceso

- Utilizar una lista de destinos autorizados para toda solicitud saliente.
- Rechazar toda solicitud cuyo destino no pertenezca a la lista de destinos autorizados.
- Definir cada destino autorizado mediante esquema, nombre de host y puerto permitido.
- Validar el destino antes de establecer la conexión.
- Limitar las comunicaciones entre componentes según el principio de mínimo privilegio.

### Comunicaciones

- Establecer tiempos máximos de espera para todas las conexiones.
- Limitar el tamaño máximo de las respuestas mediante la configuración del cliente.
- Deshabilitar redirecciones automáticas.
- Validar cada destino de redirección contra la lista de destinos autorizados antes de seguirlo.
- Registrar errores de validación, resolución DNS, conexión y redirección.

### Infraestructura

- Restringir el acceso a redes internas a los destinos incluidos en la lista de destinos autorizados.
- Restringir el acceso a servicios administrativos.
- Proteger los servicios de metadata de plataformas cloud.
- Limitar el acceso a recursos locales.
- Aplicar segmentación de red entre el servidor y los destinos autorizados.

### Datos

- Validar cualquier URL proporcionada por usuarios.
- Validar cualquier dirección IP proporcionada externamente contra la lista de destinos autorizados.
- Resolver cada nombre DNS y validar todas las direcciones IP resultantes contra la lista de destinos autorizados antes de conectar.
- Procesar únicamente destinos incluidos en la lista de destinos autorizados.

### Auditoría

- Registrar solicitudes rechazadas por políticas SSRF.
- Registrar errores de validación de destinos.
- Registrar intentos de acceso a recursos restringidos.
- Registrar cambios en listas de destinos autorizados.

---

## Acciones prohibidas

- Nunca utilizar directamente una URL proporcionada por el usuario.
- Nunca permitir conexiones hacia destinos no validados.
- Nunca permitir protocolos no autorizados.
- Nunca confiar únicamente en validaciones realizadas por el cliente.
- Nunca permitir acceso a direcciones internas que no pertenezcan a la lista de destinos autorizados.
- Nunca permitir acceso al Metadata Service de plataformas cloud.
- Nunca seguir redirecciones sin validarlas.
- Nunca utilizar listas de bloqueo como único mecanismo de protección.
- Nunca asumir que una dirección DNS permanecerá inalterada después de ser validada.
- Nunca permitir conexiones ilimitadas hacia destinos externos.
- Nunca omitir registros de eventos relacionados con solicitudes rechazadas.
- Nunca utilizar configuraciones de red excesivamente permisivas.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] Todas las URLs son validadas antes de utilizarse.
- [ ] Todos los destinos pertenecen a la lista de destinos autorizados.
- [ ] Todos los protocolos permitidos están definidos explícitamente.
- [ ] Las redirecciones son controladas.
- [ ] Existen tiempos máximos de espera para las conexiones.
- [ ] Las solicitudes a recursos internos están restringidas.
- [ ] Los servicios de metadata cloud no son accesibles.
- [ ] Los intentos de acceso no autorizados generan auditoría.
- [ ] Los errores de validación, resolución DNS, conexión y redirección son registrados.
- [ ] Ninguna solicitud utiliza directamente información proporcionada por usuarios sin validación.

---

## Referencias

- OWASP Top 10 2021 – A10: Server-Side Request Forgery (SSRF)
- OWASP ASVS
- OWASP SSRF Prevention Cheat Sheet
- OWASP Web Security Testing Guide (WSTG)
- NIST Secure Software Development Framework (SSDF)
- CWE-918
- CWE-441
