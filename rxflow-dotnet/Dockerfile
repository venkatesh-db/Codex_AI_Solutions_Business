FROM mcr.microsoft.com/dotnet/sdk:8.0.404 AS build
WORKDIR /src
COPY . .
RUN dotnet restore RxFlow.sln && dotnet publish src/RxFlow.Api/RxFlow.Api.csproj -c Release -o /app
FROM mcr.microsoft.com/dotnet/aspnet:8.0.11
COPY --from=build /app /app
ENTRYPOINT ["dotnet","/app/RxFlow.Api.dll"]
