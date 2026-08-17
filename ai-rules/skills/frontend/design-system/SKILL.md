# Design System

## Metadata

- Skill: frontend/design-system
- Skill-Version: 1.0.0
- Technology: Framework-agnostic
- Compatibility: Not applicable
- Category: frontend

## Purpose

Define the shared design-system and branding boundaries for a multi-company corporate interface, so functional components remain independent from company-specific identity and can adapt to the active company without modification.

## Applies When

Apply this skill only when `frontend/design-system` is explicitly declared in `ACTIVE-SKILLS.md` at the established Consumer Project Root and a task creates or modifies visual tokens, branding resolution, shared visual components, or company-aware presentation.

Technical applicability does not activate this skill. It does not activate other frontend skills transitively.

## Does Not Cover

This skill does not define concrete logos, color values, font files, screens, business functionality, authentication, authorization, a ThemeProvider implementation, or a Material UI theme. Concrete branding values live outside `ai-rules` in the consumer project's branding assets and data.

## Authority and Constraints

This skill is optional and governed by `skills/README.md`. Applicable Security and Engineering Rules remain mandatory and take precedence.

`ai-rules` defines reusable rules. Concrete logos, colors, and typography values belong outside `ai-rules`; they must not be copied into this skill.

## Rules

### Branding resolution

- Functional components must not contain hardcoded company colors or depend directly on a specific subsidiary logo.
- Resolve company colors and logos through the established branding, design-token, or theme boundary.
- Use the EETT corporate branding when no `EmpresaActiva` exists, including initial access, login, corporate context, and company selection when applicable.
- When `EmpresaActiva` exists, use that company's branding.
- Branding must depend on `EmpresaActiva`, not merely on the user's principal or initially assigned company.
- Preserve the ability for a user to access more than one company in the future.
- Changing `EmpresaActiva` must allow logo and company colors to change without modifying functional components.
- Add a new company by supplying its branding configuration and assets, without modifying existing functional components.

### Shared and company-specific identity

- EETT represents the general corporate branding for Grupo Tattersall.
- Use one shared corporate typography across all companies.
- Treat company colors primarily as visual identity for the header or app bar, primary actions, selection, accents, and brand elements.
- Keep common functional states consistent. Do not redefine error, warning, success, info, or disabled states arbitrarily per company.

### Accessibility and responsive behavior

- Do not communicate a functional state or required meaning exclusively through color.
- Ensure adequate contrast and accessible interaction for text, controls, focus, and status presentation.
- Keep the interface responsive and operable on desktop, tablet, and mobile.

## Recommendations

- Keep functional semantics separate from visual identity tokens.
- Give branding configurations a stable contract so company switching does not leak into feature components.
- Validate brand assets and tokens independently before integrating them into a UI framework.

## Anti-Patterns

- Hex values or company asset paths embedded in functional React components.
- Conditional rendering that imports a subsidiary logo directly based on user assignment.
- Resolving branding from the user's principal company while ignoring `EmpresaActiva`.
- Recoloring error, warning, success, info, or disabled states for each subsidiary without a shared functional rationale.
- Using color as the only indication of state, selection, error, or required action.
- Changing existing feature components whenever a new company is incorporated.

## Validation

- Inspect functional components for hardcoded company colors and direct subsidiary-logo dependencies.
- Confirm the branding contract has an EETT fallback when `EmpresaActiva` is absent and resolves the active company's identity when present.
- Verify that changing `EmpresaActiva` can change logo and identity colors without feature-component changes.
- Confirm typography is shared and company-specific values remain outside `ai-rules`.
- Check contrast, non-color state cues, and behavior at desktop, tablet, and mobile sizes.
- Confirm common functional states remain consistent across company brandings.

## Related Rules

- engineering/Architecture.md
- engineering/Testing.md
- engineering/Validation.md

## Related Skills

- frontend/frontend-design
- frontend/material-ui
- frontend/react
