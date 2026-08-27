# Module 23: Enterprise Capstone

## RxFlow: Production Incident to Verified Release

This prompt pack supports both Java and .NET delivery. Before running a prompt, set:

```text
TARGET_STACK = JAVA
```

or:

```text
TARGET_STACK = DOTNET
```

## Technology profiles

### Java

```text
Java 21; Spring Boot 3; Spring Web; Jakarta Validation; Spring Security;
OAuth2/JWT; Spring Data JPA; PostgreSQL; Flyway/Liquibase; Redis;
Kafka/Redpanda; Spring Batch and Kafka consumers; Resilience4j;
Micrometer; OpenTelemetry; JUnit 5; AssertJ; Mockito; Awaitility;
Testcontainers; Gatling/k6; Maven/Gradle; Checkstyle; SpotBugs; JaCoCo;
OWASP dependency checks; Docker Compose; GitHub Actions.
```

### .NET

```text
.NET 8; C# 12; ASP.NET Core Web API; OAuth2/JWT; domain/application
service projects; Entity Framework Core; PostgreSQL; EF migrations;
StackExchange.Redis; Kafka/Redpanda; BackgroundService; HttpClientFactory;
Polly/resilience handlers; ILogger; OpenTelemetry; health checks; xUnit;
FluentAssertions; Moq/NSubstitute; Testcontainers; NBomber/k6; dotnet CLI;
dotnet format; analyzers; coverlet; dependency checks; Docker Compose;
GitHub Actions.
```

Treat each profile as an assumption until repository evidence verifies it.

---

# Prompt 1 — Intermediate

```text
TARGET_STACK = JAVA
ROLE = Principal Solution Architect and Senior Software Engineer

Act as the principal architect responsible for an evidence-based, end-to-end incident-to-release exercise for RxFlow.

System:
RxFlow validates optical prescriptions, calculates pricing, selects a capable laboratory, creates manufacturing jobs, consumes laboratory events, calculates throughput, and tracks orders to shipment.

Objective:
Investigate one production-style incident, reproduce it deterministically, implement the smallest safe correction, validate it, review it five times, and prepare a release-readiness package.

Incident:
An optician retries an order after a slow response. RxFlow creates two manufacturing jobs and reserves laboratory capacity twice.

Business contract:
- One idempotency key identifies one logical submission.
- Same key and equivalent payload return the original result.
- Same key with conflicting payload returns a structured conflict.
- Concurrent requests create at most one order, job, financial effect and capacity reservation.
- Patient identifiers and complete prescriptions never enter logs.

Permission boundary:
- Work only in the current repository.
- Read applicable AGENTS.md and CLAUDE.md files.
- Preserve unrelated changes.
- Do not deploy, merge, push or access production.
- Use synthetic data and fake credentials only.
- Ask before adding dependencies or starting containers.
- Use MCP only for authorized incident/runbook/log/metric retrieval.
- Never claim a check passed unless it ran successfully.

Phase 1 — Repository intelligence
1. Map services, modules, dependencies and ownership.
2. Locate APIs, domain services, PostgreSQL, Redis, Kafka, workers, analytics, migrations, security, logging, telemetry, tests and CI.
3. Find every HTTP, consumer, publisher, scheduled, batch, CLI and migration entry point.
4. Determine and safely verify real build/test commands.
5. Create or improve AGENTS.md and CLAUDE.md only when missing or materially inaccurate.
6. Produce a component table: responsibility, entry point, dependencies, storage, events, tests and risks.

Phase 2 — Incident investigation
1. Retrieve authorized incident and runbook evidence through MCP.
2. Build a UTC timeline with evidence and confidence.
3. Trace request → transaction → outbox → Kafka → worker → capacity reservation.
4. Identify where idempotency should be enforced.
5. Determine whether duplication occurs at request, commit, publication, consumption or reservation.
6. Separate primary root cause, contributing defect, secondary symptom and unrelated observation.
7. Do not change production code during this phase.

Phase 3 — Reproduction
Write a deterministic test using two equivalent, overlapping requests and one idempotency key. Expected: one order, one job and one outbox event. Use fixed synthetic data and concurrency primitives, not arbitrary sleeps. Run it before the patch and record command, exit code, expected value, actual value and assertion failure.

Phase 4 — Minimal patch
Implement only the evidence-justified correction. Prefer durable uniqueness or atomic Redis enforcement, transactional order/outbox persistence, payload-conflict detection, stable replay, idempotent consumption and atomic reservation. Avoid process-local locks, read-then-write races, infinite retries, broad refactoring and unreviewed public changes.

Required tests:
- regression;
- concurrent idempotency;
- conflicting payload;
- duplicate event delivery where relevant;
- structured error;
- sensitive-log redaction.

Required telemetry:
- idempotency hit;
- idempotency conflict;
- duplicate event;
- capacity reservation failure.

Phase 5 — Five-pass review

Pass 1: Correctness and concurrency
Review invariants, transactions, races, duplicates, partial failure and recovery.

Pass 2: Security and privacy
Review authentication, authorization, injection, secrets, patient data, prescriptions, logs, metrics and traces.

Pass 3: Performance and resilience
Review blocking, database/Redis calls, timeouts, bounded retries, retry amplification and queue behaviour.

Pass 4: Test quality and compatibility
Review deterministic assertions, negative cases, HTTP/event contracts, migration compatibility and consumer impact.

Pass 5: Release and operations
Review deployment order, migration reversibility, rollback, monitoring, limitations and ownership.

For every pass: list findings with severity and evidence; fix in-scope Critical/High issues; rerun affected checks; record residual risk.

Phase 6 — Automation
Discover commands from wrappers, build files, scripts and CI. Java examples: ./mvnw test, ./mvnw verify, ./gradlew test, ./gradlew check. .NET examples: dotnet restore, dotnet build --configuration Release, dotnet test --configuration Release, dotnet format --verify-no-changes. Also inspect static analysis, dependency audit, coverage, security checks and Docker Compose validation.

Create validated release JSON containing decision, findings, test counts, build/security status, compatibility, rollback readiness and evidence.

Phase 7 — Release readiness
Produce PR description, root-cause report, test evidence, security report, deployment plan, rollback plan, monitoring plan, known limitations and human approval checklist.

Monitor duplicate orders, idempotency conflicts, job creation, Redis/database failures, Kafka failures, worker retries, capacity drift, API latency and errors.

Final report:
EXECUTIVE SUMMARY / REPOSITORY INTELLIGENCE / INCIDENT TIMELINE /
REPRODUCTION / ROOT CAUSE / PRIMARY AND SECONDARY SYMPTOMS / MINIMAL PATCH /
CONCURRENCY AND IDEMPOTENCY / FIVE REVIEWS / FILES CHANGED /
COMMANDS AND RESULTS / CI ASSESSMENT / COMPATIBILITY / SECURITY /
DEPLOYMENT / ROLLBACK / MONITORING / LIMITATIONS / APPROVAL CHECKLIST /
GO, CONDITIONAL-GO OR NO-GO RECOMMENDATION.

Begin read-only. Do not change production code until the regression test has failed for the intended reason.
```

---

# Prompt 2 — Advanced

```text
TARGET_STACK = JAVA
ROLE = Principal Enterprise Architect, Incident Commander and Release Reviewer

Lead an advanced RxFlow incident-to-release investigation spanning order_api, routing, pricing, workers, Redis, Kafka and analytics.

Incident allegation:
After intermittent lab-connector timeouts, clients retry slow submissions, duplicate manufacturing requests appear, Redis in-flight capacity exceeds active jobs, retry volume rises sharply, throughput analytics becomes inconsistent and some logs contain complete prescription objects.

Do not assume one cause. Verify every allegation.

Permission boundary:
- Work only in the repository and read all governance files.
- Do not deploy, merge, push or access production.
- Use synthetic data and redact evidence.
- Ask before dependencies, containers or external actions.
- Public changes require compatibility review.
- Database changes require rollback.
- New external calls require timeout/retry analysis.
- Infrastructure changes require security review.
- Every defect fix requires failing-before/passing-after evidence.
- Record exact commands and results.

Roles when parallel agents are available:
- Principal architect: owns scope and decision.
- Incident investigator: MCP evidence and timeline.
- Concurrency specialist: transactions, Redis and duplicate delivery.
- Security reviewer: authorization, privacy, logs and secrets.
- Performance reviewer: retry amplification, timeouts and lag.
- Release reviewer: compatibility, deployment, rollback and monitoring.

Phase 1 — Repository intelligence
Map modules, entry points, schemas, Redis keys, Kafka topics/groups, lab connectors, security, telemetry, CI and quality gates. Verify builds, unit/integration tests, analysis, local startup and Compose validation. Improve AGENTS.md/CLAUDE.md when evidence supports it.

Phase 2 — Incident evidence
Retrieve authorized incident, runbook, sanitized logs, metrics, traces, deployment history and alert context through MCP. Normalize to UTC. Correlate request latency, retries, idempotency hits, inserts, outbox rows, Kafka messages, consumer attempts, connector calls, Redis mutations and analytics projections.

Classify triggering event, primary cause, contributing defects, secondary symptoms, independent security issues and operational gaps.

Phase 3 — Contracts
Document contracts for:
1. durable request idempotency and payload conflicts;
2. stable event identifiers and at-least-once consumption;
3. bounded total retry budget with no retry multiplication;
4. atomic capacity reservation/release and reconciliation;
5. duplicate/out-of-order-safe analytics and offline interval handling;
6. allow-listed, redacted logging and low-cardinality telemetry.

Phase 4 — Failing tests
Create deterministic tests for:
- concurrent duplicate order submission;
- duplicate Kafka delivery;
- connector timeout and retry count;
- concurrent capacity reservation;
- duplicate analytics event;
- sensitive-log redaction.

Use barriers, fixed clocks, Testcontainers when authorized and controlled connector fakes. Never rely on arbitrary sleeps. Record every pre-fix failure.

Phase 5 — Controlled patch
Present scope, files, contracts, migration impact, compatibility, security, tests and rollback. Select only evidence-justified mechanisms such as database uniqueness, transactional outbox, durable inbox, atomic Redis script, stable event ID, bounded retry budget, redaction allow-list or analytics deduplication.

Phase 6 — Observability
When justified, add low-cardinality metrics for idempotency, duplicates, connector attempts/timeouts, dead letters, reservation failures and late events. Propagate correlation across HTTP, outbox, Kafka and workers. Never attach sensitive domain payloads.

Phase 7 — Five reviews
1. Correctness/concurrency: invariants, transactions, races and recovery.
2. Security/privacy: authorization, injection, secrets, logs and telemetry.
3. Performance/resilience: attempts, timeout budgets, lag, queries and hot keys.
4. Tests/compatibility: determinism, negative paths and HTTP/event/database compatibility.
5. Release/operations: rollout, migration, rollback, reconciliation, monitoring and ownership.

Each reviewer provides severity, evidence, action, resolution and residual risk. Correct Critical/High in-scope findings and repeat affected checks.

Phase 8 — Automation
Run verified build, tests, concurrency tests, analysis, coverage, dependency audit, security scan, migrations, Compose validation and CI-equivalent commands. Run a configured non-interactive Codex assessment. Store machine-readable results with check name, exact command, passed/failed/blocked/not_run status and evidence.

Phase 9 — Release package
Produce PR, incident report, architecture/data flow, tests, concurrency evidence, security/privacy report, performance report, compatibility report, deployment sequence, rollback, monitoring, limitations and approvals.

Scorecard (100): repository intelligence 10; incident evidence 15; reproduction 15; root cause 15; patch 15; concurrency/idempotency 10; security 5; performance 5; automation 5; release readiness 5.

Decision:
- GO: 90+, no unresolved Critical/High, rollback and monitoring ready.
- CONDITIONAL-GO: 75–89, no Critical, named conditions and owners.
- NO-GO: below 75, failed core test, unsafe/unknown rollback, unresolved Critical or missing evidence.

Begin with repository intelligence and MCP retrieval. Do not edit production code before contracts and failing tests are recorded.
```

---

# Prompt 3 — Production

```text
TARGET_STACK = JAVA
ROLE = Principal Architect, Production Incident Commander and Release Authority Adviser

Lead an enterprise RxFlow production incident through verified release readiness. Advise the human release authority; never approve, merge or deploy the release yourself.

Incident allegations:
- order latency exceeded client timeout;
- clients retried;
- duplicate manufacturing jobs appeared;
- some labs exceeded capacity;
- Kafka lag increased;
- connector traffic amplified;
- analytics became inconsistent;
- logs exposed patient identifiers and prescriptions;
- a routing override may lack authorization.

Treat every statement as unverified until supported by evidence.

Enterprise gates:
- Traceable, redacted evidence.
- Regression test for every fixed defect.
- Idempotency proof for financial/order paths.
- Deployment and rollback for database changes.
- Compatibility review for public contracts.
- Timeout/retry/failure policy for external calls.
- Security validation for infrastructure.
- Exact command evidence.
- Explicit unknowns.
- Human approval before release.

Phase 0 — Governance
Read instruction hierarchy, establish scope and authorized systems, define incident window, evidence handling, redaction and approval boundaries, open an investigation log, and record repository state. Do not modify production, disable controls, expose secrets, replay production events or contact external parties.

Phase 1 — Architecture intelligence
Map client → gateway → order_api → validation → pricing → routing → database/outbox → Kafka → workers → lab connector → lifecycle events → analytics → shipping. Record owners, trust boundaries, data classification, auth, schemas, Redis structures, Kafka topology, retries, timeouts, DLQs, caches, telemetry, deployments and rollback mechanisms. Validate repository commands and governance documentation.

Phase 2 — MCP evidence
Retrieve authorized incident, runbook, alerts, sanitized logs, metrics, traces, deployment metadata, feature flags and dependency status. For every item record source, retrieval time, incident range, redaction, confidence and supported conclusion. Create a UTC timeline, correlation map, symptom graph and hypothesis register with supporting/contradicting evidence.

Phase 3 — Causal analysis
Test, do not assume, a chain such as missing idempotency → duplicate outbox/events → repeated worker/connector effects → retry amplification → capacity drift → overload/latency → more retries → analytics inconsistency. Classify primary cause, contributing defect, secondary symptom, independent security issue and operational gap.

Phase 4 — Formal contracts
Define invariants, boundaries, failures, observability, tests and owners for order idempotency, pricing, routing/capacity, outbox publication, event consumption, connector resilience, DLQ recovery, analytics, sensitive logging, override authorization, HTTP/events/database compatibility, zero-downtime rollout and rollback.

Phase 5 — Reproduction suite
Build deterministic tests for same-key sequential/concurrent/conflicting requests, durable idempotency, duplicate/out-of-order events, consumer restart, partial failure, concurrent capacity limits, duplicate release, Redis interruption, reconciliation, connector timeout/transient/permanent failure, retry exhaustion, circuit opening, analytics gaps/overlaps/late events/zero availability, unauthorized override and log redaction. Use fixed clocks, barriers and controlled fakes. Record failure before correction.

Phase 6 — Controlled implementation
Present scope, invariants, files, dependencies, storage/event impact, compatibility, security, performance, tests, rollout, rollback and monitoring. Apply the smallest cohesive mechanism justified by evidence. For migrations use expand-and-contract, forward/rollback instructions, lock/runtime analysis, mixed-version compatibility and validation/reconciliation queries. Version breaking contracts and provide consumer migration plans.

Phase 7 — Observability/privacy
Add only low-cardinality actionable metrics. Preserve tracing correlation without sensitive payloads. Use log allow-lists, explicit redaction and automated capture tests. Propose alerts with signal, threshold/anomaly, evaluation period, severity, owner, runbook and false-positive risk.

Phase 8 — Five mandatory independent reviews
1. Correctness/failure atomicity: invariants, transactions, partial failure, duplicates, concurrency, recovery and reconciliation.
2. Security/privacy: threat model, auth, authorization, injection, secrets, dependencies, auditability and data exposure.
3. Performance/scale/resilience: retry amplification, timeout budget, pools, lag, partitions, locks, hot keys, memory and load shedding.
4. Tests/compatibility: deterministic quality, negative cases, HTTP/event/database compatibility and rollback proof.
5. Production operations: rollout, migrations, flags, canary, rollback, repair, monitoring, on-call and approvals.

Where supported, use independent contexts/reviewers. Each finding includes severity, evidence, component, owner, blocking status, resolution and residual risk. Repeat reviews after material corrections.

Phase 9 — Automated assessment
Execute supported clean build, unit/integration/concurrency/contract tests, migrations, static analysis, format, coverage, dependency/security scans, container validation, performance smoke test, CI-equivalent pipeline and non-interactive assessment. Store test, coverage, security, migration, compatibility, performance, command transcript and release-risk artifacts. NOT RUN and BLOCKED are never PASSED.

Phase 10 — Deployment/rollback design
Deployment: prerequisites, approvals, artifact identity, configuration, migration/service order, flags, canary, smoke tests, observation, expansion/abort criteria and owners.

Rollback: code/config/flag rollback, database rollback or forward repair, Redis/Kafka reconciliation, duplicate-order repair, verification queries and owners. This prompt authorizes no deployment.

Phase 11 — Monitoring
Cover latency/errors, idempotency, duplicates, outbox/consumer lag, connector attempts per logical event, retries/timeouts, DLQ, capacity reconciliation/saturation, analytics late/duplicate events, sensitive-log detection and authorization failures. Define baseline, release threshold, alert, window, rollback trigger and owner.

Phase 12 — Ten-minute defence
- 0–1: system and incident.
- 1–3: evidence and causal chain.
- 3–5: reproduction and root cause.
- 5–7: minimal patch and concurrency/idempotency.
- 7–8: security/privacy/performance.
- 8–9: CI, rollout, rollback and monitoring.
- 9–10: scorecard, limitations and decision request.

Prepare answers about causality, concurrency, Redis failure, retry multiplication, mixed versions, migration rollback, log privacy, decision-changing checks and patch minimality.

Scorecard (10 points each): governance; evidence; causality; reproduction; patch; concurrency/idempotency; security/privacy; performance/resilience; automation; deployment/rollback/monitoring.

Decision gates:
- GO: 90+, zero Critical/High, core tests and compatibility acceptable, rollback/monitoring ready, approvals complete.
- CONDITIONAL-GO: 80–89, zero Critical, owned/time-bound conditions, viable rollback, human acceptance.
- NO-GO: below 80, release-blocking finding, failed core test, unknown compatibility, unsafe migration, unverified rollback/monitoring or missing approval.

Required release package:
Executive recommendation; PR; architecture; timeline; root cause; symptom classification; reproduction; patch; concurrency/idempotency; five reviews; security; performance; compatibility; commands; CI/machine results; deployment; rollback/data repair; monitoring; limitations; approval checklist; scorecard and recommendation.

Lead with evidence, preserve unknowns, justify scope, and never equate passing tests with production safety. End by requesting the human go/conditional-go/no-go decision.

Begin with governance and read-only repository intelligence.
```

## Expected outcomes

| Level | Expected result |
|---|---|
| Intermediate | One primary defect reproduced and fixed with regression, idempotency and release evidence |
| Advanced | Multiple symptoms separated with concurrency, retry and machine-readable assessment evidence |
| Production | Full governance, evidence chain, independent reviews, compatibility, operational release package and panel defence |

Run the selected prompt once with `TARGET_STACK = JAVA` and once with `TARGET_STACK = DOTNET` when both implementations are required.
