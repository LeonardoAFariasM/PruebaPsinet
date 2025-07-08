# RoverMissionPlanner

🚀 API REST para gestión de tareas de rovers

📅 Validación de solapamiento de tareas

⏱️ Visualización de timeline de actividades

# Tecnologias Utilizadas

Backend: .NET 9, FluentValidation, Prometheus.
Frontend: Angular 20, TypeScript, RxJS.
Persistencia: En Memoria.
Infraestructura: GitHub.
Pruebas: ng test.

# Ejecución
# Backend
cd RoverMissionApi
cd API
dotnet restore || dotnet clean
dotnet run

# Frontend
cd RoverMissionFront
npm install
ng serve

# Endpoints y uso (con Swagger)
Tomar la direccion que forezca el comando "dotnet run"
y agregar "/swagger"
ejemplo: "http://localhost:5XXX/swagger"

# Ejemplos de datos

POST http://localhost:5XXX/rovers/Curiosity/tasks
Data: {
  "roverName": "Curiosity",
  "taskType": "Photo",
  "latitude": -34.6037,
  "longitude": -58.3816,
  "startsAt": "2025-07-07T16:00:00",
  "durationMinutes": 50,
  "status": "InProgress"
}

GET http://localhost:5XXX/rovers/Cosmos/tasks?date=2025-07-07
Data: [
    {
        "id": "20744a77-ef8d-4b11-a033-47d0b873b4b7",
        "roverName": "Cosmos",
        "taskType": "Photo",
        "latitude": -34.6037,
        "longitude": -58.3816,
        "startsAt": "2025-07-07T14:30:00",
        "durationMinutes": 500000,
        "status": "InProgress"
    }
]

GET http://localhost:5XXX/rovers/names
Data: [
    "Curiosity",
    "Cosmos"
]

# Ejecutar pruebas unitarias
cd RoverMissionApi
dotnet test Application.Tests

# Generar reporte de cobertura
Abriendo el archivo RoverMissionApi\Application.Tests\coverage.cobertura.xml\Application_RoverService.html se puede ver el test solicitado