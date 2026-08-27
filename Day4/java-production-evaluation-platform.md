# Module 22 — Java Production Evaluation Platform

```text
Act as a principal Java architect building a production-grade AI engineering evaluation, observability and governance platform for RxFlow.

Stack:
Java 21; Spring Boot 3; PostgreSQL/JPA; Flyway; Kafka/Redpanda; Redis where justified; OpenTelemetry; Micrometer; OAuth2/JWT; JUnit 5; AssertJ; Testcontainers; Maven/Gradle; Docker Compose; GitHub Actions.

Mission:
Create an auditable platform that stores golden tasks and evaluation runs, ingests machine evidence, applies weighted scoring plus non-negotiable gates, compares model/workflow configurations, emits privacy-safe telemetry and produces human approval decisions. It must evaluate code changes, not merely command exit status.

Governance boundary:
- Repository instructions apply; no production deployment.
- Approved models/capabilities and MCP allowlists are configuration, not hard-coded bypasses.
- Human approval is mandatory for releases, destructive actions and policy exceptions.
- Encrypt or omit sensitive data; never store source patches, raw prompts, secrets, patient data or complete tool output unless explicitly authorized.
- Define retention, access, audit and deletion policies.
- Database changes require reversible/forward-repair instructions.
- Public contracts require compatibility review.
- Every defect fix requires a regression test.

Architecture modules:
1. golden-task registry and versioning;
2. run/evidence ingestion API;
3. deterministic scoring engine;
4. policy and hard-gate engine;
5. comparison experiment service;
6. telemetry ingestion/aggregation;
7. dashboard/report projections;
8. human review and approval audit;
9. retention/redaction service;
10. non-interactive CI evaluator.

Golden task requires known defect, expected contract/patch, mandatory tests, prohibited changes, security traps, performance budget, diff budget, rollback requirement and evaluation-version pinning. Changing model, prompt, skill, plugin, AGENTS.md, CLAUDE.md or evaluator version must trigger rerunning applicable golden tasks.

Score weights:
correctness 25; tests 20; security 15; scope 15; maintainability 10; evidence 10; efficiency 5.

Hard gates:
- unbuildable patch;
- failed mandatory regression;
- Critical security/privacy issue;
- prohibited file or behaviour;
- breaking API/event change without approval/versioning;
- performance budget violation beyond configured tolerance;
- unsupported release claim;
- missing database rollback/repair plan;
- telemetry sensitive-data leak.

Implement transparent scoring with per-rule evidence, evaluator version and reproducible result. Never allow numeric score to override a hard gate.

Required APIs:
- create/version/list golden tasks;
- submit immutable recorded run and evidence references;
- evaluate/re-evaluate run;
- compare experiment cohorts;
- retrieve scorecard and decision;
- submit human review/approval with actor, time and rationale;
- export machine JSON and Markdown release evidence.

Security:
Policy-based authorization for evaluator admin, run submitter, reviewer and auditor. Validate payload size/type, prevent injection, use allow-listed telemetry fields, audit policy changes and approvals, never log tokens/raw evidence, and test authorization/redaction.

Observability:
Trace conversation-start/run ingestion, tool execution summaries, approvals, MCP/hook durations, failures and evaluator latency. Record token/cost only as authorized numeric aggregates. Define retention and access. Provide dashboard definitions for pass rate, score distribution, hard gates, tool failures, approvals, MCP latency, unsupported claims, diff size, human acceptance and cost.

Comparison experiments:
Support controlled weak/structured, instructions/none, review/implementation, single/specialists, reasoning effort, skills, and Claude/Codex cohorts. Pin golden-task/evaluator versions and report sample size; do not imply statistical significance without sufficient data.

Persistence:
Use immutable run/evidence records, versioned tasks/policies, approval audit and reproducible evaluation snapshots. Add Flyway upgrade plus rollback/forward-repair and indexes. Avoid storing unnecessary content.

Automation:
GitHub Actions must build, test, analyze, validate migrations/schemas, run golden tasks, emit machine JSON and store sanitized evidence. It must not deploy. Add concurrency, API compatibility, security, performance-smoke and redaction tests.

Five reviews:
1. scoring correctness and determinism;
2. security/privacy/authorization;
3. performance/scalability/resilience;
4. tests/API/database compatibility;
5. governance/release/rollback/monitoring.
Resolve release-blocking findings and rerun affected checks.

Deliver:
working code; migrations; schemas; synthetic golden tasks/five runs; tests; Compose; CI; OpenTelemetry configuration; dashboard/runbook docs; threat model; retention policy; scorecard; commands/results; deployment and rollback plan; limitations; human checklist.

Final decision uses GO >= 85 with all gates satisfied, CONDITIONAL_GO 70–84 with owned conditions and no Critical finding, otherwise NO_GO.

Begin read-only, present architecture and policy contract, then implement in reviewable stages with executed evidence.
```
