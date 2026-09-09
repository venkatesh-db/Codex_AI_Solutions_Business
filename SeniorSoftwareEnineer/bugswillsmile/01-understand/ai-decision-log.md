# AI decision log

This is an audit-friendly decision record. It contains concise rationale and evidence, not private hidden chain-of-thought.

| Point | Input/evidence | Decision | Reason |
| --- | --- | --- | --- |
| Scope | The immediately preceding ticket was catalog SQL injection | Fix only `CatalogService.Search()` and its regression coverage | Keeps the change focused and avoids altering unrelated training defects |
| Repository | CartSvc project instructions require .NET 8, Npgsql, raw SQL, and 20 tests | Keep raw SQL visible, use an Npgsql parameter, and preserve 20 test cases | Satisfies both the security outcome and repository constraints |
| Proof strategy | Existing catalog test covered only case-insensitive search | Expand that existing test into a behavior-level regression instead of adding a new test case | Preserves the exact total of 20 tests |
| Safe reproduction | A harmless SQL-shaped search could change predicate behavior | Use `' OR 1=1 --` only against the isolated lab database | Demonstrates altered query behavior without destructive data mutation |
| Root cause | Source interpolated `query` into `CommandText` | Replace interpolation in command text with `@query` and bind `%{query}%` as data | Prevents input from becoming SQL syntax while preserving contains-search semantics |
| Validation | The new regression failed before the fix | Apply the source correction, re-run the focused test, then run all tests | Provides before/after evidence and regression coverage |
| Release | No target environment or deployment authority was supplied | Stop at locally verified and ready for review | Avoids unsupported release or production claims |

## Outcome

- Regression reproduced before the correction.
- Focused regression passed after the correction.
- Complete suite passed 20/20.
- No production claim was made.
