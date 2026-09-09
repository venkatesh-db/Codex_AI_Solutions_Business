# Feature delivery and code maintenance

## Requirements to implementation

Translate the ticket into observable acceptance conditions. Inspect existing behavior before deciding whether frontend, API, database, or background processing needs changes. Flag contradictions and important missing business rules early. Follow the team's interfaces and patterns; avoid adding layers merely to match a preferred style.

Trace a vertical slice: input → validation → business rule → state change → response → user feedback. Keep API contracts and client types aligned. Consider empty, loading, failure, and permission states for UI work; inspect actual rendering when the change materially affects interaction or layout. Include accessibility checks appropriate to changed controls. Use available specialized skills where they genuinely improve the task.

For data-changing backend work, inspect ownership, transaction boundaries, retry behavior, and migration compatibility where relevant. Verify server-side authorization rather than relying only on UI visibility. Keep sensitive values out of logs and reports. Do not create a broad security audit for an unrelated localized change.

Implement the smallest cohesive change. Add useful behavior-level tests, update affected documentation, and use the project's lint/build/test commands. Do not require new tests for trivial prose or formatting edits.

## Bug investigation

Capture expected behavior, actual behavior, reproduction conditions, and available diagnostics. Form a cause supported by code or a reproduction. Prefer a regression check that fails before the correction and passes afterward when feasible. If the original failure cannot be reproduced, say so and distinguish the proposed explanation from confirmed cause.

Do not weaken assertions, remove validation, or swallow exceptions just to make a check pass. Revisit the explanation when evidence contradicts it. Route live incidents to operations.md.

## Performance and maintainability

For performance claims, obtain a comparable baseline and remeasure the affected path under stated conditions. Separate latency, throughput, allocations, query count, and resource consumption. Do not infer improvement from shorter code or one noisy measurement.

For maintainability refactors, identify the concrete source of friction and preserve behavior. Keep architectural rewrites separate from a narrow feature or bug unless necessary. Broaden checks according to affected interfaces, not arbitrary test volume.

## Evidence and handoff

Record acceptance criteria against observed results. For UI changes include the relevant interaction or visual check; for APIs include response and failure behavior; for storage include relevant persisted state. Compile success alone is insufficient for business behavior.

Prepare a concise review description: problem, resulting behavior, important decision, tests performed, and remaining limitations. If QA work is required, give reproducible steps, expected outcomes, environment, and known exclusions. A local pass is not a production-release claim.
