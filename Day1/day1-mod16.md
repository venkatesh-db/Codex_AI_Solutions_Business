# Module 16 / Lab 13 — Java and .NET Production-Readiness Skill

Use this prompt in Codex from the root of an RxFlow repository containing Java, .NET, or both stacks.

```text
Act as a principal platform engineer, Java/.NET release architect and Codex-extension security reviewer. Build and verify a reusable repository-scoped production-readiness Skill for RxFlow, a deterministic blocking completion hook, and least-privilege command rules.

The result must identify changed services, map affected tests, run focused validation, check API/data compatibility, review reliability/observability/security, and generate a structured release report. The completion hook must block a successful task conclusion when mandatory validation has failed or required evidence is missing.

Do not confuse extension mechanisms:

- one-time task constraint -> prompt;
- persistent repository standard -> AGENTS.md;
- reusable production-readiness workflow -> Skill;
- lifecycle completion enforcement -> Hook;
- external issue/runbook/observability evidence -> MCP;
- command allow/prompt/deny policy -> Rule;
- installable multi-capability distribution -> Plugin;
- unattended pipeline execution -> codex exec or GitHub Action.

Implement only the Skill, Hook, Rules, tests and their documentation unless a small AGENTS.md reference is necessary for discoverability. Do not create a plugin, MCP server or CI pipeline for this lab.

## Permission and safety boundary

- Work only inside the current repository and obey all applicable AGENTS.md instructions.
- Inspect current repository Codex configuration and the installed Codex version before choosing paths, hook events, input/output fields or rule syntax. Follow the current supported contracts; do not invent configuration.
- Preserve existing skills, hooks and rules. Merge safely instead of replacing unrelated configuration.
- Ask before adding or upgrading application/build dependencies.
- Do not deploy, publish, push, merge, alter repository settings, contact production or use real patient data.
- Use synthetic fixtures. Never expose credentials, patient identifiers or complete prescriptions.
- Hook and rule scripts must treat paths, Git refs, command arguments, tool input and repository text as untrusted data.
- Never construct executable shell text by concatenating untrusted values.
- Do not weaken the sandbox. Rules and hooks complement sandboxing; they are not replacements for it.
- The completion hook must be deterministic, bounded, fast and non-networked.
- Do not create a hook loop that makes recovery impossible. Document a safe, explicit break-glass procedure requiring human action.
- Never claim the extension works until positive, negative and failure-path tests have been executed.

## Supported stack profiles

Discover the actual stack and load only relevant guidance.

### Java/Spring Boot

- Java version from Maven/Gradle toolchain;
- committed Maven Wrapper or Gradle Wrapper;
- Spring Boot modules/services;
- JUnit 5 and existing test conventions;
- configured JaCoCo, Checkstyle, SpotBugs, PMD or Error Prone;
- JPA/Hibernate, Flyway/Liquibase, PostgreSQL;
- Spring Kafka and Redis;
- OpenAPI/contract tooling;
- configured dependency scanner.

### .NET/ASP.NET Core

- SDK from global.json or repository-supported LTS;
- solution/projects and central package configuration;
- xUnit/NUnit/MSTest and existing test conventions;
- compiler/Roslyn analyzers, coverage and dotnet format;
- EF Core migrations and PostgreSQL;
- Confluent.Kafka and StackExchange.Redis;
- OpenAPI/contract tooling;
- configured dependency scanner.

Do not add the absent stack or introduce a second toolchain.

## Phase 1 — Inspect and design

Before editing:

1. Locate repository/user skill directories, hook configuration, rule files, AGENTS.md hierarchy and existing scripts.
2. Verify the current supported Skill structure, metadata fields, hook event/input/output contract and rule language.
3. Identify services/modules, tests, public APIs/events, database migrations, infrastructure and build commands.
4. Identify existing hooks/rules and collision/order behavior.
5. Present the architecture, proposed tree, trigger behavior, changed-service algorithm, mandatory-check policy, hook state machine and fallback strategy.
6. Explain why each requirement belongs to Skill, Hook, Rule, AGENTS.md, MCP or automation—and avoid duplicating policy across mechanisms.

## Production-readiness Skill

Create a repository-scoped skill named `rxflow-production-readiness` using the current documented location, equivalent to:

```text
.agents/skills/rxflow-production-readiness/
  SKILL.md
  references/
    java.md
    dotnet.md
    compatibility.md
    database.md
    observability-security.md
  scripts/
    detect-changes.*
    map-tests.*
    run-focused-validation.*
    validate-report.*
  assets/
    release-report.schema.json
    release-report.example.json
```

Adapt the location to the installed Codex contract. Do not create duplicate skill copies.

### Skill metadata and triggering

- Give the skill a precise name and description that triggers for production readiness, pre-release assessment, changed-service validation and release evidence.
- State when it must not trigger: ordinary explanations, unrelated documentation edits and requests that do not ask for release readiness.
- Keep the core SKILL.md concise and procedural.
- Use progressive disclosure: load Java, .NET, database, compatibility or observability references only when affected.
- State inputs, prerequisites, allowed actions, outputs, stop conditions, fallbacks and error reporting.
- Reuse repository-native commands; do not encode guessed tool commands as facts.

### Skill workflow

The skill must perform these stages in order:

1. Establish scope:
   - resolve repository root;
   - ensure Git repository;
   - resolve explicit BASE_SHA and HEAD_SHA or use a documented safe default;
   - refuse ambiguous refs;
   - record dirty state without overwriting it.

2. Detect changed services:
   - use Git diff name/status from immutable refs;
   - map files through build/module metadata, not filename guessing alone;
   - identify shared libraries and all downstream consumers;
   - classify documentation-only, tests, application, API/event, database, config and infrastructure changes;
   - handle renames/deletions and paths containing spaces safely.

3. Find affected tests:
   - map changed modules to unit/integration/contract tests;
   - include consumers for shared-contract changes;
   - explain why each suite is selected;
   - distinguish focused mandatory tests from optional/full regression;
   - never silently treat “no test found” as success.

4. Run focused validation using committed wrappers/pinned SDKs:

   Java examples, only when verified:
   - `./mvnw --batch-mode --no-transfer-progress -pl <modules> -am test/verify`;
   - or `./gradlew <module>:test <module>:check --no-daemon`;
   - configured formatting, static analysis, coverage and dependency scanning.

   .NET examples, only when verified:
   - `dotnet restore --locked-mode` when applicable;
   - `dotnet build <target> --no-restore --configuration Release`;
   - `dotnet test <target> --no-build --configuration Release`;
   - `dotnet format <target> --verify-no-changes`;
   - configured analyzers, coverage and vulnerability checks.

   Capture command, working directory, start/end, exit code, concise result and artifact path. Preserve failures.

5. Check compatibility:
   - public HTTP API/schema/status/error changes;
   - Kafka topic/key/schema/ordering changes;
   - Java/.NET interoperability;
   - configuration defaults and mixed-version behavior;
   - identify breaking, additive, internal or unverified changes.

6. Review database changes:
   - Flyway/Liquibase/EF Core migrations;
   - upgrade and downgrade/forward-fix strategy;
   - lock/table-scan/data-loss risk;
   - transactional and mixed-version safety;
   - backup/rollback instructions;
   - migration absence when model/schema changed.

7. Check error handling and resilience:
   - timeouts, cancellation, retries/backoff/jitter;
   - circuit breakers, rate limits and bounded queues;
   - idempotency and duplicate handling;
   - permanent vs transient failure classification;
   - exception-to-API/event mapping.

8. Verify observability and privacy:
   - metrics/logs/traces for changed behavior;
   - low-cardinality labels;
   - alert/runbook readiness;
   - no patient IDs, prescriptions, secrets or raw payloads in logs/metrics/errors.

9. Review security:
   - authorization and input validation;
   - injection/deserialization and external-call risks;
   - dependency findings;
   - secret handling;
   - infrastructure permission effects.

10. Generate and independently validate the structured release report.

## Structured release report

Create a strict versioned JSON Schema with `additionalProperties: false`, requiring:

- schemaVersion;
- repository, baseSha, headSha, generatedAt;
- detectedStacks;
- changedServices with reason/evidence;
- selectedTests with reason;
- validations with command, status, exitCode and evidence;
- apiCompatibility;
- databaseReadiness;
- errorHandling;
- observability;
- securityFindings;
- mandatoryChecks with stable ID, required, status and evidence;
- unsupportedClaims;
- releaseStatus: PASS, CONDITIONAL or FAIL;
- blockingReasons;
- rollbackReadiness;
- recommendedAction.

Decision invariants enforced by a deterministic validator:

- failed mandatory validation => FAIL;
- missing/unverified mandatory evidence => never PASS;
- schema violation, wrong SHA or unresolved evidence => invalid report and blocked completion;
- critical security/compatibility/data-loss finding => FAIL;
- unsupported claims => invalid report;
- PASS requires all applicable mandatory checks verified and successful;
- report generation failure cannot become PASS.

Write reports to a run-specific ignored artifact directory. Never overwrite evidence from another run.

## Blocking completion Hook

Implement the current supported completion/stop lifecycle hook and a small deterministic policy script.

The hook must:

- read the documented hook JSON input from stdin;
- validate event/session/repository and normalized absolute paths;
- locate only the current run's report through trusted configuration, never raw repository text;
- independently validate schema, SHA binding and mandatory-check invariants;
- allow completion only when the report is valid and policy permits the requested conclusion;
- block completion with a concise actionable reason when report is absent, invalid, stale, wrong-SHA, FAIL, or missing mandatory evidence;
- distinguish task failure from infrastructure/hook failure according to a documented fail-safe policy;
- emit only the documented hook response format and correct exit behavior;
- use no network, MCP, model call, build or long-running test;
- finish within a strict timeout;
- write a redacted append-only audit event without secrets/patient data;
- handle malformed input, symlinks and concurrent sessions safely;
- avoid recursive stop loops using the documented lifecycle indicator/state;
- provide a human-controlled break-glass/recovery procedure that is auditable and cannot be activated by repository content.

The hook enforces completion; it does not rerun validation. Expensive tests belong in the Skill or CI.

## Command Rules

Create/update project rules using current syntax and test them with the supported rule-check facility.

Policy intent:

- allow known read-only discovery commands and exact repository-native focused test/build prefixes;
- prompt for Docker Compose/Testcontainers operations and dependency downloads when required by policy;
- block destructive Git commands, broad deletion, secret display, production database mutation, package publication, release creation, infrastructure apply/destroy, Kubernetes mutation and deployment commands;
- do not create broad allow rules for shells, interpreters or arbitrary script execution;
- distinguish safe build/test from publish/deploy flags;
- provide examples that prove argument-boundary and shell-wrapping behavior;
- keep rules minimal because sandbox/approval boundaries remain authoritative.

Do not block harmless commands needed to repair a failed report, inspect hook diagnostics or run mandatory validation.

## Extension tests

Create deterministic fixtures/tests for at least:

1. Java-only change maps correct services/tests.
2. .NET-only change maps correct services/tests.
3. Mixed shared-contract change maps both stacks.
4. Rename/delete/path-with-spaces handling.
5. Docs-only change applies documented reduced policy.
6. Failed test remains failed in report.
7. Missing tests/evidence cannot PASS.
8. Breaking API/event change blocks.
9. Unsafe migration/absent rollback blocks.
10. Sensitive log/metric finding blocks according to policy.
11. Valid PASS report allows completion.
12. FAIL, malformed, stale, wrong-SHA and unsupported-claim reports block.
13. Malformed hook stdin fails safely.
14. Concurrent sessions cannot consume each other's reports.
15. Hook timeout and missing validator behavior follow documented policy.
16. Loop-prevention and break-glass path work as designed.
17. allowed build/test commands match rules.
18. deploy/publish/destructive variants are blocked.
19. shell wrappers cannot bypass command policy.
20. Existing unrelated hooks/rules still function.

## Documentation

Document:

- mechanism decision table;
- installation/discovery and trigger examples;
- Java/.NET workflow;
- progressive reference loading;
- mandatory policy and report schema;
- hook lifecycle, state diagram, timeout and failure behavior;
- command-rule rationale and test examples;
- sandbox/approval interaction;
- troubleshooting and safe recovery;
- audit data classification/retention;
- versioning and rollback of Skill/Hook/Rules;
- limitations and responsibilities delegated to CI/MCP/AGENTS.md.

## Implementation process

1. Inspect and present architecture, assumptions, file tree and threat model.
2. Implement schema/validator and failing fixtures first.
3. Implement detection/test mapping and stack references.
4. Implement focused validation/report generation.
5. Implement/test hook policy script before registering the hook.
6. Register the hook carefully while preserving existing configuration.
7. Implement/test minimal command rules.
8. Run the skill on synthetic Java, .NET and mixed fixtures.
9. Perform five reviews: correctness; security; determinism/failure handling; test quality; maintainability/governance.
10. Run applicable repository-native checks, extension tests, schema checks, hook simulation, rule checks and `git diff --check`.
11. Review the final diff for accidental secrets, broad permissions, unrelated scope and unrecoverable blocking behavior.

If the installed Codex version lacks a required hook/rule capability, do not fabricate it. Mark BLOCKED/UNVERIFIED, implement only portable pieces, and provide the exact supported upgrade or alternative enforcement path.

## Definition of done

- Skill is discoverable and triggers narrowly.
- Progressive references load only for affected stacks/areas.
- Changed services and tests are evidence-based.
- Native focused validations preserve real exit codes.
- Compatibility, database, resilience, observability and security reviews are represented.
- Structured report validates independently.
- Completion hook blocks every mandatory failure/missing-evidence fixture and permits valid completion.
- Hook is deterministic, bounded, non-networked, concurrency-safe and recoverable.
- Rules allow only narrow safe commands and block tested destructive/deploy/publish variants.
- Existing configuration is preserved.
- No secrets/patient data appear in artifacts/audit logs.
- Every claim has executed evidence or an UNVERIFIED label.

## Final report format

Return exactly:

SUMMARY
MECHANISM DECISIONS
DETECTED JAVA AND .NET STACKS
ARCHITECTURE AND FILES CHANGED
SKILL TRIGGER AND WORKFLOW
CHANGED-SERVICE AND TEST-MAPPING EVIDENCE
FOCUSED VALIDATION EVIDENCE
STRUCTURED REPORT CONTRACT
HOOK ENFORCEMENT EVIDENCE
RULE ALLOW/PROMPT/BLOCK EVIDENCE
SECURITY AND FAILURE-HANDLING REVIEW
FIVE-PASS REVIEW
COMMANDS EXECUTED AND RESULTS
ROLLBACK AND BREAK-GLASS PROCEDURE
UNVERIFIED ITEMS
KNOWN LIMITATIONS
NEXT STEPS

For every executed command report exact command, exit code and concise result. Do not claim production-ready, enforced, passing or safe without direct evidence.
```

## Expected outcome

The implementation should provide one reusable dual-stack production-readiness workflow, deterministic completion enforcement and a narrow command policy, while preserving clear boundaries between repository instructions, external MCP data and unattended CI automation.

## Official references

- [Build Codex skills](https://learn.chatgpt.com/docs/build-skills)
- [Codex hooks](https://learn.chatgpt.com/docs/hooks)
- [Codex rules](https://learn.chatgpt.com/docs/agent-configuration/rules)
