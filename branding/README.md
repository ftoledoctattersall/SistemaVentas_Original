# Branding

El Design System define las reglas visuales y funcionales comunes; el branding contiene la identidad gráfica concreta.

EETT es la identidad corporativa inicial y Agroinsumos es la primera filial. La tipografía es corporativa y común a todas las empresas, mientras el logo y los colores pueden cambiar según `EmpresaActiva`.

Las futuras filiales se agregan bajo `branding/companies/`. Los componentes React no deben hardcodear valores empresariales.

En el frontend, `resolveBrand(EmpresaActiva)` usa EETT como fallback y Agroinsumos como primer branding empresarial; nuevos brandings deben incorporarse sin modificar componentes funcionales.

Los archivos de `logos-colores/` se mantienen como fuente original para conservar trazabilidad.
