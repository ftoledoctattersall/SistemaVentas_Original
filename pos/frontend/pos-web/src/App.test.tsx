import { render, screen } from '@testing-library/react'
import { describe, expect, it } from 'vitest'
import App from './App'

describe('App', () => {
  it('renders the technical baseline', () => {
    render(<App />)

    expect(screen.getByRole('heading', { name: 'Punto de Venta' })).toBeDefined()
    expect(screen.getByText('Baseline técnico')).toBeDefined()
  })
})
