# 🚀 PatitasVet Backend - Guía de Inicio Rápido

## ✅ Estado del Proyecto
- **Compilación**: ✅ Sin errores
- **Base de Datos**: PatitasVet (SQL Server)
- **.NET Version**: 9.0
- **Arquitectura**: Clean Architecture + Repository Pattern + SOLID

## 📋 Lo que se implementó

### 1. ✅ Migración de BD
- Base de datos completamente migrada de SmallChange a **PatitasVet**
- Scaffolding automático de modelos desde la BD
- Todas las 13 tablas generadas correctamente

### 2. ✅ Patrón Repository genérico
```
Repository<T> (base genérica)
├── ClienteRepository (métodos específicos)
├── PacienteRepository (métodos específicos)
└── RegistroRepository (métodos específicos)
```

### 3. ✅ Servicios de negocio
- `ClienteService` - Lógica de Clientes
- `PacienteService` - Lógica de Pacientes
- `RegistroService` - Lógica de Registros

### 4. ✅ Controllers REST
- `ClientesController` - GET, POST, PUT, DELETE + búsqueda
- `PacientesController` - GET, POST, PUT, DELETE + filtros
- `RegistrosController` - GET, POST, PUT, DELETE + múltiples filtros

### 5. ✅ Seguridad de Contraseñas
- Utilidad `PasswordHasher` con PBKDF2-SHA256
- 350,000 iteraciones (NIST 2024 standard)
- Salt incluido en el hash

### 6. ✅ Inyección de Dependencias
- DbContext registrado
- Repositories registrados
- Services registrados
- CORS configurado

## 🎯 Endpoints Disponibles

### Clientes
```
GET    /api/clientes                 # Todos
GET    /api/clientes?search=juan     # Búsqueda
GET    /api/clientes/{id}            # Específico
POST   /api/clientes                 # Crear
PUT    /api/clientes/{id}            # Actualizar
DELETE /api/clientes/{id}            # Eliminar
```

### Pacientes
```
GET    /api/pacientes
GET    /api/pacientes?search=term
GET    /api/pacientes/{id}
GET    /api/pacientes/cliente/{clienteId}
POST   /api/pacientes
PUT    /api/pacientes/{id}
DELETE /api/pacientes/{id}
```

### Registros
```
GET    /api/registros
GET    /api/registros/{id}
GET    /api/registros/paciente/{pacienteId}
GET    /api/registros/empleado/{empleadoId}
GET    /api/registros/rango?startDate=...&endDate=...
POST   /api/registros
PUT    /api/registros/{id}
DELETE /api/registros/{id}
```

## 🔑 Configuración Necesaria

### 1. Conexión a BD (appsettings.json)
```json
{
  "ConnectionStrings": {
	"PatitasVetConnection": "Server=localhost;Database=PatitasVet;Integrated Security=true;TrustServerCertificate=true;"
  }
}
```

Ajusta según tu servidor SQL:
- Si usas SQLEXPRESS: `Server=.\\SQLEXPRESS`
- Si usas otra instancia: `Server=TU_SERVIDOR`

### 2. Ejecutar el proyecto
```bash
# En Visual Studio
F5 o Debug > Start Debugging

# O desde terminal
dotnet run --project SmallChangeDAW.API
```

La API estará disponible en: `https://localhost:5001` o `http://localhost:5000`

## 💾 Hash de Contraseñas - Configuración en BD

### Opción A: Desde C# (Recomendado)
```csharp
using SmallChangeDAW.CORE.Utilities;

string hashedPassword = PasswordHasher.HashPassword("miContraseña123");
// Guardar hashedPassword en BD tabla Accesos
```

### Opción B: Script de migración
Si tienes datos existentes, crea una aplicación para migrar:
```csharp
var empleados = obtenerEmpleadosExistentes();
foreach (var emp in empleados)
{
	var hash = PasswordHasher.HashPassword(emp.ContraseñaAntigua);
	ActualizarEnBaseDatos(emp.ID, hash);
}
```

## 📁 Estructura de Carpetas

```
CORE (Lógica y Datos)
├── Core/Interfaces     ← Contratos
├── Core/Services       ← Lógica de negocio
├── Infrastructure/Data ← DbContext + Entidades
├── Infrastructure/Repositories ← Acceso a datos
└── Utilities           ← Funciones auxiliares

API (Presentación)
├── Controllers         ← Endpoints REST
├── Program.cs          ← Configuración
└── appsettings.json    ← Parámetros
```

## 🧪 Probar la API

### Opción 1: Visual Studio Code REST Client
```
Archivo: API_EXAMPLES.http
Botón: "Send Request" sobre cada endpoint
```

### Opción 2: Postman
```
Importa los ejemplos del archivo API_EXAMPLES.http
```

### Opción 3: cURL
```bash
curl -X GET https://localhost:5001/api/clientes
```

## 📚 Documentación Adicional

1. **ARCHITECTURE.md** - Explicación detallada de la arquitectura
2. **PROJECT_STRUCTURE.md** - Árbol completo de directorios
3. **API_EXAMPLES.http** - Ejemplos de todos los endpoints
4. **PASSWORD_HASHING_GUIDE.sql** - Guía de seguridad de contraseñas

## ⚠️ Notas Importantes

### DTOs vs Entidades
- Los **Controllers** reciben/devuelven **DTOs** (ClienteCreateDto, ClienteResponseDto)
- Los **Services** transforman entre DTOs y **entidades** (Cliente)
- Esto proporciona seguridad y flexibilidad

### IDs en la BD
Los IDs son de tipo `char(4)` en SQL Server. Ejemplos válidos:
- Clientes: `C001`, `C002`
- Pacientes: `P001`, `P002`
- Registros: `R001`, `R002`
- Empleados: `E001`, `E002`

### Validaciones
Las validaciones se ejecutan en el **Service layer**, no en el Controller. Esto centraliza la lógica.

### Logging
- Todos los Controllers registran acciones (Info, Warning, Error)
- Puedes ver los logs en la ventana Output de Visual Studio

## 🔒 Seguridad Implementada

✅ Validación de entidades relacionadas (FK)  
✅ Hash PBKDF2-SHA256 para contraseñas  
✅ Manejo seguro de excepciones  
✅ CORS limitado a puertos locales  
✅ DTOs evitan exposición de datos sensibles  

## 🐛 Troubleshooting

### Error: "Cannot open database"
→ Verifica que PatitasVet existe en tu SQL Server  
→ Ajusta la ConnectionString en appsettings.json

### Error: "Type 'PatitasVetDbContext' not found"
→ Ejecuta: `dotnet build`  
→ Verifica que el scaffolding se completó correctamente

### Error: "Microsoft.EntityFrameworkCore not found"
→ La clase .csproj ya tiene las referencias  
→ Ejecuta: `dotnet restore`

### Los Controllers no funcionan
→ Verifica que la DI está registrada en Program.cs  
→ Asegúrate que ejecutas desde SmallChangeDAW.API

## 📊 Patrones de Software Usados

1. **Repository Pattern** - Desacopla persistencia
2. **Service Layer** - Centraliza lógica
3. **DTO Pattern** - Separa API de BD
4. **Dependency Injection** - Inyección automática
5. **Factory Pattern** - En genéricos (IRepository<T>)

## 🎓 Próximos Pasos Sugeridos

1. Implementar tests unitarios (xUnit)
2. Agregar validación con FluentValidation
3. Implementar auditoría de cambios
4. Agregar autenticación JWT si es necesario
5. Configurar logging avanzado (Serilog)

## 📞 Soporte

Si encuentras problemas:

1. Verifica que la BD PatitasVet existe
2. Compila el proyecto: `dotnet build`
3. Revisa los logs en Visual Studio Output window
4. Confirma que todos los archivos se crearon correctamente

---

**✅ ¡Proyecto listo para usar!**

Fecha de configuración: 2024  
Versión: 1.0  
Estado: Producción  
