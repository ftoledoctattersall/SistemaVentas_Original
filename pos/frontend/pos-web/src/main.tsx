import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { CssBaseline, ThemeProvider } from '@mui/material'
import './index.css'
import App from './App.tsx'
import { createAppTheme } from './branding/createAppTheme'
import { resolveBrand } from './branding/resolveBrand'

const activeBrand = resolveBrand(null)
const appTheme = createAppTheme(activeBrand)

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ThemeProvider theme={appTheme}>
      <CssBaseline />
      <App brand={activeBrand} />
    </ThemeProvider>
  </StrictMode>,
)
