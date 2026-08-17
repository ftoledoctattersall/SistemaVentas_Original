import { useState } from 'react'
import type { FormEvent } from 'react'
import './App.css'

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

function App() {
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
    await ejecutar(() =>
      solicitar<VentaBorrador>('/api/ventas/borradores', {
        method: 'POST',
        body: JSON.stringify({ empresaId, clienteId }),
      }),
    )
  }

  async function agregarLinea(event: FormEvent) {
    event.preventDefault()
    if (!borrador) return

    await ejecutar(() =>
      solicitar<VentaBorrador>(`/api/ventas/borradores/${borrador.id}/lineas`, {
        method: 'POST',
        body: JSON.stringify({ productoId, cantidad: Number(cantidad) }),
      }),
    )
    setProductoId('')
    setCantidad('1')
  }

  async function modificarCantidad(linea: VentaLinea, nuevaCantidad: string) {
    if (!borrador) return

    await ejecutar(() =>
      solicitar<VentaBorrador>(`/api/ventas/borradores/${borrador.id}/lineas/${linea.id}`, {
        method: 'PUT',
        body: JSON.stringify({ cantidad: Number(nuevaCantidad) }),
      }),
    )
  }

  async function eliminarLinea(lineaId: string) {
    if (!borrador) return

    await ejecutar(() =>
      solicitar<VentaBorrador>(`/api/ventas/borradores/${borrador.id}/lineas/${lineaId}`, {
        method: 'DELETE',
      }),
    )
  }

  return (
    <main>
      <header>
        <p className="eyebrow">Agroinsumos · EP-04</p>
        <h1>Borrador de venta</h1>
        <p>Crea una venta en preparación y administra sus productos.</p>
      </header>

      {!borrador ? (
        <form onSubmit={crearBorrador}>
          <label>
            Empresa ID
            <input required value={empresaId} onChange={(event) => setEmpresaId(event.target.value)} />
          </label>
          <label>
            Cliente ID
            <input required value={clienteId} onChange={(event) => setClienteId(event.target.value)} />
          </label>
          <button disabled={ocupado} type="submit">Crear borrador</button>
        </form>
      ) : (
        <section className="draft">
          <div className="draft-summary">
            <div><span>Estado</span><strong>{borrador.estado}</strong></div>
            <div><span>Cliente</span><strong>{borrador.clienteId}</strong></div>
            <div><span>Borrador</span><strong>{borrador.id}</strong></div>
          </div>

          <form onSubmit={agregarLinea}>
            <label>
              Producto ID
              <input required value={productoId} onChange={(event) => setProductoId(event.target.value)} />
            </label>
            <label>
              Cantidad
              <input
                min="0.01"
                required
                step="any"
                type="number"
                value={cantidad}
                onChange={(event) => setCantidad(event.target.value)}
              />
            </label>
            <button disabled={ocupado} type="submit">Agregar producto</button>
          </form>

          <h2>Productos</h2>
          {borrador.lineas.length === 0 ? (
            <p className="empty">El borrador todavía no tiene productos.</p>
          ) : (
            <ul>
              {borrador.lineas.map((linea) => (
                <li key={linea.id}>
                  <span>{linea.productoId}</span>
                  <label>
                    Cantidad
                    <input
                      aria-label={`Cantidad de ${linea.productoId}`}
                      defaultValue={linea.cantidad}
                      min="0.01"
                      step="any"
                      type="number"
                      onBlur={(event) => void modificarCantidad(linea, event.target.value)}
                    />
                  </label>
                  <button disabled={ocupado} type="button" onClick={() => void eliminarLinea(linea.id)}>
                    Eliminar
                  </button>
                </li>
              ))}
            </ul>
          )}
        </section>
      )}

      {mensaje ? <p className="error" role="alert">{mensaje}</p> : null}
    </main>
  )
}

export default App
