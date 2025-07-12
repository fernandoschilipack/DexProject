# DexProject – Minimal API + .NET MAUI

This repository contains a complete demo project that simulates sending DEX files from a mobile app to an ASP.NET Core Minimal API.


<img width="1343" height="856" alt="image" src="https://github.com/user-attachments/assets/69aba26b-bf00-467a-89cb-b120f2d36cb7" />

---

## Project Structure

```
DexProject/
├── DexApi/             # ASP.NET Core 9 Minimal API (DEX endpoint)
├── DexMaui/            # .NET MAUI App (2 buttons to send DEX data)
├── SQLscripts          # SQL Scripts to create tables and procs
├── DbdDex.bak          # Backup of the database
└── DexProject.sln      # Visual Studio solution including both projects

```
## API Swagger or Postman
<img width="1671" height="738" alt="image" src="https://github.com/user-attachments/assets/27c34cbf-0671-479d-8c63-3156640caa5b" />
<img width="1726" height="887" alt="image" src="https://github.com/user-attachments/assets/5bede7c7-91eb-47eb-bfa8-286fac45cd76" />

#Table Structure
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

- Run open the developer terminal and run a quick `dotnet clean`, `dotnet restore` and `dotnet build` in the solution

### 2. Configure Startup Projects

- Right-click the solution > **Set Startup Projects...**
- Select: `Multiple startup projects`
- Set **Action** = Start for both `DexApi` and `DexMaui` Set **DebugTarget** `WindowsMachine` (for testing locally)

### 3. Run the Projects

- Press **F5** or **Start** to launch both projects.
- The API will be accessible at: `https://localhost:port/vdi-dex`
- Change the URL for your port into the .NET MAUI Project <img width="2125" height="826" alt="image" src="https://github.com/user-attachments/assets/48153e04-8a03-4e17-8c83-cd23ef6aebb0" />
- The MAUI app will show two buttons: “Send Machine A” and “Send Machine B”
- If you execute using Android make I recommend using a public url using ngrok https://ngrok.com/ or conveyor

### 4. Test the flow

- Clicking the buttons sends a hardcoded DEX file as multipart/form-data with Basic Auth. <img width="1278" height="863" alt="image" src="https://github.com/user-attachments/assets/92a0c9b6-7e2e-477a-bf70-9c64b58434ee" />

- The API processes the file and stores data in SQL Server. <img width="1158" height="586" alt="image" src="https://github.com/user-attachments/assets/77d42894-7ce5-44d7-98a4-e334f9869e72" />


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
- Database backup is available as `.bak` in the root or `/Db`, also, the entire creation of tables and procedures is available if needed.

---

## License

This project is provided for technical demonstration purposes.
