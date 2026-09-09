# Multi-ticket delivery and career evidence

Use this mode when the user owns several tickets across different repositories and needs daily prioritization, deadline guidance, or evidence of impact.

## Keep repositories isolated

Maintain one context record per repository: path, applicable instructions, stack, setup commands, test commands, current branch, relevant services, and release procedure. Inspect each repository independently. Never transfer code conventions, commands, credentials, or conclusions from one repository to another without evidence.

For each ticket capture: outcome, client concern, ticket wording, observed behavior, priority, deadline, dependencies, current state, next evidence-producing action, risk, and owner of any external dependency. Treat the ticket's proposed solution as a hypothesis until it matches the client outcome and system evidence.

## Prioritize daily

Rank work using concrete factors rather than ticket order:

1. Active production impact, security exposure, or data integrity risk.
2. Deadline and cost of delay.
3. Work blocking QA, release, or another team.
4. Confidence and size of the next deliverable slice.
5. Dependency timing and availability of required people or environments.

Do not invent a numeric priority formula when the company already has severity and priority rules. When two items conflict, state the tradeoff and recommend one. The user or authorized product/incident owner decides business priority.

Limit active implementation work. Keep one primary ticket and at most one secondary ticket whose next step does not conflict with the primary. Other tickets should have an explicit next action or waiting dependency. This reduces context switching while still allowing progress during external waits.

## Guide deadlines

Estimate after focused inspection. Separate active engineering effort from waiting time and break the forecast into investigation, implementation, validation/review, and release dependency. Use a range with confidence and assumptions. Give the next reliable checkpoint before promising completion.

Update the forecast when scope, cause, environment, or dependency evidence changes. Communicate: previous forecast, new evidence, revised range, impact, and recovery options. Never compress required correctness checks merely to preserve an outdated date.

Use these statuses precisely: intake, investigating, solution-aligned, implementing, locally-verified, ready-for-review, in-review, ready-for-QA, release-ready, released, production-verified, blocked. A code-complete status must not imply release.

## Automate the repeatable work

For each ticket, automate repository inspection, relevant test discovery, reproduction setup, focused implementation, validation output capture, diff review, PR/QA/release draft preparation, and status synthesis when tools and authorization permit. Reuse verified repository context and avoid repeating unchanged setup checks.

Do not automatically send messages, merge, deploy, or mutate production without corresponding authorization. Do not schedule daily runs unless the user requests a schedule and supplies the required data sources.

## Record impact for appraisal

Maintain evidence of outcomes, not claims about effort or superiority. Record the problem, action and judgment, measurable result, scope, collaborators, and evidence link. Useful measures include client incidents resolved, lead-time reduction, escaped defects prevented, reliability improvement, measured performance change, repeated manual work eliminated, and engineers unblocked.

Do not attribute a team result solely to the user. Distinguish direct contribution, leadership, and collaboration. Do not infer money saved without a supported calculation. Never claim that the ledger guarantees a promotion or salary increase.

Create a weekly summary from actual ticket records: delivered outcomes, production/support contribution, quality improvements, cross-team leadership, risks surfaced early, mentoring, and next priorities. Produce performance-review material only from recorded evidence.

Use [daily-ticket-cockpit.md](../assets/daily-ticket-cockpit.md) for the five-ticket view and [impact-ledger.md](../assets/impact-ledger.md) for career evidence.
