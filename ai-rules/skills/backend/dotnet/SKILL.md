# .NET / C#

## Metadata

- Skill: backend/dotnet
- Skill-Version: 1.0.0
- Technology: .NET / C#
- Compatibility: Consumer-project supported versions
- Category: backend

## Purpose

Specialize implementation and review of .NET and C# backend code through explicit runtime, dependency injection, configuration, integration, observability, and testing decisions without imposing an application architecture or external library.

## Applies When

Apply this skill only when `backend/dotnet` is explicitly declared in `ACTIVE-SKILLS.md` at the established Consumer Project Root and the task creates or materially modifies .NET or C# backend code.

Technical applicability and references from other skills do not activate this skill.

## Does Not Cover

This skill does not decide project architecture, Clean Architecture, CQRS, MediatR, microservices, serverless, databases, ORMs, business rules, undefined contracts, cloud infrastructure, or external libraries without an explicit need.

ASP.NET Core-specific web and API behavior belongs to `backend/aspnet-core`. This relationship is informative and does not activate either skill.

## Authority and Constraints

This skill is optional and governed by `skills/README.md`. It must comply with all applicable Security and Engineering Rules and may only specialize or complement them.

Do not infer missing requirements, permissions, contracts, configuration values, supported runtime versions, performance targets, or library choices.

## Rules

### C# and runtime

- Keep nullable reference types enabled when supported by the established project configuration; model nullability accurately and resolve warnings without suppression that hides an unresolved contract.
- Keep asynchronous flows asynchronous. Do not use `.Result`, `.Wait()`, or equivalent sync-over-async blocking in asynchronous application code.
- Accept and propagate `CancellationToken` through operations that support cancellation when cancellation belongs to the established execution flow; do not replace a caller token without justification.
- Dispose owned `IDisposable` and `IAsyncDisposable` resources deterministically using the appropriate synchronous or asynchronous mechanism. Do not dispose resources owned by dependency injection or another component.
- Catch exceptions only when the current boundary can handle, translate, enrich, or record them consistently. Preserve useful context and never use exceptions as normal control flow.
- Use clear types and contracts. Prefer immutability when it makes state and ownership safer or easier to reason about; do not introduce it ceremonially.
- Avoid repeated or unnecessary enumeration and materialization. Materialize a sequence only when required by ownership, repeated access, stable evaluation, or the consumer contract.
- Base optimization on an identified need or evidence; do not add complexity for speculative performance gains.

### Dependency injection

- Prefer constructor injection for required dependencies and keep them explicit.
- Select lifetimes according to ownership and state. Never capture a scoped dependency from a singleton.
- Do not use service locator patterns, mutable global services, or hidden static dependencies.

### Configuration

- Keep configuration outside application code and bind related settings through the Options pattern when it improves cohesion and validation.
- Validate critical configuration at startup or before first use so invalid state fails predictably.
- Keep secrets outside source code and the repository. If required configuration is absent, report it rather than inventing a value.

### HTTP and integration

- Use `IHttpClientFactory` when managed client lifetime, configuration, or handlers are required by the application; do not repeatedly create and discard clients in a way that causes connection-management problems.
- Define timeouts, propagate cancellation, and handle transport, protocol, and application failures explicitly.
- Add retries only for an identified transient-failure scenario and only when operation semantics, idempotency, limits, and cancellation are understood.

### Observability and testing

- Use structured logging with stable event properties and add scopes or correlation context when they improve traceability across the established operation.
- Do not log secrets or sensitive data, and avoid high-volume logging without operational value.
- Keep collaborators and side effects substitutable enough to test observable behavior. Avoid indiscriminate mocking and static dependencies that make relevant behavior difficult to isolate.

## Recommendations

- Prefer the simplest language and runtime features already supported by the consumer project.
- Follow established project conventions when they comply with applicable Rules and this skill.
- Use fakes, real lightweight collaborators, or integration tests when mocks would only reproduce implementation details.

## Anti-Patterns

- Sync-over-async with `.Result` or `.Wait()`.
- Service locator or hidden mutable global dependencies.
- A singleton capturing a scoped service.
- Hardcoded configuration or secrets.
- A broad catch that hides, discards, or falsely reports failure.
- Repeated `ToList`, `ToArray`, or enumeration without a contract-driven need.
- Abstractions or optimizations introduced for hypothetical future requirements.

## Validation

- Confirm applicable Security and Engineering Rules were followed and no missing requirement or configuration was invented.
- Check nullable contracts, async flow, cancellation propagation, resource ownership, and exception handling where applicable.
- Verify dependency lifetimes, configuration validation, HTTP timeout/error behavior, and absence of sensitive or excessive logging.
- Confirm tests cover the changed observable behavior without unnecessary coupling or mocking.

## Related Rules

- engineering/Architecture.md
- engineering/Dependencies.md
- engineering/Error-Handling.md
- engineering/Logging.md
- engineering/Testing.md
- engineering/Validation.md
- security/A02-Cryptographic-Failures.md
- security/A05-Security-Misconfiguration.md
- security/A06-Vulnerable-and-Outdated-Components.md
- security/A09-Security-Logging-and-Monitoring-Failures.md
- security/A10-Server-Side-Request-Forgery.md

## Related Skills

- backend/aspnet-core
