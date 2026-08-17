# Design System

## Metadata

- Skill: frontend/design-system
- Skill-Version: 1.0.0
- Technology: Framework-agnostic
- Compatibility: Not applicable
- Category: frontend

## Purpose

Define reusable design-system and branding boundaries so functional components remain independent from concrete identities and can adapt through configuration.

## Applies When

Apply only when `frontend/design-system` is explicitly declared in `ACTIVE-SKILLS.md` at the Consumer Project Root and the task changes visual tokens, branding resolution, shared visual components, themes, or company-aware presentation.

Technical applicability and related skills do not activate this skill.

## Does Not Cover

This skill does not define concrete logos, colors, fonts, screens, business functionality, framework-specific providers, or runtime configuration. Concrete branding belongs outside `ai-rules/`.

## Authority and Constraints

Applicable Security and Engineering Rules precede this optional skill. Do not invent brand assets or values. Keep consumer-specific identity and configuration outside `ai-rules/`.

## Rules

### Branding resolution

- Functional components must not hardcode brand colors or import identity-specific assets directly.
- Resolve identity through an established `brandDefinition`, design-token, or theme boundary.
- Use `defaultBrand` when no `activeCompany` is selected and the consumer contract defines a fallback.
- When `activeCompany` exists, resolve its approved brand configuration without coupling functional components to company identifiers.
- Adding a brand must require configuration and assets, not changes to unrelated functional components.
- Keep resolution deterministic and define safe behavior for missing or invalid brand configuration.

### Tokens and semantics

- Separate primitive values from semantic tokens and component-level decisions.
- Preserve shared semantics for error, warning, success, information, focus and disabled states unless an explicit accessible contract requires otherwise.
- Keep typography, spacing, elevation, shape and motion decisions in the appropriate token or theme boundary.
- Do not expose secrets, internal paths or untrusted executable content through branding configuration.

### Accessibility and responsive behavior

- Do not communicate meaning exclusively through color.
- Validate contrast, focus visibility, text scaling and accessible interaction.
- Preserve responsive behavior at the breakpoints and form factors approved by the project.
- Treat reduced motion and user preferences according to the consumer's accessibility requirements.

## Recommendations

- Keep a stable, minimal `brandDefinition` contract.
- Validate tokens and assets before runtime integration.
- Prefer semantic tokens over direct palette references in functional components.
- Document how `defaultBrand` and `activeCompany` are resolved.

## Anti-Patterns

- Hex values or brand asset paths embedded in functional components.
- Conditionals on concrete company names throughout the UI.
- Brand configuration that changes functional authorization or business behavior.
- Color-only state communication.
- Requiring feature changes whenever a brand is added.
- Treating related frontend skills as transitively active.

## Validation

- [ ] `frontend/design-system` is explicitly active.
- [ ] Functional components contain no concrete brand identities or asset paths.
- [ ] `defaultBrand`, `activeCompany` and `brandDefinition` behavior is explicit when applicable.
- [ ] Tokens preserve semantic consistency and avoid visual hardcodes.
- [ ] Missing configuration has deterministic safe behavior.
- [ ] Contrast, non-color cues, focus and responsive behavior were checked.
- [ ] Concrete branding remains outside `ai-rules/`.

## Related Rules

- engineering/Architecture.md
- engineering/Testing.md
- engineering/Validation.md
- security/A03-Injection.md
- security/A05-Security-Misconfiguration.md

## Related Skills

- frontend/frontend-design
- frontend/material-ui
- frontend/react

All relationships are informative. No skill is activated transitively.
