using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budgetly.Models.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string? NombreUsuario { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool EsAutenticadoGoogle { get; set; }

    }
}