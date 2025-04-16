using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budgetly.Models.DTOs;

namespace Budgetly.service.interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO?> ObtenerPerfilAsync(); // Esto es personal asiq ue solo debolvera su perfil
        Task<UsuarioDTO> CrearAsync(UsuarioCreateDTO dto);
        Task<bool> ActualizarAsync(UsuarioUpdateDTO dto);
        Task<bool> EliminarCuentaAsync();
        Task<bool> ValidarCredencialesAsync(UsuarioLoginDto dto);
    }
}