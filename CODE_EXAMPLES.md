# PatitasVet Backend - Ejemplos de Código

## 📝 Ejemplos Prácticos de Uso

### 1. Crear un nuevo cliente

**Desde Frontend (HTTP Request):**
```javascript
POST /api/clientes
Content-Type: application/json

{
  "id": "C001",
  "nombre": "María",
  "apellidos": "García López",
  "fechaNacimiento": "1990-05-15",
  "genero": "F",
  "tipoDoc": "D",
  "numDocumento": "87654321",
  "direccion": "Av. Central 456",
  "telefono": "999888777",
  "correo": "maria@example.com",
  "ciudad": "Lima",
  "distrito": "Miraflores",
  "estadoCliente": "A"
}
```

**Respuesta (201 Created):**
```json
{
  "id": "C001",
  "nombre": "María",
  "apellidos": "García López",
  "numDocumento": "87654321",
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

### 2. Registrar un paciente

**Request:**
```javascript
POST /api/pacientes
Content-Type: application/json

{
  "id": "P001",
  "idTitular": "C001",
  "nombre": "Firulais",
  "apellidos": "García",
  "tipo": "T001",
  "fechaNacimiento": "2021-03-20",
  "sexo": "M",
  "color": "Café",
  "esterilizado": false,
  "longitud": 55,
  "altura": 35,
  "peso": 20,
  "morfologia": "Mediano",
  "grupoSanguineo": "DEA",
  "microchip": true,
  "tatuaje": false,
  "observaciones": "Perro dócil, buena salud"
}
```

---

### 3. Crear un registro de cita

**Request:**
```javascript
POST /api/registros
Content-Type: application/json

{
  "id": "R001",
  "fecha": "2024-01-20",
  "hora": "10:30:00",
  "idMascota": "P001",
  "idEmpleado": "E001"
}
```

---

### 4. Buscar clientes

**Request:**
```javascript
GET /api/clientes?search=María
```

**Respuesta:**
```json
[
  {
	"id": "C001",
	"nombre": "María",
	"apellidos": "García López",
	"numDocumento": "87654321",
	"telefono": "999888777",
	"correo": "maria@example.com",
	"direccion": "Av. Central 456",
	"ciudad": "Lima",
	"distrito": "Miraflores",
	"fechaRegistro": "2024-01-15",
	"estadoCliente": "A"
  }
]
```

---

### 5. Obtener pacientes de un cliente

**Request:**
```javascript
GET /api/pacientes/cliente/C001
```

**Respuesta:**
```json
[
  {
	"id": "P001",
	"idTitular": "C001",
	"nombre": "Firulais",
	"apellidos": "García",
	"tipo": "T001",
	"fechaNacimiento": "2021-03-20",
	"fechaFallecimiento": null,
	"sexo": "M",
	"color": "Café",
	"esterilizado": false,
	"longitud": 55,
	"altura": 35,
	"peso": 20,
	"morfologia": "Mediano",
	"grupoSanguineo": "DEA",
	"microchip": true,
	"tatuaje": false,
	"observaciones": "Perro dócil, buena salud"
  },
  {
	"id": "P002",
	"idTitular": "C001",
	"nombre": "Misa",
	"apellidos": "García",
	"tipo": "T002",
	"fechaNacimiento": "2019-07-10",
	...
  }
]
```

---

### 6. Actualizar información de cliente

**Request:**
```javascript
PUT /api/clientes/C001
Content-Type: application/json

{
  "nombre": "María Carmen",
  "telefono": "999888778",
  "ciudad": "San Isidro"
}
```

**Respuesta (200 OK):**
```json
{
  "id": "C001",
  "nombre": "María Carmen",
  "apellidos": "García López",
  "numDocumento": "87654321",
  "telefono": "999888778",
  "correo": "maria@example.com",
  "direccion": "Av. Central 456",
  "ciudad": "San Isidro",
  "distrito": "Miraflores",
  "fechaRegistro": "2024-01-15",
  "estadoCliente": "A"
}
```

---

### 7. Registros por rango de fechas

**Request:**
```javascript
GET /api/registros/rango?startDate=2024-01-01&endDate=2024-01-31
```

**Respuesta:**
```json
[
  {
	"id": "R001",
	"fecha": "2024-01-20",
	"hora": "10:30:00",
	"idMascota": "P001",
	"idEmpleado": "E001"
  },
  {
	"id": "R002",
	"fecha": "2024-01-22",
	"hora": "14:15:00",
	"idMascota": "P002",
	"idEmpleado": "E002"
  }
]
```

---

### 8. Hash de contraseña (desde código C#)

```csharp
using SmallChangeDAW.CORE.Utilities;

// En tu servicio de empleados o login:
string password = "MiContraseña123!";

// Generar hash
string hashedPassword = PasswordHasher.HashPassword(password);
Console.WriteLine(hashedPassword);
// Output: 350000:BASE64_SALT:BASE64_KEY

// Guardar en BD tabla Accesos
// INSERT INTO Accesos (ID, usuario, contra) VALUES ('E001', 'juan.perez', 'hashedPassword');

// Más tarde, verificar contraseña al login:
string inputPassword = "MiContraseña123!";
string storedHash = "350000:BASE64_SALT:BASE64_KEY"; // Desde BD

bool isValid = PasswordHasher.VerifyPassword(inputPassword, storedHash);
if (isValid)
{
	Console.WriteLine("Login exitoso!");
}
```

---

## 🔄 Flujo Completo: Registrar un Cliente y su Mascota

### Paso 1: Crear Cliente
```bash
POST /api/clientes
{
  "id": "C123",
  "nombre": "Carlos",
  "apellidos": "Rodríguez",
  "telefono": "945123456",
  "correo": "carlos@email.com",
  "direccion": "Calle Flores 789",
  "ciudad": "Lima",
  "distrito": "Surco",
  "estadoCliente": "A"
}
```

### Paso 2: Crear Paciente (Mascota) vinculada al Cliente
```bash
POST /api/pacientes
{
  "id": "P123",
  "idTitular": "C123",
  "nombre": "Beethoven",
  "tipo": "T001",
  "fechaNacimiento": "2022-06-15",
  "sexo": "M",
  "color": "Blanco y Negro",
  "peso": 30,
  "altura": 50,
  "esterilizado": false
}
```

### Paso 3: Registrar Cita
```bash
POST /api/registros
{
  "id": "R123",
  "fecha": "2024-02-10",
  "hora": "15:00:00",
  "idMascota": "P123",
  "idEmpleado": "E001"
}
```

### Paso 4: Consultar Historial
```bash
GET /api/registros/paciente/P123
```

---

## 💻 JavaScript/Frontend - Consumir API

```javascript
// Crear cliente
async function crearCliente(clienteData) {
  const response = await fetch('http://localhost:5000/api/clientes', {
	method: 'POST',
	headers: { 'Content-Type': 'application/json' },
	body: JSON.stringify(clienteData)
  });
  return await response.json();
}

// Obtener clientes
async function obtenerClientes(search = '') {
  const url = search 
	? `http://localhost:5000/api/clientes?search=${search}`
	: 'http://localhost:5000/api/clientes';

  const response = await fetch(url);
  return await response.json();
}

// Actualizar cliente
async function actualizarCliente(id, datos) {
  const response = await fetch(`http://localhost:5000/api/clientes/${id}`, {
	method: 'PUT',
	headers: { 'Content-Type': 'application/json' },
	body: JSON.stringify(datos)
  });
  return await response.json();
}

// Uso:
(async () => {
  const nuevoCliente = await crearCliente({
	id: "C999",
	nombre: "Juan",
	apellidos: "Pérez",
	telefono: "987654321",
	correo: "juan@email.com",
	ciudad: "Lima",
	estadoCliente: "A"
  });
  console.log("Cliente creado:", nuevoCliente);
})();
```

---

## ⚠️ Manejo de Errores

### Error 400: Datos inválidos
```json
{
  "message": "El ID del cliente es requerido"
}
```

### Error 404: No encontrado
```json
{
  "message": "Cliente con ID C999 no encontrado"
}
```

### Error 500: Error interno
```json
{
  "message": "Error interno del servidor",
  "details": "Descripción del error"
}
```

---

## 🧪 Testing con Postman

### Colección recomendada:

1. **GET** `/api/clientes` - Obtener todos
2. **POST** `/api/clientes` - Crear uno
3. **GET** `/api/clientes/{id}` - Obtener específico
4. **PUT** `/api/clientes/{id}` - Actualizar
5. **DELETE** `/api/clientes/{id}` - Eliminar

Repetir para pacientes y registros.

---

**✅ ¡Ahora tienes ejemplos completos para integración!**
