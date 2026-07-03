# PatitasVet Backend - Estructura y Arquitectura

## 📋 Descripción General
Backend REST API para la clínica veterinaria PatitasVet. Sistema diseñado para que empleados del mostrador registren información de clientes, pacientes (mascotas) y citas/registros de atención.

## 🏗️ Arquitectura de Capas

```
SmallChangeDAW.CORE (Biblioteca de clases)
├── Core/
│   ├── Interfaces/
│   │   ├── IRepository<T>          (Interfaz genérica del patrón Repository)
│   │   ├── IClienteService         (Interfaz de servicio de Clientes)
│   │   ├── IPacienteService        (Interfaz de servicio de Pacientes)
│   │   └── IRegistroService        (Interfaz de servicio de Registros)
│   └── Services/
│       ├── ClienteService          (Lógica de negocio - Clientes)
│       ├── PacienteService         (Lógica de negocio - Pacientes)
│       └── RegistroService         (Lógica de negocio - Registros)
├── Infrastructure/
│   ├── Data/
│   │   ├── PatitasVetDbContext     (DbContext generado automáticamente)
│   │   ├── Cliente.cs              (Entidad Cliente)
│   │   ├── Paciente.cs             (Entidad Paciente)
│   │   ├── Registro.cs             (Entidad Registro)
│   │   └── [Otras entidades...]    (Empleado, Diagnostico, Vacuna, etc.)
│   └── Repositories/
│       ├── Repository<T>           (Clase base genérica)
│       ├── ClienteRepository       (Repo específico - Clientes)
│       ├── PacienteRepository      (Repo específico - Pacientes)
│       └── RegistroRepository      (Repo específico - Registros)
└── Utilities/
	├── PasswordHasher              (Utilidad para hash de contraseñas)
	└── PASSWORD_HASHING_GUIDE.sql  (Guía de uso del hash)

SmallChangeDAW.API (Aplicación Web)
└── Controllers/
	├── ClientesController          (GET, POST, PUT, DELETE)
	├── PacientesController         (GET, POST, PUT, DELETE)
	└── RegistrosController         (GET, POST, PUT, DELETE)
```

## 🎯 Patrones de Software Implementados

### 1. **Repository Pattern**
- **Interfaz**: `IRepository<T>` - Define contrato genérico
- **Implementación**: `Repository<T>` - Clase base reutilizable
- **Repositorios específicos**: `ClienteRepository`, `PacienteRepository`, `RegistroRepository`
- **Beneficio**: Desacopla la lógica de persistencia del resto de la aplicación

### 2. **Dependency Injection (DI)**
- Configurado en `Program.cs` de la API
- Inyección automática de dependencias mediante `IServiceCollection`
- Aplicado a Services y Repositories

### 3. **Service Layer**
- Cada servicio encapsula lógica de negocio
- Transformación entre DTOs y entidades
- Validaciones y reglas de negocio centralizadas

### 4. **Data Transfer Objects (DTOs)**
- Separación entre API (request/response) y entidades de BD
- Seguridad: Exposición controlada de datos
- Flexibilidad: Cambios en DB no afectan contratos de API

## 📡 Endpoints API

### **Clientes**
```
GET    /api/clientes                 # Obtener todos (con filtro opcional)
GET    /api/clientes?search=juan     # Buscar por nombre/apellido
GET    /api/clientes/{id}            # Obtener específico
POST   /api/clientes                 # Crear nuevo
PUT    /api/clientes/{id}            # Actualizar
DELETE /api/clientes/{id}            # Eliminar
```

### **Pacientes**
```
GET    /api/pacientes                # Obtener todos
GET    /api/pacientes?search=peludo  # Buscar por nombre
GET    /api/pacientes/{id}           # Obtener específico
GET    /api/pacientes/cliente/{clienteId}  # Pacientes de un cliente
POST   /api/pacientes                # Crear nuevo
PUT    /api/pacientes/{id}           # Actualizar
DELETE /api/pacientes/{id}           # Eliminar
```

### **Registros**
```
GET    /api/registros                # Obtener todos
GET    /api/registros/{id}           # Obtener específico
GET    /api/registros/paciente/{pacienteId}      # Registros de un paciente
GET    /api/registros/empleado/{empleadoId}     # Registros de un empleado
GET    /api/registros/rango?startDate=...&endDate=...  # Filtrar por fecha
POST   /api/registros                # Crear nuevo
PUT    /api/registros/{id}           # Actualizar
DELETE /api/registros/{id}           # Eliminar
```

## 🔐 Seguridad de Contraseñas

### PasswordHasher Utility
- **Algoritmo**: PBKDF2-SHA256
- **Iteraciones**: 350,000 (NIST 2024)
- **Tamaño de clave**: 512 bits

### Uso:
```csharp
using SmallChangeDAW.CORE.Utilities;

// Generar hash:
string hashedPassword = PasswordHasher.HashPassword("contraseña123");

// Verificar contraseña:
bool isValid = PasswordHasher.VerifyPassword("contraseña123", hashedPassword);
```

### En la BD:
La tabla `Accesos` almacena el hash en formato: `iterations:salt:key`

## 📋 DTOs Principales

### ClienteCreateDto / ClienteUpdateDto
Campos: nombre, apellidos, fecha_nacimiento, genero, tipo_doc, num_documento, direccion, telefono, correo, ciudad, distrito, estado_cliente

### PacienteCreateDto / PacienteUpdateDto
Campos: nombre, apellidos, tipo, fecha_nacimiento, sexo, color, esterilizado, longitud, altura, peso, morfologia, grupo_sanguineo, microchip, tatuaje, observaciones

### RegistroCreateDto / RegistroUpdateDto
Campos: fecha, hora, id_mascota, id_empleado

## ⚙️ Configuración

### appsettings.json
```json
{
  "ConnectionStrings": {
	"PatitasVetConnection": "Server=localhost;Database=PatitasVet;Integrated Security=true;TrustServerCertificate=true;"
  }
}
```

### Program.cs - Inyección de Dependencias
```csharp
// DbContext
builder.Services.AddDbContext<PatitasVetDbContext>(options =>
	options.UseSqlServer(connectionString));

// Repositories
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<PacienteRepository>();
builder.Services.AddScoped<RegistroRepository>();

// Services
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IRegistroService, RegistroService>();
```

## 📊 Entidades de la BD

- **Cliente**: Propietarios de mascotas
- **Paciente**: Mascotas (relacionadas con Cliente)
- **Empleado**: Personal de la veterinaria
- **Registro**: Visitas/citas (relacionadas con Paciente y Empleado)
- **Diagnostico**: Diagnósticos de visitas
- **Vacunas**: Vacunas aplicadas a pacientes
- **Estetica**: Servicios estéticos
- **Accesos**: Control de acceso (usuario/contraseña)
- **Tipos_Mascota**: Catálogo de tipos
- **Tipos_Vacuna**: Catálogo de vacunas
- **Tipos_Diagnostico**: Catálogo de diagnósticos
- **Concepto**: Catálogo de conceptos

## 🚀 Buenas Prácticas Implementadas

✅ **Separación de responsabilidades** - Core, Infrastructure, API  
✅ **Inyección de dependencias** - Services y Repositories  
✅ **DTOs** - Contrato de API separado de entidades  
✅ **Repository Pattern** - Acceso a datos desacoplado  
✅ **Validaciones** - En el Service layer  
✅ **Logging** - En Controllers y Services  
✅ **Manejo de excepciones** - Try-catch con responses adecuadas  
✅ **Comentarios XML** - Documentación en métodos públicos  
✅ **Hash de contraseñas** - PBKDF2-SHA256 seguro  
✅ **CORS configurado** - Para frontend  

## 🔄 Ciclo de una Solicitud

1. Cliente HTTP → `GET /api/clientes/C001`
2. Llega a `ClientesController.GetById()`
3. Llama a `IClienteService.GetByIdAsync()`
4. El Service usa `ClienteRepository.GetByIdAsync()`
5. El Repository consulta `DbContext` (EF Core)
6. Resultado mapeado a `ClienteResponseDto`
7. Respuesta JSON al cliente

## 📝 Próximos Pasos (Opcional)

- Agregar autenticación/autorización si es necesaria
- Implementar auditoría de cambios
- Agregar validaciones más complejas
- Crear migrations para cambios futuros de BD
- Tests unitarios con xUnit

---

**Versión**: 1.0  
**Framework**: .NET 9  
**Base de Datos**: SQL Server (PatitasVet)  
**Arquitectura**: Clean Architecture con Repository Pattern
