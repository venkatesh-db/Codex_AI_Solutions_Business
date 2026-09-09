# Focused fix plan

## Intended observable outcome

Catalog search must treat the complete search value as data. Normal case-insensitive substring search must continue to work, while quotes and SQL-shaped input must not modify the query predicate or cause a syntax error.

## Implementation

1. Expand the existing catalog integration test so the total suite remains 20 tests.
2. Demonstrate the original failure using a harmless SQL-shaped value in the isolated PostgreSQL test schema.
3. Replace interpolated SQL command text with a fixed command containing `@query`.
4. Bind the contains-search value through an Npgsql parameter.
5. Run the focused regression and the complete test suite.
6. Review the final patch and prepare QA/release handoff evidence.

## Acceptance checks

| Criterion | Planned check |
| --- | --- |
| Normal search preserved | Search `COF` returns `Coffee` |
| SQL-shaped input is data | Search `' OR 1=1 --` returns no matches |
| Quote is safe | Search `'` completes and returns no matches |
| Existing behavior preserved | Complete suite reports 20/20 passing |

## Risk and rollback

The change is localized to query construction and its integration test. It preserves `%value%` substring semantics. If review identifies an unintended behavior change, reverting the two-line query change restores the previous implementation, but would also restore the vulnerability. Production rollback was not assessed because no release was performed.
