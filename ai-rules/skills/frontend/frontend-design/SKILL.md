# Frontend Design

## Metadata

- Skill: frontend/frontend-design
- Skill-Version: 1.0.0
- Technology: Framework-agnostic
- Compatibility: Not applicable
- Category: frontend

## Purpose

Specialize framework-independent interface design decisions by requiring observable treatment of user-visible states, action feedback, adaptable layouts, and basic interaction accessibility, without prescribing a framework, component library, brand, or visual style.

## Applies When

Apply this skill, only when explicitly activated, to tasks that create or materially modify user-facing screens, forms, navigation surfaces, data views, or interactive controls, including tasks that define their observable loading, empty, error, completion, responsive, or keyboard-interaction behavior.

Technical applicability does not activate this skill. It applies only when `frontend/frontend-design` is explicitly declared in `ACTIVE-SKILLS.md` at the established Consumer Project Root.

## Does Not Cover

This skill does not define framework-specific implementation, component APIs, state-management libraries, HTTP communication, routing, backend or API contracts, authentication, authorization, security controls, output encoding, corporate branding, or a specific design system. React, Material UI, and comparable technologies remain outside its rules.

## Authority and Constraints

This skill is an optional specialization governed by `skills/README.md`. When active, it may specialize, concretize, or complement applicable Engineering rules. It must not omit, relax, replace, neutralize, or redefine applicable Security or Engineering obligations.

Interface behavior must not be treated as an authorization or security control. Validation, error handling, and testing remain governed by their applicable canonical standards. This skill does not require a dependency, framework, component library, or proprietary agent capability.

## Rules

- A user-facing region whose content depends on an operation or data result must represent every reachable state relevant to that region—loading or pending, successful content, empty result, and failure—using distinguishable content or controls. A state that cannot occur under the established contract need not be invented.
- An action that is not observably instantaneous must expose its in-progress state and an observable completion or failure outcome. While duplicate execution would be invalid, the interface must prevent or clearly control repeated activation until the current operation reaches a defined outcome.
- At every viewport size explicitly supported by the consumer project, primary content and required actions must remain perceivable and operable without unintended overlap, clipping, or viewport-level horizontal overflow. The skill must not invent unsupported breakpoints.
- Interactive controls must be reachable and operable by keyboard when the underlying interaction supports keyboard input, must expose an identifiable accessible name, and must provide a visible focus indication. Required meaning, status, or error information must not be communicated only by color.

## Recommendations

- Use visual hierarchy to make established primary content and actions easier to identify than supporting information.
- Group related information and controls; use spacing and progressive disclosure to reduce avoidable density when this does not hide required content.
- Prefer labels, empty-state guidance, and feedback that describe the actual user task rather than generic placeholder language.
- Give destructive, secondary, and primary actions different emphasis when their established consequences differ.
- Preserve consistency with an existing consumer-project design system or visual language when one is explicitly available.

## Anti-Patterns

- Rendering loading, empty, and failure outcomes as the same blank or indistinguishable region.
- Allowing a non-instant action to accept repeated invalid submissions without visible progress or outcome.
- Hiding required content or actions at a supported viewport through overlap, clipping, or accidental horizontal overflow.
- Removing visible focus, using pointer-only controls, or communicating required meaning only through color.
- Giving every action identical prominence despite known differences in purpose or consequence.
- Producing an interchangeable composition of generic cards, effects, or placeholder copy without reference to the established task or information structure.

## Validation

- Identify the states that are reachable under established contracts. Inspect or simulate loading, successful content, empty result, and failure, and confirm that each applicable state is distinguishable.
- Exercise non-instant actions through progress, success, and failure. When a duplicate execution would be invalid, attempt repeated activation and verify that it is prevented or explicitly controlled.
- Inspect and operate the interface at every viewport size explicitly supported by the consumer project. Verify that required content and actions do not overlap, become clipped, or cause unintended viewport-level horizontal overflow.
- Navigate interactive controls using the keyboard. Verify operability, visible focus, identifiable accessible names, and that required meaning is not conveyed only by color.
- Combine code inspection, visual inspection, interaction, alternate viewports, simulated states, and automated tests as applicable. Do not treat a single successful-state screenshot, static markup alone, or an automated tool result alone as sufficient evidence for behavior it cannot observe.
- If required runtime or visual capabilities are unavailable, report the unverified checks instead of claiming compliance.

## Related Rules

- engineering/Validation.md
- engineering/Error-Handling.md
- engineering/Testing.md

## Related Skills

None.
