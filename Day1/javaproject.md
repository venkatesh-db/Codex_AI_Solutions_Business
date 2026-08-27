# Hands-on Lab 9: X-ray the Java Analytics Service

```text
Act as a senior Java platform engineer performing an evidence-based technical investigation of an unfamiliar analytics service.

Project context:
This service processes RxFlow laboratory-throughput data. It consumes order, manufacturing, coating, completion, and shipment events; calculates lab capacity and performance metrics; stores aggregated results; and exposes those results to reporting or operational systems.

The repository has little or no reliable documentation. Your first responsibility is to determine how the service actually works from source code, configuration, tests, build files, migrations, and infrastructure definitions.

Technology assumptions:
- Java 21
- Spring Boot 3.x
- Spring Batch for ETL jobs
- Spring Data JPA and PostgreSQL
- Redis for caching and distributed coordination
- Kafka or Redpanda for event ingestion
- Apache Arrow, Tablesaw, or plain Java aggregation utilities
- Maven or Gradle
- Flyway or Liquibase
- JUnit 5, Mockito, Awaitility, and Testcontainers
- Micrometer and OpenTelemetry
- Docker Compose
- Spring Security
- SpotBugs, Checkstyle, JaCoCo, and OWASP dependency checks

Treat these as assumptions until verified from the repository.

Permission boundary:
- Work only inside the current repository.
- Begin with read-only investigation.
- Do not modify source code, configuration, dependencies, databases, or infrastructure unless explicitly requested after the investigation.
- Do not deploy the service.
- Do not access production systems.
- Do not use real patient or prescription data.
- Do not execute destructive database, Redis, Kafka, Git, Docker, or filesystem commands.
- Ask before downloading dependencies or starting containers.
- Never expose secrets, tokens, patient identifiers, or complete prescriptions in the report.

Primary objective:
Create an accurate technical map of the service based on repository evidence.

Investigate the following areas.

1. Repository instructions
- Locate and read all applicable AGENTS.md files.
- Explain which instructions apply at the repository and service-folder levels.
- Inspect README files, contribution guides, architecture documents, runbooks, and ADRs.
- Identify missing, outdated, contradictory, or unverified documentation.

2. Architecture
- Determine whether the system uses layered, hexagonal, clean, event-driven, batch-oriented, or another architecture.
- Identify major packages, modules, and their responsibilities.
- Locate domain models, application services, adapters, repositories, batch jobs, schedulers, event consumers, controllers, and shared utilities.
- Explain dependency direction.
- Identify business logic placed in inappropriate layers.

3. Entry points
Locate every verified entry point, including:
- Spring Boot application classes;
- REST controllers;
- Kafka listeners;
- Spring Batch jobs and steps;
- scheduled tasks;
- command-line runners;
- application runners;
- database migration entry points;
- maintenance or backfill commands.

For every entry point, provide:
- source file and symbol;
- trigger mechanism;
- input;
- primary processing path;
- output or side effect;
- error-handling behaviour.

4. End-to-end data flow
Trace at least one important workflow from input to final output.

Preferred workflow:
Lab manufacturing event
→ Kafka consumer or batch reader
→ parsing and validation
→ transformation
→ aggregation
→ PostgreSQL persistence
→ Redis cache update
→ metrics or reporting output.

Clearly distinguish:
- verified behaviour;
- reasonable inference;
- unknown behaviour.

Do not present assumptions as facts.

5. Build and test commands
Determine the actual build tool from repository evidence.

Inspect:
- pom.xml;
- build.gradle or build.gradle.kts;
- Maven wrapper;
- Gradle wrapper;
- Makefile;
- Dockerfiles;
- Docker Compose;
- CI workflows;
- shell scripts.

Discover and verify, where safe:
- compile command;
- unit-test command;
- integration-test command;
- complete verification command;
- application startup command;
- migration command;
- static-analysis command;
- coverage command.

Execute only safe commands that do not require external services first.

Before running a command:
- explain what it will do;
- explain whether it can modify repository or external state;
- identify required services.

Never claim that a command passed unless it was executed successfully. Record the exact command, exit status, and meaningful result.

6. Storage and schema
Identify:
- PostgreSQL databases and schemas;
- JPA entities;
- repositories;
- native SQL queries;
- Flyway or Liquibase migrations;
- transaction boundaries;
- read/write patterns;
- retention or cleanup jobs;
- indexes relevant to analytics workloads.

Review for:
- missing indexes;
- N+1 queries;
- unbounded result sets;
- unsafe native SQL;
- long transactions;
- non-reversible or risky migrations;
- timezone inconsistencies;
- duplicate event processing;
- incorrect aggregation keys.

7. Redis usage
Determine:
- key naming conventions;
- stored value types;
- TTL policies;
- cache invalidation behaviour;
- distributed locking;
- counters;
- atomicity guarantees;
- failure behaviour when Redis is unavailable.

Highlight:
- read-modify-write races;
- missing expiration;
- stale cache risk;
- high-cardinality keys;
- cache stampedes;
- unsafe serialization.

8. External integrations
Document all verified integrations, including:
- Kafka or Redpanda;
- upstream RxFlow services;
- reporting systems;
- HTTP APIs;
- object storage;
- monitoring backends;
- identity providers;
- email or notification systems.

For each integration, identify:
- adapter or client;
- configuration;
- authentication mechanism;
- timeout;
- retry policy;
- circuit breaker;
- fallback behaviour;
- observability;
- testing strategy.

9. Configuration
Map:
- application.yml and application.properties;
- profile-specific files;
- environment variables;
- Spring configuration classes;
- feature flags;
- secret references;
- Docker Compose configuration;
- CI configuration.

Create a configuration matrix with:
- setting;
- purpose;
- default;
- required or optional;
- sensitive or non-sensitive;
- environments where used;
- evidence location.

Do not print secret values.

10. Authentication and authorization
Determine:
- whether the service exposes APIs;
- authentication mechanism;
- authorization model;
- Spring Security configuration;
- role or scope checks;
- service-to-service authentication;
- actuator endpoint protection;
- administrative or backfill authorization;
- audit logging.

Identify endpoints or operations that appear insufficiently protected.

11. Error handling and resilience
Inspect:
- exception handlers;
- retry configuration;
- dead-letter topics;
- Kafka acknowledgment behaviour;
- batch skip and retry policies;
- transaction rollback behaviour;
- idempotency;
- duplicate-event handling;
- poison-message handling;
- timeout configuration;
- graceful shutdown.

Look specifically for retry amplification and infinite retry loops.

12. Observability
Identify:
- logging framework and configuration;
- structured logging;
- correlation and trace identifiers;
- Micrometer metrics;
- OpenTelemetry instrumentation;
- health and readiness checks;
- Spring Boot Actuator exposure;
- dashboards and alerts;
- batch-job metrics;
- Kafka consumer lag monitoring.

Check whether logs could expose:
- patient identifiers;
- prescription details;
- credentials;
- raw event payloads.

13. Testing strategy
Map:
- unit tests;
- slice tests;
- repository tests;
- batch-job tests;
- API tests;
- Kafka integration tests;
- Testcontainers usage;
- performance tests;
- security tests;
- CI quality gates.

Identify critical production paths with little or no coverage.

14. High-risk components
Rank risks using:
- Critical;
- High;
- Medium;
- Low.

Consider:
- incorrect throughput calculations;
- duplicate event consumption;
- non-idempotent aggregation;
- timezone and daylight-saving errors;
- concurrency defects;
- retry storms;
- data loss;
- stale cache results;
- SQL performance;
- migration safety;
- authorization gaps;
- privacy leakage;
- unbounded memory usage;
- loading entire datasets into memory;
- missing operational recovery procedures.

Every risk must include:
- severity;
- evidence;
- affected component;
- impact;
- confidence level;
- recommended validation or remediation.

Required working process:

Phase 1 — Repository reconnaissance
1. Read applicable AGENTS.md files.
2. Show a concise investigation plan.
3. Inventory top-level files and directories.
4. Identify the build system and service boundaries.
5. Locate documentation and infrastructure definitions.

Phase 2 — Static investigation
1. Map packages and components.
2. Identify entry points.
3. Trace data flow.
4. Map persistence, Redis, Kafka, configuration, security, and observability.
5. Inspect tests and CI.

Phase 3 — Verification
1. Propose safe build and test commands.
2. Run only commands permitted by the repository and permission boundary.
3. Capture exact evidence.
4. Label commands requiring unavailable services as unverified.
5. Do not repair discovered problems during this phase.

Phase 4 — Documentation
Produce the following deliverables:
- service architecture map;
- package responsibility map;
- entry-point inventory;
- end-to-end data-flow diagram;
- storage and cache map;
- integration inventory;
- configuration matrix;
- authentication and authorization summary;
- error-handling and resilience summary;
- observability summary;
- build and test command reference;
- ranked risk register;
- missing-documentation list;
- recommended next investigation steps.

Evidence rules:
- Cite repository-relative file paths and relevant class, method, or configuration names.
- Include line numbers when practical.
- Separate VERIFIED, INFERRED, and UNKNOWN findings.
- Do not invent commands, services, endpoints, schemas, or behaviour.
- If documentation conflicts with implementation, report both and treat executable code and configuration as stronger evidence.
- If a command cannot be executed, state the exact blocker.
- Do not say “production-ready,” “secure,” or “tests pass” without sufficient executed evidence.

Required diagrams:
Use Mermaid to provide:
1. component architecture;
2. entry-point and dependency flow;
3. end-to-end analytics data flow;
4. storage, cache, and external-integration map.

Final report format:

# EXECUTIVE SUMMARY

# VERIFIED TECHNOLOGY STACK

# REPOSITORY AND MODULE MAP

# ARCHITECTURE

# ENTRY POINTS

# END-TO-END DATA FLOW

# BUILD AND TEST COMMANDS

# STORAGE AND MIGRATIONS

# REDIS AND CACHE USAGE

# EXTERNAL INTEGRATIONS

# CONFIGURATION

# AUTHENTICATION AND AUTHORIZATION

# ERROR HANDLING AND RESILIENCE

# OBSERVABILITY

# TEST COVERAGE

# HIGH-RISK COMPONENTS

# MISSING OR OUTDATED DOCUMENTATION

# VERIFIED FACTS / INFERENCES / UNKNOWNS

# RECOMMENDED NEXT STEPS

# APPENDIX: COMMAND EVIDENCE

Success criteria:
A new engineer should be able to use the report to understand how the service starts, receives data, transforms it, stores results, handles failures, integrates with other systems, and how it can be built and tested safely.

Begin with repository reconnaissance. Do not implement fixes unless I explicitly request a second implementation phase.
```
