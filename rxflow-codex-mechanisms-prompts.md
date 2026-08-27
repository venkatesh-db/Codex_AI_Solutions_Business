# RxFlow Codex Mechanisms — Basic, Intermediate, and Advanced Prompts

These prompts use one project, RxFlow, to demonstrate the correct progression from a one-time prompt to durable repository guidance, reusable workflows, enforcement, external integrations, plugins, and automation.

## Mechanism Selection Guide

| Requirement | Correct mechanism |
|---|---|
| One-time constraint | Prompt |
| Repository standards | `AGENTS.md` |
| Reusable workflow | Skill |
| Lifecycle enforcement | Hook |
| External information or action | MCP |
| Command policy | Rule |
| Installable capability bundle | Plugin |
| Automated pipeline task | `codex exec` / GitHub Action |

## 1. Basic — Prompt Only

Use this for a one-time, bounded task.

```text
Work only inside the RxFlow repository.

Task:
Create a FastAPI endpoint named POST /orders.

The endpoint must:
1. Accept a prescription, frame choice, lens type, and idempotency key.
2. Reject sphere values outside -12.0 to +8.0.
3. Reject cylinder values outside -6.0 to 0.0.
4. Reject axis values outside 0 to 180.
5. Calculate a simple price.
6. Select a lab supporting the requested lens type.
7. Return the order ID, price, selected lab, and status.

Constraints:
- Use Python 3.11+, FastAPI, and Pydantic v2.
- Use in-memory storage.
- Keep business logic outside the API handler.
- Do not add dependencies without approval.
- Do not modify files unrelated to order creation.
- Do not log the prescription or optician identity.

Process:
1. Inspect the repository.
2. Show the proposed files and implementation plan.
3. Add tests before implementing the endpoint.
4. Implement the smallest working solution.
5. Run the tests.

Final report:
SUMMARY
FILES CHANGED
TESTS
COMMANDS AND RESULTS
KNOWN LIMITATIONS

Do not claim success unless the tests pass.
```

**Mechanism used:** Prompt

This is appropriate because the constraints apply only to one task.

## 2. Intermediate — Prompt + AGENTS.md + Skill + Hook + Rule

Use this when engineers and coding agents will repeatedly work in the repository.

```text
Act as a senior Python engineer working on RxFlow.

Goal:
Introduce durable repository standards and a reusable defect-resolution workflow.

Permission boundary:
Work only inside the current repository. You may create AGENTS.md files, one reusable skill, one validation hook, command-policy rules, tests, and documentation. Do not deploy or access external systems.

Part 1 — Repository standards using AGENTS.md

Create a repository-level AGENTS.md containing these durable rules:
- Every defect fix requires a regression test.
- Order-creation changes require idempotency tests.
- Database changes require upgrade, downgrade, and rollback instructions.
- Public API changes require compatibility review.
- External calls require timeouts and bounded retries.
- Sensitive prescription and optician data must never be logged.
- Final reports must include commands executed and their results.

Create src/rxflow/AGENTS.md containing source-specific rules:
- Keep business logic out of FastAPI handlers.
- Use integer cents for prices.
- Store timestamps in UTC.
- Persist an order and its outbox event atomically.
- Privileged operations require authorization and audit records.

Part 2 — Reusable workflow using a Skill

Create a reusable skill named rxflow-defect-fix.

The skill must:
1. Identify the affected component.
2. Reproduce the defect deterministically.
3. Separate confirmed evidence from hypotheses.
4. Add a failing regression test.
5. Implement the smallest safe correction.
6. Run focused tests.
7. Run Ruff and strict mypy.
8. Review security and API compatibility.
9. Produce a structured final report.
10. Stop if the defect cannot be reproduced.

Part 3 — Lifecycle enforcement using a Hook

Create a completion hook that blocks task completion when:
- Required tests fail.
- Ruff fails.
- Mypy fails.
- A changed migration lacks a downgrade.
- A defect fix has no regression test.

The hook must report the failed check and corrective action.

Part 4 — Command policy using Rules

Create command rules that:
- Allow known-safe test, lint, and type-check commands.
- Require approval for dependency installation and network access.
- Block destructive database commands.
- Block production deployment commands.
- Block force pushes and destructive Git operations.

Validation:
1. Demonstrate which instructions apply to a root file and a source file.
2. Exercise the skill on a small synthetic defect.
3. Demonstrate the hook blocking a deliberately failing validation.
4. Demonstrate one allowed and one blocked command rule.
5. Restore the repository to a passing state.

Final report:
AGENTS.MD HIERARCHY
SKILL CREATED
HOOK CREATED
RULES CREATED
VALIDATION EVIDENCE
COMMANDS AND RESULTS
RISKS
KNOWN LIMITATIONS

Do not weaken a check merely to make validation pass.
```

**Mechanisms used:**

- One-time task instructions: Prompt
- Durable repository guidance: `AGENTS.md`
- Repeatable defect workflow: Skill
- Mandatory completion checks: Hook
- Safe command enforcement: Rule

## 3. Advanced — Governed Enterprise Workflow

Use this to build a complete, automated engineering workflow around RxFlow.

```text
Act as the principal engineer responsible for a governed Codex workflow for RxFlow.

Objective:
Create a secure, repeatable production-readiness system that combines repository instructions, reusable skills, lifecycle hooks, MCP integrations, command rules, an installable plugin, and non-interactive CI automation.

Permission boundary:
- Work only inside the RxFlow repository.
- Investigation components must be read-only.
- Implementation components may write only inside the repository.
- Never access production systems.
- Never expose credentials, patient data, optician identities, or complete prescriptions.
- Request approval before enabling network access or adding dependencies.
- Do not grant an automated agent merge or deployment authority.

Phase 1 — Repository intelligence

Inspect the repository and report:
- Architecture and service boundaries
- API entry points
- Order, pricing, routing, and manufacturing flows
- PostgreSQL, Redis, Celery, and Kafka usage
- Authentication and authorization
- Configuration and secrets handling
- Logging, metrics, and tracing
- Verified build and test commands
- High-risk components
- Missing documentation

Support findings with file:line evidence.

Phase 2 — AGENTS.md hierarchy

Create or improve:
- Repository-level AGENTS.md
- API-specific AGENTS.md
- Worker-specific AGENTS.md
- Analytics-specific AGENTS.md
- Infrastructure-specific AGENTS.md

Use closest-file precedence.

Required standards:
- Idempotency tests for order-creation changes
- Rollback instructions for database changes
- Compatibility review for API and event changes
- Timeouts, retries, and circuit breakers for external calls
- No sensitive logging
- Security validation for infrastructure changes
- Regression tests for defect fixes
- Commands and results in final reports

Phase 3 — Reusable skills

Create and validate these skills:
1. rxflow-incident-investigation
2. rxflow-database-migration
3. rxflow-security-review
4. rxflow-release-readiness
5. rxflow-performance-analysis
6. rxflow-dependency-upgrade

Each skill must define:
- Trigger description
- Permission boundary
- Ordered workflow
- Required evidence
- Validation commands
- Stop conditions
- Fallback behavior
- Structured output contract

Phase 4 — Lifecycle hooks

Create hooks that:
- Prevent unsafe commands.
- Validate migrations before completion.
- Require regression tests for defect corrections.
- Run focused tests for changed components.
- Run API compatibility checks.
- Run Ruff, strict mypy, Bandit, and relevant tests.
- Block completion when mandatory validation fails.
- Produce privacy-safe audit events.

Hooks must be deterministic and fail closed.

Phase 5 — MCP integration

Design MCP access for:
- Issue tracker
- Internal runbooks
- Source repository
- Service catalogue
- Observability system
- Incident platform
- Deployment-status system

MCP tools must:
- Use least privilege.
- Validate inputs.
- Return structured results.
- Redact sensitive content.
- Distinguish read-only from mutating operations.
- Require approval for consequential actions.
- Never provide unattended production control.

If real systems are unavailable, implement synthetic local MCP fixtures and clearly label them.

Phase 6 — Command rules

Create rules that:
- Allow known-safe inspection and validation commands.
- Require approval for dependency installation and network access.
- Block destructive filesystem, Git, database, and infrastructure operations.
- Protect deployment and release commands.
- Prevent unattended production access.

Test every rule with allowed and denied examples.

Phase 7 — Installable plugin

Create an installable RxFlow engineering plugin bundling:
- The six skills
- MCP server configuration
- Hook configuration
- Command rules
- Supporting reference documents
- Validation scripts
- Plugin manifest and version metadata

Document installation, update, compatibility, and removal procedures.

Phase 8 — Automated pipeline

Create a read-only non-interactive assessment using codex exec or a GitHub Action.

The pipeline must:
1. Check out the pull request safely.
2. Run pytest, Ruff, strict mypy, Bandit, and dependency auditing.
3. Invoke Codex in read-only mode.
4. Review correctness, security, performance, test quality, and compatibility.
5. Retain the execution transcript.
6. Upload validation evidence as artifacts.
7. Fail the release decision on critical findings.
8. Request human review.
9. Prohibit automatic merging and production deployment.

Required machine-readable output:

{
  "release_status": "pass|conditional|fail",
  "critical_findings": [],
  "test_evidence": [],
  "security_findings": [],
  "rollback_readiness": "",
  "recommended_action": ""
}

The output must validate against JSON Schema.

Phase 9 — End-to-end validation

Use a synthetic RxFlow defect to verify:
- Repository instruction precedence
- Skill triggering
- MCP information retrieval
- Hook enforcement
- Command allow/block behavior
- Plugin installation
- Non-interactive pipeline output
- Transcript and artifact retention
- Human approval boundary

Final report:
EXECUTIVE SUMMARY
ARCHITECTURE
AGENTS.MD HIERARCHY
SKILLS
HOOKS
MCP TOOLS
COMMAND RULES
PLUGIN
AUTOMATION
VALIDATION EVIDENCE
SECURITY REVIEW
ROLLBACK PLAN
KNOWN LIMITATIONS
RELEASE VERDICT: PASS | CONDITIONAL | FAIL

Separate CONFIRMED evidence from HYPOTHESES.

Do not report PASS when:
- mandatory checks failed;
- required checks were not executed;
- security-critical behavior remains unverified;
- schema validation failed; or
- the automation can merge or deploy without human approval.
```

## Advanced Mechanism Mapping

| Requirement | Mechanism |
|---|---|
| Current implementation objective | Prompt |
| Durable engineering standards | `AGENTS.md` |
| Repeatable engineering workflows | Skill |
| Mandatory lifecycle checks | Hook |
| External systems and information | MCP |
| Command authorization policy | Rule |
| Installable reusable bundle | Plugin |
| Automated pull-request assessment | `codex exec` / GitHub Action |

