import { cleanup, fireEvent, render, screen, waitFor } from '@testing-library/react'
import { afterEach, describe, expect, it, vi } from 'vitest'
import App from './App'

const borrador = {
  id: '00000000-0000-0000-0000-000000000100',
  empresaId: '00000000-0000-0000-0000-000000000010',
  clienteId: '00000000-0000-0000-0000-000000000020',
  estado: 'BORRADOR',
  fechaCreacion: '2026-08-16T12:00:00Z',
  lineas: [],
}

afterEach(() => {
  cleanup()
  vi.restoreAllMocks()
})

describe('App', () => {
  it('crea un borrador y muestra su estado', async () => {
    vi.spyOn(globalThis, 'fetch').mockResolvedValue(
      new Response(JSON.stringify(borrador), { status: 201 }),
    )
    render(<App />)

    fireEvent.change(screen.getByLabelText('Empresa ID'), { target: { value: borrador.empresaId } })
    fireEvent.change(screen.getByLabelText('Cliente ID'), { target: { value: borrador.clienteId } })
    fireEvent.click(screen.getByRole('button', { name: 'Crear borrador' }))

    expect(await screen.findByText('BORRADOR')).toBeDefined()
    expect(screen.getByText('El borrador todavía no tiene productos.')).toBeDefined()
  })

  it('agrega y elimina una línea', async () => {
    const conLinea = {
      ...borrador,
      lineas: [{
        id: '00000000-0000-0000-0000-000000000200',
        productoId: '00000000-0000-0000-0000-000000000030',
        cantidad: 2,
      }],
    }
    const fetchMock = vi.spyOn(globalThis, 'fetch')
      .mockResolvedValueOnce(new Response(JSON.stringify(borrador), { status: 201 }))
      .mockResolvedValueOnce(new Response(JSON.stringify(conLinea), { status: 200 }))
      .mockResolvedValueOnce(new Response(JSON.stringify(borrador), { status: 200 }))
    render(<App />)

    fireEvent.change(screen.getByLabelText('Empresa ID'), { target: { value: borrador.empresaId } })
    fireEvent.change(screen.getByLabelText('Cliente ID'), { target: { value: borrador.clienteId } })
    fireEvent.click(screen.getByRole('button', { name: 'Crear borrador' }))
    await screen.findByText('BORRADOR')

    fireEvent.change(screen.getByLabelText('Producto ID'), {
      target: { value: '00000000-0000-0000-0000-000000000030' },
    })
    fireEvent.change(screen.getByLabelText('Cantidad'), { target: { value: '2' } })
    fireEvent.click(screen.getByRole('button', { name: 'Agregar producto' }))
    expect(await screen.findByText('00000000-0000-0000-0000-000000000030')).toBeDefined()

    fireEvent.click(screen.getByRole('button', { name: 'Eliminar' }))
    await waitFor(() => expect(screen.queryByText('00000000-0000-0000-0000-000000000030')).toBeNull())
    expect(fetchMock).toHaveBeenCalledTimes(3)
  })
})
