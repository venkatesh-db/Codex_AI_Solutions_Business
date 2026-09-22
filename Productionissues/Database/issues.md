# Database: 50 documented defect signals

These entries are grounded in upstream release notes. Each line is a **failure signal inferred from a documented fix**, not a claim that it occurred in this repository or at a named customer. Follow the source for affected versions and exact conditions. The examples apply to systems in many industries that use this technology.

| # | Symptom or failure signal to investigate | Upstream evidence |
| ---: | --- | --- |
| 1 | One-byte buffer overread when examining invalidly-encoded strings that are claimed to be in GB18030 encoding | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 2 | Self-referential foreign keys on partitioned tables are not fully enforced. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 3 | Data loss when merging compressed BRIN summaries in `brin_bloom_union()` | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 4 | Unexpected “attribute has wrong type” errors in `UPDATE`, `DELETE`, and `MERGE` queries that use whole-row table references to views or functions in `FROM` | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 5 | `MERGE ... DO NOTHING` on a partitioned table reports an unknown action. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 6 | Failure in `INSERT` commands when the table has a `GENERATED` column of a domain data type and the domain's constraints disallow null values | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 7 | Nested CTE references in data-changing statements are parsed incorrectly. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 8 | Misprocessing of casts within the keys of JSON constructor expressions | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 9 | Parallel `array_agg()` on anonymous records can fail. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 10 | Array construction on `int2vector` or `oidvector` returns the wrong type. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 11 | Possible erroneous reports of invalid affixes while parsing Ispell dictionaries | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 12 | Adding a domain-typed column leaves existing rows null instead of the domain default. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 13 | Misbehavior when there are duplicate column names in a foreign key constraint's `ON DELETE SET DEFAULT` or `SET NULL` action | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 14 | Disallowed foreign-key changes produce a misleading error. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 15 | Error when resetting the `relhassubclass` flag of a temporary table that's marked `ON COMMIT DELETE ROWS` | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 16 | Dump and restore drops the `XMLSERIALIZE INDENT` option. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 17 | Premature evaluation of the arguments of an aggregate function that has both `FILTER` and `ORDER BY` (or `DISTINCT`) options | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 18 | Erroneous deductions from column `NOT NULL` constraints in the presence of outer joins | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 19 | Incorrect optimizations based on `IS [NOT] NULL` tests that are applied to composite values | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 20 | Planner misses multiple hashable array comparisons in one expression. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 21 | Incorrect table size estimate with low fill factor | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 22 | Bitmap heap scan can return dead tuples during concurrent vacuum. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 23 | Performance issues in GIN index search startup when there are many search keys | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 24 | Malformed BRIN operator class crashes instead of reporting an error. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 25 | Query cancellation does not interrupt a waiting Append subplan. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 26 | Active WAL-sender I/O is absent from `pg_stat_io` until exit. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 27 | Race condition in handling of `synchronous_standby_names` immediately after startup | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 28 | An in-query `io_combine_limit` change is handled inconsistently. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 29 | Infinite loop if `scram_iterations` is set to INT_MAX | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 30 | Possible crashes due to double transformation of `json_array()`'s subquery | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 31 | `pg_strtof()` can crash with a null end pointer. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 32 | Crash after out-of-memory in certain GUC assignments | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 33 | Crash when a Snowball stemmer encounters an out-of-memory condition | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 34 | Over-enthusiastic freeing of SpecialJoinInfo structs during planning | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 35 | An invalidated replication slot can be copied. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 36 | A standby restores a logical slot that is invalid after promotion. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 37 | Over-advancement of catalog xmin in “fast forward” mode of logical decoding | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 38 | Data loss when DDL operations that don't take a strong lock affect tables that are being logically replicated | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 39 | Incorrect reset of replication origin when an apply worker encounters an error but the error is caught and does not result in worker exit | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 40 | Crash in logical replication if the subscriber's partitioned table has a BRIN index | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 41 | Duplicate snapshot creation in logical replication index lookups | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 42 | Mixed-origin subscriptions can receive duplicate changes without warning. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 43 | Wrong checkpoint details in error message about incorrect recovery timeline choice | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 44 | Storage open operations run in the wrong order. | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 45 | Incorrect assertion in `pgstat_report_stat()` | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 46 | Overly-strict assertion in `gistFindCorrectParent()` | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 47 | Assertion failure in parallel vacuum when `maintenance_work_mem` has a very small value | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 48 | Rare assertion failure in standby servers when the primary is restarted | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 49 | “unexpected plan node type” error when a scrollable cursor is defined on a simple `SELECT `expression`` query | [release note](https://www.postgresql.org/docs/release/17.5/) |
| 50 | `pg_dump --clean` tries to drop individual index partitions. | [release note](https://www.postgresql.org/docs/release/17.5/) |
