# RxFlow Corporate Training Lab Prompts

This document contains two standalone variants of the same training-lab prompt:

1. Java 21 and Spring Boot
2. .NET 8 and ASP.NET Core

Both variants preserve the same domain, ten seeded defects, participant experience,
instructor deliverables, and evidence-based acceptance criteria.

---

## Prompt A — Java 21 / Spring Boot

```text
Role: You are acting as principal architect for a corporate training lab,
not as a participant. Build RxFlow, a Java teaching repository. This is a
training artefact, not a product. Every design choice must serve the lab
exercise described below.

DOMAIN

Prescription-to-lens order and lab routing. An optician submits a
prescription and frame choice. The system validates the prescription against
what a lens can physically be ground to, prices it, routes the job to an Rx
lab by capability and live load, schedules surfacing and coating, and tracks
the order to shipment.

STACK

Java 21 · Spring Boot 3.x · Spring Web MVC · Jakarta Bean Validation · Jackson
· plain Java service layer · PostgreSQL with Spring Data JPA/Hibernate and
Liquibase · Redis with Spring Data Redis/Lettuce for cache and locks ·
background workers using Spring-managed executors or the repository's chosen
job abstraction · Kafka via Redpanda using Spring Kafka · JUnit 5, AssertJ,
Mockito, jqwik and Testcontainers · Maven Wrapper or Gradle Wrapper ·
Checkstyle, SpotBugs, PMD and OWASP dependency-check · Docker Compose ·
OpenTelemetry.

Choose either Maven or Gradle and use it consistently. Do not claim both are
supported. Pin Java and build-tool versions in repository-controlled files.

WHAT LAB 1 WILL ASK PARTICIPANTS TO ESTABLISH

Build the repository so each item below has a real, findable answer:

1. Module map and service boundaries — create at least five modules or
   packages with genuine boundaries, and violate at least one boundary
   somewhere in production code.

2. Full request path for POST /orders — controller → request DTO/validation →
   service → repository → background worker → Kafka event. Make it deep enough
   that tracing takes effort, but shallow enough to finish within 90 minutes.

3. Build, test, lint and static-analysis commands — they must ACTUALLY RUN.
   Include at least one stale README command that fails so “verified by
   running” differs from “inferred from documentation.”

4. Schema and migration state — Liquibase with four to six changesets. At
   least one changeset must lack a valid rollback definition while the others
   have working rollbacks.

5. Redis and worker usage — use Redis as both cache and lock storage. Maintain
   an in-flight counter using non-atomic read-modify-write. Configure a worker
   retry policy that amplifies load when a connector times out.

6. Outbound calls and timeouts — implement three or four external calls using
   one repository-standard Java HTTP client. At least one call has no timeout.
   At least one retry policy compounds with a caller's retry policy.

7. Authentication and authorization — token authentication protects most
   routes. The lab-override endpoint performs no authorization check.

8. Where sensitive data reaches logs — emit prescription values and patient
   identifiers in at least two places: one direct logging call and one
   indirect path through DTO/entity serialization, Lombok-generated toString,
   record rendering, or object formatting.

9. Five highest-risk files — make risk genuinely uneven. Include one large,
   untested pricing class of approximately 900 lines containing three copies
   of the discount rule, one concurrency-sensitive class, and one class using
   raw SQL.

10. Unknowns — leave two or three matters genuinely indeterminable from code
    alone, such as whether the caller or connector layer owns retries. An
    honest participant report must say “cannot determine.”

SEEDED DEFECTS

Seed exactly ten defects that surface across the training week. Participants
must not be told what they are:

1. Duplicate lens job when a slow submission is retried — no idempotency.
2. Prescription validation silently clamps out-of-range cylinder or axis.
3. Lab routing selects by static priority and ignores live capacity.
4. Worker retry amplifies connector-timeout load by approximately three times.
5. Redis in-flight counter drifts under concurrency.
6. Patient identifiers and prescriptions appear in application logs.
7. Lab-override endpoint has no authorization check.
8. Approximately 900-line pricing class has no tests and contains three
   copies of the discount rule.
9. One Liquibase changeset has no usable rollback path.
10. Ad-hoc reporting query is constructed with string concatenation or
    interpolation and is vulnerable to SQL injection.

DESIGN RULES

- The test suite must PASS while all ten defects remain live. For every defect,
  identify in the instructor guide which existing test appears as though it
  should catch the defect and explain precisely why it does not.

- Concurrency defects must perform realistic work inside the race window, such
  as pricing calculation, serialization, repository activity or an external
  request. Never use Thread.sleep, Awaitility delays or timing-only tricks to
  create the race.

- No source comment, test name, log message or documentation inside the
  participant repository may suggest that code is intentionally wrong. Do not
  add TODO, FIXME, “race here,” “vulnerable,” or equivalent hints. The code
  should look like ordinary work completed under deadline pressure.

- Every defect must be reproducible on demand or demonstrable with a one-line
  command.

- Include realistic mess: inconsistent naming between modules, one dead module
  or package, and one abandoned refactor that is only half applied.
  Undocumented must not mean clean.

- The README must be plausible and partly stale — the kind of document a team
  writes once and does not continuously maintain.

- Keep the defects confined to the isolated training repository. Never connect
  the training stack to real patient, prescription, corporate, production or
  shared infrastructure data. Use synthetic identifiers and fixtures only.

DELIVERABLES — PRODUCE THREE SEPARATE OUTPUTS; DO NOT MIX THEM

1. THE PARTICIPANT REPOSITORY

   The complete Java codebase. It must contain no hints, answer keys or
   instructor diagrams.

2. INSTRUCTOR.md

   Write this OUTSIDE the participant repository, in a sibling instructor
   directory. For each of the ten defects include:

   - exact file and line range;
   - one-line reproduction command;
   - expected output when the defect occurs;
   - the existing test that looks as though it should catch the defect;
   - the exact reason that test does not catch it.

3. INSTRUCTOR-DIAGRAMS.md

   Write this OUTSIDE the participant repository, in the same sibling
   instructor directory. Include exactly two Mermaid diagrams:

   - one whole-repository system-design diagram that marks the violated module
     boundary;
   - one POST /orders end-to-end code-flow diagram that marks every seeded
     defect located on that request path.

   Do not create one diagram per defect. Producing those diagrams is part of
   Lab 1 for participants and must not be pre-solved in the participant tree.

ACCEPTANCE — DO NOT PRESENT THE DELIVERABLES UNTIL ALL CONDITIONS HOLD

- docker compose up brings the full stack up from a clean checkout.
- The repository wrapper's test command passes with all ten defects live.
- The configured formatting, linting and static-analysis commands pass.
- Every reproduction command documented in INSTRUCTOR.md demonstrates its
  defect on three consecutive runs without sleep-based synchronization; paste
  the output of all three runs.
- POST /orders works end to end against the running stack.
- Liquibase applies all changesets successfully on a clean database.
- Both Mermaid diagrams render successfully and every file:line annotation
  points to a real location.
- Paste the actual command, exit code, elapsed time and complete relevant output
  for every acceptance command. Do not replace evidence with a summary.

Iterate until acceptance is met. Do not ask for confirmation between ordinary,
non-destructive local attempts. Stop and request approval before any external,
destructive, costly or scope-expanding action.

At the end, report:

- acceptance evidence;
- failures encountered during implementation;
- what changed after each failure;
- remaining assumptions or environmental limitations;
- exact locations of the participant repository and both instructor files.
```

---

## Prompt B — .NET 8 / ASP.NET Core

```text
Role: You are acting as principal architect for a corporate training lab,
not as a participant. Build RxFlow, a .NET teaching repository. This is a
training artefact, not a product. Every design choice must serve the lab
exercise described below.

DOMAIN

Prescription-to-lens order and lab routing. An optician submits a
prescription and frame choice. The system validates the prescription against
what a lens can physically be ground to, prices it, routes the job to an Rx
lab by capability and live load, schedules surfacing and coating, and tracks
the order to shipment.

STACK

.NET 8 · C# 12 · ASP.NET Core Web API · controllers or minimal APIs, chosen
once and used consistently · DataAnnotations or FluentValidation, chosen once
· plain C# application/domain service layer · PostgreSQL with EF Core and
Npgsql · EF Core migrations · Redis with StackExchange.Redis for cache and
locks · Hangfire workers backed by isolated local infrastructure · Kafka via
Redpanda using Confluent.Kafka · xUnit, FluentAssertions, NSubstitute or Moq,
FsCheck and Testcontainers for .NET · dotnet format, nullable reference types,
.NET analyzers with warnings as errors, and dotnet package vulnerability
checks · Docker Compose · OpenTelemetry.

Pin the .NET SDK in global.json and centralize package versions. Do not mix
multiple validation, mocking or background-job frameworks without a concrete
repository need.

WHAT LAB 1 WILL ASK PARTICIPANTS TO ESTABLISH

Build the repository so each item below has a real, findable answer:

1. Project map and service boundaries — create at least five projects,
   namespaces or modules with genuine boundaries, and violate at least one
   boundary somewhere in production code.

2. Full request path for POST /orders — endpoint/controller → request model and
   validation → application service → repository → Hangfire worker → Kafka
   event. Make it deep enough that tracing takes effort, but shallow enough to
   finish within 90 minutes.

3. Restore, build, test, format and analysis commands — they must ACTUALLY RUN.
   Include at least one stale README command that fails so “verified by
   running” differs from “inferred from documentation.”

4. Schema and migration state — EF Core with four to six migrations. At least
   one migration must have a missing or non-working Down method while the
   others have working downgrade behavior.

5. Redis and worker usage — use Redis as both cache and lock storage. Maintain
   an in-flight counter using non-atomic read-modify-write. Configure a
   Hangfire retry policy that amplifies load when a connector times out.

6. Outbound calls and timeouts — implement three or four external calls using
   HttpClient/IHttpClientFactory. At least one call has no explicit timeout.
   At least one retry policy compounds with a caller's or worker's retry policy.

7. Authentication and authorization — token authentication protects most
   endpoints. The lab-override endpoint performs no authorization check.

8. Where sensitive data reaches logs — emit prescription values and patient
   identifiers in at least two places: one direct ILogger call and one indirect
   path through structured logging, record rendering, object serialization or
   a generated ToString implementation.

9. Five highest-risk files — make risk genuinely uneven. Include one large,
   untested pricing class of approximately 900 lines containing three copies
   of the discount rule, one concurrency-sensitive class, and one class using
   raw SQL.

10. Unknowns — leave two or three matters genuinely indeterminable from code
    alone, such as whether the caller, worker or connector layer owns retries.
    An honest participant report must say “cannot determine.”

SEEDED DEFECTS

Seed exactly ten defects that surface across the training week. Participants
must not be told what they are:

1. Duplicate lens job when a slow submission is retried — no idempotency.
2. Prescription validation silently clamps out-of-range cylinder or axis.
3. Lab routing selects by static priority and ignores live capacity.
4. Hangfire retry amplifies connector-timeout load by approximately three times.
5. Redis in-flight counter drifts under concurrency.
6. Patient identifiers and prescriptions appear in application logs.
7. Lab-override endpoint has no authorization check.
8. Approximately 900-line pricing class has no tests and contains three
   copies of the discount rule.
9. One EF Core migration has no usable downgrade path.
10. Ad-hoc reporting query is constructed with string concatenation or
    interpolation and is vulnerable to SQL injection.

DESIGN RULES

- The test suite must PASS while all ten defects remain live. For every defect,
  identify in the instructor guide which existing test appears as though it
  should catch the defect and explain precisely why it does not.

- Concurrency defects must perform realistic work inside the race window, such
  as pricing calculation, serialization, repository activity or an external
  request. Never use Thread.Sleep, Task.Delay or timing-only tricks to create
  the race.

- No source comment, test name, log message or documentation inside the
  participant repository may suggest that code is intentionally wrong. Do not
  add TODO, FIXME, “race here,” “vulnerable,” or equivalent hints. The code
  should look like ordinary work completed under deadline pressure.

- Every defect must be reproducible on demand or demonstrable with a one-line
  command.

- Include realistic mess: inconsistent naming between projects, one dead
  project or namespace, and one abandoned refactor that is only half applied.
  Undocumented must not mean clean.

- The README must be plausible and partly stale — the kind of document a team
  writes once and does not continuously maintain.

- Keep the defects confined to the isolated training repository. Never connect
  the training stack to real patient, prescription, corporate, production or
  shared infrastructure data. Use synthetic identifiers and fixtures only.

DELIVERABLES — PRODUCE THREE SEPARATE OUTPUTS; DO NOT MIX THEM

1. THE PARTICIPANT REPOSITORY

   The complete .NET codebase. It must contain no hints, answer keys or
   instructor diagrams.

2. INSTRUCTOR.md

   Write this OUTSIDE the participant repository, in a sibling instructor
   directory. For each of the ten defects include:

   - exact file and line range;
   - one-line reproduction command;
   - expected output when the defect occurs;
   - the existing test that looks as though it should catch the defect;
   - the exact reason that test does not catch it.

3. INSTRUCTOR-DIAGRAMS.md

   Write this OUTSIDE the participant repository, in the same sibling
   instructor directory. Include exactly two Mermaid diagrams:

   - one whole-repository system-design diagram that marks the violated project
     or module boundary;
   - one POST /orders end-to-end code-flow diagram that marks every seeded
     defect located on that request path.

   Do not create one diagram per defect. Producing those diagrams is part of
   Lab 1 for participants and must not be pre-solved in the participant tree.

ACCEPTANCE — DO NOT PRESENT THE DELIVERABLES UNTIL ALL CONDITIONS HOLD

- docker compose up brings the full stack up from a clean checkout.
- dotnet restore succeeds using the pinned SDK and package versions.
- dotnet build succeeds with warnings treated as errors.
- dotnet test passes with all ten defects live.
- dotnet format --verify-no-changes and the configured analyzers pass.
- The package vulnerability check completes and its policy-defined threshold
  passes.
- Every reproduction command documented in INSTRUCTOR.md demonstrates its
  defect on three consecutive runs without delay-based synchronization; paste
  the output of all three runs.
- POST /orders works end to end against the running stack.
- EF Core applies all migrations successfully to a clean database.
- Both Mermaid diagrams render successfully and every file:line annotation
  points to a real location.
- Paste the actual command, exit code, elapsed time and complete relevant output
  for every acceptance command. Do not replace evidence with a summary.

Iterate until acceptance is met. Do not ask for confirmation between ordinary,
non-destructive local attempts. Stop and request approval before any external,
destructive, costly or scope-expanding action.

At the end, report:

- acceptance evidence;
- failures encountered during implementation;
- what changed after each failure;
- remaining assumptions or environmental limitations;
- exact locations of the participant repository and both instructor files.
```
