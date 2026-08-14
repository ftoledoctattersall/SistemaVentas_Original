# OWASP A02 – Cryptographic Failures

## Objetivo del documento

Este documento forma parte del repositorio **ai-rules** y define las reglas obligatorias que toda IA debe aplicar cuando implemente funcionalidades relacionadas con protección de información sensible mediante mecanismos criptográficos.

Las reglas de este documento complementan las instrucciones del proyecto y deben aplicarse antes de generar cualquier código.

---

## Prioridad

**Nivel:** Crítico

El incumplimiento de cualquiera de las reglas de este documento puede provocar pérdida de confidencialidad, exposición de datos sensibles, compromiso de credenciales, robo de identidad o manipulación de información protegida.

---

## Propósito

Garantizar que toda información sensible sea protegida utilizando los mecanismos criptográficos definidos por la política criptográfica del proyecto durante su almacenamiento, transmisión y procesamiento.

---

## Cuándo consultar este documento

Consultar este documento antes de implementar funcionalidades que involucren:

- Contraseñas.
- Tokens.
- JWT.
- Refresh Tokens.
- Cookies de autenticación.
- API Keys.
- Client Secrets.
- Secretos de aplicación.
- Certificados digitales.
- TLS.
- HTTPS.
- Datos personales.
- Datos financieros.
- Información confidencial.
- Firmas digitales.
- Cifrado de archivos.
- Integraciones con terceros.
- Servicios cloud.
- Variables de entorno sensibles.

---

## Documentos relacionados

Los documentos indicados en esta sección forman parte del estándar y deberán consultarse conjuntamente cuando la implementación abarque dichas responsabilidades.

Consultar además cuando corresponda:

- security/A07-Identification-and-Authentication-Failures.md
- security/A08-Software-and-Data-Integrity-Failures.md

Consultar también cuando corresponda:

- Authentication.md
- Dependencies.md

---

## No cubre

- Autorización y control de acceso.
- Logging y monitoreo.
- Gestión general de dependencias.
- Configuración de infraestructura.

---

## Riesgos mitigados

La aplicación de este documento busca reducir los riesgos asociados a:

- Exposición de datos sensibles.
- Uso de algoritmos inseguros.
- Gestión incorrecta de secretos.
- Compromiso de claves criptográficas.
- Exposición accidental de credenciales.
- Robo de tokens.
- Manipulación de información protegida.
- Transmisión insegura de información.

---

## Reglas obligatorias

### Protección de datos

- Clasificar la información según su nivel de sensibilidad.
- Cifrar la información sensible cuando deba almacenarse.
- Cifrar la información sensible durante su transmisión.
- Minimizar la información sensible almacenada.
- Eliminar información sensible cuando deje de ser necesaria.

### Contraseñas

- Almacenar contraseñas únicamente mediante algoritmos de hash resistentes al ataque por fuerza bruta.
- Utilizar una sal única para cada contraseña.
- Comparar hashes utilizando mecanismos resistentes a ataques por tiempo.
- Permitir la actualización del algoritmo de hash sin afectar la integridad de las credenciales.

### Secretos

- Obtener secretos únicamente desde un gestor seguro.
- Separar secretos del código fuente.
- Limitar el acceso a secretos siguiendo el principio de mínimo privilegio.
- Rotar secretos comprometidos inmediatamente.
- Rotar secretos según la frecuencia definida por la política de seguridad del proyecto.

### Claves criptográficas

- Proteger las claves durante todo su ciclo de vida.
- Limitar el acceso a claves privadas.
- Utilizar los tamaños mínimos de clave definidos para el algoritmo seleccionado por la política criptográfica del proyecto.
- Reemplazar inmediatamente claves comprometidas.
- Mantener separadas las claves de desarrollo, pruebas y producción.

### Comunicaciones

- Utilizar HTTPS para toda comunicación autenticada.
- Validar la cadena de confianza, vigencia y nombre de host de los certificados digitales.
- Rechazar conexiones inseguras cuando se transmitan datos sensibles.
- Proteger toda comunicación entre servicios internos que transporte información sensible.

### Tokens

- Validar firma, vigencia, emisor y audiencia en todos los tokens que incluyan esos campos.
- Invalidar tokens revocados.
- Limitar la vida útil de los tokens.
- Proteger los tokens durante almacenamiento y transmisión.

### Auditoría

- Registrar cambios de claves, rotaciones de secretos y fallos de validación criptográfica sin revelar información sensible.
- Registrar cambios de claves.
- Registrar rotaciones de secretos.
- Registrar intentos de uso de credenciales inválidas.

---

## Acciones prohibidas

- Nunca almacenar contraseñas en texto plano.
- Nunca almacenar secretos dentro del código fuente.
- Nunca almacenar claves privadas en el repositorio.
- Nunca registrar secretos en archivos de log.
- Nunca registrar contraseñas.
- Nunca registrar tokens completos.
- Nunca registrar claves criptográficas.
- Nunca utilizar algoritmos criptográficos considerados inseguros.
- Nunca transmitir información sensible mediante HTTP.
- Nunca deshabilitar la validación de certificados en producción.
- Nunca reutilizar claves criptográficas para propósitos distintos.
- Nunca compartir secretos entre entornos.
- Nunca utilizar valores predecibles para generar secretos.
- Nunca exponer secretos mediante mensajes de error.
- Nunca asumir que HTTPS reemplaza el cifrado de información almacenada.

---

## Auto verificación

Antes de finalizar una implementación verificar:

- [ ] No existen contraseñas almacenadas en texto plano.
- [ ] No existen secretos dentro del código fuente.
- [ ] No existen claves privadas expuestas.
- [ ] Toda comunicación sensible utiliza HTTPS.
- [ ] Todos los certificados validan cadena de confianza, vigencia y nombre de host.
- [ ] Todos los tokens validan firma y vigencia.
- [ ] Ningún log contiene información sensible.
- [ ] Los secretos provienen de un gestor seguro.
- [ ] Las claves criptográficas están protegidas.
- [ ] Existe un procedimiento definido para la rotación de secretos.

---

## Referencias

- OWASP Top 10 2021 – A02: Cryptographic Failures
- OWASP ASVS
- NIST SP 800-57
- NIST SP 800-63
- NIST Secure Software Development Framework (SSDF)
- CWE-259
- CWE-321
- CWE-326
- CWE-327
