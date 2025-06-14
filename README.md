# Invoice API Service

This repository hosts a modular web API built with **.NET 8**. The solution `NexKoala.API.sln` groups the shared framework projects, the API server and domain modules like **Todo**, **Catalog**, and **Invoice**.

## General Structure

- **Directory.Build.props** defines common settings for all projects:

```xml
<Project>
    <PropertyGroup>
        <TargetFramework>net8.0</TargetFramework>
        <TreatWarningsAsErrors>false</TreatWarningsAsErrors>
        <CodeAnalysisTreatWarningsAsErrors>false</CodeAnalysisTreatWarningsAsErrors>
        <GenerateDocumentationFile>true</GenerateDocumentationFile>
        <NoDefaultExcludes>true</NoDefaultExcludes>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
        <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
        <AnalysisLevel>latest</AnalysisLevel>
        <AnalysisMode>All</AnalysisMode>
        <ContainerImageTags>2.0.2-rc;latest</ContainerImageTags>
    </PropertyGroup>
    <ItemGroup>
        <PackageReference Include="SonarAnalyzer.CSharp" PrivateAssets="all" Condition="$(MSBuildProjectExtension) == '.csproj'" />
    </ItemGroup>
</Project>
```

- **global.json** pins the SDK version:

```json
{
  "sdk": {
    "version": "8.0.303",
    "rollForward": "latestFeature"
  }
}
```

- `api/server/Program.cs` is the API entry point:

```csharp
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.ConfigureNexKoalaFramework();
    builder.RegisterModules();

    var app = builder.Build();

    app.UseNexKoalaFramework();
    app.UseModules();
    await app.RunAsync();
}
```

- The `api` directory contains:
  - `framework` – shared infrastructure and base entities
  - `modules` – domain functionality (`Catalog`, `Invoice`, `Todo`, `Shared`)
  - `migrations` – EF Core migrations
  - `server` – the web API host

Monitoring services for local development are set up in the `aspire` folder.

## Getting Started

1. Install the .NET SDK version listed in `global.json`.
2. Run the API: `dotnet run --project api/server` (update `appsettings.json` if needed).
3. Browse the modules under `api/modules` to see how endpoints, services and persistence are organized.
4. Database schemas live under `api/migrations`.
5. Check out `aspire/Host` for Grafana and Prometheus integration.

## Next Steps

- Start with the Todo module to learn the basic CRUD pattern.
- Investigate background jobs in `InvoiceJobRunner` for Hangfire scheduling.
- Explore the framework infrastructure for multitenancy, authentication and logging.

