# React

## Metadata

- Skill: frontend/react
- Skill-Version: 1.0.0
- Technology: React with TypeScript
- Compatibility: Consumer-project supported versions
- Category: frontend

## Purpose

Specialize implementation and review of React components, Hooks, state, rendering, asynchronous UI, forms, and React-specific TypeScript practices without imposing a state, routing, fetching, or form library.

## Applies When

Apply this skill only when `frontend/react` is explicitly declared in `ACTIVE-SKILLS.md` at the established Consumer Project Root and the task creates or materially modifies React code.

Technical applicability and `Related Skills` references do not activate this skill.

Activating this skill does not activate `frontend/material-ui` or `frontend/frontend-design`; each skill must be declared independently.

## Does Not Cover

This skill does not decide global frontend architecture, Zustand versus Redux versus Context, routing, fetching libraries, form libraries, SSR/Next.js, React Native, microfrontends, branding, design systems, functional rules, permissions, API contracts, or an unapproved React/TypeScript version.

Cross-framework UX, visible states, general responsive behavior, and general accessibility belong to `frontend/frontend-design`. Material UI usage belongs to `frontend/material-ui`.

## Authority and Constraints

This skill is optional and governed by `skills/README.md`. Applicable Security and Engineering Rules remain mandatory and take precedence.

Do not invent data contracts, state ownership, loading semantics, breakpoints, dependencies, or client-side authorization. Client rendering must never be treated as an authorization control.

## Rules

### Components and purity

- Keep components small enough to have a clear responsibility and compose them when a component would otherwise become monolithic.
- Keep props explicit and stable. Reduce excessive prop drilling through a clearer local boundary before introducing shared Context.
- Keep component and Hook behavior pure during render. Do not perform side effects or mutate props, state, context, or values already passed to JSX.
- Keep presentation and application logic reasonably separable when that improves testing, reuse, or comprehension; do not create layers without a demonstrated need.

### Hooks

- Follow the Rules of Hooks: call Hooks only at the top level of React components or custom Hooks, never conditionally, inside loops, or inside nested functions.
- Create a custom Hook only when reusable stateful or synchronization logic exists; do not wrap trivial expressions merely to create an abstraction.
- Keep Hook inputs and returned values explicit and treat them as immutable contracts.

### Effects and events

- Use `useEffect` to synchronize with an external system such as a subscription, browser API, timer, network integration, or non-React widget.
- Do not use an Effect to derive state that can be calculated during render or as a general mechanism to coordinate application data flow.
- Put interaction-caused side effects in event handlers when the triggering user action is known.
- Return cleanup from Effects that create subscriptions, timers, resources, or other external connections.
- Declare every reactive value used by an Effect and avoid dependency omissions, redundant synchronization, and dependency-driven loops.

### State and events

- Keep state as local as reasonably possible and lift it only when a shared owner or coordination need is demonstrated.
- Avoid redundant, duplicated, contradictory, or derivable state; maintain one source of truth for each concept.
- Select global state only for a demonstrated cross-boundary need. Do not impose Redux, Zustand, Context, or another global-state library.
- Prefer event handlers for logic caused by user interaction and keep side effects visible at that boundary.

### Rendering and performance

- Give list items stable keys based on identity. Do not use array indexes when item identity exists and do not generate keys during rendering.
- Prevent avoidable re-renders through clear state ownership, stable data flow, and bounded work before adding memoization.
- Use `memo`, `useMemo`, or `useCallback` only when a demonstrated rendering or computation cost justifies their complexity.
- Keep large lists bounded, paginated, or virtualized when the established data volume requires it.
- Avoid repeated expensive calculations and speculative optimization.

### Asynchronous UI

- Represent reachable loading, error, empty, and success states for asynchronous data or actions, in coordination with `frontend/frontend-design`.
- Prevent stale asynchronous results from overwriting newer state. Cancel work or ignore obsolete results when the integration supports it.
- Do not implement repeated ad-hoc fetching when the project already defines a fetching strategy. Do not invent one when it is absent.

### Forms and TypeScript

- Choose controlled or uncontrolled form state consciously and keep the choice consistent with the component contract.
- Validate according to the applicable canonical rules and expose field errors visibly and through their associated controls.
- Type props, events, nullable values, and asynchronous states explicitly. Avoid unnecessary `any`.
- Use discriminated unions when they make mutually exclusive UI states or actions clearer. Share types only when a real contract exists.

### React accessibility and responsive behavior

- Use semantic HTML, accessible labels, stable IDs when relationships require them, and keyboard-operable interactions.
- Preserve visible focus and delegate cross-framework interaction and responsive criteria to `frontend/frontend-design`.
- Avoid rigid dimensions and allow components to adapt to supported viewports without inventing unsupported breakpoints.

## Recommendations

- Prefer composition and established project conventions over a new abstraction or global mechanism.
- Keep data transformations in render when they are inexpensive and deterministic; isolate measured expensive work deliberately.
- Use React Strict Mode and the project's Hook linting when already supported by the project configuration.

## Anti-Patterns

- Calling Hooks conditionally or outside React components/custom Hooks.
- Side effects or mutation during render.
- Effects used for derivable state or routine event coordination.
- Duplicated state or multiple sources of truth.
- Monolithic components without a current responsibility requiring them.
- Mutating props, state, context, or JSX inputs.
- Array indexes used as keys when stable identity exists.
- Indiscriminate `memo`, `useMemo`, or `useCallback`.
- Unbounded asynchronous results updating state after they are obsolete.
- Unnecessary `any` or untyped event contracts.

## Validation

- Confirm Hooks obey their rules, render remains pure, and every Effect is justified, complete, and cleaned up when necessary.
- Confirm state is not duplicated, list keys represent identity, and asynchronous results cannot overwrite newer state.
- Confirm TypeScript props, events, nullability, and UI states are explicit without unnecessary `any`.
- Confirm loading/error/empty/success behavior, keyboard interaction, semantic labels, responsive constraints, and relevant tests were considered.
- Confirm applicable Security and Engineering Rules were followed and no library, permission, or contract was invented.

## Related Rules

- engineering/Architecture.md
- engineering/Error-Handling.md
- engineering/Testing.md
- engineering/Validation.md
- security/A01-Broken-Access-Control.md
- security/A03-Injection.md

## Related Skills

- frontend/frontend-design
- frontend/material-ui
