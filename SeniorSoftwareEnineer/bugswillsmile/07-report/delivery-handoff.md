# Delivery handoff

## Outcome

Catalog search now treats the search value as query data rather than executable SQL syntax. A harmless SQL-shaped value reproduced the original failure, then passed after parameterization. The complete suite remains green at 20/20.

## Current state

- Implemented: yes.
- Locally verified: yes.
- Ready for peer review: yes.
- Ready for QA: yes, subject to the project's QA environment.
- Released: no.
- Production verified: no.

## QA handoff

Environment: isolated CartSvc PostgreSQL test schema.

Verify:

1. Search `COF`; expect `Coffee`.
2. Search `' OR 1=1 --`; expect no product results and no database error.
3. Search `'`; expect no product results and no database error.
4. Run the complete suite; expect 20 tests to pass.
5. Confirm expected product behavior for `%` and `_`; this remains a product decision.

## Developer handoff

Root cause: `CatalogService.Search()` interpolated caller input into `CommandText`.

Change: fixed SQL text now uses `@query`, and the contains-search value is supplied through an Npgsql parameter.

Evidence:

- Before: focused regression failed because SQL-shaped input returned `Coffee` and `Tea`.
- After: focused regression passed.
- Regression: complete suite passed 20/20.

## Release handoff

No deployment target or authorization was supplied. Before release, use the project's established process, confirm the exact revision and environment, run required CI/security checks, and verify normal and special-character search behavior after deployment.

## Impact record

- Risk addressed: untrusted catalog-search input changing SQL behavior.
- Engineering judgment: selected a localized parameterized-query correction and preserved the repository's 20-test constraint.
- Measured result: failing security regression changed to passing; complete suite passed 20/20.
- Collaboration still required: peer/security review and QA confirmation before release.
