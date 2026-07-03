-- =====================================================
-- GUIDE: Password Hashing for PatitasVet Backend
-- =====================================================

-- La tabla Accesos requiere hash seguro para las contraseñas.
-- Usa el utility PasswordHasher del backend para generar hashes.

-- =====================================================
-- OPCIÓN 1: Usar el Hash desde C# (Recomendado)
-- =====================================================
-- En tu aplicación C#:
/*
using SmallChangeDAW.CORE.Utilities;

string password = "MiContraseña123";
string hashedPassword = PasswordHasher.HashPassword(password);
// Resultado: "350000:BASE64_SALT:BASE64_KEY"

// Insertar en la BD:
// INSERT INTO Accesos (ID, usuario, contra) 
// VALUES ('E001', 'juan.perez', '[hashedPassword]');
*/

-- =====================================================
-- OPCIÓN 2: Verificación de Contraseña
-- =====================================================
/*
string storedHash = "350000:BASE64_SALT:BASE64_KEY";
string inputPassword = "MiContraseña123";
bool isValid = PasswordHasher.VerifyPassword(inputPassword, storedHash);
*/

-- =====================================================
-- CARACTERÍSTICAS DEL HASH
-- =====================================================
-- Algoritmo: PBKDF2-SHA256
-- Iteraciones: 350,000 (NIST 2024 recommendation)
-- Key Size: 512 bits (64 bytes)
-- Formato almacenado: iterations:salt:key (base64)
-- Seguridad: Alta, resistente a ataques de fuerza bruta

-- =====================================================
-- MIGRACIÓN DE ACCESOS EXISTENTES
-- =====================================================
-- Si tienes datos existentes en Accesos que necesitan hash:
-- 1. Lee la contraseña desde la BD
-- 2. Hashéala con PasswordHasher.HashPassword()
-- 3. Actualiza el registro con el hash generado

-- Ejemplo SQL para limpiar tabla:
-- DELETE FROM Accesos WHERE 1=1;

-- Luego inserta nuevos registros con contraseñas hasheadas desde C#
