using Budgetly.Models;
using Budgetly.Models.DTOs;
using Budgetly.service.interfaces;
using Microsoft.EntityFrameworkCore;
using Budgetly.data;
using Budgetly.Services.Auth;
using Budgetly.service.Email;

namespace Budgetly.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDBContext _context; //instancia del contexto
        private readonly IEmailVerificationService _emailVerificationService;

public UsuarioService(ApplicationDBContext context, IEmailVerificationService emailVerificationService)
{
    _context = context;
    _emailVerificationService = emailVerificationService;
}


        public async Task<UsuarioDTO?> ObtenerPerfilAsync(){
            //obtener al primer usaurio solo hay un unico usuario
            var usuario = await _context.Usuarios.FirstOrDefaultAsync();
            //si no existe que devuelva nulo y si devuelve el objeto
            return usuario == null ? null : new UsuarioDTO
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                CorreoElectronico = usuario.CorreoElectronico,
                Telefono = usuario.Telefono,
                FechaRegistro = usuario.FechaRegistro,
                EsAutenticadoGoogle = usuario.EsAutenticadoGoogle
            };
        } 

        public async Task<UsuarioDTO> CrearAsync(UsuarioCreateDTO usuarioCreateDTO)
{
    var usaurioExistente = await _context.Usuarios
        .FirstOrDefaultAsync(u => u.CorreoElectronico == usuarioCreateDTO.CorreoElectronico);

    if (usaurioExistente != null)
        throw new Exception("Ya existe un usuario con ese correo electrónico.");

    // Verificar que el correo fue validado
    if (!_emailVerificationService.IsEmailVerified(usuarioCreateDTO.CorreoElectronico))
        throw new Exception("El correo electrónico no ha sido verificado.");

    var nuevoUsuario = new Usuario
    {
        NombreUsuario = usuarioCreateDTO.NombreUsuario,
        CorreoElectronico = usuarioCreateDTO.CorreoElectronico,
        PassWordHash = HashPassword(usuarioCreateDTO.Clave),
        Telefono = usuarioCreateDTO.Telefono,
        FechaRegistro = DateTime.UtcNow,
        EsAutenticadoGoogle = false,
    };

    _context.Usuarios.Add(nuevoUsuario);
    await _context.SaveChangesAsync();

    _emailVerificationService.RemoveVerifiedEmail(usuarioCreateDTO.CorreoElectronico); // Limpiar

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
            //verificar que el usuario exista
            var usuario = await _context.Usuarios.FirstOrDefaultAsync();

            if (usuario == null) return false;

            // Actualizar los datos
            usuario.NombreUsuario = dto.NombreUsuario ?? usuario.NombreUsuario;
            usuario.Telefono = dto.Telefono ?? usuario.Telefono;

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
