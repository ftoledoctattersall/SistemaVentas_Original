# Branding

El Design System define las reglas visuales y funcionales comunes; el branding contiene la identidad gráfica concreta.

EETT es la identidad corporativa inicial y Agroinsumos es la primera filial. La tipografía es corporativa y común a todas las empresas, mientras el logo y los colores pueden cambiar según `EmpresaActiva`.

Las futuras filiales se agregan bajo `branding/companies/`. Los componentes React no deben hardcodear valores empresariales.

En el frontend, `resolveBrand(EmpresaActiva)` usa EETT como fallback y Agroinsumos como primer branding empresarial; nuevos brandings deben incorporarse sin modificar componentes funcionales.

Los archivos de `logos-colores/` se mantienen como fuente original para conservar trazabilidad.

## Uso por agentes y desarrolladores

Antes de modificar la UI:

1. Identificar la empresa o branding aplicable.
2. Revisar la definición normalizada bajo `branding/`.
3. Revisar la implementación runtime existente en el frontend.
4. Reutilizar `Theme`, `BrandDefinition`, el resolver de branding y los componentes existentes.
5. Evitar introducir colores, logos o identidad visual directamente en componentes cuando exista una abstracción equivalente.
6. Preservar el comportamiento responsive.

`logos-colores/` constituye la evidencia y el origen trazable de los activos y valores visuales. `branding/` es la representación organizada utilizada por el proyecto. Esta separación no elimina la necesidad de revisar la implementación runtime existente.
