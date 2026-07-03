# 📁 Ubicación de DTOs en PatitasVet Backend

## ✅ DTOs Correctamente Organizados

Todos los Data Transfer Objects (DTOs) están centralizados en:

```
SmallChangeDAW.CORE/
└── Core/
	└── DTOs/
		├── ClienteDtos.cs          ← DTOs de Cliente
		├── PacienteDtos.cs         ← DTOs de Paciente
		└── RegistroDtos.cs         ← DTOs de Registro
```

---

## 📋 DTOs por Archivo

### 1. **ClienteDtos.cs**
```csharp
namespace SmallChangeDAW.CORE.Core.DTOs;

public class ClienteResponseDto
public class ClienteCreateDto
public class ClienteUpdateDto
```

**Uso**: 
- Request: `ClienteCreateDto`, `ClienteUpdateDto`
- Response: `ClienteResponseDto`

---

### 2. **PacienteDtos.cs**
```csharp
namespace SmallChangeDAW.CORE.Core.DTOs;

public class PacienteResponseDto
public class PacienteCreateDto
public class PacienteUpdateDto
```

**Uso**:
- Request: `PacienteCreateDto`, `PacienteUpdateDto`
- Response: `PacienteResponseDto`

---

### 3. **RegistroDtos.cs**
```csharp
namespace SmallChangeDAW.CORE.Core.DTOs;

public class RegistroResponseDto
public class RegistroCreateDto
public class RegistroUpdateDto
```

**Uso**:
- Request: `RegistroCreateDto`, `RegistroUpdateDto`
- Response: `RegistroResponseDto`

---

## 🔄 Cómo se Importan en Otros Archivos

### En los Controllers (API)
```csharp
using SmallChangeDAW.CORE.Core.DTOs;

[HttpPost]
public async Task<ActionResult<ClienteResponseDto>> Create(ClienteCreateDto dto)
{
	// Los DTOs vienen desde Core/DTOs
}
```

### En los Services (Lógica)
```csharp
using SmallChangeDAW.CORE.Core.DTOs;

public class ClienteService : IClienteService
{
	public async Task<ClienteResponseDto> CreateAsync(ClienteCreateDto dto)
	{
		// Los DTOs transforman entre API y BD
	}
}
```

### En las Interfaces (Contratos)
```csharp
using SmallChangeDAW.CORE.Core.DTOs;

public interface IClienteService
{
	Task<ClienteResponseDto?> GetByIdAsync(string id);
	Task<ClienteResponseDto> CreateAsync(ClienteCreateDto dto);
	Task<ClienteResponseDto> UpdateAsync(string id, ClienteUpdateDto dto);
}
```

---

## 🏗️ Flujo de un DTO en la Aplicación

```
Frontend (JSON)
	  ↓
ClienteCreateDto (deserialización)
	  ↓
ClientesController.Create()
	  ↓
ClienteService.CreateAsync()
	  ↓
ClienteRepository.CreateAsync()
	  ↓
DbContext.SaveChangesAsync()
	  ↓
SQL Server (BD PatitasVet)
	  ↓
Cliente (Entidad)
	  ↓
ClienteRepository.GetByIdAsync()
	  ↓
ClienteService (mapeo a DTO)
	  ↓
ClienteResponseDto
	  ↓
Frontend (JSON Response)
```

---

## 📝 Ejemplo Completo: Crear un Cliente

### 1️⃣ Frontend envía JSON
```json
{
  "id": "C001",
  "nombre": "María",
  "apellidos": "García",
  "telefono": "999888777"
}
```

### 2️⃣ Se deserializa a `ClienteCreateDto`
```csharp
// SmallChangeDAW.CORE/Core/DTOs/ClienteDtos.cs
public class ClienteCreateDto
{
	public string Id { get; set; }
	public string? Nombre { get; set; }
	public string? Apellidos { get; set; }
	public string? Telefono { get; set; }
	// ... más campos
}
```

### 3️⃣ Controller lo recibe
```csharp
// SmallChangeDAW.API/Controllers/ClientesController.cs
[HttpPost]
public async Task<ActionResult<ClienteResponseDto>> Create(ClienteCreateDto dto)
{
	var result = await _service.CreateAsync(dto);
	return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
}
```

### 4️⃣ Service procesa y mapea
```csharp
// SmallChangeDAW.CORE/Core/Services/ClienteService.cs
public async Task<ClienteResponseDto> CreateAsync(ClienteCreateDto dto)
{
	var cliente = new Cliente
	{
		Id = dto.Id,
		Nombre = dto.Nombre,
		// ... mapeo
	};
	await _repository.CreateAsync(cliente);
	return MapToResponseDto(cliente); // ← Convierte a ClienteResponseDto
}
```

### 5️⃣ Se devuelve `ClienteResponseDto`
```json
{
  "id": "C001",
  "nombre": "María",
  "apellidos": "García",
  "telefono": "999888777",
  "correo": "maria@example.com",
  "direccion": "Av. Central 456",
  "ciudad": "Lima",
  "distrito": "Miraflores",
  "fechaRegistro": "2024-01-15",
  "estadoCliente": "A"
}
```

---

## 🎯 Por Qué Esta Estructura

### ✅ Ventajas de Tener DTOs en `/Core/DTOs`

1. **Centralización** - Todos los DTOs en un lugar
2. **Facilidad de Mantenimiento** - Cambios en un solo lugar
3. **Reutilización** - Accesibles desde Controllers y Services
4. **Seguridad** - No expone entidades de BD directamente
5. **Flexibilidad** - Puedes cambiar BD sin tocar contratos de API
6. **Documentación** - Claridad en qué datos recibe/devuelve cada endpoint

---

## 📊 Resumen de DTOs

| DTO | Ubicación | Uso |
|-----|-----------|-----|
| ClienteCreateDto | Core/DTOs/ClienteDtos.cs | POST request |
| ClienteUpdateDto | Core/DTOs/ClienteDtos.cs | PUT request |
| ClienteResponseDto | Core/DTOs/ClienteDtos.cs | GET response |
| PacienteCreateDto | Core/DTOs/PacienteDtos.cs | POST request |
| PacienteUpdateDto | Core/DTOs/PacienteDtos.cs | PUT request |
| PacienteResponseDto | Core/DTOs/PacienteDtos.cs | GET response |
| RegistroCreateDto | Core/DTOs/RegistroDtos.cs | POST request |
| RegistroUpdateDto | Core/DTOs/RegistroDtos.cs | PUT request |
| RegistroResponseDto | Core/DTOs/RegistroDtos.cs | GET response |

---

## ✨ Estructura Actual (Correcta)

```
SmallChangeDAW.CORE/
├── Core/
│   ├── DTOs/              ← ✅ Todos los DTOs aquí
│   │   ├── ClienteDtos.cs
│   │   ├── PacienteDtos.cs
│   │   └── RegistroDtos.cs
│   │
│   ├── Interfaces/        ← ✅ Solo interfaces (sin DTOs)
│   │   ├── IRepository.cs
│   │   ├── IClienteService.cs
│   │   ├── IPacienteService.cs
│   │   └── IRegistroService.cs
│   │
│   └── Services/          ← ✅ Usa DTOs desde Core/DTOs
│       ├── ClienteService.cs
│       ├── PacienteService.cs
│       └── RegistroService.cs
```

---

## 🎓 Importaciones Necesarias

Para usar DTOs en cualquier archivo:

```csharp
using SmallChangeDAW.CORE.Core.DTOs;  // ← Necesaria en Controllers y Services
```

---

**✅ DTOs correctamente organizados y listos para usar**

Ubicación: `SmallChangeDAW.CORE/Core/DTOs/`
Archivos: 3 (ClienteDtos.cs, PacienteDtos.cs, RegistroDtos.cs)
Total DTOs: 9 clases
