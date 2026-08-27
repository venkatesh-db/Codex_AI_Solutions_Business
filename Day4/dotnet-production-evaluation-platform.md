# Module 22 — .NET Production Evaluation Platform

```text
Act as a principal .NET architect building a production-grade AI engineering evaluation, observability and governance platform for RxFlow.

Stack:
.NET 8; C# 12; ASP.NET Core; EF Core/PostgreSQL; EF migrations; Kafka/Redpanda; Redis only where justified; OpenTelemetry; ILogger; OAuth2/JWT; xUnit; FluentAssertions; Testcontainers; Docker Compose; GitHub Actions.

Mission:
Build an auditable service that versions golden tasks, ingests immutable run evidence, scores AI code changes, applies hard governance gates, compares controlled experiments, emits redacted telemetry and records human release decisions. A successful command is evidence of execution, never sufficient proof of correctness.

Controls:
Follow AGENTS.md/CLAUDE.md; do not deploy; use managed model/capability/MCP policy; require human approval for release and exceptions; never retain source, raw prompts, secrets, patient/prescription data or unrestricted tool output without authorization; document classification, encryption, access, audit, retention and deletion; require rollback for migrations and compatibility review for public contracts.

Projects/components:
- Domain: GoldenTask, RecordedRun, Evidence, Finding, Policy, Scorecard, Approval.
- Application: ingestion, scoring, gates, comparison, retention and reporting.
- API: authenticated run/task/review endpoints.
- Infrastructure: EF Core, telemetry, optional messaging and evidence references.
- Worker: non-interactive evaluation and golden-task replay.
- Tests: unit, integration, authorization, compatibility, concurrency, performance and redaction.

Golden tasks are version-controlled and include known defect, behavioural contract, expected patch, mandatory tests, prohibited changes, security traps, performance/diff budgets, rollback requirements and evaluator version. Re-run tasks when models, prompts, reasoning, skills/plugins, repository instructions or evaluator logic change.

Score:
correctness 25; tests 20; security 15; scope 15; maintainability 10; evidence 10; efficiency 5.

Hard NO_GO gates:
build failure; mandatory regression failure; Critical security/privacy finding; prohibited change; unauthorized breaking HTTP/event change; unacceptable performance regression; unsupported release claim; missing migration rollback/repair; sensitive telemetry leakage. Numeric score cannot bypass a gate.

APIs:
- version/query golden tasks;
- submit run and evidence metadata idempotently;
- evaluate/re-evaluate with pinned policy version;
- compare cohorts;
- obtain JSON/Markdown scorecard;
- record human review and approval audit;
- request sanitized evidence export.

Implement idempotency for run submission, optimistic concurrency for policy/task versions, structured ProblemDetails, cancellation propagation, payload limits and stable API schemas.

Security:
Roles/policies for admin, submitter, reviewer and auditor; negative authorization tests; input validation; injection protection; telemetry allow-list; secret/header redaction; immutable approval audit; data retention and deletion tests.

Observability:
OpenTelemetry traces/metrics for run ingestion, evaluation, summarized tool events, approval decisions, MCP/hook duration, failures and latency. Token/cost values are authorized aggregates only. Dashboards cover build/tests, scores, gates, security, compatibility, performance, diff size, claims, false positives, acceptance, approvals, MCP latency and cost.

Experiments:
Support paired weak/structured, instructions/none, read-only/implementation, single/specialist, reasoning configurations, skill/unstructured and Claude/Codex comparisons. Pin task/evaluator versions, record sample size/confounders and avoid unsupported superiority claims.

Persistence and operations:
Use immutable evidence metadata, versioned task/policy snapshots and approval audit. Add EF migrations with generated SQL review, rollback/forward repair, indexes and mixed-version compatibility. Do not store unnecessary content.

Automation:
CI runs restore, Release build, tests, analyzers, format verification, coverage, dependency/security checks, migration/schema validation, golden tasks, API compatibility and performance smoke. Export machine-readable sanitized evidence; never deploy.

Five reviews:
1. evaluation correctness/determinism;
2. threat/privacy/auth review;
3. performance/resilience/scale;
4. tests/contracts/migrations;
5. governance/deployment/rollback/monitoring.

Deliver working solution, migrations, JSON schemas, five synthetic runs, tests, Compose, CI, OpenTelemetry configuration, dashboards/runbooks, threat model, retention policy, evaluation reports, exact commands/results, deployment/rollback plans, limitations and human approval checklist.

Decision: GO >= 85 and all gates pass; CONDITIONAL_GO 70–84 with no Critical and owned conditions; otherwise NO_GO.

Begin with repository reconnaissance and an explicit architecture, evidence and governance contract. Implement in small stages and verify every claim.
```
