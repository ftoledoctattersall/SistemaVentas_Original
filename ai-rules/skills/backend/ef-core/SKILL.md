# Entity Framework Core

## Metadata

- Skill: backend/ef-core
- Skill-Version: 1.0.0
- Technology: Entity Framework Core
- Compatibility: Consumer-project supported versions
- Category: backend

## Purpose

Specialize implementation and review of Entity Framework Core persistence while preserving the applicable Database, Security, and Engineering Rules without imposing an architecture, database engine, or ORM abstraction.

## Applies When

Apply this skill only when `backend/ef-core` is explicitly declared in `ACTIVE-SKILLS.md` at the established Consumer Project Root and the task creates or materially modifies Entity Framework Core persistence.

An application is not assumed to use EF Core. Activating this skill does not activate any other skill.

## Does Not Cover

This skill does not decide SQL Server versus PostgreSQL, application architecture, Clean Architecture, Repository pattern, custom Unit of Work, CQRS, MediatR, microservices, business rules, cloud strategy, or an undefined functional schema.

It does not replace the Engineering Database standard or Security A03. It does not require EF Core for every .NET application.

## Authority and Constraints

This skill is optional and governed by `skills/README.md`. Applicable Security and Engineering Rules remain mandatory and take precedence.

Do not infer a database engine, migration policy, concurrency policy, domain model, transaction boundary, performance target, or data contract that the project has not established.

## Rules

### DbContext lifecycle

- Use a DbContext lifetime coherent with the natural unit of work; in request-scoped applications, use the established per-request or equivalent scope.
- Treat DbContext as not thread-safe. Never share one instance across concurrent operations or unrelated units of work.
- Keep a DbContext only as long as the operation requires it and dispose it through its owner or dependency-injection scope.
- Use asynchronous EF Core I/O operations and propagate `CancellationToken` when the surrounding operation supports cancellation.

### Tracking

- Use tracking when entities will be changed and persisted through the current context.
- Use `AsNoTracking` or an equivalent no-tracking approach for read-only queries when identity resolution and change detection are not required.
- Avoid tracking entities that will not be modified, but do not disable tracking globally without an established reason.
- Do not attach or combine entities from incompatible contexts without an explicit state and ownership decision.

### Queries

- Compose filters, projections, ordering, and limits before materializing a query.
- Project only the fields required by the operation when a complete entity graph is unnecessary.
- Avoid N+1 access patterns. Use an intentional query shape rather than relying on accidental lazy loading.
- Use `Include` only when the related data is required; avoid loading complete graphs or collections indiscriminately.
- Prefer asynchronous query execution for database I/O.
- Keep evaluation on the database where practical and identify any client-side evaluation or costly in-memory processing before accepting it.

### Pagination

- Apply explicit limits and a deterministic ordering to collection queries; never retrieve unbounded collections by default.
- Use offset pagination when its cost and consistency are sufficient for the established use case.
- Use keyset or seek pagination only when a demonstrated scale, consistency, or navigation requirement justifies it; do not impose it universally.

### Persistence and transactions

- Use `SaveChangesAsync` for asynchronous persistence and group changes according to the natural unit of persistence.
- Avoid multiple `SaveChanges` calls when one consistent save can satisfy the operation; do not combine unrelated units solely to reduce calls.
- DbContext already provides change tracking and a unit-of-work capability. Add a Repository or custom Unit of Work only for a demonstrated architectural need.
- Rely on the implicit transaction of `SaveChanges` when it is sufficient. Use an explicit transaction only when multiple operations must commit atomically.
- Keep transactions brief and do not hold them across long external calls or user interaction.
- Do not assume distributed transactions are available or appropriate.

### Concurrency

- Use optimistic concurrency when the operation has a real risk of conflicting updates.
- Configure and honor concurrency tokens or an equivalent established mechanism where required.
- Detect and handle concurrency conflicts explicitly according to the operation contract; never silently overwrite another actor's changes.

### Migrations

- Keep schema evolution in versioned migrations and review generated SQL and operational impact for risky changes.
- Do not alter migration history manually after it is established; create a corrective migration when appropriate.
- Do not apply destructive schema changes without a compatible data and deployment strategy.
- Separate destructive data operations from schema evolution when sequencing, rollback, or safety requires it.
- Do not assume migrations should be applied automatically in production; follow the established deployment process.

### Performance

- Base query and model optimizations on measurements or a demonstrated workload.
- Consider projections, suitable indexes, query shape, and round trips together.
- Avoid indiscriminate `Include`, N+1 queries, and avoidable repeated database trips.
- Use compiled queries only when evidence shows that their complexity addresses a real bottleneck.
- Do not optimize prematurely or trade correctness for an unmeasured gain.

### Raw SQL and model configuration

- Prefer LINQ when it represents the required query clearly and safely.
- Use raw SQL only for a justified capability or query shape, and use the provider's parameterized APIs for every external value.
- Never concatenate or interpolate external data into SQL. Apply Security A03 Injection controls in addition to this skill.
- Define relationships, cardinality, and nullability explicitly when they are important to the contract.
- Use Fluent API or equivalent configuration when it makes critical mapping decisions clear; do not rely accidentally on conventions for those decisions.
- Do not make domain models anemic or rich as a universal rule; model behavior according to the established architecture and requirements.

### Testing

- Distinguish unit tests from persistence tests and verify behavior at the level appropriate to the change.
- Do not mock DbSet or DbContext as a universal substitute for database behavior.
- Use a provider or test environment that represents the relevant relational, translation, transaction, constraint, and concurrency behavior.
- Do not assume the InMemory provider reproduces a relational database. Validate critical queries against an appropriate relational engine when behavior depends on it.

## Recommendations

- Keep query shape close to the use case and make expensive or unusual database behavior visible in review.
- Prefer explicit projections and bounded result contracts for read-heavy endpoints.
- Reuse established mapping and migration conventions when they satisfy the applicable Rules.

## Anti-Patterns

- Registering DbContext as a singleton.
- Concurrent access to the same DbContext instance.
- N+1 queries or indiscriminate `Include`.
- Premature materialization with `ToList` or equivalent.
- Unbounded collection queries.
- Concatenated raw SQL or external values embedded in SQL text.
- Repetitive `SaveChanges` calls without a persistence need.
- Introducing a generic Repository only by convention.
- Silently ignoring concurrency conflicts.
- Treating InMemory as equivalent to a relational database.
- Destructive migrations without impact and data strategy.

## Validation

- Confirm DbContext lifetime, disposal, thread-safety, tracking mode, and cancellation are correct for the operation.
- Confirm queries are composed before materialization, bounded, appropriately projected, and free of evident N+1 behavior.
- Confirm transactions are justified and brief, concurrency is considered when relevant, and `SaveChangesAsync` boundaries are coherent.
- Confirm raw SQL is parameterized, migrations are versioned and reviewed, and tests represent the relevant persistence risk.
- Confirm applicable Security and Engineering Rules, especially Database, Testing, Validation, A01, and A03, were followed.

## Related Rules

- engineering/Architecture.md
- engineering/Database.md
- engineering/Dependencies.md
- engineering/Error-Handling.md
- engineering/Logging.md
- engineering/Testing.md
- engineering/Validation.md
- security/A01-Broken-Access-Control.md
- security/A03-Injection.md
- security/A06-Vulnerable-and-Outdated-Components.md

## Related Skills

- backend/dotnet
- backend/aspnet-core
