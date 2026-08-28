# RxFlow

RxFlow accepts synthetic optical prescriptions and routes lens work to training labs.

## Local development

```bash
dotnet tool restore
dotnet restore RxFlow.sln
dotnet build RxFlow.sln --no-restore
dotnet test RxFlow.sln --no-build
dotnet format RxFlow.sln --verify-no-changes
dotnet outdated
dotnet list RxFlow.sln package --vulnerable --include-transitive
docker compose up --build
```

The API listens on `http://localhost:8081`. Send `Authorization: Bearer training-token` to protected endpoints. Fixtures and examples use only `SYN-*` identifiers.
