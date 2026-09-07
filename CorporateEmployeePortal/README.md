# Corporate Employee Portal

## Description
The corporate employee project implement web app using razor, it integrates middleware for logging, response headers, terminal middlewares, authentication, serve static files.
It serves as an internal portal for employees to view company resources and corporate privacy policies.

## Key Features
- ASP.NET Core Razor Pages: Page routing, model binding, and page handler architecture.
- Middleware Pipeline:
  - Custom headers: Sets custom response headers such as X-Company, X-Portal-Version, and X-Environment.
  - Access and Audit Logging: Tracks request timestamps, HTTP methods, route paths, user agents, and status codes in log.txt.
  - Terminal emergency middleware: Serves an emergency maintenance and security alert screen.
  - Static files and routing: Serves static web assets and configures page endpoint routing.
- Shared Layout and Partials: Shared layout structure with responsive navigation, Bootstrap, and validation partials.
- Static assets: Custom stylesheets and client-side JavaScript located in the wwwroot directory.

## Project Structure
```text
CorporateEmployeePortal/
├── Pages/
│   ├── Shared/
│   │   ├── _Layout.cshtml             # Main layout shell and navigation
│   │   ├── _Layout.cshtml.css         # Layout stylesheet
│   │   └── _ValidationScriptsPartial.cshtml
│   ├── Departments.cshtml / .cs       # Department listings
│   ├── Employees.cshtml / .cs         # Staff directory
│   ├── Error.cshtml / .cs             # Error view and diagnostics
│   ├── Index.cshtml / .cs             # Portal landing dashboard
│   ├── Privacy.cshtml / .cs           # Corporate privacy policy
│   ├── Resources.cshtml / .cs         # Internal company documents and policies
│   ├── _ViewImports.cshtml            # Shared namespaces and Tag Helper imports
│   └── _ViewStart.cshtml              # Global default layout declaration
├── Properties/
│   └── launchSettings.json            # Development port and launch configurations
├── wwwroot/
│   ├── css/site.css                   # Custom global stylesheet
│   └── js/site.js                     # Client-side scripts
├── appsettings.json                   # Base application settings
├── appsettings.Development.json       # Development logging and error settings
├── EmployeePortal.csproj              # Project configuration (.NET Web SDK)
└── Program.cs                         # Application entry, service registration, and middleware pipeline
```


### Run the Application
1. Open a terminal and navigate to the project directory:
   ```bash
   cd CorporateEmployeePortal
   dotnet run
   ```

2. Open your browser and navigate to:
   - HTTP: http://localhost:5100
   - HTTPS: https://localhost:7100

## Available Routes
| Route | Description |
| :--- | :--- |
| / or /Index | Main dashboard with links to portal services |
| /Employees | Staff directory with roles and contact information |
| /Departments | Overview of corporate departments |
| /Resources | Company documents, IT support, and employee handbook |
| /Privacy | Privacy policy and data handling statement |
| /Error | Error display page |
