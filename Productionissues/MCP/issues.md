# MCP: 50 issue scenarios from reported SDK defects

Each five-row group expands one publicly reported or reproduced SDK defect into distinct operational symptoms. These are **50 diagnostic scenarios, not 50 independent confirmed production incidents**. Check the linked report for affected versions and status. No entry asserts that this repository has the defect.

| # | Observed symptom or diagnostic signal | Evidence |
| ---: | --- | --- |
| 1 | Cancelled tool call remains active on the server | [Python SDK cancellation leak](https://github.com/modelcontextprotocol/python-sdk/issues/2507) |
| 2 | Database connection remains held after client timeout | [Python SDK cancellation leak](https://github.com/modelcontextprotocol/python-sdk/issues/2507) |
| 3 | Repeated timeouts increase suspended coroutine count | [Python SDK cancellation leak](https://github.com/modelcontextprotocol/python-sdk/issues/2507) |
| 4 | Server resource use rises while clients see cancelled calls | [Python SDK cancellation leak](https://github.com/modelcontextprotocol/python-sdk/issues/2507) |
| 5 | Tool cancellation does not release acquired locks promptly | [Python SDK cancellation leak](https://github.com/modelcontextprotocol/python-sdk/issues/2507) |
| 6 | Concurrent calls on one session receive swapped results | [Python SDK duplicate request IDs](https://github.com/modelcontextprotocol/python-sdk/issues/3137) |
| 7 | One caller times out while another receives its response | [Python SDK duplicate request IDs](https://github.com/modelcontextprotocol/python-sdk/issues/3137) |
| 8 | A valid JSON-RPC result contains another caller's payload | [Python SDK duplicate request IDs](https://github.com/modelcontextprotocol/python-sdk/issues/3137) |
| 9 | Tenant data can appear in the wrong caller's tool result | [Python SDK duplicate request IDs](https://github.com/modelcontextprotocol/python-sdk/issues/3137) |
| 10 | No protocol error identifies an in-flight ID collision | [Python SDK duplicate request IDs](https://github.com/modelcontextprotocol/python-sdk/issues/3137) |
| 11 | Rejected HTTP requests still create sessions | [Python SDK rejected-session leak](https://github.com/modelcontextprotocol/python-sdk/issues/3228) |
| 12 | Idle session count rises despite failed handshakes | [Python SDK rejected-session leak](https://github.com/modelcontextprotocol/python-sdk/issues/3228) |
| 13 | Memory use rises from sessions that never became usable | [Python SDK rejected-session leak](https://github.com/modelcontextprotocol/python-sdk/issues/3228) |
| 14 | Repeated malformed requests consume the session budget | [Python SDK rejected-session leak](https://github.com/modelcontextprotocol/python-sdk/issues/3228) |
| 15 | Later valid clients receive capacity errors after rejected requests | [Python SDK rejected-session leak](https://github.com/modelcontextprotocol/python-sdk/issues/3228) |
| 16 | In-flight responder count keeps growing in one session | [Python SDK in-flight responder leak](https://github.com/modelcontextprotocol/python-sdk/issues/1385) |
| 17 | Process memory rises under repeated asynchronous requests | [Python SDK in-flight responder leak](https://github.com/modelcontextprotocol/python-sdk/issues/1385) |
| 18 | Garbage collection pressure rises during request bursts | [Python SDK in-flight responder leak](https://github.com/modelcontextprotocol/python-sdk/issues/1385) |
| 19 | Many pending tasks remain after request handling | [Python SDK in-flight responder leak](https://github.com/modelcontextprotocol/python-sdk/issues/1385) |
| 20 | Server becomes unstable without an equivalent rise in completed calls | [Python SDK in-flight responder leak](https://github.com/modelcontextprotocol/python-sdk/issues/1385) |
| 21 | `session.initialize()` hangs on a stdio connection | [Python SDK stdio initialization hang](https://github.com/modelcontextprotocol/python-sdk/issues/382) |
| 22 | Client reaches its initialization timeout | [Python SDK stdio initialization hang](https://github.com/modelcontextprotocol/python-sdk/issues/382) |
| 23 | Server appears started but tools never become available | [Python SDK stdio initialization hang](https://github.com/modelcontextprotocol/python-sdk/issues/382) |
| 24 | No initial protocol response reaches the client | [Python SDK stdio initialization hang](https://github.com/modelcontextprotocol/python-sdk/issues/382) |
| 25 | Restarting the same server process repeats the handshake stall | [Python SDK stdio initialization hang](https://github.com/modelcontextprotocol/python-sdk/issues/382) |
| 26 | Intermittent 404 responses occur on session-bound requests | [Python SDK multi-worker session loss](https://github.com/modelcontextprotocol/python-sdk/issues/520) |
| 27 | A session works until a request reaches another worker | [Python SDK multi-worker session loss](https://github.com/modelcontextprotocol/python-sdk/issues/520) |
| 28 | Ping reduces but does not eliminate session failures | [Python SDK multi-worker session loss](https://github.com/modelcontextprotocol/python-sdk/issues/520) |
| 29 | Tool calls fail after load-balancer redistribution | [Python SDK multi-worker session loss](https://github.com/modelcontextprotocol/python-sdk/issues/520) |
| 30 | The same session ID is accepted by one process and rejected by another | [Python SDK multi-worker session loss](https://github.com/modelcontextprotocol/python-sdk/issues/520) |
| 31 | Valid session ID receives `Session not found` after TCP idle close | [TypeScript SDK keepalive regression](https://github.com/modelcontextprotocol/typescript-sdk/issues/1852) |
| 32 | Tool call succeeds, then next HTTP request gets 400 | [TypeScript SDK keepalive regression](https://github.com/modelcontextprotocol/typescript-sdk/issues/1852) |
| 33 | Failures begin after upgrading the TypeScript SDK | [TypeScript SDK keepalive regression](https://github.com/modelcontextprotocol/typescript-sdk/issues/1852) |
| 34 | Session lifetime tracks socket lifetime unexpectedly | [TypeScript SDK keepalive regression](https://github.com/modelcontextprotocol/typescript-sdk/issues/1852) |
| 35 | Clients reconnect repeatedly despite valid session headers | [TypeScript SDK keepalive regression](https://github.com/modelcontextprotocol/typescript-sdk/issues/1852) |
| 36 | One transient HTTP error breaks later tool calls | [Go SDK poisoned transport](https://github.com/modelcontextprotocol/go-sdk/issues/683) |
| 37 | A timeout is followed by failures on a healthy request | [Go SDK poisoned transport](https://github.com/modelcontextprotocol/go-sdk/issues/683) |
| 38 | Client transport cannot recover from a single 503 | [Go SDK poisoned transport](https://github.com/modelcontextprotocol/go-sdk/issues/683) |
| 39 | Sequential calls fail after a network interruption | [Go SDK poisoned transport](https://github.com/modelcontextprotocol/go-sdk/issues/683) |
| 40 | Recreating the client restores calls that a retry could not | [Go SDK poisoned transport](https://github.com/modelcontextprotocol/go-sdk/issues/683) |
| 41 | `tools/list` times out as server tool count grows | [Java SDK tool listing timeout](https://github.com/modelcontextprotocol/java-sdk/issues/470) |
| 42 | Listing a small catalog succeeds but a larger one fails | [Java SDK tool listing timeout](https://github.com/modelcontextprotocol/java-sdk/issues/470) |
| 43 | Client reports timeout before any tool is called | [Java SDK tool listing timeout](https://github.com/modelcontextprotocol/java-sdk/issues/470) |
| 44 | Tool discovery latency rises with catalog size | [Java SDK tool listing timeout](https://github.com/modelcontextprotocol/java-sdk/issues/470) |
| 45 | Application startup fails because tool enumeration stalls | [Java SDK tool listing timeout](https://github.com/modelcontextprotocol/java-sdk/issues/470) |
| 46 | One response ID goes missing under concurrent load | [Rust SDK concurrent response loss](https://github.com/modelcontextprotocol/rust-sdk/issues/941) |
| 47 | Most tool calls succeed while one waits to deadline | [Rust SDK concurrent response loss](https://github.com/modelcontextprotocol/rust-sdk/issues/941) |
| 48 | Server stays running during the missing-response event | [Rust SDK concurrent response loss](https://github.com/modelcontextprotocol/rust-sdk/issues/941) |
| 49 | Extending the deadline does not recover the absent reply | [Rust SDK concurrent response loss](https://github.com/modelcontextprotocol/rust-sdk/issues/941) |
| 50 | Concurrent calls show incomplete response accounting | [Rust SDK concurrent response loss](https://github.com/modelcontextprotocol/rust-sdk/issues/941) |
