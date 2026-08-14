# OWASP A03 – Injection

## Objetivo del documento

Este documento forma parte del repositorio **ai-rules** y define las reglas obligatorias que toda IA debe aplicar cuando implemente funcionalidades que procesen datos provenientes de usuarios, sistemas externos o cualquier fuente no confiable.

Las reglas de este documento complementan las instrucciones del proyecto y deben aplicarse antes de generar cualquier código.

---

## Prioridad

**Nivel:** Crítico

El incumplimiento de cualquiera de las reglas de este documento puede permitir la ejecución de código no autorizado, acceso no autorizado a datos, modificación de información o compromiso completo de la aplicación.

---

## Propósito

Garantizar que ninguna entrada externa pueda alterar la lógica de la aplicación, modificar consultas, ejecutar comandos, manipular expresiones o alterar el comportamiento esperado del sistema.

---

## Cuándo consultar este documento

Consultar este documento antes de implementar funcionalidades que involucren:

- SQL.
- NoSQL.
- Entity Framework.
- Dapper.
- ADO.NET.
- JDBC.
- Hibernate.
- Stored Procedures.
- Consultas dinámicas.
- Motores de búsqueda.
- LDAP.
- XPath.
- XML.
- JSON dinámico.
- GraphQL.
- APIs REST.
- Shell.
- PowerShell.
- Bash.
- CMD.
- Procesamiento de archivos.
- Expresiones dinámicas.
- Plantillas dinámicas.
- Cualquier dato recibido desde usuarios o sistemas externos.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- security/A05-Security-Misconfiguration.md
- security/A09-Security-Logging-and-Monitoring-Failures.md

Consultar también cuando corresponda:

- API.md
- Validation.md
- Database.md
- Error-Handling.md

---

## No cubre

- Autenticación.
- Autorización.
- Gestión de secretos.
- Configuración general de infraestructura.

---

## Riesgos mitigados

La aplicación de este documento busca reducir los riesgos asociados a:

- SQL Injection.
- NoSQL Injection.
- LDAP Injection.
- XPath Injection.
- XML Injection.
- Command Injection.
- OS Command Injection.
- Expression Injection.
- Template Injection.
- GraphQL Injection.
- Server-Side Template Injection (SSTI).
- Manipulación de consultas.
- Ejecución arbitraria de comandos.

---

## Reglas obligatorias

### Validación de entradas

- Considerar toda entrada externa como no confiable.
- Validar todas las entradas antes de utilizarlas.
- Validar tipo de dato.
- Validar longitud.
- Validar formato.
- Validar rango de valores.
- Validar mediante listas de valores permitidos todo valor que controle una opción cerrada.
- Rechazar entradas inválidas inmediatamente.

### Consultas a bases de datos

- Utilizar consultas parametrizadas para todas las operaciones.
- Utilizar parámetros para todos los valores externos, aunque los datos hayan sido validados.
- Utilizar mecanismos parametrizados del ORM para construir consultas.
- Validar cualquier consulta dinámica antes de ejecutarla.
- Limitar la construcción dinámica a las consultas definidas por el caso de uso.
- Validar columnas, tablas y ordenamientos cuando deban seleccionarse dinámicamente mediante listas permitidas.

### Comandos del sistema

- Evitar la ejecución de comandos del sistema siempre que exista una alternativa.
- Validar estrictamente cualquier parámetro utilizado por comandos externos.
- Utilizar APIs nativas antes que intérpretes de comandos.
- Ejecutar procesos con el menor privilegio definido para el caso de uso.

### Consultas LDAP y XPath

- Parametrizar consultas cuando la tecnología lo permita.
- Validar cualquier dato incorporado a expresiones LDAP o XPath.
- Utilizar listas permitidas para atributos y filtros dinámicos.

### Archivos

- Validar nombres de archivos recibidos desde el usuario.
- Validar extensiones permitidas.
- Validar rutas antes de acceder al sistema de archivos.
- Normalizar rutas antes de utilizarlas.

### APIs y consultas dinámicas

- Validar parámetros utilizados para construir filtros.
- Validar parámetros utilizados para ordenamientos.
- Validar parámetros utilizados para paginación.
- Limitar operadores permitidos.
- Validar cualquier expresión dinámica antes de evaluarla.

### Codificación de salida

- Separar la validación de entrada de la codificación de salida.
- Aplicar codificación de salida según el contexto en que se inserte el dato.
- Aplicar HTML Encoding al insertar datos en contenido HTML.
- Aplicar JavaScript Encoding al insertar datos en código o literales JavaScript.
- Aplicar Context-aware Output Encoding para cada contexto de salida distinto.
- No utilizar la validación de entrada como sustituto de la codificación de salida.

### Manejo de errores

- Registrar errores internos sin revelar información técnica al cliente.
- Devolver mensajes de error genéricos cuando falle una validación.
- Registrar intentos de inyección detectados.

---

## Acciones prohibidas

- Nunca construir consultas SQL mediante concatenación de cadenas.
- Nunca construir consultas utilizando interpolación de cadenas.
- Nunca ejecutar SQL generado directamente desde entradas del usuario.
- Nunca ejecutar comandos del sistema utilizando datos no validados.
- Nunca confiar en validaciones realizadas únicamente en el cliente.
- Nunca utilizar entradas del usuario como nombres de tablas.
- Nunca utilizar entradas del usuario como nombres de columnas sin listas permitidas.
- Nunca utilizar entradas del usuario para construir cláusulas ORDER BY sin validación.
- Nunca utilizar entradas del usuario para construir expresiones dinámicas sin restricciones.
- Nunca utilizar funciones de evaluación dinámica sobre datos externos.
- Nunca ejecutar scripts generados por usuarios.
- Nunca exponer mensajes de error que revelen consultas SQL.
- Nunca revelar nombres de tablas, columnas o estructuras internas.
- Nunca asumir que un ORM elimina completamente el riesgo de inyección.
- Nunca deshabilitar validaciones por motivos de rendimiento.
- Nunca aceptar expresiones arbitrarias provenientes del cliente.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] Todas las consultas utilizan parámetros.
- [ ] No existen consultas construidas mediante concatenación.
- [ ] No existen consultas construidas mediante interpolación.
- [ ] Todas las entradas externas son validadas.
- [ ] Todos los comandos del sistema validan sus parámetros.
- [ ] Todas las rutas de archivos son validadas.
- [ ] Todos los filtros dinámicos utilizan listas permitidas.
- [ ] Ningún mensaje de error revela información técnica.
- [ ] Los intentos de inyección se registran en auditoría.
- [ ] La validación de entrada y la codificación de salida se aplican por separado.
- [ ] Los datos insertados en HTML utilizan HTML Encoding.
- [ ] Los datos insertados en JavaScript utilizan JavaScript Encoding.
- [ ] Cada contexto de salida utiliza Context-aware Output Encoding.
- [ ] No existen funciones de evaluación dinámica sobre datos externos.
- [ ] Las consultas generadas dinámicamente están restringidas a escenarios controlados.

---

## Referencias

- OWASP Top 10 2021 – A03: Injection
- OWASP ASVS
- OWASP SQL Injection Prevention Cheat Sheet
- OWASP Input Validation Cheat Sheet
- CWE-20
- CWE-74
- CWE-77
- CWE-78
- CWE-89
- CWE-90
- CWE-91
- CWE-94
- CWE-95
- NIST Secure Software Development Framework (SSDF)
