export type BrandColors = {
  primary: string
  secondary?: string
}

export type BrandDefinition = {
  id: 'corporate' | 'agroinsumos'
  displayName: string
  logo: string
  colors: BrandColors
}
