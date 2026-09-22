# Kafka: 50 documented defect signals

These entries are grounded in upstream release notes. Each line is a **failure signal inferred from a documented fix**, not a claim that it occurred in this repository or at a named customer. Follow the source for affected versions and exact conditions. The examples apply to systems in many industries that use this technology.

| # | Symptom or failure signal to investigate | Upstream evidence |
| ---: | --- | --- |
| 1 | Partitions of topics being deleted show up in the offline partitions metric | [KAFKA-8366](https://issues.apache.org/jira/browse/KAFKA-8366) |
| 2 | Misleading exception message for non-existant partition | [KAFKA-8862](https://issues.apache.org/jira/browse/KAFKA-8862) |
| 3 | Rebalancing a restoring or running task may cause directory livelocking with newly created task | [KAFKA-12679](https://issues.apache.org/jira/browse/KAFKA-12679) |
| 4 | In-memory store iterators can return results with null values | [KAFKA-14460](https://issues.apache.org/jira/browse/KAFKA-14460) |
| 5 | Committed stream offsets omit the message leader epoch. | [KAFKA-15344](https://issues.apache.org/jira/browse/KAFKA-15344) |
| 6 | AsyncKafkaConsumer sometimes complains “No current assignment for partition {}” | [KAFKA-16022](https://issues.apache.org/jira/browse/KAFKA-16022) |
| 7 | Repeated leader-offset range lookups add consumer overhead. | [KAFKA-16248](https://issues.apache.org/jira/browse/KAFKA-16248) |
| 8 | Max.block.ms behavior inconsistency with javadoc and the config description | [KAFKA-16372](https://issues.apache.org/jira/browse/KAFKA-16372) |
| 9 | Dynamic broker reconfiguration can block on an unnecessary lock. | [KAFKA-16649](https://issues.apache.org/jira/browse/KAFKA-16649) |
| 10 | Interrupted consumer close can skip the leave-group request. | [KAFKA-16985](https://issues.apache.org/jira/browse/KAFKA-16985) |
| 11 | Unknown telemetry state: TERMINATED thrown when closing AsyncKafkaConsumer | [KAFKA-17040](https://issues.apache.org/jira/browse/KAFKA-17040) |
| 12 | New consumer assignment lags because background state is not updated. | [KAFKA-17064](https://issues.apache.org/jira/browse/KAFKA-17064) |
| 13 | Fetch-position updates block or diverge from background consumer state. | [KAFKA-17066](https://issues.apache.org/jira/browse/KAFKA-17066) |
| 14 | LogEndOffset could be lost due to log cleaning | [KAFKA-17076](https://issues.apache.org/jira/browse/KAFKA-17076) |
| 15 | Operators cannot list registered KRaft nodes for cleanup. | [KAFKA-17094](https://issues.apache.org/jira/browse/KAFKA-17094) |
| 16 | StreamThread shutdown calls completeShutdown only in CREATED state | [KAFKA-17112](https://issues.apache.org/jira/browse/KAFKA-17112) |
| 17 | New consumer may not send effective leave group if member ID received after close | [KAFKA-17116](https://issues.apache.org/jira/browse/KAFKA-17116) |
| 18 | New consumer subscribe may join group without a call to consumer.poll | [KAFKA-17154](https://issues.apache.org/jira/browse/KAFKA-17154) |
| 19 | Possible partial write in writing snapshot | [KAFKA-17181](https://issues.apache.org/jira/browse/KAFKA-17181) |
| 20 | Async consumer fetch sessions are evicted too quickly. | [KAFKA-17182](https://issues.apache.org/jira/browse/KAFKA-17182) |
| 21 | LoginManager ctor might throw an exception causing login and loginCallbackHandler not being closed properly | [KAFKA-17188](https://issues.apache.org/jira/browse/KAFKA-17188) |
| 22 | Kafka consumer client doesn't report node request-latency metrics | [KAFKA-17230](https://issues.apache.org/jira/browse/KAFKA-17230) |
| 23 | Mirror checkpoint connector makes excessive group-offset requests. | [KAFKA-17233](https://issues.apache.org/jira/browse/KAFKA-17233) |
| 24 | Delayed request completion throws an exception. | [KAFKA-17234](https://issues.apache.org/jira/browse/KAFKA-17234) |
| 25 | Kafka admin client doesn't report node request-latency metrics | [KAFKA-17239](https://issues.apache.org/jira/browse/KAFKA-17239) |
| 26 | `kafka-topics.sh` reports confusing usage guidance. | [KAFKA-17262](https://issues.apache.org/jira/browse/KAFKA-17262) |
| 27 | New group coordinator can return REQUEST_TIMED_OUT for OFFSET_FETCHes | [KAFKA-17267](https://issues.apache.org/jira/browse/KAFKA-17267) |
| 28 | NPE when closing a non-started acceptor | [KAFKA-17268](https://issues.apache.org/jira/browse/KAFKA-17268) |
| 29 | Disconnected group coordinator is not rediscovered by heartbeat manager. | [KAFKA-17293](https://issues.apache.org/jira/browse/KAFKA-17293) |
| 30 | Retriable offset-fetch error stops the new consumer. | [KAFKA-17294](https://issues.apache.org/jira/browse/KAFKA-17294) |
| 31 | Kafka Streams consumer stops consumption | [KAFKA-17299](https://issues.apache.org/jira/browse/KAFKA-17299) |
| 32 | `kafka-configs.sh` lacks the expected `--client-metrics` option. | [KAFKA-17347](https://issues.apache.org/jira/browse/KAFKA-17347) |
| 33 | New consumer may not send leave group on race condition on network thread run & close | [KAFKA-17403](https://issues.apache.org/jira/browse/KAFKA-17403) |
| 34 | Overflow of expiried timestamp | [KAFKA-17415](https://issues.apache.org/jira/browse/KAFKA-17415) |
| 35 | DefaultStateUpdater/DefaultTaskExecutor shutdown returns before threads have fully exited | [KAFKA-17432](https://issues.apache.org/jira/browse/KAFKA-17432) |
| 36 | Consumer polling fails to trigger retrieval of new records. | [KAFKA-17439](https://issues.apache.org/jira/browse/KAFKA-17439) |
| 37 | A seek operation leaves consumer position stale. | [KAFKA-17448](https://issues.apache.org/jira/browse/KAFKA-17448) |
| 38 | `TaskCorruptedException` After Client Quota Throttling | [KAFKA-17455](https://issues.apache.org/jira/browse/KAFKA-17455) |
| 39 | One failed commit response is counted multiple times. | [KAFKA-17470](https://issues.apache.org/jira/browse/KAFKA-17470) |
| 40 | Wrong configuration of metric.reporters lead to NPE in KafkaProducer constructor | [KAFKA-17478](https://issues.apache.org/jira/browse/KAFKA-17478) |
| 41 | Commit-all operation retrieves offsets on the wrong thread. | [KAFKA-17480](https://issues.apache.org/jira/browse/KAFKA-17480) |
| 42 | Broker registration with zero minimum feature version is rewritten incorrectly. | [KAFKA-17492](https://issues.apache.org/jira/browse/KAFKA-17492) |
| 43 | Seek-to-beginning or seek-to-end runs on the wrong thread. | [KAFKA-17505](https://issues.apache.org/jira/browse/KAFKA-17505) |
| 44 | Transaction marker API returns before markers are durable in coordinator cache. | [KAFKA-17507](https://issues.apache.org/jira/browse/KAFKA-17507) |
| 45 | AsyncKafkaConsumer cannot reliably leave group when closed with small timeout | [KAFKA-17518](https://issues.apache.org/jira/browse/KAFKA-17518) |
| 46 | Interrupted `Consumer.close()` violates its expected timeout behavior. | [KAFKA-17519](https://issues.apache.org/jira/browse/KAFKA-17519) |
| 47 | Task-manager shutdown stalls. | [KAFKA-17553](https://issues.apache.org/jira/browse/KAFKA-17553) |
| 48 | Kafka-config script cannot set `cleanup.policy=delete,compact` | [KAFKA-17583](https://issues.apache.org/jira/browse/KAFKA-17583) |
| 49 | Kafka Streams Timeout During Partition Rebalance | [KAFKA-17622](https://issues.apache.org/jira/browse/KAFKA-17622) |
| 50 | Custom `partitioner.class` with an even number of partitions always writes to even partitions if use RoundRobinPartitioner | [KAFKA-17632](https://issues.apache.org/jira/browse/KAFKA-17632) |
