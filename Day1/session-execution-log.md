# RxFlow Codex session execution log

This document summarizes the requests and actions performed during the session. Attached production prompts are summarized rather than reproduced verbatim. No credentials, tokens, patient information, or production endpoints are included.

## 1. Repository instruction hierarchy

**Request:** Apply the repository-governance prompt to the .NET stack.

**Actions:**

- Inspected the .NET solution, projects, tests, SDK pin, package configuration, and existing instruction files.
- Confirmed the repository contains .NET only; no Java components were invented.
- Added the root and component-level `AGENTS.md` hierarchy.
- Added detailed engineering standards under `docs/engineering`.
- Added `scripts/agents/verify-hierarchy.ps1` and scope expectations.

**Result:** Hierarchy verification passed with exit code `0`. Build and tests were blocked because SDK `8.0.404` is not installed.

## 2. Payment-path rule discussion

**Request:** Execute payment-path rules and explain automation.

**Actions:** Inspected the order-submission flow and confirmed that no payment implementation exists.

**Result:** No payment code was invented or changed. The durable idempotency and duplicate-submission rule remains in repository guidance.

## 3. Lab-routing change

**Request:** Modify lab routing and execute agent rules.

**Actions:**

- Updated `LabRouter` so standard work routes East and `HIGH_INDEX` or `MIRROR` work routes West.
- Made matching case-insensitive.
- Added focused routing tests.
- Executed the instruction hierarchy verifier.

**Result:** Hierarchy verification passed. .NET tests could not start because SDK `8.0.404` is unavailable.

## 4. Rules applied to LabRouter

**Request:** Explain which rules executed for the routing change.

**Result:** Documented the effective root plus Application instruction chain, deterministic routing tests, regression coverage, sensitive-data requirements, and non-triggered API/database/payment/infrastructure rules.

## 5. Code-flow skill design

**Request:** Recommend global or module-level skills and create a code-flow diagram.

**Result:** Recommended a reusable global .NET skill plus an RxFlow module skill. Produced a Mermaid flow covering controller, order service, validation, inventory, pricing, routing, persistence, workers, connectors, and events.

## 6. Global and module skills

**Request:** Create both skillsets.

**Actions:** Added:

- `skills/dotnet-engineering-global/SKILL.md`
- `skills/rxflow-dotnet/SKILL.md`

**Result:** Repository hierarchy still passed. The Python skill validator could not run because Python is unavailable.

## 7. New lab-routing API

**Request:** Use the module skill to create a new API.

**Actions:**

- Added authenticated `POST /routing/lab`.
- Added request validation and a typed response.
- Added controller tests and an API project reference from the test project.
- Performed an additive API compatibility review.

**Result:** Hierarchy verification passed. Focused tests were blocked by the missing .NET SDK.

## 8. API rule verification

**Request:** Confirm whether skills and agent rules executed for the new API.

**Result:** Confirmed use of root rules, API-scoped rules, and the RxFlow module skill. Documented triggered and non-triggered controls.

## 9. Codex operating profiles

**Request:** Assess and then implement five profiles: analyst, developer, security, CI, and remediator.

**Actions:**

- Added profile matrix, threat model, operations documentation, CI wrapper, verification script, and remediation worktree scripts.
- Kept profiles as repository policy intent rather than silently changing global user configuration.
- Added a fail-closed rules example because active exec-policy syntax was not safely established.

**Result:** Profile preflight and instruction hierarchy passed. Full enforcement remains unverified without a trusted runner, Git, network policy, and the pinned SDK.

## 10. `rx-developer` execution attempts

**Request:** Execute the developer profile and fix terminal errors.

**Actions and results:**

- Corrected invalid `exec --ask-for-approval` usage to the global `-a on-request` option.
- Confirmed the real Codex state directory was read-only in the execution sandbox.
- Used a disposable repository-local Codex home.
- Codex reached startup, but service access failed with Windows socket error `10013`.
- Removed the disposable runtime directory.

## 11. Profile selection guidance

**Requests:** Which profile modifies code; can developer/tester agents modify code in isolation; show the developer configuration; list internal Codex files.

**Results:**

- Recommended `rx-developer` for normal workspace changes.
- Recommended `rx-remediator` for patches in isolated worktrees.
- Clarified that analyst, security, and CI profiles are read-only.
- Confirmed no installed `rx-developer.config.toml` exists.
- Listed relevant non-secret Codex state/configuration locations without displaying credentials.

## 12. Parallel release-review framework

**Request:** Implement the five-agent routing-capacity release-review prompt.

**Actions:**

- Added `docs/release-review-operations.md`.
- Added `docs/release-review-result.schema.json`.
- Added `scripts/codex-profiles/run-release-review.ps1`.
- Defined exactly five review lanes: correctness, tests, security, performance, and compatibility.
- Added fail-closed checks for Git, immutable refs, worktrees, and native tooling.

**Result:** The harness exited `2` because Git was unavailable. No reviewer worktrees or agents were started.

## 13. Git installation and PATH attempts

**Requests:** Install Git, configure its path, and rerun release review.

**Actions and results:**

- Checked `winget`, Chocolatey, Scoop, Git, and standard Git installation paths; none were available.
- Attempted an approved download of the official Git for Windows installer.
- Download failed because the connection closed unexpectedly.
- Repeated harness runs continued to exit `2` with the Git prerequisite blocker.
- Explained that `C:\Path\To\Git\cmd` is a placeholder and must not be added as though it were a real path.

## 14. Analyst review attempt

**Request:** Switch to `rx-analyst` and run review agents.

**Actions:** Started a read-only, ephemeral analyst invocation with a disposable Codex home.

**Result:** Codex service access failed with socket error `10013`; the release-review harness remained blocked by missing Git. No files or worktrees were changed.

## 15. Local MCP implementation

**Request:** Implement MCP locally and explain/execute it.

**Actions:** Added:

- `scripts/codex-profiles/local-mcp-server.ps1`
- `scripts/codex-profiles/local-mcp-fixture.json`
- `scripts/codex-profiles/test-local-mcp.ps1`

The server supports MCP initialization, tool discovery, and one read-only `get_release_evidence` tool backed by synthetic fixture data.

**Result:** Local MCP contract test passed with exit code `0`. Direct `get_release_evidence` invocation returned the synthetic candidate, acceptance criteria, synthetic incident status, and `UNVERIFIED` readiness.

## 16. GitHub access and plugin

**Request:** Add GitHub MCP/plugin and access `venkatesh-db/Claude_AISolutions_Business`.

**Actions and results:**

- Public browsing did not retrieve the requested repository.
- Located the official GitHub plugin candidate and requested installation.
- The installation request was not confirmed.
- A later status check reported the GitHub plugin as available but `installed: false`; no GitHub tools were loaded.

## Commands and observed results

| Command or check | Exit/result |
|---|---|
| `scripts/agents/verify-hierarchy.ps1` | `0`, passed |
| `dotnet build/test` | SDK resolution failure; `8.0.404` missing |
| Skill validator | not run; Python unavailable |
| `scripts/codex-profiles/verify-profile.ps1 -Profile rx-ci` | `0`, policy preflight passed |
| `run-release-review.ps1 -BaseSha HEAD -HeadSha HEAD` | `2`, Git unavailable |
| `test-local-mcp.ps1` | `0`, MCP contract passed |
| `codex exec` profile attempts | blocked by read-only state and/or network socket error `10013` |
| Git installer download | failed; connection closed unexpectedly |

## Current status

- Repository instruction hierarchy: implemented and verified.
- Lab-routing implementation and API: implemented; native tests not executed due missing SDK.
- Skills: created inside the repository.
- Profile framework: implemented as policy, scripts, and documentation; runtime enforcement remains partially unverified.
- Release-review harness: implemented but blocked by missing Git and immutable commit identity.
- Local MCP: implemented and verified with synthetic evidence.
- GitHub plugin: not installed according to the last verified status.
