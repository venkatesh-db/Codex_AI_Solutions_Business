# Lab 18 — Java Intermediate Quality Scorecard

```text
Act as a senior Java engineer building a working evaluation harness for AI-generated changes to RxFlow.

Technology:
- Java 21 and Spring Boot 3.x
- Maven Wrapper or Gradle Wrapper
- JUnit 5 and AssertJ
- Jackson for JSON evidence
- Micrometer and OpenTelemetry when already available
- PostgreSQL/Testcontainers only when the repository already uses them
- GitHub Actions

Objective:
Create a small, runnable codebase that evaluates five recorded Codex or Claude task runs against a golden-task definition. A zero exit code is evidence only for command execution; it is not proof of correctness.

Permission boundary:
- Work only in the repository and read AGENTS.md/CLAUDE.md first.
- Do not deploy or access production.
- Use synthetic evaluation data only.
- Do not add dependencies or start containers without approval.
- Never expose prompts, secrets, patient data or prescription values in telemetry.
- Do not claim a check passed unless it ran successfully.

Golden task model:
Create a version-controlled golden task containing:
- task ID and description;
- seeded defect;
- expected behavioural contract;
- expected patch characteristics;
- mandatory regression tests;
- prohibited files/changes;
- security traps;
- performance constraint;
- maximum recommended diff size;
- required evidence.

Use case:
RxFlow throughput is wrong when a lab is offline for part of a report window. The expected patch must clip and merge offline intervals, subtract their union from available time, add deterministic tests, avoid public API changes and avoid sensitive logging.

Recorded-run input:
Represent five synthetic or supplied runs. Each run records:
- model and reasoning configuration;
- prompt/instruction variant;
- changed files and line counts;
- commands and exit codes;
- focused/full test results;
- security findings;
- API compatibility result;
- performance result;
- unsupported claims;
- approval requests;
- MCP/tool timings;
- human acceptance result;
- rollback evidence.

Evaluation dimensions:
- build success;
- focused test pass rate;
- full regression pass rate;
- patch correctness;
- scope adherence;
- security findings;
- API compatibility;
- performance regression;
- diff size;
- unsupported claims;
- review false positives;
- human acceptance;
- rollback readiness.

Weighted score:
- Functional correctness: 25
- Test quality: 20
- Security: 15
- Scope adherence: 15
- Maintainability: 10
- Evidence quality: 10
- Efficiency: 5

Implement:
1. Immutable Java records for GoldenTask, RecordedRun, CommandEvidence, Finding and Scorecard.
2. A scoring service that returns every dimension, weighted total from 0–100, deductions and explanations.
3. Hard gates: build failure, failing mandatory regression test, Critical security issue, prohibited change or missing rollback can force NO_GO regardless of numeric score.
4. Decision: GO >= 85 with no hard-gate failure; CONDITIONAL_GO 70–84; otherwise NO_GO.
5. Unsupported claims are detected when a run claims a check passed without matching successful command evidence.
6. Scope evaluation compares changed files with allowed and prohibited paths.
7. A CLI or API that evaluates five JSON run files and emits JSON plus a readable Markdown report.
8. JUnit tests for perfect run, failed build, prohibited change, unsupported claim, Critical security issue and weighting math.
9. Fake example data only.
10. README with build, test and evaluation commands.

Observability:
Emit low-cardinality telemetry for evaluation start/end, tool failure count, approval count, MCP latency, test failures, unsupported-claim count, diff size, score and decision. Redact content; do not record source, prompts, command output, tokens, secrets or domain payloads as attributes.

Comparisons:
Generate a comparison report for weak vs structured prompt, instructions vs none, read-only vs implementation, single vs specialist review, reasoning configurations, skill vs unstructured, and Claude vs Codex when equivalent run data exists. Mark missing comparisons NOT_AVAILABLE.

Process:
1. Inspect repository and verified toolchain.
2. Present architecture and file plan.
3. Add golden-task schema and fixtures.
4. Implement scorer with unit tests.
5. Add CLI/API and reports.
6. Add telemetry and redaction tests.
7. Run focused tests, full tests, build and formatting/static checks.
8. Review final diff for scope and unsupported claims.

Final report:
SUMMARY / ARCHITECTURE / GOLDEN TASK / SCORING CONTRACT / FILES CHANGED /
TEST EVIDENCE / COMMANDS AND RESULTS / FIVE-RUN SCORECARD / COMPARISONS /
TELEMETRY AND REDACTION / GOVERNANCE / LIMITATIONS.

Begin with read-only inspection. Produce a working implementation, not only documentation.
```
