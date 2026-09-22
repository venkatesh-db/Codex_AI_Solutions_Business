# One-year data retention: 50 issue scenarios

These are **diagnostic scenarios inferred from documented storage behavior**, not 50 confirmed vendor bugs or 50 incidents in this repository. They target a one-year retention requirement across warehouses, object storage, key-value stores, and event logs. The exact cutoff, legal holds, and deletion guarantees must be set by the owning business and compliance teams.

**Documented incident:** [PostHog's February 2026 logs data-loss postmortem](https://github.com/PostHog/post-mortems/blob/main/2026-02-20-posthog-us-logs-data-loss.md) reports that a ClickHouse zero-copy replication bug deleted historical S3 data silently for hours. Hot-data dashboards stayed green, and Kafka's three-day retention limited recovery. This is direct evidence of a historical-data failure and a monitoring blind spot, but it does not establish a one-year retention requirement for this project.

| # | Observable symptom or failed retention expectation | Evidence for mechanism |
| ---: | --- | --- |
| 1 | One-year-old UTC partition disappears before local-calendar cutoff | [BigQuery partition boundaries](https://docs.cloud.google.com/bigquery/docs/managing-partitioned-tables) |
| 2 | Late-arriving rows are deleted as soon as inserted into an expired partition | [BigQuery partition boundaries](https://docs.cloud.google.com/bigquery/docs/managing-partitioned-tables) |
| 3 | Changing partition expiration immediately removes historical partitions | [BigQuery partition boundaries](https://docs.cloud.google.com/bigquery/docs/managing-partitioned-tables) |
| 4 | Table expiration deletes all partitions before the partition cutoff | [BigQuery partition boundaries](https://docs.cloud.google.com/bigquery/docs/managing-partitioned-tables) |
| 5 | Expired partitions still consume quota while background deletion runs | [BigQuery partition boundaries](https://docs.cloud.google.com/bigquery/docs/managing-partitioned-tables) |
| 6 | New tables expire while older tables in the same dataset remain | [BigQuery dataset defaults](https://docs.cloud.google.com/bigquery/docs/updating-datasets) |
| 7 | Changing the dataset default does not update existing table lifetimes | [BigQuery dataset defaults](https://docs.cloud.google.com/bigquery/docs/updating-datasets) |
| 8 | A table-level override defeats the intended one-year default | [BigQuery dataset defaults](https://docs.cloud.google.com/bigquery/docs/updating-datasets) |
| 9 | Seconds-versus-milliseconds conversion produces a wrong expiration | [BigQuery dataset defaults](https://docs.cloud.google.com/bigquery/docs/updating-datasets) |
| 10 | Unconfigured tables retain data indefinitely | [BigQuery dataset defaults](https://docs.cloud.google.com/bigquery/docs/updating-datasets) |
| 11 | Snapshot omits rows still in the streaming buffer | [BigQuery snapshots](https://docs.cloud.google.com/bigquery/docs/table-snapshots-intro) |
| 12 | Snapshot does not preserve source partition-expiration setting | [BigQuery snapshots](https://docs.cloud.google.com/bigquery/docs/table-snapshots-intro) |
| 13 | Snapshot expires before the one-year audit period ends | [BigQuery snapshots](https://docs.cloud.google.com/bigquery/docs/table-snapshots-intro) |
| 14 | Seven-day time travel cannot recover a year-old deletion | [BigQuery snapshots](https://docs.cloud.google.com/bigquery/docs/table-snapshots-intro) |
| 15 | Restored snapshot follows destination dataset defaults | [BigQuery snapshots](https://docs.cloud.google.com/bigquery/docs/table-snapshots-intro) |
| 16 | Object disappears from normal reads but old versions remain stored | [S3 versioned expiration](https://docs.aws.amazon.com/us_en/AmazonS3/latest/userguide/lifecycle-expire-general-considerations.html) |
| 17 | A delete marker appears instead of permanent data removal | [S3 versioned expiration](https://docs.aws.amazon.com/us_en/AmazonS3/latest/userguide/lifecycle-expire-general-considerations.html) |
| 18 | Storage cost stays high after current versions expire | [S3 versioned expiration](https://docs.aws.amazon.com/us_en/AmazonS3/latest/userguide/lifecycle-expire-general-considerations.html) |
| 19 | Expiry happens asynchronously after the cutoff | [S3 versioned expiration](https://docs.aws.amazon.com/us_en/AmazonS3/latest/userguide/lifecycle-expire-general-considerations.html) |
| 20 | Versioning mode changes the observable delete behavior | [S3 versioned expiration](https://docs.aws.amazon.com/us_en/AmazonS3/latest/userguide/lifecycle-expire-general-considerations.html) |
| 21 | Old versions remain past one year because no noncurrent rule exists | [S3 noncurrent versions](https://docs.aws.amazon.com/AmazonS3/latest/userguide/troubleshooting-versioning.html) |
| 22 | Delete markers accumulate after object expiry | [S3 noncurrent versions](https://docs.aws.amazon.com/AmazonS3/latest/userguide/troubleshooting-versioning.html) |
| 23 | Cleanup of current versions leaves noncurrent copies | [S3 noncurrent versions](https://docs.aws.amazon.com/AmazonS3/latest/userguide/troubleshooting-versioning.html) |
| 24 | A delete request succeeds but a version remains retrievable | [S3 noncurrent versions](https://docs.aws.amazon.com/AmazonS3/latest/userguide/troubleshooting-versioning.html) |
| 25 | Protected versions return access denied on permanent deletion | [S3 noncurrent versions](https://docs.aws.amazon.com/AmazonS3/latest/userguide/troubleshooting-versioning.html) |
| 26 | Retention cleanup receives 403 for a protected version | [S3 Object Lock](https://docs.aws.amazon.com/AmazonS3/latest/userguide/object-lock.html) |
| 27 | Legal hold prevents one-year purge | [S3 Object Lock](https://docs.aws.amazon.com/AmazonS3/latest/userguide/object-lock.html) |
| 28 | A delete marker hides a locked object without removing it | [S3 Object Lock](https://docs.aws.amazon.com/AmazonS3/latest/userguide/object-lock.html) |
| 29 | Compliance retention exceeds the requested purge date | [S3 Object Lock](https://docs.aws.amazon.com/AmazonS3/latest/userguide/object-lock.html) |
| 30 | Storage inventory shows protected historical versions after cleanup | [S3 Object Lock](https://docs.aws.amazon.com/AmazonS3/latest/userguide/object-lock.html) |
| 31 | Expired record remains queryable after its TTL timestamp | [DynamoDB TTL](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/TTL.html) |
| 32 | Cleanup finishes days after the one-year cutoff | [DynamoDB TTL](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/TTL.html) |
| 33 | Changing TTL into the future prevents deletion | [DynamoDB TTL](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/TTL.html) |
| 34 | A missing TTL attribute leaves a record indefinitely | [DynamoDB TTL](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/TTL.html) |
| 35 | Application query returns expired items until deletion completes | [DynamoDB TTL](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/TTL.html) |
| 36 | Old log segments remain until segment cleanup runs | [Kafka topic retention](https://kafka.apache.org/20/configuration/topic-level-configs/) |
| 37 | Consumer replay cannot read messages after retention cutoff | [Kafka topic retention](https://kafka.apache.org/20/configuration/topic-level-configs/) |
| 38 | Compaction retains records despite a time-based expectation | [Kafka topic retention](https://kafka.apache.org/20/configuration/topic-level-configs/) |
| 39 | Different topics retain different historical windows | [Kafka topic retention](https://kafka.apache.org/20/configuration/topic-level-configs/) |
| 40 | A wrong `retention.ms` value removes data before one year | [Kafka topic retention](https://kafka.apache.org/20/configuration/topic-level-configs/) |
| 41 | One-year cache key has no TTL and remains indefinitely | [Redis key expiry](https://redis.io/docs/latest/develop/using-commands/keyspace/) |
| 42 | Key expires earlier than the business record retention date | [Redis key expiry](https://redis.io/docs/latest/develop/using-commands/keyspace/) |
| 43 | Read returns missing data after TTL despite durable record existing | [Redis key expiry](https://redis.io/docs/latest/develop/using-commands/keyspace/) |
| 44 | Updated key keeps an old expiry when refresh logic omits it | [Redis key expiry](https://redis.io/docs/latest/develop/using-commands/keyspace/) |
| 45 | Key inventory and durable store disagree after expiry | [Redis key expiry](https://redis.io/docs/latest/develop/using-commands/keyspace/) |
| 46 | Primary record is deleted while backup copies remain | [Deletion pipeline and copies](https://cloud.google.com/blog/products/storage-data-transfer/deleting-your-data-in-google-cloud-platform) |
| 47 | Replica deletion lags the user-visible delete response | [Deletion pipeline and copies](https://cloud.google.com/blog/products/storage-data-transfer/deleting-your-data-in-google-cloud-platform) |
| 48 | Audit export retains an identifier after operational deletion | [Deletion pipeline and copies](https://cloud.google.com/blog/products/storage-data-transfer/deleting-your-data-in-google-cloud-platform) |
| 49 | Cleanup dashboard says complete before all copies age out | [Deletion pipeline and copies](https://cloud.google.com/blog/products/storage-data-transfer/deleting-your-data-in-google-cloud-platform) |
| 50 | One-year policy is applied inconsistently across storage tiers | [Deletion pipeline and copies](https://cloud.google.com/blog/products/storage-data-transfer/deleting-your-data-in-google-cloud-platform) |
