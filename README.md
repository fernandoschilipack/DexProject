# DexProject – Minimal API + .NET MAUI

This repository contains a complete demo project that simulates sending DEX files from a mobile app to an ASP.NET Core Minimal API.


<img width="1343" height="856" alt="image" src="https://github.com/user-attachments/assets/69aba26b-bf00-467a-89cb-b120f2d36cb7" />

---

## Project Structure

```
DexProject/
├── DexApi/             # ASP.NET Core 9 Minimal API (DEX endpoint)
├── DexMaui/            # .NET MAUI App (2 buttons to send DEX data)
└── DexProject.sln      # Visual Studio solution including both projects
```

---

## Prerequisites

| Component            | Version     | Notes                            |
|---------------------|-------------|----------------------------------|
| .NET SDK (MAUI)      | **8.0**     | Required to build DexMaui        |
| .NET SDK (API)       | **9.0**     | Required to build DexApi         |
| Visual Studio        | 2022+       | With MAUI and ASP.NET workload   |
| SQL Server LocalDb   | Installed   | Required to persist DEX data     |

---

## How to Run

### 1. Open the solution

Open `DexProject.sln` in Visual Studio.

- Run open the developer terminal and run a quick `dotnet clean`, `dotnet restore` in the solution

### 2. Configure Startup Projects

- Right-click the solution > **Set Startup Projects...**
- Select: `Multiple startup projects`
- Set **Action** = Start for both `DexApi` and `DexMaui`

### 3. Run the Projects

- Press **F5** or **Start** to launch both projects.
- The API will be accessible at: `https://localhost:port/vdi-dex`
- The MAUI app will show two buttons: “Send Machine A” and “Send Machine B”

### 4. Test the flow

- Clicking the buttons sends a hardcoded DEX file as multipart/form-data with Basic Auth.
- The API processes the file and stores data in SQL Server.

---

## Basic Auth for API

The API requires basic authentication:

```
Username: vendsys
Password: NFsZGmHAGWJSZ#RuvdiV
```

---

## Notes

- The API validates request format and DEX content.
- The MAUI app uses **CommunityToolkit.MVVM** and follows the MVVM pattern.
- Database backup is available as `.bak` in the root or `/Db`.

---

## License

This project is provided for technical demonstration purposes.
