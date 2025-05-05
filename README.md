# FractionsApp

A multi-platform application for working with fractions, built with .NET 9.0 and .NET Aspire.

## Project Structure

- **FractionsApp.API** - Backend API services
- **FractionsApp.AppHost** - .NET Aspire orchestration host
- **FractionsApp.Maui** - Cross-platform mobile and desktop UI (MAUI)
- **FractionsApp.ServiceDefaults** - Common service configuration defaults
- **FractionsApp.Shared** - Shared code between projects
- **FractionsApp.Web** - Web UI

## Prerequisites

- .NET 9.0 SDK or later
- Visual Studio 2022 or later with MAUI workload (for MAUI development)
- Containerization support (Docker) for Aspire orchestration

## Getting Started

### Running Backend Services with Aspire

```
cd FractionsApp.AppHost
dotnet run
```

This will start the Aspire dashboard and all configured backend services (API and Web).

### Running the MAUI Application

To run the MAUI application, you'll need to open the solution in Visual Studio and run the MAUI project targeting your desired platform, or use the dotnet CLI:

```
cd FractionsApp.Maui
dotnet build -t:Run -f net9.0-windows10.0.19041.0
```

Replace the framework with your target platform (android, ios, etc.)

## Development Notes

- The MAUI application can connect to the services orchestrated by Aspire
- For full-stack development, run both the AppHost and the MAUI app
- .NET Aspire doesn't directly deploy or run MAUI applications on devices/emulators

## License

[MIT License](LICENSE)