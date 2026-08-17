import { createTheme } from '@mui/material/styles'
import type { BrandDefinition } from './types'

export function createAppTheme(brand: BrandDefinition) {
  return createTheme({
    palette: {
      primary: { main: brand.colors.primary },
      ...(brand.colors.secondary ? { secondary: { main: brand.colors.secondary } } : {}),
    },
  })
}
