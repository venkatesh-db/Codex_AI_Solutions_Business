# Hands-on Lab 17 — Production Engineering Task Console

```text
Act as a principal TypeScript platform architect productionizing the prepared RxFlow engineering-task console.

The starter code is provided. Extend it into a secure internal engineering client; do not replace it or build an enterprise design system from scratch.

Choose the integration from evidence:
- Codex SDK for server-side application-controlled coding threads.
- App Server when the client needs low-level JSONL/JSON-RPC initialization, thread/turn lifecycle, rich streaming events and approval exchange.
- codex exec for terminal automation.
- Codex GitHub Action for CI workflows.
- Codex MCP plus Agents SDK when Codex is one specialist in a wider workflow.

Official baseline:
The TypeScript Codex SDK supports starting, continuing and resuming local coding threads and requires Node.js 18+. App Server exposes lower-level thread, turn, streaming and approval protocol behavior. Verify exact installed APIs/protocol methods from current official OpenAI documentation and the locked package/server version before implementation.

Objective:
Build a production-shaped task-control backend and starter UI integration that safely manages long-running Codex engineering threads, projects streaming state, mediates approvals, survives interruption, exports structured evidence and supports operational governance.

Use case:
An authorized engineer starts a controlled RxFlow incident task. Codex inspects the repository, proposes a plan, requests command/file approval, adds a failing regression test, makes a minimal idempotency correction, executes tests and returns a release-evidence report. The console must preserve provenance without exposing patient data or secrets.

Security boundary:
- Work only in the supplied repository.
- Read all repository instructions.
- Keep Codex credentials server-side.
- Authenticate users and authorize repository/thread access.
- Never trust a client-supplied working directory without resolving it against an allowlist.
- Never auto-approve tools, commands or file changes.
- Do not display secrets, authorization headers, environment dumps, patient identifiers or prescriptions.
- Apply payload/event size limits and output truncation.
- Do not deploy unless separately authorized.
- Ask before adding dependencies.

Architecture requirements:
1. CodexAdapter isolates SDK or App Server specifics.
2. ThreadService owns start, continue, resume, fork/read/archive only when supported and authorized.
3. EventProjector converts versioned raw events to stable internal events.
4. TaskStore persists thread metadata, cursor/checkpoint, projection version and approval audit without storing unnecessary sensitive content.
5. ApprovalService binds one authenticated decision to one pending request.
6. EvidenceService records commands/tests/diffs with provenance and redaction.
7. ReportService validates machine-readable final output.
8. API/stream transport supplies snapshot plus resumable incremental updates.
9. UI remains a thin renderer of server-authoritative state.

Protocol and lifecycle:
- For App Server, perform JSON-RPC initialization before other requests and handle JSONL framing correctly.
- Correlate requests/responses by ID and notifications by method/type.
- Model thread separately from turn.
- Support start, continue and resume; support fork/read/archive only when the installed interface exposes them.
- Track turn started, progress, plan, diff/file, tool/command, approval, output, completed, failed, interrupted and cancelled events.
- Preserve ordering with sequence/cursor when available.
- Deduplicate replayed events.
- Detect gaps and rebuild projection from an authoritative snapshot or replay.
- Treat unknown future event versions as non-fatal diagnostics.

Long-running control:
- Abort/cancel with clear semantics.
- Heartbeat or stale-run detection.
- Reconnect with exponential backoff and a ceiling.
- Resume from persisted thread ID and cursor.
- Do not start a replacement thread silently after resume failure.
- Prevent concurrent turns on one thread unless explicitly supported.
- Enforce per-user/repository active-task and resource limits.

Approval governance:
- Server-side authorization and repository policy checks.
- Display exact sanitized target, rationale, scope and risk.
- Approve or deny explicitly; no timeout-to-approve.
- Single-use request token and idempotent decision endpoint.
- Audit actor, request, decision, timestamp and outcome.
- Expire stale approvals safely.
- Deny mismatched, replayed or already-resolved requests.
- Support policy classes such as read-only, workspace write, dependency install, external access and destructive action.

Evidence integrity:
Every changed file, command, test and final claim records its originating event ID/sequence and verification state. A command exiting zero proves only that command succeeded. A test is PASSED only when the corresponding execution evidence proves it. Missing evidence is NOT_RUN or UNKNOWN.

Structured final schema:
Include schemaVersion, threadId, turnId, repository/workdir identity, status, summary, plan, changed files, commands, tests, approvals, compatibility, security/privacy, rollback readiness, limitations, evidence references and timestamps. Validate strictly but retain a sanitized raw final response when validation fails.

Privacy and retention:
- Define data classification for thread metadata, prompts, tool output, diffs and approvals.
- Minimize persistence.
- Encrypt protected data at rest/in transit when the starter includes storage.
- Redact before logs/telemetry.
- Define retention and deletion.
- Keep metric labels low-cardinality.
- Provide audit access without leaking task content.

Observability:
Emit OpenTelemetry-compatible spans/metrics for thread/turn lifecycle, stream reconnects, event lag, projection failure, approval latency/decision, tool duration/failure, tests, resume success/failure and report validation. Never attach prompts, source, command output, secret values, patient data or prescriptions.

Reliability:
- Backpressure and bounded event buffers.
- Atomic checkpoint/projection persistence.
- Idempotent event application.
- Graceful shutdown preserving resumable state.
- Structured retryable/permanent error classification.
- Circuit-breaker/backoff decision for App Server process/transport failures.
- Health/readiness checks that do not expose thread content.

Testing:
1. Unit tests for reducer/projector, redaction, schema and approval state machine.
2. Contract tests using recorded synthetic SDK/App Server event fixtures.
3. Integration tests with a fake transport/server for initialization, streaming, interruption and resume.
4. Security tests for path traversal, cross-user thread access, approval replay, payload limits and secret redaction.
5. Concurrency tests for double decisions and overlapping turns.
6. Resilience tests for dropped/repeated/out-of-order events, malformed JSONL, request timeout and process restart.
7. UI tests for plan, diff, approvals, tests, failures and resume presentation.

Automation:
Run type-check, unit/integration/security tests, lint, build, dependency audit and fixture/schema validation. CI must use fakes by default and must not auto-approve, access arbitrary repositories or run destructive commands.

Five review passes:
1. Lifecycle/protocol correctness.
2. Authentication, authorization, approval and privacy.
3. Reliability, performance and backpressure.
4. Tests, schema compatibility and upgrade behavior.
5. Operations, retention, rollback and human governance.

For each pass, record severity, evidence, resolution and residual risk. Fix release-blocking issues and rerun affected checks.

Deliverables:
- working starter extension;
- architecture and trust-boundary diagram;
- versioned internal event and report schemas;
- fake adapter/transport and recorded fixtures;
- thread/approval/evidence persistence design;
- tests and exact results;
- OpenTelemetry configuration;
- security and privacy notes;
- operational runbook;
- upgrade/compatibility notes;
- rollback plan;
- known limitations;
- human approval checklist.

Final report:
EXECUTIVE SUMMARY / INTERFACE DECISION / ARCHITECTURE / TRUST BOUNDARIES /
THREAD AND TURN LIFECYCLE / STREAMING PROJECTION / APPROVAL GOVERNANCE /
INTERRUPTION AND RESUME / EVIDENCE INTEGRITY / STRUCTURED OUTPUT /
SECURITY AND PRIVACY / OBSERVABILITY / TESTS / COMMANDS AND RESULTS /
FIVE REVIEWS / OPERATIONS / ROLLBACK / LIMITATIONS / APPROVAL CHECKLIST.

Begin with read-only starter inspection and current installed-interface verification. Do not build beyond the supplied console's scope.
```
