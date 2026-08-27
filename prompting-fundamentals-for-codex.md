# Prompting Fundamentals for Codex

This lesson progresses from a simple request to a controlled, evidence-based engineering task. The examples use the RxFlow prescription-to-lens platform.

## 1. Simple Prompt — Explain

Use this when you want a quick introduction to an unfamiliar part of the repository.

```text
Read-only: do not modify any files.

Explain how an optician's prescription submission moves through RxFlow, from the FastAPI endpoint to lab routing.

Identify the main files and functions involved. Cite each claim using file:line references.

Report as:
1. ENTRY POINT
2. REQUEST FLOW
3. KEY COMPONENTS
4. OPEN QUESTIONS
```

### What this teaches

- State the permission boundary.
- Give Codex a specific objective.
- Require repository evidence.
- Define the expected output.

## 2. Intermediate Prompt — Diagnose

Use this when a defect must be investigated and reproduced, but not fixed yet.

```text
Run read-only. Do not modify files, update dependencies, run migrations, or write the fix.

Context: RxFlow sometimes creates two lens jobs when an optician retries a slow prescription submission.

Steps, in order:
1. Locate the API endpoint and service-layer code that create a lens job. Cite file:line.
2. Trace the request through persistence, Redis, Celery, and Kafka where applicable.
3. Determine whether an idempotency key or equivalent uniqueness control exists.
4. Identify the concurrency window that could permit duplicate jobs.
5. Describe a minimal deterministic test that would reproduce the defect.
6. State the root cause, separating CONFIRMED findings from HYPOTHESES.

Report as:
ROOT CAUSE / EVIDENCE / REQUEST TRACE / REPRODUCTION / PROPOSED TEST / RISK / OPEN QUESTIONS

Do not implement a correction. I will review the diagnosis first.
```

### What this adds

- Supplies relevant system context.
- Orders the investigation.
- Defines prohibited actions explicitly.
- Separates evidence from inference.
- Prevents Codex from running ahead into implementation.

## 3. Advanced Prompt — Implement and Validate

Use this after the diagnosis and intended behaviour have been reviewed.

```text
Work only inside the RxFlow repository. You may modify application code and tests. Do not deploy, access production, expose secrets, change public APIs without approval, or run destructive database operations.

Task: prevent duplicate lens jobs when an optician retries a slow submission.

Required behaviour:
- The client supplies an idempotency key for a submission.
- Repeating the same request with the same key returns the original job and creates no additional database row, Celery task, or Kafka event.
- Concurrent requests using the same key produce exactly one job.
- Reusing a key with a materially different payload returns a clear client error.
- Existing clients without a key must retain the currently documented behaviour. If that behaviour is unclear, stop and report the ambiguity before changing the API contract.

Process:
1. Inspect the repository instructions and relevant implementation.
2. Summarize the confirmed current behaviour and propose a minimal plan.
3. Add deterministic failing regression tests first, including a concurrent-request case.
4. Implement the smallest safe correction at the authoritative persistence boundary.
5. Ensure transaction rollback cannot leave stale locks or idempotency records.
6. Verify that sensitive prescription or patient data is not added to logs.
7. Run focused tests, then the relevant lint, type, security, and regression checks documented by the repository.
8. Review the final diff for scope, correctness, compatibility, concurrency, and operational risk.

Stop and request approval before:
- adding a dependency;
- changing a public API contract;
- modifying a database schema without a downgrade plan;
- using network access; or
- changing files unrelated to this defect.

Final report:
SUMMARY
ROOT CAUSE
FILES CHANGED
TESTS ADDED
COMMANDS AND RESULTS
CONCURRENCY AND IDEMPOTENCY EVIDENCE
SECURITY AND COMPATIBILITY REVIEW
ROLLBACK PLAN
KNOWN LIMITATIONS

Do not claim success unless the reported validation commands passed. Clearly label anything not verified.
```

### What makes this advanced

- Defines permissions and approval checkpoints.
- Expresses the behavioural contract and edge cases.
- Requires tests before the implementation.
- Addresses concurrency, transactions, security, and compatibility.
- Fixes the validation and final-report contracts for unattended execution.

## Reusable Codex Prompt Pattern

Strong Codex prompts generally contain:

```text
PERMISSION BOUNDARY
CONTEXT
OBJECTIVE OR REQUIRED BEHAVIOUR
ORDERED STEPS
VALIDATION REQUIREMENTS
FIXED OUTPUT FORMAT
STOP CONDITIONS
```

The prompt should describe the outcome and constraints without guessing the implementation. Codex should inspect the repository and support its conclusions with evidence.
