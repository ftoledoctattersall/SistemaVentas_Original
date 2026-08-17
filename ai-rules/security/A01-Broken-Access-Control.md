# OWASP A01 – Broken Access Control

## Objetivo del documento

Este documento forma parte del repositorio **ai-rules** y define las reglas obligatorias que toda IA debe aplicar cuando implemente funcionalidades relacionadas con el control de acceso.

Las reglas de este documento complementan las instrucciones del proyecto y deben aplicarse antes de generar cualquier código.

---

## Prioridad

**Nivel:** Crítico

El incumplimiento de cualquiera de las reglas de este documento puede generar vulnerabilidades clasificadas como **Broken Access Control** y permitir acceso no autorizado a funcionalidades, recursos o información sensible.

---

## Propósito

Establecer las reglas obligatorias para prevenir accesos no autorizados a funcionalidades, recursos y datos de la aplicación.

Toda implementación que incluya autenticación, autorización o acceso a recursos protegidos debe cumplir las reglas definidas en este documento antes de considerarse terminada.

---

## Cuándo consultar este documento

Consultar este documento antes de implementar o modificar funcionalidades que involucren:

- APIs REST, GraphQL o gRPC.
- Endpoints protegidos.
- Autenticación de usuarios.
- Autorización basada en roles o permisos.
- JWT, OAuth2, OpenID Connect o mecanismos equivalentes.
- Multiempresa (Multi Company).
- Multitenancy.
- ACL (Access Control Lists).
- Recursos privados.
- Operaciones CRUD.
- Archivos protegidos.
- Reportes y exportaciones.
- Funcionalidades administrativas.
- Recursos pertenecientes a otros usuarios, empresas o tenants.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- security/A07-Identification-and-Authentication-Failures.md
- security/A09-Security-Logging-and-Monitoring-Failures.md

Consultar también cuando corresponda:

- engineering/Authorization.md
- engineering/Authentication.md
- engineering/Logging.md

---

## No cubre

- Gestión de contraseñas y autenticación.
- Criptografía y gestión de secretos.
- Logging general y monitoreo.
- Configuración de infraestructura.

---

## Riesgos mitigados

La aplicación de este documento busca reducir los riesgos asociados a:

- Broken Access Control.
- Insecure Direct Object Reference (IDOR).
- Horizontal Privilege Escalation.
- Vertical Privilege Escalation.
- Cross Tenant Access.
- Cross Company Access.
- Forced Browsing.
- Elevación de privilegios.
- Bypass de autorización.
- Exposición de recursos protegidos.

---

## Reglas obligatorias

### Autenticación

- Verificar la autenticación antes de evaluar cualquier autorización.
- Rechazar inmediatamente solicitudes no autenticadas cuando el recurso lo requiera.
- Obtener la identidad del usuario únicamente desde el mecanismo oficial de autenticación.
- Validar que la identidad autenticada corresponda al contexto de ejecución.

### Autorización

- Verificar la autorización en el servidor para cada solicitud cuando la operación o el recurso requieran autorización.
- Evaluar la autorización antes de ejecutar cualquier lógica de negocio cuando la operación se encuentre protegida.
- Validar permisos para cada operación individual que requiera autorización.
- Validar los permisos requeridos independientemente del método HTTP utilizado.
- Aplicar el principio de mínimo privilegio.
- Aplicar el principio de denegación por defecto cuando no exista una autorización explícita.
- Separar claramente autenticación y autorización.
- Cuando corresponda evaluar permisos de un usuario, utilizar siempre la identidad autenticada obtenida mediante el mecanismo oficial.
- Denegar el acceso cuando exista cualquier duda respecto a la autorización.
- Validar nuevamente la autorización cuando cambie el contexto de seguridad.

### Autorización no definida

Una decisión de autorización se considera **pendiente** cuando una operación o recurso requiere control de acceso y no existe una fuente explícita y verificable que defina quién puede acceder y bajo qué condiciones. Dicha fuente puede definir sujetos autorizables, capacidades, atributos, relaciones con el recurso, ownership u otros criterios equivalentes, o declarar explícitamente que el acceso es público o que sólo requiere autenticación.

Cuando una fuente autoritativa declare inequívocamente que una operación es pública, el acceso se encuentra definido y no deberá inventarse autorización adicional. La operación deberá mantener los demás controles de seguridad que correspondan.

Cuando una fuente autoritativa declare inequívocamente que una operación requiere identidad autenticada pero no autorización adicional, deberá exigirse autenticación y no deberán inventarse permisos, roles, capacidades, ownership ni otros criterios de autorización. Esta definición no convierte autenticación en autorización y no constituye una decisión de autorización pendiente.

Mientras la decisión permanezca pendiente:

- No considerar que la operación se encuentra correctamente autorizada.
- No interpretar la ausencia de requisitos como acceso público.
- No inferir autorización a partir de la autenticación.
- No utilizar identificadores recibidos desde el cliente como identidad del actor autenticado.
- No crear roles, permisos, capacidades, ownership, membresías ni otras reglas de acceso.
- No ampliar el alcance de autorizaciones existentes.
- No reutilizar una autorización existente sin una correspondencia explícita de propósito, operación, recurso y alcance.
- Identificar la operación afectada, informar la definición ausente y mantener pendiente la decisión de autorización.
- Detener la decisión de autorización afectada hasta disponer de una fuente explícita y verificable.

Informar la ausencia de una definición no resuelve la autorización. La decisión podrá reanudarse cuando exista una fuente autoritativa verificable, como un requisito explícito del producto, una regla de negocio existente, una autorización existente cuyo alcance incluya inequívocamente la operación o una declaración explícita sobre acceso público o acceso únicamente autenticado.

La denegación por defecto significa no conceder acceso cuando no existe una autorización explícita. No determina quién deberá recibir acceso posteriormente y no autoriza a inventar una condición de acceso permitido.

### Recursos

- Validar la propiedad del recurso antes de permitir acceso cuando el modelo o los requisitos definan dicha relación.
- Validar la propiedad del recurso antes de permitir modificaciones cuando el modelo o los requisitos definan dicha relación.
- Validar la propiedad del recurso antes de permitir eliminación cuando el modelo o los requisitos definan dicha relación.
- Nunca inventar ownership ni relaciones equivalentes para completar una decisión de autorización.
- Mantener pendiente la decisión cuando el control de acceso dependa de una relación con el recurso que no se encuentre definida.
- Validar autorización para archivos descargables.
- Validar autorización para reportes.
- Validar autorización para exportaciones.
- Validar autorización para operaciones masivas.
- Validar autorización para operaciones administrativas.
- Proteger todos los endpoints sensibles.
- Aplicar controles de autorización sobre todos los recursos expuestos que una fuente autoritativa defina como protegidos.

### Multiempresa y Multitenancy

- Validar el contexto de empresa en cada solicitud.
- Validar el contexto del tenant antes de acceder a cualquier dato.
- Impedir acceso entre empresas.
- Impedir acceso entre tenants.
- Limitar todas las consultas al contexto autorizado.
- Garantizar aislamiento lógico entre empresas y tenants.

### Administración

- Restringir funciones administrativas exclusivamente a usuarios autorizados.
- Validar privilegios elevados antes de ejecutar operaciones críticas.
- Aplicar separación de funciones cuando existan privilegios administrativos.
- Revisar explícitamente permisos administrativos antes de cualquier operación privilegiada.

### Auditoría

- Registrar todos los accesos denegados.
- Registrar cambios de roles.
- Registrar cambios de permisos.
- Registrar elevaciones de privilegios.
- Registrar intentos de acceso a recursos protegidos.
- Registrar accesos administrativos.

---

## Acciones prohibidas

- Nunca confiar en permisos enviados por el cliente.
- Nunca utilizar únicamente el frontend para aplicar autorización.
- Nunca asumir permisos debido a autenticaciones anteriores.
- Nunca reutilizar autorizaciones obtenidas fuera del contexto actual.
- Nunca omitir verificaciones de autorización por motivos de rendimiento.
- Nunca exponer recursos utilizando únicamente identificadores secuenciales.
- Nunca permitir acceso directo mediante URLs predecibles.
- Nunca confiar en parámetros enviados por el usuario para determinar privilegios.
- Nunca compartir datos entre empresas.
- Nunca compartir datos entre tenants.
- Nunca utilizar información de otra sesión para autorizar una solicitud.
- Nunca permitir que un usuario seleccione libremente el contexto de empresa o tenant sin validación.
- Nunca deshabilitar controles de autorización durante pruebas o depuración.
- Nunca ejecutar lógica de negocio antes de validar los permisos requeridos por una operación protegida.
- Nunca asumir que un usuario posee permisos administrativos.
- Nunca exponer funcionalidades ocultándolas únicamente desde la interfaz gráfica.
- Nunca devolver información protegida cuando falle una validación de autorización.
- Nunca revelar información que permita inferir la existencia de recursos protegidos.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] Todos los recursos protegidos verifican autenticación.
- [ ] Todos los recursos protegidos verifican autorización.
- [ ] Todas las operaciones CRUD que requieren autorización validan sus permisos.
- [ ] Los recursos protegidos validan ownership cuando dicha relación se encuentra definida o aplican el criterio de autorización establecido por el modelo o los requisitos.
- [ ] Existe aislamiento entre empresas.
- [ ] Existe aislamiento entre tenants.
- [ ] Los permisos administrativos se validan explícitamente.
- [ ] No existen endpoints protegidos sin autorización.
- [ ] No existen recursos accesibles únicamente mediante identificadores.
- [ ] El acceso se deniega por defecto cuando no existe autorización explícita.
- [ ] Ninguna condición de acceso permitido fue inventada para satisfacer la denegación por defecto.
- [ ] Toda decisión de autorización pendiente fue informada y no se consideró resuelta.
- [ ] Las autorizaciones existentes sólo se reutilizan cuando su alcance incluye explícitamente la operación.
- [ ] Todos los accesos denegados generan auditoría.
- [ ] Los cambios de privilegios generan auditoría.
- [ ] Ninguna validación depende exclusivamente del frontend.

---

## Referencias

- OWASP Top 10 2021 – A01: Broken Access Control
- OWASP ASVS
- CWE-22
- CWE-284
- CWE-285
- CWE-639
- NIST Secure Software Development Framework (SSDF)
