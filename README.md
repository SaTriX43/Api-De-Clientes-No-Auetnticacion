🚀 API de Clientes (sin autenticación) — ASP.NET Core 8

API REST creada para practicar CRUD, validaciones, LINQ, filtrado por fechas, ordenamiento y manejo global de errores.
Forma parte de un plan de 20 proyectos orientados a fortalecer habilidades como Backend Developer .NET.

📌 Características principales

✔ CRUD completo para la entidad Cliente
✔ Búsqueda por ID y por email
✔ Filtro de clientes por rango de fechas
✔ Ordenamiento ascendente o descendente (A-Z / Z-A)
✔ Email único en base de datos
✔ Fecha de registro generada automáticamente por SQL Server
✔ Manejo global de errores con middleware
✔ Logging profesional con Serilog
✔ Arquitectura por Capas (Controllers → Services → Repositories → EF Core)
✔ Uso del patrón Result para respuestas controladas

🛠️ Tecnologías utilizadas

.NET 8 Web API

C#

Entity Framework Core

SQL Server

Serilog (logs por nivel en /logs/)

LINQ

Swagger para documentación

DTOs para separación de modelos

🧱 Modelo principal
Cliente
public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Email { get; set; }
    public DateTime FechaDeRegistro { get; set; }
}

Reglas aplicadas

Email único → HasIndex(c => c.Email).IsUnique()

FechaDeRegistro automática → HasDefaultValueSql("GETDATE()")

📁 Estructura del proyecto
API de Clientes
│── Controllers
│    └── ClienteController.cs
│── DALs
│    ├── IClienteRepository.cs
│    └── ClienteRepository.cs
│── DTOs
│    ├── ClienteCrearDto.cs
│    └── ClienteDto.cs
│── Middlewares
│    └── ErrorHandlerMiddleware.cs
│── Models
│    ├── Cliente.cs
│    ├── ApplicationDbContext.cs
│    └── Result.cs
│── Services
│    ├── IClienteService.cs
│    └── ClienteService.cs
│── logs
│── Program.cs
│── appsettings.json

📚 Endpoints principales
🔹 Obtener cliente por ID
GET /api/cliente/obtener-cliente/{id}

🔹 Obtener todos los clientes (con filtros opcionales)
GET /api/cliente/obtener-clientes?fechaInicio=2025-12-01&fechaFinal=2025-12-09&ordenarDeZ=true


Parámetros disponibles:

fechaInicio → opcional

fechaFinal → opcional

ordenarDeZ → true / false

🔹 Obtener cliente por email
GET /api/cliente/obtener-cliente-email/{email}

🔹 Crear cliente
POST /api/cliente/crear-cliente


Body:

{
  "nombre": "Juan",
  "email": "juan@gmail.com"
}

🔹 Actualizar cliente
PUT /api/cliente/actualizar-cliente/{id}

🔹 Eliminar cliente
DELETE /api/cliente/eliminar-cliente/{id}

🧪 Validaciones implementadas
DTO de creación:

Nombre obligatorio (máx. 100 caracteres)

Email obligatorio, formato válido, máx. 150 caracteres

Email único validado tanto en BD como en el Service

🧩 Manejo global de errores

Se usa un middleware personalizado:

Captura excepciones no controladas

Genera un log en Serilog

Devuelve respuesta clara al cliente

Formato:

{
  "status": 500,
  "message": "Ocurrió un error inesperado. Intente más tarde."
}

📝 Logging con Serilog

Configurado en appsettings.json para generar:

logs/info-.log

logs/warning-.log

logs/error-.log

Con rotación diaria y niveles separados.

⚙️ Cómo ejecutar el proyecto
1️⃣ Restaurar dependencias
dotnet restore

2️⃣ Crear la base de datos
dotnet ef database update

3️⃣ Ejecutar la API
dotnet run

4️⃣ Abrir Swagger
http://localhost:5000/swagger

🎯 Objetivo educativo

Este proyecto refuerza conceptos esenciales para un Backend .NET Junior, especialmente:

Validaciones correctas

Filtros avanzados

Ordenamiento con LINQ

Patrones de arquitectura limpios

Manejo de errores realista

Buenas prácticas en API REST

Forma parte del Proyecto 4 del plan de 20 proyectos.