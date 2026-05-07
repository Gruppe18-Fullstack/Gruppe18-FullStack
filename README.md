# Gruppe 18 Weather App

## What Is This Project?

This is a weather tracking application built for managing weather stations and their readings. It allows users to:
- Track multiple weather stations across different locations
- Record weather measurements (temperature, wind speed, humidity)
- Import live weather data from Norway's official weather service (Met.no/Yr.no)
- View and analyze weather data over time

The application is designed for organizations or individuals who need to monitor weather conditions at specific locations, such as agricultural operations, research facilities, or environmental monitoring stations.

## Who Is This For?

- **Weather enthusiasts** tracking local conditions
- **Researchers** collecting weather data for analysis  
- **Organizations** monitoring multiple weather stations
- **Students** learning full-stack web development with ASP.NET Core

---

## Getting Started

### Requirements
- .NET 10.0 or higher installed on your computer
- A web browser (Chrome, Firefox, Edge, Safari)
- Terminal or PowerShell

### How to Run the Application

1. **Extract the project files** to a folder on your computer (e.g., `Documents\Gruppe18-FullStack\`)

2. **Open your terminal or PowerShell**

3. **Navigate to the main project folder:**
cd [path-to-where-you-extracted]\Gruppe18-FullStack
   Example: `cd C:\Users\YourName\Documents\Gruppe18-FullStack`

4. **Start the application:**
dotnet run

5. **Open your web browser and go to:**
http://localhost:5044

6. **You'll be redirected to the login page** - use the test credentials below

### Stopping the Application
Press `Ctrl+C` in the terminal to stop the server.

---

## Test Credentials

### For Testing Login (Existing User)
If you want to login and explore the application immediately:

**Email:** testuser@gruppe18.no  
**Password:** Test1234!

After logging in, you'll be redirected to the weather application homepage with access to all features.

### For Testing Registration (New Account)

The application allows new users to create accounts. Here's how to test the registration feature:

**Test Scenario 1 - Creating a New Account (Success Case):**
1. On the login page, click "Register as a new user"
2. Enter any email address you want (e.g., professor@usn.no, yourname@example.com)
3. Create a password with at least:
   - One uppercase letter
   - One lowercase letter
   - One number
   - One special character (like ! @ # $)
   - Example password: Test1234!
4. Click "Register"
5. You'll be redirected to the login page
6. Login with your new credentials
7. You'll be redirected to the application home page

**Test Scenario 2 - Email Already Taken (Validation Case):**
1. On the registration page, try registering with: testuser@gruppe18.no
2. You'll see an error: "Username 'testuser@gruppe18.no' is already taken"
3. This demonstrates that the application prevents duplicate accounts (real-world validation working correctly)

---

## How the Application Works

### First Time Visit
1. When you open http://localhost:5044, you'll be **automatically redirected to the login page**
2. If you don't have an account, click "Register as a new user"
3. After registration, you'll be redirected back to the login page
4. Login with your credentials
5. You'll be redirected to the weather application home page (Welcome screen)

### Navigation
Once logged in, you can navigate using the top navbar:
- **Home** - Returns to the welcome screen
- **Weather Stations** - View and manage weather stations
- **Weather Readings** - View and manage weather measurements
- **Email display** - Shows which user is logged in
- **Logout** - Logs you out and returns to login page

### Main Features

**Weather Stations:**
- View all registered weather stations in a table
- Add new weather stations with name, location, and GPS coordinates (latitude/longitude)
- Edit existing station information
- View details for each station
- Delete stations (with confirmation)

**Weather Readings:**
- View all weather measurements in a table
- Manually add weather readings for any station (temperature, wind speed, humidity)
- **Import live weather data from Yr.no** - Click "Import Weather Data" button to fetch current weather automatically
- Edit existing readings
- View detailed information for each reading
- Delete readings (with confirmation)
- Each reading is linked to its weather station

**Live Data Import:**
- Click the green "Import Weather Data" button on the Weather Readings page
- The application fetches current weather from Met.no API (Norway's official meteorological service)
- Data is automatically saved to your local database
- View the imported readings immediately in the readings table

---

## Project Structure

This project consists of two separate folders:
```
[Your extraction folder]
├── Gruppe18-FullStack/              (Main application - this is what you run)
│   ├── Controllers/                 (Handles user requests and coordinates actions)
│   ├── Models/                      (Data structures: WeatherStation, WeatherReading)
│   ├── Views/                       (Web pages you see in your browser)
│   │   ├── Home/                    (Welcome page)
│   │   ├── WeatherStations/         (Station management pages)
│   │   ├── WeatherReadings/         (Reading management pages)
│   │   └── Shared/                  (Navbar, layout, shared components)
│   ├── Data/                        (Database configuration)
│   ├── Services/                    (WeatherApiService - communicates with Yr.no API)
│   ├── Migrations/                  (Database schema version history)
│   ├── wwwroot/                     (CSS, JavaScript, static files)
│   ├── Program.cs                   (Application startup and configuration)
│   ├── Gruppe18.db                  (SQLite database file - your data lives here)
│   └── README.md                    (This file)
│
└── Gruppe18-FullStack.Tests/       (Automated tests - separate folder)
    ├── WeatherStationsControllerTests.cs
    └── Gruppe18-FullStack.Tests.csproj
```
**Important:** The test project is in a **separate folder** (not inside the main project). This is standard practice for ASP.NET projects and follows professional software development conventions.

---

## Technologies Used

This application is built with modern web development technologies:

- **ASP.NET Core 10.0 MVC** - Microsoft's web framework for building modern web applications
- **Entity Framework Core** - Object-relational mapping for database management
- **SQLite** - Lightweight file-based database (no installation or setup required)
- **ASP.NET Core Identity** - User authentication and security system
- **Tailwind CSS** - Modern utility-first CSS framework for styling
- **Met.no Weather API** - Real-time weather data from Norwegian Meteorological Institute
- **xUnit** - Automated testing framework for quality assurance

---

## Database

The application uses SQLite, a self-contained file-based database that requires no additional setup or installation.

**Database file:** `Gruppe18.db` (located in the main project folder)

**What's stored:**
- **Weather stations:** Name, location, latitude, longitude
- **Weather readings:** Temperature (°C), wind speed (m/s), humidity (%), timestamp, linked station
- **User accounts:** Email addresses, encrypted passwords, authentication data
- **Relationships:** Each reading is linked to its weather station (one station can have many readings)

**Viewing the database:** You can open Gruppe18.db with any SQLite database viewer if you want to see the raw data tables.

---

## Known Issues

### Double Migration Files
You may notice two similar migration files in the Migrations folder: `InitialCreate` and `InitialCreate1`.

**What happened:** During initial project setup, the first migration failed due to a `Program.cs` configuration error. A second migration was created to complete the database schema setup.

**Impact:** None - both migrations apply successfully during database initialization, and the database works perfectly. This is purely a cosmetic issue in the project structure.

**Why we kept both:** Deleting migrations after they've been applied to a database can cause synchronization issues and potentially break the database. Since everything functions correctly, we left both migrations in place.

---

## API Integration Details

The application integrates with **Met.no** (Norwegian Meteorological Institute) to fetch real-time weather data:

- **API Endpoint:** https://api.met.no/weatherapi/locationforecast/2.0/
- **Data Source:** Official Norwegian weather service (same provider that powers Yr.no)
- **How it works:** 
  1. When you click "Import Weather Data", the application sends an HTTP GET request to Met.no
  2. Request includes GPS coordinates (currently configured for Horten, Norway: 59.27°N, 10.41°E)
  3. Met.no returns current weather data in JSON format
  4. Application parses the JSON and extracts temperature, wind speed, and humidity
  5. Data is saved to your local SQLite database
  6. You can immediately view the imported reading in the Weather Readings table

---

## For Developers

### Running Tests
The test project is located in a separate folder. To run the automated tests:

```bash
cd [path-to-test-project]\Gruppe18-FullStack.Tests
dotnet test
```

**Test Coverage:**
- WeatherStations controller Create action
- WeatherStations controller Index action
- WeatherStations controller Delete action
- All tests use in-memory database for isolation

### Project Architecture
- **MVC Pattern:** Clear separation between Models (data), Views (UI), and Controllers (logic)
- **Repository Pattern:** Database access through ApplicationDbContext
- **Service Layer:** WeatherApiService handles external API communication
- **Authentication:** ASP.NET Core Identity with cookie-based sessions and password security

### Adding New Features
If you want to extend this application:

1. **Add a new model** in `Models/` folder
2. **Update** `ApplicationDbContext` with a new DbSet
3. **Create migration:** `dotnet ef migrations add YourMigrationName`
4. **Update database:** `dotnet ef database update`
5. **Scaffold controller** or create a custom controller in `Controllers/`
6. **Add views** in `Views/[ControllerName]/`

---

## Questions or Issues?

This is a student project created for the **TSD2491 exam** at **USN Vestfold** (May 2026).

**Created by:** Gruppe 18
- Student 268958
- Student 271924

For any questions about setup or usage, please refer to this README file.
