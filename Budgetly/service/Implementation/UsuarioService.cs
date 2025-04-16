using Budgetly.Models;
using Budgetly.Models.DTOs;
using Budgetly.service.interfaces;
using Microsoft.EntityFrameworkCore;
using Budgetly.data;

namespace Budgetly.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDBContext _context;

        // Inyección de dependencias
        public UsuarioService(ApplicationDBContext context)
        {
            _context = context;
        }

        // 1. Obtener el perfil del único usuario
        public async Task<UsuarioDTO?> ObtenerPerfilAsync()
        {
            // Asumimos que solo hay un usuario
            var usuario = await _context.Usuarios.FirstOrDefaultAsync();

            if (usuario == null) return null;

            // Mapeamos de la entidad Usuario al DTO
            return new UsuarioDTO
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                CorreoElectronico = usuario.CorreoElectronico,
                Telefono = usuario.Telefono,
                FechaRegistro = usuario.FechaRegistro,
                EsAutenticadoGoogle = usuario.EsAutenticadoGoogle
            };
        }

        // 2. Crear el único usuario
        public async Task<UsuarioDTO> CrearAsync(UsuarioCreateDTO dto)
        {
            // Verificar si ya existe el usuario
            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == dto.CorreoElectronico);
            if (usuarioExistente != null) throw new Exception("Ya existe un usuario con ese correo electrónico.");

            // Convertir el DTO a la entidad Usuario
            var nuevoUsuario = new Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                CorreoElectronico = dto.CorreoElectronico,
                PassWordHash = HashPassword(dto.Clave), // Usamos un método de hash
                Telefono = dto.Telefono,
                FechaRegistro = DateTime.UtcNow,
                EsAutenticadoGoogle = false // Inicialmente no está autenticado con Google
            };

            // Agregar el usuario y guardar cambios
            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            // Convertir la entidad creada a DTO para devolverla
            return new UsuarioDTO
            {
                Id = nuevoUsuario.Id,
                NombreUsuario = nuevoUsuario.NombreUsuario,
                CorreoElectronico = nuevoUsuario.CorreoElectronico,
                Telefono = nuevoUsuario.Telefono,
                FechaRegistro = nuevoUsuario.FechaRegistro,
                EsAutenticadoGoogle = nuevoUsuario.EsAutenticadoGoogle
            };
        }

        // 3. Actualizar los datos del usuario
        public async Task<bool> ActualizarAsync(UsuarioUpdateDTO dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync();

            if (usuario == null) return false;

            // Actualizar los datos
            usuario.NombreUsuario = dto.NombreUsuario ?? usuario.NombreUsuario;
            usuario.Telefono = dto.Telefono ?? usuario.Telefono;
            usuario.EsAutenticadoGoogle = dto.EsAutenticadoGoogle ?? usuario.EsAutenticadoGoogle;

            // Guardar cambios
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return true;
        }

        // 4. Eliminar la cuenta del único usuario
        public async Task<bool> EliminarCuentaAsync()
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync();

            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return true;
        }

        // 5. Validar las credenciales del usuario (login)
        public async Task<bool> ValidarCredencialesAsync(UsuarioLoginDto dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.CorreoElectronico == dto.CorreoElectronico);

            if (usuario == null) return false;

            // Validamos la contraseña (compara el hash)
            return VerificarPassword(dto.Clave, usuario.PassWordHash);
        }

        // Métodos auxiliares para manejo de contraseñas
        private string HashPassword(string password)
        {
            // Aquí debes usar un algoritmo de hashing (por ejemplo, bcrypt o SHA-256)
            return password; // En producción usa un hashing real
        }

        private bool VerificarPassword(string password, string hashedPassword)
        {
            // Aquí se debe verificar si la contraseña proporcionada coincide con el hash
            return password == hashedPassword; // En producción usa un método real de verificación
        }
    }
}
