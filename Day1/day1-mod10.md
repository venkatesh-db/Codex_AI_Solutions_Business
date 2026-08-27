# Lab 10 — Contract, Reproduce and Minimally Patch Offline-Lab Throughput

Use this prompt in Codex from the root of the RxFlow Java/.NET analytics repository.

```text
Act as a principal analytics engineer and senior Java/.NET defect investigator. Resolve this narrowly scoped RxFlow defect:

> “The throughput report is sometimes wrong for labs that were offline part of the day.”

Do not guess the intended formula or immediately edit code. First convert the ambiguity into an explicit business contract, identify the implemented behavior, reproduce the gap-handling defect deterministically, add a regression test that fails for the expected reason, implement the smallest safe correction, and provide before/after evidence.

## Success criteria

The task is complete only when:

- “offline,” “throughput,” “reporting window” and “gap” have precise definitions;
- the defect reproduces deterministically without sleeps or wall-clock dependence;
- the regression test fails on the original behavior for the intended reason;
- exactly one minimal correction makes the regression pass;
- existing relevant tests still pass;
- boundary, time-zone, duplicate and compatibility risks are evaluated;
- every claim is backed by executed evidence or marked UNVERIFIED.

## Permission boundary

- Work only inside the current repository and obey every applicable AGENTS.md.
- Inspect before editing. Preserve unrelated user changes.
- Do not deploy, push, merge, publish, access production or mutate shared databases/queues.
- Use local fixtures, test doubles, Testcontainers or existing isolated test infrastructure only.
- Use synthetic lab/job/event identifiers; never expose patient identifiers or prescriptions.
- Ask before adding or upgrading a dependency.
- Do not refactor unrelated analytics code, rename public models or rewrite the pipeline.
- Do not change API/event/database contracts unless evidence proves the minimal fix requires it and compatibility is approved.
- Do not “fix” the test by loosening assertions, changing fixtures to avoid the defect or suppressing errors.
- Do not claim the original test failed or the patch passed unless those commands were executed.

## Supported stacks

Discover actual repository tools.

Java possibilities:

- Java toolchain pinned by Maven/Gradle;
- Spring Boot/Spring Batch/Spring Kafka;
- JUnit 5, AssertJ/Mockito and Testcontainers;
- JPA/JDBC/jOOQ/PostgreSQL;
- Arrow/Parquet/DuckDB/Tablesaw or repository analytics library;
- `java.time` and injected `Clock`.

.NET possibilities:

- SDK pinned by global.json;
- ASP.NET Core/Worker Service;
- xUnit/NUnit/MSTest and Testcontainers;
- EF Core/Dapper/Npgsql/PostgreSQL;
- Microsoft.Data.Analysis/Arrow/Parquet/DuckDB.NET or repository analytics library;
- `DateTimeOffset`, `TimeZoneInfo` and injected `TimeProvider`/clock abstraction.

Do not add the missing stack or replace the existing time/data library.

## Phase 1 — Repository intelligence

Before proposing a contract:

1. Read instructions, README/runbooks, API/event schemas and analytics definitions.
2. Locate report endpoint/job/CLI, service layer, aggregation logic, queries, data-frame operations and output model.
3. Locate lab availability/offline/calendar/maintenance data and its source of truth.
4. Locate tests for throughput, reporting windows, availability, time zones and gaps.
5. Verify actual build/test commands from wrappers, CI and project files.
6. Trace one request/job end to end from input window to output.
7. Record current Git status and candidate files; do not disturb unrelated changes.

Report verified facts separately from inferences and missing product decisions.

## Phase 2 — Contract the ambiguity

Identify and resolve these questions from repository evidence, tests, runbooks or user-approved assumptions:

### Throughput definition

Determine whether throughput means:

- completed jobs per elapsed wall-clock hour;
- completed jobs per online/available hour;
- count only, with no time denominator;
- average/percentile processing time;
- stage-transition rate;
- another explicitly documented metric.

Do not combine distinct metrics under one name.

### Offline definition

Determine whether a lab is offline when:

- explicit availability intervals say offline;
- heartbeat/telemetry is absent beyond a threshold;
- maintenance calendar marks closure;
- connector reports unavailable;
- no events exist;
- another rule applies.

Absence of jobs is not automatically offline. Absence of telemetry is not automatically zero throughput unless the contract says so.

### Time-window rules

Define:

- inclusive/exclusive interval convention, preferably `[from, to)` if repository-compatible;
- time zone used for input, storage, grouping and output;
- DST behavior;
- partial buckets at start/end;
- offline intervals partially outside the report window;
- overlapping/adjacent/duplicate offline intervals;
- labs offline for the entire window;
- zero-duration or invalid windows;
- online time equal to zero;
- events exactly on boundaries;
- late/out-of-order/replayed events;
- duplicate job/stage events;
- rounding and numeric precision;
- stale/missing availability data.

### Candidate contracts

Present at least two plausible interpretations with examples and business impact. Select one only when supported by evidence or explicitly label it an assumption requiring approval.

Example for discussion—not an imposed answer:

```text
report window: 08:00–12:00 = 240 minutes
offline: 09:00–10:00 = 60 minutes
completed unique jobs: 90
online minutes: 180

wall-clock rate = 90 / 4.0 = 22.5 jobs/hour
online-time rate = 90 / 3.0 = 30.0 jobs/online-hour
```

The contract must state which value is correct and why.

Produce a concise contract table:

| Case | Input window | Availability | Events | Expected result | Rationale |
|---|---|---|---|---|---|

Include normal, partial-offline, fully-offline, multiple-gap and boundary cases.

If evidence cannot resolve a material business decision, stop before application edits and ask for that decision. You may still prepare a failing characterization test labeled with the unresolved assumption.

## Phase 3 — Identify the defect mechanism

Trace how the current implementation handles gaps. Look for:

- resampling/fill-forward/fill-zero behavior;
- missing time buckets dropped from a denominator;
- offline minutes incorrectly counted as online;
- treating no events as offline or vice versa;
- union/intersection mistakes for offline intervals;
- double subtraction of overlapping gaps;
- SQL joins that remove empty buckets;
- integer division or premature rounding;
- local/UTC conversion errors;
- end-boundary double counting;
- grouping by event count instead of unique job;
- duplicate/replay amplification;
- cache key omitting time zone/window/availability version;
- stale materialized view/cache;
- data-frame null semantics differing between implementations.

Write one falsifiable root-cause hypothesis with exact file/class/method/query evidence before adding the regression test.

## Phase 4 — Deterministic reproduction

Construct the smallest fixture that exposes the suspected gap defect.

Requirements:

- fixed timestamps with explicit offset/time zone;
- injected deterministic clock/TimeProvider if “now” is involved;
- no sleeps, polling races or dependence on the current date;
- synthetic lab and job IDs;
- explicit reporting window;
- explicit offline intervals;
- explicit completed/non-completed/duplicate events;
- isolated in-memory or Testcontainers storage according to existing test architecture;
- assertion on the public service/API/report output when practical, not only a private helper;
- expected value computed transparently from the agreed contract.

First add a characterization test if necessary to preserve unrelated existing behavior. Then add one focused regression test for the gap defect.

Execute the focused test against the unpatched implementation and capture:

- exact command;
- exit code;
- failing assertion;
- actual vs expected value;
- evidence that failure is caused by gap handling rather than setup/build errors.

If it unexpectedly passes, do not implement a speculative patch. Revisit the hypothesis and fixture.

## Phase 5 — Minimal safe patch

Implement only the smallest change that satisfies the contracted behavior.

Prefer correcting the existing interval/denominator/query transformation at its owning layer. Avoid:

- broad ETL rewrites;
- new frameworks/dependencies;
- public schema changes;
- database migrations;
- cache redesign;
- unrelated formatting/refactoring;
- duplicated Java/.NET logic when a shared contract or test fixture already exists.

The patch must:

- use half-open interval arithmetic consistently when compatible;
- clip offline intervals to the report window;
- normalize/merge overlapping or adjacent intervals when required;
- never subtract more than the report duration;
- handle zero online duration explicitly without divide-by-zero/NaN/Infinity leakage;
- preserve unique-job/idempotency semantics;
- preserve numeric precision and documented rounding;
- keep internal UTC and presentation-zone responsibilities clear;
- avoid loading unbounded event history into memory;
- preserve cancellation/async behavior;
- avoid sensitive logging.

If both Java and .NET implement the same report, create a language-neutral contract fixture and verify both implementations. Patch only the defective implementation unless both independently violate the same accepted contract.

## Tests after the patch

At minimum verify:

1. no offline gap;
2. one partial-day gap;
3. multiple disjoint gaps;
4. overlapping/adjacent gaps;
5. gap clipped at report start/end;
6. fully offline window;
7. zero completed jobs;
8. event exactly at start and end boundary;
9. duplicate/replayed completion;
10. stale/missing availability according to contract;
11. non-UTC zone and a DST transition if local-day reports are supported;
12. existing successful throughput behavior;
13. API/serialization numeric behavior;
14. cached versus uncached result if cache participates.

Use parameterized/theory tests where they clarify the contract. Do not add a large matrix unrelated to the defect.

## Stack-specific verification

Use repository-discovered commands.

Java examples:

- focused Maven: `./mvnw --batch-mode --no-transfer-progress -Dtest=<TestClass> test`;
- full Maven: `./mvnw --batch-mode --no-transfer-progress verify`;
- focused Gradle: `./gradlew <module>:test --tests '<TestClass>' --no-daemon`;
- full Gradle: `./gradlew check --no-daemon`.

.NET examples:

- focused: `dotnet test <project> --filter <fully-qualified-filter> --configuration Release`;
- full: `dotnet test <solution> --configuration Release`;
- build: `dotnet build <solution> --configuration Release`;
- format: `dotnet format <solution> --verify-no-changes`.

Run configured static analysis, coverage or dependency checks only as applicable. Do not alter repository settings to force green results.

## Compatibility and risk review

Review:

- public response field meaning and numeric serialization;
- historical report changes caused by recomputation;
- Java/.NET parity;
- database query/index impact;
- cache invalidation/versioning;
- materialized reports/backfill needs;
- performance for long windows/many gaps;
- concurrency and repeatability;
- API/event schema compatibility;
- dashboards/alerts whose thresholds assumed old semantics;
- rollout and rollback.

Clearly distinguish fixing incorrect results from intentionally changing a public metric definition.

## Evidence and diff discipline

Capture:

- pre-patch failing test evidence;
- minimal code diff;
- post-patch focused test;
- relevant neighboring tests;
- complete applicable suite;
- build/format/static checks;
- `git diff --check`;
- final diff/stat and unrelated-change confirmation.

Do not claim the regression test “failed before” if it was only reasoned about. If preserving a literal pre-patch state is difficult, use a temporary worktree or record the command before editing; do not destroy user changes.

## Five-pass review

Before finalizing, perform:

1. Contract review: formulas, examples, assumptions and terminology agree.
2. Time/data review: intervals, boundaries, zones, DST, gaps, duplicates and missing data.
3. Patch review: smallest owning-layer change, no unrelated behavior.
4. Test review: pre-fix sensitivity, deterministic assertions and negative/boundary coverage.
5. Compatibility/operations review: API, cache, database, historical reports, rollout and rollback.

Resolve critical/high findings or stop and report the blocker.

## Rollback plan

Provide:

- exact code/config rollback steps;
- whether cached/materialized results must be invalidated or rebuilt;
- historical reports affected;
- mixed-version behavior;
- verification query/test after rollback;
- metrics/logs to monitor;
- trigger and owner for rollback;
- conditions under which rollback would restore known incorrect behavior and a forward-fix is safer.

## Definition of done

- accepted contract is explicit and exemplified;
- root cause is tied to code evidence;
- reproduction is deterministic;
- focused regression fails before patch for the correct reason;
- minimal patch makes it pass;
- relevant boundaries and existing behavior pass;
- Java/.NET parity is checked when applicable;
- no unrelated files/behavior changed;
- compatibility, performance, cache and historical-report impact are assessed;
- rollback is actionable;
- every claim has command/file evidence or an UNVERIFIED label.

## Final report

Return exactly:

SUMMARY
AMBIGUITIES AND ACCEPTED CONTRACT
CONTRACT EXAMPLES
CURRENT IMPLEMENTATION
ROOT CAUSE
DETERMINISTIC REPRODUCTION
PRE-PATCH FAILING TEST EVIDENCE
MINIMAL PATCH
FILES CHANGED
POST-PATCH FOCUSED TEST EVIDENCE
JAVA VERIFICATION
.NET VERIFICATION
BOUNDARY AND FAULT CASES
COMPATIBILITY AND HISTORICAL-DATA IMPACT
PERFORMANCE AND CACHE IMPACT
FIVE-PASS REVIEW
COMMANDS EXECUTED AND RESULTS
ROLLBACK PLAN
UNVERIFIED ITEMS
KNOWN LIMITATIONS
NEXT STEPS

For every command report exact command, working directory, exit code and concise result. Never claim fixed, correct, compatible, passing or verified without evidence.
```

## Expected outcome

The lab should turn one ambiguous sentence into an explicit analytics contract, a deterministic red test, one narrow production-safe correction and traceable before/after evidence for Java, .NET or both.
