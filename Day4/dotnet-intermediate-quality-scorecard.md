# Lab 18 — .NET Intermediate Quality Scorecard

```text
Act as a senior .NET engineer building a working evaluation harness for AI-generated changes to RxFlow.

Technology:
-.NET 8, C# 12 and nullable reference types
- ASP.NET Core Minimal API or console application
- System.Text.Json
- xUnit and FluentAssertions when available
- OpenTelemetry for authorized aggregate signals
- GitHub Actions

Objective:
Implement a runnable evaluator that scores five recorded Codex or Claude runs against one golden task. Command success alone must never be treated as proof of patch correctness.

Safety:
Read AGENTS.md/CLAUDE.md; work only in the repository; preserve unrelated changes; do not deploy or access production; use synthetic data; ask before packages/containers; redact prompts, code, secrets, patient data and prescriptions; report only executed evidence.

Golden use case:
RxFlow throughput is incorrect for a laboratory offline during part of the day. The expected correction clips and merges offline intervals, uses available operating hours, adds deterministic regression tests, preserves API compatibility and changes no prohibited files.

Create versioned models and JSON schemas for:
- GoldenTask: defect, contract, mandatory tests, allowed/prohibited paths, security traps, performance budget, diff budget and evidence requirements.
- RecordedRun: model/configuration, prompt variant, diff, commands, tests, findings, compatibility, performance, approvals, telemetry, claims, human acceptance and rollback.
- EvaluationResult: dimensions, weighted score, gates, decision, evidence and limitations.

Score weights:
Functional correctness 25; test quality 20; security 15; scope adherence 15; maintainability 10; evidence quality 10; efficiency 5.

Implement:
1. Strongly typed immutable records.
2. An EvaluationService with transparent formulas and explanations.
3. Hard NO_GO gates for build failure, mandatory-test failure, Critical security issue, prohibited change or missing required rollback.
4. GO >= 85 with gates satisfied; CONDITIONAL_GO 70–84; otherwise NO_GO.
5. Detection of unsupported claims by matching claims with CommandEvidence and exit codes.
6. Diff-scope and size checks.
7. CLI or Minimal API accepting five JSON runs and emitting JSON and Markdown.
8. xUnit tests for weighting, hard gates, unsupported claims, security, scope and a perfect run.
9. Synthetic fixtures and README.

Observability:
Use OpenTelemetry-compatible counters/histograms for evaluation duration, failed tools, approvals, MCP latency, test failures, repeated investigations, unsupported claims, diff size, score and decision. Use low-cardinality attributes only. Add a test proving sensitive fields are not exported.

Comparison report:
Compare weak/structured task; instructions/none; read-only/implementation; single/specialist; reasoning levels; skill/unstructured; Claude/Codex. Do not invent absent data.

Verification:
Discover actual commands. Typical commands are dotnet restore, dotnet build --configuration Release, dotnet test --configuration Release, dotnet format --verify-no-changes. Record exact exit status and result.

Final report:
SUMMARY / ARCHITECTURE / GOLDEN TASK / SCORING / HARD GATES / FIVE RUNS /
COMPARISONS / FILES / TESTS / COMMAND EVIDENCE / TELEMETRY SECURITY /
GOVERNANCE / LIMITATIONS.

Begin with read-only repository inspection and build working code rather than a conceptual document.
```
