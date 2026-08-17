# SAP Integration

## Metadata

- Skill: integration/sap
- Skill-Version: 1.0.0
- Technology: Framework-agnostic
- Compatibility: Contract-dependent
- Category: integration

## Purpose

Specialize implementation and review of SAP integrations as secure, explicit, decoupled and resilient external boundaries. Apply only after the consumer project has selected the SAP system, service, protocol and contract.

## Applies When

Apply only when `integration/sap` is explicitly declared in `ACTIVE-SKILLS.md` at the Consumer Project Root and the task creates or changes a SAP integration. Presence of SAP, S/4HANA, OData, RFC or SOAP does not activate this skill.

## Does Not Cover

This skill does not decide SAP ECC versus S/4HANA, RISE, OData versus RFC/SOAP/events, API or version, OData V2 versus V4, middleware, Integration Suite/CPI/PI/PO, Lambda, business rules, internal model, synchronization frequency, master-data ownership or functional conflict resolution.

## Authority and Constraints

Security and Engineering rules are mandatory and precede this skill. The consumer project's approved SAP contract prevails. Do not invent fields, permissions, endpoints, semantics, guarantees or architecture. Do not silently change an API, protocol or version. Related skills are informative and are not activated transitively.

## Rules

### External contract and versioning

- Identify the SAP system, service/API, version, protocol, environment and approved contract.
- Use integration DTOs and explicit mappings; keep SAP naming and models at the boundary unless propagation is an intentional contract decision.
- Treat changes to fields, types, behavior, API or version as explicit contract changes; do not migrate from an approved API or OData V2 to V4 automatically.
- Validate response shape, nullability, unknown or missing fields, units, currencies, identifiers and dates. Do not map invalid values silently to functional defaults or truncate values.
- Use an anti-corruption boundary only when it reduces coupling; avoid layers that add no behavior.

### OData and query construction

When the approved service uses OData:

- Respect the service's actual OData version and documented capabilities; do not assume every query option is supported.
- Build `$select`, `$filter`, `$orderby`, `$top` and other parameters from validated values and encode them correctly.
- Use `$select` and explicit limits when they reduce payload without changing the contract.
- Do not concatenate external values into filters or URLs without validation.
- Keep ordering deterministic whenever pagination or synchronization depends on it.

### Pagination

- Follow server-driven continuation links or tokens returned by SAP; do not reconstruct continuation URLs when the received contract can be preserved.
- Validate every continuation URL against the configured SAP host, scheme and approved base path before following it.
- Treat tokens as opaque: OData V2 may use `$skiptoken`; OData V4 or another service may use a different continuation mechanism.
- Bound pages/elements when appropriate, detect repeated tokens or URLs, and stop on a defensible limit to avoid infinite loops.
- Do not assume that client-side `$skip`/`$top` is equivalent to server-driven paging.

### Incremental synchronization

- Use a documented and approved change field or cursor; never assume `LastChangeDateTime` is universal.
- Persist an explicit durable checkpoint when needed, with a stable ordering and a boundary strategy that does not lose records sharing a timestamp.
- Advance the checkpoint only after the batch is successfully processed and its effects are durable.
- Make re-execution safe and reconcile duplicates; do not claim exactly-once delivery.

### Idempotency and writes

- Assume calls and events can be repeated. Define the authoritative record/operation identity and handle duplicates explicitly.
- Use upsert only when the approved functional contract permits it. Do not turn a non-idempotent POST into a retry-safe operation by assumption.
- Coordinate idempotency with `cloud/aws-lambda` when a Lambda invokes SAP; that relationship does not activate the other skill.

### Authentication, authorization and HTTP

- Obtain Basic, OAuth, certificate or other approved credentials from secure configuration; never hardcode or log them. Use TLS and rotation appropriate to the mechanism.
- Respect SAP permissions and least privilege. Distinguish 401 from 403 and never increase privileges automatically.
- Use reusable clients, explicit timeouts, cancellation, controlled headers, content type, encoding and response-size limits.
- Handle transport, DNS, TLS, timeout, HTTP, authentication, authorization, functional SAP and malformed-payload failures distinctly.
- Treat configured `BaseUrl` as trusted configuration. Do not let user input choose a SAP host, scheme or port.
- Do not follow external redirects or continuation links unless each destination is validated against the SAP allowlist. Apply Security A10.

### Retries and throttling

- Retry only failures that are demonstrably transient and whose operation has an explicit duplicate strategy.
- Use bounded attempts, backoff/jitter and `Retry-After` when supplied; prevent retry storms and record exhaustion.
- Respect SAP throttling and quotas. Bound concurrency and prefer supported batching/pagination over indiscriminate parallel synchronization.
- Do not retry authentication, validation, contract or permanent functional failures as if they were transient.

### Data and batch semantics

- Interpret dates and time zones from the actual service contract; do not assume local time. Test temporal boundaries.
- Preserve decimal precision for amounts and quantities; do not use floating point where it can introduce monetary error. Do not convert units or assume a default currency without an approved rule.
- Use SAP batch endpoints only when they add value and the contract supports them. Bound batch size, handle partial failures, and do not assume atomicity.

### Observability

- Record service, operation, duration, result, status code, retry count, item count, checkpoint and correlation ID when available.
- Distinguish transport, timeout, authentication/authorization, functional, contract, throttling and permanent failures for diagnosis and alerting.
- Never log Authorization headers, passwords, tokens or complete sensitive payloads. Apply Security A09 and Engineering Logging.

### Testing and change control

- Test mappings, parsing, empty/null responses, malformed responses, HTTP 401/403/429/5xx, timeouts, retries, continuation, duplicate execution and synchronization boundaries.
- Separate deterministic unit/contract tests from approved integration tests against SAP. Mocks do not reproduce all SAP behavior, pagination, throttling or authorization semantics.
- Treat SAP metadata, API/version, pagination and field-semantic changes as reviewable integration changes; do not adapt behavior silently.

## Recommendations

- Prefer the smallest explicit boundary that satisfies the approved contract.
- Preserve server-provided continuation URLs when validated; official SAP documentation warns against modifying such links in server-side pagination scenarios. [SAP server-side pagination](https://help.sap.com/docs/successfactors-platform/sap-successfactors-api-reference-guide-odata-v2/server-side-pagination)
- Consult the service-specific documentation in SAP Help Portal or SAP Business Accelerator Hub; SAP publishes API references there for S/4HANA. [SAP APIs on Business Accelerator Hub](https://help.sap.com/docs/SAP_S4HANA_ON-PREMISE/2628c891a3a04f05a293c7ca5d23e4b6/1e60f14bdc224c2c975c8fa8bcfd7f3f.html)
- Relate `backend/dotnet`, `backend/aspnet-core` and `cloud/aws-lambda` only when those skills are independently active.

## Anti-Patterns

- SAP models leaking directly into the domain without an explicit decision.
- URLs or hosts built from untrusted input.
- Continuation links followed without host/path validation.
- Indiscriminate retries or assuming exactly-once delivery.
- Pagination without stable ordering where consistency requires it.
- Advancing a checkpoint before the batch completes.
- Unsafe filter concatenation.
- Hardcoded credentials or logs containing Authorization/payload secrets.
- Assuming local timezone, truncating monetary precision or inventing SAP field semantics.
- Automatically migrating API, protocol or version.

## Validation

- [ ] Approved SAP service, protocol, version and contract are identified.
- [ ] DTOs/mappings, nullability, dates, units, currencies and precision are explicit.
- [ ] OData capabilities, query encoding and limits match the selected service.
- [ ] Pagination/continuation is validated, bounded and loop-safe.
- [ ] Checkpoint advances only after successful durable processing.
- [ ] Idempotency and retry classification are explicit.
- [ ] Credentials, TLS, least privilege and SSRF controls comply with Security.
- [ ] Timeouts, cancellation, throttling and concurrency protect SAP.
- [ ] Logs are useful without secrets or sensitive payloads.
- [ ] Tests cover contract, pagination, failures, duplicates and temporal boundaries.

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
- engineering/Database.md
- security/A02-Cryptographic-Failures.md
- security/A03-Injection.md
- security/A05-Security-Misconfiguration.md
- security/A07-Identification-and-Authentication-Failures.md
- security/A09-Security-Logging-and-Monitoring-Failures.md
- security/A10-Server-Side-Request-Forgery.md

## Related Skills

- backend/dotnet
- backend/aspnet-core
- cloud/aws-lambda

All relationships are informative. No skill is activated transitively.
