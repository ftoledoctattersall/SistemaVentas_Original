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

## Branding y empresa activa

`EmpresaActiva` representa la identidad de la empresa seleccionada para el contexto visual de la aplicación. La aplicación debe proporcionar su identificador al resolver el branding; la skill reusable no define cómo se autentica, selecciona o persiste ese valor.

Cuando no existe una empresa activa, el contrato visual del consumidor utiliza el branding corporativo EETT como fallback. Cuando existe una empresa activa soportada, el resolver obtiene su configuración de branding y permite cambiar logo y colores sin modificar componentes funcionales. Los estados funcionales comunes, como error, advertencia, éxito e información, permanecen compartidos entre empresas.

La resolución actual es un baseline técnico determinista: [`resolveBrand`](../../pos/frontend/pos-web/src/branding/resolveBrand.ts) recibe un identificador opcional, devuelve el branding corporativo cuando es nulo o desconocido y devuelve Agroinsumos cuando el identificador corresponde a esa empresa. La resolución real de empresa todavía no está implementada.

Las identidades, assets y tokens concretos se mantienen en [`branding/`](../../branding/README.md). La aplicación y su futura fuente de contexto son responsables de proporcionar `EmpresaActiva`; el sistema de branding sólo resuelve la presentación aprobada y no cambia autorización ni reglas de negocio.
