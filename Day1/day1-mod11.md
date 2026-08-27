# Module 14 / Lab 11 — Java and .NET RxFlow AGENTS.md Hierarchy

Use this prompt in Codex from the root of an RxFlow monorepo containing Java, .NET, or both stacks.

```text
Act as a principal software architect and repository-governance engineer. Design, implement and verify a concise, maintainable AGENTS.md hierarchy for the RxFlow monorepo across `order_api`, `routing`, `pricing`, `workers`, `analytics` and `infra`.

The hierarchy must support Java/Spring Boot and .NET/ASP.NET Core components, follow closest-file precedence, keep universal rules at the repository root, place component-only guidance near the component, reference detailed supporting documents rather than duplicating them, and prove that required instructions are discovered in the correct scope.

## Required durable rules

The effective instruction chain must enforce:

1. Payment-path changes require idempotency and duplicate-submission tests.
2. Database changes require upgrade, rollback/forward-fix instructions and migration validation.
3. Public API changes require compatibility review.
4. New external calls require explicit timeout and retry policies.
5. Sensitive data must never be logged.
6. Infrastructure changes require security validation.
7. Every defect fix requires a regression test that fails before and passes after the correction.
8. Final reports must include commands executed, exit codes and concise results.

These are repository standards, not one-time prompt instructions. Put each durable rule in the narrowest common AGENTS.md scope that reliably covers every relevant Java and .NET component.

## Permission boundary

- Work only inside the current repository.
- Read all existing AGENTS.md and AGENTS.override.md files before editing.
- Inspect the installed Codex instruction-discovery configuration, including fallback filenames and byte limits.
- Preserve useful existing guidance and unrelated user changes.
- Do not modify application behavior, dependencies, database schema or infrastructure resources.
- Do not create global user instructions under the real Codex home directory. Provide a sanitized example only if global guidance is needed for teaching.
- Do not deploy, push, merge, access production or use real patient data.
- Do not put secrets, patient identifiers, prescriptions, credentials, machine-specific paths or production endpoints in instructions/examples.
- Treat repository instructions as durable guidance, not a security boundary. They cannot override sandbox, managed policy, user authorization or system instructions.
- Do not use AGENTS.md for command enforcement, lifecycle blocking, external data retrieval or a large reusable workflow when Rule, Hook, MCP or Skill is the correct mechanism.
- Do not claim precedence works until tested from representative component directories.

## Supported stack profiles

Discover actual tools and paths; do not invent missing components.

Java/Spring Boot guidance may reference:

- Java version pinned by Maven/Gradle toolchains;
- committed Maven/Gradle Wrapper;
- JUnit 5 and repository test conventions;
- Spring MVC/WebFlux, JPA/Hibernate, Flyway/Liquibase;
- Spring Kafka, Redis/Lettuce and Resilience4j;
- configured formatting, static analysis, coverage and dependency checks.

.NET/ASP.NET Core guidance may reference:

- SDK pinned by global.json;
- solution/project/central package configuration;
- xUnit/NUnit/MSTest and repository test conventions;
- ASP.NET Core, EF Core migrations;
- Confluent.Kafka, StackExchange.Redis and Polly/.NET resilience;
- dotnet build/test/format, analyzers, coverage and dependency checks.

Use repository-verified commands only. Do not copy generic commands that do not work.

## Phase 1 — Repository intelligence

Before editing:

1. Map repository root, component directories, language/build boundaries, shared libraries and tests.
2. Locate all current instruction files and determine their effective scope.
3. Identify contradictory, duplicated, stale, vague or non-operational guidance.
4. Verify AGENTS.md discovery and closest-file precedence for the installed Codex version.
5. Measure the combined instruction size for representative directories against the configured limit.
6. Identify detailed standards that belong in referenced documents instead of AGENTS.md.
7. Present current hierarchy, proposed hierarchy, ownership, precedence examples and migration plan before editing.

If actual directories differ from `order_api`, `routing`, `pricing`, `workers`, `analytics` and `infra`, map the conceptual components to real paths and explain the mapping.

## Required hierarchy

Adapt to the real monorepo, delivering the equivalent of:

```text
AGENTS.md
docs/engineering/
  testing-standards.md
  api-compatibility.md
  database-migrations.md
  external-call-resilience.md
  sensitive-data-and-observability.md
  infrastructure-security.md
  final-evidence-reporting.md
services/
  order_api/AGENTS.md
  routing/AGENTS.md
  pricing/AGENTS.md
  workers/AGENTS.md
  analytics/AGENTS.md
infra/AGENTS.md
scripts/agents/verify-hierarchy.*
tests/agents/...
```

For separate language implementations, add a nested AGENTS.md only when Java and .NET commands or conventions genuinely differ, for example:

```text
services/order_api/java/AGENTS.md
services/order_api/dotnet/AGENTS.md
```

Do not create empty or copy-pasted language files. Closest-file instructions should add/override only what differs.

## Root AGENTS.md

Keep the root concise and operational. It must contain:

- one-paragraph repository purpose and architecture pointer;
- instruction-precedence reminder;
- task workflow: inspect, plan proportionally, change narrowly, test, review diff, report evidence;
- universal privacy rule: never log patient identifiers, prescriptions, secrets or raw sensitive payloads;
- universal defect rule: every defect fix requires a regression test with pre/post-fix evidence;
- universal dependency rule: ask before adding/upgrading production dependencies;
- universal database-change rule with link to migration/rollback standard;
- universal public-contract rule with link to API/event compatibility standard;
- universal external-call rule requiring timeout, retry classification/backoff/jitter, cancellation and idempotency analysis;
- universal final-report rule requiring exact commands, exit codes, results, unverified items and known limitations;
- root-level build/test entry points only when verified;
- links to supporting documents using stable repository-relative paths;
- explicit prohibition on deployments/production mutation unless the user separately authorizes it.

Avoid essays, architecture duplication, temporary task details, model-specific prompting tricks and rules that belong only to one component.

## Component instructions

### `order_api/AGENTS.md`

- identify public HTTP/validation/authentication/idempotency boundaries;
- require API compatibility review for routes, schemas, status codes and errors;
- require duplicate-request/idempotency tests for payment/order submission paths;
- require authorization tests for privileged overrides;
- keep business logic out of controllers/handlers according to actual architecture;
- specify focused Java/.NET API test commands discovered from the repository;
- prohibit sensitive request-body logging.

### `routing/AGENTS.md`

- protect capacity correctness under concurrency;
- require tests for oversubscription, zero capacity, ties, release-on-failure and Redis atomicity;
- require timeout/retry/circuit-breaker/backpressure design for new lab connector calls;
- require connector idempotency analysis because a timeout may occur after acceptance;
- require low-cardinality metrics and cancellation propagation;
- specify verified focused routing tests.

### `pricing/AGENTS.md`

- preserve exact monetary/decimal semantics and currency/rounding rules;
- require tests for discounts, boundaries and duplicated pricing rules;
- treat payment-impacting changes as idempotency-sensitive where applicable;
- prohibit unrelated refactoring during defect fixes;
- require compatibility review for externally visible price schema/meaning;
- specify verified focused pricing tests.

### `workers/AGENTS.md`

- document delivery/acknowledgment and idempotent-consumer expectations;
- require bounded retry/backoff/jitter and permanent/transient classification;
- prohibit retry amplification and layered retries without analysis;
- require duplicate/redelivery, poison-message, cancellation and shutdown tests;
- protect transaction/outbox/offset ordering;
- prohibit sensitive payload logging;
- specify verified worker tests.

### `analytics/AGENTS.md`

- require parameterized queries and prohibit string-built SQL;
- define permitted de-identified analytics fields;
- require deterministic ETL/data-quality tests and time-zone/window semantics;
- protect schema/contract compatibility;
- prohibit patient-level/raw-prescription output in logs, reports or fixtures;
- specify verified Java/.NET analytics test commands when both exist.

### `infra/AGENTS.md`

- require security validation for infrastructure changes;
- require least privilege, secret references rather than secret values, pinned images/actions and safe defaults;
- require deployment and rollback instructions;
- require database migration operational review;
- require timeout/retry/resource-limit/health-check analysis;
- prohibit production apply/deploy/destroy from routine tasks;
- specify safe lint/validate commands that do not mutate infrastructure.

## Language-specific nested instructions

Where needed, Java nested files may define only:

- wrapper command and module selectors;
- Java formatting/static/coverage commands;
- Spring-specific testing/migration conventions;
- build-output paths.

.NET nested files may define only:

- solution/project command and filters;
- dotnet format/analyzer/coverage commands;
- ASP.NET/EF Core testing/migration conventions;
- build-output paths.

Do not repeat universal privacy, regression, database, API, external-call or final-report rules. They are inherited.

## Supporting documents

Move explanatory detail into focused documents with owners/review dates:

- testing standard: unit/integration/contract/load/fault expectations and evidence;
- API compatibility: HTTP and Kafka compatibility, versioning and mixed-version rollout;
- database migrations: online-safety, upgrade, downgrade/forward-fix, backup and validation;
- external-call resilience: deadlines, retries, jitter, circuit breakers, rate limiting and idempotency;
- sensitive data/observability: classification, redaction, metrics labels and trace rules;
- infrastructure security: least privilege, image/action pinning, validation and rollback;
- final evidence: required commands/results/unverified/limitations format.

AGENTS.md should link to exact relevant sections, not say “read all docs.” Verify every link.

## Precedence and conflict rules

- Root guidance applies to the repository unless a closer instruction adds a valid local requirement.
- Nested guidance should refine, not weaken, privacy/security/evidence rules.
- When language/component rules conflict with root requirements, choose the stricter safe interpretation and report the conflict.
- Do not rely on instruction order to override a safety requirement.
- Avoid AGENTS.override.md unless an intentional temporary/exceptional override is required and documented with owner/expiry.
- Do not use global instructions for RxFlow-specific policy.

## Hierarchy verification

Create a deterministic read-only verifier and fixtures that check:

1. required files exist only at justified scopes;
2. repository-root discovery is correct;
3. effective chain from root to each of six component directories is as expected;
4. Java and .NET nested paths inherit root/component guidance;
5. closest-file additions appear only in their component/language scope;
6. no nested file weakens universal rules;
7. all supporting links resolve;
8. commands referenced actually exist and are syntactically appropriate;
9. combined instruction size remains within configured limit;
10. no duplicated/contradictory normative rule exists;
11. no secret, patient identifier, prescription or production credential is present;
12. files use clear imperative, testable language rather than aspirations;
13. no AGENTS.md contains temporary ticket-specific instructions;
14. renamed/moved components cannot silently lose applicable guidance;
15. final-report requirement is inherited everywhere.

Also run representative Codex instruction-summary checks from:

- repository root;
- each of six component directories;
- a Java nested component;
- a .NET nested component.

Record the effective files in discovery order. If Codex invocation is unavailable, test the hierarchy using the documented discovery algorithm and label live verification UNVERIFIED.

## Behavioral scenario tests

Verify expected guidance for hypothetical changes without modifying application code:

1. Order submission retry defect -> idempotency + regression tests.
2. Java Flyway or .NET EF migration -> upgrade/rollback instructions.
3. Public endpoint response change -> compatibility review.
4. New Rx lab HTTP client -> timeout/retry/cancellation/idempotency policy.
5. Logging a prescription -> explicitly prohibited.
6. Terraform/Kubernetes change -> infrastructure security validation.
7. Worker retry bug -> regression plus amplification/redelivery tests.
8. Analytics f-string/string-interpolated SQL equivalent -> parameterization requirement.
9. Final task completion -> commands, exit codes, results and limitations.

## Maintenance and governance

Document:

- owner for root and every component file;
- review cadence and triggers;
- how build/architecture changes update instructions;
- how to add/move a component without losing guidance;
- how contradictions/stale commands are detected;
- instruction size budget;
- pull-request checklist for AGENTS.md changes;
- changelog/decision record for material policy changes;
- distinction between AGENTS.md, Skill, Hook, Rule, MCP, Plugin and CI responsibilities.

## Implementation process

1. Inspect and present existing/proposed hierarchy, precedence map, risk list and file tree.
2. Draft root universal rules and supporting standards.
3. Add component files one at a time, removing duplication.
4. Add language-specific files only where commands/conventions differ.
5. Implement hierarchy/link/size/secret/duplication validation.
6. Run component and behavioral scenario tests.
7. Perform five reviews: correctness/precedence; concision/duplication; Java/.NET accuracy; security/privacy; maintenance/governance.
8. Run applicable Markdown/link/lint checks and `git diff --check`.
9. Review final diff for accidental application changes, secrets, vague language, broken links and excessive scope.

## Definition of done

- root and nested guidance matches actual repository structure;
- all eight required durable rules apply in every relevant scope;
- Java/.NET commands are verified and localized without duplication;
- closest-file precedence is demonstrated;
- supporting docs hold detail while AGENTS.md stays concise;
- privacy/security rules cannot be weakened by nested files;
- all links, sizes and scenario tests pass;
- ownership and maintenance process are documented;
- application behavior is unchanged;
- every final claim has evidence or an UNVERIFIED label.

## Final report

Return exactly:

SUMMARY
EXISTING AND FINAL HIERARCHY
PRECEDENCE MAP
JAVA AND .NET COMPONENT MAPPING
FILES CHANGED
ROOT GUIDANCE
COMPONENT GUIDANCE
SUPPORTING DOCUMENTS
HIERARCHY AND LINK TEST EVIDENCE
BEHAVIORAL SCENARIO EVIDENCE
INSTRUCTION SIZE AND DUPLICATION CHECKS
FIVE-PASS REVIEW
COMMANDS EXECUTED AND RESULTS
MAINTENANCE AND OWNERSHIP
UNVERIFIED ITEMS
KNOWN LIMITATIONS
NEXT STEPS

For every command report exact command, working directory, exit code and concise result. Never claim inherited, enforced, compatible, passing or verified without evidence.
```

## Expected outcome

The lab should produce a compact, evidence-tested AGENTS.md hierarchy that gives every Java and .NET component the correct universal and local guidance without duplication or ambiguous precedence.

## Official reference

- [Custom instructions with AGENTS.md](https://learn.chatgpt.com/docs/agent-configuration/agents-md)
