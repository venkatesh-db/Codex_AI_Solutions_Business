# Hands-on Lab 17 — Intermediate Engineering Task Console

```text
Act as a senior TypeScript engineer extending the prepared RxFlow engineering-task console starter repository.

Use the Codex SDK for an application-controlled coding thread. Use App Server only if the starter already uses its JSONL/JSON-RPC protocol. Do not replace the supplied starter or build a complete production UI.

Verified integration guidance:
- Terminal-only automation belongs in codex exec.
- GitHub workflow automation belongs in the Codex GitHub Action.
- An application-controlled coding thread belongs in the Codex SDK.
- A custom rich client requiring low-level lifecycle and streaming control belongs on App Server.
- Codex as one specialist in a broader agent workflow belongs behind Codex MCP with the Agents SDK.

Technology:
- Node.js 18 or later
- TypeScript with strict mode
- @openai/codex-sdk when using the SDK
- Starter-provided UI/terminal framework
- Zod or starter-provided JSON Schema validation
- Vitest/Jest and the starter's test utilities

Objective:
From the prepared starter, implement a working console that starts an RxFlow engineering task, streams progress, displays the current plan, records changed files, surfaces approval requests, captures test evidence, resumes an interrupted thread and produces a validated final report.

Engineering use case:
Ask Codex to investigate and minimally fix an RxFlow defect where a duplicate order may be created after a slow client retries POST /orders. The console coordinates the task; it does not contain the fix itself.

Permission boundary:
- Work only in the starter repository.
- Read AGENTS.md, CLAUDE.md and README files first.
- Preserve existing architecture and UI components.
- Do not deploy, push, merge or access production.
- Never auto-approve file changes or command execution.
- Do not expose API keys, prompts, patient data, prescriptions or raw environment variables.
- Do not add dependencies without asking.
- Do not claim an event, test or change occurred unless received from Codex or verified locally.

Phase 1 — Inspect
1. Determine whether the starter uses Codex SDK or App Server.
2. Identify entry point, state model, event adapter, renderer, persistence and tests.
3. Verify Node/package-manager versions and real commands.
4. Present the smallest implementation plan.

Phase 2 — Thread lifecycle
Implement:
- start a thread with an explicit working directory;
- submit an engineering prompt;
- retain and display the thread ID;
- continue the active thread with a follow-up turn;
- persist enough non-sensitive state to resume after process interruption;
- resume by thread ID;
- display active, waiting-for-approval, completed, failed, interrupted or cancelled state.

Do not confuse a thread with a single turn. Multiple turns may run in one thread.

Phase 3 — Streaming projection
Consume the starter/SDK event stream and maintain one application state projection containing:
- current status;
- streamed progress messages;
- active plan and step states;
- changed-file paths and change status;
- command/tool execution state;
- approval request and decision;
- test commands, exit status and summary;
- final response;
- structured error when failed.

Handle duplicate or repeated events idempotently. Ignore unknown event types safely and record a diagnostic without crashing.

Phase 4 — Approval handling
Surface file-change and command-execution approval requests with:
- request ID;
- action type;
- concise command/change summary;
- working directory or affected path;
- risk explanation when supplied;
- approve/deny controls.

Requirements:
- default to no decision;
- never approve automatically;
- prevent double submission;
- associate a decision with the correct request;
- record decision time and result;
- redact sensitive command content.

Phase 5 — Test evidence
Display only observed test evidence:
- exact command when safe;
- running/completed/failed state;
- exit code;
- passed, failed and skipped counts when present;
- short sanitized output;
- NOT RUN when no test event exists.

Phase 6 — Final structured report
Validate a final object with this contract:

{
  "threadId": "string",
  "status": "completed | failed | interrupted | cancelled",
  "summary": "string",
  "plan": [{"step":"string","status":"pending | in_progress | completed"}],
  "changedFiles": [{"path":"string","change":"added | modified | deleted | unknown"}],
  "commands": [{"command":"string","exitCode":0,"result":"string"}],
  "tests": {"passed":0,"failed":0,"skipped":0,"verified":true},
  "approvals": [{"requestId":"string","decision":"approved | denied"}],
  "limitations": ["string"]
}

Never synthesize successful tests or commands to satisfy the schema. Use verified=false and limitations when evidence is absent.

Phase 7 — Error handling
Handle invalid working directory, start failure, stream interruption, malformed event, approval failure, resume failure, timeout and structured-output validation failure. Preserve the thread ID whenever possible and show a recoverable next action.

Phase 8 — Tests
Add focused tests for:
- starting and continuing a thread;
- streamed progress order;
- plan replacement/update;
- changed-file deduplication;
- approval approve/deny and double-click prevention;
- test result capture;
- interrupted-thread persistence and resume;
- unknown/malformed event handling;
- final schema validation;
- sensitive-data redaction.

Use a fake Codex adapter and recorded synthetic events. Unit tests must not require a live Codex task or API credentials.

Verification:
Run the repository's actual install, type-check, test, lint and build commands. Do not invent command names or results.

Final report:
SUMMARY / STARTER ARCHITECTURE / INTERFACE CHOICE / FILES CHANGED /
THREAD LIFECYCLE / STREAMING EVENTS / APPROVAL SAFETY / RESUME FLOW /
STRUCTURED REPORT / TEST EVIDENCE / COMMANDS AND RESULTS / LIMITATIONS.

Begin with starter inspection. Keep scope within the supplied console and avoid building unrelated production UI features.
```
