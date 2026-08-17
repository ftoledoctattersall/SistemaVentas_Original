# ASP.NET Core

## Metadata

- Skill: backend/aspnet-core
- Skill-Version: 1.0.0
- Technology: ASP.NET Core
- Compatibility: Consumer-project supported versions
- Category: backend

## Purpose

Specialize implementation and review of ASP.NET Core web applications and APIs through explicit pipeline, HTTP contract, endpoint, authentication, authorization, dependency injection, configuration, client, logging, and API-safety decisions.

## Applies When

Apply this skill only when `backend/aspnet-core` is explicitly declared in `ACTIVE-SKILLS.md` at the established Consumer Project Root and the task creates or materially modifies an ASP.NET Core web application or API.

Activating this skill does not activate `backend/dotnet`; each skill must be declared independently.

## Does Not Cover

This skill does not decide project architecture, Clean Architecture, CQRS, MediatR, microservices, serverless, databases, ORMs, business rules, undefined contracts, cloud infrastructure, or external libraries without an explicit need.

It does not select Controllers versus Minimal APIs as a universal approach and does not assume JWT, SQL Server, or Entity Framework Core.

General .NET and C# runtime practices belong to `backend/dotnet`. The relationship is informative and non-transitive.

## Authority and Constraints

This skill is optional and governed by `skills/README.md`. Applicable Security and Engineering Rules remain mandatory and take precedence.

Do not treat middleware, model binding, endpoint metadata, policies, filters, or UI visibility as substitutes for required server-side security controls. Do not invent routes, status codes, authorization policy, payload limits, or public contracts that are not established.

## Rules

### Pipeline and environment

- Keep the middleware pipeline explicit, minimal, and ordered according to each middleware dependency and the established authentication, authorization, routing, error-handling, and endpoint flow.
- Add middleware only for an identified cross-cutting responsibility and keep environment-specific behavior in explicit environment configuration.
- Do not expose development diagnostics or development-only middleware in production.

### APIs and endpoints

- Define clear request and response DTOs and stable HTTP contracts. Do not expose persistence entities directly through public endpoints.
- Use HTTP status codes that match the established operation outcome and represent errors through the application's uniform mechanism, using `ProblemDetails` when it is the selected compatible contract.
- Validate model-bound input before business execution and bind only fields required by the operation to prevent overposting.
- Keep controllers, handlers, and Minimal API endpoints focused on HTTP concerns and orchestration; place business behavior in its responsible component.
- Never trust client-supplied identifiers, roles, claims, tenant values, or ownership assertions as authorization decisions.

### Authentication and authorization

- Configure authentication in the appropriate infrastructure and pipeline boundary and perform authorization on the server before the protected operation.
- Use policies when they make established authorization criteria explicit and reusable; do not invent policies or permissions.
- Deny safely when required authorization is absent or cannot be resolved from an authoritative source.
- Return `401 Unauthorized` for missing or invalid authentication and `403 Forbidden` for an authenticated principal denied by an established authorization decision, subject to the application's security disclosure policy.
- Never treat hidden UI elements or client-side route protection as authorization.

### Dependency injection and configuration

- Select web-service lifetimes according to ownership and request scope. Use scoped lifetime for per-request dependencies when appropriate and keep singleton services thread-safe.
- Never inject or otherwise capture a scoped service in a singleton.
- Bind cohesive settings with `IOptions<T>` or its variants when appropriate, keep environment configuration explicit, and validate critical settings during startup.

### HTTP clients and logging

- Use named or typed clients when they clarify a distinct external integration and centralize its established configuration.
- Define timeouts, propagate request cancellation where appropriate, and handle external failures explicitly.
- Add resilience policies only for an identified failure mode. Do not automatically retry non-idempotent operations without an explicit design for duplicate execution and side effects.
- Preserve request or correlation context in relevant structured events. Do not indiscriminately log complete request or response bodies, secrets, tokens, or sensitive data.

### API safety

- Apply established reasonable limits to request bodies, uploads, and collection operations; require pagination when unbounded results could exceed those limits.
- Handle files explicitly with validated type, size, name, storage destination, and processing boundaries when file operations are part of the contract.
- Validate externally influenced outbound URLs and destinations under the applicable SSRF rules before issuing server-side requests.

## Recommendations

- Choose Controllers or Minimal APIs from established project conventions and the needs of the endpoint set, not as a universal rule.
- Prefer framework mechanisms already present in the project over additional middleware or libraries with overlapping responsibility.
- Keep endpoint-level tests focused on observable HTTP behavior and add integration coverage where pipeline ordering or framework binding is material.

## Anti-Patterns

- Extensive business logic inside a controller or endpoint.
- Returning exception details or stack traces to clients.
- Trusting client-provided IDs, roles, claims, tenant, or ownership for authorization.
- Exposing persistence entities as public API contracts.
- Middleware ordered without regard to authentication, authorization, routing, or error boundaries.
- Authorization implemented only in the frontend.
- Unbounded endpoints that return or accept large collections or payloads.
- Automatic retries for non-idempotent operations without an explicit duplicate-execution design.

## Validation

- Confirm applicable Security and Engineering Rules were applied and neither contracts nor authorization decisions were invented.
- Verify pipeline order, environment behavior, service lifetimes, configuration validation, and cancellation where applicable.
- Check DTO boundaries, validation, status codes, uniform errors, server-side authorization, payload limits, and outbound URL handling.
- Confirm logs avoid sensitive bodies and tests cover the changed HTTP behavior and relevant pipeline integration.

## Related Rules

- engineering/Architecture.md
- engineering/API.md
- engineering/Authentication.md
- engineering/Authorization.md
- engineering/Dependencies.md
- engineering/Error-Handling.md
- engineering/Logging.md
- engineering/Testing.md
- engineering/Validation.md
- security/A01-Broken-Access-Control.md
- security/A03-Injection.md
- security/A05-Security-Misconfiguration.md
- security/A07-Identification-and-Authentication-Failures.md
- security/A09-Security-Logging-and-Monitoring-Failures.md
- security/A10-Server-Side-Request-Forgery.md

## Related Skills

- backend/dotnet
