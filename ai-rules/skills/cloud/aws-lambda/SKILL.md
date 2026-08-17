# AWS Lambda

## Metadata

- Skill: cloud/aws-lambda
- Skill-Version: 1.0.0
- Technology: AWS Lambda
- Compatibility: Consumer-project supported configuration
- Category: cloud

## Purpose

Specialize implementation and review of AWS Lambda functions for safe event-driven execution, idempotent side effects, controlled retries, bounded concurrency, observability, resilience, performance, and cost awareness without imposing a language, architecture, or AWS service selection.

## Applies When

Apply this skill only when `cloud/aws-lambda` is explicitly declared in `ACTIVE-SKILLS.md` at the established Consumer Project Root and the task creates or materially modifies an AWS Lambda function or its directly related event configuration.

Lambda is assumed to have been selected by an existing architectural decision. This skill does not activate any other skill.

## Does Not Cover

This skill does not decide whether Lambda is the primary backend, serverless versus persistent ASP.NET, microservices, the event source, SQS versus EventBridge, DynamoDB versus SQL, RDS, API Gateway, infrastructure-as-code technology, AWS Region, multi-account strategy, VPC, disaster recovery, or business rules.

It does not define the consumer application's architecture, organizational IAM strategy, or a universal language/runtime implementation. It does not require `backend/dotnet` even when the consumer project uses .NET.

## Authority and Constraints

This skill is optional and governed by `skills/README.md`. Applicable Security and Engineering Rules remain mandatory and take precedence.

Do not invent event contracts, delivery guarantees, permissions, timeout values, retry policy, concurrency limits, secret locations, or downstream capabilities. Treat client-side behavior and deployment-console state as insufficient security controls.

## Rules

### Execution model

- Treat each invocation as an independent event-driven operation. Do not depend on a previous invocation having run in the same execution environment.
- Assume an execution environment may be reused, but may also be discarded at any time. Keep functional state in an authoritative external system when it must survive an invocation.
- Do not retain user data, complete events, secrets, or sensitive invocation state in process memory or temporary storage for accidental reuse by another invocation.
- Initialize reusable clients, configuration, and connections outside the handler only when their ownership, freshness, thread-safety, and failure behavior make reuse safe and beneficial.

### Handler

- Keep the handler small: validate the event, establish the operation context, delegate complex behavior, and translate the result or failure explicitly.
- Validate event shape, required fields, size, and source assumptions before using values.
- Respect runtime cancellation and the remaining invocation time when the language/runtime exposes them. Do not start work that cannot reasonably finish within the available budget.
- Avoid repeating expensive initialization in the handler when safe reuse outside it is established; do not move mutable or request-specific state outside the handler merely for speed.

### Idempotency

- Assume an event or invocation can be delivered more than once whenever the trigger or service provides at-least-once semantics; never assume exactly-once delivery.
- Design every externally visible side effect for idempotency, especially writes, billing, SAP updates, message sends, and calls to external systems.
- Use a stable idempotency key derived from an authoritative event or operation identity when the operation needs duplicate detection.
- Make duplicate handling explicit: return the prior result, safely ignore the duplicate, or apply a defined reconciliation path.
- Do not rely on local memory, execution-environment reuse, or retries being absent to prevent duplicate effects.

### Retries and event sources

- Distinguish transient failures from permanent validation, contract, authorization, or business failures before retrying.
- Apply retries only when the operation is idempotent or has an explicit duplicate-effect design. Use bounded backoff and jitter when retries are appropriate.
- Respect the retry and visibility semantics of the invocation model; synchronous, asynchronous, SQS, streams, S3, EventBridge, and other event sources can behave differently.
- Avoid retry storms by coordinating retry limits, timeouts, backoff, concurrency, and downstream capacity.
- When processing batches, isolate item failures where the event source supports partial batch responses, avoid reprocessing successful items unnecessarily, and preserve idempotency for every item.
- Configure dead-letter queues or failure destinations when the invocation model supports them and the failure risk justifies them; monitor and operate the destination rather than treating it as recovery by itself.

### Timeouts and concurrency

- Define the function timeout consciously from measured work, downstream budgets, and event-source constraints; do not accept a maximum or default timeout without analysis.
- Set downstream timeouts below the total invocation budget and propagate cancellation where supported so the function can stop coherently.
- Remember that Lambda can run multiple execution environments concurrently. Protect databases, SAP, APIs, queues, and other quota-limited systems from uncontrolled fan-out.
- Consider reserved or maximum concurrency when a downstream limit, workload isolation, or cost boundary requires it; do not prescribe universal values.
- Design shared clients, pools, caches, and temporary resources for concurrent execution and possible reuse without leaking request data.

### Configuration and secrets

- Keep operational configuration outside code and separate it by environment. Validate critical configuration before processing an event.
- Do not hardcode credentials, API keys, ARNs, URLs, regions, or environment-specific values unless an explicit project contract requires them.
- Treat environment variables as configuration, not as a universal secrets manager. Store sensitive credentials and tokens in the project-approved secret mechanism, such as AWS Secrets Manager when appropriate.
- Limit function access to secrets by least privilege and never log secrets, tokens, or complete sensitive payloads.

### IAM and networked dependencies

- Use an execution role with least-privilege actions and resources for the function's actual responsibility. Do not grant administrative or wildcard permissions for convenience.
- Do not embed static AWS credentials in function code or configuration.
- Handle DNS, connection, protocol, and downstream failures explicitly; reuse connections when safe and avoid connection storms.
- Validate externally controlled URLs and destinations under Security A10 before making server-side requests. Do not assume VPC placement is always required or is itself a security control.
- When accessing a database, bound concurrency and connection lifetime, preserve transaction and idempotency semantics, and introduce a proxy or intermediary only for a demonstrated need.

### Cold starts, performance, and cost

- Reduce unnecessary initialization, dependencies, package size, and repeated work only when they have a measured impact on the workload.
- Select memory and related performance settings from measurements, balancing latency, throughput, and cost; do not optimize speculative micro-benchmarks.
- Avoid unnecessary invocations, uncontrolled recursive triggers, and loops that can multiply work or cost.
- Consider batching when it reduces invocations and remains compatible with ordering, failure, idempotency, and latency requirements.

### Observability and deployment

- Emit structured logs with request, correlation, or event identifiers when available and distinguish validation, transient, permanent, retry, timeout, and downstream failures.
- Monitor relevant invocation count, duration, errors, throttles, concurrency, retry/failure accumulation, and event-source lag or age where supported.
- Do not log complete events or request/response payloads indiscriminately; record only the context needed for diagnosis and recovery.
- Add metrics, tracing, or a failure destination when they provide operational value; do not impose X-Ray, OpenTelemetry, or a particular observability library universally.
- Keep function configuration and infrastructure reproducible and versionable when the project uses infrastructure as code. Do not depend exclusively on manual console changes.

### Testing

- Keep functional behavior testable outside the handler when that improves isolation without inventing architecture.
- Test event validation, duplicate delivery, idempotency, retries, permanent and transient failures, timeout/cancellation behavior, and partial-batch handling when applicable.
- Use unit tests for deterministic logic and integration tests for interactions with relevant AWS or downstream behavior.
- Do not assume mocks fully reproduce AWS event-source, retry, visibility, permissions, throttling, or consistency behavior.

## Recommendations

- Keep the implementation language-neutral and apply `backend/dotnet` separately when C#/.NET is explicitly active.
- Prefer the smallest event contract and permission set that satisfies the established use case.
- Load-test meaningful workloads before tuning timeout, memory, concurrency, batching, or cold-start behavior.

## Anti-Patterns

- Assuming exactly-once delivery.
- Non-idempotent handlers with external side effects.
- Hardcoded credentials or secrets.
- Overly privileged execution roles or unnecessary `*` permissions.
- Indiscriminate retries or retry storms.
- Using a maximum/default timeout without workload analysis.
- Creating an expensive new connection for every operation when safe reuse is available.
- Depending on local memory or execution-environment persistence for functional state.
- Unlimited concurrency against a bounded downstream system.
- Logging complete payloads, credentials, or tokens.
- Uncontrolled recursive invocation.
- Choosing Lambda by default for every problem.

## Validation

- Confirm the event is validated and the handler remains small, bounded, and explicit about errors and cancellation.
- Confirm idempotency, duplicate effects, retry safety, event-source semantics, and partial failures were considered.
- Confirm timeout budgets, concurrency, downstream protection, and connection behavior are coherent.
- Confirm configuration and secrets are externalized, IAM follows least privilege, and SSRF controls apply to external URLs.
- Confirm structured observability distinguishes errors, retries, throttles, latency, and accumulated failures without sensitive payload logging.
- Confirm cost/performance choices are evidence-based, tests match the failure risk, and applicable Security and Engineering Rules were followed.

## Related Rules

- engineering/Architecture.md
- engineering/Dependencies.md
- engineering/Error-Handling.md
- engineering/Logging.md
- engineering/Testing.md
- engineering/Validation.md
- engineering/Authentication.md
- engineering/Authorization.md
- security/A02-Cryptographic-Failures.md
- security/A05-Security-Misconfiguration.md
- security/A07-Identification-and-Authentication-Failures.md
- security/A09-Security-Logging-and-Monitoring-Failures.md
- security/A10-Server-Side-Request-Forgery.md

## Related Skills

- backend/dotnet
