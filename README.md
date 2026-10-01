# CollegeFestMVC — College Technical Fest Registration System

ASP.NET Core MVC implementation of the CIE-1 task shown in the supplied screenshots.

## Requirements covered

- ASP.NET Core MVC project configured for HTTP `5000` and HTTPS `5001`.
- `Participant` and `Event` models with the requested properties.
- Bootstrap registration form with ASP.NET Core Tag Helpers and validation.
- Temporary participant storage using `List<Participant>`.
- Bootstrap participant table with Details links.
- `ParticipantExtensions` containing `GetRegistrationStatus()` and `GetFeeCategory()`.
- Details page showing registration status and fee category.
- Welcome page with navigation using `asp-action`.
- Bootstrap conditional success/warning display.
- `GlobalUsings.cs` with global namespace imports.

## Run

Install the .NET 8 SDK, then from this directory run:

```bash
dotnet restore
dotnet run --launch-profile https
```

Open `https://localhost:5001`.

The application intentionally stores registrations in memory, as requested by the task. Restarting the application clears newly added registrations.
