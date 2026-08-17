# Material UI

## Metadata

- Skill: frontend/material-ui
- Skill-Version: 1.0.0
- Technology: Material UI
- Compatibility: Consumer-project supported versions
- Category: frontend

## Purpose

Specialize maintainable and accessible use of Material UI components, theme, `sx`, responsive layout, data views, forms, overlays, typography, and internationalization readiness without imposing a design system or library version.

## Applies When

Apply this skill only when `frontend/material-ui` is explicitly declared in `ACTIVE-SKILLS.md` at the established Consumer Project Root and the task creates or materially modifies Material UI usage.

Activating this skill does not activate `frontend/react` or `frontend/frontend-design`; each skill must be declared independently.

## Does Not Cover

This skill does not decide global frontend architecture, state management, routing, data fetching, form libraries, SSR/Next.js, React Native, microfrontends, branding, an undefined corporate design system, functional rules, permissions, API contracts, or an unapproved React/Material UI version.

General React behavior belongs to `frontend/react`; cross-framework UX, states, responsive behavior, and accessibility belong to `frontend/frontend-design`.

## Authority and Constraints

This skill is optional and governed by `skills/README.md`. Applicable Security and Engineering Rules remain mandatory and take precedence.

Do not use visual styling as authorization, validation, or error handling. Do not infer a theme, breakpoint policy, component API, premium feature, or product decision that the consumer project has not established.

## Rules

### Theme and styling

- Use `ThemeProvider` when the project defines a Material UI theme and keep shared visual tokens in the established theme.
- Prefer theme values for palette, spacing, typography, and breakpoints instead of repeating arbitrary hardcoded values.
- Use `sx` for reasonable local adjustments. Extract a reusable component or style when `sx` becomes large, repeated, or difficult to review.
- Apply global component defaults or overrides through the theme only when the behavior is genuinely global and intentional.
- Do not mix styling strategies without a clear ownership and maintenance reason.

### Responsive layout

- Use the project's theme breakpoints and responsive layout primitives for desktop, tablet, and mobile behavior.
- Prefer mobile-first rules when they fit the supported viewport contract, but do not invent breakpoints or collapse required content without an established design decision.
- Avoid layouts dependent exclusively on fixed dimensions and ensure touch targets and actions remain usable at supported sizes.
- Choose the simplest semantic layout primitive that solves the problem; avoid unnecessary `Box`, `Grid`, `Stack`, or wrapper nesting.

### Components and public APIs

- Prefer supported public component props, slots, and extension points for the Material UI version used by the project.
- Do not depend on internal class names, private DOM structure, undocumented slots, or fragile selectors.
- Keep component composition and overrides local unless a repeated, globally consistent behavior justifies a shared component or theme rule.

### Forms and overlays

- Associate labels, helper text, and validation errors with their controls and preserve keyboard navigation.
- Keep validation and error meaning available without relying only on color or visual styling.
- Ensure Dialog, Modal, and Drawer interactions have visible focus, keyboard handling, and an explicit close path.
- Require confirmation for destructive actions when their established consequence warrants it and avoid unnecessary nested dialogs.

### Tables and data views

- Do not render unbounded datasets by default. Use pagination, virtualization, or another established strategy when volume requires it.
- Give columns and row actions clear identity, labels, and keyboard access.
- Define responsive behavior for tables and data views explicitly; do not assume a desktop table remains usable on mobile.
- Do not assume DataGrid Pro or Premium capabilities unless the project has approved and configured them.

### Typography and semantics

- Keep visual typography variants separate from semantic HTML hierarchy and use the appropriate underlying element when they differ.
- Preserve a meaningful heading hierarchy and do not choose a heading solely because its visual size is convenient.
- Keep contrast, focus, labels, and accessible names aligned with the applicable accessibility rules.

### Internationalization readiness and performance

- Avoid concatenated UI text and layouts dependent on an exact label length so future translation can vary text and order.
- Do not hardcode date, time, number, or currency formats as universal product decisions.
- Avoid unnecessary complex component creation, global overrides, and repeated style objects.
- Base optimization on evidence; use an explicit strategy for large lists, tables, or expensive rendering instead of speculative memoization.

## Recommendations

- Prefer the theme and public component APIs already established by the project.
- Keep local `sx` concise and move repeated visual language into a shared component or theme when repetition is meaningful.
- Review representative desktop, tablet, and mobile states before finalizing a responsive change.

## Anti-Patterns

- Repeating hardcoded colors, spacing, typography, or breakpoints instead of using the theme.
- Giant or duplicated `sx` objects.
- Excessive layout nesting or unnecessary Box/Grid/Stack wrappers.
- Overrides that depend on private DOM structure or internal CSS classes.
- Semantics or heading hierarchy sacrificed for visual styling.
- Desktop-only layouts or controls unusable by keyboard or touch.
- Tables and data views rendered without a bounded-data strategy.
- Assuming DataGrid Pro/Premium or another unapproved capability.
- UI text and formats hardcoded in a way that blocks future internationalization.

## Validation

- Confirm theme tokens and public APIs are reused, local styling remains maintainable, and no private internals are overridden.
- Verify responsive behavior across supported desktop, tablet, and mobile viewports, including touch usability and bounded data views.
- Check semantic elements, heading hierarchy, labels, helper/error text, contrast, focus, and keyboard behavior.
- Confirm dialogs and destructive actions have safe focus and close behavior.
- Confirm internationalization-sensitive text and formats are not structurally hardcoded and that performance choices are evidence-based.
- Confirm applicable Security, Engineering, `frontend/frontend-design`, and React Rules were followed without inventing a library, contract, or permission.

## Related Rules

- engineering/Architecture.md
- engineering/Testing.md
- engineering/Validation.md
- security/A01-Broken-Access-Control.md
- security/A03-Injection.md
- security/A05-Security-Misconfiguration.md

## Related Skills

- frontend/react
- frontend/frontend-design
