# Configuración

## Archivos actuales

`pos/backend/src/Pos.Api/appsettings.json` contiene la configuración base de niveles de logging y `AllowedHosts`.

`pos/backend/src/Pos.Api/appsettings.Development.json` contiene los niveles de logging aplicados en el ambiente Development.

Actualmente no existen:

- connection strings;
- configuración SAP;
- configuración AWS;
- configuración de Entra ID;
- configuración de base de datos;
- secretos productivos.

> Los secretos no deben almacenarse directamente en archivos versionados.
