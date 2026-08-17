import { describe, expect, it } from 'vitest'
import { resolveBrand } from './resolveBrand'

describe('resolveBrand', () => {
  it('usa EETT sin EmpresaActiva', () => {
    expect(resolveBrand(null).id).toBe('corporate')
  })

  it('resuelve Agroinsumos por EmpresaActiva', () => {
    expect(resolveBrand('agroinsumos').id).toBe('agroinsumos')
  })

  it('usa EETT como fallback para empresas desconocidas', () => {
    expect(resolveBrand('empresa-no-soportada').id).toBe('corporate')
  })
})
