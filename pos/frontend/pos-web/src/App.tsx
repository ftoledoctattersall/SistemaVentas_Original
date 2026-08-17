import { useState } from 'react'
import type { FormEvent } from 'react'
import { Alert, Box, Button, Chip, Paper, Stack, TextField, Typography } from '@mui/material'
import { resolveBrand } from './branding/resolveBrand'
import type { BrandDefinition } from './branding/types'

type VentaLinea = {
  id: string
  productoId: string
  cantidad: number
}

type VentaBorrador = {
  id: string
  empresaId: string
  clienteId: string
  estado: string
  fechaCreacion: string
  lineas: VentaLinea[]
}

async function solicitar<T>(url: string, init?: RequestInit): Promise<T> {
  const response = await fetch(url, {
    ...init,
    headers: init?.body ? { 'Content-Type': 'application/json' } : undefined,
  })

  if (!response.ok) {
    const problem = (await response.json().catch(() => null)) as { detail?: string; title?: string } | null
    throw new Error(problem?.detail ?? problem?.title ?? 'No fue posible completar la operación.')
  }

  return response.json() as Promise<T>
}

type AppProps = {
  brand?: BrandDefinition
}

function App({ brand = resolveBrand(null) }: AppProps) {
  const [empresaId, setEmpresaId] = useState('')
  const [clienteId, setClienteId] = useState('')
  const [productoId, setProductoId] = useState('')
  const [cantidad, setCantidad] = useState('1')
  const [borrador, setBorrador] = useState<VentaBorrador | null>(null)
  const [mensaje, setMensaje] = useState('')
  const [ocupado, setOcupado] = useState(false)

  async function ejecutar(accion: () => Promise<VentaBorrador>) {
    setOcupado(true)
    setMensaje('')
    try {
      setBorrador(await accion())
    } catch (error) {
      setMensaje(error instanceof Error ? error.message : 'Ocurrió un error inesperado.')
    } finally {
      setOcupado(false)
    }
  }

  async function crearBorrador(event: FormEvent) {
    event.preventDefault()
    await ejecutar(() => solicitar<VentaBorrador>('/api/ventas/borradores', {
      method: 'POST',
      body: JSON.stringify({ empresaId, clienteId }),
    }))
  }

  async function agregarLinea(event: FormEvent) {
    event.preventDefault()
    if (!borrador) return

    await ejecutar(() => solicitar<VentaBorrador>(`/api/ventas/borradores/${borrador.id}/lineas`, {
      method: 'POST',
      body: JSON.stringify({ productoId, cantidad: Number(cantidad) }),
    }))
    setProductoId('')
    setCantidad('1')
  }

  async function modificarCantidad(linea: VentaLinea, nuevaCantidad: string) {
    if (!borrador) return
    await ejecutar(() => solicitar<VentaBorrador>(`/api/ventas/borradores/${borrador.id}/lineas/${linea.id}`, {
      method: 'PUT',
      body: JSON.stringify({ cantidad: Number(nuevaCantidad) }),
    }))
  }

  async function eliminarLinea(lineaId: string) {
    if (!borrador) return
    await ejecutar(() => solicitar<VentaBorrador>(`/api/ventas/borradores/${borrador.id}/lineas/${lineaId}`, {
      method: 'DELETE',
    }))
  }

  return (
    <Box component="main" sx={{ width: 'min(880px, calc(100% - 2rem))', mx: 'auto', py: { xs: 3, sm: 6 } }}>
      <Stack component="header" spacing={2} sx={{ mb: 4 }}>
        <Paper component="section" elevation={0} sx={{ bgcolor: 'primary.main', color: 'primary.contrastText', p: { xs: 2, sm: 3 } }}>
          <Stack direction="row" spacing={2} sx={{ alignItems: 'center' }}>
            <Box component="img" src={brand.logo} alt={`Logo ${brand.displayName}`} sx={{ width: 112, maxHeight: 52, objectFit: 'contain' }} />
            <Box>
              <Typography variant="overline" component="p">{brand.displayName}</Typography>
              <Typography variant="h4" component="h1">Borrador de venta</Typography>
            </Box>
          </Stack>
        </Paper>
        <Typography color="text.secondary">Crea una venta en preparación y administra sus productos.</Typography>
      </Stack>

      {!borrador ? (
        <Paper component="form" onSubmit={crearBorrador} sx={{ p: { xs: 2, sm: 3 } }}>
          <Stack spacing={2}>
            <TextField label="Empresa ID" slotProps={{ htmlInput: { required: true } }} value={empresaId} onChange={(event) => setEmpresaId(event.target.value)} />
            <TextField label="Cliente ID" slotProps={{ htmlInput: { required: true } }} value={clienteId} onChange={(event) => setClienteId(event.target.value)} />
            <Button disabled={ocupado} type="submit" variant="contained">Crear borrador</Button>
          </Stack>
        </Paper>
      ) : (
        <Stack spacing={3}>
          <Stack direction={{ xs: 'column', sm: 'row' }} spacing={1}>
            <Chip label={borrador.estado} color="primary" />
            <Chip label={`Cliente: ${borrador.clienteId}`} variant="outlined" />
            <Chip label={`Borrador: ${borrador.id}`} variant="outlined" />
          </Stack>

          <Paper component="form" onSubmit={agregarLinea} sx={{ p: { xs: 2, sm: 3 } }}>
            <Stack spacing={2}>
              <TextField label="Producto ID" slotProps={{ htmlInput: { required: true } }} value={productoId} onChange={(event) => setProductoId(event.target.value)} />
              <TextField label="Cantidad" type="number" slotProps={{ htmlInput: { min: 0.01, required: true, step: 'any' } }} value={cantidad} onChange={(event) => setCantidad(event.target.value)} />
              <Button disabled={ocupado} type="submit" variant="contained">Agregar producto</Button>
            </Stack>
          </Paper>

          <Typography variant="h5" component="h2">Productos</Typography>
          {borrador.lineas.length === 0 ? (
            <Typography color="text.secondary">El borrador todavía no tiene productos.</Typography>
          ) : (
            <Stack component="ul" spacing={1} sx={{ listStyle: 'none', p: 0, m: 0 }}>
              {borrador.lineas.map((linea) => (
                <Paper component="li" key={linea.id} sx={{ p: 2 }}>
                  <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2} sx={{ alignItems: { sm: 'center' } }}>
                    <Typography sx={{ flex: 1, overflowWrap: 'anywhere' }}>{linea.productoId}</Typography>
                    <TextField label="Cantidad" aria-label={`Cantidad de ${linea.productoId}`} defaultValue={linea.cantidad} type="number" slotProps={{ htmlInput: { min: 0.01, step: 'any' } }} onBlur={(event) => void modificarCantidad(linea, event.target.value)} />
                    <Button disabled={ocupado} type="button" color="secondary" onClick={() => void eliminarLinea(linea.id)}>Eliminar</Button>
                  </Stack>
                </Paper>
              ))}
            </Stack>
          )}
        </Stack>
      )}

      {mensaje ? <Alert severity="error" role="alert" sx={{ mt: 2 }}>{mensaje}</Alert> : null}
    </Box>
  )
}

export default App
