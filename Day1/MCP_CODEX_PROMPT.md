# Prompt for OpenAI Codex — Run/Verify RxFlowOpsMcp-Azure

Paste everything below into Codex (CLI or IDE extension) as-is. It's
self-contained: it tells Codex what the project is, exactly how to run it,
and every gotcha that was already hit once building/running it — so Codex
doesn't waste turns rediscovering them.

---

## Task

Run and verify the existing MCP server at `RxFlowOpsMcp-Azure/RxFlowOpsMcp`
(C#/.NET 8, stdio transport). Do **not** rebuild it from scratch — it
already exists and works. Your job is to:

1. Build it, run its unit tests, and confirm both are green.
2. Run the server directly over stdio (JSON-RPC) and call `get_ticket`
   against a real Azure DevOps work item to prove the read path works.
3. Report back build/test output and the live tool-call result.

Do not touch git (this directory is not a git repo). Do not commit or push
anything.

## Project layout

- `RxFlowOpsMcp/` — the server project (`RxFlowOpsMcp.csproj`,
  `Program.cs`, `AzureDevOpsClient.cs`, `OpsCatalogClient.cs`,
  `PermissionGate.cs`, `ToolCallHooks.cs`, `RxFlowTools.cs`,
  `appsettings.json`)
- `RxFlowOpsMcp.Tests/` — xUnit tests for `PermissionGate` and
  `ToolCallHooks`
- `README.md` — setup/usage doc
- `PROMPT.md` — the original from-scratch build brief (for reference only,
  don't re-run it)

## Environment gotchas already hit once — don't repeat these

1. **This machine's default `dotnet` may be an old SDK (6.0), even though
   .NET 8 is installed separately.** Before any `dotnet build`/`run`/`test`:
   ```bash
   export DOTNET_ROOT="$HOME/.dotnet8"
   export PATH="$HOME/.dotnet8:$PATH"
   ```
   If `~/.dotnet8` doesn't exist on this machine, install it user-scoped
   (no sudo):
   ```bash
   curl -sSL https://dot.net/v1/dotnet-install.sh -o dotnet-install.sh
   chmod +x dotnet-install.sh
   ./dotnet-install.sh --channel 8.0 --install-dir "$HOME/.dotnet8"
   ```

2. **Required env vars for the server to start at all:**
   ```bash
   export AZDO_ORG=venkateshdb
   export AZDO_PROJECT=RxFlow
   export AZDO_PAT=<a-real-personal-access-token>
   export OPS_CATALOG_MOCK=true
   ```
   - `AZDO_PAT` must be a live Azure DevOps Personal Access Token with
     **Work Items → Read, write, & manage** scope, from
     `https://dev.azure.com/venkateshdb/_usersSettings/tokens`.
   - **Never ask the human to paste the PAT into chat/stdout.** If you
     need it, ask them to export it in their own shell, or to put it
     directly into a config file you then read — never echo it back, never
     put it in a shell command you print to the transcript, never write it
     to a committed file. If a token is ever exposed in chat/logs, tell the
     human to revoke and reissue it — don't just keep using the exposed
     one.
   - `OPS_CATALOG_MOCK=true` is required unless you also have a real
     `OPS_CATALOG_BASE_URL` / `OPS_CATALOG_TOKEN` — without one or the
     other, `get_service_owner`/`get_runbook`/`get_lab_health` throw at
     first call.

3. **`workItemsbatch` is an org-level Azure DevOps endpoint, not
   project-scoped** — if you ever touch `AzureDevOpsClient.cs`, don't
   "fix" the URL to `POST /{project}/_apis/wit/workitemsbatch`; it must
   stay `POST /_apis/wit/workitemsbatch` or it 404s.

4. **This project's Azure DevOps process template only has `Issue`/`Epic`
   work item types — not `Task`, not `"Change Request"`.** Work item #1
   and #2 on the real board are both type `Issue`. If you call
   `create_change_request`, pass `workItemType: "Issue"` explicitly (its
   code default is `"Change Request"`, which will 400 on this project).
   Confirm valid types first with `list_valid_states` if unsure.

5. **Piping a static file of JSON-RPC lines into `dotnet run` via plain
   shell redirection can silently lose stdout** (buffering interacts badly
   with stdin closing early), even though server-side stderr logs show it
   processed every request. Instead spawn the server as a background
   process, write JSON-RPC lines to its stdin with a trailing `sleep` to
   let it flush, then read the captured stdout file. Pattern that's known
   to work:
   ```bash
   export DOTNET_ROOT="$HOME/.dotnet8"; export PATH="$HOME/.dotnet8:$PATH"
   export AZDO_ORG=venkateshdb AZDO_PROJECT=RxFlow OPS_CATALOG_MOCK=true
   export AZDO_PAT=<token>   # from your own shell, not printed by you

   cd RxFlowOpsMcp-Azure/RxFlowOpsMcp

   (
   printf '%s\n' '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{},"clientInfo":{"name":"codex-smoke-test","version":"1.0"}}}'
   printf '%s\n' '{"jsonrpc":"2.0","method":"notifications/initialized"}'
   printf '%s\n' '{"jsonrpc":"2.0","id":2,"method":"tools/call","params":{"name":"get_ticket","arguments":{"workItemId":1}}}'
   sleep 3
   ) | dotnet run --no-build > client_output.log 2> server.log &
   PID=$!
   sleep 6
   kill $PID 2>/dev/null

   cat client_output.log
   tail -30 server.log
   rm -f client_output.log server.log   # don't leave debug artifacts behind
   ```

6. **The MCP server has no live-reload.** If it's already running under an
   MCP client (Claude Desktop/Code, or Codex's own MCP support) and you
   change `.mcp.json`/env vars, the running process keeps its old env
   until the client is restarted. Don't waste turns retrying a tool call
   through an already-running connection after an env change — tell the
   human a restart is required, or (for verification purposes) spawn the
   server directly as shown in step 5 instead of relying on the live
   connection.

7. **Two mutating tools require a live, deliberate human "yes" before you
   call them**: `create_change_request` and `update_ticket_status`. Even
   with a valid PAT and an allow-listed project, do not call these without
   the human explicitly confirming the exact parameters (title,
   description, target state) first. The server's `PermissionGate` is a
   second, independent check (project allow-list in
   `appsettings.json` → `AzureDevOps:WritableProjects`), not a substitute
   for asking.

## Verification steps (in order)

```bash
export DOTNET_ROOT="$HOME/.dotnet8"; export PATH="$HOME/.dotnet8:$PATH"

cd RxFlowOpsMcp-Azure/RxFlowOpsMcp
dotnet build            # expect: 0 warnings, 0 errors

cd ../RxFlowOpsMcp.Tests
dotnet test              # expect: 9/9 passed
```

Then run the stdio smoke test from gotcha #5 above against `get_ticket`
(workItemId 1) and report the returned title/state.

## Acceptance criteria

- `dotnet build` — 0 warnings, 0 errors.
- `dotnet test` — 9/9 passed.
- Live `get_ticket(1)` via the real MCP `tools/call` protocol (not raw
  `curl`) returns real data: title `"Ingest timeout on rxflow-ingest"`,
  state `"To Do"`.
- No secrets echoed into your own output at any point.
- No write tools (`create_change_request`, `update_ticket_status`) called
  without the human explicitly approving the exact parameters first.

## If asked to also build the slide deck

`RxFlowOpsMcp-Azure/slides/` has HTML→PPTX source (`build.js`,
`html2pptx.js`, numbered `*.html` slides). This machine's default Node is
18, but Playwright (used by `html2pptx.js`) requires **Node 20+**. If
`~/.nvm` exists, use it:
```bash
export NVM_DIR="$HOME/.nvm"; [ -s "$NVM_DIR/nvm.sh" ] && . "$NVM_DIR/nvm.sh"
nvm use 20
cd RxFlowOpsMcp-Azure/slides
node build.js
```
If `~/.nvm` doesn't exist, install it the same user-scoped way as the
.NET 8 SDK (`curl ... | bash` from nvm's official install script), then
`nvm install 20`. Don't touch the system's Node 18.
