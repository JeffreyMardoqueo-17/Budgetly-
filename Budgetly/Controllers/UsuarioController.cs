using Budgetly.Models.DTOs;
using Budgetly.service.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Budgetly.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // 1. Obtener perfil
        [HttpGet("perfil")]
        public async Task<ActionResult<UsuarioDTO>> ObtenerPerfil()
        {
            try
            {
                var perfil = await _usuarioService.ObtenerPerfilAsync();
                if (perfil == null)
                    return NotFound("No se encontro al usaurio");

                return Ok(perfil);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 2. Crear usuario
        [HttpPost("crear")]
        public async Task<ActionResult<UsuarioDTO>> Crear([FromBody] UsuarioCreateDTO dto)
        {
            try
            {
                var nuevoUsuario = await _usuarioService.CrearAsync(dto);
                return CreatedAtAction(nameof(ObtenerPerfil), new { id = nuevoUsuario.Id }, nuevoUsuario);
                    //    CreatedAtAction(
                    //    nameof(ObtenerPerfil),                 // 1. El nombre del método al que apunta el recurso creado (por convención, el "GET")
                    //    new { id = nuevoUsuario.Id },          // 2. Los parámetros necesarios para construir la URL a ese método
                    //    nuevoUsuario                           // 3. El objeto que se devuelve en el body
                    //)
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 3. Actualizar usuario
        [HttpPut("actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] UsuarioUpdateDTO dto)
        {
            var actualizado = await _usuarioService.ActualizarAsync(dto);
            if (!actualizado)
                return NotFound("Usuario no encontrado para actualizar.");

            return NoContent();
        }

        // 4. Eliminar cuenta
        [HttpDelete("eliminar")]
        public async Task<IActionResult> Eliminar()
        {
            var eliminado = await _usuarioService.EliminarCuentaAsync();
            if (!eliminado)
                return NotFound("No se encontró el usuario para eliminar.");

            return NoContent();
        }

        // 5. Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto dto)
        {
            var esValido = await _usuarioService.ValidarCredencialesAsync(dto);
            if (!esValido)
                return Unauthorized("Credenciales inválidas.");

            return Ok("Login exitoso.");
        }
    }
}
