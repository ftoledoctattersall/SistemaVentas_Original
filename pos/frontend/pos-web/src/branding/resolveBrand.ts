import { agroinsumosBrand } from './companies/agroinsumosBrand'
import { corporateBrand } from './corporateBrand'
import type { BrandDefinition } from './types'

export function resolveBrand(companyId: string | null | undefined): BrandDefinition {
  if (companyId === agroinsumosBrand.id) return agroinsumosBrand
  return corporateBrand
}
