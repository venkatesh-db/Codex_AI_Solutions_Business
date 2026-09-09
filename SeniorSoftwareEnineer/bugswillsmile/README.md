# Bug Will Smile — SQL injection fix evidence

Issue: Catalog search constructs SQL using untrusted search input  
Repository: `cartsvc-dotnet`  
Date: 2026-09-09  
Final status: **Locally verified; ready for review**  
Release status: **Not released**

This folder records the Senior Software Engineer workflow used to fix the catalog-search SQL-injection defect. It provides organizational evidence for the requirement, source inspection, plan, implementation, validation, self-review, and handoff.

It does not contain private model chain-of-thought. [AI decision log](01-understand/ai-decision-log.md) provides an auditable record of inputs, assumptions, decisions, actions, and evidence. [User thinking](01-understand/user-thinking.md) records only thinking explicitly supplied by the user.

## Evidence map

| Stage | Evidence | Result |
| --- | --- | --- |
| 1. Understand | [User thinking](01-understand/user-thinking.md), [AI decision log](01-understand/ai-decision-log.md) | Scope and proof requirements established |
| 2. Inspect | [Source evidence](02-inspect/source-evidence.md) | Interpolated SQL confirmed |
| 3. Plan | [Fix plan](03-plan/fix-plan.md) | Focused parameterization and regression approach selected |
| 4. Execute | [Code patch](04-execute/code-changes.patch), [implementation record](04-execute/implementation.md) | Source and existing test changed |
| 5. Validate | [Failing regression](05-validate/01-regression-before.txt), [focused pass](05-validate/02-focused-after.txt), [full pass](05-validate/03-full-suite-after.txt) | Defect reproduced, fixed, and suite passed 20/20 |
| 6. Review | [Self-review](06-review/self-review.md) | Scoped change reviewed; limitations recorded |
| 7. Report | [Delivery handoff](07-report/delivery-handoff.md) | Ready for peer review and QA |
| Audit | [Execution issues](audit/execution-issues.md) | Three orchestration errors and one evidence-count assertion disclosed |

## Acceptance-to-evidence summary

| Expected behavior | Check performed | Actual evidence | Status |
| --- | --- | --- | --- |
| Normal case-insensitive search remains functional | Focused integration test | `Coffee` returned for `COF` | Verified |
| SQL-shaped input is treated as text | Regression test with harmless input | Before fix returned `Coffee` and `Tea`; after fix returned an empty result | Verified |
| A quote does not break the SQL command | Focused integration test | Test completed successfully after parameterization | Verified |
| Existing behavior does not regress | Complete test suite | 20 passed, 0 failed | Verified |
| Production is corrected | Deployment and production check | Not performed or authorized | Unverified |

## Changed files

- [`CatalogService.cs`](../cartsvc-dotnet/src/CartSvc/CatalogService.cs)
- [`ServiceTests.cs`](../cartsvc-dotnet/tests/CartSvc.Tests/ServiceTests.cs)

Other seeded CartSvc training defects remain outside this fix.
