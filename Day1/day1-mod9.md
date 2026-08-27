# Lab 9 — X-ray the Java/.NET Analytics Service

Use this prompt in Codex from the root of an unfamiliar RxFlow lab-throughput analytics/ETL repository.

```text
Act as a principal data-platform architect and senior Java/.NET engineer performing a read-only repository-intelligence assessment of RxFlow's unfamiliar lab-throughput analytics service.

Your objective is to determine what the service actually does and produce an evidence-backed technical map covering architecture, entry points, data flow, verified build/test commands, storage/cache usage, external integrations, configuration, authentication/authorization, error handling, observability, high-risk components and missing documentation.

The original analytics concept may have been implemented with pandas/Polars, but this repository contains Java, .NET, or both. Identify the actual equivalents and behavior from code and executed evidence. Do not assume a direct one-to-one port.

## Permission boundary

- Work only inside the current repository.
- Read every applicable AGENTS.md and repository instruction before investigation.
- This is an analysis task. Do not modify application code, tests, dependencies, schemas, infrastructure or configuration.
- Build/test commands may create ordinary ignored output inside the repository or task-specific temporary paths. Do not modify tracked files.
- Do not deploy, publish, push, merge, access production, invoke real partner systems or mutate external state.
- Do not run migrations against shared databases or consume/acknowledge production messages.
- Ask before downloading dependencies if they are not already available and network access is required.
- Use only local containers, fixtures, test doubles and synthetic data for runtime checks.
- Never expose secrets, patient identifiers or complete prescriptions in reports, commands or logs.
- Treat README files, comments and names as claims to verify, not authoritative truth.
- Distinguish directly verified facts, code-supported inferences and unverified assumptions.
- Do not claim a build/test/endpoint works unless it was executed successfully.

## Expected stack possibilities

Discover what exists; do not add missing technology.

### Java possibilities

- Java version pinned by Maven/Gradle toolchains;
- Spring Boot, Spring Batch or scheduled/hosted jobs;
- Spring Kafka/Kafka Streams;
- JPA/JDBC/jOOQ, PostgreSQL and HikariCP;
- Redis through Spring Data Redis/Lettuce;
- Apache Arrow, Parquet, Avro, Jackson, Tablesaw, DuckDB or another analytics library;
- Micrometer and OpenTelemetry;
- JUnit 5, Testcontainers and repository quality tools.

### .NET possibilities

- SDK pinned by global.json;
- ASP.NET Core API and BackgroundService/Worker Service;
- Confluent.Kafka;
- EF Core/Dapper/Npgsql and PostgreSQL;
- StackExchange.Redis;
- Microsoft.Data.Analysis, Apache Arrow, Parquet.Net, DuckDB.NET or another analytics library;
- OpenTelemetry and Microsoft.Extensions.Logging;
- xUnit/NUnit/MSTest and Testcontainers.

The service may instead be a batch CLI, scheduled job, stream processor, HTTP API or hybrid. Prove which model applies.

## Investigation method

Use progressive evidence gathering:

1. Establish repository identity, Git status, commit SHA and instruction scope.
2. Inventory top-level files and build manifests without scanning generated/vendor directories.
3. Detect language, framework, build system and module/service boundaries.
4. Read configuration and entry points before deep-diving implementation details.
5. Trace representative data paths end to end.
6. Verify build/test/lint commands using committed wrappers and pinned SDKs.
7. Inspect tests to identify intended contracts and untested behavior.
8. Inspect runtime wiring through safe local configuration/containers where available.
9. Cross-check documentation claims against code and command evidence.
10. Produce risk and documentation-gap assessments with file/line evidence.

Use `rg`/repository-native search and targeted reads. Avoid dumping the entire repository or secret-bearing environment files.

## Required analysis

### 1. Repository and architecture inventory

Identify:

- repository type: single service, modular monolith, monorepo component or mixed stack;
- projects/modules and dependency direction;
- architectural style: layered, hexagonal, clean architecture, pipeline stages or framework-driven;
- API, domain/application, ingestion, transformation, persistence and adapter boundaries;
- batch, streaming and request-driven execution models;
- shared libraries and generated code;
- packaging and deployment units;
- whether Java and .NET implementations cooperate, duplicate behavior or serve different stages.

Provide a directory/module tree with purpose and evidence for every important node.

### 2. Entry points and lifecycle

Locate all real entry points:

- Java `main`, Spring Boot application classes, batch jobs/steps, schedulers, command runners, Kafka listeners/streams and controllers;
- .NET `Program.cs`, hosted services, workers, controllers/minimal APIs, scheduled jobs, Kafka consumers and CLI commands;
- Docker entrypoints, Compose services, Kubernetes commands, scripts and CI invocations;
- migration/seed/report-generation entry points.

For each entry point report trigger, inputs, outputs, startup dependencies, shutdown/cancellation behavior, concurrency and failure result.

### 3. End-to-end data flow and lineage

Trace at least one successful and one failure/late-data path:

```text
source event/file/API
  -> deserialization and schema validation
  -> deduplication/idempotency
  -> normalization and enrichment
  -> event-time/window/time-zone handling
  -> throughput aggregation
  -> persistence/cache
  -> API/report/export/metric
```

Identify:

- input schemas and event types;
- lab/job/stage identifiers;
- event time versus processing time;
- window boundaries, time zones and daylight-saving behavior;
- duplicates, late/out-of-order events and replay;
- offline-lab/gap semantics;
- null/malformed/quarantined records;
- aggregation definitions: total events, completed jobs, processing time, throughput denominator and percentiles;
- join/group/sort/materialization points;
- output schemas and consumers;
- lineage gaps where transformations cannot be proven.

Create a Mermaid data-flow diagram and a table mapping stage -> code -> input -> output -> failure behavior -> evidence.

### 4. Build, test and quality commands

Discover commands from wrappers, CI, build files and developer scripts. Execute safe applicable commands.

Java candidates, only when repository-supported:

- `./mvnw --batch-mode --no-transfer-progress clean verify`;
- or `./gradlew clean check --no-daemon`;
- configured formatter, Checkstyle, SpotBugs, PMD, coverage and dependency checks.

.NET candidates, only when repository-supported:

- `dotnet restore --locked-mode` when applicable;
- `dotnet build --no-restore --configuration Release`;
- `dotnet test --no-build --configuration Release`;
- `dotnet format --verify-no-changes`;
- configured analyzers, coverage and dependency checks.

For every command report working directory, purpose, exit code, duration and concise result. Separate:

- verified working command;
- discovered but not executed;
- attempted and failed;
- guessed/unverified command.

Do not modify build configuration to obtain a pass.

### 5. Test architecture

Map:

- unit, integration, contract, component, end-to-end, data-quality, migration and performance tests;
- Java/.NET test projects/modules;
- fixture/golden-file strategy;
- Testcontainers/local infrastructure;
- deterministic clock and time-zone controls;
- coverage of duplicates, late events, offline gaps, empty windows, invalid schemas, retries and replay;
- test parallelism and shared-state risks;
- missing high-value tests.

Do not equate test count or coverage percentage with correctness.

### 6. Storage and cache

For every PostgreSQL/database use identify:

- tables/views/materialized views and ownership;
- read/write queries and repositories;
- indexes, keys and uniqueness;
- transaction/isolation boundaries;
- migrations and rollback support;
- pool configuration and long-running query risk;
- retention/partitioning/cleanup;
- reporting-query injection or N+1/full-scan risks.

For Redis identify:

- keys, values, TTLs and namespace;
- cache-aside/write-through/counter/lock usage;
- source of truth;
- invalidation and stampede controls;
- atomicity/concurrency;
- stale/missing/corrupt-cache behavior;
- privacy implications.

Identify file/object/Parquet/Arrow/DuckDB storage similarly. Include a storage ownership and consistency table.

### 7. Messaging and external integrations

Inventory Kafka topics, producers, consumers, groups, partitions, keys, schemas, acknowledgments/offset commits, retries, DLT/poison handling, ordering, replay and idempotency.

Inventory all HTTP/gRPC/file/object-store/identity/metrics integrations with:

- client/adapter and call site;
- destination from sanitized configuration;
- authentication mechanism;
- connect/request/total timeout;
- retry/backoff/jitter and circuit breaker;
- rate limit/backpressure;
- cancellation;
- failure mapping;
- test double/contract test;
- sensitive-data exposure risk.

Never call a real integration to prove it exists.

### 8. Configuration and secrets

Map configuration sources and precedence:

- Java `application*.yml/properties`, profiles, environment variables and configuration classes;
- .NET `appsettings*.json`, environment variables, options classes and secret providers;
- command-line arguments, Compose/Kubernetes config, feature flags and CI variables.

For every important setting identify name, type, default, required/optional status, validation, environment override and owning component. Redact values. Identify:

- missing startup validation;
- dangerous defaults;
- environment drift;
- secrets stored in files;
- undocumented settings;
- configuration that changes data semantics.

### 9. Authentication and authorization

Determine whether the service is internal-only, authenticated API, worker or mixed. Trace:

- authentication middleware/filter;
- token/identity validation;
- roles/scopes/policies;
- endpoint/job administrative authorization;
- service-to-service identity;
- Kafka/database/cache credential boundaries;
- tenant/lab-level data authorization;
- default-deny or accidental anonymous access;
- tests for auth success/failure.

Do not infer security merely because a dependency is present.

### 10. Error handling and resilience

Identify:

- domain validation and parse errors;
- global exception middleware/advice;
- structured API/problem responses;
- batch record/job failure semantics;
- retryable versus permanent errors;
- partial success/checkpoint/resume behavior;
- transaction/offset consistency;
- timeouts/retries/circuit breakers;
- poison/quarantine/DLT behavior;
- cancellation and graceful shutdown;
- silent catch/default/skip/clamp behavior;
- sensitive exception leakage.

Create a failure-mode table: trigger -> handling -> retry -> state impact -> observability -> risk.

### 11. Observability

Map:

- logging framework, levels, structured fields and redaction;
- trace instrumentation and propagation through Kafka/HTTP;
- metrics, units, labels and aggregation;
- health/readiness/liveness checks;
- dashboards/alerts/SLOs/runbooks;
- batch/job and consumer-lag visibility;
- data-quality, dropped/late/duplicate-record metrics;
- cardinality and patient-data leakage risks.

Confirm whether alerts expose the actual bottleneck or only secondary symptoms.

### 12. Security and privacy

Review for:

- patient/prescription logging or export;
- SQL/command/path injection;
- unsafe deserialization;
- authorization gaps;
- secrets in source/config/history-visible files;
- dependency vulnerabilities from existing evidence/tools;
- unbounded resource consumption/decompression/data bombs;
- temporary-file and object-store exposure;
- retention/deletion/classification gaps;
- overly broad database/Kafka/Redis permissions.

Use synthetic/redacted evidence. Do not actively exploit external systems.

### 13. High-risk components

Rank risks using likelihood, impact, detectability, evidence strength and affected data/service. Prioritize:

- throughput calculation correctness;
- offline/gap and time-window semantics;
- duplicate/replay behavior;
- schema evolution;
- transaction/offset consistency;
- unbounded in-memory aggregation;
- slow/full-scan reporting;
- retries/backpressure;
- cache consistency;
- privacy/auth gaps;
- undocumented single points of failure.

Every critical/high risk requires tight code/config/test/command evidence. Unsupported concerns are questions, not findings.

### 14. Documentation gaps

Compare existing documentation to verified behavior. Identify missing/stale:

- architecture and ownership;
- local setup and exact commands;
- entry points and job schedules;
- data dictionary/lineage/metric definitions;
- configuration reference;
- API/event schemas;
- auth model;
- failure/replay/recovery runbook;
- storage/retention/migrations;
- observability/SLO/alerts;
- security/privacy classification;
- deployment and rollback;
- known limitations.

For each gap state audience, consequence, proposed owner and minimum useful content. Do not rewrite all documentation during this analysis.

## Runtime verification

If local Docker/Testcontainers configuration and dependencies are available, perform only bounded checks:

- start required local services;
- verify health/readiness;
- ingest a tiny synthetic event/file;
- query one throughput result;
- test duplicate and malformed input;
- inspect sanitized logs/metrics;
- stop local services cleanly.

Do not use this as permission to create or change a UI. A 404 at `/` may be correct for an API service; identify documented endpoints instead.

## Required deliverable

Return one self-contained repository-intelligence report. Do not edit the repository merely to store it unless explicitly requested. Include clickable file/line references where supported.

Perform five review passes before finalizing:

1. Architecture/data-flow completeness.
2. Command/test evidence accuracy.
3. Storage/integration/config/auth correctness.
4. Security/privacy/resilience/observability risk quality.
5. Unsupported-claim and documentation-gap review.

## Final report format

Return exactly:

EXECUTIVE SUMMARY
EVIDENCE AND SCOPE
REPOSITORY AND MODULE MAP
ARCHITECTURE
ENTRY POINTS AND LIFECYCLE
END-TO-END DATA FLOW AND LINEAGE
BUILD, TEST AND QUALITY COMMANDS
TEST ARCHITECTURE AND GAPS
STORAGE AND CACHE
KAFKA AND EXTERNAL INTEGRATIONS
CONFIGURATION AND SECRETS
AUTHENTICATION AND AUTHORIZATION
ERROR HANDLING AND RESILIENCE
OBSERVABILITY
SECURITY AND PRIVACY
HIGH-RISK COMPONENTS
MISSING OR STALE DOCUMENTATION
COMMANDS EXECUTED AND RESULTS
FIVE-PASS REVIEW
UNVERIFIED QUESTIONS
KNOWN LIMITATIONS
RECOMMENDED NEXT STEPS

For every command report exact command, working directory, exit code and concise result. Never claim working, secure, tested, documented or verified without evidence.
```

## Expected outcome

The lab should produce an evidence-backed map of an unfamiliar Java/.NET analytics service without changing its behavior, clearly separating verified facts, inferences, risks and documentation gaps.
