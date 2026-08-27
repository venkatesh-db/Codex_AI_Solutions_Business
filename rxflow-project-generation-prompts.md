# RxFlow Project Generation Prompts

These prompts progressively increase the scope, engineering discipline, and validation required from Codex.

## 1. Simple Level — Generate a Minimal RxFlow Prototype

```text
Create a small Python project named RxFlow.

Purpose:
An optician submits a prescription and frame choice. The system validates the prescription, calculates a basic price, selects a suitable lens lab, and returns an order confirmation.

Use:
- Python 3.11+
- FastAPI
- Pydantic v2
- pytest
- In-memory storage only

Implement:
1. POST /orders endpoint.
2. Prescription, frame, order, and lab models.
3. Basic prescription range validation.
4. A simple pricing service.
5. Lab routing based on supported lens type.
6. Unit tests for one successful and two invalid submissions.
7. A README with setup and run commands.

Keep the design small and easy to understand. Separate API, domain models, and services into different modules.

Before editing, show the proposed file structure. Then create the files and run the tests.

Final report:
- files created;
- implemented behaviour;
- commands executed and results;
- known limitations.
```

## 2. Intermediate Level — Generate a Working RxFlow Service

```text
Build a working Python service named RxFlow for prescription-to-lens order processing and lab routing.

Permission boundary:
Work only inside the current repository. You may create application code, tests, configuration, migrations, and local Docker resources. Do not deploy anything, access production systems, or use real patient data. Ask before adding a dependency not listed below.

Business flow:
1. An optician submits a prescription, frame selection, and idempotency key.
2. RxFlow validates whether the prescription can be manufactured.
3. It calculates the order price.
4. It selects a capable lab using configured priority and available capacity.
5. It persists the order and publishes a manufacturing request.
6. The client can retrieve order status.

Technology:
- Python 3.11+
- FastAPI and Pydantic v2
- SQLAlchemy 2.x and PostgreSQL
- Alembic
- Redis for capacity and idempotency support
- Celery for background manufacturing tasks
- pytest and pytest-asyncio
- Docker Compose
- ruff and mypy --strict

Required endpoints:
- POST /orders
- GET /orders/{order_id}
- GET /labs
- PATCH /orders/{order_id}/status

Engineering requirements:
- Use a service layer; keep business logic out of API handlers.
- Reject invalid prescriptions instead of silently correcting them.
- Enforce idempotent order submission.
- Use atomic Redis operations for in-flight counters.
- Do not log patient identifiers or complete prescriptions.
- Return consistent structured errors.
- Add an Alembic upgrade and downgrade migration.
- Add unit and API integration tests for core behaviour.
- Document configuration using an example environment file with fake values only.

Process:
1. Inspect the repository and any AGENTS.md instructions.
2. Present the architecture, assumptions, and implementation plan.
3. Create the project in small, reviewable stages.
4. Run focused tests after each stage.
5. Run the complete test suite, ruff, and mypy at the end.
6. Review the final diff for correctness, security, and unnecessary scope.

Final report:
SUMMARY / ARCHITECTURE / FILES CHANGED / TEST EVIDENCE / QUALITY CHECKS / SECURITY NOTES / KNOWN LIMITATIONS / NEXT STEPS

Do not claim a check passed unless you executed it successfully. Label unverified behaviour clearly.
```

## 3. Advanced Level — Generate the Production-Style RxFlow Training Platform

```text
Act as a senior software engineer and build RxFlow, a production-style prescription-to-lens order and lab-routing platform used for an enterprise engineering course.

Permission and safety boundary:
- Work only inside the current repository.
- You may create and modify source code, tests, migrations, local infrastructure, CI configuration, documentation, and synthetic fixtures.
- Never use real patient, customer, credential, or production data.
- Do not deploy, contact production services, or perform destructive database operations.
- Request approval before adding unlisted dependencies, changing a public contract after it has been established, or enabling network access.

Product workflow:
An optician submits a prescription and frame choice. RxFlow validates physical manufacturability, prices the order, routes it to an Rx lab using capability and live load, schedules surfacing and coating, emits lifecycle events, and tracks the job through shipment.

Required stack:
- API: FastAPI and Pydantic v2
- Domain: plain Python service layer
- Data: PostgreSQL, SQLAlchemy 2.x, Alembic
- Cache and distributed coordination: Redis
- Async processing: Celery
- Events: Kafka, using Redpanda locally
- Analytics: pandas or Polars ETL
- Tests: pytest, pytest-asyncio, Hypothesis, testcontainers, and Locust
- Quality: ruff, mypy --strict, bandit, and pip-audit
- Operations: Docker Compose, GitHub Actions, and OpenTelemetry

Functional requirements:
1. Accept an order containing optician identity, prescription, frame, requested lens options, and idempotency key.
2. Validate sphere, cylinder, axis, prism, material, frame, coating, and lab capability constraints. Reject invalid data; never clamp it silently.
3. Calculate an itemized price using one authoritative pricing policy.
4. Select a healthy, capable lab using live capacity, current load, estimated completion time, and deterministic tie-breaking.
5. Persist the order and publish downstream work exactly once from the user's perspective.
6. Schedule surfacing and coating through retry-safe Celery tasks.
7. Maintain Redis in-flight counters atomically and reconcile them against durable state.
8. Publish versioned Kafka events using an outbox or an equivalently reliable pattern.
9. Track status transitions through accepted, validated, routed, surfacing, coating, quality-check, shipped, completed, and failed.
10. Support an authorized lab override with an audit trail.
11. Produce a daily lab-throughput report that handles offline periods, gaps, nulls, late events, and timezone boundaries correctly.
12. Expose health, readiness, and metrics endpoints without leaking sensitive information.

Security and reliability requirements:
- Apply authentication and role-based authorization to privileged operations.
- Never log patient identifiers or full prescriptions.
- Use parameterized database queries only.
- Enforce idempotency under concurrent duplicate requests.
- Configure bounded retries with exponential backoff and jitter.
- Use timeouts and a circuit breaker for lab connectors.
- Apply rate limiting and backpressure before downstream capacity is exhausted.
- Provide upgrade and downgrade paths for every migration.
- Protect secrets through environment-based configuration and fake local examples.
- Add correlation identifiers and privacy-safe structured logging.
- Instrument API, database, Redis, Celery, Kafka, and lab-connector operations with OpenTelemetry.

Repository deliverables:
- Service source code with clear module boundaries.
- Repository-level and component-level AGENTS.md guidance.
- Alembic migrations.
- Docker Compose for PostgreSQL, Redis, Redpanda, API, and workers.
- Synthetic seed data and deterministic fixtures.
- Unit, integration, contract, concurrency, property-based, fault-injection, and minimal load tests.
- GitHub Actions pipeline running tests and quality/security checks.
- Architecture, local setup, API, operations, troubleshooting, deployment, rollback, and threat-model documentation.
- A machine-readable release-risk report schema.

Implementation process:
1. Inspect the workspace and repository instructions.
2. State assumptions and unresolved product decisions.
3. Propose the architecture, dependency boundaries, data model, API contracts, event contracts, and failure model.
4. Create a phased plan with explicit validation checkpoints.
5. Implement a thin vertical slice first: submit, validate, persist, route, and retrieve an order.
6. Add each distributed component only with tests for its failure modes.
7. For every defect fixed during development, add a regression test first.
8. Run focused validation after each phase.
9. Run the complete test, type, lint, security, migration, and local integration checks.
10. Review the final diff for scope, correctness, concurrency, idempotency, privacy, API compatibility, operability, and rollback readiness.

Stop and report instead of guessing when:
- a business rule materially affects prescription safety or pricing;
- credentials or external infrastructure are required;
- an irreversible migration appears necessary;
- repository instructions conflict;
- a required validation cannot run in the available environment.

Final output contract:
EXECUTIVE SUMMARY
ARCHITECTURE
BUSINESS RULES AND ASSUMPTIONS
API AND EVENT CONTRACTS
DATA AND CONCURRENCY DESIGN
SECURITY AND PRIVACY CONTROLS
FILES CREATED OR CHANGED
TESTS AND FAULT SCENARIOS
COMMANDS EXECUTED WITH RESULTS
MIGRATION AND ROLLBACK PLAN
OBSERVABILITY AND OPERATIONS
KNOWN LIMITATIONS
RELEASE VERDICT: PASS | CONDITIONAL | FAIL

Do not report PASS if mandatory tests or security checks failed or were not executed. Separate confirmed evidence from hypotheses and unverified assumptions.
```

## Progression

- **Simple:** teaches project structure and basic Codex instructions.
- **Intermediate:** adds persistence, async work, idempotency, migrations, and quality gates.
- **Advanced:** defines a distributed, secure, observable, testable, and governed enterprise platform.

