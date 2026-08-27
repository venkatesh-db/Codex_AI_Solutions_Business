# Hands-on Lab 10: Contract, Reproduce, Minimal Patch — .NET

```text
Act as a senior .NET engineer investigating and fixing a narrowly scoped defect in an unfamiliar RxFlow lab-throughput analytics service.

Ambiguous requirement:
“The throughput report is sometimes wrong for labs that were offline part of the day.”

Your job is to convert this ambiguous statement into an explicit behavioural contract, reproduce the defect deterministically, write a failing regression test, identify the root cause, and implement the smallest safe patch supported by evidence.

Do not redesign or modernize the service.

Project context:
RxFlow processes events from optical laboratories, including:
- manufacturing started;
- surfacing;
- coating;
- completed;
- shipped;
- lab availability changes;
- lab offline and online periods.

The analytics service calculates metrics such as:
- jobs completed;
- available operating minutes;
- offline minutes;
- throughput per operating hour;
- utilization percentage;
- average processing time;
- performance by lab and reporting period.

Technology assumptions:
- .NET 8
- ASP.NET Core Web API
- C# 12
- Entity Framework Core
- PostgreSQL
- Redis
- Kafka or Redpanda
- BackgroundService or hosted workers
- xUnit
- FluentAssertions
- Moq or NSubstitute
- Testcontainers for .NET
- Docker Compose
- OpenTelemetry
- Serilog
- coverlet
- dotnet format
- nullable reference types enabled

Treat every technology above as an assumption until it is verified from repository evidence.

Permission boundary:
- Work only inside the current repository.
- Read all applicable AGENTS.md files before acting.
- Begin with read-only investigation.
- Do not deploy anything.
- Do not access production systems or production data.
- Do not use real patient or prescription data.
- Do not change public API contracts without approval.
- Do not change database schemas unless the defect cannot be fixed safely without doing so.
- Do not add or upgrade dependencies without asking first.
- Do not execute destructive Git, database, Redis, Kafka, Docker, or filesystem commands.
- Do not perform unrelated refactoring.
- Do not rewrite the analytics subsystem.
- Preserve existing formatting and architectural conventions.
- Run only safe local commands and tests.
- Never claim a test or quality check passed unless it was executed successfully.

Primary objective:
Produce a minimal, evidence-backed correction for incorrect throughput calculations when a lab was offline during part of the reporting period.

Required outcome:
1. A precise behavioural contract.
2. A deterministic reproduction.
3. A regression test that fails before the patch.
4. Evidence showing why it fails.
5. A minimal production-code patch.
6. Evidence that the new test passes after the patch.
7. Evidence that relevant existing tests still pass.
8. A concise risk and limitations report.

Important working rule:
Do not modify production code until you have demonstrated the defect with a failing automated test.

Phase 1 — Repository reconnaissance

1. Read repository instructions
- Locate every applicable AGENTS.md.
- Explain which instructions apply to the files likely involved.
- Inspect README files, architecture documents, ADRs, runbooks, and contribution guides.

2. Identify the actual stack
Inspect:
- solution and project files;
- global.json;
- Directory.Build.props;
- Directory.Build.targets;
- NuGet configuration;
- Dockerfiles;
- Docker Compose files;
- CI workflows;
- database migrations;
- application configuration;
- test projects.

Report the verified:
- .NET SDK version;
- target framework;
- test framework;
- persistence technology;
- build command;
- test command;
- formatting and static-analysis commands.

3. Locate the throughput calculation
Search for:
- throughput;
- utilization;
- capacity;
- availability;
- operating minutes;
- downtime;
- offline periods;
- reporting windows;
- event aggregation;
- lab status;
- completed jobs.

Identify:
- API entry point;
- request and response models;
- application service;
- domain calculation;
- repositories and queries;
- background consumers;
- cache usage;
- database entities;
- existing tests.

4. Trace the current data flow
Trace one throughput request from:
HTTP request, scheduled job, or event trigger
→ input parsing
→ report-window construction
→ laboratory availability lookup
→ manufacturing-event lookup
→ gap or interval calculation
→ aggregation
→ persistence or cache
→ response.

Cite repository-relative file paths, types, and methods.

Label findings as:
- VERIFIED;
- INFERRED;
- UNKNOWN.

Phase 2 — Contract the ambiguous requirement

Before writing code, convert the requirement into explicit rules.

At minimum, determine or propose answers for:

1. What is throughput?
Consider whether the system defines it as:

completed jobs / total reporting hours

or:

completed jobs / available operating hours.

2. What counts as offline?
Determine whether offline time comes from:
- explicit LAB_OFFLINE and LAB_ONLINE events;
- status snapshots;
- missing heartbeat events;
- scheduled operating hours;
- manual overrides;
- maintenance windows;
- another source.

3. How should offline time affect the denominator?
The preferred contract is:

available duration =
intersection of reporting window and scheduled operating window
minus the union of offline intervals within that window.

throughput per operating hour =
completed jobs within the reporting window
divided by available duration in hours.

Do not assume this is correct if repository evidence establishes another definition.

4. Required boundary behaviour
Define behaviour for:
- lab offline at the beginning of the report window;
- lab still offline at the end of the report window;
- offline interval entirely inside the window;
- interval beginning before and ending inside the window;
- interval beginning inside and ending after the window;
- interval spanning the entire window;
- multiple offline intervals;
- overlapping intervals;
- adjacent intervals;
- duplicated status events;
- out-of-order events;
- missing online event;
- missing offline event;
- zero available time;
- no manufacturing events;
- completed jobs while status says offline;
- daylight-saving transitions;
- different time zones;
- timestamps exactly on report boundaries.

5. Interval semantics
State whether the reporting window uses:
- `[from, to)`, inclusive start and exclusive end; or
- another convention verified from the codebase.

Prefer `[from, to)` when no existing contract is present, because adjacent windows then do not double-count boundary events.

6. Zero-availability behaviour
Determine whether zero available minutes should produce:
- throughput of zero;
- a null throughput;
- a structured “not applicable” result;
- a validation error.

Do not silently divide by zero.

Contract deliverable:
Write a short decision table containing:
- scenario;
- input interval;
- expected available minutes;
- expected completed-job count;
- expected throughput;
- evidence or assumption.

If a product decision is truly required and cannot be derived from the repository, stop before production changes and identify the exact unresolved decision.

Phase 3 — Reproduce deterministically

Create the smallest deterministic scenario that demonstrates the suspected defect.

Preferred reproduction:

Reporting window:
08:00–16:00 UTC = 480 total minutes.

Lab status:
- available from 08:00–12:00;
- offline from 12:00–14:00;
- available from 14:00–16:00.

Completed jobs:
- 12 jobs within the report window.

Expected:
- offline duration: 120 minutes;
- available duration: 360 minutes = 6 hours;
- throughput: 12 / 6 = 2.0 jobs per operating hour.

Incorrect implementation may calculate:
- 12 / 8 = 1.5 jobs per hour;
- 12 / 4 = 3.0 jobs per hour;
- another incorrect value caused by missing interval closure or double-counting.

Use fixed timestamps and a fixed timezone.
Do not use:
- DateTime.Now;
- DateTimeOffset.Now;
- Task.Delay;
- random test values;
- live Kafka;
- live Redis;
- shared external databases;
- timing-dependent polling.

If time is read internally, use the project’s existing clock abstraction. If none exists, avoid introducing one unless required for deterministic testing.

Phase 4 — Write the failing regression test

Add one focused regression test at the lowest reliable layer that exercises the defective calculation.

Test naming example:

CalculateThroughput_LabOfflineForPartOfWindow_ExcludesOfflineMinutesFromAvailableTime

The test must:
- use Arrange / Act / Assert;
- use fixed DateTimeOffset values;
- clearly show the reporting window;
- clearly show the offline interval;
- clearly show completed jobs;
- assert available minutes;
- assert offline minutes if exposed;
- assert throughput;
- avoid unrelated setup;
- fail for the suspected business reason.

Preferred assertion:

result.AvailableMinutes.Should().Be(360);
result.ThroughputPerOperatingHour.Should().Be(2.0);

If the public result does not expose available minutes, assert the nearest observable contract without expanding the API solely for the test.

Run the smallest relevant test command.

Capture:
- exact command;
- exit code;
- test name;
- expected result;
- actual result;
- assertion failure.

Confirm that the test fails because of the gap-handling defect, not because of:
- invalid setup;
- dependency injection failure;
- database connectivity;
- timezone parsing;
- missing test data;
- compilation errors;
- an unrelated exception.

Do not change production code until this evidence exists.

Phase 5 — Root-cause analysis

Identify the exact defect.

Investigate likely causes such as:
- denominator uses the full report duration;
- missing gaps interpreted as available time;
- missing gaps interpreted as offline time;
- offline intervals are not clipped to the reporting window;
- overlapping offline intervals are double-counted;
- open-ended offline intervals are ignored;
- intervals are subtracted before being merged;
- status events are processed in database return order instead of timestamp order;
- inclusive boundaries double-count minutes;
- local DateTime and UTC DateTimeOffset values are mixed;
- cached aggregates omit availability changes;
- Redis cache keys do not include the reporting window or data version;
- LINQ grouping drops labs with no events;
- SQL joins multiply completed jobs by status intervals;
- integer division truncates throughput;
- a left join is accidentally implemented as an inner join.

Root-cause deliverable:
Provide:
- defective file and method;
- faulty assumption;
- failing input;
- incorrect intermediate value;
- expected intermediate value;
- why existing tests did not catch it.

Do not speculate beyond the available evidence.

Phase 6 — Minimal patch

Implement the smallest safe change that satisfies the written contract and makes the regression test pass.

Preferred patch characteristics:
- localized to the throughput or interval calculation;
- no unrelated renaming;
- no dependency changes;
- no public API change;
- no schema migration;
- no broad abstraction;
- no formatting churn;
- no modification of unrelated tests.

If the defect involves interval handling, use a clear algorithm:

1. Clip every offline interval to the report window.
2. Discard intervals with no positive overlap.
3. Sort intervals by start time.
4. Merge overlapping or adjacent intervals.
5. Sum the merged offline duration.
6. Subtract it from scheduled duration.
7. Clamp available duration to a minimum of zero.
8. Apply the documented zero-availability behaviour.
9. Calculate throughput using decimal or double arithmetic without integer truncation.

Use `DateTimeOffset` for instants unless the existing domain deliberately uses another type.

Example interval overlap:

clippedStart = Max(offlineStart, reportStart)
clippedEnd = Min(offlineEnd, reportEnd)

The interval contributes only when:

clippedStart < clippedEnd

Do not silently correct malformed data unless that behaviour is already part of the domain contract. Prefer a clear validation or error path.

Phase 7 — Verification

Run verification in increasing scope.

1. New regression test
Run only the new test.
Confirm that it passes after the patch.

2. Related test class or project
Run the tests closest to the modified component.

3. Complete test suite
Run the repository’s verified full test command when practical.

4. Build
Run the verified build command.

5. Formatting and analysis
Run available repository checks, which may include:

dotnet format --verify-no-changes
dotnet build --configuration Release
dotnet test --configuration Release
dotnet test --collect:"XPlat Code Coverage"

Run only commands supported by repository evidence.

6. Diff review
Review the final diff for:
- minimality;
- unrelated changes;
- accidental public contract changes;
- timezone errors;
- boundary errors;
- privacy leaks;
- formatting churn;
- missing test assertions.

Do not claim success for checks that were not executed.

Phase 8 — Additional edge-case analysis

After the minimal patch passes, assess whether additional tests are needed for:
- an offline interval spanning the full report window;
- an open-ended offline interval;
- overlapping offline intervals;
- adjacent offline intervals;
- intervals outside the report window;
- zero available minutes;
- daylight-saving transitions;
- out-of-order status events.

Keep the required patch narrow. List additional cases separately unless one is essential to demonstrate that the implementation is safe.

Security and privacy requirements:
- Do not log patient identifiers.
- Do not log complete prescriptions.
- Use synthetic lab, event, and order identifiers in tests.
- Do not print environment-variable values.
- Do not include database passwords, connection strings, tokens, or Kafka credentials in the report.
- Do not weaken authentication or authorization.
- Do not expose internal exception details through API responses.

Required final report:

# SUMMARY

State whether the defect was reproduced and fixed.

# BEHAVIOURAL CONTRACT

Document:
- throughput formula;
- reporting-window semantics;
- offline interval handling;
- zero-availability behaviour;
- timezone rules.

# REPRODUCTION

Include:
- deterministic timestamps;
- completed-job count;
- expected value;
- pre-patch actual value.

# ROOT CAUSE

Identify:
- file;
- method;
- faulty logic;
- why it produced the wrong result.

# FAILING TEST EVIDENCE

Include:
- test name;
- exact pre-patch command;
- exit status;
- relevant assertion failure.

# MINIMAL PATCH

Explain:
- files changed;
- exact behavioural correction;
- why the patch is minimal.

# PASSING TEST EVIDENCE

Include:
- exact post-patch commands;
- exit status;
- tests executed;
- pass/fail counts.

# BUILD AND QUALITY CHECKS

List each check as:
- PASSED;
- FAILED;
- NOT RUN;
- BLOCKED.

# FINAL DIFF REVIEW

Confirm whether the diff contains unrelated changes.

# RISKS AND LIMITATIONS

Document:
- unverified edge cases;
- external services not exercised;
- performance implications;
- remaining ambiguity.

# FILES CHANGED

List every changed file and its purpose.

Success criteria:
- The ambiguous requirement becomes an explicit contract.
- The defect is reproduced with fixed timestamps.
- The new test demonstrably fails before production code changes.
- The failure is caused by offline-gap handling.
- The production patch is minimal.
- The test passes after the patch.
- Relevant existing tests remain green.
- Every conclusion is supported by repository or command evidence.

Begin with repository reconnaissance and the behavioural contract. Do not edit production code until the failing regression test has been executed and its failure recorded.
```

## Workflow summary

```text
Ambiguous requirement
→ explicit contract
→ deterministic reproduction
→ failing regression test
→ root cause
→ minimal patch
→ verification evidence
```

This sequence prevents implementation from beginning before the defect and expected behaviour are proven.
