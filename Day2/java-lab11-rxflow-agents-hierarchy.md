# Hands-on Lab 11: RxFlow `AGENTS.md` Hierarchy — Java

```text
Act as a principal Java platform engineer responsible for creating and proving a hierarchical AGENTS.md governance model for RxFlow.

Objective:
Build or adapt a production-shaped, real-time prescription-to-lens platform organized into order_api, routing, pricing, workers, analytics, and infra. Create repository-level and component-level AGENTS.md files whose instructions are concrete, testable, and scoped to the code beneath each file.

Do not merely describe the hierarchy. Implement the instruction files, make the smallest representative code or test changes needed to demonstrate them, and verify the result with executed commands.

Technology baseline:
- Java 21
- Spring Boot 3.x
- Maven Wrapper or Gradle Wrapper, determined from repository evidence
- Spring Web and Bean Validation
- Spring Data JPA with PostgreSQL
- Flyway or Liquibase migrations
- Kafka or Redpanda for real-time events
- Redis for idempotency, capacity, and short-lived projections
- Resilience4j for external-call timeout, retry, and circuit-breaker policies
- Spring Security with OAuth2/JWT where APIs require authentication
- Micrometer and OpenTelemetry
- JUnit 5, AssertJ, Mockito, Awaitility, and Testcontainers
- Docker Compose for local infrastructure
- SpotBugs, Checkstyle, JaCoCo, and OWASP dependency checks when already available

Treat the stack as an assumption until verified. Do not add a dependency without asking when the repository does not already declare it.

Real-time business flow:
1. order_api accepts a prescription, frame selection, lens options, and idempotency key.
2. The prescription is validated without logging patient or full prescription data.
3. pricing calculates a versioned quote.
4. routing selects a capable lab using supported products, priority, and current capacity.
5. The order and outbox event are committed atomically.
6. A publisher emits OrderAccepted or ManufacturingRequested to Kafka.
7. workers consume the event idempotently, call a lab connector with bounded timeout and retry, and emit lifecycle events.
8. analytics consumes lifecycle and lab-availability events, builds operational projections, and calculates throughput using available operating time.
9. infra provides local PostgreSQL, Redis, Kafka/Redpanda, observability, and reversible operational instructions.

Required repository structure:

AGENTS.md
order_api/AGENTS.md
routing/AGENTS.md
pricing/AGENTS.md
workers/AGENTS.md
analytics/AGENTS.md
infra/AGENTS.md

If the repository uses Maven modules, Gradle subprojects, or src/main/java packages instead of these exact directories, place each AGENTS.md at the closest real component boundary and explain the mapping.

Permission boundary:
- Work only in the current repository.
- Read all applicable existing AGENTS.md files before editing.
- Preserve user changes and unrelated work.
- Do not deploy, merge, push, or access production systems.
- Do not use real patient or prescription data.
- Do not print or commit secrets.
- Ask before adding dependencies or starting external services.
- Use fake values in examples and environment files.
- Do not weaken tests, authentication, authorization, privacy controls, or quality gates.
- Never claim a command passed unless it was executed successfully.

Instruction-resolution contract:
- Root AGENTS.md establishes repository-wide requirements.
- A component AGENTS.md adds narrower rules for files beneath its directory.
- Instructions are cumulative unless a child file explicitly overrides a parent rule.
- A child file may make a parent rule stricter but must not silently weaken security, privacy, test, or rollback requirements.
- When files from multiple components change, apply the root instructions and every relevant component instruction.
- In the final report, state which AGENTS.md files governed each changed file.

Root AGENTS.md requirements:
Create a concise root file that enforces all of the following:

1. Scope and architecture
- Identify the service/module before editing.
- Keep HTTP, messaging, persistence, and infrastructure adapters separate from domain logic.
- Preserve public contracts unless compatibility impact is reviewed.

2. Defect discipline
- Every defect fix requires a regression test that fails before the fix and passes after it.
- Record the pre-fix failure and post-fix success.
- Do not combine a defect fix with unrelated refactoring.

3. Payment and financial paths
- Any change affecting payment, price finalization, refunds, credits, invoices, discounts, taxes, or financially binding order creation requires idempotency tests.
- Tests must prove the same idempotency key cannot create duplicate financial effects.
- Concurrency-sensitive paths require a deterministic concurrent or transaction-level test.

4. Database changes
- Every schema or data migration requires upgrade instructions, rollback or downgrade instructions, compatibility notes, and validation queries.
- Destructive migrations require an expand-and-contract plan.
- Never assume a migration is reversible when data loss is possible.

5. Public API changes
- Any change to a public HTTP endpoint, Kafka event, schema, error code, field meaning, or serialization format requires compatibility review.
- State whether the change is backward-compatible, additive, conditionally compatible, or breaking.
- Breaking changes require explicit approval and a migration/versioning plan.

6. External calls
- Every new HTTP, Kafka, database, Redis, object-storage, or vendor integration requires explicit timeout, bounded retry, backoff, jitter where appropriate, circuit-breaker/fallback decision, and observability.
- Never use infinite retry.
- Do not retry non-idempotent operations unless protected by an idempotency mechanism.

7. Sensitive data
- Never log patient names, identifiers, prescription values, tokens, credentials, or raw request/event payloads containing sensitive data.
- Log stable internal correlation IDs and redacted metadata only.
- Tests must verify redaction when logging behaviour changes.

8. Infrastructure
- Infrastructure changes require security validation, least-privilege review, secret-handling review, health checks, resource limits where relevant, and rollback instructions.
- No automatic production deployment.

9. Required final report
- Files changed and purpose.
- Applicable AGENTS.md files.
- Commands executed exactly as run.
- Exit status and meaningful result for each command.
- Tests added or changed.
- Pre-patch and post-patch evidence for defects.
- Compatibility, migration, security, privacy, and operational impact.
- Known limitations and commands not run.

order_api/AGENTS.md requirements:
- Keep controllers thin; business decisions belong in application/domain services.
- Validate idempotency keys at the boundary and enforce them transactionally.
- Reject invalid prescriptions; never silently normalize out-of-range values.
- Do not expose JPA entities directly.
- Use consistent structured errors with stable machine-readable codes.
- Public API changes require request/response contract tests and compatibility classification.
- Authentication and authorization changes require positive and negative tests.
- Never log request bodies containing prescription or patient data.
- Order acceptance must atomically persist the order and outbox record.

routing/AGENTS.md requirements:
- Routing must be deterministic for identical capability and capacity snapshots.
- Filter incapable labs before ranking.
- Capacity claims must use atomic Redis operations or transactional persistence.
- Define behaviour when Redis or capacity data is unavailable.
- External lab-capability calls require timeout, bounded retry, and failure tests.
- Routing overrides require authorization and audit evidence.
- Tests must cover no-capable-lab, equal priority, exhausted capacity, and concurrent claims.

pricing/AGENTS.md requirements:
- Use BigDecimal for money; never double or float.
- Make currency and rounding mode explicit.
- Centralize discount, tax, and surcharge rules.
- Financially binding price changes require idempotency and regression tests.
- Preserve quote/version information used to reproduce a price.
- Public pricing response changes require compatibility review.
- Test boundary values, rounding, stacked discounts, and duplicate submission.

workers/AGENTS.md requirements:
- Consumers and jobs must be idempotent under at-least-once delivery.
- Acknowledge events only after durable success or deliberate dead-letter handling.
- External connector calls require finite connect/read/overall timeouts.
- Retry must be bounded, use backoff, and avoid multiplicative retry layers.
- Classify retryable and permanent failures explicitly.
- Never log raw events or sensitive prescription content.
- Tests must cover duplicate delivery, timeout, retry exhaustion, poison event, and partial failure.

analytics/AGENTS.md requirements:
- Use Instant or OffsetDateTime for event instants and make reporting timezone explicit.
- Reporting windows use documented boundary semantics, preferably [from, to).
- Throughput must account for scheduled availability and merged offline intervals.
- Aggregations must be idempotent under duplicated and out-of-order events.
- Do not place patient identifiers or full prescriptions in analytics tables, logs, metrics, or labels.
- Tests use fixed clocks and deterministic fixtures.
- Cover nulls, gaps, overlapping offline intervals, DST boundaries, zero availability, and late events.

infra/AGENTS.md requirements:
- Use fake local credentials only.
- Pin major image/tool versions and document exposed ports.
- Apply least privilege to users, roles, containers, and service accounts.
- Add health checks and dependency ordering without assuming readiness from process startup alone.
- Database changes require rollback instructions and validation queries.
- Infrastructure changes require security validation and operational rollback.
- Never run terraform apply, kubectl apply, cloud deployment, or production migration automatically.

Enforcement scenarios:
Create a table or executable documentation showing how the hierarchy handles these representative changes:

A. A discount calculation defect in pricing
Expected rules: root + pricing; failing regression test; BigDecimal; idempotency test if financially binding; final command evidence.

B. A new field on POST /orders
Expected rules: root + order_api; compatibility review; contract tests; structured error impact.

C. A new lab connector in workers
Expected rules: root + workers; timeout, retry, idempotency, failure tests, redacted logs, metrics.

D. A new PostgreSQL index or column
Expected rules: root + owning component + infra when migration files live there; upgrade, rollback, validation, compatibility notes.

E. A Docker Compose or Kubernetes change
Expected rules: root + infra; security validation, least privilege, fake secrets, health check, rollback; no deployment.

F. A throughput gap-handling fix
Expected rules: root + analytics; deterministic failing test; fixed timestamps; gap/interval contract; post-fix evidence.

Required process:

Phase 1 — Inspect
1. Read existing AGENTS.md and repository documentation.
2. Identify module boundaries and actual build tool.
3. Inventory API, database, messaging, Redis, security, logging, and infrastructure code.
4. Present the proposed AGENTS.md placement before editing.

Phase 2 — Implement hierarchy
1. Create or refine the root AGENTS.md.
2. Create each component AGENTS.md.
3. Keep instructions concise, specific, non-duplicative, and executable.
4. Reference verified module-specific commands where appropriate.

Phase 3 — Demonstrate
1. Choose at least two low-risk representative scenarios, one of which must be a defect fix or test-only reproduction.
2. Show which instruction files apply.
3. Add the required tests or documentation evidence.
4. Do not introduce a fake defect into production code solely to satisfy the lab.

Phase 4 — Verify
Discover commands from mvnw, gradlew, pom.xml, build.gradle, Makefile, and CI. Run the relevant verified commands, for example:

./mvnw test
./mvnw verify
./gradlew test
./gradlew check
docker compose config

Do not invent passing results. If infrastructure is unavailable, report the exact blocker.

Required deliverables:
- Root AGENTS.md.
- Six component AGENTS.md files or documented equivalents.
- AGENTS hierarchy diagram in Mermaid.
- Instruction applicability matrix.
- Real-time RxFlow event-flow diagram in Mermaid.
- At least two representative enforcement demonstrations.
- Commands and results report.
- Compatibility review template.
- Migration and rollback checklist.
- External-call resilience checklist.
- Security and sensitive-logging checklist.

Final report format:

# Executive summary
# Verified repository and technology map
# AGENTS.md hierarchy
# Instruction resolution examples
# Real-time data and event flow
# Enforcement demonstrations
# Files changed
# Tests added or changed
# Commands executed and results
# Compatibility review
# Database and rollback review
# External-call timeout and retry review
# Security, privacy, and logging review
# Infrastructure validation
# Known limitations and unverified items

Success criteria:
- Root and component instructions have clear scopes.
- Every mandatory rule appears in an applicable AGENTS.md.
- Payment-path changes cannot be reported complete without idempotency evidence.
- Database changes cannot be reported complete without rollback instructions.
- Public contract changes cannot be reported complete without compatibility review.
- New external calls cannot be reported complete without timeout and retry decisions.
- Sensitive values are explicitly prohibited from logs.
- Infrastructure changes require security validation.
- Every defect fix requires a regression test.
- Final reports always list exact commands and results.
- The Java service remains buildable and tested.

Begin by inspecting the repository and presenting the proposed AGENTS.md hierarchy. Do not create production code until the hierarchy and applicable existing instructions have been identified.
```
