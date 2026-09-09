# Production investigation and release support

## Incident work

Establish affected service/environment, user impact, time window, recent changes, and the authorized scope. Use existing runbooks and read-only telemetry where available. Limit queries and log retrieval to the relevant scope; redact sensitive values in output. Do not assume access to production because local repository access exists.

Separate observed symptoms, suspected causes, confirmed causes, mitigation, and permanent correction. Investigate a plausible path using logs, metrics, traces, recent diffs, or a safe reproduction. A timeout or missing response does not prove a write failed; reconcile external state before retrying a mutation.

Propose the least disruptive supported mitigation. Execute a production action only within existing task-specific authorization. If required authorization is absent, finish the investigation and prepare the exact target, action, expected effect, and recovery plan before asking. Never automatically retry an unbounded restart, rollback, migration, or deployment.

After mitigation, check the relevant health and business outcomes. A service restarting successfully does not establish recovery for affected users. Record unresolved uncertainty and next steps. Produce an incident summary when requested or useful: impact, evidence-backed timeline, mitigation, cause confidence, and follow-up work. Do not invent timestamps or owners.

## Release preparation

Inspect the actual deployment procedure, CI result, artifact/revision, target environment, configuration changes, and database migration requirements. Do not invent a pipeline for an unspecified stack.

Prepare a compact release checklist appropriate to the change:

- Exact artifact or revision and target environment.
- Required checks, configuration, migration, and dependencies.
- Rollout sequence and health/business checks.
- Stop condition and authorized rollback or mitigation path.
- Known limitations and remaining approvals or external dependencies.

Do not claim migration rollback is safe without inspecting reversibility and data compatibility. Prefer the team's existing procedure over an ad hoc command.

## Execute and verify

When the user authorizes deployment to a named target, run the established process and inspect its results. If a health check fails, stop further rollout and follow the authorized recovery procedure; do not blindly repeat deployment.

Verify the released revision plus relevant application behavior, not just the deployment command's exit status. Report release status and post-release verification separately. If access is unavailable, provide the prepared commands/checklist and state that release verification remains unperformed.

Do not create a background monitor or recurring job unless the user requests ongoing observation. When they do, use the available scheduling mechanism, clear completion conditions, and meaningful-change notifications.
