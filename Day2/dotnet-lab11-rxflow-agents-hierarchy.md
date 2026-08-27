# Hands-on Lab 11: RxFlow `AGENTS.md` Hierarchy — .NET

```text
Act as a principal .NET platform engineer responsible for creating and proving a hierarchical AGENTS.md governance model for RxFlow.

Objective:
Build or adapt a production-shaped, real-time prescription-to-lens platform organized into order_api, routing, pricing, workers, analytics, and infra. Create repository-level and component-level AGENTS.md files whose instructions are concrete, testable, and scoped to all code beneath each instruction file.

Do not stop at documentation. Implement the hierarchy, demonstrate how instruction resolution works, add representative tests or evidence, and run the repository's verified build and quality commands.

Technology baseline:
- .NET 8 LTS
- C# 12 with nullable reference types enabled
- ASP.NET Core Web API
- Entity Framework Core with PostgreSQL
- EF Core migrations
- Kafka or Redpanda for real-time events
- StackExchange.Redis for idempotency and capacity projections
- HttpClientFactory with resilience handlers or Polly
- ASP.NET Core authentication/authorization with OAuth2/JWT where required
- BackgroundService for event consumers and outbox publishing
- OpenTelemetry, ILogger structured logging, and health checks
- xUnit, FluentAssertions, Moq or NSubstitute, and Testcontainers for .NET
- Docker Compose
- dotnet format, analyzers, coverlet, and dependency/security checks when already configured

Treat all technologies as assumptions until repository evidence confirms them. Ask before adding or upgrading packages.

Real-time business flow:
1. order_api accepts prescription, frame, lens options, and an idempotency key.
2. Validation rejects unmanufacturable prescriptions without logging sensitive values.
3. pricing creates a reproducible, versioned monetary quote.
4. routing chooses a capable lab from current priority and capacity snapshots.
5. The order and outbox message commit in one database transaction.
6. A hosted publisher emits OrderAccepted or ManufacturingRequested.
7. workers consume at least once, deduplicate, call the lab connector with explicit timeout and bounded retry, and publish lifecycle events.
8. analytics consumes lifecycle and availability events, supports late/duplicate delivery, and calculates throughput from available operating time.
9. infra supplies local PostgreSQL, Redis, Kafka/Redpanda, telemetry, health checks, and rollback instructions.

Required hierarchy:

AGENTS.md
order_api/AGENTS.md
routing/AGENTS.md
pricing/AGENTS.md
workers/AGENTS.md
analytics/AGENTS.md
infra/AGENTS.md

When the solution uses projects such as src/RxFlow.OrderApi or src/RxFlow.Analytics, place each AGENTS.md at the closest project boundary and document the equivalent hierarchy.

Permission boundary:
- Work only in the current repository.
- Read existing AGENTS.md files before editing.
- Preserve unrelated and user-authored changes.
- Do not deploy, merge, push, or access production systems.
- Never use real patient data, real prescriptions, or real credentials.
- Do not start containers or add NuGet packages without permission.
- Do not weaken analyzers, nullable checks, tests, authorization, or privacy controls.
- Never report success for a command that was not executed successfully.

Instruction-resolution contract:
- Root AGENTS.md applies everywhere.
- The nearest component AGENTS.md adds component-specific rules.
- Parent and child instructions are cumulative.
- Child rules may be stricter but cannot silently weaken security, privacy, compatibility, testing, or rollback obligations.
- Cross-component changes must satisfy every applicable file.
- Final reports map each changed file to its governing AGENTS.md files.

Root AGENTS.md must enforce:

1. Architecture
- Identify affected projects before editing.
- Keep endpoints, application services, domain logic, persistence, messaging, and infrastructure concerns separated.
- Respect solution and project dependency direction.

2. Defect fixes
- Every defect fix requires a deterministic regression test.
- Execute and record the test failing before the production patch.
- Execute and record it passing after the patch.
- Avoid unrelated refactoring in the defect diff.

3. Payment and financial paths
- Changes to pricing finalization, payment, invoice, refund, credit, discount, tax, or financially binding order creation require idempotency tests.
- Prove duplicate requests and repeated events do not create duplicate financial effects.
- Concurrency-sensitive changes require deterministic concurrency or transaction tests.

4. Database changes
- EF Core migrations require apply instructions, rollback instructions, generated SQL review, compatibility impact, and validation queries.
- Destructive changes require expand-and-contract planning.
- Do not equate Down() existence with safe reversibility when data may be lost.

5. Public contracts
- Changes to HTTP APIs, OpenAPI, problem details, Kafka schemas, event semantics, or serialized DTOs require compatibility review.
- Classify changes as additive, backward-compatible, conditionally compatible, or breaking.
- Breaking changes require explicit approval, versioning, and consumer migration plans.

6. External calls
- New HTTP, database, Kafka, Redis, cloud, or vendor calls require connect/request timeout, bounded retries, backoff, jitter decision, circuit breaker/fallback decision, cancellation-token propagation, and telemetry.
- Do not stack retries across HttpClient, worker, broker, and job layers without a total attempt budget.
- Do not retry non-idempotent calls without an idempotency mechanism.

7. Sensitive data
- Never log patient identifiers, prescription values, credentials, tokens, authorization headers, or raw sensitive payloads.
- Use correlation IDs and redacted metadata.
- Logging changes touching sensitive paths require redaction tests.
- Do not use sensitive or unbounded values as metric labels.

8. Infrastructure
- Require least privilege, secret review, image/version review, health checks, resource and network validation, and rollback instructions.
- Do not automatically run terraform apply, kubectl apply, cloud deployment, or production migrations.

9. Final reporting
- List files changed and purpose.
- List applicable AGENTS.md files.
- Record exact commands, exit codes, and meaningful results.
- Include test counts and pre/post defect evidence.
- Include compatibility, migration, resilience, security, privacy, and operational impact.
- Mark checks PASSED, FAILED, NOT RUN, or BLOCKED.

order_api/AGENTS.md must enforce:
- Minimal API handlers/controllers remain thin.
- Domain and application services own decisions.
- Idempotency keys are validated and enforced using atomic database or Redis mechanisms.
- Invalid prescriptions are rejected rather than corrected silently.
- EF entities are not exposed as public DTOs.
- Use stable RFC 7807-compatible problem details or the repository's verified structured-error convention.
- Public contract changes require integration/contract tests and compatibility classification.
- Authorization changes require allowed and forbidden tests.
- Do not log request bodies containing sensitive data.
- Order and outbox persistence share a transaction.

routing/AGENTS.md must enforce:
- Remove incapable labs before capacity ranking.
- Identical snapshots produce deterministic choices.
- Capacity reservations use atomic Redis scripts/operations or database transactions.
- Document degraded behaviour when Redis or capacity data is unavailable.
- New lab-directory calls require HttpClientFactory, cancellation, timeout, bounded retry, and failure tests.
- Overrides require policy-based authorization and an audit record.
- Tests cover no candidates, capacity exhaustion, equal rank, stale data, and concurrent reservation.

pricing/AGENTS.md must enforce:
- Use decimal for monetary calculations; never float or double.
- Make currency, scale, and MidpointRounding mode explicit.
- Centralize discount, tax, and surcharge policies.
- Financial changes require regression and idempotency tests.
- Store or return quote/rule versions sufficient for reproduction.
- Public pricing DTO changes require compatibility review.
- Test rounding boundaries, stacked rules, duplicate requests, and concurrency where applicable.

workers/AGENTS.md must enforce:
- BackgroundService consumers are idempotent under at-least-once delivery.
- Pass CancellationToken through all async operations.
- Commit/acknowledge only after durable success or deliberate dead-letter handling.
- External calls use named/typed HttpClient with explicit timeout and bounded retry.
- Classify transient and permanent failures.
- Avoid retry multiplication across broker, worker, and HTTP policies.
- Never log raw messages or sensitive data.
- Tests cover duplicate delivery, timeout, cancellation, retry exhaustion, poison events, and partial failure.

analytics/AGENTS.md must enforce:
- Use DateTimeOffset or Instant-equivalent semantics for event instants.
- Make report timezone and [from, to) boundary semantics explicit.
- Merge and clip offline intervals before calculating available operating duration.
- Handle duplicate, late, missing, and out-of-order events deterministically.
- Never store patient identifiers or full prescriptions in analytics projections, logs, metrics, or traces.
- Use TimeProvider or fixed DateTimeOffset fixtures rather than DateTime.UtcNow in tests.
- Test nulls, gaps, overlaps, zero availability, DST, duplicated events, and late arrivals.

infra/AGENTS.md must enforce:
- Use fake development credentials only.
- Pin appropriate image versions and document ports.
- Apply least privilege to database roles, containers, service accounts, and networks.
- Add health checks that validate readiness, not only process existence.
- Database migrations include rollback and validation instructions.
- Infrastructure changes include security validation and rollback.
- Never execute production changes automatically.

Representative enforcement scenarios:

A. Pricing discount defect
Applicable: root + pricing.
Required: failing regression test, decimal correctness, idempotency if financially binding, post-fix proof.

B. New property on an order response
Applicable: root + order_api.
Required: compatibility classification, JSON/OpenAPI contract test, consumer impact review.

C. New external laboratory HTTP client
Applicable: root + routing or workers.
Required: HttpClientFactory, cancellation, timeout, bounded retry, non-idempotent retry decision, telemetry, failure tests.

D. New EF Core column/index
Applicable: root + owning component and possibly infra.
Required: migration, SQL review, rollback, compatibility note, validation query.

E. Docker Compose, Terraform, or Kubernetes change
Applicable: root + infra.
Required: fake secrets, least privilege, security validation, health check, rollback; no deployment.

F. Offline-gap throughput defect
Applicable: root + analytics.
Required: explicit interval contract, deterministic failing test, fixed timestamps, minimal patch, post-fix evidence.

Required execution process:

Phase 1 — Inspect
1. Read existing instructions and documentation.
2. Inspect .sln, .csproj, global.json, Directory.Build.props, NuGet configuration, Docker, CI, migrations, and tests.
3. Map the real project boundaries.
4. Present proposed AGENTS.md placement before edits.

Phase 2 — Implement
1. Create/refine root AGENTS.md.
2. Create the six component files or verified equivalents.
3. Keep shared rules at root and component-only rules in child files.
4. Use direct, testable language rather than vague advice.

Phase 3 — Demonstrate
1. Select at least two safe representative scenarios.
2. At least one must demonstrate regression-test enforcement or be a test-only reproduction.
3. Identify applicable instructions for each scenario.
4. Add minimal evidence without manufacturing a production defect.

Phase 4 — Verify
Discover the real commands from solution files, scripts, Makefile, README, and CI. Run relevant commands such as:

dotnet restore
dotnet build --configuration Release --no-restore
dotnet test --configuration Release --no-build
dotnet format --verify-no-changes --no-restore
dotnet list package --vulnerable --include-transitive
docker compose config

Run only commands justified by repository evidence and available tools. Report exact blockers.

Required deliverables:
- Root AGENTS.md.
- Six component AGENTS.md files or documented project equivalents.
- Mermaid hierarchy diagram.
- Applicability matrix mapping change types to instructions.
- Mermaid real-time event and data-flow diagram.
- Two or more enforcement demonstrations.
- Compatibility-review template.
- EF migration and rollback checklist.
- External-call resilience checklist.
- Security/privacy/logging checklist.
- Commands-and-results report.

Final report:

# Executive summary
# Verified solution and technology map
# AGENTS.md hierarchy
# Instruction resolution and applicability matrix
# Real-time event flow
# Enforcement demonstrations
# Files changed
# Tests and regression evidence
# Commands executed and results
# Public contract compatibility review
# EF Core migration and rollback review
# External-call resilience review
# Security, privacy, and logging validation
# Infrastructure validation
# Known limitations and blocked checks

Success criteria:
- Root and component scopes are unambiguous.
- Payment-path changes require demonstrated idempotency.
- Database changes require rollback instructions and validation.
- Public APIs and events require compatibility review.
- External calls require timeout and bounded retry policies.
- Sensitive information is prohibited from logs and metric labels.
- Infrastructure changes require security validation and rollback.
- Every defect fix requires a regression test with pre/post evidence.
- Final reports contain exact commands and results.
- The .NET solution remains buildable and verified.

Begin with repository reconnaissance and a proposed hierarchy. Do not edit production code before identifying all applicable existing instructions and project boundaries.
```
